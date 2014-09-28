using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.Enums;
using KDSCommon.Helpers;
using KDSCommon.Interfaces;
using KDSCommon.Interfaces.DAL;
using KDSCommon.Interfaces.Logs;
using KDSCommon.Interfaces.Managers;
using KDSCommon.Interfaces.Shinuyim;
using KDSCommon.UDT;
using KdsShinuyim.Enums;
using KdsShinuyim.ShinuyImpl;
using Microsoft.Practices.Unity;
using ObjectCompare;


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
             ShinuyInputData inputData = null;
            try
            {
                 inputData = FillInputData(misparIshi, cardDate, btchRequest, userId);

                 if (inputData.OvedDetails.bOvedDetailsExists)
                 {
                     List<ShinuyTypes> ShinuyimExecOrderList = GetExecOrderList();
                     List<IShinuy> listShinuyim = GetListShinuyim();
                     ShinuyimExecOrderList.ForEach(shinuyType =>
                     {
                         var shinuyObj = listShinuyim.SingleOrDefault(shinuy => shinuy.ShinuyType == shinuyType);
                         if (shinuyObj != null)
                             try
                             {
                                 shinuyObj.ExecShinuy(inputData);
                             }
                             catch (Exception ex)
                             {
                                 AddLogErrorToDb(inputData, shinuyObj.ShinuyType, ex);
                                 inputData.IsSuccsess = false;
                             }
                     });

                     SaveDataBase(inputData);
                 }
            }
            catch (Exception ex)
            {
                _container.Resolve<ILogBakashot>().InsertLog(btchRequest.HasValue ? btchRequest.Value : 0, "E",0,"ExecShinuyim: " + ex.Message,misparIshi, cardDate, null);
                 inputData.IsSuccsess = false;
            }
            return FillResult(inputData);
        }

        private FlowShinuyResult FillResult( ShinuyInputData inputData)
        {
            FlowShinuyResult ShinuyResult = new FlowShinuyResult();
            ShinuyResult.IsSuccess = inputData.IsSuccsess;
            ShinuyResult.bHaveCount = inputData.bHaveCount;
          
            return ShinuyResult;
        }

        protected void AddLogErrorToDb(ShinuyInputData input,ShinuyTypes ShinuyType, Exception ex)
        {
            long btchId = 0;
            if (input.BtchRequestId.HasValue)
                btchId = input.BtchRequestId.Value;


            _container.Resolve<ILogBakashot>().InsertLog(btchId, "E", (int)ShinuyType, ShinuyType.ToString() + ": " + ex.Message, input.iMisparIshi, input.CardDate);

        }
        private void SaveDataBase(ShinuyInputData inputData)
        {
            try
            {
                var shinuyimManager = _container.Resolve<IShinuyimManager>();

                shinuyimManager.SaveShinuyKelet(inputData);

                if (inputData.oCollIdkunRashemet.Count > 0)
                    shinuyimManager.SaveIdkunRashemet(inputData.oCollIdkunRashemet);

                if (inputData.oCollIdkunRashemetDel.Count > 0)
                    shinuyimManager.DeleteIdkunRashemet(inputData.oCollIdkunRashemetDel);

                if (inputData.oCollApprovalErrors.Count > 0)
                    shinuyimManager.UpdateAprrovalErrors(inputData.oCollApprovalErrors);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<IShinuy> GetListShinuyim()
        {
            try
            {
                List<IShinuy> listShinuyim = new List<IShinuy>();

                listShinuyim.Add(_container.Resolve<DeleteSidureyRetzifut>());
                listShinuyim.Add(_container.Resolve<IpusArachimBeforeTahalich>());
                listShinuyim.Add(_container.Resolve<ShinuyMisparSidur01>());
                listShinuyim.Add(_container.Resolve<ShinuyMisparSidurVisa02>());
                listShinuyim.Add(_container.Resolve<ShinuyMergerSiduryMapa28>());
                listShinuyim.Add(_container.Resolve<ShinuyAddElementMechine05>());
                listShinuyim.Add(_container.Resolve<ShinuyFixedShatGmar10>());
                listShinuyim.Add(_container.Resolve<ShinuySetSidurLoLetashlum11>());
                listShinuyim.Add(_container.Resolve<ShinuySidureyMapaWhithStatusNullLoLetashlum29>());
                listShinuyim.Add(_container.Resolve<ShinuyFixedPitzulHafsaka06>());
                listShinuyim.Add(_container.Resolve<ShinuyChishuvShatHatchala30>());
                listShinuyim.Add(_container.Resolve<ShinuyFixedSidurHours08>());
                listShinuyim.Add(_container.Resolve<ShinuyFixedItyatzvutNahag23>());
                listShinuyim.Add(_container.Resolve<ShinuyimOutMichsaHashlamaChariga>());
                listShinuyim.Add(_container.Resolve<DeleteElementRechev07>());
                listShinuyim.Add(_container.Resolve<ShinuyShatHatchalaLefiShatItyatzvut12>());
                listShinuyim.Add(_container.Resolve<ShinuyFixedShatHamtana25>());
                listShinuyim.Add(_container.Resolve<ShinuyShatHatchalaGmar_19_26_27>());
                listShinuyim.Add(_container.Resolve<SetBooleanParams>());
                listShinuyim.Add(_container.Resolve<ShinuyHosafatSidurHeadrutWithPaymeny15>());
                listShinuyim.Add(_container.Resolve<ShinuyUpdateTachograph>());
                listShinuyim.Add(_container.Resolve<ShinuyHashlamaForYomAvoda>());
                listShinuyim.Add(_container.Resolve<ShinuyUpdateBitulZmanNesiot>());
                listShinuyim.Add(_container.Resolve<ShinuyKonnutGrira03>());
                listShinuyim.Add(_container.Resolve<ShinuyFixedLina07>());
                listShinuyim.Add(_container.Resolve<SetSidurObjects>());
                listShinuyim.Add(_container.Resolve<ShinuyUpdateHalbasha>());

                return listShinuyim;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<ShinuyTypes> GetExecOrderList()
        {
            List<ShinuyTypes> listShinuyim = new List<ShinuyTypes>();

            try
            {
                listShinuyim.Add(ShinuyTypes.DeleteSidureyRetzifut);
                listShinuyim.Add(ShinuyTypes.IpusArachimBeforeTahalich);
                listShinuyim.Add(ShinuyTypes.ShinuyMisparSidur01);
                listShinuyim.Add(ShinuyTypes.ShinuyMisparSidurVisa02);
                listShinuyim.Add(ShinuyTypes.ShinuyMergerSiduryMapa28);
                listShinuyim.Add(ShinuyTypes.ShinuyAddElementMechine05);
                listShinuyim.Add(ShinuyTypes.ShinuyFixedShatGmar10);
                listShinuyim.Add(ShinuyTypes.ShinuySetSidurLoLetashlum11);
                listShinuyim.Add(ShinuyTypes.ShinuySidureyMapaWhithStatusNullLoLetashlum29);
                listShinuyim.Add(ShinuyTypes.ShinuyFixedPitzulHafsaka06);
                listShinuyim.Add(ShinuyTypes.ShinuyChishuvShatHatchala30);
                listShinuyim.Add(ShinuyTypes.ShinuyFixedSidurHours08);
                listShinuyim.Add(ShinuyTypes.ShinuyFixedItyatzvutNahag23);
                listShinuyim.Add(ShinuyTypes.ShinuyimOutMichsaHashlamaChariga);
                listShinuyim.Add(ShinuyTypes.DeleteElementRechev07);
                listShinuyim.Add(ShinuyTypes.ShinuyShatHatchalaLefiShatItyatzvut12);
                listShinuyim.Add(ShinuyTypes.ShinuyFixedShatHamtana25);
                listShinuyim.Add(ShinuyTypes.ShinuyShatHatchalaGmar_19_26_27);
                listShinuyim.Add(ShinuyTypes.SetBooleanParams);
                listShinuyim.Add(ShinuyTypes.ShinuyHosafatSidurHeadrutWithPaymeny15);
                listShinuyim.Add(ShinuyTypes.ShinuyUpdateTachograph);
                listShinuyim.Add(ShinuyTypes.ShinuyHashlamaForYomAvoda);
                listShinuyim.Add(ShinuyTypes.ShinuyUpdateBitulZmanNesiot);
                listShinuyim.Add(ShinuyTypes.ShinuyKonnutGrira03);
                listShinuyim.Add(ShinuyTypes.ShinuyFixedLina07);
                listShinuyim.Add(ShinuyTypes.SetSidurObjects);
                listShinuyim.Add(ShinuyTypes.ShinuyUpdateHalbasha);
                return listShinuyim;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ShinuyInputData FillInputData(int misparIshi, DateTime cardDate, long? btchRequest, int? userId)
        {
            int iLast = 0;
            var inputData = new ShinuyInputData();

            inputData.iMisparIshi = misparIshi;
            inputData.CardDate = cardDate;

            var cache = _container.Resolve<IKDSCacheManager>();
            string sCarNumbers;
            OrderedDictionary htFullEmployeeDetails = new OrderedDictionary();
            OrderedDictionary htSpecialEmployeeDetails = new OrderedDictionary();
        
            try
            {
                inputData.YamimMeyuchadim = cache.GetCacheItem<DataTable>(CachedItems.YamimMeyuhadim);
                inputData.SugeyYamimMeyuchadim = cache.GetCacheItem<DataTable>(CachedItems.SugeyYamimMeyuchadim);
               // inputData.dtSibotLedivuachYadani = cache.GetCacheItem<DataTable>(CachedItems.SibotLedivuchYadani);

                //var ovedManager = ServiceLocator.Current.GetInstance<IOvedManager>();
                inputData.oMeafyeneyOved = _container.Resolve<IOvedManager>().CreateMeafyenyOved(misparIshi, cardDate);
                inputData.iSugYom = DateHelper.GetSugYom(inputData.YamimMeyuchadim, cardDate, inputData.SugeyYamimMeyuchadim);//, _oMeafyeneyOved.GetMeafyen(56).IntValue);

                IParametersManager paramManager = _container.Resolve<IParametersManager>();
                inputData.oParam = paramManager.CreateClsParametrs(cardDate, inputData.iSugYom);   

                //Get Meafyeney Sug Sidur
                //dtSugSidur = cache.GetCacheItem<DataTable>(CachedItems.SugeySidur);


                inputData.dtMatzavOved = _container.Resolve<IOvedDAL>().GetOvedMatzav(misparIshi, cardDate);
                inputData.UserId = userId;
                inputData.BtchRequestId = btchRequest;

                inputData.dtTmpSidurimMeyuchadim = _container.Resolve<ISidurManager>().GetTmpSidurimMeyuchadim(cardDate, cardDate);
                inputData.dtTmpMeafyeneyElements = _container.Resolve <IPeilutManager>().GetTmpMeafyeneyElements(cardDate, cardDate);


                IOvedManager ovedManager = _container.Resolve<IOvedManager>();
                inputData.OvedDetails = ovedManager.CreateOvedDetails(misparIshi, cardDate);
                inputData.oMeafyeneyOved = ovedManager.CreateMeafyenyOved(misparIshi, cardDate);

                //if (dtOvedCardDetails.Rows.Count>0)
                if (inputData.OvedDetails != null && inputData.OvedDetails.bOvedDetailsExists)
                {
                    if ((inputData.OvedDetails.sKodMaamd == "331") || (inputData.OvedDetails.sKodMaamd == "332") || (inputData.OvedDetails.sKodMaamd == "333") || (inputData.OvedDetails.sKodMaamd == "334"))
                    {
                        _container.Resolve<ILogBakashot>().InsertLog(inputData.BtchRequestId.HasValue ? inputData.BtchRequestId.Value : 0, "I", 0, "MainInputData: " + "HR - לעובד זה חסרים נתונים ב ", inputData.iMisparIshi, inputData.CardDate);

                        inputData.IsSuccsess = false;
                        inputData.bHaveCount = false;
                    }
                    else if (inputData.OvedDetails.iIsuk == 0)
                    {
                        _container.Resolve<ILogBakashot>().InsertLog(inputData.BtchRequestId.HasValue ? inputData.BtchRequestId.Value : 0, "W", 0, "MainInputData: " + "HR - לעובד זה חסרים נתונים ב ", inputData.iMisparIshi, inputData.CardDate);

                       inputData.IsSuccsess = false;
                    }
                    else
                    {
                        //Get Oved Details
                        var ovedDetails = ovedManager.GetOvedDetails(misparIshi, cardDate);
                        if (ovedDetails.Rows.Count > 0)
                        {
                           
                            //OrderedDictionary htFullSidurimDetails = new OrderedDictionary();
                            //Insert Oved Details to Class
                            inputData.htEmployeeDetails = ovedManager.GetEmployeeDetails(false,ovedDetails, inputData.CardDate, misparIshi, out iLast, out htSpecialEmployeeDetails, out htFullEmployeeDetails);
                            // htEmployeeDetails = oDefinition.InsertEmployeeDetails(false, dtDetails, dCardDate, ref iLastMisaprSidur, out _htSpecialEmployeeDetails, ref htFullSidurimDetails);//, out  _htEmployeeDetailsWithCancled
                            inputData.htFullEmployeeDetails = htFullEmployeeDetails;
                            inputData.htSpecialEmployeeDetails = htSpecialEmployeeDetails;

                            sCarNumbers = _container.Resolve<IKavimManager>().GetMasharCarNumbers(inputData.htEmployeeDetails);
                            if (sCarNumbers != string.Empty)
                            {
                                inputData.dtMashar = _container.Resolve<IKavimDAL>().GetMasharData(sCarNumbers);
                            }
                        }
                        //_dtApproval = clDefinitions.GetApprovalToEmploee(iMisparIshi, dCardDate);

                        var shinuyManager = _container.Resolve<IShinuyimManager>();
                        inputData.IdkuneyRashemet = shinuyManager.GetIdkuneyRashemet(misparIshi, cardDate);
                        inputData.IdkuneyRashemet.Columns.Add("update_machine", System.Type.GetType("System.Int32"));
                        inputData.ApprovalError = shinuyManager.GetApprovalErrors(misparIshi, cardDate);

                        InsertToYameyAvodaForUpdate(inputData);
                       // _IsExecuteInputData = true;
                    }
                }
                
                
                return inputData;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    

        private void InsertToYameyAvodaForUpdate(ShinuyInputData inputData)
        {
            try
            {
                inputData.oObjYameyAvodaUpd = new OBJ_YAMEY_AVODA_OVDIM();
                inputData.oObjYameyAvodaUpd.MISPAR_ISHI = inputData.iMisparIshi;
                inputData.oObjYameyAvodaUpd.TACHOGRAF = inputData.OvedDetails.sTachograf;
                if (!String.IsNullOrEmpty(inputData.OvedDetails.sHalbasha))
                {
                    inputData.oObjYameyAvodaUpd.HALBASHA = int.Parse(inputData.OvedDetails.sHalbasha);
                }
                if (!String.IsNullOrEmpty(inputData.OvedDetails.sHashlamaLeyom))
                {
                    inputData.oObjYameyAvodaUpd.HASHLAMA_LEYOM = int.Parse(inputData.OvedDetails.sHashlamaLeyom);
                }
                if (!String.IsNullOrEmpty(inputData.OvedDetails.sBitulZmanNesiot))
                {
                    inputData.oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = int.Parse(inputData.OvedDetails.sBitulZmanNesiot);
                }
                if (!String.IsNullOrEmpty(inputData.OvedDetails.sLina))
                {
                    inputData.oObjYameyAvodaUpd.LINA = int.Parse(inputData.OvedDetails.sLina);
                }
                inputData.oObjYameyAvodaUpd.TAARICH = inputData.CardDate;
                inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = inputData.OvedDetails.iZmanNesiaHaloch;
                inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = inputData.OvedDetails.iZmanNesiaHazor;
                if (!String.IsNullOrEmpty(inputData.OvedDetails.sHamara))
                {
                    inputData.oObjYameyAvodaUpd.HAMARAT_SHABAT = int.Parse(inputData.OvedDetails.sHamara);
                }

                inputData.oObjYameyAvodaUpd.SIBAT_HASHLAMA_LEYOM = inputData.OvedDetails.iSibatHashlamaLeyom;

                if (inputData.UserId.HasValue)
                    inputData.oObjYameyAvodaUpd.MEADKEN_ACHARON = (long)inputData.UserId;

                ModificationRecorder<OBJ_YAMEY_AVODA_OVDIM> recorder = new ModificationRecorder<OBJ_YAMEY_AVODA_OVDIM>(inputData.oObjYameyAvodaUpd);
                recorder.StartRecord();
                inputData.oCollYameyAvodaUpdRecorder.Add(recorder);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
    }
}
