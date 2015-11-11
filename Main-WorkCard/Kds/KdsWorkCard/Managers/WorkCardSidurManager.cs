using KDSCommon.DataModels;
using KDSCommon.DataModels.Errors;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.DataModels.WorkCard;
using KDSCommon.Enums;
using KDSCommon.Helpers;
using KDSCommon.Interfaces;
using KDSCommon.Interfaces.DAL;
using KDSCommon.Interfaces.Errors;
using KDSCommon.Interfaces.Managers;
using KDSCommon.Interfaces.Managers.Security;
using KDSCommon.Interfaces.Shinuyim;
using KdsLibrary;
using KdsLibrary.Security;
using KdsWorkCard.Converters;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;

namespace KdsWorkCard.Managers
{
    public class WorkCardSidurManager : IWorkCardSidurManager
    {
        private IUnityContainer _container;

        public WorkCardSidurManager(IUnityContainer container)
        {
            _container = container;
        }

        public SidurimWC GetSidurim(int misparIshi,DateTime cardDate,int userId=-2,int batchRequest =0,bool isFromEmda=false)
        {
            SidurimWC result = new SidurimWC();
            //SetSecurityLevel();
            //PerUserSecurity

            UpdateGlobalSystemParamsCache(cardDate);
            var oOvedYomAvodaDetails = _container.Resolve<IOvedManager>().CreateOvedDetails(misparIshi, cardDate);

            if(RequiredShinuyeeKelet(oOvedYomAvodaDetails))
            {
                //Get the shinuyim
                FlowShinuyResult shinuyResults = _container.Resolve<IShinuyimFlowManager>().ExecShinuyim(misparIshi, cardDate, batchRequest, userId);
                if(shinuyResults.IsSuccess==true)
                {
                    //Continue on to shguim
                    FlowErrorResult errorResults = _container.Resolve<IErrorFlowManager>().ExecuteFlow(misparIshi, cardDate, batchRequest, userId);
                }
            }
            WorkCardResult wcResult = GetWorkCardResult(misparIshi, cardDate);

            WorkCardResultToSidurmWC converter = new WorkCardResultToSidurmWC();
            var profiles = _container.Resolve<ILoginUserManager>().GetLoggedUser().UserProfiles;
            result = converter.Convert(wcResult,userId,profiles);

            return result;
        }
        
        private WorkCardResult GetWorkCardResult(int misparIshi, DateTime cardDate)
        {
            WorkCardResult wcResult = new WorkCardResult();
            int iLast = 0;
            OrderedDictionary htSpecialEmployeeDetails, htFullEmployeeDetails;
            var ovedManager = _container.Resolve<IOvedManager>();
            var ovedDetails = ovedManager.GetOvedDetails(misparIshi, cardDate);

            //WorkCardResult WCResult = new WorkCardResult() { Succeeded = false };
            //if (ovedDetails.Rows.Count > 0)
            //{
            wcResult.htEmployeeDetails = ovedManager.GetEmployeeDetails(false, ovedDetails, cardDate, misparIshi, out iLast, out htSpecialEmployeeDetails, out htFullEmployeeDetails);
            wcResult.htFullEmployeeDetails = htFullEmployeeDetails;
            wcResult.Succeeded = true;
            wcResult.dtMashar = GetMasharData(wcResult.htEmployeeDetails);
            //}
            wcResult.oParam = _container.Resolve<IKDSAgedQueueParameters>().GetItem(cardDate);

            var cache = _container.Resolve<IKDSCacheManager>();
            wcResult.dtLookUp = cache.GetCacheItem<DataTable>(CachedItems.LookUpTables);
            wcResult.dtSugSidur = cache.GetCacheItem<DataTable>(CachedItems.SugeySidur);


            wcResult.oOvedYomAvodaDetails = ovedManager.CreateOvedDetails(misparIshi, cardDate);
            wcResult.oMeafyeneyOved = ovedManager.CreateMeafyenyOved(misparIshi, cardDate);
            wcResult.dtDetails = ovedManager.GetOvedDetails(misparIshi, cardDate);

            return wcResult;
        }

        private DataTable GetMasharData(OrderedDictionary htEmployeeDetails)
        {
            string sCarNumbers = "";
            DataTable dtLicenseNumber = new DataTable();


            sCarNumbers = _container.Resolve<IKavimManager>().GetMasharCarNumbers(htEmployeeDetails);

            if (sCarNumbers != string.Empty)
            {
                dtLicenseNumber = _container.Resolve<IKavimDAL>().GetMasharData(sCarNumbers);
            }
            return dtLicenseNumber;
        }

        private bool RequiredShinuyeeKelet(OvedYomAvodaDetailsDM ovedDetails)
        {
            return ovedDetails.iBechishuvSachar != (int)clGeneral.enBechishuvSachar.bsActive;
        }

        private void UpdateGlobalSystemParamsCache(DateTime dCardDate)
        {

            var cache = _container.Resolve<IKDSCacheManager>();
            var dtYamimMeyuchadim = cache.GetCacheItem<DataTable>(CachedItems.YamimMeyuhadim);
            var dtSugeyYamimMeyuchadim = cache.GetCacheItem<DataTable>(CachedItems.SugeyYamimMeyuchadim);
            int iSugYom = DateHelper.GetSugYom(dtYamimMeyuchadim, dCardDate, dtSugeyYamimMeyuchadim);//, _oMeafyeneyOved.GetMeafyen(56).IntValue);

            var cacheAge = _container.Resolve<IKDSAgedQueueParameters>();
            var param = cacheAge.GetItem(dCardDate);
            if (param == null)
            {
                IParametersManager paramManager = _container.Resolve<IParametersManager>();
                var oParam = paramManager.CreateClsParametrs(dCardDate, iSugYom);
                cacheAge.Add(oParam, dCardDate);
            }

        }
    }
}
