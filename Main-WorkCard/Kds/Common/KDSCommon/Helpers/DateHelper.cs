
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.Enums;

namespace KDSCommon.Helpers
{
    public class DateHelper
    {
        public const int cYearNull = 1900;
        public static DateTime GetDateTimeFromStringHour(string sShaa, DateTime dDate)
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
        public static DateTime ConvertMefyenShaotValid(DateTime dTaarich, string sShaaMeafyen)
        {
            DateTime dMeafyenDate;
            string sMeafyen;
            sMeafyen = ConvertToValidHour(sShaaMeafyen);
            if (IsEggedTime(sMeafyen))
            {
                dMeafyenDate = DateHelper.GetDateTimeFromStringHour(ConvertFromEggedTime(sMeafyen), dTaarich.Date).AddDays(1);
            }
            else
            {
                dMeafyenDate = DateHelper.GetDateTimeFromStringHour(sMeafyen, dTaarich.Date);
            }
            return dMeafyenDate;
        }
        public static bool IsEggedTime(string sHour)
        {
            string[] arr;

            //מחזיר אמת אם השעה היא שעה בין 24-32
            try
            {
                if ((sHour.IndexOf(char.Parse(":"))) == -1)
                {
                    sHour = sHour.Substring(0, 2) + ":" + sHour.Substring(2, 2);
                }
                arr = sHour.Split(char.Parse(":"));

                return ((int.Parse(arr[0]) >= 24) && (int.Parse(arr[0]) <= 32));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string ConvertToValidHour(string sHour)
        {
            //הפונקציה מקבלת שעה למשל 545 או 1 ומחזיר 05:45 או 00:00
            string sNewHour = sHour.PadLeft(4, (char)48);
            try
            {
                if ((sNewHour.IndexOf(char.Parse(":"))) == -1)
                {
                    sNewHour = sNewHour.Substring(0, 2) + ":" + sNewHour.Substring(2, 2);
                }

                return sNewHour;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ConvertFromEggedTime(string sHour)
        {
            string[] arr;
            int iHour;
            string sNewHour = sHour;
            try
            {
                arr = sHour.Split(char.Parse(":"));
                if (arr.Length > 1)
                {
                    iHour = int.Parse(arr[0]);
                    if ((iHour >= 24) && (iHour <= 32))
                    {
                        iHour = iHour - 24;
                    }
                    sNewHour = iHour.ToString() + ":" + arr[1];
                }
                return sNewHour;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ConvertToEggedTime(string sHour)
        {
            string[] arr;
            int iHour;
            string sNewHour = sHour;
            //הפונקציה מקבלת שעה בפורמט 00:00-08:00 ןמחזירה 24:00-32:00
            try
            {
                arr = sHour.Split(char.Parse(":"));
                if (arr.Length > 1)
                {
                    iHour = int.Parse(arr[0]);
                    if ((iHour >= 0) && (iHour <= 4))
                    {
                        iHour = iHour + 24;
                    }
                    sNewHour = iHour.ToString() + ":" + arr[1];
                }
                return sNewHour;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool CheckShaaton(int isugYom, DateTime dTaarich, DataTable SugeyYamimMeyuchadim)
        {
            if ((dTaarich.DayOfWeek.GetHashCode() + 1) == enDay.Shabat.GetHashCode())
                return true;
            else if (SugeyYamimMeyuchadim.Select("sug_yom=" + isugYom).Length > 0)
            {
                return (SugeyYamimMeyuchadim.Select("sug_yom=" + isugYom)[0]["Shbaton"].ToString() == "1") ? true : false;
            }
            else return false;
        }


        public static int GetSugYom(int iMisparIshi, DateTime dTaarich, DataTable dtYamimMeyuchadim, int iSectorOved, DataTable dtSugeyYamimMeyuchadim, int iMeafyen56)
        {
            DataRow[] drYaminMeyuchadim;
            int iSugYom;

            drYaminMeyuchadim = dtYamimMeyuchadim.Select("taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')", "");
            if (drYaminMeyuchadim.Length > 0)
            {
                if (iSectorOved == enSectorAvoda.Tafkid.GetHashCode())
                {
                    if (!string.IsNullOrEmpty(drYaminMeyuchadim[0]["Sug_Yom_Muchlaf_Minhal"].ToString()))
                    { iSugYom = int.Parse(drYaminMeyuchadim[0]["Sug_Yom_Muchlaf_Minhal"].ToString()); }
                    else { iSugYom = int.Parse(drYaminMeyuchadim[0]["sug_yom"].ToString()); }
                }
                else if (iSectorOved == enSectorAvoda.Meshek.GetHashCode())
                {
                    if (!string.IsNullOrEmpty(drYaminMeyuchadim[0]["Sug_Yom_Muchlaf_Meshek"].ToString()))
                    { iSugYom = int.Parse(drYaminMeyuchadim[0]["Sug_Yom_Muchlaf_Meshek"].ToString()); }
                    else { iSugYom = int.Parse(drYaminMeyuchadim[0]["sug_yom"].ToString()); }
                }
                else if (iSectorOved == enSectorAvoda.Nihul.GetHashCode())
                {
                    if (!string.IsNullOrEmpty(drYaminMeyuchadim[0]["Sug_Yom_Muchlaf_Tnua"].ToString()))
                    { iSugYom = int.Parse(drYaminMeyuchadim[0]["Sug_Yom_Muchlaf_Tnua"].ToString()); }
                    else { iSugYom = int.Parse(drYaminMeyuchadim[0]["sug_yom"].ToString()); }
                }
                else if (iSectorOved == enSectorAvoda.Nahagut.GetHashCode())
                {
                    if (!string.IsNullOrEmpty(drYaminMeyuchadim[0]["Sug_Yom_Muchlaf_Nehagut"].ToString()))
                    { iSugYom = int.Parse(drYaminMeyuchadim[0]["Sug_Yom_Muchlaf_Nehagut"].ToString()); }
                    else { iSugYom = int.Parse(drYaminMeyuchadim[0]["sug_yom"].ToString()); }
                }
                else { iSugYom = int.Parse(drYaminMeyuchadim[0]["sug_yom"].ToString()); }

                if ((dTaarich.DayOfWeek.GetHashCode() + 1) == enDay.Shabat.GetHashCode())
                { iSugYom = 20; }
                else if ((dTaarich.DayOfWeek.GetHashCode() + 1) == enDay.Shishi.GetHashCode() && !(dtSugeyYamimMeyuchadim.Select("sug_yom=" + iSugYom)[0]["Shishi_Muhlaf"].ToString() == "1") && (iMeafyen56 == enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || iMeafyen56 == enMeafyenOved56.enOved5DaysInWeek2.GetHashCode()))
                { iSugYom = 10; }

            }
            else
            {
                switch ((dTaarich.DayOfWeek.GetHashCode() + 1))
                {
                    case 7:
                        { iSugYom = 20; break; }
                    case 6:
                        { iSugYom = 10; break; }
                    default:
                        { iSugYom = 1; break; }
                }
            }

            return iSugYom;
        }

        public static int GetSugYom(DataTable dtYamimMeyuchadim, DateTime dTaarich, DataTable dtSugeyYamimMeyuchadim) //, int GetMeafyen(56).IntValue)
        {
            int iSugYom;
            if (dtYamimMeyuchadim.Select("taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')").Length > 0)
            {
                iSugYom = int.Parse(dtYamimMeyuchadim.Select("taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')")[0]["sug_yom"].ToString());
                if ((dTaarich.DayOfWeek.GetHashCode() + 1) == enDay.Shabat.GetHashCode())
                { iSugYom = 20; }
                else if ((dTaarich.DayOfWeek.GetHashCode() + 1) == enDay.Shishi.GetHashCode()) // && !(dtSugeyYamimMeyuchadim.Select("sug_yom=" + iSugYom)[0]["Shishi_Muhlaf"].ToString() == "1") && (GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek2.GetHashCode()))
                {
                    if (dtSugeyYamimMeyuchadim.Select("sug_yom=" + iSugYom).Length > 0)
                    {
                        if (!(dtSugeyYamimMeyuchadim.Select("sug_yom=" + iSugYom)[0]["Shbaton"].ToString() == "1"))
                            iSugYom = 10;
                    }
                }
                return iSugYom;
            }
            else
            {
                switch ((dTaarich.DayOfWeek.GetHashCode() + 1))
                {
                    case 7: return 20;
                    case 6: return 10;
                    default: return 1;
                }
            }
        }

        public static int GetSugMishmeret(int iMisparIshi, DateTime Taarich, int iSugYom, DateTime dShatHatchalaSidur, DateTime dShatGmarSidur, clParametersDM oParameters)
        {
            int iSugMishmeret;
            DateTime dTemp1;//=new DateTime();
            DateTime dTemp2;//= new DateTime();
            try
            {
                iSugMishmeret = enSugMishmeret.Boker.GetHashCode();
                if (iSugYom == 1 || iSugYom == 3 || iSugYom == 4 || iSugYom == 5)
                {

                    dTemp1 = oParameters.dMinStartMishmeretMafilimChol;
                    dTemp2 = oParameters.dMaxStartMishmeretMafilimChol;
                    //-	אם סוג יום [שליפת סוג יום לשליפת מכסה יומית (תאריך, מ.א.)] הוא אחד מתוך 01 או 03 או 04 או 05 וגם [שעת התחלה ראשונה] >= 11:00 וקטן מ- 17:00 וגם [שעת גמר אחרונה] > 18 אזי [סוג משמרת] = צהריים 
                    if (dShatHatchalaSidur >= dTemp1 && dShatHatchalaSidur < dTemp2)
                    {
                        if (dShatGmarSidur > oParameters.dMinEndMishmeretMafilimChol)
                        {
                            iSugMishmeret = enSugMishmeret.Tzaharim.GetHashCode();
                        }
                    }
                    dTemp1 = oParameters.dMinEndMishmeretMafilimLilaChol1;
                    dTemp2 = oParameters.dMinStartMishmeretMafilimLilaChol;
                    //-	אם סוג יום [שליפת סוג יום לשליפת מכסה יומית (תאריך, מ.א.)] הוא אחד מתוך 01 או 03 או 04 או 05 וגם [שעת התחלה ראשונה] >= 17:00 וגם [שעת גמר אחרונה] > 21:00 אזי [סוג משמרת] = לילה
                    if (dShatHatchalaSidur > dTemp2 && dShatGmarSidur > dTemp1)
                    {
                        iSugMishmeret = enSugMishmeret.Liyla.GetHashCode();
                    }
                }
                //-	אם סוג יום [שליפת סוג יום לשליפת מכסה יומית (תאריך, מ.א.)] הוא אחד מתוך 11 או 13 או 14 או 15 או 16 או 17 או 18 וגם [שעת גמר אחרונה] > 13:00 אזי [סוג משמרת] = צהריים
                if (iSugYom == 11 || iSugYom == 13 || (iSugYom >= 14 && iSugYom <= 18))
                {
                    dTemp1 = oParameters.dMinEndMishmeretMafilimShishi;
                    if (dShatGmarSidur >= dTemp1)
                    {
                        iSugMishmeret = enSugMishmeret.Tzaharim.GetHashCode();
                    }
                }

                dTemp1 = oParameters.dMinEndMishmeretMafilimLilaChol2;
                //-	אם סוג יום [שליפת סוג יום לשליפת מכסה יומית (תאריך, מ.א.)] הוא אחד מתוך 01 או 03 או 04 או 05 וגם [שעת גמר אחרונה] >= 23:15 אזי [סוג משמרת] = לילה
                if (dShatGmarSidur >= dTemp1)
                {
                    iSugMishmeret = enSugMishmeret.Liyla.GetHashCode();
                }

                return iSugMishmeret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DateTime getCorrectDay(DateTime hour, DateTime dCardDate)
        {
            string date = hour.ToShortDateString();
            if (hour >= DateTime.Parse(date + " 00:01:00") && hour <= DateTime.Parse(date + " 07:59:00"))
            {
                return dCardDate.AddDays(1);
            }
            else return dCardDate;
        }

        public static int GetDiffDays(DateTime dFromDate, DateTime dToDate)
        {
            //הפרש בימים בין תאריכים
            TimeSpan ts = dToDate - dFromDate;
            int iDays = ts.Days;
            return iDays;
        }
      
    }
 
}
