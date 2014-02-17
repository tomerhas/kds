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
                               .Any(matzav => IsOvedMatzavExists(matzav, dtMatzavOved));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        protected bool IsOvedMatzavExists(string sKodMatzav, DataTable dtMatzavOved)
        {
            DataRow[] dr;
            bool bOvedMatzavExists;
            
            try
            {
                sKodMatzav = Append0ToNumber(sKodMatzav);

                dr = dtMatzavOved.Select(string.Concat("kod_matzav='",sKodMatzav+"'"));
                bOvedMatzavExists = dr.Length > 0;
                return bOvedMatzavExists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     

        protected void AddLogErrorToDb(ErrorInputData input, Exception ex)
        {
            long btchId = 0;
            if (input.BtchRequestId.HasValue)
                btchId= input.BtchRequestId.Value;
            clLogBakashot.InsertErrorToLog(btchId, input.iMisparIshi, "E"
                , (int)CardErrorType, input.CardDate, CardErrorType.ToString()+ ": " + ex.Message);
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

        private string Append0ToNumber(string val)
        {

            if(IsNumber(val))
              val =  string.Format("{0:00}", int.Parse(val));
            return val;
        }

        private bool IsNumber(string val)
        {
            int temp = 0;
            return int.TryParse(val, out temp);
        }

        protected bool CheckShaaton(int isugYom, DateTime dTaarich, ErrorInputData input)
        {
            if ((dTaarich.DayOfWeek.GetHashCode() + 1) == clGeneral.enDay.Shabat.GetHashCode())
                return true;
            else if (input.SugeyYamimMeyuchadim.Select("sug_yom=" + isugYom).Length > 0)
            {
                return (input.SugeyYamimMeyuchadim.Select("sug_yom=" + isugYom)[0]["Shbaton"].ToString() == "1") ? true : false;
            }
            else return false;
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

        protected DateTime GetDateTimeFromStringHour(string sShaa, DateTime dDate)
        {
            DateTime dTemp;
            string[] arrTime;
            try
            {
                dDate = dDate.Date;
                arrTime = sShaa.Split(char.Parse(":"));
                if (arrTime.Length > 1)
                {
                    dTemp = dDate.AddHours(double.Parse(arrTime[0])).AddMinutes(double.Parse(arrTime[1]));
                    if (arrTime.Length > 2)
                    {
                        dTemp = dTemp.AddSeconds(double.Parse(arrTime[2]));
                    }
                }
                else
                {
                    sShaa = sShaa.PadLeft(4, (char)48);
                    dTemp = dDate.AddHours(double.Parse(sShaa.Substring(0, 2))).AddMinutes(double.Parse(sShaa.Substring(2, 2)));
                }

                return dTemp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected bool IsSidurNahagut(DataRow[] drSugSidur, SidurDM oSidur)
        {
            bool bSidurNahagut = false;

            //הפונקציה תחזיר TRUE אם הסידור הוא סידור נהגות

            try
            {
                if (oSidur.bSidurMyuhad)
                {//סידור מיוחד
                    bSidurNahagut = (oSidur.sSectorAvoda == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString());
                }
                else
                {//סידור רגיל
                    if (drSugSidur.Length > 0)
                    {
                        bSidurNahagut = (drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 0, oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurNahagut: " + ex.Message, null);
                //_bSuccsess = false;
            }

            return bSidurNahagut;
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
                    bSidurNihulTnua = (oSidur.sSectorAvoda == clGeneral.enSectorAvoda.Nihul.GetHashCode().ToString());
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
                        bSidurNihulTnua = (drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nihul.GetHashCode().ToString());
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
            //א. לעובד אין רישיון נהיגה באוטובוס (יודעים אם לעובד יש רישיון לפי ערכים 6, 10, 11 בקוד נתון 7 (קוד רישיון אוטובוס) בטבלת פרטי עובדים)
            bError = (!IsOvedHasDriverLicence(input));

            //ב. עובד הוא מותאם שאסור לו לנהוג (יודעים שעובד הוא מותאם שאסור לו לנהוג לפי ערכים 4, 5 בקוד נתון 8 (קוד עובד מותאם) בטבלת פרטי עובדים) 
            if (!(bError))
            {
                bError = IsOvedMutaam(input);
            }

            if (!bError)
            {
                //ד. עובד הוא בשלילה (יודעים שעובד הוא בשלילה לפי ערך 1 בקוד בנתון 21 (שלילת   רשיון) בטבלת פרטי עובדים) 
                if (input.CardDate != input.OvedDetails.dTaarichMe)
                    bError = IsOvedBShlila(input);
            }

            if (!bError)
            {
                //. עובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה (יודעים שעובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה לפי ערכים 6, 7 בקוד נתון 8 (קוד עובד מותאם) בטבלת פרטי עובדים) במקרה זה יש לבדוק אם הסידור מכיל רק נסיעות ריקות, מפעילים את הרוטינה לזיהוי מקט
                bError = IsOvedMutaamForEmptyPeilut(input);
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
            //. עובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה (יודעים שעובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה לפי ערכים 6, 7 בקוד נתון 8 (קוד עובד מותאם) בטבלת פרטי עובדים) במקרה זה יש לבדוק אם הסידור מכיל רק נסיעות ריקות, מפעילים את הרוטינה לזיהוי מקט
            return ((input.OvedDetails.sMutamut == clGeneral.enMutaam.enMutaam6.GetHashCode().ToString() ||
                   input.OvedDetails.sMutamut == clGeneral.enMutaam.enMutaam7.GetHashCode().ToString())
                   && (input.curSidur.bSidurNotEmpty));

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
    } 
}
