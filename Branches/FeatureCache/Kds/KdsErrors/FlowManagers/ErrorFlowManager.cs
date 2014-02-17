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
            var flowResult = new FlowErrorResult();
            var inputData = FillInputData(misparIshi, cardDate, btchRequest, userId);

            ExecuteFlow(inputData);

            flowResult.Errors = inputData.dtErrors;
            flowResult.IsSuccess = inputData.IsSuccsess;
            return flowResult;
        }

        private void ExecuteFlow(ErrorInputData inputData)
        {
            ISubErrorFlowManager oYomErrors = _container.Resolve<ISubErrorFlowFactory>().GetFlowManager(SubFlowManagerTypes.OvedYomFlowManager);
            ISubErrorFlowManager oSidurErrors = _container.Resolve<ISubErrorFlowFactory>().GetFlowManager(SubFlowManagerTypes.OvedSidurErrorFlowManager);
            ISubErrorFlowManager oPeilutErrors = _container.Resolve<ISubErrorFlowFactory>().GetFlowManager(SubFlowManagerTypes.OvedPeilutErrorFlowManager);

            oYomErrors.ExecuteFlow(inputData, 1);
            for (int i = 0; i < inputData.htEmployeeDetails.Count; i++)
            {
                inputData.curSidur = (SidurDM)inputData.htEmployeeDetails[i];
                inputData.iSidur = i;
                inputData.drSugSidur = GetOneSugSidurMeafyen(inputData);
                oSidurErrors.ExecuteFlow(inputData, 1);
                for (int j = 0; j < inputData.curSidur.htPeilut.Count; j++)
                {
                    inputData.curPeilut = (PeilutDM)inputData.curSidur.htPeilut[j];
                    inputData.iPeilut = j;
                    oPeilutErrors.ExecuteFlow(inputData,1);
                }
                oSidurErrors.ExecuteFlow(inputData, 2);
            }
            oYomErrors.ExecuteFlow(inputData, 2);
            
        }


        private ErrorInputData FillInputData(int misparIshi, DateTime cardDate, long? btchRequest = null, int? userId = null)
        {
            var inputData = new ErrorInputData();

            inputData.iMisparIshi = misparIshi;
            inputData.CardDate = cardDate;

            var cacheManager = _container.Resolve<IKDSCacheManager>();
         
            inputData.SugeyYamimMeyuchadim =cacheManager.GetCacheItem<DataTable>(CachedItems.SugeyYamimMeyuchadim);
            inputData.YamimMeyuchadim = cacheManager.GetCacheItem<DataTable>(CachedItems.YamimMeyuhadim);
            inputData.iSugYom = clGeneral.GetSugYom(inputData.YamimMeyuchadim, cardDate, inputData.SugeyYamimMeyuchadim);//, _oMeafyeneyOved.GetMeafyen(56).IntValue);
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
            inputData.htEmployeeDetails = ovedManager.GetEmployeeDetails(ovedDetails, inputData.CardDate, misparIshi, out iLast);
            inputData.iLastMisaprSidur = iLast;
            inputData.dtErrors = BuildErrorDataTable();

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

        private DataRow[] GetOneSugSidurMeafyen(ErrorInputData input)
        {
            DataRow[] dr;

            var cacheManager = _container.Resolve<IKDSCacheManager>();
            DataTable sugSidur = cacheManager.GetCacheItem<DataTable>(CachedItems.SugeySidur);

            dr = sugSidur.Select(string.Concat("sug_sidur=", input.curSidur.iSugSidurRagil.ToString(), " and Convert('", input.CardDate.ToShortDateString(), "','System.DateTime') >= me_tarich and Convert('", input.CardDate.ToShortDateString(), "', 'System.DateTime') <= ad_tarich"));
            // dr = dtSugSidur.Select(string.Concat("sug_sidur=", iSugSidur.ToString()));

            return dr;
        }

       
    }
}
