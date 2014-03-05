using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.Enums;
using KDSCommon.Interfaces;
using KDSCommon.Interfaces.DAL;
using KDSCommon.Interfaces.Managers;
using KDSCommon.Interfaces.Shinuyim;
using KdsShinuyim.Enums;
using KdsShinuyim.ShinuyImpl;
using Microsoft.Practices.Unity;

namespace KdsShinuyim.FlowManager
{
    public class ShinuyimFlowManager : IShinuyimFlowManager
    {
        private IUnityContainer _container;

        public ShinuyimFlowManager(IUnityContainer container)
        {
            _container = container;
        }

        public FlowShinuyResult ExecShinuyim(int misparIshi, DateTime cardDate, long? btchRequest = null, int? userId = null)
        {
            FlowShinuyResult ShinuyResult=null;
            ShinuyInputData inputData = FillInputData(misparIshi, cardDate, btchRequest, userId);

            List<ShinuyTypes> ShinuyimExecOrderList = GetExecOrderList();
            List<IShinuy> listShinuyim = GetListShinuyim();
            ShinuyimExecOrderList.ForEach(shinuyType => {
             
                var shinuyObj = listShinuyim.SingleOrDefault(shinuy => shinuy.ShinuyType == shinuyType);
                if (shinuyObj != null)
                    shinuyObj.ExecShinuy(inputData);
            });

            SaveDataBase();
            ShinuyResult.IsSuccess = inputData.IsSuccsess;
            return ShinuyResult;
        }

        private void SaveDataBase()
        {
            //clDefinitions.ShinuyKelet(oCollYameyAvodaUpd, oCollSidurimOvdimUpd, oCollSidurimOvdimIns, oCollSidurimOvdimDel, oCollPeilutOvdimUpd, oCollPeilutOvdimIns, oCollPeilutOvdimDel);

            //if (_oCollIdkunRashemet.Count > 0)
            //    clDefinitions.SaveIdkunRashemet(_oCollIdkunRashemet);

            //if (_oCollApprovalErrors.Count > 0)
            //    clDefinitions.UpdateAprrovalErrors(_oCollApprovalErrors);
        }
        private List<IShinuy> GetListShinuyim()
        {
            List<IShinuy> listShinuyim = new List<IShinuy>();

            listShinuyim.Add(_container.Resolve<ShinuyMisparSidur01>());
            listShinuyim.Add(_container.Resolve<ShinuyMisparSidurVisa02>());

            return listShinuyim;
        }
        private List<ShinuyTypes> GetExecOrderList()
        {
            List<ShinuyTypes> listShinuyim = new List<ShinuyTypes>();

            listShinuyim.Add(ShinuyTypes.ShinuyMisparSidur01);
            listShinuyim.Add(ShinuyTypes.ShinuyMisparSidurVisa02);

            return listShinuyim;
        }

        private ShinuyInputData FillInputData(int misparIshi, DateTime cardDate, long? btchRequest, int? userId)
        {
            var inputData = new ShinuyInputData();

            inputData.iMisparIshi = misparIshi;
            inputData.CardDate = cardDate;

            var cacheManager = _container.Resolve<IKDSCacheManager>();

           // //inputData.SugeyYamimMeyuchadim = cacheManager.GetCacheItem<DataTable>(CachedItems.SugeyYamimMeyuchadim);
           // //inputData.YamimMeyuchadim = cacheManager.GetCacheItem<DataTable>(CachedItems.YamimMeyuhadim);
           //// inputData.iSugYom = DateHelper.GetSugYom(inputData.YamimMeyuchadim, cardDate, inputData.SugeyYamimMeyuchadim);//, _oMeafyeneyOved.GetMeafyen(56).IntValue);
           // inputData.LookUp = cacheManager.GetCacheItem<DataTable>(CachedItems.LookUpTables);

           // inputData.dtMatzavOved = _container.Resolve<IOvedDAL>().GetOvedMatzav(misparIshi, cardDate);
           // inputData.UserId = userId;
           // inputData.BtchRequestId = btchRequest;


            IOvedManager ovedManager = _container.Resolve<IOvedManager>();
           // inputData.OvedDetails = ovedManager.CreateOvedDetails(misparIshi, cardDate);
           // inputData.oMeafyeneyOved = ovedManager.CreateMeafyenyOved(misparIshi, cardDate);

           // IParametersManager paramManager = _container.Resolve<IParametersManager>();
           // inputData.op = paramManager.CreateClsParametrs(cardDate, inputData.iSugYom);

           // if (inputData.dtMatzavOved.Rows.Count > 0)
           // {
           //     inputData.dTarTchilatMatzav = DateTime.Parse(inputData.dtMatzavOved.Rows[0]["TAARICH_HATCHALA"].ToString());
           // }

           
            var ovedDetails = ovedManager.GetOvedDetails(misparIshi, cardDate);
           // inputData.htEmployeeDetails = ovedManager.GetEmployeeDetails(ovedDetails, inputData.CardDate, misparIshi, out iLast);
         
            return inputData;
        }
    }
}
