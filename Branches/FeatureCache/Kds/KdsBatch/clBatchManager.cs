using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Specialized;
using KdsLibrary.BL;
using System.Collections;
using KdsLibrary.Utils;
using KdsLibrary;
using KdsWorkFlow.Approvals;
using System.Web;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.DataModels;
using KDSCommon.Interfaces;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KDSCommon.Interfaces.Managers;
using KDSCommon.Helpers;
using KDSCommon.Interfaces.DAL;
using KDSCommon.Interfaces.Errors;
using KDSCommon.UDT;
using DalOraInfra.DAL;
using ObjectCompare;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.Interfaces.Shinuyim;

namespace KdsBatch
{
    public class clBatchManager : IDisposable
    {

        public DataTable dtMashar;
        private clParametersDM _oParameters;
        private DataTable _dtLookUp;
        private DataTable _dtYamimMeyuchadim;
        private MeafyenimDM _oMeafyeneyOved;
        private OvedYomAvodaDetailsDM _oOvedYomAvodaDetails;
        private DataTable _dtSugSidur;
        
        private DataTable _dtSugeyYamimMeyuchadim;
        private DataTable _dtDetails;       
        private DataTable _dtMatzavOved;
        private OrderedDictionary _htEmployeeDetails;
        private OrderedDictionary _htFullEmployeeDetails;
        private OrderedDictionary _htSpecialEmployeeDetails;
        private int _iLoginUserId = 0;
        private DataTable _dtErrors;
        private int _iSugYom;
        private int _iMisparIshi;
        private DateTime _dCardDate;
        private bool _IsExecuteErrors = false;
        private long? _btchRequest;
        private int _iUserId;
        private CardStatus _CardStatus = CardStatus.Valid;
  
        


        public clBatchManager(long? btchRequest,int iMisparIshi, DateTime dCardDate)
        {
            _btchRequest = btchRequest;
            _iMisparIshi = iMisparIshi;
            _dCardDate = dCardDate;
            _iUserId = -2;
        }

        public clBatchManager( int iMisparIshi, DateTime dCardDate)
        {
            _iMisparIshi = iMisparIshi;
            _dCardDate = dCardDate;
            _iUserId = -2;
        }

        public clBatchManager(long? btchRequest)
        {
            _btchRequest = btchRequest;
            _iUserId = -2;
        }

        public clBatchManager()
        {

        }

      
        public void InitGeneralData()
        {
            int iSugYom;
            int iLastMisaprSidur=0;
            clUtils oUtils = new clUtils();
            clDefinitions oDefinition = new clDefinitions();
            try
            {
               

                //Get Parameters Table
                //dtParameters = GetKdsParametrs();
                var cache = ServiceLocator.Current.GetInstance<IKDSCacheManager>();

                dtLookUp = cache.GetCacheItem<DataTable>(CachedItems.LookUpTables);
                dtYamimMeyuchadim = cache.GetCacheItem<DataTable>(CachedItems.YamimMeyuhadim);

                _dtSugeyYamimMeyuchadim = cache.GetCacheItem<DataTable>(CachedItems.SugeyYamimMeyuchadim);

                //Get Meafyeny Ovdim
                GetMeafyeneyOvdim(_iMisparIshi, _dCardDate);

                iSugYom = DateHelper.GetSugYom(dtYamimMeyuchadim, _dCardDate, _dtSugeyYamimMeyuchadim);//, _oMeafyeneyOved.GetMeafyen(56).IntValue);

                //Set global variable with parameters
                SetParameters(_dCardDate, iSugYom);

                
                //Get Meafyeney Sug Sidur
                //dtSugSidur = cache.GetCacheItem<DataTable>(CachedItems.SugeySidur);

                SetOvedYomAvodaDetails(_iMisparIshi, _dCardDate);

                if (oOvedYomAvodaDetails!=null)
                {
                    //Get Oved Details
                    dtDetails = oDefinition.GetOvedDetails(_iMisparIshi, _dCardDate);
                    if (dtDetails.Rows.Count > 0)
                    {
                        OrderedDictionary htFullSidurimDetails = new OrderedDictionary();
                        //Insert Oved Details to Class
                        htEmployeeDetails = oDefinition.InsertEmployeeDetails(false, dtDetails, _dCardDate, ref iLastMisaprSidur, out _htSpecialEmployeeDetails, ref htFullSidurimDetails);//, out  _htEmployeeDetailsWithCancled
                        htFullEmployeeDetails = htFullSidurimDetails;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void SetOvedYomAvodaDetails(int iMisparIshi, DateTime dCardDate)
        {
            try
            {

                IOvedManager ovedManager = ServiceLocator.Current.GetInstance<IOvedManager>();
                oOvedYomAvodaDetails = ovedManager.CreateOvedDetails(iMisparIshi, dCardDate);

                _CardStatus = (CardStatus)oOvedYomAvodaDetails.iStatus;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

      
        

        public FlowErrorResult MainOvedErrorsNew(int iMisparIshi, DateTime dCardDate)
        {
            var errorFlowManager = ServiceLocator.Current.GetInstance<IErrorFlowManager>();
            FlowErrorResult errorResults = errorFlowManager.ExecuteFlow(iMisparIshi, dCardDate, 0, 0);
            
            _dtErrors = errorResults.Errors;
            _CardStatus = errorResults.CardStatus;
            return errorResults;
         
        }

        public FlowShinuyResult MainInputDataNew(int iMisparIshi, DateTime dCardDate)
        {
            var shinuyFlowManager = ServiceLocator.Current.GetInstance<IShinuyimFlowManager>();
            FlowShinuyResult shinuyResults = shinuyFlowManager.ExecShinuyim(iMisparIshi, dCardDate, 0, 0);

            return shinuyResults;
        }
      

        private void GetMeafyeneyOvdim(int iMisparIshi, DateTime dCardDate)
        {
            //clOvdim oOvdim = new clOvdim();
            try
            {
                var ovedManager = ServiceLocator.Current.GetInstance<IOvedManager>();
                oMeafyeneyOved = ovedManager.CreateMeafyenyOved(iMisparIshi, dCardDate);
                //oMeafyeneyOved = new clMeafyenyOved(iMisparIshi, dCardDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetParameters(DateTime dCardDate, int iSugYom)
        {
            var cache = ServiceLocator.Current.GetInstance<IKDSAgedQueueParameters>();
            var param = cache.GetItem(dCardDate);
            if (param != null)
                oParam = param;
            else
            {
                IParametersManager paramManager = ServiceLocator.Current.GetInstance<IParametersManager>();
                oParam = paramManager.CreateClsParametrs(dCardDate, iSugYom);
                cache.Add(oParam, dCardDate);
            }

        }

     
        


        public clParametersDM oParam
        {
            set
            {
                _oParameters = value;
            }
            get
            {
                return _oParameters;
            }
        }
        public DataTable dtLookUp
        {
            set
            {
                _dtLookUp = value;
            }
            get
            {
                return _dtLookUp;
            }
        }
        public DataTable dtYamimMeyuchadim
        {
            set
            {
                _dtYamimMeyuchadim = value;
            }
            get
            {
                return _dtYamimMeyuchadim;
            }
        }
        public OvedYomAvodaDetailsDM oOvedYomAvodaDetails
        {
            set
            {
                _oOvedYomAvodaDetails = value;
            }
            get
            {
                return _oOvedYomAvodaDetails;
            }
        }  
        public MeafyenimDM oMeafyeneyOved
        {
            set
            {
                _oMeafyeneyOved = value;
            }
            get
            {
                return _oMeafyeneyOved;
            }
        }      
        public int iSugYom
        {
            set
            {
                _iSugYom = value;
            }
            get
            {
                return _iSugYom;
            }
        }

        public DataTable dtSugSidur
        {
            set
            {
                _dtSugSidur = value;
            }
            get
            {
                return _dtSugSidur;
            }
        }


        public DataTable dtErrors
        {
            set
            {
                _dtErrors = value;
            }
            get
            {
                return _dtErrors;
            }
        }

        public DataTable dtMatzavOved
        {
            set
            {
                _dtMatzavOved = value;
            }
            get
            {
                return _dtMatzavOved;
            }
        }
        public DataTable dtDetails
        {
            set
            {
                _dtDetails = value;
            }
            get
            {
                return _dtDetails;
            }
        }
        public OrderedDictionary htEmployeeDetails
        {
            set
            {
                _htEmployeeDetails = value;
            }
            get
            {
                return _htEmployeeDetails;
            }
        }

        public int iLoginUserId
        {
            set
            {
                _iLoginUserId = value;
            }
            get
            {
                return _iLoginUserId;
            }
        }
        public OrderedDictionary htFullEmployeeDetails
        {
            set
            {
                _htFullEmployeeDetails = value;
            }
            get
            {
                return _htFullEmployeeDetails;
            }
        }

        public CardStatus CardStatus
        {
            set { _CardStatus = value; }
            get { return _CardStatus; }
        }


      
      

        public bool IsExecuteErrors
        {
            set
            {
                _IsExecuteErrors = value;
            }
            get
            {
                return _IsExecuteErrors;
            }
        }
        public int MisparIshi
        {
            set
            {
                _iMisparIshi = value;
            }
            get
            {
                return _iMisparIshi;
            }
        }
        public DateTime CardDate
        {
            set
            {
                _dCardDate = value;
            }
            get
            {
                return _dCardDate;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            //if (_dtApproval != null)
            //    _dtApproval.Dispose();
            if (_dtDetails != null)
                _dtDetails.Dispose();
            if (_dtErrors != null)
                _dtErrors.Dispose();
            if (_dtMatzavOved != null)
                _dtMatzavOved.Dispose();
            //if (_dtSidurimMeyuchadim != null)
            //    _dtSidurimMeyuchadim.Dispose();
            if (_dtSugeyYamimMeyuchadim != null)
                _dtSugeyYamimMeyuchadim.Dispose();
            //if (_dtSugSidur != null)
            //    _dtSugSidur.Dispose();
            if (_dtYamimMeyuchadim != null)
                _dtYamimMeyuchadim.Dispose();
            //if (dtChishuvYom != null)
            //    dtChishuvYom.Dispose();
            //if (dtMashar != null)
            //    dtMashar.Dispose();
            //if (dtMutamut != null)
            //    dtMutamut.Dispose();
            //if (dtSibotLedivuachYadani != null)
            //    dtSibotLedivuachYadani.Dispose();

        }

        #endregion

      
        
    }
}

