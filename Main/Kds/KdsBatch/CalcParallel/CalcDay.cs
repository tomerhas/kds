using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.UDT;
using KdsLibrary;

namespace KdsBatch
{
    class CalcDay
    {
        private Oved objOved;
        private CalcPeilut oPeilut;
        private CalcSidur oSidur;
      //  public int SugYom { get; set; }
        private clCalcBL oCalcBL;

        private DataTable _dtChishuvYom;
        private DateTime dShatHatchalaYomAvoda;

        public CalcDay(Oved oOved)
        {

            objOved = oOved;
            _dtChishuvYom = objOved._dsChishuv.Tables["CHISHUV_YOM"];
            oPeilut = new CalcPeilut(objOved);
            oSidur = new CalcSidur(objOved);          
            oCalcBL = new clCalcBL();
            }

        internal void CalcRechivim()
        {
            try
            {
                if (objOved.DtYemeyAvodaYomi.Rows[0]["SHAT_HATCHALA"].ToString() != "")
                    dShatHatchalaYomAvoda = DateTime.Parse(objOved.DtYemeyAvodaYomi.Rows[0]["SHAT_HATCHALA"].ToString());
    
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

                //דקות נוכחות בפועל(רכיב 18 
                CalcRechiv18();

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

                // מכסת שעות נוספות תפקיד חול (רכיב 143
                CalcRechiv143();

                //יום הסבה לקו (רכיב 72) : 
                CalcRechiv72();

                // יום שליחות בחו''ל
                CalcRechiv73();

                //ימי נוכחות לעובד (רכיב 75) 
                CalcRechiv75();

               
                //זמן להמרת שעות שבת (רכיב 53) 
                CalcRechiv53();

                //חופש זכות (רכיב 132):
                CalcRechiv132();

                //דקות כיסוי תור (רכיב 218
                CalcRechiv218();

                //דקות פרמיה בשבת  (רכיב 26 ) 
                CalcRechiv26();

                ////דקות פרמיה יומית  (רכיב 30)
                CalcRechiv30();

                //דקות פרמיה יומית  (רכיב 30)
                //CalcRechiv30_2();

                //דקות פרמיה בשישי  (רכיב 202 ) 
                CalcRechiv202();

                //פרמיה רגילה (רכיב 133): 
                CalcRechiv133();

                //משמרת שנייה במשק (רכיב 125) 
                CalcRechiv125();

                //קיזוז דקות התחלה-גמר  (רכיב 86):
                CalcRechiv86();

                //CalcRechiv87();

                //קיזוז זמן בויזות (רכיב 89) 
                //CalcRechiv89();

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

                //נוכחות לפרמיה - רישום ( רכיב 209): 
                CalcRechiv209();

                //סהכ תפקיד (רכיב 192):  
                CalcRechiv192();

                //תוספת רציפות 1-1(נהגות)   - רכיב 96:  
                CalcRechiv96();

                //תוספת זמן הלבשה (רכיב 93)
                CalcRechiv93();

                //דקות לתוספת מיוחדת בתפקיד  - תמריץ (רכיב 10) 
                CalcRechiv10();

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

                //כמות גמול חסכון נוספות (רכיב 44) 
                CalcRechiv44();

                //כמות גמול חסכון (רכיב 22) 
                CalcRechiv22();

                UpdateRechiv1();

                //קיזוז לעובד מותאם (רכיב 90
                CalcRechiv90();

                //יום היעדרות  (רכיב 66) 
                CalcRechiv66();

                //דקות היעדרות (רכיב 220) 
                CalcRechiv220();

                // שעות היעדרות ( רכיב 5) 
                CalcRechiv5();
                //דקות רגילות (רכיב 32
                CalcRechiv32();

                //ימי נוכחות לעובד (רכיב 109)
                CalcRechiv109();

                //דקות פרמיה בתוך המכסה  (רכיב 27) 
                CalcRechiv27();


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

                //נוספות 100% לעובד חודשי (רכיב 146 ) 
                CalcRechiv146();

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

                ////טיפול בסידור ועד עובדים (99008)
                //SetSidureyVaadOvdim();

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

                //נוכחות לפרמיה – משק כוננות גרירה (277)
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

                //אגד תעבורה פער בין מכסה רגילה למוקטנת(רכיב 278)
                CalcRechiv278();

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", 0,objOved.Taarich, "CalcRechivim: " + ex.StackTrace + "\n message: "+ ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        public void SetNullObjects()
        {
            oPeilut = null;
            oSidur.SetNullObject();
            oSidur = null;
            oCalcBL = null;
        }


        private void CalcRechiv1()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv1();

               // fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);

                addRowToTable(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), fSumDakotRechiv, fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void UpdateRechiv1()
        {
            float fSumDakotRechiv,fDakotNochechut,fMichsaYomit, fZmanNesia, fDakotNocheut, fTemp, fShaotshabat100, fZmanNesiot;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNochehutLetashlum);  //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);
                fDakotNochechut= oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNochehutLetashlum);  //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);

                fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanHashlama);  // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanHashlama.GetHashCode(), objOved.Taarich); 
                fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanHalbasha);  //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanHalbasha.GetHashCode(), objOved.Taarich); 
                fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.HashlamaBenihulTnua);  //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.HashlamaBenihulTnua.GetHashCode(), objOved.Taarich); 
                fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.HashlamaBetafkid);  //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.HashlamaBetafkid.GetHashCode(), objOved.Taarich); 
                fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.HashlamaBenahagut);  //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.HashlamaBenahagut.GetHashCode(), objOved.Taarich); 

                fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanRetzifutNehiga);  //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode(), objOved.Taarich);  
                fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanRetzifutTafkid);  //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode(), objOved.Taarich); 

                //fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode(), objOved.Taarich);
                //fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode(), objOved.Taarich);

                fSumDakotRechiv = fSumDakotRechiv - oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanAruchatTzaraim);  //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode(), objOved.Taarich); 
              //  fSumDakotRechiv = fSumDakotRechiv - oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.KizuzBevisa.GetHashCode(), objOved.Taarich); 
              //  fSumDakotRechiv = fSumDakotRechiv - oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.KizuzOvedMutam.GetHashCode(), objOved.Taarich); 

                fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);  

                fZmanNesia = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanNesia.GetHashCode(), objOved.Taarich);  

                if (fZmanNesia > 0 && (fMichsaYomit - fSumDakotRechiv) > 0)
                {
                    fTemp = Math.Min(fZmanNesia, (fMichsaYomit - fSumDakotRechiv));
                    fSumDakotRechiv = fSumDakotRechiv + fTemp;
                    addRowToTable(clGeneral.enRechivim.ZmanNesia.GetHashCode(), fZmanNesia - fTemp);
                }

                if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                {
                    if (fSumDakotRechiv < fMichsaYomit && (fMichsaYomit - fSumDakotRechiv) <= 2)
                        fSumDakotRechiv = fMichsaYomit;
                }

                addRowToTable(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), fSumDakotRechiv, fDakotNochechut);

                //עדכון רכיב 131
                fDakotNocheut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);

                fZmanNesiot = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanNesia.GetHashCode(), objOved.Taarich); 

                if (fZmanNesiot > 0)
                {
                    fShaotshabat100 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ShaotShabat100.GetHashCode(), objOved.Taarich); 

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
            float fErechRechiv1, fErechRechiv126, fErechRechiv2, fErechRechiv133, fErechRechiv134, fErechRechiv30, fErechRechiv28;
            float ftosefetMax28,ftosefetMax30,ftosefetMax30_2, fErechRechiv1New, fErechRechiv1New2;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                fErechRechiv1 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNochehutLetashlum); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich); 
                fErechRechiv2 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNehigaChol); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNehigaChol.GetHashCode(), objOved.Taarich);  
                fErechRechiv126 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichsaYomitMechushevet); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich); 
                
                fErechRechiv133 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.PremyaRegila); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.PremyaRegila.GetHashCode(), objOved.Taarich); 
                fErechRechiv134 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.PremyaNamlak); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.PremyaNamlak.GetHashCode(), objOved.Taarich); 
                
                fErechRechiv30 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotPremiaYomit); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.PremyaNamlak.GetHashCode(), objOved.Taarich); 
                fErechRechiv28 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotPremiaVisa); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.PremyaNamlak.GetHashCode(), objOved.Taarich); 
                
                if ((fErechRechiv1 < fErechRechiv126) && (fErechRechiv2 >= (fErechRechiv126 / 2)) &&
                      ((fErechRechiv126 - fErechRechiv1) <= 30) && ((fErechRechiv28 + fErechRechiv30) > 0))
                {
                    ftosefetMax30 = fErechRechiv30 > 30 ? 30 : fErechRechiv30;
                    ftosefetMax28 = fErechRechiv28 > 30 ? 30 : fErechRechiv28;

                    fErechRechiv1New = Math.Min(fErechRechiv126, fErechRechiv1 + ftosefetMax28);
                    addRowToTable(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), fErechRechiv1New);

                    fErechRechiv28 = fErechRechiv28 - (Math.Min(ftosefetMax28, fErechRechiv126 - fErechRechiv1));
                    fErechRechiv134 = fErechRechiv134 - (Math.Min(ftosefetMax28, fErechRechiv126 - fErechRechiv1));
                    addRowToTable(clGeneral.enRechivim.DakotPremiaVisa.GetHashCode(), fErechRechiv28);
                    addRowToTable(clGeneral.enRechivim.PremyaNamlak.GetHashCode(), fErechRechiv134);

                    if (fErechRechiv1New < fErechRechiv126)
                    {
                        ftosefetMax30_2 = Math.Min(fErechRechiv30, ((30 - fErechRechiv28) < 0 ? 0 : (30 - fErechRechiv28)));
                        fErechRechiv1New2 = Math.Min(fErechRechiv126, fErechRechiv1New + ftosefetMax30_2);
                        addRowToTable(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), fErechRechiv1New2);

                        fErechRechiv30 = fErechRechiv30 - (Math.Min(ftosefetMax30_2, fErechRechiv126 - fErechRechiv1New));
                        fErechRechiv133 = fErechRechiv133 - (Math.Min(ftosefetMax30_2, fErechRechiv126 - fErechRechiv1New));
                        addRowToTable(clGeneral.enRechivim.DakotPremiaYomit.GetHashCode(), fErechRechiv30);
                        addRowToTable(clGeneral.enRechivim.PremyaRegila.GetHashCode(), fErechRechiv133);
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
            try
            {
                oSidur.CalcRechiv2();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotNehigaChol.GetHashCode(), objOved.Taarich);  
                if (fSumDakotRechiv > 0)
                {
                    Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                    fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanHashlama); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanHashlama.GetHashCode(), objOved.Taarich); 
                    fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanRetzifutTafkid); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode(), objOved.Taarich);  
                    fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.HashlamaBenahagut); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.HashlamaBenahagut.GetHashCode(), objOved.Taarich); 
                    addRowToTable(clGeneral.enRechivim.DakotNehigaChol.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotNehigaChol.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv3()
        {
            float fSumDakotRechiv, fDakotNehigaChol;
            try
            {
                oSidur.CalcRechiv3();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode(), objOved.Taarich);
                if (fSumDakotRechiv > 0)
                {
                    fDakotNehigaChol = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNehigaChol.GetHashCode(), objOved.Taarich);  
                    if (fDakotNehigaChol == 0)
                        fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanHashlama.GetHashCode(), objOved.Taarich); 
                        
                    fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.HashlamaBenihulTnua.GetHashCode(), objOved.Taarich); 
                    addRowToTable(clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv4()
        {
            float fSumDakotRechiv, fDakotNehigaChol, fDakotNihulChol;
            try
            {
                oSidur.CalcRechiv4();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), objOved.Taarich);
                if (fSumDakotRechiv > 0)
                {
                    Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                    fDakotNehigaChol = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNehigaChol); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNehigaChol.GetHashCode(), objOved.Taarich);
                    fDakotNihulChol = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNihulTnuaChol); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode(), objOved.Taarich);
                    if (fDakotNehigaChol == 0 && fDakotNihulChol == 0)
                        fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanHashlama.GetHashCode(), objOved.Taarich);

                    fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.HashlamaBetafkid); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.HashlamaBetafkid.GetHashCode(), objOved.Taarich);  
                   // fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanGrirot); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanGrirot.GetHashCode(), objOved.Taarich);  
                    fSumDakotRechiv = fSumDakotRechiv - oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanAruchatTzaraim); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode(), objOved.Taarich);

                    addRowToTable(clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
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
            try
            {
                //iSugYom = clCalcGeneral.iSugYom;
                //bErevChag=clCalcGeneral.CheckErevChag(iSugYom);
                //bErevShabat=clCalcGeneral.CheckYomShishi(iSugYom);

                //fMichsaYomit126 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);
                // drMichsaYomit = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')");

                // //ביום שישי/ערב חג [זיהוי ערב חג (תאריך)] כאשר עבור רכיב מכסה יומית מחושבת לא קיימת רשומה או ערכו = 0 אין לפתוח רשומה לרכיב זה.   
                //if (!bErevChag && !bErevShabat || ((bErevChag || bErevShabat) && fMichsaYomit126>0 && drMichsaYomit.Length>0))
                //   { 
                //       if (objOved.objMeafyeneyOved.iMeafyen33  == 40)
                //        {
                //           oSidur.CalcRechiv5();

                //            fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.ShaotHeadrut.GetHashCode(), objOved.Taarich);
                //            if (fSumDakotRechiv != 0)
                //            {
                //                addRowToTable(clGeneral.enRechivim.ShaotHeadrut.GetHashCode(), fSumDakotRechiv);
                //            }
                //        }
                //       else if (objOved.objMeafyeneyOved.iMeafyen33 == 41)
                //       {
                //           fDakotNochehut1 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);
                //           drDakotNochehut = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')");
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
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotHeadrut.GetHashCode(), objOved.Taarich);
                if (fSumDakotRechiv > 0)
                {
                    fSumDakotRechiv = fSumDakotRechiv / 60;
                    if (fSumDakotRechiv != 0)
                    {
                        addRowToTable(clGeneral.enRechivim.ShaotHeadrut.GetHashCode(), fSumDakotRechiv);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ShaotHeadrut.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv7()
        {
            float fSumDakotRechiv, fTosefetRezifut96;
            try
            {
                if (objOved.objPirteyOved.iDirug != 85 || objOved.objPirteyOved.iDarga != 30)
                {
                    oSidur.CalcRechiv7();
                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode(), objOved.Taarich);
                    if (fSumDakotRechiv > 0)
                    {
                        fTosefetRezifut96 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode(), objOved.Taarich);
                        addRowToTable(clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode(), fSumDakotRechiv + fTosefetRezifut96);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv10()
        {
            float fTempX, fMichsaYomit, fDakotRechiv;
            try
            {
                if (objOved.objPirteyOved.iDirug != 85 || objOved.objPirteyOved.iDarga != 30)
                {

                    if (objOved.objMeafyeneyOved.iMeafyen54 > 0)
                    {
                        oSidur.CalcRechiv10();

                        fTempX = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotTamritzTafkid.GetHashCode(), objOved.Taarich);
                        if (fTempX>0)
                            fTempX += oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanHalbasha.GetHashCode(), objOved.Taarich);  
                        fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich); 

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
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotTamritzTafkid.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv12()
        {
            float fSumDakotRechiv, fMichsaYomit126, fTempDakot,fTempZ;
            try
            {
                oSidur.CalcRechiv12();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotTosefetMeshek.GetHashCode(), objOved.Taarich); 
               
                if (fSumDakotRechiv > 0)
                {
                    fMichsaYomit126 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);
                    if (fMichsaYomit126 > 0)
                        fTempZ = 120;
                    else
                    {
                        if (objOved.SugYom == clGeneral.enDay.Shabat.GetHashCode() || clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                            fTempZ = 0;
                        else fTempZ = 240;
                     }

                    if (fSumDakotRechiv <= fMichsaYomit126)
                    {
                        fSumDakotRechiv = (fSumDakotRechiv * objOved.objParameters.fAchuzTosefetLeovdeyMeshek) / 100;
                    }
                    else
                    {
                        fTempDakot = fSumDakotRechiv - fMichsaYomit126;
                        if (fTempDakot < fTempZ)
                        {
                            fSumDakotRechiv = ((fMichsaYomit126 * objOved.objParameters.fAchuzTosefetLeovdeyMeshek) / 100) + (fTempDakot * float.Parse("1.25") * objOved.objParameters.fAchuzTosefetLeovdeyMeshek) / 100;

                        }
                        else
                        {
                            fSumDakotRechiv = ((fMichsaYomit126 * objOved.objParameters.fAchuzTosefetLeovdeyMeshek) / 100) + (fTempZ * float.Parse("1.25") * objOved.objParameters.fAchuzTosefetLeovdeyMeshek / 100) + (fTempDakot - fTempZ) * float.Parse("1.5") * objOved.objParameters.fAchuzTosefetLeovdeyMeshek / 100;
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
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotTosefetMeshek.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv13()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv13();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotMachlifMeshaleach.GetHashCode(), objOved.Taarich); 
                if (fSumDakotRechiv > 0)
                {

                    addRowToTable(clGeneral.enRechivim.DakotMachlifMeshaleach.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMachlifMeshaleach.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }



        private void CalcRechiv14()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv14();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotMachlifSadran.GetHashCode(), objOved.Taarich); 
                if (fSumDakotRechiv > 0)
                {
                    addRowToTable(clGeneral.enRechivim.DakotMachlifSadran.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMachlifSadran.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv15()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv15();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotMachlifPakach.GetHashCode(), objOved.Taarich); 
                if (fSumDakotRechiv > 0)
                {
                    addRowToTable(clGeneral.enRechivim.DakotMachlifPakach.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMachlifPakach.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv16()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv16();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotMachlifKupai.GetHashCode(), objOved.Taarich); 
                addRowToTable(clGeneral.enRechivim.DakotMachlifKupai.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMachlifKupai.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }

        }

        private void CalcRechiv17()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv17();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotMachlifRakaz.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.DakotMachlifRakaz.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMachlifRakaz.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }

        }

        private void CalcRechiv18()
        {
            float fSumDakotRechiv,fDakotNochechutLeTashlum;
            try
            {
                fDakotNochechutLeTashlum = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);
                if (fDakotNochechutLeTashlum > 0)
                {
                    oSidur.CalcRechiv18();
                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode(), objOved.Taarich);
                    addRowToTable(clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv19_20_21()
        {
            float fSumDakotRechiv19, fTempX, fMichsaYomit,fDakotNochechut, fDakotNahagutChol, fDakotNihulChol, fDakotTafkidChol, fSumDakotRechiv20, fSumDakotRechiv21;
            int iCountNihulTnua, iCountNahagutChol, iCountTafkidChol;
            fSumDakotRechiv19 = 0;
            fSumDakotRechiv20 = 0;
            fSumDakotRechiv21 = 0;
            int kodMutamut=0;
            try
            {
                if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                {
                    if (objOved.objPirteyOved.iMutamut>0)
                        kodMutamut = int.Parse(objOved.oGeneralData.dtMutamutAll.Select("KOD_MUTAMUT=" + objOved.objPirteyOved.iMutamut)[0]["kod_mutamut"].ToString());

                    //if (!oCalcBL.CheckIsurShaotNosafot(objOved.objPirteyOved, objOved.oGeneralData.dtMutamutAll))
                    if (kodMutamut != 1 && kodMutamut != 3 && kodMutamut != 5 && kodMutamut != 7)
                    {
                        //if (objOved.objMeafyeneyOved.iMeafyen2 == 0)
                        //{
                            Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                            fTempX = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNochehutLetashlum);  
                           // fTempX = fTempX - oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ShaotShabat100);  
                           // fTempX = fTempX - oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotZikuyChofesh);
                            //fTempX = fTempX - oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotShabat); 

                            fMichsaYomit = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichsaYomitMechushevet);  
                            fDakotNochechut = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNochehutLetashlum);  
                            if (fMichsaYomit>0 && fDakotNochechut>0)
                            {
                                objOved._dsChishuv.Tables["CHISHUV_YOM"].Select(null, "KOD_RECHIV");
                                iCountNihulTnua = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')").Length;
                                iCountNahagutChol = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaChol.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')").Length;
                                iCountTafkidChol = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidChol.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')").Length;

                                if (fTempX > fMichsaYomit)
                                {
                                    fDakotTafkidChol = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotTafkidChol); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), objOved.Taarich);
                                    //fDakotTafkidChol = fDakotTafkidChol - oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanNesia); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanNesia.GetHashCode(), objOved.Taarich); 
                                    if (fDakotTafkidChol > 0 && (iCountNihulTnua == 0 && iCountNahagutChol == 0))
                                    {
                                        fSumDakotRechiv21 = fTempX - fMichsaYomit;
                                    }


                                    fDakotNihulChol = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNihulTnuaChol); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode(), objOved.Taarich); 
                                    if (fDakotNihulChol > 0 && (iCountTafkidChol == 0 && iCountNahagutChol == 0))
                                    {
                                        fSumDakotRechiv20 = fTempX - fMichsaYomit;
                                    }

                                    fDakotNahagutChol = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNehigaChol); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNehigaChol.GetHashCode(), objOved.Taarich); 
                                    if (fDakotNahagutChol > 0 && iCountTafkidChol == 0 && iCountNihulTnua == 0)
                                    {
                                        fSumDakotRechiv19 = fTempX - fMichsaYomit;
                                    }

                                    //כלומר ביצע עבודת תפקיד + נהגות
                                    if (fDakotTafkidChol > 0 && fDakotNahagutChol > 0 && iCountNihulTnua == 0)
                                    {
                                        if (fDakotTafkidChol >= fMichsaYomit && fDakotTafkidChol >= fTempX)
                                        {
                                            fSumDakotRechiv21 = fTempX - fMichsaYomit;
                                        }
                                        else if (fDakotTafkidChol >= fMichsaYomit && fDakotTafkidChol < fTempX)
                                        {
                                            fSumDakotRechiv21 = fDakotTafkidChol - fMichsaYomit;
                                            fSumDakotRechiv19 = fTempX - fMichsaYomit - fSumDakotRechiv21;
                                        }
                                        else if (fDakotTafkidChol <= fMichsaYomit)
                                        {
                                            fSumDakotRechiv19 = fTempX - fMichsaYomit;
                                        }
                                    }

                                    //כלומר ביצע עבודת תנועה + נהגות
                                    if (fDakotNihulChol > 0 && fDakotNahagutChol > 0 && iCountTafkidChol == 0)
                                    {
                                        if (fDakotNihulChol >= fMichsaYomit && fDakotNihulChol >= fTempX)
                                        {
                                            fSumDakotRechiv20 = fTempX - fMichsaYomit;
                                        }
                                        else if (fDakotNihulChol >= fMichsaYomit && fDakotNihulChol < fTempX)
                                        {
                                            fSumDakotRechiv20 = fDakotNihulChol - fMichsaYomit;
                                            fSumDakotRechiv19 = fTempX - fMichsaYomit - fSumDakotRechiv20;
                                        }
                                        else if (fDakotNihulChol <= fMichsaYomit)
                                        {
                                            fSumDakotRechiv19 = fTempX - fMichsaYomit;
                                        }
                                    }

                                    //כלומר ביצע עבודת תנועה + תפקיד
                                    if (fDakotNihulChol > 0 && fDakotTafkidChol > 0 && iCountNahagutChol == 0)
                                    {
                                        if (fDakotTafkidChol >= fMichsaYomit && fDakotTafkidChol > fTempX)
                                        {
                                            fSumDakotRechiv21 = fTempX - fMichsaYomit;
                                        }
                                        else if (fDakotTafkidChol >= fMichsaYomit && fDakotTafkidChol < fTempX)
                                        {
                                            fSumDakotRechiv21 = fDakotTafkidChol - fMichsaYomit;
                                            fSumDakotRechiv20 = fTempX - fMichsaYomit - fSumDakotRechiv21;
                                        }
                                        else if (fDakotTafkidChol <= fMichsaYomit)
                                        {
                                            fSumDakotRechiv20 = fTempX - fMichsaYomit;
                                        }
                                    }


                                    //כלומר ביצע עבודת נהגות + תנועה + תפקיד
                                    if (fDakotNihulChol > 0 && fDakotTafkidChol > 0 && fDakotNahagutChol > 0)
                                    {
                                        if (fDakotTafkidChol >= fMichsaYomit && fDakotTafkidChol >= fTempX)
                                        {
                                            fSumDakotRechiv21 = fTempX - fMichsaYomit;
                                        }
                                        else if (fDakotTafkidChol >= fMichsaYomit && fDakotTafkidChol < fTempX)
                                        {
                                            fSumDakotRechiv21 = fDakotTafkidChol - fMichsaYomit;
                                            fSumDakotRechiv19 = fTempX - fMichsaYomit - fSumDakotRechiv21;
                                            if (fDakotNahagutChol < fSumDakotRechiv19)
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

                                    if (kodMutamut != 2)
                                        addRowToTable(clGeneral.enRechivim.DakotNosafotNahagut.GetHashCode(), fSumDakotRechiv19);
                                    addRowToTable(clGeneral.enRechivim.DakotNosafotNihul.GetHashCode(), fSumDakotRechiv20);
                                    //if ((objOved.objMeafyeneyOved.sMeafyen74 != "1") || (objOved.objMeafyeneyOved.sMeafyen74 == "1" && objOved.objPirteyOved.iIsuk == clGeneral.enIsukOved.Poked.GetHashCode() && oCalcBL.CheckYomShishi(objOved.SugYom)))
                                    //{
                                        addRowToTable(clGeneral.enRechivim.DakotNosafotTafkid.GetHashCode(), fSumDakotRechiv21);
                                   // }
                                }
                            }
                        //}

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotNosafotTafkid.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv147()
        {
            float fSumDakotRechiv,fDakotNosafot21,fDakotShishi193;
            try
            {

                fDakotNosafot21 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNosafotTafkid.GetHashCode(), objOved.Taarich);
                fDakotShishi193 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(), objOved.Taarich);

                fSumDakotRechiv = fDakotNosafot21 + fDakotShishi193;
                addRowToTable(clGeneral.enRechivim.KizuzNosafotTafkidChol.GetHashCode(), fSumDakotRechiv);  
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzNosafotTafkidChol.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv160()
        {
            float fSumDakotRechiv, fDakotNosafot20, fDakotShishi191;
            try
            {

                fDakotNosafot20 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNosafotNihul.GetHashCode(), objOved.Taarich);
                fDakotShishi191 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode(), objOved.Taarich);

                fSumDakotRechiv = fDakotNosafot20 + fDakotShishi191;
                addRowToTable(clGeneral.enRechivim.KizuzNosafotTnuaChol.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzNosafotTnuaChol.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv22()
        {
            //יש לפתוח רכיב רק אם העובד בעל מאפיין ביצוע [שליפת מאפיין ביצוע (קוד מאפיין=60)] עם ערך כלשהו ו/או קיים סידור מזכה לגמול 
            float fErechRechiv, fDakotNehiga,fErechRechivSidur=0, fMichsaYomit126, fNochechtLeTashlum,fMichsatMutam,fTempX;
            DataRow[] dr;
            clCalcBL oCalcBL = new clCalcBL();
            try
            {
                //יש לחשב רק בתנאים אלו:
                // אם עובד שכיר קבוע (קוד מעמד = 22 או 21) 
                // העובד חבר  (הספרה הראשונה של קוד מעמד = 1) 
                //if (objOved.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.Sachir12.GetHashCode() || objOved.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.SachirKavua.GetHashCode() || (objOved.objPirteyOved.iKodMaamdRashi == clGeneral.enMaamad.Friends.GetHashCode()))
                //{
                fMichsatMutam = 0; fErechRechiv = 0;
                dr = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur=99006");
                if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30) && !(objOved.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode() && dr.Length > 0))
                {
                    fMichsaYomit126 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);
                    fNochechtLeTashlum = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich); 
                    //יש לבדוק האם ביום העבודה בו מוגדר הסידור קיימת מכסה יומית מחושבת (רכיב 126) > 0. אם קיימת, להמשיך בחישוב. אם לא קיימת, אין לבצע את החישוב בשום רמה 
                    if (fMichsaYomit126 > 0 && fNochechtLeTashlum>0)
                    {
                        oSidur.CalcRechiv22();
                        if (objOved.objPirteyOved.iMutamut == 1 || objOved.objPirteyOved.iMutamut == 3 || objOved.objPirteyOved.iMutamut == 5 || objOved.objPirteyOved.iMutamut == 7)
                        {
                            if (objOved.objPirteyOved.iZmanMutamut > 0)
                                fMichsatMutam = objOved.objPirteyOved.iZmanMutamut;
                            else fMichsatMutam = fMichsaYomit126;

                            //fNochechtKursim = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime') AND (MISPAR_SIDUR in (99207,99007,99011) )"));
                              
                            //fNochechtLeTashlum = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);
                            //fTempX = Math.Min(fNochechtLeTashlum - fNochechtKursim, fMichsatMutam);
                            fErechRechivSidur = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.KamutGmulChisachon.GetHashCode(), objOved.Taarich);
                            //fTempX = Math.Min(fErechRechivSidur, fMichsatMutam);
                            fTempX = fErechRechivSidur;
                        }
                        else
                        {
                            
                            fTempX = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.KamutGmulChisachon.GetHashCode(), objOved.Taarich);
                              
                            //if (objOved.objMeafyeneyOved.iMeafyen60 == 0)
                            //{
                            //    oSidur.CalcRechiv22();
                            //    fTempX = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.KamutGmulChisachon.GetHashCode(), objOved.Taarich);
                            //}
                            //else
                            //{
                                //fSumDakotTafkid4 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), objOved.Taarich);
                                //fNochechtLeTashlum = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime') AND (MISPAR_SIDUR in (99207,99007,99011) )"));
                                //fTempX = fSumDakotTafkid4 - fNochechtLeTashlum;

                                //drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and SUG_AVODA=7");
                                //for (int i = 0; i < drSidurim.Length; i++)
                                //{
                                //    sum = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime') AND MISPAR_SIDUR=" + drSidurim[i]["mispar_sidur"].ToString()));
                                //    fTempX += sum;
                                //}

                                //drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)<>99");
                                //for (int i = 0; i < drSidurim.Length; i++)
                                //{
                                //    iSugSidur = int.Parse(drSidurim[i]["sug_sidur"].ToString());
                                //    if (oCalcBL.CheckSugSidur(objOved, clGeneral.enMeafyen.SugAvoda.GetHashCode(), clGeneral.enSugAvoda.Kupai.GetHashCode(), iSugSidur))
                                //    {
                                //        sum = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime') AND MISPAR_SIDUR=" + drSidurim[i]["mispar_sidur"].ToString()));
                                //        fTempX += sum;
                                //    }
                                //}

                           //}
                        }

                        if (objOved.objPirteyOved.iMutamut == 1 || objOved.objPirteyOved.iMutamut == 3 || objOved.objPirteyOved.iMutamut == 5 || objOved.objPirteyOved.iMutamut == 7)
                        {
                            if (fTempX >= 240)
                                fErechRechiv = 1;
                            //if (fNochechtLeTashlum>0 && fTempX >= fMichsatMutam)
                            //    fErechRechiv = 1;
                        }
                        if ((objOved.objPirteyOved.iMutamut != 1 && objOved.objPirteyOved.iMutamut != 3 && objOved.objPirteyOved.iMutamut != 5 && objOved.objPirteyOved.iMutamut != 7) || (fTempX < fMichsatMutam))
                        {
                                fDakotNehiga = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNehigaChol.GetHashCode(), objOved.Taarich);

                                if (fTempX >= objOved.objParameters.iMinDakotGmulLeloNehiga && fDakotNehiga == 0) // || objOved.objPirteyOved.iMutamut == 1 || objOved.objPirteyOved.iMutamut == 3))
                                {
                                    fErechRechiv = 1;
                                }

                                if (fTempX >= objOved.objParameters.iMinDakotGmulImNehiga && fDakotNehiga != 0)
                                {
                                    fErechRechiv = 1;
                                }

                                if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                                {
                                    fErechRechiv = float.Parse(Math.Round(fErechRechiv * objOved.fMekademNipuach, 2, MidpointRounding.AwayFromZero).ToString());
                                }
                            }
                       
                        if ((objOved.objPirteyOved.iKodMaamdRashi == clGeneral.enMaamad.Friends.GetHashCode() || objOved.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.SachirKavua.GetHashCode()) && ( oCalcBL.CheckYomShishi(objOved.SugYom) && dr.Length > 0)   )
                            fErechRechiv = 1;

                        addRowToTable(clGeneral.enRechivim.KamutGmulChisachon.GetHashCode(), fErechRechiv);
                  
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KamutGmulChisachon.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv22_1()
        {
            //יש לפתוח רכיב רק אם העובד בעל מאפיין ביצוע [שליפת מאפיין ביצוע (קוד מאפיין=60)] עם ערך כלשהו ו/או קיים סידור מזכה לגמול 
            float fSumDakotRechiv, fDakotNehiga, fMichsaYomit126, fNochechtLeTashlum,sum;
            DataRow[] drSidurim;
            DataRow[] dr;
            int iSugSidur;
            clCalcBL oCalcBL = new clCalcBL();
            try
            {
                //יש לחשב רק בתנאים אלו:
                // אם עובד שכיר קבוע (קוד מעמד = 22 או 21) 
                // העובד חבר  (הספרה הראשונה של קוד מעמד = 1) 
                //if (objOved.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.Sachir12.GetHashCode() || objOved.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.SachirKavua.GetHashCode() || (objOved.objPirteyOved.iKodMaamdRashi == clGeneral.enMaamad.Friends.GetHashCode()))
                //{
                if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30))
                {
                    fMichsaYomit126 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich); 

                    //יש לבדוק האם ביום העבודה בו מוגדר הסידור קיימת מכסה יומית מחושבת (רכיב 126) > 0. אם קיימת, להמשיך בחישוב. אם לא קיימת, אין לבצע את החישוב בשום רמה 
                    if (fMichsaYomit126 > 0)
                    {
                        if (objOved.objMeafyeneyOved.iMeafyen60 > 0)
                        {
                            fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), objOved.Taarich);
                            fNochechtLeTashlum = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime') AND (MISPAR_SIDUR=99207 or MISPAR_SIDUR=99007 or MISPAR_SIDUR=99011 )")); 
                            fSumDakotRechiv = fSumDakotRechiv - fNochechtLeTashlum;
                            drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and SUG_AVODA=7");
                            for (int i=0;i<drSidurim.Length;i++){
                                sum = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime') AND MISPAR_SIDUR=" + drSidurim[i]["mispar_sidur"].ToString()));
                                fSumDakotRechiv += sum;
                            }
                            drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)<>99");
                            for (int i = 0; i < drSidurim.Length; i++)
                            {
                                iSugSidur = int.Parse(drSidurim[i]["sug_sidur"].ToString());
                                if (oCalcBL.CheckSugSidur(objOved, clGeneral.enMeafyen.SugAvoda.GetHashCode(), clGeneral.enSugAvoda.Kupai.GetHashCode(), iSugSidur))
                                {
                                    sum = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime') AND MISPAR_SIDUR=" + drSidurim[i]["mispar_sidur"].ToString() ));
                                    fSumDakotRechiv += sum;
                                }
                            }
                        }
                        else
                        {
                            oSidur.CalcRechiv22();
                            fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.KamutGmulChisachon.GetHashCode(), objOved.Taarich);
                            if (fSumDakotRechiv == 0)
                            {
                                if (objOved.objPirteyOved.iMutamut == 1 || objOved.objPirteyOved.iMutamut == 3 || objOved.objPirteyOved.iMutamut == 5 || objOved.objPirteyOved.iMutamut == 7)
                                {
                                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR IN (99014,99221) AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                }
                            }
                            //if (oCalcBL.CheckMutamut(objOved.objPirteyOved, objOved.oGeneralData.dtMutamutAll))
                            //{
                            //    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);
                            //}
                            //else
                            //{
                            //    oSidur.CalcRechiv22();

                            //    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.KamutGmulChisachon.GetHashCode(), objOved.Taarich); 
                            //}
                        }

                        fDakotNehiga = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNehigaChol.GetHashCode(), objOved.Taarich);  
                        if (fSumDakotRechiv >= objOved.objParameters.iMinDakotGmulLeloNehiga && (fDakotNehiga == 0 || objOved.objPirteyOved.iMutamut == 1 || objOved.objPirteyOved.iMutamut == 3))
                        {
                            fSumDakotRechiv = 1;
                        }
                        else if (fSumDakotRechiv >= objOved.objParameters.iMinDakotGmulImNehiga && fDakotNehiga != 0)
                        {
                            fSumDakotRechiv = 1;
                        }
                        else { fSumDakotRechiv = 0; }
                        if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                        {
                            fSumDakotRechiv = float.Parse(Math.Round(fSumDakotRechiv * objOved.fMekademNipuach, 2,MidpointRounding.AwayFromZero).ToString());
                        }
                        addRowToTable(clGeneral.enRechivim.KamutGmulChisachon.GetHashCode(), fSumDakotRechiv);
                    }
                }
                //}
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KamutGmulChisachon.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv23()
        {
            float fSumDakotRechiv;
            try
            {
                if (objOved.objPirteyOved.iDirug != 85 || objOved.objPirteyOved.iDarga != 30)
                {
                    //יש לבדוק האם הרכיב בתוקף על ידי שליפת פרמטרים כללים: מתאריך תוקף = שליפת פרמטר (קוד פרמטר = 62) עד תאריך = שליפת פרמטר (קוד פרמטר = 63). אם תאריך יום העבודה נמצא בין תאריכי התוקף אזי להמשיך בחישוב. 
                    if (objOved.objParameters.dDateFirstTosefetSikun <= objOved.Taarich && objOved.objParameters.dDateLastTosefetSikun >= objOved.Taarich)
                    {
                        oSidur.CalcRechiv23();

                        fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotSikun.GetHashCode(), objOved.Taarich); 

                        addRowToTable(clGeneral.enRechivim.DakotSikun.GetHashCode(), fSumDakotRechiv);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotSikun.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv26_2()
        {
            float fSumDakotRechiv, fNuchehutLepremia, fTosefetRetzifut, fDakotKisuyTor, fSumDakotSikun;
            float fTosefetGil, fDakotHagdara, fDakotHistaglut, fSachNesiot, fDakotLepremia,fMichsaYomit;
            fTosefetGil = 0;
            //int iSugYom;
            try
            {
                if (  objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0  and TACHOGRAF=1", "shat_hatchala_sidur ASC").Length == 0)
                {
                  //  iSugYom = SugYom;
                    if (clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                    {
                        if (objOved.objPirteyOved.iMutamut != 1 && objOved.objPirteyOved.iMutamut != 3)
                        {
                            if (objOved.objPirteyOved.iDirug != 85 || objOved.objPirteyOved.iDarga != 30)
                            {
                                oSidur.CalcRechiv26();
                                fNuchehutLepremia = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(), objOved.Taarich);
                                if (fNuchehutLepremia > 0)
                                {
                                    Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                                    fTosefetRetzifut = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanRetzifutNehiga); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode(), objOved.Taarich); 
                                    fNuchehutLepremia += fTosefetRetzifut;

                                    fMichsaYomit = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichsaYomitMechushevet);
                                    fTosefetGil = oCalcBL.ChishuvTosefetGil(objOved,fMichsaYomit, fNuchehutLepremia,dShatHatchalaYomAvoda);

                                    fDakotHagdara = oCalcBL.GetSumErechRechivChelki(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotHagdara.GetHashCode(),clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(), objOved.Taarich) / float.Parse("0.75");
                                    fSumDakotSikun = oCalcBL.GetSumErechRechivChelki(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotSikun.GetHashCode(),clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(),objOved.Taarich) / float.Parse("0.75");
                                    fDakotHistaglut = oCalcBL.GetSumErechRechivChelki(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotHistaglut.GetHashCode(),clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(),objOved.Taarich); 
                                    fDakotKisuyTor = oCalcBL.GetSumErechRechivChelki(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotKisuiTor.GetHashCode(),clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(),objOved.Taarich); 
                                    fDakotLepremia = oCalcBL.GetSumErechRechivChelki(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.SachDakotLepremia.GetHashCode(),clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(),objOved.Taarich); 
                                    fSachNesiot = oCalcBL.GetSumErechRechivChelki(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.SachNesiot.GetHashCode(),clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(),objOved.Taarich);

                                    fSumDakotRechiv = float.Parse(Math.Round(fDakotHagdara + fSumDakotSikun + fDakotHistaglut + fDakotKisuyTor +  fDakotLepremia + fTosefetRetzifut +
                                                                            ((2 + (fSachNesiot - 1)) * objOved.objParameters.fElementZar) + (fTosefetGil - fNuchehutLepremia), MidpointRounding.AwayFromZero).ToString());
                                  //  fSumDakotRechiv = float.Parse(Math.Round((fDakotHagdara / float.Parse("0.75")) + fDakotHistaglut + fDakotKisuyTor + fDakotLepremia + fTosefetRetzifut + fSumDakotSikun + (fSachNesiot * objOved.objParameters.fElementZar) + (fTosefetGil - fNuchehutLepremia), MidpointRounding.AwayFromZero).ToString());

                                    addRowToTable(clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(), fSumDakotRechiv);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv26()
        {
            float fMichsaYomit, fErechRechiv, fErechSidur, fErechSidurOkev, fErechSidurPrev, SumEzer;
            DataRow[] drYom;
            DataRow drSidur, drSidurOkev, drSidurPrev;
            DateTime dShatHtchalaSidurOkev, dShatHtchalaSidur, dShatGmarSidur, dShatGmarPrev;
            int i = 0;
            dShatHtchalaSidurOkev = new DateTime();
            dShatGmarPrev = new DateTime();
            try
            {
                if (objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0  and TACHOGRAF=1", "").Length == 0)
                {
                    fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);

                    if (clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich) || oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom) || oCalcBL.CheckYomShishi(objOved.SugYom))  
                    {
                        if (objOved.objPirteyOved.iMutamut != 1 && objOved.objPirteyOved.iMutamut != 3)
                        {
                            if (objOved.objPirteyOved.iDirug != 85 || objOved.objPirteyOved.iDarga != 30)
                            {
                                oSidur.CalcRechiv26();
                                fErechRechiv = 0;
                                SumEzer = 0;
                                drYom = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null", "shat_hatchala_sidur ASC");

                                if (drYom.Length > 0)
                                {
                                    while (i < drYom.Length)
                                    {
                                        drSidur = drYom[i];
                                        dShatGmarSidur = DateTime.Parse(drSidur["shat_gmar_letashlum"].ToString());
                                        fErechSidur = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + drSidur["mispar_sidur"].ToString() + " AND SHAT_HATCHALA=Convert('" + drSidur["shat_hatchala_sidur"].ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaShabat.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                                        if ((i + 1) <= (drYom.Length - 1))
                                        {
                                            drSidurOkev = drYom[i + 1];
                                            dShatHtchalaSidurOkev = DateTime.Parse(drSidurOkev["shat_hatchala_letashlum"].ToString());
                                            fErechSidurOkev = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + drSidurOkev["mispar_sidur"].ToString() + " AND SHAT_HATCHALA=Convert('" + drSidurOkev["shat_hatchala_sidur"].ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaShabat.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                        }
                                        else fErechSidurOkev = 0;

                                        if (fErechSidur != 0 && fErechSidurOkev != 0)
                                        {
                                            if (float.Parse((dShatHtchalaSidurOkev - dShatGmarSidur).TotalMinutes.ToString()) <= 60)
                                            {
                                                SumEzer += fErechSidur;
                                                i++;
                                            }
                                            else
                                            {
                                                SumEzer += fErechSidur;
                                                if (SumEzer > 0)
                                                    fErechRechiv += float.Parse(Math.Round(SumEzer, MidpointRounding.AwayFromZero).ToString()); 
                                                SumEzer = 0;
                                                i++;
                                            }
                                        }
                                        else if (fErechSidur != 0 && fErechSidurOkev == 0)
                                        {
                                            if (i > 0)
                                            {
                                                drSidurPrev = drYom[i - 1];
                                                dShatGmarPrev = DateTime.Parse(drSidurPrev["shat_gmar_letashlum"].ToString());
                                                fErechSidurPrev = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + drSidurPrev["mispar_sidur"].ToString() + " AND SHAT_HATCHALA=Convert('" + drSidurPrev["shat_hatchala_sidur"].ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaShabat.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                            }
                                            else fErechSidurPrev = 0;

                                            dShatHtchalaSidur = DateTime.Parse(drSidur["shat_hatchala_letashlum"].ToString());

                                            if (fErechSidurPrev != 0 && float.Parse((dShatHtchalaSidur - dShatGmarPrev).TotalMinutes.ToString()) <= 60)
                                            {
                                                SumEzer += fErechSidur;
                                                if (SumEzer > 0)
                                                    fErechRechiv += float.Parse(Math.Round(SumEzer, MidpointRounding.AwayFromZero).ToString()); 
                                                SumEzer = 0;
                                                i++;
                                            }
                                            else
                                            {
                                                if (SumEzer > 0)
                                                    fErechRechiv += float.Parse(Math.Round(SumEzer, MidpointRounding.AwayFromZero).ToString()); 
                                                SumEzer = 0;
                                                SumEzer += fErechSidur;
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            if (SumEzer > 0)
                                                fErechRechiv += float.Parse(Math.Round(SumEzer, MidpointRounding.AwayFromZero).ToString()); 
                                            SumEzer = 0;
                                            i++;
                                        }
                                    }
                                    if (SumEzer > 0)
                                        fErechRechiv += float.Parse(Math.Round(SumEzer, MidpointRounding.AwayFromZero).ToString()); 
                                   // fErechRechiv = float.Parse(Math.Round(fErechRechiv, MidpointRounding.AwayFromZero).ToString());
                                    addRowToTable(clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(), fErechRechiv);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: " + ex.Message);
                throw (ex);
            }
        }
       
        private void CalcRechiv27()
        {
            float fSumDakotRechiv, fMichsaMechushevet, fDakotNochehut;
            try
            {
                if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30))
                {
                    Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                    fMichsaMechushevet = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichsaYomitMechushevet); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);  
                    fDakotNochehut = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNochehutLetashlum); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich); 

                    if (fMichsaMechushevet > 0)
                    {
                        if (fDakotNochehut >= fMichsaMechushevet)
                        {
                            fSumDakotRechiv = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotPremiaYomit); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotPremiaYomit.GetHashCode(), objOved.Taarich);
                            fSumDakotRechiv += oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotPremiaVisa); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotPremiaVisa.GetHashCode(), objOved.Taarich);
                            fSumDakotRechiv = fSumDakotRechiv / fDakotNochehut * fMichsaMechushevet;
                        }
                        else
                        {
                            fSumDakotRechiv = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotPremiaYomit); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotPremiaYomit.GetHashCode(), objOved.Taarich);
                            fSumDakotRechiv += oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotPremiaVisa); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotPremiaVisa.GetHashCode(), objOved.Taarich);
                        }

                        fSumDakotRechiv = float.Parse(Math.Floor(fSumDakotRechiv).ToString());
                        addRowToTable(clGeneral.enRechivim.DakotPremiaBetochMichsa.GetHashCode(), fSumDakotRechiv);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotPremiaBetochMichsa.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv28()
        {
            float fSumDakotRechiv, fSachKmVisa, fErechSidur;
            try
            {
                if (objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0  and TACHOGRAF=1", "shat_hatchala_sidur ASC").Length == 0)
                {
                    if (objOved.objPirteyOved.iMutamut != 4 && objOved.objPirteyOved.iMutamut != 5)
                    {
                        if (objOved.objPirteyOved.iDirug != 85 || objOved.objPirteyOved.iDarga != 30)
                        {
                            if (!oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom) && !oCalcBL.CheckYomShishi(objOved.SugYom) && !clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                            {
                                oSidur.CalcRechiv28();

                                fErechSidur = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotPremiaVisa.GetHashCode(), objOved.Taarich); 

                                fSachKmVisa = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode(), objOved.Taarich);

                                fSumDakotRechiv = float.Parse(Math.Round((((fErechSidur / 1.2) + fSachKmVisa) / 50) * 60 * 0.33, MidpointRounding.AwayFromZero).ToString());

                                addRowToTable(clGeneral.enRechivim.DakotPremiaVisa.GetHashCode(), fSumDakotRechiv);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotPremiaVisa.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv29()
        {
            float fSumDakotRechiv;//, fSachKmVisa, fErechSidur;
            try
            {
                if (objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0  and TACHOGRAF=1", "shat_hatchala_sidur ASC").Length == 0)
                {
                    if (oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom) || oCalcBL.CheckYomShishi(objOved.SugYom) || clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                    {
                        if (objOved.objPirteyOved.iMutamut != 4 && objOved.objPirteyOved.iMutamut != 5)
                        {
                            if (objOved.objPirteyOved.iDirug != 85 || objOved.objPirteyOved.iDarga != 30)
                            {
                                oSidur.CalcRechiv29();
                                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotPremiaVisaShabat.GetHashCode(), objOved.Taarich);  
                                addRowToTable(clGeneral.enRechivim.DakotPremiaVisaShabat.GetHashCode(), fSumDakotRechiv);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotPremiaVisaShabat.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv30_2()
        {
            float fSumDakotRechiv, fMichsaYomit, fNuchehutLepremia, fTosefetRetzifut, fSumDakotSikun;
            float fTosefetGil, fDakotHagdara, fDakotHistaglut, fSachNesiot, fDakotLepremia, fDakotKisuyTor;
            Boolean bShish, bErevChag;
            DateTime dShatHatchalaYom, dShatHatchalaShabaton;
            DataRow[] drYom;
            string sMispareySidur;
            fTosefetGil = 0;
            fDakotHagdara = 0;
            fTosefetRetzifut = 0;
            fDakotLepremia = 0;
            fDakotHistaglut = 0;
            fDakotKisuyTor = 0;
            fSachNesiot = 0;
            fSumDakotSikun = 0;
            try
            {
                if (objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0  and TACHOGRAF=1", "").Length == 0)
                {
                    fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich); 

                    if (fMichsaYomit > 0)
                    {
                        if (objOved.objPirteyOved.iMutamut != 1 && objOved.objPirteyOved.iMutamut != 3)
                        {
                            if (objOved.objPirteyOved.iDirug != 85 || objOved.objPirteyOved.iDarga != 30)
                            {
                                drYom = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null", "shat_hatchala_sidur ASC");

                                if (drYom.Length > 0)
                                {
                                    dShatHatchalaYom = DateTime.Parse(drYom[0]["shat_hatchala_sidur"].ToString());
                                    dShatHatchalaShabaton = objOved.objParameters.dKnisatShabat;

                                    oSidur.CalcRechiv30_2(out sMispareySidur);
                                    fNuchehutLepremia = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotPremiaYomit.GetHashCode(), objOved.Taarich);
                                    if (fNuchehutLepremia > 0)
                                    {
                                        if (sMispareySidur.Length > 0)
                                            sMispareySidur = sMispareySidur.Substring(1, sMispareySidur.Length - 1);

                                        fTosefetRetzifut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode(), objOved.Taarich);  // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR IN (" + sMispareySidur + ") AND KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                        fNuchehutLepremia += fTosefetRetzifut;
                                       
                                        bShish = oCalcBL.CheckYomShishi(objOved.SugYom);
                                        bErevChag = oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom);


                                        if (fMichsaYomit > 0 && ((!bShish && !bErevChag) || ((bShish || bErevChag) && dShatHatchalaYom < dShatHatchalaShabaton)))
                                        {
                                            if (objOved.objPirteyOved.iGil == clGeneral.enKodGil.enKashish.GetHashCode())
                                            {
                                                fTosefetGil = (fNuchehutLepremia * 30) / objOved.objParameters.iChalukaTosefetGilKashish;
                                            }
                                            else if (objOved.objPirteyOved.iGil == clGeneral.enKodGil.enKshishon.GetHashCode())
                                            {
                                                fTosefetGil = (fNuchehutLepremia * 30) / objOved.objParameters.iChalukaTosefetGilKshishon;

                                            }
                                        }
                                        if (sMispareySidur.Length > 0)
                                        {
                                            fDakotHagdara = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR IN (" + sMispareySidur + ") AND KOD_RECHIV=" + clGeneral.enRechivim.DakotHagdara.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                            fDakotHistaglut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR IN (" + sMispareySidur + ") AND KOD_RECHIV=" + clGeneral.enRechivim.DakotHistaglut.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                            fSachNesiot = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR IN (" + sMispareySidur + ") AND KOD_RECHIV=" + clGeneral.enRechivim.SachNesiot.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                            fDakotKisuyTor = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR IN (" + sMispareySidur + ") AND KOD_RECHIV=" + clGeneral.enRechivim.DakotKisuiTor.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                            fDakotLepremia = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR IN (" + sMispareySidur + ") AND KOD_RECHIV=" + clGeneral.enRechivim.SachDakotLepremia.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                            fSumDakotSikun = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR IN (" + sMispareySidur + ") AND KOD_RECHIV=" + clGeneral.enRechivim.DakotSikun.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')")) / float.Parse("0.75");
                                        }
                                        fSumDakotRechiv = float.Parse(Math.Round((fDakotHagdara / float.Parse("0.75")) + fDakotHistaglut + fDakotKisuyTor + fDakotLepremia + fTosefetRetzifut + fSumDakotSikun + (2 + (fSachNesiot - 1) * objOved.objParameters.fElementZar) + (fTosefetGil - fNuchehutLepremia), MidpointRounding.AwayFromZero).ToString());

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
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotPremiaYomit.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
            finally
            {
                drYom = null;
            }
        }


        private void CalcRechiv30()
        {
            float fMichsaYomit, fErechRechiv, fErechSidur, fErechSidurOkev, fErechSidurPrev, SumEzer;
            DataRow[] drYom;
            DataRow drSidur, drSidurOkev, drSidurPrev;
            DateTime dShatHtchalaSidurOkev,dShatHtchalaSidur, dShatGmarSidur, dShatGmarPrev;
            int i = 0;
            dShatHtchalaSidurOkev = new DateTime();
            dShatGmarPrev = new DateTime();
            try
            {
                if (objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0  and TACHOGRAF=1", "").Length == 0)
                {
                    fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);

                    if (fMichsaYomit > 0)
                    {
                        if (objOved.objPirteyOved.iMutamut != 1 && objOved.objPirteyOved.iMutamut != 3)
                        {
                            if (objOved.objPirteyOved.iDirug != 85 || objOved.objPirteyOved.iDarga != 30)
                            {
                                oSidur.CalcRechiv30();
                                fErechRechiv = 0;
                                SumEzer = 0;
                                drYom = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null", "shat_hatchala_sidur ASC");

                                if (drYom.Length > 0)
                                {
                                    while (i < drYom.Length)
                                    {
                                        drSidur = drYom[i];
                                        dShatGmarSidur = DateTime.Parse(drSidur["shat_gmar_letashlum"].ToString());
                                        fErechSidur = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + drSidur["mispar_sidur"].ToString() + " AND SHAT_HATCHALA=Convert('" + drSidur["shat_hatchala_sidur"].ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaYomit.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                                        if ((i + 1) <= (drYom.Length - 1))
                                        {
                                            drSidurOkev = drYom[i + 1];
                                            dShatHtchalaSidurOkev = DateTime.Parse(drSidurOkev["shat_hatchala_letashlum"].ToString());
                                            fErechSidurOkev = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + drSidurOkev["mispar_sidur"].ToString() + " AND SHAT_HATCHALA=Convert('" + drSidurOkev["shat_hatchala_sidur"].ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaYomit.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                        }
                                        else fErechSidurOkev = 0;

                                        if (fErechSidur != 0 && fErechSidurOkev != 0)
                                        {
                                            if (float.Parse((dShatHtchalaSidurOkev - dShatGmarSidur).TotalMinutes.ToString()) <= 60)
                                            {
                                                SumEzer += fErechSidur;
                                                i++;
                                            }
                                            else
                                            {
                                                SumEzer += fErechSidur;
                                                if (SumEzer>0)
                                                    fErechRechiv += float.Parse(Math.Round(SumEzer, MidpointRounding.AwayFromZero).ToString()); 
                                                SumEzer = 0;
                                                i++;
                                            }
                                        }
                                        else if (fErechSidur != 0 && fErechSidurOkev == 0)
                                        {
                                            if (i > 0)
                                            {
                                                drSidurPrev = drYom[i - 1];
                                                dShatGmarPrev = DateTime.Parse(drSidurPrev["shat_gmar_letashlum"].ToString());
                                                fErechSidurPrev = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + drSidurPrev["mispar_sidur"].ToString() + " AND SHAT_HATCHALA=Convert('" + drSidurPrev["shat_hatchala_sidur"].ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaYomit.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                            }
                                            else fErechSidurPrev = 0;
                                            
                                            dShatHtchalaSidur = DateTime.Parse(drSidur["shat_hatchala_letashlum"].ToString());
                                               
                                            if (fErechSidurPrev != 0 && float.Parse((dShatHtchalaSidur - dShatGmarPrev).TotalMinutes.ToString()) <= 60)
                                            {
                                                SumEzer += fErechSidur;
                                                if (SumEzer > 0)
                                                    fErechRechiv += float.Parse(Math.Round(SumEzer, MidpointRounding.AwayFromZero).ToString()); 
                                                SumEzer = 0;
                                                i++;
                                            }
                                            else
                                            {
                                                if (SumEzer > 0)
                                                    fErechRechiv += float.Parse(Math.Round(SumEzer, MidpointRounding.AwayFromZero).ToString()); 
                                                SumEzer = 0;
                                                SumEzer += fErechSidur;
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            if (SumEzer > 0)
                                                fErechRechiv += float.Parse(Math.Round(SumEzer, MidpointRounding.AwayFromZero).ToString()); 
                                            SumEzer = 0;
                                            i++;
                                        }
                                    }
                                    if (SumEzer > 0)
                                        fErechRechiv += float.Parse(Math.Round(SumEzer, MidpointRounding.AwayFromZero).ToString()); 

                                   // fErechRechiv = float.Parse(Math.Round(fErechRechiv, MidpointRounding.AwayFromZero).ToString());
                                    addRowToTable(clGeneral.enRechivim.DakotPremiaYomit.GetHashCode(), fErechRechiv);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotPremiaYomit.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: " + ex.Message);
                throw (ex);
            }
            finally
            {
                drYom = null;
            }
        }
        private void CalcRechiv32()
        {
            float fSumDakotRechiv, fMichsaYomit126, fDakotNochehut1;
            try
            {
                //החישוב היומי רלוונטי רק אם העובד הוא עובד יומי [שליפת מאפיין ביצוע (קוד מאפיין = 56, מ.א., תאריך)] ערך = 51 או 61.

                if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                {
                    //if (objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30 && objOved.SugYom == clGeneral.enSugYom.Bchirot.GetHashCode())
                    //{
                    //    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich); 
                    //}
                    //else
                    //{

                        fMichsaYomit126 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich); 
                        fDakotNochehut1 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);
                       // fAruchatZaharim88 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode(), objOved.Taarich);
                        fSumDakotRechiv = 0;
                        if (fMichsaYomit126 > 0 && fDakotNochehut1>0)
                        {
                            fSumDakotRechiv = Math.Min(fMichsaYomit126, fDakotNochehut1);  
                        }
                    //}

                    if (fSumDakotRechiv > 0)
                    {
                        addRowToTable(clGeneral.enRechivim.DakotRegilot.GetHashCode(), fSumDakotRechiv);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotRegilot.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv35()
        {
            float fSumDakotRechiv;
            try
            {
                //if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30 && objOved.SugYom == clGeneral.enSugYom.Bchirot.GetHashCode()))
                //{
                    oSidur.CalcRechiv35();
                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotNehigaShabat.GetHashCode(), objOved.Taarich); 
                    if (fSumDakotRechiv > 0)
                    {
                        Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                        fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanHashlama); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanHashlama.GetHashCode(), objOved.Taarich);
                        fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanRetzifutTafkid); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode(), objOved.Taarich); 
                        fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.HashlamaBenahagut); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.HashlamaBenahagut.GetHashCode(), objOved.Taarich);  
                        addRowToTable(clGeneral.enRechivim.DakotNehigaShabat.GetHashCode(), fSumDakotRechiv);

                    }
                //}
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotNehigaShabat.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv36()
        {
            float fSumDakotRechiv, fDakotNehigaShabat;
            try
            {
                //if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30 && objOved.SugYom == clGeneral.enSugYom.Bchirot.GetHashCode()))
                //{
                    oSidur.CalcRechiv36();

                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode(), objOved.Taarich);  
                    if (fSumDakotRechiv > 0)
                    {
                        fDakotNehigaShabat = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNehigaShabat.GetHashCode(), objOved.Taarich); 
                        if (fDakotNehigaShabat == 0)
                        {
                            fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanHashlama.GetHashCode(), objOved.Taarich); 
                            //fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode(), objOved.Taarich);
                        }
                        fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.HashlamaBenihulTnua.GetHashCode(), objOved.Taarich); 
                        addRowToTable(clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode(), fSumDakotRechiv);
                    }
                //}
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        internal float GetRechiv36OutMichsa()
        {
            float fSumDakotRechiv;

            fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "OUT_MICHSA=0 AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode().ToString()));
            fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanHashlama.GetHashCode(), objOved.Taarich); 
            fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode(), objOved.Taarich);
            return fSumDakotRechiv;
        }

        private void CalcRechiv37()
        {
            float fSumDakotRechiv, fDakotNehigaShabat, fDakotNihulShabat, fMichsaYomit, fTempX;
            try
            {
                //if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30 && objOved.SugYom == clGeneral.enSugYom.Bchirot.GetHashCode()))
                //{
                    oSidur.CalcRechiv37();
                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotTafkidShabat.GetHashCode(), objOved.Taarich);
                    fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);  

                    if (fMichsaYomit > 0 && fSumDakotRechiv>0)
                    {
                        fTempX = oSidur.GetSumMinutsBeforeShabat();
                        if (fTempX < fMichsaYomit)
                            fSumDakotRechiv -= ( fMichsaYomit - fTempX);
                    }
                   
                    if (fSumDakotRechiv > 0)
                    {
                        Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                        fDakotNehigaShabat = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNehigaShabat); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNehigaShabat.GetHashCode(), objOved.Taarich); 
                        fDakotNihulShabat = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNihulTnuaShabat); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode(), objOved.Taarich);  
                        if (fDakotNehigaShabat == 0 && fDakotNihulShabat == 0)
                            fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanHashlama); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanHashlama.GetHashCode(), objOved.Taarich);
                        fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanHalbasha); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanHashlama.GetHashCode(), objOved.Taarich);

                        addRowToTable(clGeneral.enRechivim.DakotTafkidShabat.GetHashCode(), fSumDakotRechiv);
                    }
                //}
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotTafkidShabat.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }


        internal float GetRechiv37OutMichsa()
        {
            float fSumDakotRechiv;
            Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"]);

            fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "OUT_MICHSA=0 AND  KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidShabat.GetHashCode().ToString()));
            fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanHashlama); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanHashlama.GetHashCode(), objOved.Taarich); 
            fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanRetzifutTafkid); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode(), objOved.Taarich); 
            fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanHalbasha); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanHalbasha.GetHashCode(), objOved.Taarich); 
            fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanGrirot); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanGrirot.GetHashCode(), objOved.Taarich); 
            fSumDakotRechiv = fSumDakotRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanNesia); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanNesia.GetHashCode(), objOved.Taarich);  
            //fSumDakotRechiv = fSumDakotRechiv - oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.KizuzAruchatBoker.GetHashCode(), objOved.Taarich);

            fSumDakotRechiv = fSumDakotRechiv - oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanAruchatTzaraim); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode(), objOved.Taarich);

            return fSumDakotRechiv;
        }

        private void CalcRechiv38()
        {
            //הרכיב יחושב רק עבור עובדים הזכאים לאש"ל [מאפיין ביצוע (קוד=50) עם ערך 1] 
            //אם קיים לפחות סידור אחד ביום עבורו ערך הרכיב = 1 אזי ערך הרכיב ברמת היום = 1. אחרת אין לפתוח רשומה לרכיב ברמת היום
            try
            {
                if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30))
                {
                    if (objOved.objMeafyeneyOved.sMeafyen50 == "1")
                    {
                        oSidur.CalcRechiv38();
                        objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Select(null, "KOD_RECHIV");
                        if (objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Select("ERECH_RECHIV=1 AND KOD_RECHIV=" + clGeneral.enRechivim.SachEshelBoker.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')").Length > 0)
                        {
                            addRowToTable(clGeneral.enRechivim.SachEshelBoker.GetHashCode(), 1);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachEshelBoker.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv40()
        {
            try
            {
                if (objOved.objPirteyOved.iDirug != 85 || objOved.objPirteyOved.iDarga != 30)
                {
                    //הרכיב יחושב רק עבור עובדים הזכאים לאש"ל [מאפיין ביצוע (קוד=50) עם ערך 1] 
                    if (objOved.objMeafyeneyOved.sMeafyen50 == "1")
                    {
                        oSidur.CalcRechiv40();
                        objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Select(null, "KOD_RECHIV");
                        if (objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Select("ERECH_RECHIV=1 AND KOD_RECHIV=" + clGeneral.enRechivim.SachEshelErev.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')").Length > 0)
                        {
                            addRowToTable(clGeneral.enRechivim.SachEshelErev.GetHashCode(), 1);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachEshelErev.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv42()
        {
            float fDakotNochehut1;
            try
            {
                //הרכיב יחושב רק עבור עובדים הזכאים לאש"ל [מאפיין ביצוע (קוד=50) עם ערך 1] 
                if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30))
                {
                    if (objOved.objMeafyeneyOved.sMeafyen50 == "1")
                    {
                        oSidur.CalcRechiv42();
                        fDakotNochehut1 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich); 

                        if (fDakotNochehut1 >= objOved.objParameters.iMinTimeSidurEshelTzaharayim)
                        {
                            objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Select(null, "KOD_RECHIV");
                            if (objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Select("ERECH_RECHIV=1 AND KOD_RECHIV=" + clGeneral.enRechivim.SachEshelTzaharayim.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')").Length > 0)
                            {
                                addRowToTable(clGeneral.enRechivim.SachEshelTzaharayim.GetHashCode(), 1);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachEshelTzaharayim.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv44()
        {
            //כמות גמול חסכון נוספות (רכיב 44) 
            float fSumDakotRechiv, fZmanHalbasha, fMichsaYomit126, fZmanGrirot,fTempY;

            //א.	אם העובד בעל מאפיין ביצוע [שליפת מאפיין ביצוע (קוד מאפיין=60)] עם ערך = 2 אזי:
            //אין משמעות לרכיב ברמת ביום.
            //אחרת
            //X = סכום ערך הרכיב עבור כל הסידורים ביום + תוספת זמן הלבשה (רכיב 94) + תוספת זמן נסיעות (רכיב 95) + זמן גרירות (רכיב 128).
            //ב.	אם X  > ממכסה יומית מחושבת (רכיב 126)  אזי : ערך הרכיב  = X  פחות מכסה יומית מחושבת (רכיב 126)   אחרת : ערך הרכיב = 0.
            try
            {
                fTempY = 0;
                if (objOved.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.Sachir12.GetHashCode() || objOved.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.SachirKavua.GetHashCode() || (objOved.objPirteyOved.iKodMaamdRashi == clGeneral.enMaamad.Friends.GetHashCode()))
                {
                    if (objOved.objMeafyeneyOved.iMeafyen60 == 2)
                    {
                        if (objOved.objPirteyOved.iDirug != 85 || objOved.objPirteyOved.iDarga != 30)
                        {
                            if (!clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                            {
                                oSidur.CalcRechiv44();
                                fTempY = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.KamutGmulChisachonNosafot.GetHashCode(), objOved.Taarich);
                                if (fTempY > 0)
                                {
                                    Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                                    fZmanHalbasha = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanHalbasha); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanHalbasha.GetHashCode(), objOved.Taarich);  
                                    //  fZmanNesiot = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanNesia); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanNesia.GetHashCode(), objOved.Taarich);  
                                    fZmanGrirot = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanGrirot); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanGrirot.GetHashCode(), objOved.Taarich); 
                                    fSumDakotRechiv = fTempY + fZmanHalbasha + fZmanGrirot;
                                    fMichsaYomit126 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichsaYomitMechushevet); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);  
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
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KamutGmulChisachonNosafot.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv39_41_43()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv39_41_43();
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], objOved.Taarich);

                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachEshelBokerMevkrim); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), objOved.Taarich);  
                if (fSumDakotRechiv > 0)
                {
                    addRowToTable(clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), fSumDakotRechiv);
                }
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachEshelErevMevkrim); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), objOved.Taarich); 
                if (fSumDakotRechiv > 0)
                {
                    addRowToTable(clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), fSumDakotRechiv);
                }
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachEshelTzaharayimMevakrim); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), objOved.Taarich);  
                if (fSumDakotRechiv > 0)
                {
                    addRowToTable(clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        //private void CalcRechiv45()
        //{
        //    float fSumDakotRechiv;
        //    double dSumDakotRechiv;
        //    try
        //    {
        //        if (objOved.objPirteyOved.iDirug != 85 || objOved.objPirteyOved.iDarga != 30)
        //        {
        //            if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
        //            {
        //                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.KamutGmulChisachon.GetHashCode().ToString()));
        //               dSumDakotRechiv = Math.Round((fSumDakotRechiv * clCalcGeneral.CalcMekademNipuach(objOved.Taarich, objOved.Mispar_ishi)));
        //                 addRowToTable(clGeneral.enRechivim.KamutGmulChisachonMeyuchad.GetHashCode(), float.Parse(dSumDakotRechiv.ToString()));

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KamutGmulChisachonMeyuchad.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
        //        throw (ex);
        //    }
        //}

        private void CalcRechiv47()
        {
            DataRow[] rowLina;
            try
            {
                if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30))
                {
                    //אם 1= TB_Yamey_Avoda_Ovdim.Lina אזי ערך הרכיב = 1 
                    rowLina = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and Lina=1");
                    if (rowLina.Length > 0)
                    {

                        addRowToTable(clGeneral.enRechivim.SachLina.GetHashCode(), 1);

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachLina.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
            finally
            {
                rowLina = null;
            }
        }

        private void CalcRechiv48()
        {
            DataRow[] rowLina;
            try
            {
                //2= TB_Yamey_Avoda_Ovdim.Lina אזי ערך הרכיב = 6  
                if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30))
                {
                    rowLina = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and Lina=2");
                    if (rowLina.Length > 0)
                    {

                        addRowToTable(clGeneral.enRechivim.SachLinaKfula.GetHashCode(), 6);

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachLinaKfula.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
            finally
            {
                rowLina = null;
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

                rowPitzul = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and Pitzul_hafsaka = 1");
                objOved._dsChishuv.Tables["CHISHUV_YOM"].Select(null, "KOD_RECHIV");
                rowPitzulKaful = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime') and KOD_RECHIV=" + clGeneral.enRechivim.SachPitzulKaful.GetHashCode().ToString());

                if ((rowPitzul.Length > 0) && (rowPitzulKaful.Length == 0))
                {
                    addRowToTable(clGeneral.enRechivim.SachPitzul.GetHashCode(), 1);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachPitzul.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
            finally
            {
                rowPitzul=null;
                rowPitzulKaful=null;
            }
        }

        private void CalcRechiv50()
        {
            DataRow[] rowPitzul;
            try
            {
                //אזי הרכיב = 1 TB_Sidurim_Ovedim.Pitzul_hafsaka = 2 אם
                if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30))
                {
                    rowPitzul = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and Pitzul_hafsaka = 2");
                    if (rowPitzul.Length > 0)
                    {

                        addRowToTable(clGeneral.enRechivim.SachPitzulKaful.GetHashCode(), 1);

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachPitzulKaful.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
            finally
            {
                rowPitzul = null;
            }
        }

        private void CalcRechiv52()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv52();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.SachDakotLepremia.GetHashCode(), objOved.Taarich); 
                addRowToTable(clGeneral.enRechivim.SachDakotLepremia.GetHashCode(), fSumDakotRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachDakotLepremia.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv53()
        {
            float fSumDakotRechiv,fTosefetRezifut96;
            try
            {
                if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30))
                {

                    oSidur.CalcRechiv53();
                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode(), objOved.Taarich);
                    if (fSumDakotRechiv > 0)
                    {
                        fTosefetRezifut96 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode(), objOved.Taarich);
                        addRowToTable(clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode(), fSumDakotRechiv + fTosefetRezifut96);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv54()
        {
            float fSumDakotRechiv;
            try
            {
                if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30))
                {
                    if ((objOved.objPirteyOved.iKodMaamdMishni != clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode()) ||
                         (objOved.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode() &&
                         (objOved.objPirteyOved.iIsuk == 122 || objOved.objPirteyOved.iIsuk == 123 || objOved.objPirteyOved.iIsuk == 124 || objOved.objPirteyOved.iIsuk == 127)))
                    {
                        oSidur.CalcRechiv54();
                        fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.ZmanLailaEgged.GetHashCode(), objOved.Taarich);  
                        fSumDakotRechiv += oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanRetzifutLaylaEgged.GetHashCode(), objOved.Taarich); 
                        addRowToTable(clGeneral.enRechivim.ZmanLailaEgged.GetHashCode(), fSumDakotRechiv);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ZmanLailaEgged.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv55()
        {
            float fSumDakotRechiv, fTempX, fTempY, fTempZ, fZmanLilaSidureyBoker, fMichsaYomitMechushevet;
            try
            {
                if ((objOved.objPirteyOved.iKodMaamdMishni != clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode()) ||
                     (objOved.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode() &&
                     (objOved.objPirteyOved.iIsuk == 122 || objOved.objPirteyOved.iIsuk == 123 || objOved.objPirteyOved.iIsuk == 124 || objOved.objPirteyOved.iIsuk == 127)))
                {
                    oSidur.CalcRechiv55();

                    Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                    fTempX = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.ZmanLailaChok.GetHashCode(), objOved.Taarich);

                    fZmanLilaSidureyBoker = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanLilaSidureyBoker); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanLilaSidureyBoker.GetHashCode(), objOved.Taarich);

                    if (objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30)
                    {
                        fSumDakotRechiv = fTempX;
                    }
                    else
                    {
                        fTempX += oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanRetzifutLaylaChok); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanRetzifutLaylaChok.GetHashCode(), objOved.Taarich);

                        fTempY = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.Nosafot125); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Nosafot125.GetHashCode(), objOved.Taarich);
                        fTempY = fTempY + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.Shaot25); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Shaot25.GetHashCode(), objOved.Taarich);
                        fTempZ = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.Nosafot150); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Nosafot150.GetHashCode(), objOved.Taarich);
                        fTempZ = fTempZ + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.Shaot50); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Shaot50.GetHashCode(), objOved.Taarich); 

                        if (objOved.objPirteyOved.iKodMaamdRashi == clGeneral.enMaamad.Salarieds.GetHashCode() && CheckSugMishmeret() == clGeneral.enSugMishmeret.Liyla.GetHashCode() &&
                         (objOved.objPirteyOved.iIsuk == 122 || objOved.objPirteyOved.iIsuk == 123 || objOved.objPirteyOved.iIsuk == 124 || objOved.objPirteyOved.iIsuk == 127))
                        {
                            fSumDakotRechiv = fTempX;
                        }
                        else
                        {
                            if (fTempX > 0)
                            {
                                fMichsaYomitMechushevet = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich); 

                                if (fMichsaYomitMechushevet == 0)
                                {
                                    fSumDakotRechiv = fTempX;
                                }
                                else if (fTempZ >= fTempX)
                                {
                                    fSumDakotRechiv =float.Parse(Math.Round(fTempX * float.Parse("1.5"),MidpointRounding.AwayFromZero).ToString());
                                }
                                else
                                {
                                    fTempX = fTempX - fTempZ;

                                    fSumDakotRechiv = float.Parse(Math.Round(fTempZ * float.Parse("1.5"), MidpointRounding.AwayFromZero).ToString());

                                    if (fTempY > fTempX)
                                    {

                                        fSumDakotRechiv = fSumDakotRechiv + float.Parse(Math.Round(fTempX * float.Parse("1.25"), MidpointRounding.AwayFromZero).ToString());
                                    }
                                    else
                                    {
                                        fSumDakotRechiv = fSumDakotRechiv + float.Parse(Math.Round(fTempY * float.Parse("1.25"), MidpointRounding.AwayFromZero).ToString()) + (fTempX - fTempY);
                                    }
                                }
                            }
                            else
                            {
                                fSumDakotRechiv = 0;
                            }

                        }
                    }


                    fSumDakotRechiv = fSumDakotRechiv + fZmanLilaSidureyBoker + oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanLailaEgged.GetHashCode(), objOved.Taarich); 

                    addRowToTable(clGeneral.enRechivim.ZmanLailaChok.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ZmanLailaChok.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv56()
        {
            float fErechRechiv;

            try
            {
                if (objOved.objPirteyOved.iKodMaamdMishni != clGeneral.enKodMaamad.Shtachim.GetHashCode())
                {
                    fErechRechiv = CalcHeadruyot(clGeneral.enRechivim.YomEvel.GetHashCode());
                    addRowToTable(clGeneral.enRechivim.YomEvel.GetHashCode(), fErechRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomEvel.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv57()
        {
            float fErechRechiv, fMichsaYomitMechushevet, fTempX, fMichsaYomit;
            int iCount;
            try
            {
                fTempX = 0;
                fErechRechiv = 0;
                fMichsaYomitMechushevet = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich); 

                fErechRechiv = oSidur.CalcYemeyHeadrut(clGeneral.enRechivim.YomHadracha.GetHashCode(), out iCount, fMichsaYomitMechushevet);
                objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Select(null, "KOD_RECHIV");
                //if (objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Select("ERECH_RECHIV>0  AND MISPAR_SIDUR=99703 AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')").Length > 0)
                //{
                //    fErechRechiv = 1;
                //}
                //else
                //{
                    fTempX = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR IN(99207, 99011,99007) AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    if (objOved.objParameters.iDakotKabalatYomHadracha > 0 && fTempX >= objOved.objParameters.iDakotKabalatYomHadracha)
                    {
                        fErechRechiv = 1;
                    }
                    else
                    {
                        if (objOved.objPirteyOved.iZmanMutamut > 0)
                        {
                            fMichsaYomit = objOved.objPirteyOved.iZmanMutamut;
                        }
                        else
                        {
                            if (fMichsaYomitMechushevet > 0)
                            {
                                fMichsaYomit = fMichsaYomitMechushevet;
                            }
                            else{
                                if (objOved.objPirteyOved.iGil == clGeneral.enKodGil.enTzair.GetHashCode())
                                {
                                    fMichsaYomit = objOved.objParameters.iGmulNosafotTzair;
                                }
                                else if (objOved.objPirteyOved.iGil == clGeneral.enKodGil.enKshishon.GetHashCode())
                                {
                                    fMichsaYomit = objOved.objParameters.iGmulNosafotKshishon;
                                }
                                else
                                {
                                    fMichsaYomit = objOved.objParameters.iGmulNosafotKashish;
                                }
                            }
                        }

                        fErechRechiv = Math.Min(1, (fTempX / fMichsaYomit));
                    }
                //}
                addRowToTable(clGeneral.enRechivim.YomHadracha.GetHashCode(), fErechRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomHadracha.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv59()
        {
            float fErechRechiv, fMichsaYomit;

            try
            {
                fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);
                if (fMichsaYomit > 0)
                {
                    if (objOved.objPirteyOved.iIsuk != 122 && objOved.objPirteyOved.iIsuk != 123 && objOved.objPirteyOved.iIsuk != 124 && objOved.objPirteyOved.iIsuk != 127)
                    {
                        if (objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is null").Length > 0)
                        {
                            fErechRechiv = 1;
                            addRowToTable(clGeneral.enRechivim.YomLeloDivuach.GetHashCode(), fErechRechiv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomLeloDivuach.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv60()
        {
            float fErechRechiv, fErechSidur, fMichsaYomit, fDakotNochechut,fTempX, fTempW, fTempY;
            int iCount;
            DataRow[] drRechiv;
            string sSidurimMeyuchadim = "";
            try
            {
                fTempX = 0;
                fErechRechiv = 0;
                fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);
                fDakotNochechut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);  
                if (fMichsaYomit > 0)
                {
                    fErechSidur = oSidur.CalcYemeyHeadrut(clGeneral.enRechivim.YomMachla.GetHashCode(), out iCount, fMichsaYomit);
                    if (iCount > 0)
                    {
                        if (iCount == 1 && fErechSidur > 0)
                        {
                            fTempX = 1;
                        }
                        else if (fErechSidur > 0)
                        {
                            sSidurimMeyuchadim = oSidur.GetSidurimMeyuchRechiv(clGeneral.enRechivim.YomMachla.GetHashCode());
                            fTempY = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR NOT IN(" + sSidurimMeyuchadim + ") AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            objOved._dsChishuv.Tables["CHISHUV_YOM"].Select(null, "KOD_RECHIV");
                            drRechiv = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("ERECH_RECHIV>0 AND KOD_RECHIV=" + clGeneral.enRechivim.YomMachla.GetHashCode().ToString() + " and taarich<Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')", "taarich desc");
                            if (drRechiv.Length > 0 && DateTime.Parse(drRechiv[0]["taarich"].ToString()) == objOved.Taarich.AddDays(-1) && float.Parse(drRechiv[0]["erech_rechiv"].ToString()) > 0)
                            {
                                fTempX = 1;
                            }
                            else if (fTempY < fMichsaYomit)
                            {
                                fTempX = (fMichsaYomit - fTempY) / fMichsaYomit;
                            }
                        }

                        if (fTempX > 1)
                        { fTempX = 1; }

                        fTempW = 1;

                        if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                        {
                            fTempW = objOved.fMekademNipuach;
                        }

                        fErechRechiv = fTempW * fTempX;

                        if (objOved.Taarich >= objOved.objParameters.dChodeshTakanonSoziali && objOved.objPirteyOved.iZmanMutamut > 0 &&
                             (objOved.objPirteyOved.iSibotMutamut == 2 || objOved.objPirteyOved.iSibotMutamut == 3 || objOved.objPirteyOved.iSibotMutamut == 22))
                        {
                            if (objOved.objPirteyOved.iIshurKeren > 0 && fDakotNochechut == 0)
                            {
                                fErechRechiv = (objOved.objPirteyOved.iZmanMutamut / fMichsaYomit);
                            }

                        }

                        fErechRechiv = float.Parse(Math.Round(fErechRechiv, 2, MidpointRounding.AwayFromZero).ToString());

                        addRowToTable(clGeneral.enRechivim.YomMachla.GetHashCode(), fErechRechiv);
                    }
                }

                if (objOved.Taarich >= objOved.objParameters.dChodeshTakanonSoziali && objOved.objPirteyOved.iZmanMutamut > 0 &&
                            (objOved.objPirteyOved.iSibotMutamut == 2 || objOved.objPirteyOved.iSibotMutamut == 3 || objOved.objPirteyOved.iSibotMutamut == 22))
                {

                    if (objOved.objPirteyOved.iIshurKeren == 0 && fErechRechiv != 1 && fMichsaYomit > 0)
                    {
                        fErechRechiv = (fMichsaYomit - objOved.objPirteyOved.iZmanMutamut) / fMichsaYomit;
                        fErechRechiv = float.Parse(Math.Round(fErechRechiv, 2, MidpointRounding.AwayFromZero).ToString());
                        addRowToTable(clGeneral.enRechivim.YomMachla.GetHashCode(), fErechRechiv);
                    }                 
                }  
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomMachla.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
            finally
            {
                drRechiv = null;
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
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomMachalaBoded.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
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
                fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich); 
                if (fMichsaYomit > 0)
                {
                    fErechRechiv = oSidur.CalcRechiv62(out iCount);
                    if (fErechRechiv > 0)
                    {
                        fTempX = fErechRechiv;

                        fTempW = 1;

                        if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                        {
                            fTempW = objOved.fMekademNipuach;
                        }

                        fErechRechiv = float.Parse(Math.Round(fTempW * fTempX, 2, MidpointRounding.AwayFromZero).ToString());
                        addRowToTable(clGeneral.enRechivim.YomMiluim.GetHashCode(), fErechRechiv);
                    }
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomMiluim.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
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
                if (iCount > 0 && fErechRechiv == 1)
                {
                    fErechRechiv = 1;
                    addRowToTable(clGeneral.enRechivim.YomAvodaBechul.GetHashCode(), fErechRechiv);
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomAvodaBechul.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv64()
        {
            float fErechRechiv;

            try
            {
                    fErechRechiv = CalcHeadruyot(clGeneral.enRechivim.YomTeuna.GetHashCode());
                    addRowToTable(clGeneral.enRechivim.YomTeuna.GetHashCode(), fErechRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomTeuna.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: " + ex.Message);
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
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomShmiratHerayon.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv66()
        {
            float fErechRechiv=0, fMichsaYomit, fDakotNochehut, fKizuzMeheadrut,fMichsatMutamBitachon,fErech60;
            DataRow[] rowSidur;
            string sRechivim;
            bool bflag = false;
           // clPirteyOved oEzerPratim; 
            try
            {
                fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich); 
                fKizuzMeheadrut = 0;
                if (!(oCalcBL.GetSugYomLemichsa(objOved, objOved.Taarich, objOved.objPirteyOved.iKodSectorIsuk, objOved.objMeafyeneyOved.iMeafyen56) == clGeneral.enSugYom.ErevYomHatsmaut.GetHashCode()
                    && (objOved.objMeafyeneyOved.sMeafyen63 != "" || objOved.objMeafyeneyOved.sMeafyen63 != "0") && objOved.objMeafyeneyOved.iMeafyen33 == 1))
                {
                    rowSidur = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur=99801 and Hashlama_Leyom=1");
                    if (rowSidur.Length == 0)
                    {
                        rowSidur = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur=99830");
                        if (rowSidur.Length == 0)
                        {
                            rowSidur = null;
                            rowSidur = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur=99841 and Hashlama_Leyom=1");
                            if (!(rowSidur.Length > 0 && fMichsaYomit > 0 && objOved.objMeafyeneyOved.iMeafyen33 == 1))
                            {
                                fErechRechiv = 0;
                                if (fMichsaYomit > 0)
                                {
                                    rowSidur = null;
                                    rowSidur = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and sidur_misug_headrut=1");
                                    if (rowSidur.Length > 0)
                                    {
                                        fErechRechiv = 1;
                                    }
                                }
                                //ג
                                fDakotNochehut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);
                                if (fMichsaYomit > 0 && fDakotNochehut == 0 && objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null").Length == 0 && objOved.objMeafyeneyOved.iMeafyen33 == 1)
                                { fErechRechiv = 1; }

                                //ה
                                if (((objOved.Taarich < objOved.objParameters.dChodeshTakanonSoziali && 
                                    (objOved.objPirteyOved.iMutamBitachon == 4 || objOved.objPirteyOved.iMutamBitachon == 5 || objOved.objPirteyOved.iMutamBitachon == 6 || objOved.objPirteyOved.iMutamBitachon == 8 || objOved.objPirteyOved.iMutamut == 1 ||
                                    objOved.objPirteyOved.iSibotMutamut == 4 || objOved.objPirteyOved.iSibotMutamut == 5 || objOved.objPirteyOved.iSibotMutamut == 6 || objOved.objPirteyOved.iSibotMutamut == 8) && fMichsaYomit > 0) ||
                                     (objOved.Taarich >= objOved.objParameters.dChodeshTakanonSoziali && objOved.objPirteyOved.iMutamBitachon == 6 )) //&& !objOved.bMeafyen85YomMeyuchad)) 
                                     && (objOved.objPirteyOved.iZmanMutamut > 0 && (objOved.objPirteyOved.iMutamut == 1 ||  objOved.objPirteyOved.iMutamut == 5 || objOved.objPirteyOved.iMutamut == 7)))
                                {
                                    if (objOved.objPirteyOved.iKodMaamdRashi == clGeneral.enMaamad.Salarieds.GetHashCode())
                                    {
                                        if (objOved.objMeafyeneyOved.iMeafyen33 == 1)
                                        {
                                            fErechRechiv = ((fMichsaYomit - fDakotNochehut) / fMichsaYomit);
                                        }
                                   
                                        if (objOved.objMeafyeneyOved.iMeafyen33 == 0)// && fDakotNochehut < objOved.objPirteyOved.iZmanMutamut)
                                        {
                                            fErechRechiv = ((fMichsaYomit - objOved.objPirteyOved.iZmanMutamut) / fMichsaYomit); //((objOved.objPirteyOved.iZmanMutamut - fDakotNochehut) / fMichsaYomit);
                                        }
                                    }
                                    else if (objOved.objPirteyOved.iKodMaamdRashi == clGeneral.enMaamad.Friends.GetHashCode())
                                    {
                                        if (objOved.objMeafyeneyOved.iMeafyen33 == 1) // && fDakotNochehut < objOved.objPirteyOved.iZmanMutamut)
                                        {
                                            fErechRechiv = ((fMichsaYomit  - fDakotNochehut) / fMichsaYomit);
                                        }
                                        //else if (objOved.objMeafyeneyOved.iMeafyen33 == 0 && fDakotNochehut < objOved.objPirteyOved.iMutamBitachon)
                                        //{
                                        //    fErechRechiv = ((objOved.objPirteyOved.iZmanMutamut - fDakotNochehut) / fMichsaYomit);
                                        //}
                                    }
                                }
                                else
                                {
                                    if (fDakotNochehut < fMichsaYomit && fDakotNochehut > 0 && fMichsaYomit > 0 && objOved.objMeafyeneyOved.iMeafyen33 == 1)
                                    {
                                        if (!HaveRechivimInDay(objOved.Taarich))
                                            fErechRechiv = (fMichsaYomit - fDakotNochehut) / fMichsaYomit;
                                    }
                                }

                               // oEzerPratim =  objOved.PirteyOved.First(Pratim => (Pratim.iMutamut == 4 || Pratim.iMutamut == 5 || Pratim.iMutamut == 8));

                                if (objOved.Taarich >= objOved.objParameters.dChodeshTakanonSoziali && (objOved.objPirteyOved.iZmanMutamut > 0  && (objOved.objPirteyOved.iMutamut == 1 ||  objOved.objPirteyOved.iMutamut == 5 || objOved.objPirteyOved.iMutamut == 7))&&
                                    (((objOved.objPirteyOved.iSibotMutamut == 2 || objOved.objPirteyOved.iSibotMutamut == 3 || objOved.objPirteyOved.iSibotMutamut == 22) && objOved.objPirteyOved.iIshurKeren > 0) ||
                                     (objOved.objPirteyOved.iSibotMutamut == 4 || objOved.objPirteyOved.iSibotMutamut == 5 || objOved.objPirteyOved.iSibotMutamut == 8 || objOved.objPirteyOved.iSibotMutamut == 1 ) ))
                                {
                                    fErechRechiv = (fMichsaYomit - objOved.objPirteyOved.iZmanMutamut) / fMichsaYomit;
                                
                                    if ((objOved.objPirteyOved.iSibotMutamut == 2 || objOved.objPirteyOved.iSibotMutamut == 3 || objOved.objPirteyOved.iSibotMutamut == 22) && objOved.objPirteyOved.iIshurKeren > 0)
                                    {
                                        fErech60 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMachla.GetHashCode(), objOved.Taarich);
                                        if (objOved.objMeafyeneyOved.iMeafyen33 == 1 && fMichsaYomit > 0 && fDakotNochehut < objOved.objPirteyOved.iZmanMutamut && fErech60 == 0)
                                            fErechRechiv += ((objOved.objPirteyOved.iZmanMutamut - fDakotNochehut) / fMichsaYomit);
                                    }

                                }

                                //ו
                                if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                                {
                                    fErechRechiv = fErechRechiv * objOved.fMekademNipuach;
                                }

                                //	חישוב עבור עובדים גלובליים
                                if (objOved.objMeafyeneyOved.iMeafyen83 == 1 && objOved.objMeafyeneyOved.iMeafyen33 == 1)
                                {
                                    sRechivim = clGeneral.enRechivim.YomHadracha.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomMachalatHorim.GetHashCode().ToString() +
                                          "," + clGeneral.enRechivim.YomMachalaYeled.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomShmiratHerayon.GetHashCode().ToString() +
                                          "," + clGeneral.enRechivim.YomMachalatBenZug.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomAvodaBechul.GetHashCode().ToString() +
                                           "," + clGeneral.enRechivim.YomTeuna.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomMachla.GetHashCode().ToString() +
                                           "," + clGeneral.enRechivim.YomMiluim.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomMachalaBoded.GetHashCode().ToString();
                                    if (objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV in(" + sRechivim + ") and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')").Length > 0)
                                    {
                                        fErechRechiv = 0;
                                    }
                                    if (fMichsaYomit > 0 && fMichsaYomit > fDakotNochehut && fDakotNochehut > 0 ) //|| (objOved.SugYom == clGeneral.enSugYom.CholHamoedPesach.GetHashCode() || objOved.SugYom == clGeneral.enSugYom.CholHamoedSukot.GetHashCode())))
                                    {
                                        fKizuzMeheadrut = (fMichsaYomit - fDakotNochehut) / 60;
                                    }

                                }
                                //חישוב עבור חוה"מ וערבי חג כאשר התשלום הוא 60:40
                                if (objOved.bMeafyen85YomMeyuchad && !oCalcBL.CheckOvedPutar(objOved) && fMichsaYomit > 0 && fErechRechiv > 0)
                                {
                                    fErechRechiv = fErechRechiv * float.Parse("0.6");
                                    bflag = true;
                                }

                                if (objOved.Taarich >= objOved.objParameters.dChodeshTakanonSoziali && objOved.objPirteyOved.iKodMaamdMishni != clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
                                {
                                    if ((fErechRechiv == 1 || (bflag && fErechRechiv==float.Parse("0.6")) ||
                                        (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode() && fErechRechiv == (1 * objOved.fMekademNipuach)))
                                        && objOved.objMatzavOved.iKod_Headrut == 1)
                                    {
                                        addRowToTable(clGeneral.enRechivim.YomChofesh.GetHashCode(), fErechRechiv);
                                        fErechRechiv = 0;
                                    }
                                }
                                fErechRechiv = float.Parse(Math.Round(fErechRechiv,2).ToString());
                                addRowToTable(clGeneral.enRechivim.YomHeadrut.GetHashCode(), fErechRechiv, fKizuzMeheadrut);
                            }
                        }
                    }
                }
             

                        
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomHeadrut.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
            finally
            {
                rowSidur = null;
            }
        }

        private void CalcRechiv67()
        {
            float fErechRechiv, fMichsaYomit,fErech60, fDakotNochehut, fMichsatMutamBitachon,fDakotNochehutLeloKizuz, fKizuzMeheadrut;
            DataRow[] rowSidur ;
            string sRechivim;
            bool flag = false;
          // int matzav=0;
            try
            {
                fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich); 
                fKizuzMeheadrut = 0; 
                if (!(oCalcBL.GetSugYomLemichsa(objOved, objOved.Taarich, objOved.objPirteyOved.iKodSectorIsuk, objOved.objMeafyeneyOved.iMeafyen56) == clGeneral.enSugYom.ErevYomHatsmaut.GetHashCode()
                    && (objOved.objMeafyeneyOved.sMeafyen63 != "" && objOved.objMeafyeneyOved.sMeafyen63 != "0") && objOved.objMeafyeneyOved.iMeafyen33 == 0))
                {
                     rowSidur = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur=99830");
                     if (rowSidur.Length == 0)
                     {
                         rowSidur = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur=99841 and Hashlama_Leyom=1");
                         if (!(rowSidur.Length > 0 && fMichsaYomit > 0 && objOved.objMeafyeneyOved.iMeafyen33 == 0))
                         {
                             fErechRechiv = 0;
                             if (fMichsaYomit > 0)
                             {
                                 rowSidur = null;
                                 rowSidur = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and sidur_misug_headrut=5");
                                 if (rowSidur.Length > 0)
                                 {
                                     fErechRechiv = 1;
                                 }
                             }
                             //א
                             fDakotNochehut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);
                             if (fMichsaYomit > 0 && fDakotNochehut == 0 && objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null").Length == 0 && objOved.objMeafyeneyOved.iMeafyen33 == 0)
                             { fErechRechiv = 1; }

                             //ג

                             //if (((objOved.Taarich < objOved.objParameters.dChodeshTakanonSoziali &&
                             //        (objOved.objPirteyOved.iMutamBitachon == 4 || objOved.objPirteyOved.iMutamBitachon == 5 || objOved.objPirteyOved.iMutamBitachon == 6 || objOved.objPirteyOved.iMutamBitachon == 8 || objOved.objPirteyOved.iMutamut == 1 ||
                             //        objOved.objPirteyOved.iSibotMutamut == 4 || objOved.objPirteyOved.iSibotMutamut == 5 || objOved.objPirteyOved.iSibotMutamut == 6 || objOved.objPirteyOved.iSibotMutamut == 8) && fMichsaYomit > 0) ||
                             //         (objOved.Taarich >= objOved.objParameters.dChodeshTakanonSoziali && objOved.objPirteyOved.iMutamBitachon == 6)) && objOved.objPirteyOved.iZmanMutamut > 0)
                             if ((objOved.objPirteyOved.iMutamBitachon == 4 || objOved.objPirteyOved.iMutamBitachon == 5 || objOved.objPirteyOved.iMutamBitachon == 6 || objOved.objPirteyOved.iMutamBitachon == 8 || objOved.objPirteyOved.iMutamut == 1 ||
                                     objOved.objPirteyOved.iSibotMutamut == 2 || objOved.objPirteyOved.iSibotMutamut == 3 || objOved.objPirteyOved.iSibotMutamut == 4 || objOved.objPirteyOved.iSibotMutamut == 5 || objOved.objPirteyOved.iSibotMutamut == 6 || objOved.objPirteyOved.iSibotMutamut == 8)
                                     && fMichsaYomit > 0 && (objOved.objPirteyOved.iZmanMutamut > 0 && (objOved.objPirteyOved.iMutamut == 1 || objOved.objPirteyOved.iMutamut == 5 || objOved.objPirteyOved.iMutamut == 7)) ) //&& !objOved.bMeafyen85YomMeyuchad)
                             {
                                 if (objOved.objPirteyOved.iKodMaamdRashi == clGeneral.enMaamad.Friends.GetHashCode() &&
                                     ((objOved.objPirteyOved.iMutamBitachon == 6) ||
                                      ((objOved.objPirteyOved.iZmanMutamut > 0 && (objOved.objPirteyOved.iMutamut == 1 || objOved.objPirteyOved.iMutamut == 5 || objOved.objPirteyOved.iMutamut == 7)) && objOved.Taarich < objOved.objParameters.dChodeshTakanonSoziali)))
                                 {
                                     rowSidur = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur in(99820,99822,99821)");

                                     if (objOved.objMeafyeneyOved.iMeafyen33 == 0 && rowSidur.Length == 0)
                                     {
                                         fErechRechiv = (fMichsaYomit - fDakotNochehut) / fMichsaYomit;
                                     }

                                     //if (objOved.objMeafyeneyOved.iMeafyen33 == 1 && fDakotNochehut < objOved.objPirteyOved.iZmanMutamut)
                                     //{
                                     //    fErechRechiv = (objOved.objPirteyOved.iZmanMutamut - fDakotNochehut) / fMichsaYomit;    
                                     //}
                                 }
                                 else if (objOved.objPirteyOved.iKodMaamdRashi == clGeneral.enMaamad.Salarieds.GetHashCode() ||
                                          (objOved.objPirteyOved.iKodMaamdRashi == clGeneral.enMaamad.Friends.GetHashCode() &&
                                           objOved.objPirteyOved.iMutamBitachon != 6 && objOved.Taarich >= objOved.objParameters.dChodeshTakanonSoziali &&
                                           (objOved.objPirteyOved.iZmanMutamut > 0 && (objOved.objPirteyOved.iMutamut == 1 || objOved.objPirteyOved.iMutamut == 5 || objOved.objPirteyOved.iMutamut == 7))))
                                 {
                                     if (objOved.objMeafyeneyOved.iMeafyen33 == 0 && fDakotNochehut < objOved.objPirteyOved.iZmanMutamut)
                                     {
                                         fErechRechiv = (objOved.objPirteyOved.iZmanMutamut - fDakotNochehut) / fMichsaYomit;
                                     }
                                 }

                                 if (HaveRechivimInDay(objOved.Taarich, "60,61,65,69,70,71,68") && fErechRechiv == 1)
                                 {
                                     fErechRechiv = 0;
                                 }
                             }
                             else
                             {
                                 if (fDakotNochehut < fMichsaYomit && fDakotNochehut > 0 && fMichsaYomit > 0 && objOved.objMeafyeneyOved.iMeafyen33 == 0)
                                 {
                                     if (!HaveRechivimInDay(objOved.Taarich))
                                         fErechRechiv = (fMichsaYomit - fDakotNochehut) / fMichsaYomit;
                                 }
                             }

                             if (objOved.Taarich >= objOved.objParameters.dChodeshTakanonSoziali && objOved.objPirteyOved.iZmanMutamut > 0 &&
                                  fMichsaYomit > 0 && ((objOved.objPirteyOved.iSibotMutamut == 2 || objOved.objPirteyOved.iSibotMutamut == 3)))
                             {
                                 fErech60 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMachla.GetHashCode(), objOved.Taarich);
                                 if (((fErech60 == 1 && objOved.objPirteyOved.iIshurKeren == 0) || (fErech60 > 0 && objOved.objPirteyOved.iIshurKeren > 0)) || fDakotNochehut >= objOved.objPirteyOved.iZmanMutamut)
                                     fErechRechiv = 0;
                                 else if (fErechRechiv == 1)
                                     fErechRechiv = objOved.objPirteyOved.iZmanMutamut / fMichsaYomit;
                                 else
                                     fErechRechiv = (objOved.objPirteyOved.iZmanMutamut - fDakotNochehut) / fMichsaYomit;

                             }
                             ////if (objOved.Taarich >= objOved.objParameters.dChodeshTakanonSoziali && objOved.objPirteyOved.iZmanMutamut > 0 &&
                             ////     ( (objOved.objPirteyOved.iSibotMutamut == 2 || objOved.objPirteyOved.iSibotMutamut == 3 || objOved.objPirteyOved.iSibotMutamut == 22) ))
                             ////{
                             ////    fErech60 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMachla.GetHashCode(), objOved.Taarich);

                             ////    if (fErech60 !=1 && objOved.objMeafyeneyOved.iMeafyen33 == 0 && fMichsaYomit>0 && fDakotNochehut < objOved.objPirteyOved.iZmanMutamut)
                             ////        fErechRechiv = (objOved.objPirteyOved.iZmanMutamut - fDakotNochehut) / fMichsaYomit;
                             ////    else fErechRechiv = 0;
                             ////}

                             //ד
                             if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                             {
                                 fErechRechiv = fErechRechiv * objOved.fMekademNipuach;
                             }

                             //חישוב עבור עובדים גלובליים:
                             if (objOved.objMeafyeneyOved.iMeafyen83 == 1 && objOved.objMeafyeneyOved.iMeafyen33 == 0)
                             {
                                 sRechivim = clGeneral.enRechivim.YomHadracha.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomMachalatHorim.GetHashCode().ToString() +
                                       "," + clGeneral.enRechivim.YomMachalaYeled.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomShmiratHerayon.GetHashCode().ToString() +
                                       "," + clGeneral.enRechivim.YomMachalatBenZug.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomAvodaBechul.GetHashCode().ToString() +
                                        "," + clGeneral.enRechivim.YomTeuna.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomMachla.GetHashCode().ToString() +
                                        "," + clGeneral.enRechivim.YomMiluim.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomMachalaBoded.GetHashCode().ToString();
                                 if (objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV in(" + sRechivim + ") and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')").Length > 0)
                                 {
                                     fErechRechiv = 0;
                                 }
                                 if (fMichsaYomit > 0 && fMichsaYomit > fDakotNochehut && fDakotNochehut > 0) //|| (objOved.SugYom == clGeneral.enSugYom.CholHamoedPesach.GetHashCode() || objOved.SugYom == clGeneral.enSugYom.CholHamoedSukot.GetHashCode())))
                                 {
                                     fKizuzMeheadrut = fMichsaYomit - fDakotNochehut;
                                 }
                             }

                             //	חישוב עבור חוה"מ וערבי חג כאשר התשלום הוא 60:40 
                             if (objOved.bMeafyen85YomMeyuchad && !oCalcBL.CheckOvedPutar(objOved) && fMichsaYomit > 0 && fErechRechiv > 0)
                             {
                                 fErechRechiv = fErechRechiv * float.Parse("0.6");
                             }

                             rowSidur = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur=99822");

                             if (rowSidur.Length == 0)
                             {
                                 // int.TryParse(objOved.objMatzavOved.sKod_Matzav,ref matzav);
                                 if (((objOved.SugYom >= 11 && objOved.SugYom <= 16) || objOved.SugYom == 18) ||
                                     ((objOved.SugYom == clGeneral.enSugYom.CholHamoedPesach.GetHashCode() || objOved.SugYom == clGeneral.enSugYom.CholHamoedSukot.GetHashCode()) && objOved.objMeafyeneyOved.iMeafyen85 == 1) ||
                                       objOved.Taarich < objOved.dtaarichHakpaa)
                                     flag = true;

                                 if (!flag && objOved.Taarich >= objOved.objParameters.dChodeshTakanonSoziali && objOved.objPirteyOved.iKodMaamdMishni != clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
                                 {
                                     if (fErechRechiv == 1 && objOved.objMatzavOved.iKod_Headrut == 0 && objOved.Taarich < objOved.oGeneralData._dTaarichHefreshim)
                                     {
                                         fErechRechiv = 0;
                                         addRowToTable(clGeneral.enRechivim.YomHeadrut.GetHashCode(), 1);
                                     }
                                 }
                             }
                             fErechRechiv = float.Parse(Math.Round(fErechRechiv, 2).ToString());
                             addRowToTable(clGeneral.enRechivim.YomChofesh.GetHashCode(), fErechRechiv, fKizuzMeheadrut);
                         }
                     }
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomChofesh.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
            finally
            {
                rowSidur = null;
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
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomTipatChalav.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
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
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomMachalatBenZug.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
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
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomMachalatHorim.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
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
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomMachalaYeled.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv72()
        {
            float fErechRechiv;

            try
            {

                fErechRechiv = CalcHeadruyot(clGeneral.enRechivim.YomKursHasavaLekav.GetHashCode());
                addRowToTable(clGeneral.enRechivim.YomKursHasavaLekav.GetHashCode(), fErechRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomKursHasavaLekav.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        public void CalcRechiv73()
        {
            float fErech;
            try
            {
                oSidur.CalcRechiv73();
                fErech = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.YomShlichutBeChul.GetHashCode(), objOved.Taarich); 
                if (fErech >0)
                    addRowToTable(clGeneral.enRechivim.YomShlichutBeChul.GetHashCode(), 1);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomShlichutBeChul.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: " + ex.Message);
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
                fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);  
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

                    if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                    {
                        fTempW = objOved.fMekademNipuach;
                    }

                    fErechRechiv = float.Parse(Math.Round(fTempW * fTempX, 2, MidpointRounding.AwayFromZero).ToString());
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
            float fErechRechiv, fMichsaYomit, fDakotNochehut, fYomMiluim, fYomShmiratHerayon;
            float fYomMachaltHorim, fYomMachalaBoded, fYomMachala, fYomEvel, fYomMachalatYeled, fYomMachalaBenZug, fYomTeuna;
            DataRow[] rowSidur;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);
                fDakotNochehut = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNochehutLetashlum); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);

                if (objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30)
                {
                    if (fDakotNochehut > 0)
                    {
                        fErechRechiv = 1;
                        addRowToTable(clGeneral.enRechivim.YemeyNochehutLeoved.GetHashCode(), fErechRechiv);
                    }
                }
                else
                {
                   fMichsaYomit = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichsaYomitMechushevet); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);
                   
                   if (fMichsaYomit > 0 && fDakotNochehut > 0)
                   {
                        fErechRechiv = 1;
                        addRowToTable(clGeneral.enRechivim.YemeyNochehutLeoved.GetHashCode(), fErechRechiv);
                   }
                   else
                   {
                        rowSidur = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur=99822");
                        if (rowSidur.Length > 0)
                        {
                            fErechRechiv = 1;
                            addRowToTable(clGeneral.enRechivim.YemeyNochehutLeoved.GetHashCode(), fErechRechiv);
                        }
                   }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YemeyNochehutLeoved.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv76()
        {
            float fDakotRechiv, fMichsaYomit;
           // int iSugYom;
            try
            {
                fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich); 
             //   iSugYom = SugYom;

                if (!(objOved.objMeafyeneyOved.iMeafyen92>0 && oCalcBL.CheckYomShishi(objOved.SugYom)))
                {
                    if (((objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode()) && (objOved.objMeafyeneyOved.sMeafyen32 != "1")) ||
                        objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode() && fMichsaYomit == 0 && oCalcBL.CheckYomShishi(objOved.SugYom))
                    {

                        fDakotRechiv = CalcDayRechiv76(fMichsaYomit, objOved.Taarich);

                        addRowToTable(clGeneral.enRechivim.Nosafot125.GetHashCode(), fDakotRechiv);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.Nosafot125.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private float CalcDayRechiv76(float fMichsaYomit, DateTime dTaarich)
        {
            float fDakotNochehut, fErech, fNochehutBeshishi, fShaotShabat100, fDakotNochechutSidurey100, fDakotNosafotSidurey100;
            fErech = 0;

            try
            {
                 fDakotNosafotSidurey100 = 0;
                //fDakotNochehut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);
                fDakotNochehut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);

                fDakotNochechutSidurey100 = oSidur.GetSumSidurim100(clGeneral.enRechivim.Nosafot125);
                if (fDakotNochechutSidurey100 > fMichsaYomit)
                    fDakotNosafotSidurey100 = fDakotNochechutSidurey100 - fMichsaYomit;
                
                ////SumNochechutMeyuchdim = oSidur.GetSumSidurim100();
                ////if (SumNochechutMeyuchdim > fMichsaYomit)
                ////    fDakotNosafot = SumNochechutMeyuchdim - fMichsaYomit;
               // fDakotNochehut = fDakotNochehut - SumNochechutMeyuchdim;

                fNochehutBeshishi = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.NochehutBeshishi.GetHashCode(), objOved.Taarich); 
                //  fNochehutLetashlum = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);
                if (fMichsaYomit > 0 && fDakotNochehut > 0)
                {
                    if (fDakotNochehut > fMichsaYomit)
                    {
                       //fErech = fDakotNochehut - fDakotNochechutSidurey100;
                        fErech = Math.Min(120, fDakotNochehut - fDakotNosafotSidurey100 - fMichsaYomit);
                    }
                    //else
                    //{
                    //    fErech = fDakotNochehut - fDakotNochechutSidurey100 - fMichsaYomit;
                    //    fErech = Math.Min(120, fErech);
                    //}
                }

                if (fMichsaYomit == 0 && oCalcBL.GetSugYomLemichsa(objOved, dTaarich, objOved.objPirteyOved.iKodSectorIsuk, objOved.objMeafyeneyOved.iMeafyen56) == clGeneral.enSugYom.Shishi.GetHashCode())
                {
                    if ((objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30) && fNochehutBeshishi > 120)
                    {
                        fShaotShabat100 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ShaotShabat100.GetHashCode(), objOved.Taarich);
                        fErech = Math.Min(120, fDakotNochehut - fShaotShabat100);
                    }
                    else if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30) && fNochehutBeshishi > 0)
                        fErech = Math.Min(240, fDakotNochehut - fDakotNochechutSidurey100);
                }

                fErech = float.Parse(Math.Floor(fErech).ToString());
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
          //  int iSugYom;
            try
            {
                fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich); 
               // iSugYom = SugYom;

                if (!(objOved.objMeafyeneyOved.iMeafyen92 > 0 && oCalcBL.CheckYomShishi(objOved.SugYom)))
                {
                    if ((objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode()) ||
                        objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode() && fMichsaYomit == 0 && oCalcBL.CheckYomShishi(objOved.SugYom))
                    {

                        fDakotRechiv = CalcDayRechiv77(fMichsaYomit, objOved.Taarich);

                        addRowToTable(clGeneral.enRechivim.Nosafot150.GetHashCode(), fDakotRechiv);

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.Nosafot150.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private float CalcDayRechiv77(float fMichsaYomit, DateTime dTaarich)
        {
            float fDakotNochehut, fErech, fNosafot125, fDakotNochechutSidurey100, fDakotNosafotSidurey100;
          //  float SumNochechutMeyuchdim;
            fErech = 0;
            Boolean bMafilimSchirim = false;
            DataRow[] dr;
            try
            {
                fDakotNosafotSidurey100 = 0;
                fDakotNochechutSidurey100 = oSidur.GetSumSidurim100(clGeneral.enRechivim.Nosafot150);

                fDakotNochehut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);
                if (fDakotNochechutSidurey100 > fMichsaYomit)
                    fDakotNosafotSidurey100 = fDakotNochechutSidurey100 - fMichsaYomit;
                

             //   fDakotNochehut = fDakotNochehut - SumNochechutMeyuchdim;
                if (fMichsaYomit > 0 && fDakotNochehut > 0 && objOved.objMeafyeneyOved.sMeafyen32 != "1")
                {
                    if (fDakotNochehut > (fMichsaYomit + 120))
                    {
                        fErech = fDakotNochehut - fDakotNosafotSidurey100 - fMichsaYomit - 120;
                        fErech = Math.Max(0, fErech);
                    }
                }
                else if (fMichsaYomit > 0 && fDakotNochehut > 0 && objOved.objMeafyeneyOved.sMeafyen32 == "1")
                {
                    if (fDakotNochehut > fMichsaYomit)
                    {
                        fErech = fDakotNochehut - fMichsaYomit;
                    }
                }
                if (fMichsaYomit == 0 && fDakotNochehut > 0 && oCalcBL.GetSugYomLemichsa(objOved, dTaarich, objOved.objPirteyOved.iKodSectorIsuk, objOved.objMeafyeneyOved.iMeafyen56) == clGeneral.enSugYom.Shishi.GetHashCode() && objOved.objMeafyeneyOved.sMeafyen32 != "1")
                {
                    fErech = fDakotNochehut -fDakotNosafotSidurey100 - 240;
                    fErech = Math.Max(0, fErech);
                }
                else if (fMichsaYomit == 0 && fDakotNochehut > 0 && oCalcBL.GetSugYomLemichsa(objOved, dTaarich, objOved.objPirteyOved.iKodSectorIsuk, objOved.objMeafyeneyOved.iMeafyen56) == clGeneral.enSugYom.Shishi.GetHashCode() && objOved.objMeafyeneyOved.sMeafyen32 == "1")
                {
                    fErech = fDakotNochehut;
                }

                //ו.	עבור מפעילים שכירים במשמרת לילה יש לשלם את שעות 125% ב- 150% כדלקמן: אם העיסוק שליפת פרטי עובד (קוד נתון  HR = 6, מ.א., תאריך) עם ערך = 122,  123,  124,  127 וגם מעמד שכיר שליפת פרטי עובד (קוד נתון  HR = 13, מ.א., תאריך) הספרה הראשונה = 2 וגם [סוג משמרת] = לילה (ראה חישוב [סוג משמרת] למפעילים ברכיב 126) אזי: ערך הרכיב = ערך הרכיב + נוספות 125% (רכיב 76)
                if (objOved.objPirteyOved.iIsuk == 122 || objOved.objPirteyOved.iIsuk == 123 || objOved.objPirteyOved.iIsuk == 124 || objOved.objPirteyOved.iIsuk == 127)
                {
                    if (objOved.objPirteyOved.iKodMaamdRashi == clGeneral.enMaamad.Salarieds.GetHashCode())
                    {
                        if (CheckSugMishmeret() == clGeneral.enSugMishmeret.Liyla.GetHashCode())
                        {
                            bMafilimSchirim = true;
                        }
                    }
                }
                if (bMafilimSchirim)
                {
                    fNosafot125 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Nosafot125.GetHashCode(), objOved.Taarich);
                    fErech = fErech + fNosafot125;
                    objOved._dsChishuv.Tables["CHISHUV_YOM"].Select(null, "KOD_RECHIV");
                    dr = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.Nosafot125.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')");
                    if (dr.Length > 0)
                    {
                        dr[0].Delete();
                    }
                }

                fErech = float.Parse(Math.Floor(fErech).ToString());
                return fErech;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dr = null;
            }
        }


        private void CalcRechiv78()
        {
            float fSumDakotRechiv, fDakotNahagut, fDakotTnua, fDakotTafkid, fTosefetZmanNesia, fDakotZikuyChofesh, fDakotNochehut;
            float fDakotRechiv76, fDakotRechiv77, fZmanRetzifutShabat275, fNosafotShabatKizuz, fMichsaYomit;
            Boolean bShishiErevChag = false, bNotCalc=false;
            float fRechivEzer;
            try
            {
                fSumDakotRechiv = 0;
                fDakotZikuyChofesh = 0;
                fRechivEzer = 0;
                fDakotNochehut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);
                   
               if  (oCalcBL.CheckYomShishi(objOved.SugYom) || oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom))
               {
                   bShishiErevChag = true;

                   fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);

                   if (fDakotNochehut > 0 && fMichsaYomit > 0 && fMichsaYomit > fDakotNochehut)
                   {
                       bNotCalc = true;
                   }
               }

               if (!bNotCalc)
               {
                  // if ((!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30 && objOved.SugYom == clGeneral.enSugYom.Bchirot.GetHashCode())))
                  // {
                       Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);
                       if (clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich) || bShishiErevChag)
                       {

                           fDakotNahagut = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNehigaShabat); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNehigaShabat.GetHashCode(), objOved.Taarich);
                           fDakotTnua = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNihulTnuaShabat); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode(), objOved.Taarich);
                           fDakotTafkid = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotTafkidShabat); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotTafkidShabat.GetHashCode(), objOved.Taarich);
                           fDakotZikuyChofesh = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotZikuyChofesh); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode(), objOved.Taarich);
                           fTosefetZmanNesia = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanNesia); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanNesia.GetHashCode(), objOved.Taarich);
                           //  fDakotZikuy100 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ShaotShabat100.GetHashCode(), objOved.Taarich);
                           fZmanRetzifutShabat275 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanRetzifutShabat); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanRetzifutShabat.GetHashCode(), objOved.Taarich);

                           fSumDakotRechiv = (fDakotNahagut + fDakotTnua + fDakotTafkid + fZmanRetzifutShabat275) - (fDakotZikuyChofesh + fTosefetZmanNesia);

                           if (objOved.objMeafyeneyOved.iMeafyen92>0 &&  oCalcBL.CheckYomShishi(objOved.SugYom))
                           {
                               fRechivEzer = fDakotNochehut - fSumDakotRechiv;
                           }
                           addRowToTable(clGeneral.enRechivim.NosafotShabat.GetHashCode(), fSumDakotRechiv, fRechivEzer);
                       }
                       fNosafotShabatKizuz = 0;
                       if (fSumDakotRechiv > 0)
                       { fNosafotShabatKizuz = fSumDakotRechiv; }
                       else if (fDakotZikuyChofesh > 0)
                       { fNosafotShabatKizuz = fDakotZikuyChofesh; }

                       if (fNosafotShabatKizuz > 0)
                       {
                           //קיזוז נוספות שבת (200%) (רכיב 78) מרכיבים נוספות 125% ונוספות 150%:
                           kizuzNosafotShabat(fNosafotShabatKizuz, clGeneral.enRechivim.Nosafot125, clGeneral.enRechivim.Nosafot150);

                           //קיזוז נוספות שבת (200%) (רכיב 78) מרכיבים שעות 25% ושעות 50%:
                           kizuzNosafotShabat(fNosafotShabatKizuz, clGeneral.enRechivim.Shaot25, clGeneral.enRechivim.Shaot50);

                       }
                 //  }
               }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NosafotShabat.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void kizuzNosafotShabat(float fErechNosafotShabet, clGeneral.enRechivim RechivLekizuz1, clGeneral.enRechivim RechivLekizuz2)
        {
            float fDakotRechiv1, fDakotRechiv2;
            try
            {
                fDakotRechiv1 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], RechivLekizuz1.GetHashCode(), objOved.Taarich);
                fDakotRechiv2 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], RechivLekizuz2.GetHashCode(), objOved.Taarich); 
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
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NosafotShabat.GetHashCode(), objOved.Taarich, "CalcDay:kizuzNosafotShabet: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }
        private void CalcRechiv80()
        {
            float fSumDakotRechiv, fTempX, fNosafotTafkid;
            try
            {
                oSidur.CalcRechiv80();

               // fZmanNesia = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanNesia.GetHashCode(), objOved.Taarich);
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode(), objOved.Taarich);

                fTempX = fSumDakotRechiv;// +fZmanNesia;

                fNosafotTafkid = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNosafotTafkid.GetHashCode(), objOved.Taarich);

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
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv81()
        {
            float fSumDakotRechiv, fDakotMichutzTafkid, fDakotMichutzNihul;
            try
            {
                oSidur.CalcRechiv81();

                fDakotMichutzTafkid = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichutzLamichsaTafkidShabat.GetHashCode(), objOved.Taarich);
                fDakotMichutzNihul = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichutzLamichsaNihulShabat.GetHashCode(), objOved.Taarich);

                fSumDakotRechiv = fDakotMichutzTafkid + fDakotMichutzNihul;

                addRowToTable(clGeneral.enRechivim.DakotMichutzLamichsaShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMichutzLamichsaShabat.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv85()
        {
            float fSumDakotRechiv, fSumRetxzigfutNehiga, fSumRetzifutTafkid;
            try
            {
                oSidur.CalcRechiv85();

                fSumRetxzigfutNehiga = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode(), objOved.Taarich);
                fSumRetzifutTafkid = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode(), objOved.Taarich);

                fSumDakotRechiv = fSumRetxzigfutNehiga + fSumRetzifutTafkid;

                addRowToTable(clGeneral.enRechivim.SachTosefetRetzifut.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachTosefetRetzifut.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv86()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv86();

                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.KizuzDakotHatchalaGmar.GetHashCode(), objOved.Taarich);

                addRowToTable(clGeneral.enRechivim.KizuzDakotHatchalaGmar.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzDakotHatchalaGmar.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv88()
        {
            float fSumDakotRechiv, fMichsaYomit;
            DataRow[] drSidurim;
            try
            {
                drSidurim = objOved.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime') and MISPAR_SIDUR IN(99011,99207,99007)");
                if (drSidurim.Length == 0)
                {
                    //א.	יום העבודה הינו א–ה וגם היום אינו שבתון סוג יום שליפת סוג יום (תאריך) = 01 
                    if (objOved.SugYom < clGeneral.enSugYom.Shishi.GetHashCode())
                    {
                        fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);

                        if (fMichsaYomit > 0)
                        {
                            if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30))
                            {
                                //ב.	עיסוק ראשי של העובד (התו הראשון בקוד העיסוק [שליפת פרטי עובד (קוד נתון HR=6, מ.א., תאריך)]) <> 5
                                if (objOved.objPirteyOved.iIsuk < 500 || objOved.objPirteyOved.iIsuk >= 600)// ((objOved.objPirteyOved.iIsuk.ToString()).Substring(0, 1) != "5")
                                {

                                    if (objOved.objMeafyeneyOved.iMeafyen30 == 1)
                                    {
                                        oSidur.CalcRechiv88();

                                        fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode(), objOved.Taarich);

                                        if (fSumDakotRechiv >= 18)
                                        { fSumDakotRechiv = 18; }
                                       // else fSumDakotRechiv = 0;

                                        addRowToTable(clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode(), fSumDakotRechiv);
                                    }

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv89()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv89();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.KizuzBevisa.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.KizuzBevisa.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzBevisa.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
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
                iZmanMutamut = objOved.objPirteyOved.iZmanMutamut;
                fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);
                   
                if (iZmanMutamut == 0)
                {
                    fMichsatMutam = fMichsaYomit;
                }
                else { fMichsatMutam = iZmanMutamut; }

                if (objOved.objPirteyOved.iMutamut > 0)
                {
                    iMutam = objOved.objPirteyOved.iMutamut;
                    fDakotNochehut1 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);
                    fDakotNehiga = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNehigaChol.GetHashCode(), objOved.Taarich);

                    if (iMutam == 1 && fMichsatMutam < fDakotNochehut1)
                    { fDakotRechiv = fDakotNochehut1 - fMichsatMutam; }
                    if (iMutam == 2 && fMichsatMutam < fDakotNehiga)
                    { fDakotRechiv = fDakotNehiga - fMichsatMutam; }
                    if (iMutam == 3) // && fMichsatMutam < fDakotNochehut1)
                    {
                        if ((fDakotNehiga - fMichsatMutam) < 0)
                            fTempZ = 0;
                        else fTempZ = fDakotNehiga - fMichsatMutam;

                        fDakotRechiv = fTempZ;
                        fTempY = fDakotNochehut1 - fTempZ;
                        if(fTempY > fMichsaYomit)
                            fDakotRechiv += (fTempY - fMichsaYomit);
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

                    fDakotNochehut1 -= fDakotRechiv;
                    addRowToTable(clGeneral.enRechivim.KizuzOvedMutam.GetHashCode(), fDakotRechiv);
                    addRowToTable(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), fDakotNochehut1);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzOvedMutam.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv100()
        {
            float fSumDakotRechiv, fMichsaYomit, fDakotNochehut;
            try
            {
                if (objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30)
                {
                    fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);
                    fDakotNochehut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);

                    if (fMichsaYomit > 0 && fDakotNochehut > 0)
                    {
                        if (fMichsaYomit <= fDakotNochehut)
                            fSumDakotRechiv = fMichsaYomit;
                        else fSumDakotRechiv = fDakotNochehut;

                        addRowToTable(clGeneral.enRechivim.Shaot100Letashlum.GetHashCode(), fSumDakotRechiv);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.Shaot100Letashlum.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: " + ex.Message);
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
                if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30))
                {
                    if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
                    {
                        fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);

                        if (fMichsaYomit > 0 || (objOved.objMeafyeneyOved.iMeafyen92>0 && oCalcBL.CheckYomShishi(objOved.SugYom)))
                        {
                            fDakotRechiv = CalcDayRechiv76(fMichsaYomit, objOved.Taarich);

                            addRowToTable(clGeneral.enRechivim.Shaot25.GetHashCode(), fDakotRechiv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.Shaot25.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv92()
        {
            float fMichsaYomit, fDakotRechiv, fNochehutLetashlum;
            try
            {
                fDakotRechiv = 0;
                if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30))
                {
                    fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);
                    fNochehutLetashlum = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);

                    //יש לחשב את הרכיב עבור עובדים בעלי מאפיין ביצוע 56 עם ערך 52: [שליפת מאפיין ביצוע (קוד מאפיין = 56, מ.א., תאריך)] עם ערך = 52 (עובד 5 ימים חודשי).
                    //אחרת, אין לפתוח רשומה לרכיב.
                    //אם מכסה יומית  מחושבת (רכיב 126) > 0 
                    //ערך הרכיב = ערך רכיב נוספות 150% (רכיב 77) 
                    if (fMichsaYomit > 0 || (objOved.objMeafyeneyOved.iMeafyen92 > 0 && oCalcBL.CheckYomShishi(objOved.SugYom)))
                    {
                        if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
                        {

                            fDakotRechiv = CalcDayRechiv77(fMichsaYomit, objOved.Taarich);
                        }
                        else if (objOved.objPirteyOved.iIsuk == 122 || objOved.objPirteyOved.iIsuk == 123 || objOved.objPirteyOved.iIsuk == 124 || objOved.objPirteyOved.iIsuk == 127)
                        {
                            if (objOved.objPirteyOved.iKodMaamdRashi == clGeneral.enMaamad.Salarieds.GetHashCode())
                            {
                                if (CheckSugMishmeret() == clGeneral.enSugMishmeret.Liyla.GetHashCode())
                                {
                                    oSidur.CalcRechiv92(fMichsaYomit, fNochehutLetashlum);
                                    fDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.Shaot50.GetHashCode(), objOved.Taarich);
                                    fDakotRechiv = fMichsaYomit - fDakotRechiv;
                                }
                            }
                        }
                    }

                    addRowToTable(clGeneral.enRechivim.Shaot50.GetHashCode(), fDakotRechiv);

                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.Shaot50.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv93()
        {
            DataRow[] rowZmanHalbash, rowMezakeHalbash, rowSidurim;
            float fDakotRechiv, fMichsaYomit, fErech, fHalbashaTchilatYom, fHalbashaSofYom, fSachDakotTafkid, fDakotTafkidChol, fDakotTafkidShabat, fDakotTafkidShishi;
            DataRow RowKodem, RowNext;
            int iSugYom;
            try
            {
                //תוספת זמן הלבשה (רכיב 93
                // 0 <  TB_Yamey_Avoda_Ovdim.Halbashaיש לחשב את הרכיב רק אם 
                // א.	ערך הרכיב = [הלבשה תחילת יום] + [הלבשה סוף יום]
                fSachDakotTafkid = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.SachDakotTafkid.GetHashCode(), objOved.Taarich);
                fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);

                rowZmanHalbash = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and halbasha > 0");
                if (rowZmanHalbash.Length > 0 && fSachDakotTafkid >= objOved.objParameters.iMinYomAvodaForHalbasha)
                {
                    rowSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0", "shat_hatchala_sidur asc"); ;
                    RowKodem = rowSidurim[0];
                    fHalbashaTchilatYom = 0;
                    fHalbashaSofYom = 0;

                    //ב.	חישוב [הלבשה תחילת יום]:
                    //אם TB_Yamey_Avoda_Ovdim.Halbasha = 1 או 3 אזי יש לזהות את הסידור הראשון ביום שמזכה להלבשה TB_Sidurim_Ovedim.Mezake_Halbasha= 1 אם אין לפניו סידור אחר אזי [הלבשה תחילת יום] = 10 דקות [שליפת פרמטר (קוד פרמטר = 143)]
                    //אחרת, [הלבשה תחילת יום] = הנמוך מבין (10 דקות [שליפת פרמטר (קוד פרמטר = 143)], שעת התחלה לתשלום של הסידור המזכה הלבשה פחות שעת גמר לתשלום של הסידור שלפניו)
                    rowMezakeHalbash = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and (Mezake_Halbasha=1 or Mezake_Halbasha=3)", "shat_hatchala_sidur asc"); ;
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
                                fHalbashaTchilatYom = objOved.objParameters.iZmanHalbash;
                            else fHalbashaTchilatYom = Math.Min(float.Parse((DateTime.Parse(rowMezakeHalbash[0]["shat_hatchala_letashlum"].ToString()) - DateTime.Parse(RowKodem["shat_gmar_letashlum"].ToString())).TotalMinutes.ToString()), objOved.objParameters.iZmanHalbash);


                        }
                    }



                    //ג.	חישוב [הלבשה סוף יום]:
                    //אם TB_Yamey_Avoda_Ovdim.Halbasha = 2 או 3 אזי יש לזהות את הסידור האחרון ביום שמזכה להלבשה TB_Sidurim_Ovedim.Mezake_Halbasha= 1. אם אין אחריו סידור אחר אזי [הלבשה סוף יום] = 10 דקות [שליפת פרמטר (קוד פרמטר = 143)]
                    //אחרת, [הלבשה סוף יום] = הנמוך מבין (10 דקות [שליפת פרמטר (קוד פרמטר = 143)], שעת התחלה לתשלום של הסידור שאחרי הסידור המזכה הלבשה פחות שעת גמר לתשלום של הסידור המזכה הלבשה)
                    rowMezakeHalbash = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and (Mezake_Halbasha=2 or Mezake_Halbasha=3)", "shat_hatchala_sidur desc"); ;
                    if (rowMezakeHalbash.Length > 0)
                    {
                        RowNext = rowSidurim[0];
                        for (int I = 0; I < rowSidurim.Length; I++)
                        {
                            RowNext = rowSidurim[I];
                            if ((rowMezakeHalbash[0]["mispar_sidur"].ToString() == rowSidurim[I]["mispar_sidur"].ToString()) && (rowMezakeHalbash[0]["shat_hatchala_sidur"].ToString() == rowSidurim[I]["shat_hatchala_sidur"].ToString()))
                            {
                                if (I < rowSidurim.Length - 1)
                                {
                                    I += 1;
                                    RowNext = rowSidurim[I];
                                }
                                break;

                            }
                        }

                        if ((rowZmanHalbash[0]["halbasha"].ToString() == "2") || (rowZmanHalbash[0]["halbasha"].ToString() == "3"))
                        {
                            if ((rowMezakeHalbash[0]["mispar_sidur"].ToString() == RowNext["mispar_sidur"].ToString()) && (rowMezakeHalbash[0]["shat_hatchala_sidur"].ToString() == RowNext["shat_hatchala_sidur"].ToString()))
                                fHalbashaSofYom = objOved.objParameters.iZmanHalbash;
                            else fHalbashaSofYom = Math.Min(float.Parse((DateTime.Parse(RowNext["shat_hatchala_letashlum"].ToString()) - DateTime.Parse(rowMezakeHalbash[0]["shat_gmar_letashlum"].ToString())).TotalMinutes.ToString()), objOved.objParameters.iZmanHalbash);

                        }
                    }

                    fDakotRechiv = fHalbashaTchilatYom + fHalbashaSofYom;
                    addRowToTable(clGeneral.enRechivim.ZmanHalbasha.GetHashCode(), fDakotRechiv);

                    //אם דקות תפקיד חול (רכיב 4) > 0 יש להוסיף את רכיב 93 לרכיב 4.
                    fErech = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), objOved.Taarich);
                    if (fErech>0)
                        addRowToTable(clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), (fErech + fDakotRechiv));
                    //אם דקות תפקיד בשישי (רכיב 193) > 0 יש להוסיף את רכיב 93 לרכיב 193.
                    fErech = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(), objOved.Taarich);
                    if (fErech > 0)
                        addRowToTable(clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(), (fErech + fDakotRechiv));
                    //אם דקות שבת בתפקיד (רכיב 37) > 0 יש להוסיף את רכיב 93 לרכיב 37.
                    fErech = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotTafkidShabat.GetHashCode(), objOved.Taarich);
                    if (fErech > 0)
                        addRowToTable(clGeneral.enRechivim.DakotTafkidShabat.GetHashCode(), (fErech + fDakotRechiv));
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ZmanHalbasha.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
            finally
            {
                rowZmanHalbash=null;
                rowMezakeHalbash=null;
                rowSidurim=null;
                RowKodem=null;
                RowNext=null;
            }
        }

        private void CalcRechiv94()
        {
            float fDakotNocheut, fErechRechiv, fMichsaYomit, fTempX, fHashlamaTafkid, fHashlamaNihul, fHashlamaNahagut;
            //תוספת זמן השלמה – רכיב 94 : 
            try
            {
                fErechRechiv = 0;
                if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30))
                {
                    //oSidur.CalcRechiv94();
                    //fErechRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.ZmanHashlama.GetHashCode(), objOved.Taarich);

                    if (objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and Hashlama_Leyom =1").Length > 0)
                    {
                        if (oCalcBL.CheckUshraBakasha(clGeneral.enKodIshur.HashlamaLeyom.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich) || oCalcBL.CheckUshraBakasha(clGeneral.enKodIshur.HashlamatShaotLemutamut.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich))
                        {
                            Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                            fHashlamaTafkid = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.HashlamaBetafkid); 
                            fHashlamaNihul = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.HashlamaBenihulTnua); 
                            fHashlamaNahagut = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.HashlamaBenahagut); 
                            fDakotNocheut = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNochehutLetashlum);
                            //if (objOved.objPirteyOved.iZmanMutamut > 0)
                            //    fMichsaYomit = objOved.objPirteyOved.iZmanMutamut;
                            //else
                                fMichsaYomit = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichsaYomitMechushevet);

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
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ZmanHashlama.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv95()
        {
            DataRow[] rowMezakeNesia, rowSidurim, rowBitulZmanNesiot;
            float fDakotRechiv, fNesiotTchilatYom, fNesiotSofYom, fSachNihulTnua, fSachNahagut, fSachTafkid;
            float fDakotTafkidChol, fDakotTafkidShabat, fDakotTafkidShishi, fMichsaYomit;
            DataRow RowKodem, RowNext;
            int iSugYom;
            bool bLogNahag = false;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                fSachNihulTnua = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachDakotNihulTnua); 
                fSachNahagut = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachDakotNahagut); 
                fSachTafkid = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.SachDakotTafkid.GetHashCode(), objOved.Taarich);
                fMichsaYomit = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichsaYomitMechushevet);

                if ((objOved.objPirteyOved.iIsuk.ToString()).Substring(0, 1) != "5")
                    bLogNahag = true;
                // תנאי מקדים לחישוב הרכיב: 
                //אם העובד הוא קופאי - עיסוק העובד שליפת פרטי עובד (קוד נתון  HR = 6, מ.א., תאריך)  = 183, 184, 193 וגם סהכ ניהול תנועה (רכיב 190) גדול שווה 240 [שליפת פרמטר (קוד פרמטר = 238)]
                //אחרת – אם עובד אינו קופאי:
                //וגם {דקות סהכ נהגות (רכיב 188) +  וגם סהכ ניהול תנועה (רכיב 190) גדול שווה 180 דקות [שליפת פרמטר (קוד פרמטר = 239)] או סהכ תפקיד (רכיב 192) גדול שווה 240 דקות [שליפת פרמטר (קוד פרמטר = 238)]}

                if (((objOved.objPirteyOved.iIsuk == 183 || objOved.objPirteyOved.iIsuk == 184 || objOved.objPirteyOved.iIsuk == 193) && fSachNihulTnua > objOved.objParameters.iMinDakotLezakautNesiot)
                            || ((objOved.objPirteyOved.iIsuk != 183 && objOved.objPirteyOved.iIsuk != 184 && objOved.objPirteyOved.iIsuk != 193) && (fSachNahagut + fSachNihulTnua) > objOved.objParameters.iMinDakotNehigaVetnuaLezakautNesiot || fSachTafkid >= objOved.objParameters.iMinDakotLezakautNesiot))
                {
                    //תוספת זמן נסיעה – רכיב 95:
                    //א.	ערך הרכיב = [נסיעות תחילת יום] + [נסיעות סוף יום]
                    //יש לפתוח רשומה לרכיב רק אם סכום [נסיעות תחילת יום] + [נסיעות סוף יום] > 0


                    rowSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0", "shat_hatchala_sidur asc"); ;
                    if (rowSidurim.Length > 0)
                    {
                        RowKodem = rowSidurim[0];
                        fNesiotTchilatYom = 0;
                        fNesiotSofYom = 0;

                        //ב.	חישוב [ תחילת יום]:
                        rowBitulZmanNesiot = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and (Bitul_Zman_nesiot =1 or Bitul_Zman_nesiot =3)", "shat_hatchala_sidur asc"); ;
                        if (rowBitulZmanNesiot.Length > 0)
                        {
                            rowMezakeNesia = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and Mezake_nesiot>0", "shat_hatchala_sidur asc"); ;

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
                                
                                if (fNesiotTchilatYom < 0)
                                    fNesiotTchilatYom = 0;
                                
                            }
                        }
                        //ג.	חישוב [ סוף יום]:
                        rowBitulZmanNesiot = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and (Bitul_Zman_nesiot =2 or Bitul_Zman_nesiot =3)", "shat_hatchala_sidur asc"); ;
                        if (rowBitulZmanNesiot.Length > 0)
                        {
                            rowMezakeNesia = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and Mezake_nesiot>0", "shat_hatchala_sidur asc"); ;

                            if (rowMezakeNesia.Length > 0)
                            {
                                RowNext = rowSidurim[0];
                                for (int I = 0; I < rowSidurim.Length; I++)
                                {
                                    RowNext = rowSidurim[I];
                                    if ((rowMezakeNesia[rowMezakeNesia.Length - 1]["mispar_sidur"].ToString() == rowSidurim[I]["mispar_sidur"].ToString()) && (rowMezakeNesia[rowMezakeNesia.Length -1]["shat_hatchala_sidur"].ToString() == rowSidurim[I]["shat_hatchala_sidur"].ToString()))
                                    {
                                        if (I < (rowSidurim.Length - 1))
                                        {
                                            RowNext = rowSidurim[I + 1];
                                        }

                                        break;
                                    }
                                }


                                if ((rowMezakeNesia[rowMezakeNesia.Length - 1]["mispar_sidur"].ToString() == RowNext["mispar_sidur"].ToString()) && (rowMezakeNesia[rowMezakeNesia.Length -1]["shat_hatchala_sidur"].ToString() == RowNext["shat_hatchala_sidur"].ToString()))
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
                                    fNesiotSofYom = int.Parse(rowMezakeNesia[0]["Zman_Nesia_Hazor"].ToString());
                                    if (bLogNahag)
                                        fNesiotSofYom = fNesiotSofYom - 45;
                                    fNesiotSofYom = Math.Min(fNesiotSofYom, float.Parse((DateTime.Parse(RowNext["shat_hatchala_letashlum"].ToString()) - DateTime.Parse(rowMezakeNesia[0]["shat_gmar_letashlum"].ToString())).TotalMinutes.ToString()));
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

                                if (fNesiotSofYom < 0)
                                    fNesiotSofYom = 0;
                            }
                        }

                        fDakotRechiv = fNesiotTchilatYom + fNesiotSofYom;

                        addRowToTable(clGeneral.enRechivim.ZmanNesia.GetHashCode(), fDakotRechiv);

                        iSugYom = oCalcBL.GetSugYomLemichsa(objOved, objOved.Taarich, objOved.objPirteyOved.iKodSectorIsuk, objOved.objMeafyeneyOved.iMeafyen56);
                        //if (iSugYom == clGeneral.enSugYom.Shishi.GetHashCode() && fMichsaYomit == 0)
                        //{
                        //    fDakotTafkidShishi = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachDakotTafkidShishi); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(), objOved.Taarich);
                        //    addRowToTable(clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(), (fDakotTafkidShishi + fDakotRechiv));

                        //}
                        //else
                        if (objOved.Taarich.DayOfWeek.GetHashCode() + 1 == clGeneral.enDay.Shabat.GetHashCode() || clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, iSugYom, objOved.Taarich))
                        {
                            fDakotTafkidShabat = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotTafkidShabat); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotTafkidShabat.GetHashCode(), objOved.Taarich);

                            addRowToTable(clGeneral.enRechivim.DakotTafkidShabat.GetHashCode(), (fDakotTafkidShabat + fDakotRechiv));

                        }
                        //else
                        //{
                        //    fDakotTafkidChol = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotTafkidChol); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), objOved.Taarich);

                        //    addRowToTable(clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), (fDakotTafkidChol + fDakotRechiv));
                        //}

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ZmanNesia.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv96()
        {
            float fSumDakotRechiv;
            float fSumDakotLaylaEgged, fSumDakotLaylaChok, fSumDakotLaylaBoker, fSumDakotShabat;
            try
            {
                fSumDakotLaylaEgged = 0; fSumDakotLaylaChok = 0; fSumDakotLaylaBoker = 0; fSumDakotShabat = 0;
                fSumDakotRechiv = oSidur.CalcRechiv96(ref fSumDakotLaylaEgged, ref fSumDakotLaylaChok, ref fSumDakotLaylaBoker, ref fSumDakotShabat);

                addRowToTable(clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode(), fSumDakotRechiv);

                //רציפות לילה אגד  
                if (fSumDakotLaylaEgged > 0 && fSumDakotLaylaEgged > fSumDakotRechiv && fSumDakotLaylaChok == 0)
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

                if (clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich) && fSumDakotRechiv > 0)
                {
                    addRowToTable(clGeneral.enRechivim.ZmanRetzifutShabat.GetHashCode(), fSumDakotRechiv);
                }
                else if ((oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom) || oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom)) && fSumDakotShabat > 0)
                {
                    addRowToTable(clGeneral.enRechivim.ZmanRetzifutShabat.GetHashCode(), fSumDakotShabat);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
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

                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                fTempX = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanRetzifutTafkid); 
                fTempX += oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanRetzifutNehiga); 

                if (fTempX <= objOved.objParameters.iMaxRetzifutChodshitLetashlum && (fSumDakotRechiv + fTempX) > objOved.objParameters.iMaxRetzifutChodshitImGlisha)
                {
                    fSumDakotRechiv = objOved.objParameters.iMaxRetzifutChodshitLetashlum - fTempX;
                    addRowToTable(clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode(), fSumDakotRechiv);
                }
                else if (fTempX <= objOved.objParameters.iMaxRetzifutChodshitLetashlum && (fSumDakotRechiv + fTempX) < objOved.objParameters.iMaxRetzifutChodshitImGlisha)
                    addRowToTable(clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode(), fSumDakotRechiv);

                //רציפות לילה  
                fTempZ1 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanRetzifutLaylaEgged); 
                if (fSumDakotLaylaEgged > 0 && fSumDakotLaylaEgged > fSumDakotRechiv && fSumDakotLaylaChok == 0)
                    addRowToTable(clGeneral.enRechivim.ZmanRetzifutLaylaEgged.GetHashCode(), fTempZ1 + fSumDakotRechiv);
                else
                    addRowToTable(clGeneral.enRechivim.ZmanRetzifutLaylaEgged.GetHashCode(), fTempZ1 + fSumDakotLaylaEgged);

                fTempZ2 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanRetzifutLaylaChok); 
                if (fSumDakotLaylaChok > 0 && fSumDakotLaylaChok > fSumDakotRechiv && fSumDakotLaylaEgged == 0)
                    addRowToTable(clGeneral.enRechivim.ZmanRetzifutLaylaChok.GetHashCode(), fTempZ2 + fSumDakotRechiv);
                else
                    addRowToTable(clGeneral.enRechivim.ZmanRetzifutLaylaChok.GetHashCode(), fTempZ2 + fSumDakotLaylaChok);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv109()
        {
            float fSumDakotRechiv, fMichsaMechushevet, fDakotNochehut;
            DataRow[] drRowSidurim;
            int iKaymRechiv;
            string sRechivim;
            try
            {
                fSumDakotRechiv = 0;

                if (objOved.SugYom == clGeneral.enSugYom.ErevYomHatsmaut.GetHashCode() && (objOved.objMeafyeneyOved.sMeafyen63 != "" && objOved.objMeafyeneyOved.sMeafyen63 != "0"))
                {
                    fSumDakotRechiv = 1;
                }

                if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
                {
                    drRowSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and MISPAR_SIDUR=99822", "shat_hatchala_sidur asc");
                       
                    if (objOved.objPirteyOved.iKodMaamdRashi == clGeneral.enMaamad.Friends.GetHashCode())
                    {
                        if (drRowSidurim.Length > 0)
                        {
                            fSumDakotRechiv = 1;
                        }
                    }
                    else if (objOved.objPirteyOved.iKodMaamdRashi == clGeneral.enMaamad.Salarieds.GetHashCode())
                    {
                        sRechivim = clGeneral.enRechivim.YomMachla.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomMachalaBoded.GetHashCode().ToString() + "," +
                                    clGeneral.enRechivim.YomTeuna.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomShmiratHerayon.GetHashCode().ToString() + "," +
                                    clGeneral.enRechivim.YomMachalatBenZug.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomEvel.GetHashCode().ToString() + "," +
                                    clGeneral.enRechivim.YomMachalatHorim.GetHashCode().ToString() + "," + clGeneral.enRechivim.YomMachalaYeled.GetHashCode().ToString();
                        iKaymRechiv = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV IN(" + sRechivim + ")").Length;

                        if (drRowSidurim.Length > 0 && iKaymRechiv == 0)
                        {
                            fSumDakotRechiv = 1;
                        }
                    }
                    if (!oCalcBL.CheckYomShishi(objOved.SugYom) && !clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                    {
                        fDakotNochehut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);
                        if (fDakotNochehut > objOved.objParameters.iMinDakotNechshavYomAvoda)
                            fSumDakotRechiv = 1;
                    }
                }

                if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode())
                {
                    drRowSidurim = null;
                    drRowSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and Hashlama_Leyom=1", "shat_hatchala_sidur asc");
                    if (drRowSidurim.Length > 0)
                    {
                        fSumDakotRechiv = 1;
                    }
                }

                if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                {
                    fMichsaMechushevet = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);
                    fDakotNochehut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);
                     if (fDakotNochehut > 0 && fMichsaMechushevet >0)
                     {
                         if (fDakotNochehut > fMichsaMechushevet)
                               fSumDakotRechiv = 1;
                         else fSumDakotRechiv =float.Parse(( Math.Truncate((fDakotNochehut / fMichsaMechushevet) * 100) / 100).ToString());
                     }
                }

                addRowToTable(clGeneral.enRechivim.YemeyAvoda.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YemeyAvoda.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
            finally
            {
                drRowSidurim = null;
            }
        }

        private void CalcRechiv110()
        {
            float fDakotNochehut, fMichsaMechushevet, fYomMachala, fYomMachaltHorim, fYomMachalaBoded, fYomMachalaBenZug;
            float fYomShmiratHerayon, fYomEvel, fYomMachalatYeled, fYomHadracha, fYomTeuna, fYomKursHasavaLekav,fYomMiluim;

            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);


                fDakotNochehut = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNochehutLetashlum); 
                fMichsaMechushevet = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichsaYomitMechushevet);

                fYomMachala = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMachla); 
                fYomMachalaBoded = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMachalaBoded);
                fYomTeuna = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomTeuna); 
                fYomShmiratHerayon = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomShmiratHerayon);
                fYomMachalaBenZug = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMachalatBenZug); 
                fYomEvel = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomEvel); 
                fYomMachaltHorim = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMachalatHorim); 
                fYomMachalatYeled = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMachalaYeled); 
                fYomKursHasavaLekav = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomKursHasavaLekav);
                fYomHadracha = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomHadracha);
                fYomMiluim = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMiluim); 

                if (fDakotNochehut > 0 && fMichsaMechushevet > 0 && fYomMachala == 0 && fYomMachalaBoded == 0
                    && fYomShmiratHerayon == 0 && fYomEvel == 0 && fYomMachalatYeled == 0 && fYomHadracha == 0 && fYomMiluim==0
                    && fYomKursHasavaLekav == 0 && fYomMachalaBenZug == 0 && fYomTeuna == 0 && fYomMachaltHorim == 0)
                {
                    addRowToTable(clGeneral.enRechivim.YemeyAvodaLeloMeyuchadim.GetHashCode(), 1);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YemeyAvodaLeloMeyuchadim.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv125()
        {
            float fSumDakotRechiv;
            DateTime dShatGmarAvoda;
            string sSidurimMeyuchadim = "";
            try
            {
                //יש לבצע את חישוב הרכיב רק עבור עובדים עם [שליפת מאפיין ביצוע (קוד מאפיין = 42, מ.א., תאריך)] עם ערך 70, אחרת אין לפתוח רשומה לרכיב זה בשום רמה.
                if (objOved.objMeafyeneyOved.sMeafyen42 == "1" &&  objOved.SugYom < clGeneral.enSugYom.Shishi.GetHashCode())
                {
                    oSidur.CalcRechiv125();

                    //א.	X = סכימת ערך הרכיב לכל הסידורים ביום העבודה.
                    //אם .ב	X  >= 360 (6 שעות) [שליפת פרמטר (קוד פרמטר = 186)] וגם שעת גמר עבודה  >= 1930 [שליפת פרמטר (קוד פרמטר = 187)] אזי: ערך הרכיב  = 1

                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.MishmeretShniaBameshek.GetHashCode(), objOved.Taarich);
                    if (fSumDakotRechiv > objOved.objParameters.iMinZmanMishmeretShniaBameshek)
                    {
                        sSidurimMeyuchadim = oSidur.GetSidurimMeyuchRechiv(clGeneral.enRechivim.MishmeretShniaBameshek.GetHashCode());
                        if (sSidurimMeyuchadim.Length > 0)
                        {
                            dShatGmarAvoda = DateTime.Parse(objOved.DtYemeyAvodaYomi.Select("MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ")", "SHAT_GMAR_SIDUR DESC")[0]["SHAT_GMAR_SIDUR"].ToString());
                            if (dShatGmarAvoda >= objOved.objParameters.dSiyumMishmeretShniaBameshek)
                            {
                                addRowToTable(clGeneral.enRechivim.MishmeretShniaBameshek.GetHashCode(), 1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.MishmeretShniaBameshek.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        public void CalcRechiv126(DateTime dTaarich)
        {
            //מכסה יומית מחושבת רכיב 126 
            int iSugYom, iSugMishmeret, iMeafyen1, iSugYomLemichsa;
            DataRow[] rowSidurim;
            float fErechRechiv = 0;
            iSugYomLemichsa = 0;
           // bool flag = false;
            objOved.Taarich = dTaarich;

            try
            {
                iSugYom = 0;
                fErechRechiv = oCalcBL.getMichsaYomit(objOved, ref iSugYom);

                //fErechRechiv = oCalcBL.GetMichsaYomit(objOved, ref iSugYomLemichsa);

                //iSugYom = iSugYomLemichsa;
                //if (iSugYom == clGeneral.enSugYom.Purim.GetHashCode() && objOved.objPirteyOved.iEzor == clGeneral.enEzor.Yerushalim.GetHashCode())
                //{
                //    if (!oCalcBL.CheckYomShishi(iSugYom))
                //    {
                //        iSugYomLemichsa = clGeneral.enSugYom.Chol.GetHashCode();
                //        fErechRechiv = oCalcBL.GetMichsaYomit(objOved, ref iSugYomLemichsa);
                //    }
                //    else
                //    {
                //        iSugYomLemichsa = clGeneral.enSugYom.Shishi.GetHashCode();
                //        fErechRechiv = oCalcBL.GetMichsaYomit(objOved, ref iSugYomLemichsa);
                //    }
                //}

                //if (iSugYom == clGeneral.enSugYom.ShushanPurim.GetHashCode() && objOved.objPirteyOved.iEzor == clGeneral.enEzor.Yerushalim.GetHashCode())
                //{
                //    iSugYomLemichsa = clGeneral.enSugYom.Purim.GetHashCode();
                //    fErechRechiv = oCalcBL.GetMichsaYomit(objOved, ref iSugYomLemichsa);
                //}

                //if (iSugYom == clGeneral.enSugYom.ShushanPurim.GetHashCode() && objOved.objPirteyOved.iEzor != clGeneral.enEzor.Yerushalim.GetHashCode())
                //{
                //    if (!oCalcBL.CheckYomShishi(iSugYom))
                //    {
                //        iSugYomLemichsa = clGeneral.enSugYom.Chol.GetHashCode();
                //        fErechRechiv = oCalcBL.GetMichsaYomit(objOved, ref iSugYomLemichsa);
                //    }
                //    else
                //    {
                //        iSugYomLemichsa = clGeneral.enSugYom.Shishi.GetHashCode();
                //        fErechRechiv = oCalcBL.GetMichsaYomit(objOved, ref iSugYomLemichsa);
                //    }
                //}

                if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30))
                {
                    iMeafyen1 = int.Parse(objOved.objMeafyeneyOved.sMeafyen1);
                    //יוצאים מהכלל/הנחות ברמת יום עבודה
                    if (iMeafyen1 == 23 || iMeafyen1 == 22 || iMeafyen1 == 63 || iMeafyen1 == 66 || (iMeafyen1 >= 70 && iMeafyen1 <= 75) || iMeafyen1 ==86)
                    {
                        if (objOved.objPirteyOved.iGil == clGeneral.enKodGil.enKashish.GetHashCode() && (iSugYom == clGeneral.enSugYom.Chol.GetHashCode() || iSugYom == clGeneral.enSugYom.CholHamoedPesach.GetHashCode() || iSugYom == clGeneral.enSugYom.CholHamoedSukot.GetHashCode() || iSugYom == clGeneral.enSugYom.Purim.GetHashCode()))
                            fErechRechiv = 444;
                        else if (objOved.objPirteyOved.iGil == clGeneral.enKodGil.enKshishon.GetHashCode() && (iSugYom == clGeneral.enSugYom.Chol.GetHashCode() || iSugYom == clGeneral.enSugYom.CholHamoedPesach.GetHashCode() || iSugYom == clGeneral.enSugYom.CholHamoedSukot.GetHashCode() || iSugYom == clGeneral.enSugYom.Purim.GetHashCode()))
                            fErechRechiv = 462;
                        else if ((iSugYom >= 13 && iSugYom <= 18) || iSugYom == 11)
                            fErechRechiv = 300;
                        else if (objOved.objPirteyOved.iGil == clGeneral.enKodGil.enKashish.GetHashCode() && iSugYom == clGeneral.enSugYom.ErevYomKipur.GetHashCode())
                            fErechRechiv = 270;
                    }
                    else
                   // if (int.Parse(objOved.objMeafyeneyOved.sMeafyen1) != 22 && int.Parse(objOved.objMeafyeneyOved.sMeafyen1) != 23 && int.Parse(objOved.objMeafyeneyOved.sMeafyen1) != 71 && int.Parse(objOved.objMeafyeneyOved.sMeafyen1) != 72)
                    {
                        if (objOved.objPirteyOved.iGil == clGeneral.enKodGil.enKashish.GetHashCode())
                        {
                            if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
                            {
                                if (iSugYom < clGeneral.enSugYom.Shishi.GetHashCode())
                                    fErechRechiv = fErechRechiv - 72;
                                else if (iSugYom > clGeneral.enSugYom.Shishi.GetHashCode() && iSugYom < clGeneral.enSugYom.LagBaomerOrPurim.GetHashCode())
                                    fErechRechiv = fErechRechiv - 60;
                            }
                            else if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode() || objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek2.GetHashCode())
                            {
                                if (iSugYom < clGeneral.enSugYom.Shabat.GetHashCode())
                                    fErechRechiv = fErechRechiv - 60;
                            }
                        }
                        else if (objOved.objPirteyOved.iGil == clGeneral.enKodGil.enKshishon.GetHashCode())
                        {
                            if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
                            {
                                if (iSugYom < clGeneral.enSugYom.Shishi.GetHashCode())
                                    fErechRechiv = fErechRechiv - 36;
                                else if (iSugYom > clGeneral.enSugYom.Shishi.GetHashCode() && iSugYom < clGeneral.enSugYom.LagBaomerOrPurim.GetHashCode())
                                    fErechRechiv = fErechRechiv - 30;
                            }
                            else if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode() || objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek2.GetHashCode())
                            {
                                if (iSugYom < clGeneral.enSugYom.Shabat.GetHashCode())
                                    fErechRechiv = fErechRechiv - 30;
                            }
                        }
                    }

                    if (objOved.objMeafyeneyOved.sMeafyen47 == "1")
                    { fErechRechiv = fErechRechiv - 60; }

                    //rowSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur in(99707, 99706, 99708 , 99702, 99703, 99704,99701 ,99705)", "shat_hatchala_sidur desc");
                    rowSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and michsat_shishi_lebaley_x is not null", "shat_hatchala_sidur desc");
                   
                    if (rowSidurim.Length > 0 && iSugYom == clGeneral.enSugYom.Shishi.GetHashCode() && fErechRechiv == 0)
                    {
                        if (objOved.objPirteyOved.iGil == clGeneral.enKodGil.enKashish.GetHashCode())
                        { fErechRechiv = 330; }
                        else if (objOved.objPirteyOved.iGil == clGeneral.enKodGil.enKshishon.GetHashCode())
                        { fErechRechiv = 360; }
                        else if (objOved.objPirteyOved.iGil == clGeneral.enKodGil.enTzair.GetHashCode())
                        { fErechRechiv = 390; }
                    }

                    if (objOved.objPirteyOved.iIsuk == 122 || objOved.objPirteyOved.iIsuk == 123 || objOved.objPirteyOved.iIsuk == 124 || objOved.objPirteyOved.iIsuk == 127)
                    {
                        if (objOved.objPirteyOved.iKodMaamdRashi == clGeneral.enMaamad.Salarieds.GetHashCode())
                        {

                            iSugMishmeret = CheckSugMishmeret();
                            if (iSugYom == clGeneral.enSugYom.Chol.GetHashCode())
                            {
                                if (iSugMishmeret == clGeneral.enSugMishmeret.Boker.GetHashCode())
                                {
                                    fErechRechiv = objOved.objParameters.iDakotMafileyMachshevColBoker;
                                }
                                else if (iSugMishmeret == clGeneral.enSugMishmeret.Tzaharim.GetHashCode())
                                {
                                    fErechRechiv = objOved.objParameters.iDakotMafileyMachshevColTzaharim;
                                }
                                else if (iSugMishmeret == clGeneral.enSugMishmeret.Liyla.GetHashCode())
                                {
                                    fErechRechiv = objOved.objParameters.iDakotMafileyMachshevColLiyla;
                                }
                            }
                            else if (iSugYom > clGeneral.enSugYom.Shishi.GetHashCode() && iSugYom < clGeneral.enSugYom.LagBaomerOrPurim.GetHashCode())
                            {
                                if (iSugMishmeret == clGeneral.enSugMishmeret.Boker.GetHashCode())
                                {
                                    fErechRechiv = objOved.objParameters.iDakotMafileyMachshevErevChagBoker;
                                }
                                else if (iSugMishmeret == clGeneral.enSugMishmeret.Tzaharim.GetHashCode())
                                {
                                    fErechRechiv = objOved.objParameters.iDakotMafileyMachshevErevChagTzaharim;
                                }
                            }
                            else if (iSugYom == clGeneral.enSugYom.CholHamoedPesach.GetHashCode() || iSugYom == clGeneral.enSugYom.CholHamoedSukot.GetHashCode() || iSugYom == clGeneral.enSugYom.Purim.GetHashCode())
                            {
                                if (iSugMishmeret == clGeneral.enSugMishmeret.Boker.GetHashCode() || iSugMishmeret == clGeneral.enSugMishmeret.Tzaharim.GetHashCode())
                                {
                                    fErechRechiv = objOved.objParameters.iDakotMafileyMacshevChagBoker;
                                }
                                else if (iSugMishmeret == clGeneral.enSugMishmeret.Liyla.GetHashCode())
                                {
                                    fErechRechiv = objOved.objParameters.iDakotMafileyMacshevChagLiyla;
                                }
                            }
                            addRowToTable(clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), fErechRechiv, fErechRechiv);


                        }
                    }

                    if (objOved.Taarich < objOved.objParameters.dChodeshTakanonSoziali)
                    {
                        if ((objOved.objPirteyOved.iMutamBitachon == 4 || objOved.objPirteyOved.iMutamBitachon == 5 || objOved.objPirteyOved.iMutamBitachon == 6 || objOved.objPirteyOved.iMutamBitachon == 8 ||
                             objOved.objPirteyOved.iSibotMutamut == 4 || objOved.objPirteyOved.iSibotMutamut == 5 || objOved.objPirteyOved.iSibotMutamut == 6 || objOved.objPirteyOved.iSibotMutamut == 8))
                        {
                            //if (!oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, iSugYom))
                            //    flag = false;
                            //else
                            if (objOved.objPirteyOved.iZmanMutamut > 0 && oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, iSugYom))
                            {
                                fErechRechiv = Math.Min(fErechRechiv, objOved.objPirteyOved.iZmanMutamut);
                                //   flag = false;
                            }
                            //else flag = true;
                        }
                        else if ((objOved.objPirteyOved.iMutamut == clGeneral.enMutaam.enMutaam1.GetHashCode() || objOved.objPirteyOved.iMutamut == clGeneral.enMutaam.enMutaam4.GetHashCode() || objOved.objPirteyOved.iMutamut == clGeneral.enMutaam.enMutaam5.GetHashCode() || objOved.objPirteyOved.iMutamut == clGeneral.enMutaam.enMutaam6.GetHashCode()) && objOved.objPirteyOved.iZmanMutamut > 0)
                        {
                            fErechRechiv = Math.Min(fErechRechiv, objOved.objPirteyOved.iZmanMutamut);
                        }
                    }
                    if (objOved.objMeafyeneyOved.sMeafyen91.Length > 0)
                    {
                        if (int.Parse(objOved.objMeafyeneyOved.sMeafyen91) > 0)
                        {
                            fErechRechiv = Math.Min(fErechRechiv, int.Parse(objOved.objMeafyeneyOved.sMeafyen91));
                        }
                    }

                    if ((dTaarich.DayOfWeek.GetHashCode() + 1) == clGeneral.enDay.Shabat.GetHashCode())
                    { fErechRechiv = 0; }
                    else if ((dTaarich.DayOfWeek.GetHashCode() + 1) == clGeneral.enDay.Shishi.GetHashCode() && !CheckShishiMuchlaf(iSugYom, dTaarich) && (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode()))
                    { fErechRechiv = 0; }

                }
                else if (objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30 && iSugYom < clGeneral.enSugYom.Shishi.GetHashCode())
                {
                    if (fErechRechiv > 420)
                    {
                        int iMinuts = 0;
                        DateTime dShatHatchala;
                        DateTime dShatGmar = DateTime.Parse(objOved.Taarich.AddDays(1).ToShortDateString() + " 06:00");

                        dShatHatchala = DateTime.Parse(objOved.Taarich.ToShortDateString() + " 22:00");
                        rowSidurim = null;
                        rowSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and Shat_gmar_Letashlum>=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime')", "");
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
                        {
                            addRowToTable(clGeneral.enRechivim.ETMichsaMekoritBeforeHafchata.GetHashCode(), fErechRechiv);
                            fErechRechiv = 420;
                        }
                    }
                }

                addRowToTable(clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), fErechRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
            finally
            {
                rowSidurim = null;
            }
        }

        private void CalcRechiv128()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv128();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.ZmanGrirot.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.ZmanGrirot.GetHashCode(), fSumDakotRechiv);

                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode(), fSumDakotRechiv);

                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ZmanGrirot.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv129()
        {
            float fDakorRechiv, fMachlifMeshaleach, fMachlifRakaz, fMachlifSadran, fMachlifKupai, fMachlifPakach;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                fMachlifMeshaleach = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotMachlifMeshaleach);
                fMachlifRakaz = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotMachlifRakaz);
                fMachlifSadran = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotMachlifSadran);
                fMachlifKupai = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotMachlifKupai); 
                fMachlifPakach = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotMachlifPakach);

                fDakorRechiv = fMachlifMeshaleach + fMachlifRakaz + fMachlifSadran + fMachlifKupai + fMachlifPakach;

                addRowToTable(clGeneral.enRechivim.DakotMachlifTnua.GetHashCode(), fDakorRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMachlifTnua.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv131()
        {
            //שבת/שעות 100% (רכיב 131):
            float fErechRechiv, fTempY, fDakotNocheut,fShaotGoleshLeshabaton,fDakotShabat, fMichsaYomit, fShaot100, fShaot100ET, fDakotNocheutGmar, fZmanAdShabat = 0, fZmanAfterShabat=0;
            string sSidurim="";
            DataRow[] dr;
            try
            {
                fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);
                fDakotNocheut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);
                fShaot100 = 0; fShaot100ET = 0; fErechRechiv = 0; fDakotShabat = 0;

                oSidur.CalcRechiv131(ref fZmanAdShabat, ref fZmanAfterShabat);
                fErechRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.ShaotShabat100.GetHashCode(), objOved.Taarich);

                //ב
                if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30))
                {
                    fShaotGoleshLeshabaton = oSidur.GetSumShaotShabat100(ref fDakotShabat);
                    fTempY = oSidur.GetSumSidurim100(clGeneral.enRechivim.ShaotShabat100) + fShaotGoleshLeshabaton;
                    if (fMichsaYomit == 0)
                    {
                        fShaot100 = fTempY;
                    }
                    else
                    {
                        if (fTempY <= fMichsaYomit)
                        {
                            if (fDakotNocheut > 0 && fMichsaYomit > fDakotNocheut && (fShaotGoleshLeshabaton+fDakotShabat)>0)
                            {
                                fShaot100= Math.Min((fShaotGoleshLeshabaton+fDakotShabat), fMichsaYomit);;
                            }
                            else { fShaot100 = 0; }
                        }
                        else
                            fShaot100 = fTempY - fMichsaYomit;        
                    }
                }
                else{//ד
                    if (objOved.SugYom >= clGeneral.enSugYom.Shishi.GetHashCode() && objOved.SugYom < clGeneral.enSugYom.Shabat.GetHashCode())
                    {
                        fDakotNocheutGmar = 0;
                        
                        if (fMichsaYomit == 0 && fDakotNocheut > 0)
                        {
                            dr = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and Shat_gmar_Letashlum<=Convert('" + objOved.objParameters.dKnisatShabat.ToString() + "', 'System.DateTime')", "");
                            if (dr.Length > 0)
                            {
                                for (int i = 0; i < dr.Length; i++)
                                    sSidurim += dr[i]["mispar_sidur"].ToString() + ",";

                                sSidurim = sSidurim.Substring(0, sSidurim.Length - 1);

                                fDakotNocheutGmar = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime') AND MISPAR_SIDUR in (" + sSidurim + ")"));
                            }
                        
                            fShaot100ET = Math.Min(120, fDakotNocheut);
                           
                             fShaot100ET = Math.Min(fZmanAdShabat + fDakotNocheutGmar, fShaot100ET);
                         }
                        else if (fMichsaYomit > 0 && fDakotNocheut > 0 && fMichsaYomit > fDakotNocheut)
                        {
                            dr = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and Shat_hatchala_Letashlum<Convert('" + objOved.objParameters.dKnisatShabat.ToString() + "', 'System.DateTime') and Shat_gmar_Letashlum>Convert('" + objOved.objParameters.dKnisatShabat.ToString() + "', 'System.DateTime')", "");
                            if (dr.Length > 0)
                            {
                                for (int i = 0; i < dr.Length; i++)
                                {
                                    fDakotNocheutGmar += float.Parse((DateTime.Parse(dr[i]["Shat_gmar_Letashlum"].ToString()) - objOved.objParameters.dKnisatShabat).TotalMinutes.ToString());
                                }
                                
                                fShaot100ET = fDakotNocheutGmar;
                            }
                        }
                    }
                }
                //א
               // fZmanNesiot = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanNesia.GetHashCode(), objOved.Taarich);
                 
                 //ה
                if (objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30)
                {
                    fErechRechiv = fShaot100ET + fZmanAfterShabat;
                }
                else { fErechRechiv = fErechRechiv + fShaot100; }

                dr = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null and Hamarat_shabat=1");
                if (dr.Length > 0)
                    fErechRechiv = fErechRechiv + oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode(), objOved.Taarich);

                addRowToTable(clGeneral.enRechivim.ShaotShabat100.GetHashCode(), fErechRechiv);
             
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ShaotShabat100.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv132()
        {
            float fSumDakotRechiv, fZmanHamaratDakotShabat, fDakotZikuyChofesh;
            try
            {
                oSidur.CalcRechiv132();
                fZmanHamaratDakotShabat = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode(), objOved.Taarich);
                fDakotZikuyChofesh = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode(), objOved.Taarich);

                fSumDakotRechiv = fZmanHamaratDakotShabat + fDakotZikuyChofesh;
                addRowToTable(clGeneral.enRechivim.ChofeshZchut.GetHashCode(), fSumDakotRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ChofeshZchut.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv133()
        {
            try
            {
                if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30))
                {
                    oSidur.CalcRechiv133();
                    UpdateRechiv133();
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.PremyaRegila.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void UpdateRechiv133()
        {
            float fSumDakotRechiv, fPremiaYomit, fPremiaShabat, fPremiaBeshishi;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                fPremiaShabat = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotPremiaShabat); 
                fPremiaYomit = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotPremiaYomit);
                fPremiaBeshishi = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotPremiaBeShishi); 

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
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.PremyaNamlak.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void UpdateRechiv134()
        {
            float fSumDakotRechiv, fPremiaVisa, fPremiaVisaShabat, fPremiaVisaBeShishi;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                fPremiaVisa = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotPremiaVisa);
                fPremiaVisaShabat = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotPremiaVisaShabat);
                fPremiaVisaBeShishi = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotPremiaVisaShishi); 

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
            try
            {
                CalcKizuzRechiv(clGeneral.enRechivim.KizuzKaytanaTafkid.GetHashCode());
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzKaytanaTafkid.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv136()
        {
            try
            {
                CalcKizuzRechiv(clGeneral.enRechivim.KizuzKaytanaShivuk.GetHashCode());
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzKaytanaShivuk.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv137()
        {
            try
            {
                CalcKizuzRechiv(clGeneral.enRechivim.KizuzKaytanaBniaPeruk.GetHashCode());
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzKaytanaBniaPeruk.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv138()
        {
            try
            {
                CalcKizuzRechiv(clGeneral.enRechivim.KizuzKaytanaYeshivatTzevet.GetHashCode());
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzKaytanaYeshivatTzevet.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv139()
        {
            try
            {
                CalcKizuzRechiv(clGeneral.enRechivim.KizuzKaytanaYamimArukim.GetHashCode());
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzKaytanaYamimArukim.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv140()
        {
            try
            {
                CalcKizuzRechiv(clGeneral.enRechivim.KizuzKaytanaMeavteach.GetHashCode());
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzKaytanaMeavteach.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv141()
        {
            try
            {
                CalcKizuzRechiv(clGeneral.enRechivim.KizuzKaytanaMatzil.GetHashCode());
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzKaytanaMatzil.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv142()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv142();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.KizuzNochehut.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.KizuzNochehut.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzNochehut.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv143()
        {
            float fMichsaYomit, fDakotNochehut, fDakotNochehutBefoal, fHadracha,  fErech;
            int iSugYom;
            Boolean bPutar = false;
            Boolean bLechishuv = false;
            DataRow[] drs;
            try
            {
                fErech = 0;
                if ((objOved.objMeafyeneyOved.iMeafyen14 <= 0) && objOved.objMeafyeneyOved.iMeafyen12 > 0)
                {
                    bPutar = oCalcBL.CheckOvedPutar(objOved);
                    fDakotNochehut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);
                    fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);

                    iSugYom = clGeneral.GetSugYom(objOved.oGeneralData.dtYamimMeyuchadim, objOved.Taarich, objOved.oGeneralData.dtSugeyYamimMeyuchadim);//, objOved.objMeafyeneyOved.iMeafyen56);
                    if ((iSugYom == clGeneral.enSugYom.CholHamoedPesach.GetHashCode() || iSugYom == clGeneral.enSugYom.CholHamoedSukot.GetHashCode() ||
                            iSugYom == clGeneral.enSugYom.ErevRoshHashna.GetHashCode() || iSugYom == clGeneral.enSugYom.ErevYomKipur.GetHashCode() ||
                            iSugYom == clGeneral.enSugYom.ErevSukot.GetHashCode() || iSugYom == clGeneral.enSugYom.ErevSimchatTora.GetHashCode() ||
                            iSugYom == clGeneral.enSugYom.ErevPesach.GetHashCode() || iSugYom == clGeneral.enSugYom.ErevPesachSheni.GetHashCode() ||
                            iSugYom == clGeneral.enSugYom.ErevShavuot.GetHashCode())
                        && objOved.objMeafyeneyOved.iMeafyen85 == 1 && fDakotNochehut == 0 && fMichsaYomit > 0 && bPutar)
                    {
                        bLechishuv = true;
                    }
                    if (iSugYom == clGeneral.enSugYom.ErevYomHatsmaut.GetHashCode() && objOved.objMeafyeneyOved.sMeafyen63.Length > 0)
                    {
                        bLechishuv = true;
                    }
                    if (objOved.Taarich.Day >= 24 && fMichsaYomit > 0)
                    {
                        drs = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV IN (60,61,70,71,69,65,64,56) and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')");
                        if (drs.Length>0)
                            bLechishuv = true;
                    }

                    if (bLechishuv == false)
                    {
                        fHadracha = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomHadracha.GetHashCode(), objOved.Taarich);
                        fDakotNochehutBefoal = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode(), objOved.Taarich);
                        if ((fMichsaYomit > 0 && fDakotNochehutBefoal >= 240) || (fHadracha == 1 && fDakotNochehut >= 240))
                        {
                            bLechishuv = true;
                        }
                    }

                    if (bLechishuv)
                    {
                        fErech = 1;
                        addRowToTable(clGeneral.enRechivim.MichsatShaotNosafotTafkidChol.GetHashCode(), fErech);                 
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.MichsatShaotNosafotTafkidChol.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: " + ex.Message);
                throw (ex);
            }
        }
        private void CalcRechiv145()
        {
            try
            {
                CalcKizuzRechiv(clGeneral.enRechivim.KizuzVaadOvdim.GetHashCode());
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzVaadOvdim.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv146()
        {
            float fMichsaYomit, fDakotNocheut, fSumDakotRechiv,fShaot25,fShaot50;
            try
            {
                if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
                {
                    fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);
                    fDakotNocheut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);
                    if ((fMichsaYomit > 0 && fDakotNocheut > fMichsaYomit))
                    {
                       // fSumDakotRechiv = (fDakotNocheut - fMichsaYomit) / 60;
                        fShaot25 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Shaot25.GetHashCode(), objOved.Taarich);
                        fShaot50 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Shaot50.GetHashCode(), objOved.Taarich);
                        fSumDakotRechiv = (fShaot25 + fShaot50) / 60;
                        addRowToTable(clGeneral.enRechivim.Nosafot100.GetHashCode(), fSumDakotRechiv);
                    }
                   
                    //if (objOved.objMeafyeneyOved.iMeafyen92 > 0 && oCalcBL.CheckYomShishi(objOved.SugYom))
                    //{
                    //    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Shaot25.GetHashCode(), objOved.Taarich);
                    //    fSumDakotRechiv += oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Shaot50.GetHashCode(), objOved.Taarich);
                    //    addRowToTable(clGeneral.enRechivim.Nosafot100.GetHashCode(), (fSumDakotRechiv/60));
                  
                    //}
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.Nosafot100.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }
        private void SetSidureyVaadOvdim()
        {
            float fTempX, fTempY, fShaot150, fShaot50, fTempZ, fTempW, fTempT, fShaot125, fShaot25;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                fTempX = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=99008 AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fShaot125 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.Nosafot125);
                fShaot25 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.Shaot25); 
                fShaot150 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.Nosafot150);
                fShaot50 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.Shaot50); 

                fTempY = fShaot25 + fShaot125 + fShaot50 + fShaot150;

                if (fTempX > 0 && fTempY > 0)
                {
                    if (fTempX < fShaot50 + fShaot150)
                    {
                        fTempZ = fShaot50 + fShaot150;
                        if (fShaot150 > 0)
                            addRowToTable(clGeneral.enRechivim.Nosafot150.GetHashCode(), fShaot150 - fTempX + (fTempX / float.Parse("1.5")));

                        if (fShaot50 > 0)
                            addRowToTable(clGeneral.enRechivim.Shaot50.GetHashCode(), fShaot50 - fTempX + (fTempX / float.Parse("1.5")));

                        fShaot150 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Nosafot150.GetHashCode(), objOved.Taarich);
                        fShaot50 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Shaot50.GetHashCode(), objOved.Taarich);

                        fTempW = fTempZ - (fShaot50 + fShaot150);
                    }
                    else
                    {
                        fTempZ = fShaot50 + fShaot150;
                        fTempT = fTempX - fTempZ;
                        if (fShaot150 > 0)
                            addRowToTable(clGeneral.enRechivim.Nosafot150.GetHashCode(), (fShaot150 / float.Parse("1.5")));

                        if (fShaot50 > 0)
                            addRowToTable(clGeneral.enRechivim.Shaot50.GetHashCode(), (fShaot50 / float.Parse("1.5")));

                        fShaot150 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Nosafot150.GetHashCode(), objOved.Taarich);
                        fShaot50 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Shaot50.GetHashCode(), objOved.Taarich);

                        fTempW = fTempZ - (fShaot50 + fShaot150);

                        if (fTempT > (fShaot25 + fShaot125))
                        {
                            fTempZ = (fShaot25 + fShaot125);
                            if (fShaot125 > 0)
                                addRowToTable(clGeneral.enRechivim.Nosafot125.GetHashCode(), (fShaot125 / float.Parse("1.25")));

                            if (fShaot25 > 0)
                                addRowToTable(clGeneral.enRechivim.Shaot25.GetHashCode(), (fShaot25 / float.Parse("1.25")));

                            fShaot125 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Nosafot125.GetHashCode(), objOved.Taarich);
                            fShaot25 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Shaot25.GetHashCode(), objOved.Taarich);

                            fTempW = fTempW + fTempZ - (fShaot125 + fShaot25);
                        }
                        else
                        {
                            fTempZ = fShaot125 + fShaot25;
                            if (fShaot125 > 0)
                                addRowToTable(clGeneral.enRechivim.Nosafot125.GetHashCode(), fShaot125 - fTempT + (fTempT / float.Parse("1.25")));

                            if (fShaot25 > 0)
                                addRowToTable(clGeneral.enRechivim.Shaot25.GetHashCode(), fShaot25 - fTempT + (fTempT / float.Parse("1.25")));

                            fShaot125 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Nosafot125.GetHashCode(), objOved.Taarich);
                            fShaot25 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Shaot25.GetHashCode(), objOved.Taarich);

                            fTempW = fTempW + fTempZ - (fShaot125 + fShaot25);
                        }
                    }

                    if (fTempW > 0)
                    {
                         ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                         fShaot125 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.Nosafot125);
                         fShaot25 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.Shaot25); 
                         fShaot150 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.Nosafot150); 
                         fShaot50 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.Shaot50); 

                        if ((fShaot25 + fShaot125 + fShaot50 + fShaot150) > 0)
                        {
                            fTempX = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);

                            addRowToTable(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), fTempX - fTempW);

                            fTempX = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), objOved.Taarich);

                            addRowToTable(clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), fTempX - fTempW);


                            fTempX = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNosafotTafkid.GetHashCode(), objOved.Taarich);
                            if (fTempX > 0)
                            {
                                addRowToTable(clGeneral.enRechivim.DakotNosafotTafkid.GetHashCode(), fTempX - fTempW);

                                fTempY = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachNosafotTafkidCholVeshishi.GetHashCode(), objOved.Taarich);

                                addRowToTable(clGeneral.enRechivim.SachNosafotTafkidCholVeshishi.GetHashCode(), fTempY - fTempW);

                                fTempY = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotTafkid.GetHashCode(), objOved.Taarich);

                                addRowToTable(clGeneral.enRechivim.SachDakotTafkid.GetHashCode(), fTempY - fTempW);

                                fTempY = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.NochehutChol.GetHashCode(), objOved.Taarich);

                                addRowToTable(clGeneral.enRechivim.NochehutChol.GetHashCode(), fTempY - fTempW);
                            }

                            fTempY = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode(), objOved.Taarich);

                            if (fTempX == fTempY)
                                addRowToTable(clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode(), fTempX - fTempW);

                        }

                    }

                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", 0, objOved.Taarich, "CalcDay: SetSidureyVaadOvdim" + ex.StackTrace + "\n message: "+ ex.Message);
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
                    objOved.DtYemeyAvodaYomi.Select(null, "Lo_letashlum");
                    _drSidurMeyuchad = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=1 and MISPAR_SIDUR IN(" + sSidurim + ")");
                    fErechRechiv = 0;
                    for (int I = 0; I < _drSidurMeyuchad.Length; I++)
                    {
                        dShatHatchalaSidur = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_sidur"].ToString());
                        iMisparSidur = int.Parse(_drSidurMeyuchad[I]["mispar_sidur"].ToString());
                        fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString()));

                       // oPeilut.dTaarich = objOved.Taarich;
                        fErechRechiv = fErechRechiv + oSidur.CalcRechiv1BySidur(_drSidurMeyuchad[I], fMichsaYomit, oPeilut);
                    }
                    addRowToTable(iKodRechiv, fErechRechiv);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _drSidurMeyuchad = null;
            }
        }


        private void CalcRechiv149()
        {
            float fDakotNochehut, fMichsaYomit, fDakotPremia, fDakotVisa, fDakotRechiv;
            try
            {
                fDakotRechiv = 0;
                if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30))
                {
                    if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
                    {
                        Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);


                        fDakotNochehut = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNochehutLetashlum);
                        fMichsaYomit = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichsaYomitMechushevet);
                        fDakotPremia = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotPremiaYomit);
                        fDakotVisa = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotPremiaVisa);
                        if (fMichsaYomit > fDakotNochehut)
                        {
                            if (((fMichsaYomit - fDakotNochehut) <= objOved.objParameters.iMaxHashlamaAlCheshbonPremia) && fDakotPremia > 0)
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
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.HashlamaAlCheshbonPremia.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        //private void CalcRechiv164()
        //{
        //    float fSumDakotRechiv, fTempX,fDakotPakach,fDakotMachlifPakach;
        //    try
        //    {
        //        oSidur.CalcRechiv164();
        //        fTempX = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotMichutzLemichsaPakach.GetHashCode(), objOved.Taarich);
        //        fDakotPakach = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNosafotPakach.GetHashCode(), objOved.Taarich);
        //        fDakotMachlifPakach = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNosafotMachlifPakach.GetHashCode(), objOved.Taarich);

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
        //        clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMichutzLemichsaPakach.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
        //        throw (ex);
        //    }
        //}


        //private void CalcRechiv165()
        //{
        //    float fSumDakotRechiv, fTempX, fDakotSadran, fDakotMachlifSadran;
        //    try
        //    {
        //        oSidur.CalcRechiv165();
        //        fTempX = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotMichutzLemichsaSadsran.GetHashCode(), objOved.Taarich);
        //        fDakotSadran = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNosafotSadran.GetHashCode(), objOved.Taarich);
        //        fDakotMachlifSadran= oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNosafotMachlifSadran.GetHashCode(), objOved.Taarich);

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
        //        clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMichutzLemichsaSadsran.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
        //        throw (ex);
        //    }
        //}

        //private void CalcRechiv166()
        //{
        //    float fSumDakotRechiv, fTempX, fDakotMachlifRakaz, fDakotRakaz;
        //    try
        //    {
        //        oSidur.CalcRechiv166();
        //        fTempX = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotMichutzLemichsaRakaz.GetHashCode(), objOved.Taarich);
        //        fDakotRakaz = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNosafotRakaz.GetHashCode(), objOved.Taarich);
        //        fDakotMachlifRakaz = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNosafotMachlifRakaz.GetHashCode(), objOved.Taarich);

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
        //        clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMichutzLemichsaRakaz.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
        //        throw (ex);
        //    }
        //}

        //private void CalcRechiv167()
        //{
        //    float fSumDakotRechiv, fTempX, fDakotMachlifMeshaleach, fDakotMeshaleach;
        //    try
        //    {
        //        oSidur.CalcRechiv167();
        //        fTempX = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotMichutzLemichsaMeshaleach.GetHashCode(), objOved.Taarich);
        //        fDakotMeshaleach = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNosafotMeshaleach.GetHashCode(), objOved.Taarich);
        //        fDakotMachlifMeshaleach = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNosafotMachlifMeshaleach.GetHashCode(), objOved.Taarich);

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
        //        clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMichutzLemichsaMeshaleach.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
        //        throw (ex);
        //    }
        //}

        private void CalcRechiv174()
        {
            try
            {
                CalcKizuzRechiv(clGeneral.enRechivim.KizuzMachlifPakach.GetHashCode());
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzMachlifPakach.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv175()
        {
            try
            {
                CalcKizuzRechiv(clGeneral.enRechivim.KizuzMachlifSadran.GetHashCode());
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzMachlifSadran.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv176()
        {
            try
            {
                CalcKizuzRechiv(clGeneral.enRechivim.KizuzMachlifRakaz.GetHashCode());
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzMachlifRakaz.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv177()
        {
            try
            {
                CalcKizuzRechiv(clGeneral.enRechivim.KizuzMachlifMeshaleach.GetHashCode());
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzMachlifMeshaleach.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv178()
        {
            try
            {
                CalcKizuzRechiv(clGeneral.enRechivim.KizuzMachlifKupai.GetHashCode());
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzMachlifKupai.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv179()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv179();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.SachDakotPakach.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.SachDakotPakach.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachDakotPakach.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv180()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv180();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.SachDakotSadran.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.SachDakotSadran.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachDakotSadran.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv181()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv181();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.SachDakotRakaz.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.SachDakotRakaz.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachDakotRakaz.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv182()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv182();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.SachDakotMeshalech.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.SachDakotMeshalech.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachDakotMeshalech.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv183()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv183();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.SachDakotKupai.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.SachDakotKupai.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachDakotKupai.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv184()
        {
            float fSumDakotRechiv, fTempX, fDakotNosafotNihulTnua;
            try
            {
                oSidur.CalcRechiv184();
                fTempX = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotMichutzLamichsaNihulTnua.GetHashCode(), objOved.Taarich);
                fDakotNosafotNihulTnua = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNosafotNihul.GetHashCode(), objOved.Taarich);

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
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotMichutzLamichsaNihulTnua.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv185()
        {
            try
            {
                CalcKizuzRechiv(clGeneral.enRechivim.KizuzMishpatChaverim.GetHashCode());
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.KizuzMishpatChaverim.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv186()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv186();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.MichutzLamichsaTafkidShabat.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.MichutzLamichsaTafkidShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.MichutzLamichsaTafkidShabat.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv187()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv187();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.MichutzLamichsaNihulShabat.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.MichutzLamichsaNihulShabat.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.MichutzLamichsaNihulShabat.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv188()
        {
            float fSumDakotRechiv, fDakotNehigaChol, fDakotNehigaShabat, fDakotNehigaShishi;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                fDakotNehigaChol = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNehigaChol);
                fDakotNehigaShabat = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNehigaShabat);
                fDakotNehigaShishi = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachDakotNehigaShishi);

                fSumDakotRechiv = fDakotNehigaChol + fDakotNehigaShabat + fDakotNehigaShishi;
                addRowToTable(clGeneral.enRechivim.SachDakotNahagut.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachDakotNahagut.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv189()
        {
            float fSumDakotRechiv, fZmanHashlama, fHashlamaBenahagut, fRetzifutTafkid;
            try
            {
                oSidur.CalcRechiv189();
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode(), objOved.Taarich);
                if (fSumDakotRechiv > 0)
                {
                    fZmanHashlama = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanHashlama); 
                    fRetzifutTafkid = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanRetzifutTafkid); 
                    fHashlamaBenahagut = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.HashlamaBenahagut);

                    fSumDakotRechiv = fSumDakotRechiv + fZmanHashlama + fHashlamaBenahagut + fRetzifutTafkid;
                    addRowToTable(clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv190()
        {
            float fSumDakotRechiv, fDakotNihulChol, fDakotNihulShabat, fDakotNihulShishi;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                fDakotNihulChol = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNihulTnuaChol); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode(), objOved.Taarich);
                fDakotNihulShabat = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNihulTnuaShabat); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode(), objOved.Taarich);
                fDakotNihulShishi = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachDakotNihulShishi); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode(), objOved.Taarich);

                fSumDakotRechiv = fDakotNihulChol + fDakotNihulShabat + fDakotNihulShishi;
                addRowToTable(clGeneral.enRechivim.SachDakotNihulTnua.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachDakotNihulTnua.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv191()
        {
            float fSumDakotRechiv, fZmanRetzifut, fZmanHashlama, fHashlamaNihul, fDakotNehigaShishi;
            try
            {
                oSidur.CalcRechiv191();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode(), objOved.Taarich);
                if (fSumDakotRechiv > 0)
                {
                    Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                    fZmanRetzifut = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanRetzifutTafkid); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotLepremia.GetHashCode(), objOved.Taarich);
                    fZmanHashlama = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanHashlama); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanHashlama.GetHashCode(), objOved.Taarich);
                    fHashlamaNihul = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.HashlamaBenihulTnua); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.HashlamaBenihulTnua.GetHashCode(), objOved.Taarich);
                    fDakotNehigaShishi = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachDakotNehigaShishi); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode(), objOved.Taarich);

                    if (fDakotNehigaShishi == 0)
                    { fSumDakotRechiv = fSumDakotRechiv + fZmanHashlama + fZmanRetzifut; }

                   fSumDakotRechiv += fHashlamaNihul;
                    addRowToTable(clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv192()
        {
            float fSumDakotRechiv, fDakotTafkidChol, fDakotTafkidShabat, fDakotTafkidShishi;
            try
            {
                oSidur.CalcRechiv192();
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);
                fDakotTafkidChol = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotTafkidChol); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), objOved.Taarich);
                fDakotTafkidShabat = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotTafkidShabat); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotTafkidShabat.GetHashCode(), objOved.Taarich);
                fDakotTafkidShishi = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachDakotTafkidShishi); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(), objOved.Taarich);

                fSumDakotRechiv = fDakotTafkidChol + fDakotTafkidShabat + fDakotTafkidShishi;

                addRowToTable(clGeneral.enRechivim.SachDakotTafkid.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachDakotTafkid.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv193()
        {
            float fSumDakotRechiv, fDakotNehigaShishi, fZmanHashlama, fDakotNihulShishi, fKizuzAruchatTzaharim, fDakotNochechutSidurey100;
            try
            {
                oSidur.CalcRechiv193();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(), objOved.Taarich);
                if (fSumDakotRechiv > 0)
                {
                    Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                    fKizuzAruchatTzaharim = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanAruchatTzaraim); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode(), objOved.Taarich);
                    fZmanHashlama = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanHashlama); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanHashlama.GetHashCode(), objOved.Taarich);
                    fDakotNehigaShishi = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachDakotNehigaShishi); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode(), objOved.Taarich);
                    fDakotNihulShishi = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachDakotNihulShishi); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode(), objOved.Taarich);

                    if (fDakotNehigaShishi == 0 && fDakotNihulShishi == 0)
                    { fSumDakotRechiv = fSumDakotRechiv + fZmanHashlama; }

                    if (fSumDakotRechiv > 0)
                    {
                        fSumDakotRechiv = fSumDakotRechiv - fKizuzAruchatTzaharim;
                        fDakotNochechutSidurey100 = oSidur.GetSumSidurim100(clGeneral.enRechivim.SachDakotTafkidShishi);
                        fSumDakotRechiv = fSumDakotRechiv - fDakotNochechutSidurey100;
                    }
                    addRowToTable(clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv194()
        {
            float fSumDakotRechiv, fNihulTnua, fNehigaChol, fTafkidChol;
            try
            {
                oSidur.CalcRechiv194();
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);
                fNehigaChol = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNehigaChol); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNehigaChol.GetHashCode(), objOved.Taarich);
                fNihulTnua = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNihulTnuaChol); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode(), objOved.Taarich);
                fTafkidChol = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotTafkidChol); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), objOved.Taarich);

                fSumDakotRechiv = fNehigaChol + fNihulTnua + fTafkidChol;
                addRowToTable(clGeneral.enRechivim.NochehutChol.GetHashCode(), fSumDakotRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutChol.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv195()
        {
            float fSumDakotRechiv, fNihulShishi, fNehigaShishi, fTafkidShishi;
            try
            {
                oSidur.CalcRechiv195();
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);
                fNehigaShishi = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachDakotNehigaShishi); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode(), objOved.Taarich);
                fNihulShishi = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachDakotNihulShishi); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode(), objOved.Taarich);
                fTafkidShishi = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachDakotTafkidShishi); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(), objOved.Taarich);

                fSumDakotRechiv = fNehigaShishi + fNihulShishi + fTafkidShishi;
                addRowToTable(clGeneral.enRechivim.NochehutBeshishi.GetHashCode(), fSumDakotRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutBeshishi.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv196()
        {
            float fSumDakotRechiv, fNihulShabat, fNehigaShabat, fTafkidShabat;
            try
            {
                oSidur.CalcRechiv196();
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);
                fNehigaShabat = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNehigaShabat); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNehigaShabat.GetHashCode(), objOved.Taarich);
                fNihulShabat = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotNihulTnuaShabat); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode(), objOved.Taarich);
                fTafkidShabat = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotTafkidShabat); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotTafkidShabat.GetHashCode(), objOved.Taarich);

                fSumDakotRechiv = fNehigaShabat + fNihulShabat + fTafkidShabat;
                addRowToTable(clGeneral.enRechivim.NochehutShabat.GetHashCode(), fSumDakotRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutShabat.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv198()
        {
            float fSumDakotRechiv, fShaot25, fShaot50, fNosafot125, fNosafot150;
            int iSugYomLemichsa;
            try
            {
                iSugYomLemichsa = oCalcBL.GetSugYomLemichsa(objOved, objOved.Taarich, objOved.objPirteyOved.iKodSectorIsuk, objOved.objMeafyeneyOved.iMeafyen56);
                if (iSugYomLemichsa >= clGeneral.enSugYom.Shishi.GetHashCode() && iSugYomLemichsa.ToString().Substring(0, 1) == "1")
                {
                    Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);
                    fNosafot125 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.Nosafot125); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Nosafot125.GetHashCode(), objOved.Taarich);
                    fNosafot150 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.Nosafot150); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Nosafot150.GetHashCode(), objOved.Taarich);
                    fShaot25 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.Shaot25); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Shaot25.GetHashCode(), objOved.Taarich);
                    fShaot50 = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.Shaot50); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Shaot50.GetHashCode(), objOved.Taarich);

                    fSumDakotRechiv = fShaot25 + fShaot50 + fNosafot125 + fNosafot150;
                    addRowToTable(clGeneral.enRechivim.NosafotShishi.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NosafotShishi.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv200()
        {
            float fSumDakotRechiv, fTafkidChol, fTnuaChol;
            try
            {

                fTafkidChol = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode(), objOved.Taarich);
                fTnuaChol = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotMichutzLamichsaNihulTnua.GetHashCode(), objOved.Taarich);

                fSumDakotRechiv = fTafkidChol + fTnuaChol;
                addRowToTable(clGeneral.enRechivim.MichutzLamichsaChol.GetHashCode(), fSumDakotRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.MichutzLamichsaChol.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv201()
        {
            float fSumDakotRechiv, fTafkidChol, fTnuaChol;
            try
            {

                fTafkidChol = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichutzLamichsaTafkidShishi.GetHashCode(), objOved.Taarich);
                fTnuaChol = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichutzLamichsaTnuaShishi.GetHashCode(), objOved.Taarich);

                fSumDakotRechiv = fTafkidChol + fTnuaChol;
                addRowToTable(clGeneral.enRechivim.MichutzLamichsaShishi.GetHashCode(), fSumDakotRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.MichutzLamichsaShishi.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv202_2()
        {
            float fSumDakotRechiv, fMichsaYomit, fNuchehutLepremia, fTosefetRetzifut, fSumDakotSikun;
            float fDakotHagdara, fDakotHistaglut, fSachNesiot, fDakotLepremia, fTosefetGil, fDakotKisuyTor;

            try
            {
                objOved.DtYemeyAvodaYomi.Select(null, "TACHOGRAF");
                if (objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0  and TACHOGRAF=1", "shat_hatchala_sidur ASC").Length == 0)
                {
                    Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                    fMichsaYomit = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichsaYomitMechushevet); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);

                    if (fMichsaYomit == 0 && (oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom) || oCalcBL.CheckYomShishi(objOved.SugYom)))
                    {
                        if (objOved.objPirteyOved.iMutamut != 1 && objOved.objPirteyOved.iMutamut != 3)
                        {
                            if (objOved.objPirteyOved.iDirug != 85 || objOved.objPirteyOved.iDarga != 30)
                            {
                                oSidur.CalcRechiv202();

                                fNuchehutLepremia = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(), objOved.Taarich);
                                if (fNuchehutLepremia > 0)
                                {
                                    fTosefetRetzifut = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.ZmanRetzifutNehiga); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode(), objOved.Taarich);
                                    fDakotLepremia = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.SachDakotLepremia); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotLepremia.GetHashCode(), objOved.Taarich);
                                    fNuchehutLepremia += fTosefetRetzifut;

                                    fTosefetGil = oCalcBL.ChishuvTosefetGil(objOved, fMichsaYomit, fNuchehutLepremia, dShatHatchalaYomAvoda);

                                    fDakotHagdara = oCalcBL.GetSumErechRechivChelki(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotHagdara.GetHashCode(), clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(), objOved.Taarich) / float.Parse("0.75");
                                    fSumDakotSikun = oCalcBL.GetSumErechRechivChelki(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotSikun.GetHashCode(), clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(), objOved.Taarich) / float.Parse("0.75");
                                    fDakotHistaglut = oCalcBL.GetSumErechRechivChelki(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotHistaglut.GetHashCode(), clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(), objOved.Taarich);
                                    fDakotKisuyTor = oCalcBL.GetSumErechRechivChelki(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotKisuiTor.GetHashCode(), clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(), objOved.Taarich);
                                    fDakotLepremia = oCalcBL.GetSumErechRechivChelki(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.SachDakotLepremia.GetHashCode(), clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(), objOved.Taarich);
                                    fSachNesiot = oCalcBL.GetSumErechRechivChelki(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.SachNesiot.GetHashCode(), clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(), objOved.Taarich);

                                    fSumDakotRechiv = float.Parse(Math.Round(fDakotHagdara + fSumDakotSikun + fDakotHistaglut + fDakotKisuyTor + fDakotLepremia + fTosefetRetzifut +
                                                                            ((2 + (fSachNesiot - 1)) * objOved.objParameters.fElementZar) + (fTosefetGil - fNuchehutLepremia), MidpointRounding.AwayFromZero).ToString());
                            
                                    //fSumDakotRechiv = float.Parse(Math.Round((fDakotHagdara / float.Parse("0.75")) + fDakotHistaglut + fDakotKisuyTor + fDakotLepremia + fTosefetRetzifut + fSumDakotSikun + (fSachNesiot * objOved.objParameters.fElementZar) + (fTosefetGil - fNuchehutLepremia), MidpointRounding.AwayFromZero).ToString());

                                    addRowToTable(clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(), fSumDakotRechiv);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv202()
        {
            float fMichsaYomit, fErechRechiv, fErechSidur, fErechSidurOkev, fErechSidurPrev, SumEzer;
            DataRow[] drYom;
            DataRow drSidur, drSidurOkev, drSidurPrev;
            DateTime dShatHtchalaSidurOkev,dShatHtchalaSidur, dShatGmarSidur, dShatGmarPrev;
            int i = 0;
            dShatHtchalaSidurOkev = new DateTime();
            dShatGmarPrev = new DateTime();
            try
            {
                if (objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0  and TACHOGRAF=1", "").Length == 0)
                {
                    fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);

                    if (fMichsaYomit == 0 && oCalcBL.CheckYomShishi(objOved.SugYom))
                    {
                        if (objOved.objPirteyOved.iMutamut != 1 && objOved.objPirteyOved.iMutamut != 3)
                        {
                            if (objOved.objPirteyOved.iDirug != 85 || objOved.objPirteyOved.iDarga != 30)
                            {
                                oSidur.CalcRechiv202();
                                fErechRechiv = 0;
                                SumEzer = 0;
                                drYom = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null", "shat_hatchala_sidur ASC");

                                if (drYom.Length > 0)
                                {
                                    while (i < drYom.Length)
                                    {
                                        drSidur = drYom[i];
                                        dShatGmarSidur = DateTime.Parse(drSidur["shat_gmar_letashlum"].ToString());
                                        fErechSidur = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + drSidur["mispar_sidur"].ToString() + " AND SHAT_HATCHALA=Convert('" + drSidur["shat_hatchala_sidur"].ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                                        if ((i + 1) <= (drYom.Length - 1))
                                        {
                                            drSidurOkev = drYom[i + 1];
                                            dShatHtchalaSidurOkev = DateTime.Parse(drSidurOkev["shat_hatchala_letashlum"].ToString());
                                            fErechSidurOkev = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + drSidurOkev["mispar_sidur"].ToString() + " AND SHAT_HATCHALA=Convert('" + drSidurOkev["shat_hatchala_sidur"].ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                        }
                                        else fErechSidurOkev = 0;

                                        if (fErechSidur != 0 && fErechSidurOkev != 0)
                                        {
                                            if (float.Parse((dShatHtchalaSidurOkev - dShatGmarSidur).TotalMinutes.ToString()) <= 60)
                                            {
                                                SumEzer += fErechSidur;
                                                i++;
                                            }
                                            else
                                            {
                                                SumEzer += fErechSidur;
                                                if (SumEzer>0)
                                                    fErechRechiv += float.Parse(Math.Round(SumEzer, MidpointRounding.AwayFromZero).ToString()); 
                                                SumEzer = 0;
                                                i++;
                                            }
                                        }
                                        else if (fErechSidur != 0 && fErechSidurOkev == 0)
                                        {
                                            if (i > 0)
                                            {
                                                drSidurPrev = drYom[i - 1];
                                                dShatGmarPrev = DateTime.Parse(drSidurPrev["shat_gmar_letashlum"].ToString());
                                                fErechSidurPrev = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + drSidurPrev["mispar_sidur"].ToString() + " AND SHAT_HATCHALA=Convert('" + drSidurPrev["shat_hatchala_sidur"].ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                            }
                                            else fErechSidurPrev = 0;
                                            
                                            dShatHtchalaSidur = DateTime.Parse(drSidur["shat_hatchala_letashlum"].ToString());
                                               
                                            if (fErechSidurPrev != 0 && float.Parse((dShatHtchalaSidur - dShatGmarPrev).TotalMinutes.ToString()) <= 60)
                                            {
                                                SumEzer += fErechSidur;
                                                if (SumEzer > 0)
                                                    fErechRechiv += float.Parse(Math.Round(SumEzer, MidpointRounding.AwayFromZero).ToString()); 
                                                SumEzer = 0;
                                                i++;
                                            }
                                            else
                                            {
                                                if (SumEzer > 0)
                                                    fErechRechiv += float.Parse(Math.Round(SumEzer, MidpointRounding.AwayFromZero).ToString()); 
                                                SumEzer = 0;
                                                SumEzer += fErechSidur;
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            if (SumEzer > 0)
                                                fErechRechiv += float.Parse(Math.Round(SumEzer, MidpointRounding.AwayFromZero).ToString()); 
                                            SumEzer = 0;
                                            i++;
                                        }
                                    }
                                    if (SumEzer > 0)
                                        fErechRechiv += float.Parse(Math.Round(SumEzer, MidpointRounding.AwayFromZero).ToString()); 
                                   // fErechRechiv = float.Parse(Math.Round(fErechRechiv, MidpointRounding.AwayFromZero).ToString());
                                    addRowToTable(clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(), fErechRechiv);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: " + ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv203()
        {
            float fSumDakotRechiv, fSachKmVisa, fErechSidur;
            try
            {
                objOved.DtYemeyAvodaYomi.Select(null, "TACHOGRAF");
                if (objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0  and TACHOGRAF=1", "shat_hatchala_sidur ASC").Length == 0)
                {
                    if (oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom) && oCalcBL.CheckYomShishi(objOved.SugYom))
                    {
                        oSidur.CalcRechiv203();

                        fErechSidur = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotPremiaVisaShishi.GetHashCode(), objOved.Taarich);

                        fSachKmVisa = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode(), objOved.Taarich);

                        fSumDakotRechiv = float.Parse(Math.Round((((fErechSidur / 1.2) + fSachKmVisa) / 50) * 60 * 0.33, MidpointRounding.AwayFromZero).ToString());


                        addRowToTable(clGeneral.enRechivim.DakotPremiaVisaShishi.GetHashCode(), fSumDakotRechiv);

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotPremiaVisaShishi.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv207()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv207();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.MichutzLamichsaTafkidShishi.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.MichutzLamichsaTafkidShishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.MichutzLamichsaTafkidShishi.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv208()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv208();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.MichutzLamichsaTnuaShishi.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.MichutzLamichsaTnuaShishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.MichutzLamichsaTnuaShishi.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv209()
        {
            float fSumDakotRechiv, fZmanAruchatTzharayim;
            int iMutaam;
            try
            {
                if (objOved.objPirteyOved.iIsuk == clGeneral.enIsukOved.Rasham.GetHashCode() || objOved.objPirteyOved.iIsuk == clGeneral.enIsukOved.SganMenahel.GetHashCode() || objOved.objPirteyOved.iIsuk == clGeneral.enIsukOved.MenahelMachlaka.GetHashCode())
                {
                    if (objOved.objPirteyOved.iYechidaIrgunit == clGeneral.enYechidaIrgunit.RishumZafonHaifa.GetHashCode() || objOved.objPirteyOved.iYechidaIrgunit == clGeneral.enYechidaIrgunit.RishumArtzi.GetHashCode() || objOved.objPirteyOved.iYechidaIrgunit == clGeneral.enYechidaIrgunit.RishumBameshek.GetHashCode())
                    {
                        iMutaam = objOved.objPirteyOved.iMutamut;
                        if (iMutaam != clGeneral.enMutaam.enMutaam1.GetHashCode() && iMutaam != clGeneral.enMutaam.enMutaam3.GetHashCode() && iMutaam != clGeneral.enMutaam.enMutaam5.GetHashCode() && iMutaam != clGeneral.enMutaam.enMutaam7.GetHashCode())
                        {
                            if (objOved.objMeafyeneyOved.iMeafyen60 == 0)
                            {
                                fZmanAruchatTzharayim = oSidur.CalcRechiv209();

                                if (fZmanAruchatTzharayim > 30)
                                { fZmanAruchatTzharayim = 30; }

                                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.NochehutPremiaLerishum.GetHashCode(), objOved.Taarich);
                                fSumDakotRechiv = fSumDakotRechiv - fZmanAruchatTzharayim;
                                addRowToTable(clGeneral.enRechivim.NochehutPremiaLerishum.GetHashCode(), fSumDakotRechiv);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutPremiaLerishum.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv210()
        {
            float fSumDakotRechiv, fZmanAruchatBoker, fZmanAruchatTzharayim, fZmanAruchatErev;
            try
            {

                oSidur.CalcRechiv210(out fZmanAruchatBoker, out fZmanAruchatTzharayim, out fZmanAruchatErev);

                if (fZmanAruchatBoker > 20)
                { fZmanAruchatBoker = 20; }
                if (fZmanAruchatTzharayim > 30)
                { fZmanAruchatTzharayim = 30; }
                if (fZmanAruchatErev > 20)
                { fZmanAruchatErev = 20; }

                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.NochehutPremiaMevakrim.GetHashCode(), objOved.Taarich);
                fSumDakotRechiv = fSumDakotRechiv - fZmanAruchatBoker - fZmanAruchatTzharayim - fZmanAruchatErev;
                addRowToTable(clGeneral.enRechivim.NochehutPremiaMevakrim.GetHashCode(), fSumDakotRechiv);


            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutPremiaMevakrim.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv211_212()
        {
            float fSumRechivMusachim, fSumRechivAchsana, fZmanAruchatBoker, fZmanAruchatTzharayim, fZmanAruchatErev;
            int iBokerRechiv, iTzharyimRechiv, iErevRechiv;
            try
            {
                //if (objOved.sSugYechida.ToLower() == "m_ms" || objOved.sSugYechida.ToLower() == "m_me")
                //{
                    oSidur.CalcRechiv211_212(out fZmanAruchatBoker, out fZmanAruchatTzharayim, out fZmanAruchatErev, out  iBokerRechiv, out  iTzharyimRechiv, out  iErevRechiv);

                    ////if (fZmanAruchatBoker > 20)
                    ////{ fZmanAruchatBoker = 20; }
                    ////if (fZmanAruchatTzharayim > 30)
                    ////{ fZmanAruchatTzharayim = 30; }
                    ////if (fZmanAruchatErev > 20)
                    ////{ fZmanAruchatErev = 20; }

                    fSumRechivMusachim = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode(), objOved.Taarich);
                    fSumRechivAchsana = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.NochehutPremiaMeshekAchsana.GetHashCode(), objOved.Taarich);
                    //if (iBokerRechiv == clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode())
                    //{
                    //    fSumRechivMusachim = fSumRechivMusachim - fZmanAruchatBoker;
                    //}
                    //else
                    //{ fSumRechivAchsana = fSumRechivAchsana - fZmanAruchatBoker; }

                    //if (iTzharyimRechiv == clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode())
                    //{
                    //    fSumRechivMusachim = fSumRechivMusachim - fZmanAruchatTzharayim;
                    //}
                    //else
                    //{ fSumRechivAchsana = fSumRechivAchsana - fZmanAruchatTzharayim; }

                    //if (iErevRechiv == clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode())
                    //{
                    //    fSumRechivMusachim = fSumRechivMusachim - fZmanAruchatErev;
                    //}
                    //else
                    //{ fSumRechivAchsana = fSumRechivAchsana - fZmanAruchatErev; }


                    addRowToTable(clGeneral.enRechivim.NochehutPremiaMeshekAchsana.GetHashCode(), fSumRechivAchsana);
                    addRowToTable(clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode(), fSumRechivMusachim);

                //}

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv276()
        {
            float fSumErechRechiv;
            try
            {
                //if (objOved.sSugYechida.ToLower() == "m_ms" || objOved.sSugYechida.ToLower() == "m_me")
                //{
                    oSidur.CalcRechiv276();
                    fSumErechRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.NochechutLePremiyaMeshekGrira.GetHashCode(), objOved.Taarich);
                    addRowToTable(clGeneral.enRechivim.NochechutLePremiyaMeshekGrira.GetHashCode(), fSumErechRechiv);
                //}

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochechutLePremiyaMeshekGrira.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv277()
        {
            float fSumErechRechiv;
            try
            {
                //if (objOved.sSugYechida.ToLower() == "m_ms" || objOved.sSugYechida.ToLower() == "m_me")
                //{
                    oSidur.CalcRechiv277();
                    fSumErechRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.NochechutLePremiyaMeshekKonenutGrira.GetHashCode(), objOved.Taarich);
                    addRowToTable(clGeneral.enRechivim.NochechutLePremiyaMeshekKonenutGrira.GetHashCode(), fSumErechRechiv);
                //}

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochechutLePremiyaMeshekKonenutGrira.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv213()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv213();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.SachNesiot.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.SachNesiot.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachNesiot.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv214()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv214();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotHistaglut.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.DakotHistaglut.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotHistaglut.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv215()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv215();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.SachKM.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.SachKM.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachKM.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv216()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv216();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv217()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv217();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotHagdara.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.DakotHagdara.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotHagdara.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv218()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv218();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotKisuiTor.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.DakotKisuiTor.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotKisuiTor.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv219()
        {
            float fSumDakotRechiv, fDakotChofesh;
            try
            {
                fDakotChofesh = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotChofesh.GetHashCode(), objOved.Taarich);
                if (fDakotChofesh > 0)
                {
                    fSumDakotRechiv = fDakotChofesh / 60;
                    addRowToTable(clGeneral.enRechivim.ShaotChofesh.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ShaotChofesh.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv220()
        {
            float fSumDakotRechiv, fYomHeadrut, fMichsaYomit;
            try
            {
                fYomHeadrut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomHeadrut.GetHashCode(), objOved.Taarich);
                fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);
                if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                {
                    fSumDakotRechiv = fYomHeadrut * fMichsaYomit * (1 / objOved.fMekademNipuach);
                }
                else
                {
                    fSumDakotRechiv = fYomHeadrut * fMichsaYomit;
                }
                fSumDakotRechiv = float.Parse(Math.Floor(Math.Round( fSumDakotRechiv,MidpointRounding.AwayFromZero)).ToString());
                addRowToTable(clGeneral.enRechivim.DakotHeadrut.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotHeadrut.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv221()
        {
            float fSumDakotRechiv, fYomChofesh, fMichsaYomit;
            try
            {
                fYomChofesh = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomChofesh.GetHashCode(), objOved.Taarich);
                fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);
                if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                {
                    fSumDakotRechiv = fYomChofesh * fMichsaYomit *(1/objOved.fMekademNipuach);
                }
                else
                {
                    fSumDakotRechiv = fYomChofesh * fMichsaYomit;
                }
                
                fSumDakotRechiv = float.Parse(Math.Floor(Math.Round( fSumDakotRechiv,MidpointRounding.AwayFromZero)).ToString());
                addRowToTable(clGeneral.enRechivim.DakotChofesh.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotChofesh.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv223()
        {
            float fSumDakotRechiv, fZmanAruchatTzharayim;
            int iMutaam;
            try
            {

                if (objOved.objPirteyOved.iYechidaIrgunit == clGeneral.enYechidaIrgunit.MachsanKartisimDarom.GetHashCode() || objOved.objPirteyOved.iYechidaIrgunit == clGeneral.enYechidaIrgunit.MachsanKartisimJerusalem.GetHashCode())
                {
                    iMutaam = objOved.objPirteyOved.iMutamut;
                    if (iMutaam > 0 && iMutaam != clGeneral.enMutaam.enMutaam1.GetHashCode() && iMutaam != clGeneral.enMutaam.enMutaam3.GetHashCode() && iMutaam != clGeneral.enMutaam.enMutaam5.GetHashCode() && iMutaam != clGeneral.enMutaam.enMutaam7.GetHashCode())
                    {
                        if (objOved.objMeafyeneyOved.iMeafyen60 > 0)
                        {
                            fZmanAruchatTzharayim = oSidur.CalcRechiv223();

                            if (fZmanAruchatTzharayim > 30)
                            { fZmanAruchatTzharayim = 30; }

                            fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.NochehutLepremiaMachsanKartisim.GetHashCode(), objOved.Taarich);
                            fSumDakotRechiv = fSumDakotRechiv - fZmanAruchatTzharayim;
                            addRowToTable(clGeneral.enRechivim.NochehutLepremiaMachsanKartisim.GetHashCode(), fSumDakotRechiv);

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLepremiaMachsanKartisim.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv233()
        {
            float fSumDakotRechiv, fZmanAruchatBoker, fZmanAruchatTzharayim, fZmanAruchatErev;
            try
            {

                oSidur.CalcNochehutPremia(clGeneral.enRechivim.NochehutLePremyatMifalYetzur.GetHashCode(), (objOved.objMeafyeneyOved.iMeafyen101 > 0 ? true : false), out fZmanAruchatBoker, out fZmanAruchatTzharayim, out fZmanAruchatErev);

                if (fZmanAruchatBoker > 20)
                { fZmanAruchatBoker = 20; }
                if (fZmanAruchatTzharayim > 30)
                { fZmanAruchatTzharayim = 30; }
                if (fZmanAruchatErev > 20)
                { fZmanAruchatErev = 20; }

                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.NochehutLePremyatMifalYetzur.GetHashCode(), objOved.Taarich);
                fSumDakotRechiv = fSumDakotRechiv - fZmanAruchatBoker - fZmanAruchatTzharayim - fZmanAruchatErev;
                addRowToTable(clGeneral.enRechivim.NochehutLePremyatMifalYetzur.GetHashCode(), fSumDakotRechiv);


            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLePremyatMifalYetzur.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv234()
        {
            float fSumDakotRechiv, fZmanAruchatBoker, fZmanAruchatTzharayim, fZmanAruchatErev;
            try
            {

                oSidur.CalcNochehutPremia(clGeneral.enRechivim.NochehutLePremyatNehageyTovala.GetHashCode(), (objOved.objMeafyeneyOved.iMeafyen102 > 0 ? true : false), out fZmanAruchatBoker, out fZmanAruchatTzharayim, out fZmanAruchatErev);

                if (fZmanAruchatBoker > 20)
                { fZmanAruchatBoker = 20; }
                if (fZmanAruchatTzharayim > 30)
                { fZmanAruchatTzharayim = 30; }
                if (fZmanAruchatErev > 20)
                { fZmanAruchatErev = 20; }

                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.NochehutLePremyatNehageyTovala.GetHashCode(), objOved.Taarich);
                fSumDakotRechiv = fSumDakotRechiv - fZmanAruchatBoker - fZmanAruchatTzharayim - fZmanAruchatErev;
                addRowToTable(clGeneral.enRechivim.NochehutLePremyatNehageyTovala.GetHashCode(), fSumDakotRechiv);


            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLePremyatNehageyTovala.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv235()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv235();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.NochehutLePremyatNehageyTenderim.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.NochehutLePremyatNehageyTenderim.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLePremyatNehageyTenderim.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv236()
        {
            float fSumDakotRechiv, fZmanAruchatBoker, fZmanAruchatTzharayim, fZmanAruchatErev;
            try
            {

                oSidur.CalcNochehutPremia(clGeneral.enRechivim.NochehutLePremyatDfus.GetHashCode(), (objOved.objMeafyeneyOved.iMeafyen104 > 0 ? true : false), out fZmanAruchatBoker, out fZmanAruchatTzharayim, out fZmanAruchatErev);

                if (fZmanAruchatBoker > 20)
                { fZmanAruchatBoker = 20; }
                if (fZmanAruchatTzharayim > 30)
                { fZmanAruchatTzharayim = 30; }
                if (fZmanAruchatErev > 20)
                { fZmanAruchatErev = 20; }

                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.NochehutLePremyatDfus.GetHashCode(), objOved.Taarich);
                fSumDakotRechiv = fSumDakotRechiv - fZmanAruchatBoker - fZmanAruchatTzharayim - fZmanAruchatErev;
                addRowToTable(clGeneral.enRechivim.NochehutLePremyatDfus.GetHashCode(), fSumDakotRechiv);


            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLePremyatDfus.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv237()
        {
            float fSumDakotRechiv, fZmanAruchatBoker, fZmanAruchatTzharayim, fZmanAruchatErev;
            try
            {

                oSidur.CalcNochehutPremia(clGeneral.enRechivim.NochehutLePremyatAchzaka.GetHashCode(), (objOved.objMeafyeneyOved.iMeafyen105 > 0 ? true : false), out fZmanAruchatBoker, out fZmanAruchatTzharayim, out fZmanAruchatErev);

                if (fZmanAruchatBoker > 20)
                { fZmanAruchatBoker = 20; }
                if (fZmanAruchatTzharayim > 30)
                { fZmanAruchatTzharayim = 30; }
                if (fZmanAruchatErev > 20)
                { fZmanAruchatErev = 20; }

                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.NochehutLePremyatAchzaka.GetHashCode(), objOved.Taarich);
                fSumDakotRechiv = fSumDakotRechiv - fZmanAruchatBoker - fZmanAruchatTzharayim - fZmanAruchatErev;
                addRowToTable(clGeneral.enRechivim.NochehutLePremyatAchzaka.GetHashCode(), fSumDakotRechiv);


            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLePremyatAchzaka.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv238()
        {
            float fSumDakotRechiv, fZmanAruchatBoker, fZmanAruchatTzharayim, fZmanAruchatErev;
            try
            {

                oSidur.CalcNochehutPremia(clGeneral.enRechivim.NochehutLePremyatGifur.GetHashCode(), (objOved.objMeafyeneyOved.iMeafyen106 > 0 ? true : false), out fZmanAruchatBoker, out fZmanAruchatTzharayim, out fZmanAruchatErev);

                if (fZmanAruchatBoker > 20)
                { fZmanAruchatBoker = 20; }
                if (fZmanAruchatTzharayim > 30)
                { fZmanAruchatTzharayim = 30; }
                if (fZmanAruchatErev > 20)
                { fZmanAruchatErev = 20; }

                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.NochehutLePremyatGifur.GetHashCode(), objOved.Taarich);
                fSumDakotRechiv = fSumDakotRechiv - fZmanAruchatBoker - fZmanAruchatTzharayim - fZmanAruchatErev;
                addRowToTable(clGeneral.enRechivim.NochehutLePremyatGifur.GetHashCode(), fSumDakotRechiv);


            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLePremyatGifur.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv239()
        {
            float fSumDakotRechiv, fZmanAruchatBoker, fZmanAruchatTzharayim, fZmanAruchatErev;
            try
            {

                oSidur.CalcNochehutPremia(clGeneral.enRechivim.NochehutLePremyaRishonLetzyon.GetHashCode(), (objOved.objMeafyeneyOved.iMeafyen107 > 0 ? true : false), out fZmanAruchatBoker, out fZmanAruchatTzharayim, out fZmanAruchatErev);

                if (fZmanAruchatBoker > 20)
                { fZmanAruchatBoker = 20; }
                if (fZmanAruchatTzharayim > 30)
                { fZmanAruchatTzharayim = 30; }
                if (fZmanAruchatErev > 20)
                { fZmanAruchatErev = 20; }

                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.NochehutLePremyaRishonLetzyon.GetHashCode(), objOved.Taarich);
                fSumDakotRechiv = fSumDakotRechiv - fZmanAruchatBoker - fZmanAruchatTzharayim - fZmanAruchatErev;
                addRowToTable(clGeneral.enRechivim.NochehutLePremyaRishonLetzyon.GetHashCode(), fSumDakotRechiv);


            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLePremyaRishonLetzyon.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv240()
        {
            float fSumDakotRechiv, fZmanAruchatBoker, fZmanAruchatTzharayim, fZmanAruchatErev;
            try
            {

                oSidur.CalcNochehutPremia(clGeneral.enRechivim.NochehutLePremyaTechnayYetzur.GetHashCode(), (objOved.objMeafyeneyOved.iMeafyen108 > 0 ? true : false), out fZmanAruchatBoker, out fZmanAruchatTzharayim, out fZmanAruchatErev);

                if (fZmanAruchatBoker > 20)
                { fZmanAruchatBoker = 20; }
                if (fZmanAruchatTzharayim > 30)
                { fZmanAruchatTzharayim = 30; }
                if (fZmanAruchatErev > 20)
                { fZmanAruchatErev = 20; }

                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.NochehutLePremyaTechnayYetzur.GetHashCode(), objOved.Taarich);
                fSumDakotRechiv = fSumDakotRechiv - fZmanAruchatBoker - fZmanAruchatTzharayim - fZmanAruchatErev;
                addRowToTable(clGeneral.enRechivim.NochehutLePremyaTechnayYetzur.GetHashCode(), fSumDakotRechiv);


            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLePremyaTechnayYetzur.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv241()
        {
            float fSumDakotRechiv, fZmanAruchatBoker, fZmanAruchatTzharayim, fZmanAruchatErev;
            try
            {

                oSidur.CalcNochehutPremia(clGeneral.enRechivim.NochehutLePremyatPerukVeshiputz.GetHashCode(), (objOved.objMeafyeneyOved.iMeafyen110 > 0 ? true : false), out fZmanAruchatBoker, out fZmanAruchatTzharayim, out fZmanAruchatErev);

                if (fZmanAruchatBoker > 20)
                { fZmanAruchatBoker = 20; }
                if (fZmanAruchatTzharayim > 30)
                { fZmanAruchatTzharayim = 30; }
                if (fZmanAruchatErev > 20)
                { fZmanAruchatErev = 20; }

                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.NochehutLePremyatPerukVeshiputz.GetHashCode(), objOved.Taarich);
                fSumDakotRechiv = fSumDakotRechiv - fZmanAruchatBoker - fZmanAruchatTzharayim - fZmanAruchatErev;
                addRowToTable(clGeneral.enRechivim.NochehutLePremyatPerukVeshiputz.GetHashCode(), fSumDakotRechiv);


            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLePremyatPerukVeshiputz.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv245()
        {
            float fMichsaYomit, fErechRechiv, fNochehutBefoal;
            try
            {
                if (objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30)
                {
                    if (objOved.objMeafyeneyOved.sMeafyen50 == "1")
                    {
                        fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);
                        fNochehutBefoal = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode(), objOved.Taarich);

                        if (fMichsaYomit > 0 && fNochehutBefoal > 0)
                        {
                            fErechRechiv = 1;

                            addRowToTable(clGeneral.enRechivim.EshelLeEggedTaavura.GetHashCode(), fErechRechiv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.EshelLeEggedTaavura.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv246()
        {
            float fSumDakotRechiv, fZmanAruchatTzharayim;
            try
            {
                if (objOved.objPirteyOved.iIsuk == clGeneral.enIsukOved.MenahelMoreNehiga.GetHashCode())
                {

                    fZmanAruchatTzharayim = oSidur.CalcRechiv246();

                    if (fZmanAruchatTzharayim > 30)
                    { fZmanAruchatTzharayim = 30; }

                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.NochechutLepremyaMenahel.GetHashCode(), objOved.Taarich);
                    fSumDakotRechiv = fSumDakotRechiv - fZmanAruchatTzharayim;
                    addRowToTable(clGeneral.enRechivim.NochechutLepremyaMenahel.GetHashCode(), fSumDakotRechiv);



                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochechutLepremyaMenahel.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv248()
        {
            float fSumDakotRechiv, fDakotNochehut, fMichsaYomit;
            try
            {
                fSumDakotRechiv = 0;
                fDakotNochehut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);
                fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);
                if (fDakotNochehut == 0 && fMichsaYomit > 0 && objOved.objMeafyeneyOved.iMeafyen33 == 0 && objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is null", "shat_hatchala_sidur ASC").Length > 0)
                {
                    fSumDakotRechiv = 1;
                }
                addRowToTable(clGeneral.enRechivim.YomChofeshNoDivuach.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomChofeshNoDivuach.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv249()
        {
            float fSumDakotRechiv, fDakotNochehut, fMichsaYomit;
            try
            {
                fSumDakotRechiv = 0;
                fDakotNochehut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);
                fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);
                if (fDakotNochehut == 0 && fMichsaYomit > 0 && objOved.objMeafyeneyOved.iMeafyen33 == 1 && objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is null", "shat_hatchala_sidur ASC").Length > 0)
                {
                    fSumDakotRechiv = 1;
                }
                addRowToTable(clGeneral.enRechivim.YomHeadrutNoDivuach.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomHeadrutNoDivuach.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv250()
        {
            float fSumDakotRechiv, fDakotTafkidChol, fTafkidShishi;
            try
            {
                fSumDakotRechiv = 0;
                fDakotTafkidChol = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNosafotTafkid.GetHashCode(), objOved.Taarich);
                fTafkidShishi = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(), objOved.Taarich);
                fSumDakotRechiv = fTafkidShishi + fDakotTafkidChol;
                addRowToTable(clGeneral.enRechivim.SachNosafotTafkidCholVeshishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachNosafotTafkidCholVeshishi.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv251()
        {
            float fSumDakotRechiv, fDakotTnuaChol, fTnuaShishi;
            try
            {
                fSumDakotRechiv = 0;
                fDakotTnuaChol = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNosafotNihul.GetHashCode(), objOved.Taarich);
                fTnuaShishi = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode(), objOved.Taarich);
                fSumDakotRechiv = fDakotTnuaChol + fTnuaShishi;
                addRowToTable(clGeneral.enRechivim.SachNosafotTnuaCholVeshishi.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachNosafotTnuaCholVeshishi.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv252()
        {
            float fSumDakotRechiv, fDakotNahagutChol, fNahagutShishi;
            try
            {
                if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                {
                    fSumDakotRechiv = 0;
                    fDakotNahagutChol = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNosafotNahagut.GetHashCode(), objOved.Taarich);
                    fNahagutShishi = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode(), objOved.Taarich);
                    fSumDakotRechiv = fNahagutShishi + fDakotNahagutChol;
                    addRowToTable(clGeneral.enRechivim.SachNosafotNahagutCholVeshishi.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.SachNosafotNahagutCholVeshishi.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv256()
        {
            float fSumRechiv, fZmanAruchatTzharayim;
            int iErevRechiv;
            try
            {

                oSidur.CalcRechiv256( out fZmanAruchatTzharayim, out  iErevRechiv);

                //if (fZmanAruchatTzharayim > 30)
                //{ fZmanAruchatTzharayim = 30; }

                fSumRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode(), objOved.Taarich);
               // fZmanAruchatTzharayim = Math.Min(fZmanAruchatTzharayim, 30 - objOved.fTotalAruchatZaharimForDay); ;
                fSumRechiv = fSumRechiv - fZmanAruchatTzharayim; // fZmanAruchatErev - fZmanAruchatTzharayim - fZmanAruchatBoker;

                objOved.fTotalAruchatZaharimForDay += fZmanAruchatTzharayim;
                addRowToTable(clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode(), fSumRechiv);


            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv257()
        {
            float fSumRechiv, fZmanAruchatTzharayim;
            int iErevRechiv;
            try
            {

                oSidur.CalcRechiv257( out fZmanAruchatTzharayim, out  iErevRechiv);

                //if (fZmanAruchatTzharayim > 30)
                //{ fZmanAruchatTzharayim = 30; }

                fSumRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.NochehutLepremiaMetachnenTnua.GetHashCode(), objOved.Taarich);

               // fZmanAruchatTzharayim = Math.Min(fZmanAruchatTzharayim, 30 - objOved.fTotalAruchatZaharimForDay); ;
                fSumRechiv = fSumRechiv - fZmanAruchatTzharayim; // fZmanAruchatErev - fZmanAruchatTzharayim - fZmanAruchatBoker;

                objOved.fTotalAruchatZaharimForDay += fZmanAruchatTzharayim;
                addRowToTable(clGeneral.enRechivim.NochehutLepremiaMetachnenTnua.GetHashCode(), fSumRechiv);


            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
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

                fSumRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.NochehutLepremiaSadran.GetHashCode(), objOved.Taarich);

              //  fZmanAruchatTzharayim = Math.Min(fZmanAruchatTzharayim, 30 - objOved.fTotalAruchatZaharimForDay); ;
                fSumRechiv = fSumRechiv - fZmanAruchatTzharayim; // fZmanAruchatErev - fZmanAruchatTzharayim - fZmanAruchatBoker;
                objOved.fTotalAruchatZaharimForDay += fZmanAruchatTzharayim;

                addRowToTable(clGeneral.enRechivim.NochehutLepremiaSadran.GetHashCode(), fSumRechiv);


            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
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

                fSumRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.NochehutLepremiaRakaz.GetHashCode(), objOved.Taarich);

              //  fZmanAruchatTzharayim = Math.Min(fZmanAruchatTzharayim, 30 - objOved.fTotalAruchatZaharimForDay); ;
                fSumRechiv = fSumRechiv - fZmanAruchatTzharayim; // fZmanAruchatErev - fZmanAruchatTzharayim - fZmanAruchatBoker;
                objOved.fTotalAruchatZaharimForDay += fZmanAruchatTzharayim;

                addRowToTable(clGeneral.enRechivim.NochehutLepremiaRakaz.GetHashCode(), fSumRechiv);


            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
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
  

                fSumRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.NochehutLepremiaPakach.GetHashCode(), objOved.Taarich);

             //   fZmanAruchatTzharayim = Math.Min(fZmanAruchatTzharayim, 30 - objOved.fTotalAruchatZaharimForDay); ;
                fSumRechiv = fSumRechiv - fZmanAruchatTzharayim; // fZmanAruchatErev - fZmanAruchatTzharayim - fZmanAruchatBoker;
                objOved.fTotalAruchatZaharimForDay += fZmanAruchatTzharayim;

                addRowToTable(clGeneral.enRechivim.NochehutLepremiaPakach.GetHashCode(), fSumRechiv);


            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochehutLepremiaPakach.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }
        private void CalcRechiv261()
        {
            float fSumDakotRechiv, fMachala;
            try
            {
                fSumDakotRechiv = 0;
                fMachala = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMachla.GetHashCode(), objOved.Taarich);
                if (fMachala == 1)
                    fSumDakotRechiv = fMachala;
                addRowToTable(clGeneral.enRechivim.MachalaYomMale.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.MachalaYomMale.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv262()
        {
            float fSumDakotRechiv, fMachala;
            try
            {
                fSumDakotRechiv = 0;
                fMachala = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMachla.GetHashCode(), objOved.Taarich);
                if (fMachala > 0 && fMachala < 1)
                    fSumDakotRechiv = 1;
                addRowToTable(clGeneral.enRechivim.MachalaYomChelki.GetHashCode(), fSumDakotRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.MachalaYomChelki.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv263()
        {
            float fSumDakotRechiv;
            try
            {
                if (objOved.objPirteyOved.iDirug != 85 || objOved.objPirteyOved.iDarga != 30)
                {
                    oSidur.CalcRechiv263();
                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.HashlamaBenahagut.GetHashCode(), objOved.Taarich);
                    addRowToTable(clGeneral.enRechivim.HashlamaBenahagut.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.HashlamaBenahagut.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv264()
        {
            float fSumDakotRechiv;
            try
            {
                if (objOved.objPirteyOved.iDirug != 85 || objOved.objPirteyOved.iDarga != 30)
                {
                    oSidur.CalcRechiv264();
                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.HashlamaBenihulTnua.GetHashCode(), objOved.Taarich);
                    addRowToTable(clGeneral.enRechivim.HashlamaBenihulTnua.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.HashlamaBenihulTnua.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv265()
        {
            float fSumDakotRechiv;
            try
            {
                if (objOved.objPirteyOved.iDirug != 85 || objOved.objPirteyOved.iDarga != 30)
                {
                    oSidur.CalcRechiv265();
                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.HashlamaBetafkid.GetHashCode(), objOved.Taarich);
                    addRowToTable(clGeneral.enRechivim.HashlamaBetafkid.GetHashCode(), fSumDakotRechiv);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.HashlamaBetafkid.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv266()
        {
            float fErechRechiv, fMichsaYomit, fTempW, fTempX;
            try
            {
                fTempX = 0;
                fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);
                if (fMichsaYomit > 0)
                {
                    fErechRechiv = oSidur.CalcRechiv266();
                    if (fErechRechiv > 0)
                    {
                        fTempX = fErechRechiv;

                        fTempW = 1;

                        if (objOved.objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                        {
                            fTempW = objOved.fMekademNipuach;
                        }

                        fErechRechiv = float.Parse(Math.Round(fTempW * fTempX, 2, MidpointRounding.AwayFromZero).ToString());
                        addRowToTable(clGeneral.enRechivim.YomMiluimChelki.GetHashCode(), fErechRechiv);
                    }
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YomMiluimChelki.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv267()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv267();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotElementim.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.DakotElementim.GetHashCode(), fSumDakotRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotElementim.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv268()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv268();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.DakotNesiaLepremia.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.DakotNesiaLepremia.GetHashCode(), fSumDakotRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotNesiaLepremia.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv269()
        {
            float fErechRechiv = 0;
            float fMichsaYomit, fDakotChofesh, fDakotHeadrut;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                fDakotChofesh = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotChofesh); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotChofesh.GetHashCode(), objOved.Taarich);
                fDakotHeadrut = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.DakotHeadrut); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotHeadrut.GetHashCode(), objOved.Taarich);
                fErechRechiv = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMachalatBenZug); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMachalatBenZug.GetHashCode(), objOved.Taarich);
                fErechRechiv = fErechRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMachalatHorim); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMachalatHorim.GetHashCode(), objOved.Taarich);
                fErechRechiv = fErechRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMachalaYeled); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMachalaYeled.GetHashCode(), objOved.Taarich);
                fErechRechiv = fErechRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMachla); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMachla.GetHashCode(), objOved.Taarich);
                fErechRechiv = fErechRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMachalaBoded); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMachalaBoded.GetHashCode(), objOved.Taarich);
                fErechRechiv = fErechRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomEvel); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomEvel.GetHashCode(), objOved.Taarich);
                fErechRechiv = fErechRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMiluim); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMiluim.GetHashCode(), objOved.Taarich);
                fErechRechiv = fErechRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomTeuna); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomTeuna.GetHashCode(), objOved.Taarich);
                fErechRechiv = fErechRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomShmiratHerayon); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomShmiratHerayon.GetHashCode(), objOved.Taarich);
                fErechRechiv = fErechRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMiluimChelki); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMiluimChelki.GetHashCode(), objOved.Taarich);
                fMichsaYomit = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.MichsaYomitMechushevet); // oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);
                fErechRechiv = fDakotChofesh + fDakotHeadrut + (fErechRechiv * fMichsaYomit);

                addRowToTable(clGeneral.enRechivim.DakotChofeshHeadrut.GetHashCode(), fErechRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.DakotChofeshHeadrut.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv270()
        {
            float fErechRechiv = 0;
            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], objOved.Taarich);

                fErechRechiv = fErechRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomChofesh); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomChofesh.GetHashCode(), objOved.Taarich);
                fErechRechiv = fErechRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomHeadrut); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomHeadrut.GetHashCode(), objOved.Taarich);
                fErechRechiv = fErechRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMachalatBenZug); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMachalatBenZug.GetHashCode(), objOved.Taarich);
                fErechRechiv = fErechRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMachalatHorim); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMachalatHorim.GetHashCode(), objOved.Taarich);
                fErechRechiv = fErechRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMachalaYeled); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMachalaYeled.GetHashCode(), objOved.Taarich);
                fErechRechiv = fErechRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMachalaBoded); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMachalaBoded.GetHashCode(), objOved.Taarich);
                fErechRechiv = fErechRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomEvel); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomEvel.GetHashCode(), objOved.Taarich);
                fErechRechiv = fErechRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMiluim); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMiluim.GetHashCode(), objOved.Taarich);
                fErechRechiv = fErechRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomTeuna); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomTeuna.GetHashCode(), objOved.Taarich);
                fErechRechiv = fErechRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomShmiratHerayon); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomShmiratHerayon.GetHashCode(), objOved.Taarich);
                fErechRechiv = fErechRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMiluimChelki); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMiluimChelki.GetHashCode(), objOved.Taarich);
                fErechRechiv = fErechRechiv + oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMachla); //oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomMachla.GetHashCode(), objOved.Taarich);

                addRowToTable(clGeneral.enRechivim.YemeyChofeshHeadrut.GetHashCode(), fErechRechiv);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YemeyChofeshHeadrut.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }


        private void CalcRechiv278()
        {
            float fErechRechiv = 0, fMichsaYomit = 0, fDakotNochechut = 0, fMichsaMekorit = 0;
            try
            {
               if (objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30)
               {
                   fMichsaMekorit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ETMichsaMekoritBeforeHafchata.GetHashCode(), objOved.Taarich);  
                  fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);  
                  fDakotNochechut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);

                  if (fMichsaYomit < fMichsaMekorit && fDakotNochechut > fMichsaYomit )
                  {
                      fErechRechiv = Math.Min(fDakotNochechut, fMichsaMekorit)  - fMichsaYomit;
                      addRowToTable(clGeneral.enRechivim.ETPaarBetweenMichsaRegilaAndMuktenet.GetHashCode(), fErechRechiv);
                  }
              }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.YemeyChofeshHeadrut.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }
        private void CalcRechiv271()
        {
            float fSumDakotRechiv;
            try
            {
                if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30))
                {
                    if ((objOved.objPirteyOved.iKodMaamdMishni != clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode()) ||
                         (objOved.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode() &&
                         (objOved.objPirteyOved.iIsuk == 122 || objOved.objPirteyOved.iIsuk == 123 || objOved.objPirteyOved.iIsuk == 124 || objOved.objPirteyOved.iIsuk == 127)))
                    {
                        //fTempX = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Nosafot125.GetHashCode(), objOved.Taarich);
                        //fTempX = fTempX + oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Shaot25.GetHashCode(), objOved.Taarich);
                        //fTempX = fTempX + oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Nosafot150.GetHashCode(), objOved.Taarich);
                        //fTempX = fTempX + oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.Shaot50.GetHashCode(), objOved.Taarich);

                        //if (fTempX > 0)
                        //{
                        oSidur.CalcRechiv271();
                        fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.ZmanLilaSidureyBoker.GetHashCode(), objOved.Taarich);
                        fSumDakotRechiv += oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanRetzifutBoker.GetHashCode(), objOved.Taarich);
                        addRowToTable(clGeneral.enRechivim.ZmanLilaSidureyBoker.GetHashCode(), fSumDakotRechiv);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.ZmanLilaSidureyBoker.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv931()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv931();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.HalbashaTchilatYom.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.HalbashaTchilatYom.GetHashCode(), fSumDakotRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.HalbashaTchilatYom.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private void CalcRechiv932()
        {
            float fSumDakotRechiv;
            try
            {
                oSidur.CalcRechiv932();
                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"], clGeneral.enRechivim.HalbashaSofYom.GetHashCode(), objOved.Taarich);
                addRowToTable(clGeneral.enRechivim.HalbashaSofYom.GetHashCode(), fSumDakotRechiv);

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.HalbashaSofYom.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }

        private int CheckSugMishmeret()
        {
            int iSugMishmeret = 0;
            DateTime dShatHatchala, dShatGmar;
            DataRow[] dr;
            int iSugYomLemichsa;
            try
            {
                dr = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and MISPAR_SIDUR=99001", "shat_hatchala_sidur ASC");
                if (dr.Length > 0)
                {
                    dShatHatchala = DateTime.Parse(dr[0]["SHAT_HATCHALA_LETASHLUM"].ToString());
                    dShatGmar = DateTime.Parse(dr[dr.Length - 1]["SHAT_GMAR_LETASHLUM"].ToString());
                    iSugYomLemichsa = oCalcBL.GetSugYomLemichsa(objOved, objOved.Taarich, objOved.objPirteyOved.iKodSectorIsuk, objOved.objMeafyeneyOved.iMeafyen56);
                    iSugMishmeret = clDefinitions.GetSugMishmeret(objOved.Mispar_ishi, objOved.Taarich, iSugYomLemichsa, dShatHatchala, dShatGmar, objOved.objParameters);
                }
                return iSugMishmeret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dr = null;
            }
        }

        private bool HaveRechivimInDay(DateTime taarich, string lst_rechivim)
        {
            DataRow[] dr;
            try
            {
                dr = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV in (" + lst_rechivim + ") and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')");

                if (dr.Length==0)
                {
                    return false;
                } 
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        private bool HaveRechivimInDay(DateTime taarich)
        {
            float fYomMachala, fYomMachalaBoded, fYomMachalatYeled, fYomMachaltHorim, fYomMachalaBenZug, fYomShmiratHerayon, fYomMiluim;

            try
            {
                Dictionary<int, float> ListOfSum = oCalcBL.GetSumsOfRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], taarich);

                fYomMachala = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMachla);
                fYomMachalaBoded = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMachalaBoded);
                fYomMachalatYeled = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMachalaYeled);
                fYomMachaltHorim = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMachalatHorim);
                fYomMachalaBenZug = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMachalatBenZug);
                fYomShmiratHerayon = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomShmiratHerayon);
                fYomMiluim = oCalcBL.GetSumErechRechiv(ListOfSum, clGeneral.enRechivim.YomMiluim);

                if (fYomMachala == 0 && fYomMachalaBoded == 0 && fYomMachalatYeled == 0 && fYomMachaltHorim == 0
                    && fYomMachalaBenZug == 0 && fYomShmiratHerayon == 0 && fYomMiluim == 0)
                {
                    return false;
                } return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        private bool CheckShishiMuchlaf(int iSugYom, DateTime dTaarich)
        {
            bool bShishiMuchlaf = false;

            if (objOved.oGeneralData.dtSugeyYamimMeyuchadim.Select("sug_yom=" + iSugYom)[0]["Shishi_Muhlaf"].ToString() == "1")
            {
                bShishiMuchlaf = true;
            }

            return bShishiMuchlaf;
        }

        public void ChishuvEmptyYemeyAvoda(clGeneral.enRechivim iKodRechiv, bool bBefore)
        {
            DateTime taarich_me, taarich_ad;
            float fMichsatYom = 0, fErechRechiv, fMichsatYomit;
            int sug_yom;
            clMeafyenyOved objMeafyeneyOved= null;
            try
            {
               
                if (iKodRechiv == clGeneral.enRechivim.DakotHeadrut)
                {
                    if (bBefore)
                        taarich_me = objOved.dTchilatAvoda;
                    else taarich_me = objOved.Month;

                    while (fMichsatYom == 0 && taarich_me < objOved.Month.AddMonths(1))
                    {
                         sug_yom = clGeneral.GetSugYom(objOved.oGeneralData.dtYamimMeyuchadim, taarich_me, objOved.oGeneralData.dtSugeyYamimMeyuchadim);
                        if (sug_yom != clGeneral.enSugYom.Shishi.GetHashCode() &&
                            !clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, sug_yom, taarich_me) &&
                            !oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, sug_yom))
                        {
                            fMichsatYom = float.Parse(objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + taarich_me.ToShortDateString() + "', 'System.DateTime')")[0]["ERECH_RECHIV"].ToString());
                        }
                        taarich_me = taarich_me.AddDays(1);
                    }
                }
                if (iKodRechiv == clGeneral.enRechivim.DakotHeadrut)
                {
                    objMeafyeneyOved = objOved.MeafyeneyOved.Find(Meafyenim => (Meafyenim._Taarich ==(bBefore ? objOved.dTchilatAvoda : objOved.Month) ));
                }      
               
                if (bBefore)
                {
                    taarich_me = objOved.Month;
                    taarich_ad = objOved.dTchilatAvoda;
                }
                else
                {
                    taarich_me = objOved.dSiyumAvoda.AddDays(1);
                    taarich_ad = objOved.Month.AddMonths(1);
                }
                
                while (taarich_me < taarich_ad)
                {
                    sug_yom = clGeneral.GetSugYom(objOved.oGeneralData.dtYamimMeyuchadim, taarich_me, objOved.oGeneralData.dtSugeyYamimMeyuchadim);
                    if ((sug_yom != clGeneral.enSugYom.Shishi.GetHashCode() &&
                        !clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, sug_yom, taarich_me) &&
                        !oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, sug_yom)) || iKodRechiv == clGeneral.enRechivim.MichsaYomitMechushevet)
                    {
                        switch (iKodRechiv)
                        {
                            case clGeneral.enRechivim.YomHeadrut:
                                addRowToTable(iKodRechiv.GetHashCode(), taarich_me, 1);
                                break;
                            case clGeneral.enRechivim.MichsaYomitMechushevet:
                                objOved.Taarich = taarich_me;
                                fMichsatYomit = oCalcBL.getMichsaYomit(objOved);
                                addRowToTable(iKodRechiv.GetHashCode(), taarich_me, fMichsatYomit);
                                break;
                            case clGeneral.enRechivim.DakotHeadrut:
                                fErechRechiv = fMichsatYom;
                                if (objMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                                    fErechRechiv = fErechRechiv * (1 / objOved.fMekademNipuach);

                                addRowToTable(iKodRechiv.GetHashCode(), taarich_me, fErechRechiv);
                                break;
                            case clGeneral.enRechivim.ShaotHeadrut:
                                fErechRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotHeadrut.GetHashCode(), taarich_me) / 60;
                                addRowToTable(iKodRechiv.GetHashCode(), taarich_me, fErechRechiv);
                                break;
                        }

                    }

                    taarich_me = taarich_me.AddDays(1);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void CalcChofeshHeadrutToShguyim(clGeneral.enRechivim iKodRechiv)
        {
            DateTime dTarMe, dTarAd;
            float fMichsaYomit = 0, fDakotNochehut = 0;
            float fChofesh = 0,fHeadrut=0;
            try
            {
                dTarMe = objOved.Month;
                dTarAd = objOved.Month.AddMonths(1).AddDays(-1);
                 
                while (dTarMe <= dTarAd)
                {
                    fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), dTarMe);
                    fDakotNochehut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), dTarMe);
                    fChofesh = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomChofesh.GetHashCode(), dTarMe);
                    fHeadrut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.YomHeadrut.GetHashCode(), dTarMe);

                    if (fMichsaYomit > 0 && fDakotNochehut == 0 && fChofesh == 0 && fHeadrut == 0 && objOved.DtYemeyAvoda.Select("taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime') and Lo_letashlum=0 and mispar_sidur is not null").Length == 0)
                    {
                        objOved.Taarich = dTarMe;
                        objOved.objMeafyeneyOved = objOved.MeafyeneyOved.Find(Meafyenim => (Meafyenim._Taarich == dTarMe));
                        objOved.SetMatzavOved();

                       //פעיל = "01", חופשה = "03", חופשה בחול = "04",מחלה ארוכה="05", חופש שלילה="06",עבודות שרות="07", מאסר השהיה עם שכר="08"
                        if (iKodRechiv.GetHashCode() == clGeneral.enRechivim.YomHeadrut.GetHashCode() && objOved.sMatazavOved != "01" && objOved.sMatazavOved != "03" &&
                            objOved.sMatazavOved != "04" && objOved.sMatazavOved != "05" && objOved.sMatazavOved != "06" && objOved.sMatazavOved != "07"
                                && objOved.sMatazavOved != "08" && objOved.objMeafyeneyOved.iMeafyen33 == 0)
                        { addRowToTable(iKodRechiv.GetHashCode(), dTarMe, 1); }

                        if (iKodRechiv.GetHashCode()== clGeneral.enRechivim.YomChofesh.GetHashCode() && objOved.objMeafyeneyOved.iMeafyen33 == 0 &&
                             (objOved.sMatazavOved =="01" || objOved.sMatazavOved == "03" || objOved.sMatazavOved == "04" || objOved.sMatazavOved == "05" ||
                                    objOved.sMatazavOved == "06" || objOved.sMatazavOved == "07" || objOved.sMatazavOved == "08"))
                        {
                            addRowToTable(iKodRechiv.GetHashCode(), dTarMe, 1);
                        }
                        if (iKodRechiv.GetHashCode() == clGeneral.enRechivim.YomHeadrut.GetHashCode() && objOved.objMeafyeneyOved.iMeafyen33 == 1)
                        {
                            addRowToTable(iKodRechiv.GetHashCode(), dTarMe, 1);
                        }
                    }

                    dTarMe = dTarMe.AddDays(1);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private void addRowToTable(int iKodRechiv, float fErechRechiv)
        {
            DataRow drChishuv;
//            drChishuvRows = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + iKodRechiv.ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')");
            if (CountOfRecords(iKodRechiv) == 0) // instead of (drChishuvRows.Length == 0)
            {
                if (fErechRechiv > 0)
                {
                    drChishuv = _dtChishuvYom.NewRow();
                    drChishuv["BAKASHA_ID"] = objOved.iBakashaId;
                    drChishuv["MISPAR_ISHI"] = objOved.Mispar_ishi;
                    drChishuv["TAARICH"] = objOved.Taarich;
                    drChishuv["TKUFA"] = objOved.Taarich.ToString("MM/yyyy");
                    drChishuv["KOD_RECHIV"] = iKodRechiv;
                    drChishuv["ERECH_RECHIV"] = fErechRechiv;
                    drChishuv["ERECH_EZER"] = System.DBNull.Value;
                    _dtChishuvYom.Rows.Add(drChishuv);
                    drChishuv = null;
                }
            }
            else
            {
                UpdateRowInTable(iKodRechiv, fErechRechiv, 0);
            }
        }

        private void addRowToTable(int iKodRechiv,DateTime dtaarich, float fErechRechiv)
        {
            DataRow drChishuv;
            //            drChishuvRows = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + iKodRechiv.ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')");
            //if (CountOfRecords(iKodRechiv) == 0) // instead of (drChishuvRows.Length == 0)
            //{
                //if (fErechRechiv > 0)
                //{
                    drChishuv = _dtChishuvYom.NewRow();
                    drChishuv["BAKASHA_ID"] = objOved.iBakashaId;
                    drChishuv["MISPAR_ISHI"] = objOved.Mispar_ishi;
                    drChishuv["TAARICH"] = dtaarich;
                    drChishuv["TKUFA"] = objOved.Taarich.ToString("MM/yyyy");
                    drChishuv["KOD_RECHIV"] = iKodRechiv;
                    drChishuv["ERECH_RECHIV"] = fErechRechiv;
                    drChishuv["ERECH_EZER"] = System.DBNull.Value;
                    _dtChishuvYom.Rows.Add(drChishuv);
                    drChishuv = null;
                //}
            //}
            //else
            //{
            //    UpdateRowInTable(iKodRechiv, fErechRechiv, 0);
            //}
        }
        private void addRowToTable(int iKodRechiv, float fErechRechiv, float fErechEzer)
        {
            if (fErechRechiv > 0 || fErechEzer>0)
            {
                DataRow drChishuv;
//                DataRow[] drChishuvRows;
  //              drChishuvRows = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select(null, "KOD_RECHIV");
    //            drChishuvRows = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + iKodRechiv.ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')");
                if (CountOfRecords(iKodRechiv) == 0) // instead of (drChishuvRows.Length == 0)
                {
                    drChishuv = _dtChishuvYom.NewRow();
                    drChishuv["BAKASHA_ID"] = objOved.iBakashaId;
                    drChishuv["MISPAR_ISHI"] = objOved.Mispar_ishi;
                    drChishuv["TAARICH"] = objOved.Taarich;
                    drChishuv["TKUFA"] = objOved.Taarich.ToString("MM/yyyy");
                    drChishuv["KOD_RECHIV"] = iKodRechiv;
                    drChishuv["ERECH_RECHIV"] = fErechRechiv;
                    drChishuv["ERECH_EZER"] = fErechEzer;
                    _dtChishuvYom.Rows.Add(drChishuv);
                    drChishuv = null;
                }
                else
                {
                    UpdateRowInTable(iKodRechiv, fErechRechiv, fErechEzer);
                }
            }
        }
        private int CountOfRecords(int iKodRechiv)
        {
            try
            {
                //DataRow[] drChishuvRows;
                //drChishuvRows = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select(null, "KOD_RECHIV");
                //drChishuvRows = objOved._dsChishuv.Tables["CHISHUV_YOM"].Select("KOD_RECHIV=" + iKodRechiv.ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')");

                //return drChishuvRows.Count();
                return (from c in objOved._dsChishuv.Tables["CHISHUV_YOM"].AsEnumerable()
                        where c.Field<int>("KOD_RECHIV").Equals(iKodRechiv)
                        && c.Field<DateTime>("taarich").ToShortDateString().Equals(objOved.Taarich.ToShortDateString())
                        select c).Count();
            }
            catch(Exception ex) 
            {
                throw new Exception("CountOfRecords:" + ex.StackTrace + "\n message: "+ ex.Message);
            }
        }

        private void UpdateRowInTable(int iKodRechiv, float fErechRechiv, float fErechEzer)
        {
            DataRow drChishuv;
            _dtChishuvYom.Select(null, "KOD_RECHIV");
            drChishuv = _dtChishuvYom.Select("KOD_RECHIV=" + iKodRechiv + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')")[0];
            drChishuv["ERECH_RECHIV"] = fErechRechiv;
            if (fErechEzer > 0)
            {
                drChishuv["ERECH_EZER"] = fErechEzer;
            }
            drChishuv = null;
        }


    }
}
