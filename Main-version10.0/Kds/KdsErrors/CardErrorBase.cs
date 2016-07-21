using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Interfaces.Errors;
using KDSCommon.DataModels.Errors;
using System.Data;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.Interfaces;
using KDSCommon.Enums;
using KdsLibrary;
using KDSCommon.DataModels;
using KDSCommon.Interfaces.Managers;
using System.Diagnostics;
using Microsoft.Practices.Unity;
using KDSCommon.Interfaces.Logs;
using System.Web;
using KDSCommon.Interfaces.DAL;

namespace KdsErrors
{
    public abstract class CardErrorBase : ICardError
    {
        protected IUnityContainer _container;

        public CardErrorBase(IUnityContainer container)
        {
            _container = container;
        }

        public CardErrorBase()
        {
        }

        public bool IsCorrect(ErrorInputData input)
        {
            if (IsActive(input))
            {
                try
                {
                    bool isCorrect =  InternalIsCorrect(input);
                    if (isCorrect == false)
                    {
                        Debug.WriteLine("Have card error in card type {0}.", CardErrorType.ToString());
                    }
                    return isCorrect;
                }
                catch (Exception ex)
                {
                    AddLogErrorToDb(input, ex);
                    input.IsSuccsess = false;
                    return false;
                }
            }
            //If the error is not active - no need to commit validation and therefore return true
            return true;
        }

        public abstract bool InternalIsCorrect(ErrorInputData input);
        public abstract ErrorTypes CardErrorType { get; }
        public abstract ErrorSubLevel CardErrorSubLevel { get; }

        protected bool IsActive( ErrorInputData input)
        {
            DataRow[] drShgiaNotActive;
            drShgiaNotActive = input.ErrorsNotActive.Select("kod_shgia=" + (int)CardErrorType);
            if (drShgiaNotActive!=null && drShgiaNotActive.Length > 0)
                return false;
            return true;
        }

        protected bool IsOvedInMatzav(string sMatzavim, DataTable dtMatzavOved)
        {
            bool result = false;
            try
            {
                //return result;
                result = sMatzavim.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                               .Any(matzav =>  _container.Resolve<IOvedManager>().IsOvedMatzavExists(matzav, dtMatzavOved));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        protected void AddLogErrorToDb(ErrorInputData input, Exception ex)
        {
            long btchId = 0;
            if (input.BtchRequestId.HasValue)
                btchId= input.BtchRequestId.Value;

            _container.Resolve<ILogBakashot>().InsertLog(btchId, "W", (int)CardErrorType, CardErrorType.ToString() + ": " + ex.Message, input.iMisparIshi, input.CardDate);
        }

        protected string GetLookUpKods(string sTableName, ErrorInputData input)
        {
            //The function get lookup table name and return all kods in string, separate by comma
            string sLookUp = "";
            DataRow[] drLookUpAll;
            try
            {
                drLookUpAll = input.LookUp.Select(string.Concat("table_name='", sTableName,"'"));
                foreach (DataRow drLookUp in drLookUpAll)
                {
                    sLookUp = string.Concat(sLookUp, drLookUp["Kod"].ToString(), ",");
                }
                sLookUp = sLookUp.Substring(0, sLookUp.Length - 1);

                return sLookUp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

   

        protected bool IsSidurHeadrut(SidurDM oSidur)
        {
            bool bSidurHeadrut = false;
            try
            {
                //הפונקציה תחזיר  אם הסידור הוא סידור העדרות מסוג מחלה/מילואים/תאונה  TRUE
                try
                {
                    if (oSidur.bSidurMyuhad)
                    {//סידור מיוחד
                        bSidurHeadrut = !string.IsNullOrEmpty(oSidur.sHeadrutTypeKod);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                   // clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 0, oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurHeadrut: " + ex.Message, null);
                    //_bSuccsess = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bSidurHeadrut;
        }

        protected bool IsSidurWithPeiluyotOnlyWithHeterEmptyBus(SidurDM oSidur)
        {
            PeilutDM oPeilut;
            int makat;
            enMakatType type;
            try
            {
                for (int i = 0; i < oSidur.htPeilut.Count; i++)
                {
                    oPeilut = (PeilutDM)oSidur.htPeilut[i];
                    makat = int.Parse(oPeilut.lMakatNesia.ToString().Substring(0, 3));
                    type = (enMakatType)oPeilut.iMakatType;

                    if (makat != 701 && makat != 702 && makat != 711 && type != enMakatType.mEmpty &&
                        !(type == enMakatType.mElement && oPeilut.sElementNesiaReka != "") && !(type == enMakatType.mElement && !oPeilut.bBusNumberMustExists))
                                return false;
                        
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                //clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 0, oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurNihulTnua: " + ex.Message, null);
                //_bSuccsess = false;
            }
        }
    
        protected bool IsSidurNihulTnua(DataRow[] drSugSidur, SidurDM oSidur)
        {
            bool bSidurNihulTnua = false;
            bool bElementZviraZman = false;
            //הפונקציה תחזיר TRUE אם הסידור הוא סידור נהגות

            try
            {
                if (oSidur.bSidurMyuhad)
                {//סידור מיוחד
                    bSidurNihulTnua = (oSidur.sSectorAvoda == enSectorAvoda.Nihul.GetHashCode().ToString());
                    if (!bSidurNihulTnua)
                        if (oSidur.iMisparSidur == 99301)
                        { // oSidur.bMatalaKlalitLeloRechev

                            PeilutDM oPeilut = null;
                            for (int i = 0; i < oSidur.htPeilut.Count; i++)
                            {
                                oPeilut = (PeilutDM)oSidur.htPeilut[i];
                                if (!string.IsNullOrEmpty(oPeilut.sElementZviraZman))
                                    if (int.Parse(oPeilut.sElementZviraZman) == 4)
                                    {
                                        bElementZviraZman = true;
                                        break;
                                    }
                            }
                            if (bElementZviraZman)
                                bSidurNihulTnua = true;
                        }
                }
                else
                {//סידור רגיל
                    if (drSugSidur.Length > 0)
                    {
                        bSidurNihulTnua = (drSugSidur[0]["sector_avoda"].ToString() == enSectorAvoda.Nihul.GetHashCode().ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 0, oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurNihulTnua: " + ex.Message, null);
                //_bSuccsess = false;
            }

            return bSidurNihulTnua;
        }

        protected bool CheckConditionsAllowSidur(ErrorInputData input)
        {
            bool bError = false;
            bool checkHeter = true;
            //א. לעובד אין רישיון נהיגה באוטובוס (יודעים אם לעובד יש רישיון לפי ערכים 6, 10, 11 בקוד נתון 7 (קוד רישיון אוטובוס) בטבלת פרטי עובדים)
            bError = (!IsOvedHasDriverLicence(input));

            //ב. עובד הוא מותאם שאסור לו לנהוג (יודעים שעובד הוא מותאם שאסור לו לנהוג לפי ערכים 4, 5 בקוד נתון 8 (קוד עובד מותאם) בטבלת פרטי עובדים) 
            if (!(bError))
            {
                bError = IsOvedMutaam(input);
                checkHeter = false;
            }

            if (!bError)
            {
                //ד. עובד הוא בשלילה (יודעים שעובד הוא בשלילה לפי ערך 1 בקוד בנתון 21 (שלילת   רשיון) בטבלת פרטי עובדים) 
                if (input.CardDate != input.OvedDetails.dTaarichMe)
                {
                    bError = IsOvedBShlila(input);
                    checkHeter = false;
                }
            }

            if (!bError)
            {
                //. עובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה (יודעים שעובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה לפי ערכים 6, 7 בקוד נתון 8 (קוד עובד מותאם) בטבלת פרטי עובדים) במקרה זה יש לבדוק אם הסידור מכיל רק נסיעות ריקות, מפעילים את הרוטינה לזיהוי מקט
                bError = IsOvedMutaamForEmptyPeilut(input);
            }
            if (checkHeter && bError)//ה
            {
                if (input.curSidur.iKodHeterNehiga == 80)// && input.curSidur.htPeilut.Count > 0 && IsSidurWithPeiluyotOnlyWithHeterEmptyBus(input.curSidur))
                    bError = false;
            }
            return bError;
        }

        private bool IsOvedHasDriverLicence(ErrorInputData input)
        {
            //א. לעובד אין רישיון נהיגה באוטובוס (יודעים אם לעובד יש רישיון לפי ערכים 6, 10, 11 בקוד נתון 7 (קוד רישיון אוטובוס) בטבלת פרטי עובדים)
            return (input.OvedDetails.sRishyonAutobus == clGeneral.enRishyonAutobus.enRishyon10.GetHashCode().ToString() ||
                    input.OvedDetails.sRishyonAutobus == clGeneral.enRishyonAutobus.enRishyon11.GetHashCode().ToString() ||
                    input.OvedDetails.sRishyonAutobus == clGeneral.enRishyonAutobus.enRishyon6.GetHashCode().ToString());

        }

        private bool IsOvedMutaam(ErrorInputData input)
        {
            //ב. עובד הוא מותאם שאסור לו לנהוג (יודעים שעובד הוא מותאם שאסור לו לנהוג לפי ערכים 4, 5 בקוד נתון 8 (קוד עובד מותאם) בטבלת פרטי עובדים)
            return (input.OvedDetails.sMutamut == clGeneral.enMutaam.enMutaam4.GetHashCode().ToString() ||
                    input.OvedDetails.sMutamut == clGeneral.enMutaam.enMutaam5.GetHashCode().ToString());

        }

        protected bool IsOvedBShlila(ErrorInputData input)
        {
            //ד. עובד הוא בשלילה (יודעים שעובד הוא בשלילה לפי ערך 1 בקוד בנתון 21 (שלילת   רשיון) בטבלת פרטי עובדים) 
            return input.OvedDetails.sShlilatRishayon == clGeneral.enOvedBShlila.enBShlila.GetHashCode().ToString();
        }


        private bool IsOvedMutaamForEmptyPeilut(ErrorInputData input)
        {
            int makat;
            enMakatType type;
            //. עובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה (יודעים שעובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה לפי ערכים 6, 7 בקוד נתון 8 (קוד עובד מותאם) בטבלת פרטי עובדים) במקרה זה יש לבדוק אם הסידור מכיל רק נסיעות ריקות, מפעילים את הרוטינה לזיהוי מקט
            if (((input.OvedDetails.sMutamut == enMutaam.enMutaam6.GetHashCode().ToString() ||
                   input.OvedDetails.sMutamut == enMutaam.enMutaam7.GetHashCode().ToString())
                   && (input.curSidur.bSidurNotEmpty)))
            {
                foreach (PeilutDM peilut in input.curSidur.htPeilut.Values.Cast<PeilutDM>().ToList())
                {
                    
                    makat = int.Parse(peilut.lMakatNesia.ToString().Substring(0, 3));
                    type= (enMakatType)peilut.iMakatType;
                    if (makat != 701 && makat != 702 && makat != 711 && type != enMakatType.mEmpty &&
                        !(type == enMakatType.mElement && peilut.sElementNesiaReka != "") && !(type == enMakatType.mElement && !peilut.bBusNumberMustExists))
                        return true;
                }
                //return true;
            }
            return false;

        }

        public bool CheckSidurHeadrutExsits(DataTable dtSidurim, string sug_headrut, int imispar_sidur)
        {
            SidurDM oSidur;
            try
            {
                foreach (DataRow dr in dtSidurim.Rows)
                {
                    var sidurManager = ServiceLocator.Current.GetInstance<ISidurManager>();
                    oSidur = sidurManager.CreateClsSidurFromEmployeeDetails(dr);
              
                    if (!string.IsNullOrEmpty(oSidur.sHeadrutTypeKod) && oSidur.sHeadrutTypeKod == sug_headrut && oSidur.iMisparSidur == imispar_sidur
                           && (oSidur.iLoLetashlum == 0 || (oSidur.iLoLetashlum == 1 && oSidur.iKodSibaLoLetashlum == 22)))
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckAnozerSidurExsits(ErrorInputData input)
        {
            try
            {
                foreach (SidurDM sidur in input.htEmployeeDetails.Values)
                {
                    if (input.curSidur.iMisparSidur != sidur.iMisparSidur && input.curSidur.dFullShatHatchala != sidur.dFullShatHatchala && sidur.iLoLetashlum == 0)
                        // if (!oSidur.bHeadrutTypeKodExists || (oSidur.bHeadrutTypeKodExists && oSidur.iSidurLebdikatRezefMachala > 0 && oSidur.sHeadrutTypeKod != sug_headrut))
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckHourValid(string sHour)
        {
            string[] arr;
            bool bValid = true;
            //מקבל מחרוזת בפורמט XX:XX ומחזיר שגיאה אם לא בין 00:01 ל23:59-
            try
            {
                if (sHour.Length > 0)
                {
                    arr = sHour.Split(char.Parse(":"));
                    if (!((int.Parse(arr[0])) >= 0 && (int.Parse(arr[0])) <= 23))
                    {
                        bValid = false;
                    }
                    if (!(int.Parse(arr[1]) >= 1 || (int.Parse(arr[1]) == 0 && int.Parse(arr[0]) >= 0) && int.Parse(arr[1]) <= 59))
                    {
                        bValid = false;
                    }
                }
                else
                {
                    bValid = false;
                }

                return bValid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected int IsBusNumberValid(long otoNumber, DateTime cardDate)
        {
            string sCacheKey = otoNumber + cardDate.ToShortDateString();
            if (HttpRuntime.Cache.Get(sCacheKey) == null || HttpRuntime.Cache.Get(sCacheKey).ToString() == "")
            {
                var kavimDal = ServiceLocator.Current.GetInstance<IKavimDAL>();
                var result = kavimDal.IsBusNumberValid(otoNumber, cardDate);

                HttpRuntime.Cache.Insert(sCacheKey, result, null, DateTime.MaxValue, TimeSpan.FromMinutes(1440));
                return result;
            }
            else
            {
                return int.Parse(HttpRuntime.Cache.Get(sCacheKey).ToString().Trim());
            }
        }

        protected int IsLicenseNumberValid(long licenseNumber, DateTime cardDate)
        {
            string sCacheKey = licenseNumber + cardDate.ToShortDateString();
            if (HttpRuntime.Cache.Get(sCacheKey) == null || HttpRuntime.Cache.Get(sCacheKey).ToString() == "")
            {
                var kavimDal = ServiceLocator.Current.GetInstance<IKavimDAL>();
                var result = kavimDal.IsLicenseNumberValid(licenseNumber, cardDate);

                HttpRuntime.Cache.Insert(sCacheKey, result, null, DateTime.MaxValue, TimeSpan.FromMinutes(1440));
                return result;
            }
            else
            {
                return int.Parse(HttpRuntime.Cache.Get(sCacheKey).ToString().Trim());
            }
        }
        protected bool HaveSidurNahgutInDay(ErrorInputData inputData)
        {
            SidurDM sidur;
            for (int i = 0; i < inputData.htEmployeeDetails.Count; i++)
            {
                sidur  = (SidurDM)inputData.htEmployeeDetails[i];
                var cacheManager = ServiceLocator.Current.GetInstance<IKDSCacheManager>();
                if(_container.Resolve<ISidurManager>().IsSidurNahagut(inputData.drSugSidur, sidur))
                   return true;
            }
            return false;
        }
        protected bool IsPeilutDoreshetMisparRechev(PeilutDM oPeilut)
        {
            enMakatType oMakatType = (enMakatType)oPeilut.iMakatType;

            if (((oMakatType == enMakatType.mKavShirut) || (oMakatType == enMakatType.mEmpty) || (oMakatType == enMakatType.mNamak) || (oMakatType == enMakatType.mVisa)
                           || (oMakatType == enMakatType.mElement && oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "700")) || ((oPeilut.iMakatType == enMakatType.mElement.GetHashCode()) && oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "700" && (oPeilut.bBusNumberMustExists) && (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "701") && (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "712") && (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "711")))
                return true;
            return false;
        }


        protected bool CheckIdkunRashemet(string sFieldToChange, ErrorInputData inputData)
        {
            bool bHaveIdkun = false;
            DataRow[] drIdkunim;
            try
            {
                drIdkunim = inputData.IdkuneyRashemet.Select("shem_db='" + sFieldToChange.ToUpper() + "'");
                if (drIdkunim.Length > 0)
                    bHaveIdkun = true;

                //if (sFieldToChange.ToUpper() == "SHAT_HATCHALA")
                //    bHaveIdkun = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bHaveIdkun;
        }

        public float GetSachNochechutDay(ErrorInputData input)
        {
            try
            {
                float nochechut=0;
                foreach (SidurDM sidur in input.htEmployeeDetails.Values)
                {
                    if (sidur.iLoLetashlum == 0 && sidur.dFullShatGmarLetashlum != DateTime.MinValue && sidur.dFullShatHatchalaLetashlum != DateTime.MinValue)
                        nochechut += float.Parse((sidur.dFullShatGmarLetashlum - sidur.dFullShatHatchalaLetashlum).TotalMinutes.ToString());
                }
                return nochechut;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public float GetMichsaYomit(ErrorInputData input)
        {
            try
            {
                DataRow[] drMichsa;
                int iShvuaAvoda;
                if (input.OvedDetails.iZmanMutamut > 0)
                    return input.OvedDetails.iZmanMutamut;

                var cacheManager = _container.Resolve<IKDSCacheManager>();
                var TbMichsa =cacheManager.GetCacheItem<DataTable>(CachedItems.MichsaYomit);

                if (input.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved6DaysInWeek1.GetHashCode() || input.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved6DaysInWeek2.GetHashCode())
                     iShvuaAvoda = 6; 
                 else  iShvuaAvoda = 5; 

                drMichsa = TbMichsa.Select("Kod_Michsa=" + input.oMeafyeneyOved.GetMeafyen(1).IntValue + " and SHAVOA_AVODA=" + iShvuaAvoda + " and sug_yom=" + input.iSugYom + " and me_taarich<=Convert('" + input.CardDate.ToShortDateString() + "', 'System.DateTime')" + " and ad_taarich>=Convert('" + input.CardDate.ToShortDateString() + "', 'System.DateTime')");
                 
                if (drMichsa.Length > 0)
                { 
                    return int.Parse((float.Parse(drMichsa[0]["michsa"].ToString()) * 60).ToString()); 
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    } 
}
