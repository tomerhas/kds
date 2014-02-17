using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;
using KdsLibrary.UDT;
using KdsLibrary;
using KdsLibrary.BL;

namespace KdsBatch
{
    class CalcSidur
    {
        private Oved objOved;
      //  public int SugYom { get; set; }
        private DataTable _dtChishuvSidur;
        public CalcPeilut oPeilut;
        private clCalcBL oCalcBL;
 
        public CalcSidur(Oved oOved)
        {
            objOved = oOved;
            _dtChishuvSidur = objOved._dsChishuv.Tables["CHISHUV_SIDUR"]; //objOved._DtSidur;
            oPeilut = new CalcPeilut(objOved);
          // oPeilut.iSugYom = SugYom;
            oCalcBL = new clCalcBL();
         }
        public void SetNullObject()
        {
            oPeilut = null;
            oCalcBL = null;
        }

        public void CalcRechiv1()
        {
            //דקות נוכחות לתשלום( רכיב 1):
            //שעת גמר לתשלום של סידור פחות שעת התחלה לתשלום של סידור.
            //[TB_Sidurim_Ovedim.Shat_gmar_Letashlum] פחות [TB_Sidurim_Ovedim.Shat_hatchala_Letashlum]
            // לפתוח רכיב לכל הסידורים הרגילים וכן לפתוח רשומה עבור כל הסידורים המיוחדים (מספר סידור מתחיל ב- "99") הנסכמים לרכיב על פי [שליפת סידורים מיוחדים לרכיב (קוד רכיב = 1)].
            int iMisparSidur, iSugSidur,iMeafyen53;
            float fErechRechiv, fMichsaYomit;
            string sSidurim;
            float fSumErechRechiv = 0;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            DataRow[] _drSidurMeyuchad, _drSidurRagil;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            float fZmanAruhatZharayim;

            try
            {
                // סידורים רגילים
               // //oPeilut.objOved.Taarich = objOved.Taarich;
                _drSidurRagil = GetSidurimRegilim();
                if (_drSidurRagil.Length > 0)
                {
                    for (int I = 0; I < _drSidurRagil.Length; I++)
                    {

                        iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_sidur"].ToString());
                        dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                        dShatGmarLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_gmar_letashlum"].ToString());

                        fErechRechiv = float.Parse(_drSidurRagil[I]["ZMAN_LELO_HAFSAKA"].ToString()); //float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());              
                       // fErechRechiv = float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());

                        // //SetSugSidur(ref _drSidurRagil[I],objOved.Taarich, iMisparSidur);

                        //iSugSidur = int.Parse(_drSidurRagil[I]["sug_sidur"].ToString());
                        //if (iSugSidur == 69)
                        //{
                        //    //•	סידור כוננות גרירה – אם סוג סידור = 69 
                        //    if (fErechRechiv > objOved.objParameters.iMinZmanGriraDarom)
                        //    {
                        //        if (int.Parse(iMisparSidur.ToString().Substring(0, 2)) > 11)
                        //        {
                        //            fErechRechiv = objOved.objParameters.iMinZmanGriraDarom;
                        //        }
                        //        else
                        //        {
                        //            fErechRechiv = objOved.objParameters.iMinZmanGriraTzafon;
                        //        }
                        //    }
                        //    else if (fErechRechiv > objOved.objParameters.iMinZmanGriraTzafon && fErechRechiv <= objOved.objParameters.iMinZmanGriraDarom)
                        //    {
                        //        if (int.Parse(iMisparSidur.ToString().Substring(0, 2)) <= 11)
                        //        {
                        //            fErechRechiv = objOved.objParameters.iMinZmanGriraTzafon;
                        //        }
                        //    }
                        //}
                        fSumErechRechiv += fErechRechiv;
                        addRowToTable(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);
                    }
                }

                // סידורים מיוחדים
                sSidurim = GetSidurimMeyuchRechiv(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode());
                if (sSidurim.Length > 0)
                {
                    _drSidurMeyuchad = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and sidur_misug_headrut is null AND MISPAR_SIDUR IN(" + sSidurim + ")", "taarich asc");
                    fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString()));

                    for (int I = 0; I < _drSidurMeyuchad.Length; I++)
                    {
                        dShatHatchalaSidur = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_sidur"].ToString());
                        iMisparSidur = int.Parse(_drSidurMeyuchad[I]["mispar_sidur"].ToString());

                        fErechRechiv = CalcRechiv1BySidur(_drSidurMeyuchad[I], fMichsaYomit, oPeilut);

                        if (string.IsNullOrEmpty(_drSidurMeyuchad[I]["sidur_misug_headrut"].ToString()))
                            fSumErechRechiv += fErechRechiv;

                        addRowToTable(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);
                    }

                    //ב.	אם מדובר בסידור היעדרות שאינו מילואים [שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 53 )] עם ערך כלשהו <> 3 יש לבצע את החישוב לאחר סיום חישוב רכיב זה לכל שאר הסידורים ביום. יש לחשב את X = סכום ערך הרכיב עבור כלל הסידורים ביום העבודה שאינם סידורי היעדרות (לא כולל מילואים). כלומר אין לכלול בסכימה את הסידורים בעלי מאפיין 53 עם ערך כלשהו השונה מ- 3 [שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 53 )]  . 
                    //אם X >= ממכסה יומית מחושבת (רכיב 126) אזי אין לפתוח רשומה לרכיב זה לסידור.
                    //אחרת, ערך הרכיב = הנמוך מבין (נוכחות מחושבת, מכסה יומית מחושבת (רכיב 126) פחות X)
                    _drSidurMeyuchad = null;
                    _drSidurMeyuchad = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and sidur_misug_headrut is not null AND MISPAR_SIDUR IN(" + sSidurim + ")", "taarich asc");

                    for (int I = 0; I < _drSidurMeyuchad.Length; I++)
                    {
                        iMeafyen53= int.Parse(_drSidurMeyuchad[I]["sidur_misug_headrut"].ToString());
                        iMisparSidur = int.Parse(_drSidurMeyuchad[I]["mispar_sidur"].ToString());
                        if ((iMeafyen53 == 2 && iMisparSidur != 99816) || iMeafyen53 == 4 || iMeafyen53 == 6 || (iMeafyen53 == 3 && iMisparSidur == 99832) || iMeafyen53 == 8)
                        {
                            dShatHatchalaSidur = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_sidur"].ToString());
                          
                            fZmanAruhatZharayim = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode(), objOved.Taarich);
                            if ((fSumErechRechiv>0 || iMisparSidur == 99801) && fSumErechRechiv < (fMichsaYomit + fZmanAruhatZharayim))
                            {
                                fErechRechiv = CalcRechiv1BySidur(_drSidurMeyuchad[I], fMichsaYomit, oPeilut);
                                if (iMisparSidur == 99832)
                                {
                                    fErechRechiv = Math.Min(fErechRechiv, (fMichsaYomit + fZmanAruhatZharayim - fSumErechRechiv));
                                    fErechRechiv = Math.Min(fErechRechiv, fMichsaYomit/2);
                                }
                                else
                                    fErechRechiv = Math.Min(fErechRechiv, (fMichsaYomit + fZmanAruhatZharayim - fSumErechRechiv));
                                fSumErechRechiv += fErechRechiv;
                                addRowToTable(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);
                            }
                        }
                    }

                 
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
             finally{
                _drSidurMeyuchad=null;
                _drSidurRagil = null; ;
            }
        }

        public float CalcRechiv1BySidur(DataRow drSidur, float fMichsaYomit, CalcPeilut oPeilut)
        {
            float fErechRechiv, fErechPeiluyot,fErech;
            int iMisparSidur;//, iSugYom;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dTempDtFrom, dTempDtTo, dShatHatchalaSidur,shaa;
            string sQury="";
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                iMisparSidur = int.Parse(drSidur["mispar_sidur"].ToString());
                dShatHatchalaSidur = DateTime.Parse(drSidur["shat_hatchala_sidur"].ToString());

                //  iDay = int.Parse(_drSidurMeyuchad[I]["day_taarich"].ToString());
              //  iSugYom = SugYom;
                dShatHatchalaLetashlum = DateTime.Parse(drSidur["shat_hatchala_letashlum"].ToString());
                dShatGmarLetashlum = DateTime.Parse(drSidur["shat_gmar_letashlum"].ToString());

                fErechRechiv = float.Parse(drSidur["ZMAN_LELO_HAFSAKA"].ToString()); //float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());              
            
                //יוצאי דופן סידורים מיוחדים  
                //if (iMisparSidur == 99707)
                //    fErechRechiv = fMichsaYomit + 120;
                //else if (iMisparSidur == 99708)
                //    fErechRechiv = fMichsaYomit + 60;
                //else
                if (iMisparSidur == 99010)
                {
                    //•	סידור 99010 (8549) – כדורגל: הנמוך מבין (נוכחות מחושבת, 180 שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 60 ) ).
                    fErechRechiv = Math.Min(fErechRechiv, int.Parse(drSidur["max_dakot_boded"].ToString()));
                }
                else if (!string.IsNullOrEmpty(drSidur["shat_gmar_auto"].ToString()))
                {
                   //ג.	סידור עם מאפיין שעת גמר אוטומטית לחישוב:
                    //אם לסידור מאפיין שעת גמר אוטומטית לחישוב שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 89):
                    //ערך הרכיב  = ערך מאפיין 89 פחות ש.התחלה לתשלום.
                    fErechRechiv =float.Parse((clGeneral.GetDateTimeFromStringHour(drSidur["shat_gmar_auto"].ToString(), objOved.Taarich)  - dShatHatchalaLetashlum).TotalMinutes.ToString());
                }
                //	סידורים מוגבלים בדקות התשלום היומיות  
                else if (!string.IsNullOrEmpty(drSidur["max_shaot_byom_hol"].ToString()) && objOved.SugYom == clGeneral.enSugYom.Chol.GetHashCode())
                {
                    fErechRechiv = Math.Min(fErechRechiv, int.Parse(drSidur["max_shaot_byom_hol"].ToString()));
                }
                else if (!string.IsNullOrEmpty(drSidur["max_shaot_byom_shishi"].ToString()) && objOved.SugYom == clGeneral.enSugYom.Shishi.GetHashCode())
                {
                    fErechRechiv = Math.Min(fErechRechiv, int.Parse(drSidur["max_shaot_byom_shishi"].ToString()));
                }
                else if (!string.IsNullOrEmpty(drSidur["max_shaot_beshabaton"].ToString()) && objOved.SugYom == clGeneral.enSugYom.Shabat.GetHashCode() && (!(iMisparSidur == 99006 && objOved.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())))
                {
                    fErechRechiv = Math.Min(fErechRechiv, int.Parse(drSidur["max_shaot_beshabaton"].ToString()));
                }
                //•	סידורים המזכים בדקות קבועות ליום עבודה
                else if (fErechRechiv>0 && !string.IsNullOrEmpty(drSidur["tashlum_kavua_bchol"].ToString()) && objOved.SugYom == clGeneral.enSugYom.Chol.GetHashCode())
                {
                    fErechRechiv = float.Parse(drSidur["tashlum_kavua_bchol"].ToString());
                }
                else if (fErechRechiv > 0 && !string.IsNullOrEmpty(drSidur["tashlum_kavua_beshishi"].ToString()) && objOved.SugYom == clGeneral.enSugYom.Shishi.GetHashCode())
                {
                    fErechRechiv = float.Parse(drSidur["tashlum_kavua_beshishi"].ToString());
                }
                else if (fErechRechiv > 0 && !string.IsNullOrEmpty(drSidur["shabaton_tashlum_kavua"].ToString()) && objOved.SugYom == clGeneral.enSugYom.Shabat.GetHashCode())
                {
                    fErechRechiv =  float.Parse(drSidur["shabaton_tashlum_kavua"].ToString());
                }
                //else if (iMisparSidur == 99706)
                //{
                //    //•	סידור 99706 (8552) – קייטנה מאבטח: הנמוך מבין (נוכחות מחושבת, מכסה יומית מחושבת (רכיב 126)).
                //    fErechRechiv = fMichsaYomit; // Math.Min(fErechRechiv, fMichsaYomit);

            //}
                //else if (iMisparSidur == 99703)
                //{
                //    //	סידור 99703 (8567) – קייטנה השתלמות טרום: 
                //    if (fMichsaYomit > 0)
                //    {
                //        ////אם קיימת מכסה יומית מחושבת (רכיב 126): הנמוך מבין (נוכחות מחושבת, מכסה יומית מחושבת (רכיב 126)).
                //        fErechRechiv = Math.Min(fErechRechiv, fMichsaYomit);
                //    }
                //    else
                //    {
                //        // לא קיימת מכסה יומית מחושבת (רכיב 126) וזה יום שישי : הנמוך מבין (נוכחות מחושבת,390 [שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 18 ])
                //        if (oCalcBL.CheckYomShishi(objOved.SugYom))
                //        {
                //            fErechRechiv = Math.Min(fErechRechiv, int.Parse(drSidur["max_shaot_byom_shishi"].ToString()));
                //        }
                //        //-	אם לא קיימת מכסה וזה שבת/שבתון [זיהוי שבת/ון]: הנמוך מבין (נוכחות מחושבת,480 [שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 19 ]).
                //        else if (clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                //        {
                //            fErechRechiv = Math.Min(fErechRechiv, int.Parse(drSidur["max_shaot_beshabaton"].ToString()));
                //        }
                //    }
                //}
                //else if (iMisparSidur == 99700)
                //{
                //    //•	סידור 99700 (8589) – אירועי קיץ 
                //    //-	אם לא שבת/שבתון: מכסה יומית (רכיב 126) + 390 [שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 62 ]). 
                //    if (!clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                //    {
                //        fErechRechiv = fMichsaYomit + int.Parse(drSidur["dakot_n_letashlum_hol"].ToString());
                //    }
                //    else
                //    {
                //        //-	אם שבת/שבתון [זיהוי שבת/ון]: 240 [שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 19 )].
                //        fErechRechiv = int.Parse(drSidur["max_shaot_beshabaton"].ToString());
                //    }
                //}
                //else if (iMisparSidur == 99711)
                //{
                //    //•	סידור 99711 (חדש)– אירועי קיץ – חוצה ישראל 
                //    //-	אם לא שבת/שבתון : מכסה יומית (רכיב 126) + 390 [שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 62 )]). 
                //    if (!clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                //    {
                //        fErechRechiv = fMichsaYomit + int.Parse(drSidur["dakot_n_letashlum_hol"].ToString());
                //    }
                //    else
                //    {
                //        //-	בשבת/שבתון: 420 שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 19 ).
                //        fErechRechiv = int.Parse(drSidur["max_shaot_beshabaton"].ToString());
                //    }
                //}
                else if (iMisparSidur == 99006)
                {//•	סידור 99006 (8554) – שליחות בחו"ל: 
                    if ((objOved.SugYom == clGeneral.enSugYom.Chol.GetHashCode()) ||  (oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom) && !oCalcBL.CheckYomShishi(objOved.SugYom)))
                    {
                        //-	ימים א – ה : מכסה יומית (רכיב 126) + 120 דקות שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 62 ). 
                        fErechRechiv = fMichsaYomit + int.Parse(drSidur["dakot_n_letashlum_hol"].ToString());

                    }
                    //-	שישי/ערב חג [זיהוי ערב חג] ושבת/שבתון [זיהוי שבת/ון]: מעמד חוזה (מעמד 23) = 240 דקות ,  שאר המעמדות = 300 דקות שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 19 ).
                    else if (clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich) || oCalcBL.CheckYomShishi(objOved.SugYom))
                    {
                        if (objOved.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
                        {
                            fErechRechiv = 240;
                        }
                        else
                        {
                            fErechRechiv = int.Parse(drSidur["max_shaot_beshabaton"].ToString());
                        }
                    }

                }
                else if (iMisparSidur == 99014)
                {
                    //•	סידור 99014 (8547) – תרבות : 
                    //-	שישי/ערב חג [זיהוי ערב חג] ושבת/שבתון [זיהוי שבת/ון]: הנמוך מבין (נוכחות מחושבת, 240 שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 19 )) 
                    if (clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich) || oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom))
                    {
                        fErechRechiv = Math.Min(fErechRechiv, int.Parse(drSidur["max_shaot_beshabaton"].ToString()));
                    }

                }
                else if (iMisparSidur == 99801)
                {
                    //•	סידור 99801 (8582) – תשלום יום עבודה 
                    // הנמוך מבין (נוכחות מחושבת, מכסה יומית (רכיב 126) ).
                    fErechRechiv = Math.Min(fErechRechiv, fMichsaYomit);
                }
                else if (!string.IsNullOrEmpty(drSidur["realy_veod_yom"].ToString()) && fMichsaYomit > 0)
                { //•	סידור עם מאפיין ריאלי ועד מכסה:
                    //אם לסידור מאפיין 91 שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 91) וגם מכסה יומית מחושבת (רכיב 126) > 0 ערך הרכיב = הנמוך מבין (דקות נוכחות לתשלום (רכיב 1) שחושבו עד כה, מכסה יומית מחושבת (רכיב 126) ) 
                    if (iMisparSidur == 99011 && objOved.objPirteyOved.iGil == clGeneral.enKodGil.enTzair.GetHashCode() && fErechRechiv >= 480)
                        fErechRechiv = fMichsaYomit;
                    else
                        fErechRechiv = Math.Min(fErechRechiv, fMichsaYomit); 
                }
                //else if (iMisparSidur == 99207 || iMisparSidur == 99011 || iMisparSidur == 99007)
                //{
                //    //•	99207 (8512) – קורס, 99011 (8513) – קורס
                //    if (objOved.SugYom == clGeneral.enSugYom.Chol.GetHashCode() || oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom))
                //    {
                //        if (fErechRechiv > fMichsaYomit)
                //            fErechRechiv = fMichsaYomit;
                //        //-	ימים א – ה וערב חג [זיהוי ערב חג]: הנמוך מבין (נוכחות מחושבת, מכסה יומית (רכיב 126)).
                //        if (fErechRechiv >= 480)
                //            if (fErechRechiv < fMichsaYomit)
                //                fErechRechiv = fMichsaYomit;
                //    }
                //    else if (oCalcBL.CheckYomShishi(objOved.SugYom))
                //    {
                //        // -	שישי: אם מכסה יומית (רכיב 126) > 0 אזי הנמוך מבין (נוכחות מחושבת, מכסה יומית (רכיב 126)).
                //        if (fMichsaYomit > 0)
                //        {
                //            if (fErechRechiv >= 480)
                //                fErechRechiv = fMichsaYomit;

                                //        }
                //        else
                //        {
                //            //אחרת: הנמוך מבין (נוכחות מחושבת, 390 שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 18 ). 
                //            fErechRechiv = Math.Min(fErechRechiv, int.Parse(drSidur["max_shaot_byom_shishi"].ToString()));
                //        }
                //    }
                //}
                else if (iMisparSidur == 99013)
                {
                    //•	סידור 99013 (8585) – שעות 100% בלבד
                    // הנמוך מבין (נוכחות מחושבת, מכסה יומית (רכיב 126) ).
                    if (fMichsaYomit > 0)
                        fErechRechiv = Math.Min(fErechRechiv, fMichsaYomit);
                    // else fErechRechiv = fErechRechiv;
                }
                else if (iMisparSidur == 99220)
                {
                    //•	סידור גרירה בפועל 99220 : ערך הרכיב = ערך רכיב זמן גרירות (רכיב 128) ברמת סידור 
                    fErechRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.ZmanGrirot.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                }
                else if (!string.IsNullOrEmpty(drSidur["dakot_n_letashlum_hol"].ToString()) && fMichsaYomit > 0)
                {
                    //	סידורים עם מאפיין לתשלום ביום חול :
                    // אם מכסה יומית מחושבת (רכיב 126) > 0 (בימי חול וערבי חג יש מכסה): ערך הרכיב = מכסה יומית מחושבת (רכיב 126) + ערך מאפיין 62 שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 62)

                    fErechRechiv = fMichsaYomit + int.Parse(drSidur["dakot_n_letashlum_hol"].ToString());
                }
                else if (!string.IsNullOrEmpty(drSidur["michsat_shishi_lebaley_x"].ToString()) && fMichsaYomit > 0 && objOved.SugYom == clGeneral.enSugYom.Shishi.GetHashCode())
                {
                    //	סידורים עם מאפיין לתשלום ביום שישי לעובדים בעלי מיכסה 
                    //אם מכסה יומית מחושבת (רכיב 126) > 0 וגם יום הוא שישי (לא ערב חג
                    //ערך הרכיב = מכסה יומית מחושבת (רכיב 126) + ערך מאפיין 63 שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 63)
                    fErechRechiv = fMichsaYomit + int.Parse(drSidur["michsat_shishi_lebaley_x"].ToString());
                }

                oPeilut.CalcRechiv1(iMisparSidur, dShatHatchalaSidur);

                fErechPeiluyot = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_PEILUT"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                fErechPeiluyot = fErechPeiluyot - (int.Parse(drSidur["Hafhatat_Nochechut_Visa"].ToString()) * 60);

                //•	עבור סידורי ויזה "תיירות פנים": 
                if (!string.IsNullOrEmpty(drSidur["sidur_namlak_visa"].ToString()))
                {
                    if (int.Parse(drSidur["sidur_namlak_visa"].ToString()) == 2 && (drSidur["sidur_rak_lechevrot_banot"].ToString() == "" || objOved.Taarich>=DateTime.Parse("01/05/2013")))
                    {
                        if (int.Parse(drSidur["yom_VISA"].ToString()) == 4)
                        {
                            //א
                            fErechRechiv = Math.Min(fErechRechiv, objOved.objParameters.iMaxNochehutVisaBodedet);
                            if (fErechRechiv == objOved.objParameters.iMaxNochehutVisaBodedet)
                            { fErechRechiv = fErechRechiv + fErechPeiluyot; }
                        }
                        else if (int.Parse(drSidur["yom_VISA"].ToString()) == 1)
                        {
                            //ב
                            dTempDtFrom = clGeneral.GetDateTimeFromStringHour("00:01", objOved.Taarich.Date);
                            dTempDtTo = clGeneral.GetDateTimeFromStringHour("13:59", objOved.Taarich.Date);

                            if (fMichsaYomit > 0 && DateTime.Parse(drSidur["shat_hatchala_letashlum"].ToString()) >= dTempDtFrom && DateTime.Parse(drSidur["shat_hatchala_letashlum"].ToString()) <= dTempDtTo)
                            {
                                fErechRechiv = objOved.objParameters.iNuchehutVisa1;
                                fErechRechiv = fErechRechiv + fErechPeiluyot;
                            }
                            //ג
                            dTempDtFrom = clGeneral.GetDateTimeFromStringHour("14:00", objOved.Taarich.Date);
                            dTempDtTo = clGeneral.GetDateTimeFromStringHour("24:00", objOved.Taarich.Date);

                            if (fMichsaYomit > 0 &&  DateTime.Parse(drSidur["shat_hatchala_letashlum"].ToString()) >= dTempDtFrom && DateTime.Parse(drSidur["shat_hatchala_letashlum"].ToString()) <= dTempDtTo)
                            {
                                fErechRechiv = Math.Min(objOved.objParameters.iMaxNuchehutVisaPnimRishon1, Math.Max(fErechRechiv, objOved.objParameters.iMinNuchehutVisaPnimRishon1));
                                if (fErechRechiv == objOved.objParameters.iMaxNuchehutVisaPnimRishon1)
                                { fErechRechiv = fErechRechiv + fErechPeiluyot; }
                            }

                            //ד
                            if (fMichsaYomit == 0 && objOved.SugYom == clGeneral.enSugYom.Shishi.GetHashCode())
                            {
                                fErechRechiv = Math.Min(objOved.objParameters.iMaxNuchehutVisaPnimRishon2, Math.Max(fErechRechiv, objOved.objParameters.iMinNuchehutVisaPnimRishon2));
                                if (fErechRechiv == objOved.objParameters.iMaxNuchehutVisaPnimRishon2)
                                { fErechRechiv = fErechRechiv + fErechPeiluyot; }
                            }
                            //ה
                            if (clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                            {
                                fErechRechiv = Math.Min(objOved.objParameters.iMaxNochehutVisaPnim, Math.Max(fErechRechiv, objOved.objParameters.iMinNochehutVisaPnim));
                                if (fErechRechiv == objOved.objParameters.iMaxNochehutVisaPnim)
                                { fErechRechiv = fErechRechiv + fErechPeiluyot; }
                            }
                        }
                        else if (int.Parse(drSidur["yom_VISA"].ToString()) == 2 && !clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich) && (objOved.SugYom == clGeneral.enSugYom.Chol.GetHashCode() || objOved.SugYom == clGeneral.enSugYom.Shishi.GetHashCode()))
                        {
                            //ו
                            fErechRechiv = objOved.objParameters.iNochehutVisaPnimNoShabaton;
                            fErechRechiv = fErechRechiv - (int.Parse(drSidur["Hafhatat_Nochechut_Visa"].ToString()) * 60);
                        }
                        else if (int.Parse(drSidur["yom_VISA"].ToString()) == 2 && clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                        {
                            //ז
                            fErechRechiv = Math.Min(objOved.objParameters.iMaxNochehutVisaPnimEmtzai, Math.Max(fErechRechiv, objOved.objParameters.iMinNochehutVisaPnimEmtzai));
                            fErechRechiv = fErechRechiv - (int.Parse(drSidur["Hafhatat_Nochechut_Visa"].ToString()) * 60);
                        }
                        else if (int.Parse(drSidur["yom_VISA"].ToString()) == 3 && !clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich) && (objOved.SugYom == clGeneral.enSugYom.Chol.GetHashCode() || objOved.SugYom == clGeneral.enSugYom.Shishi.GetHashCode()))
                        {
                           //ח
                            fErechRechiv = Math.Min(objOved.objParameters.iMaxNochehutVisaPnimAcharon, Math.Max(fErechRechiv, objOved.objParameters.iMinNochehutVisaPnimAcharon));
                            if (fErechRechiv == objOved.objParameters.iMaxNochehutVisaPnimAcharon)
                            { fErechRechiv = fErechRechiv + fErechPeiluyot; }
                        }
                        else if (int.Parse(drSidur["yom_VISA"].ToString()) == 3 && clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                        {
                            //ט
                            if (objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0  and TACHOGRAF=1", "shat_hatchala_sidur ASC").Length > 0)
                            {
                                shaa = DateTime.Parse(objOved.Taarich.ToShortDateString() + " 09:00:00");
                                if (dShatGmarLetashlum < shaa)
                                    dShatHatchalaLetashlum = shaa.AddHours(-5);
                                else dShatHatchalaLetashlum = shaa.AddHours(6);
                            }
                            fErechRechiv = float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());
                           
                            fErechRechiv = Math.Min(objOved.objParameters.iMaxNochehutVisaPnimEmtzai, Math.Max(fErechRechiv, objOved.objParameters.iMinNochehutVisaPnimAcharon));
                            if (fErechRechiv == objOved.objParameters.iMaxNochehutVisaPnimEmtzai)
                            { fErechRechiv = fErechRechiv + fErechPeiluyot; }
                        }

                    }
                    //עבור סידורי ויזה "צבאית"
                    else if (int.Parse(drSidur["sidur_namlak_visa"].ToString()) == 1)
                    {

                        if (int.Parse(drSidur["yom_VISA"].ToString()) == 1 || int.Parse(drSidur["yom_VISA"].ToString()) == 2)
                        {
                            dShatGmarLetashlum = DateTime.Parse(objOved.Taarich.ToShortDateString() + " 04:00:00").AddDays(1);
                        }
                        else if (int.Parse(drSidur["yom_VISA"].ToString()) == 3)
                        {
                            shaa= DateTime.Parse(objOved.Taarich.ToShortDateString() + " 04:00:00");
                            if (dShatHatchalaLetashlum > shaa)
                                dShatHatchalaLetashlum = DateTime.Parse(objOved.Taarich.ToShortDateString() + " 04:00:00");
                        }

                        sQury = "MISPAR_SIDUR=" + iMisparSidur + " and SHAT_HATCHALA_SIDUR=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and (SUBSTRING(makat_nesia,1,3)='790')";
                        fErech = oCalcBL.GetSumErechRechiv(objOved.DtPeiluyotYomi.Compute("sum(zmanElement)", sQury));
                        fErechRechiv = Math.Min(objOved.objParameters.iMaxNochehutVisaTzahalLoAcharon,
                            float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString()) - fErech);

                        if (fErechRechiv >= objOved.objParameters.iMaxNochehutVisaTzahalLoAcharon)
                        {
                            sQury = "MISPAR_SIDUR=" + iMisparSidur +" and SHAT_HATCHALA_SIDUR=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime')";
                            sQury += " and (SUBSTRING(makat_nesia,1,3) in (719,720,791,785))";
                            fErech = oCalcBL.GetSumErechRechiv(objOved.DtPeiluyotYomi.Compute("sum(zmanElement)", sQury));
                            fErechRechiv += fErech;
                        }
                    }
                }
                return fErechRechiv;
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, 0, objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcRechiv1BySidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv2()
        {
            //דקות נהגות בימי חול ( רכיב 2):
            // יש לבצע את החישוב עבור סידורים המוגדרים כסידורי נהגות – זיהוי סידורי נהיגה:
            //  ישנו מאפיין (קוד מאפיין = 3) לסוג סידור/סידור מיוחד המתאר את סוג העבודה   .
            //ערך מאפיין זה (סוג עבודה) = 5 = עבודת נהגות.
            int iMisparSidur, iSugSidur;
            bool bYeshSidur;
            int iDay , iMichutzLamichsa;
            float fErech;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            DataRow[] _drSidurMeyuchad, _drSidurRagil; ;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                //בדיקת סידורים רגילים
                // נלקחים הסידורים הרגילים שסקטור העבודה שלהם =נהגות
                _drSidurRagil = GetSidurimRegilim();
                if (_drSidurRagil.Length > 0)
                {
                    for (int I = 0; I < _drSidurRagil.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_sidur"].ToString());

                        //SetSugSidur(ref _drSidurRagil[I], objOved.Taarich, iMisparSidur);

                        iSugSidur = int.Parse(_drSidurRagil[I]["sug_sidur"].ToString());

                        bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nahagut.GetHashCode(), objOved.Taarich, iSugSidur);
                        if (bYeshSidur)
                        {

                            iDay = int.Parse(_drSidurRagil[I]["day_taarich"].ToString());
                          //  iSugYom = SugYom;
                            dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                            //fTosefetGrirotHatchala = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            //fTosefetGrirotSof = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                          //  dShatHatchalaLetashlum = dShatHatchalaLetashlum.AddMinutes(-fTosefetGrirotSof);
                            dShatGmarLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_gmar_letashlum"].ToString());
                          //  dShatGmarLetashlum = dShatGmarLetashlum.AddMinutes(+fTosefetGrirotHatchala);
                            iMichutzLamichsa = int.Parse(_drSidurRagil[I]["out_michsa"].ToString());

                            if (objOved.SugYom == clGeneral.enSugYom.Chol.GetHashCode() && (_drSidurRagil[I]["sidur_namlak_visa"].ToString().Trim() == "1" || _drSidurRagil[I]["sidur_namlak_visa"].ToString().Trim() == "2"))
                            {
                                fErech = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                addRowToTable(clGeneral.enRechivim.DakotNehigaChol.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech, iMichutzLamichsa);
                            }
                            else
                                CheckSidurCholToAdd(clGeneral.enRechivim.DakotNehigaChol.GetHashCode(), iMisparSidur, iDay, objOved.SugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false);

                        }
                    }

                }

                //בדיקת סידורים מיוחדים
                // נלקחים רק סידורים מיוחדים מסקטור עבודה =  נהגות
                _drSidurMeyuchad = GetSidurimMeyuchadim("sector_avoda", clGeneral.enSectorAvoda.Nahagut.GetHashCode());

                for (int I = 0; I <= _drSidurMeyuchad.Length - 1; I++)
                {
                    iMisparSidur = int.Parse(_drSidurMeyuchad[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_sidur"].ToString());

                    iDay = int.Parse(_drSidurMeyuchad[I]["day_taarich"].ToString());
                 //   iSugYom = SugYom;
                    dShatHatchalaLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_letashlum"].ToString());
                    //fTosefetGrirotHatchala = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    //fTosefetGrirotSof = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                   // dShatHatchalaLetashlum = dShatHatchalaLetashlum.AddMinutes(-fTosefetGrirotSof);
                    dShatGmarLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_gmar_letashlum"].ToString());
                   // dShatGmarLetashlum = dShatGmarLetashlum.AddMinutes(+fTosefetGrirotHatchala);
                    iMichutzLamichsa = int.Parse(_drSidurMeyuchad[I]["out_michsa"].ToString());

                    if (objOved.SugYom == clGeneral.enSugYom.Chol.GetHashCode() && (_drSidurMeyuchad[I]["sidur_namlak_visa"].ToString().Trim() =="1" || _drSidurMeyuchad[I]["sidur_namlak_visa"].ToString().Trim() == "2"))
                    {
                        fErech = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        addRowToTable(clGeneral.enRechivim.DakotNehigaChol.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech, iMichutzLamichsa);
                    }
                    else
                        CheckSidurCholToAdd(clGeneral.enRechivim.DakotNehigaChol.GetHashCode(), iMisparSidur, iDay, objOved.SugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotNehigaChol.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurMeyuchad = null;
                _drSidurRagil = null; ;
            }
        }

        public void CalcRechiv3()
        {
            //דקות בניהול תנועה בימי חול ( רכיב 3):
            // יש לבצע את החישוב עבור סידורים המוגדרים כסידורי נהגות – זיהוי סידורי נהיגה:
            //  ישנו מאפיין (קוד מאפיין = 3) לסוג סידור/סידור מיוחד המתאר את סוג העבודה   .
            //ערך מאפיין זה (סוג עבודה) = 4 =  ניהול תנועה.
            int iMisparSidur;
            bool bSidurNihulTnua = false;
            int iDay, iMichutzLamichsa;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            DataRow[] _drSidurim;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                //בדיקת סידורים רגילים
                // נלקחים הסידורים הרגילים שסקטור העבודה שלהם =ניהול תנועה
              
                //בדיקת סידורים מיוחדים
                // נלקחים רק סידורים מיוחדים מסקטור עבודה =  ניהול

                //סידור מיוחד עם מאפיין 101 (מטלה כללית ללא רכב) וקיים לפחות אלמנט אחד עם מאפיין 14 וערך = 4.     
                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0  and mispar_sidur is not null");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    bSidurNihulTnua = isSidurNihulTnua(_drSidurim[I]);
                    if (bSidurNihulTnua)
                    {
                        iDay = int.Parse(_drSidurim[I]["day_taarich"].ToString());
                        // iSugYom = SugYom;
                        dShatHatchalaLetashlum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                        dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                        iMichutzLamichsa = int.Parse(_drSidurim[I]["out_michsa"].ToString());
                        CheckSidurCholToAdd(clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode(), iMisparSidur, iDay, objOved.SugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public void CalcRechiv4()
        {
            //דקות בתפקיד בימי חול ( רכיב 4):
            // יש לבצע את החישוב עבור סידורים המוגדרים כסידורי נהגות – זיהוי סידורי נהיגה:
            //  ישנו מאפיין (קוד מאפיין = 3) לסוג סידור/סידור מיוחד המתאר את סוג העבודה   .
            //ערך מאפיין זה (סוג עבודה) = 1 =  תפקיד.
            int iMisparSidur;
            bool bSidurTafkid;
            float  fTosefetGrirotHatchala, fTosefetGrirotSof;
            int iDay, iMichutzLamichsa;
            DataRow[] _drSidurim;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                //בדיקת סידורים רגילים
                // נלקחים הסידורים הרגילים שסקטור העבודה שלהם =תפקיד


                //בדיקת סידורים מיוחדים
                // נלקחים רק סידורים מיוחדים מסקטור עבודה =  תפקיד

                //סידור מיוחד עם מאפיין 101 (מטלה כללית ללא רכב) וקיים לפחות אלמנט אחד עם מאפיין 14 וערך = 4.     
                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0  and mispar_sidur is not null");


                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                    bSidurTafkid = isSidurTafkid(_drSidurim[I]);
                    if (bSidurTafkid)
                    {
                        iDay = int.Parse(_drSidurim[I]["day_taarich"].ToString());
                        //  iSugYom = SugYom;

                        fTosefetGrirotHatchala = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        fTosefetGrirotSof = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                        dShatHatchalaLetashlum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                        dShatHatchalaLetashlum = dShatHatchalaLetashlum.AddMinutes(-fTosefetGrirotSof);
                        dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                        dShatGmarLetashlum = dShatGmarLetashlum.AddMinutes(+fTosefetGrirotHatchala);

                        iMichutzLamichsa = int.Parse(_drSidurim[I]["out_michsa"].ToString());
                        CheckSidurCholToAdd(clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), iMisparSidur, iDay, objOved.SugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, true);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: " + ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        //public void CalcRechiv5()
        //{
        //    // שעות היעדרות ( רכיב 5) 
        //    //החישוב ברמת סידור רלוונטי רק במקרה בו עובד מקבל היעדרות בחופש (ערך 40 במאפיין ביצוע 33) עבור סידור מיוחד (מס' סידור מתחיל ב- "99") שנחשב כהיעדרות מדווחת קיים ערך 1 במאפיין לסידור מיוחד קוד מאפיין = 53.
        //    int iMisparSidur;
        //    float fErech;
        //    DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
        //    DataRow[] _drSidurim;
        //    try
        //    {
        //        if (clCalcGeneral.objOved.objMeafyeneyOved.GetMeafyen(33).IntValue == 40)
        //        {
        //            _drSidurim = GetSidurimMeyuchadim("sidur_misug_headrut", 1);

        //            for (int I = 0; I < _drSidurim.Length; I++)
        //            {
        //                iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
        //                dShatHatchalaLetashlum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
        //                dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
        //                dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

        //               fErech=(dShatGmarLetashlum-dShatHatchalaLetashlum).TotalMinutes/60;
        //                addRowToTable(clGeneral.enRechivim.ShaotHeadrut.GetHashCode(), dShatHatchalaSidur,iMisparSidur,fErech );
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error Calc Sidur Rechiv 5: " + ex.StackTrace + "\n message: "+ ex.Message);
        //    }
        //}

        public void CalcRechiv7()
        {
          int iMisparSidur, iSugSidur;
            int iDay,  iMichutzLamichsa;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            DataRow[] drSidurim;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            bool bYeshSidur;
            try
            {

                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and hamarat_shabat=1 and zakay_lehamara<>2 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99");
                for (int I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                    iMichutzLamichsa = int.Parse(drSidurim[I]["out_michsa"].ToString());

                    iDay = int.Parse(drSidurim[I]["day_taarich"].ToString());
                    //  iSugYom = SugYom;
                    dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                    dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());
                    CheckSidurShabatToAdd(clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode(), iMisparSidur, iDay, objOved.SugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false);


                }
                drSidurim = null;
                drSidurim = GetSidurimRegilim();
                if (drSidurim.Length > 0)
                {
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        if (drSidurim[I]["hamarat_shabat"].ToString() == "1")
                        {
                            iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                            iMichutzLamichsa = int.Parse(drSidurim[I]["out_michsa"].ToString());
                            dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                            //SetSugSidur(ref drSidurim[I], objOved.Taarich, iMisparSidur);

                            iSugSidur = int.Parse(drSidurim[I]["sug_sidur"].ToString());

                            bYeshSidur = CheckSugSidur(clGeneral.enMeafyenim.HamaratShaot.GetHashCode(), 2, objOved.Taarich, iSugSidur);
                            if (!bYeshSidur)
                            {
                                iDay = int.Parse(drSidurim[I]["day_taarich"].ToString());
                                //  iSugYom = SugYom;
                                dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                                dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());
                                CheckSidurShabatToAdd(clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode(), iMisparSidur, iDay, objOved.SugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false);
                            }
                        }
                    }
                }


                //_drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null and  zakay_lehamara<>2 and Hamarat_shabat=1");

                //for (int I = 0; I < _drSidurim.Length; I++)
                //{
                //    bSidurZakay = false;
                //    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                //    if (iMisparSidur.ToString().Substring(0,2) =="99")
                //        bSidurZakay = int.Parse(_drSidurim[I]["zakay_lehamara"].ToString()) != 2;
                //    else{
                //        iSugSidur = int.Parse(_drSidurim[I]["sug_sidur"].ToString());
                //        bSidurZakay = CheckSugSidur(clGeneral.enMeafyenim.HamaratShaot.GetHashCode(), 2, objOved.Taarich, iSugSidur);
                //        bSidurZakay = !bSidurZakay;             
                //    }
                //    if (bSidurZakay)
                //    {
                //        iMichutzLamichsa = int.Parse(_drSidurim[I]["out_michsa"].ToString());
                //        dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                //        fZmanHafsaka = float.Parse(_drSidurim[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());
                //        iDay = int.Parse(_drSidurim[I]["day_taarich"].ToString());
                //        //    iSugYom = SugYom;
                //        dShatHatchalaLetashlum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                //        dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());

                //        CheckSidurShabatToAdd(clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode(), iMisparSidur, iDay, objOved.SugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false, fZmanHafsaka);
                //    }
                //}

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            //finally
            //{
            //    _drSidurim = null;
            //}
        }

        public void CalcRechiv10()
        {
            //  דקות לתוספת מיוחדת בתפקיד  - תמריץ (רכיב 10) 
            //יש לפתוח רשומה לרכיב ברמת הסידור רק עבור 
            //סידורים מזכים לתמריץ לפי [שליפת סידורים מיוחדים לרכיב (קוד רכיב=10)] 

            int iMisparSidur;
            int iDay, iMichutzLamichsa;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            DataRow[] _drSidurim, drPeiluyot;
            string sSidurim, sQury;
            // DataTable dtPeiluyot;
            Boolean bMezake;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {

                sSidurim = GetSidurimMeyuchRechiv(clGeneral.enRechivim.DakotTamritzTafkid.GetHashCode());
                if (sSidurim.Length > 0)
                {
                    _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and MISPAR_SIDUR IN(" + sSidurim + ")");

                    for (int I = 0; I < _drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                        iMichutzLamichsa = int.Parse(_drSidurim[I]["out_michsa"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                        iMichutzLamichsa = int.Parse(oCalcBL.CheckOutMichsa(objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, iMichutzLamichsa).GetHashCode().ToString());

                        iDay = int.Parse(_drSidurim[I]["day_taarich"].ToString());
                     //   iSugYom = SugYom;
                        dShatHatchalaLetashlum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                        dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());

                        bMezake = false;
                        if (iMisparSidur == 99400 || iMisparSidur == 99402)
                        {
                          //  //oPeilut.objOved.Taarich = objOved.Taarich;
                            //dtPeiluyot = oPeilut.GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);
                            //drPeiluyot = dtPeiluyot.Select("SUBSTRING(makat_nesia,1,1)=7 and sector_zvira_zman_haelement=6");
                            drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur, "(SUBSTRING(makat_nesia,1,1)=7 and sector_zvira_zman_haelement=6)");
                            if (drPeiluyot.Length > 0)
                            {
                                bMezake = true;
                            }
                        }
                        else
                        { bMezake = true; }

                        if (bMezake)
                        { CheckSidurCholToAdd(clGeneral.enRechivim.DakotTamritzTafkid.GetHashCode(), iMisparSidur, iDay, objOved.SugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false); }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotTamritzTafkid.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
                drPeiluyot = null;
            }
        }

        public void CalcRechiv12()
        {
            //  דקות לתוספת משק  (רכיב 12) 
            //  	יש לפתוח רשומות לרכיב זה רק אם העובד זכאי לקבל תמריץ: לפי [שליפת מאפיין ביצוע (קוד מאפיין = 45, מ.א., תאריך)] עם גדול מ-0 - עובד משק המזכה לתוספת משק.

            try
            {
                if (objOved.objMeafyeneyOved.GetMeafyen(45).Value.Trim() != "")
                {
                    if (int.Parse(objOved.objMeafyeneyOved.GetMeafyen(45).Value) > 0)
                    {
                        CalcDakotMachlif(clGeneral.enRechivim.DakotTosefetMeshek.GetHashCode());
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void CalcRechiv13()
        {
            //דקות מחליף משלח (רכיב 13
            //   מחליף משלח רק מי שעשה סידור  99210 (8516) . להבדיל ממשלח שבא ממפת הסידורים  ומאופיין בסוג סידור 60, 61, 62.
            try
            {
                CalcDakotMachlif(clGeneral.enRechivim.DakotMachlifMeshaleach.GetHashCode());
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void CalcRechiv14()
        {
            //דקות מחליף סדרן (רכיב 14
            //  מחליף סדרן רק מי שעשה סידור  99204 (8517) . להבדיל מסדרן שבא ממפת הסידורים  ומאופיין בסוג סידור 40, 41, 42.
            try
            {
                CalcDakotMachlif(clGeneral.enRechivim.DakotMachlifSadran.GetHashCode());
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void CalcRechiv15()
        {
            //דקות מחליף פקח (רכיב 15
            // מחליף פקח רק מי שעשה סידור  99205 (8576). להבדיל מפקח שבא ממפת הסידורים  ומאופיין בסוג סידור 50, 51, 52.

            try
            {
                CalcDakotMachlif(clGeneral.enRechivim.DakotMachlifPakach.GetHashCode());
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void CalcRechiv16()
        {
            //דקות מחליף קופאי (רכיב 16
            //מחליף קופאי רק מי שעשה סידור  99206 (8571). להבדיל מקופאי שבא ממפת הסידורים  ומאופיין בסוג סידור 70, 71, 72.

            try
            {
                CalcDakotMachlif(clGeneral.enRechivim.DakotMachlifKupai.GetHashCode());
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void CalcRechiv17()
        {
            //דקות מחליף רכז (רכיב 17
            // מחליף רכז רק מי שעשה סידור  99209 (8575). להבדיל מרכז שמאופיין לפי קוד עיסוק 402.

            try
            {
                CalcDakotMachlif(clGeneral.enRechivim.DakotMachlifRakaz.GetHashCode());
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void CalcRechiv18()
        {
            //דקות נוכחות בפועל(רכיב 18
            //שעת גמר של סידור פחות שעת התחלה של סידור.

            int iMisparSidur;
            float fErech;
            DateTime dShatGmarSidur, dShatHatchalaSidur;
            DataRow[] _drSidurim;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");
                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                    dShatGmarSidur = DateTime.Parse(_drSidurim[I]["shat_gmar_sidur"].ToString());

                   // fErech = float.Parse(_drSidurim[I]["ZMAN_LELO_HAFSAKA"].ToString()); //float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString());
                    fErech = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString());
                    fErech -= oPeilut.getZmanHafsakaBesidur(iMisparSidur, dShatHatchalaSidur);
                    addRowToTable(clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public void CalcRechiv22()
        {
            //כמות גמול חסכון (רכיב 22
            //יש לשלוף את רשימת הסידורים המיוחדים וסוגי הסידור הנחשבים לסידורים המזכים לגמול חיסכון כדלקמן:
            // סידורים מיוחדים לרכיב (קוד רכיב) המסתכמים לרכיב קוד רכיב = 22.
            // סוגי סידור לרכיב (קוד רכיב) המסתכמים לרכיב קוד רכיב = 22.
            string sSidurimMeyuchadim, sSugeySidur ;
            int iMisparSidur, iKodRechiv, iSugSidur;
            float fErech, fSumDakotRechiv;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            DataRow[] drSidurim, dr;
            iKodRechiv = clGeneral.enRechivim.KamutGmulChisachon.GetHashCode();
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
            
                sSidurimMeyuchadim = GetSidurimMeyuchRechiv(iKodRechiv);
                if (sSidurimMeyuchadim.Length > 0)
                {
                    //אם לעובד אין מאפיין ביצוע [שליפת מאפיין ביצוע (קוד מאפיין=60)]:
                    //  אם הסידור נשלף לרכיב 22 שליפת סידורים מיוחדים לרכיב (קוד רכיב) :
                    // ערך הרכיב = דקות נוכחות לתשלום (רכיב 1)

                    //. אם העובד בעל מאפיין ביצוע [שליפת מאפיין ביצוע (קוד מאפיין=60)] עם ערך כלשהו:
                    //     2. אם הסידור נשלף לרכיב 22 שליפת סידורים מיוחדים לרכיב (קוד רכיב) :
                     // ערך הרכיב = דקות נוכחות לתשלום (רכיב 1)

                    drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ")");
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                        fSumDakotRechiv = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fSumDakotRechiv);
                      
                        //dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                        //dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());
                        ////א.	סידור ועד עובדים 99008 – הסידור מוגדר כסידור מזכה, אולם יש לפתוח רכיב לסידור זה רק אם מתקיימים התנאים הבאים :
                        ////	אם קיים סידור זה לפחות ב- 6 ימים בחודש המחושב.
                        ////	Y = סכום הרכיב דקות נוכחות לתשלום (רכיב 1) לכל הסידורים 99008 (ועד עובדים) ביום אליו הם שייכים. 
                        ////אם ביום של הסידור הזה לא קיימים סידורים אחרים אזי יש לפתוח רשומה רכיב לסידור רק אם Y >=210 דקות
                        ////אחרת: יש לפתוח רשומה לרכיב לסידור זה רק אם Y >=300 דקות

                        //if (iMisparSidur == 99008)
                        //{
                        //    if (objOved.DtYemeyAvoda.Select("Lo_letashlum=0 and MISPAR_SIDUR=99008").Length >= 6)
                        //    {
                        //        fSumDakotRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        //        if (objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and MISPAR_SIDUR<>99008").Length == 0)
                        //        {
                        //            if (fSumDakotRechiv >= 210)
                        //            { addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fSumDakotRechiv); }
                        //        }
                        //        else
                        //        {
                        //            if (fSumDakotRechiv >= 300)
                        //            { addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fSumDakotRechiv); }
                        //        }
                        //    }
                        //}
                        //else
                        //{

                        //    dr = objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Select("MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')");
                        //    if (dr.Length > 0)
                        //        fErech = float.Parse(dr[0]["ERECH_RECHIV"].ToString());
                        //    else fErech = 0;
                        //    // fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());

                        //    addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fErech);
                        //}

                      
                       
                    }
                }

                sSugeySidur = GetSugeySidurRechiv(iKodRechiv);


                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        fSumDakotRechiv = 0;
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                        iSugSidur = int.Parse(drSidurim[I]["sug_sidur"].ToString());

                        //    שליפת סוגי סידור לרכיב (קוד רכיב) המסתכמים לרכיב קוד רכיב = 22.
                        // ערך הרכיב = דקות נוכחות לתשלום (רכיב 1)
                        if ((iMisparSidur.ToString().Substring(0, 2) != "99") && sSugeySidur.IndexOf("," + iSugSidur.ToString() + ",") > -1)
                        {
                            fSumDakotRechiv = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fSumDakotRechiv);
                        }

                        //אם העובד בעל מאפיין ביצוע [שליפת מאפיין ביצוע (קוד מאפיין=60)] עם ערך כלשהו:
                        if (objOved.objMeafyeneyOved.GetMeafyen(60).IntValue != 0 )
                        {
                            if (iMisparSidur.ToString().Substring(0, 2) == "99")
                            {
                                //אם הסידור הוא סידור מיוחד עם ערך 7 במאפיין 52 (קופאי):
                                    //  ערך הרכיב = דקות נוכחות לתשלום (רכיב 1)
                                 if (drSidurim[I]["SUG_AVODA"].ToString() == clGeneral.enSugAvoda.Kupai.GetHashCode().ToString())
                                {
                                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                }

                                //אם לסידור מאפיין 49 עם ערך כלשהו:  ערך הרכיב = דקות נוכחות לתשלום (רכיב 1)
                                 
                                 if (drSidurim[I]["zakaut_legmul_chisachon"].ToString() != "0")
                                    fSumDakotRechiv = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                           
                            }
                            //  //אם הסידור הוא סידור מפה עם ערך 7 במאפיין 52 (קופאי):
                            //  ערך הרכיב = דקות נוכחות לתשלום (רכיב 1)
                            else if (oCalcBL.CheckSugSidur(objOved, clGeneral.enMeafyen.SugAvoda.GetHashCode(), clGeneral.enSugAvoda.Kupai.GetHashCode(), iSugSidur))
                            {
                                fSumDakotRechiv = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            }
                        }

                        if (objOved.objPirteyOved.iMutamut == 1 || objOved.objPirteyOved.iMutamut == 3 || objOved.objPirteyOved.iMutamut == 5 || objOved.objPirteyOved.iMutamut == 7)
                        {
                            fSumDakotRechiv = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        }
                        if (fSumDakotRechiv > 0)
                            addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fSumDakotRechiv);

                    }

                
                //if (sSugeySidur.Length > 0)
                //{
                //    drSidurim = null;
                //    drSidurim = GetSidurimRegilim();
                //    for (int I = 0; I < drSidurim.Length; I++)
                //    {
                //        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                //        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                //        //SetSugSidur(ref drSidurim[I], objOved.Taarich, iMisparSidur);

                //        iSugSidur = int.Parse(drSidurim[I]["sug_sidur"].ToString());
                //        if (sSugeySidur.IndexOf("," + iSugSidur.ToString() + ",") > -1)
                //        {

                //            dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                //            dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());
                //            //fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());
                //            dr = null;
                //            dr = objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Select("MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')");
                //            if (dr.Length > 0)
                //                fErech = float.Parse(dr[0]["ERECH_RECHIV"].ToString());
                //            else fErech = 0;
                //            addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fErech);
                //        }
                //    }


                //}

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, iKodRechiv, objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
                dr = null;
            }
        }

        public void CalcRechiv23()
        {
            //דקות סיכון (רכיב 23) 

            int iMisparSidur;
            float fErech;
            DateTime dShatHatchalaSidur;
            DataRow[] _drSidurim;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
               // //oPeilut.objOved.Taarich = objOved.Taarich;

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");
                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                    oPeilut.CalcRechiv23(iMisparSidur, dShatHatchalaSidur);


                    fErech = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_PEILUT"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotSikun.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    if (fErech > 0)
                    {
                        addRowToTable(clGeneral.enRechivim.DakotSikun.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                    }
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotSikun.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null; 
            }
        }


        public void CalcRechiv26_2()
        {
            //דקות פרמיה שבת  (רכיב 26) 

            int iMisparSidur, iSugSidur;
            bool bYeshSidur;
            int iDay;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            DataRow[] _drSidurMeyuchad, _drSidurRagil;
            float fErechRechiv, fDakotNochehutSidur;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
              //  iSugYom = SugYom;

                //בדיקת סידורים רגילים
                // נלקחים הסידורים הרגילים שסקטור העבודה שלהם =נהגות
                _drSidurRagil = GetSidurimRegilim();
                if (_drSidurRagil.Length > 0)
                {
                    for (int I = 0; I < _drSidurRagil.Length; I++)
                    {
                        if (_drSidurRagil[I]["sidur_namlak_visa"].ToString() == "")
                        {
                            iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());
                            dShatHatchalaSidur = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_sidur"].ToString());

                            //SetSugSidur(ref _drSidurRagil[I], objOved.Taarich, iMisparSidur);

                            iSugSidur = int.Parse(_drSidurRagil[I]["sug_sidur"].ToString());
                            bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nahagut.GetHashCode(), objOved.Taarich, iSugSidur);
                            if (bYeshSidur)
                            {

                                iDay = int.Parse(_drSidurRagil[I]["day_taarich"].ToString());
                                dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                                dShatGmarLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_gmar_letashlum"].ToString());
                                fDakotNochehutSidur = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                fErechRechiv = 0;
                                //if (oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim,iSugYom) || oCalcBL.CheckYomShishi(iSugYom))
                                //{
                                //    if (dShatHatchalaLetashlum >= objOved.objParameters.dKnisatShabat)
                                //    {
                                //        fErechRechiv = fDakotNochehutSidur;
                                //    }
                                //}
                                //else if (clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, iSugYom, objOved.Taarich) && dShatHatchalaLetashlum <= objOved.objParameters.dEndShabat)
                                if (clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                                {
                                    fErechRechiv = fDakotNochehutSidur - oPeilut.getZmanHamtanaEilat(iMisparSidur, dShatHatchalaSidur); 
                                }

                                addRowToTable(clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);

                            }
                        }
                    }
                }

                //בדיקת סידורים מיוחדים
                // נלקחים רק סידורים מיוחדים מסקטור עבודה =  נהגות
                _drSidurMeyuchad = GetSidurimMeyuchadim("sector_avoda", clGeneral.enSectorAvoda.Nahagut.GetHashCode());

                for (int I = 0; I <= _drSidurMeyuchad.Length - 1; I++)
                {
                    fErechRechiv = 0;
                    if (_drSidurMeyuchad[I]["sidur_namlak_visa"].ToString() == "")
                    {
                        iMisparSidur = int.Parse(_drSidurMeyuchad[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_sidur"].ToString());

                        iDay = int.Parse(_drSidurMeyuchad[I]["day_taarich"].ToString());
                        dShatHatchalaLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_letashlum"].ToString());
                        dShatGmarLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_gmar_letashlum"].ToString());
                        fDakotNochehutSidur = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                        //if (oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim,iSugYom) || oCalcBL.CheckYomShishi(iSugYom))
                        //{
                        //    if (dShatHatchalaLetashlum >=objOved.objParameters.dKnisatShabat)
                        //    {
                        //        fErechRechiv = fDakotNochehutSidur;
                        //    }
                        //}
                        //else if (clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, iSugYom, objOved.Taarich) && dShatHatchalaLetashlum <= objOved.objParameters.dEndShabat)
                        if (clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                        {
                            fErechRechiv = fDakotNochehutSidur - oPeilut.getZmanHamtanaEilat(iMisparSidur,dShatHatchalaSidur);
                        }

                        addRowToTable(clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurMeyuchad= null;
                _drSidurRagil=null;
            }
        }


        public void CalcRechiv26()
        {
            //דקות פרמיה בשישי  (רכיב 202 ) :  

            int iMisparSidur, iSugSidur;
            bool bYeshSidur,bChishuv;
            DateTime dShatHatchalaSidur;
            DataRow[] _drSidurMeyuchad, _drSidurRagil;
            float fErechRechiv, fDakotNochehutSidur, fTosefetGil, fMichsaYomit, fNuchehutLepremia;
            float fDakotHagdara, fSumDakotSikun, fDakotHistaglut, fSachNesiot, fDakotLepremia, fDakotKisuyTor;

            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);

                //בדיקת סידורים רגילים
                // נלקחים הסידורים הרגילים שסקטור העבודה שלהם =נהגות
                _drSidurRagil = GetSidurimRegilim();
                if (_drSidurRagil.Length > 0)
                {
                    for (int I = 0; I < _drSidurRagil.Length; I++)
                    {
                        fErechRechiv = 0;
                        fTosefetGil = 0;
                        bChishuv = true;
                        if (_drSidurRagil[I]["sidur_namlak_visa"].ToString() == "")
                        {
                            iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());
                            iSugSidur = int.Parse(_drSidurRagil[I]["sug_sidur"].ToString());
                            bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nahagut.GetHashCode(), objOved.Taarich, iSugSidur);
                            if (bYeshSidur)
                            {
                                iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());
                                dShatHatchalaSidur = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_sidur"].ToString());
                                // dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                                if ( oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom) && dShatHatchalaSidur < objOved.objParameters.dKnisatShabat)
                                    bChishuv = false;
                               // if (dShatHatchalaSidur < objOved.objParameters.dKnisatShabat)
                                  //  bChishuv = false;
                                if (bChishuv)
                                {
                                    fDakotNochehutSidur = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                                    fNuchehutLepremia = fDakotNochehutSidur - oPeilut.getZmanHamtanaEilat(iMisparSidur, dShatHatchalaSidur);

                                    fDakotHagdara = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotHagdara.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')")) / float.Parse("0.75");
                                    fSumDakotSikun = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotSikun.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')")) / float.Parse("0.75");
                                    fDakotHistaglut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotHistaglut.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                    fDakotKisuyTor = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotKisuiTor.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                    fDakotLepremia = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.SachDakotLepremia.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                    fSachNesiot = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.SachNesiot.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                   
                                    if ((fDakotHagdara + fDakotLepremia) > 0)
                                    {
                                        fErechRechiv = float.Parse((fDakotHagdara + fSumDakotSikun + fDakotHistaglut + fDakotKisuyTor + fDakotLepremia + (fSachNesiot * objOved.objParameters.fElementZar) + fTosefetGil - fNuchehutLepremia).ToString());
                                    }
                                    addAnyRowToTable(clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);
                                }
                            }
                        }
                    }
                }

                //בדיקת סידורים מיוחדים
                // נלקחים רק סידורים מיוחדים מסקטור עבודה =  נהגות
                _drSidurMeyuchad = GetSidurimMeyuchadim("sector_avoda", clGeneral.enSectorAvoda.Nahagut.GetHashCode());

                for (int I = 0; I <= _drSidurMeyuchad.Length - 1; I++)
                {
                    fErechRechiv = 0;
                    fTosefetGil = 0;
                    bChishuv = true;
                    iMisparSidur = int.Parse(_drSidurMeyuchad[I]["mispar_sidur"].ToString());
                    if (iMisparSidur != 99220 && (string.IsNullOrEmpty(_drSidurMeyuchad[I]["sidur_namlak_visa"].ToString())))
                    {
                        dShatHatchalaSidur = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_sidur"].ToString());
                        //  dShatHatchalaLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_letashlum"].ToString());
                        if (oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom) && dShatHatchalaSidur < objOved.objParameters.dKnisatShabat)
                            bChishuv = false;
                        if (bChishuv)
                        {
                            fDakotNochehutSidur = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            fNuchehutLepremia = fDakotNochehutSidur - oPeilut.getZmanHamtanaEilat(iMisparSidur, dShatHatchalaSidur);

                            fDakotHagdara = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotHagdara.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')")) / float.Parse("0.75");
                            fSumDakotSikun = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotSikun.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')")) / float.Parse("0.75");
                            fDakotHistaglut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotHistaglut.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            fDakotKisuyTor = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotKisuiTor.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            fDakotLepremia = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.SachDakotLepremia.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            fSachNesiot = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.SachNesiot.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                            fErechRechiv = float.Parse((fDakotHagdara + fSumDakotSikun + fDakotHistaglut + fDakotKisuyTor + fDakotLepremia + (fSachNesiot * objOved.objParameters.fElementZar) + fTosefetGil - fNuchehutLepremia).ToString());

                            addAnyRowToTable(clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: " + ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurMeyuchad = null;
                _drSidurRagil = null;
            }
        }
        public void CalcRechiv28()
        {

            int iMisparSidur;
            float fErech216;
            DateTime dShatHatchalaSidur;
            DataRow[] _drSidurim;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            int iErechElementimReka;
            try
            {
                //oPeilut.objOved.Taarich = objOved.Taarich;

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");
                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                    iErechElementimReka = oPeilut.CalcElementReka(iMisparSidur, dShatHatchalaSidur);

                    fErech216 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and KOD_RECHIV=" + clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    if (fErech216 > 0)
                    {
                        addRowToTable(clGeneral.enRechivim.DakotPremiaVisa.GetHashCode(), dShatHatchalaSidur, iMisparSidur, float.Parse(iErechElementimReka.ToString()));
                    }
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotPremiaVisa.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }


        public void CalcRechiv29()
        {

            int iMisparSidur;
            float fErech216, fSumDakotRechiv;
            DateTime dShatHatchalaSidur;
            DataRow[] _drSidurim;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            int iErechElementimReka;
            try
            {
                //oPeilut.objOved.Taarich = objOved.Taarich;

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");
                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    fErech216 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and KOD_RECHIV=" + clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                    if (fErech216 > 0)
                    {
                        if (dShatHatchalaSidur >= objOved.objParameters.dKnisatShabat || clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                        {
                            iErechElementimReka = oPeilut.CalcElementReka(iMisparSidur, dShatHatchalaSidur);
                            fSumDakotRechiv = float.Parse(Math.Round((((iErechElementimReka / 1.2) + fErech216) / 50) * 60 * 0.33, MidpointRounding.AwayFromZero).ToString());
                            addRowToTable(clGeneral.enRechivim.DakotPremiaVisaShabat.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fSumDakotRechiv);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotPremiaVisaShabat.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public void CalcRechiv30_2(out string sMispareySidur)
        {
            //דקות פרמיה יומית  (רכיב 30) 

            int iMisparSidur, iSugSidur;
            bool bYeshSidur;
            int iDay;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            DataRow[] _drSidurMeyuchad, _drSidurRagil;
            float fErechRechiv, fDakotNochehutSidur;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
             //   iSugYom = SugYom;
                sMispareySidur = "";
                //בדיקת סידורים רגילים
                // נלקחים הסידורים הרגילים שסקטור העבודה שלהם =נהגות
                _drSidurRagil = GetSidurimRegilim();
                if (_drSidurRagil.Length > 0)
                {
                    for (int I = 0; I < _drSidurRagil.Length; I++)
                    {
                         if (_drSidurRagil[I]["sidur_namlak_visa"].ToString() == "")
                        {
                            iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());

                            //SetSugSidur(ref _drSidurRagil[I], objOved.Taarich, iMisparSidur);

                            iSugSidur = int.Parse(_drSidurRagil[I]["sug_sidur"].ToString());
                            bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nahagut.GetHashCode(), objOved.Taarich, iSugSidur);
                            if (bYeshSidur)
                            {
                                iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());
                                dShatHatchalaSidur = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_sidur"].ToString());

                                iDay = int.Parse(_drSidurRagil[I]["day_taarich"].ToString());
                                dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                                dShatGmarLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_gmar_letashlum"].ToString());
                                fDakotNochehutSidur = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                fErechRechiv = 0;
                                sMispareySidur += "," + iMisparSidur.ToString();
                                //if (oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim,iSugYom) || oCalcBL.CheckYomShishi(iSugYom))
                                //    {
                                //        if (dShatHatchalaLetashlum < objOved.objParameters.dKnisatShabat)
                                //        {
                                //            fErechRechiv = fDakotNochehutSidur;
                                //        }
                                //    }
                                //    else
                                //    {
                                fErechRechiv = fDakotNochehutSidur - oPeilut.getZmanHamtanaEilat(iMisparSidur,dShatHatchalaSidur);
                                //}

                                addRowToTable(clGeneral.enRechivim.DakotPremiaYomit.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);
                            }
                        }
                    }

                }

                //בדיקת סידורים מיוחדים
                // נלקחים רק סידורים מיוחדים מסקטור עבודה =  נהגות
                _drSidurMeyuchad = GetSidurimMeyuchadim("sector_avoda", clGeneral.enSectorAvoda.Nahagut.GetHashCode());

                for (int I = 0; I <= _drSidurMeyuchad.Length - 1; I++)
                {
                    fErechRechiv = 0;
                    if (_drSidurMeyuchad[I]["sidur_namlak_visa"].ToString() == "")
                    {
                        iMisparSidur = int.Parse(_drSidurMeyuchad[I]["mispar_sidur"].ToString());
                        if (iMisparSidur != 99500 && iMisparSidur != 99220 && (string.IsNullOrEmpty(_drSidurMeyuchad[I]["sidur_namlak_visa"].ToString())))
                        {
                            dShatHatchalaSidur = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_sidur"].ToString());

                            iDay = int.Parse(_drSidurMeyuchad[I]["day_taarich"].ToString());
                            dShatHatchalaLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_letashlum"].ToString());
                            dShatGmarLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_gmar_letashlum"].ToString());
                            fDakotNochehutSidur = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                            //if (oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim,iSugYom) || oCalcBL.CheckYomShishi(iSugYom))
                            //{
                            //    if (dShatHatchalaLetashlum < objOved.objParameters.dKnisatShabat)
                            //    {
                            //        fErechRechiv = fDakotNochehutSidur; 
                            //    }
                            //}
                            //else
                            //{
                            fErechRechiv = fDakotNochehutSidur - oPeilut.getZmanHamtanaEilat(iMisparSidur, dShatHatchalaSidur); ;
                            //}
                            sMispareySidur += "," + iMisparSidur.ToString();

                            addRowToTable(clGeneral.enRechivim.DakotPremiaYomit.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotPremiaYomit.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurMeyuchad = null;
                _drSidurRagil = null;
            }
        }


        public void CalcRechiv30()
        {
            //דקות פרמיה יומית  (רכיב 30) 

            int iMisparSidur, iSugSidur;
            bool bYeshSidur,bErevChag;
            DateTime dShatHatchalaLetashlum,dShatGmarLetashlum, dShatHatchalaSidur; 
            DataRow[] _drSidurMeyuchad, _drSidurRagil;
            float fErechRechiv, fDakotNochehutSidur,fTosefetGil,fMichsaYomit, fNuchehutLepremia;
            float fDakotHagdara,fSumDakotSikun, fDakotHistaglut, fSachNesiot, fDakotLepremia , fDakotKisuyTor;

            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                //   iSugYom = SugYom;
               //sMispareySidur = "";
               fMichsaYomit =oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);
               
                //בדיקת סידורים רגילים
                // נלקחים הסידורים הרגילים שסקטור העבודה שלהם =נהגות
                _drSidurRagil = GetSidurimRegilim();
                if (_drSidurRagil.Length > 0)
                {
                    for (int I = 0; I < _drSidurRagil.Length; I++)
                    {
                        fErechRechiv = 0;
                        fTosefetGil = 0;
                        bErevChag = false;
                        if (_drSidurRagil[I]["sidur_namlak_visa"].ToString() == "")
                        {
                            iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());
                            iSugSidur = int.Parse(_drSidurRagil[I]["sug_sidur"].ToString());
                            bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nahagut.GetHashCode(), objOved.Taarich, iSugSidur);
                            if (bYeshSidur)
                            {
                                iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());
                                dShatHatchalaSidur = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_sidur"].ToString());
                                dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                                dShatGmarLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_gmar_letashlum"].ToString());
                                if (oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom) && !oCalcBL.CheckYomShishi(objOved.SugYom))
                                {
                                    if (dShatHatchalaSidur >= objOved.objParameters.dKnisatShabat)
                                        bErevChag = true;
                                }
                                if (!bErevChag)
                                {
                                    fDakotNochehutSidur = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                    fTosefetGil = oCalcBL.ChishuvTosefetGil(objOved, fMichsaYomit, fDakotNochehutSidur, dShatGmarLetashlum);
                                    fNuchehutLepremia = fDakotNochehutSidur - oPeilut.getZmanHamtanaEilat(iMisparSidur, dShatHatchalaSidur);

                                    fDakotHagdara = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotHagdara.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')")) / float.Parse("0.75");
                                    fSumDakotSikun = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotSikun.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')")) / float.Parse("0.75");
                                    fDakotHistaglut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotHistaglut.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                    fDakotKisuyTor = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotKisuiTor.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                    fDakotLepremia = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.SachDakotLepremia.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                    fSachNesiot = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.SachNesiot.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                                    if ((fDakotHagdara + fDakotLepremia) > 0)
                                    {
                                        fErechRechiv = float.Parse((fDakotHagdara + fSumDakotSikun + fDakotHistaglut + fDakotKisuyTor + fDakotLepremia + (fSachNesiot * objOved.objParameters.fElementZar) + fTosefetGil - fNuchehutLepremia).ToString());
                                    }
                                    addAnyRowToTable(clGeneral.enRechivim.DakotPremiaYomit.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);
                                }
                            }
                        }
                    }

                }

                //בדיקת סידורים מיוחדים
                // נלקחים רק סידורים מיוחדים מסקטור עבודה =  נהגות
                _drSidurMeyuchad = GetSidurimMeyuchadim("sector_avoda", clGeneral.enSectorAvoda.Nahagut.GetHashCode());

                for (int I = 0; I <= _drSidurMeyuchad.Length - 1; I++)
                {
                    fErechRechiv = 0;
                    fTosefetGil = 0;
                    bErevChag = false;
                    iMisparSidur = int.Parse(_drSidurMeyuchad[I]["mispar_sidur"].ToString());
                    if (iMisparSidur != 99220 && (string.IsNullOrEmpty(_drSidurMeyuchad[I]["sidur_namlak_visa"].ToString())))
                    {
                        dShatHatchalaSidur = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_sidur"].ToString());
                        dShatHatchalaLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_letashlum"].ToString());
                       
                         if (oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom) && !oCalcBL.CheckYomShishi(objOved.SugYom))
                         {
                            if (dShatHatchalaSidur >= objOved.objParameters.dKnisatShabat)
                                bErevChag = true;
                         }
                         if (!bErevChag)
                         {
                             fDakotNochehutSidur = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                             fTosefetGil = oCalcBL.ChishuvTosefetGil(objOved, fMichsaYomit, fDakotNochehutSidur, dShatHatchalaLetashlum);
                             fNuchehutLepremia = fDakotNochehutSidur - oPeilut.getZmanHamtanaEilat(iMisparSidur, dShatHatchalaSidur);

                             fDakotHagdara = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotHagdara.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')")) / float.Parse("0.75");
                             fSumDakotSikun = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotSikun.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')")) / float.Parse("0.75");
                             fDakotHistaglut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotHistaglut.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                             fDakotKisuyTor = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotKisuiTor.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                             fDakotLepremia = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.SachDakotLepremia.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                             fSachNesiot = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.SachNesiot.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                             if ((fDakotHagdara + fDakotLepremia) > 0)
                             {
                                 fErechRechiv = float.Parse((fDakotHagdara + fSumDakotSikun + fDakotHistaglut + fDakotKisuyTor + fDakotLepremia + (fSachNesiot * objOved.objParameters.fElementZar) + fTosefetGil - fNuchehutLepremia).ToString());
                             }
                             addAnyRowToTable(clGeneral.enRechivim.DakotPremiaYomit.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);
                         }
                    }  
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotPremiaYomit.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: " + ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurMeyuchad = null;
                _drSidurRagil = null;
            }
        }
        public void CalcRechiv35()
        {
            //דקות בנהיגה בימי שבתון ( רכיב 35):
            // יש לבצע את החישוב עבור סידורים המוגדרים כסידורי נהגות – זיהוי סידורי נהיגה:
            //  ישנו מאפיין (קוד מאפיין = 3) לסוג סידור/סידור מיוחד המתאר את סוג העבודה   .
            //ערך מאפיין זה (סוג עבודה) = 5 = עבודת נהגות.
            int iMisparSidur, iSugSidur;
            bool bYeshSidur;
            int iDay, iMichutzLamichsa;
            float fErechRechiv; // fTosefetGrirotHatchala, fTosefetGrirotSof,
            DataRow[] _drSidurMeyuchad, _drSidurRagil;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                //סידורי ויזה
                //if (clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                //{
                    _drSidurMeyuchad = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and not sidur_namlak_visa is null");
                    for (int I = 0; I < _drSidurMeyuchad.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurMeyuchad[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_sidur"].ToString());
                        dShatGmarLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_gmar_letashlum"].ToString());

                        if (clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                        {
                            if (dShatGmarLetashlum <= objOved.objParameters.dShatMaavarYom)
                            {
                                fErechRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                addRowToTable(clGeneral.enRechivim.DakotNehigaShabat.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);
                            }
                        }
                        else if (oCalcBL.CheckYomShishi(objOved.SugYom) || oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom))
                        {
                            if (dShatHatchalaSidur >= objOved.objParameters.dKnisatShabat)
                            {
                                fErechRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                addRowToTable(clGeneral.enRechivim.DakotNehigaShabat.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);
                            }
                            else if (dShatHatchalaSidur < objOved.objParameters.dKnisatShabat && dShatGmarLetashlum > objOved.objParameters.dKnisatShabat)
                            {
                                fErechRechiv = float.Parse((dShatGmarLetashlum - objOved.objParameters.dKnisatShabat).TotalMinutes.ToString());
                                addRowToTable(clGeneral.enRechivim.DakotNehigaShabat.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);

                            }
                        }
                    }
                //}

                //בדיקת סידורים רגילים
                // נלקחים הסידורים הרגילים שסקטור העבודה שלהם =נהגות
                _drSidurRagil = GetSidurimRegilim();
                if (_drSidurRagil.Length > 0)
                {
                    for (int I = 0; I < _drSidurRagil.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());
                        iMichutzLamichsa = int.Parse(_drSidurRagil[I]["out_michsa"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_sidur"].ToString());

                        //SetSugSidur(ref _drSidurRagil[I], objOved.Taarich, iMisparSidur);

                        iSugSidur = int.Parse(_drSidurRagil[I]["sug_sidur"].ToString());
                        bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nahagut.GetHashCode(), objOved.Taarich, iSugSidur);
                        if (bYeshSidur)
                        {
                            iDay = int.Parse(_drSidurRagil[I]["day_taarich"].ToString());
                         //   iSugYom = SugYom;
                            dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                            //fTosefetGrirotHatchala = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            //fTosefetGrirotSof = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                           // dShatHatchalaLetashlum = dShatHatchalaLetashlum.AddMinutes(-fTosefetGrirotSof);
                            dShatGmarLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_gmar_letashlum"].ToString());
                            //dShatGmarLetashlum = dShatGmarLetashlum.AddMinutes(+fTosefetGrirotHatchala);
                          
                            CheckSidurShabatToAdd(clGeneral.enRechivim.DakotNehigaShabat.GetHashCode(), iMisparSidur, iDay, objOved.SugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false);


                            if (iSugSidur == 69)
                            {
                                fErechRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaShabat.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                                if (fErechRechiv > objOved.objParameters.iMinZmanGriraDarom)
                                {
                                    if (int.Parse(iMisparSidur.ToString().Substring(0, 2)) > 11)
                                    {
                                        fErechRechiv = objOved.objParameters.iMinZmanGriraDarom;
                                    }
                                    else
                                    {
                                        fErechRechiv = objOved.objParameters.iMinZmanGriraTzafon;
                                    }
                                }
                                else if (fErechRechiv > objOved.objParameters.iMinZmanGriraTzafon && fErechRechiv <= objOved.objParameters.iMinZmanGriraDarom)
                                {
                                    if (int.Parse(iMisparSidur.ToString().Substring(0, 2)) <= 11)
                                    {
                                        fErechRechiv = objOved.objParameters.iMinZmanGriraTzafon;
                                    }
                                }

                                addRowToTable(clGeneral.enRechivim.DakotNehigaShabat.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);

                            }

                        }

                    }
                }

                //בדיקת סידורים מיוחדים
                // נלקחים רק סידורים מיוחדים מסקטור עבודה =  נהגות
                _drSidurMeyuchad = GetSidurimMeyuchadim("sector_avoda", clGeneral.enSectorAvoda.Nahagut.GetHashCode());

                for (int I = 0; I < _drSidurMeyuchad.Length; I++)
                {
                    if (string.IsNullOrEmpty(_drSidurMeyuchad[I]["sidur_namlak_visa"].ToString()))
                    {
                        iMisparSidur = int.Parse(_drSidurMeyuchad[I]["mispar_sidur"].ToString());
                        iMichutzLamichsa = int.Parse(_drSidurMeyuchad[I]["out_michsa"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_sidur"].ToString());

                        iDay = int.Parse(_drSidurMeyuchad[I]["day_taarich"].ToString());
                        //   iSugYom = SugYom;
                        dShatHatchalaLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_letashlum"].ToString());
                        //fTosefetGrirotHatchala = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        //fTosefetGrirotSof = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        //dShatHatchalaLetashlum = dShatHatchalaLetashlum.AddMinutes(-fTosefetGrirotSof);
                        dShatGmarLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_gmar_letashlum"].ToString());
                        //dShatGmarLetashlum = dShatGmarLetashlum.AddMinutes(+fTosefetGrirotHatchala);

                        CheckSidurShabatToAdd(clGeneral.enRechivim.DakotNehigaShabat.GetHashCode(), iMisparSidur, iDay, objOved.SugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotNehigaShabat.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurMeyuchad = null;
                _drSidurRagil = null;
            }
        }

        public void CalcRechiv36()
        {
            //דקות בניהול תנועה בימי שבת ( רכיב 3):
            // יש לבצע את החישוב עבור סידורים המוגדרים כסידורי נהגות – זיהוי סידורי נהיגה:
            //  ישנו מאפיין (קוד מאפיין = 3) לסוג סידור/סידור מיוחד המתאר את סוג העבודה   .
            //ערך מאפיין זה (סוג עבודה) = 4 =  ניהול תנועה.
            int iMisparSidur;
            bool bSidurNihulTnua;
            int iDay,  iMichutzLamichsa;
            DataRow[] _drSidurim;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                //בדיקת סידורים רגילים
                // נלקחים הסידורים הרגילים שסקטור העבודה שלהם =ניהול תנועה
              
                //בדיקת סידורים מיוחדים
                // נלקחים רק סידורים מיוחדים מסקטור עבודה =  ניהול

                //סידור מיוחד עם מאפיין 101 (מטלה כללית ללא רכב) וקיים לפחות אלמנט אחד עם מאפיין 14 וערך = 4.     
                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0  and mispar_sidur is not null");
                
                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    iMichutzLamichsa = int.Parse(_drSidurim[I]["out_michsa"].ToString());

                    bSidurNihulTnua = isSidurNihulTnua(_drSidurim[I]);
                    if (bSidurNihulTnua)
                    {
                        dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                        iMichutzLamichsa = int.Parse(oCalcBL.CheckOutMichsa(objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, iMichutzLamichsa).GetHashCode().ToString());

                        iDay = int.Parse(_drSidurim[I]["day_taarich"].ToString());
                        // iSugYom = SugYom;
                        dShatHatchalaLetashlum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                        dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                        CheckSidurShabatToAdd(clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode(), iMisparSidur, iDay, objOved.SugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public void CalcRechiv37()
        {
            //דקות בתפקיד בשבתון ( רכיב 4):
            // יש לבצע את החישוב עבור סידורים המוגדרים כסידורי נהגות – זיהוי סידורי נהיגה:
            //  ישנו מאפיין (קוד מאפיין = 3) לסוג סידור/סידור מיוחד המתאר את סוג העבודה   .
            //ערך מאפיין זה (סוג עבודה) = 1 =  תפקיד.
            int iMisparSidur;
            bool bSidurTafkid, bCalc, bYeshSidur;
            float fTosefetGrirotHatchala, fTosefetGrirotSof;
            int iDay, iMichutzLamichsa, iSugYom;
            DataRow[] _drSidurim;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;

            try
            {
                //בדיקת סידורים רגילים
                // נלקחים הסידורים הרגילים שסקטור העבודה שלהם =תפקיד
              
                //בדיקת סידורים מיוחדים
                // נלקחים רק סידורים מיוחדים מסקטור עבודה =  תפקיד

                //סידור מיוחד עם מאפיין 101 (מטלה כללית ללא רכב) וקיים לפחות אלמנט אחד עם מאפיין 14 וערך = 4.     
                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0  and mispar_sidur is not null");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());

                    bYeshSidur = IsSidur100(iMisparSidur);
                    if (!bYeshSidur || iMisparSidur == 99006)
                    {
                        iMichutzLamichsa = int.Parse(_drSidurim[I]["out_michsa"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                        bSidurTafkid = isSidurTafkid(_drSidurim[I]);
                        if (bSidurTafkid)
                        {
                            iMichutzLamichsa = int.Parse(oCalcBL.CheckOutMichsa(objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, iMichutzLamichsa).GetHashCode().ToString());

                            iDay = int.Parse(_drSidurim[I]["day_taarich"].ToString());
                            iSugYom = objOved.SugYom;
                            //dShatHatchalaLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_letashlum"].ToString());
                            //dShatGmarLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_gmar_letashlum"].ToString());
                            fTosefetGrirotHatchala = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            fTosefetGrirotSof = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                            dShatHatchalaLetashlum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                            dShatHatchalaLetashlum = dShatHatchalaLetashlum.AddMinutes(-fTosefetGrirotSof);
                            dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                            dShatGmarLetashlum = dShatGmarLetashlum.AddMinutes(+fTosefetGrirotHatchala);

                            bCalc = true;
                            if (iMisparSidur == 99006)
                            {
                                if (objOved.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
                                {
                                    bCalc = false;
                                }
                                else
                                {
                                    if (oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, iSugYom) || oCalcBL.CheckYomShishi(iSugYom))
                                    {
                                        bCalc = false;
                                    }
                                    else if (clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, iSugYom, objOved.Taarich))
                                    {
                                        addRowToTable(clGeneral.enRechivim.DakotTafkidShabat.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 300);
                                        bCalc = false;
                                    }
                                }
                            }

                            if (bCalc)
                            {
                                CheckSidurShabatToAdd(clGeneral.enRechivim.DakotTafkidShabat.GetHashCode(), iMisparSidur, iDay, objOved.SugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotTafkidShabat.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public float GetSumMinutsBeforeShabat()
        {

            int iDay, iSugYom;
            DataRow[] _drSidurim;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchlaShabat;
            float fTempX;

            try
            {
                fTempX=0;
                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0  and mispar_sidur is not null");
                dShatHatchlaShabat = objOved.objParameters.dKnisatShabat;
                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    dShatHatchalaLetashlum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                    dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());

                    if (dShatHatchalaLetashlum < dShatHatchlaShabat && dShatGmarLetashlum > dShatHatchlaShabat)
                        fTempX += float.Parse((dShatHatchlaShabat - dShatHatchalaLetashlum).TotalMinutes.ToString());
                }
                return fTempX;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void CalcRechiv38()
        {
            //סה"כ לאשל בוקר (רכיב 38) 
            DataRow[] drSidurim;
            DateTime dShatHatchalaLetashlum, dShatHatchalaSidur, dShatGmarLetashlum;
            float fErech;
            int iMisparSidur;
            bool bSidurMezake = false;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");

                for (int I = 0; I < drSidurim.Length; I++)
                {
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());
                    dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());

                    bSidurMezake = ZihuyMezakeEshel(ref drSidurim[I]);

                    if (dShatHatchalaLetashlum >= objOved.objParameters.dStartSidurEshelBoker && dShatHatchalaLetashlum <= objOved.objParameters.dEndSidurEshelBoker && bSidurMezake)
                    {
                        fErech = 1;

                        addRowToTable(clGeneral.enRechivim.SachEshelBoker.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.SachEshelBoker.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        private bool ZihuyMezakeEshel(ref  DataRow drSidur)
        {
            bool bSidurMezake = false;
            DataRow[] drPeiluyot;
            // DataTable  dtPeiluyot;
            DataRow drDetailsPeilut;
            int iMisparSidur;
            float fKmPremia;
            int iSugSidur;
            string sSidurVisa, sQury;
            DateTime dShatHatchalaSidur;
            //לבדוק שלא ממשיך  בדיקות כשהגיע return

            iMisparSidur = int.Parse(drSidur["mispar_sidur"].ToString());
            sSidurVisa = drSidur["sidur_namlak_visa"].ToString();
            dShatHatchalaSidur = DateTime.Parse(drSidur["shat_hatchala_sidur"].ToString());

            //ב.	אם הסידור הינו סידור ויזה המזכה באש"ל. תיאור הבדיקה: זיהוי סידור ויזה לפי סידור מיוחד (מס' סידור מתחיל ב- "99") וגם שליפת מאפיין (מס' סידור מיוחד, קוד מאפיין = 45, תאריך) עם ערך כלשהו וגם רכיב סהכ ק"מ של הסידור (רכיב 215) >=  20 [שליפת פרמטר (קוד פרמטר = 33)].
            if (iMisparSidur.ToString().Substring(0, 2) == "99" && sSidurVisa != "")
            {
                fKmPremia = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.SachKM.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')")); ;
                if (fKmPremia >= objOved.objParameters.iKmMinTosafetEshel)
                {
                    return true;
                }
            }

            //ג.	קיים לפחות סידור מפה אחד (סידורים שמס' הסידור שלהם אינו מתחיל ב- "99") עבורו סוג סידור [שליפת נתונים מסידורים-מפה (מס' סידור,תאריך)]  = 02 או 07.
            if (iMisparSidur.ToString().Substring(0, 2) != "99")
            {
                //SetSugSidur(ref drSidur, dShatHatchalaSidur, iMisparSidur);

                iSugSidur = int.Parse(drSidur["sug_sidur"].ToString());
                if (iSugSidur == 2 || iSugSidur == 7)
                {
                    return true;
                }

            }

            //א.	קיימת לפחות פעילות אחת בסידור המזכה באש"ל. הבדיקה האם פעילות מזכה באשל: עבוור כל הפעילויות שבסידור שהמק"ט שלהן מתחיל ב- 0,1,2,3,4,8,9 יש לפנות לשירות [שליפת נתונים מקטלוג הנסיעות (מק"ט פעילות)] [פותח ע"י ורד לשגויים] ולשלוף את שדה "אש"ל" Eshel אם שדה זה = 1 או 3 אזי הפעילות מזכה באש"ל. 
            //oPeilut.objOved.Taarich = objOved.Taarich;
            //dtPeiluyot = oPeilut.GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);
            //drPeiluyot = dtPeiluyot.Select("SUBSTRING(makat_nesia,1,1) in(0,1,2,3,4,8,9)");
            drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur, "(SUBSTRING(makat_nesia,1,1) in(0,1,2,3,4,8,9))");
                          
            if (drPeiluyot.Length > 0)
            {
                for (int J = 0; J < drPeiluyot.Length; J++)
                {
                    drDetailsPeilut = oPeilut.GetDetailsFromCatalaog(objOved.Taarich, long.Parse(drPeiluyot[J]["MAKAT_NESIA"].ToString()));

                    if (drDetailsPeilut["ESHEL"].ToString() == "1" || drDetailsPeilut["ESHEL"].ToString() == "3")
                    {
                        bSidurMezake = true;
                        break;
                    }
                }
                if (bSidurMezake) { return true; }
            }


            //ד.	קיימת לפחות נסיעה ריקה אחת המזכה באש"ל. תיאור הבדיקה: נסיעות ריקות שדווחו ע"י הרישום הינן בעלות 3 ספרות ראשונות של המק"ט הוא אחד מתוך (791 , 792 , 744 , 785)  וגם [קמ] של נסיעה >=20 [שליפת פרמטר (קוד פרמטר = 33)].כאשר:  [קמ] = [חישוב קמ לפי זמן נסיעה (מק"ט פעילות)]
            //  drPeiluyot = dtPeiluyot.Select("SUBSTRING(makat_nesia,1,3) in(791,792,744,785)");
            drPeiluyot = null;
            drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur, "(SUBSTRING(makat_nesia,1,3) in(791,792,744,785))");
           
            if (drPeiluyot.Length > 0)
            {
                for (int J = 0; J < drPeiluyot.Length; J++)
                {
                    if (oCalcBL.CalcKm(long.Parse(drPeiluyot[0]["MAKAT_NESIA"].ToString()), objOved.objParameters.fGoremChishuvKm) >= objOved.objParameters.iKmMinTosafetEshel)
                    {
                        bSidurMezake = true;
                        break;
                    }

                }
                if (bSidurMezake) { return true; }
            }

            return bSidurMezake;
        }

        public void CalcRechiv40()
        {
            //סה"כ לאשל ערב (רכיב 40) 
            DataRow[] drSidurim;
            DateTime dShatHatchalaLetashlum, dShatHatchalaSidur, dShatGmarLetashlum;
            float fErech;
            int iMisparSidur;
            bool bSidurMezake;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");

                for (int I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());

                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                    dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());

                    bSidurMezake = ZihuyMezakeEshel(ref drSidurim[I]);

                    if (bSidurMezake && dShatGmarLetashlum >= objOved.objParameters.dStartSidurEshelErev)
                    {
                        dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                        fErech = 1;

                        addRowToTable(clGeneral.enRechivim.SachEshelErev.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.SachEshelErev.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        public void CalcRechiv42()
        {
            //סה"כ לאשל צהרים (רכיב 42
            DataRow[] drSidurim;
            DateTime dShatHatchalaLetashlum, dShatGmarSidur, dShatHatchalaSidur, dShatGmarLetashlum;
            float fErech;
            int iMisparSidur;
            bool bSidurMezake;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");

                for (int I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                    dShatGmarSidur = DateTime.Parse(drSidurim[I]["shat_gmar_sidur"].ToString());

                    dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());
                    dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());

                    bSidurMezake = ZihuyMezakeEshel(ref drSidurim[I]);

                    if (bSidurMezake && ((dShatHatchalaLetashlum <= objOved.objParameters.dStartSidurEshelTzaharayim && dShatGmarLetashlum >= objOved.objParameters.dEndSidurEshelTzaharayim) ||
                        (dShatGmarLetashlum >= objOved.objParameters.dStartSidurEshelTzaharayim && dShatGmarLetashlum <= objOved.objParameters.dEndSidurEshelTzaharayim) ||
                         (dShatHatchalaLetashlum >= objOved.objParameters.dStartSidurEshelTzaharayim && dShatHatchalaLetashlum <= objOved.objParameters.dEndSidurEshelTzaharayim)))
                    {
                        fErech = 1;

                        addRowToTable(clGeneral.enRechivim.SachEshelTzaharayim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.SachEshelTzaharayim.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        public void CalcRechiv39_41_43()
        {
            DataRow[] drSidurim;
            float fDakotNochehutSidur;
            int iMisparSidur;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            //סה"כ לאשל בוקר, צהרים וערב למבקרים בדרכים (רכיבים 39, 43, 41 ) 
            try
            {
                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");

                for (int I = 0; I < drSidurim.Length; I++)
                {
                    if (!string.IsNullOrEmpty(drSidurim[I]["KOD_SIBA_LEDIVUCH_YADANI_IN"].ToString()))
                    {
                        if ((int.Parse(drSidurim[I]["KOD_SIBA_LEDIVUCH_YADANI_IN"].ToString()) == 10 || int.Parse(drSidurim[I]["KOD_SIBA_LEDIVUCH_YADANI_OUT"].ToString()) == 10) && IsSidurShaon(drSidurim[I]))
                        {
                            iMisparSidur = int.Parse(drSidurim[I]["MISPAR_SIDUR"].ToString());
                            dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                            fDakotNochehutSidur = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                            dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());

                            //ב.	אם דקות נוכחות לתשלום (רכיב 1) של סידור > 0 וגם דקות נוכחות לתשלום (רכיב 1) של סידור < 240 דקות [שליפת פרמטר (קוד פרמטר = 65)] וגם שעת התחלה לתשלום של סידור < 04:01 [שליפת פרמטר (קוד פרמטר = 70)] וגם שעת גמר לתשלום של סידור > 04:59 [שליפת פרמטר (קוד פרמטר = 71)] אזי  רכיב אש"ל בוקר מבקרים (רכיב 39) עם ערך = 1. אין לפתוח רשומה לשאר הרכיבים (זכאי רק לאש"ל בוקר). 
                            if ((fDakotNochehutSidur > 0) && (fDakotNochehutSidur < objOved.objParameters.iNochehutMinRetzufa) &&
                                (dShatHatchalaLetashlum < objOved.objParameters.dHatchalaEshelBoker) &&
                                 (dShatGmarLetashlum > objOved.objParameters.dGmarEshelBoker))
                            {
                                addRowToTable(clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                            }

                            //ג.	אם דקות נוכחות לתשלום (רכיב 1) של סידור >= 240 דקות [שליפת פרמטר (קוד פרמטר = 65)] וגם דקות נוכחות לתשלום (רכיב 1) של סידור <= 479 דקות [שליפת פרמטר (קוד פרמטר = 66)] 
                            if ((fDakotNochehutSidur >= objOved.objParameters.iNochehutMinRetzufa) &&
                                (fDakotNochehutSidur <= objOved.objParameters.iNochehutMaxEshel1))
                            {
                                if ((dShatHatchalaLetashlum <= objOved.objParameters.dZmanHatchalaBoker) &&
                                (dShatGmarLetashlum < objOved.objParameters.dZmanSiyumBoker))
                                {
                                    addRowToTable(clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                }

                                if ((dShatHatchalaLetashlum <= objOved.objParameters.dZmanHatchalaBoker) &&
                               (dShatGmarLetashlum > objOved.objParameters.dZmanSiyumBoker))
                                {
                                    addRowToTable(clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                    addRowToTable(clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                }

                                if ((dShatHatchalaLetashlum > objOved.objParameters.dZmanHatchalaBoker) &&
                              (dShatHatchalaLetashlum <= objOved.objParameters.dZmanSiyumTzharayim) &&
                              (dShatGmarLetashlum > objOved.objParameters.dZmanSiyumBoker) &&
                              (dShatGmarLetashlum < objOved.objParameters.dZmanHatchalaErev))
                                {
                                    addRowToTable(clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                }

                                if ((dShatHatchalaLetashlum <= objOved.objParameters.dZmanSiyumTzharayim) &&
                             (dShatGmarLetashlum > objOved.objParameters.dZmanHatchalaErev))
                                {
                                    addRowToTable(clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                    addRowToTable(clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                }

                                if ((dShatHatchalaLetashlum > objOved.objParameters.dZmanSiyumTzharayim) &&
                            (dShatGmarLetashlum > objOved.objParameters.dZmanHatchalaErev))
                                {
                                    addRowToTable(clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                }
                            }

                            //ד.	אם דקות נוכחות לתשלום (רכיב 1) של סידור > 479 דקות [שליפת פרמטר (קוד פרמטר = 66)]   וגם דקות נוכחות לתשלום (רכיב 1) של סידור <= 599 דקות [שליפת פרמטר (קוד פרמטר = 67)]  
                            if ((fDakotNochehutSidur >= objOved.objParameters.iNochehutMaxEshel1) &&
                               (fDakotNochehutSidur <= objOved.objParameters.iNochehutMaxEshel))
                            {
                                if ((dShatHatchalaLetashlum >= objOved.objParameters.dZmanHatchalaBoker2) &&
                                (dShatGmarLetashlum < objOved.objParameters.dZmanSiyumBoker))
                                {
                                    addRowToTable(clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                }

                                if ((dShatHatchalaLetashlum <= objOved.objParameters.dZmanSiyumBoker) &&
                               (dShatGmarLetashlum > objOved.objParameters.dZmanSiyumBoker) && (dShatGmarLetashlum < objOved.objParameters.dZmanHatchalaErev))
                                {
                                    addRowToTable(clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                    addRowToTable(clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                }

                                if ((dShatHatchalaLetashlum > objOved.objParameters.dZmanSiyumBoker) &&
                             (dShatHatchalaLetashlum <= objOved.objParameters.dZmanSiyumTzharayim) &&
                             (dShatGmarLetashlum > objOved.objParameters.dZmanHatchalaErev))
                                {
                                    addRowToTable(clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                    addRowToTable(clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);

                                }


                                if ((dShatHatchalaLetashlum > objOved.objParameters.dZmanSiyumTzharayim) &&
                            (dShatGmarLetashlum > objOved.objParameters.dZmanHatchalaErev))
                                {
                                    addRowToTable(clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                }

                                if ((dShatHatchalaLetashlum <= objOved.objParameters.dZmanSiyumBoker) &&
                            (dShatGmarLetashlum > objOved.objParameters.dZmanHatchalaErev))
                                {
                                    addRowToTable(clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                    addRowToTable(clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                    addRowToTable(clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                }
                            }

                            //ה.	אם דקות נוכחות לתשלום (רכיב 1) של סידור >= 600 דקות  (10 שעות) [שליפת פרמטר (קוד פרמטר = 68)] 
                            if (fDakotNochehutSidur >= objOved.objParameters.iNochehueMinEshel)
                            {
                                if ((dShatHatchalaLetashlum >= objOved.objParameters.dZmanHatchalaBoker2) &&
                                (dShatGmarLetashlum < objOved.objParameters.dZmanSiyumBoker))
                                {
                                    addRowToTable(clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                    addRowToTable(clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                }

                                if ((dShatHatchalaLetashlum <= objOved.objParameters.dZmanSiyumBoker) &&
                               (dShatGmarLetashlum > objOved.objParameters.dZmanSiyumBoker) &&
                               (dShatGmarLetashlum < objOved.objParameters.dZmanSiyumTzharayim))
                                {
                                    addRowToTable(clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                    addRowToTable(clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                }

                                if ((dShatHatchalaLetashlum > objOved.objParameters.dZmanSiyumBoker) &&
                                    (dShatHatchalaLetashlum <= objOved.objParameters.dZmanSiyumTzharayim) &&
                              (dShatGmarLetashlum > objOved.objParameters.dZmanHatchalaErev))
                                {
                                    addRowToTable(clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                    addRowToTable(clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                }

                                if ((dShatHatchalaLetashlum <= objOved.objParameters.dZmanSiyumBoker) &&
                             (dShatGmarLetashlum > objOved.objParameters.dZmanSiyumTzharayim))
                                {
                                    addRowToTable(clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                    addRowToTable(clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                    addRowToTable(clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                }

                                if (dShatHatchalaLetashlum > objOved.objParameters.dZmanSiyumTzharayim)
                                {
                                    addRowToTable(clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        public void CalcRechiv44()
        {
            //כמות גמול חסכון נוספות (רכיב 44)
            int iMisparSidur, iKodRechiv, iSugSidur;
            float fDakotNochehutSidur;
            DataRow[] drSidurim;
            DateTime dShatHatchalaSidur;
            string sSidurimMeyuchadim, sSugeySidur;
            //  אם העובד בעל מאפיין ביצוע [שליפת מאפיין ביצוע (קוד מאפיין=60)] עם ערך = 2 אין משמעות לחישוב ברמת סידור עבור לחישוב ברמת יום.
            //אחרת, אם לעובד יש סידורים מזכים לגמול לפי:
            //יש לשלוף את רשימת הסידורים המיוחדים וסוגי הסידור הנחשבים לסידורים המזכים לגמול חיסכון כדלקמן:
            //שליפת סידורים מיוחדים לרכיב (קוד רכיב) המסתכמים לרכיב קוד רכיב = 44.
            //שליפת סוגי סידור לרכיב (קוד רכיב) המסתכמים לרכיב קוד רכיב = 44.
            //ערך הרכיב ברמת הסידור = ערך רכיב דקות נוכחות לתשלום (רכיב 1) של הסידור.
            iKodRechiv = clGeneral.enRechivim.KamutGmulChisachonNosafot.GetHashCode();
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                sSidurimMeyuchadim = GetSidurimMeyuchRechiv(iKodRechiv);

                if (sSidurimMeyuchadim.Length > 0)
                {
                    drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ")");

                    for (int I = 0; I < drSidurim.Length; I++)
                    {

                        iMisparSidur = int.Parse(drSidurim[I]["MISPAR_SIDUR"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                        fDakotNochehutSidur = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                        addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fDakotNochehutSidur);

                    }
                }

                sSugeySidur = GetSugeySidurRechiv(iKodRechiv);
                if (sSugeySidur.Length > 0)
                {
                    drSidurim = null;
                    drSidurim = GetSidurimRegilim();
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                        //SetSugSidur(ref drSidurim[I], objOved.Taarich, iMisparSidur);

                        iSugSidur = int.Parse(drSidurim[I]["sug_sidur"].ToString());

                        if (sSugeySidur.IndexOf("," + iSugSidur.ToString() + ",") > -1)
                        {
                            fDakotNochehutSidur = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                            addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fDakotNochehutSidur);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, iKodRechiv, objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        private void CalcDakotMachlif(int iKodRechiv)
        {
            string sSidurimMeyuchadim;
            DataRow[] drSidurim;
            int iMisparSidur;
            float fErech;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                sSidurimMeyuchadim = GetSidurimMeyuchRechiv(iKodRechiv);
                if (sSidurimMeyuchadim.Length > 0)
                {
                    drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ")");
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                        dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                        dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());

                        fErech = float.Parse(drSidurim[I]["ZMAN_LELO_HAFSAKA"].ToString()); //float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());
                      //  fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());

                        addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fErech);

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, iKodRechiv, objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        public void CalcRechiv52()
        {
            int iMisparSidur;
            float fErech, fDakotElementim;
            DateTime dShatHatchalaSidur;
            DataRow[] _drSidurim;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                //oPeilut.objOved.Taarich = objOved.Taarich;

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");
                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                    fDakotElementim = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_PEILUT"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotElementim.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                    fErech = fDakotElementim;

                    addRowToTable(clGeneral.enRechivim.SachDakotLepremia.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.SachDakotLepremia.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }


        public void CalcRechiv53()
        {
            DataRow[] drSidurim;
            int iMisparSidur, iSugSidur, iMichutzLamichsa, iDay;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            bool bYeshSidur;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {

                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and hamarat_shabat=1 and zakay_lehamara<>2 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99");
                for (int I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                    iMichutzLamichsa = int.Parse(drSidurim[I]["out_michsa"].ToString());

                    iDay = int.Parse(drSidurim[I]["day_taarich"].ToString());
                  //  iSugYom = SugYom;
                    dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                    dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());
                    CheckSidurShabatToAdd(clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode(), iMisparSidur, iDay, objOved.SugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false);


                }
                drSidurim = null;
                drSidurim = GetSidurimRegilim();
                if (drSidurim.Length > 0)
                {
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        if (drSidurim[I]["hamarat_shabat"].ToString() == "1")
                        {
                            iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                            iMichutzLamichsa = int.Parse(drSidurim[I]["out_michsa"].ToString());
                            dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                            //SetSugSidur(ref drSidurim[I], objOved.Taarich, iMisparSidur);

                            iSugSidur = int.Parse(drSidurim[I]["sug_sidur"].ToString());

                            bYeshSidur = CheckSugSidur(clGeneral.enMeafyenim.HamaratShaot.GetHashCode(), 2, objOved.Taarich, iSugSidur);
                            if (!bYeshSidur)
                            {
                                iDay = int.Parse(drSidurim[I]["day_taarich"].ToString());
                                //  iSugYom = SugYom;
                                dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                                dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());
                                CheckSidurShabatToAdd(clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode(), iMisparSidur, iDay, objOved.SugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enMeafyenim.HamaratShaot.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        public void CalcRechiv54()
        {
            DataRow[] drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur, dShatHatchalaLetashlum, dShatGmarLetashlum;
            float fErech, fHalbashaTchilatYom, fHalbashaSofYom;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            bool isHafsakaLast = false;
            DateTime shatHatchalaHafsakaLast = new DateTime();
            try
            {

                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");

                if (drSidurim.Length > 0)
                {
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        if (drSidurim[I]["ein_leshalem_tos_lila"].ToString() == "")
                        {
                            iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                            dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                            isHafsakaLast = oPeilut.CheckHafasakaLast(iMisparSidur, dShatHatchalaSidur, ref shatHatchalaHafsakaLast);
                          
                            fHalbashaTchilatYom = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.HalbashaTchilatYom.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            fHalbashaSofYom = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.HalbashaSofYom.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                            dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString()).AddMinutes(-fHalbashaTchilatYom);

                            if (isHafsakaLast)
                                dShatGmarLetashlum = shatHatchalaHafsakaLast.AddMinutes(fHalbashaSofYom);
                            else
                                dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString()).AddMinutes(fHalbashaSofYom);

                            fErech = 0;
                            if (dShatHatchalaLetashlum >= objOved.objParameters.dTchilatTosefetLaila && dShatGmarLetashlum <= objOved.objParameters.dSiyumTosefetLaila)
                            {
                                fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());
                            }
                            else if (dShatHatchalaLetashlum <= objOved.objParameters.dTchilatTosefetLaila && dShatGmarLetashlum <= objOved.objParameters.dSiyumTosefetLaila)
                            {
                                fErech = float.Parse((dShatGmarLetashlum - objOved.objParameters.dTchilatTosefetLaila).TotalMinutes.ToString());
                            }
                            else if (dShatHatchalaLetashlum >= objOved.objParameters.dTchilatTosefetLaila && dShatGmarLetashlum >= objOved.objParameters.dSiyumTosefetLaila)
                            {
                                fErech = float.Parse((objOved.objParameters.dSiyumTosefetLaila - dShatHatchalaLetashlum).TotalMinutes.ToString());
                            }
                            else if (dShatHatchalaLetashlum <= objOved.objParameters.dTchilatTosefetLaila && dShatGmarLetashlum >= objOved.objParameters.dTchilatTosefetLaila)
                            {
                                fErech = float.Parse((objOved.objParameters.dSiyumTosefetLaila - objOved.objParameters.dTchilatTosefetLaila).TotalMinutes.ToString());
                            }

                            if (fErech > 0)
                            {
                                HachsaratPeilut790LaylaEgged(iMisparSidur, dShatHatchalaSidur, ref fErech);
                                addRowToTable(clGeneral.enRechivim.ZmanLailaEgged.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.ZmanLailaEgged.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        private void HachsaratPeilut790LaylaEgged(int iMisparSidur, DateTime dShatHatchalaSidur,ref float fErechRechiv)
        {
            DataRow[] dr790;
            string sQury;
            DateTime shat_yetzia;
            int meshec790;
            try
            {
                sQury = "MISPAR_SIDUR=" + iMisparSidur + "  AND taarich=Convert('" + dShatHatchalaSidur.ToShortDateString() + "', 'System.DateTime') and ";
                sQury += "SHAT_HATCHALA_SIDUR=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and (SUBSTRING(makat_nesia,1,3)='790')";
                dr790 = objOved.DtPeiluyotYomi.Select(sQury, "shat_yetzia asc");

                if (dr790.Length > 0)
                {
                    shat_yetzia = DateTime.Parse(dr790[0]["shat_yetzia"].ToString());
                    meshec790 = int.Parse(dr790[0]["makat_nesia"].ToString().Substring(3, 3));
                    if (shat_yetzia >= objOved.objParameters.dTchilatTosefetLaila && shat_yetzia.AddMinutes(meshec790) <= objOved.objParameters.dTchilatTosefetLailaChok)
                        fErechRechiv -= meshec790;
                    else if (shat_yetzia < objOved.objParameters.dTchilatTosefetLaila && shat_yetzia.AddMinutes(meshec790) > objOved.objParameters.dTchilatTosefetLaila)
                        fErechRechiv -= int.Parse((shat_yetzia.AddMinutes(meshec790) - objOved.objParameters.dTchilatTosefetLaila).TotalMinutes.ToString());
                    else if (shat_yetzia < objOved.objParameters.dTchilatTosefetLailaChok && shat_yetzia.AddMinutes(meshec790) >= objOved.objParameters.dTchilatTosefetLailaChok)
                        fErechRechiv -= int.Parse((objOved.objParameters.dTchilatTosefetLailaChok - shat_yetzia).TotalMinutes.ToString());
               
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.ZmanLailaEgged.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: " + ex.Message, null);
                throw (ex);
            }
        }

       
        public void CalcRechiv55()
        {
            DataRow[] drSidurim;
            int iMisparSidur, iIsuk;
            DateTime dShatHatchalaSidur, dShatHatchalaLetashlum, dShatGmarLetashlum, dZmanSiyuomTosLila, dZmatTchilatTosLila;
            float fErech, fZmanLilaSidureyBoker;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            float fHalbashaTchilatYom, fHalbashaSofYom;
            dZmatTchilatTosLila = DateTime.MaxValue;
            dZmanSiyuomTosLila = DateTime.MaxValue;
            bool isHafsakaLast = false;
            DateTime shatHatchalaHafsakaLast = new DateTime();
            try
            {

                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");

                if (drSidurim.Length > 0)
                {
                    for (int I = 0; I < drSidurim.Length; I++)
                    {

                        if (drSidurim[I]["ein_leshalem_tos_lila"].ToString() == "")
                        {
                            iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                            dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                            isHafsakaLast = oPeilut.CheckHafasakaLast(iMisparSidur, dShatHatchalaSidur, ref shatHatchalaHafsakaLast);
                            fZmanLilaSidureyBoker = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_PEILUT"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.ZmanLilaSidureyBoker.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            //	אין להתייחס בחישוב לסידורים עבורם קיים רכיב 271
                            if (fZmanLilaSidureyBoker == 0)
                            {
                                fHalbashaTchilatYom = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.HalbashaTchilatYom.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                fHalbashaSofYom = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.HalbashaSofYom.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                                dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString()).AddMinutes(-fHalbashaTchilatYom);
                                if (isHafsakaLast)
                                    dShatGmarLetashlum = shatHatchalaHafsakaLast.AddMinutes(fHalbashaSofYom);
                                else
                                    dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString()).AddMinutes(fHalbashaSofYom);

                                fErech = 0;
                                if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30))
                                {
                                    dZmatTchilatTosLila = objOved.objParameters.dTchilatTosefetLailaChok;
                                    dZmanSiyuomTosLila = objOved.objParameters.dSiyumTosefetLailaChok;

                                    //אם סידור הינו סידור ויזה צבאית - סידור מיוחד בעל שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 45 ) עם ערך = 1 זהו יום אחרון של הוויזה - TB_Sidurim_Ovedim.Yom_Visa= 3 אזי יש לבצע את בדיקת זמן הסידור מול שעות לילה חוק לפי שעת התחלה של סידור TB_Sidurim_Ovedim. Shat_hatchala  ולא שעת התחלה לתשלום של סידור.
                                    if (drSidurim[I]["sidur_namlak_visa"].ToString() == "1" && drSidurim[I]["yom_visa"].ToString() == "3")
                                    {
                                        dShatHatchalaLetashlum = dShatHatchalaSidur;
                                    }

                                    iIsuk = objOved.objPirteyOved.iIsuk;
                                    iIsuk = objOved.objPirteyOved.iIsuk;
                                    if ((iIsuk == 122 || iIsuk == 123 || iIsuk == 124 || iIsuk == 127) && iMisparSidur == 99001 && clDefinitions.GetSugMishmeret(objOved.Mispar_ishi, objOved.Taarich, objOved.SugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, objOved.objParameters) == clGeneral.enSugMishmeret.Liyla.GetHashCode())
                                    {
                                        dZmanSiyuomTosLila = objOved.objParameters.dSiyumMishmeretLilaMafilim;
                                    }
                                    else
                                    {
                                        dZmanSiyuomTosLila = objOved.objParameters.dSiyumTosefetLailaChok;
                                    }

                                }
                                else if (objOved.SugYom < clGeneral.enSugYom.Shishi.GetHashCode())
                                {
                                    dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                                    if (isHafsakaLast)
                                        dShatGmarLetashlum = shatHatchalaHafsakaLast;
                                    else
                                        dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());

                                    if (objOved.objPirteyOved.iMikumYechida == 141)
                                    {
                                        dZmatTchilatTosLila = DateTime.Parse(objOved.Taarich.ToShortDateString() + " 21:00");
                                        dZmanSiyuomTosLila = objOved.objParameters.dSiyumTosLilaTaavura;
                                    }
                                    else
                                    {
                                        dZmatTchilatTosLila = objOved.objParameters.dTchilatTosLilaTaavura;
                                        dZmanSiyuomTosLila = objOved.objParameters.dSiyumTosLilaTaavura;
                                    }
                                }

                                if (dShatHatchalaLetashlum >= dZmatTchilatTosLila && dShatGmarLetashlum <= dZmanSiyuomTosLila)
                                {
                                    fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());
                                }
                                else if (dShatHatchalaLetashlum <= dZmatTchilatTosLila && dShatGmarLetashlum <= dZmanSiyuomTosLila && dShatGmarLetashlum >= dZmatTchilatTosLila)
                                {
                                    fErech = float.Parse((dShatGmarLetashlum - dZmatTchilatTosLila).TotalMinutes.ToString());
                                }
                                else if (dShatHatchalaLetashlum >= dZmatTchilatTosLila && dShatGmarLetashlum >= dZmanSiyuomTosLila && dShatHatchalaLetashlum < dZmanSiyuomTosLila)
                                {
                                    fErech = float.Parse((dZmanSiyuomTosLila - dShatHatchalaLetashlum).TotalMinutes.ToString());
                                }
                                else if (dShatHatchalaLetashlum <= dZmatTchilatTosLila && dShatGmarLetashlum >= dZmatTchilatTosLila)
                                {
                                    fErech = float.Parse((dZmanSiyuomTosLila - dZmatTchilatTosLila).TotalMinutes.ToString());
                                }
                                if (fErech > 0)
                                {
                                    HachsaratPeilut790LaylaChok(iMisparSidur, dShatHatchalaSidur, ref fErech);
                                    addRowToTable(clGeneral.enRechivim.ZmanLailaChok.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.ZmanLailaChok.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        private void HachsaratPeilut790LaylaChok(int iMisparSidur, DateTime dShatHatchalaSidur, ref float fErechRechiv)
        {
            DataRow[] dr790;
            string sQury;
            DateTime shat_yetzia;
            int meshec790;
            try
            {
                sQury = "MISPAR_SIDUR=" + iMisparSidur + "  AND taarich=Convert('" + dShatHatchalaSidur.ToShortDateString() + "', 'System.DateTime') and ";
                sQury += "SHAT_HATCHALA_SIDUR=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and (SUBSTRING(makat_nesia,1,3)='790')";
                dr790 = objOved.DtPeiluyotYomi.Select(sQury, "shat_yetzia asc");

                if (dr790.Length > 0)
                {
                    shat_yetzia = DateTime.Parse(dr790[0]["shat_yetzia"].ToString());
                    meshec790 = int.Parse(dr790[0]["makat_nesia"].ToString().Substring(3, 3));
                    if (shat_yetzia >= objOved.objParameters.dTchilatTosefetLailaChok && shat_yetzia.AddMinutes(meshec790) <= objOved.objParameters.dSiyumTosefetLailaChok)
                        fErechRechiv -= meshec790;
                    else if (shat_yetzia > objOved.objParameters.dTchilatTosefetLaila && shat_yetzia < objOved.objParameters.dTchilatTosefetLailaChok && shat_yetzia.AddMinutes(meshec790) > objOved.objParameters.dTchilatTosefetLailaChok)
                        fErechRechiv -= int.Parse((shat_yetzia.AddMinutes(meshec790) - objOved.objParameters.dTchilatTosefetLailaChok).TotalMinutes.ToString());
                    else if (shat_yetzia > objOved.objParameters.dTchilatTosefetLailaChok && shat_yetzia > objOved.Taarich &&  shat_yetzia.AddMinutes(meshec790) > objOved.objParameters.dSiyumTosefetLailaChok)
                        fErechRechiv -= int.Parse((objOved.objParameters.dSiyumTosefetLailaChok - shat_yetzia).TotalMinutes.ToString());

                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.ZmanLailaEgged.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: " + ex.Message, null);
                throw (ex);
            }
        }
        public float CalcRechiv62(out int iCount)
        {
            DataRow[] drSidurim;
            int iMisparSidur;
            float fErech;
            string sSidurimMeyuchadim;
            DateTime dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                fErech = 0;
                iCount = 0;
                sSidurimMeyuchadim = GetSidurimMeyuchRechiv(clGeneral.enRechivim.YomMiluim.GetHashCode());
                if (sSidurimMeyuchadim.Length > 0)
                {
                    drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ")");
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                        fErech += 1;
                        iCount += 1;
                    }
                }
                return fErech;
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.YomMiluim.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }

        }

        public float CalcRechiv63(out int iCount)
        {
            DataRow[] drSidurim;
            int iMisparSidur;
            float fErech;
            string sSidurimMeyuchadim;
            DateTime dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                fErech = 0;
                iCount = 0;
                sSidurimMeyuchadim = GetSidurimMeyuchRechiv(clGeneral.enRechivim.YomAvodaBechul.GetHashCode());
                if (sSidurimMeyuchadim.Length > 0)
                {
                    drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ")");
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                        fErech += 1;
                        iCount += 1;
                    }
                }
                return fErech;
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.YomAvodaBechul.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }

        }

        public float CalcRechiv67()
        {
            DataRow[] drSidurim;
            float fErech;
            string sSidurimMeyuchadim;
            DateTime dShatHatchalaSidur = DateTime.MinValue;
            int iMisparSidur = 0;
            try
            {
                fErech = 0;
                sSidurimMeyuchadim = GetSidurimMeyuchRechiv(clGeneral.enRechivim.YomChofesh.GetHashCode());
                if (sSidurimMeyuchadim.Length > 0)
                {
                    drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ")");
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                        if (!clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                        {
                            fErech += 1;
                        }
                    }
                }
                return fErech;
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.YomChofesh.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }

        }

        public void CalcRechiv80()
        {
            //דקות מחוץ למכסה תפקיד חול (רכיב 80): 
            float fErech;
            DataRow[] drSidurim;
            DateTime dShatHatchalaSidur = DateTime.MinValue;
            int iMisparSidur = 0;
            int iOutMichsa;
            try
            {
                //אם סידור מסומן מחוץ למכסה TB_Sidurim_Ovedim.Out_michsa = 1  וגם דקות תפקיד חול (רכיב 4) של סידור > 0 ערך הרכיב = דקות תפקיד חול (רכיב 4).
                //אחרת, אין לפתוח רשומה לרכיב זה ברמת סידור.

                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");
                for (int I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                    iOutMichsa = int.Parse(drSidurim[I]["out_michsa"].ToString());

                    if (oCalcBL.CheckOutMichsa(objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, iOutMichsa))
                    {
                        fErech = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidChol.GetHashCode().ToString() + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and mispar_sidur=" + iMisparSidur + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                        if (fErech > 0)
                        {

                            addRowToTable(clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        public void CalcRechiv81()
        {
            float fErech, fDakotMichutzTafkid, fDakotMichutzNihul;
            DateTime dShatHatchalaSidur = DateTime.MinValue;
            int iMisparSidur = 0;
            DataRow[] drSidurim;
            try
            {

                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");
                for (int I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                    fDakotMichutzTafkid = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaTafkidShabat.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    fDakotMichutzNihul = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaNihulShabat.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                    fErech = fDakotMichutzTafkid + fDakotMichutzNihul;

                    addRowToTable(clGeneral.enRechivim.DakotMichutzLamichsaShabat.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotMichutzLamichsaShabat.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        public void CalcRechiv85()
        {
            float fErech, fRetzifutNehiga, fRetzifutTafkid;
            DateTime dShatHatchalaSidur = DateTime.MinValue;
            int iMisparSidur = 0;
            DataRow[] drSidurim;
            try
            {

                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");
                for (int I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                    fRetzifutNehiga = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    fRetzifutTafkid = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                    fErech = fRetzifutNehiga + fRetzifutTafkid;

                    addRowToTable(clGeneral.enRechivim.SachTosefetRetzifut.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.SachTosefetRetzifut.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        public void CalcRechiv86()
        {
            float fErech;
            DateTime dShatHatchalaSidur, dShatGmarSidur, dShatHatchalaLetashlum, dShatGmarLetashlum;
            dShatHatchalaSidur = DateTime.MinValue;
            int iMisparSidur = 0;
            DataRow[] drSidurim;
            try
            {

                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");
                for (int I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());

                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                    dShatGmarSidur = DateTime.Parse(drSidurim[I]["shat_gmar_sidur"].ToString());
                    dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                    dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());

                    fErech = float.Parse((dShatHatchalaLetashlum - dShatHatchalaSidur).TotalMinutes.ToString()) + float.Parse((dShatGmarSidur - dShatGmarLetashlum).TotalMinutes.ToString());

                    addRowToTable(clGeneral.enRechivim.KizuzDakotHatchalaGmar.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.KizuzDakotHatchalaGmar.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        public void CalcRechiv88()
        {
            DataRow[] drSidurim;
            int iMisparSidur, iHashlama;
            float fErech, fTempX;
            DateTime dShatHatchalaSidur, dShatHatchalaLetashlum, dShatGmarLetashlum;
            DateTime dStartHafsaketZaharim, dEndHafsaketZaharim, dShaa;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            string sSidurim;
            bool flag;
            try
            {
                sSidurim = GetSidurimMeyuchRechiv(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode());
                if (sSidurim.Length > 0)
                {
                    drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0  AND MISPAR_SIDUR IN(" + sSidurim + ")");
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        flag = true;
                        fTempX = 0;

                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                        iHashlama = (drSidurim[I]["hashlama"] == System.DBNull.Value) ? 0 : int.Parse(drSidurim[I]["hashlama"].ToString());

                        //א.	אם מס' סידור TB_Sidurim_Ovedim.Mispar_sidur = 99006 (8554) – שליחות לחו"ל  אזי : אין לפתוח רשומה לרכיב לסידור זה.
                        if (iMisparSidur != 99006)
                        {
                            //אם הסידור הוא סידור תפקיד [זיהוי סידורי תפקיד (מס' סידור, תאריך)] וגם דקות נוכחות לתשלום (רכיב 1) > 0   וגם קוד השלמה Hashlama TB_Sidurim_Ovedim.= 0  אזי : בצע [חישוב], אחרת : אין לפתוח רשומה לרכיב לסידור זה.
                            if (CheckSidurBySectorAvoda(ref drSidurim[I], iMisparSidur, clGeneral.enSectorAvoda.Tafkid.GetHashCode()) && iHashlama == 0)
                            {
                                dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                                dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());

                                dShaa = DateTime.Parse(dShatHatchalaLetashlum.ToShortDateString() + " 18:00:00");

                                if (dShatHatchalaLetashlum >= dShaa.AddHours(-7) && dShatHatchalaLetashlum <= dShaa.AddHours(-1) && dShatGmarLetashlum > dShaa)
                                    flag= false;
                                
                                if(flag)
                                {
                                    if (objOved.objPirteyOved.iEzor == clGeneral.enEzor.Yerushalim.GetHashCode() || objOved.objPirteyOved.iMikumYechida == 180)
                                    {
                                        dStartHafsaketZaharim = objOved.objParameters.dStartAruchatTzaharayim246;
                                        dEndHafsaketZaharim = objOved.objParameters.dEndAruchatTzaharayim247;
                                    }
                                    else
                                    {
                                        dStartHafsaketZaharim = objOved.objParameters.dStartAruchatTzaharayim;
                                        dEndHafsaketZaharim = objOved.objParameters.dEndAruchatTzaharayim;
                                    }

                                    if ((dShatHatchalaLetashlum <= dEndHafsaketZaharim) && (dShatGmarLetashlum >= dStartHafsaketZaharim))
                                    {
                                        if ((dShatHatchalaLetashlum <= dStartHafsaketZaharim) && (dShatGmarLetashlum >= dStartHafsaketZaharim))
                                        {
                                            fTempX = float.Parse((dShatGmarLetashlum - dStartHafsaketZaharim).TotalMinutes.ToString());
                                        }
                                        else if ((dShatHatchalaLetashlum > dStartHafsaketZaharim) && (dShatHatchalaLetashlum < dEndHafsaketZaharim) && (dShatGmarLetashlum > dEndHafsaketZaharim))
                                        {
                                            fTempX = float.Parse((dEndHafsaketZaharim - dShatHatchalaLetashlum).TotalMinutes.ToString());
                                        }
                                        else if ((dShatHatchalaLetashlum > dStartHafsaketZaharim) && (dShatGmarLetashlum < dEndHafsaketZaharim))
                                        {
                                            fTempX = float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());

                                        }

                                        if (fTempX >= 18)
                                        {
                                            fErech = 18;
                                        }
                                        else
                                        {
                                            fErech = fTempX;
                                        }

                                        addRowToTable(clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                                    }
                                }
                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        public void CalcRechiv89()
        {
            DataRow[] drSidurim;
            int iMisparSidur;
            float fErech, fNochehutLetashlum, fNochehutBefoal;
            DateTime dShatHatchalaSidur;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0  and mispar_sidur is not null and (sidur_namlak_visa=1 or sidur_namlak_visa=2)");
                for (int I = 0; I < drSidurim.Length; I++)
                {

                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                    fNochehutLetashlum = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    fNochehutBefoal = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    fErech = fNochehutBefoal - fNochehutLetashlum;
                    addRowToTable(clGeneral.enRechivim.KizuzBevisa.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.KizuzBevisa.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        public void CalcRechiv92(float fMichsaYomit, float fNochehutLetashlum)
        {
            DataRow[] drSidurim;
            DateTime dShatHatchalaSidur, dShatGmarLetaslum, dShatHatchalaLetashlum, dTemp;
            int iMisparSidur;
            float fErech;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {

                if (fMichsaYomit > 0 && fNochehutLetashlum >= fMichsaYomit)
                {
                    drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0  and MISPAR_SIDUR=99001", "shat_hatchala_sidur ASC");
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                        dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["SHAT_HATCHALA_LETASHLUM"].ToString());

                        //if (dShatHatchalaLetashlum >= objOved.objMeafyeneyOved.ConvertMefyenShaotValid(objOved.Taarich, objOved.objMeafyeneyOved.GetMeafyen(25).Value))
                        //{
                        dShatGmarLetaslum = DateTime.Parse(drSidurim[I]["SHAT_GMAR_LETASHLUM"].ToString());
                        dTemp = objOved.objParameters.dTchlatTashlumTosefetLilaMafilim;
                        fErech = 0;
                        if (dShatHatchalaLetashlum < dTemp)
                        {
                            fErech = float.Parse((objOved.objParameters.dTchlatTashlumTosefetLilaMafilim - dShatHatchalaLetashlum).TotalMinutes.ToString());
                            //if (dShatHatchalaLetashlum < objOved.objParameters.dTchilatMishmeretLilaMafilim)
                            //    fErech = float.Parse((objOved.objParameters.dTchlatTashlumTosefetLilaMafilim - dShatHatchalaLetashlum).TotalMinutes.ToString());
                            //else
                            //    fErech = float.Parse((objOved.objParameters.dTchlatTashlumTosefetLilaMafilim - objOved.objParameters.dTchilatMishmeretLilaMafilim).TotalMinutes.ToString());
                        }

                        addRowToTable(clGeneral.enRechivim.Shaot50.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                        //}                                
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.Shaot50.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        //public void CalcRechiv94()
        //{
        //    DataRow[] drSidurim, drSidurimLeyom;
        //    int iMisparSidur;
        //    DataRow RowNext;
        //    DateTime dShatHatchalaSidur;
        //    float fErech, fDakotNochehut;
        //    dShatHatchalaSidur = DateTime.MinValue;
        //    iMisparSidur = 0;
        //    //אם קוד השלמה של סידור TB_Sidurim_Ovedim.Hashlama > 0 אזי
        //    //וגם בדיקה האם אושרה בקשה (קוד אישור = 38, מ.א., מס' סידור, תאריך) = 
        //    //X = הגבוה מבין [0, (קוד ההשלמה של הסידור * 60) פחות דקות נוכחות לתשלום (רכיב 1) של סידור] . אם 0 אין לפתוח רשומה לרכיב זה ברמת סידור 

        //    try {
        //        drSidurim = objOved.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and Hashlama>0 and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')");
        //        for (int I = 0; I < drSidurim.Length; I++)
        //        {
        //            iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
        //            dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
        //            if (oCalcBL.CheckUshraBakasha(clGeneral.enKodIshur.HashlamaLeshaot.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur))
        //            {
        //                fDakotNochehut = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

        //                fErech = Math.Max(0, (int.Parse(drSidurim[I]["Hashlama"].ToString()) * 60) - fDakotNochehut);

        //                //אם X > 0 יש לבדוק האם יש סידור עוקב לסידור עם ההשלמה אם כן, 
        //                //ערך הרכיב = הנמוך מבין (X , שעת התחלה לתשלום של הסידור העוקב פחות שעות גמר לתשלום של הסידור הנבדק)
        //                //אם אין סידור עוקב, ערך הרכיב = X

        //                if (fErech > 0)
        //                {
        //                    drSidurimLeyom = objOved.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur asc");
        //                    RowNext = drSidurimLeyom[0];
        //                    for (int J = 0; J < drSidurimLeyom.Length; J++)
        //                    {
        //                        RowNext = drSidurimLeyom[J];
        //                        if ((drSidurim[I]["mispar_sidur"].ToString() == drSidurimLeyom[J]["mispar_sidur"].ToString()) && (drSidurim[I]["shat_hatchala_sidur"].ToString() == drSidurimLeyom[J]["shat_hatchala_sidur"].ToString()))
        //                        {
        //                            if (J < (drSidurimLeyom.Length-1))
        //                            {
        //                                RowNext = drSidurimLeyom[J + 1];
        //                            }

        //                            break;
        //                        }
        //                    }
        //                    if ((drSidurim[I]["mispar_sidur"].ToString() != RowNext["mispar_sidur"].ToString()) && (drSidurim[I]["shat_hatchala_sidur"].ToString() != RowNext["shat_hatchala_sidur"].ToString()))
        //                    {
        //                        fErech = Math.Min(fErech,float.Parse((DateTime.Parse(RowNext["shat_hatchala_letashlum"].ToString())-DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString())).TotalMinutes.ToString()));
        //                    }
        //                    addRowToTable(clGeneral.enRechivim.ZmanHashlama.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
        //                }
        //            }                               
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.ZmanHashlama.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
        //        throw (ex);
        //    }
        //}

        public float CalcRechiv96(ref float fSumDakotLaylaEgged, ref float fSumDakotLaylaChok, ref float fSumDakotLaylaBoker , ref float fSumDakotShabat)
        {
            DataRow[] drSidurim;
            int iMisparSidur, J;
            DateTime dShatHatchalaSidur, dShatHatchalaLetashlum, dShatGmarLetashlum;
            float fErech, fErechSidur, fTempX, fSachDakotTafkid;
            float fErechLaylaEgged, fErechSidurLaylaEgged;
            float fErechLaylaChok, fErechSidurLaylaChok;
            float fErecBoker, fErechSidurBoker;
            float fErechShabat;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            bool bSidurNehigaOrNihulFirst = false;
            bool bSidurNehigaOrNihulSecond = false;
            bool bSidurNihulOrTafkidFirst = false;
            bool bSidurNihulOrTafkidSecond = false;

            try
            {
                fTempX = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode());

                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0  and mispar_sidur is not null", "shat_hatchala_sidur ASC");
                fErech = 0;
                fErechLaylaEgged = 0;
                fErechLaylaChok = 0;
                fErecBoker = 0;
                fErechShabat = 0;
                for (int I = 0; I < drSidurim.Length; I++)
                {
                    bSidurNehigaOrNihulFirst = false;
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());


                    bSidurNehigaOrNihulFirst = (isSidurNehiga(ref drSidurim[I]) || isSidurNihulTnua(drSidurim[I]));
                    bSidurNihulOrTafkidFirst = isSidurNihulTnuaOrTafkidMezakeRezifut(ref drSidurim[I]);

                    if (bSidurNehigaOrNihulFirst || bSidurNihulOrTafkidFirst)
                    {
                        J = I + 1;
                        if (J < drSidurim.Length)
                        {

                            bSidurNehigaOrNihulSecond = (isSidurNehiga(ref drSidurim[J]) || isSidurNihulTnua(drSidurim[J]));
                            bSidurNihulOrTafkidSecond = isSidurNihulTnuaOrTafkidMezakeRezifut(ref drSidurim[J]);

                            if ((bSidurNehigaOrNihulFirst && (bSidurNehigaOrNihulSecond || bSidurNihulOrTafkidSecond)) || (bSidurNehigaOrNihulSecond && (bSidurNehigaOrNihulFirst || bSidurNihulOrTafkidFirst)))
                            {
                                dShatHatchalaLetashlum = DateTime.Parse(drSidurim[J]["shat_hatchala_letashlum"].ToString());
                                dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());

                                fErechSidur = float.Parse((dShatHatchalaLetashlum - dShatGmarLetashlum).TotalMinutes.ToString());
                                fSachDakotTafkid = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotTafkid.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                                if (fErechSidur >= 1 && fErechSidur <= objOved.objParameters.iMinTimeBetweenSidurim)
                                {
                                    if (fSachDakotTafkid >= objOved.objParameters.iMinYomAvodaForHalbasha &&
                                        (drSidurim[I]["Mezake_Halbasha"].ToString() == "2" || drSidurim[I]["Mezake_Halbasha"].ToString() == "3" || drSidurim[J]["Mezake_Halbasha"].ToString() == "1" || drSidurim[J]["Mezake_Halbasha"].ToString() == "3"))
                                    {
                                        if (fErechSidur >= 10)
                                            fErechSidur = fErechSidur - 10;
                                        else fErechSidur = 0;
                                    }

                                    if ((fTempX + fErech) < objOved.objParameters.iMaxRetzifutChodshitLetashlum)
                                    {
                                        if ((fErech + fErechSidur + fTempX) > objOved.objParameters.iMaxRetzifutChodshitImGlisha)
                                            fErech += objOved.objParameters.iMaxRetzifutChodshitLetashlum - (fTempX + fErech);
                                        else fErech += fErechSidur;
                                    }
                                    else fErechSidur = 0;
                                    // fErech += fErechSidur;

                                    //רציפות לילה  
                                    if (fSachDakotTafkid >= objOved.objParameters.iMinYomAvodaForHalbasha)
                                    {
                                        if (drSidurim[I]["Mezake_Halbasha"].ToString() == "2" || drSidurim[I]["Mezake_Halbasha"].ToString() == "3")
                                            dShatGmarLetashlum = dShatGmarLetashlum.AddMinutes(10) < dShatHatchalaLetashlum ? dShatGmarLetashlum.AddMinutes(10) : dShatHatchalaLetashlum;
                                        if (drSidurim[J]["Mezake_Halbasha"].ToString() == "1" || drSidurim[J]["Mezake_Halbasha"].ToString() == "3")
                                            dShatHatchalaLetashlum = dShatHatchalaLetashlum.AddMinutes(-10) > dShatGmarLetashlum ? dShatHatchalaLetashlum.AddMinutes(-10) : dShatGmarLetashlum;
                                    }
                                    if (fErechSidur>0)
                                    {
                                        fErechSidurLaylaEgged = getDakotRezifutLayla(dShatHatchalaLetashlum, dShatGmarLetashlum, objOved.objParameters.dTchilatTosefetLaila, objOved.objParameters.dSiyumTosefetLaila);//9,10
                                        fErechSidurLaylaChok = getDakotRezifutLayla(dShatHatchalaLetashlum, dShatGmarLetashlum, objOved.objParameters.dSiyumTosefetLaila, objOved.objParameters.dSiyumTosefetLailaChok);//10,12
                                        fErechSidurBoker = getDakotRezifutBoker(dShatHatchalaLetashlum, dShatGmarLetashlum);

                                        fErechLaylaEgged += fErechSidurLaylaEgged;
                                        fErechLaylaChok += fErechSidurLaylaChok;
                                        fErecBoker += fErechSidurBoker;

                                        if (dShatGmarLetashlum >= objOved.objParameters.dKnisatShabat)
                                            fErechShabat += float.Parse((dShatHatchalaLetashlum - dShatGmarLetashlum).TotalMinutes.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
                fSumDakotLaylaEgged = fErechLaylaEgged;
                fSumDakotLaylaChok = fErechLaylaChok;
                fSumDakotLaylaBoker = fErecBoker;
                fSumDakotShabat = fErechShabat;
                return fErech;
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        private bool isSidurNehiga(ref DataRow drSidurim)
        {
            int iMisparSidur = 0;
            bool bSidurNehiga = false;
            bool bYeshSidur = false;
            int iSugSidur;
            DateTime dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                iMisparSidur = int.Parse(drSidurim["mispar_sidur"].ToString());
                dShatHatchalaSidur = DateTime.Parse(drSidurim["shat_hatchala_sidur"].ToString());

                if (iMisparSidur.ToString().Substring(0, 2) == "99" && drSidurim["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString())
                    bSidurNehiga = true;
                else if (iMisparSidur.ToString().Substring(0, 2) != "99")
                {
                    //SetSugSidur(ref drSidurim, objOved.Taarich, iMisparSidur);
                    iSugSidur = int.Parse(drSidurim["sug_sidur"].ToString());

                    bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nahagut.GetHashCode(), objOved.Taarich, iSugSidur);
                    if (bYeshSidur)
                        bSidurNehiga = true;
                }
                return bSidurNehiga;
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, 0, objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
        }
        private bool isSidurNihulTnuaOrTafkidMezakeRezifut(ref DataRow drSidurim)
        {
            int iMisparSidur = 0;
            bool bSidurNihulOrTafkid = false;
            //bool bYeshSidur = false;
            //int iSugSidur;
            //string sQury = "";
            //DataRow[] drPeiluyot;
            // DataTable dtPeiluyot;
            DateTime dShatHatchalaSidur = DateTime.MinValue;
            int zakay = 0;
            try
            {
                iMisparSidur = int.Parse(drSidurim["mispar_sidur"].ToString());
                dShatHatchalaSidur = DateTime.Parse(drSidurim["shat_hatchala_sidur"].ToString());
                if (drSidurim["zakay_lechishuv_retzifut"].ToString() != "")
                    zakay = int.Parse(drSidurim["zakay_lechishuv_retzifut"].ToString());

                bSidurNihulOrTafkid = isSidurNihulTnua(drSidurim);

                if (!bSidurNihulOrTafkid)
                    if (iMisparSidur.ToString().Substring(0, 2) == "99" && zakay == 1)
                      bSidurNihulOrTafkid = true;
                //else if (iMisparSidur.ToString().Substring(0, 2) != "99")
                //{
                //    //SetSugSidur(ref drSidurim, objOved.Taarich, iMisparSidur);
                //    iSugSidur = int.Parse(drSidurim["sug_sidur"].ToString());

                //    bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nihul.GetHashCode(), objOved.Taarich, iSugSidur);
                //    if (bYeshSidur)
                //        bSidurNihulOrTafkid = true;
                //    else
                //    {
                //        //dtPeiluyot = oPeilut.GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);
                //        //drPeiluyot = dtPeiluyot.Select("sector_zvira_zman_haelement=4");
 
                //        drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur, "(sector_zvira_zman_haelement=4)");
           
                //        bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nihul.GetHashCode(), objOved.Taarich, iSugSidur);
                //        if (bYeshSidur || (iMisparSidur == 99301 && drPeiluyot.Length > 0)) // && clCalcGeneral.objOved.objMeafyeneyOved.GetMeafyen(33).IntValue))
                //            bSidurNihulOrTafkid = true;

                //    }
                //}
                return bSidurNihulOrTafkid;
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, 0, objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            //finally
            //{
            //    drPeiluyot = null;
            //}
        }

        private bool isSidurNihulTnua(DataRow drSidurim)
        {
            int iMisparSidur = 0;
            bool bSidurNihulTnua = false;
            bool bYeshSidur = false;
            int iSugSidur;
            DataRow[] drPeiluyot;
            DateTime dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                iMisparSidur = int.Parse(drSidurim["mispar_sidur"].ToString());
                dShatHatchalaSidur = DateTime.Parse(drSidurim["shat_hatchala_sidur"].ToString());
              
                if (iMisparSidur.ToString().Substring(0, 2) == "99" && drSidurim["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nihul.GetHashCode().ToString())
                    bSidurNihulTnua = true;
                else 
                {
                    iSugSidur = int.Parse(drSidurim["sug_sidur"].ToString());
                    //SetSugSidur(ref drSidurim, objOved.Taarich, iMisparSidur);
                    if (iMisparSidur.ToString().Substring(0, 2) != "99")
                    {
                        bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nihul.GetHashCode(), objOved.Taarich, iSugSidur);
                        if (bYeshSidur)
                            bSidurNihulTnua = true;
                    }
                    else
                    {
                        drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur, "(sector_zvira_zman_haelement=4)");
                        
                     //   bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nihul.GetHashCode(), objOved.Taarich, iSugSidur);
                     // bYeshSidur || 
                        if (drSidurim["matala_klalit_lelo_rechev"].ToString()!="" && drPeiluyot.Length > 0) // && clCalcGeneral.objOved.objMeafyeneyOved.GetMeafyen(33).IntValue))
                            bSidurNihulTnua = true;
                    }
                }
                return bSidurNihulTnua;
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, 0, objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: isSidurNihulTnua-" + ex.StackTrace + "\n message: " + ex.Message, null);
                throw (ex);
            }
            finally
            {
                drPeiluyot = null;
            }
        }

        private bool isSidurTafkid(DataRow drSidurim)
        {
            int iMisparSidur = 0;
            bool bSidurTafkid = false;
            bool bYeshSidur = false;
            int iSugSidur;
            DataRow[] drPeiluyot;
            DateTime dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                iMisparSidur = int.Parse(drSidurim["mispar_sidur"].ToString());
                dShatHatchalaSidur = DateTime.Parse(drSidurim["shat_hatchala_sidur"].ToString());

                if (iMisparSidur.ToString().Substring(0, 2) == "99" && drSidurim["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Tafkid.GetHashCode().ToString())
                    bSidurTafkid = true;
                else
                {
                    iSugSidur = int.Parse(drSidurim["sug_sidur"].ToString());
                    //SetSugSidur(ref drSidurim, objOved.Taarich, iMisparSidur);
                    if (iMisparSidur.ToString().Substring(0, 2) != "99")
                    {
                        bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Tafkid.GetHashCode(), objOved.Taarich, iSugSidur);
                        if (bYeshSidur)
                            bSidurTafkid = true;
                    }
                    else
                    {
                        drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur, "(sector_zvira_zman_haelement=1)");

                        //   bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nihul.GetHashCode(), objOved.Taarich, iSugSidur);
                        // bYeshSidur || 
                        if (drSidurim["matala_klalit_lelo_rechev"].ToString() != "" && drPeiluyot.Length > 0) // && clCalcGeneral.objOved.objMeafyeneyOved.GetMeafyen(33).IntValue))
                            bSidurTafkid = true;
                    }
                }
                return bSidurTafkid;
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, 0, objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: isSidurTafkid-" + ex.StackTrace + "\n message: " + ex.Message, null);
                throw (ex);
            }
            finally
            {
                drPeiluyot = null;
            }
        }

        public float CalcRechiv97(ref float fSumDakotLaylaEgged, ref float fSumDakotLaylaChok)
        {
            DataRow[] drSidurim, drPeiluyot;
            int iMisparSidur, J, iMisparSidurNext;
            DateTime dShatHatchalaSidur, dShatHatchalaLetashlum, dShatGmarLetashlum;
            float fErech, fErechSidur;
            float fErechLaylaEgged, fErechSidurLaylaEgged;
            float fErechLaylaChok, fErechSidurLaylaChok;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            bool bSidurNehiga = false;
            bool bSidurNihulOrTafkid = false;
            bool bSidurMezake = false;
            int iSugSidur;
            string sQury = "";
            bool bYeshSidur = false;

            //  DataTable dtPeiluyot;
            try
            {
                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0  and mispar_sidur is not null", "shat_hatchala_sidur ASC");
                fErech = 0;
                fErechLaylaEgged = 0;
                fErechLaylaChok = 0;
                for (int I = 0; I < drSidurim.Length; I++)
                {
                    bSidurNehiga = false;
                    bSidurNihulOrTafkid = false;
                    bSidurMezake = false;
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    if (iMisparSidur.ToString().Substring(0, 2) == "99" && drSidurim[I]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString())
                        bSidurNehiga = true;
                    else if (iMisparSidur.ToString().Substring(0, 2) == "99" && (drSidurim[I]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nihul.GetHashCode().ToString() || int.Parse(drSidurim[I]["zakay_lepizul"].ToString()) == 1))
                        bSidurNihulOrTafkid = true;
                    else if (iMisparSidur.ToString().Substring(0, 2) != "99")
                    {
                        //SetSugSidur(ref drSidurim[I], objOved.Taarich, iMisparSidur);
                        iSugSidur = int.Parse(drSidurim[I]["sug_sidur"].ToString());

                        bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nahagut.GetHashCode(), objOved.Taarich, iSugSidur);
                        if (bYeshSidur)
                            bSidurNehiga = true;
                        else
                        {
                            //dtPeiluyot = oPeilut.GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);
                            //drPeiluyot = dtPeiluyot.Select("sector_zvira_zman_haelement=4");
 
                            drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur, "(sector_zvira_zman_haelement=4)");
           
                            bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nihul.GetHashCode(), objOved.Taarich, iSugSidur);
                            if (bYeshSidur || (iMisparSidur == 99301 && drPeiluyot.Length > 0)) // && clCalcGeneral.objOved.objMeafyeneyOved.GetMeafyen(33).IntValue))
                                bSidurNihulOrTafkid = true;

                        }
                    }

                    if (bSidurNehiga || bSidurNihulOrTafkid)
                    {
                        J = I + 1;
                        if (J < drSidurim.Length)
                        {

                            iMisparSidurNext = int.Parse(drSidurim[J]["mispar_sidur"].ToString());
                            if (bSidurNihulOrTafkid && iMisparSidurNext.ToString().Substring(0, 2) == "99" && drSidurim[J]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString())
                                bSidurMezake = true;
                            else if (bSidurNehiga && iMisparSidurNext.ToString().Substring(0, 2) == "99" && (drSidurim[J]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nihul.GetHashCode().ToString() || int.Parse(drSidurim[J]["zakay_lepizul"].ToString()) == 1))
                                bSidurMezake = true;
                            else if (iMisparSidurNext.ToString().Substring(0, 2) != "99")
                            {
                                //SetSugSidur(ref drSidurim[J], objOved.Taarich, iMisparSidurNext);
                                iSugSidur = int.Parse(drSidurim[J]["sug_sidur"].ToString());

                                bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nihul.GetHashCode(), objOved.Taarich, iSugSidur);
                                if (bSidurNehiga && bYeshSidur)
                                    bSidurMezake = true;
                                else if (bSidurNihulOrTafkid)
                                {
                                    bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nahagut.GetHashCode(), objOved.Taarich, iSugSidur);
                                    if (bYeshSidur)
                                        bSidurMezake = true;
                                }
                            }

                            if (bSidurMezake)
                            {
                                dShatHatchalaLetashlum = DateTime.Parse(drSidurim[J]["shat_hatchala_letashlum"].ToString());
                                dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());

                                fErechSidur = float.Parse((dShatHatchalaLetashlum - dShatGmarLetashlum).TotalMinutes.ToString());
                                if (fErechSidur >= 1 && fErechSidur <= objOved.objParameters.iMinTimeBetweenSidurim)
                                {
                                    if (drSidurim[I]["Mezake_Halbasha"].ToString() == "2" || drSidurim[I]["Mezake_Halbasha"].ToString() == "3" || drSidurim[J]["Mezake_Halbasha"].ToString() == "1" || drSidurim[J]["Mezake_Halbasha"].ToString() == "3")
                                    {
                                        if (fErechSidur > 10)
                                            fErechSidur = fErechSidur - 10;
                                        else fErechSidur = 0;
                                    }

                                    fErech += fErechSidur;

                                    //רציפות לילה                                
                                    fErechSidurLaylaEgged = getDakotRezifutLayla(dShatHatchalaLetashlum, dShatGmarLetashlum, objOved.objParameters.dTchilatTosefetLaila, objOved.objParameters.dSiyumTosefetLaila);//9,10
                                    fErechSidurLaylaChok = getDakotRezifutLayla(dShatHatchalaLetashlum, dShatGmarLetashlum, objOved.objParameters.dSiyumTosefetLaila, objOved.objParameters.dSiyumTosefetLailaChok);//10,12
                                    if (drSidurim[I]["Mezake_Halbasha"].ToString() == "2" || drSidurim[I]["Mezake_Halbasha"].ToString() == "3" || drSidurim[J]["Mezake_Halbasha"].ToString() == "1" || drSidurim[J]["Mezake_Halbasha"].ToString() == "3")
                                    {
                                        if (fErechSidurLaylaEgged >= 10)
                                            fErechSidurLaylaEgged = fErechSidurLaylaEgged - 10;
                                        else fErechSidurLaylaEgged = 0;

                                        if (fErechSidurLaylaChok >= 10)
                                            fErechSidurLaylaChok = fErechSidurLaylaChok - 10;
                                        else fErechSidurLaylaChok = 0;
                                    }
                                    fErechLaylaEgged += fErechSidurLaylaEgged;
                                    fErechLaylaChok += fErechSidurLaylaChok;
                                }
                            }
                        }
                    }
                }
                fSumDakotLaylaEgged = fErechLaylaEgged;
                fSumDakotLaylaChok = fErechLaylaChok;
                return fErech;
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
                drPeiluyot = null;
            }
        }

        public void CalcRechiv125()
        {
            DataRow[] drSidurim;
            int iMisparSidur, iSugSidur;
            DateTime dShatHatchalaSidur;
            float fErech, fDakotNochehut;
            string sSidurimMeyuchadim;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            DataRow[] drSidureyGrira;

            bool bNotCalc = false;

            try
            {
                sSidurimMeyuchadim = GetSidurimMeyuchRechiv(clGeneral.enRechivim.MishmeretShniaBameshek.GetHashCode());
                if (sSidurimMeyuchadim.Length > 0)
                {
                    drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ")");
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        bNotCalc = false;
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                        drSidureyGrira = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and shat_hatchala_letashlum<=Convert('" + drSidurim[I]["shat_gmar_letashlum"].ToString() + "', 'System.DateTime')");
                        if (drSidureyGrira.Length > 0)
                        {
                            for (int J = 0; J < drSidureyGrira.Length; J++)
                            {
                                iMisparSidur = int.Parse(drSidureyGrira[J]["mispar_sidur"].ToString());
                                dShatHatchalaSidur = DateTime.Parse(drSidureyGrira[J]["shat_hatchala_sidur"].ToString());

                                if (drSidureyGrira[J]["mispar_sidur"].ToString().Substring(0, 2) != "99")
                                {
                                    //SetSugSidur(ref drSidureyGrira[J], dShatHatchalaSidur, iMisparSidur);

                                    iSugSidur = int.Parse(drSidureyGrira[J]["sug_sidur"].ToString());

                                    if (CheckSugSidur(clGeneral.enMeafyen.SugAvoda.GetHashCode(), clGeneral.enSugAvoda.Grira.GetHashCode(), objOved.Taarich, iSugSidur))
                                    {
                                        bNotCalc = true;
                                        break;

                                    }
                                }
                                else if (drSidureyGrira[J]["sug_avoda"].ToString() == clGeneral.enSugAvoda.Grira.GetHashCode().ToString())
                                {
                                    bNotCalc = true;
                                    break;
                                }
                            }
                        }

                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                        if (iMisparSidur != 99220 || (iMisparSidur == 99220 && !bNotCalc))
                        {
                            if (oCalcBL.GetSugYomLemichsa(objOved, objOved.Taarich, objOved.objPirteyOved.iKodSectorIsuk,objOved.objMeafyeneyOved.GetMeafyen(56).IntValue) < 10)
                            {
                                fDakotNochehut = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and mispar_sidur=" + iMisparSidur + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                                fErech = fDakotNochehut;
                                addRowToTable(clGeneral.enRechivim.MishmeretShniaBameshek.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.MishmeretShniaBameshek.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
                drSidureyGrira = null;
            }
        }

        public void CalcRechiv128()
        {
            DataRow[] drSidurim, drSidurimLeyom;
            DataRow RowKodem, RowNext, drConenutGrira;
            int iMisparSidur;
            DateTime dShatHatchalaSidur, dShatHatchalaLetashlum, dShatGmarLetashlum;
            float fErech, fTempX, fTempY, fTempDakot;
            string sSidurimMeyuchadim;
            dShatHatchalaSidur = DateTime.MinValue;
            DateTime dShaa = objOved.Taarich;
            iMisparSidur = 0;
            bool bConenutGrira, bGriraInConenutGrira;
            int iMikumKnisa = 0;
            int iMikumYetzia = 0;
            try
            {

                drConenutGrira = null;
                sSidurimMeyuchadim = GetSidurimMeyuchRechiv(clGeneral.enRechivim.ZmanGrirot.GetHashCode());
                if (sSidurimMeyuchadim.Length > 0)
                {
                    bConenutGrira = IsSidurConenutGriraExist(ref drConenutGrira);
                    drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ")");
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        fTempX = 0;
                        fTempY = 0;
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                        if (bConenutGrira)
                        {
                            dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                            dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());
                            bGriraInConenutGrira = cheakSidurGrirainConenutGrira(dShatHatchalaLetashlum, dShatGmarLetashlum, drConenutGrira);

                            dShaa = DateTime.Parse(objOved.Taarich.ToShortDateString() + " 15:30:00");

                            if (drSidurim[I]["MIKUM_SHAON_KNISA"].ToString() != "")
                                iMikumKnisa = int.Parse(drSidurim[I]["MIKUM_SHAON_KNISA"].ToString());
                            if (drSidurim[I]["MIKUM_SHAON_YETZIA"].ToString() != "")
                                iMikumYetzia = int.Parse(drSidurim[I]["MIKUM_SHAON_YETZIA"].ToString());

                            if (dShatHatchalaSidur >= dShaa && bGriraInConenutGrira && (iMikumKnisa > 0 || iMikumYetzia > 0))
                            {
                                drSidurimLeyom = null;
                                drSidurimLeyom = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0  and mispar_sidur is not null", "shat_hatchala_sidur asc");
                                if (int.Parse(drSidurimLeyom[0]["mispar_sidur"].ToString()) == iMisparSidur && DateTime.Parse(drSidurimLeyom[0]["shat_hatchala_sidur"].ToString()) == dShatHatchalaSidur && iMikumKnisa > 0)
                                {
                                    fTempY = objOved.objParameters.iTosefetZmanGrira;
                                }
                                else if ((int.Parse(drSidurimLeyom[0]["mispar_sidur"].ToString()) != iMisparSidur || DateTime.Parse(drSidurimLeyom[0]["shat_hatchala_sidur"].ToString()) != dShatHatchalaSidur) && iMikumKnisa > 0)
                                {
                                    // fTempX = 0;
                                    RowKodem = drSidurimLeyom[0];
                                    for (int J = 0; J < drSidurimLeyom.Length; J++)
                                    {
                                        RowKodem = drSidurimLeyom[J];
                                        if ((drSidurim[I]["mispar_sidur"].ToString() == drSidurimLeyom[J]["mispar_sidur"].ToString()) && (drSidurim[I]["shat_hatchala_sidur"].ToString() == drSidurimLeyom[J]["shat_hatchala_sidur"].ToString()))
                                        {
                                            if (J > 0)
                                            {
                                                RowKodem = drSidurimLeyom[J - 1];
                                            }

                                            break;
                                        }
                                    }
                                    fTempDakot = float.Parse((DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString()) - DateTime.Parse(RowKodem["shat_gmar_letashlum"].ToString())).TotalMinutes.ToString());
                                    fTempY = Math.Min(objOved.objParameters.iTosefetZmanGrira, fTempDakot);
                                }

                                drSidurimLeyom = null;
                                drSidurimLeyom = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null", "shat_hatchala_sidur desc");
                                if (int.Parse(drSidurimLeyom[0]["mispar_sidur"].ToString()) == iMisparSidur && DateTime.Parse(drSidurimLeyom[0]["shat_hatchala_sidur"].ToString()) == dShatHatchalaSidur && iMikumYetzia > 0)
                                {
                                    fTempX = objOved.objParameters.iTosefetZmanGrira;
                                }
                                else if ((int.Parse(drSidurimLeyom[0]["mispar_sidur"].ToString()) != iMisparSidur || DateTime.Parse(drSidurimLeyom[0]["shat_hatchala_sidur"].ToString()) != dShatHatchalaSidur) && iMikumYetzia > 0)
                                {
                                    //    fTempY = 0;
                                    RowNext = drSidurimLeyom[0];
                                    for (int J = 0; J < drSidurimLeyom.Length; J++)
                                    {
                                        RowNext = drSidurimLeyom[J];
                                        if ((drSidurim[I]["mispar_sidur"].ToString() == drSidurimLeyom[J]["mispar_sidur"].ToString()) && (drSidurim[I]["shat_hatchala_sidur"].ToString() == drSidurimLeyom[J]["shat_hatchala_sidur"].ToString()))
                                        {
                                            if (J > 0)
                                            {
                                                RowNext = drSidurimLeyom[J - 1];
                                            }

                                            break;
                                        }
                                    }
                                    fTempDakot = float.Parse((DateTime.Parse(RowNext["shat_hatchala_letashlum"].ToString()) - DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString())).TotalMinutes.ToString());
                                    fTempX = Math.Min(objOved.objParameters.iTosefetZmanGrira, fTempDakot);
                                }
                            }
                        }
                        addRowToTable(clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fTempY);
                        addRowToTable(clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fTempX);

                        dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                        dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());

                        fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString()) + fTempX + fTempY;

                        addRowToTable(clGeneral.enRechivim.ZmanGrirot.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                        drSidurimLeyom = null;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.ZmanGrirot.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
               drSidurim=null;
               drSidurimLeyom = null;
            }
        }

        public void CalcRechiv131(ref float fZmanAdShabat, ref float fZmanafterShabat)
        {
            DataRow[] _drSidurim;
            int iMisparSidur, iMichutzLamichsa, iDay;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            float fMichsaYomit, fErech, fDakotRechiv;
            int iIsuk;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            fZmanAdShabat = 0;
            fZmanafterShabat = 0;

            try
            {
                //iSugYom = SugYom;
                if (!(objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30))
                {

                    //א
                    _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null and Hamarat_shabat=1");

                    for (int I = 0; I < _drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                        iMichutzLamichsa = int.Parse(_drSidurim[I]["out_michsa"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                        iDay = int.Parse(_drSidurim[I]["day_taarich"].ToString());
                        dShatHatchalaLetashlum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                        dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                        CheckSidurShabatToAdd(clGeneral.enRechivim.ShaotShabat100.GetHashCode(), iMisparSidur, iDay, objOved.SugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false);
                    }


                    fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);
                    //ג
                    ////if ((fMichsaYomit == 0) && (objOved.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode()))
                    ////{
                    ////    _drSidurim = null;
                    ////    _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null and MISPAR_SIDUR IN(99703,99202,99701,99010 ,99006)");
                    ////    for (int I = 0; I < _drSidurim.Length; I++)
                    ////    {
                    ////        iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    ////        fErech = 0;
                    ////        dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                    ////        fErech = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                    ////        if (fErech > 0)
                    ////        {
                    ////            fDakotRechiv = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ShaotShabat100.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    ////            fErech = fErech + fDakotRechiv;
                    ////            addRowToTable(clGeneral.enRechivim.ShaotShabat100.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                    ////        }

                    ////    }
                    ////}
                    ////ג
                    //if (clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                    //{
                    //    _drSidurim = null;
                    //    _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null and MISPAR_SIDUR IN(99012)");
                    //    for (int I = 0; I < _drSidurim.Length; I++)
                    //    {
                    //        iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    //        fErech = 0;
                    //        dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                    //        fErech = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                    //        if (fErech > 0)
                    //        {
                    //            fDakotRechiv = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ShaotShabat100.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    //            fErech = fErech + fDakotRechiv;
                    //            addRowToTable(clGeneral.enRechivim.ShaotShabat100.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                    //        }

                    //    }
                    //}
                }
                if (clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                {
                    _drSidurim = null;
                    _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");
                    fErech = 0;
                    //ד
                    for (int I = 0; I < _drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());

                        dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                        dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                        dShatHatchalaLetashlum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                       
                        fErech = 0;
                        if ((dShatHatchalaLetashlum < objOved.objParameters.dShatMaavarYom) && dShatGmarLetashlum > objOved.objParameters.dShatMaavarYom)
                        {
                            iIsuk = objOved.objPirteyOved.iIsuk;
                            if (iIsuk != 122 && iIsuk != 123 && iIsuk != 124 && iIsuk != 127)
                            {
                                fErech = float.Parse((dShatGmarLetashlum - objOved.objParameters.dShatMaavarYom).TotalMinutes.ToString());
                            }
                        }

                        if (fErech > 0)
                        {
                            fZmanafterShabat = +fErech;
                            fDakotRechiv = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ShaotShabat100.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            fErech = fErech + fDakotRechiv;
                            addRowToTable(clGeneral.enRechivim.ShaotShabat100.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                        }

                    }

                }
                //ו'
                if (objOved.objPirteyOved.iDirug == 85 && objOved.objPirteyOved.iDarga == 30)
                {
                    _drSidurim = null;
                    _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");
                    fErech = 0;
                    for (int I = 0; I < _drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());

                        dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                        dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                        dShatHatchalaLetashlum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());

                        fErech = 0;
                        if (oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom) || oCalcBL.CheckYomShishi(objOved.SugYom))
                        {
                            if (dShatGmarLetashlum > objOved.objParameters.dKnisatShabat && dShatHatchalaSidur < objOved.objParameters.dKnisatShabat)
                            { fErech = float.Parse((objOved.objParameters.dKnisatShabat - dShatHatchalaLetashlum).TotalMinutes.ToString()); }

                            if (fErech > 0)
                            {
                                fZmanAdShabat =+ fErech;
                                //fDakotRechiv = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ShaotShabat100.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                //fErech = fErech + fDakotRechiv;
                               // addRowToTable(clGeneral.enRechivim.ShaotShabat100.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                            }
                        }
                    }
                   
                }
                //if ((oCalcBL.CheckYomShishi(iSugYom) || oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim,iSugYom)) && fMichsaYomit == 0)
                //{
                //    _drSidurim = objOved.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime') and MISPAR_SIDUR IN(99011,99207,99007)");

                //    for (int I = 0; I < _drSidurim.Length; I++)
                //    {
                //        iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                //        fErech = 0;
                //        dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                //        fErech = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                //        if (fErech > 0)
                //        {
                //            addRowToTable(clGeneral.enRechivim.ShaotShabat100.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                //        }

                //    }
                //}

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.ShaotShabat100.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public void CalcRechiv132()
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur;
            float fErech, fDakotHamaraShabat, fDakotZikuyChofesh;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    fDakotHamaraShabat = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    fDakotZikuyChofesh = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                    fErech = fDakotHamaraShabat + fDakotZikuyChofesh;
                    addRowToTable(clGeneral.enRechivim.ChofeshZchut.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.ChofeshZchut.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public void CalcRechiv133()
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur;
            float fErech, fDakotPremiaYomit, fDakotPremiaShabat, fDakotPremiaBeshishi;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    fDakotPremiaShabat = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaShabat.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    fDakotPremiaYomit = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaYomit.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    fDakotPremiaBeshishi = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                    fErech = fDakotPremiaShabat + fDakotPremiaYomit + fDakotPremiaBeshishi;
                    addRowToTable(clGeneral.enRechivim.PremyaRegila.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.PremyaRegila.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public void CalcRechiv134()
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur;
            float fErech, fPremiaVisaShabat, fPremiaVisa, fPremiaVisaBeShishi;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    fPremiaVisa = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaVisa.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    fPremiaVisaShabat = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaVisaShabat.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    fPremiaVisaBeShishi = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaVisaShishi.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                    fErech = fPremiaVisaShabat + fPremiaVisa + fPremiaVisaBeShishi;
                    addRowToTable(clGeneral.enRechivim.PremyaNamlak.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.PremyaNamlak.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public void CalcRechiv142()
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur;
            float fErech, fDakotNochehutLetashlum, fDakotNochehutBefoal;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    fDakotNochehutBefoal = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    fDakotNochehutLetashlum = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                    fErech = fDakotNochehutBefoal - fDakotNochehutLetashlum;
                    addRowToTable(clGeneral.enRechivim.KizuzNochehut.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.KizuzNochehut.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        //public void CalcRechiv164()
        //{
        //    try 
        //    {
        //        CalcDakotMechutzLemichsa(clGeneral.enRechivim.DakotMichutzLemichsaPakach.GetHashCode());

        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //}


        //public void CalcRechiv165()
        //{
        //    try
        //    {
        //        CalcDakotMechutzLemichsa(clGeneral.enRechivim.DakotMichutzLemichsaSadsran.GetHashCode());

        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //}

        //public void CalcRechiv166()
        //{
        //    try
        //    {
        //        CalcDakotMechutzLemichsa(clGeneral.enRechivim.DakotMichutzLemichsaRakaz.GetHashCode());

        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //}

        //public void CalcRechiv167()
        //{
        //    try
        //    {
        //        CalcDakotMechutzLemichsa(clGeneral.enRechivim.DakotMichutzLemichsaMeshaleach.GetHashCode());
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //}

        public void CalcRechiv179()
        {
            try
            {
                CalcSachDakot(clGeneral.enRechivim.SachDakotPakach.GetHashCode());
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void CalcRechiv180()
        {
            try
            {
                CalcSachDakot(clGeneral.enRechivim.SachDakotSadran.GetHashCode());
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void CalcRechiv181()
        {
            try
            {
                CalcSachDakot(clGeneral.enRechivim.SachDakotRakaz.GetHashCode());
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void CalcRechiv182()
        {
            try
            {
                CalcSachDakot(clGeneral.enRechivim.SachDakotMeshalech.GetHashCode());
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void CalcRechiv183()
        {
            try
            {
                CalcSachDakot(clGeneral.enRechivim.SachDakotKupai.GetHashCode());
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void CalcRechiv184()
        {

            DataRow[] drSidurim;
            int iMisparSidur, iOutMichsa;
            DateTime dShatHatchalaSidur;
            float fErech, fDakotNihulTnua;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");
                for (int I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                    iOutMichsa = int.Parse(drSidurim[I]["out_michsa"].ToString());

                    if (oCalcBL.CheckOutMichsa(objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, iOutMichsa))
                    {
                        fDakotNihulTnua = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                        if (fDakotNihulTnua > 0)
                        {

                            fErech = fDakotNihulTnua;
                            addRowToTable(clGeneral.enRechivim.DakotMichutzLamichsaNihulTnua.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotMichutzLamichsaNihulTnua.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        public void CalcRechiv186()
        {
            DataRow[] drSidurim;
            int iMisparSidur, iOutMichsa;
            float fErech;
            DateTime dShatHatchalaSidur;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                //אם סידור מסומן מחוץ למכסה TB_Sidurim_Ovedim.Out_michsa = 1  וגם דקות תפקיד בשבת (רכיב 37) של סידור > 0 ערך הרכיב = דקות תפקיד בשבת (רכיב 37) של סידור 

                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");
                for (int I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                    iOutMichsa = int.Parse(drSidurim[I]["out_michsa"].ToString());

                    if (oCalcBL.CheckOutMichsa(objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, iOutMichsa))
                    {
                        fErech = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidShabat.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                        if (fErech > 0)
                        {
                            addRowToTable(clGeneral.enRechivim.MichutzLamichsaTafkidShabat.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.MichutzLamichsaTafkidShabat.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        public void CalcRechiv187()
        {
            DataRow[] drSidurim;
            int iMisparSidur, iOutMichsa;
            float fErech;
            DateTime dShatHatchalaSidur;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                //אם סידור מסומן מחוץ למכסה TB_Sidurim_Ovedim.Out_michsa = 1  וגם דקות בניהול תנועה בשבת (רכיב 36) של סידור > 0 ערך הרכיב = דקות בניהול תנועה בשבת (רכיב 36)

                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");
                for (int I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                    iOutMichsa = int.Parse(drSidurim[I]["out_michsa"].ToString());

                    if (oCalcBL.CheckOutMichsa(objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, iOutMichsa))
                    {
                        fErech = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                        if (fErech > 0)
                        {
                            addRowToTable(clGeneral.enRechivim.MichutzLamichsaNihulShabat.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.MichutzLamichsaNihulShabat.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        private void CalcDakotMechutzLemichsa(int iKodRechiv)
        {
            string sSidurimMeyuchadim, sSugeySidur;
            DataRow[] drSidurim;
            int iMisparSidur, iSugSidur;
            float fErech;
            DateTime dShatHatchalaSidur;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                sSidurimMeyuchadim = GetSidurimMeyuchRechiv(iKodRechiv);
                if (sSidurimMeyuchadim.Length > 0)
                {
                    drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null and out_michsa=1 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ")");
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                        fErech = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                        addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fErech);

                    }
                }

                sSugeySidur = GetSugeySidurRechiv(iKodRechiv);
                if (sSugeySidur.Length > 0)
                {
                    drSidurim = null;
                    drSidurim = GetSidurimRegilim();
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        if ((int)drSidurim[I]["out_michsa"] == 1)
                        {
                            iMisparSidur = int.Parse(drSidurim[I]["MISPAR_SIDUR"].ToString());
                            dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                            //SetSugSidur(ref drSidurim[I], dShatHatchalaSidur, iMisparSidur);

                            iSugSidur = int.Parse(drSidurim[I]["sug_sidur"].ToString());

                            if (sSugeySidur.IndexOf("," + iSugSidur.ToString() + ",") > -1)
                            {

                                fErech = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                                addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fErech);

                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, iKodRechiv, objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        public void CalcRechiv189()
        {
            // יש לבצע את החישוב עבור סידורים המוגדרים כסידורי נהגות – זיהוי סידורי נהיגה:
            //  ישנו מאפיין (קוד מאפיין = 3) לסוג סידור/סידור מיוחד המתאר את סוג העבודה   .
            //ערך מאפיין זה (סוג עבודה) = 5 = עבודת נהגות.
            int iMisparSidur, iSugSidur;
            bool bYeshSidur;
            float fMichsaYomit, fDakotRechiv, fDakotChol;
            int iSugYom, iSugYomNext;
            float fErechRechiv; // fTosefetGrirotHatchala, fTosefetGrirotSof,
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, dShatHatchlaShabat;
            DataRow[] _drSidurMeyuchad, _drSidurRagil;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                fDakotChol = 0;
                dShatHatchlaShabat = objOved.objParameters.dKnisatShabat;
                iSugYom = oCalcBL.GetSugYomLemichsa(objOved, objOved.Taarich, objOved.objPirteyOved.iKodSectorIsuk, objOved.objMeafyeneyOved.GetMeafyen(56).IntValue);
                //בדיקת סידורים רגילים
                // נלקחים הסידורים הרגילים שסקטור העבודה שלהם =נהגות
                _drSidurRagil = GetSidurimRegilim();
                fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);

                if (_drSidurRagil.Length > 0)
                {
                    for (int I = 0; I < _drSidurRagil.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());
                        fDakotRechiv = 0;
                        dShatHatchalaSidur = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_sidur"].ToString());

                        //SetSugSidur(ref _drSidurRagil[I], objOved.Taarich, iMisparSidur);

                        iSugSidur = int.Parse(_drSidurRagil[I]["sug_sidur"].ToString());

                        bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nahagut.GetHashCode(), objOved.Taarich, iSugSidur);
                        if (bYeshSidur)
                        {
                            dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                            //fTosefetGrirotHatchala = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            //fTosefetGrirotSof = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                           // dShatHatchalaLetashlum = dShatHatchalaLetashlum.AddMinutes(-fTosefetGrirotSof);
                            dShatGmarLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_gmar_letashlum"].ToString());
                            //dShatGmarLetashlum = dShatGmarLetashlum.AddMinutes(+fTosefetGrirotHatchala);
                            if (iSugYom == clGeneral.enSugYom.Shishi.GetHashCode() && fMichsaYomit == 0)
                            {
                                if (dShatHatchalaLetashlum < dShatHatchlaShabat && dShatGmarLetashlum > dShatHatchlaShabat)
                                {
                                    fDakotRechiv = float.Parse((dShatHatchlaShabat - dShatHatchalaLetashlum).TotalMinutes.ToString());
                                }
                                else if (dShatHatchalaLetashlum < dShatHatchlaShabat && dShatGmarLetashlum <= dShatHatchlaShabat)
                                {
                                    fDakotRechiv = float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());
                                }
                                else
                                {
                                    fDakotRechiv = float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());
                                }
                                addRowToTable(clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fDakotRechiv);
                            }
                            iSugYomNext = clGeneral.GetSugYom(objOved.oGeneralData.dtYamimMeyuchadim, DateTime.Parse(objOved.Taarich.ToShortDateString()).AddDays(1),objOved.oGeneralData.dtSugeyYamimMeyuchadim);//, objOved.objMeafyeneyOved.GetMeafyen(56).IntValue);
                            if (iSugYomNext == clGeneral.enSugYom.Shishi.GetHashCode() && fMichsaYomit == 0)
                            {
                                CalcGlishaLeyomShishi(dShatHatchalaLetashlum, dShatGmarLetashlum, ref fDakotChol, ref fDakotRechiv);
                                addRowToTable(clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fDakotRechiv);

                            }


                            if (iSugSidur == 69)
                            {
                                fErechRechiv = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                                if (fErechRechiv > objOved.objParameters.iMinZmanGriraDarom)
                                {
                                    if (int.Parse(iMisparSidur.ToString().Substring(0, 2)) > 11)
                                    {
                                        fErechRechiv = objOved.objParameters.iMinZmanGriraDarom;
                                    }
                                    else
                                    {
                                        fErechRechiv = objOved.objParameters.iMinZmanGriraTzafon;
                                    }
                                }
                                else if (fErechRechiv > objOved.objParameters.iMinZmanGriraTzafon && fErechRechiv <= objOved.objParameters.iMinZmanGriraDarom)
                                {
                                    if (int.Parse(iMisparSidur.ToString().Substring(0, 2)) <= 11)
                                    {
                                        fErechRechiv = objOved.objParameters.iMinZmanGriraTzafon;
                                    }
                                }
                                addRowToTable(clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);

                            }
                        }

                    }
                }

                //בדיקת סידורים מיוחדים
                // נלקחים רק סידורים מיוחדים מסקטור עבודה =  נהגות
                _drSidurMeyuchad = GetSidurimMeyuchadim("sector_avoda", clGeneral.enSectorAvoda.Nahagut.GetHashCode());

                for (int I = 0; I <= _drSidurMeyuchad.Length - 1; I++)
                {
                    iMisparSidur = int.Parse(_drSidurMeyuchad[I]["mispar_sidur"].ToString());

                    if (iMisparSidur != 99220)
                    {
                        dShatHatchalaSidur = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_sidur"].ToString());

                        dShatHatchalaLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_letashlum"].ToString());
                        //fTosefetGrirotHatchala = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        //fTosefetGrirotSof = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                      //  dShatHatchalaLetashlum = dShatHatchalaLetashlum.AddMinutes(-fTosefetGrirotSof);
                        dShatGmarLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_gmar_letashlum"].ToString());
                     //   dShatGmarLetashlum = dShatGmarLetashlum.AddMinutes(+fTosefetGrirotHatchala);
                        fDakotRechiv = 0;
                        iSugYom = oCalcBL.GetSugYomLemichsa(objOved, objOved.Taarich, objOved.objPirteyOved.iKodSectorIsuk, objOved.objMeafyeneyOved.GetMeafyen(56).IntValue);
                        if (iSugYom == clGeneral.enSugYom.Shishi.GetHashCode() && fMichsaYomit == 0)
                        {
                            if (dShatHatchalaLetashlum < dShatHatchlaShabat && dShatGmarLetashlum > dShatHatchlaShabat)
                            {
                                fDakotRechiv = float.Parse((dShatHatchlaShabat - dShatHatchalaLetashlum).TotalMinutes.ToString());
                            }
                            else if (dShatHatchalaLetashlum < dShatHatchlaShabat && dShatGmarLetashlum <= dShatHatchlaShabat)
                            {
                                fDakotRechiv = float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());
                            }
                            else
                            {
                                fDakotRechiv = float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());
                            }
                            addRowToTable(clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fDakotRechiv);
                        }

                        iSugYomNext = clGeneral.GetSugYom(objOved.oGeneralData.dtYamimMeyuchadim, DateTime.Parse(objOved.Taarich.ToShortDateString()).AddDays(1),objOved.oGeneralData.dtSugeyYamimMeyuchadim);//, objOved.objMeafyeneyOved.GetMeafyen(56).IntValue);
                        if (iSugYomNext == clGeneral.enSugYom.Shishi.GetHashCode() && fMichsaYomit == 0)
                        {
                            CalcGlishaLeyomShishi(dShatHatchalaLetashlum, dShatGmarLetashlum, ref fDakotChol, ref fDakotRechiv);
                            addRowToTable(clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fDakotRechiv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurMeyuchad=null;
                _drSidurRagil = null;
            }
        }

        public void CalcRechiv191()
        {
            // יש לבצע את החישוב עבור סידורים המוגדרים כסידורי נהגות – זיהוי סידורי נהיגה:
            //  ישנו מאפיין (קוד מאפיין = 3) לסוג סידור/סידור מיוחד המתאר את סוג העבודה   .
            //ערך מאפיין זה (סוג עבודה) = 4 =  ניהול תנועה.
            int iMisparSidur, iSugSidur;
            bool bYeshSidur;
            int iSugYomNext, iSugYom;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, dShatHatchlaShabat;
            DataRow[] _drSidurMeyuchad, _drSidurRagil;
            float fMichsaYomit, fDakotRechiv, fDakotChol, fZmanHafsaka;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                dShatHatchlaShabat = objOved.objParameters.dKnisatShabat;
                iSugYom = oCalcBL.GetSugYomLemichsa(objOved, objOved.Taarich, objOved.objPirteyOved.iKodSectorIsuk, objOved.objMeafyeneyOved.GetMeafyen(56).IntValue);
                fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);
                fDakotChol = 0;
                //בדיקת סידורים רגילים
                // נלקחים הסידורים הרגילים שסקטור העבודה שלהם =ניהול תנועה
                _drSidurRagil = GetSidurimRegilim();
                if (_drSidurRagil.Length > 0)
                {
                    for (int I = 0; I < _drSidurRagil.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_sidur"].ToString());

                        //SetSugSidur(ref _drSidurRagil[I], objOved.Taarich, iMisparSidur);

                        iSugSidur = int.Parse(_drSidurRagil[I]["sug_sidur"].ToString());

                        bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nihul.GetHashCode(), objOved.Taarich, iSugSidur);
                        if (bYeshSidur)
                        {
                            dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                            dShatGmarLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_gmar_letashlum"].ToString());
                            fZmanHafsaka = float.Parse(_drSidurRagil[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());
                            fDakotRechiv = 0;
                            if (iSugYom == clGeneral.enSugYom.Shishi.GetHashCode() && fMichsaYomit == 0)
                            {
                                if (dShatHatchalaLetashlum < dShatHatchlaShabat && dShatGmarLetashlum > dShatHatchlaShabat)
                                {
                                    fDakotRechiv = float.Parse((dShatHatchlaShabat - dShatHatchalaLetashlum).TotalMinutes.ToString());
                                }
                                else if (dShatHatchalaLetashlum < dShatHatchlaShabat && dShatGmarLetashlum <= dShatHatchlaShabat)
                                {
                                    fDakotRechiv = float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());
                                }
                                fDakotRechiv -= fZmanHafsaka;
                                addRowToTable(clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fDakotRechiv);
                            }
                            iSugYomNext = clGeneral.GetSugYom(objOved.oGeneralData.dtYamimMeyuchadim, DateTime.Parse(objOved.Taarich.ToShortDateString()).AddDays(1),objOved.oGeneralData.dtSugeyYamimMeyuchadim);//, objOved.objMeafyeneyOved.GetMeafyen(56).IntValue);
                            if (iSugYomNext == clGeneral.enSugYom.Shishi.GetHashCode() && fMichsaYomit == 0)
                            {
                                CalcGlishaLeyomShishi(dShatHatchalaLetashlum, dShatGmarLetashlum, ref fDakotChol, ref fDakotRechiv);
                                addRowToTable(clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fDakotRechiv);

                            }
                        }

                    }
                }

                //בדיקת סידורים מיוחדים
                // נלקחים רק סידורים מיוחדים מסקטור עבודה =  ניהול
                _drSidurMeyuchad = GetSidurimMeyuchadim("sector_avoda", clGeneral.enSectorAvoda.Nihul.GetHashCode());

                for (int I = 0; I < _drSidurMeyuchad.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurMeyuchad[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_sidur"].ToString());

                    dShatHatchalaLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_letashlum"].ToString());
                    dShatGmarLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_gmar_letashlum"].ToString());
                    fZmanHafsaka = float.Parse(_drSidurMeyuchad[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());
                    fDakotRechiv = 0;
                    if (iSugYom == clGeneral.enSugYom.Shishi.GetHashCode() && fMichsaYomit == 0)
                    {
                        if (dShatHatchalaLetashlum < dShatHatchlaShabat && dShatGmarLetashlum > dShatHatchlaShabat)
                        {
                            fDakotRechiv = float.Parse((dShatHatchlaShabat - dShatHatchalaLetashlum).TotalMinutes.ToString());
                        }
                        else if (dShatHatchalaLetashlum < dShatHatchlaShabat && dShatGmarLetashlum <= dShatHatchlaShabat)
                        {
                            fDakotRechiv = float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());
                        }
                        fDakotRechiv -= fZmanHafsaka;
                        addRowToTable(clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fDakotRechiv);
                    }

                    iSugYomNext = clGeneral.GetSugYom(objOved.oGeneralData.dtYamimMeyuchadim, DateTime.Parse(objOved.Taarich.ToShortDateString()).AddDays(1),objOved.oGeneralData.dtSugeyYamimMeyuchadim);//, objOved.objMeafyeneyOved.GetMeafyen(56).IntValue);
                    if (iSugYomNext == clGeneral.enSugYom.Shishi.GetHashCode() && fMichsaYomit == 0)
                    {
                        CalcGlishaLeyomShishi(dShatHatchalaLetashlum, dShatGmarLetashlum, ref fDakotChol, ref fDakotRechiv);
                        addRowToTable(clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fDakotRechiv);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv192()
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur;
            float fErech, fDakotTafkidChol, fDakotTafkidShabat, fDakotTafkidShishi;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    fDakotTafkidChol = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidChol.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    fDakotTafkidShabat = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidShabat.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    fDakotTafkidShishi = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                    fErech = fDakotTafkidChol + fDakotTafkidShabat + fDakotTafkidShishi;

                    addRowToTable(clGeneral.enRechivim.SachDakotTafkid.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.SachDakotTafkid.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public void CalcRechiv193()
        {
            // יש לבצע את החישוב עבור סידורים המוגדרים כסידורי נהגות – זיהוי סידורי נהיגה:
            //  ישנו מאפיין (קוד מאפיין = 3) לסוג סידור/סידור מיוחד המתאר את סוג העבודה   .
            //ערך מאפיין זה (סוג עבודה) = 1 =  תפקיד.
            int iMisparSidur, iSugSidur;
            bool bYeshSidur;
            int iSugYom, iSugYomNext;
            float fDakotRechiv, fMichsaYomit, fDakotChol, fTosefetGrirotHatchala, fTosefetGrirotSof;
            DataRow[] _drSidurMeyuchad, _drSidurRagil;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, dShatHatchlaShabat;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                //בדיקת סידורים רגילים
                // נלקחים הסידורים הרגילים שסקטור העבודה שלהם =תפקיד
                fDakotChol = 0;
                fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);

                _drSidurRagil = GetSidurimRegilim();
                if (_drSidurRagil.Length > 0)
                {
                    for (int I = 0; I < _drSidurRagil.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());
                        fDakotRechiv = 0;
                        //SetSugSidur(ref _drSidurRagil[I], objOved.Taarich, iMisparSidur);

                        iSugSidur = int.Parse(_drSidurRagil[I]["sug_sidur"].ToString());
                        bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Tafkid.GetHashCode(), objOved.Taarich, iSugSidur);
                        if (bYeshSidur)
                        {
                            iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());
                            dShatHatchalaSidur = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_sidur"].ToString());

                            //dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                            //dShatGmarLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_gmar_letashlum"].ToString());
                            fTosefetGrirotHatchala = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            fTosefetGrirotSof = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                            dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                            dShatHatchalaLetashlum = dShatHatchalaLetashlum.AddMinutes(-fTosefetGrirotSof);
                            dShatGmarLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_gmar_letashlum"].ToString());
                            dShatGmarLetashlum = dShatGmarLetashlum.AddMinutes(+fTosefetGrirotHatchala);

                            dShatHatchlaShabat = objOved.objParameters.dKnisatShabat;
                            fDakotRechiv = 0;
                            iSugYom = oCalcBL.GetSugYomLemichsa(objOved, objOved.Taarich, objOved.objPirteyOved.iKodSectorIsuk, objOved.objMeafyeneyOved.GetMeafyen(56).IntValue);
                            if (iSugYom == clGeneral.enSugYom.Shishi.GetHashCode() && fMichsaYomit == 0)
                            {
                                if (dShatHatchalaLetashlum < dShatHatchlaShabat && dShatGmarLetashlum > dShatHatchlaShabat)
                                {
                                    fDakotRechiv = float.Parse((dShatHatchlaShabat - dShatHatchalaLetashlum).TotalMinutes.ToString());
                                }
                                else if (dShatHatchalaLetashlum < dShatHatchlaShabat && dShatGmarLetashlum <= dShatHatchlaShabat)
                                {
                                    fDakotRechiv = float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());
                                }
                                else
                                {
                                    fDakotRechiv = float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());
                                }

                                addRowToTable(clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fDakotRechiv);
                            }

                            iSugYomNext = clGeneral.GetSugYom(objOved.oGeneralData.dtYamimMeyuchadim, DateTime.Parse(objOved.Taarich.ToShortDateString()).AddDays(1),objOved.oGeneralData.dtSugeyYamimMeyuchadim);//, objOved.objMeafyeneyOved.GetMeafyen(56).IntValue);
                            if (iSugYomNext == clGeneral.enSugYom.Shishi.GetHashCode() && fMichsaYomit == 0)
                            {
                                CalcGlishaLeyomShishi(dShatHatchalaLetashlum, dShatGmarLetashlum, ref fDakotChol, ref fDakotRechiv);
                                addRowToTable(clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fDakotRechiv);
                            }
                        }

                    }
                }

                //בדיקת סידורים מיוחדים
                // נלקחים רק סידורים מיוחדים מסקטור עבודה =  תפקיד
                _drSidurMeyuchad = GetSidurimMeyuchadim("sector_avoda", clGeneral.enSectorAvoda.Tafkid.GetHashCode());

                for (int I = 0; I < _drSidurMeyuchad.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurMeyuchad[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_sidur"].ToString());

                    //dShatHatchalaLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_letashlum"].ToString());
                    //dShatGmarLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_gmar_letashlum"].ToString());
                    fTosefetGrirotHatchala = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    fTosefetGrirotSof = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                    dShatHatchalaLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_letashlum"].ToString());
                    dShatHatchalaLetashlum = dShatHatchalaLetashlum.AddMinutes(-fTosefetGrirotSof);
                    dShatGmarLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_gmar_letashlum"].ToString());
                    dShatGmarLetashlum = dShatGmarLetashlum.AddMinutes(+fTosefetGrirotHatchala);

                    dShatHatchlaShabat = objOved.objParameters.dKnisatShabat;
                    fDakotRechiv = 0;
                    iSugYom = oCalcBL.GetSugYomLemichsa(objOved, objOved.Taarich, objOved.objPirteyOved.iKodSectorIsuk, objOved.objMeafyeneyOved.GetMeafyen(56).IntValue);
                    if (iSugYom == clGeneral.enSugYom.Shishi.GetHashCode() && fMichsaYomit == 0)
                    {
                        if (dShatHatchalaLetashlum < dShatHatchlaShabat && dShatGmarLetashlum > dShatHatchlaShabat)
                        {
                            fDakotRechiv = float.Parse((dShatHatchlaShabat - dShatHatchalaLetashlum).TotalMinutes.ToString());
                        }
                        else if (dShatHatchalaLetashlum < dShatHatchlaShabat && dShatGmarLetashlum <= dShatHatchlaShabat)
                        {
                            fDakotRechiv = float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());
                        }
                        else
                        {
                            fDakotRechiv = float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());
                        }

                        addRowToTable(clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fDakotRechiv);
                    }

                    iSugYomNext = clGeneral.GetSugYom(objOved.oGeneralData.dtYamimMeyuchadim, DateTime.Parse(objOved.Taarich.ToShortDateString()).AddDays(1),objOved.oGeneralData.dtSugeyYamimMeyuchadim);//, objOved.objMeafyeneyOved.GetMeafyen(56).IntValue);
                    if (iSugYomNext == clGeneral.enSugYom.Shishi.GetHashCode() && fMichsaYomit == 0)
                    {
                        CalcGlishaLeyomShishi(dShatHatchalaLetashlum, dShatGmarLetashlum, ref fDakotChol, ref fDakotRechiv);
                        addRowToTable(clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fDakotRechiv);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurMeyuchad = null;
                _drSidurRagil = null;
            }
        }

        public void CalcRechiv194()
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur;
            float fErech, fNehigaChol, fNihulTnua, fTafkidChol;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    fNehigaChol = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaChol.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    fNihulTnua = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    fTafkidChol = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidChol.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                    fErech = fNehigaChol + fNihulTnua + fTafkidChol;
                    addRowToTable(clGeneral.enRechivim.NochehutChol.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.NochehutChol.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public void CalcRechiv195()
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur;
            float fErech, fNehigaShishi, fNihulShishi, fTafkidShishi;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    fNehigaShishi = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    fNihulShishi = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    fTafkidShishi = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                    fErech = fNehigaShishi + fNihulShishi + fTafkidShishi;
                    addRowToTable(clGeneral.enRechivim.NochehutBeshishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.NochehutBeshishi.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public void CalcRechiv196()
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur;
            float fErech, fNehigaShabat, fNihulShabat, fTafkidShabat;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    fNehigaShabat = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaShabat.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    fNihulShabat = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    fTafkidShabat = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidShabat.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                    fErech = fNehigaShabat + fNihulShabat + fTafkidShabat;
                    addRowToTable(clGeneral.enRechivim.NochehutShabat.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.NochehutShabat.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public void CalcRechiv202_2()
        {
            //דקות פרמיה בשישי  (רכיב 202 ) :  

            int iMisparSidur, iSugSidur;
            bool bYeshSidur;
            int iDay;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            DataRow[] _drSidurMeyuchad, _drSidurRagil;
            float fErechRechiv, fDakotNochehutSidur, fMichsaYomit;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
              // iSugYom = SugYom;
                fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(),objOved.Taarich);

                //בדיקת סידורים רגילים
                // נלקחים הסידורים הרגילים שסקטור העבודה שלהם =נהגות
                _drSidurRagil = GetSidurimRegilim();
                if (_drSidurRagil.Length > 0)
                {
                    for (int I = 0; I < _drSidurRagil.Length; I++)
                    {
                        if (_drSidurRagil[I]["sidur_namlak_visa"].ToString() == "")
                        {
                            iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());
                            dShatHatchalaSidur = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_sidur"].ToString());

                            //SetSugSidur(ref _drSidurRagil[I], objOved.Taarich, iMisparSidur);

                            iSugSidur = int.Parse(_drSidurRagil[I]["sug_sidur"].ToString());
                            bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nahagut.GetHashCode(), objOved.Taarich, iSugSidur);
                            if (bYeshSidur)
                            {
                                iDay = int.Parse(_drSidurRagil[I]["day_taarich"].ToString());
                                dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                                dShatGmarLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_gmar_letashlum"].ToString());
                                fDakotNochehutSidur = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                fErechRechiv = 0;

                                //if (dShatHatchalaLetashlum < objOved.objParameters.dKnisatShabat)
                                if ((oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom) || oCalcBL.CheckYomShishi(objOved.SugYom)) && fMichsaYomit == 0)
                                {
                                    fErechRechiv = fDakotNochehutSidur - oPeilut.getZmanHamtanaEilat(iMisparSidur, dShatHatchalaSidur); 
                                }

                                addRowToTable(clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);

                            }
                        }
                    }
                }

                //בדיקת סידורים מיוחדים
                // נלקחים רק סידורים מיוחדים מסקטור עבודה =  נהגות
                _drSidurMeyuchad = GetSidurimMeyuchadim("sector_avoda", clGeneral.enSectorAvoda.Nahagut.GetHashCode());

                for (int I = 0; I <= _drSidurMeyuchad.Length - 1; I++)
                {
                    fErechRechiv = 0;
                    if (_drSidurMeyuchad[I]["sidur_namlak_visa"].ToString() == "")
                    {
                        iMisparSidur = int.Parse(_drSidurMeyuchad[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_sidur"].ToString());

                        iDay = int.Parse(_drSidurMeyuchad[I]["day_taarich"].ToString());
                        dShatHatchalaLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_letashlum"].ToString());
                        dShatGmarLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_gmar_letashlum"].ToString());
                        fDakotNochehutSidur = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                        //if (dShatHatchalaLetashlum < objOved.objParameters.dKnisatShabat)
                        if ((oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom) || oCalcBL.CheckYomShishi(objOved.SugYom)) && fMichsaYomit == 0)
                        {
                            fErechRechiv = fDakotNochehutSidur - oPeilut.getZmanHamtanaEilat(iMisparSidur, dShatHatchalaSidur); 
                        }

                        addRowToTable(clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurMeyuchad=null;
                _drSidurRagil = null;
            }
        }

        public void CalcRechiv202()
        {
            //דקות פרמיה בשישי  (רכיב 202 ) :  

            int iMisparSidur, iSugSidur;
            bool bYeshSidur;
            DateTime dShatHatchalaSidur;
            DataRow[] _drSidurMeyuchad, _drSidurRagil;
            float fErechRechiv, fDakotNochehutSidur, fTosefetGil, fMichsaYomit, fNuchehutLepremia;
            float fDakotHagdara, fSumDakotSikun, fDakotHistaglut, fSachNesiot, fDakotLepremia, fDakotKisuyTor;

            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);

                //בדיקת סידורים רגילים
                // נלקחים הסידורים הרגילים שסקטור העבודה שלהם =נהגות
                _drSidurRagil = GetSidurimRegilim();
                if (_drSidurRagil.Length > 0)
                {
                    for (int I = 0; I < _drSidurRagil.Length; I++)
                    {
                        fErechRechiv = 0;
                        fTosefetGil = 0;
                        if (_drSidurRagil[I]["sidur_namlak_visa"].ToString() == "")
                        {
                            iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());
                            iSugSidur = int.Parse(_drSidurRagil[I]["sug_sidur"].ToString());
                            bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nahagut.GetHashCode(), objOved.Taarich, iSugSidur);
                            if (bYeshSidur)
                            {
                                iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());
                                dShatHatchalaSidur = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_sidur"].ToString());
                               // dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                                 if (dShatHatchalaSidur < objOved.objParameters.dKnisatShabat)
                                {
                                    fDakotNochehutSidur = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                                    fNuchehutLepremia = fDakotNochehutSidur - oPeilut.getZmanHamtanaEilat(iMisparSidur, dShatHatchalaSidur);

                                    fDakotHagdara = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotHagdara.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')")) / float.Parse("0.75");
                                    fSumDakotSikun = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotSikun.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')")) / float.Parse("0.75");
                                    fDakotHistaglut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotHistaglut.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                    fDakotKisuyTor = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotKisuiTor.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                    fDakotLepremia = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.SachDakotLepremia.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                    fSachNesiot = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.SachNesiot.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                                   
                                     if ((fDakotHagdara + fDakotLepremia) > 0)
                                    {
                                        fErechRechiv = float.Parse((fDakotHagdara + fSumDakotSikun + fDakotHistaglut + fDakotKisuyTor + fDakotLepremia + (fSachNesiot * objOved.objParameters.fElementZar) + fTosefetGil - fNuchehutLepremia).ToString());
                                    }
                                    addAnyRowToTable(clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);
                                }
                            }
                        }
                    }
                }

                //בדיקת סידורים מיוחדים
                // נלקחים רק סידורים מיוחדים מסקטור עבודה =  נהגות
                _drSidurMeyuchad = GetSidurimMeyuchadim("sector_avoda", clGeneral.enSectorAvoda.Nahagut.GetHashCode());

                for (int I = 0; I <= _drSidurMeyuchad.Length - 1; I++)
                {
                    fErechRechiv = 0;
                    fTosefetGil = 0;
                    iMisparSidur = int.Parse(_drSidurMeyuchad[I]["mispar_sidur"].ToString());
                    if (iMisparSidur != 99220 && (string.IsNullOrEmpty(_drSidurMeyuchad[I]["sidur_namlak_visa"].ToString())))
                    {
                        dShatHatchalaSidur = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_sidur"].ToString());
                      //  dShatHatchalaLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_letashlum"].ToString());
                        if (dShatHatchalaSidur < objOved.objParameters.dKnisatShabat)
                         {
                             fDakotNochehutSidur = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                             fNuchehutLepremia = fDakotNochehutSidur - oPeilut.getZmanHamtanaEilat(iMisparSidur, dShatHatchalaSidur);

                             fDakotHagdara = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotHagdara.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')")) / float.Parse("0.75");
                             fSumDakotSikun = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotSikun.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')")) / float.Parse("0.75");
                             fDakotHistaglut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotHistaglut.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                             fDakotKisuyTor = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotKisuiTor.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                             fDakotLepremia = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.SachDakotLepremia.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                             fSachNesiot = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.SachNesiot.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                             fErechRechiv = float.Parse((fDakotHagdara + fSumDakotSikun + fDakotHistaglut + fDakotKisuyTor + fDakotLepremia + (fSachNesiot * objOved.objParameters.fElementZar) + fTosefetGil - fNuchehutLepremia).ToString());

                             addAnyRowToTable(clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);
                         }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: " + ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurMeyuchad = null;
                _drSidurRagil = null;
            }
        }
      
        public void CalcRechiv203()
        {

            int iMisparSidur;
            float fErech216;
            DateTime dShatHatchalaSidur;
            DataRow[] _drSidurim;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            int iErechElementimReka;
            try
            {
                //oPeilut.objOved.Taarich = objOved.Taarich;

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");
                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                    if (dShatHatchalaSidur < objOved.objParameters.dKnisatShabat)
                    {
                        iErechElementimReka = oPeilut.CalcElementReka(iMisparSidur, dShatHatchalaSidur);

                        fErech216 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and KOD_RECHIV=" + clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        if (fErech216 > 0)
                        {
                            addRowToTable(clGeneral.enRechivim.DakotPremiaVisaShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, float.Parse(iErechElementimReka.ToString()));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotPremiaVisaShishi.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
             finally
            {
                _drSidurim=null;
            }
        
        }

        public void CalcRechiv207()
        {
            DataRow[] _drSidurim;
            int iMisparSidur, iOutMichsa;
            DateTime dShatHatchalaSidur;
            float fErech, fTafkidShishi;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    iOutMichsa = int.Parse(_drSidurim[I]["out_michsa"].ToString());

                    if (oCalcBL.CheckOutMichsa(objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, iOutMichsa))
                    {
                        fTafkidShishi = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                        if (fTafkidShishi > 0)
                        {
                            fErech = fTafkidShishi;
                            addRowToTable(clGeneral.enRechivim.MichutzLamichsaTafkidShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.MichutzLamichsaTafkidShishi.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public void CalcRechiv208()
        {
            DataRow[] _drSidurim;
            int iMisparSidur, iOutMichsa;
            DateTime dShatHatchalaSidur;
            float fErech, fTnuaShishi;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    iOutMichsa = int.Parse(_drSidurim[I]["out_michsa"].ToString());

                    if (oCalcBL.CheckOutMichsa(objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, iOutMichsa))
                    {
                        fTnuaShishi = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                        if (fTnuaShishi > 0)
                        {
                            fErech = fTnuaShishi;
                            addRowToTable(clGeneral.enRechivim.MichutzLamichsaTnuaShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.MichutzLamichsaTnuaShishi.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public float CalcRechiv209()
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur, dShatGmarSidur, dTempM1, dTempM2;
            float fErech;
            float fZmanAruchatTzharyim = 0;
            float fSumZmanAruchatTzharyim = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null and mispar_sidur=99001");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    dShatGmarSidur = DateTime.Parse(_drSidurim[I]["shat_gmar_sidur"].ToString());

                    fErech = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString());
                    addRowToTable(clGeneral.enRechivim.NochehutPremiaLerishum.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                    dTempM1 = clGeneral.GetDateTimeFromStringHour("12:30", objOved.Taarich.Date);
                    dTempM2 = clGeneral.GetDateTimeFromStringHour("13:30", objOved.Taarich.Date);

                    if (dShatHatchalaSidur <= dTempM1 && dShatGmarSidur >= dTempM1)
                    { fZmanAruchatTzharyim = float.Parse((dShatGmarSidur - dTempM1).TotalMinutes.ToString()); }
                    if (dShatHatchalaSidur >= dTempM1 && dShatHatchalaSidur <= dTempM2 && dShatGmarSidur >= dTempM2)
                    { fZmanAruchatTzharyim = float.Parse((dTempM2 - dShatHatchalaSidur).TotalMinutes.ToString()); }
                    if (dShatHatchalaSidur >= dTempM1 && dShatGmarSidur <= dTempM2)
                    { fZmanAruchatTzharyim = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString()); }
                    if (fZmanAruchatTzharyim > 30)
                    { fZmanAruchatTzharyim = 30; }
                    fSumZmanAruchatTzharyim += fZmanAruchatTzharyim;
                }
                return fSumZmanAruchatTzharyim;
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.NochehutPremiaLerishum.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public void CalcRechiv210(out float fZmanAruchatBoker, out float fZmanAruchatTzharim, out float fZmanAruchatErev)
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur, dShatGmarSidur, dShatHatchalaLetashlum, dTempM1, dTempM2;
            float fErech;
            fZmanAruchatBoker = 0;
            fZmanAruchatTzharim = 0;
            fZmanAruchatErev = 0;
            float fZmanAruchatBokerSidur = 0;
            float fZmanAruchatTzharimSidur = 0;
            float fZmanAruchatErevSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                if (!clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                {
                    if (objOved.objMeafyeneyOved.GetMeafyen(100).IntValue > 0)
                    {
                        _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur=99001");

                        for (int I = 0; I < _drSidurim.Length; I++)
                        {
                            iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                            dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                            dShatGmarSidur = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                            dShatHatchalaLetashlum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());

                            fErech = float.Parse((dShatGmarSidur - dShatHatchalaLetashlum).TotalMinutes.ToString());
                            addRowToTable(clGeneral.enRechivim.NochehutPremiaMevakrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                            //חישוב זמן ארוחת בוקר
                            dTempM1 = clGeneral.GetDateTimeFromStringHour("08:30", objOved.Taarich.Date);
                            dTempM2 = clGeneral.GetDateTimeFromStringHour("09:30", objOved.Taarich.Date);

                            if (dShatHatchalaLetashlum <= dTempM1 && dShatGmarSidur >= dTempM1)
                            { fZmanAruchatBokerSidur = float.Parse((dShatGmarSidur - dTempM1).TotalMinutes.ToString()); }
                            if (dShatHatchalaLetashlum >= dTempM1 && dShatHatchalaLetashlum <= dTempM2 && dShatGmarSidur >= dTempM2)
                            { fZmanAruchatBokerSidur = float.Parse((dTempM2 - dShatHatchalaSidur).TotalMinutes.ToString()); }
                            if (dShatHatchalaLetashlum >= dTempM1 && dShatGmarSidur <= dTempM2)
                            { fZmanAruchatBokerSidur = float.Parse((dShatGmarSidur - dShatHatchalaLetashlum).TotalMinutes.ToString()); }
                            
                            if (fZmanAruchatBokerSidur > 20)
                            { fZmanAruchatBokerSidur = 20; }
                            //else { fZmanAruchatBokerSidur = 0; }

                            //חישוב זמן ארוחת צהריים
                            dTempM1 = objOved.objParameters.dStartAruchatTzaharayim;
                            dTempM2 = objOved.objParameters.dEndAruchatTzaharayim;

                            if (dShatHatchalaSidur <= dTempM1 && dShatGmarSidur >= dTempM1)
                            { fZmanAruchatTzharimSidur = float.Parse((dShatGmarSidur - dTempM1).TotalMinutes.ToString()); }
                            if (dShatHatchalaSidur >= dTempM1 && dShatHatchalaSidur <= dTempM2 && dShatGmarSidur >= dTempM2)
                            { fZmanAruchatTzharimSidur = float.Parse((dTempM2 - dShatHatchalaSidur).TotalMinutes.ToString()); }
                            if (dShatHatchalaSidur >= dTempM1 && dShatGmarSidur <= dTempM2)
                            { fZmanAruchatTzharimSidur = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString()); }
                           
                            if (fZmanAruchatTzharimSidur > 30)
                            { fZmanAruchatTzharimSidur = 30; }
                            //else { fZmanAruchatTzharimSidur = 0; }

                            //חישוב זמן ארוחת ערב
                            dTempM1 = clGeneral.GetDateTimeFromStringHour("18:00", objOved.Taarich.Date);
                            dTempM2 = clGeneral.GetDateTimeFromStringHour("19:30", objOved.Taarich.Date);

                            if (dShatHatchalaSidur <= dTempM1 && dShatGmarSidur >= dTempM1)
                            { fZmanAruchatErevSidur = float.Parse((dShatGmarSidur - dTempM1).TotalMinutes.ToString()); }
                            if (dShatHatchalaSidur >= dTempM1 && dShatHatchalaSidur <= dTempM2 && dShatGmarSidur >= dTempM2)
                            { fZmanAruchatErevSidur = float.Parse((dTempM2 - dShatHatchalaSidur).TotalMinutes.ToString()); }
                            if (dShatHatchalaSidur >= dTempM1 && dShatGmarSidur <= dTempM2)
                            { fZmanAruchatErevSidur = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString()); }
                            
                            if (fZmanAruchatErevSidur > 20)
                            { fZmanAruchatErevSidur = 20; }
                            //else { fZmanAruchatErevSidur = 0; }

                            fZmanAruchatBoker += fZmanAruchatBokerSidur;
                            fZmanAruchatTzharim += fZmanAruchatTzharimSidur;
                            fZmanAruchatErev += fZmanAruchatErevSidur;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.NochehutPremiaMevakrim.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }


        public void CalcRechiv211_212(out float fZmanAruchatBoker, out float fZmanAruchatTzharim, out float fZmanAruchatErev, out int iBokerRechiv, out int iTzharyimRechiv, out int iErevRechiv)
        {
            DataRow[] _drSidurim;
            int iMisparSidur, iKodRechiv;
            DateTime dShatHatchalaSidur, dShatGmarSidur, dShatGmarLetashlum, dTempM1, dTempM2, dShatHatchalaLetaslum;
            float fErech;
            float fZmanAruchatBokerSidur = 0;
            float fZmanAruchatTzharimSidur = 0;
            float fZmanAruchatErevSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                fZmanAruchatBoker = 0;
                fZmanAruchatTzharim = 0;
                fZmanAruchatErev = 0;
                iBokerRechiv = 0;
                iErevRechiv = 0;
                iTzharyimRechiv = 0;

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null", "shat_hatchala_sidur asc");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    dShatGmarSidur = DateTime.Parse(_drSidurim[I]["shat_gmar_sidur"].ToString());
                    dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                    dShatHatchalaLetaslum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                    fErech = 0;
                    iKodRechiv = 0;
                    if (iMisparSidur == 99001 && (objOved.sSugYechida.ToLower() == "m_ms" || objOved.sSugYechida.ToLower() == "m_me"))
                    {
                        if (objOved.objPirteyOved.iAchsana == 1 || objOved.objPirteyOved.iAchsana == 2)
                        {
                            fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaSidur).TotalMinutes.ToString());
                            iKodRechiv = clGeneral.enRechivim.NochehutPremiaMeshekAchsana.GetHashCode();
                        }
                        else
                        {
                            fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaLetaslum).TotalMinutes.ToString());
                            iKodRechiv = clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode();
                        }
                    }
                    else if (iMisparSidur == 99222)
                    {
                        fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaLetaslum).TotalMinutes.ToString());
                        iKodRechiv = clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode();
                    }
                    else if (iMisparSidur == 99221)
                    {
                        fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaSidur).TotalMinutes.ToString());
                        iKodRechiv = clGeneral.enRechivim.NochehutPremiaMeshekAchsana.GetHashCode();
                    }

                    addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fErech);

                    //if (iKodRechiv == clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode())
                    //{ dShatHatchalaSidur = dShatHatchalaLetaslum; }

                    ////dTempM1 = clGeneral.GetDateTimeFromStringHour("09:00", objOved.Taarich.Date);
                    ////dTempM2 = clGeneral.GetDateTimeFromStringHour("09:20", objOved.Taarich.Date);

                    ////if (dShatHatchalaSidur <= dTempM1 && dShatGmarLetashlum >= dTempM1)
                    ////{ fZmanAruchatBokerSidur = float.Parse((dShatGmarLetashlum - dTempM1).TotalMinutes.ToString()); }
                    ////if (dShatHatchalaSidur >= dTempM1 && dShatHatchalaSidur <= dTempM2 && dShatGmarLetashlum >= dTempM2)
                    ////{ fZmanAruchatBokerSidur = float.Parse((dTempM2 - dShatHatchalaSidur).TotalMinutes.ToString()); }
                    ////if (dShatHatchalaSidur >= dTempM1 && dShatGmarLetashlum <= dTempM2)
                    ////{ fZmanAruchatBokerSidur = float.Parse((dShatGmarLetashlum - dShatHatchalaSidur).TotalMinutes.ToString()); }
                    ////if (fZmanAruchatBokerSidur > 20)
                    ////{ fZmanAruchatBokerSidur = 20; }
                    ////else { fZmanAruchatBokerSidur = 0; }

                    //////חישוב זמן ארוחת צהריים
                    ////dTempM1 = objOved.objParameters.dStartAruchatTzaharayim;
                    ////dTempM2 = objOved.objParameters.dEndAruchatTzaharayim;

                    ////if (dShatHatchalaSidur <= dTempM1 && dShatGmarLetashlum >= dTempM1)
                    ////{ fZmanAruchatTzharimSidur = float.Parse((dShatGmarLetashlum - dTempM1).TotalMinutes.ToString()); }
                    ////if (dShatHatchalaSidur >= dTempM1 && dShatHatchalaSidur <= dTempM2 && dShatGmarLetashlum >= dTempM2)
                    ////{ fZmanAruchatTzharimSidur = float.Parse((dTempM2 - dShatHatchalaSidur).TotalMinutes.ToString()); }
                    ////if (dShatHatchalaSidur >= dTempM1 && dShatGmarLetashlum <= dTempM2)
                    ////{ fZmanAruchatTzharimSidur = float.Parse((dShatGmarLetashlum - dShatHatchalaSidur).TotalMinutes.ToString()); }
                    ////if (fZmanAruchatTzharimSidur > 30)
                    ////{ fZmanAruchatTzharimSidur = 30; }
                    ////else { fZmanAruchatTzharimSidur = 0; }

                    //////חישוב זמן ארוחת ערב
                    ////dTempM1 = clGeneral.GetDateTimeFromStringHour("19:00", objOved.Taarich.Date);
                    ////dTempM2 = clGeneral.GetDateTimeFromStringHour("19:20", objOved.Taarich.Date);

                    ////if (dShatHatchalaSidur <= dTempM1 && dShatGmarLetashlum >= dTempM1)
                    ////{ fZmanAruchatErevSidur = float.Parse((dShatGmarLetashlum - dTempM1).TotalMinutes.ToString()); }
                    ////if (dShatHatchalaSidur >= dTempM1 && dShatHatchalaSidur <= dTempM2 && dShatGmarLetashlum >= dTempM2)
                    ////{ fZmanAruchatErevSidur = float.Parse((dTempM2 - dShatHatchalaSidur).TotalMinutes.ToString()); }
                    ////if (dShatHatchalaSidur >= dTempM1 && dShatGmarLetashlum <= dTempM2)
                    ////{ fZmanAruchatErevSidur = float.Parse((dShatGmarLetashlum - dShatHatchalaSidur).TotalMinutes.ToString()); }
                    ////if (fZmanAruchatErevSidur > 20)
                    ////{ fZmanAruchatErevSidur = 20; }
                    ////else { fZmanAruchatErevSidur = 0; }

                    ////if (fZmanAruchatBoker == 0 && fZmanAruchatBokerSidur > 0)
                    ////{
                    ////    iBokerRechiv = iKodRechiv;
                    ////}
                    ////if (fZmanAruchatTzharim == 0 && fZmanAruchatTzharimSidur > 0)
                    ////{
                    ////    iTzharyimRechiv = iKodRechiv;
                    ////}
                    ////if (fZmanAruchatErev == 0 && fZmanAruchatErevSidur > 0)
                    ////{
                    ////    iErevRechiv = iKodRechiv;
                    ////}
                    ////fZmanAruchatBoker += fZmanAruchatBokerSidur;
                    ////fZmanAruchatTzharim += fZmanAruchatTzharimSidur;
                    ////fZmanAruchatErev += fZmanAruchatErevSidur;

                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }


        public void CalcRechiv276()
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur;
            DateTime dShatGmarLetashlum;
            float fErechRechiv;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                 _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                  
                    if (iMisparSidur == 99220)
                    {
                        dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                        dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());

                        fErechRechiv = float.Parse((dShatGmarLetashlum - dShatHatchalaSidur).TotalMinutes.ToString());

                        addRowToTable(clGeneral.enRechivim.NochechutLePremiyaMeshekGrira.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);
                    } 
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.NochechutLePremiyaMeshekGrira.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public void CalcRechiv277()
        {
            DataRow[] _drSidurim;
            int iMisparSidur, iSugSidur;
            DateTime dShatHatchalaSidur;
            DateTime dShatGmarLetashlum;
            float fErechRechiv;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    iSugSidur = int.Parse(_drSidurim[I]["sug_sidur"].ToString());

                    if (iSugSidur == 69)
                    {
                        dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                        dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());

                        fErechRechiv = float.Parse((dShatGmarLetashlum - dShatHatchalaSidur).TotalMinutes.ToString());

                        addRowToTable(clGeneral.enRechivim.NochechutLePremiyaMeshekKonenutGrira.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, objOved.Mispar_ishi, "E", clGeneral.enRechivim.NochechutLePremiyaMeshekKonenutGrira.GetHashCode(), objOved.Taarich, "CalcDay: " + ex.StackTrace + "\n message: "+ ex.Message);
                throw (ex);
            }
        }
        public void CalcRechiv213()
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur;
            float fErech;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                //oPeilut.objOved.Taarich = objOved.Taarich;
                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                    fErech = oPeilut.CalcRechiv213(iMisparSidur, dShatHatchalaSidur);

                    addRowToTable(clGeneral.enRechivim.SachNesiot.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.SachNesiot.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public void CalcRechiv214()
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur;
            float fErech;
            bool bSidurNehiga; 
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                //oPeilut.objOved.Taarich = objOved.Taarich;

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    bSidurNehiga = false;
                    bSidurNehiga = isSidurNehiga(ref _drSidurim[I]);
                    if (bSidurNehiga)
                    {
                        iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                        oPeilut.CalcRechiv214(iMisparSidur, dShatHatchalaSidur);
                        fErech = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_PEILUT"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND KOD_RECHIV=" + clGeneral.enRechivim.DakotHistaglut.GetHashCode().ToString() + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                        addRowToTable(clGeneral.enRechivim.DakotHistaglut.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                    }
                }


            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotHistaglut.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public void CalcRechiv215()
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur;
            float fErech;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                //oPeilut.objOved.Taarich = objOved.Taarich;

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                    oPeilut.CalcRechiv215(iMisparSidur, dShatHatchalaSidur);
                    fErech = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_PEILUT"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND KOD_RECHIV=" + clGeneral.enRechivim.SachKM.GetHashCode().ToString() + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                    addRowToTable(clGeneral.enRechivim.SachKM.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.SachKM.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public void CalcRechiv216()
        {
            DataRow[] _drSidurMeyuchad;
            int iMisparSidur;
            DateTime dShatHatchalaSidur, dShatHatchalaLetashlum, dShatGmarLetashlum;
            float fErech;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {

                _drSidurMeyuchad = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and not sidur_namlak_visa is null");

                for (int I = 0; I <= _drSidurMeyuchad.Length - 1; I++)
                {
                    iMisparSidur = int.Parse(_drSidurMeyuchad[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_sidur"].ToString());
                    dShatHatchalaLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_letashlum"].ToString());
                    dShatGmarLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_gmar_letashlum"].ToString());

                    oPeilut.CalcRechiv216(iMisparSidur, dShatHatchalaSidur);
                    fErech = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_PEILUT"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND KOD_RECHIV=" + clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode().ToString() + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                    fErech = fErech * (1 - float.Parse(_drSidurMeyuchad[I]["Achuz_knas_lepremyat_visa"].ToString()) / 100) * (1 + float.Parse(_drSidurMeyuchad[I]["ACHUZ_VIZA_BESIKUN"].ToString()) / 100);

                    addRowToTable(clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurMeyuchad = null;
            }
        }


        public void CalcRechiv217()
        {
            DataRow[] _drSidurim;
            int iMisparSidur, iMisparSidurFirst;
            DateTime dShatHatchalaSidur, dShatHatchalaSidurFirst;
            float fErech;
            dShatHatchalaSidur = DateTime.MinValue;
            dShatHatchalaSidurFirst = DateTime.MinValue;
            iMisparSidur = 0;
            bool bFirstSidur = false;
            iMisparSidurFirst = 0;
            try
            {
                //oPeilut.objOved.Taarich = objOved.Taarich;

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("mispar_sidur is not null", "shat_hatchala_sidur asc");
                if (_drSidurim.Length > 0)
                {
                    iMisparSidurFirst = int.Parse(_drSidurim[0]["mispar_sidur"].ToString());
                    dShatHatchalaSidurFirst = DateTime.Parse(_drSidurim[0]["shat_hatchala_sidur"].ToString());
                }
                _drSidurim = null;
                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    if (iMisparSidur == iMisparSidurFirst && dShatHatchalaSidur == dShatHatchalaSidurFirst)
                        bFirstSidur = true;
                    oPeilut.CalcRechiv217(iMisparSidur, dShatHatchalaSidur, bFirstSidur);
                    fErech = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_PEILUT"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND KOD_RECHIV=" + clGeneral.enRechivim.DakotHagdara.GetHashCode().ToString() + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                    addRowToTable(clGeneral.enRechivim.DakotHagdara.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotHagdara.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public void CalcRechiv218()
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur;
            float fErech;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
              //  //oPeilut.objOved.Taarich = objOved.Taarich;

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                    oPeilut.CalcRechiv218(iMisparSidur, dShatHatchalaSidur);
                    fErech = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_PEILUT"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND KOD_RECHIV=" + clGeneral.enRechivim.DakotKisuiTor.GetHashCode().ToString() + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                    addRowToTable(clGeneral.enRechivim.DakotKisuiTor.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotKisuiTor.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public float CalcRechiv223()
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur, dShatGmarSidur, dTempM1, dTempM2;
            float fErech;
            float fZmanAruchatTzharyim = 0;
            float fSumZmanAruchatTzharyim = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur=99001");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    dShatGmarSidur = DateTime.Parse(_drSidurim[I]["shat_gmar_sidur"].ToString());

                    fErech = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString());
                    addRowToTable(clGeneral.enRechivim.NochehutLepremiaMachsanKartisim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                    dTempM1 = clGeneral.GetDateTimeFromStringHour("12:30", objOved.Taarich.Date);
                    dTempM2 = clGeneral.GetDateTimeFromStringHour("13:30", objOved.Taarich.Date);

                    if (dShatHatchalaSidur <= dTempM1 && dShatGmarSidur >= dTempM1)
                    { fZmanAruchatTzharyim = float.Parse((dShatGmarSidur - dTempM1).TotalMinutes.ToString()); }
                    if (dShatHatchalaSidur >= dTempM1 && dShatHatchalaSidur <= dTempM2 && dShatGmarSidur >= dTempM2)
                    { fZmanAruchatTzharyim = float.Parse((dTempM2 - dShatHatchalaSidur).TotalMinutes.ToString()); }
                    if (dShatHatchalaSidur >= dTempM1 && dShatGmarSidur <= dTempM2)
                    { fZmanAruchatTzharyim = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString()); }
                    if (fZmanAruchatTzharyim > 30)
                    { fZmanAruchatTzharyim = 30; }
                    fSumZmanAruchatTzharyim += fZmanAruchatTzharyim;
                }
                return fSumZmanAruchatTzharyim;
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.NochehutLepremiaMachsanKartisim.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }



        public void CalcRechiv235()
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur, dShatGmarLetashlum, dShatHatchalaLetashlum;
            float fErech;
            string sSidurimMeyuchadim = "", sQury;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                //if (objOved.objMeafyeneyOved.iMeafyen103 > 0)
                //{
                    sSidurimMeyuchadim = GetSidurimMeyuchRechiv(clGeneral.enRechivim.NochehutLePremyatNehageyTenderim.GetHashCode());
                    if (sSidurimMeyuchadim.Length > 0)
                    {
                        _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ")");
                        for (int I = 0; I < _drSidurim.Length; I++)
                        {
                            iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                            dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                            dShatHatchalaLetashlum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                            dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());

                            fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());
                            addRowToTable(clGeneral.enRechivim.NochehutLePremyatNehageyTenderim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                        }
                    }

                   
                        _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and MISPAR_SIDUR NOT IN(" + sSidurimMeyuchadim + ")");
                        for (int I = 0; I < _drSidurim.Length; I++)
                        {
                            iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                            dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                            oPeilut.CalcRechiv235(iMisparSidur, dShatHatchalaSidur);
                            fErech = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_PEILUT"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.NochehutLePremyatNehageyTenderim.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                            if (fErech > 0)
                                addRowToTable(clGeneral.enRechivim.NochehutLePremyatNehageyTenderim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                        }

                    //}
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.NochehutLePremyatNehageyTenderim.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }


        public float CalcRechiv246()
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur, dShatGmarSidur, dTempM1, dTempM2;
            float fErech;
            float fZmanAruchatTzharyim = 0;
            float fSumZmanAruchatTzharyim = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur=99001");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    dShatGmarSidur = DateTime.Parse(_drSidurim[I]["shat_gmar_sidur"].ToString());

                    fErech = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString());
                    addRowToTable(clGeneral.enRechivim.NochechutLepremyaMenahel.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                    dTempM1 = clGeneral.GetDateTimeFromStringHour("12:30", objOved.Taarich.Date);
                    dTempM2 = clGeneral.GetDateTimeFromStringHour("13:30", objOved.Taarich.Date);

                    if (dShatHatchalaSidur <= dTempM1 && dShatGmarSidur >= dTempM1)
                    { fZmanAruchatTzharyim = float.Parse((dShatGmarSidur - dTempM1).TotalMinutes.ToString()); }
                    if (dShatHatchalaSidur >= dTempM1 && dShatHatchalaSidur <= dTempM2 && dShatGmarSidur >= dTempM2)
                    { fZmanAruchatTzharyim = float.Parse((dTempM2 - dShatHatchalaSidur).TotalMinutes.ToString()); }
                    if (dShatHatchalaSidur >= dTempM1 && dShatGmarSidur <= dTempM2)
                    { fZmanAruchatTzharyim = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString()); }
                    if (fZmanAruchatTzharyim > 30)
                    { fZmanAruchatTzharyim = 30; }
                    fSumZmanAruchatTzharyim += fZmanAruchatTzharyim;
                }
                return fSumZmanAruchatTzharyim;
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.NochechutLepremyaMenahel.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public void CalcRechiv256( out float fZmanAruchatTzharim,  out int iErevRechiv)
        {
            DataRow[] _drSidurim;
            int iMisparSidur, iMaxDakot=30;
            DateTime dShatHatchalaSidur, dShatGmarSidur, dShatGmarLetashlum, dShatHatchalaLetaslum;
            float fErech,fTemp;
            float fZmanAruchatTzharimSidur = 0;

            string sSidurimLerchiv = "";
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {        
                fZmanAruchatTzharim = 0;
                iErevRechiv = 0;
                fTemp = 0;
                

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null", "shat_hatchala_sidur asc");
                sSidurimLerchiv = GetSidurimMeyuchRechiv(clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode());

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    fZmanAruchatTzharimSidur = 0;
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    dShatGmarSidur = DateTime.Parse(_drSidurim[I]["shat_gmar_sidur"].ToString());
                    dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                    dShatHatchalaLetaslum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());

                    if (!clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich) &&
                        ((iMisparSidur == 99001 && (objOved.objPirteyOved.iIsuk == 17 || objOved.objPirteyOved.iIsuk == 18 || objOved.objPirteyOved.iIsuk == 19 || objOved.objPirteyOved.iIsuk == 20)) || (sSidurimLerchiv.IndexOf(iMisparSidur.ToString()) > -1)))
                    {
                       // fErech = float.Parse(_drSidurim[I]["ZMAN_LELO_HAFSAKA"].ToString());// float.Parse((dShatGmarLetashlum - dShatHatchalaLetaslum).TotalMinutes.ToString());
                        fErech = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString());
                        if (_drSidurim[I]["ZMAN_HAFSAKA_BESIDUR"].ToString().Trim() != "")
                            fErech -= float.Parse(_drSidurim[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());
                       
                        if (!oCalcBL.CheckYomShishi(objOved.SugYom))
                        {
                            if (objOved.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.SachirKavua.GetHashCode())
                                iMaxDakot = 18;

                            CalcZmaneyAruchot(dShatHatchalaLetaslum, dShatGmarLetashlum, out fZmanAruchatTzharimSidur, iMaxDakot);
                            fTemp = Math.Min(fZmanAruchatTzharimSidur, iMaxDakot - fZmanAruchatTzharim - objOved.fTotalAruchatZaharimForDay);
                            fZmanAruchatTzharim += fTemp;
                        }
                        fErech = fErech - fTemp;
                        addRowToTable(clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                    }
                    //if (iMisparSidur == 99202)
                    //{
                    //    fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaLetaslum).TotalMinutes.ToString());
                    //    if (_drSidurim[I]["ZMAN_HAFSAKA_BESIDUR"].ToString().Trim() != "")
                    //        fErech -= float.Parse(_drSidurim[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());
                    //    addRowToTable(clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                    //}
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }


        private void CalcZmaneyAruchot(DateTime dShatHatchalaLetaslum, DateTime dShatGmarLetashlum, out float fZmanAruchatTzharim, int iMaxDakot)
        {
            float fTempX = 0;
            DateTime dTempM1, dTempM2;

            try
            {
                //dTempM1 = clGeneral.GetDateTimeFromStringHour("08:00", objOved.Taarich.Date);
                //dTempM2 = clGeneral.GetDateTimeFromStringHour("09:30", objOved.Taarich.Date);
                //fTempX = 0;
                //if (dShatHatchalaLetaslum <= dTempM1 && dShatGmarLetashlum >= dTempM1)
                //{ fTempX = float.Parse((dShatGmarLetashlum - dTempM1).TotalMinutes.ToString()); }
                //if (dShatHatchalaLetaslum >= dTempM1 && dShatHatchalaLetaslum <= dTempM2 && dShatGmarLetashlum >= dTempM2)
                //{ fTempX = float.Parse((dTempM2 - dShatHatchalaLetaslum).TotalMinutes.ToString()); }
                //if (dShatHatchalaLetaslum >= dTempM1 && dShatGmarLetashlum <= dTempM2)
                //{ fTempX = float.Parse((dShatGmarLetashlum - dShatHatchalaLetaslum).TotalMinutes.ToString()); }
                //if (fTempX > 20)
                //{ fZmanAruchatBoker = 20; }
                //else { fZmanAruchatBoker = fTempX; }

                //חישוב זמן ארוחת צהריים
                dTempM1 = objOved.objParameters.dStartAruchatTzaharayim;
                dTempM2 = objOved.objParameters.dEndAruchatTzaharayim;
                fTempX = 0;
                if (dShatHatchalaLetaslum <= dTempM1 && dShatGmarLetashlum >= dTempM1)
                { fTempX = float.Parse((dShatGmarLetashlum - dTempM1).TotalMinutes.ToString()); }
                if (dShatHatchalaLetaslum >= dTempM1 && dShatHatchalaLetaslum <= dTempM2 && dShatGmarLetashlum >= dTempM2)
                { fTempX = float.Parse((dTempM2 - dShatHatchalaLetaslum).TotalMinutes.ToString()); }
                if (dShatHatchalaLetaslum >= dTempM1 && dShatGmarLetashlum <= dTempM2)
                { fTempX = float.Parse((dShatGmarLetashlum - dShatHatchalaLetaslum).TotalMinutes.ToString()); }

                fZmanAruchatTzharim = (fTempX > iMaxDakot) ? iMaxDakot : fTempX;
           
                //חישוב זמן ארוחת ערב
                //dTempM1 = clGeneral.GetDateTimeFromStringHour("18:00", objOved.Taarich.Date);
                //dTempM2 = clGeneral.GetDateTimeFromStringHour("19:30", objOved.Taarich.Date);
                //fTempX = 0;
                //if (dShatHatchalaLetaslum <= dTempM1 && dShatGmarLetashlum >= dTempM1)
                //{ fTempX = float.Parse((dShatGmarLetashlum - dTempM1).TotalMinutes.ToString()); }
                //if (dShatHatchalaLetaslum >= dTempM1 && dShatHatchalaLetaslum <= dTempM2 && dShatGmarLetashlum >= dTempM2)
                //{ fTempX = float.Parse((dTempM2 - dShatHatchalaLetaslum).TotalMinutes.ToString()); }
                //if (dShatHatchalaLetaslum >= dTempM1 && dShatGmarLetashlum <= dTempM2)
                //{ fTempX = float.Parse((dShatGmarLetashlum - dShatHatchalaLetaslum).TotalMinutes.ToString()); }
                //if (fTempX > 20)
                //{ fZmanAruchatErev = 20; }
                //else { fZmanAruchatErev = fTempX; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void CalcRechiv257( out float fZmanAruchatTzharim, out int iErevRechiv)
        {
            DataRow[] _drSidurim;
            int iMisparSidur, iMaxDakot=30;
            DateTime dShatHatchalaSidur, dShatGmarSidur, dShatGmarLetashlum, dShatHatchalaLetaslum;
            float fErech,fTemp;
            float fZmanAruchatTzharimSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {

                fZmanAruchatTzharim = 0;
                iErevRechiv = 0;
                fTemp=0;

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null", "shat_hatchala_sidur asc");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    fZmanAruchatTzharimSidur = 0;
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    dShatGmarSidur = DateTime.Parse(_drSidurim[I]["shat_gmar_sidur"].ToString());
                    dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                    dShatHatchalaLetaslum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());

                    if ((!clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich) && iMisparSidur == 99001 && (objOved.objPirteyOved.iIsuk == 181 || objOved.objPirteyOved.iIsuk == 191)))
                    {
                        //fErech = float.Parse(_drSidurim[I]["ZMAN_LELO_HAFSAKA"].ToString()); // float.Parse((dShatGmarLetashlum - dShatHatchalaLetaslum).TotalMinutes.ToString());
                        fErech = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString());
                        if (_drSidurim[I]["ZMAN_HAFSAKA_BESIDUR"].ToString().Trim() != "")
                            fErech -= float.Parse(_drSidurim[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());
                       
                        if (!oCalcBL.CheckYomShishi(objOved.SugYom))
                        {
                            if (objOved.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.SachirKavua.GetHashCode())
                                iMaxDakot = 18;

                            CalcZmaneyAruchot(dShatHatchalaLetaslum, dShatGmarLetashlum, out fZmanAruchatTzharimSidur, iMaxDakot);
                            fTemp = Math.Min(fZmanAruchatTzharimSidur, iMaxDakot - fZmanAruchatTzharim - objOved.fTotalAruchatZaharimForDay);
                            fZmanAruchatTzharim += fTemp;
                        }

                        fErech = fErech - fTemp;
                        addRowToTable(clGeneral.enRechivim.NochehutLepremiaMetachnenTnua.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.NochehutLepremiaMetachnenTnua.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }




        public void CalcRechiv73()
        {
            int iMisparSidur;
            DateTime dShatHatchalaSidur;
            DataRow[] drSidurim;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            float fDakotNochehut;
            try
            {

                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur=99006");
                for (int I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
              
                    fDakotNochehut = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    if (fDakotNochehut>0)
                    {
                        addRowToTable(clGeneral.enRechivim.YomShlichutBeChul.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fDakotNochehut);
                       
                    } 
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.YomShlichutBeChul.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: " + ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        public void CalcRechiv258( out float fZmanAruchatTzharim, out int iErevRechiv)
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur, dShatGmarSidur, dShatGmarLetashlum, dShatHatchalaLetaslum;
            float fErech, fTemp, fZmanHafsaka;
            float fZmanAruchatTzharimSidur = 0;
            string sSidurimLerchiv = "";
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            int iSugSidur, iMaxDakot=30;
            bool bYeshSidur;
            string sIsuk, sQury;
            DataRow[] drPeiluyot;
            // DataTable dtPeiluyot;
            try
            {
                fZmanAruchatTzharim = 0;
                iErevRechiv = 0;
                fTemp = 0;
                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null", "shat_hatchala_sidur asc");
                sSidurimLerchiv = GetSidurimMeyuchRechiv(clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode());

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    fZmanAruchatTzharimSidur = 0;
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    dShatGmarSidur = DateTime.Parse(_drSidurim[I]["shat_gmar_sidur"].ToString());
                    dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                    dShatHatchalaLetaslum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                    fErech = 0;
                    if (!clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                    {
                        if ((iMisparSidur == 99204 || iMisparSidur == 99224) && objOved.objPirteyOved.iIsuk == 420)
                        {
                            iMaxDakot = 30;
                            fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaLetaslum).TotalMinutes.ToString());
                            fZmanHafsaka = float.Parse(_drSidurim[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());
                            CalcZmaneyAruchot(dShatHatchalaLetaslum, dShatGmarLetashlum, out  fZmanAruchatTzharimSidur, iMaxDakot);
                        }
                        else
                        {
                            sQury = "MISPAR_SIDUR=" + iMisparSidur + " and SHAT_HATCHALA_SIDUR=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and (SUBSTRING(makat_nesia,1,3)<>'740') ";
                        
                            sIsuk = ",401, 402, 403,421, 422 ,404,412,420,";
                            //oPeilut.objOved.Taarich = objOved.Taarich;
                            // dtPeiluyot = oPeilut.GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);
                            if (objOved.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.SachirKavua.GetHashCode())
                                iMaxDakot = 18;
                            else iMaxDakot = 30;
                            drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur, "(SUBSTRING(makat_nesia,1,3)='740')");

                            if (drPeiluyot.Length > 0)
                            {
                                if (iMisparSidur.ToString().Substring(0, 2) != "99")
                                {
                                    //SetSugSidur(ref _drSidurim[I], objOved.Taarich, iMisparSidur);

                                    iSugSidur = int.Parse(_drSidurim[I]["sug_sidur"].ToString());
                                    bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nihul.GetHashCode(), objOved.Taarich, iSugSidur);
                                    if (bYeshSidur)
                                    {
                                        fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaLetaslum).TotalMinutes.ToString());

                                        if (getPeiluyot(iMisparSidur, dShatHatchalaSidur, "(SUBSTRING(makat_nesia,1,3)<>'740')").Length > 0)
                                            fErech -= oCalcBL.GetSumErechRechiv(objOved.DtPeiluyotYomi.Compute("sum(zmanElement)", sQury));

                                        oPeilut.CalcZmaneyAruchot(drPeiluyot, dShatHatchalaLetaslum, dShatGmarLetashlum, out  fZmanAruchatTzharimSidur, iMaxDakot);
                                    }
                                }
                                else if (iMisparSidur == 99402 && sIsuk.IndexOf("," + objOved.objPirteyOved.iIsuk.ToString() + ",") > -1)
                                {
                                    fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaLetaslum).TotalMinutes.ToString());

                                    if (getPeiluyot(iMisparSidur, dShatHatchalaSidur, "(SUBSTRING(makat_nesia,1,3)<>'740')").Length > 0)
                                        fErech -= oCalcBL.GetSumErechRechiv(objOved.DtPeiluyotYomi.Compute("sum(zmanElement)", sQury));

                                    oPeilut.CalcZmaneyAruchot(drPeiluyot, dShatHatchalaLetaslum, dShatGmarLetashlum, out  fZmanAruchatTzharimSidur, iMaxDakot);
                                }
                            }
                        }

                       
                        if (!oCalcBL.CheckYomShishi(objOved.SugYom))
                        {
                            fTemp = Math.Min(fZmanAruchatTzharimSidur, iMaxDakot - fZmanAruchatTzharim - objOved.fTotalAruchatZaharimForDay);
                            fZmanAruchatTzharim += fTemp;
                            //     fZmanAruchatTzharim += fZmanAruchatTzharimSidur;
                        }
                        fErech = fErech - fTemp;
                        addRowToTable(clGeneral.enRechivim.NochehutLepremiaSadran.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                           
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.NochehutLepremiaSadran.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
                drPeiluyot = null;
            }
        }


        public void CalcRechiv259( out float fZmanAruchatTzharim, out int iErevRechiv)
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur, dShatGmarSidur, dShatGmarLetashlum, dShatHatchalaLetaslum;
            float fErech, fTemp;
            float fZmanAruchatTzharimSidur = 0;
            string sSidurimLerchiv = "";
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            int iSugSidur, iMaxDakot=30;
            bool bYeshSidur;
            string sIsuk, sQury;
            DataRow[] drPeiluyot;
            //   DataTable dtPeiluyot;
            try
            {
                fZmanAruchatTzharim = 0;
                iErevRechiv = 0;
                fTemp = 0;
                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null", "shat_hatchala_sidur asc");
                sSidurimLerchiv = GetSidurimMeyuchRechiv(clGeneral.enRechivim.NochehutLepremiaRakaz.GetHashCode());

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    fZmanAruchatTzharimSidur = 0;
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    dShatGmarSidur = DateTime.Parse(_drSidurim[I]["shat_gmar_sidur"].ToString());
                    dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                    dShatHatchalaLetaslum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                    fErech = 0;
                    if (!clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                    {
                        sQury = "MISPAR_SIDUR=" + iMisparSidur + "  and SHAT_HATCHALA_SIDUR=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and (SUBSTRING(makat_nesia,1,3)<>'730')";
                            
                        sIsuk = ",401, 402, 403,421, 422 ,404,412,420,";
                        //oPeilut.objOved.Taarich = objOved.Taarich;
                        //   dtPeiluyot = oPeilut.GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);
                        if (objOved.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.SachirKavua.GetHashCode())
                            iMaxDakot = 18;
                        else iMaxDakot = 30;
                        
                       
                        drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur, "(SUBSTRING(makat_nesia,1,3)='730')");
                        if (drPeiluyot.Length > 0)
                        {
                            if (iMisparSidur.ToString().Substring(0, 2) != "99")
                            {
                                //SetSugSidur(ref _drSidurim[I], objOved.Taarich, iMisparSidur);

                                iSugSidur = int.Parse(_drSidurim[I]["sug_sidur"].ToString());
                                bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nihul.GetHashCode(), objOved.Taarich, iSugSidur);
                                if (bYeshSidur)
                                {
                                    fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaLetaslum).TotalMinutes.ToString());

                                    if (getPeiluyot(iMisparSidur, dShatHatchalaSidur, "(SUBSTRING(makat_nesia,1,3)<>'730')").Length > 0)
                                        fErech = oCalcBL.GetSumErechRechiv(objOved.DtPeiluyotYomi.Compute("sum(zmanElement)", sQury));

                                    oPeilut.CalcZmaneyAruchot(drPeiluyot, dShatHatchalaLetaslum, dShatGmarLetashlum, out  fZmanAruchatTzharimSidur, iMaxDakot);
                                }
                            }
                            else if (iMisparSidur == 99402 && sIsuk.IndexOf("," + objOved.objPirteyOved.iIsuk.ToString() + ",") > -1)
                            {
                                fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaLetaslum).TotalMinutes.ToString());

                                if (getPeiluyot(iMisparSidur, dShatHatchalaSidur, "(SUBSTRING(makat_nesia,1,3)<>'730')").Length > 0)
                                    fErech = oCalcBL.GetSumErechRechiv(objOved.DtPeiluyotYomi.Compute("sum(zmanElement)", sQury));

                                oPeilut.CalcZmaneyAruchot(drPeiluyot, dShatHatchalaLetaslum, dShatGmarLetashlum, out  fZmanAruchatTzharimSidur, iMaxDakot);
                            }
                        }
                       if (!oCalcBL.CheckYomShishi(objOved.SugYom))
                        {
                            fTemp = Math.Min(fZmanAruchatTzharimSidur, 30 - fZmanAruchatTzharim - objOved.fTotalAruchatZaharimForDay);
                            fZmanAruchatTzharim += fTemp;
                        }
                        fErech = fErech - fTemp;
                        addRowToTable(clGeneral.enRechivim.NochehutLepremiaRakaz.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                       
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.NochehutLepremiaRakaz.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
                drPeiluyot = null;
            }
        }


        public void CalcRechiv260( out float fZmanAruchatTzharim, out int iErevRechiv)
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur, dShatGmarSidur, dShatGmarLetashlum, dShatHatchalaLetaslum;
            float fErech, fTemp, fZmanHafsaka;
            float fZmanAruchatTzharimSidur = 0;
            string sSidurimLerchiv = "";
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            int iSugSidur, iMaxDakot=30;
            bool bYeshSidur;
            string sIsuk, sQury;
            DataRow[] drPeiluyot;
            //  DataTable dtPeiluyot;
            try
            {
                fZmanAruchatTzharim = 0;
                iErevRechiv = 0;
                fTemp = 0;
                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null", "shat_hatchala_sidur asc");
                sSidurimLerchiv = GetSidurimMeyuchRechiv(clGeneral.enRechivim.NochehutLepremiaPakach.GetHashCode());

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    fZmanAruchatTzharimSidur = 0;
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    dShatGmarSidur = DateTime.Parse(_drSidurim[I]["shat_gmar_sidur"].ToString());
                    dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                    dShatHatchalaLetaslum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                    fErech = 0;
                    if (!clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                    {
                        if ((iMisparSidur == 99205 || iMisparSidur == 99225 || iMisparSidur==99216) && (objOved.objPirteyOved.iIsuk == 401 || objOved.objPirteyOved.iIsuk == 422))
                        {
                            iMaxDakot = 30;

                            fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaLetaslum).TotalMinutes.ToString());
                            fZmanHafsaka = float.Parse(_drSidurim[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());
                            CalcZmaneyAruchot(dShatHatchalaLetaslum, dShatGmarLetashlum, out  fZmanAruchatTzharimSidur, iMaxDakot);

                        }
                        else
                        {
                            sIsuk = ",401, 402, 403,421, 422 ,404,412,420,";
                            //oPeilut.objOved.Taarich = objOved.Taarich;
                            // dtPeiluyot = oPeilut.GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);
                            sQury = "MISPAR_SIDUR=" + iMisparSidur + " and SHAT_HATCHALA_SIDUR=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and (SUBSTRING(makat_nesia,1,3)<>'750')";
                           
                            drPeiluyot = getPeiluyot(iMisparSidur, dShatHatchalaSidur, "(SUBSTRING(makat_nesia,1,3)='750')");

                            if (objOved.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.SachirKavua.GetHashCode())
                                iMaxDakot = 18;
                            else iMaxDakot = 30;

                            if (drPeiluyot.Length > 0)
                            {
                                if (iMisparSidur.ToString().Substring(0, 2) != "99")
                                {
                                    //SetSugSidur(ref _drSidurim[I], objOved.Taarich, iMisparSidur);

                                    iSugSidur = int.Parse(_drSidurim[I]["sug_sidur"].ToString());
                                    bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nihul.GetHashCode(), objOved.Taarich, iSugSidur);
                                    if (bYeshSidur)
                                    {
                                        fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaLetaslum).TotalMinutes.ToString());

                                        if (getPeiluyot(iMisparSidur, dShatHatchalaSidur, "(SUBSTRING(makat_nesia,1,3)<>'750')").Length > 0)
                                            fErech = oCalcBL.GetSumErechRechiv(objOved.DtPeiluyotYomi.Compute("sum(zmanElement)", sQury));

                                        oPeilut.CalcZmaneyAruchot(drPeiluyot, dShatHatchalaLetaslum, dShatGmarLetashlum, out  fZmanAruchatTzharimSidur, iMaxDakot);
                                    }
                                }
                                else if (iMisparSidur == 99402 && sIsuk.IndexOf("," + objOved.objPirteyOved.iIsuk.ToString() + ",") > -1)
                                {
                                    fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaLetaslum).TotalMinutes.ToString());

                                    if (getPeiluyot(iMisparSidur, dShatHatchalaSidur, "(SUBSTRING(makat_nesia,1,3)<>'750')").Length > 0)
                                        fErech = oCalcBL.GetSumErechRechiv(objOved.DtPeiluyotYomi.Compute("sum(zmanElement)", sQury));

                                    oPeilut.CalcZmaneyAruchot(drPeiluyot, dShatHatchalaLetaslum, dShatGmarLetashlum, out  fZmanAruchatTzharimSidur, iMaxDakot);
                                }
                            }
                        }

                       
                        if (!oCalcBL.CheckYomShishi(objOved.SugYom))
                        {
                            fTemp = Math.Min(fZmanAruchatTzharimSidur, iMaxDakot - fZmanAruchatTzharim - objOved.fTotalAruchatZaharimForDay);
                            fZmanAruchatTzharim += fTemp;
                        }
                        fErech = fErech - fTemp;
                        addRowToTable(clGeneral.enRechivim.NochehutLepremiaPakach.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.NochehutLepremiaPakach.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
                drPeiluyot = null;
            }
        }

        public void CalcRechiv263()
        {
            int iMisparSidur, iSugHashlama;
            DateTime dShatHatchalaSidur;
            DataRow[] drSidurim, drSidurimLeyom;
            DataRow RowNext;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            float fErech;
            float fDakotNochehut;
            try
            {

                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null and Hashlama>0");
                for (int I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                    iSugHashlama = int.Parse(drSidurim[I]["sug_Hashlama"].ToString());

                    if (iSugHashlama == 1 || oCalcBL.CheckUshraBakasha(clGeneral.enKodIshur.HashlamaLeshaot.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur))
                    {
                        fDakotNochehut = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                        if (CheckSidurBySectorAvoda(ref drSidurim[I], iMisparSidur, clGeneral.enSectorAvoda.Nahagut.GetHashCode()))
                        {
                            fErech = Math.Max(0, (int.Parse(drSidurim[I]["Hashlama"].ToString()) * 60) - fDakotNochehut);

                            //אם X > 0 יש לבדוק האם יש סידור עוקב לסידור עם ההשלמה אם כן, 
                            //ערך הרכיב = הנמוך מבין (X , שעת התחלה לתשלום של הסידור העוקב פחות שעות גמר לתשלום של הסידור הנבדק)
                            //אם אין סידור עוקב, ערך הרכיב = X

                            if (fErech > 0)
                            {
                                drSidurimLeyom = null;
                                drSidurimLeyom = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null", "shat_hatchala_sidur asc");
                                RowNext = drSidurimLeyom[0];
                                for (int J = 0; J < drSidurimLeyom.Length; J++)
                                {
                                    RowNext = drSidurimLeyom[J];
                                    if ((drSidurim[I]["mispar_sidur"].ToString() == drSidurimLeyom[J]["mispar_sidur"].ToString()) && (drSidurim[I]["shat_hatchala_sidur"].ToString() == drSidurimLeyom[J]["shat_hatchala_sidur"].ToString()))
                                    {
                                        if (J < (drSidurimLeyom.Length - 1))
                                        {
                                            RowNext = drSidurimLeyom[J + 1];
                                        }

                                        break;
                                    }
                                }
                                if ((drSidurim[I]["mispar_sidur"].ToString() != RowNext["mispar_sidur"].ToString()) && (drSidurim[I]["shat_hatchala_sidur"].ToString() != RowNext["shat_hatchala_sidur"].ToString()))
                                {
                                    fErech = Math.Min(fErech, float.Parse((DateTime.Parse(RowNext["shat_hatchala_letashlum"].ToString()) - DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString())).TotalMinutes.ToString()));
                                }

                                addRowToTable(clGeneral.enRechivim.HashlamaBenahagut.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.HashlamaBenahagut.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
                drSidurimLeyom = null;
            }
        }

        public void CalcRechiv264()
        {
            int iMisparSidur, iSugHashlama;
            DateTime dShatHatchalaSidur;
            DataRow[] drSidurim, drSidurimLeyom;
            DataRow RowNext;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            float fErech;
            float fDakotNochehut;
            try
            {

                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null and Hashlama>0");
                for (int I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                    iSugHashlama = int.Parse(drSidurim[I]["sug_Hashlama"].ToString());

                    if (iSugHashlama == 1 || oCalcBL.CheckUshraBakasha(clGeneral.enKodIshur.HashlamaLeshaot.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur))
                    {
                        fDakotNochehut = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                        if (isSidurNihulTnua(drSidurim[I]))
                        {
                            fErech = Math.Max(0, (int.Parse(drSidurim[I]["Hashlama"].ToString()) * 60) - fDakotNochehut);

                            //אם X > 0 יש לבדוק האם יש סידור עוקב לסידור עם ההשלמה אם כן, 
                            //ערך הרכיב = הנמוך מבין (X , שעת התחלה לתשלום של הסידור העוקב פחות שעות גמר לתשלום של הסידור הנבדק)
                            //אם אין סידור עוקב, ערך הרכיב = X

                            if (fErech > 0)
                            {
                                drSidurimLeyom = null;
                                drSidurimLeyom = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null", "shat_hatchala_sidur asc");
                                RowNext = drSidurimLeyom[0];
                                for (int J = 0; J < drSidurimLeyom.Length; J++)
                                {
                                    RowNext = drSidurimLeyom[J];
                                    if ((drSidurim[I]["mispar_sidur"].ToString() == drSidurimLeyom[J]["mispar_sidur"].ToString()) && (drSidurim[I]["shat_hatchala_sidur"].ToString() == drSidurimLeyom[J]["shat_hatchala_sidur"].ToString()))
                                    {
                                        if (J < (drSidurimLeyom.Length - 1))
                                        {
                                            RowNext = drSidurimLeyom[J + 1];
                                        }

                                        break;
                                    }
                                }
                                if ((drSidurim[I]["mispar_sidur"].ToString() != RowNext["mispar_sidur"].ToString()) && (drSidurim[I]["shat_hatchala_sidur"].ToString() != RowNext["shat_hatchala_sidur"].ToString()))
                                {
                                    fErech = Math.Min(fErech, float.Parse((DateTime.Parse(RowNext["shat_hatchala_letashlum"].ToString()) - DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString())).TotalMinutes.ToString()));
                                }

                                addRowToTable(clGeneral.enRechivim.HashlamaBenihulTnua.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.HashlamaBenihulTnua.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
                drSidurimLeyom = null;
            }
        }

        public void CalcRechiv265()
        {
            int iMisparSidur, iSugHashlama;
            DateTime dShatHatchalaSidur;
            DataRow[] drSidurim, drSidurimLeyom;
            DataRow RowNext;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            float fErech;
            float fDakotNochehut;
            try
            {

                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null and Hashlama>0");
                for (int I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                    iSugHashlama = int.Parse(drSidurim[I]["sug_Hashlama"].ToString());

                    if (iSugHashlama == 1 || oCalcBL.CheckUshraBakasha(clGeneral.enKodIshur.HashlamaLeshaot.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur))
                    {
                        fDakotNochehut = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                        if (isSidurTafkid( drSidurim[I]))
                        {
                            fErech = Math.Max(0, (int.Parse(drSidurim[I]["Hashlama"].ToString()) * 60) - fDakotNochehut);

                            //אם X > 0 יש לבדוק האם יש סידור עוקב לסידור עם ההשלמה אם כן, 
                            //ערך הרכיב = הנמוך מבין (X , שעת התחלה לתשלום של הסידור העוקב פחות שעות גמר לתשלום של הסידור הנבדק)
                            //אם אין סידור עוקב, ערך הרכיב = X

                            if (fErech > 0)
                            {
                                drSidurimLeyom = null;
                                drSidurimLeyom = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null", "shat_hatchala_sidur asc");
                                RowNext = drSidurimLeyom[0];
                                for (int J = 0; J < drSidurimLeyom.Length; J++)
                                {
                                    RowNext = drSidurimLeyom[J];
                                    if ((drSidurim[I]["mispar_sidur"].ToString() == drSidurimLeyom[J]["mispar_sidur"].ToString()) && (drSidurim[I]["shat_hatchala_sidur"].ToString() == drSidurimLeyom[J]["shat_hatchala_sidur"].ToString()))
                                    {
                                        if (J < (drSidurimLeyom.Length - 1))
                                        {
                                            RowNext = drSidurimLeyom[J + 1];
                                        }

                                        break;
                                    }
                                }
                                if ((drSidurim[I]["mispar_sidur"].ToString() != RowNext["mispar_sidur"].ToString()) && (drSidurim[I]["shat_hatchala_sidur"].ToString() != RowNext["shat_hatchala_sidur"].ToString()))
                                {
                                    fErech = Math.Min(fErech, float.Parse((DateTime.Parse(RowNext["shat_hatchala_letashlum"].ToString()) - DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString())).TotalMinutes.ToString()));
                                }

                                addRowToTable(clGeneral.enRechivim.HashlamaBetafkid.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.HashlamaBetafkid.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
                drSidurimLeyom = null;
            }
        }


        public float CalcRechiv266()
        {
            DataRow[] drSidurim;
            int iMisparSidur;
            float fErech;
            string sSidurimMeyuchadim;
            DateTime dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                fErech = 0;
                sSidurimMeyuchadim = GetSidurimMeyuchRechiv(clGeneral.enRechivim.YomMiluimChelki.GetHashCode());
                if (sSidurimMeyuchadim.Length > 0)
                {
                    drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ")");
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                        fErech += objOved.objParameters.fChelkiyutAchuzMiluim;
                    }
                }
                return fErech;
            }

            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.YomMiluimChelki.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }

        }

        public void CalcRechiv267()
        {
            int iMisparSidur;
            float fErech;
            DateTime dShatHatchalaSidur;
            DataRow[] _drSidurim;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                //oPeilut.objOved.Taarich = objOved.Taarich;

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");
                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                    oPeilut.CalcRechiv267(iMisparSidur, dShatHatchalaSidur);

                    fErech = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_PEILUT"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotElementim.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    addRowToTable(clGeneral.enRechivim.DakotElementim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotElementim.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }


        public void CalcRechiv268()
        {
            int iMisparSidur;
            float fErech;
            DateTime dShatHatchalaSidur;
            DataRow[] _drSidurim;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                //oPeilut.objOved.Taarich = objOved.Taarich;

                _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");
                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                    oPeilut.CalcRechiv268(iMisparSidur, dShatHatchalaSidur);

                    fErech = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_PEILUT"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNesiaLepremia.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                    addRowToTable(clGeneral.enRechivim.DakotNesiaLepremia.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotNesiaLepremia.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public void CalcRechiv271()
        {
            DataRow[] drSidurim;
            int iMisparSidur, iIsuk;
            DateTime dShatHatchalaSidur, dShatHatchalaLetashlum, dShatGmarLetashlum, dZmanSiyuomTosLila, dZmanTchilatTosLila;
            float fErech, fHalbashaTchilatYom, fHalbashaSofYom;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            int iSugYomLemichsa;
            bool isHafsakaLast = false;
            DateTime shatHatchalaHafsakaLast = new DateTime();
            try
            {

                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");

                if (drSidurim.Length > 0)
                {
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        if (drSidurim[I]["ein_leshalem_tos_lila"].ToString() == "")
                        {
                            iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                            dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                            isHafsakaLast = oPeilut.CheckHafasakaLast(iMisparSidur, dShatHatchalaSidur, ref shatHatchalaHafsakaLast);

                            fHalbashaTchilatYom = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.HalbashaTchilatYom.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                            fHalbashaSofYom = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.HalbashaSofYom.GetHashCode().ToString() + " and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                            dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString()).AddMinutes(-fHalbashaTchilatYom);
                            if (isHafsakaLast)
                                dShatGmarLetashlum = shatHatchalaHafsakaLast.AddMinutes(fHalbashaSofYom);
                            else
                                dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString()).AddMinutes(fHalbashaSofYom);
                            fErech = 0;

                            //אם סידור הינו סידור ויזה צבאית - סידור מיוחד בעל שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 45 ) עם ערך = 1 זהו יום אחרון של הוויזה - TB_Sidurim_Ovedim.Yom_Visa= 3 אזי יש לבצע את בדיקת זמן הסידור מול שעות לילה חוק לפי שעת התחלה של סידור TB_Sidurim_Ovedim. Shat_hatchala  ולא שעת התחלה לתשלום של סידור.
                            if (drSidurim[I]["sidur_namlak_visa"].ToString() == "1" && drSidurim[I]["yom_visa"].ToString() == "3")
                            {
                                dShatHatchalaLetashlum = dShatHatchalaSidur;
                            }

                            iIsuk = objOved.objPirteyOved.iIsuk;
                            iSugYomLemichsa = oCalcBL.GetSugYomLemichsa(objOved, objOved.Taarich, objOved.objPirteyOved.iKodSectorIsuk, objOved.objMeafyeneyOved.GetMeafyen(56).IntValue);

                            if ((iIsuk == 122 || iIsuk == 123 || iIsuk == 124 || iIsuk == 127) && iMisparSidur == 99001 && clDefinitions.GetSugMishmeret(objOved.Mispar_ishi, objOved.Taarich, iSugYomLemichsa, dShatHatchalaSidur, DateTime.Parse(drSidurim[I]["shat_gmar_sidur"].ToString()), objOved.objParameters) == clGeneral.enSugMishmeret.Liyla.GetHashCode())
                            {
                                dZmanSiyuomTosLila = objOved.objParameters.dSiyumMishmeretLilaMafilim.AddDays(-1);
                            }
                            else
                            {
                                dZmanSiyuomTosLila = objOved.objParameters.dSiyumTosefetLailaChok.AddDays(-1);

                            }

                            dZmanTchilatTosLila = clGeneral.GetDateTimeFromStringHour("00:01", objOved.Taarich);
                            if (dShatHatchalaLetashlum >= dZmanTchilatTosLila && dShatGmarLetashlum <= dZmanSiyuomTosLila)
                            {
                                fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());
                            }
                            else if (dShatHatchalaLetashlum <= dZmanTchilatTosLila && dShatGmarLetashlum <= dZmanSiyuomTosLila && dShatGmarLetashlum > dZmanTchilatTosLila)
                            {
                                fErech = float.Parse((dShatGmarLetashlum - dZmanTchilatTosLila).TotalMinutes.ToString());
                            }
                            else if (dShatHatchalaLetashlum >= dZmanTchilatTosLila && dShatGmarLetashlum >= dZmanSiyuomTosLila && dShatHatchalaLetashlum < dZmanSiyuomTosLila)
                            {
                                fErech = float.Parse((dZmanSiyuomTosLila - dShatHatchalaLetashlum).TotalMinutes.ToString());
                            }
                            else if (dShatHatchalaLetashlum <= dZmanTchilatTosLila && dShatGmarLetashlum >= dZmanTchilatTosLila)
                            {
                                fErech = float.Parse((dZmanSiyuomTosLila - dZmanTchilatTosLila).TotalMinutes.ToString());
                            }

                            if (fErech > 0)
                            {
                                HachsaratPeilut790SidureyBoker(iMisparSidur, dShatHatchalaSidur, ref fErech);
                                addRowToTable(clGeneral.enRechivim.ZmanLilaSidureyBoker.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                            }
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.ZmanLilaSidureyBoker.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        private void HachsaratPeilut790SidureyBoker(int iMisparSidur, DateTime dShatHatchalaSidur, ref float fErechRechiv)
        {
            DataRow[] dr790;
            string sQury;
            DateTime shat_yetzia;
            int meshec790;
            try
            {
                sQury = "MISPAR_SIDUR=" + iMisparSidur + "  AND taarich=Convert('" + dShatHatchalaSidur.ToShortDateString() + "', 'System.DateTime') and ";
                sQury += "SHAT_HATCHALA_SIDUR=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and (SUBSTRING(makat_nesia,1,3)='790')";
                dr790 = objOved.DtPeiluyotYomi.Select(sQury, "shat_yetzia asc");

                if (dr790.Length > 0)
                {
                    shat_yetzia = DateTime.Parse(dr790[0]["shat_yetzia"].ToString());
                    meshec790 = int.Parse(dr790[0]["makat_nesia"].ToString().Substring(3, 3));
                    if (shat_yetzia.ToShortDateString() == objOved.Taarich.ToShortDateString() &&
                        shat_yetzia < objOved.objParameters.dTchilatTosefetLailaYomNochechi)
                    {
                        if (shat_yetzia.AddMinutes(meshec790) <= objOved.objParameters.dSiyumTosefetLailaYomNochechi)
                            fErechRechiv -= meshec790;
                        else if (shat_yetzia.AddMinutes(meshec790) > objOved.objParameters.dSiyumTosefetLailaYomNochechi)
                            fErechRechiv -= int.Parse((objOved.objParameters.dSiyumTosefetLailaYomNochechi - shat_yetzia).TotalMinutes.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.ZmanLailaEgged.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: " + ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv931()
        {
            DataRow[] drSidurim;
            int iMisparSidur = 0;
            DateTime dShatHatchalaSidur = DateTime.MinValue;
            DateTime dShatGmarSidurKodem;
            float fErech;
            int iMisparSidurKodem;
            try
            {
                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null", "shat_hatchala_sidur asc");

                if (drSidurim.Length > 0)
                {
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                        if (drSidurim[I]["halbasha"].ToString() == "1" || drSidurim[I]["halbasha"].ToString() == "3")
                        {
                            if (drSidurim[I]["mezake_halbasha"].ToString() == "1" || drSidurim[I]["mezake_halbasha"].ToString() == "3")
                            {
                                if (I == 0)
                                    addRowToTable(clGeneral.enRechivim.HalbashaTchilatYom.GetHashCode(), dShatHatchalaSidur, iMisparSidur, objOved.objParameters.iZmanHalbash);
                                else
                                {
                                    dShatGmarSidurKodem = dShatHatchalaSidur;
                                    iMisparSidurKodem = int.Parse(drSidurim[I - 1]["mispar_sidur"].ToString());
                                    if ((iMisparSidurKodem == 99500 || iMisparSidurKodem == 99501) && (I - 2) >= 0)
                                        dShatGmarSidurKodem = DateTime.Parse(drSidurim[I - 2]["shat_gmar_sidur"].ToString());
                                    else if ((I - 1) >= 0) dShatGmarSidurKodem = DateTime.Parse(drSidurim[I - 1]["shat_gmar_sidur"].ToString());

                                    fErech = Math.Min(objOved.objParameters.iZmanHalbash, float.Parse((dShatHatchalaSidur - dShatGmarSidurKodem).TotalMinutes.ToString()));
                                    addRowToTable(clGeneral.enRechivim.HalbashaTchilatYom.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.HalbashaTchilatYom.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        public void CalcRechiv932()
        {
            DataRow[] drSidurim;
            int iMisparSidur = 0;
            DateTime dShatHatchalaSidur = DateTime.MinValue;
            DateTime dShatHatchalaSidurNext, dShatGmarSidur;
            float fErech;
            int iMisparSidurNext;
            try
            {
                drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null", "shat_hatchala_sidur asc");

                if (drSidurim.Length > 0)
                {
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                        if (drSidurim[I]["halbasha"].ToString() == "2" || drSidurim[I]["halbasha"].ToString() == "3")
                        {
                            if (drSidurim[I]["mezake_halbasha"].ToString() == "2" || drSidurim[I]["mezake_halbasha"].ToString() == "3")
                            {
                                if (I == drSidurim.Length - 1)
                                    addRowToTable(clGeneral.enRechivim.HalbashaSofYom.GetHashCode(), dShatHatchalaSidur, iMisparSidur, objOved.objParameters.iZmanHalbash);
                                else
                                {
                                    dShatGmarSidur = DateTime.Parse(drSidurim[I]["shat_gmar_sidur"].ToString());
                                    dShatHatchalaSidurNext = dShatGmarSidur;
                                    iMisparSidurNext = int.Parse(drSidurim[I + 1]["mispar_sidur"].ToString());
                                    if ((iMisparSidurNext == 99500 || iMisparSidurNext == 99501) && (I + 2) < drSidurim.Length)
                                        dShatHatchalaSidurNext = DateTime.Parse(drSidurim[I + 2]["shat_hatchala_sidur"].ToString());
                                    else if ((I + 1) < drSidurim.Length) dShatHatchalaSidurNext = DateTime.Parse(drSidurim[I + 1]["shat_hatchala_sidur"].ToString());


                                    fErech = Math.Min(objOved.objParameters.iZmanHalbash, float.Parse((dShatHatchalaSidurNext - dShatGmarSidur).TotalMinutes.ToString()));
                                    addRowToTable(clGeneral.enRechivim.HalbashaSofYom.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                                }
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.HalbashaSofYom.GetHashCode(), objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        public void CalcNochehutPremia(int iKodRechiv, Boolean bMeafyenPremia, out float fZmanAruchatBoker, out float fZmanAruchatTzharim, out float fZmanAruchatErev)
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur, dShatGmarLetashlumSidur, dTempM1, dTempM2, dShatHatchalaLethaslum;
            float fErech;
            fZmanAruchatBoker = 0;
            fZmanAruchatTzharim = 0;
            fZmanAruchatErev = 0;
            float fZmanAruchatBokerSidur = 0;
            float fZmanAruchatTzharimSidur = 0;
            float fZmanAruchatErevSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                if (bMeafyenPremia)
                {
                    _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur=99001");

                    for (int I = 0; I < _drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                        dShatGmarLetashlumSidur = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                        dShatHatchalaLethaslum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());

                        fErech = float.Parse((dShatGmarLetashlumSidur - dShatHatchalaLethaslum).TotalMinutes.ToString());
                        addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fErech);

                        if (iKodRechiv == clGeneral.enRechivim.NochehutLePremyatDfus.GetHashCode() || iKodRechiv == clGeneral.enRechivim.NochehutLePremyatNehageyTovala.GetHashCode())
                        { dShatHatchalaLethaslum = dShatHatchalaSidur; }

                        //חישוב זמן ארוחת בוקר
                        dTempM1 = clGeneral.GetDateTimeFromStringHour("09:00", objOved.Taarich.Date);
                        dTempM2 = clGeneral.GetDateTimeFromStringHour("09:20", objOved.Taarich.Date);

                        if (dShatHatchalaLethaslum <= dTempM1 && dShatGmarLetashlumSidur >= dTempM1)
                        { fZmanAruchatBokerSidur = float.Parse((dShatGmarLetashlumSidur - dTempM1).TotalMinutes.ToString()); }
                        if (dShatHatchalaLethaslum >= dTempM1 && dShatHatchalaLethaslum <= dTempM2 && dShatGmarLetashlumSidur >= dTempM2)
                        { fZmanAruchatBokerSidur = float.Parse((dTempM2 - dShatHatchalaLethaslum).TotalMinutes.ToString()); }
                        if (dShatHatchalaLethaslum >= dTempM1 && dShatGmarLetashlumSidur <= dTempM2)
                        { fZmanAruchatBokerSidur = float.Parse((dShatGmarLetashlumSidur - dShatHatchalaLethaslum).TotalMinutes.ToString()); }
                        if (fZmanAruchatBokerSidur > 20)
                        { fZmanAruchatBokerSidur = 20; }
                        else { fZmanAruchatBokerSidur = 0; }

                        //חישוב זמן ארוחת צהריים
                        dTempM1 = objOved.objParameters.dStartAruchatTzaharayim;
                        dTempM2 = objOved.objParameters.dEndAruchatTzaharayim;

                        if (dShatHatchalaLethaslum <= dTempM1 && dShatGmarLetashlumSidur >= dTempM1)
                        { fZmanAruchatTzharimSidur = float.Parse((dShatGmarLetashlumSidur - dTempM1).TotalMinutes.ToString()); }
                        if (dShatHatchalaLethaslum >= dTempM1 && dShatHatchalaLethaslum <= dTempM2 && dShatGmarLetashlumSidur >= dTempM2)
                        { fZmanAruchatTzharimSidur = float.Parse((dTempM2 - dShatHatchalaLethaslum).TotalMinutes.ToString()); }
                        if (dShatHatchalaLethaslum >= dTempM1 && dShatGmarLetashlumSidur <= dTempM2)
                        { fZmanAruchatTzharimSidur = float.Parse((dShatGmarLetashlumSidur - dShatHatchalaLethaslum).TotalMinutes.ToString()); }
                        if (fZmanAruchatTzharimSidur > 30)
                        { fZmanAruchatTzharimSidur = 30; }
                        else { fZmanAruchatTzharimSidur = 0; }

                        //חישוב זמן ארוחת ערב
                        dTempM1 = clGeneral.GetDateTimeFromStringHour("19:00", objOved.Taarich.Date);
                        dTempM2 = clGeneral.GetDateTimeFromStringHour("19:20", objOved.Taarich.Date);

                        if (dShatHatchalaLethaslum <= dTempM1 && dShatGmarLetashlumSidur >= dTempM1)
                        { fZmanAruchatErevSidur = float.Parse((dShatGmarLetashlumSidur - dTempM1).TotalMinutes.ToString()); }
                        if (dShatHatchalaLethaslum >= dTempM1 && dShatHatchalaLethaslum <= dTempM2 && dShatGmarLetashlumSidur >= dTempM2)
                        { fZmanAruchatErevSidur = float.Parse((dTempM2 - dShatHatchalaLethaslum).TotalMinutes.ToString()); }
                        if (dShatHatchalaLethaslum >= dTempM1 && dShatGmarLetashlumSidur <= dTempM2)
                        { fZmanAruchatErevSidur = float.Parse((dShatGmarLetashlumSidur - dShatHatchalaLethaslum).TotalMinutes.ToString()); }
                        if (fZmanAruchatErevSidur > 20)
                        { fZmanAruchatErevSidur = 20; }
                        else { fZmanAruchatErevSidur = 0; }

                        fZmanAruchatBoker += fZmanAruchatBokerSidur;
                        fZmanAruchatTzharim += fZmanAruchatTzharimSidur;
                        fZmanAruchatErev += fZmanAruchatErevSidur;
                    }
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, iKodRechiv, objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public float CalcYemeyHeadrut(int iKodRechiv, out int iCountSidurim, float fMichsaYomit)
        {
            DataRow[] drSidurim;
            DataRow[] drAnotherSidurim;
            int iMisparSidur;
            float fErech, fDakotNochehut;
            string sSidurimMeyuchadim;
            DateTime dShatHatchalaSidur = DateTime.MinValue;
            DateTime dShatGmarLetashlum, dShatHatchalaLetashlum;
            iMisparSidur = 0;
            try
            {
                fErech = 0;
                iCountSidurim = 0;
                sSidurimMeyuchadim = GetSidurimMeyuchRechiv(iKodRechiv);
                if (sSidurimMeyuchadim.Length > 0)
                {
                    drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");
                    iCountSidurim = drSidurim.Length;

                    drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ")");
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                        fDakotNochehut = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                        dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());

                        drAnotherSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and MISPAR_SIDUR not IN(" + sSidurimMeyuchadim + ")");
                        if (fDakotNochehut == 0 && drAnotherSidurim.Length==0)
                            fDakotNochehut = Math.Min(fMichsaYomit, float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString()));

                        fErech += fDakotNochehut;
                        if (iKodRechiv ==56)
                            addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fDakotNochehut);

                    }
                }
                return fErech;
            }

            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, iKodRechiv, objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        public float CalcYemeyHeadrut(int iKodRechiv, float fMichsaYomit)
        {
            DataRow[] drSidurim;
            int iMisparSidur;
            float fErech, fDakotNochehut;
            string sSidurimMeyuchadim;
            DateTime dShatHatchalaSidur = DateTime.MinValue;
            DateTime dShatGmarLetashlum, dShatHatchalaLetashlum;
            iMisparSidur = 0;
            try
            {
                fErech = 0;
                sSidurimMeyuchadim = GetSidurimMeyuchRechiv(iKodRechiv);
                if (sSidurimMeyuchadim.Length > 0)
                {
                   
                    drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and MISPAR_SIDUR NOT IN(" + sSidurimMeyuchadim + ")");
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                        fDakotNochehut = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                        dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());

                        if (fDakotNochehut == 0)
                            fDakotNochehut = Math.Min(fMichsaYomit, float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString()));

                        fErech += fDakotNochehut;
                    }
                }
                return fErech;
            }

            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, iKodRechiv, objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: " + ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }
        private void CalcSachDakot(int iKodRechiv)
        {
            string sSidurimMeyuchadim, sSugeySidur;
            DataRow[] drSidurim;
            int iMisparSidur, iSugSidur;
            float fErech;
            DateTime dShatHatchalaSidur;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                sSidurimMeyuchadim = GetSidurimMeyuchRechiv(iKodRechiv);
                if (sSidurimMeyuchadim.Length > 0)
                {
                    drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ")");
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                        fErech = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                        addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fErech);

                    }
                }

                sSugeySidur = GetSugeySidurRechiv(iKodRechiv);
                if (sSugeySidur.Length > 0)
                {
                    drSidurim = null;
                    drSidurim = GetSidurimRegilim();
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(drSidurim[I]["MISPAR_SIDUR"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                        //SetSugSidur(ref drSidurim[I], objOved.Taarich, iMisparSidur);

                        iSugSidur = int.Parse(drSidurim[I]["sug_sidur"].ToString());
                        if (sSugeySidur.IndexOf("," + iSugSidur.ToString() + ",") > -1)
                        {

                            fErech = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));

                            addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fErech);

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(objOved.iBakashaId, "E", null, iKodRechiv, objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            finally
            {
                drSidurim = null;
            }
        }

        private void CheckSidurCholToAdd(int iRechiv, int iMisparSidur, int iDay, int iSugYom, DateTime dShatHatchalaLetashlum, DateTime dShatGmarLetashlum, DateTime dShatHatchalaSidur, int iMichutzLamichsa, bool bCheckMafil)
        {
            bool bIsShabat;
            float fErechShabat, fErech, fMichsaYomit126, fErechShishi;
            int iSugYomNext;
            fErech = 0;
            fErechShabat = 0;
            fErechShishi = 0;

            //בדיקת יוצאים מן הכלל לחישוב
            //סידורים שבוצעו בשבת או בערב שבת או חג לא נכללים
            //יש לחשב רק החלק היחסי שבוצע בחול
            try
            {
                bIsShabat = CheckShabat(iMisparSidur, iDay, iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, ref fErech, ref fErechShabat, bCheckMafil);
                   
                if (!bIsShabat)
                {
                    fMichsaYomit126 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);
                    if (!CheckErevShabatChag(iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, ref fErech, ref fErechShabat))
                    {
                        //בדיקת גלישת סידור מיום חול ליום ו'/ערב חג 
                        iSugYomNext = clGeneral.GetSugYom(objOved.oGeneralData.dtYamimMeyuchadim, DateTime.Parse(objOved.Taarich.ToShortDateString()).AddDays(1),objOved.oGeneralData.dtSugeyYamimMeyuchadim);//, objOved.objMeafyeneyOved.GetMeafyen(56).IntValue);
                        if (oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim,iSugYomNext) || oCalcBL.CheckYomShishi(iSugYomNext))
                        {
                            fMichsaYomit126 = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich.AddDays(1));
                            if (fMichsaYomit126 > 0)
                            {
                                if (fErech > 0)
                                {
                                    HoradatZmanHafsaktCholBeShishiErevChag(iMisparSidur, dShatHatchalaSidur, ref  fErech);
                                    addRowToTable(iRechiv, dShatHatchalaSidur, iMisparSidur, fErech, iMichutzLamichsa);
                                }
                            }
                            else
                            {
                                //אם מכסה יומית מחושבת = 0 יש לחשב את החלק היחסי שבוצע בחול
                                CalcGlishaLeyomShishi(dShatHatchalaLetashlum, dShatGmarLetashlum, ref fErech, ref fErechShishi);
                                HoradatZmanHafsaktCholBeShishiErevChag(iMisparSidur, dShatHatchalaSidur, ref  fErech);                        
                                if (fErech > 0)
                                {
                                    addRowToTable(iRechiv, dShatHatchalaSidur, iMisparSidur, fErech, iMichutzLamichsa);
                                }
                            }
                        }
                        else if (fErech > 0)
                        {
                            addRowToTable(iRechiv, dShatHatchalaSidur, iMisparSidur, fErech, iMichutzLamichsa);
                        }
                    }
                    else if (fMichsaYomit126 > 0 && fErech > 0)
                    {
                        //אם הסידור מתחיל בערב חג או שבת  
                        HoradatZmanHafsaktCholBeShishiErevChag(iMisparSidur, dShatHatchalaSidur, ref  fErech);       
                        addRowToTable(iRechiv, dShatHatchalaSidur, iMisparSidur, fErech, iMichutzLamichsa);
                    }
                }
                else if (fErech > 0)
                {
                    //אם הסידור מתחיל בשבת
                    HoradatZmanHafsaktCholBeShabat(iMisparSidur, dShatHatchalaSidur, bCheckMafil, ref  fErech);
                    addRowToTable(iRechiv, dShatHatchalaSidur, iMisparSidur, fErech, iMichutzLamichsa);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ChishuvShatYeziatShabat(int iMisparSidur, bool bCheckMafil, ref DateTime dShatGmarShabat)
        {
            DateTime  dNextDay;
            bool bNextShabat;
            if (bCheckMafil && (objOved.objPirteyOved.iIsuk == 122 || objOved.objPirteyOved.iIsuk == 123 || objOved.objPirteyOved.iIsuk == 124 || objOved.objPirteyOved.iIsuk == 127) && iMisparSidur == 99001)
            {
                dShatGmarShabat = objOved.objParameters.dSiyumLilaMotsashMafil;
            }
            else
            {
                dShatGmarShabat = objOved.objParameters.dShatMaavarYom;
            }

            dNextDay = objOved.Taarich.AddDays(1);

            if ((dNextDay.DayOfWeek.GetHashCode() + 1) == clGeneral.enDay.Shabat.GetHashCode())
            { bNextShabat = true; }
            else { bNextShabat = clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, clGeneral.GetSugYom(objOved.oGeneralData.dtYamimMeyuchadim, dNextDay, objOved.oGeneralData.dtSugeyYamimMeyuchadim), dNextDay); } //, objOved.objMeafyeneyOved.GetMeafyen(56).IntValue), dNextDay); }

            if (bNextShabat)
            { dShatGmarShabat = dShatGmarShabat.AddDays(1); }

        }
        private void HoradatZmanHafsaktCholBeShabat( int iMisparSidur, DateTime dShatHatchalaSidur,bool bCheckMafil,ref float fErechChol)
        {
            DataRow[] dr790;
            DateTime shat_yetzia;
            string sQury;
            int meshec790;
            DateTime dShatGmarShabat;
            dShatGmarShabat = new DateTime();
            ChishuvShatYeziatShabat(iMisparSidur,bCheckMafil, ref dShatGmarShabat);
            
            sQury = "MISPAR_SIDUR=" + iMisparSidur + "  AND taarich=Convert('" + dShatHatchalaSidur.ToShortDateString() + "', 'System.DateTime') and ";
            sQury += "SHAT_HATCHALA_SIDUR=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and (SUBSTRING(makat_nesia,1,3)='790')";
            dr790 = objOved.DtPeiluyotYomi.Select(sQury, "shat_yetzia asc");

            if (dr790.Length > 0)
            {
                shat_yetzia = DateTime.Parse(dr790[0]["shat_yetzia"].ToString());
                meshec790 = int.Parse(dr790[0]["makat_nesia"].ToString().Substring(3, 3));
                if (shat_yetzia > dShatGmarShabat)
                    fErechChol -= meshec790;
                else if (shat_yetzia < dShatGmarShabat && shat_yetzia.AddMinutes(meshec790) > dShatGmarShabat)
                    fErechChol -= (meshec790 - int.Parse((dShatGmarShabat - shat_yetzia).TotalMinutes.ToString()));
            }
        }
        private void HoradatZmanHafsaktCholBeShishiErevChag(int iMisparSidur, DateTime dShatHatchalaSidur, ref float fErechChol)
        {
            DataRow[] dr790;
            DateTime shat_yetzia, dShatHatchlaShabat;
            string sQury;
            int meshec790;

            dShatHatchlaShabat = objOved.objParameters.dKnisatShabat;
            
            sQury = "MISPAR_SIDUR=" + iMisparSidur + "  AND taarich=Convert('" + dShatHatchalaSidur.ToShortDateString() + "', 'System.DateTime') and ";
            sQury += "SHAT_HATCHALA_SIDUR=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and (SUBSTRING(makat_nesia,1,3)='790')";
            dr790 = objOved.DtPeiluyotYomi.Select(sQury, "shat_yetzia asc");

            if (dr790.Length > 0)
            {
                shat_yetzia = DateTime.Parse(dr790[0]["shat_yetzia"].ToString());
                meshec790 = int.Parse(dr790[0]["makat_nesia"].ToString().Substring(3, 3));
                if (shat_yetzia < dShatHatchlaShabat && shat_yetzia.AddMinutes(meshec790) < dShatHatchlaShabat)
                    fErechChol -= meshec790;
                else if (shat_yetzia < dShatHatchlaShabat && shat_yetzia.AddMinutes(meshec790) > dShatHatchlaShabat)
                    fErechChol -= (int.Parse((dShatHatchlaShabat - shat_yetzia).TotalMinutes.ToString()));
            }
        }
        private void CheckSidurShabatToAdd(int iRechiv, int iMisparSidur, int iDay, int iSugYom, DateTime dShatHatchalaLetashlum, DateTime dShatGmarLetashlum, DateTime dShatHatchalaSidur, int iMichutzLamichsa, bool bCheckMafil)
        {
            bool bIsShabat;
            float fErechShabat, fErech;
            fErech = 0;
            fErechShabat = 0;
            //בדיקת יוצאים מן הכלל לחישוב
            //סידורים שבוצעו בחול לא נכללים
            //יש לחשב רק החלק היחסי שבוצע בשבת
            try
            {
                bIsShabat = CheckShabat(iMisparSidur, iDay, iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, ref fErech, ref fErechShabat, bCheckMafil);
                   
                if (!bIsShabat)
                {
                    if (CheckErevShabatChag(iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, ref fErech, ref fErechShabat))
                    {
                        //אם הסידור מתחיל בערב חג או שבת  
                        if (fErechShabat > 0)
                        {
                            HoradatZmanHafsaktShabat(iMisparSidur, dShatHatchalaSidur, bCheckMafil, ref  fErechShabat);
                            addRowToTable(iRechiv, dShatHatchalaSidur, iMisparSidur, fErechShabat, iMichutzLamichsa);
                        }
                    }

                }
                else if (fErechShabat > 0)
                {
                    //אם הסידור מתחיל בשבת
                    HoradatZmanHafsaktShabat(iMisparSidur, dShatHatchalaSidur, bCheckMafil, ref  fErechShabat);
                    addRowToTable(iRechiv, dShatHatchalaSidur, iMisparSidur, fErechShabat, iMichutzLamichsa);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void  HoradatZmanHafsaktShabat(int iMisparSidur,DateTime dShatHatchalaSidur,bool bCheckMafil, ref float  fErechShabat)
        {
            DataRow[] dr790;
            DateTime shat_yetzia;
            string sQury;
            int meshec790;
            DateTime dShatGmarShabat, dShatHatchlaShabat;
            dShatGmarShabat = new DateTime();
            ChishuvShatYeziatShabat(iMisparSidur,bCheckMafil, ref dShatGmarShabat);
            dShatHatchlaShabat = objOved.objParameters.dKnisatShabat;
           

            sQury = "MISPAR_SIDUR=" + iMisparSidur + "  AND taarich=Convert('" + dShatHatchalaSidur.ToShortDateString() + "', 'System.DateTime') and ";
            sQury += "SHAT_HATCHALA_SIDUR=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and (SUBSTRING(makat_nesia,1,3)='790')";
            dr790 = objOved.DtPeiluyotYomi.Select(sQury, "shat_yetzia asc");

            if (dr790.Length > 0)
            {
                shat_yetzia = DateTime.Parse(dr790[0]["shat_yetzia"].ToString());
                meshec790 = int.Parse(dr790[0]["makat_nesia"].ToString().Substring(3, 3));
                if (shat_yetzia > dShatHatchlaShabat && shat_yetzia < dShatGmarShabat)
                    fErechShabat -= meshec790;
                else if (shat_yetzia < dShatHatchlaShabat && shat_yetzia.AddMinutes(meshec790) > dShatHatchlaShabat)
                    fErechShabat -= (meshec790 - int.Parse((dShatHatchlaShabat - shat_yetzia).TotalMinutes.ToString()));
                else if (shat_yetzia > dShatHatchlaShabat && shat_yetzia.AddMinutes(meshec790) > dShatGmarShabat)
                    fErechShabat -= (meshec790 - int.Parse((shat_yetzia - dShatGmarShabat).TotalMinutes.ToString()));
            }
        }
        private bool CheckSugSidur(int iMeafyen, int iErech, DateTime dDate, int iSugSidur)
        {   //הפונקציה מקבלת קוד מאפיין,סוג סידור,ערך מאפיין ותאריך ומחזירה האם קיים כזה סוג סידור

            DataRow[] dr;
            try
            {
                dr = objOved.oGeneralData.dtMeafyeneySugSidurAll.Select("kod_meafyen=" + iMeafyen.ToString() + " and erech=" + iErech + " and Convert('" + dDate.ToShortDateString() + "','System.DateTime') >= me_taarich and Convert('" + dDate.ToShortDateString() + "', 'System.DateTime') <= ad_taarich and sug_sidur=" + iSugSidur.ToString());

                return ((dr.Length > 0) ? true : false);
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


        private bool IsSidur100(int iMisparSidur)
        {    
            DataRow[] dr=null;
            try
            {
                if ((oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom) && !oCalcBL.CheckYomShishi(objOved.SugYom))
                    || objOved.SugYom == clGeneral.enSugYom.Chol.GetHashCode())
                    dr = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur=" + iMisparSidur + " and sug_shaot_byom_hol_if_migbala=100");
                if (oCalcBL.CheckYomShishi(objOved.SugYom))
                    dr = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur=" + iMisparSidur  + " and sug_hashaot_beyom_shishi=100");
                else if (clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                    dr = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur =" + iMisparSidur + " and sug_hashaot_beyom_shabaton=100");

                if (dr == null) return false;

                return ((dr.Length > 0) ? true : false);
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

        private bool IsSidurShaon(DataRow drSidur)
        {
            try
            {
                if (drSidur["shaon_nochachut"].ToString() == "1" || drSidur["shaon_nochachut"].ToString() == "2" || drSidur["shaon_nochachut"].ToString() == "3")
                 return true;

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        private DataRow[] GetSidurimMeyuchadim()
        {
            DataRow[] dr;

            dr = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99");

            return dr;
        }

        private DataRow[] GetSidurimRegilim()
        {
            DataRow[] dr;

            dr = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)<>99 ");

            return dr;
        }


        private DataRow[] GetSidurimMeyuchadim(string sMeafyen, int iErech)
        {
            DataRow[] dr;

            dr = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and " + sMeafyen + "=" + iErech);

            return dr;
        }

        private DataRow[] GetSidurimMeyuchadim(string sMeafyen, int iErech, int iMisparSidur)
        {
            DataRow[] dr;

            dr = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur=" + iMisparSidur + " and " + sMeafyen + "=" + iErech);

            return dr;
        }

        //private void //SetSugSidur(ref DataRow drSidur, DateTime dSidurDate, int iMisparSidur)
        //{
        //        //סידורים רגילים
        //        //נבדוק מול התנועה
        //        DataRow[] rowSidur;
        //    try
        //    {
        //        if (drSidur["sug_sidur"].ToString() == "0" || drSidur["sug_sidur"].ToString() == "")
        //        {
        //            drSidur["sug_sidur"] = 0;
        //            rowSidur = objOved.oGeneralData.dtSugeySidurAll.Select("taarich=Convert('" + dSidurDate.ToShortDateString() + "', 'System.DateTime') and mispar_sidur=" + iMisparSidur);
        //            if (rowSidur.Length > 0)
        //            {
        //                if (rowSidur[0]["SUG_SIDUR"].ToString() != "")
        //                    drSidur["sug_sidur"] = int.Parse(rowSidur[0]["SUG_SIDUR"].ToString());
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        rowSidur = null;
        //    }

        //}


        //private bool CheckSidurMeyuchad(int iMeafyen,int  iErech,DateTime dDate,int iMisparSidur)
        //{   // בדיקה האם קיים סידור מיוחד לפי: מספר סידור, מאפיין , ערך ותאריך

        //    DataRow[] dr;

        //    dr = _dtSidurimMeyuchadim.Select("kod_meafyen=" + iMeafyen.ToString() + " and erech=" + iErech + " and Convert('" + dDate.ToShortDateString() +  "','System.DateTime') >= me_taarich and Convert('" + dDate.ToShortDateString() + "', 'System.DateTime') <= ad_taarich and mispar_sidur=" + iMisparSidur.ToString());

        //    return ((dr.Length>0) ? true : false);
        //}

        private void addRowToTable(int iKodRechiv, DateTime dShatHatchala, int iMisparSidur, float fErechRechiv)
        {
            DataRow drChishuv;
//            DataRow[] drChishuvRows;
//            drChishuvRows= objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Select(null, "KOD_RECHIV");
//            drChishuvRows = objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Select("KOD_RECHIV=" + iKodRechiv + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')");
            if (CountOfRecords(iKodRechiv,dShatHatchala,iMisparSidur) == 0 )  //instead of (drChishuvRows.Length == 0)
            {
                if (fErechRechiv > 0)
                {
                    drChishuv = _dtChishuvSidur.NewRow();
                    drChishuv["BAKASHA_ID"] = objOved.iBakashaId;
                    drChishuv["MISPAR_ISHI"] = objOved.Mispar_ishi;
                    drChishuv["TAARICH"] = objOved.Taarich;
                    drChishuv["MISPAR_SIDUR"] = iMisparSidur;
                    drChishuv["SHAT_HATCHALA"] = dShatHatchala;
                    drChishuv["KOD_RECHIV"] = iKodRechiv;
                    drChishuv["ERECH_RECHIV"] = fErechRechiv;
                    _dtChishuvSidur.Rows.Add(drChishuv);
                    drChishuv = null;
                }
            }
            else
            {
                UpdateRowInTable(iKodRechiv, fErechRechiv, dShatHatchala, iMisparSidur);
            }

        }

        private void addAnyRowToTable(int iKodRechiv, DateTime dShatHatchala, int iMisparSidur, float fErechRechiv)
        {
            DataRow drChishuv;
            //            DataRow[] drChishuvRows;
            //            drChishuvRows= objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Select(null, "KOD_RECHIV");
            //            drChishuvRows = objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Select("KOD_RECHIV=" + iKodRechiv + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')");
            if (CountOfRecords(iKodRechiv, dShatHatchala, iMisparSidur) == 0)  //instead of (drChishuvRows.Length == 0)
            {
                    drChishuv = _dtChishuvSidur.NewRow();
                    drChishuv["BAKASHA_ID"] = objOved.iBakashaId;
                    drChishuv["MISPAR_ISHI"] = objOved.Mispar_ishi;
                    drChishuv["TAARICH"] = objOved.Taarich;
                    drChishuv["MISPAR_SIDUR"] = iMisparSidur;
                    drChishuv["SHAT_HATCHALA"] = dShatHatchala;
                    drChishuv["KOD_RECHIV"] = iKodRechiv;
                    drChishuv["ERECH_RECHIV"] = fErechRechiv;
                    _dtChishuvSidur.Rows.Add(drChishuv);
                    drChishuv = null;              
            }
            else
            {
                UpdateRowInTable(iKodRechiv, fErechRechiv, dShatHatchala, iMisparSidur);
            }

        }

        private void UpdateRowInTable(int iKodRechiv, float fErechRechiv, DateTime dShatHatchala, int iMisparSidur)
        {
            DataRow drChishuv;
            _dtChishuvSidur.Select(null, "KOD_RECHIV");
            drChishuv = _dtChishuvSidur.Select("KOD_RECHIV=" + iKodRechiv + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')")[0];
            drChishuv["ERECH_RECHIV"] = fErechRechiv;
            drChishuv = null;
        }

        private void addRowToTable(int iKodRechiv, DateTime dShatHatchala, int iMisparSidur, float fErechRechiv, int iOutMichsa)
        {
            DataRow drChishuv;
//            DataRow[] drChishuvRows;
//            drChishuvRows= objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Select(null, "KOD_RECHIV");
//            drChishuvRows = objOved._dsChishuv.Tables["CHISHUV_SIDUR"].Select("KOD_RECHIV=" + iKodRechiv + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')");
            if (CountOfRecords(iKodRechiv, dShatHatchala, iMisparSidur) == 0)  //instead of (drChishuvRows.Length == 0)
            {
                if (fErechRechiv > 0)
                {
                    drChishuv = _dtChishuvSidur.NewRow();
                    drChishuv["BAKASHA_ID"] = objOved.iBakashaId;
                    drChishuv["MISPAR_ISHI"] = objOved.Mispar_ishi;
                    drChishuv["TAARICH"] = objOved.Taarich;
                    drChishuv["MISPAR_SIDUR"] = iMisparSidur;
                    drChishuv["SHAT_HATCHALA"] = dShatHatchala;
                    drChishuv["KOD_RECHIV"] = iKodRechiv;
                    drChishuv["ERECH_RECHIV"] = fErechRechiv;
                    drChishuv["OUT_MICHSA"] = iOutMichsa;
                    _dtChishuvSidur.Rows.Add(drChishuv);
                    drChishuv = null;
                }
            }
            else
            {
                UpdateRowInTable(iKodRechiv, fErechRechiv, dShatHatchala, iMisparSidur);
            }
        }


        private int CountOfRecords(int iKodRechiv , DateTime dShatHatchala  ,int iMisparSidur )
        {
            try
            {
                return (from c in objOved._dsChishuv.Tables["CHISHUV_SIDUR"].AsEnumerable()
                        where c.Field<int>("KOD_RECHIV").Equals(iKodRechiv)
                        && c.Field<int>("mispar_sidur").Equals(iMisparSidur)
                        && c.Field<DateTime>("SHAT_HATCHALA").Equals(dShatHatchala)
                        && c.Field<DateTime>("taarich").ToShortDateString().Equals(objOved.Taarich.ToShortDateString())
                        select c).Count();
            }
            catch (Exception ex)
            {
                throw new Exception("CountOfRecords:" + ex.StackTrace + "\n message: "+ ex.Message);
            }
        }

        private bool CheckShabat(int iMisparSidur, int iDay, int iSugYom, DateTime dShatHatchalaSidur, DateTime dShatGmarSidur, ref float fErechChol, ref float fErechShabat, bool bCheckMafil)
        {
            bool bShabat, bNextShabat;
            DateTime dShatGmarShabat, dNextDay;
       
            bNextShabat = false;
            bShabat = false;

            if (iDay == clGeneral.enDay.Shabat.GetHashCode())
            { bShabat = true; }
            else
            {
                if (clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, iSugYom, objOved.Taarich))
                { bShabat = true; }
                else { bShabat = false; }
            }

            if (bShabat)
            {
                if (bCheckMafil && (objOved.objPirteyOved.iIsuk == 122 || objOved.objPirteyOved.iIsuk == 123 || objOved.objPirteyOved.iIsuk == 124 || objOved.objPirteyOved.iIsuk == 127) && iMisparSidur == 99001)
                {
                    dShatGmarShabat = objOved.objParameters.dSiyumLilaMotsashMafil;
                }
                else
                {
                    dShatGmarShabat = objOved.objParameters.dShatMaavarYom;
                }

                dNextDay = objOved.Taarich.AddDays(1);

                if ((dNextDay.DayOfWeek.GetHashCode() + 1) == clGeneral.enDay.Shabat.GetHashCode())
                { bNextShabat = true; }
                else { bNextShabat = clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, clGeneral.GetSugYom(objOved.oGeneralData.dtYamimMeyuchadim, dNextDay,objOved.oGeneralData.dtSugeyYamimMeyuchadim), dNextDay); } //, objOved.objMeafyeneyOved.GetMeafyen(56).IntValue), dNextDay); }

                if (bNextShabat)
                { dShatGmarShabat = dShatGmarShabat.AddDays(1); }

                if (dShatGmarSidur > dShatGmarShabat)
                {
                    fErechShabat = float.Parse((dShatGmarShabat - dShatHatchalaSidur).TotalMinutes.ToString());
                    fErechChol = float.Parse((dShatGmarSidur - dShatGmarShabat).TotalMinutes.ToString());
                }
                else
                {
                    fErechChol = 0;
                    fErechShabat = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString());
                }
            }
            else
            {
                fErechChol = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString());
                fErechShabat = 0;
            }
            return bShabat;
        }

        private bool CheckErevShabatChag(int iSugYom, DateTime dShatHatchalaSidur, DateTime dShatGmarSidur, ref float fErechChol, ref float fErechShabat)
        {
            bool bErevShabatChag;
            DateTime dShatHatchlaShabat;

            if (oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim,iSugYom) || oCalcBL.CheckYomShishi(iSugYom))
            {
                bErevShabatChag = true;
                dShatHatchlaShabat = objOved.objParameters.dKnisatShabat;
                if (dShatHatchalaSidur < dShatHatchlaShabat && dShatGmarSidur > dShatHatchlaShabat)
                {
                    fErechShabat = float.Parse((dShatGmarSidur - dShatHatchlaShabat).TotalMinutes.ToString());
                    fErechChol = float.Parse((dShatHatchlaShabat - dShatHatchalaSidur).TotalMinutes.ToString());
                }
                else if (dShatHatchalaSidur < dShatHatchlaShabat && dShatGmarSidur <= dShatHatchlaShabat)
                {
                    fErechShabat = 0;
                    fErechChol = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString());
                }
                else
                {
                    fErechChol = 0;
                    fErechShabat = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString());
                }
            }
            else
            {
                bErevShabatChag = false;
                fErechChol = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString());
                fErechShabat = 0;
            }
            return bErevShabatChag;
        }

        private void CalcGlishaLeyomShishi(DateTime dShatHatchalaSidur, DateTime dShatGmarSidur, ref float fErechChol, ref float fErechShishi)
        {
            DateTime dShatHatchlaErevShabat;


            dShatHatchlaErevShabat = DateTime.Parse(objOved.Taarich.AddDays(1).ToShortDateString() + " 08:00");
            if (dShatGmarSidur < dShatHatchlaErevShabat)
            {
                fErechShishi = 0;
                fErechChol = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString());
            }
            else
            {
                dShatHatchlaErevShabat = objOved.objParameters.dShatMaavarYom; // DateTime.Parse(objOved.Taarich.AddDays(1).ToShortDateString() + objOved.objParameters.GetOneParam(32, dShatHatchlaErevShabat));
                fErechShishi = float.Parse((dShatGmarSidur - dShatHatchlaErevShabat).TotalMinutes.ToString());
                fErechChol = float.Parse((dShatHatchlaErevShabat - dShatHatchalaSidur).TotalMinutes.ToString());
            }
        }

        public float GetSumSidurim100(clGeneral.enRechivim kodRechiv)
        {
            float fDakotNochehutSidur, fMichsaYomit, fErechSidur, fErech = 0;
            DataRow[] _drSidurim = null;
            int iMisparSidur;
            DateTime dShatHatchalaSidur,dGamrLetashlum,dHatchalaLetashlum;
            try
            {
                if ((oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom) && !oCalcBL.CheckYomShishi(objOved.SugYom))
                    || objOved.SugYom == clGeneral.enSugYom.Chol.GetHashCode())
                    _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null and sug_shaot_byom_hol_if_migbala=100");
                if (oCalcBL.CheckYomShishi(objOved.SugYom))
                    if (kodRechiv == clGeneral.enRechivim.Nosafot125 && objOved.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
                    {
                        _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null and (sug_hashaot_beyom_shishi=100 or mispar_sidur=99006)");
}
                    else
                    {
                        _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null and sug_hashaot_beyom_shishi=100");
                    }
            else if (clDefinitions.CheckShaaton(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom, objOved.Taarich))
                    if (kodRechiv == clGeneral.enRechivim.Nosafot125 && objOved.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
                    {
                        _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null and (sug_hashaot_beyom_shabaton=100 or mispar_sidur=99006)");
                    }
                    else
                    {
                        _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null and sug_hashaot_beyom_shabaton=100");
                    }
                if (_drSidurim != null)
                {
                    fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);
                    for (int I = 0; I < _drSidurim.Length; I++)
                    {
                        fErechSidur = 0;
                        iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                        if (kodRechiv == clGeneral.enRechivim.ShaotShabat100)
                        {
                            dHatchalaLetashlum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                            dGamrLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                            fDakotNochehutSidur = float.Parse((dGamrLetashlum - dHatchalaLetashlum).TotalMinutes.ToString());
                        }
                        else
                        {
                            fDakotNochehutSidur = oCalcBL.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')"));
                        }
                        if (kodRechiv == clGeneral.enRechivim.ShaotShabat100 && (iMisparSidur == 99011 || iMisparSidur == 99207 || iMisparSidur == 99007) && fDakotNochehutSidur >= fMichsaYomit)
                            fErechSidur = fMichsaYomit;
                        else
                            fErechSidur = fDakotNochehutSidur;

                        fErech += fErechSidur;
                    }
                }
                return fErech;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public string GetSidurimMeyuchRechiv(int iKodRechiv)
        {
            DataRow[] drSidurim;
            int I;
            string sSidurim = "";
            objOved.oGeneralData.dtSidurimMeyuchRechivAll.Select(null, "KOD_RECHIV");
            drSidurim = objOved.oGeneralData.dtSidurimMeyuchRechivAll.Select("kod_rechiv=" + iKodRechiv + " and me_taarich<=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')  and ad_taarich>=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')");
            if (drSidurim.Length > 0)
            {
                for (I = 0; I < drSidurim.Length; I++)
                {
                    sSidurim = sSidurim + drSidurim[I]["mispar_sidur"] + ",";
                }
                sSidurim = sSidurim.Substring(0, sSidurim.Length - 1);

            }
            drSidurim = null;
            return sSidurim;
        }

        private string GetSugeySidurRechiv(int iKodRechiv)
        {
            DataRow[] drSidurim;
            int I;
            string sSugeySidur = "";
            objOved.oGeneralData.dtSugeySidurRechivAll.Select(null, "KOD_RECHIV");
            drSidurim = objOved.oGeneralData.dtSugeySidurRechivAll.Select("kod_rechiv=" + iKodRechiv + " and me_taarich<=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')  and ad_taarich>=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')");
            if (drSidurim.Length > 0)
            {
                for (I = 0; I < drSidurim.Length; I++)
                {
                    sSugeySidur = sSugeySidur + drSidurim[I]["sug_sidur"] + ",";
                }

            }
            sSugeySidur = "," + sSugeySidur;
            drSidurim = null;
            return sSugeySidur;
        }

        private bool CheckSidurBySectorAvoda(ref DataRow drSidur, int iMisparSidur, int iSectorAvoda)
        {
            bool bCheck = false, bYeshSidur;
            DataRow[] drSidurim;
            int iSugSidur;
            try
            {
                if ((iMisparSidur.ToString()).Substring(0, 2) == "99")
                {
                    drSidurim = GetSidurimMeyuchadim("sector_avoda", iSectorAvoda, iMisparSidur);
                    if (drSidurim.Length > 0) bCheck = true;
                }
                else
                {
                    //SetSugSidur(ref drSidur, objOved.Taarich, iMisparSidur);

                    iSugSidur = int.Parse(drSidur["sug_sidur"].ToString());
                    bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), iSectorAvoda, objOved.Taarich, iSugSidur);
                    if (bYeshSidur) bCheck = true;


                }
                return bCheck;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                drSidurim = null;
            }
        }

        public float GetSumShaotShabat100(ref float fDakotShabat)
        {
            float fDakotNochehutSidur, fDakotNochechut,fMichsaYomit, fErech = 0;
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatSiyum, dTemp, dShatHatchala, dShatHatchalaLetashlum, dShatGmarLetashlum;
            try
            {
                fDakotShabat = 0;
               // iSugYom = SugYom;
                if (oCalcBL.CheckYomShishi(objOved.SugYom) || oCalcBL.CheckErevChag(objOved.oGeneralData.dtSugeyYamimMeyuchadim, objOved.SugYom))
                {
                    fMichsaYomit = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), objOved.Taarich);
                    fDakotNochechut = oCalcBL.GetSumErechRechiv(objOved._dsChishuv.Tables["CHISHUV_YOM"], clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), objOved.Taarich);

                    if (fMichsaYomit > 0 && fDakotNochechut>0)
                    {
                        
                        _drSidurim = objOved.DtYemeyAvodaYomi.Select("Lo_letashlum=0 and mispar_sidur is not null");
                        for (int I = 0; I < _drSidurim.Length; I++)
                        {
                            iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                            if (iMisparSidur != 99006 && iMisparSidur != 99801)
                            {
                                dShatSiyum = DateTime.Parse(_drSidurim[I]["shat_gmar_sidur"].ToString());
                                dShatHatchala = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                                dShatHatchalaLetashlum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                                dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());

                                if (dShatGmarLetashlum > objOved.objParameters.dKnisatShabat && dShatHatchalaLetashlum < objOved.objParameters.dKnisatShabat)
                                {
                                    // dTemp = clDefinitions.GetMinDate(dShatGmarLetashlum, objOved.objParameters.dKnisatShabat);
                                    // fDakotNochehutSidur = float.Parse((dTemp - dShatHatchalaLetashlum).TotalMinutes.ToString());
                                    fDakotNochehutSidur = float.Parse((dShatGmarLetashlum - objOved.objParameters.dKnisatShabat).TotalMinutes.ToString());
                                    fErech += fDakotNochehutSidur;
                                }
                                else if (dShatHatchalaLetashlum >= objOved.objParameters.dKnisatShabat)
                                {
                                    fDakotShabat += float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());

                                }
                            }
                         }
                     }
                }
                return fErech;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _drSidurim = null;
            }
        }

        public float getDakotRezifutLayla(DateTime dShatHatchalaLetashlum, DateTime dShatGmarLetashlum, DateTime Param1, DateTime Param2)
        {
            float fErech;
            try
            {
                fErech = 0;
                if (dShatGmarLetashlum < Param1 && dShatHatchalaLetashlum > Param1)
                {
                    fErech = float.Parse((dShatHatchalaLetashlum - Param1).TotalMinutes.ToString());
                }
                else if (dShatGmarLetashlum >= Param1 && dShatHatchalaLetashlum <= Param2)
                {
                    fErech = float.Parse((dShatHatchalaLetashlum - dShatGmarLetashlum).TotalMinutes.ToString());
                }
                else if ((dShatGmarLetashlum >= Param1 && dShatGmarLetashlum < Param2)
                          && dShatHatchalaLetashlum > Param2)
                {
                    fErech = float.Parse((Param2 - dShatGmarLetashlum).TotalMinutes.ToString());
                }
                //else if (dShatHatchalaLetashlum.Date == objOved.Taarich && dShatGmarLetashlum.Date==objOved.Taarich &&
                //         dShatGmarLetashlum < Param2.AddDays(-1) && dShatHatchalaLetashlum <= Param2.AddDays(-1))
                //{
                //    fErech = float.Parse((dShatHatchalaLetashlum - dShatGmarLetashlum).TotalMinutes.ToString());
                //}
                //else if (dShatHatchalaLetashlum.Date == objOved.Taarich && dShatGmarLetashlum.Date == objOved.Taarich &&
                //         dShatGmarLetashlum < Param2.AddDays(-1) && dShatHatchalaLetashlum > Param2.AddDays(-1))
                //{
                //    fErech = float.Parse((Param2.AddDays(-1) - dShatGmarLetashlum).TotalMinutes.ToString());
                //}

                return fErech;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public float getDakotRezifutBoker(DateTime dShatHatchalaLetashlum, DateTime dShatGmarLetashlum)
        {
            float fErech;
            DateTime param12 = new DateTime();
            try
            {
                fErech = 0;
                param12 = objOved.objParameters.dSiyumTosefetLailaChok;
                if (dShatHatchalaLetashlum.Date == objOved.Taarich && dShatGmarLetashlum.Date == objOved.Taarich &&
                        dShatGmarLetashlum < param12.AddDays(-1) && dShatHatchalaLetashlum <= param12.AddDays(-1))
                {
                    fErech = float.Parse((dShatHatchalaLetashlum - dShatGmarLetashlum).TotalMinutes.ToString());
                }
                else if (dShatHatchalaLetashlum.Date == objOved.Taarich && dShatGmarLetashlum.Date == objOved.Taarich &&
                         dShatGmarLetashlum < param12.AddDays(-1) && dShatHatchalaLetashlum > param12.AddDays(-1))
                {
                    fErech = float.Parse((param12.AddDays(-1) - dShatGmarLetashlum).TotalMinutes.ToString());
                }

                return fErech;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsSidurConenutGriraExist(ref DataRow dr)
        {
            DataRow[]  drSidurim;
            string sSql = "";
            try
            {
                //objOved.oGeneralData.dtSugeySidurAll.Select(null, "sug_sidur");
                //sSql = "sug_sidur=69 and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime')";
                //drSugSidur = objOved.oGeneralData.dtSugeySidurAll.Select(sSql);
                sSql = "sug_sidur=69 and taarich=Convert('" + objOved.Taarich.ToShortDateString() + "', 'System.DateTime') and lo_letashlum=1";// and mispar_sidur=" + drSugSidur[0]["mispar_sidur"];
                drSidurim = objOved.DtYemeyAvodaYomi.Select(sSql);
                if (drSidurim.Length > 0)
                {
                   // sSql = "lo_letashlum=1 and mispar_sidur=" + drSugSidur[0]["mispar_sidur"];
                   // drSidurim = objOved.DtYemeyAvodaYomi.Select(sSql);
                    //if (drSidurim.Length > 0)
                    //{
                        dr = drSidurim[0];
                        return true;
                    //}
                    //else return false;
                }
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                drSidurim = null;
            }
        }

        private bool cheakSidurGrirainConenutGrira(DateTime dShatHatchalaGrira, DateTime dShatGmarGrira, DataRow drConenutGrira)
        {
            DateTime dShatHatchalaConenutGrira, dShatGmarConenutGrira;
            try
            {
                dShatHatchalaConenutGrira = DateTime.Parse(drConenutGrira["shat_hatchala_letashlum"].ToString());
                dShatGmarConenutGrira = DateTime.Parse(drConenutGrira["shat_gmar_letashlum"].ToString());

                if (dShatHatchalaGrira >= dShatHatchalaConenutGrira && dShatGmarGrira <= dShatGmarConenutGrira)
                    return true;
                else if (dShatHatchalaGrira < dShatHatchalaConenutGrira && dShatGmarGrira > dShatHatchalaConenutGrira && dShatGmarGrira <= dShatGmarConenutGrira)
                    return true;
                else if (dShatHatchalaGrira >= dShatHatchalaConenutGrira && dShatGmarGrira > dShatGmarConenutGrira)
                    return true;
                else if (dShatHatchalaGrira < dShatHatchalaConenutGrira && dShatGmarGrira > dShatGmarConenutGrira)
                    return true;
                else return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataRow[] getPeiluyot(int iMisparSidur, DateTime dShatHatchalaSidur, string sCondition)
        {
            DataRow[] drPeiluyot;
            string sQury;
            try
            {
                sQury = "MISPAR_SIDUR=" + iMisparSidur + " and SHAT_HATCHALA_SIDUR=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime')";
                if (sCondition != "")
                    sQury += " and " + sCondition;
                drPeiluyot = objOved.DtPeiluyotYomi.Select(sQury, "shat_yetzia asc");
                return drPeiluyot;
            }
            catch (Exception ex)
            {
                //  clLogBakashot.SetError(objOved.iBakashaId, "E", null, clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(),  objOved.Mispar_ishi, objOved.Taarich, iMisparSidur, dShatHatchla, dShatYetzia, iMisparKnisa, "CalcPeilut: " + ex.StackTrace + "\n message: "+ ex.Message, null);
                throw (ex);
            }
            
        }
    }

}


