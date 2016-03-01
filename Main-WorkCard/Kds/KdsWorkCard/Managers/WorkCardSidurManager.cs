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
    /// <summary>
    /// This is the manager used by the web methods for retriving the data related to work card (angular version)
    /// </summary>
    public class WorkCardSidurManager : IWorkCardSidurManager
    {
        private IUnityContainer _container;

        private const string ERROR_IN_SITE = "החתמה באתר לא תקין";
        private const string PTOR = "פטור";
        private const string MISSING_SIBA = "חסרה החתמה";

        public WorkCardSidurManager(IUnityContainer container)
        {
            _container = container;
        }

        public WorkCardResultContainer GetUserWorkCard(int misparIshi, DateTime cardDate, int userId = -2, int batchRequest = 0, bool isFromEmda = false)
        {
            WorkCardResultContainer result = new WorkCardResultContainer();
            //SetSecurityLevel();
            //PerUserSecurity
            result.bRashemet= LoginUser.IsRashemetProfile(LoginUser.GetLoginUser()) ;
            result.bMenahelBankShaot = LoginUser.IsMenahelBankShaot(LoginUser.GetLoginUser());
            
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
            result.Sidurim = converter.Convert(wcResult,userId,profiles);
            
            result.FirstHityazvut = GetParticipation(wcResult, clGeneral.enHityazvut.enFirstHityatzvut);
            result.SecondHityazvut = GetParticipation(wcResult, clGeneral.enHityazvut.enSecondHityatzvut);
            result.CardStatus = GetStatusCard(wcResult, (result.bRashemet || result.bMenahelBankShaot));
            result.DayDetails = GetNetuneyYomAvoda(wcResult);
            result.oParams = wcResult.oParam;
            return result;
        }

        private StatusCard GetStatusCard(WorkCardResult wcResult, bool bHaveHarshaa)
        {


            DataView dv;
            StatusCard status = new StatusCard();
            if (wcResult.oOvedYomAvodaDetails != null && wcResult.oOvedYomAvodaDetails.bOvedDetailsExists)
            {
                //_StatusCard = oBatchManager.CardStatus;
                if (bHaveHarshaa)
                {
                    if (((CardStatus)(wcResult.oOvedYomAvodaDetails.iStatus)) != (wcResult.CardStatus))
                    {
                        dv = new DataView(wcResult.dtLookUp);
                        dv.RowFilter = string.Concat("table_name='", "ctb_status_kartis", "' and kod =", wcResult.CardStatus.GetHashCode());
                        status.TeurStatus= dv[0]["teur"].ToString();
                    }
                    else
                    {
                        status.TeurStatus = wcResult.oOvedYomAvodaDetails.sStatusCardDesc;
                    }
                    switch (wcResult.CardStatus)
                    {
                        case CardStatus.Error:
                            status.ClassStr ="CardStatusError";
                            status.Kodstatus = (int)CardStatus.Error;
                           // tdCardStatus2.Attributes.Add("class", "CardStatusError");
                            //strImageUrl = "../../Images/btn-error.jpg";
                            break;
                        case CardStatus.Valid:
                            status.ClassStr = "CardStatusValid";
                            status.Kodstatus = (int)CardStatus.Valid;
                           // tdCardStatus.Attributes.Add("class", "CardStatusValid");
                           // tdCardStatus2.Attributes.Add("class", "CardStatusValid");
                            //strImageUrl = "../../Images/btn-ok.jpg";
                            break;
                        case CardStatus.Calculate:
                            status.ClassStr = "CardStatusCalculate";
                            status.Kodstatus = (int)CardStatus.Calculate;
                           // tdCardStatus.Attributes.Add("class", "CardStatusCalculate");
                           // tdCardStatus2.Attributes.Add("class", "CardStatusCalculate");
                            //strImageUrl = "../../Images/btn-ok.jpg";
                            break;
                    }
                }
                // SD.StatusCard = _wcResult.CardStatus;
            }
            //else
            //{
            //    // tdCardStatus.Attributes.Add("class", "");
            //    // tdCardStatus2.Attributes.Add("class", "");
            //    return "";
            //}
            return status;
        }
        
        #region Helper methods for filling in sidurim
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
        #endregion

        #region Netunim
       

        protected HityazvutInfo GetParticipation(WorkCardResult wcResult, clGeneral.enHityazvut enHityazvut)
        {
            
            int iKodLedivuch;
            HityazvutInfo oHityazvut = new HityazvutInfo();
           
          
            // txtParticipation.Text = "";
            oHityazvut.ValuReadOnly = true;
            oHityazvut.KodSibaEnabled = false;
            var oSidur = wcResult.htEmployeeDetails.Values.Cast<SidurDM>().ToList().SingleOrDefault(Sidur => (clGeneral.enHityazvut)Sidur.iNidreshetHitiatzvut == enHityazvut);
            if (oSidur != null)
            {
                if ((oSidur.iHachtamaBeatarLoTakin == clGeneral.enHityazvutErrorInSite.enHityazvutErrorInSite.GetHashCode())
                    && (oSidur.dShatHitiatzvut != null) && (oSidur.dShatHitiatzvut.Year > DateHelper.cYearNull))
                {
                    oHityazvut.Value = ERROR_IN_SITE;
                    //txtParticipation.ReadOnly = true;
                }
                else
                {
                    if ((oSidur.dShatHitiatzvut != null) && (oSidur.dShatHitiatzvut.Year > DateHelper.cYearNull))
                    {
                        oHityazvut.Value = oSidur.dShatHitiatzvut.ToShortTimeString();
                    }
                    else
                    {
                        //אם אין שעת התייצבות ויש ערך 1 שדה פטור מהתייצבות, נציג את הצלל 'פטור'
                        if (oSidur.iPtorMehitiatzvut == clGeneral.enPtorHityazvut.PtorHityazvut.GetHashCode())
                        {
                            oHityazvut.Value = PTOR;
                            // txtParticipation.ReadOnly = true;
                        }
                    }
                }
                if (oHityazvut.Value == string.Empty)
                {
                    oHityazvut.Value = MISSING_SIBA;
                    oHityazvut.KodSibaEnabled = true;
                }
                iKodLedivuch = oSidur.iKodSibaLedivuchYadaniIn;
                oHityazvut.KodSiba = iKodLedivuch == 0 ? 0 : iKodLedivuch;//.ToString();                  
            }

            return oHityazvut;
        }

        public NetuneyYomAvoda  GetNetuneyYomAvoda(WorkCardResult wcResult)
        {
            NetuneyYomAvoda oDay= new NetuneyYomAvoda();
            bool bEnable;

            oDay.Tachograf.Value = string.IsNullOrEmpty(wcResult.oOvedYomAvodaDetails.sTachograf) ? 0 : int.Parse(wcResult.oOvedYomAvodaDetails.sTachograf);
            oDay.Halbasha.Value = string.IsNullOrEmpty(wcResult.oOvedYomAvodaDetails.sHalbasha) ? 0 : int.Parse(wcResult.oOvedYomAvodaDetails.sHalbasha);
            oDay.Lina.Value = string.IsNullOrEmpty(wcResult.oOvedYomAvodaDetails.sLina) ? 0 : int.Parse(wcResult.oOvedYomAvodaDetails.sLina);
            oDay.ZmanNesiot.Value = string.IsNullOrEmpty(wcResult.oOvedYomAvodaDetails.sBitulZmanNesiot) ? 0 : int.Parse(wcResult.oOvedYomAvodaDetails.sBitulZmanNesiot);

            bEnable = EnabledHashlamaForDay(wcResult);
            oDay.HashlamaForDay.Value = (!String.IsNullOrEmpty(wcResult.oOvedYomAvodaDetails.sHashlamaLeyom) && int.Parse(wcResult.oOvedYomAvodaDetails.sHashlamaLeyom) > 0) ? 1 : 0;
            oDay.HashlamaForDay.IsEnabled = ((bEnable)); //***|| (!(clWorkCard.IsIdkunExists(iMisparIshiIdkunRashemet, bRashemet, ErrorLevel.LevelYomAvoda, clUtils.GetPakadId(dtPakadim, "HASHLAMA_LEYOM"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref dtIdkuneyRashemet))));
         //   oDay.HamaratShabat.Value = (!String.IsNullOrEmpty(wcResult.oOvedYomAvodaDetails.sHamara) && int.Parse(wcResult.oOvedYomAvodaDetails.sHamara) > 0) ? 1 : 0;
            oDay.SibatHashlamaLeyom.Value = wcResult.oOvedYomAvodaDetails.iSibatHashlamaLeyom;
            oDay.SibatHashlamaLeyom.IsEnabled = ((bEnable)); //***&& (!(clWorkCard.IsIdkunExists(iMisparIshiIdkunRashemet, bRashemet, ErrorLevel.LevelYomAvoda, clUtils.GetPakadId(dtPakadim, "SIBAT_HASHLAMA_LEYOM"), 0, DateTime.MinValue, DateTime.MinValue, 0, ref dtIdkuneyRashemet))));
            return oDay;
        }

        protected bool EnabledHashlamaForDay(WorkCardResult wcResult)
        {
            /* 1. אין לאפשר שינוי הערך עבור עובד מאגד תעבורה (Ovdim.Kod_Hevra=4895 )
           2. אסור לעדכן ביום שבתון/שבת.
           3. ביום שישי (רק ערך 6 מה- Oracle, לא ערב חג) - מותר רק לעובד 6 ימים (מזהים לפי ערך 61, 62) במאפיין 56 במאפייני עובדים.*/

            bool bEnabled = true;
            int iSidurDay = String.IsNullOrEmpty(wcResult.oOvedYomAvodaDetails.sSidurDay) ? 0 : int.Parse(wcResult.oOvedYomAvodaDetails.sSidurDay);

            //אין לאפשר עדכון עבור עובד שאינו מאגד (Ovdim.Kod_Hevra=580 ) מאגד תעבורה (Ovdim.Kod_Hevra=4895)
            //2. אסור לעדכן ביום שבתון/שבת/ערב חג.
            if ((wcResult.oOvedYomAvodaDetails.iKodHevra == enEmployeeType.enEggedTaavora.GetHashCode()) ||
                (wcResult.oOvedYomAvodaDetails.sShabaton == "1") || (iSidurDay == enDay.Shabat.GetHashCode()))
                return false;

            //. עובד 5 ימים - מותר בימים  א-ה כולל ערבי חג בימים אלה. זיהוי עובד 5 ימים לפי לפי ערך 51/52 במאפיין 56 במאפייני עובדים.
            if (((iSidurDay == enDay.Shabat.GetHashCode()) || (wcResult.oOvedYomAvodaDetails.sShabaton == "1") || ((iSidurDay == enDay.Shishi.GetHashCode())))
                && ((wcResult.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek1.GetHashCode()) || (wcResult.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())))
                return false;

            //. עובד 6 ימים - מותר בימים  א-ו כולל ערבי חג בימים אלה. זיהוי עובד 6 ימים לפי לפי ערך 61/62 במאפיין 56 במאפייני עובדים
            if ((iSidurDay == enDay.Shabat.GetHashCode()) || (wcResult.oOvedYomAvodaDetails.sShabaton == "1"))
                if ((((wcResult.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved6DaysInWeek1.GetHashCode()) && (wcResult.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved6DaysInWeek2.GetHashCode()))))
                    return false;

            if (wcResult.oOvedYomAvodaDetails.sMutamut != clGeneral.enMutaam.enMutaam1.GetHashCode().ToString())
                return false;

            return bEnabled;
        }
        #endregion


        public WorkCardLookupsContainer GetWorkCardLookups()
        {
            WorkCardLookupsContainer wcLookups = new WorkCardLookupsContainer();
            WorkCardLookupsHelper lookupHelper = _container.Resolve<WorkCardLookupsHelper>();

            wcLookups.HalbashaList = lookupHelper.GetHalbashaList();
            wcLookups.HarigaList = lookupHelper.GetHarigaList();
            wcLookups.HashlamaList = lookupHelper.GetHashlamaList();
            wcLookups.HashlameLeYomList = lookupHelper.GetHashlameLeYomList();
            wcLookups.LinaList = lookupHelper.GetLinaList();
            wcLookups.PizulList = lookupHelper.GetPizulList();
            wcLookups.SibotLedivuachList = lookupHelper.GetSibotLedivuachList();
            wcLookups.TachografList = lookupHelper.GetTachografList();
            wcLookups.ZmanNesiaList = lookupHelper.GetZmanNesiaList();

            return wcLookups;

        }
    }
}
