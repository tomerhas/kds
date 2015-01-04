using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KdsErrors.FlowManagers;
using KDSCommon.DataModels;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using KDSCommon.Interfaces;
using KDSCommon.Interfaces.DAL;
using KDSCommon.Interfaces.Errors;
using KDSCommon.Interfaces.Managers;
using KdsLibrary;
using Microsoft.Practices.Unity;
using KdsErrors.DAL;
using KDSCommon.Helpers;
using System.Collections.Specialized;
using KDSCommon.Interfaces.Logs;

namespace KdsErrors.FlowManagers
{
    public class ErrorFlowManager : IErrorFlowManager
    {
        private IUnityContainer _container;

        public ErrorFlowManager(IUnityContainer container)
        {
            _container = container;
        }

        public FlowErrorResult ExecuteFlow(int misparIshi, DateTime cardDate, long? btchRequest = null, int? userId = null)
        {
            
            var inputData = FillInputData(misparIshi, cardDate, btchRequest, userId);

            if (inputData.OvedDetails.bOvedDetailsExists)
                ExecuteFlow(inputData);
    
            return FillResult( inputData);
        }

        private FlowErrorResult FillResult(ErrorInputData inputData)
        {
            FlowErrorResult flowResult = new FlowErrorResult();
            flowResult.Errors = inputData.dtErrors;
            flowResult.IsSuccess = inputData.IsSuccsess;
            flowResult.CardStatus = GetCardStatus(inputData);
            flowResult.htEmployeeDetails = inputData.htEmployeeDetails;
            //flowResult.htFullEmployeeDetails = inputData.htFullEmployeeDetails;
            return flowResult;
        }

        private void ExecuteFlow(ErrorInputData inputData)
        {
            ISubErrorFlowManager oYomErrors = _container.Resolve<ISubErrorFlowFactory>().GetFlowManager(SubFlowManagerTypes.OvedYomFlowManager);
            ISubErrorFlowManager oSidurErrors = _container.Resolve<ISubErrorFlowFactory>().GetFlowManager(SubFlowManagerTypes.OvedSidurErrorFlowManager);
            ISubErrorFlowManager oPeilutErrors = _container.Resolve<ISubErrorFlowFactory>().GetFlowManager(SubFlowManagerTypes.OvedPeilutErrorFlowManager);

            try
            {
                oYomErrors.ExecuteFlow(inputData, 1);
                for (int i = 0; i < inputData.htEmployeeDetails.Count; i++)
                {
                    inputData.curSidur = (SidurDM)inputData.htEmployeeDetails[i];
                    inputData.iSidur = i;    
                    inputData.drSugSidur = _container.Resolve<ISidurManager>().GetOneSugSidurMeafyen(inputData.curSidur.iSugSidurRagil, inputData.CardDate);
                    oSidurErrors.ExecuteFlow(inputData, 1);
                    for (int j = 0; j < inputData.curSidur.htPeilut.Count; j++)
                    {
                        inputData.curPeilut = (PeilutDM)inputData.curSidur.htPeilut[j];
                        inputData.iPeilut = j;
                        oPeilutErrors.ExecuteFlow(inputData, 1);
                    }
                    oSidurErrors.ExecuteFlow(inputData, 2);
                }
                oYomErrors.ExecuteFlow(inputData, 2);

                //if (bSaveChange)
                //{
                    ErrorsDal errDal = _container.Resolve<ErrorsDal>();

                    //Delete errors from shgiot
                    errDal.DeleteErrorsFromTbShgiot(inputData.iMisparIshi, inputData.CardDate);
                    //Remove shgiot meusharot
                    string sArrKodShgia = RemoveShgiotMeusharotFromDt(inputData.dtErrors);
                    bool haveShgiot = false;
                    //Validate shgiot letezuga
                    if (sArrKodShgia.Length > 0)
                    {
                        sArrKodShgia = sArrKodShgia.Substring(0, sArrKodShgia.Length - 1);
                        haveShgiot = errDal.CheckShgiotLetzuga(sArrKodShgia);
                    }
                    //Insert errors to shgiot

                    if (inputData.dtErrors.Rows.Count > 0)
                        errDal.InsertErrorsToTbShgiot(inputData.dtErrors, inputData.CardDate);

                    //Update 
                    errDal.UpdateRitzatShgiotDate(inputData.iMisparIshi, inputData.CardDate, haveShgiot);
                //}
            }
            catch (Exception ex)
            {
                _container.Resolve<ILogBakashot>().InsertLog(inputData.BtchRequestId.HasValue ? inputData.BtchRequestId.Value : 0, "E", 0, "ExecuteErrors: " + ex.Message, inputData.iMisparIshi, inputData.CardDate, null);
                inputData.IsSuccsess = false;
            }
        }

        private string  RemoveShgiotMeusharotFromDt(DataTable dtErrors)
        {
            ErrorLevel iErrorLevel;
            bool bMeushar;
            DataRow dr;
            int iCount;
            iCount = dtErrors.Rows.Count;
            int I = 0;
            try
            {
                ErrorsDal errDal = _container.Resolve<ErrorsDal>();
                string sArrKodShgia = "";
                if (iCount > 0)
                {
                    do
                    {
                        bMeushar = false;
                        dr = dtErrors.Rows[I];
                        if (!(string.IsNullOrEmpty(dr["shat_yetzia"].ToString())))
                        {
                            iErrorLevel = ErrorLevel.LevelPeilut;
                            bMeushar = errDal.IsErrorApprovalExists(iErrorLevel, (int)dr["check_num"], (int)dr["mispar_ishi"], DateTime.Parse(dr["Taarich"].ToString()), (int)dr["mispar_sidur"], DateTime.Parse(dr["shat_hatchala"].ToString()), DateTime.Parse(dr["shat_yetzia"].ToString()), (int)dr["mispar_knisa"]);

                        }
                        else if (string.IsNullOrEmpty(dr["mispar_sidur"].ToString()))
                        {
                            iErrorLevel = ErrorLevel.LevelYomAvoda;
                            bMeushar = errDal.IsErrorApprovalExists(iErrorLevel, (int)dr["check_num"], (int)dr["mispar_ishi"], DateTime.Parse(dr["Taarich"].ToString()), 0, DateTime.MinValue, DateTime.MinValue, 0);

                        }
                        else
                        {
                            iErrorLevel = ErrorLevel.LevelSidur;
                            bMeushar = errDal.IsErrorApprovalExists(iErrorLevel, (int)dr["check_num"], (int)dr["mispar_ishi"], DateTime.Parse(dr["Taarich"].ToString()), (int)dr["mispar_sidur"], DateTime.Parse(dr["shat_hatchala"].ToString()), DateTime.MinValue, 0);
                        }


                        if (bMeushar)
                        {
                            dr.Delete();
                        }
                        else
                        {
                            sArrKodShgia += dr["check_num"].ToString() + ",";
                            I += 1;
                        }

                        iCount = dtErrors.Rows.Count;
                    }
                    while (I < iCount);

                }
                return sArrKodShgia;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// TODO - need to set this function as priveate once 'MainOvedErrors' is removed from code
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public CardStatus GetCardStatus(ErrorInputData inputData)
        {
            CardStatus status = CardStatus.Valid;

            if (inputData.dtErrors.Rows.Count > 0)
            {
                
                status = CardStatus.Error;
            }
            else
            {
                status = CardStatus.Valid;
            }
            if ((int)status != inputData.OvedDetails.iStatus)
            {
                _container.Resolve<IOvedManager>().UpdateCardStatus(inputData.iMisparIshi, inputData.CardDate, status, inputData.UserId.Value);
                
            }
            return status;
        }

        private ErrorInputData FillInputData(int misparIshi, DateTime cardDate, long? btchRequest = null, int? userId = null)
        {
            var inputData = new ErrorInputData();
            OrderedDictionary htFullSidurimDetails = new OrderedDictionary();
            OrderedDictionary htSpecialEmployeeDetails = new OrderedDictionary();

            inputData.iMisparIshi = misparIshi;
            inputData.CardDate = cardDate;

            var cacheManager = _container.Resolve<IKDSCacheManager>();
         
            inputData.SugeyYamimMeyuchadim =cacheManager.GetCacheItem<DataTable>(CachedItems.SugeyYamimMeyuchadim);
            inputData.YamimMeyuchadim = cacheManager.GetCacheItem<DataTable>(CachedItems.YamimMeyuhadim);
            inputData.iSugYom = DateHelper.GetSugYom(inputData.YamimMeyuchadim, cardDate, inputData.SugeyYamimMeyuchadim);//, _oMeafyeneyOved.GetMeafyen(56).IntValue);
            inputData.ErrorsNotActive = cacheManager.GetCacheItem<DataTable>(CachedItems.ErrorTable);
            inputData.LookUp = cacheManager.GetCacheItem<DataTable>(CachedItems.LookUpTables);

            inputData.dtMatzavOved = _container.Resolve<IOvedDAL>().GetOvedMatzav(misparIshi, cardDate);
            inputData.UserId = userId;
            inputData.BtchRequestId = btchRequest;


            IOvedManager ovedManager =  _container.Resolve<IOvedManager>();
            inputData.OvedDetails = ovedManager.CreateOvedDetails(misparIshi, cardDate);
            inputData.oMeafyeneyOved = ovedManager.CreateMeafyenyOved(misparIshi, cardDate);

            IParametersManager paramManager = _container.Resolve<IParametersManager>();
            inputData.oParameters = paramManager.CreateClsParametrs(cardDate, inputData.iSugYom);

            if (inputData.dtMatzavOved.Rows.Count > 0)
            {
                inputData.dTarTchilatMatzav = DateTime.Parse(inputData.dtMatzavOved.Rows[0]["TAARICH_HATCHALA"].ToString());
            }

            int iLast = 0;
            var ovedDetails = ovedManager.GetOvedDetails(misparIshi, cardDate);
            inputData.htEmployeeDetails = ovedManager.GetEmployeeDetails(true,ovedDetails, inputData.CardDate, misparIshi, out iLast,out htSpecialEmployeeDetails,out htFullSidurimDetails);
            inputData.iLastMisaprSidur = iLast;
            inputData.dtErrors = BuildErrorDataTable();

            var shinuyManager = _container.Resolve<IShinuyimManager>();
            inputData.IdkuneyRashemet = shinuyManager.GetIdkuneyRashemet(misparIshi, cardDate);

            return inputData;
        }

        private DataTable BuildErrorDataTable()
        {
            DataTable dtErrors = new DataTable();
            DataColumn col = new DataColumn();
            try
            {
                col = new DataColumn("check_num", System.Type.GetType("System.Int32"));
                dtErrors.Columns.Add(col);

                col = new DataColumn("mispar_ishi", System.Type.GetType("System.Int32"));
                dtErrors.Columns.Add(col);

                col = new DataColumn("taarich", System.Type.GetType("System.DateTime"));
                dtErrors.Columns.Add(col);

                col = new DataColumn("mispar_sidur", System.Type.GetType("System.Int32"));
                dtErrors.Columns.Add(col);

                col = new DataColumn("shat_hatchala", System.Type.GetType("System.DateTime"));
                dtErrors.Columns.Add(col);

                col = new DataColumn("mispar_knisa", System.Type.GetType("System.Int32"));
                dtErrors.Columns.Add(col);

                col = new DataColumn("shat_yetzia", System.Type.GetType("System.DateTime"));
                dtErrors.Columns.Add(col);

                col = new DataColumn("sadot_nosafim", System.Type.GetType("System.Int32"));
                dtErrors.Columns.Add(col);

                col = new DataColumn("makat_nesia", System.Type.GetType("System.Int64"));
                dtErrors.Columns.Add(col);

                //col = new DataColumn("error_desc", System.Type.GetType("System.String"));
                //dtErrors.Columns.Add(col);
                return dtErrors;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

       
    }
}
