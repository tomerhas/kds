using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels
{
    public class clParametersDM
    {
        public DateTime dSidurStartLimitHourParam1;  // 1 - מגבלת שעת התחלה
        public DateTime dSidurLimitShatGmar; //2 - שעת גמר רשמי
        public DateTime dSidurEndLimitHourParam3;   //מגבלת שעת סיום - 3
        public DateTime dSidurLimitShatGmarMafilim; //4 -שעת גמר מפעילי מחשב 

        public DateTime dTchilatTosefetLaila; //9	תחילת תוספת לילה אגד	שעה	19:01
        public DateTime dSiyumTosefetLaila; //10	סיום תוספת לילה אגד	שעה	22:00
        public DateTime dTchilatTosefetLailaChok;//11	תחילת תוספת לילה חוק	שעה	22:01
        public DateTime dSiyumTosefetLailaChok;//12	סיום תוספת לילה חוק	שעה	06:00
        public DateTime dTchilatTosefetLailaYomNochechi;//13	תחילת תוספת לילה יום נוכחי 
        public DateTime dSiyumTosefetLailaYomNochechi;//14	סיום תוספת לילה יום נוכחי 


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
        public int iMaxMonthToDisplay;//מציג את מספר החודשים אחורה שניתן להציג 100
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

        public int iMaxZmanRekaNichleletafter8; //126


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

        public DateTime dTaarichPremiatNahagut; // - 279  תאריך שינוי לוגיקת פרמית נהגות 
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
        public int iValidDays;//252 -(מספר ימים בתוקף (45
        public DateTime dChodeshTakanonSoziali;//265 - חודש הפעלת שינוי תקנון סוציאלי 
        //  public DateTime dChodeshTakanonSozialiFrinds;//268 - חודש הפעלת שינוי תקנון סוציאלי לחברים 
        public DateTime dStartAruchatTzaharayim246;//246 - תחילת ארוחת צהריים
        public DateTime dEndAruchatTzaharayim247;//247 - סיום ארוחת צהריים
        public int iVisutMustRechevWC; //261 - ויסות דורש רכב בכרטיס עבוד
        public int iDaysToViewWorkCard;//263 - מספר הימים שניתן לראות כרטיס עבודה ללא סידורים (נהג או משתמש רגיל)
        public DateTime dTaarichTokefShgiotHachtamatShaon;//272 - תאריך תוקף לבדיקת שגיאות החתמת שעון
        public DateTime dTaarichBitulTamrizNahagut;//273 - תאריך ביטול תמריץ נהגות
        public DateTime dShaaGrirotChol; //-  274   
        public DateTime dShaaGrirotErevChagShishi; // - 275
        public DateTime dShatHatchalaGrira; //276-התחלה מותרת-טווח עליון-גרירה בפועל
        public DateTime dTaarichHafalatMichsot; // - 277
        public float fBasisLechishuvPremia; //504 -אגד תעבורה- בסיס לחישוב פרמיית נהיגה
        public float fMichsatSaotChodshitET; //503 -אגד תעבורה- מכסת שעות חודשית 
        public float fMaxPremiatNehiga; //505 - אגד תעבורה - מקסימום פרמיית נהיגה
        public float fBasisLechishuvPremiatNehigaETElad; //506 -תעבורה-אלעד-בסיס לחישוב פרמיית נהיגה
        public float fMaxPremiatNehigaETElad; //507 - תעבורה-אלעד-מכסימום פרמיית נהיגה
        public float fFactorLetashlumPizulimTaavura; //508 -תעבורה-פקטור לתשלום פיצולים
        public float fFactorLetashlumPizulimTaavuraElad; //509 -תעבורה-אלעד-פקטור לתשלום פיצולים
        public DateTime _Taarich;
    }
}
