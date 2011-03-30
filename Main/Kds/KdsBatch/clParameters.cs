using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;
using KdsLibrary;
using KdsLibrary.BL;
namespace KdsBatch
{
    public class clParameters
    {
        public DateTime dSidurStartLimitHourParam1;  // 1 - מגבלת שעת התחלה
        public DateTime dSidurLimitShatGmar; //2 - שעת גמר רשמי
        public DateTime dSidurEndLimitHourParam3;   //מגבלת שעת סיום - 3
        public DateTime dSidurLimitShatGmarMafilim; //4 -שעת גמר מפעילי מחשב 
      
        public DateTime dTchilatTosefetLaila; //9	תחילת תוספת לילה אגד	שעה	19:01
        public DateTime dSiyumTosefetLaila; //10	סיום תוספת לילה אגד	שעה	22:00
        public DateTime dTchilatTosefetLailaChok;//11	תחילת תוספת לילה חוק	שעה	22:01
        public DateTime dSiyumTosefetLailaChok;//12	סיום תוספת לילה חוק	שעה	06:00

        public DateTime dStartAruchatTzaharayim;//18 - תחילת ארוחת צהריים
        public DateTime dEndAruchatTzaharayim;//19 - סיום ארוחת צהריים
        public DateTime dStartHourForPeilut;  //שעת התחלה ראשונה מותרת לפעילות בסידור - מאפיין 29
        public DateTime dEndHourForPeilut; //שעת יציאה אחרונה מותרת לפעילות - מאפיין 30
        public float fFactor;  //פקטור נסיעות שירות בין גמר לתכנון, פרמטר מספר 42
        //public int iShabatStartInAuttom; //6- שעה שמציינת שעת כניסת השבת בחורף בפורמט HHMM
        //public int iShabatStartInSummer; //7- שעה שמציינת שעת כניסת השבת בקיץ בפורמט HHMM

        public DateTime dSummerStart; //25- תאריך שמציין תחילת שעון קיץ (אגד) בפורמט DD/MM
         public DateTime dSummerEnd; //26- תאריך שמציין סיום שעון קיץ (אגד) בפורמט  DD/MM

        //public int iShabatStart; //משתנה זה יכיל את השעה בפועל שמתחילה השבת לפי פרמטר 6/7 לפי התקופה בשנה
        public DateTime dKnisatShabat;//משתנה זה יכיל את השעה בפועל שמתחילה השבת לפי פרמטר 6/7 לפי התקופה בשנה בפורמט XX:MM
        public int iMaxDriverLessons; //141-מכסימום שעורי נהיגה למורה בסידור יחיד)
        public DateTime dShatMaavarYom; //שעת סיום שבת - 32 
      
        public int iKmMinTosafetEshel;//33- אורך נסיעה מינימלי לצורך תוספת אש"ל.

        public DateTime dStartSidurEshelBoker;  //34 - שעת התחלת סידור לצורך אש"ל בוקר
        public DateTime dEndSidurEshelBoker; //35- שעת סיום     סידור לצורך אש"ל בוקר

        public DateTime dStartSidurEshelTzaharayim;  //36 - שעת התחלת סידור לצורך אש"ל צהריים
        public DateTime dEndSidurEshelTzaharayim; //37 - שעת סיום     סידור לצורך אש"ל צהריים

        public int iMinTimeSidurEshelTzaharayim;  //38 - זמן מינימלי לסידור לצורך אש"ל צהריים
        public DateTime dStartSidurEshelErev;  //39 - שעת התחלת סידור לצורך אש"ל ערב
        public int iZmanChariga; //זמן חריגה התחלה / גמר על חשבון העובד -41-
        public float fFactorNesiotRekot;//43-    פקטור נסיעות ריקות בין גמר לתכנון 
        //תאריכים חוקיים לסידורי קייטנות וארועי קייץ 44-47
        public DateTime dStartNihulVShivik;//44 - אירועי קיץ - תאריך חוקי לתחילת סידורי ניהול ושיווק,
        public DateTime dStartTiful;//45 -  אירועי קיץ - תאריך חוקי לתחילת סידור תפעול,
        public DateTime dEndNihulVShivik;//46 -   אירועי קיץ - תאריך חוקי לסיום סידור שיווק,ל,
        public DateTime dEndTiful;//47 -   אירועי קיץ - תאריך חוקי לסיום סידור),
        public DateTime dStartShabat;//48 -   - קיץ - תאריך התחלה לצורך חישוב כניסת שבת),
        public DateTime dEndShabat;//49 -    - קיץ - תאריך סיום לצורך חישוב כניסת שבת),
        public float fAchuzTosefetLeovdeyMeshek;//59-    אחוז תוספת לעובדי משק
        public int iShortSidurInTnuaMap; //פרמטר 60 - סידור קצר במפת התנועה
        
        public DateTime dDateFirstTosefetSikun;//62 - תאריך ראשון לצורך תוספת סיכון
        public DateTime dDateLastTosefetSikun;//63 - תאריך אחרון לצורך תוספת סיכון
        public float fGoremChishuvKm; // 64 - גורם לחישוב ק"מ בנסיעה ריקה שלא מקטלוג נסיעות
        public int iNochehutMinRetzufa;   //אש"ל מיוחד – מינימום נוכחות רצופה- 65 - 
        public int iNochehutMaxEshel1;  //66 - אש"ל מיוחד – נוכחות מכסימום קבוצה 1
        public int iNochehutMaxEshel; //67 - אש"ל מיוחד – נוכחות מכסימום קבוצה 2
        public int iNochehueMinEshel; //68 - אש"ל מיוחד – נוכחות מינימום קבוצה 3
        public DateTime dZmanHatchalaBoker2; //69 - אש"ל מיוחד – זמן התחלה לבוקר קבוצה 2
        public DateTime dHatchalaEshelBoker;//70 - אש"ל מיוחד חריג בוקר – התחלה לפני
        public DateTime dGmarEshelBoker;//71 -אש"ל מיוחד חריג בוקר – גמר אחרי
        public DateTime dZmanHatchalaBoker;// 72 -אש"ל מיוחד – זמן התחלה לבוקר 
        public DateTime dZmanSiyumBoker;//73 -אש"ל מיוחד – זמן סיום לבוקר 
        public DateTime dZmanSiyumTzharayim;//74 -אש"ל מיוחד – זמן סיום לצהרים
        public DateTime dZmanHatchalaErev;//75 -אש"ל מיוחד – זמן התחלה לערב
        public int iTamrizNosafotLoLetashlum;//76 -תמריץ-שעות נוספות שאינן לתשלום
        public int iMaxNosafotTafkid;//77 -תמריץ-מכסימום ש.נ לתמריץ תפקיד
        public int iMaxNosafotNahagut;//78 -תמריץ-מכסימום ש.נ לתמריץ נהגות
        public DateTime dNahagutLimitShatGmar; //80 - שעת גמר מקסימלית לסידור מסוג נהגות
        public float fElementZar; //81 - פרמיית נהגות גורם אלמנט זר
        public int iChalukaTosefetGilKashish; //82 - פרמיית נהגות גורם חלוקה לתוספת גיל קשיש
        public int iChalukaTosefetGilKshishon; //83 - פרמיית נהגות גורם חלוקה לתוספת גיל קשישון
        public int iBdikatIchurLesidurHityatzvut; //85 - בדיקת סידור התיצבות לסידור התייצבות
        public float fAchuzHistaglutPremyaMifraki; //86 - אחוז הסתגלות בפרמיה לרכב מיפרקי
        public float fMekademTosefetZmanLefiRechev; //87- מקדם תוספת זמן לפי רכב
        public int iMaxZmanRekaAdShmone; //88- מקסימום זמן ריקה הנכללת בהכנה ראשונה עד 8:00
        public int iMaxZmanRekaAchreyShmone; //89- מקסימום זמן ריקה הנכללת בהכנה ראשונה אחרי 8:00
        public int iRadyusMerchakMeshaonLehityazvut; //90 - רדיוס מרחק משעון לבדיקת התייצבות
        public int iPaarBeinSidurimMechayevHityazvut; // 91 - פער בין סידורי נהגות המחייב התייצבות
        public DateTime dSidurEndLimitShatHatchala; // - 93 גבול עליון שעת התחלה מותרת
        public DateTime dTchilatTosLilaTaavura; //-  95 אגד תעבורה תחילת תוספת לילה   
        public DateTime dSiyumTosLilaTaavura; //אגד תעבור סיום תוספת לילה  - 96
        public float fChelkiyutAchuzMiluim; //97 חלקיות אחוז מילואים
        public int iMaxMinutsForKnisot; //98 מקסימום דקות בפועל לכניסות לפי הצורך
        public int iHashlamaYomRagil; //parameter 101
        public int iHashlamaShisi;   //parameter 102
        public int iHashlamaShabat; //parameter 103
        public int iMinTimeBetweenSidurim; //הפרש מכסימלי בין סידורים לרציפות  - 104
        public int iMinHefreshSidurimLepitzulSummer; //מינימום הפרש בין סידורים לפיצול - קיץ  - 106
        public int iMinHefreshSidurimLeptzulWinter; //מינימום הפרש בין סידורים לפיצול חורף - 107
        public int iHashlamaMaxYomRagil;//parameter 108
        public int iHashlamaMaxShisi;  //parameter 109
        public int iHashlamaMaxShabat;//parameter 110

        public int iPrepareFirstMechineMaxTime; //120 - זמן הכנת מכונה ראשונה ביום
        public int iPrepareOtherMechineMaxTime; //121 - זמן הכנת מכונות נוספות ביום
        public int iPrepareOtherMechineTotalMaxTime; //122 - מקסימום זמן לסה"כ הכנות מכונה נוספות,
        public int iPrepareAllMechineTotalMaxTimeInDay; //123 - מקסימום זמן לסה"כ הכנות מכונה (ראשונה ונוספות), ביום
        public int iPrepareAllMechineTotalMaxTimeForSidur; //124 - מקסימום הכנות מכונה מותר בסידור     
       
        public int iZmanHalbash; //143 -זמן הלבשה
        public int iTosefetZmanGrira; //145 - תוספת זמן לסידור גרירה בפועל בזמן כוננות
        public int iMaxZmanHamtanaEilat; //מקסימום זמן המתנה אילת-  148
        public float fOrechNesiaKtzaraEilat; // 149 - אורך נסיעה קצרה לאילת
        public int iMinZmanAvodaBenahagut;  //152- מינימום זמן עבודה בנהגות הנחשב ליום עבודה
        public int iMinHashlamaCharigaForMutamutDriver; //מינימום השלמה חריגה למותאם בנהגות - 153
        public int iMaxChafifaBeinSidureyNihulTnua;  //154- חפיפה מותרת בסידורי ניהול תנועה
              
        public int iMaxRetzifutChodshitLetashlum; //157 -מכסימום רציפות חודשית לתשלום
        public int iMaxDakotNosafotSadran; //158 - מכסימום דקות נוספות חודשי לעבודת סדרן
        public int iMaxDakotNosafotPakach;//מכסימום דקות נוספות חודשי לעבודת פקח  - 159  
        public int iMaxDakotNosafotRakaz;//160 - מכסימום דקות נוספות חודשי לעבודת רכז
        public int iMaximumHmtanaTime; //161 - מקסימום זמן המתנה
        public int iMaxDakotNosafot; //162 -מקסימום דקות נוספות
        public int iMinZmanGriraDarom; //163 -זמן גרירה ראשונה מינימלי באזור דרום בזמן כוננות גרירה    
        public int iMinZmanGriraTzafon; //164 -זמן גרירה ראשונה מינימלי באזור צפון בזמן כוננות גרירה 
        public int iMinZmanGriraJrusalem; //165 - בזמן כוננות גרירה זמן גרירה ראשונה מינימלי באזור ירושלים
         public int iMinYomAvodaForHalbasha; //166- מינימום יום עבודה לצורך זכאות הלבשה
        public int iMinDakotLemutamLeHalbasha; //167 - מינימום יום עבודה למותאם לצורך זכאות הלבשה
        public int iMinDakotGmulLeloNehiga; //168 -מינימום תפקיד לצורך גמול ביום ללא נהיגה
        public int iMinDakotGmulImNehiga; //169 -מינימום תפקיד לצורך גמול ביום עם נהיגה
        public int iMaxPitzulLeyom;//מקסימום זכאות לפיצולים ביום עבודה - 170
        public float fAchuzSikun1; // 171 - אחוז סיכון לנסיעה בדרגת סיכון 1
        public float fAchuzSikun2; // 172 - אחוז סיכון לנסיעה בדרגת סיכון 2
        public float fAchuzSikun3; // 173 - אחוז סיכון לנסיעה בדרגת סיכון 3
        public float fAchuzSikun4; // 174 - אחוז סיכון לנסיעה בדרגת סיכון 4
        public float fAchuzSikunLehamtana; //175 - אחוז סיכון להמתנה באזור סיכון (אלמנט 64);
        public int iMaxYamimHamaratShaotNosafot; //176- מקסימום ימים להמרת שעות נוספות לחופש
        public float fMekademNipuachGmulChisachon;  //177 -מקדם ניפוח גמול חסכון מיוחד (רכיב 45)
        public int iGmulNosafotTzair;  //178 - גמול נוספות-מיכסה יומית צעיר
        public int iGmulNosafotKshishon;  //179 - גמול נוספות-מיכסה יומית קשישון
        public int iGmulNosafotKashish;  //180 - גמול נוספות-מיכסה יומית קשיש
        public int iGriraAddMinTime; // 181 - זמן גרירה נוספת מינימלי בזמן כוננות גרירה 
        public int iMaxDakotNosafotMeshaleach; //מכסימום דקות נוספות חודשי לעבודת משלח - 182  
        public int iMaxDakotNosafotKupai; //מכסימום דקות נוספות חודשי לעבודת קופאי - 183  
        public int iMaxRetzifutYomiLetashlum; //מכסימום רציפות יומית לתשלום - 184  
        public int iMaxRetzifutChodshitImGlisha; //מכסימום רציפות חודשית לתשלום עם גלישה- 185 
        public int iMinZmanMishmeretShniaBameshek; //186 - מינימום זמן משמרת שניה במשק 
        public DateTime dSiyumMishmeretShniaBameshek; //187  - סיום עבודה במשק במשמרת שניה
        public int iMaxHashlamaAlCheshbonPremia;//188 - מכסימום השלמה ליום עבודה על חשבון פרמיה בנהיגה
        public float fAchuzPremiaRishum; //189 - אחוז פרמיה למחלקת רישום
        public int iDakotKabalatYomHadracha; //190  - דקות לקבלת יום הדרכה מלא
        public float fAchuzPremiatMachsanKartisim; //191 - אחוז פרמיית מחסן כרטיסים
        public int iMinDakotNechshavYomAvoda; //192 - מינימום דקות שנחשב ליום עבודה
        public int iDakotMafileyMachshevColBoker; //194 -מפעילי מחשב חול בוקר
        public int iDakotMafileyMachshevColTzaharim; //195 -מפעילי מחשב חול צהריים
        public int iDakotMafileyMachshevColLiyla; //196 -מפעילי מחשב חול לילה
        public int iDakotMafileyMachshevErevChagBoker; //197 -מפעילי מחשב ערב חג בוקר
        public int iDakotMafileyMachshevErevChagTzaharim; //198 -מפעילי מחשב ערב חג צהריים
        public int iDakotMafileyMacshevChagBoker; //199 -מפעילי מחשב חוה''מ  ופורים בקר וצהריים
        public int iDakotMafileyMacshevChagLiyla; //200 -מפעילי מחשב חוה''מ ופורים לילה
        public DateTime dTchlatTashlumTosefetLilaMafilim; //202 - מפעילי מחשב שכירים התחלת תשלום תוספת לילה
        public float fAchuzPremiaMenahel; //203 - אחוז פרמיה למנהל בית ספר לנהיגה 
        public DateTime dMinStartMishmeretMafilimChol; //205 - מפעלים משמרת צהרים חול מינימום התחלה
        public DateTime dMaxStartMishmeretMafilimChol; //206 - מפעילים משמרת צהריים חול מקסימום התחלה
        public DateTime dMinEndMishmeretMafilimChol; //207 - מפעילים משמרת צהריים חול מינימום סיום
        public DateTime dMinEndMishmeretMafilimShishi; //208 - מפעילים משמרת צהריים שישי מינימום סיום
        public DateTime dMinStartMishmeretMafilimLilaChol; //209 - מפעילים משמרת לילה חול מינימום התחלה
        public DateTime dMinEndMishmeretMafilimLilaChol1; //210 - מפעילים משמרת לילה חול מינימום סיום  תלוי התחלה
        public DateTime dMinEndMishmeretMafilimLilaChol2; //211 -מפעילים משמרת לילה חול מינימום סיום לא תלוי התחלה
      public DateTime dTchilatMishmeretLilaMafilim; //220 שעת התחלה משמרת לילה
      public DateTime dSiyumMishmeretLilaMafilim; //221 שעת סיום משמרת לילה
        public int iMaxNochehutVisaBodedet; //222 -מקסימום נוכחות לויזה בודדת
        public int iNuchehutVisa1; //223 -  נוכחות לויזה עד 14:00
        public int iMaxNuchehutVisaPnimRishon1; //224 - מקסימום הנוכחות  לויזה  - יום ראשון- עד 14:00
        public int iMinNuchehutVisaPnimRishon1; //225- מינימום הנוכחות לויזה  יום ראשון- עד-  14:00
        public int iMaxNuchehutVisaPnimRishon2; //226 - מקסימום הנוכחות לויזה  יום ראשון- מ- 14:00
        public int iMinNuchehutVisaPnimRishon2; //227 - מינימום הנוכחות לויזה - יום ראשון-   מ- 14:00
        public int iMaxNochehutVisaPnim; //228 - ויזה פנים -יום ראשון - מקסימום נוכחות
        public int iMinNochehutVisaPnim; //229 - ויזה פנים -יום ראשון - מינימום נוכחות
        public int iNochehutVisaPnimNoShabaton; //230 - ויזה פנים נוכחות - לא שבתון
        public int iMaxNochehutVisaPnimEmtzai; //-231 - מקסימום נוכחות ויזה פנים יום אמצעי
        public int iMinNochehutVisaPnimEmtzai; //-232 - מינימום נוכחות ויזה פנים יום אמצעי
        public int iMaxNochehutVisaPnimAcharon; //-233 - מקסימום נוכחות ויזה פנים יום אחרון
        public int iMinNochehutVisaPnimAcharon; //-234 - מינימום נוכחות ויזה פנים יום אחרון
        public int iMaxNochehutVisaTzahalLoAcharon; //-235 - מקסימום נוכחות ויזה צה"ל  לא יום אחרון
        public DateTime dShatHatchalaVisaPnimAcharon; //-236 - שעת התחלה לתשלום ויזה פנים יום אחרון
        public DateTime dShatHatchalaVisaTzahalAcharon; //-237 - שעת התחלה לתשלום ויזה צה"ל יום אחרון
        public int iMinDakotLezakautNesiot;//-238 - מינימום יום עבודה לצורך זכאות לנסיעות
        public int iMinDakotNehigaVetnuaLezakautNesiot; //-239 - מינימום דקות נהיגה ותנועה לצורך זכאות לנסיעות
        public DateTime dSiyumLilaLeovedLoMafil; //240- סיום לילה לעובד עם מאפיין לא מפעיל
        public DateTime dSiyumLilaMotsashMafil; //241- סיום לילה מוצ''ש מפעיל
        public DateTime dShatGmarNextDay; //242-שעת גמר לבדיקת יום של שעת יציאה
        public DateTime dShatHatchalaNahagutNihulTnua; //244-התחלה מותרת-טווח עליון-נהגות וניהול תנועה
        public float fHighPremya; //245- פרמיה גבוהה
        public float fBasisLechishuvPremia; //504 -אגד תעבורה- בסיס לחישוב פרמיית נהיגה
        public float fMaxPremiatNehiga; //505 - אגד תעבורה - מקסימום פרמיית נהיגה
        
        private DataTable dtParameters;
        private string _Type="";
        public DateTime _Taarich; 

        public clParameters(DateTime dCardDate, int iSugYom)
        {
            clUtils oUtils = new clUtils();
            try
            {
                _Taarich = dCardDate;
                dtParameters = oUtils.GetKdsParametrs();
                SetParameters(dCardDate, iSugYom);
                dtParameters.Dispose();
            }
             catch (Exception ex)
            {
                throw ex;
            }
        }

        public clParameters(DateTime dCardDate, int iSugYom,string type)
        {
            _Taarich = dCardDate;
            _Type = type;
            dtParameters = clCalcData.DtParameters;
            SetParameters(dCardDate, iSugYom);
            dtParameters.Dispose();
        }

        public clParameters(DateTime dCardDate, int iSugYom, string type,DataTable dtParams)
        {
            _Taarich = dCardDate;
            _Type = type;
            dtParameters = dtParams;
            SetParameters(dCardDate, iSugYom);
            dtParameters.Dispose();
        }
        private void SetParameters(DateTime dCardDate, int iSugYom)
        {
            string sTmp;
            try
            {
                //מגבלת שעת התחלה1- 
                sTmp = GetOneParam(1, dCardDate);
                dSidurStartLimitHourParam1 = GetParamHour(sTmp, dCardDate);//GetOneParam(1, dCardDate);

                //2- שעת גמר רשמי
                sTmp = GetOneParam(2, dCardDate);
                dSidurLimitShatGmar = GetParamHour(sTmp, dCardDate);

               
                //3 - מגבלת שעת סיום
                sTmp = GetOneParam(3, dCardDate.AddDays(1));
                dSidurEndLimitHourParam3 = GetParamHour(sTmp, dCardDate);//GetOneParam(3, dCardDate);
                if (dSidurEndLimitHourParam3 >= clGeneral.GetDateTimeFromStringHour("00:01", dCardDate) && dSidurEndLimitHourParam3 <= clGeneral.GetDateTimeFromStringHour("07:59", dCardDate))
                {
                    dSidurEndLimitHourParam3 = dSidurEndLimitHourParam3.AddDays(1);
                }
                 //4 -שעת גמר מפעילי מחשב 
                sTmp = GetOneParam(4, dCardDate.AddDays(1));
                dSidurLimitShatGmarMafilim = GetParamHour(sTmp, dCardDate);
                if (dSidurLimitShatGmarMafilim >= clGeneral.GetDateTimeFromStringHour("00:01", dCardDate) && dSidurLimitShatGmarMafilim <= clGeneral.GetDateTimeFromStringHour("07:59", dCardDate))
                {
                    dSidurLimitShatGmarMafilim = dSidurLimitShatGmarMafilim.AddDays(1);
                }
                //9	תחילת תוספת לילה אגד	שעה	19:01
               sTmp = GetOneParam(9, dCardDate);
               dTchilatTosefetLaila = GetParamHour(sTmp, dCardDate);
               
                //10	סיום תוספת לילה אגד	שעה	
                sTmp = GetOneParam(10, dCardDate);
                dSiyumTosefetLaila = GetParamHour(sTmp, dCardDate);

                //11	תחילת תוספת לילה חוק	שעה	
                sTmp = GetOneParam(11, dCardDate);
                dTchilatTosefetLailaChok = GetParamHour(sTmp, dCardDate);

                //12	סיום תוספת לילה חוק	שעה	
                sTmp = GetOneParam(12, dCardDate.AddDays(1));
                dSiyumTosefetLailaChok = GetParamHour(sTmp, dCardDate.AddDays(1));

                //18 - תחילת ארוחת צהריים
                sTmp = GetOneParam(18, dCardDate);
                dStartAruchatTzaharayim = GetParamHour(sTmp, dCardDate);

                //19 - סיום ארוחת צהריים
                sTmp = GetOneParam(19, dCardDate);
                dEndAruchatTzaharayim = GetParamHour(sTmp, dCardDate);

                //25- תאריך שמציין תחילת שעון קיץ (אגד) בפורמט DD/MM
                sTmp = GetOneParam(25, dCardDate);
                dSummerStart = GetParamDate(sTmp, dCardDate);// String.IsNullOrEmpty(sTmp) ? DateTime.Parse(string.Concat("01/01", "/", clGeneral.cYearNull.ToString())) : DateTime.Parse(string.Concat(sTmp, "/", dCardDate.Year.ToString()));

                //26- תאריך שמציין סיום שעון קיץ (אגד) בפורמט  DD/MM
                sTmp = GetOneParam(26, dCardDate);
                dSummerEnd = GetParamDate(sTmp, dCardDate);// String.IsNullOrEmpty(sTmp) ? DateTime.Parse(string.Concat("01/01", "/", clGeneral.cYearNull.ToString())) : DateTime.Parse(string.Concat(sTmp, "/", dCardDate.Year.ToString()));

                //שעת התחלה ראשונה מותרת לפעילות בסידור - מאפיין 29
                sTmp = GetOneParam(29, dCardDate);
                dStartHourForPeilut = GetParamHour(sTmp, dCardDate);//GetOneParam(29, dCardDate);

                //שעת יציאה אחרונה מותרת לפעילות - מאפיין 30
                sTmp = GetOneParam(30, dCardDate.AddDays(1));
                dEndHourForPeilut = GetParamHour(sTmp, dCardDate.AddDays(1));//GetOneParam(30, dCardDate);

                //32 -   שעת סיום שבת
                sTmp = GetOneParam(32, dCardDate);
                dShatMaavarYom = GetParamHour(sTmp, dCardDate.AddDays(1));

                ////33- אורך נסיעה מינימלי לצורך תוספת אש"ל.
                sTmp = GetOneParam(33, dCardDate);
                iKmMinTosafetEshel = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                ////34 - שעת התחלת סידור לצורך אש"ל בוקר
                sTmp = GetOneParam(34, dCardDate);
                dStartSidurEshelBoker = GetParamHour(sTmp, dCardDate);

                ////35 - שעת סיום     סידור לצורך אש"ל בוקר
                sTmp = GetOneParam(35, dCardDate);
                dEndSidurEshelBoker = GetParamHour(sTmp, dCardDate);

                ////36 - שעת התחלת סידור לצורך אש"ל צהריים
                sTmp = GetOneParam(36, dCardDate);
                dStartSidurEshelTzaharayim = GetParamHour(sTmp, dCardDate);

                ////37 - שעת סיום     סידור לצורך אש"ל צהריים
                sTmp = GetOneParam(37, dCardDate);
                dEndSidurEshelTzaharayim = GetParamHour(sTmp, dCardDate);

                ////זמן מינימלי לסידור לצורך אש"ל צהריים
                sTmp = GetOneParam(38, dCardDate);
                iMinTimeSidurEshelTzaharayim = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);


                ////39 - שעת התחלת סידור לצורך אש"ל ערב
                sTmp = GetOneParam(39, dCardDate);
                dStartSidurEshelErev = GetParamHour(sTmp, dCardDate);

               
                //פקטור נסיעות שירות בין גמר לתכנון, פרמטר מספר 42
                sTmp = GetOneParam(42, dCardDate);
                fFactor = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);


               
                //41 - זמן חריגה התחלה / גמר על חשבון העובד
                sTmp = GetOneParam(41, dCardDate);
                iZmanChariga = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //43-    פקטור נסיעות ריקות בין גמר לתכנון 
                sTmp = GetOneParam(43, dCardDate);
                fFactorNesiotRekot = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //44 - אירועי קיץ - תאריך חוקי לתחילת סידורי ניהול ושיווק,
                sTmp = GetOneParam(44, dCardDate);
                dStartNihulVShivik = GetParamDate(sTmp, dCardDate);//String.IsNullOrEmpty(sTmp) ? DateTime.Parse(string.Concat("01/01", "/", clGeneral.cYearNull.ToString())) : DateTime.Parse(string.Concat(sTmp, "/", dCardDate.Year.ToString()));

                //45 -  אירועי קיץ - תאריך חוקי לתחילת סידור תפעול,  
                sTmp = GetOneParam(45, dCardDate);
                dStartTiful = GetParamDate(sTmp, dCardDate); //String.IsNullOrEmpty(sTmp) ? DateTime.Parse(string.Concat("01/01", "/", clGeneral.cYearNull.ToString())) : DateTime.Parse(string.Concat(sTmp, "/", dCardDate.Year.ToString()));

                //46 -   אירועי קיץ - תאריך חוקי לסיום סידור שיווק,ל,
                sTmp = GetOneParam(46, dCardDate);
                dEndNihulVShivik = GetParamDate(sTmp, dCardDate); // String.IsNullOrEmpty(sTmp) ? DateTime.Parse(string.Concat("01/01", "/", clGeneral.cYearNull.ToString())) : DateTime.Parse(string.Concat(sTmp, "/", dCardDate.Year.ToString())); 

                //47 אירועי קיץ - תאריך חוקי לסיום סידור תפעול 
                sTmp = GetOneParam(47, dCardDate);
                dEndTiful = GetParamDate(sTmp, dCardDate);//DateTime.Parse(string.Concat(GetOneParam(47, dCardDate), "/", dCardDate.Year.ToString()));

                //48  - קיץ - תאריך התחלה לצורך חישוב כניסת שבת) 
                sTmp = GetOneParam(48, dCardDate);
                dStartShabat = GetParamDate(sTmp, dCardDate);//DateTime.Parse(string.Concat(GetOneParam(48, dCardDate), "/", dCardDate.Year.ToString()));

                //49  קיץ - תאריך סיום לצורך חישוב כניסת שבת 
                sTmp = GetOneParam(49, dCardDate);
                dEndShabat = GetParamDate(sTmp, dCardDate);//DateTime.Parse(string.Concat(GetOneParam(49, dCardDate), "/", dCardDate.Year.ToString())); 

                //59 - אחוז תוספת לעובדי משק
                sTmp = GetOneParam(59, dCardDate);
                fAchuzTosefetLeovdeyMeshek = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //פרמטר 60 - סידור קצר במפת התנועה
                sTmp = GetOneParam(60, dCardDate);
                iShortSidurInTnuaMap = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //62 - תאריך ראשון לצורך תוספת סיכון
                sTmp = GetOneParam(62, dCardDate);
                dDateFirstTosefetSikun = GetParamDate(sTmp, dCardDate);

                //63 - תאריך אחרון לצורך תוספת סיכון
                sTmp = GetOneParam(63, dCardDate);
                dDateLastTosefetSikun = GetParamDate(sTmp,dCardDate);

                // 64 - גורם לחישוב ק"מ בנסיעה ריקה שלא מקטלוג נסיעות
                sTmp = GetOneParam(64, dCardDate);
                fGoremChishuvKm = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                ////אש"ל מיוחד – מינימום נוכחות רצופה- 65 - 
                sTmp = GetOneParam(65, dCardDate);
                iNochehutMinRetzufa = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                ////66 - אש"ל מיוחד – נוכחות מכסימום קבוצה 1
                sTmp = GetOneParam(66, dCardDate);
                iNochehutMaxEshel1 = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                ////67 - אש"ל מיוחד – נוכחות מכסימום קבוצה 2
                sTmp = GetOneParam(67, dCardDate);
                iNochehutMaxEshel = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                ////68 - אש"ל מיוחד – נוכחות מינימום קבוצה 3
                sTmp = GetOneParam(68, dCardDate);
                iNochehueMinEshel = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //69 - אש"ל מיוחד – זמן התחלה לבוקר קבוצה 2
                sTmp = GetOneParam(69, dCardDate);
                dZmanHatchalaBoker2 = GetParamHour(sTmp, dCardDate);
                //70 - אש"ל מיוחד חריג בוקר – התחלה לפני
                sTmp = GetOneParam(70, dCardDate);
                dHatchalaEshelBoker = GetParamHour(sTmp, dCardDate);
                //71 -אש"ל מיוחד חריג בוקר – גמר אחרי
                sTmp = GetOneParam(71, dCardDate);
                dGmarEshelBoker = GetParamHour(sTmp, dCardDate);
                // 72 -אש"ל מיוחד – זמן התחלה לבוקר
                sTmp = GetOneParam(72, dCardDate);
                dZmanHatchalaBoker = GetParamHour(sTmp, dCardDate);

                //73 -אש"ל מיוחד – זמן סיום לבוקר
                sTmp = GetOneParam(73, dCardDate);
                dZmanSiyumBoker = GetParamHour(sTmp, dCardDate);
                //74 -אש"ל מיוחד – זמן סיום לצהרים
                sTmp = GetOneParam(74, dCardDate);
                dZmanSiyumTzharayim = GetParamHour(sTmp, dCardDate);
                //75 -אש"ל מיוחד – זמן התחלה לערב
                sTmp = GetOneParam(75, dCardDate);
                dZmanHatchalaErev = GetParamHour(sTmp, dCardDate);

                //76 - תמריץ-שעות נוספות שאינן לתשלום
                sTmp = GetOneParam(76, dCardDate);
                iTamrizNosafotLoLetashlum = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //77 - תמריץ-מכסימום ש.נ לתמריץ תפקיד
                sTmp = GetOneParam(77, dCardDate);
                iMaxNosafotTafkid = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //78 - תמריץ-מכסימום ש.נ לתמריץ נהגות
                sTmp = GetOneParam(78, dCardDate);
                iMaxNosafotNahagut = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //80 - שעת גמר מקסימלית לסידור מסוג נהגות          
                sTmp = GetOneParam(80, dCardDate.AddDays(1));
                dNahagutLimitShatGmar = GetParamHour(sTmp, dCardDate.AddDays(1));

                 //81 - פרמיית נהגות גורם אלמנט זר
                sTmp = GetOneParam(81, dCardDate);
                fElementZar = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                  // 82 - פרמיית נהגות גורם חלוקה לתוספת גיל קשיש
                sTmp = GetOneParam(82, dCardDate);
                iChalukaTosefetGilKashish = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                 //83 - פרמיית נהגות גורם חלוקה לתוספת גיל קשישון
                sTmp = GetOneParam(83, dCardDate);
                iChalukaTosefetGilKshishon = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                 //85 - בדיקת סידור התיצבות לסידור התייצבות
                sTmp = GetOneParam(85, dCardDate);
                iBdikatIchurLesidurHityatzvut = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //86 - אחוז הסתגלות בפרמיה לרכב מיפרקי
                sTmp = GetOneParam(86, dCardDate);
                fAchuzHistaglutPremyaMifraki = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //87- מקדם תוספת זמן לפי רכב
                sTmp = GetOneParam(87, dCardDate);
                fMekademTosefetZmanLefiRechev = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //88- מקסימום זמן ריקה הנכללת בהכנה ראשונה עד 8:00
              sTmp = GetOneParam(88, dCardDate);
              iMaxZmanRekaAdShmone = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

               //89- מקסימום זמן ריקה הנכללת בהכנה ראשונה אחרי 8:00
                sTmp = GetOneParam(89, dCardDate);
                iMaxZmanRekaAchreyShmone = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //90 - רדיוס מרחק משעון לבדיקת התייצבות
                sTmp = GetOneParam(90, dCardDate);
                iRadyusMerchakMeshaonLehityazvut = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);
                
                // 91 - פער בין סידורי נהגות המחייב התייצבות
                sTmp = GetOneParam(91, dCardDate);
                iPaarBeinSidurimMechayevHityazvut = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //גבול עליון של שעת התחלה - 93
                sTmp = GetOneParam(93, dCardDate);                                                
                dSidurEndLimitShatHatchala = GetParamHour(sTmp, dCardDate);

                 //-  95 אגד תעבורה תחילת תוספת לילה   
                sTmp = GetOneParam(95, dCardDate);                                                
                 dTchilatTosLilaTaavura= GetParamHour(sTmp, dCardDate);

                //אגד תעבור סיום תוספת לילה  - 96
                sTmp = GetOneParam(96, dCardDate);
                dSiyumTosLilaTaavura = GetParamHour(sTmp, dCardDate.AddDays(1));

                //97 חלקיות אחוז מילואים
                sTmp = GetOneParam(97, dCardDate);
                fChelkiyutAchuzMiluim = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //98 - מקסימום דקות בפועל לכניסות לפי הצורך
                sTmp = GetOneParam(98, dCardDate);
                iMaxMinutsForKnisot = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //זמן מינימום לסידור חול להשלמה-  101
                sTmp = GetOneParam(101, dCardDate);
                iHashlamaYomRagil = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //זמן מינימום לסידור שישי/ערב חג להשלמה-  102
                sTmp = GetOneParam(102, dCardDate);
                iHashlamaShisi = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //זמן מינימום לסידור שבתון להשלמה-  103
                sTmp = GetOneParam(103, dCardDate);
                iHashlamaShabat = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //104 - הפרש מכסימלי בין סידורים לרציפות 
                sTmp = GetOneParam(104, dCardDate);
                iMinTimeBetweenSidurim = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //106 -מינימום הפרש בין סידורים לפיצול קיץ
                sTmp = GetOneParam(106, dCardDate);
                iMinHefreshSidurimLepitzulSummer = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);


                //107 - מינימום הפרש בין סידורים לפיצול חורף 
                sTmp = GetOneParam(107, dCardDate);
                iMinHefreshSidurimLeptzulWinter = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);
                
                //מכסימום השלמות ביום חול-  108
                sTmp = GetOneParam(108, dCardDate);
                iHashlamaMaxYomRagil = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //מכסימום השלמות בשישי/ע.ח-  109
                sTmp = GetOneParam(109, dCardDate);
                iHashlamaMaxShisi = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //מכסימום השלמות בשבתון-  110
                sTmp = GetOneParam(110, dCardDate);
                iHashlamaMaxShabat = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //120 - זמן הכנת מכונה ראשונה ביום
                sTmp = GetOneParam(120, dCardDate);
                iPrepareFirstMechineMaxTime = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //121 - זמן הכנת מכונות נוספות ביום
                sTmp = GetOneParam(121, dCardDate);
                iPrepareOtherMechineMaxTime = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //122 - מקסימום זמן לסה"כ הכנות מכונה נוספות,
                sTmp = GetOneParam(122, dCardDate);
                iPrepareOtherMechineTotalMaxTime = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //123 - מקסימום זמן לסה"כ הכנות מכונה (ראשונה ונוספות), ביום
                sTmp = GetOneParam(123, dCardDate);
                iPrepareAllMechineTotalMaxTimeInDay = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //124 - מקסימום הכנות מכונה מותר בסידור    
                sTmp = GetOneParam(124, dCardDate);
                iPrepareAllMechineTotalMaxTimeForSidur = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

               
               
                //מכסימום שעורי נהיגה למורה בסידור יחיד141)
                sTmp = GetOneParam(141, dCardDate);
                iMaxDriverLessons = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //זמן הלבשה -  143
                sTmp = GetOneParam(143, dCardDate);
                iZmanHalbash = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //תוספת זמן לסידור גרירה בפועל בזמן כוננות -  145
                sTmp = GetOneParam(145, dCardDate);
                iTosefetZmanGrira = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //מקסימום זמן המתנה אילת-  148
                sTmp = GetOneParam(148, dCardDate);
                iMaxZmanHamtanaEilat = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //149 - אורך נסיעה קצרה לאילת
                sTmp = GetOneParam(149, dCardDate);
                fOrechNesiaKtzaraEilat = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                
                //152- מינימום זמן עבודה בנהגות הנחשב ליום עבודה
                sTmp = GetOneParam(152, dCardDate);
                iMinZmanAvodaBenahagut= String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //153- מינימום השלמה חריגה למותאם בנהגות
                sTmp = GetOneParam(153, dCardDate);
                iMinHashlamaCharigaForMutamutDriver = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                 //154- חפיפה מותרת בסידורי ניהול תנועה
                sTmp = GetOneParam(154, dCardDate);
                iMaxChafifaBeinSidureyNihulTnua = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //157 - מכסימום רציפות חודשית לתשלום
                sTmp = GetOneParam(157, dCardDate);
                iMaxRetzifutChodshitLetashlum = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //158 - מכסימום דקות נוספות חודשי לעבודת סדרן
                sTmp = GetOneParam(158, dCardDate);
                iMaxDakotNosafotSadran = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //159 - מכסימום דקות נוספות חודשי לעבודת פקח
                sTmp = GetOneParam(1159, dCardDate);
                iMaxDakotNosafotPakach = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //160 - מכסימום דקות נוספות חודשי לעבודת רכז
                sTmp = GetOneParam(160, dCardDate);
                iMaxDakotNosafotRakaz = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //161 - מקסימום זמן המתנה
                sTmp = GetOneParam(161, dCardDate);
                iMaximumHmtanaTime = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                // מקסימום דקות נוספות 125%-  162
                sTmp = GetOneParam(162, dCardDate);
                iMaxDakotNosafot = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                // זמן גרירה ראשונה מינימלי באזור דרום   בזמן כוננות גרירה  163
                sTmp = GetOneParam(163, dCardDate);
                iMinZmanGriraDarom = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                // זמן גרירה ראשונה מינימלי באזור צפון   בזמן כוננות גרירה  164
                sTmp = GetOneParam(164, dCardDate);
                iMinZmanGriraTzafon = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                // 165  זמן גרירה ראשונה מינימלי באזור ירושלים   בזמן כוננות גרירה
                 sTmp = GetOneParam(165, dCardDate);
                iMinZmanGriraJrusalem = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                
               
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


                SetShatKnisatShabat(dCardDate, iSugYom);

                
                //166 - מינימום יום עבודה לצורך זכאות הלבשה
                sTmp = GetOneParam(166, dCardDate);
                iMinYomAvodaForHalbasha = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //167 - מינימום יום עבודה למותאם לצורך זכאות הלבשה
                sTmp = GetOneParam(167, dCardDate);
                iMinDakotLemutamLeHalbasha = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //168 - מינימום תפקיד לצורך גמול ביום ללא נהיגה
                sTmp = GetOneParam(168, dCardDate);
                iMinDakotGmulLeloNehiga = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                // 169 - מינימום תפקיד לצורך גמול ביום עם נהיגה
                sTmp = GetOneParam(169, dCardDate);
                iMinDakotGmulImNehiga = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                // 170 - מקסימום זכאות לפיצולים ביום עבודה
                sTmp = GetOneParam(170, dCardDate);
                iMaxPitzulLeyom = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //אחוז סיכון לנסיעה בדרגת סיכון 1 171
                sTmp = GetOneParam(171, dCardDate);
                fAchuzSikun1 = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //אחוז סיכון לנסיעה בדרגת סיכון 2 172
                sTmp = GetOneParam(172, dCardDate);
                fAchuzSikun2 = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //אחוז סיכון לנסיעה בדרגת סיכון 3 173
                sTmp = GetOneParam(173, dCardDate);
                fAchuzSikun3 = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //אחוז סיכון לנסיעה בדרגת סיכון 4 174
                sTmp = GetOneParam(174, dCardDate);
                fAchuzSikun4 = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //אחוז סיכון להמתנה באזור סיכון (אלמנט 64) 175
                sTmp = GetOneParam(175, dCardDate);
                fAchuzSikunLehamtana = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //176 - מקסימום ימים להמרת שעות נוספןת לחופש
                sTmp = GetOneParam(176, dCardDate);
                iMaxYamimHamaratShaotNosafot = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //מקדם ניפוח גמול חסכון מיוחד 177 )
                sTmp = GetOneParam(177, dCardDate);
                fMekademNipuachGmulChisachon = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                // 178 - גמול נוספות-מיכסה יומית צעיר
                sTmp = GetOneParam(178, dCardDate);
                iGmulNosafotTzair = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //179 - גמול נוספות-מיכסה יומית קשיישון
                sTmp = GetOneParam(179, dCardDate);
                iGmulNosafotKshishon = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //180 - גמול נוספות-מיכסה יומית קשיש
                sTmp = GetOneParam(180, dCardDate);
                iGmulNosafotKashish = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                sTmp = GetOneParam(181, dCardDate);
                iGriraAddMinTime =String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //182 - מכסימום דקות נוספות חודשי לעבודת משלח
                sTmp = GetOneParam(182, dCardDate);
                iMaxDakotNosafotMeshaleach = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //183 - מכסימום דקות נוספות חודשי לעבודת קופאי
                sTmp = GetOneParam(183, dCardDate);
                iMaxDakotNosafotKupai = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //184 - מכסימום רציפות יומית לתשלום
                sTmp = GetOneParam(184, dCardDate);
                iMaxRetzifutYomiLetashlum = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //מכסימום רציפות חודשית לתשלום עם גלישה185- 
                sTmp = GetOneParam(185, dCardDate);
                iMaxRetzifutChodshitImGlisha = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

              //186 - מינימום זמן משמרת שניה במשק 
                sTmp = GetOneParam(186, dCardDate);
                iMinZmanMishmeretShniaBameshek = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //187  - סיום עבודה במשק במשמרת שניה
                  sTmp = GetOneParam(187, dCardDate);
                dSiyumMishmeretShniaBameshek = GetParamHour(sTmp, dCardDate);

                //188  מכסימום השלמה ליום עבודה על חשבון פרמיה בנהיגה
                sTmp = GetOneParam(188, dCardDate);
                iMaxHashlamaAlCheshbonPremia = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //189 - אחוז פרמיה למחלקת רישום
                sTmp = GetOneParam(189, dCardDate);
                fAchuzPremiaRishum= String.IsNullOrEmpty(sTmp) ? 0 :float.Parse(sTmp);

               //190  - דקות לקבלת יום הדרכה מלא
                sTmp = GetOneParam(190, dCardDate);
                iDakotKabalatYomHadracha = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                 //191 - אחוז פרמיית מחסן כרטיסים
                sTmp = GetOneParam(191, dCardDate);
                fAchuzPremiatMachsanKartisim = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //192 - מינימום דקות שנחשב ליום עבודה
                sTmp = GetOneParam(192, dCardDate);
                iMinDakotNechshavYomAvoda = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                 //194 -מפעילי מחשב חול בוקר
                sTmp = GetOneParam(194, dCardDate);
                iDakotMafileyMachshevColBoker = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //195 -מפעילי מחשב חול צהריים
                sTmp = GetOneParam(195, dCardDate);
                iDakotMafileyMachshevColTzaharim = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //196 -מפעילי מחשב חול ערב
                sTmp = GetOneParam(196, dCardDate);
                iDakotMafileyMachshevColLiyla = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //197 -מפעילי מחשב חול בוקר
                sTmp = GetOneParam(197, dCardDate);
                iDakotMafileyMachshevErevChagBoker = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //198 -מפעילי מחשב חול צהריים
                sTmp = GetOneParam(198, dCardDate);
                iDakotMafileyMachshevErevChagTzaharim = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //199 -מפעילי מחשב חול ערב
                sTmp = GetOneParam(199, dCardDate);
                iDakotMafileyMacshevChagBoker = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //200 -מפעילי מחשב חול ערב
                sTmp = GetOneParam(200, dCardDate);
                iDakotMafileyMacshevChagLiyla = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                 //202 - מפעילי מחשב שכירים התחלת תשלום תוספת לילה
                sTmp = GetOneParam(202, dCardDate);
                dTchlatTashlumTosefetLilaMafilim = GetParamHour(sTmp, dCardDate);

                //203 - אחוז פרמיה למנהל בית ספר לנהיגה
                sTmp = GetOneParam(203, dCardDate);
                fAchuzPremiaMenahel = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

               //205 - מפעלים משמרת צהרים חול מינימום התחלה
                 sTmp = GetOneParam(205, dCardDate);
                 dMinStartMishmeretMafilimChol = GetParamHour(sTmp, dCardDate);
               
                  //206 - מפעילים משמרת צהריים חול מקסימום התחלה
                 sTmp = GetOneParam(206, dCardDate);
                 dMaxStartMishmeretMafilimChol = GetParamHour(sTmp, dCardDate);

               //207 - מפעילים משמרת צהריים חול מינימום סיום
                 sTmp = GetOneParam(207, dCardDate);
                 dMinEndMishmeretMafilimChol = GetParamHour(sTmp, dCardDate);
   
                 //208 - מפעילים משמרת צהריים שישי מינימום סיום
                 sTmp = GetOneParam(208, dCardDate);
                 dMinEndMishmeretMafilimShishi = GetParamHour(sTmp, dCardDate);
      
              //209 - מפעילים משמרת לילה חול מינימום התחלה
                 sTmp = GetOneParam(209, dCardDate);
                 dMinStartMishmeretMafilimLilaChol = GetParamHour(sTmp, dCardDate);
     
               //210 - מפעילים משמרת לילה חול מינימום סיום  תלוי התחלה
                 sTmp = GetOneParam(210, dCardDate);
                 dMinEndMishmeretMafilimLilaChol1 = GetParamHour(sTmp, dCardDate);

                 //211 -מפעילים משמרת לילה חול מינימום סיום לא תלוי התחלה
                 sTmp = GetOneParam(211, dCardDate.AddDays(1));
                 dMinEndMishmeretMafilimLilaChol2 = GetParamHour(sTmp, dCardDate.AddDays(1));
                                 
                //220 שעת התחלה משמרת לילה
                 sTmp = GetOneParam(220, dCardDate);
                 dTchilatMishmeretLilaMafilim = GetParamHour(sTmp, dCardDate);

                 //221 שעת סיום משמרת לילה
                 sTmp = GetOneParam(221, dCardDate);
                 dSiyumMishmeretLilaMafilim = GetParamHour(sTmp, dCardDate.AddDays(1));

                //222 -מקסימום נוכחות לויזה בודדת
                 sTmp = GetOneParam(222, dCardDate);
                iMaxNochehutVisaBodedet = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);
            
                 //223 -  נוכחות לויזה עד 14:00
                  sTmp = GetOneParam(223, dCardDate);
                iNuchehutVisa1 = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //224 - מקסימום הנוכחות  לויזה  - יום ראשון- עד 14:00
                  sTmp = GetOneParam(224, dCardDate);
                iMaxNuchehutVisaPnimRishon1 = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                 //225- מינימום הנוכחות לויזה  יום ראשון- עד-  14:00
                 sTmp = GetOneParam(225, dCardDate);
                iMinNuchehutVisaPnimRishon1 = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                 //226 - מקסימום הנוכחות לויזה  יום ראשון- מ- 14:00
                  sTmp = GetOneParam(226, dCardDate);
                iMaxNuchehutVisaPnimRishon2 = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //227 - מינימום הנוכחות לויזה - יום ראשון-   מ- 14:00
                  sTmp = GetOneParam(227, dCardDate);
                iMinNuchehutVisaPnimRishon2 = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //228 - ויזה פנים -מקסימום נוכחות
                  sTmp = GetOneParam(228, dCardDate);
                iMaxNochehutVisaPnim = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //229 - ויזה פנים -  מינימום נוכחות
                  sTmp = GetOneParam(229, dCardDate);
                iMinNochehutVisaPnim = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //230 - ויזה פנים נוכחות - לא שבתון
                  sTmp = GetOneParam(230, dCardDate);
                iNochehutVisaPnimNoShabaton = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //-231 - מקסימום נוכחות ויזה פנים יום אמצעי
                  sTmp = GetOneParam(231, dCardDate);
                iMaxNochehutVisaPnimEmtzai = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //-232 - מינימום נוכחות ויזה פנים יום אמצעי
                  sTmp = GetOneParam(232, dCardDate);
                iMinNochehutVisaPnimEmtzai = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //-233 - מקסימום נוכחות ויזה פנים יום אחרון
                  sTmp = GetOneParam(233, dCardDate);
                iMaxNochehutVisaPnimAcharon = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                 //-234 - מינימום נוכחות ויזה פנים יום אחרון
                  sTmp = GetOneParam(234, dCardDate);
                iMinNochehutVisaPnimAcharon = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);
       
                          //-235 - מקסימום נוכחות ויזה צה"ל  לא יום אחרון
                  sTmp = GetOneParam(235, dCardDate);
                iMaxNochehutVisaTzahalLoAcharon = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //236 --ויזה פנים שעת התחלה לתשלום - יום אחרון
                sTmp = GetOneParam(236, dCardDate);
                dShatHatchalaVisaPnimAcharon = GetParamHour(sTmp, dCardDate);

                //237 -ויזה צה"ל שעת התחלה לתשלום - יום אחרון
                sTmp = GetOneParam(237, dCardDate);
                dShatHatchalaVisaTzahalAcharon = GetParamHour(sTmp, dCardDate);

                //238- מינימום דקות לזכאות לנסיעות
                 sTmp = GetOneParam(238, dCardDate);
                 iMinDakotLezakautNesiot = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                //239- מינימום דקות נהיגה ותנועה לזכות נסיעות
                sTmp = GetOneParam(239, dCardDate);
                iMinDakotNehigaVetnuaLezakautNesiot = String.IsNullOrEmpty(sTmp) ? 0 : int.Parse(sTmp);

                 //240- סיום לילה לעובד עם מאפיין לא מפעיל
                sTmp = GetOneParam(240, dCardDate);
                dSiyumLilaLeovedLoMafil = GetParamHour(sTmp, dCardDate.AddDays(1));

                //241- סיום לילה מוצ''ש מפעיל
                sTmp = GetOneParam(241, dCardDate);
                dSiyumLilaMotsashMafil = GetParamHour(sTmp, dCardDate.AddDays(1));

                //242- שעת גמר לבדיקת יום של שעת יציאה
                sTmp = GetOneParam(242, dCardDate);
                dShatGmarNextDay = GetParamHour(sTmp, dCardDate.AddDays(1));

                //244- התחלה מותרת-טווח עליון-נהגות וניהול תנועה
                sTmp = GetOneParam(244, dCardDate);
                dShatHatchalaNahagutNihulTnua = GetParamHour(sTmp, dCardDate.AddDays(1));

                //245- פרמיה גבוהה
                sTmp = GetOneParam(245, dCardDate);
                fHighPremya = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);


                //504 -אגד תעבורה- בסיס לחישוב פרמיית נהיגה
                sTmp = GetOneParam(504, dCardDate);
                fBasisLechishuvPremia = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);

                //505 - אגד תעבורה - מקסימום פרמיית נהיגה
                sTmp = GetOneParam(505, dCardDate);
                fMaxPremiatNehiga = String.IsNullOrEmpty(sTmp) ? 0 : float.Parse(sTmp);
               
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
                tmpDate = new DateTime(clGeneral.cYearNull,1,1);
            }

            return tmpDate;
        }

        private DateTime GetParamHour(string sHour, DateTime dCardDate)
        {
            DateTime dTemp;
           if (String.IsNullOrEmpty(sHour)) {
                     dTemp =new DateTime(clGeneral.cYearNull, 1, 1);
           }
           else
           {
               dTemp = clGeneral.GetDateTimeFromStringHour(sHour, dCardDate);
           }
            return dTemp;
         }
       
       

        public string GetOneParam(int iParamNum, DateTime dDate)
        {   //הפונקציה מקבלת קוד פרמטר ותאריך ומחזירה את הערך
            string sParamVal = "";
            DataRow[] dr;

            dr = dtParameters.Select(string.Concat("kod_param=", iParamNum.ToString(), " and Convert('", dDate.ToShortDateString(), "','System.DateTime') >= me_taarich and Convert('", dDate.ToShortDateString(), "', 'System.DateTime') <= ad_taarich"));
            //dr = dtParameters.Select(string.Concat("kod_param=", iParamNum.ToString()));
            if (dr.Length > 0)
            {
                sParamVal = dr[0]["erech_param"].ToString();
            }

            return sParamVal;
        }

        public void SetShatKnisatShabat(DateTime dTaarich, int iSugYom)
        {

            if (iSugYom == clGeneral.enSugYom.ErevYomHatsmaut.GetHashCode())
            {//יום העצמאות - פרמטר 8
                dKnisatShabat = clGeneral.GetDateTimeFromStringHour(GetOneParam(8, dTaarich), dTaarich);
            }
            else
            {
                //נקבע את שעת כניסת השבת לפי התקופה
                if ((dTaarich >= dStartShabat) && (dTaarich <= dEndShabat))
                {//פרמטר 7 קיץ                        
                    dKnisatShabat = clGeneral.GetDateTimeFromStringHour(GetOneParam(7, dTaarich),dTaarich);
                    // iShabatStart = iShabatStartInSummer;
                }
                else
                {//חורף פרמטר 6
                    dKnisatShabat = clGeneral.GetDateTimeFromStringHour(GetOneParam(6, dTaarich), dTaarich); 
                    //iShabatStart = iShabatStartInAuttom; //פרמטר 6  
                }
            }
        }

     
    }

}
