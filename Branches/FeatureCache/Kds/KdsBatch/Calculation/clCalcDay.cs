using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary;
using KDSCommon.DataModels;
using KDSCommon.Enums;

namespace KdsBatch
{
    public class clCalcDay : GlobalDataset
    {
        private DateTime _Taarich;
        private int _iMisparIshi;
        private clCalcSidur oSidur;
        private long _lBakashaId;
        private MeafyenimDM _objMefyeneyOved;
        private clParametersDM _objParameters;
        private DataTable _dtChishuvYom;
        private DataTable _dtYemeyAvodaOved;
        private clCalcGeneral _oGeneralData;
        private clCalcPeilut oPeilut;

        public clCalcDay(int iMisparIshi, long lBakashaId, clCalcGeneral oGeneralData)
        {
            _iMisparIshi = iMisparIshi;
             _lBakashaId = lBakashaId;

             _dtChishuvYom = clCalcData.DtDay;
             _oGeneralData = oGeneralData;

             oSidur = new clCalcSidur(_iMisparIshi, _lBakashaId, _oGeneralData);
             oPeilut = new clCalcPeilut(_iMisparIshi, _lBakashaId, _oGeneralData);
             oSidur.oPeilut = oPeilut;
         }

        internal void CalcRechivim(DateTime dTaarich)
        {
            try
            {
           
            _Taarich = dTaarich;

            _objMefyeneyOved = _oGeneralData.objMeafyeneyOved;
            _objParameters = _oGeneralData.objParameters;
            _dtYemeyAvodaOved = clCalcData.DtYemeyAvoda;
           
            oSidur.dTaarich = dTaarich;
            //יש משמעות לסדר חישוב הרכיבים , אין להזיז מיקום!

            //דקות 1:1 אלמנטים(רכיב 267
            CalcRechiv267();

            //דקות נסיעה לפרמיה  (רכיב 268
            CalcRechiv268();

                //דקות 1:1 (רכיב 52
                CalcRechiv52();
                
                //  דקות זיכוי חופש (רכיב 7) 
                CalcRechiv7();
                               
                //  דקות לתוספת משק  (רכיב 12) 
                CalcRechiv12();

                //דקות מחליף משלח (רכיב 13) 
                CalcRechiv13();

                //דקות מחליף סדרן (רכיב 14) 
                CalcRechiv14();

                //דקות מחליף פקח(רכיב 15) 
                CalcRechiv15();

                //דקות מחליף קופאי (רכיב 16
                CalcRechiv16();

                //דקות מחליף רכז (רכיב 17) 
                CalcRechiv17();

                //דקות נוכחות בפועל(רכיב 18 
                CalcRechiv18();

                //דקות מחליף תנועה (רכיב 129): 
                CalcRechiv129();
                               
                //דקות סיכון (רכיב 23)
                CalcRechiv23();

                //סהכ ק"מ  (רכיב 215)
                CalcRechiv215();

                //סה"כ לאשל בוקר (רכיב 38) 
         //       CalcRechiv38();

                //סה"כ לאשל ערב (רכיב 40)
           //     CalcRechiv40();
                       
                //סה"כ לינה (רכיב 47 
                CalcRechiv47();

                //סה"כ לינה  כפולה (רכיב 48)
                CalcRechiv48();

                //סה"כ פיצול כפול (רכיב 50
                CalcRechiv50();

                //זמן להמרת שעות שבת (רכיב 53) 
                CalcRechiv53();

                //חופש זכות (רכיב 132):
                CalcRechiv132();

                //סהכ דקות הסתגלות (רכיב 214)
                CalcRechiv214();

                //סהכ נסיעות (רכיב 213)
                CalcRechiv213();
              
                //דקות הגדרה (רכיב 217) :
                CalcRechiv217();

                //סהכ ק"מ ויזה לפרמיה  (רכיב 216
                CalcRechiv216();

                //דקות פרמיה ויזה (רכיב 28) 
                CalcRechiv28();

                //דקות פרמיה ויזה בשבת (רכיב 29) 
                CalcRechiv29();

                // CalcRechiv39();

                //פרמיה נמל"ק (רכיב 134):
                CalcRechiv134();

                //רכיב 931 – הלבשה תחילת יום
                CalcRechiv931();

                //רכיב 932 - הלבשה סוף יום
                CalcRechiv932();

                //*54**//

                //  CalcRechiv74();

                 //סה"כ לפיצול (רכיב 49)
                CalcRechiv49();

                //זמן גרירות (רכיב 128 ) 
                CalcRechiv128();

                //זמן  ארוחת צהרים  רכיב 88
                CalcRechiv88();

                //דקות נוכחות לתשלום( רכיב 1)
                CalcRechiv1();

                //יום אבל  (רכיב 56) :
                CalcRechiv56();

                //יום הדרכה רכיב 57 :  
                CalcRechiv57();

                //יום ללא דיווח (רכיב 59) 
                CalcRechiv59();

                //יום מחלה (רכיב 60) :
                CalcRechiv60();

                //יום מחלה בודד (רכיב 61) :
                CalcRechiv61();

                //יום מילואים (רכיב 62) :
                CalcRechiv62();

                //יום עבודה בחו"ל (רכיב 63): 
                CalcRechiv63();

                //יום תאונה  (רכיב 64) : 
                CalcRechiv64();

                //יום שמירת הריון בת-זוג  (רכיב 65):
                CalcRechiv65();

                //יום טיפת חלב  (רכיב 68) :
                CalcRechiv68();

                //יום מחלת בן-זוג  (רכיב 69) :
                CalcRechiv69();

                //יום מחלת הורים (רכיב 70) :
                CalcRechiv70();

                //יום מחלת ילד (רכיב 71) :
                CalcRechiv71();

                //יום הסבה לקו (רכיב 72) : 
                CalcRechiv72();

                //ימי נוכחות לעובד (רכיב 75) 
                CalcRechiv75();

                //תוספת רציפות 1-1(נהגות)   - רכיב 96:  
                CalcRechiv96();

                //דקות כיסוי תור (רכיב 218
                CalcRechiv218();

                //דקות פרמיה בשבת  (רכיב 26 ) 
                CalcRechiv26();

                //דקות פרמיה יומית  (רכיב 30)
                CalcRechiv30();

                //דקות פרמיה בשישי  (רכיב 202 ) 
                CalcRechiv202();

                //פרמיה רגילה (רכיב 133): 
                CalcRechiv133();

                //ימי נוכחות לעובד (רכיב 109)
                CalcRechiv109();

                //משמרת שנייה במשק (רכיב 125) 
                CalcRechiv125();

                //קיזוז דקות התחלה-גמר  (רכיב 86):
                CalcRechiv86();

                //CalcRechiv87();

                //קיזוז זמן בויזות (רכיב 89) 
                CalcRechiv89();

                //השלמה בנהגות – רכיב 263 
                CalcRechiv263();

                //השלמה בניהול תנועה – רכיב 264 :  
                CalcRechiv264();

                //השלמה בתפקיד – רכיב 265 :  
                CalcRechiv265();

                //תוספת זמן השלמה – רכיב 94 
                 CalcRechiv94();

                
                //תוספת רציפות תפקיד   - רכיב 97:  
                //CalcRechiv97(); בוטל

                //ימי עבודה  ללא מיוחדים (רכיב 110
                CalcRechiv110();

                // דקות בנהיגה בימי חול ( רכיב 2)
                CalcRechiv2();

                //דקות בניהול תנועה בימי חול ( רכיב 3)
                CalcRechiv3();
                
                //דקות בתפקיד בימי חול ( רכיב 4)
                CalcRechiv4();
                    
                // דקות בנהיגה בימי שבתון ( רכיב 35)
                CalcRechiv35();
               
                //חישוב קיזוז עבור סידורים מיוחדים בעלי מכסה חודשית. 
                CalcRechiv135();
                CalcRechiv136();
                CalcRechiv137();
                CalcRechiv138();
                CalcRechiv139();
                CalcRechiv140();
                CalcRechiv141();
                CalcRechiv145();
             
                CalcRechiv185();

                //קיזוז נוכחות ( רכיב 142): 
                CalcRechiv142();
                               
                //דקות רגילות (רכיב 32
                CalcRechiv32();


                // דקות בניהול תנועה בימי שבתון ( רכיב 36)
                CalcRechiv36();

                //דקות בתפקיד בימי שבת ( רכיב 37)
                CalcRechiv37();
                    
                //סה"כ לאשל צהריים (רכיב 42)
            //    CalcRechiv42();

                //סה"כ לאשל בוקר/ערב/צהריים למבקרים בדרכים (רכיב /41/43/39) 
                CalcRechiv39_41_43();

                //כמות גמול חסכון מיוחד (רכיב 45) 
                //CalcRechiv45();
                              
                //דקות לתוספת מיוחדת בתפקיד  - תמריץ (רכיב 10) 
                CalcRechiv10();

                //השלמה ע"ח פרמיה (רכיב 149): 
             //   CalcRechiv149();

                ////CalcRechiv150();
                ////CalcRechiv151();
                ////CalcRechiv152();
                ////CalcRechiv153();
                ////CalcRechiv154();
                ////CalcRechiv155();
                ////CalcRechiv156();
                ////CalcRechiv157();
                ////CalcRechiv158();
                ////CalcRechiv159();

                ////דקות מחוץ למכסה פקח (רכיב 164): 
                //CalcRechiv164();

                ////דקות מחוץ למכסה סדרן (רכיב 165
                //CalcRechiv165();

                ////דקות מחוץ למכסה רכז (רכיב 166)
                //CalcRechiv166();

                ////דקות מחוץ למכסה משלח (רכיב 167):  
                //CalcRechiv167();

                ////CalcRechiv168();

                //חישוב קיזוז עבור סידורים מיוחדים בעלי מכסה חודשית. 
                CalcRechiv174();
                CalcRechiv175();
                CalcRechiv176();
                CalcRechiv177();
                CalcRechiv178();

                //סהכ דקות פקח (רכיב 179):  
                CalcRechiv179();

                //סהכ דקות סדרן (רכיב 180)
                CalcRechiv180();

                //סהכ דקות רכז (רכיב 181
                CalcRechiv181();

                //סהכ דקות משלח (רכיב 182):
                CalcRechiv182();

                //סהכ דקות קופאי (רכיב 183)
                CalcRechiv183();


                //מחוץ למכסה תפקיד בשבת (רכיב 186): 
                CalcRechiv186();

                //מחוץ למכסה ניהול תנועה בשבת (רכיב 187): 
                CalcRechiv187();

                //סה"כ דקות מחוץ למכסה בשבת (רכיב 81) : 
                CalcRechiv81();

                //דקות נהגות בשישי ( רכיב 189): 
               CalcRechiv189();

                //סהכ נהגות (רכיב 188): 
                CalcRechiv188();

                // דקות ניהול תנועה בשישי ( רכיב 191): 
                CalcRechiv191();
                               
                // דקות תפקיד בשישי ( רכיב 193)
                CalcRechiv193();
                              
                //יום היעדרות  (רכיב 66) 
                CalcRechiv66();

                //דקות היעדרות (רכיב 220) 
                CalcRechiv220();

                // שעות היעדרות ( רכיב 5) 
                CalcRechiv5();

                //נוכחות לפרמיה - רישום ( רכיב 209): 
                CalcRechiv209();

                //סהכ תפקיד (רכיב 192):  
                CalcRechiv192();
                
                //תוספת זמן הלבשה (רכיב 93)
                CalcRechiv93();

                //סהכ ניהול תנועה (רכיב 190): 
                CalcRechiv190();

                //תוספת זמן נסיעה – רכיב 95
                CalcRechiv95();
                
                //סה"כ תוספת רציפות (רכיב 85)
                CalcRechiv85();

                //נוכחות חול ( רכיב 194): 
                CalcRechiv194();
                
                //נוכחות שישי ( רכיב 195)
                CalcRechiv195();

                //נוכחות שבת ( רכיב 196): 
                CalcRechiv196();

                //שבת/שעות 100% (רכיב 131): 
                CalcRechiv131();

                //כמות גמול חסכון (רכיב 22) 
                CalcRechiv22();

                //כמות גמול חסכון נוספות (רכיב 44) 
                CalcRechiv44();

                 //קיזוז לעובד מותאם (רכיב 90
                CalcRechiv90();

                UpdateRechiv1();

                //דקות פרמיה בתוך המכסה  (רכיב 27) 
                CalcRechiv27();

                //נוספות 100% לעובד חודשי (רכיב 146 ) 
                CalcRechiv146();
                //שעות % 100 לתשלום (רכיב 100
                CalcRechiv100();

                //שעות 25% (רכיב 91
                CalcRechiv91();

                //שעות 50% (רכיב 92
                CalcRechiv92();

                //יום חופש  (רכיב 67) 
                CalcRechiv67();

                //דקות חופש (רכיב 221) 
                CalcRechiv221();

                //שעות חופש (רכיב 219) 
                CalcRechiv219();

                //נוספות 125% (רכיב 76): 
                CalcRechiv76();

                //נוספות 150% (רכיב 77): 
                CalcRechiv77();

                //נוספות שבת (רכיב 78) 
                CalcRechiv78();

                //זמן לילה סידורי בוקר (רכיב 271): 
                CalcRechiv271();

                //זמן לילה ראשון  (רכיב 54) 
                CalcRechiv54();

                //זמן לילה שני אחרי 2200 (רכיב 55)
                CalcRechiv55();

                //נוספות שישי ( רכיב 198)
                CalcRechiv198();

                //מחוץ למכסה בתפקיד שישי (רכיב 207): 
                CalcRechiv207();
                
                //מחוץ למכסה בניהול תנועה שישי (רכיב 208): 
                CalcRechiv208();

                //דקות נוספות בנהגות (רכיב 19)/ דקות נוספות בניהול תנועה (רכיב 20)/ דקות נוספות בתפקיד (רכיב 21): 
                CalcRechiv19_20_21();

                //קיזוז נוספות תפקיד חול ושישי (רכיב 147)
                CalcRechiv147();

                //קיזוז נוספות תנועה חול (רכיב 160)
                CalcRechiv160();

                //נוספות תפקיד חול ושישי לאחר קיזוז (רכיב 250)
                CalcRechiv250();

                //דקות מחוץ למכסה תפקיד חול (רכיב 80):
                CalcRechiv80();

                //טיפול בסידור ועד עובדים (99008)
                SetSidureyVaadOvdim();

                //דקות מחוץ למכסה ניהול תנועה חול (רכיב 184)
                CalcRechiv184();

                //מחוץ למכסה חול ( רכיב 200): 
                CalcRechiv200();

                //נוספות נהגות חול ושישי לאחר קיזוז (רכיב 252) 
                CalcRechiv252();

                //נוספות תנועה חול ושישי לאחר קיזוז (רכיב 251) : 
                CalcRechiv251();

                //נוכחות לפרמיה – משק מוסכים (רכיב 211) -נוכחות לפרמיה – משק אחסנה (רכיב 212)
                CalcRechiv211_212();

                //נוכחות לפרמיה – משק גרירה (276)
                CalcRechiv276();

                //נוכחות לפרמיה – משק כוננות גרירה (76)
                CalcRechiv277();

                //מחוץ למכסה שישי ( רכיב 201): 
                CalcRechiv201();

                //דקות פרמיה ויזה בשישי (רכיב 203) :
                CalcRechiv203();

                //נוכחות לפרמיית נהגי טנדרים (רכיב 235)
                CalcRechiv235();

                //אש"ל לאגד תעבורה (רכיב 245) 
                CalcRechiv245();

                //נוכחות לפרמיה – מבקרים בדרכים ( רכיב 210): 
                CalcRechiv210();

                //נוכחות לפרמיה – מחסן כרטיסים ( רכיב 223
                CalcRechiv223();

                //נוכחות לפרמיית מפעל ייצור (רכיב 233
                CalcRechiv233();

                //נוכחות לפרמיית נהגי תובלה (רכיב 234) 
                CalcRechiv234();

                //נוכחות לפרמיית דפוס (רכיב 236) 
                CalcRechiv236();

                //נוכחות לפרמיית אחזקה (רכיב 237) 
                CalcRechiv237();

                //נוכחות לפרמיית גיפור (רכיב 238) 
                CalcRechiv238();

                //נוכחות לפרמיית מוסך ראשלצ (רכיב 239) 
                CalcRechiv239();

                //נוכחות לפרמיית טכנאי ייצור (רכיב 240) 
                CalcRechiv240();

                //נוכחות לפרמיית פירוק ושיפוץ מכללים (רכיב 241) 
                CalcRechiv241();

                //נוכחות לפרמית מנהל בית ספר לנהיגה ( רכיב 246): 
                CalcRechiv246();

                //יום חופש עקב אי דיווח (רכיב 248) : 
                CalcRechiv248();

                //יום היעדרות עקב אי דיווח (רכיב 249) : 
                CalcRechiv249();

                //מחלה יום מלא (רכיב 261) 
                CalcRechiv261();

                //מחלה יום חלקי (רכיב 262) : 
                CalcRechiv262();

                //נוכחות לפרמיית מנ"סים (רכיב 256) 
                CalcRechiv256();

                //נוכחות לפרמיית מתכנן תנועה (רכיב 257)  
                CalcRechiv257();

                //נוכחות לפרמיית סדרן (רכיב 258) 
                CalcRechiv258();

                //נוכחות לפרמיית רכז (רכיב 259)  
                CalcRechiv259();

                //נוכחות לפרמיית פקח (רכיב 260) 
                CalcRechiv260();
                
                //יום מילואים חלקי (רכיב 266) 
                CalcRechiv266();

                //דקות חופש/היעדרות (רכיב 269) 
                CalcRechiv269();

                //ימי חופש/היעדרות (רכיב 270) 
                CalcRechiv270();

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", 0, _Taarich, "CalcRechivim: " + ex.Message);
                throw (ex);
            }
        }



        private void CalcRechiv1()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv1();

                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                addRowToTable(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void UpdateRechiv1()
        {
            float fSumDakotRechiv, fMichsaYomit, fZmanNesia,fDakotNocheut, fTemp, fShaotshabat100, fZmanNesiot;
            try
            {
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanHashlama.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanHalbasha.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.HashlamaBenihulTnua.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.HashlamaBetafkid.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.HashlamaBenahagut.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                //fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                //fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                fSumDakotRechiv = fSumDakotRechiv - clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fSumDakotRechiv = fSumDakotRechiv - clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzBevisa.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fSumDakotRechiv = fSumDakotRechiv - clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzOvedMutam.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                fZmanNesia = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanNesia.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                if (fZmanNesia > 0 && (fMichsaYomit - fSumDakotRechiv) > 0)
                {
                    fTemp = Math.Min(fZmanNesia, (fMichsaYomit - fSumDakotRechiv));
                    fSumDakotRechiv = fSumDakotRechiv + fTemp;
                    addRowToTable(clGeneral.enRechivim.ZmanNesia.GetHashCode(), fZmanNesia - fTemp);
                }


                if (fSumDakotRechiv < fMichsaYomit && (fMichsaYomit - fSumDakotRechiv) <= 2)
                    fSumDakotRechiv = fMichsaYomit;

                addRowToTable(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), fSumDakotRechiv);

                //עדכון רכיב 131
                fDakotNocheut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                fZmanNesiot = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanNesia.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                
                if (fZmanNesiot > 0)
                {
                    fShaotshabat100 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ShaotShabat100.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
               
                    addRowToTable(clGeneral.enRechivim.ShaotShabat100.GetHashCode(), fShaotshabat100 + fZmanNesiot);
                }
                 HashlamatNochehutLetashlumMiRechiveyPremia();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        private void HashlamatNochehutLetashlumMiRechiveyPremia()
        {
            float fErechRechiv1 ,fErechRechiv126, fErechRechiv2, fErechRechiv133, fErechRechiv134;
            float ftosefetMax133, ftosefetMax134, ftosefetMax134_2,fErechRechiv1New, fErechRechiv1New2;
            try{
                fErechRechiv1 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fErechRechiv126 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fErechRechiv2 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fErechRechiv133 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.PremyaRegila.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fErechRechiv134 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.PremyaNamlak.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                if ((fErechRechiv1 < fErechRechiv126) && (fErechRechiv2 >= (fErechRechiv126 / 2)) &&
                      ((fErechRechiv126 - fErechRechiv1)<=30)  && ((fErechRechiv133 + fErechRechiv134) > 0))
                {
                    ftosefetMax133 = fErechRechiv133 > 30 ? 30 : fErechRechiv133;
                    ftosefetMax134 = fErechRechiv134 > 30 ? 30 : fErechRechiv134;

                    fErechRechiv1New = Math.Min(fErechRechiv126, fErechRechiv1 + ftosefetMax133);
                    addRowToTable(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), fErechRechiv1New);
                    fErechRechiv133 = fErechRechiv133 - (Math.Min(ftosefetMax133, fErechRechiv126 - fErechRechiv1));
                    addRowToTable(clGeneral.enRechivim.PremyaRegila.GetHashCode(), fErechRechiv133);

                    if (fErechRechiv1New < fErechRechiv126)
                    {
                        ftosefetMax134_2 = Math.Min(fErechRechiv134,( (30 - fErechRechiv133) < 0 ? 0 : (30 - fErechRechiv133)) );
                        fErechRechiv1New2 = Math.Min(fErechRechiv126, fErechRechiv1New + ftosefetMax134_2);
                        addRowToTable(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), fErechRechiv1New2);

                        fErechRechiv134 = fErechRechiv134 - (Math.Min(ftosefetMax134_2 , fErechRechiv126 - fErechRechiv1New));
                        addRowToTable(clGeneral.enRechivim.PremyaNamlak.GetHashCode(), fErechRechiv134);
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        private void CalcRechiv2()
        {
            float fSumDakotRechiv;
          try{
            oSidur.CalcRechiv2();
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            if (fSumDakotRechiv > 0)
            {
                fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanHashlama.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                 fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.HashlamaBenahagut.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                addRowToTable(clGeneral.enRechivim.DakotNehigaChol.GetHashCode(), fSumDakotRechiv);
            }
         }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotNehigaChol.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv3()
        {
            float fSumDakotRechiv, fDakotNehigaChol;
            try
            {
                oSidur.CalcRechiv3();
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                if (fSumDakotRechiv > 0)
                {
                    fDakotNehigaChol = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    if (fDakotNehigaChol == 0)
                    {
                        fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanHashlama.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    }
                    fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.HashlamaBenihulTnua.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    addRowToTable(clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv4()
        {
            float fSumDakotRechiv, fDakotNehigaChol,fDakotNihulChol;
            try
            {
                oSidur.CalcRechiv4();
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                if (fSumDakotRechiv > 0)
                {
                    fDakotNehigaChol = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    fDakotNihulChol = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    if (fDakotNehigaChol == 0 && fDakotNihulChol == 0)
                        fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanHashlama.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                    fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.HashlamaBetafkid.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanGrirot.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                     fSumDakotRechiv = fSumDakotRechiv - clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                    addRowToTable(clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv5()
        {
            //float fMichsaYomit126, fDakotNochehut1;
            //int iSugYom;
            //DataRow[] drDakotNochehut, drMichsaYomit;
            //bool bHeadrutMelaa = false;
            //bool bHeadrutChelkit = false;
            //bool bErevChag,bErevShabat;
            float fSumDakotRechiv;
            fSumDakotRechiv = 0;
            try{
            //iSugYom = clCalcGeneral.iSugYom;
            //bErevChag=clCalcGeneral.CheckErevChag(iSugYom);
            //bErevShabat=clCalcGeneral.CheckYomShishi(iSugYom);

            //fMichsaYomit126 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            // drMichsaYomit = _dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')");
             
            // //ביום שישי/ערב חג [זיהוי ערב חג (תאריך)] כאשר עבור רכיב מכסה יומית מחושבת לא קיימת רשומה או ערכו = 0 אין לפתוח רשומה לרכיב זה.   
            //if (!bErevChag && !bErevShabat || ((bErevChag || bErevShabat) && fMichsaYomit126>0 && drMichsaYomit.Length>0))
            //   { 
            //       if (_objMefyeneyOved.GetMeafyen(33).IntValue  == 40)
            //        {
            //           oSidur.CalcRechiv5();

            //            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ShaotHeadrut.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            //            if (fSumDakotRechiv != 0)
            //            {
            //                addRowToTable(clGeneral.enRechivim.ShaotHeadrut.GetHashCode(), fSumDakotRechiv);
            //            }
            //        }
            //       else if (_objMefyeneyOved.GetMeafyen(33).IntValue == 41)
            //       {
            //           fDakotNochehut1 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            //           drDakotNochehut = _dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')");
            //           if (fMichsaYomit126 > 0 && (fDakotNochehut1 == 0 || drDakotNochehut.Length == 0))
            //           { bHeadrutMelaa = true; }
            //           else if (fMichsaYomit126 > 0 && fDakotNochehut1 < 0 && fDakotNochehut1 < fMichsaYomit126)
            //           { bHeadrutChelkit = true; }
            //           if (bHeadrutMelaa)
            //           {
            //               //אם מדובר בהיעדרות מלאה  :    ערך הרכיב = מכסה יומית מחושבת  (רכיב 126) חלקי 60
            //               fSumDakotRechiv = fMichsaYomit126 / 60;
            //           }
            //           else if (bHeadrutChelkit)
            //           {
            //               // אם מדובר בהיעדרות חלקית: ערך הרכיב = (מכסה יומית מחושבת  (רכיב 126) פחות   דקות נוכחות לתשלום (רכיב 1))  חלקי 60.  :    
            //               fSumDakotRechiv = (fMichsaYomit126 - fDakotNochehut1) / 60;
            //           }
            //           if (fSumDakotRechiv != 0 && (bHeadrutChelkit || bHeadrutMelaa))
            //           {
            //               addRowToTable(clGeneral.enRechivim.ShaotHeadrut.GetHashCode(), fSumDakotRechiv);
            //           }
            //       }
            //   }
               fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotHeadrut.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
               fSumDakotRechiv = fSumDakotRechiv / 60;
                if (fSumDakotRechiv != 0 )
                   {
                       addRowToTable(clGeneral.enRechivim.ShaotHeadrut.GetHashCode(), fSumDakotRechiv);
                   }
           }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.ShaotHeadrut.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv7()
        {
            float fSumDakotRechiv;
          try{
            oSidur.CalcRechiv7();
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            if (fSumDakotRechiv != 0)
            {
                addRowToTable(clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode(), fSumDakotRechiv);
            }
          }
          catch (Exception ex)
          {
              clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
              throw (ex);
          }
        }

        private void CalcRechiv10()
        {
            float fTempX,fMichsaYomit, fDakotRechiv;
            try
            {
                if (_oGeneralData.objPirteyOved.iDirug != 85 || _oGeneralData.objPirteyOved.iDarga != 30)
                {
                   
                    if (_oGeneralData.objMeafyeneyOved.GetMeafyen(54).IntValue > 0)
                    {
                        oSidur.CalcRechiv10();

                        fTempX = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTamritzTafkid.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                        if (fTempX > fMichsaYomit)
                        {
                            fDakotRechiv = fTempX - fMichsaYomit;
                        }
                        else
                        { fDakotRechiv = 0; }

                        addRowToTable(clGeneral.enRechivim.DakotTamritzTafkid.GetHashCode(), fDakotRechiv);

                    }
                    
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotTamritzTafkid.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv12()
        {
            float fSumDakotRechiv, fMichsaYomit126,fTempDakot;
            try{
            oSidur.CalcRechiv12();
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTosefetMeshek.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            if (fSumDakotRechiv > 0)
            {
               fMichsaYomit126 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                if (fSumDakotRechiv <= fMichsaYomit126)
                {
                    fSumDakotRechiv = (fSumDakotRechiv * _objParameters.fAchuzTosefetLeovdeyMeshek) / 100;
                }
                else
                {
                    fTempDakot = fSumDakotRechiv - fMichsaYomit126;
                    if (fTempDakot < 120)
                    {
                        fSumDakotRechiv = ((fMichsaYomit126 * _objParameters.fAchuzTosefetLeovdeyMeshek) / 100) + (fTempDakot * float.Parse("1.25") * _objParameters.fAchuzTosefetLeovdeyMeshek) / 100;

                    }
                    else
                    {
                        fSumDakotRechiv = ((fMichsaYomit126 * _objParameters.fAchuzTosefetLeovdeyMeshek) / 100) + (_objParameters.iMaxDakotNosafot / 100 * float.Parse("1.25") * _objParameters.fAchuzTosefetLeovdeyMeshek / 100) + (fTempDakot - _objParameters.iMaxDakotNosafot) * float.Parse("1.5") * _objParameters.fAchuzTosefetLeovdeyMeshek / 100;
                    }
                }
                if (fSumDakotRechiv > 0)
                {
                    addRowToTable(clGeneral.enRechivim.DakotTosefetMeshek.GetHashCode(), fSumDakotRechiv);
                }
           }
           }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotTosefetMeshek.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv13()
        {
            float fSumDakotRechiv;
           try{
            oSidur.CalcRechiv13();
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMachlifMeshaleach.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            if (fSumDakotRechiv > 0)
            {

                addRowToTable(clGeneral.enRechivim.DakotMachlifMeshaleach.GetHashCode(), fSumDakotRechiv);
            }
           }
           catch (Exception ex)
           {
               clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotMachlifMeshaleach.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
               throw (ex);
           }
        }



        private void CalcRechiv14()
        {
            float fSumDakotRechiv;
           try{
            oSidur.CalcRechiv14();
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMachlifSadran.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            if (fSumDakotRechiv > 0)
            {
                addRowToTable(clGeneral.enRechivim.DakotMachlifSadran.GetHashCode(), fSumDakotRechiv);
            }
           }
           catch (Exception ex)
           {
               clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotMachlifSadran.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
               throw (ex);
           }
        }

        private void CalcRechiv15()
        {
            float fSumDakotRechiv;
           try{
            oSidur.CalcRechiv15();
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMachlifPakach.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            if (fSumDakotRechiv > 0)
            {
                addRowToTable(clGeneral.enRechivim.DakotMachlifPakach.GetHashCode(), fSumDakotRechiv);
            }
           }
           catch (Exception ex)
           {
               clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotMachlifPakach.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
               throw (ex);
           }
        }

        private void CalcRechiv16()
        {
            float fSumDakotRechiv;
           try{
            oSidur.CalcRechiv16();
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMachlifKupai.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            addRowToTable(clGeneral.enRechivim.DakotMachlifKupai.GetHashCode(), fSumDakotRechiv);
           }
           catch (Exception ex)
           {
               clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotMachlifKupai.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
               throw (ex);
           } 
          
        }

         private void CalcRechiv17()
        {
            float fSumDakotRechiv;
          try{
             oSidur.CalcRechiv17();
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMachlifRakaz.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
           addRowToTable(clGeneral.enRechivim.DakotMachlifRakaz.GetHashCode(), fSumDakotRechiv);
          }
          catch (Exception ex)
          {
              clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotMachlifRakaz.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
              throw (ex);
          }
         
        }

         private void CalcRechiv18()
         {
             float fSumDakotRechiv;
             try{
             oSidur.CalcRechiv18();
             fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            addRowToTable(clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode(), fSumDakotRechiv);
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                 throw (ex);
             }   
        }

         private void CalcRechiv19_20_21()
         {
             float fSumDakotRechiv19, fTempX, fMichsaYomit, fDakotNahagutChol,fDakotNihulChol, fDakotTafkidChol, fSumDakotRechiv20, fSumDakotRechiv21;
             int iCountNihulTnua, iCountNahagutChol, iCountTafkidChol;
             fSumDakotRechiv19=0;
             fSumDakotRechiv20=0;
                 fSumDakotRechiv21=0;
             try
             {
                 if (!_oGeneralData.CheckIsurShaotNosafot())
                     {
                         if ( _objMefyeneyOved.GetMeafyen(2).IntValue == 0)
                             {
                             fTempX = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                             fTempX = fTempX - clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ShaotShabat100.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                             fTempX = fTempX - clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                             fTempX = fTempX - clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            
                             fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                             iCountNihulTnua = _dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')").Length;
                             iCountNahagutChol = _dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')").Length;
                             iCountTafkidChol = _dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')").Length;
                            
                             if (fTempX > fMichsaYomit)
                             {
                                 fDakotTafkidChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                 fDakotTafkidChol = fDakotTafkidChol - clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanNesia.GetHashCode() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                 if (fDakotTafkidChol > 0 && (iCountNihulTnua == 0 && iCountNahagutChol == 0))
                                 {
                                     fSumDakotRechiv21 = fTempX - fMichsaYomit;
                                 }
                               

                                 fDakotNihulChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                 if (fDakotNihulChol > 0 && (iCountTafkidChol == 0 && iCountNahagutChol == 0))
                                 {
                                     fSumDakotRechiv20 = fTempX - fMichsaYomit;
                                 }
                               
                                 fDakotNahagutChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                 if (fDakotNahagutChol > 0 && iCountTafkidChol == 0 && iCountNihulTnua == 0)
                                  {
                                         fSumDakotRechiv19 = fTempX - fMichsaYomit;
                                  }

                                 //כלומר ביצע עבודת תפקיד + נהגות
                                 if (fDakotTafkidChol > 0 && fDakotNahagutChol > 0 && iCountNihulTnua==0)
                                 { 
                                     if (fDakotTafkidChol>=fMichsaYomit && fDakotTafkidChol>=fTempX)
                                     {
                                         fSumDakotRechiv21 = fTempX - fMichsaYomit;
                                     }
                                     else if (fDakotTafkidChol>=fMichsaYomit && fDakotTafkidChol<fTempX)
                                     {
                                           fSumDakotRechiv21 = fDakotTafkidChol - fMichsaYomit;
                                           fSumDakotRechiv19 = fTempX - fMichsaYomit-fSumDakotRechiv21;
                                     }
                                     else if (fDakotTafkidChol<=fMichsaYomit)
                                     {
                                       fSumDakotRechiv19 = fTempX - fMichsaYomit;
                                     }
                                 }

                                 //כלומר ביצע עבודת תנועה + נהגות
                                 if (fDakotNihulChol > 0 && fDakotNahagutChol > 0 && iCountTafkidChol==0)
                                 { 
                                     if (fDakotNihulChol>=fMichsaYomit && fDakotNihulChol>=fTempX)
                                     {
                                         fSumDakotRechiv20 = fTempX - fMichsaYomit;
                                     }
                                     else if (fDakotTafkidChol>=fMichsaYomit && fDakotTafkidChol<fTempX)
                                     {
                                           fSumDakotRechiv20 = fDakotNihulChol - fMichsaYomit;
                                           fSumDakotRechiv19 = fTempX - fMichsaYomit-fSumDakotRechiv20;
                                     }
                                     else if (fDakotTafkidChol<=fMichsaYomit)
                                     {
                                       fSumDakotRechiv19 = fTempX - fMichsaYomit;
                                     }
                                 }

                                 //כלומר ביצע עבודת תנועה + תפקיד
                                   if (fDakotNihulChol > 0 && fDakotTafkidChol > 0 && iCountNahagutChol==0)
                                 { 
                                     if (fDakotTafkidChol>=fMichsaYomit && fDakotTafkidChol>=fTempX)
                                     {
                                         fSumDakotRechiv21 = fTempX - fMichsaYomit;
                                     }
                                     else if (fDakotTafkidChol>=fMichsaYomit && fDakotTafkidChol<fTempX)
                                     {
                                           fSumDakotRechiv21 = fDakotTafkidChol - fMichsaYomit;
                                           fSumDakotRechiv19 = fTempX - fMichsaYomit-fSumDakotRechiv21;
                                     }
                                     else if (fDakotTafkidChol<=fMichsaYomit)
                                     {
                                       fSumDakotRechiv20 = fTempX - fMichsaYomit;
                                     }
                                 }


                                 //כלומר ביצע עבודת נהגות + תנועה + תפקיד
                                  if (fDakotNihulChol > 0 && fDakotTafkidChol > 0 && fDakotNahagutChol>0)
                                 { 
                                     if (fDakotTafkidChol>=fMichsaYomit && fDakotTafkidChol>=fTempX)
                                     {
                                         fSumDakotRechiv21 = fTempX - fMichsaYomit;
                                     }
                                     else if (fDakotTafkidChol>=fMichsaYomit && fDakotTafkidChol<fTempX)
                                     {
                                           fSumDakotRechiv21 = fDakotTafkidChol - fMichsaYomit;
                                           fSumDakotRechiv19 = fTempX - fMichsaYomit-fSumDakotRechiv21;
                                           if (fDakotNahagutChol< fSumDakotRechiv19)
                                           {
                                               fSumDakotRechiv20 = fSumDakotRechiv19 - fDakotNahagutChol;
                                               fSumDakotRechiv19 = fDakotNahagutChol;
                                           }
                                     }
                                     else if (fDakotTafkidChol <= fMichsaYomit)
                                     {
                                         fSumDakotRechiv19 = fTempX - fMichsaYomit;

                                         if (fDakotNahagutChol < fSumDakotRechiv19)
                                         {
                                             fSumDakotRechiv20 = fSumDakotRechiv19 - fDakotNahagutChol;
                                             fSumDakotRechiv19 = fDakotNahagutChol;
                                         }
                                     }
                                      
                                 }
                   
                            
                                addRowToTable(clGeneral.enRechivim.DakotNosafotNahagut.GetHashCode(), fSumDakotRechiv19);
                                 addRowToTable(clGeneral.enRechivim.DakotNosafotNihul.GetHashCode(), fSumDakotRechiv20);
                                 if ((_objMefyeneyOved.GetMeafyen(74).Value != "1") || (_objMefyeneyOved.GetMeafyen(74).Value == "1" && _oGeneralData.objPirteyOved.iIsuk == clGeneral.enIsukOved.Poked.GetHashCode() && clCalcData.CheckYomShishi(clCalcData.iSugYom)))
                                {
                                 addRowToTable(clGeneral.enRechivim.DakotNosafotTafkid.GetHashCode(), fSumDakotRechiv21);
                                }
                             }
                       
                     }
                      
                 }

             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotNosafotTafkid.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                 throw (ex);
             }
         }

         private void CalcRechiv147()
         {
             float fSumDakotRechiv, fDakotNosafot21, fDakotShishi193;
             try
             {

                 fDakotNosafot21 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotTafkid.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')")); 
                 fDakotShishi193 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                 fSumDakotRechiv = fDakotNosafot21 + fDakotShishi193;
                 addRowToTable(clGeneral.enRechivim.KizuzNosafotTafkidChol.GetHashCode(), fSumDakotRechiv);
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.KizuzNosafotTafkidChol.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                 throw (ex);
             }
         }

         private void CalcRechiv160()
         {
             float fSumDakotRechiv, fDakotNosafot20, fDakotShishi191;
             try
             {

                 fDakotNosafot20 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotNihul.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')")); 
                 fDakotShishi191 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')")); 

                 fSumDakotRechiv = fDakotNosafot20 + fDakotShishi191;
                 addRowToTable(clGeneral.enRechivim.KizuzNosafotTnuaChol.GetHashCode(), fSumDakotRechiv);
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.KizuzNosafotTnuaChol.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                 throw (ex);
             }
         }
         private void CalcRechiv22()
         {
             //יש לפתוח רכיב רק אם העובד בעל מאפיין ביצוע [שליפת מאפיין ביצוע (קוד מאפיין=60)] עם ערך כלשהו ו/או קיים סידור מזכה לגמול 
             float fSumDakotRechiv, fDakotNehiga, fMichsaYomit126,fNochechtLeTashlum;
             try{
             //יש לחשב רק בתנאים אלו:
             // אם עובד שכיר קבוע (קוד מעמד = 22 או 21) 
             // העובד חבר  (הספרה הראשונה של קוד מעמד = 1) 
             //if (_oGeneralData.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.Sachir12.GetHashCode() || _oGeneralData.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.SachirKavua.GetHashCode() || (_oGeneralData.objPirteyOved.iKodMaamdRashi == clGeneral.enMaamad.Friends.GetHashCode()))
             //{
                 if (!(_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30))
                 {
                     fMichsaYomit126 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                     //יש לבדוק האם ביום העבודה בו מוגדר הסידור קיימת מכסה יומית מחושבת (רכיב 126) > 0. אם קיימת, להמשיך בחישוב. אם לא קיימת, אין לבצע את החישוב בשום רמה 
                     if (fMichsaYomit126 > 0)
                     {
                         if (_objMefyeneyOved.GetMeafyen(60).IntValue > 0)
                         {
                             fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                             fNochechtLeTashlum = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime') AND (MISPAR_SIDUR=99207 or MISPAR_SIDUR=99007 or MISPAR_SIDUR=99011 )"));
                             fSumDakotRechiv = fSumDakotRechiv - fNochechtLeTashlum;
                         }
                         else
                         {
                             if (_oGeneralData.CheckMutamut())
                             {
                                 fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                             }
                             else
                             {
                                 oSidur.CalcRechiv22();

                                 fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KamutGmulChisachon.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                             }
                         }

                         fDakotNehiga = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                         if (fSumDakotRechiv >= _objParameters.iMinDakotGmulLeloNehiga && (fDakotNehiga == 0 || _oGeneralData.objPirteyOved.iMutamut == 1 || _oGeneralData.objPirteyOved.iMutamut == 3))
                         {
                             fSumDakotRechiv = 1;
                         }
                         else if (fSumDakotRechiv >= _objParameters.iMinDakotGmulImNehiga && fDakotNehiga != 0)
                         {
                             fSumDakotRechiv = 1;
                         }
                         else { fSumDakotRechiv = 0; }
                         if (_objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                         {
                             fSumDakotRechiv = float.Parse(Math.Round(fSumDakotRechiv * clCalcData.fMekademNipuach, 2).ToString());
                         }
                         addRowToTable(clGeneral.enRechivim.KamutGmulChisachon.GetHashCode(), fSumDakotRechiv);
                     }
                 }
             //}
              }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.KamutGmulChisachon.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
         }

         private void CalcRechiv23()
         {
             float fSumDakotRechiv;
             try
             {
                 if (_oGeneralData.objPirteyOved.iDirug != 85 || _oGeneralData.objPirteyOved.iDarga != 30)
                 {
                     //יש לבדוק האם הרכיב בתוקף על ידי שליפת פרמטרים כללים: מתאריך תוקף = שליפת פרמטר (קוד פרמטר = 62) עד תאריך = שליפת פרמטר (קוד פרמטר = 63). אם תאריך יום העבודה נמצא בין תאריכי התוקף אזי להמשיך בחישוב. 
                     if (_objParameters.dDateFirstTosefetSikun <= _Taarich && _objParameters.dDateLastTosefetSikun >= _Taarich)
                     {
                         oSidur.CalcRechiv23();

                         fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotSikun.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                         addRowToTable(clGeneral.enRechivim.DakotSikun.GetHashCode(), fSumDakotRechiv);
                     }
                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotSikun.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                 throw (ex);
             }
         }

         private void CalcRechiv26()
         {
             float fSumDakotRechiv, fNuchehutLepremia, fTosefetRetzifut, fDakotKisuyTor, fSumDakotSikun;
             float fTosefetGil, fDakotHagdara, fDakotHistaglut, fSachNesiot, fDakotLepremia;
             fTosefetGil = 0;
             int iSugYom;
             try
             {
                 if (_dtYemeyAvodaOved.Select("Lo_letashlum=0  and TACHOGRAF=1 and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur ASC").Length == 0)
                 {
                     iSugYom = clCalcData.iSugYom;
                     if (clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, iSugYom, _Taarich))
                     {
                         if (_oGeneralData.objPirteyOved.iMutamut != 1 && _oGeneralData.objPirteyOved.iMutamut != 3)
                         {
                             if (_oGeneralData.objPirteyOved.iDirug != 85 || _oGeneralData.objPirteyOved.iDarga != 30)
                             {
                                 oSidur.CalcRechiv26();

                                 fNuchehutLepremia = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                 if (fNuchehutLepremia > 0)
                                 {
                                     fTosefetRetzifut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                     fDakotLepremia = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotLepremia.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                     fNuchehutLepremia += fTosefetRetzifut;
                                     ////ג.	אם תוספת רציפות 1-1 (רכיב 96) > 60 וגם דקות 1-1 (רכיב 52) >= תוספת רציפות 1-1 (רכיב 96) אזי: [דקות 1-1 לפרמיה]  = דקות 1-1 (רכיב 52) + 60 פחות -  תוספת רציפות 1-1 (רכיב 96).  אחרת, [דקות 1-1 לפרמיה]  = דקות 1-1 (רכיב 52) 
                                     //if (fTosefetRetzifut > 60 && fZmanRetzifut >= fTosefetRetzifut)
                                     //{
                                     //    fDakotLepremia = fZmanRetzifut + 60 - fTosefetRetzifut;
                                     //}
                                     //else { fDakotLepremia = fZmanRetzifut; }

                                     fTosefetGil = 0;
                                     fDakotHagdara = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotHagdara.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                     fDakotHistaglut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotHistaglut.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                     fSachNesiot = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachNesiot.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                     fDakotKisuyTor = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotKisuiTor.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                     fSumDakotSikun = (clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotSikun.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"))) / float.Parse("0.75");

                                     fSumDakotRechiv = (fDakotHagdara / float.Parse("0.75")) + fDakotHistaglut + fDakotKisuyTor + fDakotLepremia + fTosefetRetzifut + fSumDakotSikun + (fSachNesiot * _objParameters.fElementZar) + (fTosefetGil - fNuchehutLepremia);

                                     addRowToTable(clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(), fSumDakotRechiv);
                                 }
                             }
                         }
                     }
                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                 throw (ex);
             }
         }

         private void CalcRechiv27()
         {
             float fSumDakotRechiv, fMichsaMechushevet, fDakotNochehut;
             try
             {
                 if (!(_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30))
                 {
                     fMichsaMechushevet = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                     fDakotNochehut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                     if (fMichsaMechushevet > 0)
                     {
                         if (fDakotNochehut >= fMichsaMechushevet)
                         {
                             fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaYomit.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                             fSumDakotRechiv += clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaVisa.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                             fSumDakotRechiv = fSumDakotRechiv / fDakotNochehut * fMichsaMechushevet;
                         }
                         else
                         {
                             fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaYomit.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                             fSumDakotRechiv += clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaVisa.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                         }

                         addRowToTable(clGeneral.enRechivim.DakotPremiaBetochMichsa.GetHashCode(), fSumDakotRechiv);
                     }
                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotPremiaBetochMichsa.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                 throw (ex);
             }
         }

         private void CalcRechiv28()
         {
             float fSumDakotRechiv, fSachKmVisa, fErechSidur;
             try
             {
                 if (_dtYemeyAvodaOved.Select("Lo_letashlum=0  and TACHOGRAF=1 and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur ASC").Length == 0)
                 {
                     if (_oGeneralData.objPirteyOved.iMutamut != 4 && _oGeneralData.objPirteyOved.iMutamut != 5)
                     {
                         if (_oGeneralData.objPirteyOved.iDirug != 85 || _oGeneralData.objPirteyOved.iDarga != 30)
                         {
                             if (!clCalcData.CheckErevChag(clCalcData.iSugYom) && !clCalcData.CheckYomShishi(clCalcData.iSugYom) && !clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, clCalcData.iSugYom, _Taarich))
                            {
                                 oSidur.CalcRechiv28();

                                 fErechSidur = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaVisa.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                                 fSachKmVisa = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                                 fSumDakotRechiv = float.Parse(((((fErechSidur / 1.2) +fSachKmVisa)/50)*60  * 0.33).ToString());

                                 addRowToTable(clGeneral.enRechivim.DakotPremiaVisa.GetHashCode(), fSumDakotRechiv);
                             }
                         }
                     }
                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotPremiaVisa.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                 throw (ex);
             }
         }

         private void CalcRechiv29()
         {
             float fSumDakotRechiv, fSachKmVisa, fErechSidur;
             try
             {
                 if (_dtYemeyAvodaOved.Select("Lo_letashlum=0  and TACHOGRAF=1 and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur ASC").Length == 0)
                 {
                     if (clCalcData.CheckErevChag(clCalcData.iSugYom) || clCalcData.CheckYomShishi(clCalcData.iSugYom) || clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, clCalcData.iSugYom, _Taarich))
                     {
                         if (_oGeneralData.objPirteyOved.iMutamut != 4 && _oGeneralData.objPirteyOved.iMutamut != 5)
                         {
                             if (_oGeneralData.objPirteyOved.iDirug != 85 || _oGeneralData.objPirteyOved.iDarga != 30)
                             {

                                 oSidur.CalcRechiv29();

                                 fErechSidur = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaVisaShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                                 fSachKmVisa = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                                 fSumDakotRechiv = float.Parse(((((fErechSidur / 1.2) + fSachKmVisa) / 50) *60* 0.33).ToString());


                                 addRowToTable(clGeneral.enRechivim.DakotPremiaVisaShabat.GetHashCode(), fSumDakotRechiv);
                             }

                         }
                     }
                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotPremiaVisaShabat.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                 throw (ex);
             }
         }

         private void CalcRechiv30()
         {
             float fSumDakotRechiv, fMichsaYomit, fNuchehutLepremia, fTosefetRetzifut, fSumDakotSikun;
             float fTosefetGil, fDakotHagdara, fDakotHistaglut, fSachNesiot, fDakotLepremia, fDakotKisuyTor;
             Boolean bShish,bErevChag;
             DateTime dShatHatchalaYom,dShatHatchalaShabaton;
             DataRow[] drYom;
             string sMispareySidur;
             fTosefetGil = 0;
             fDakotHagdara = 0;
             fTosefetRetzifut = 0;
             fDakotHistaglut = 0;
             fDakotKisuyTor = 0;
             fSachNesiot = 0;
             fDakotLepremia = 0;
             fSumDakotSikun = 0;
             try
             {
                 if (_dtYemeyAvodaOved.Select("Lo_letashlum=0  and TACHOGRAF=1  and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')", "").Length == 0)
                 {
                     fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                     if (fMichsaYomit > 0)
                     {
                         if (_oGeneralData.objPirteyOved.iMutamut != 1 && _oGeneralData.objPirteyOved.iMutamut != 3)
                         {
                             if (_oGeneralData.objPirteyOved.iDirug != 85 || _oGeneralData.objPirteyOved.iDarga != 30)
                             {
                                 drYom = _dtYemeyAvodaOved.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur ASC");

                                 if (drYom.Length > 0)
                                 {
                                     dShatHatchalaYom = DateTime.Parse(drYom[0]["shat_hatchala_sidur"].ToString());
                                     dShatHatchalaShabaton = _objParameters.dKnisatShabat;

                                     oSidur.CalcRechiv30(out sMispareySidur);
                                     fNuchehutLepremia = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaYomit.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                     if (fNuchehutLepremia > 0)
                                     {
                                         if (sMispareySidur.Length > 0)
                                             sMispareySidur = sMispareySidur.Substring(1, sMispareySidur.Length - 1);

                                         fTosefetRetzifut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));// clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR IN (" + sMispareySidur + ") AND KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                                         fNuchehutLepremia += fTosefetRetzifut;

                                         bShish = clCalcData.CheckYomShishi(clCalcData.iSugYom);
                                         bErevChag = clCalcData.CheckErevChag(clCalcData.iSugYom);


                                         if (fMichsaYomit > 0 && ((!bShish && !bErevChag) || ((bShish || bErevChag) && dShatHatchalaYom < dShatHatchalaShabaton)))
                                         {
                                             if (_oGeneralData.objPirteyOved.iGil == clGeneral.enKodGil.enKashish.GetHashCode())
                                             {
                                                 fTosefetGil = (fNuchehutLepremia * 30) / _objParameters.iChalukaTosefetGilKashish;
                                             }
                                             else if (_oGeneralData.objPirteyOved.iGil == clGeneral.enKodGil.enKshishon.GetHashCode())
                                             {
                                                 fTosefetGil = (fNuchehutLepremia * 30) / _objParameters.iChalukaTosefetGilKshishon;

                                             }
                                         }
                                         if (sMispareySidur.Length > 0)
                                         {
                                             fDakotHagdara = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR IN (" + sMispareySidur + ") AND KOD_RECHIV=" + clGeneral.enRechivim.DakotHagdara.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                             fDakotHistaglut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR IN (" + sMispareySidur + ") AND KOD_RECHIV=" + clGeneral.enRechivim.DakotHistaglut.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                             fSachNesiot = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR IN (" + sMispareySidur + ") AND KOD_RECHIV=" + clGeneral.enRechivim.SachNesiot.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                             fDakotKisuyTor = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR IN (" + sMispareySidur + ") AND KOD_RECHIV=" + clGeneral.enRechivim.DakotKisuiTor.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                             fDakotLepremia = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR IN (" + sMispareySidur + ") AND KOD_RECHIV=" + clGeneral.enRechivim.SachDakotLepremia.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                             fSumDakotSikun = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR IN (" + sMispareySidur + ") AND KOD_RECHIV=" + clGeneral.enRechivim.DakotSikun.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')")) / float.Parse("0.75");
                                         }
                                         fSumDakotRechiv = (fDakotHagdara / float.Parse("0.75")) + fDakotHistaglut + fDakotKisuyTor + fDakotLepremia + fTosefetRetzifut + fSumDakotSikun + (2 + (fSachNesiot - 1) * _objParameters.fElementZar) + (fTosefetGil - fNuchehutLepremia);

                                         addRowToTable(clGeneral.enRechivim.DakotPremiaYomit.GetHashCode(), fSumDakotRechiv);
                                     }
                                 }
                             }
                         }
                     }
                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotPremiaYomit.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                 throw (ex);
             }
         }

         private void CalcRechiv32()
         {
             float fSumDakotRechiv, fMichsaYomit126, fDakotNochehut1, fAruchatZaharim88;
             try{
                 //החישוב היומי רלוונטי רק אם העובד הוא עובד יומי [שליפת מאפיין ביצוע (קוד מאפיין = 56, מ.א., תאריך)] ערך = 51 או 61.

                 if (_objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || _objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                 {
                     if (_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30 && clCalcData.iSugYom == enSugYom.Bchirot.GetHashCode())
                     {
                         fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                     }
                     else
                       {
                          
                         fMichsaYomit126 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                         fDakotNochehut1 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                         fAruchatZaharim88 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                         fSumDakotRechiv = 0;
                         if (fMichsaYomit126 > 0 && fDakotNochehut1 > 0)
                         {
                             fSumDakotRechiv = Math.Min(fMichsaYomit126, fDakotNochehut1 - fAruchatZaharim88);  
                         }
                     }

                     if (fSumDakotRechiv > 0 )
                     {
                         addRowToTable(clGeneral.enRechivim.DakotRegilot.GetHashCode(), fSumDakotRechiv);
                     }
                   }
              }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotRegilot.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
         }

         private void CalcRechiv35()
        {
            float fSumDakotRechiv;
            try
            {
                if (!(_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30 && clCalcData.iSugYom == enSugYom.Bchirot.GetHashCode()))
                {
                    oSidur.CalcRechiv35();
                    fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    if (fSumDakotRechiv > 0)
                    {
                        fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanHashlama.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.HashlamaBenahagut.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        addRowToTable(clGeneral.enRechivim.DakotNehigaShabat.GetHashCode(), fSumDakotRechiv);

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotNehigaShabat.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

         private void CalcRechiv36()
        {
            float fSumDakotRechiv, fDakotNehigaShabat;
            try
            {
                if (!(_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30 && clCalcData.iSugYom == enSugYom.Bchirot.GetHashCode()))
                {
                    oSidur.CalcRechiv36();

                    fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    if (fSumDakotRechiv > 0)
                    {
                        fDakotNehigaShabat = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        if (fDakotNehigaShabat == 0)
                        {
                            fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanHashlama.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        }
                        fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.HashlamaBenihulTnua.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        addRowToTable(clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode(), fSumDakotRechiv);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

         internal float GetRechiv36OutMichsa()
         {
             float fSumDakotRechiv;
             
             fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "OUT_MICHSA=0 AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode().ToString() ));
             fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanHashlama.GetHashCode().ToString() ));
             fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode().ToString()));
             return fSumDakotRechiv;
         }

         private void CalcRechiv37()
        {
            float fSumDakotRechiv, fDakotNehigaShabat, fDakotNihulShabat;
               try
            {
                if (!(_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30 && clCalcData.iSugYom == enSugYom.Bchirot.GetHashCode()))
                {
                    oSidur.CalcRechiv37();
                    fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    if (fSumDakotRechiv > 0)
                    {
                        fDakotNehigaShabat = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        fDakotNihulShabat = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        if (fDakotNehigaShabat == 0 && fDakotNihulShabat == 0)
                            fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanHashlama.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                        fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.HashlamaBetafkid.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanGrirot.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                         fSumDakotRechiv = fSumDakotRechiv - clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                        addRowToTable(clGeneral.enRechivim.DakotTafkidShabat.GetHashCode(), fSumDakotRechiv);
                    }
                }
                    }
               catch (Exception ex)
               {
                   clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotTafkidShabat.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                   throw (ex);
               }
        }


         internal float GetRechiv37OutMichsa()
        {
            float fSumDakotRechiv;

            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "OUT_MICHSA=0 AND  KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidShabat.GetHashCode().ToString() ));
            fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanHashlama.GetHashCode().ToString()));
            fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode().ToString() ));
            fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanHalbasha.GetHashCode().ToString()));
            fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanGrirot.GetHashCode().ToString()));
            fSumDakotRechiv = fSumDakotRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanNesia.GetHashCode().ToString() ));
            //fSumDakotRechiv = fSumDakotRechiv - clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzAruchatBoker.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            
             fSumDakotRechiv = fSumDakotRechiv - clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode().ToString()));
           
            return fSumDakotRechiv;
        }

        private void CalcRechiv38()
        {
            //הרכיב יחושב רק עבור עובדים הזכאים לאש"ל [מאפיין ביצוע (קוד=50) עם ערך 1] 
            //אם קיים לפחות סידור אחד ביום עבורו ערך הרכיב = 1 אזי ערך הרכיב ברמת היום = 1. אחרת אין לפתוח רשומה לרכיב ברמת היום
            try
            {
                if (!(_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30 ))
                {
                    if (_objMefyeneyOved.GetMeafyen(50).Value == "1")
                    {
                        oSidur.CalcRechiv38();

                        if (_dsChishuv.Tables["CHISHUV_SIDUR"].Select("ERECH_RECHIV=1 AND KOD_RECHIV=" + clGeneral.enRechivim.SachEshelBoker.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')").Length > 0)
                        {
                            addRowToTable(clGeneral.enRechivim.SachEshelBoker.GetHashCode(), 1);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachEshelBoker.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv40()
        {
            try
            {
                if (_oGeneralData.objPirteyOved.iDirug != 85 || _oGeneralData.objPirteyOved.iDarga != 30)
                {
                    //הרכיב יחושב רק עבור עובדים הזכאים לאש"ל [מאפיין ביצוע (קוד=50) עם ערך 1] 
                    if (_objMefyeneyOved.GetMeafyen(50).Value == "1")
                    {
                        oSidur.CalcRechiv40();

                        if (_dsChishuv.Tables["CHISHUV_SIDUR"].Select("ERECH_RECHIV=1 AND KOD_RECHIV=" + clGeneral.enRechivim.SachEshelErev.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')").Length > 0)
                        {
                            addRowToTable(clGeneral.enRechivim.SachEshelErev.GetHashCode(), 1);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachEshelErev.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv42()
        {
            float fDakotNochehut1;
            try
            {
                //הרכיב יחושב רק עבור עובדים הזכאים לאש"ל [מאפיין ביצוע (קוד=50) עם ערך 1] 
                if (!(_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30))
                {
                    if (_objMefyeneyOved.GetMeafyen(50).Value == "1")
                    {
                        oSidur.CalcRechiv42();
                        fDakotNochehut1 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                        if (fDakotNochehut1 >= _objParameters.iMinTimeSidurEshelTzaharayim)
                        {
                            if (_dsChishuv.Tables["CHISHUV_SIDUR"].Select("ERECH_RECHIV=1 AND KOD_RECHIV=" + clGeneral.enRechivim.SachEshelTzaharayim.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')").Length > 0)
                            {
                                addRowToTable(clGeneral.enRechivim.SachEshelTzaharayim.GetHashCode(), 1);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachEshelTzaharayim.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv44()
        {
            //כמות גמול חסכון נוספות (רכיב 44) 
            float fSumDakotRechiv, fZmanHalbasha, fMichsaYomit126, fZmanNesiot, fZmanGrirot;

            //א.	אם העובד בעל מאפיין ביצוע [שליפת מאפיין ביצוע (קוד מאפיין=60)] עם ערך = 2 אזי:
            //אין משמעות לרכיב ברמת ביום.
            //אחרת
            //X = סכום ערך הרכיב עבור כל הסידורים ביום + תוספת זמן הלבשה (רכיב 94) + תוספת זמן נסיעות (רכיב 95) + זמן גרירות (רכיב 128).
            //ב.	אם X  > ממכסה יומית מחושבת (רכיב 126)  אזי : ערך הרכיב  = X  פחות מכסה יומית מחושבת (רכיב 126)   אחרת : ערך הרכיב = 0.
            try{
                if (_oGeneralData.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.Sachir12.GetHashCode() || _oGeneralData.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.SachirKavua.GetHashCode() || (_oGeneralData.objPirteyOved.iKodMaamdRashi == clGeneral.enMaamad.Friends.GetHashCode()))
                {
                    if (_oGeneralData.objMeafyeneyOved.GetMeafyen(60).IntValue == 2)
                    {
                        if (_oGeneralData.objPirteyOved.iDirug != 85 || _oGeneralData.objPirteyOved.iDarga != 30)
                        {


                            oSidur.CalcRechiv44();
                            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KamutGmulChisachonNosafot.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            fZmanHalbasha = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanHalbasha.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            fZmanNesiot = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanNesia.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            fZmanGrirot = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanGrirot.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            fSumDakotRechiv = fSumDakotRechiv + fZmanHalbasha + fZmanNesiot + fZmanGrirot;
                            fMichsaYomit126 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            if (fSumDakotRechiv > fMichsaYomit126)
                            {
                                fSumDakotRechiv = fSumDakotRechiv - fMichsaYomit126;
                                addRowToTable(clGeneral.enRechivim.KamutGmulChisachonNosafot.GetHashCode(), fSumDakotRechiv);
                            }
                            else
                            {
                                addRowToTable(clGeneral.enRechivim.KamutGmulChisachonNosafot.GetHashCode(), 0);

                            }
                        }
                    }
                }
             }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.KamutGmulChisachonNosafot.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv39_41_43()
        {
            float fSumDakotRechiv;
            try{
            oSidur.CalcRechiv39_41_43();
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            if (fSumDakotRechiv > 0)
            {
                addRowToTable(clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), fSumDakotRechiv);
            }
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            if (fSumDakotRechiv > 0)
            {
                addRowToTable(clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), fSumDakotRechiv);
            }
            fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            if (fSumDakotRechiv > 0)
            {
                addRowToTable(clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), fSumDakotRechiv);
            }
             }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        //private void CalcRechiv45()
        //{
        //    float fSumDakotRechiv;
        //    double dSumDakotRechiv;
        //    try
        //    {
        //        if (_oGeneralData.objPirteyOved.iDirug != 85 || _oGeneralData.objPirteyOved.iDarga != 30)
        //        {
        //            if (_oGeneralData.objMeafyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
        //            {
        //                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KamutGmulChisachon.GetHashCode().ToString()));
        //               dSumDakotRechiv = Math.Round((fSumDakotRechiv * clCalcGeneral.CalcMekademNipuach(_Taarich, _iMisparIshi)));
        //                 addRowToTable(clGeneral.enRechivim.KamutGmulChisachonMeyuchad.GetHashCode(), float.Parse(dSumDakotRechiv.ToString()));
                        
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.KamutGmulChisachonMeyuchad.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
        //        throw (ex);
        //    }
        //}

        private void CalcRechiv47()
        {
            DataRow[] rowLina;
            try{
                if (!(_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30))
                {
                    //אם 1= TB_Yamey_Avoda_Ovdim.Lina אזי ערך הרכיב = 1 
                    rowLina = _dtYemeyAvodaOved.Select("Lo_letashlum=0 and Lina=1 and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')");
                    if (rowLina.Length > 0)
                    {

                        addRowToTable(clGeneral.enRechivim.SachLina.GetHashCode(), 1);

                    }
                }
             }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachLina.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv48()
        {
            DataRow[] rowLina;
            try
            {
                //2= TB_Yamey_Avoda_Ovdim.Lina אזי ערך הרכיב = 6  
                if (!(_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30))
                {
                    rowLina = _dtYemeyAvodaOved.Select("Lo_letashlum=0 and Lina=2 and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')");
                    if (rowLina.Length > 0)
                    {

                        addRowToTable(clGeneral.enRechivim.SachLinaKfula.GetHashCode(), 6);

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachLinaKfula.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv49()
        {
            DataRow[] rowPitzul;
            DataRow[] rowPitzulKaful;
            try
            {
                //אם קיים לפחות סידור אחד ביום עבורו TB_Sidurim_Ovedim.Pitzul_hafsaka = 1
                //וגם לא קיימת רשומה ליום עבור סה"כ פיצול כפול (רכיב 50) אזי
                //ערך הרכיב = 1

                rowPitzul = _dtYemeyAvodaOved.Select("Lo_letashlum=0 and Pitzul_hafsaka = 1 and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')");
                rowPitzulKaful = _dsChishuv.Tables["CHISHUV_YOM"].Select("taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime') and KOD_RECHIV=" + clGeneral.enRechivim.SachPitzulKaful.GetHashCode().ToString());

                if ((rowPitzul.Length > 0) && (rowPitzulKaful.Length == 0))
                {
                    addRowToTable(clGeneral.enRechivim.SachPitzul.GetHashCode(), 1);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachPitzul.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv50()
        {
            DataRow[] rowPitzul;
            try
            {
                //אזי הרכיב = 1 TB_Sidurim_Ovedim.Pitzul_hafsaka = 2 אם
                if (!(_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30))
                {
                    rowPitzul = _dtYemeyAvodaOved.Select("Lo_letashlum=0 and Pitzul_hafsaka = 2 and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')");
                    if (rowPitzul.Length > 0)
                    {

                        addRowToTable(clGeneral.enRechivim.SachPitzulKaful.GetHashCode(), 1);

                    }
                }
             }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachPitzulKaful.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv52()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv52();
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotLepremia.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                addRowToTable(clGeneral.enRechivim.SachDakotLepremia.GetHashCode(), fSumDakotRechiv);
          
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachDakotLepremia.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv53()
        {
            float fSumDakotRechiv;
            try
            {
                if (!(_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30))
                {

                    oSidur.CalcRechiv53();
                    fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    addRowToTable(clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode(), fSumDakotRechiv);

                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv54()
        {
            float fSumDakotRechiv;
            try{
                if (!(_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30))
                {
                    if ((_oGeneralData.objPirteyOved.iKodMaamdMishni != clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode()) ||
                         (_oGeneralData.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode() &&
                         (_oGeneralData.objPirteyOved.iIsuk == 122 || _oGeneralData.objPirteyOved.iIsuk == 123 || _oGeneralData.objPirteyOved.iIsuk == 124 || _oGeneralData.objPirteyOved.iIsuk == 127)))
                    {
                        oSidur.CalcRechiv54();
                        fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanLailaEgged.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        fSumDakotRechiv += clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutLaylaEgged.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        addRowToTable(clGeneral.enRechivim.ZmanLailaEgged.GetHashCode(), fSumDakotRechiv);
                    }
                }
             }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.ZmanLailaEgged.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
       }

        private void CalcRechiv55()
        {
            float fSumDakotRechiv, fTempX, fTempY, fTempZ, fZmanLilaSidureyBoker,fMichsaYomitMechushevet;
            try
            {
                if ((_oGeneralData.objPirteyOved.iKodMaamdMishni != clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode()) ||
                     (_oGeneralData.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode() &&
                     (_oGeneralData.objPirteyOved.iIsuk == 122 || _oGeneralData.objPirteyOved.iIsuk == 123 || _oGeneralData.objPirteyOved.iIsuk == 124 || _oGeneralData.objPirteyOved.iIsuk == 127)))
                {
                    oSidur.CalcRechiv55();
                    fTempX = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanLailaChok.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                      
                    fZmanLilaSidureyBoker = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanLilaSidureyBoker.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                     
                    if (_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30)
                      {
                          fSumDakotRechiv = fTempX;
                      }
                      else{
                          fTempX += clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutLaylaChok.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    
                          fTempY = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot125.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                          fTempY = fTempY + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Shaot25.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                          fTempZ = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot150.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                          fTempZ = fTempZ + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Shaot50.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    
                          if (_oGeneralData.objPirteyOved.iKodMaamdRashi == clGeneral.enMaamad.Salarieds.GetHashCode() && CheckSugMishmeret() == clGeneral.enSugMishmeret.Liyla.GetHashCode() &&
                           (_oGeneralData.objPirteyOved.iIsuk == 122 || _oGeneralData.objPirteyOved.iIsuk == 123 || _oGeneralData.objPirteyOved.iIsuk == 124 || _oGeneralData.objPirteyOved.iIsuk == 127))
                          {
                              fSumDakotRechiv = fTempX;
                          }
                          else
                          {
                              if (fTempX > 0)
                              {
                                  fMichsaYomitMechushevet = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                                  if (fMichsaYomitMechushevet  == 0)
                                  {
                                      fSumDakotRechiv = fTempX;
                                  }
                                  else  if (fTempZ >= fTempX)
                                  {
                                      fSumDakotRechiv = fTempX * float.Parse("1.5");
                                  }
                                  else
                                  {
                                      fTempX = fTempX - fTempZ;

                                      fSumDakotRechiv = fTempZ * float.Parse("1.5");

                                      if (fTempY > fTempX)
                                      {

                                          fSumDakotRechiv = fSumDakotRechiv + (fTempX * float.Parse("1.25"));
                                      }
                                      else
                                      {
                                          fSumDakotRechiv = fSumDakotRechiv + (fTempY * float.Parse("1.25")) + (fTempX - fTempY);
                                      }
                                  }
                              }
                              else
                              {
                                  fSumDakotRechiv = 0;
                              }

                          }
                      }
                    

                    fSumDakotRechiv = fSumDakotRechiv + fZmanLilaSidureyBoker + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanLailaEgged.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                   
                    addRowToTable(clGeneral.enRechivim.ZmanLailaChok.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.ZmanLailaChok.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv56()
        {
            float fErechRechiv;

            try
            {
                if (_oGeneralData.objPirteyOved.iKodMaamdMishni!=clGeneral.enKodMaamad.OvedChadshKavua.GetHashCode())
                {
                fErechRechiv = CalcHeadruyot(clGeneral.enRechivim.YomEvel.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomEvel.GetHashCode(), fErechRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YomEvel.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv57()
        {
            float fErechRechiv, fMichsaYomitMechushevet, fTempX,fMichsaYomit;
            int iCount;
            try
            {
                fTempX = 0;
                fErechRechiv = 0;
                fMichsaYomitMechushevet = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                fErechRechiv = oSidur.CalcYemeyHeadrut(clGeneral.enRechivim.YomHadracha.GetHashCode(), out iCount, fMichsaYomitMechushevet);

                 if (_dsChishuv.Tables["CHISHUV_SIDUR"].Select("ERECH_RECHIV>0  AND MISPAR_SIDUR=99703 AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')").Length > 0)
                 {
                     fErechRechiv = 1;
                 }
                 else
                 {
                     fTempX = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR IN(99207, 99011,99007) AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                     if (_objParameters.iDakotKabalatYomHadracha>0 && fTempX >= _objParameters.iDakotKabalatYomHadracha)
                     {
                         fErechRechiv = 1;
                     }
                     else
                     {
                         if (fMichsaYomitMechushevet > 0)
                         {
                             fMichsaYomit = fMichsaYomitMechushevet;
                         }
                         else
                         {
                             if (_oGeneralData.objPirteyOved.iGil == clGeneral.enKodGil.enTzair.GetHashCode())
                             {
                                 fMichsaYomit = _oGeneralData.objParameters.iGmulNosafotTzair;
                             }
                             else if (_oGeneralData.objPirteyOved.iGil == clGeneral.enKodGil.enKshishon.GetHashCode())
                             {
                                 fMichsaYomit = _oGeneralData.objParameters.iGmulNosafotKshishon;
                             }
                             else
                             {
                                 fMichsaYomit = _oGeneralData.objParameters.iGmulNosafotKashish;
                             }
                         }
                         fErechRechiv = Math.Min(1, (fTempX / fMichsaYomit));
                     }

                 }
                addRowToTable(clGeneral.enRechivim.YomHadracha.GetHashCode(), fErechRechiv);
             
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YomHadracha.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv59()
        {
            float fErechRechiv, fMichsaYomit;

            try
            {
                fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                if (fMichsaYomit > 0)
                {
                    if (_oGeneralData.objPirteyOved.iIsuk != 122 && _oGeneralData.objPirteyOved.iIsuk != 123 && _oGeneralData.objPirteyOved.iIsuk != 124 && _oGeneralData.objPirteyOved.iIsuk != 127)
                    {
                        if (clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is null and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')").Length > 0)
                        {
                            fErechRechiv = 1;
                            addRowToTable(clGeneral.enRechivim.YomLeloDivuach.GetHashCode(), fErechRechiv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YomLeloDivuach.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv60()
        {
            float fErechRechiv, fMichsaYomit, fTempX, fTempW,fDakotNochehut;
            int iCount;
            DataRow[] drRechiv;
            string sSidurimMeyuchadim="";
            try
            {
                fTempX=0;
                 fErechRechiv = 0;
                fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                if (fMichsaYomit > 0)
                {
                    fErechRechiv = oSidur.CalcYemeyHeadrut(clGeneral.enRechivim.YomMachla.GetHashCode(), out iCount, fMichsaYomit);
                    if (iCount > 0)
                    {
                        if (iCount == 1 && fErechRechiv > 0)
                        {
                            fTempX = 1;
                        }
                        else if (fErechRechiv > 0)
                        {
                            sSidurimMeyuchadim = oSidur.GetSidurimMeyuchRechiv(clGeneral.enRechivim.YomMachla.GetHashCode());
                            fDakotNochehut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR NOT IN(" + sSidurimMeyuchadim  + ") AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                            drRechiv = _dsChishuv.Tables["CHISHUV_YOM"].Select("ERECH_RECHIV>0 AND KOD_RECHIV=" + clGeneral.enRechivim.YomMachla.GetHashCode().ToString() + " and taarich<Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')", "taarich desc");
                            if (drRechiv.Length > 0)
                            {
                                if (DateTime.Parse(drRechiv[0]["taarich"].ToString()) == _Taarich.AddDays(-1) && float.Parse(drRechiv[0]["erech_rechiv"].ToString()) >0)
                                {
                                    fTempX = 1;
                                }
                                else if (fDakotNochehut <= fMichsaYomit)
                                {
                                    fTempX = fErechRechiv / fMichsaYomit;
                                }
                            }
                            else if (fDakotNochehut <= fMichsaYomit)
                            {
                                fTempX = fErechRechiv / fMichsaYomit;
                            }
                        }

                        if (fTempX > 1)
                        { fTempX = 1; }

                        fTempW = 1;

                        if (_objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                        {
                            fTempW = clCalcData.fMekademNipuach;
                        }

                        fErechRechiv = float.Parse(Math.Round(fTempW * fTempX, 2).ToString());
                        addRowToTable(clGeneral.enRechivim.YomMachla.GetHashCode(), fErechRechiv);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YomMachla.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv61()
        {
            float fErechRechiv;

            try
            {

                fErechRechiv = CalcHeadruyot(clGeneral.enRechivim.YomMachalaBoded.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomMachalaBoded.GetHashCode(), fErechRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YomMachalaBoded.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv62()
        {
            float fErechRechiv, fMichsaYomit, fTempW, fTempX;
            int iCount;
            try
            {
                fTempX = 0;
                fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                if (fMichsaYomit > 0)
                {
                    fErechRechiv = oSidur.CalcRechiv62(out iCount);
                    if (fErechRechiv > 0)
                    {
                       fTempX = fErechRechiv;
                      
                        fTempW = 1;

                        if (_objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                        {
                            fTempW = clCalcData.fMekademNipuach;
                        }

                        fErechRechiv = float.Parse(Math.Round(fTempW * fTempX, 2).ToString());
                        addRowToTable(clGeneral.enRechivim.YomMiluim.GetHashCode(), fErechRechiv);
                    }
                }
                
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YomMiluim.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv63()
        {
            float fErechRechiv;
            int iCount;
            try
            {

                fErechRechiv = oSidur.CalcRechiv63(out iCount);
                if (iCount > 0 && fErechRechiv ==1)
                {
                    fErechRechiv = 1;
                    addRowToTable(clGeneral.enRechivim.YomAvodaBechul.GetHashCode(), fErechRechiv);
                }
              
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YomAvodaBechul.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv64()
        {
            float fErechRechiv;

            try
            {
                //א.	אם TB_Yamey_Avoda_Ovdim.Hashlama_Leyom = 1 אין לפתוח רשומה לרכיב ליום עבודה זה.
                if (clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and Hashlama_Leyom=1 and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')").Length == 0)
                {
                fErechRechiv = CalcHeadruyot(clGeneral.enRechivim.YomTeuna.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomTeuna.GetHashCode(), fErechRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YomTeuna.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv65()
        {
            float fErechRechiv;

            try
            {

                fErechRechiv = CalcHeadruyot(clGeneral.enRechivim.YomShmiratHerayon.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomShmiratHerayon.GetHashCode(), fErechRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YomShmiratHerayon.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv66()
        {
            float fErechRechiv, fMichsaYomit, fDakotNochehut, fMichsatYomitLeloMutamut, fKizuzMeheadrut;
            DataRow[] rowSidur;
            string sRechivim;
            try
            {
                fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fKizuzMeheadrut = 0;      
                if (!(_oGeneralData.GetSugYomLemichsa(_iMisparIshi, _Taarich) == enSugYom.ErevYomHatsmaut.GetHashCode()
                    && (_objMefyeneyOved.GetMeafyen(63).Value != "" || _objMefyeneyOved.GetMeafyen(63).Value != "0") && _objMefyeneyOved.GetMeafyen(33).IntValue == 1))
                {
                    rowSidur = _dtYemeyAvodaOved.Select("Lo_letashlum=0 and mispar_sidur=99801 and Hashlama_Leyom=1 and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')");
                    if (rowSidur.Length == 0)
                    {
                        rowSidur = _dtYemeyAvodaOved.Select("Lo_letashlum=0 and mispar_sidur=99841 and Hashlama_Leyom=1 and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')");
                        if (!(rowSidur.Length > 0 && fMichsaYomit > 0 && _objMefyeneyOved.GetMeafyen(33).IntValue == 1))
                        {
                            fErechRechiv = 0;
                            if (fMichsaYomit > 0)
                            {
                                rowSidur = _dtYemeyAvodaOved.Select("Lo_letashlum=0 and sidur_misug_headrut=1  and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')");
                                if (rowSidur.Length > 0)
                                {
                                    fErechRechiv = 1;
                                }
                            }
                            fDakotNochehut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            if (fMichsaYomit > 0 && fDakotNochehut == 0 && _dtYemeyAvodaOved.Select("Lo_letashlum=0 and mispar_sidur is null and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')").Length > 0 && _objMefyeneyOved.GetMeafyen(33).IntValue == 1)
                            { fErechRechiv = 1; }

                            if (fDakotNochehut < fMichsaYomit && fDakotNochehut > 0 && fMichsaYomit > 0 && _objMefyeneyOved.GetMeafyen(33).IntValue == 1)
                            {
                                fErechRechiv = (fMichsaYomit - fDakotNochehut) / fMichsaYomit;
                            }

                            if (_oGeneralData.objMeafyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                            {
                                fErechRechiv = fErechRechiv * clCalcData.fMekademNipuach;
                            }

                            if (_oGeneralData.objPirteyOved.iMutamBitachon > 0 && _oGeneralData.objPirteyOved.iKodMaamdRashi==clGeneral.enMaamad.Salarieds.GetHashCode())
                            {
                                fMichsatYomitLeloMutamut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_EZER)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')")); ;
                                rowSidur = _dtYemeyAvodaOved.Select("Lo_letashlum=0 and mispar_sidur in(99820,99822,99821) and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')");
                                if (rowSidur.Length == 0 && fMichsatYomitLeloMutamut>0)
                                {
                                    fErechRechiv = fErechRechiv + fMichsatYomitLeloMutamut - _oGeneralData.objPirteyOved.iZmanMutamut;
                                }
                            }

                            if ((clCalcData.iSugYom == enSugYom.CholHamoedPesach.GetHashCode() || clCalcData.iSugYom == enSugYom.CholHamoedSukot.GetHashCode()) && _oGeneralData.objMeafyeneyOved.GetMeafyen(85).IntValue == 1 && !clCalcData.CheckOvedPutar(_iMisparIshi, _Taarich) && fMichsaYomit > 0 && fErechRechiv > 0)
                            {
                                fErechRechiv = fErechRechiv * float.Parse("0.6");
                            }

                            if (_oGeneralData.objMeafyeneyOved.GetMeafyen(83).IntValue == 1 && _oGeneralData.objMeafyeneyOved.GetMeafyen(33).IntValue == 1)
                            {
                                  sRechivim=clGeneral.enRechivim.YomHadracha.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomMachalatHorim.GetHashCode().ToString()   +
                                        "," + clGeneral.enRechivim.YomMachalaYeled.GetHashCode().ToString()  + "," + clGeneral.enRechivim.YomShmiratHerayon.GetHashCode().ToString()  + 
                                        "," + clGeneral.enRechivim.YomMachalatBenZug.GetHashCode().ToString()  + "," + clGeneral.enRechivim.YomAvodaBechul.GetHashCode().ToString() +
                                         "," + clGeneral.enRechivim.YomTeuna.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomMachla.GetHashCode().ToString() +
                                         "," + clGeneral.enRechivim.YomMiluim.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomMachalaBoded.GetHashCode().ToString();
                                if (_dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV in(" + sRechivim + ") and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')").Length>0)
                                {
                                    fErechRechiv=0;
                                }
                                if (fMichsaYomit > 0 && fMichsaYomit > fDakotNochehut && (fDakotNochehut > 0 || (clCalcData.iSugYom == enSugYom.CholHamoedPesach.GetHashCode() || clCalcData.iSugYom == enSugYom.CholHamoedSukot.GetHashCode())))
                                {
                                    fKizuzMeheadrut = (fMichsaYomit - fDakotNochehut) / 60;
                                }
                            
                            }

                            addRowToTable(clGeneral.enRechivim.YomHeadrut.GetHashCode(), fErechRechiv, fKizuzMeheadrut);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YomHeadrut.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv67()
        {
            float fErechRechiv, fMichsaYomit, fDakotNochehut, fMichsatYomitLeloMutamut, fKizuzMeheadrut;
            DataRow[] rowSidur;
            string sRechivim;
            try
            {
                fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fKizuzMeheadrut = 0;
                if (!(_oGeneralData.GetSugYomLemichsa(_iMisparIshi, _Taarich) == enSugYom.ErevYomHatsmaut.GetHashCode()
                    && (_objMefyeneyOved.GetMeafyen(63).Value != "" || _objMefyeneyOved.GetMeafyen(63).Value != "0") && _objMefyeneyOved.GetMeafyen(33).IntValue == 0))
                {
                   
                        rowSidur = _dtYemeyAvodaOved.Select("Lo_letashlum=0 and mispar_sidur=99841 and Hashlama_Leyom=1 and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')");
                        if (!(rowSidur.Length > 0 && fMichsaYomit > 0 && _objMefyeneyOved.GetMeafyen(33).IntValue == 0))
                        {
                            fErechRechiv = 0;
                            if (fMichsaYomit > 0)
                            {
                                rowSidur = _dtYemeyAvodaOved.Select("Lo_letashlum=0 and sidur_misug_headrut=5  and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')");
                                if (rowSidur.Length > 0)
                                {
                                    fErechRechiv = 1;
                                }
                            }
                            fDakotNochehut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            if (fMichsaYomit > 0 && fDakotNochehut == 0 && _dtYemeyAvodaOved.Select("Lo_letashlum=0 and mispar_sidur is null and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')").Length > 0 && _objMefyeneyOved.GetMeafyen(33).IntValue == 0)
                            { fErechRechiv = 1; }

                            if (fDakotNochehut < fMichsaYomit && fDakotNochehut > 0 && fMichsaYomit > 0 && _objMefyeneyOved.GetMeafyen(33).IntValue == 0)
                            {
                                fErechRechiv = (fMichsaYomit - fDakotNochehut) / fMichsaYomit;
                            }

                            if (_oGeneralData.objMeafyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                            {
                                fErechRechiv = fErechRechiv * clCalcData.fMekademNipuach;
                            }

                            if (_oGeneralData.objPirteyOved.iMutamBitachon > 0 && _oGeneralData.objPirteyOved.iKodMaamdRashi ==clGeneral.enMaamad.Friends.GetHashCode())
                            {
                                fMichsatYomitLeloMutamut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_EZER)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')")); ;
                                rowSidur = _dtYemeyAvodaOved.Select("Lo_letashlum=0 and mispar_sidur in(99820,99822,99821) and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')");
                                if (rowSidur.Length == 0 && fMichsatYomitLeloMutamut > 0)
                                {
                                    fErechRechiv = fErechRechiv + fMichsatYomitLeloMutamut - _oGeneralData.objPirteyOved.iZmanMutamut;
                                }
                            }

                            if ((clCalcData.iSugYom == enSugYom.CholHamoedPesach.GetHashCode() || clCalcData.iSugYom == enSugYom.CholHamoedSukot.GetHashCode()) && _oGeneralData.objMeafyeneyOved.GetMeafyen(85).IntValue == 1 && !clCalcData.CheckOvedPutar(_iMisparIshi, _Taarich) && fMichsaYomit > 0 && fErechRechiv > 0)
                            {
                                fErechRechiv = fErechRechiv * float.Parse("0.6");
                            }

                            if (_oGeneralData.objMeafyeneyOved.GetMeafyen(83).IntValue == 1 && _oGeneralData.objMeafyeneyOved.GetMeafyen(33).IntValue == 0)
                            {
                                sRechivim = clGeneral.enRechivim.YomHadracha.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomMachalatHorim.GetHashCode().ToString() +
                                      "," + clGeneral.enRechivim.YomMachalaYeled.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomShmiratHerayon.GetHashCode().ToString() +
                                      "," + clGeneral.enRechivim.YomMachalatBenZug.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomAvodaBechul.GetHashCode().ToString() +
                                       "," + clGeneral.enRechivim.YomTeuna.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomMachla.GetHashCode().ToString() +
                                       "," + clGeneral.enRechivim.YomMiluim.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomMachalaBoded.GetHashCode().ToString();
                                if (_dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV in(" + sRechivim + ") and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')").Length > 0)
                                {
                                    fErechRechiv = 0;
                                }
                                if (fMichsaYomit > 0 && fMichsaYomit > fDakotNochehut && (fDakotNochehut > 0 || (clCalcData.iSugYom == enSugYom.CholHamoedPesach.GetHashCode() || clCalcData.iSugYom == enSugYom.CholHamoedSukot.GetHashCode())))
                                {
                                    fKizuzMeheadrut = (fMichsaYomit - fDakotNochehut) / 60;
                                }

                            }

                            addRowToTable(clGeneral.enRechivim.YomChofesh.GetHashCode(), fErechRechiv, fKizuzMeheadrut);
                        }
                    }
                
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YomChofesh.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv68()
        {
            float fErechRechiv;

            try
            {

                fErechRechiv = CalcHeadruyot(clGeneral.enRechivim.YomTipatChalav.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomTipatChalav.GetHashCode(), fErechRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YomTipatChalav.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv69()
        {
            float fErechRechiv;

            try
            {

                fErechRechiv = CalcHeadruyot(clGeneral.enRechivim.YomMachalatBenZug.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomMachalatBenZug.GetHashCode(), fErechRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YomMachalatBenZug.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv70()
        {
            float fErechRechiv;

            try
            {

                fErechRechiv = CalcHeadruyot(clGeneral.enRechivim.YomMachalatHorim.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomMachalatHorim.GetHashCode(), fErechRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YomMachalatHorim.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv71()
        {
            float fErechRechiv;

            try
            {

                fErechRechiv = CalcHeadruyot(clGeneral.enRechivim.YomMachalaYeled.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomMachalaYeled.GetHashCode(), fErechRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YomMachalaYeled.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv72()
        {
            float fErechRechiv;
   
             try
            {
     
                fErechRechiv =CalcHeadruyot(clGeneral.enRechivim.YomKursHasavaLekav.GetHashCode());
                 addRowToTable(clGeneral.enRechivim.YomKursHasavaLekav.GetHashCode(), fErechRechiv);

             }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YomKursHasavaLekav.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }


        private float CalcHeadruyot(int iKodRechiv)
        {
            float fErechRechiv, fTempX, fMichsaYomit, fTempW;
            int iCount;
            try
            {
                fErechRechiv = 0;
                fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                if (fMichsaYomit > 0)
                {
                    fErechRechiv = oSidur.CalcYemeyHeadrut(iKodRechiv, out iCount, fMichsaYomit);

                    if (iCount == 1 && fErechRechiv > 0)
                    {
                        fTempX = 1;
                    }
                    else
                    {
                        fTempX = fErechRechiv / fMichsaYomit;
                    }

                    if (fTempX > 1) { fTempX = 1; }

                    fTempW = 1;

                    if (_objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                    {
                        fTempW = clCalcData.fMekademNipuach;
                    }

                    fErechRechiv = float.Parse(Math.Round(fTempW * fTempX, 2).ToString());
                 }
                return fErechRechiv;
               
            }
            catch (Exception ex)
            {
                 throw (ex);
            }
        }

        private void CalcRechiv75()
        {
            float fErechRechiv, fMichsaYomit, fDakotNochehut, fYomMiluim, fYomHadracha,fYomShmiratHerayon;
            float fYomMachaltHorim, fYomMachalaBoded, fYomMachala, fYomEvel, fYomMachalatYeled, fYomMachalaBenZug, fYomTeuna;
            try
            {
                fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fDakotNochehut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                fYomMachala = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachla.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fYomMachalaBoded = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalaBoded.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fYomTeuna = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomTeuna.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fYomShmiratHerayon = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomShmiratHerayon.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fYomMachalaBenZug = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalatBenZug.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fYomEvel = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomEvel.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fYomMachaltHorim = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalatHorim.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fYomMachalatYeled = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalaYeled.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fYomMiluim = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMiluim.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fYomHadracha = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomHadracha.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                if (fDakotNochehut > 0  && fYomMachala == 0 && fYomMachalaBoded == 0
                    && fYomShmiratHerayon == 0 && fYomEvel == 0 && fYomMachalatYeled == 0 && fYomHadracha == 0
                    && fYomMiluim == 0 && fYomMachalaBenZug == 0 && fYomTeuna == 0 && fYomMachaltHorim == 0)
                {
                    fErechRechiv = 1;
                    addRowToTable(clGeneral.enRechivim.YemeyNochehutLeoved.GetHashCode(), fErechRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YemeyNochehutLeoved.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv76()
        {
            float fDakotRechiv, fMichsaYomit;
            int iSugYom;
            try
            {
                fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                iSugYom = clCalcData.iSugYom;
  
                if (((_objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode()|| _objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode()) && (_objMefyeneyOved.GetMeafyen(32).Value != "1")) ||
                    _objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode() && fMichsaYomit == 0 && clCalcData.CheckYomShishi(iSugYom))
                {

                    fDakotRechiv = CalcDayRechiv76(fMichsaYomit,_Taarich);

                    addRowToTable(clGeneral.enRechivim.Nosafot125.GetHashCode(), fDakotRechiv);
                  
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.Nosafot125.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private float CalcDayRechiv76(float fMichsaYomit,DateTime dTaarich)
        {
            float fDakotNochehut, fErech, fNochehutBeshishi, fNochehutLetashlum;
            float SumNochechutMeyuchdim;
            fErech = 0;
          
            try
            {
               
                fDakotNochehut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                SumNochechutMeyuchdim = oSidur.GetSumSidurim100();
                fDakotNochehut = fDakotNochehut - SumNochechutMeyuchdim;

                fNochehutBeshishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutBeshishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
              //  fNochehutLetashlum = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                if (fMichsaYomit > 0 && fDakotNochehut > 0)
                {
                    if (fDakotNochehut > fMichsaYomit)
                    {
                        fErech = fDakotNochehut - fMichsaYomit;
                        fErech = Math.Min(120, fErech);
                    }
                }

                if (fMichsaYomit == 0 && _oGeneralData.GetSugYomLemichsa(_iMisparIshi, dTaarich) == enSugYom.Shishi.GetHashCode())
                {
                    if ((_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30) && fDakotNochehut > 120)
                        fErech = Math.Min(120, fDakotNochehut);
                    else if (!(_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30) && fNochehutBeshishi > 0)
                        fErech = Math.Min(240, fDakotNochehut);
                }
                
                return fErech;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CalcRechiv77()
        {
            float fDakotRechiv, fMichsaYomit;
            int iSugYom;
            try
            {
                fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                iSugYom = clCalcData.iSugYom;

                if ((_objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || _objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())  ||
                    _objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode() && fMichsaYomit == 0 && clCalcData.CheckYomShishi(iSugYom))
                {

                    fDakotRechiv = CalcDayRechiv77(fMichsaYomit,_Taarich);

                    addRowToTable(clGeneral.enRechivim.Nosafot150.GetHashCode(), fDakotRechiv);

                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.Nosafot150.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private float CalcDayRechiv77(float fMichsaYomit,DateTime dTaarich)
        {
            float  fDakotNochehut, fErech, fNosafot125;
            float SumNochechutMeyuchdim;
            fErech = 0;
            Boolean bMafilimSchirim = false;
            DataRow[] dr;
            try
            {
                SumNochechutMeyuchdim = oSidur.GetSumSidurim100();
                
                //ו.	עבור מפעילים שכירים במשמרת לילה יש לשלם את שעות 125% ב- 150% כדלקמן: אם העיסוק שליפת פרטי עובד (קוד נתון  HR = 6, מ.א., תאריך) עם ערך = 122,  123,  124,  127 וגם מעמד שכיר שליפת פרטי עובד (קוד נתון  HR = 13, מ.א., תאריך) הספרה הראשונה = 2 וגם [סוג משמרת] = לילה (ראה חישוב [סוג משמרת] למפעילים ברכיב 126) אזי: ערך הרכיב = ערך הרכיב + נוספות 125% (רכיב 76)
              if (_oGeneralData.objPirteyOved.iIsuk == 122 || _oGeneralData.objPirteyOved.iIsuk == 123 || _oGeneralData.objPirteyOved.iIsuk == 124 || _oGeneralData.objPirteyOved.iIsuk == 127)
                  {
                      if (_oGeneralData.objPirteyOved.iKodMaamdRashi== clGeneral.enMaamad.Salarieds.GetHashCode())
                      {
                          if (CheckSugMishmeret()==clGeneral.enSugMishmeret.Liyla.GetHashCode())
                          {
                          bMafilimSchirim=true;
                          }
                      }
                  }
               

                    fDakotNochehut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    fDakotNochehut = fDakotNochehut - SumNochechutMeyuchdim;
                    if (fMichsaYomit > 0 && fDakotNochehut > 0 && _objMefyeneyOved.GetMeafyen(32).Value != "1")
                    {
                        if (fDakotNochehut > (fMichsaYomit + 120))
                        {
                            fErech = fDakotNochehut - fMichsaYomit - 120;
                            fErech = Math.Max(0, fErech);
                        }
                    }
                    else if (fMichsaYomit > 0 && fDakotNochehut > 0 && _objMefyeneyOved.GetMeafyen(32).Value == "1")
                    {
                        if (fDakotNochehut > fMichsaYomit)
                        {
                            fErech = fDakotNochehut - fMichsaYomit;
                        }
                    }
                    if (fMichsaYomit == 0 && fDakotNochehut > 0 && _oGeneralData.GetSugYomLemichsa(_iMisparIshi, dTaarich) == enSugYom.Shishi.GetHashCode() && _objMefyeneyOved.GetMeafyen(32).Value != "1")
                    {
                        fErech = fDakotNochehut - 240;
                        fErech = Math.Max(0, fErech);
                    }
                    else if (fMichsaYomit == 0 && fDakotNochehut > 0 && _oGeneralData.GetSugYomLemichsa(_iMisparIshi, dTaarich) == enSugYom.Shishi.GetHashCode() && _objMefyeneyOved.GetMeafyen(32).Value == "1")
                    {
                        fErech = fDakotNochehut;
                    }

                    if (bMafilimSchirim)
                    {
                         fNosafot125 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot125.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                         fErech = fErech + fNosafot125;
                         dr = _dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Nosafot125.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')");
                        if (dr.Length>0)
                        {
                        dr[0].Delete();
                        }
                    }
                return fErech;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


         private void CalcRechiv78()
        {
            float fSumDakotRechiv, fDakotNahagut, fDakotTnua, fDakotTafkid, fDakotZikuyChofesh, fTosefetZmanNesia;
            float fDakotRechiv76, fDakotRechiv77, fZmanRetzifutShabat275, fDakotRechiv91, fDakotRechiv92;
            try
            {
                fSumDakotRechiv = 0;
                if ((!(_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30 && clCalcData.iSugYom == enSugYom.Bchirot.GetHashCode())))
                {
                    if (clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, clCalcData.iSugYom, _Taarich) || clCalcData.CheckYomShishi(clCalcData.iSugYom) || clCalcData.CheckErevChag(clCalcData.iSugYom))
                    {
                        fDakotNahagut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        fDakotTnua = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        fDakotTafkid = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        fDakotZikuyChofesh = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        fTosefetZmanNesia = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanNesia.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                      //  fDakotZikuy100 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ShaotShabat100.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        fZmanRetzifutShabat275 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                        fSumDakotRechiv = (fDakotNahagut + fDakotTnua + fDakotTafkid + fZmanRetzifutShabat275) - (fDakotZikuyChofesh + fTosefetZmanNesia);
                        addRowToTable(clGeneral.enRechivim.NosafotShabat.GetHashCode(), fSumDakotRechiv);
                    }

                    if (fSumDakotRechiv > 0)
                    {
                        //קיזוז נוספות שבת (200%) (רכיב 78) מרכיבים נוספות 125% ונוספות 150%:
                        kizuzNosafotShabet(fSumDakotRechiv, clGeneral.enRechivim.Nosafot125, clGeneral.enRechivim.Nosafot150);

                        //קיזוז נוספות שבת (200%) (רכיב 78) מרכיבים שעות 25% ושעות 50%:
                        kizuzNosafotShabet(fSumDakotRechiv, clGeneral.enRechivim.Shaot25, clGeneral.enRechivim.Shaot50);

                        //fDakotRechiv76 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot125.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        //fDakotRechiv77 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot150.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        //if (fDakotRechiv77 > 0)
                        //{
                        //    if (fDakotRechiv77 >= fSumDakotRechiv)
                        //    {
                        //        fDakotRechiv77 = (fDakotRechiv77 - fSumDakotRechiv);
                        //        addRowToTable(clGeneral.enRechivim.Nosafot150.GetHashCode(), fDakotRechiv77);
                        //    }
                        //    else if (fDakotRechiv77 < fSumDakotRechiv)
                        //    {
                        //        fDakotRechiv76 = Math.Max(fDakotRechiv76 - (fSumDakotRechiv - fDakotRechiv77), 0);
                        //        addRowToTable(clGeneral.enRechivim.Nosafot125.GetHashCode(), fDakotRechiv76);
                        //        addRowToTable(clGeneral.enRechivim.Nosafot150.GetHashCode(), 0);
                        //    }
                        //}
                        //else if (fDakotRechiv77 == 0 && fDakotRechiv76 > 0)
                        //{
                        //    fDakotRechiv76 = Math.Max(fDakotRechiv76 - fSumDakotRechiv, 0);
                        //    addRowToTable(clGeneral.enRechivim.Nosafot125.GetHashCode(), fDakotRechiv76);
                        //}
                      
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NosafotShabat.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
         }

         private void kizuzNosafotShabet(float fErechNosafotShabet, clGeneral.enRechivim RechivLekizuz1, clGeneral.enRechivim RechivLekizuz2)
         {
             float fDakotRechiv1, fDakotRechiv2;
             try{
                 fDakotRechiv1 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + RechivLekizuz1.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                 fDakotRechiv2 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + RechivLekizuz2.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                 if (fDakotRechiv2 > 0)
                 {
                     if (fDakotRechiv2 >= fErechNosafotShabet)
                     {
                         fDakotRechiv2 = (fDakotRechiv2 - fErechNosafotShabet);
                         addRowToTable(RechivLekizuz2.GetHashCode(), fDakotRechiv2);
                     }
                     else if (fDakotRechiv2 < fErechNosafotShabet)
                     {
                         fDakotRechiv1 = Math.Max(fDakotRechiv1 - (fErechNosafotShabet - fDakotRechiv2), 0);
                         addRowToTable(RechivLekizuz1.GetHashCode(), fDakotRechiv1);
                         addRowToTable(RechivLekizuz2.GetHashCode(), 0);
                     }
                 }
                 else if (fDakotRechiv2 == 0 && fDakotRechiv1 > 0)
                 {
                     fDakotRechiv1 = Math.Max(fDakotRechiv1 - fErechNosafotShabet, 0);
                     addRowToTable(RechivLekizuz1.GetHashCode(), fDakotRechiv1);
                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NosafotShabat.GetHashCode(), _Taarich, "CalcDay:kizuzNosafotShabet: " + ex.Message);
                 throw (ex);
             }
         }
         private void CalcRechiv80()
         {
             float fSumDakotRechiv, fZmanNesia, fTempX, fNosafotTafkid ;
             try{
             oSidur.CalcRechiv80();
            
             fZmanNesia = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanNesia.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
             fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

             fTempX = fSumDakotRechiv + fZmanNesia;

             fNosafotTafkid = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotTafkid.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

             if (fTempX >= fNosafotTafkid)
             {
                 fSumDakotRechiv = fNosafotTafkid;
             }
             else 
             {
                 fSumDakotRechiv = fTempX;
             }
            addRowToTable(clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode(), fSumDakotRechiv);
         }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
         }

         private void CalcRechiv81()
         {
             float fSumDakotRechiv, fDakotMichutzTafkid, fDakotMichutzNihul;
             try
             {
                 oSidur.CalcRechiv81();

                 fDakotMichutzTafkid = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaTafkidShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                 fDakotMichutzNihul = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaNihulShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                 fSumDakotRechiv = fDakotMichutzTafkid + fDakotMichutzNihul;

                 addRowToTable(clGeneral.enRechivim.DakotMichutzLamichsaShabat.GetHashCode(), fSumDakotRechiv);
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotMichutzLamichsaShabat.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                 throw (ex);
             }
         }

         private void CalcRechiv85()
         {
             float fSumDakotRechiv,fSumRetxzigfutNehiga, fSumRetzifutTafkid;
             try
             {
             oSidur.CalcRechiv85();

             fSumRetxzigfutNehiga = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
             fSumRetzifutTafkid = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

             fSumDakotRechiv = fSumRetxzigfutNehiga + fSumRetzifutTafkid;

             addRowToTable(clGeneral.enRechivim.SachTosefetRetzifut.GetHashCode(), fSumDakotRechiv);
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachTosefetRetzifut.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                 throw (ex);
             }
         }

         private void CalcRechiv86()
         {
             float fSumDakotRechiv;
             try{
             oSidur.CalcRechiv86();

             fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzDakotHatchalaGmar.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
             
             addRowToTable(clGeneral.enRechivim.KizuzDakotHatchalaGmar.GetHashCode(), fSumDakotRechiv);
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.KizuzDakotHatchalaGmar.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                 throw (ex);
             }
         }

         private void CalcRechiv88()
         {
             float fSumDakotRechiv, fMichsaYomit;
             try
             {
                 //א.	יום העבודה הינו א–ה וגם היום אינו שבתון סוג יום שליפת סוג יום (תאריך) = 01 
                 if (clCalcData.iSugYom <enSugYom.Shabat.GetHashCode())
                 {
                     fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    
                     if (fMichsaYomit>0)
                     {
                         if (!(_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30))
                         {
                             //ב.	עיסוק ראשי של העובד (התו הראשון בקוד העיסוק [שליפת פרטי עובד (קוד נתון HR=6, מ.א., תאריך)]) <> 5
                             if (_oGeneralData.objPirteyOved.iIsuk<500 || _oGeneralData.objPirteyOved.iIsuk>=600) // ((_oGeneralData.objPirteyOved.iIsuk.ToString()).Substring(0, 1) != "5")
                             {

                                 if (_oGeneralData.objMeafyeneyOved.GetMeafyen(30).IntValue == 1)
                                 {
                                     oSidur.CalcRechiv88();

                                     fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                                     if (fSumDakotRechiv > 18)
                                     { fSumDakotRechiv = 18; }

                                     addRowToTable(clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode(), fSumDakotRechiv);
                                 }

                             }
                         }
                     }
                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                 throw (ex);
             }
         }

         private void CalcRechiv89()
         {
             float fSumDakotRechiv;
             try
             {
                 oSidur.CalcRechiv89();
                 fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzBevisa.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                 addRowToTable(clGeneral.enRechivim.KizuzBevisa.GetHashCode(), fSumDakotRechiv);
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.KizuzBevisa.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                 throw (ex);
             }
         }

         private void CalcRechiv90()
         {
             int iZmanMutamut, iMutam;
             float fMichsaYomit, fTempY, fTempZ, fDakotRechiv, fMichsatMutam, fDakotNochehut1, fDakotNehiga;
             try
             {
                 //90 קיזוז לעובד מותאם (רכיב 
                 fDakotRechiv = 0;
                 iZmanMutamut = _oGeneralData.objPirteyOved.iZmanMutamut;
                 if (iZmanMutamut == 0)
                 {
                     fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                     fMichsatMutam = fMichsaYomit;
                 }
                 else { fMichsatMutam = iZmanMutamut; }

                 if (_oGeneralData.objPirteyOved.iMutamut > 0)
                 {
                     iMutam = _oGeneralData.objPirteyOved.iMutamut;
                     fDakotNochehut1 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                     fDakotNehiga = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                     if (iMutam == 1 && fMichsatMutam < fDakotNochehut1)
                     { fDakotRechiv = fDakotNochehut1 - fMichsatMutam; }
                     if (iMutam == 2 && fMichsatMutam < fDakotNehiga)
                     { fDakotRechiv = fDakotNehiga - fMichsatMutam; }
                     if (iMutam == 3 && fMichsatMutam < fDakotNehiga)
                     {
                         fDakotRechiv = fDakotNehiga - fMichsatMutam;
                         fTempY = fDakotNochehut1 - fDakotRechiv;
                         if (fMichsatMutam < fTempY)
                         {
                             fDakotRechiv = fDakotRechiv + (fTempY - fMichsatMutam);
                         }
                     }
                     if (iMutam == 4 && fMichsatMutam < fDakotNehiga)
                     {
                         fDakotRechiv = fDakotNehiga;
                     }

                     if ((iMutam == 5) && fMichsatMutam < fDakotNochehut1)
                     {
                         if (fDakotNehiga > 0)
                         {
                             fTempY = fDakotNehiga;
                             fTempZ = fDakotNochehut1 - fDakotNehiga;
                         }
                         else
                         {
                             fTempY = 0; fTempZ = fDakotNochehut1;
                         }

                         fDakotRechiv = fTempY + Math.Max(0, fTempZ - fMichsatMutam);
                     }


                     if (iMutam == 7 && fMichsatMutam < fDakotNochehut1)
                         fDakotRechiv = (fDakotNochehut1 - fMichsatMutam);


                     if (iMutam == 9)
                     {
                         fDakotRechiv = fDakotNehiga;
                     }

                     addRowToTable(clGeneral.enRechivim.KizuzOvedMutam.GetHashCode(), fDakotRechiv);
                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.KizuzOvedMutam.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                 throw (ex);
             }
         }

         private void CalcRechiv100()
         {
             float fSumDakotRechiv, fMichsaYomit, fDakotNochehut;
            try
            {
                fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fDakotNochehut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                if (fMichsaYomit > 0 && fDakotNochehut > 0)
                {
                    if (fMichsaYomit <= fDakotNochehut)
                        fSumDakotRechiv = fMichsaYomit;
                    else fSumDakotRechiv = fDakotNochehut;

                    addRowToTable(clGeneral.enRechivim.Shaot100Letashlum.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.Shaot100Letashlum.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
            }
         }

        private void CalcRechiv91()
        {
            float fMichsaYomit, fDakotRechiv;
            try
            {
            //יש לחשב את הרכיב עבור עובדים בעלי מאפיין ביצוע 56 עם ערך 52: [שליפת מאפיין ביצוע (קוד מאפיין = 56, מ.א., תאריך)] עם ערך = 52 (עובד 5 ימים חודשי).
            //אחרת, אין לפתוח רשומה לרכיב.
            //אם מכסה יומית  מחושבת (רכיב 126) > 0 
	        //ערך הרכיב = ערך רכיב נוספות 125% (רכיב 76) 
                   if (!(_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30))
                   { 
                        if (_objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
                        {
                            fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                            if (fMichsaYomit > 0)
                            {
                                fDakotRechiv = CalcDayRechiv76(fMichsaYomit,_Taarich);
                                
                                addRowToTable(clGeneral.enRechivim.Shaot25.GetHashCode(),fDakotRechiv);
                            }
                        }
                   }
             }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.Shaot25.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv92()
        {
            float fMichsaYomit, fDakotRechiv,fNochehutLetashlum;
            try
            {
                fDakotRechiv = 0;
                if (!(_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30))
                {
                    fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                     fNochehutLetashlum = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                    //יש לחשב את הרכיב עבור עובדים בעלי מאפיין ביצוע 56 עם ערך 52: [שליפת מאפיין ביצוע (קוד מאפיין = 56, מ.א., תאריך)] עם ערך = 52 (עובד 5 ימים חודשי).
                    //אחרת, אין לפתוח רשומה לרכיב.
                    //אם מכסה יומית  מחושבת (רכיב 126) > 0 
                    //ערך הרכיב = ערך רכיב נוספות 150% (רכיב 77) 
                    if (fMichsaYomit > 0)
                    {
                        if (_objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
                        {

                            fDakotRechiv = CalcDayRechiv77(fMichsaYomit,_Taarich);
                        }
                        else if (_oGeneralData.objPirteyOved.iIsuk == 122 || _oGeneralData.objPirteyOved.iIsuk == 123 || _oGeneralData.objPirteyOved.iIsuk == 124 || _oGeneralData.objPirteyOved.iIsuk == 127)
                        {
                            if (_oGeneralData.objPirteyOved.iKodMaamdRashi== clGeneral.enMaamad.Salarieds.GetHashCode())
                            {
                                if (CheckSugMishmeret() == clGeneral.enSugMishmeret.Liyla.GetHashCode())
                                {
                                    oSidur.CalcRechiv92(fMichsaYomit, fNochehutLetashlum);
                                    fDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Shaot50.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                    fDakotRechiv = fMichsaYomit-fDakotRechiv;
                                }
                            }
                        }
                    }

                    addRowToTable(clGeneral.enRechivim.Shaot50.GetHashCode(), fDakotRechiv);
                     
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.Shaot50.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv93()
        {
            DataRow[] rowZmanHalbash, rowMezakeHalbash, rowSidurim;
            float fDakotRechiv,fMichsaYomit, fHalbashaTchilatYom, fHalbashaSofYom, fSachDakotTafkid, fDakotTafkidChol, fDakotTafkidShabat, fDakotTafkidShishi;
            DataRow RowKodem, RowNext;
            int iSugYom;
            try{
            //תוספת זמן הלבשה (רכיב 93
            // 0 <  TB_Yamey_Avoda_Ovdim.Halbashaיש לחשב את הרכיב רק אם 
           // א.	ערך הרכיב = [הלבשה תחילת יום] + [הלבשה סוף יום]
                fSachDakotTafkid = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotTafkid.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                               
                rowZmanHalbash = _dtYemeyAvodaOved.Select("Lo_letashlum=0 and halbasha > 0 and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')");
            if (rowZmanHalbash.Length > 0 && fSachDakotTafkid >= _objParameters.iMinYomAvodaForHalbasha)
            {
                rowSidurim = _dtYemeyAvodaOved.Select("Lo_letashlum=0 and  taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur asc"); ;
                RowKodem = rowSidurim[0];
                fHalbashaTchilatYom = 0;
                fHalbashaSofYom = 0;

                //ב.	חישוב [הלבשה תחילת יום]:
                //אם TB_Yamey_Avoda_Ovdim.Halbasha = 1 או 3 אזי יש לזהות את הסידור הראשון ביום שמזכה להלבשה TB_Sidurim_Ovedim.Mezake_Halbasha= 1 אם אין לפניו סידור אחר אזי [הלבשה תחילת יום] = 10 דקות [שליפת פרמטר (קוד פרמטר = 143)]
                //אחרת, [הלבשה תחילת יום] = הנמוך מבין (10 דקות [שליפת פרמטר (קוד פרמטר = 143)], שעת התחלה לתשלום של הסידור המזכה הלבשה פחות שעת גמר לתשלום של הסידור שלפניו)
                rowMezakeHalbash = _dtYemeyAvodaOved.Select("Lo_letashlum=0 and (Mezake_Halbasha=1 or Mezake_Halbasha=3) and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur asc"); ;
                if (rowMezakeHalbash.Length > 0)
                {
                    for (int I = 0; I < rowSidurim.Length; I++)
                    {
                        RowKodem = rowSidurim[I];
                        if ((rowMezakeHalbash[0]["mispar_sidur"].ToString() == rowSidurim[I]["mispar_sidur"].ToString()) && (rowMezakeHalbash[0]["shat_hatchala_sidur"].ToString() == rowSidurim[I]["shat_hatchala_sidur"].ToString()))
                        {
                            if (I > 0)
                            {
                                I -= 1;
                                RowKodem = rowSidurim[I];
                            }
                            break;


                        }
                    }

                    if ((rowZmanHalbash[0]["halbasha"].ToString() == "1") || (rowZmanHalbash[0]["halbasha"].ToString() == "3"))
                    {
                        if ((rowMezakeHalbash[0]["mispar_sidur"].ToString() == RowKodem["mispar_sidur"].ToString()) && (rowMezakeHalbash[0]["shat_hatchala_sidur"].ToString() == RowKodem["shat_hatchala_sidur"].ToString()))
                            fHalbashaTchilatYom = _objParameters.iZmanHalbash;
                        else  fHalbashaTchilatYom = Math.Min(float.Parse((DateTime.Parse(rowMezakeHalbash[0]["shat_hatchala_letashlum"].ToString()) - DateTime.Parse(RowKodem["shat_gmar_letashlum"].ToString())).TotalMinutes.ToString()), _objParameters.iZmanHalbash);
                       
                         
                    }
                }
               

                
                //ג.	חישוב [הלבשה סוף יום]:
                //אם TB_Yamey_Avoda_Ovdim.Halbasha = 2 או 3 אזי יש לזהות את הסידור האחרון ביום שמזכה להלבשה TB_Sidurim_Ovedim.Mezake_Halbasha= 1. אם אין אחריו סידור אחר אזי [הלבשה סוף יום] = 10 דקות [שליפת פרמטר (קוד פרמטר = 143)]
                //אחרת, [הלבשה סוף יום] = הנמוך מבין (10 דקות [שליפת פרמטר (קוד פרמטר = 143)], שעת התחלה לתשלום של הסידור שאחרי הסידור המזכה הלבשה פחות שעת גמר לתשלום של הסידור המזכה הלבשה)
                rowMezakeHalbash = _dtYemeyAvodaOved.Select("Lo_letashlum=0 and (Mezake_Halbasha=2 or Mezake_Halbasha=3) and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur desc"); ;
                if (rowMezakeHalbash.Length > 0)
                {
                    RowNext = rowSidurim[0];
                    for (int I = 0; I < rowSidurim.Length; I++)
                    {
                        RowNext = rowSidurim[I];
                        if ((rowMezakeHalbash[0]["mispar_sidur"].ToString() == rowSidurim[I]["mispar_sidur"].ToString()) && (rowMezakeHalbash[0]["shat_hatchala_sidur"].ToString() == rowSidurim[I]["shat_hatchala_sidur"].ToString()))
                        {
                            if (I < rowSidurim.Length-1)
                            {
                                I+=1;
                                RowNext = rowSidurim[I];
                            }
                            break;
                           
                        }
                    }

                    if ((rowZmanHalbash[0]["halbasha"].ToString() == "2") || (rowZmanHalbash[0]["halbasha"].ToString() == "3"))
                    {
                        if ((rowMezakeHalbash[0]["mispar_sidur"].ToString() == RowNext["mispar_sidur"].ToString()) && (rowMezakeHalbash[0]["shat_hatchala_sidur"].ToString() == RowNext["shat_hatchala_sidur"].ToString()))
                          fHalbashaSofYom = _objParameters.iZmanHalbash;
                        else fHalbashaSofYom = Math.Min(float.Parse((DateTime.Parse(RowNext["shat_hatchala_letashlum"].ToString()) - DateTime.Parse(rowMezakeHalbash[0]["shat_gmar_letashlum"].ToString())).TotalMinutes.ToString()), _objParameters.iZmanHalbash);
                        
                    }
                }
               

                fDakotRechiv = fHalbashaTchilatYom + fHalbashaSofYom;

                addRowToTable(clGeneral.enRechivim.ZmanHalbasha.GetHashCode(), fDakotRechiv);

                
                 iSugYom = _oGeneralData.GetSugYomLemichsa(_iMisparIshi, _Taarich);
                 if (iSugYom == enSugYom.Shishi.GetHashCode() && fMichsaYomit == 0)
                 {
                     fDakotTafkidShishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                     addRowToTable(clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(), (fDakotTafkidShishi + fDakotRechiv));
                
                 }
                 else if (_Taarich.DayOfWeek.GetHashCode() + 1 == clGeneral.enDay.Shabat.GetHashCode() || clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, iSugYom, _Taarich))
                 {
                     fDakotTafkidShabat = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
               
                     addRowToTable(clGeneral.enRechivim.DakotTafkidShabat.GetHashCode(), (fDakotTafkidShabat + fDakotRechiv));
                
                 }
                 else
                 {
                     fDakotTafkidChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
               
                     addRowToTable(clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), (fDakotTafkidChol + fDakotRechiv));
                 }     
            }
             }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.ZmanHalbasha.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv94()
        {
            float fDakotNocheut, fErechRechiv, fMichsaYomit, fTempX, fHashlamaTafkid, fHashlamaNihul, fHashlamaNahagut;
            //תוספת זמן השלמה – רכיב 94 : 
             try{
                 fErechRechiv = 0;
                 if (!(_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30))
                 {
                     //oSidur.CalcRechiv94();
                     //fErechRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanHashlama.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                     if (_dtYemeyAvodaOved.Select("Lo_letashlum=0 and Hashlama_Leyom =1 and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')").Length > 0)
                     {
                         if (clCalcData.CheckUshraBakasha(clGeneral.enKodIshur.HashlamaLeyom.GetHashCode(), _iMisparIshi, _Taarich) || clCalcData.CheckUshraBakasha(clGeneral.enKodIshur.HashlamatShaotLemutamut.GetHashCode(), _iMisparIshi, _Taarich))
                         {
                             fHashlamaTafkid = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.HashlamaBetafkid.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                             fHashlamaNihul = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.HashlamaBenihulTnua.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                             fHashlamaNahagut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.HashlamaBenahagut.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                             fDakotNocheut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                             fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                             fTempX = fDakotNocheut + fHashlamaNahagut + fHashlamaNihul + fHashlamaTafkid;
                             if (fTempX < fMichsaYomit)
                             {
                                 fErechRechiv = fMichsaYomit - fTempX;
                             }
                         }
                     }

                 }
                addRowToTable(clGeneral.enRechivim.ZmanHashlama.GetHashCode(), fErechRechiv);
                }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.ZmanHashlama.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv95()
        {
            DataRow[] rowMezakeNesia, rowSidurim, rowBitulZmanNesiot;
            float fDakotRechiv, fNesiotTchilatYom, fNesiotSofYom, fSachNihulTnua, fSachNahagut, fSachTafkid;
            float fDakotTafkidChol, fDakotTafkidShabat, fDakotTafkidShishi, fMichsaYomit;
            DataRow RowKodem, RowNext;
            int iSugYom,iLastRowMezake,indexRow;
            bool bLogNahag = false;
            try
            {

                fSachNihulTnua = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNihulTnua.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fSachNahagut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNahagut.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fSachTafkid = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotTafkid.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                if ((_oGeneralData.objPirteyOved.iIsuk.ToString()).Substring(0, 1) != "5")
                    bLogNahag = true;
                    // תנאי מקדים לחישוב הרכיב: 
                    //אם העובד הוא קופאי - עיסוק העובד שליפת פרטי עובד (קוד נתון  HR = 6, מ.א., תאריך)  = 183, 184, 193 וגם סהכ ניהול תנועה (רכיב 190) גדול שווה 240 [שליפת פרמטר (קוד פרמטר = 238)]
                    //אחרת – אם עובד אינו קופאי:
                    //וגם {דקות סהכ נהגות (רכיב 188) +  וגם סהכ ניהול תנועה (רכיב 190) גדול שווה 180 דקות [שליפת פרמטר (קוד פרמטר = 239)] או סהכ תפקיד (רכיב 192) גדול שווה 240 דקות [שליפת פרמטר (קוד פרמטר = 238)]}

                if (((_oGeneralData.objPirteyOved.iIsuk == 183 || _oGeneralData.objPirteyOved.iIsuk == 184 || _oGeneralData.objPirteyOved.iIsuk == 193) && fSachNihulTnua > _objParameters.iMinDakotLezakautNesiot)
                            || ((_oGeneralData.objPirteyOved.iIsuk != 183 && _oGeneralData.objPirteyOved.iIsuk != 184 && _oGeneralData.objPirteyOved.iIsuk != 193) && (fSachNahagut + fSachTafkid) > _objParameters.iMinDakotNehigaVetnuaLezakautNesiot || fSachTafkid >= _objParameters.iMinDakotLezakautNesiot))
                {
                    //תוספת זמן נסיעה – רכיב 95:
                    //א.	ערך הרכיב = [נסיעות תחילת יום] + [נסיעות סוף יום]
                    //יש לפתוח רשומה לרכיב רק אם סכום [נסיעות תחילת יום] + [נסיעות סוף יום] > 0


                    rowSidurim = _dtYemeyAvodaOved.Select("Lo_letashlum=0 and  taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur asc"); ;
                    if (rowSidurim.Length > 0)
                    {
                        RowKodem = rowSidurim[0];
                        fNesiotTchilatYom = 0;
                        fNesiotSofYom = 0;

                        //ב.	חישוב [ תחילת יום]:
                        rowBitulZmanNesiot = _dtYemeyAvodaOved.Select("Lo_letashlum=0 and (Bitul_Zman_nesiot =1 or Bitul_Zman_nesiot =3) and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur asc"); ;
                        if (rowBitulZmanNesiot.Length > 0)
                        {
                            rowMezakeNesia = _dtYemeyAvodaOved.Select("Lo_letashlum=0 and Mezake_nesiot>0 and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur asc"); ;

                            if (rowMezakeNesia.Length > 0)
                            {
                                for (int I = 0; I < rowSidurim.Length; I++)
                                {
                                    RowKodem = rowSidurim[I];
                                    if ((rowMezakeNesia[0]["mispar_sidur"].ToString() == rowSidurim[I]["mispar_sidur"].ToString()) && (rowMezakeNesia[0]["shat_hatchala_sidur"].ToString() == rowSidurim[I]["shat_hatchala_sidur"].ToString()))
                                    {

                                        if (I > 0)
                                        {
                                            RowKodem = rowSidurim[I - 1];
                                        }

                                        break;
                                    }

                                }

                                if ((rowMezakeNesia[0]["mispar_sidur"].ToString() == RowKodem["mispar_sidur"].ToString()) && (rowMezakeNesia[0]["shat_hatchala_sidur"].ToString() == RowKodem["shat_hatchala_sidur"].ToString()))
                                {
                                    fNesiotTchilatYom = int.Parse(rowMezakeNesia[0]["Zman_Nesia_Haloch"].ToString());
                                    if (bLogNahag)
                                        fNesiotTchilatYom = fNesiotTchilatYom - 45;
                                    if (fNesiotTchilatYom < 0)
                                    {
                                        fNesiotTchilatYom = 0;
                                    }
                                }
                                else
                                {
                                    fNesiotTchilatYom = int.Parse(rowMezakeNesia[0]["Zman_Nesia_Haloch"].ToString());
                                    if (bLogNahag)
                                        fNesiotTchilatYom = fNesiotTchilatYom - 45;
                                    fNesiotTchilatYom = Math.Min(fNesiotTchilatYom, (float.Parse((DateTime.Parse(rowMezakeNesia[0]["shat_hatchala_letashlum"].ToString()) - DateTime.Parse(RowKodem["shat_gmar_letashlum"].ToString())).TotalMinutes.ToString())));
                                    if (fNesiotTchilatYom < 0)
                                    {
                                        fNesiotTchilatYom = 0;
                                    }
                                }
                            }
                            else
                            {
                                if (bLogNahag)
                                    fNesiotTchilatYom = int.Parse(rowBitulZmanNesiot[0]["Zman_Nesia_Haloch"].ToString()) - 45;
                                else
                                    fNesiotTchilatYom = int.Parse(rowBitulZmanNesiot[0]["Zman_Nesia_Haloch"].ToString());
                            }                           
                        }
                        //ג.	חישוב [ סוף יום]:
                        rowBitulZmanNesiot = _dtYemeyAvodaOved.Select("Lo_letashlum=0 and (Bitul_Zman_nesiot =2 or Bitul_Zman_nesiot =3) and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur asc"); ;
                        if (rowBitulZmanNesiot.Length > 0)
                        {
                            
                            rowMezakeNesia = _dtYemeyAvodaOved.Select("Lo_letashlum=0 and Mezake_nesiot>0 and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur asc"); ;
                            iLastRowMezake = rowMezakeNesia.Length - 1;
                            indexRow = 0;
                            if (rowMezakeNesia.Length > 0)
                            {
                                RowNext = rowSidurim[0];
                                for (int I = 0; I < rowSidurim.Length; I++)
                                {
                                    RowNext = rowSidurim[I];
                                    if ((rowMezakeNesia[iLastRowMezake]["mispar_sidur"].ToString() != rowSidurim[I]["mispar_sidur"].ToString()) && (rowMezakeNesia[iLastRowMezake]["shat_hatchala_sidur"].ToString() != rowSidurim[I]["shat_hatchala_sidur"].ToString()))
                                    {
                                        if (I < (rowSidurim.Length - 1))
                                        {
                                            RowNext = rowSidurim[I + 1];
                                            indexRow = I + 1;
                                        }

                                       // break;
                                    }
                                }

                                if (indexRow ==  rowSidurim.Length-1)
                               // if ((rowMezakeNesia[iLastRowMezake]["mispar_sidur"].ToString() == RowNext["mispar_sidur"].ToString()) && (rowMezakeNesia[iLastRowMezake]["shat_hatchala_sidur"].ToString() == RowNext["shat_hatchala_sidur"].ToString()))
                                {
                                    fNesiotSofYom = int.Parse(rowMezakeNesia[0]["Zman_Nesia_Hazor"].ToString());
                                    if (bLogNahag)
                                        fNesiotSofYom = fNesiotSofYom - 45;
                                    if (fNesiotSofYom < 0)
                                    {
                                        fNesiotSofYom = 0;
                                    }
                                }
                                else
                                {
                                    RowNext = rowSidurim[indexRow + 1];
                                    fNesiotSofYom = int.Parse(rowMezakeNesia[iLastRowMezake]["Zman_Nesia_Hazor"].ToString());
                                    if (bLogNahag)
                                        fNesiotSofYom = fNesiotSofYom - 45;
                                    fNesiotSofYom = Math.Min(fNesiotSofYom, float.Parse((DateTime.Parse(RowNext["shat_hatchala_letashlum"].ToString()) - DateTime.Parse(rowMezakeNesia[iLastRowMezake]["shat_gmar_letashlum"].ToString())).TotalMinutes.ToString()));
                                    if (fNesiotSofYom < 0)
                                    {
                                        fNesiotSofYom = 0;
                                    }
                                }
                            }
                            else
                            {
                                if (bLogNahag)
                                    fNesiotSofYom = int.Parse(rowBitulZmanNesiot[0]["Zman_Nesia_Hazor"].ToString()) - 45;
                                else
                                    fNesiotSofYom = int.Parse(rowBitulZmanNesiot[0]["Zman_Nesia_Hazor"].ToString());
                            }
                        }

                        fDakotRechiv = fNesiotTchilatYom + fNesiotSofYom;

                        addRowToTable(clGeneral.enRechivim.ZmanNesia.GetHashCode(), fDakotRechiv);

                        iSugYom = _oGeneralData.GetSugYomLemichsa(_iMisparIshi, _Taarich);
                        if (iSugYom == enSugYom.Shishi.GetHashCode() && fMichsaYomit == 0)
                        {
                            fDakotTafkidShishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            addRowToTable(clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(), (fDakotTafkidShishi + fDakotRechiv));

                        }
                        else if (_Taarich.DayOfWeek.GetHashCode() + 1 == clGeneral.enDay.Shabat.GetHashCode() || clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, iSugYom, _Taarich))
                        {
                            fDakotTafkidShabat = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                            addRowToTable(clGeneral.enRechivim.DakotTafkidShabat.GetHashCode(), (fDakotTafkidShabat + fDakotRechiv));

                        }
                        else
                        {
                            fDakotTafkidChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                            addRowToTable(clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), (fDakotTafkidChol + fDakotRechiv));
                        }  

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.ZmanNesia.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv96()
        {
            float fSumDakotRechiv ;
            float fSumDakotLaylaEgged, fSumDakotLaylaChok, fSumDakotLaylaBoker, fSumDakotShabat;
            //int iSugYom = clCalcGeneral.iSugYom;
            try
            {
                fSumDakotLaylaEgged = 0; fSumDakotLaylaChok = 0; fSumDakotLaylaBoker = 0; fSumDakotShabat = 0;
                fSumDakotRechiv = oSidur.CalcRechiv96(ref fSumDakotLaylaEgged, ref fSumDakotLaylaChok, ref fSumDakotLaylaBoker, ref fSumDakotShabat);

                ////fTempX = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode().ToString()));
                //fTempX += clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode().ToString()));

                ////if (fTempX < _oGeneralData.objParameters.iMaxRetzifutChodshitLetashlum && (fSumDakotRechiv + fTempX) > _oGeneralData.objParameters.iMaxRetzifutChodshitImGlisha)
                ////{
                ////    fSumDakotRechiv = _oGeneralData.objParameters.iMaxRetzifutChodshitLetashlum - fTempX;
                ////    addRowToTable(clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode(), _oGeneralData.objParameters.iMaxRetzifutChodshitLetashlum - fTempX);
                ////}
                ////else if (fTempX <  _oGeneralData.objParameters.iMaxRetzifutChodshitLetashlum && (fSumDakotRechiv + fTempX) < _oGeneralData.objParameters.iMaxRetzifutChodshitImGlisha)
                    addRowToTable(clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode(), fSumDakotRechiv);

                //רציפות לילה אגד  
                if (fSumDakotLaylaEgged > 0 && fSumDakotLaylaEgged > fSumDakotRechiv && fSumDakotLaylaChok==0)
                    addRowToTable(clGeneral.enRechivim.ZmanRetzifutLaylaEgged.GetHashCode(), fSumDakotRechiv);
                else
                    addRowToTable(clGeneral.enRechivim.ZmanRetzifutLaylaEgged.GetHashCode(), fSumDakotLaylaEgged);
                //רציפות לילה חוק  
                if (fSumDakotLaylaChok > 0 && fSumDakotLaylaChok > fSumDakotRechiv && fSumDakotLaylaEgged == 0)
                    addRowToTable(clGeneral.enRechivim.ZmanRetzifutLaylaChok.GetHashCode(), fSumDakotRechiv);
                else
                    addRowToTable(clGeneral.enRechivim.ZmanRetzifutLaylaChok.GetHashCode(), fSumDakotLaylaChok);
                //רציפות בוקר  
                addRowToTable(clGeneral.enRechivim.ZmanRetzifutBoker.GetHashCode(), fSumDakotLaylaBoker);

                if (clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, clCalcData.iSugYom, _Taarich) && fSumDakotRechiv>0)
                {
                    addRowToTable(clGeneral.enRechivim.ZmanRetzifutShabat.GetHashCode(), fSumDakotRechiv);
                }
                else if ((clCalcData.CheckErevChag(clCalcData.iSugYom) || clCalcData.CheckYomShishi(clCalcData.iSugYom)) && fSumDakotShabat>0)
                {
                    addRowToTable(clGeneral.enRechivim.ZmanRetzifutShabat.GetHashCode(), fSumDakotShabat);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv97()
        {
            float fSumDakotRechiv, fTempX;
            float fSumDakotLaylaEgged, fTempZ1;
            float fSumDakotLaylaChok, fTempZ2;
            try
            {
                fSumDakotLaylaEgged = 0; fSumDakotLaylaChok = 0;
                fSumDakotRechiv = oSidur.CalcRechiv97(ref fSumDakotLaylaEgged, ref fSumDakotLaylaChok);

                fTempX = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode().ToString()));
                fTempX += clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode().ToString()));

                if (fTempX <= _oGeneralData.objParameters.iMaxRetzifutChodshitLetashlum && (fSumDakotRechiv + fTempX) > _oGeneralData.objParameters.iMaxRetzifutChodshitImGlisha)
                {
                    fSumDakotRechiv = _oGeneralData.objParameters.iMaxRetzifutChodshitLetashlum - fTempX;
                    addRowToTable(clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode(), fSumDakotRechiv);
                }
                else if (fTempX <= _oGeneralData.objParameters.iMaxRetzifutChodshitLetashlum && (fSumDakotRechiv + fTempX) < _oGeneralData.objParameters.iMaxRetzifutChodshitImGlisha)
                    addRowToTable(clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode(), fSumDakotRechiv);

                //רציפות לילה  
                fTempZ1 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutLaylaEgged.GetHashCode().ToString() +" and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                if (fSumDakotLaylaEgged > 0 && fSumDakotLaylaEgged > fSumDakotRechiv && fSumDakotLaylaChok == 0)
                    addRowToTable(clGeneral.enRechivim.ZmanRetzifutLaylaEgged.GetHashCode(), fTempZ1 + fSumDakotRechiv);
                else
                    addRowToTable(clGeneral.enRechivim.ZmanRetzifutLaylaEgged.GetHashCode(),fTempZ1 + fSumDakotLaylaEgged);

                fTempZ2 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutLaylaChok.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                if (fSumDakotLaylaChok > 0 && fSumDakotLaylaChok > fSumDakotRechiv && fSumDakotLaylaEgged == 0)
                    addRowToTable(clGeneral.enRechivim.ZmanRetzifutLaylaChok.GetHashCode(), fTempZ2 + fSumDakotRechiv);
                else
                    addRowToTable(clGeneral.enRechivim.ZmanRetzifutLaylaChok.GetHashCode(), fTempZ2 + fSumDakotLaylaChok);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv109()
        {
            float fSumDakotRechiv, fMichsaMechushevet, fDakotNochehut;
            DataRow[] drRowSidurim;
            int iKaymRechiv;
            string sRechivim;
            try{
            fSumDakotRechiv=0;
            drRowSidurim = _dtYemeyAvodaOved.Select("Lo_letashlum=0 and MISPAR_SIDUR=99822 and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur asc");
            if (drRowSidurim.Length > 0)
            {
                fSumDakotRechiv = 1;
            }

            drRowSidurim = _dtYemeyAvodaOved.Select("Lo_letashlum=0 and Hashlama_Leyom=1 and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur asc");
            if (drRowSidurim.Length > 0)
            {
                fSumDakotRechiv = 1;
            }

            fMichsaMechushevet = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            fDakotNochehut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

            if (clCalcData.iSugYom == enSugYom.ErevYomHatsmaut.GetHashCode() && (_objMefyeneyOved.GetMeafyen(63).Value != "" || _objMefyeneyOved.GetMeafyen(63).Value != "0"))
            {
                fSumDakotRechiv = 1;
            }

            if (fDakotNochehut >0 && fMichsaMechushevet > 0 && (_oGeneralData.objMeafyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() | _oGeneralData.objMeafyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode()))
            {
                
                    fSumDakotRechiv = fDakotNochehut / fMichsaMechushevet;
            }

            sRechivim = clGeneral.enRechivim.YomMachla.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomMachalaBoded.GetHashCode().ToString() + "," +
                clGeneral.enRechivim.YomTeuna.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomShmiratHerayon.GetHashCode().ToString() + "," +
                clGeneral.enRechivim.YomMachalatBenZug.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomEvel.GetHashCode().ToString() + "," +
                clGeneral.enRechivim.YomKursHasavaLekav.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomMachalatHorim.GetHashCode().ToString() + "," +
                clGeneral.enRechivim.YomMachalaYeled.GetHashCode().ToString();
            
            iKaymRechiv=_dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV IN(" + sRechivim + ")").Length;
           
            if (iKaymRechiv == 0 && fDakotNochehut > _objParameters.iMinDakotNechshavYomAvoda && fMichsaMechushevet > 0 && _oGeneralData.objMeafyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
            {

                    fSumDakotRechiv = fDakotNochehut / fMichsaMechushevet;
            }
       
            addRowToTable(clGeneral.enRechivim.YemeyAvoda.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YemeyAvoda.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv110()
        {
            float fDakotNochehut, fMichsaMechushevet, fYomMachala,fYomMachaltHorim, fYomMachalaBoded,fYomMachalaBenZug;
            float fYomShmiratHerayon, fYomEvel, fYomMachalatYeled, fYomHadracha, fYomTeuna, fYomKursHasavaLekav;

            try{
            fDakotNochehut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            fMichsaMechushevet = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

            fYomMachala = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachla.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            fYomMachalaBoded = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMiluim.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            fYomTeuna = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomTeuna.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            fYomShmiratHerayon = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomShmiratHerayon.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            fYomMachalaBenZug = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalatBenZug.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            fYomEvel = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomEvel.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            fYomMachaltHorim = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalatHorim.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            fYomMachalatYeled = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalaYeled.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            fYomKursHasavaLekav = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomKursHasavaLekav.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            fYomHadracha = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomHadracha.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

            if (fDakotNochehut > 0 && fMichsaMechushevet > 0 && fYomMachala == 0 && fYomMachalaBoded==0
                &&  fYomShmiratHerayon==0 && fYomEvel==0 && fYomMachalatYeled==0 && fYomHadracha==0
                && fYomKursHasavaLekav==0 && fYomMachalaBenZug==0 && fYomTeuna==0 && fYomMachaltHorim==0)
            {
                addRowToTable(clGeneral.enRechivim.YemeyAvodaLeloMeyuchadim.GetHashCode(), 1);
            }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YemeyAvodaLeloMeyuchadim.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv125()
        {
            float fSumDakotRechiv;
            DateTime dShatGmarAvoda;
       try{     
            //יש לבצע את חישוב הרכיב רק עבור עובדים עם [שליפת מאפיין ביצוע (קוד מאפיין = 42, מ.א., תאריך)] עם ערך 70, אחרת אין לפתוח רשומה לרכיב זה בשום רמה.
            if (_objMefyeneyOved.GetMeafyen(42).Value == "1")
            {
                oSidur.CalcRechiv125();

                //א.	X = סכימת ערך הרכיב לכל הסידורים ביום העבודה.
                //אם .ב	X  >= 360 (6 שעות) [שליפת פרמטר (קוד פרמטר = 186)] וגם שעת גמר עבודה  >= 1930 [שליפת פרמטר (קוד פרמטר = 187)] אזי: ערך הרכיב  = 1

                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MishmeretShniaBameshek.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                if (fSumDakotRechiv > _objParameters.iMinZmanMishmeretShniaBameshek)
                {
                    dShatGmarAvoda = DateTime.Parse(clCalcData.DtYemeyAvoda.Select("taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')", "SHAT_GMAR_SIDUR DESC")[0]["SHAT_GMAR_SIDUR"].ToString());
                    if (dShatGmarAvoda >= _objParameters.dSiyumMishmeretShniaBameshek)
                    {
                        addRowToTable(clGeneral.enRechivim.MishmeretShniaBameshek.GetHashCode(), 1);
                    }
                }
            }
       }
       catch (Exception ex)
       {
           clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.MishmeretShniaBameshek.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
           throw (ex);
       }
        }

        public void CalcRechiv126(DateTime dTaarich)
        {
            //מכסה יומית מחושבת רכיב 126 
            int iSugYom, iSugMishmeret, iSugYomLemichsa;
            DataRow[] rowSidurim;
            float fErechRechiv=0;
            iSugYomLemichsa = 0;
            bool flag = true;
            _Taarich = dTaarich;

            _objMefyeneyOved = _oGeneralData.objMeafyeneyOved;
            _objParameters = _oGeneralData.objParameters;
            _dtYemeyAvodaOved = clCalcData.DtYemeyAvoda;

            oSidur.dTaarich = dTaarich;

            try
            {

                fErechRechiv = _oGeneralData.GetMichsaYomit(_iMisparIshi, int.Parse(_objMefyeneyOved.GetMeafyen(1).Value), ref iSugYomLemichsa, _Taarich);
                   
               
                    iSugYom = iSugYomLemichsa;
                    if (iSugYom == enSugYom.Purim.GetHashCode() && _oGeneralData.objPirteyOved.iEzor == clGeneral.enEzor.Yerushalim.GetHashCode())
                    {
                        if (!clCalcData.CheckYomShishi(iSugYom))
                        {
                            iSugYomLemichsa = enSugYom.Chol.GetHashCode();
                            fErechRechiv = _oGeneralData.GetMichsaYomit(_iMisparIshi, int.Parse(_objMefyeneyOved.GetMeafyen(1).Value), ref iSugYomLemichsa, _Taarich);
                        }
                        else
                        {
                            iSugYomLemichsa = enSugYom.Shishi.GetHashCode();
                            fErechRechiv = _oGeneralData.GetMichsaYomit(_iMisparIshi, int.Parse(_objMefyeneyOved.GetMeafyen(1).Value), ref iSugYomLemichsa, _Taarich);
                        }
                    }

                    if (iSugYom == enSugYom.ShushanPurim.GetHashCode() && _oGeneralData.objPirteyOved.iEzor == clGeneral.enEzor.Yerushalim.GetHashCode())
                    {
                        iSugYomLemichsa = enSugYom.Purim.GetHashCode();
                        fErechRechiv = _oGeneralData.GetMichsaYomit(_iMisparIshi, int.Parse(_objMefyeneyOved.GetMeafyen(1).Value), ref iSugYomLemichsa, _Taarich);
                    }

                    if (iSugYom == enSugYom.ShushanPurim.GetHashCode() && _oGeneralData.objPirteyOved.iEzor != clGeneral.enEzor.Yerushalim.GetHashCode())
                    {
                        if (!clCalcData.CheckYomShishi(iSugYom))
                        {
                            iSugYomLemichsa = enSugYom.Chol.GetHashCode();
                            fErechRechiv = _oGeneralData.GetMichsaYomit(_iMisparIshi, int.Parse(_objMefyeneyOved.GetMeafyen(1).Value),ref iSugYomLemichsa , _Taarich); }
                        else
                        {
                            iSugYomLemichsa=enSugYom.Shishi.GetHashCode();
                            fErechRechiv = _oGeneralData.GetMichsaYomit(_iMisparIshi, int.Parse(_objMefyeneyOved.GetMeafyen(1).Value), ref iSugYomLemichsa , _Taarich); }
                    }

                    if (!(_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30))
                    {
                        //יוצאים מהכלל/הנחות ברמת יום עבודה
                        if ((int.Parse(_objMefyeneyOved.GetMeafyen(1).Value) == 23 || int.Parse(_objMefyeneyOved.GetMeafyen(1).Value) == 22))
                        {
                            if (_oGeneralData.objPirteyOved.iGil == clGeneral.enKodGil.enKashish.GetHashCode() && iSugYom == enSugYom.Chol.GetHashCode())
                                fErechRechiv = 444;
                            else if (_oGeneralData.objPirteyOved.iGil == clGeneral.enKodGil.enKshishon.GetHashCode() && iSugYom == enSugYom.Chol.GetHashCode())
                                fErechRechiv = 462;
                            else if ((iSugYom >= 13 && iSugYom <= 18) || iSugYom == 11)
                                fErechRechiv = 300;
                            else if (_oGeneralData.objPirteyOved.iGil == clGeneral.enKodGil.enKashish.GetHashCode() && iSugYom == enSugYom.ErevYomKipur.GetHashCode())
                                fErechRechiv = 270;
                        }

                        if (int.Parse(_objMefyeneyOved.GetMeafyen(1).Value) != 22 && int.Parse(_objMefyeneyOved.GetMeafyen(1).Value) != 23)
                        {
                            if (_oGeneralData.objPirteyOved.iGil == clGeneral.enKodGil.enKashish.GetHashCode())
                            {
                                if (_objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || _objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
                                {
                                    if (iSugYom < enSugYom.Shishi.GetHashCode())
                                        fErechRechiv = fErechRechiv - 72;
                                    else if (iSugYom > enSugYom.Shishi.GetHashCode() && iSugYom < enSugYom.LagBaomerOrPurim.GetHashCode())  
                                        fErechRechiv = fErechRechiv - 60;
                                }
                                else if (_objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode() || _objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved6DaysInWeek2.GetHashCode())
                                {
                                    if (iSugYom < enSugYom.Shabat.GetHashCode())
                                        fErechRechiv = fErechRechiv - 60;
                                }
                            }
                            else if (_oGeneralData.objPirteyOved.iGil == clGeneral.enKodGil.enKshishon.GetHashCode())
                            {
                                if (_objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || _objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
                                {
                                    if (iSugYom < enSugYom.Shishi.GetHashCode())
                                        fErechRechiv = fErechRechiv - 36;
                                    else if (iSugYom > enSugYom.Shishi.GetHashCode() && iSugYom < enSugYom.LagBaomerOrPurim.GetHashCode())
                                        fErechRechiv = fErechRechiv - 30;
                                }
                                else if (_objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode() || _objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved6DaysInWeek2.GetHashCode())
                                {
                                    if (iSugYom < enSugYom.Shabat.GetHashCode())
                                        fErechRechiv = fErechRechiv - 30;
                                }
                            }
                            //if (_oGeneralData.objPirteyOved.iGil == clGeneral.enKodGil.enKashish.GetHashCode() && (_objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || _objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode()) && iSugYom < enSugYom.Shishi.GetHashCode())
                            //    fErechRechiv = fErechRechiv - 72;
                            //else if (_oGeneralData.objPirteyOved.iGil == clGeneral.enKodGil.enKashish.GetHashCode() && ((_objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || _objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode()) && iSugYom > enSugYom.Shishi.GetHashCode() && iSugYom < enSugYom.LagBaomerOrPurim.GetHashCode()) || (_objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode() || _objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved6DaysInWeek2.GetHashCode()))
                            //    fErechRechiv = fErechRechiv - 60;
                            //else if (_oGeneralData.objPirteyOved.iGil == clGeneral.enKodGil.enKshishon.GetHashCode() && (_objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || _objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode()) && iSugYom < enSugYom.Shishi.GetHashCode())
                            //    fErechRechiv = fErechRechiv - 36;
                            //else if (_oGeneralData.objPirteyOved.iGil == clGeneral.enKodGil.enKshishon.GetHashCode() && ((_objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || _objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode()) && iSugYom > enSugYom.Shishi.GetHashCode() && iSugYom < enSugYom.LagBaomerOrPurim.GetHashCode()) || (_objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode() || _objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved6DaysInWeek2.GetHashCode()))
                            //    fErechRechiv = fErechRechiv - 30;
                        }

                        if (_objMefyeneyOved.GetMeafyen(47).Value == "1")
                        { fErechRechiv = fErechRechiv - 60; }

                        rowSidurim = _dtYemeyAvodaOved.Select("Lo_letashlum=0 and mispar_sidur in(99707, 99706, 99708 , 99702, 99703, 99704,99701 ,99705)  and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur desc");
                        if (rowSidurim.Length > 0 && iSugYom == enSugYom.Shishi.GetHashCode() && fErechRechiv == 0)
                        {
                            if (_oGeneralData.objPirteyOved.iGil == clGeneral.enKodGil.enKashish.GetHashCode())
                            { fErechRechiv = 330; }
                            else if (_oGeneralData.objPirteyOved.iGil == clGeneral.enKodGil.enKshishon.GetHashCode())
                            { fErechRechiv = 360; }
                            else if (_oGeneralData.objPirteyOved.iGil == clGeneral.enKodGil.enTzair.GetHashCode())
                            { fErechRechiv = 390; }
                        }

                        if (_oGeneralData.objPirteyOved.iIsuk == 122 || _oGeneralData.objPirteyOved.iIsuk == 123 || _oGeneralData.objPirteyOved.iIsuk == 124 || _oGeneralData.objPirteyOved.iIsuk == 127)
                        {
                            if (_oGeneralData.objPirteyOved.iKodMaamdRashi == clGeneral.enMaamad.Salarieds.GetHashCode())
                            {

                                iSugMishmeret = CheckSugMishmeret();
                                if (iSugYom == enSugYom.Chol.GetHashCode())
                                {
                                    if (iSugMishmeret == clGeneral.enSugMishmeret.Boker.GetHashCode())
                                    {
                                        fErechRechiv = _oGeneralData.objParameters.iDakotMafileyMachshevColBoker;
                                    }
                                    else if (iSugMishmeret == clGeneral.enSugMishmeret.Tzaharim.GetHashCode())
                                    {
                                        fErechRechiv = _oGeneralData.objParameters.iDakotMafileyMachshevColTzaharim;
                                    }
                                    else if (iSugMishmeret == clGeneral.enSugMishmeret.Liyla.GetHashCode())
                                    {
                                        fErechRechiv = _oGeneralData.objParameters.iDakotMafileyMachshevColLiyla;
                                    }
                                }
                                else if (iSugYom > enSugYom.Shishi.GetHashCode() && iSugYom < enSugYom.LagBaomerOrPurim.GetHashCode())
                                {
                                    if (iSugMishmeret == clGeneral.enSugMishmeret.Boker.GetHashCode())
                                    {
                                        fErechRechiv = _oGeneralData.objParameters.iDakotMafileyMachshevErevChagBoker;
                                    }
                                    else if (iSugMishmeret == clGeneral.enSugMishmeret.Tzaharim.GetHashCode())
                                    {
                                        fErechRechiv = _oGeneralData.objParameters.iDakotMafileyMachshevErevChagTzaharim;
                                    }
                                }
                                else if (iSugYom == enSugYom.CholHamoedPesach.GetHashCode() || iSugYom == enSugYom.CholHamoedSukot.GetHashCode() || iSugYom == enSugYom.Purim.GetHashCode())
                                {
                                    if (iSugMishmeret == clGeneral.enSugMishmeret.Boker.GetHashCode() || iSugMishmeret == clGeneral.enSugMishmeret.Tzaharim.GetHashCode())
                                    {
                                        fErechRechiv = _oGeneralData.objParameters.iDakotMafileyMacshevChagBoker;
                                    }
                                    else if (iSugMishmeret == clGeneral.enSugMishmeret.Liyla.GetHashCode())
                                    {
                                        fErechRechiv = _oGeneralData.objParameters.iDakotMafileyMacshevChagLiyla;
                                    }
                                }
                                addRowToTable(clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), fErechRechiv, fErechRechiv);


                            }
                        }


                        if ((_oGeneralData.objPirteyOved.iMutamBitachon == 4 || _oGeneralData.objPirteyOved.iMutamBitachon == 5 || _oGeneralData.objPirteyOved.iMutamBitachon == 6 || _oGeneralData.objPirteyOved.iMutamBitachon == 8))
                        {
                            if (!clCalcData.CheckErevChag(iSugYom))
                                flag = false;
                            else if (_oGeneralData.objPirteyOved.iZmanMutamut > 0 && clCalcData.CheckErevChag(iSugYom))
                            {
                                fErechRechiv = Math.Min(fErechRechiv, _oGeneralData.objPirteyOved.iZmanMutamut);
                                flag = false;
                            }
                            else flag = true;
                        }
                        else if (flag && (_oGeneralData.objPirteyOved.iMutamut == clGeneral.enMutaam.enMutaam1.GetHashCode() || _oGeneralData.objPirteyOved.iMutamut == clGeneral.enMutaam.enMutaam4.GetHashCode() || _oGeneralData.objPirteyOved.iMutamut == clGeneral.enMutaam.enMutaam5.GetHashCode() || _oGeneralData.objPirteyOved.iMutamut == clGeneral.enMutaam.enMutaam6.GetHashCode()) && _oGeneralData.objPirteyOved.iZmanMutamut > 0)
                        {
                            fErechRechiv = Math.Min(fErechRechiv, _oGeneralData.objPirteyOved.iZmanMutamut);
                        }

                        //if ((_oGeneralData.objPirteyOved.iMutamut == clGeneral.enMutaam.enMutaam1.GetHashCode() || _oGeneralData.objPirteyOved.iMutamut == clGeneral.enMutaam.enMutaam4.GetHashCode() || _oGeneralData.objPirteyOved.iMutamut == clGeneral.enMutaam.enMutaam5.GetHashCode() || _oGeneralData.objPirteyOved.iMutamut == clGeneral.enMutaam.enMutaam6.GetHashCode()) && _oGeneralData.objPirteyOved.iZmanMutamut > 0)
                        //{
                        //    fErechRechiv = Math.Min(fErechRechiv, _oGeneralData.objPirteyOved.iZmanMutamut);
                        //}

                        //if ((_oGeneralData.objPirteyOved.iMutamBitachon == 4 || _oGeneralData.objPirteyOved.iMutamBitachon == 5 || _oGeneralData.objPirteyOved.iMutamBitachon == 6 || _oGeneralData.objPirteyOved.iMutamBitachon == 8) && !clCalcData.CheckErevChag(iSugYom)
                        //        && (_oGeneralData.objPirteyOved.iZmanMutamut > 0))
                        //{
                        //    fErechRechiv = Math.Min(fErechRechiv, _oGeneralData.objPirteyOved.iZmanMutamut);
                        //}

                        if (_objMefyeneyOved.GetMeafyen(91).Value.Length > 0)
                        {
                            if (int.Parse(_objMefyeneyOved.GetMeafyen(91).Value) > 0)
                            {
                                fErechRechiv = Math.Min(fErechRechiv, int.Parse(_objMefyeneyOved.GetMeafyen(91).Value));
                            }
                        }

                        if ((dTaarich.DayOfWeek.GetHashCode() + 1) == clGeneral.enDay.Shabat.GetHashCode())
                        { fErechRechiv = 0; }
                        else if ((dTaarich.DayOfWeek.GetHashCode() + 1) == clGeneral.enDay.Shishi.GetHashCode() && !CheckShishiMuchlaf(iSugYom, dTaarich) && (_objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || _objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode()))
                        { fErechRechiv = 0; }

                    }
                    else if (_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30 && iSugYom < enSugYom.Shishi.GetHashCode())
                    {
                        if (fErechRechiv > 420)
                        {
                            int iMinuts = 0;
                            DateTime dShatHatchala;
                            DateTime dShatGmar=DateTime.Parse(_Taarich.AddDays(1).ToShortDateString() + " 06:00");
                            
                            dShatHatchala = DateTime.Parse(_Taarich.ToShortDateString() + " 22:00");

                            rowSidurim = _dtYemeyAvodaOved.Select("Lo_letashlum=0 and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime') and Shat_gmar_Letashlum>=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime')", "");
                            for (int i = 0; i < rowSidurim.Length; i++)
                            {
                                if (DateTime.Parse(rowSidurim[i]["Shat_hatchala_Letashlum"].ToString()) >= dShatHatchala && DateTime.Parse(rowSidurim[i]["Shat_gmar_Letashlum"].ToString()) <= dShatGmar)
                                {
                                    iMinuts += int.Parse((DateTime.Parse(rowSidurim[i]["Shat_gmar_Letashlum"].ToString()) - DateTime.Parse(rowSidurim[i]["Shat_hatchala_Letashlum"].ToString())).TotalMinutes.ToString());
                                }
                                else if (DateTime.Parse(rowSidurim[i]["Shat_gmar_Letashlum"].ToString()) < dShatGmar && DateTime.Parse(rowSidurim[i]["Shat_gmar_Letashlum"].ToString()) > dShatHatchala)
                                {
                                    iMinuts += int.Parse((DateTime.Parse(rowSidurim[i]["Shat_gmar_Letashlum"].ToString()) - dShatHatchala).TotalMinutes.ToString());
                                }
                                else if (DateTime.Parse(rowSidurim[i]["Shat_hatchala_Letashlum"].ToString()) > dShatHatchala && DateTime.Parse(rowSidurim[i]["Shat_gmar_Letashlum"].ToString()) > dShatGmar)
                                {
                                    iMinuts += int.Parse((dShatGmar - DateTime.Parse(rowSidurim[i]["Shat_hatchala_Letashlum"].ToString())).TotalMinutes.ToString());
                                }
                                else if (DateTime.Parse(rowSidurim[i]["Shat_hatchala_Letashlum"].ToString()) < dShatHatchala && DateTime.Parse(rowSidurim[i]["Shat_gmar_Letashlum"].ToString()) > dShatGmar)
                                {
                                    iMinuts += int.Parse((dShatGmar - dShatHatchala).TotalMinutes.ToString());
                                }
                            }

                            if (iMinuts >= 120)
                                fErechRechiv = 420;
                        }
                    }

                addRowToTable(clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), fErechRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
         }

        private void CalcRechiv128()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv128();
                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanGrirot.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                addRowToTable(clGeneral.enRechivim.ZmanGrirot.GetHashCode(), fSumDakotRechiv);

                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                addRowToTable(clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode(), fSumDakotRechiv);

                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                addRowToTable(clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.ZmanGrirot.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv129()
        {
            float fDakorRechiv, fMachlifMeshaleach, fMachlifRakaz, fMachlifSadran, fMachlifKupai, fMachlifPakach;
            try{
            fMachlifMeshaleach = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMachlifMeshaleach.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            fMachlifRakaz = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMachlifRakaz.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            fMachlifSadran = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMachlifSadran.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            fMachlifKupai = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMachlifPakach.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
            fMachlifPakach = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMachlifPakach.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

            fDakorRechiv = fMachlifMeshaleach + fMachlifRakaz + fMachlifSadran + fMachlifKupai + fMachlifPakach;

            addRowToTable(clGeneral.enRechivim.DakotMachlifTnua.GetHashCode(), fDakorRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotMachlifTnua.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }   
        }

          private void CalcRechiv131()
        {
            //שבת/שעות 100% (רכיב 131):
            float fErechRechiv,fTempY,fTempX, fDakotNocheut, fMichsaYomit, fSidurim100, fShaot100;
            try
            {
                fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                 fDakotNocheut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                   
                if (!(_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30))
                {
                    oSidur.CalcRechiv131();
                    fErechRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ShaotShabat100.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                   
                    //חישוב סידורים מיוחדים לפי 100%
                    fSidurim100 = 0;
                    fTempY = oSidur.GetSumSidurim100();
                    
                    if (((fDakotNocheut - fTempY) > fMichsaYomit) ||(fMichsaYomit ==0))
                    {
                        fSidurim100 = fTempY;
                    }
                    else if (((fDakotNocheut - fTempY) < fMichsaYomit) && fDakotNocheut > fMichsaYomit)
                    {
                        fSidurim100 = fDakotNocheut - fMichsaYomit;
                    } 

                    // 100%   חישוב שעות שבת 
                    fShaot100 = 0;
                    fTempX = oSidur.GetSumShaotShabat100();
                    if (fTempX > 0)
                    {
                        if (fTempX > 0 && fDakotNocheut <= fMichsaYomit)
                        {
                            fShaot100 = fDakotNocheut - fTempX;
                        }
                        else if (fDakotNocheut > fMichsaYomit && fTempX < fMichsaYomit)
                        {
                            fShaot100 = fMichsaYomit - fTempX;
                        }
                    }
                    fErechRechiv = fErechRechiv + fSidurim100 + fShaot100;
                    addRowToTable(clGeneral.enRechivim.ShaotShabat100.GetHashCode(), fErechRechiv);

                }
                else if (clCalcData.iSugYom >= enSugYom.Shishi.GetHashCode() && clCalcData.iSugYom < enSugYom.Shabat.GetHashCode())
                {
                    if (fMichsaYomit == 0 && fDakotNocheut > 0)
                    {
                        fErechRechiv = Math.Min(120, fDakotNocheut);
                        addRowToTable(clGeneral.enRechivim.ShaotShabat100.GetHashCode(), fErechRechiv);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.ShaotShabat100.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
        }

          private void CalcRechiv132()
          {
              float fSumDakotRechiv, fZmanHamaratDakotShabat, fDakotZikuyChofesh;
              try
              {
                   oSidur.CalcRechiv132();
                  fZmanHamaratDakotShabat = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fDakotZikuyChofesh = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                  fSumDakotRechiv = fZmanHamaratDakotShabat + fDakotZikuyChofesh;
                  addRowToTable(clGeneral.enRechivim.ChofeshZchut.GetHashCode(), fSumDakotRechiv);
             
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.ChofeshZchut.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv133()
          {
              try
              {
                  if (!(_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30))
                  {
                      oSidur.CalcRechiv133();
                      UpdateRechiv133();
                  }
                }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.PremyaRegila.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void UpdateRechiv133()
          {
              float fSumDakotRechiv, fPremiaYomit, fPremiaShabat, fPremiaBeshishi;
              try
              {
                  fPremiaShabat = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fPremiaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaYomit.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fPremiaBeshishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                  fSumDakotRechiv = fPremiaYomit + fPremiaShabat + fPremiaBeshishi;
                  addRowToTable(clGeneral.enRechivim.PremyaRegila.GetHashCode(), fSumDakotRechiv);

              }
              catch (Exception ex)
              {
                  throw (ex);
              }
          }

          private void CalcRechiv134()
          {
              try
              {
                  oSidur.CalcRechiv134();
                  UpdateRechiv134();
             }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.PremyaNamlak.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void UpdateRechiv134()
          {
              float fSumDakotRechiv, fPremiaVisa, fPremiaVisaShabat, fPremiaVisaBeShishi;
              try
              {
                  fPremiaVisa = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaVisa.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fPremiaVisaShabat = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaVisaShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fPremiaVisaBeShishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaVisaShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                  fSumDakotRechiv = fPremiaVisa + fPremiaVisaShabat + fPremiaVisaBeShishi;
                  addRowToTable(clGeneral.enRechivim.PremyaNamlak.GetHashCode(), fSumDakotRechiv);

              }
              catch (Exception ex)
              {
                  throw (ex);
              }
          }

          private void CalcRechiv135()
          {
              try{
              CalcKizuzRechiv(clGeneral.enRechivim.KizuzKaytanaTafkid.GetHashCode());
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.KizuzKaytanaTafkid.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
              }


          private void CalcRechiv136()
          {
              try{
              CalcKizuzRechiv(clGeneral.enRechivim.KizuzKaytanaShivuk.GetHashCode());
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.KizuzKaytanaShivuk.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
              }


          private void CalcRechiv137()
          {
              try{
              CalcKizuzRechiv(clGeneral.enRechivim.KizuzKaytanaBniaPeruk.GetHashCode());
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.KizuzKaytanaBniaPeruk.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
              }


          private void CalcRechiv138()
          {
              try{
              CalcKizuzRechiv(clGeneral.enRechivim.KizuzKaytanaYeshivatTzevet.GetHashCode());
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.KizuzKaytanaYeshivatTzevet.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
              }


          private void CalcRechiv139()
          {
              try{
              CalcKizuzRechiv(clGeneral.enRechivim.KizuzKaytanaYamimArukim.GetHashCode());
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.KizuzKaytanaYamimArukim.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }


          private void CalcRechiv140()
          {
              try{
              CalcKizuzRechiv(clGeneral.enRechivim.KizuzKaytanaMeavteach.GetHashCode());
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.KizuzKaytanaMeavteach.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
              }


          private void CalcRechiv141()
          {
              try{
              CalcKizuzRechiv(clGeneral.enRechivim.KizuzKaytanaMatzil.GetHashCode());
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.KizuzKaytanaMatzil.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
         }

          private void CalcRechiv142()
          {
              float fSumDakotRechiv;
              try
              {
                  oSidur.CalcRechiv142();
                  fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.KizuzNochehut.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  addRowToTable(clGeneral.enRechivim.KizuzNochehut.GetHashCode(), fSumDakotRechiv);
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.KizuzNochehut.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv145()
          {
              try{
              CalcKizuzRechiv(clGeneral.enRechivim.KizuzVaadOvdim.GetHashCode());
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.KizuzVaadOvdim.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv146()
          {
              float fMichsaYomit, fDakotNocheut, fSumDakotRechiv;
              try
              {
                  if (_oGeneralData.objMeafyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
                  {
                      fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                      fDakotNocheut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                      if (fMichsaYomit > 0 && fDakotNocheut > fMichsaYomit)
                      {
                          fSumDakotRechiv = (fDakotNocheut - fMichsaYomit) / 60;
                          addRowToTable(clGeneral.enRechivim.Nosafot100.GetHashCode(), fSumDakotRechiv);
                      }
                  }
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.Nosafot100.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void SetSidureyVaadOvdim()
          {
              float fTempX, fTempY,fShaot150,fShaot50,fTempZ,fTempW,fTempT,fShaot125,fShaot25;
              try {
                  fTempX = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=99008 AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fShaot125 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot125.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fShaot25 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Shaot25.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                   fShaot150 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot150.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                   fShaot50 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Shaot50.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                   fTempY = fShaot25 + fShaot125+fShaot50 + fShaot150;

                  if (fTempX > 0 && fTempY > 0)
                  {
                      if (fTempX < fShaot50 + fShaot150)
                      {
                          fTempZ = fShaot50 + fShaot150;
                          if (fShaot150 > 0)
                              addRowToTable(clGeneral.enRechivim.Nosafot150.GetHashCode(), fShaot150 - fTempX + (fTempX / float.Parse("1.5")));

                          if (fShaot50 > 0)
                              addRowToTable(clGeneral.enRechivim.Shaot50.GetHashCode(), fShaot50 - fTempX + (fTempX / float.Parse("1.5")));

                          fShaot150 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot150.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                          fShaot50 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Shaot50.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                          fTempW = fTempZ - (fShaot50 + fShaot150);
                      }
                      else 
                      {
                          fTempZ = fShaot50 + fShaot150;
                          fTempT = fTempX - fTempZ;
                          if (fShaot150 > 0)
                              addRowToTable(clGeneral.enRechivim.Nosafot150.GetHashCode(), (fShaot150/ float.Parse("1.5")));

                          if (fShaot50 > 0)
                              addRowToTable(clGeneral.enRechivim.Shaot50.GetHashCode(), (fShaot50/float.Parse("1.5")));
                         
                          fShaot150 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot150.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                          fShaot50 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Shaot50.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                          fTempW = fTempZ - (fShaot50 + fShaot150);

                          if (fTempT > (fShaot25 + fShaot125))
                          {
                              fTempZ = (fShaot25 + fShaot125);
                              if (fShaot125 > 0)
                                  addRowToTable(clGeneral.enRechivim.Nosafot125.GetHashCode(), (fShaot125 / float.Parse("1.25")));

                              if (fShaot25 > 0)
                                  addRowToTable(clGeneral.enRechivim.Shaot25.GetHashCode(), (fShaot25 / float.Parse("1.25")));

                              fShaot125 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot125.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                              fShaot25 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Shaot25.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                              fTempW = fTempW + fTempZ - (fShaot125 + fShaot25);
                          }
                          else {
                              fTempZ = fShaot125 + fShaot25;
                              if (fShaot125 > 0)
                                  addRowToTable(clGeneral.enRechivim.Nosafot125.GetHashCode(), fShaot125 - fTempT + (fTempT / float.Parse("1.25")));

                              if (fShaot25 > 0)
                                  addRowToTable(clGeneral.enRechivim.Shaot25.GetHashCode(), fShaot25 - fTempT + (fTempT / float.Parse("1.25")));

                              fShaot125 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot125.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                              fShaot25 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Shaot25.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                              fTempW = fTempW + fTempZ - (fShaot125 + fShaot25);
                          }
                      }

                      if (fTempW > 0)
                      {
                          fShaot125 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot125.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                          fShaot25 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Shaot25.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                          fShaot150 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot150.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                          fShaot50 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Shaot50.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                          if ((fShaot25 + fShaot125 + fShaot50 + fShaot150) > 0) 
                          {
                              fTempX = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                              addRowToTable(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), fTempX-fTempW);

                              fTempX = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                              addRowToTable(clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), fTempX - fTempW);


                              fTempX = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotTafkid.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                              if (fTempX > 0)
                              {
                                  addRowToTable(clGeneral.enRechivim.DakotNosafotTafkid.GetHashCode(), fTempX - fTempW);

                                  fTempY = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachNosafotTafkidCholVeshishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                                  addRowToTable(clGeneral.enRechivim.SachNosafotTafkidCholVeshishi.GetHashCode(), fTempY - fTempW);

                                  fTempY = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotTafkid.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                                  addRowToTable(clGeneral.enRechivim.SachDakotTafkid.GetHashCode(), fTempY - fTempW);

                                  fTempY = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                                  addRowToTable(clGeneral.enRechivim.NochehutChol.GetHashCode(), fTempY - fTempW);
                              }

                              fTempY = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                              if (fTempX==fTempY)
                                    addRowToTable(clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode(), fTempX - fTempW);

                          }

                      }
                     
                  }

              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", 0, _Taarich, "CalcDay: SetSidureyVaadOvdim" + ex.Message);
                  throw (ex);
              }
          }


          private void CalcKizuzRechiv(int iKodRechiv)
          {
              string sSidurim;
              DataRow[] _drSidurMeyuchad;
              float fMichsaYomit, fErechRechiv;
              int iMisparSidur;
              DateTime dShatHatchalaSidur;
             try
             {
                sSidurim = oSidur.GetSidurimMeyuchRechiv(iKodRechiv);

              if (sSidurim.Length > 0)
              {
                 
                  _drSidurMeyuchad = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=1 and MISPAR_SIDUR IN(" + sSidurim + ") and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')");
                  fErechRechiv = 0;
                  for (int I = 0; I < _drSidurMeyuchad.Length ; I++)
                  {
                      dShatHatchalaSidur = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_sidur"].ToString());
                      iMisparSidur = int.Parse(_drSidurMeyuchad[I]["mispar_sidur"].ToString());
                      fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString()));

                      oPeilut.dTaarich = _Taarich;
                      fErechRechiv = fErechRechiv + oSidur.CalcRechiv1BySidur(_drSidurMeyuchad[I], fMichsaYomit,  oPeilut);
                   }
                  addRowToTable(iKodRechiv, fErechRechiv);
              }
            }
            catch (Exception ex)
            {
                throw ex;
            }
          }


          private void CalcRechiv149()
          {
              float fDakotNochehut, fMichsaYomit, fDakotPremia, fDakotVisa,fDakotRechiv;
              try
              {
                  fDakotRechiv = 0;
                  if (!(_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30))
                  {
                      if (_objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
                      {
                          fDakotNochehut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                          fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                          fDakotPremia = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaYomit.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                          fDakotVisa = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaVisa.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                          if (fMichsaYomit > fDakotNochehut)
                          {
                              if (((fMichsaYomit - fDakotNochehut) <= _objParameters.iMaxHashlamaAlCheshbonPremia) && fDakotPremia > 0)
                              {
                                  fDakotRechiv = Math.Min((fDakotPremia + fDakotVisa), (fMichsaYomit - fDakotNochehut));
                              }
                              if (fDakotRechiv <= fDakotPremia)
                              {
                                  addRowToTable(clGeneral.enRechivim.DakotPremiaYomit.GetHashCode(), (fDakotPremia - fDakotRechiv));
                              }
                              else
                              {
                                  addRowToTable(clGeneral.enRechivim.DakotPremiaVisa.GetHashCode(), (fDakotVisa - fDakotRechiv - fDakotPremia));
                                  addRowToTable(clGeneral.enRechivim.DakotPremiaYomit.GetHashCode(), 0);
                              }
                              UpdateRechiv133();
                              UpdateRechiv134();
                              addRowToTable(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), (fDakotNochehut + fDakotRechiv));
                              addRowToTable(clGeneral.enRechivim.HashlamaAlCheshbonPremia.GetHashCode(), fDakotRechiv);
                          }
                      }
                  }
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.HashlamaAlCheshbonPremia.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          //private void CalcRechiv164()
          //{
          //    float fSumDakotRechiv, fTempX,fDakotPakach,fDakotMachlifPakach;
          //    try
          //    {
          //        oSidur.CalcRechiv164();
          //        fTempX = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzLemichsaPakach.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
          //        fDakotPakach = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotPakach.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
          //        fDakotMachlifPakach = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotMachlifPakach.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

          //        if (fTempX >= (fDakotPakach + fDakotMachlifPakach))
          //        {
          //            fSumDakotRechiv = fDakotPakach + fDakotMachlifPakach; 
          //        }
          //        else 
          //        { 
          //            fSumDakotRechiv=fTempX;
          //        }
          //        addRowToTable(clGeneral.enRechivim.DakotMichutzLemichsaPakach.GetHashCode(), fSumDakotRechiv);

          //    }
          //    catch (Exception ex)
          //    {
          //        clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotMichutzLemichsaPakach.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
          //        throw (ex);
          //    }
          //}


          //private void CalcRechiv165()
          //{
          //    float fSumDakotRechiv, fTempX, fDakotSadran, fDakotMachlifSadran;
          //    try
          //    {
          //        oSidur.CalcRechiv165();
          //        fTempX = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzLemichsaSadsran.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
          //        fDakotSadran = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotSadran.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
          //        fDakotMachlifSadran= clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotMachlifSadran.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

          //        if (fTempX >= (fDakotSadran + fDakotMachlifSadran))
          //        {
          //            fSumDakotRechiv = fDakotSadran + fDakotMachlifSadran;
          //        }
          //        else
          //        {
          //            fSumDakotRechiv = fTempX;
          //        }
                  
          //        addRowToTable(clGeneral.enRechivim.DakotMichutzLemichsaSadsran.GetHashCode(), fSumDakotRechiv);

          //    }
          //    catch (Exception ex)
          //    {
          //        clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotMichutzLemichsaSadsran.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
          //        throw (ex);
          //    }
          //}

          //private void CalcRechiv166()
          //{
          //    float fSumDakotRechiv, fTempX, fDakotMachlifRakaz, fDakotRakaz;
          //    try
          //    {
          //        oSidur.CalcRechiv166();
          //        fTempX = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzLemichsaRakaz.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
          //        fDakotRakaz = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotRakaz.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
          //        fDakotMachlifRakaz = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotMachlifRakaz.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

          //        if (fTempX >= (fDakotRakaz + fDakotMachlifRakaz))
          //        {
          //            fSumDakotRechiv = fDakotRakaz + fDakotMachlifRakaz;
          //        }
          //        else
          //        {
          //            fSumDakotRechiv = fTempX;
          //        }
          //        addRowToTable(clGeneral.enRechivim.DakotMichutzLemichsaRakaz.GetHashCode(), fSumDakotRechiv);

          //    }
          //    catch (Exception ex)
          //    {
          //        clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotMichutzLemichsaRakaz.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
          //        throw (ex);
          //    }
          //}

          //private void CalcRechiv167()
          //{
          //    float fSumDakotRechiv, fTempX, fDakotMachlifMeshaleach, fDakotMeshaleach;
          //    try
          //    {
          //        oSidur.CalcRechiv167();
          //        fTempX = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzLemichsaMeshaleach.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
          //        fDakotMeshaleach = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotMeshaleach.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
          //        fDakotMachlifMeshaleach = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotMachlifMeshaleach.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

          //        if (fTempX >= (fDakotMeshaleach + fDakotMachlifMeshaleach))
          //        {
          //            fSumDakotRechiv = fDakotMeshaleach + fDakotMachlifMeshaleach;
          //        }
          //        else
          //        {
          //            fSumDakotRechiv = fTempX;
          //        }
          //        addRowToTable(clGeneral.enRechivim.DakotMichutzLemichsaMeshaleach.GetHashCode(), fSumDakotRechiv);

          //    }
          //    catch (Exception ex)
          //    {
          //        clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotMichutzLemichsaMeshaleach.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
          //        throw (ex);
          //    }
          //}

          private void CalcRechiv174()
          {
              try{
              CalcKizuzRechiv(clGeneral.enRechivim.KizuzMachlifPakach.GetHashCode());
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.KizuzMachlifPakach.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
              }

          private void CalcRechiv175()
          {
              try{
              CalcKizuzRechiv(clGeneral.enRechivim.KizuzMachlifSadran.GetHashCode());
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.KizuzMachlifSadran.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
              }

          private void CalcRechiv176()
          {
              try{
              CalcKizuzRechiv(clGeneral.enRechivim.KizuzMachlifRakaz.GetHashCode());
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.KizuzMachlifRakaz.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
              }

          private void CalcRechiv177()
          {
              try{
              CalcKizuzRechiv(clGeneral.enRechivim.KizuzMachlifMeshaleach.GetHashCode());
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.KizuzMachlifMeshaleach.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
              }

          private void CalcRechiv178()
          {
              try{
              CalcKizuzRechiv(clGeneral.enRechivim.KizuzMachlifKupai.GetHashCode());
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.KizuzMachlifKupai.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
              }

          private void CalcRechiv179()
          {
              float fSumDakotRechiv;
              try{
              oSidur.CalcRechiv179();
              fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotPakach.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
              addRowToTable(clGeneral.enRechivim.SachDakotPakach.GetHashCode(), fSumDakotRechiv);
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachDakotPakach.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
              }

          private void CalcRechiv180()
          {
              float fSumDakotRechiv;
            try{
              oSidur.CalcRechiv180();
              fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotSadran.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
              addRowToTable(clGeneral.enRechivim.SachDakotSadran.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachDakotSadran.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
            }

          private void CalcRechiv181()
          {
              float fSumDakotRechiv;
            try{
              oSidur.CalcRechiv181();
              fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotRakaz.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
              addRowToTable(clGeneral.enRechivim.SachDakotRakaz.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachDakotRakaz.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
            }

          private void CalcRechiv182()
          {
              float fSumDakotRechiv;
               try{
              oSidur.CalcRechiv182();
              fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotMeshalech.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
              addRowToTable(clGeneral.enRechivim.SachDakotMeshalech.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachDakotMeshalech.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
            }

          private void CalcRechiv183()
          {
              float fSumDakotRechiv;
            try{
              oSidur.CalcRechiv183();
              fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotKupai.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
              addRowToTable(clGeneral.enRechivim.SachDakotKupai.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachDakotKupai.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
            }

          private void CalcRechiv184()
          {
              float fSumDakotRechiv, fTempX, fDakotNosafotNihulTnua;
              try
              {
                  oSidur.CalcRechiv184();
                  fTempX = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzLamichsaNihulTnua.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fDakotNosafotNihulTnua = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotNihul.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                 
                  if (fTempX >= fDakotNosafotNihulTnua)
                  {
                      fSumDakotRechiv = fDakotNosafotNihulTnua;
                  }
                  else
                  {
                      fSumDakotRechiv = fTempX;
                  }
                  addRowToTable(clGeneral.enRechivim.DakotMichutzLamichsaNihulTnua.GetHashCode(), fSumDakotRechiv);

              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotMichutzLamichsaNihulTnua.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }


          private void CalcRechiv185()
          {
              try{
              CalcKizuzRechiv(clGeneral.enRechivim.KizuzMishpatChaverim.GetHashCode());
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.KizuzMishpatChaverim.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv186()
          {
              float fSumDakotRechiv;
            try{
              oSidur.CalcRechiv186();
              fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaTafkidShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
              addRowToTable(clGeneral.enRechivim.MichutzLamichsaTafkidShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.MichutzLamichsaTafkidShabat.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                throw (ex);
            }
            }


          private void CalcRechiv187()
          {
              float fSumDakotRechiv;
              try{
              oSidur.CalcRechiv187();
              fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaNihulShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
              addRowToTable(clGeneral.enRechivim.MichutzLamichsaNihulShabat.GetHashCode(), fSumDakotRechiv);
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.MichutzLamichsaNihulShabat.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
              }


          private void CalcRechiv188()
          {
              float fSumDakotRechiv, fDakotNehigaChol, fDakotNehigaShabat, fDakotNehigaShishi;
             try{
              fDakotNehigaChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
              fDakotNehigaShabat = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
              fDakotNehigaShishi= clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
             
              fSumDakotRechiv = fDakotNehigaChol+ fDakotNehigaShabat+ fDakotNehigaShishi;
              addRowToTable(clGeneral.enRechivim.SachDakotNahagut.GetHashCode(), fSumDakotRechiv);
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachDakotNahagut.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                 throw (ex);
             }
          }

          private void CalcRechiv189()
          {
              float fSumDakotRechiv, fZmanHashlama, fHashlamaBenahagut,fZmanGrirot, fRetzifutTafkid;
              try
              {
                  oSidur.CalcRechiv189();
                  fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fZmanGrirot = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanGrirot.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fSumDakotRechiv = fSumDakotRechiv + fZmanGrirot;
                  if (fSumDakotRechiv > 0)
                  {
                      fZmanHashlama = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanHashlama.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                      fRetzifutTafkid = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                      fHashlamaBenahagut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.HashlamaBenahagut.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                      
                      fSumDakotRechiv = fSumDakotRechiv  + fZmanHashlama + fHashlamaBenahagut + fRetzifutTafkid;
                      addRowToTable(clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode(), fSumDakotRechiv);
                  }
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv190()
          {
              float fSumDakotRechiv, fDakotNihulChol, fDakotNihulShabat, fDakotNihulShishi;
             try{
              fDakotNihulChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
              fDakotNihulShabat = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
              fDakotNihulShishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

              fSumDakotRechiv = fDakotNihulChol + fDakotNihulShabat + fDakotNihulShishi;
              addRowToTable(clGeneral.enRechivim.SachDakotNihulTnua.GetHashCode(), fSumDakotRechiv);
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachDakotNihulTnua.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                 throw (ex);
             }
          }

          private void CalcRechiv191()
          {
              float fSumDakotRechiv, fZmanRetzifut, fZmanHashlama, fHashlamaNihul, fDakotNehigaShishi;
              try
              {
                  oSidur.CalcRechiv191();
                  fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  if (fSumDakotRechiv > 0)
                  {
                      fZmanRetzifut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotLepremia.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                      fZmanHashlama = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanHashlama.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                      fHashlamaNihul = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.HashlamaBenihulTnua.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                      fDakotNehigaShishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                      if (fDakotNehigaShishi == 0)
                      { fSumDakotRechiv = fSumDakotRechiv + fZmanHashlama + fZmanRetzifut; }
                      
                      fSumDakotRechiv = fSumDakotRechiv  + fHashlamaNihul;
                      addRowToTable(clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode(), fSumDakotRechiv);
                  }
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv192()
          {
              float fSumDakotRechiv, fDakotTafkidChol, fDakotTafkidShabat, fDakotTafkidShishi;
             try{
                 oSidur.CalcRechiv192();
              fDakotTafkidChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
              fDakotTafkidShabat = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
              fDakotTafkidShishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

              fSumDakotRechiv = fDakotTafkidChol + fDakotTafkidShabat + fDakotTafkidShishi;
                
              addRowToTable(clGeneral.enRechivim.SachDakotTafkid.GetHashCode(), fSumDakotRechiv);
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachDakotTafkid.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                 throw (ex);
             }
          }

          private void CalcRechiv193()
          {
              float fSumDakotRechiv,fDakotNehigaShishi, fZmanHashlama,fDakotNihulShishi, fKizuzAruchatTzaharim;
              try
              {
                  oSidur.CalcRechiv193();
                  fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  if (fSumDakotRechiv > 0)
                  {
                      fKizuzAruchatTzaharim = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                      fZmanHashlama = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanHashlama.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                       fDakotNehigaShishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                      fDakotNihulShishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                      if (fDakotNehigaShishi == 0 && fDakotNihulShishi==0)
                      { fSumDakotRechiv =fSumDakotRechiv + fZmanHashlama ; }
                     
                      if (fSumDakotRechiv > 0)
                      {
                          fSumDakotRechiv = fSumDakotRechiv - fKizuzAruchatTzaharim;
                      }
                      addRowToTable(clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(), fSumDakotRechiv);
                  }
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }


          private void CalcRechiv194()
          {
              float fSumDakotRechiv, fNihulTnua, fNehigaChol, fTafkidChol;
              try
              {
                  oSidur.CalcRechiv194();
                  fNehigaChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fNihulTnua = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fTafkidChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                  fSumDakotRechiv = fNehigaChol + fNihulTnua + fTafkidChol;
                  addRowToTable(clGeneral.enRechivim.NochehutChol.GetHashCode(), fSumDakotRechiv);

              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutChol.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv195()
          {
              float fSumDakotRechiv, fNihulShishi, fNehigaShishi, fTafkidShishi;
              try
              {
                  oSidur.CalcRechiv195();
                  fNehigaShishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fNihulShishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fTafkidShishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                  fSumDakotRechiv = fNehigaShishi + fNihulShishi + fTafkidShishi;
                  addRowToTable(clGeneral.enRechivim.NochehutBeshishi.GetHashCode(), fSumDakotRechiv);

              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutBeshishi.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv196()
          {
              float fSumDakotRechiv, fNihulShabat, fNehigaShabat, fTafkidShabat;
              try
              {
                  oSidur.CalcRechiv196();
                  fNehigaShabat = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fNihulShabat = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fTafkidShabat = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidShabat.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                  fSumDakotRechiv = fNehigaShabat + fNihulShabat + fTafkidShabat;
                  addRowToTable(clGeneral.enRechivim.NochehutShabat.GetHashCode(), fSumDakotRechiv);

              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutShabat.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv198()
          {
              float fSumDakotRechiv, fShaot25, fShaot50, fNosafot125,fNosafot150;
              int iSugYomLemichsa;
              try
              {
                  iSugYomLemichsa=_oGeneralData.GetSugYomLemichsa(_iMisparIshi, _Taarich);
                  if (iSugYomLemichsa>=enSugYom.Shishi.GetHashCode() && iSugYomLemichsa.ToString().Substring(0, 1) == "1")
                  {
                      fNosafot125 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot125.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                      fNosafot150 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot150.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                      fShaot25 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Shaot25.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                      fShaot50 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Shaot50.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                      fSumDakotRechiv = fShaot25 + fShaot50 + fNosafot125+fNosafot150;
                      addRowToTable(clGeneral.enRechivim.NosafotShishi.GetHashCode(), fSumDakotRechiv);
                  }
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NosafotShishi.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }


          private void CalcRechiv200()
          {
              float fSumDakotRechiv, fTafkidChol, fTnuaChol;
              try
              {

                  fTafkidChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fTnuaChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotMichutzLamichsaNihulTnua.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                   fSumDakotRechiv = fTafkidChol + fTnuaChol;
                   addRowToTable(clGeneral.enRechivim.MichutzLamichsaChol.GetHashCode(), fSumDakotRechiv);
                 
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.MichutzLamichsaChol.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }


          private void CalcRechiv201()
          {
              float fSumDakotRechiv, fTafkidChol, fTnuaChol;
              try
              {

                  fTafkidChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaTafkidShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fTnuaChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaTnuaShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                  fSumDakotRechiv = fTafkidChol + fTnuaChol;
                  addRowToTable(clGeneral.enRechivim.MichutzLamichsaShishi.GetHashCode(), fSumDakotRechiv);

              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.MichutzLamichsaShishi.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv202()
          {
              float fSumDakotRechiv, fMichsaYomit, fNuchehutLepremia, fTosefetRetzifut, fSumDakotSikun;
              float fDakotHagdara, fDakotHistaglut, fSachNesiot, fDakotLepremia, fTosefetGil, fDakotKisuyTor;
             
              try
              {
                  if (_dtYemeyAvodaOved.Select("Lo_letashlum=0  and TACHOGRAF=1 and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur ASC").Length == 0)
                  {
                      fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                      if (fMichsaYomit == 0 && (clCalcData.CheckErevChag(clCalcData.iSugYom) || clCalcData.CheckYomShishi(clCalcData.iSugYom)))
                      {
                          if (_oGeneralData.objPirteyOved.iMutamut != 1 && _oGeneralData.objPirteyOved.iMutamut != 3)
                          {
                              if (_oGeneralData.objPirteyOved.iDirug != 85 || _oGeneralData.objPirteyOved.iDarga != 30)
                              {
                                  oSidur.CalcRechiv202();

                                  fNuchehutLepremia = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                  if (fNuchehutLepremia > 0)
                                  {
                                      fTosefetRetzifut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                      fDakotLepremia = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotLepremia.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                      fNuchehutLepremia += fTosefetRetzifut;
                                      ////ג.	אם תוספת רציפות 1-1 (רכיב 96) > 60 וגם דקות 1-1 (רכיב 52) >= תוספת רציפות 1-1 (רכיב 96) אזי: [דקות 1-1 לפרמיה]  = דקות 1-1 (רכיב 52) + 60 פחות -  תוספת רציפות 1-1 (רכיב 96).  אחרת, [דקות 1-1 לפרמיה]  = דקות 1-1 (רכיב 52) 
                                      //if (fTosefetRetzifut > 60 && fZmanRetzifut >= fTosefetRetzifut)
                                      //{
                                      //    fDakotLepremia = fZmanRetzifut + 60 - fTosefetRetzifut;
                                      //}
                                      //else { fDakotLepremia = fZmanRetzifut; }
                                      fTosefetGil = 0;

                                      fDakotHagdara = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotHagdara.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                      fDakotHistaglut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotHistaglut.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                      fSachNesiot = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachNesiot.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                      fDakotKisuyTor = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotKisuiTor.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                      fSumDakotSikun = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotSikun.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')")) / float.Parse("0.75");

                                      fSumDakotRechiv = (fDakotHagdara / float.Parse("0.75")) + fDakotHistaglut + fDakotKisuyTor + fDakotLepremia + fTosefetRetzifut + fSumDakotSikun + (fSachNesiot * _objParameters.fElementZar) + (fTosefetGil - fNuchehutLepremia);

                                      addRowToTable(clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(), fSumDakotRechiv);
                                  }
                              }
                          }
                      }
                  }
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }


          private void CalcRechiv203()
          {
              float fSumDakotRechiv, fSachKmVisa, fErechSidur;
              try
              {
                  if (_dtYemeyAvodaOved.Select("Lo_letashlum=0  and TACHOGRAF=1 and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur ASC").Length == 0)
                  {
                      if (clCalcData.CheckErevChag(clCalcData.iSugYom) && clCalcData.CheckYomShishi(clCalcData.iSugYom) )
                      {
                           oSidur.CalcRechiv203();

                           fErechSidur = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaVisaShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                           fSachKmVisa = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                            fSumDakotRechiv = float.Parse(((((fErechSidur / 1.2) + fSachKmVisa) / 50)*60 * 0.33).ToString());


                           addRowToTable(clGeneral.enRechivim.DakotPremiaVisaShishi.GetHashCode(), fSumDakotRechiv);

                      }
                  }
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotPremiaVisaShishi.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }


          private void CalcRechiv207()
          {
              float fSumDakotRechiv;
              try
              {
                  oSidur.CalcRechiv207();
                  fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaTafkidShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  addRowToTable(clGeneral.enRechivim.MichutzLamichsaTafkidShishi.GetHashCode(), fSumDakotRechiv);
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.MichutzLamichsaTafkidShishi.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }


          private void CalcRechiv208()
          {
              float fSumDakotRechiv;
              try
              {
                  oSidur.CalcRechiv208();
                  fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaTnuaShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  addRowToTable(clGeneral.enRechivim.MichutzLamichsaTnuaShishi.GetHashCode(), fSumDakotRechiv);
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.MichutzLamichsaTnuaShishi.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv209()
          {
              float fSumDakotRechiv, fZmanAruchatTzharayim;
              int iMutaam;
              try
              {
                  if (_oGeneralData.objPirteyOved.iIsuk == clGeneral.enIsukOved.Rasham.GetHashCode() || _oGeneralData.objPirteyOved.iIsuk == clGeneral.enIsukOved.SganMenahel.GetHashCode() || _oGeneralData.objPirteyOved.iIsuk == clGeneral.enIsukOved.MenahelMachlaka.GetHashCode())
                  {
                      if (_oGeneralData.objPirteyOved.iYechidaIrgunit == clGeneral.enYechidaIrgunit.RishumArtzi.GetHashCode() || _oGeneralData.objPirteyOved.iYechidaIrgunit == clGeneral.enYechidaIrgunit.RishumBameshek.GetHashCode())
                      {
                          iMutaam = _oGeneralData.objPirteyOved.iMutamut;
                          if (iMutaam != clGeneral.enMutaam.enMutaam1.GetHashCode() && iMutaam != clGeneral.enMutaam.enMutaam3.GetHashCode() && iMutaam != clGeneral.enMutaam.enMutaam5.GetHashCode() && iMutaam != clGeneral.enMutaam.enMutaam7.GetHashCode())
                          {
                              if (_objMefyeneyOved.GetMeafyen(60).IntValue>0)
                              {
                                   fZmanAruchatTzharayim = oSidur.CalcRechiv209();

                                      if (fZmanAruchatTzharayim > 30)
                                      { fZmanAruchatTzharayim = 30; }

                                      fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutPremiaLerishum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                      fSumDakotRechiv = fSumDakotRechiv - fZmanAruchatTzharayim;
                                      addRowToTable(clGeneral.enRechivim.NochehutPremiaLerishum.GetHashCode(), fSumDakotRechiv);
                                 
                              }
                          }
                      }
                  }
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutPremiaLerishum.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv210()
          {
              float fSumDakotRechiv,  fZmanAruchatBoker,  fZmanAruchatTzharayim, fZmanAruchatErev;
              try
              {

                  oSidur.CalcRechiv210(out fZmanAruchatBoker, out fZmanAruchatTzharayim,out fZmanAruchatErev);

                  if (fZmanAruchatBoker > 20)
                  { fZmanAruchatBoker = 20; }
                  if (fZmanAruchatTzharayim > 30)
                  { fZmanAruchatTzharayim = 30; }
                  if (fZmanAruchatErev > 20)
                  { fZmanAruchatErev = 20; }

                  fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutPremiaMevakrim.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fSumDakotRechiv = fSumDakotRechiv -fZmanAruchatBoker- fZmanAruchatTzharayim - fZmanAruchatErev;
                  addRowToTable(clGeneral.enRechivim.NochehutPremiaMevakrim.GetHashCode(), fSumDakotRechiv);
           
                 
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutPremiaMevakrim.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv211_212()
          {
              float fSumRechivMusachim, fSumRechivAchsana, fZmanAruchatBoker, fZmanAruchatTzharayim, fZmanAruchatErev;
              int iBokerRechiv, iTzharyimRechiv, iErevRechiv;
              try
              {
                  if (clCalcData.sSugYechida.ToLower() == "m_ms" || clCalcData.sSugYechida.ToLower() == "m_me")
                  {
                      oSidur.CalcRechiv211_212(out fZmanAruchatBoker, out fZmanAruchatTzharayim, out fZmanAruchatErev,out  iBokerRechiv,out  iTzharyimRechiv,out  iErevRechiv);

                      if (fZmanAruchatBoker > 20)
                      { fZmanAruchatBoker = 20; }
                      if (fZmanAruchatTzharayim > 30)
                      { fZmanAruchatTzharayim = 30; }
                      if (fZmanAruchatErev > 20)
                      { fZmanAruchatErev = 20; }

                      fSumRechivMusachim = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                      fSumRechivAchsana = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutPremiaMeshekAchsana.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                      if (iBokerRechiv == clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode())
                      {
                          fSumRechivMusachim = fSumRechivMusachim - fZmanAruchatBoker;
                      }
                      else
                      {  fSumRechivAchsana = fSumRechivAchsana - fZmanAruchatBoker ;}

                      if (iTzharyimRechiv == clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode())
                      {
                          fSumRechivMusachim = fSumRechivMusachim - fZmanAruchatTzharayim;
                      }
                      else
                      { fSumRechivAchsana = fSumRechivAchsana - fZmanAruchatTzharayim; }

                      if (iErevRechiv == clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode())
                      {
                          fSumRechivMusachim = fSumRechivMusachim - fZmanAruchatErev;
                      }
                      else
                      { fSumRechivAchsana = fSumRechivAchsana - fZmanAruchatErev; }

                     
                      addRowToTable(clGeneral.enRechivim.NochehutPremiaMeshekAchsana.GetHashCode(), fSumRechivAchsana);
                      addRowToTable(clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode(), fSumRechivMusachim);
                  
                  }

              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv276()
          {
              float fSumErechRechiv;
              try
              {
                  if (clCalcData.sSugYechida.ToLower() == "m_ms" || clCalcData.sSugYechida.ToLower() == "m_me")
                  {
                      oSidur.CalcRechiv276();
                      fSumErechRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochechutLePremiyaMeshekKonenutGrira.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                      addRowToTable(clGeneral.enRechivim.NochechutLePremiyaMeshekKonenutGrira.GetHashCode(), fSumErechRechiv);
                  }

              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochechutLePremiyaMeshekKonenutGrira.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv277()
          {
              float fSumErechRechiv;
              try
              {
                  if (clCalcData.sSugYechida.ToLower() == "m_ms" || clCalcData.sSugYechida.ToLower() == "m_me")
                  {
                      oSidur.CalcRechiv277();
                      fSumErechRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochechutLePremiyaMeshekGrira.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                      addRowToTable(clGeneral.enRechivim.NochechutLePremiyaMeshekGrira.GetHashCode(), fSumErechRechiv);
                  }

              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochechutLePremiyaMeshekGrira.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }
          private void CalcRechiv213()
          {
              float fSumDakotRechiv;
              try
              {
                  oSidur.CalcRechiv213();
                  fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachNesiot.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  addRowToTable(clGeneral.enRechivim.SachNesiot.GetHashCode(), fSumDakotRechiv);
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachNesiot.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv214()
          {
              float fSumDakotRechiv;
              try
              {
                  oSidur.CalcRechiv214();
                  fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotHistaglut.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  addRowToTable(clGeneral.enRechivim.DakotHistaglut.GetHashCode(), fSumDakotRechiv);
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotHistaglut.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv215()
          {
              float fSumDakotRechiv;
              try
              {
                  oSidur.CalcRechiv215();
                  fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachKM.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  addRowToTable(clGeneral.enRechivim.SachKM.GetHashCode(), fSumDakotRechiv);
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachKM.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv216()
          {
              float fSumDakotRechiv;
              try
              {
                  oSidur.CalcRechiv216();
                  fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  addRowToTable(clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode(), fSumDakotRechiv);
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv217()
          {
              float fSumDakotRechiv;
              try
              {
                  oSidur.CalcRechiv217();
                  fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotHagdara.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  addRowToTable(clGeneral.enRechivim.DakotHagdara.GetHashCode(), fSumDakotRechiv);
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotHagdara.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv218()
          {
              float fSumDakotRechiv;
              try
              {
                  oSidur.CalcRechiv218();
                  fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotKisuiTor.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  addRowToTable(clGeneral.enRechivim.DakotKisuiTor.GetHashCode(), fSumDakotRechiv);
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotKisuiTor.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv219()
          {
              float fSumDakotRechiv, fDakotChofesh;
              try
              {
                  fDakotChofesh = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotChofesh.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fSumDakotRechiv = fDakotChofesh / 60;
                  addRowToTable(clGeneral.enRechivim.ShaotChofesh.GetHashCode(), fSumDakotRechiv);
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.ShaotChofesh.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv220()
          {
              float fSumDakotRechiv, fYomHeadrut, fMichsaYomit;
              try
              {
                  fYomHeadrut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomHeadrut.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  if (_oGeneralData.objMeafyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                  {
                      fSumDakotRechiv = fYomHeadrut * fMichsaYomit * clCalcData.fMekademNipuach;
                  }
                  else {
                      fSumDakotRechiv = fYomHeadrut * fMichsaYomit;
                  }
                  addRowToTable(clGeneral.enRechivim.DakotHeadrut.GetHashCode(), fSumDakotRechiv);
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotHeadrut.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv221()
          {
              float fSumDakotRechiv, fYomChofesh, fMichsaYomit;
              try
              {
                  fYomChofesh= clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomChofesh.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  if (_oGeneralData.objMeafyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                  {
                      fSumDakotRechiv = fYomChofesh * fMichsaYomit * clCalcData.fMekademNipuach;
                  }
                  else
                  {
                      fSumDakotRechiv = fYomChofesh * fMichsaYomit;
                  }
                  addRowToTable(clGeneral.enRechivim.DakotChofesh.GetHashCode(), fSumDakotRechiv);
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotChofesh.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv223()
          {
              float fSumDakotRechiv, fZmanAruchatTzharayim;
              int iMutaam;
              try
              {
                  
                      if (_oGeneralData.objPirteyOved.iYechidaIrgunit == clGeneral.enYechidaIrgunit.MachsanKartisimDarom.GetHashCode() || _oGeneralData.objPirteyOved.iYechidaIrgunit == clGeneral.enYechidaIrgunit.MachsanKartisimJerusalem.GetHashCode())
                      {
                          iMutaam = _oGeneralData.objPirteyOved.iMutamut;
                          if (iMutaam > 0 && iMutaam != clGeneral.enMutaam.enMutaam1.GetHashCode() && iMutaam != clGeneral.enMutaam.enMutaam3.GetHashCode() && iMutaam != clGeneral.enMutaam.enMutaam5.GetHashCode() && iMutaam != clGeneral.enMutaam.enMutaam7.GetHashCode())
                          {
                              if (_objMefyeneyOved.GetMeafyen(60).IntValue>0)
                              {
                                      fZmanAruchatTzharayim = oSidur.CalcRechiv223();

                                      if (fZmanAruchatTzharayim > 30)
                                      { fZmanAruchatTzharayim = 30; }

                                      fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLepremiaMachsanKartisim.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                      fSumDakotRechiv = fSumDakotRechiv - fZmanAruchatTzharayim;
                                      addRowToTable(clGeneral.enRechivim.NochehutLepremiaMachsanKartisim.GetHashCode(), fSumDakotRechiv);
                                  
                              }
                          }
                      }
                 
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutLepremiaMachsanKartisim.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv233()
          {
              float fSumDakotRechiv, fZmanAruchatBoker, fZmanAruchatTzharayim, fZmanAruchatErev;
              try
              {
                  
                  oSidur.CalcNochehutPremia(clGeneral.enRechivim.NochehutLePremyatMifalYetzur.GetHashCode(), (_objMefyeneyOved.GetMeafyen(101).IntValue>0?true:false), out fZmanAruchatBoker, out fZmanAruchatTzharayim, out fZmanAruchatErev);

                  if (fZmanAruchatBoker > 20)
                  { fZmanAruchatBoker = 20; }
                  if (fZmanAruchatTzharayim > 30)
                  { fZmanAruchatTzharayim = 30; }
                  if (fZmanAruchatErev > 20)
                  { fZmanAruchatErev = 20; }

                  fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLePremyatMifalYetzur.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fSumDakotRechiv = fSumDakotRechiv - fZmanAruchatBoker - fZmanAruchatTzharayim - fZmanAruchatErev;
                  addRowToTable(clGeneral.enRechivim.NochehutLePremyatMifalYetzur.GetHashCode(), fSumDakotRechiv);


              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutLePremyatMifalYetzur.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv234()
          {
              float fSumDakotRechiv, fZmanAruchatBoker, fZmanAruchatTzharayim, fZmanAruchatErev;
              try
              {

                  oSidur.CalcNochehutPremia(clGeneral.enRechivim.NochehutLePremyatNehageyTovala.GetHashCode(), (_objMefyeneyOved.GetMeafyen(102).IntValue > 0 ? true : false), out fZmanAruchatBoker, out fZmanAruchatTzharayim, out fZmanAruchatErev);

                  if (fZmanAruchatBoker > 20)
                  { fZmanAruchatBoker = 20; }
                  if (fZmanAruchatTzharayim > 30)
                  { fZmanAruchatTzharayim = 30; }
                  if (fZmanAruchatErev > 20)
                  { fZmanAruchatErev = 20; }

                  fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLePremyatNehageyTovala.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fSumDakotRechiv = fSumDakotRechiv - fZmanAruchatBoker - fZmanAruchatTzharayim - fZmanAruchatErev;
                  addRowToTable(clGeneral.enRechivim.NochehutLePremyatNehageyTovala.GetHashCode(), fSumDakotRechiv);


              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutLePremyatNehageyTovala.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv235()
          {
              float fSumDakotRechiv;
              try
              {
                  if (_objMefyeneyOved.GetMeafyen(103).IntValue>0)
                  {
                      oSidur.CalcRechiv235();
                      fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLePremyatNehageyTenderim.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                      addRowToTable(clGeneral.enRechivim.NochehutLePremyatNehageyTenderim.GetHashCode(), fSumDakotRechiv);
                  }
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutLePremyatNehageyTenderim.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv236()
          {
              float fSumDakotRechiv, fZmanAruchatBoker, fZmanAruchatTzharayim, fZmanAruchatErev;
              try
              {

                  oSidur.CalcNochehutPremia(clGeneral.enRechivim.NochehutLePremyatDfus.GetHashCode(), (_objMefyeneyOved.GetMeafyen(104).IntValue > 0 ? true : false), out fZmanAruchatBoker, out fZmanAruchatTzharayim, out fZmanAruchatErev);

                  if (fZmanAruchatBoker > 20)
                  { fZmanAruchatBoker = 20; }
                  if (fZmanAruchatTzharayim > 30)
                  { fZmanAruchatTzharayim = 30; }
                  if (fZmanAruchatErev > 20)
                  { fZmanAruchatErev = 20; }

                  fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLePremyatDfus.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fSumDakotRechiv = fSumDakotRechiv - fZmanAruchatBoker - fZmanAruchatTzharayim - fZmanAruchatErev;
                  addRowToTable(clGeneral.enRechivim.NochehutLePremyatDfus.GetHashCode(), fSumDakotRechiv);


              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutLePremyatDfus.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv237()
          {
              float fSumDakotRechiv, fZmanAruchatBoker, fZmanAruchatTzharayim, fZmanAruchatErev;
              try
              {

                  oSidur.CalcNochehutPremia(clGeneral.enRechivim.NochehutLePremyatAchzaka.GetHashCode(), (_objMefyeneyOved.GetMeafyen(105).IntValue > 0 ? true : false), out fZmanAruchatBoker, out fZmanAruchatTzharayim, out fZmanAruchatErev);

                  if (fZmanAruchatBoker > 20)
                  { fZmanAruchatBoker = 20; }
                  if (fZmanAruchatTzharayim > 30)
                  { fZmanAruchatTzharayim = 30; }
                  if (fZmanAruchatErev > 20)
                  { fZmanAruchatErev = 20; }

                  fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLePremyatAchzaka.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fSumDakotRechiv = fSumDakotRechiv - fZmanAruchatBoker - fZmanAruchatTzharayim - fZmanAruchatErev;
                  addRowToTable(clGeneral.enRechivim.NochehutLePremyatAchzaka.GetHashCode(), fSumDakotRechiv);


              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutLePremyatAchzaka.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv238()
          {
              float fSumDakotRechiv, fZmanAruchatBoker, fZmanAruchatTzharayim, fZmanAruchatErev;
              try
              {

                  oSidur.CalcNochehutPremia(clGeneral.enRechivim.NochehutLePremyatGifur.GetHashCode(), (_objMefyeneyOved.GetMeafyen(106).IntValue > 0 ? true : false), out fZmanAruchatBoker, out fZmanAruchatTzharayim, out fZmanAruchatErev);

                  if (fZmanAruchatBoker > 20)
                  { fZmanAruchatBoker = 20; }
                  if (fZmanAruchatTzharayim > 30)
                  { fZmanAruchatTzharayim = 30; }
                  if (fZmanAruchatErev > 20)
                  { fZmanAruchatErev = 20; }

                  fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLePremyatGifur.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fSumDakotRechiv = fSumDakotRechiv - fZmanAruchatBoker - fZmanAruchatTzharayim - fZmanAruchatErev;
                  addRowToTable(clGeneral.enRechivim.NochehutLePremyatGifur.GetHashCode(), fSumDakotRechiv);


              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutLePremyatGifur.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv239()
          {
              float fSumDakotRechiv, fZmanAruchatBoker, fZmanAruchatTzharayim, fZmanAruchatErev;
              try
              {

                  oSidur.CalcNochehutPremia(clGeneral.enRechivim.NochehutLePremyaRishonLetzyon.GetHashCode(), (_objMefyeneyOved.GetMeafyen(107).IntValue > 0 ? true : false), out fZmanAruchatBoker, out fZmanAruchatTzharayim, out fZmanAruchatErev);

                  if (fZmanAruchatBoker > 20)
                  { fZmanAruchatBoker = 20; }
                  if (fZmanAruchatTzharayim > 30)
                  { fZmanAruchatTzharayim = 30; }
                  if (fZmanAruchatErev > 20)
                  { fZmanAruchatErev = 20; }

                  fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLePremyaRishonLetzyon.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fSumDakotRechiv = fSumDakotRechiv - fZmanAruchatBoker - fZmanAruchatTzharayim - fZmanAruchatErev;
                  addRowToTable(clGeneral.enRechivim.NochehutLePremyaRishonLetzyon.GetHashCode(), fSumDakotRechiv);


              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutLePremyaRishonLetzyon.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv240()
          {
              float fSumDakotRechiv, fZmanAruchatBoker, fZmanAruchatTzharayim, fZmanAruchatErev;
              try
              {

                  oSidur.CalcNochehutPremia(clGeneral.enRechivim.NochehutLePremyaTechnayYetzur.GetHashCode(), (_objMefyeneyOved.GetMeafyen(108).IntValue>0?true:false), out fZmanAruchatBoker, out fZmanAruchatTzharayim, out fZmanAruchatErev);

                  if (fZmanAruchatBoker > 20)
                  { fZmanAruchatBoker = 20; }
                  if (fZmanAruchatTzharayim > 30)
                  { fZmanAruchatTzharayim = 30; }
                  if (fZmanAruchatErev > 20)
                  { fZmanAruchatErev = 20; }

                  fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLePremyaTechnayYetzur.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fSumDakotRechiv = fSumDakotRechiv - fZmanAruchatBoker - fZmanAruchatTzharayim - fZmanAruchatErev;
                  addRowToTable(clGeneral.enRechivim.NochehutLePremyaTechnayYetzur.GetHashCode(), fSumDakotRechiv);


              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutLePremyaTechnayYetzur.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv241()
          {
              float fSumDakotRechiv, fZmanAruchatBoker, fZmanAruchatTzharayim, fZmanAruchatErev;
              try
              {

                  oSidur.CalcNochehutPremia(clGeneral.enRechivim.NochehutLePremyatPerukVeshiputz.GetHashCode(), (_objMefyeneyOved.GetMeafyen(11).IntValue > 0 ? true : false), out fZmanAruchatBoker, out fZmanAruchatTzharayim, out fZmanAruchatErev);

                  if (fZmanAruchatBoker > 20)
                  { fZmanAruchatBoker = 20; }
                  if (fZmanAruchatTzharayim > 30)
                  { fZmanAruchatTzharayim = 30; }
                  if (fZmanAruchatErev > 20)
                  { fZmanAruchatErev = 20; }

                  fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLePremyatPerukVeshiputz.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fSumDakotRechiv = fSumDakotRechiv - fZmanAruchatBoker - fZmanAruchatTzharayim - fZmanAruchatErev;
                  addRowToTable(clGeneral.enRechivim.NochehutLePremyatPerukVeshiputz.GetHashCode(), fSumDakotRechiv);


              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutLePremyatPerukVeshiputz.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv245()
          {
              float fMichsaYomit, fErechRechiv,fNochehutBefoal;
              try
              {
                  if (_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30)
                  {
                      if (_objMefyeneyOved.GetMeafyen(50).Value =="1")
                      {
                          fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                          fNochehutBefoal = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                          if (fMichsaYomit > 0 && fNochehutBefoal>0)
                          {
                              fErechRechiv=1;

                              addRowToTable(clGeneral.enRechivim.EshelLeEggedTaavura.GetHashCode(), fErechRechiv);
                          }
                      }
                  }
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.EshelLeEggedTaavura.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv246()
          {
              float fSumDakotRechiv, fZmanAruchatTzharayim;
              try
              {
                  if (_oGeneralData.objPirteyOved.iIsuk == clGeneral.enIsukOved.MenahelMoreNehiga.GetHashCode())
                  {
                    
                      fZmanAruchatTzharayim = oSidur.CalcRechiv246();

                      if (fZmanAruchatTzharayim > 30)
                      { fZmanAruchatTzharayim = 30; }

                      fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochechutLepremyaMenahel.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                      fSumDakotRechiv = fSumDakotRechiv - fZmanAruchatTzharayim;
                      addRowToTable(clGeneral.enRechivim.NochechutLepremyaMenahel.GetHashCode(), fSumDakotRechiv);

                            
                     
                  }
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochechutLepremyaMenahel.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv248()
          {
              float fSumDakotRechiv, fDakotNochehut, fMichsaYomit;
              try
              {
                  fSumDakotRechiv = 0;
                  fDakotNochehut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  if (fDakotNochehut == 0 && fMichsaYomit > 0 && _oGeneralData.objMeafyeneyOved.GetMeafyen(33).IntValue == 0 && clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is null and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur ASC").Length > 0)
                  {
                      fSumDakotRechiv = 1;
                  }
                  addRowToTable(clGeneral.enRechivim.YomChofeshNoDivuach.GetHashCode(), fSumDakotRechiv);
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YomChofeshNoDivuach.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv249()
          {
              float fSumDakotRechiv, fDakotNochehut, fMichsaYomit;
              try
              {
                  fSumDakotRechiv = 0;
                  fDakotNochehut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  if (fDakotNochehut == 0 && fMichsaYomit > 0 && _oGeneralData.objMeafyeneyOved.GetMeafyen(33).IntValue == 1 && clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is null and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur ASC").Length > 0)
                  {
                      fSumDakotRechiv = 1;
                  }
                  addRowToTable(clGeneral.enRechivim.YomHeadrutNoDivuach.GetHashCode(), fSumDakotRechiv);
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YomHeadrutNoDivuach.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv250()
          {
              float fSumDakotRechiv, fDakotTafkidChol, fTafkidShishi;
              try
              {
                  fSumDakotRechiv = 0;
                  fDakotTafkidChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotTafkid.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fTafkidShishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fSumDakotRechiv = fTafkidShishi + fDakotTafkidChol;
                  addRowToTable(clGeneral.enRechivim.SachNosafotTafkidCholVeshishi.GetHashCode(), fSumDakotRechiv);
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachNosafotTafkidCholVeshishi.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv251()
          {
              float fSumDakotRechiv, fDakotTnuaChol, fTnuaShishi;
              try
              {
                  fSumDakotRechiv = 0;
                  fDakotTnuaChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotNihul.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fTnuaShishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fSumDakotRechiv = fDakotTnuaChol + fTnuaShishi;
                  addRowToTable(clGeneral.enRechivim.SachNosafotTnuaCholVeshishi.GetHashCode(), fSumDakotRechiv);
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachNosafotTnuaCholVeshishi.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv252()
          {
              float fSumDakotRechiv, fDakotNahagutChol, fNahagutShishi;
              try
              {
                  fSumDakotRechiv = 0;
                  fDakotNahagutChol = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNosafotNahagut.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fNahagutShishi = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fSumDakotRechiv = fNahagutShishi + fDakotNahagutChol;
                  addRowToTable(clGeneral.enRechivim.SachNosafotNahagutCholVeshishi.GetHashCode(), fSumDakotRechiv);
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.SachNosafotNahagutCholVeshishi.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv256()
          {
              float fSumRechiv, fZmanAruchatTzharayim;
              int  iErevRechiv;
              try
              {
                  
                      oSidur.CalcRechiv256( out fZmanAruchatTzharayim, out  iErevRechiv);

                      //if (fZmanAruchatTzharayim > 30)
                      //{ fZmanAruchatTzharayim = 30; }
                    

                      fSumRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                      fZmanAruchatTzharayim = Math.Min(fZmanAruchatTzharayim, 30 - clCalcData.fTotalAruchatZaharimForDay); ;
                      fSumRechiv = fSumRechiv - fZmanAruchatTzharayim;
                      clCalcData.fTotalAruchatZaharimForDay += fZmanAruchatTzharayim;
 
                      addRowToTable(clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode(), fSumRechiv);

                  
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv257()
          {
              float fSumRechiv, fZmanAruchatTzharayim;
              int iErevRechiv;
              try
              {

                  oSidur.CalcRechiv257(out fZmanAruchatTzharayim, out  iErevRechiv);

                  //if (fZmanAruchatTzharayim > 30)
                  //{ fZmanAruchatTzharayim = 30; }

                  fSumRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLepremiaMetachnenTnua.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fZmanAruchatTzharayim = Math.Min(fZmanAruchatTzharayim, 30 - clCalcData.fTotalAruchatZaharimForDay); ;
                  fSumRechiv = fSumRechiv - fZmanAruchatTzharayim;
                  clCalcData.fTotalAruchatZaharimForDay += fZmanAruchatTzharayim;

                  addRowToTable(clGeneral.enRechivim.NochehutLepremiaMetachnenTnua.GetHashCode(), fSumRechiv);


              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv258()
          {
              float fSumRechiv, fZmanAruchatTzharayim;
              int iErevRechiv;
              try
              {

                  oSidur.CalcRechiv258( out fZmanAruchatTzharayim, out  iErevRechiv);


                  //if (fZmanAruchatTzharayim > 30)
                  //{ fZmanAruchatTzharayim = 30; }


                  fSumRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLepremiaSadran.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                  fZmanAruchatTzharayim = Math.Min(fZmanAruchatTzharayim, 30 - clCalcData.fTotalAruchatZaharimForDay); ;
                  fSumRechiv = fSumRechiv - fZmanAruchatTzharayim;
                  clCalcData.fTotalAruchatZaharimForDay += fZmanAruchatTzharayim;

                  addRowToTable(clGeneral.enRechivim.NochehutLepremiaSadran.GetHashCode(), fSumRechiv);


              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }


          private void CalcRechiv259()
          {
              float fSumRechiv, fZmanAruchatTzharayim;
              int iErevRechiv;
              try
              {

                  oSidur.CalcRechiv259( out fZmanAruchatTzharayim, out  iErevRechiv);

                  //if (fZmanAruchatTzharayim > 30)
                  //{ fZmanAruchatTzharayim = 30; }

                  fSumRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLepremiaRakaz.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                  fZmanAruchatTzharayim = Math.Min(fZmanAruchatTzharayim, 30 - clCalcData.fTotalAruchatZaharimForDay); ;
                  fSumRechiv = fSumRechiv - fZmanAruchatTzharayim;
                  clCalcData.fTotalAruchatZaharimForDay += fZmanAruchatTzharayim;

                  addRowToTable(clGeneral.enRechivim.NochehutLepremiaRakaz.GetHashCode(), fSumRechiv);


              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv260()
          {
              float fSumRechiv, fZmanAruchatTzharayim;
              int iErevRechiv;
              try
              {

                  oSidur.CalcRechiv260( out fZmanAruchatTzharayim, out  iErevRechiv);

                  //if (fZmanAruchatTzharayim > 30)
                  //{ fZmanAruchatTzharayim = 30; }

                  fSumRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.NochehutLepremiaPakach.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                  fZmanAruchatTzharayim = Math.Min(fZmanAruchatTzharayim, 30 - clCalcData.fTotalAruchatZaharimForDay); ;
                  fSumRechiv = fSumRechiv - fZmanAruchatTzharayim;
                  clCalcData.fTotalAruchatZaharimForDay += fZmanAruchatTzharayim;

                  addRowToTable(clGeneral.enRechivim.NochehutLepremiaPakach.GetHashCode(), fSumRechiv);


              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.NochehutLepremiaPakach.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }
          private void CalcRechiv261()
          {
              float fSumDakotRechiv, fMachala;
              try
              {
                  fSumDakotRechiv = 0;
                  fMachala = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachla.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  if (fMachala==1)
                      fSumDakotRechiv = fMachala;
                  addRowToTable(clGeneral.enRechivim.MachalaYomMale.GetHashCode(), fSumDakotRechiv);
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.MachalaYomMale.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv262()
          {
              float fSumDakotRechiv, fMachala;
              try
              {
                  fSumDakotRechiv = 0;
                  fMachala = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachla.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  if (fMachala > 0 && fMachala<1)
                      fSumDakotRechiv = 1;
                  addRowToTable(clGeneral.enRechivim.MachalaYomChelki.GetHashCode(), fSumDakotRechiv);
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.MachalaYomChelki.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv263()
          {
              float fSumDakotRechiv;
              try
              {
                  if (_oGeneralData.objPirteyOved.iDirug != 85 || _oGeneralData.objPirteyOved.iDarga != 30)
                  {
                      oSidur.CalcRechiv263();
                      fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.HashlamaBenahagut.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                      addRowToTable(clGeneral.enRechivim.HashlamaBenahagut.GetHashCode(), fSumDakotRechiv);
                  }
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.HashlamaBenahagut.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv264()
          {
              float fSumDakotRechiv;
              try
              {
                  if (_oGeneralData.objPirteyOved.iDirug != 85 || _oGeneralData.objPirteyOved.iDarga != 30)
                  {
                      oSidur.CalcRechiv264();
                      fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.HashlamaBenihulTnua.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                      addRowToTable(clGeneral.enRechivim.HashlamaBenihulTnua.GetHashCode(), fSumDakotRechiv);
                  }
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.HashlamaBenihulTnua.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv265()
          {
              float fSumDakotRechiv;
              try
              {
                  if (_oGeneralData.objPirteyOved.iDirug != 85 || _oGeneralData.objPirteyOved.iDarga != 30)
                  {
                      oSidur.CalcRechiv265();
                      fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.HashlamaBetafkid.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                      addRowToTable(clGeneral.enRechivim.HashlamaBetafkid.GetHashCode(), fSumDakotRechiv);
                  }
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.HashlamaBetafkid.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }


          private void CalcRechiv266()
          {
              float fErechRechiv, fMichsaYomit, fTempW, fTempX;
              try
              {
                  fTempX = 0;
                  fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  if (fMichsaYomit > 0)
                  {
                      fErechRechiv = oSidur.CalcRechiv266();
                      if (fErechRechiv > 0)
                      {
                          fTempX = fErechRechiv;

                          fTempW = 1;

                          if (_objMefyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                          {
                              fTempW = clCalcData.fMekademNipuach;
                          }

                          fErechRechiv = float.Parse(Math.Round(fTempW * fTempX, 2).ToString());
                          addRowToTable(clGeneral.enRechivim.YomMiluimChelki.GetHashCode(), fErechRechiv);
                      }
                  }

              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YomMiluimChelki.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv267()
          {
              float fSumDakotRechiv;
              try
              {
                  oSidur.CalcRechiv267();
                  fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotElementim.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  addRowToTable(clGeneral.enRechivim.DakotElementim.GetHashCode(), fSumDakotRechiv);

              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotElementim.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv268()
          {
              float fSumDakotRechiv;
              try
              {
                  oSidur.CalcRechiv268();
                  fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNesiaLepremia.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  addRowToTable(clGeneral.enRechivim.DakotNesiaLepremia.GetHashCode(), fSumDakotRechiv);

              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotNesiaLepremia.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv269()
          {
              float fErechRechiv = 0;
              float fMichsaYomit, fDakotChofesh, fDakotHeadrut;
              try
              {
                  fDakotChofesh = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotChofesh.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fDakotHeadrut = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotHeadrut.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fErechRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalatBenZug.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fErechRechiv = fErechRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalatHorim.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fErechRechiv = fErechRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalaYeled.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fErechRechiv = fErechRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachla.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fErechRechiv = fErechRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalaBoded.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fErechRechiv = fErechRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomEvel.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fErechRechiv = fErechRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMiluim.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fErechRechiv = fErechRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomTeuna.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fErechRechiv = fErechRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomShmiratHerayon.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fErechRechiv = fErechRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMiluimChelki.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fErechRechiv = fDakotChofesh + fDakotHeadrut + (fErechRechiv * fMichsaYomit);

                 addRowToTable(clGeneral.enRechivim.DakotChofeshHeadrut.GetHashCode(), fErechRechiv);
                }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.DakotChofeshHeadrut.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv270()
          {
              float fErechRechiv = 0;
              try
              {
                 fErechRechiv = fErechRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomChofesh.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fErechRechiv = fErechRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomHeadrut.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fErechRechiv = fErechRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalatBenZug.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fErechRechiv = fErechRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalatHorim.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fErechRechiv = fErechRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalaYeled.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fErechRechiv = fErechRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalaBoded.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fErechRechiv = fErechRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomEvel.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fErechRechiv = fErechRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMiluim.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fErechRechiv = fErechRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomTeuna.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fErechRechiv = fErechRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomShmiratHerayon.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fErechRechiv = fErechRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMiluimChelki.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  fErechRechiv = fErechRechiv + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachla.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
               
                  addRowToTable(clGeneral.enRechivim.YemeyChofeshHeadrut.GetHashCode(), fErechRechiv);
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.YemeyChofeshHeadrut.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv271()
          {
              float fSumDakotRechiv;
              try
              {
                  if (!(_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30))
                  {
                      if ((_oGeneralData.objPirteyOved.iKodMaamdMishni != clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode()) ||
                           (_oGeneralData.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode() &&
                           (_oGeneralData.objPirteyOved.iIsuk == 122 || _oGeneralData.objPirteyOved.iIsuk == 123 || _oGeneralData.objPirteyOved.iIsuk == 124 || _oGeneralData.objPirteyOved.iIsuk == 127)))
                      {
                          //fTempX = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot125.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                          //fTempX = fTempX + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Shaot25.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                          //fTempX = fTempX + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Nosafot150.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                          //fTempX = fTempX + clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.Shaot50.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));

                          //if (fTempX > 0)
                          //{
                          oSidur.CalcRechiv271();
                          fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanLilaSidureyBoker.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                          fSumDakotRechiv += clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutBoker.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                          addRowToTable(clGeneral.enRechivim.ZmanLilaSidureyBoker.GetHashCode(), fSumDakotRechiv);
                          //}
                      }
                  }
              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.ZmanLilaSidureyBoker.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv931()
          {
              float fSumDakotRechiv;
              try
              {
                  oSidur.CalcRechiv931();
                  fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.HalbashaTchilatYom.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  addRowToTable(clGeneral.enRechivim.HalbashaTchilatYom.GetHashCode(), fSumDakotRechiv);

              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.HalbashaTchilatYom.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private void CalcRechiv932()
          {
              float fSumDakotRechiv;
              try
              {
                  oSidur.CalcRechiv932();
                  fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.HalbashaSofYom.GetHashCode().ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')"));
                  addRowToTable(clGeneral.enRechivim.HalbashaSofYom.GetHashCode(), fSumDakotRechiv);

              }
              catch (Exception ex)
              {
                  clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", clGeneral.enRechivim.HalbashaSofYom.GetHashCode(), _Taarich, "CalcDay: " + ex.Message);
                  throw (ex);
              }
          }

          private int CheckSugMishmeret()
          {
              int iSugMishmeret=0;
              DateTime dShatHatchala, dShatGmar;
              DataRow[] dr;
              int iSugYomLemichsa;
              try
              {
                  dr = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and MISPAR_SIDUR=99001 and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur ASC");
                  if (dr.Length > 0)
                  {
                      dShatHatchala = DateTime.Parse(dr[0]["SHAT_HATCHALA_LETASHLUM"].ToString());
                      dShatGmar = DateTime.Parse(dr[dr.Length - 1]["SHAT_GMAR_LETASHLUM"].ToString());
                      iSugYomLemichsa = _oGeneralData.GetSugYomLemichsa(_iMisparIshi, _Taarich);
                      iSugMishmeret = clDefinitions.GetSugMishmeret(_iMisparIshi, _Taarich,iSugYomLemichsa, dShatHatchala, dShatGmar, _oGeneralData.objParameters);
                  }
                  return iSugMishmeret;
              }
                catch (Exception ex)
              {
                  throw ex;
              }
          }

          private bool CheckShishiMuchlaf(int iSugYom,DateTime dTaarich)
          {
             bool bShishiMuchlaf = false;
             
              if (clCalcData.DtSugeyYamimMeyuchadim.Select("sug_yom=" + iSugYom)[0]["Shishi_Muhlaf"].ToString() == "1")
              {
                  bShishiMuchlaf = true;
              }

              return bShishiMuchlaf;
          }

          private void addRowToTable(int iKodRechiv, float fErechRechiv)
          {
              DataRow drChishuv;

              if (_dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + iKodRechiv.ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')").Length == 0)
              {
                  if (fErechRechiv > 0)
                  {
                      drChishuv = _dtChishuvYom.NewRow();
                      drChishuv["BAKASHA_ID"] = _lBakashaId;
                      drChishuv["MISPAR_ISHI"] = _iMisparIshi;
                      drChishuv["TAARICH"] = _Taarich;
                      drChishuv["TKUFA"] = _Taarich.ToString("MM/yyyy");
                      drChishuv["KOD_RECHIV"] = iKodRechiv;
                      drChishuv["ERECH_RECHIV"] = fErechRechiv;
                      drChishuv["ERECH_EZER"] = System.DBNull.Value;
                      _dtChishuvYom.Rows.Add(drChishuv);
                  }
              }
              else
              {
                  UpdateRowInTable(iKodRechiv, fErechRechiv, 0);
              }

          }

        private void addRowToTable(int iKodRechiv, float fErechRechiv, float fErechEzer)
        {
            DataRow drChishuv;
            if (fErechRechiv > 0)
            {
                if (_dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + iKodRechiv.ToString() + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')").Length == 0)
                {
                    drChishuv = _dtChishuvYom.NewRow();
                    drChishuv["BAKASHA_ID"] = _lBakashaId;
                    drChishuv["MISPAR_ISHI"] = _iMisparIshi;
                    drChishuv["TAARICH"] = _Taarich;
                    drChishuv["TKUFA"] = _Taarich.ToString("MM/yyyy");
                    drChishuv["KOD_RECHIV"] = iKodRechiv;
                    drChishuv["ERECH_RECHIV"] = fErechRechiv;
                    drChishuv["ERECH_EZER"] = fErechEzer;
                    _dtChishuvYom.Rows.Add(drChishuv);
                }
                else
                {
                    UpdateRowInTable(iKodRechiv, fErechRechiv, fErechEzer);
                }
            }
        }

        private void UpdateRowInTable(int iKodRechiv, float fErechRechiv, float fErechEzer)
        {
            DataRow drChishuv;
            drChishuv = _dtChishuvYom.Select("KOD_RECHIV=" + iKodRechiv + " and taarich=Convert('" + _Taarich.ToShortDateString() + "', 'System.DateTime')")[0];
            drChishuv["ERECH_RECHIV"] = fErechRechiv;
            if (fErechEzer > 0)
            {
                drChishuv["ERECH_EZER"] = fErechEzer;
            }

        }

 
    }
}
