using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KDSCommon.DataModels;
using KDSCommon.Interfaces;
using Microsoft.Practices.Unity;
using KDSCommon.Interfaces.DAL;
using KDSCommon.Helpers;
using KDSCommon.Enums;

namespace KdsLibrary
{
    public class ParametersManager : IParametersManager
    {
        private IUnityContainer _container;
        private DataTable _dtParams;

        public ParametersManager(IUnityContainer container)
        {
            _container = container;
        }

        public clParametersDM CreateClsParametrs(DateTime dCardDate, int iSugYom)
        {
            return CreateClsParametrs(dCardDate, iSugYom,"");
        }

        public clParametersDM CreateClsParametrs(DateTime dCardDate, int iSugYom, string type)
        {
            return CreateClsParametrs(dCardDate, iSugYom, type,null);
        }
        public clParametersDM CreateClsParametrs(DateTime dCardDate, int iSugYom, string type, DataTable dtParams)
        {
            clParametersDM cls = new clParametersDM();

            cls._Taarich = dCardDate;
            SetParameters(cls , dCardDate, iSugYom, dtParams);
            return cls;
        }

        private void SetParameters(clParametersDM cls, DateTime dCardDate, int iSugYom, DataTable dtParams)
        {
            if (dtParams == null)
            {
                //In order not to modify all the use of GetOneParam to the list of params - saving state for this method only
                _dtParams = _container.Resolve<IParametersDAL>().GetKdsParametrs();
            }
            else
            {
                _dtParams = dtParams;
            }
            StringBuilder sHour = new StringBuilder();
            string sTmp;
            try
            {
                //מגבלת שעת התחלה1- 
                sTmp = GetOneParam(1, dCardDate);
                cls.dSidurStartLimitHourParam1 = GetParamHour(sTmp, dCardDate);//GetOneParam(1, dCardDate);

                //2- שעת גמר רשמי
                sTmp = GetOneParam(2, dCardDate);
                cls.dSidurLimitShatGmar = GetParamHour(sTmp, dCardDate);


                //3 - מגבלת שעת סיום
                sTmp = GetOneParam(3, dCardDate.AddDays(1));
                cls.dSidurEndLimitHourParam3 = GetParamHour(sTmp, dCardDate);//GetOneParam(3, dCardDate);
                if (cls.dSidurEndLimitHourParam3 >= DateHelper.GetDateTimeFromStringHour("00:01", dCardDate) && cls.dSidurEndLimitHourParam3 <= DateHelper.GetDateTimeFromStringHour("07:59", dCardDate))
                {
                    cls.dSidurEndLimitHourParam3 = cls.dSidurEndLimitHourParam3.AddDays(1);
                }
                //4 -שעת גמר מפעילי מחשב 
                sTmp = GetOneParam(4, dCardDate.AddDays(1));
                cls.dSidurLimitShatGmarMafilim = GetParamHour(sTmp, dCardDate);
                if (cls.dSidurLimitShatGmarMafilim >= DateHelper.GetDateTimeFromStringHour("00:01", dCardDate) && cls.dSidurLimitShatGmarMafilim <= DateHelper.GetDateTimeFromStringHour("07:59", dCardDate))
                {
                    cls.dSidurLimitShatGmarMafilim = cls.dSidurLimitShatGmarMafilim.AddDays(1);
                }
                //9	תחילת תוספת לילה אגד	שעה	19:01
                sTmp = GetOneParam(9, dCardDate);
                cls.dTchilatTosefetLaila = GetParamHour(sTmp, dCardDate);

                //10	סיום תוספת לילה אגד	שעה	
                sTmp = GetOneParam(10, dCardDate);
                cls.dSiyumTosefetLaila = GetParamHour(sTmp, dCardDate);

                //11	תחילת תוספת לילה חוק	שעה	
                sTmp = GetOneParam(11, dCardDate);
                cls.dTchilatTosefetLailaChok = GetParamHour(sTmp, dCardDate);

                //12	סיום תוספת לילה חוק	שעה	
                sTmp = GetOneParam(12, dCardDate.AddDays(1));
                cls.dSiyumTosefetLailaChok = GetParamHour(sTmp, dCardDate.AddDays(1));

                //13	תחילת תוספת לילה יום נוכחי 
                sTmp = GetOneParam(13, dCardDate);
                cls.dTchilatTosefetLailaYomNochechi = GetParamHour(sTmp, dCardDate);

                //14	סיום תוספת לילה יום נוכחי 
                sTmp = GetOneParam(14, dCardDate);
                cls.dSiyumTosefetLailaYomNochechi = GetParamHour(sTmp, dCardDate);

                //18 - תחילת ארוחת צהריים
                sTmp = GetOneParam(18, dCardDate);
                cls.dStartAruchatTzaharayim = GetParamHour(sTmp, dCardDate);


                //19 - סיום ארוחת צהריים
                sTmp = GetOneParam(19, dCardDate);
                cls.dEndAruchatTzaharayim = GetParamHour(sTmp, dCardDate);

                //25- תאריך שמציין תחילת שעון קיץ (אגד) בפורמט DD/MM
                sTmp = GetOneParam(25, dCardDate);
                cls.dSummerStart = GetParamDate(sTmp, dCardDate);// String.IsNullOrEmpty(sTmp) ? DateTime.Parse(string.Concat("01/01", "/", DateHelper.cYearNull.ToString())) : DateTime.Parse(string.Concat(sTmp, "/", dCardDate.Year.ToString()));

                //26- תאריך שמציין סיום שעון קיץ (אגד) בפורמט  DD/MM
                sTmp = GetOneParam(26, dCardDate);
                cls.dSummerEnd = GetParamDate(sTmp, dCardDate);// String.IsNullOrEmpty(sTmp) ? DateTime.Parse(string.Concat("01/01", "/", DateHelper.cYearNull.ToString())) : DateTime.Parse(string.Concat(sTmp, "/", dCardDate.Year.ToString()));

                //שעת התחלה ראשונה מותרת לפעילות בסידור - מאפיין 29
                sTmp = GetOneParam(29, dCardDate);
                cls.dStartHourForPeilut = GetParamHour(sTmp, dCardDate);//GetOneParam(29, dCardDate);

                //שעת יציאה אחרונה מותרת לפעילות - מאפיין 30
                sTmp = GetOneParam(30, dCardDate.AddDays(1));
                cls.dEndHourForPeilut = GetParamHour(sTmp, dCardDate.AddDays(1));//GetOneParam(30, dCardDate);

                //32 -   שעת סיום שבת
                sTmp = GetOneParam(32, dCardDate);
                cls.dShatMaavarYom = GetParamHour(sTmp, dCardDate.AddDays(1));

                ////33- אורך נסיעה מינימלי לצורך תוספת אש"ל.
                sTmp = GetOneParam(33, dCardDate);
                cls.iKmMinTosafetEshel = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                ////34 - שעת התחלת סידור לצורך אש"ל בוקר
                sTmp = GetOneParam(34, dCardDate);
                cls.dStartSidurEshelBoker = GetParamHour(sTmp, dCardDate);

                ////35 - שעת סיום     סידור לצורך אש"ל בוקר
                sTmp = GetOneParam(35, dCardDate);
                cls.dEndSidurEshelBoker = GetParamHour(sTmp, dCardDate);

                ////36 - שעת התחלת סידור לצורך אש"ל צהריים
                sTmp = GetOneParam(36, dCardDate);
                cls.dStartSidurEshelTzaharayim = GetParamHour(sTmp, dCardDate);

                ////37 - שעת סיום     סידור לצורך אש"ל צהריים
                sTmp = GetOneParam(37, dCardDate);
                cls.dEndSidurEshelTzaharayim = GetParamHour(sTmp, dCardDate);

                ////זמן מינימלי לסידור לצורך אש"ל צהריים
                sTmp = GetOneParam(38, dCardDate);
                cls.iMinTimeSidurEshelTzaharayim = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);


                ////39 - שעת התחלת סידור לצורך אש"ל ערב
                sTmp = GetOneParam(39, dCardDate);
                cls.dStartSidurEshelErev = GetParamHour(sTmp, dCardDate);


                //פקטור נסיעות שירות בין גמר לתכנון, פרמטר מספר 42
                sTmp = GetOneParam(42, dCardDate);
                cls.fFactor = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);



                //41 - זמן חריגה התחלה / גמר על חשבון העובד
                sTmp = GetOneParam(41, dCardDate);
                cls.iZmanChariga = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //43-    פקטור נסיעות ריקות בין גמר לתכנון 
                sTmp = GetOneParam(43, dCardDate);
                cls.fFactorNesiotRekot = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //44 - אירועי קיץ - תאריך חוקי לתחילת סידורי ניהול ושיווק,
                sTmp = GetOneParam(44, dCardDate);
                cls.dStartNihulVShivik = GetParamDate(sTmp, dCardDate);//String.IsNullOrEmpty(sTmp) ? DateTime.Parse(string.Concat("01/01", "/", DateHelper.cYearNull.ToString())) : DateTime.Parse(string.Concat(sTmp, "/", dCardDate.Year.ToString()));

                //45 -  אירועי קיץ - תאריך חוקי לתחילת סידור תפעול,  
                sTmp = GetOneParam(45, dCardDate);
                cls.dStartTiful = GetParamDate(sTmp, dCardDate); //String.IsNullOrEmpty(sTmp) ? DateTime.Parse(string.Concat("01/01", "/", DateHelper.cYearNull.ToString())) : DateTime.Parse(string.Concat(sTmp, "/", dCardDate.Year.ToString()));

                //46 -   אירועי קיץ - תאריך חוקי לסיום סידור שיווק,ל,
                sTmp = GetOneParam(46, dCardDate);
                cls.dEndNihulVShivik = GetParamDate(sTmp, dCardDate); // String.IsNullOrEmpty(sTmp) ? DateTime.Parse(string.Concat("01/01", "/", DateHelper.cYearNull.ToString())) : DateTime.Parse(string.Concat(sTmp, "/", dCardDate.Year.ToString())); 

                //47 אירועי קיץ - תאריך חוקי לסיום סידור תפעול 
                sTmp = GetOneParam(47, dCardDate);
                cls.dEndTiful = GetParamDate(sTmp, dCardDate);//DateTime.Parse(string.Concat(GetOneParam(47, dCardDate), "/", dCardDate.Year.ToString()));

                //48  - קיץ - תאריך התחלה לצורך חישוב כניסת שבת) 
                sTmp = GetOneParam(48, dCardDate);
                cls.dStartShabat = GetParamDate(sTmp, dCardDate);//DateTime.Parse(string.Concat(GetOneParam(48, dCardDate), "/", dCardDate.Year.ToString()));

                //49  קיץ - תאריך סיום לצורך חישוב כניסת שבת 
                sTmp = GetOneParam(49, dCardDate);
                cls.dEndShabat = GetParamDate(sTmp, dCardDate);//DateTime.Parse(string.Concat(GetOneParam(49, dCardDate), "/", dCardDate.Year.ToString())); 

                //59 - אחוז תוספת לעובדי משק
                sTmp = GetOneParam(59, dCardDate);
                cls.fAchuzTosefetLeovdeyMeshek = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //פרמטר 60 - סידור קצר במפת התנועה
                sTmp = GetOneParam(60, dCardDate);
                cls.iShortSidurInTnuaMap = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //62 - תאריך ראשון לצורך תוספת סיכון
                sTmp = GetOneParam(62, dCardDate);
                cls.dDateFirstTosefetSikun = GetParamDate(sTmp, dCardDate);

                //63 - תאריך אחרון לצורך תוספת סיכון
                sTmp = GetOneParam(63, dCardDate);
                cls.dDateLastTosefetSikun = GetParamDate(sTmp, dCardDate);

                // 64 - גורם לחישוב ק"מ בנסיעה ריקה שלא מקטלוג נסיעות
                sTmp = GetOneParam(64, dCardDate);
                cls.fGoremChishuvKm = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                ////אש"ל מיוחד – מינימום נוכחות רצופה- 65 - 
                sTmp = GetOneParam(65, dCardDate);
                cls.iNochehutMinRetzufa = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                ////66 - אש"ל מיוחד – נוכחות מכסימום קבוצה 1
                sTmp = GetOneParam(66, dCardDate);
                cls.iNochehutMaxEshel1 = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                ////67 - אש"ל מיוחד – נוכחות מכסימום קבוצה 2
                sTmp = GetOneParam(67, dCardDate);
                cls.iNochehutMaxEshel = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                ////68 - אש"ל מיוחד – נוכחות מינימום קבוצה 3
                sTmp = GetOneParam(68, dCardDate);
                cls.iNochehueMinEshel = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //69 - אש"ל מיוחד – זמן התחלה לבוקר קבוצה 2
                sTmp = GetOneParam(69, dCardDate);
                cls.dZmanHatchalaBoker2 = GetParamHour(sTmp, dCardDate);
                //70 - אש"ל מיוחד חריג בוקר – התחלה לפני
                sTmp = GetOneParam(70, dCardDate);
                cls.dHatchalaEshelBoker = GetParamHour(sTmp, dCardDate);
                //71 -אש"ל מיוחד חריג בוקר – גמר אחרי
                sTmp = GetOneParam(71, dCardDate);
                cls.dGmarEshelBoker = GetParamHour(sTmp, dCardDate);
                // 72 -אש"ל מיוחד – זמן התחלה לבוקר
                sTmp = GetOneParam(72, dCardDate);
                cls.dZmanHatchalaBoker = GetParamHour(sTmp, dCardDate);

                //73 -אש"ל מיוחד – זמן סיום לבוקר
                sTmp = GetOneParam(73, dCardDate);
                cls.dZmanSiyumBoker = GetParamHour(sTmp, dCardDate);
                //74 -אש"ל מיוחד – זמן סיום לצהרים
                sTmp = GetOneParam(74, dCardDate);
                cls.dZmanSiyumTzharayim = GetParamHour(sTmp, dCardDate);
                //75 -אש"ל מיוחד – זמן התחלה לערב
                sTmp = GetOneParam(75, dCardDate);
                cls.dZmanHatchalaErev = GetParamHour(sTmp, dCardDate);

                //76 - תמריץ-שעות נוספות שאינן לתשלום
                sTmp = GetOneParam(76, dCardDate);
                cls.iTamrizNosafotLoLetashlum = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //77 - תמריץ-מכסימום ש.נ לתמריץ תפקיד
                sTmp = GetOneParam(77, dCardDate);
                cls.iMaxNosafotTafkid = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //78 - תמריץ-מכסימום ש.נ לתמריץ נהגות
                sTmp = GetOneParam(78, dCardDate);
                cls.iMaxNosafotNahagut = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //80 - שעת גמר מקסימלית לסידור מסוג נהגות          
                sTmp = GetOneParam(80, dCardDate.AddDays(1));
                cls.dNahagutLimitShatGmar = GetParamHour(sTmp, dCardDate.AddDays(1));

                //81 - פרמיית נהגות גורם אלמנט זר
                sTmp = GetOneParam(81, dCardDate);
                cls.fElementZar = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                // 82 - פרמיית נהגות גורם חלוקה לתוספת גיל קשיש
                sTmp = GetOneParam(82, dCardDate);
                cls.iChalukaTosefetGilKashish = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //83 - פרמיית נהגות גורם חלוקה לתוספת גיל קשישון
                sTmp = GetOneParam(83, dCardDate);
                cls.iChalukaTosefetGilKshishon = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //85 - בדיקת סידור התיצבות לסידור התייצבות
                sTmp = GetOneParam(85, dCardDate);
                cls.iBdikatIchurLesidurHityatzvut = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //86 - אחוז הסתגלות בפרמיה לרכב מיפרקי
                sTmp = GetOneParam(86, dCardDate);
                cls.fAchuzHistaglutPremyaMifraki = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //87- מקדם תוספת זמן לפי רכב
                sTmp = GetOneParam(87, dCardDate);
                cls.fMekademTosefetZmanLefiRechev = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //88- מקסימום זמן ריקה הנכללת בהכנה ראשונה עד 8:00
                sTmp = GetOneParam(88, dCardDate);
                cls.iMaxZmanRekaAdShmone = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //89- מקסימום זמן ריקה הנכללת בהכנה ראשונה אחרי 8:00
                sTmp = GetOneParam(89, dCardDate);
                cls.iMaxZmanRekaAchreyShmone = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //90 - רדיוס מרחק משעון לבדיקת התייצבות
                sTmp = GetOneParam(90, dCardDate);
                cls.iRadyusMerchakMeshaonLehityazvut = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                // 91 - פער בין סידורי נהגות המחייב התייצבות
                sTmp = GetOneParam(91, dCardDate);
                cls.iPaarBeinSidurimMechayevHityazvut = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //גבול עליון של שעת התחלה - 93
                sTmp = GetOneParam(93, dCardDate);
                cls.dSidurEndLimitShatHatchala = GetParamHour(sTmp, dCardDate);

                //-  95 אגד תעבורה תחילת תוספת לילה   
                sTmp = GetOneParam(95, dCardDate);
                cls.dTchilatTosLilaTaavura = GetParamHour(sTmp, dCardDate);

                //אגד תעבור סיום תוספת לילה  - 96
                sTmp = GetOneParam(96, dCardDate);
                cls.dSiyumTosLilaTaavura = GetParamHour(sTmp, dCardDate.AddDays(1));

                //97 חלקיות אחוז מילואים
                sTmp = GetOneParam(97, dCardDate);
                cls.fChelkiyutAchuzMiluim = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //98 - מקסימום דקות בפועל לכניסות לפי הצורך
                sTmp = GetOneParam(98, dCardDate);
                cls.iMaxMinutsForKnisot = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //- מציין את מספר החודשים שניתן להציג  100
                sTmp = GetOneParam(100, dCardDate);
                cls.iMaxMonthToDisplay = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //זמן מינימום לסידור חול להשלמה-  101
                sTmp = GetOneParam(101, dCardDate);
                cls.iHashlamaYomRagil = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //זמן מינימום לסידור שישי/ערב חג להשלמה-  102
                sTmp = GetOneParam(102, dCardDate);
                cls.iHashlamaShisi = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //זמן מינימום לסידור שבתון להשלמה-  103
                sTmp = GetOneParam(103, dCardDate);
                cls.iHashlamaShabat = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //104 - הפרש מכסימלי בין סידורים לרציפות 
                sTmp = GetOneParam(104, dCardDate);
                cls.iMinTimeBetweenSidurim = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //106 -מינימום הפרש בין סידורים לפיצול קיץ
                sTmp = GetOneParam(106, dCardDate);
                cls.iMinHefreshSidurimLepitzulSummer = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);


                //107 - מינימום הפרש בין סידורים לפיצול חורף 
                sTmp = GetOneParam(107, dCardDate);
                cls.iMinHefreshSidurimLeptzulWinter = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //מכסימום השלמות ביום חול-  108
                sTmp = GetOneParam(108, dCardDate);
                cls.iHashlamaMaxYomRagil = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //מכסימום השלמות בשישי/ע.ח-  109
                sTmp = GetOneParam(109, dCardDate);
                cls.iHashlamaMaxShisi = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //מכסימום השלמות בשבתון-  110
                sTmp = GetOneParam(110, dCardDate);
                cls.iHashlamaMaxShabat = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //120 - זמן הכנת מכונה ראשונה ביום
                sTmp = GetOneParam(120, dCardDate);
                cls.iPrepareFirstMechineMaxTime = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //121 - זמן הכנת מכונות נוספות ביום
                sTmp = GetOneParam(121, dCardDate);
                cls.iPrepareOtherMechineMaxTime = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //122 - מקסימום זמן לסה"כ הכנות מכונה נוספות,
                sTmp = GetOneParam(122, dCardDate);
                cls.iPrepareOtherMechineTotalMaxTime = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //123 - מקסימום זמן לסה"כ הכנות מכונה (ראשונה ונוספות), ביום
                sTmp = GetOneParam(123, dCardDate);
                cls.iPrepareAllMechineTotalMaxTimeInDay = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //124 - מקסימום הכנות מכונה מותר בסידור    
                sTmp = GetOneParam(124, dCardDate);
                cls.iPrepareAllMechineTotalMaxTimeForSidur = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //126  
                sTmp = GetOneParam(126, dCardDate);
                cls.iMaxZmanRekaNichleletafter8 = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);



                //מכסימום שעורי נהיגה למורה בסידור יחיד141)
                sTmp = GetOneParam(141, dCardDate);
                cls.iMaxDriverLessons = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //זמן הלבשה -  143
                sTmp = GetOneParam(143, dCardDate);
                cls.iZmanHalbash = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //תוספת זמן לסידור גרירה בפועל בזמן כוננות -  145
                sTmp = GetOneParam(145, dCardDate);
                cls.iTosefetZmanGrira = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //מקסימום זמן המתנה אילת-  148
                sTmp = GetOneParam(148, dCardDate);
                cls.iMaxZmanHamtanaEilat = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //149 - אורך נסיעה קצרה לאילת
                sTmp = GetOneParam(149, dCardDate);
                cls.fOrechNesiaKtzaraEilat = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);


                //152- מינימום זמן עבודה בנהגות הנחשב ליום עבודה
                sTmp = GetOneParam(152, dCardDate);
                cls.iMinZmanAvodaBenahagut = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //153- מינימום השלמה חריגה למותאם בנהגות
                sTmp = GetOneParam(153, dCardDate);
                cls.iMinHashlamaCharigaForMutamutDriver = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //154- חפיפה מותרת בסידורי ניהול תנועה
                sTmp = GetOneParam(154, dCardDate);
                cls.iMaxChafifaBeinSidureyNihulTnua = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //157 - מכסימום רציפות חודשית לתשלום
                sTmp = GetOneParam(157, dCardDate);
                cls.iMaxRetzifutChodshitLetashlum = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //158 - מכסימום דקות נוספות חודשי לעבודת סדרן
                sTmp = GetOneParam(158, dCardDate);
                cls.iMaxDakotNosafotSadran = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //159 - מכסימום דקות נוספות חודשי לעבודת פקח
                sTmp = GetOneParam(1159, dCardDate);
                cls.iMaxDakotNosafotPakach = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //160 - מכסימום דקות נוספות חודשי לעבודת רכז
                sTmp = GetOneParam(160, dCardDate);
                cls.iMaxDakotNosafotRakaz = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //161 - מקסימום זמן המתנה
                sTmp = GetOneParam(161, dCardDate);
                cls.iMaximumHmtanaTime = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                // מקסימום דקות נוספות 125%-  162
                sTmp = GetOneParam(162, dCardDate);
                cls.iMaxDakotNosafot = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                // זמן גרירה ראשונה מינימלי באזור דרום   בזמן כוננות גרירה  163
                sTmp = GetOneParam(163, dCardDate);
                cls.iMinZmanGriraDarom = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                // זמן גרירה ראשונה מינימלי באזור צפון   בזמן כוננות גרירה  164
                sTmp = GetOneParam(164, dCardDate);
                cls.iMinZmanGriraTzafon = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                // 165  זמן גרירה ראשונה מינימלי באזור ירושלים   בזמן כוננות גרירה
                sTmp = GetOneParam(165, dCardDate);
                cls.iMinZmanGriraJrusalem = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);



                ////6- שעה שמציינת שעת כניסת השבת בחורף בפורמט HH:MM
                //sTmp = GetOneParam(6, dCardDate).Remove(2,1);
                //iShabatStartInAuttom = int.Parse(GetOneParam(6, dCardDate).Remove(2, 1));

                ////7- שעה שמציינת שעת כניסת השבת בקיץ בפורמט HH:MM
                //iShabatStartInSummer = int.Parse(GetOneParam(7, dCardDate).Remove(2, 1));


                //נקבע את שעת כניסת השבת לפי התקופה
                // if ((dCardDate >= dSummerStart) && (dCardDate <= dSummerEnd))
                // {//קיץ                        
                //     iShabatStart = iShabatStartInSummer; //פרמטר   -7  
                // }
                // else
                // {//חורף
                //     iShabatStart = iShabatStartInAuttom; //פרמטר 6  
                //}


                SetShatKnisatShabat(cls,dCardDate, iSugYom);


                //166 - מינימום יום עבודה לצורך זכאות הלבשה
                sTmp = GetOneParam(166, dCardDate);
                cls.iMinYomAvodaForHalbasha = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //167 - מינימום יום עבודה למותאם לצורך זכאות הלבשה
                sTmp = GetOneParam(167, dCardDate);
                cls.iMinDakotLemutamLeHalbasha = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //168 - מינימום תפקיד לצורך גמול ביום ללא נהיגה
                sTmp = GetOneParam(168, dCardDate);
                cls.iMinDakotGmulLeloNehiga = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                // 169 - מינימום תפקיד לצורך גמול ביום עם נהיגה
                sTmp = GetOneParam(169, dCardDate);
                cls.iMinDakotGmulImNehiga = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                // 170 - מקסימום זכאות לפיצולים ביום עבודה
                sTmp = GetOneParam(170, dCardDate);
                cls.iMaxPitzulLeyom = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //אחוז סיכון לנסיעה בדרגת סיכון 1 171
                sTmp = GetOneParam(171, dCardDate);
                cls.fAchuzSikun1 = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //אחוז סיכון לנסיעה בדרגת סיכון 2 172
                sTmp = GetOneParam(172, dCardDate);
                cls.fAchuzSikun2 = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //אחוז סיכון לנסיעה בדרגת סיכון 3 173
                sTmp = GetOneParam(173, dCardDate);
                cls.fAchuzSikun3 = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //אחוז סיכון לנסיעה בדרגת סיכון 4 174
                sTmp = GetOneParam(174, dCardDate);
                cls.fAchuzSikun4 = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //אחוז סיכון להמתנה באזור סיכון (אלמנט 64) 175
                sTmp = GetOneParam(175, dCardDate);
                cls.fAchuzSikunLehamtana = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //176 - מקסימום ימים להמרת שעות נוספןת לחופש
                sTmp = GetOneParam(176, dCardDate);
                cls.iMaxYamimHamaratShaotNosafot = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //מקדם ניפוח גמול חסכון מיוחד 177 )
                sTmp = GetOneParam(177, dCardDate);
                cls.fMekademNipuachGmulChisachon = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                // 178 - גמול נוספות-מיכסה יומית צעיר
                sTmp = GetOneParam(178, dCardDate);
                cls.iGmulNosafotTzair = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //179 - גמול נוספות-מיכסה יומית קשיישון
                sTmp = GetOneParam(179, dCardDate);
                cls.iGmulNosafotKshishon = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //180 - גמול נוספות-מיכסה יומית קשיש
                sTmp = GetOneParam(180, dCardDate);
                cls.iGmulNosafotKashish = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                sTmp = GetOneParam(181, dCardDate);
                cls.iGriraAddMinTime = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //182 - מכסימום דקות נוספות חודשי לעבודת משלח
                sTmp = GetOneParam(182, dCardDate);
                cls.iMaxDakotNosafotMeshaleach = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //183 - מכסימום דקות נוספות חודשי לעבודת קופאי
                sTmp = GetOneParam(183, dCardDate);
                cls.iMaxDakotNosafotKupai = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //184 - מכסימום רציפות יומית לתשלום
                sTmp = GetOneParam(184, dCardDate);
                cls.iMaxRetzifutYomiLetashlum = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //מכסימום רציפות חודשית לתשלום עם גלישה185- 
                sTmp = GetOneParam(185, dCardDate);
                cls.iMaxRetzifutChodshitImGlisha = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //186 - מינימום זמן משמרת שניה במשק 
                sTmp = GetOneParam(186, dCardDate);
                cls.iMinZmanMishmeretShniaBameshek = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //187  - סיום עבודה במשק במשמרת שניה
                sTmp = GetOneParam(187, dCardDate);
                cls.dSiyumMishmeretShniaBameshek = GetParamHour(sTmp, dCardDate);

                //188  מכסימום השלמה ליום עבודה על חשבון פרמיה בנהיגה
                sTmp = GetOneParam(188, dCardDate);
                cls.iMaxHashlamaAlCheshbonPremia = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //189 - אחוז פרמיה למחלקת רישום
                sTmp = GetOneParam(189, dCardDate);
                cls.fAchuzPremiaRishum = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //190  - דקות לקבלת יום הדרכה מלא
                sTmp = GetOneParam(190, dCardDate);
                cls.iDakotKabalatYomHadracha = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //191 - אחוז פרמיית מחסן כרטיסים
                sTmp = GetOneParam(191, dCardDate);
                cls.fAchuzPremiatMachsanKartisim = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //192 - מינימום דקות שנחשב ליום עבודה
                sTmp = GetOneParam(192, dCardDate);
                cls.iMinDakotNechshavYomAvoda = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //194 -מפעילי מחשב חול בוקר
                sTmp = GetOneParam(194, dCardDate);
                cls.iDakotMafileyMachshevColBoker = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //195 -מפעילי מחשב חול צהריים
                sTmp = GetOneParam(195, dCardDate);
                cls.iDakotMafileyMachshevColTzaharim = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //196 -מפעילי מחשב חול ערב
                sTmp = GetOneParam(196, dCardDate);
                cls.iDakotMafileyMachshevColLiyla = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //197 -מפעילי מחשב חול בוקר
                sTmp = GetOneParam(197, dCardDate);
                cls.iDakotMafileyMachshevErevChagBoker = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //198 -מפעילי מחשב חול צהריים
                sTmp = GetOneParam(198, dCardDate);
                cls.iDakotMafileyMachshevErevChagTzaharim = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //199 -מפעילי מחשב חול ערב
                sTmp = GetOneParam(199, dCardDate);
                cls.iDakotMafileyMacshevChagBoker = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //200 -מפעילי מחשב חול ערב
                sTmp = GetOneParam(200, dCardDate);
                cls.iDakotMafileyMacshevChagLiyla = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //202 - מפעילי מחשב שכירים התחלת תשלום תוספת לילה
                sTmp = GetOneParam(202, dCardDate);
                cls.dTchlatTashlumTosefetLilaMafilim = GetParamHour(sTmp, dCardDate);

                //203 - אחוז פרמיה למנהל בית ספר לנהיגה
                sTmp = GetOneParam(203, dCardDate);
                cls.fAchuzPremiaMenahel = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //205 - מפעלים משמרת צהרים חול מינימום התחלה
                sTmp = GetOneParam(205, dCardDate);
                cls.dMinStartMishmeretMafilimChol = GetParamHour(sTmp, dCardDate);

                //206 - מפעילים משמרת צהריים חול מקסימום התחלה
                sTmp = GetOneParam(206, dCardDate);
                cls.dMaxStartMishmeretMafilimChol = GetParamHour(sTmp, dCardDate);

                //207 - מפעילים משמרת צהריים חול מינימום סיום
                sTmp = GetOneParam(207, dCardDate);
                cls.dMinEndMishmeretMafilimChol = GetParamHour(sTmp, dCardDate);

                //208 - מפעילים משמרת צהריים שישי מינימום סיום
                sTmp = GetOneParam(208, dCardDate);
                cls.dMinEndMishmeretMafilimShishi = GetParamHour(sTmp, dCardDate);

                //209 - מפעילים משמרת לילה חול מינימום התחלה
                sTmp = GetOneParam(209, dCardDate);
                cls.dMinStartMishmeretMafilimLilaChol = GetParamHour(sTmp, dCardDate);

                //210 - מפעילים משמרת לילה חול מינימום סיום  תלוי התחלה
                sTmp = GetOneParam(210, dCardDate);
                cls.dMinEndMishmeretMafilimLilaChol1 = GetParamHour(sTmp, dCardDate);

                //211 -מפעילים משמרת לילה חול מינימום סיום לא תלוי התחלה
                sTmp = GetOneParam(211, dCardDate.AddDays(1));
                cls.dMinEndMishmeretMafilimLilaChol2 = GetParamHour(sTmp, dCardDate);

                //220 שעת התחלה משמרת לילה
                sTmp = GetOneParam(220, dCardDate);
                cls.dTchilatMishmeretLilaMafilim = GetParamHour(sTmp, dCardDate);

                //221 שעת סיום משמרת לילה
                sTmp = GetOneParam(221, dCardDate);
                cls.dSiyumMishmeretLilaMafilim = GetParamHour(sTmp, dCardDate.AddDays(1));

                //222 -מקסימום נוכחות לויזה בודדת
                sTmp = GetOneParam(222, dCardDate);
                cls.iMaxNochehutVisaBodedet = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //223 -  נוכחות לויזה עד 14:00
                sTmp = GetOneParam(223, dCardDate);
                cls.iNuchehutVisa1 = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //224 - מקסימום הנוכחות  לויזה  - יום ראשון- עד 14:00
                sTmp = GetOneParam(224, dCardDate);
                cls.iMaxNuchehutVisaPnimRishon1 = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //225- מינימום הנוכחות לויזה  יום ראשון- עד-  14:00
                sTmp = GetOneParam(225, dCardDate);
                cls.iMinNuchehutVisaPnimRishon1 = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //226 - מקסימום הנוכחות לויזה  יום ראשון- מ- 14:00
                sTmp = GetOneParam(226, dCardDate);
                cls.iMaxNuchehutVisaPnimRishon2 = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //227 - מינימום הנוכחות לויזה - יום ראשון-   מ- 14:00
                sTmp = GetOneParam(227, dCardDate);
                cls.iMinNuchehutVisaPnimRishon2 = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //228 - ויזה פנים -מקסימום נוכחות
                sTmp = GetOneParam(228, dCardDate);
                cls.iMaxNochehutVisaPnim = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //229 - ויזה פנים -  מינימום נוכחות
                sTmp = GetOneParam(229, dCardDate);
                cls.iMinNochehutVisaPnim = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //230 - ויזה פנים נוכחות - לא שבתון
                sTmp = GetOneParam(230, dCardDate);
                cls.iNochehutVisaPnimNoShabaton = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //-231 - מקסימום נוכחות ויזה פנים יום אמצעי
                sTmp = GetOneParam(231, dCardDate);
                cls.iMaxNochehutVisaPnimEmtzai = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //-232 - מינימום נוכחות ויזה פנים יום אמצעי
                sTmp = GetOneParam(232, dCardDate);
                cls.iMinNochehutVisaPnimEmtzai = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //-233 - מקסימום נוכחות ויזה פנים יום אחרון
                sTmp = GetOneParam(233, dCardDate);
                cls.iMaxNochehutVisaPnimAcharon = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //-234 - מינימום נוכחות ויזה פנים יום אחרון
                sTmp = GetOneParam(234, dCardDate);
                cls.iMinNochehutVisaPnimAcharon = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //-235 - מקסימום נוכחות ויזה צה"ל  לא יום אחרון
                sTmp = GetOneParam(235, dCardDate);
                cls.iMaxNochehutVisaTzahalLoAcharon = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //236 --ויזה פנים שעת התחלה לתשלום - יום אחרון
                sTmp = GetOneParam(236, dCardDate);
                cls.dShatHatchalaVisaPnimAcharon = GetParamHour(sTmp, dCardDate);

                //237 -ויזה צה"ל שעת התחלה לתשלום - יום אחרון
                sTmp = GetOneParam(237, dCardDate);
                cls.dShatHatchalaVisaTzahalAcharon = GetParamHour(sTmp, dCardDate);

                //238- מינימום דקות לזכאות לנסיעות
                sTmp = GetOneParam(238, dCardDate);
                cls.iMinDakotLezakautNesiot = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //239- מינימום דקות נהיגה ותנועה לזכות נסיעות
                sTmp = GetOneParam(239, dCardDate);
                cls.iMinDakotNehigaVetnuaLezakautNesiot = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //240- סיום לילה לעובד עם מאפיין לא מפעיל
                sTmp = GetOneParam(240, dCardDate);
                cls.dSiyumLilaLeovedLoMafil = GetParamHour(sTmp, dCardDate.AddDays(1));

                //241- סיום לילה מוצ''ש מפעיל
                sTmp = GetOneParam(241, dCardDate);
                cls.dSiyumLilaMotsashMafil = GetParamHour(sTmp, dCardDate.AddDays(1));

                //242- שעת גמר לבדיקת יום של שעת יציאה
                sTmp = GetOneParam(242, dCardDate);
                cls.dShatGmarNextDay = GetParamHour(sTmp, dCardDate.AddDays(1));

                //244- התחלה מותרת-טווח עליון-נהגות וניהול תנועה
                sTmp = GetOneParam(244, dCardDate);
                cls.dShatHatchalaNahagutNihulTnua = GetParamHour(sTmp, dCardDate.AddDays(1));

                //245- פרמיה גבוהה
                sTmp = GetOneParam(245, dCardDate);
                cls.fHighPremya = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //246 - תחילת ארוחת צהריים
                sTmp = GetOneParam(246, dCardDate);
                cls.dStartAruchatTzaharayim246 = GetParamHour(sTmp, dCardDate);


                //265 - חודש הפעלת שינוי תקנון סוציאלי
                sTmp = GetOneParam(265, dCardDate);
                cls.dChodeshTakanonSoziali = String.IsNullOrEmpty(sTmp) ? DateTime.MaxValue : DateTime.Parse(sTmp);


                //268 - חודש הפעלת שינוי תקנון סוציאלי לחברים
                //sTmp = GetOneParam(268, dCardDate);
                //dChodeshTakanonSozialiFrinds = String.IsNullOrEmpty(sTmp) ? DateTime.MaxValue : DateTime.Parse(sTmp);


                //247 - תחילת ארוחת צהריים
                sTmp = GetOneParam(247, dCardDate);
                cls.dEndAruchatTzaharayim247 = GetParamHour(sTmp, dCardDate);

                sTmp = GetOneParam(272, dCardDate);
                cls.dTaarichTokefShgiotHachtamatShaon = String.IsNullOrEmpty(sTmp) ? DateTime.MaxValue : DateTime.Parse(sTmp);

                // תאריך ביטול תמריץ נהגות
                sTmp = GetOneParam(273, dCardDate);
                cls.dTaarichBitulTamrizNahagut = String.IsNullOrEmpty(sTmp) ? DateTime.MinValue : DateTime.Parse(sTmp);

                // 274
                sTmp = GetOneParam(274, dCardDate);
                cls.dShaaGrirotChol = GetParamHour(sTmp, dCardDate);
               
                // 275
                sTmp = GetOneParam(275, dCardDate);
                cls.dShaaGrirotErevChagShishi = GetParamHour(sTmp, dCardDate);


                //252 - מספר ימים בתוקף 45 יום                
                sTmp = GetOneParam(252, dCardDate);
                cls.iValidDays = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //276- התחלה מותרת-טווח עליון-גרירה           
                sTmp = GetOneParam(276, dCardDate);
                cls.dShatHatchalaGrira = GetParamHour(sTmp, dCardDate.AddDays(1));
                          
                // 277
                sTmp = GetOneParam(277, dCardDate);
                cls.dTaarichHafalatMichsot = String.IsNullOrEmpty(sTmp) ? DateTime.MaxValue : DateTime.Parse(sTmp);

                // 278
                sTmp = GetOneParam(278, dCardDate);
                cls.dTaarichmichsatHachanatMechona = String.IsNullOrEmpty(sTmp) ? DateTime.MaxValue : DateTime.Parse(sTmp);

                // 279
                sTmp = GetOneParam(279, dCardDate);
                cls.dTaarichPremiatNahagut = String.IsNullOrEmpty(sTmp) ? DateTime.MinValue : DateTime.Parse(sTmp);

                //283
                sTmp = GetOneParam(283, dCardDate);
                cls.dTaarichBitulEshel = String.IsNullOrEmpty(sTmp) ? DateTime.MinValue : DateTime.Parse(sTmp);

                //284
                sTmp = GetOneParam(284, dCardDate);
                cls.dTaarichBitulNosafotAlTikni = String.IsNullOrEmpty(sTmp) ? DateTime.MinValue : DateTime.Parse(sTmp);

                //285
                sTmp = GetOneParam(285, dCardDate);
                cls.dTaarichHafalatNochechutBeyomMiluim = String.IsNullOrEmpty(sTmp) ? DateTime.MinValue : DateTime.Parse(sTmp);

                //286
                sTmp = GetOneParam(286, dCardDate);
                cls.dTaarichHafalatHafchataZmanNesia = String.IsNullOrEmpty(sTmp) ? DateTime.MinValue : DateTime.Parse(sTmp);

                //287
                sTmp = GetOneParam(287, dCardDate);
                cls.iDakotHafchataZmanNesia = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //288
                sTmp = GetOneParam(288, dCardDate);
                cls.dTaarichBitulPremiaNahagutForHoraa = String.IsNullOrEmpty(sTmp) ? DateTime.MinValue : DateTime.Parse(sTmp);

                //289
                sTmp = GetOneParam(289, dCardDate);
                cls.dTaarichHafalatEshelMichshuv = String.IsNullOrEmpty(sTmp) ? DateTime.MinValue : DateTime.Parse(sTmp);
                
                //290
                sTmp = GetOneParam(290, dCardDate);
                cls.dParam290 = GetParamHour(sTmp, dCardDate);

                //291
                sTmp = GetOneParam(291, dCardDate);
                cls.dParam291 = GetParamHour(sTmp, dCardDate.AddDays(1));

                //292
                sTmp = GetOneParam(292, dCardDate);
                cls.dParam292 = GetParamHour(sTmp, dCardDate);
                //293
                sTmp = GetOneParam(293, dCardDate);
                cls.dParam293 = GetParamHour(sTmp, dCardDate.AddDays(1));

                //294
                sTmp = GetOneParam(294, dCardDate);
                cls.dParam294 = GetParamHour(sTmp, dCardDate);

                //295
                sTmp = GetOneParam(295, dCardDate);
                cls.dParam295 = GetParamHour(sTmp, dCardDate);

                //296
                sTmp = GetOneParam(296, dCardDate);
                cls.dParam296 = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //297
                sTmp = GetOneParam(297, dCardDate);
                cls.dParam297 = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //298
                sTmp = GetOneParam(298, dCardDate);
                cls.dParam298 = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //299
                sTmp = GetOneParam(299, dCardDate);
                cls.dParam299 = String.IsNullOrEmpty(sTmp) ? DateTime.MinValue : DateTime.Parse(sTmp);
 
                //300
                sTmp = GetOneParam(300, dCardDate);
                cls.iMaxYamimGmulYeriET = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //301
                sTmp = GetOneParam(301, dCardDate);
                cls.dParam301 = String.IsNullOrEmpty(sTmp) ? DateTime.MinValue : DateTime.Parse(sTmp);

                //302
                sTmp = GetOneParam(302, dCardDate);
                cls.dTaarichChishuvElementZar = String.IsNullOrEmpty(sTmp) ? DateTime.MinValue : DateTime.Parse(sTmp);

                //503 -אגד תעבורה- מכסת שעות חודשית 
                sTmp = GetOneParam(503, dCardDate);
                cls.fMichsatSaotChodshitET = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //504 -אגד תעבורה- בסיס לחישוב פרמיית נהיגה
                sTmp = GetOneParam(504, dCardDate);
                cls.fBasisLechishuvPremia = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //261 - ויסות דורש רכב בכרטיס עבוד
                sTmp = GetOneParam(261, dCardDate);
                cls.iVisutMustRechevWC = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //263 - מספר הימים שניתן לראות כרטיס עבודה ללא סידורים (נהג או משתמש רגיל)
                sTmp = GetOneParam(263, dCardDate);
                cls.iDaysToViewWorkCard = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //505 - אגד תעבורה - מקסימום פרמיית נהיגה
                sTmp = GetOneParam(505, dCardDate);
                cls.fMaxPremiatNehiga = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);


                //506 -תעבורה-אלעד-בסיס לחישוב פרמיית נהיגה
                sTmp = GetOneParam(506, dCardDate);
                cls.fBasisLechishuvPremiatNehigaETElad = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //507 - תעבורה-אלעד-מכסימום פרמיית נהיגה
                sTmp = GetOneParam(507, dCardDate);
                cls.fMaxPremiatNehigaETElad = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //508 -תעבורה-פקטור לתשלום פיצולים
                sTmp = GetOneParam(508, dCardDate);
                cls.fFactorLetashlumPizulimTaavura = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //509 -תעבורה-אלעד-פקטור לתשלום פיצולים
                sTmp = GetOneParam(509, dCardDate);
                cls.fFactorLetashlumPizulimTaavuraElad = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

            }
            catch (Exception ex)
            {
                throw new Exception("Error In clParameters: " + ex);
            }
        }
        private DateTime GetParamDate(string sDate, DateTime dCardDate)
        {
            DateTime tmpDate = DateTime.MinValue;
            string[] arrDate;
            if (!string.IsNullOrEmpty(sDate))
            {
                arrDate = sDate.Split(char.Parse("/"));
                if (arrDate.Length > 2)
                {
                    tmpDate = new DateTime(int.Parse(arrDate[2]), int.Parse(arrDate[1]), int.Parse(arrDate[0]));
                }
                else
                {
                    tmpDate = new DateTime(dCardDate.Year, int.Parse(arrDate[1]), int.Parse(arrDate[0]));
                }
            }
            else
            {
                tmpDate = new DateTime(DateHelper.cYearNull, 1, 1);
            }

            return tmpDate;
        }

        private DateTime GetParamHour(string sHour, DateTime dCardDate)
        {
            DateTime dTemp;
            if (String.IsNullOrEmpty(sHour))
            {
                dTemp = new DateTime(DateHelper.cYearNull, 1, 1);
            }
            else
            {
                dTemp = DateHelper.GetDateTimeFromStringHour(sHour, dCardDate);
            }



            return dTemp;
        }

        private string GetOneParam(int iParamNum, DateTime dDate)
        {   //הפונקציה מקבלת קוד פרמטר ותאריך ומחזירה את הערך
            //            string sParamVal = "";
            //DataRow[] dr;

            //dr = dtParameters.Select(string.Concat("kod_param=", iParamNum.ToString(), " and Convert('", dDate.ToShortDateString(), "','System.DateTime') >= me_taarich and Convert('", dDate.ToShortDateString(), "', 'System.DateTime') <= ad_taarich"));
            ////dr = dtParameters.Select(string.Concat("kod_param=", iParamNum.ToString()));
            //if (dr.Length > 0)
            //{
            //    sParamVal = dr[0]["erech_param"].ToString();
            //}
            //dr = null;
            //            sParamVal = GetValueOfKod(iParamNum, dDate);
            //return sParamVal;
            try
            {
                var rows = (from c in _dtParams.AsEnumerable()
                            where c.Field<int>("kod_param").Equals(iParamNum)
                            && c.Field<DateTime>("me_taarich") <= dDate
                            && c.Field<DateTime>("ad_taarich") >= dDate
                            select c.Field<string>("erech_param"));//.First().ToString();
                if (rows.Count() > 0)
                    return rows.First().ToString();
                else return "";

            }
            catch (Exception ex)
            {
                throw new Exception("GetOneParam:" + ex.Message);
            }
        }
        private void SetShatKnisatShabat(clParametersDM cls, DateTime dTaarich, int iSugYom)
        {

            if (iSugYom == enSugYom.ErevYomHatsmaut.GetHashCode())
            {//יום העצמאות - פרמטר 8
                cls.dKnisatShabat = DateHelper.GetDateTimeFromStringHour(GetOneParam(8, dTaarich), dTaarich);
            }
            else
            {
                //נקבע את שעת כניסת השבת לפי התקופה
                if ((dTaarich >= cls.dStartShabat) && (dTaarich <= cls.dEndShabat))
                {//פרמטר 7 קיץ                        
                    cls.dKnisatShabat = DateHelper.GetDateTimeFromStringHour(GetOneParam(7, dTaarich), dTaarich);
                    // iShabatStart = iShabatStartInSummer;
                }
                else
                {//חורף פרמטר 6
                    cls.dKnisatShabat = DateHelper.GetDateTimeFromStringHour(GetOneParam(6, dTaarich), dTaarich);
                    //iShabatStart = iShabatStartInAuttom; //פרמטר 6  
                }
            }
        }
    }
}
