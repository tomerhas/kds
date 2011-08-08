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
    class clCalcSidur : GlobalDataset
    {
        public DateTime dTaarich;
        private int _iMisparIshi;
        private long _lBakashaId;

        private DataTable _dtChishuvSidur;
        private clCalcGeneral _oGeneralData;
        public  clCalcPeilut oPeilut;

        public clCalcSidur(int iMisparIshi, long lBakashaId, clCalcGeneral oGeneralData)
        {
            _iMisparIshi = iMisparIshi;
            _lBakashaId = lBakashaId;
            _oGeneralData = oGeneralData;

            _dtChishuvSidur = clCalcData.DtSidur;

        }

       
        public void CalcRechiv1()
        {
            //דקות נוכחות לתשלום( רכיב 1):
              //שעת גמר לתשלום של סידור פחות שעת התחלה לתשלום של סידור.
            //[TB_Sidurim_Ovedim.Shat_gmar_Letashlum] פחות [TB_Sidurim_Ovedim.Shat_hatchala_Letashlum]
            // לפתוח רכיב לכל הסידורים הרגילים וכן לפתוח רשומה עבור כל הסידורים המיוחדים (מספר סידור מתחיל ב- "99") הנסכמים לרכיב על פי [שליפת סידורים מיוחדים לרכיב (קוד רכיב = 1)].
            int iMisparSidur, iSugSidur;
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
                oPeilut.dTaarich=dTaarich;
                _drSidurRagil = GetSidurimRegilim();
                if (_drSidurRagil.Length > 0)
                {
                    for (int I = 0; I < _drSidurRagil.Length ; I++)
                    {

                        iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_sidur"].ToString());
                        dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                        dShatGmarLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_gmar_letashlum"].ToString());
                        fErechRechiv = float.Parse(_drSidurRagil[I]["ZMAN_LELO_HAFSAKA"].ToString()); //float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());
                        
                        // //SetSugSidur(ref _drSidurRagil[I],dTaarich, iMisparSidur);

                        //iSugSidur = int.Parse(_drSidurRagil[I]["sug_sidur"].ToString());
                        //if (iSugSidur == 69)
                        //{
                        //    //•	סידור כוננות גרירה – אם סוג סידור = 69 
                        //    if (fErechRechiv > _oGeneralData.objParameters.iMinZmanGriraDarom)
                        //    {
                        //        if (int.Parse(iMisparSidur.ToString().Substring(0, 2)) > 11)
                        //        {
                        //            fErechRechiv = _oGeneralData.objParameters.iMinZmanGriraDarom;
                        //        }
                        //        else
                        //        {
                        //            fErechRechiv = _oGeneralData.objParameters.iMinZmanGriraTzafon;
                        //        }
                        //    }
                        //    else if (fErechRechiv > _oGeneralData.objParameters.iMinZmanGriraTzafon && fErechRechiv <= _oGeneralData.objParameters.iMinZmanGriraDarom)
                        //    {
                        //        if (int.Parse(iMisparSidur.ToString().Substring(0, 2)) <= 11)
                        //        {
                        //            fErechRechiv = _oGeneralData.objParameters.iMinZmanGriraTzafon;
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
                    _drSidurMeyuchad = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and (sidur_misug_headrut is null or sidur_misug_headrut=3) AND MISPAR_SIDUR IN(" + sSidurim + ") and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')","taarich asc");
                    fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString()));
                       
                    for (int I = 0; I < _drSidurMeyuchad.Length; I++)
                    {
                        dShatHatchalaSidur = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_sidur"].ToString());
                        iMisparSidur = int.Parse(_drSidurMeyuchad[I]["mispar_sidur"].ToString());
                      
                        fErechRechiv = CalcRechiv1BySidur(_drSidurMeyuchad[I], fMichsaYomit, oPeilut);

                        if (string.IsNullOrEmpty(_drSidurMeyuchad[I]["sidur_misug_headrut"].ToString()))
                             fSumErechRechiv += fErechRechiv;

                    //    fErechRechiv = fErechRechiv - int.Parse(_drSidurRagil[I]["ZMAN_LELO_HAFSAKA"].ToString());// oPeilut.getZmanHafsakaBesidur(iMisparSidur, dShatHatchalaSidur);
                        addRowToTable(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);
                   }

                    //ב.	אם מדובר בסידור היעדרות שאינו מילואים [שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 53 )] עם ערך כלשהו <> 3 יש לבצע את החישוב לאחר סיום חישוב רכיב זה לכל שאר הסידורים ביום. יש לחשב את X = סכום ערך הרכיב עבור כלל הסידורים ביום העבודה שאינם סידורי היעדרות (לא כולל מילואים). כלומר אין לכלול בסכימה את הסידורים בעלי מאפיין 53 עם ערך כלשהו השונה מ- 3 [שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 53 )]  . 
                        //אם X >= ממכסה יומית מחושבת (רכיב 126) אזי אין לפתוח רשומה לרכיב זה לסידור.
                        //אחרת, ערך הרכיב = הנמוך מבין (נוכחות מחושבת, מכסה יומית מחושבת (רכיב 126) פחות X)

                    _drSidurMeyuchad = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and sidur_misug_headrut is not null and sidur_misug_headrut<>3 AND MISPAR_SIDUR IN(" + sSidurim + ") and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')", "taarich asc");

                    for (int I = 0; I < _drSidurMeyuchad.Length; I++)
                    {
                        dShatHatchalaSidur = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_sidur"].ToString());
                        iMisparSidur = int.Parse(_drSidurMeyuchad[I]["mispar_sidur"].ToString());
                       fZmanAruhatZharayim= clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                       if (fSumErechRechiv < (fMichsaYomit + fZmanAruhatZharayim))
                        {
                            fErechRechiv = CalcRechiv1BySidur(_drSidurMeyuchad[I], fMichsaYomit, oPeilut);
                            fErechRechiv = Math.Min(fErechRechiv, (fMichsaYomit + fZmanAruhatZharayim - fSumErechRechiv));
                            fSumErechRechiv += fErechRechiv;
                            addRowToTable(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

       
        public float CalcRechiv1BySidur(DataRow drSidur, float fMichsaYomit,  clCalcPeilut oPeilut)
        {
            float fErechRechiv, fErechPeiluyot;
            int iMisparSidur, iSugYom;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dTempDtFrom, dTempDtTo, dShatHatchalaSidur;
           
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                iMisparSidur = int.Parse(drSidur["mispar_sidur"].ToString());
                dShatHatchalaSidur = DateTime.Parse(drSidur["shat_hatchala_sidur"].ToString());

                //  iDay = int.Parse(_drSidurMeyuchad[I]["day_taarich"].ToString());
                iSugYom = clCalcData.iSugYom;
                 dShatHatchalaLetashlum = DateTime.Parse(drSidur["shat_hatchala_letashlum"].ToString());
                 dShatGmarLetashlum = DateTime.Parse(drSidur["shat_gmar_letashlum"].ToString());

                 fErechRechiv = (float)((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes);

                //יוצאי דופן סידורים מיוחדים  
                if (iMisparSidur == 99707)
                    fErechRechiv = fMichsaYomit + 120;
                else if (iMisparSidur == 99708)
                    fErechRechiv = fMichsaYomit + 60;
                else if (iMisparSidur == 99010)
                {
                    //•	סידור 99010 (8549) – כדורגל: הנמוך מבין (נוכחות מחושבת, 180 שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 60 ) ).
                    fErechRechiv = Math.Min(fErechRechiv, int.Parse(drSidur["max_dakot_boded"].ToString()));
                }
                else if (iMisparSidur == 99706)
                {
                    //•	סידור 99706 (8552) – קייטנה מאבטח: הנמוך מבין (נוכחות מחושבת, מכסה יומית מחושבת (רכיב 126)).
                    fErechRechiv = fMichsaYomit; // Math.Min(fErechRechiv, fMichsaYomit);

                }
                else if (iMisparSidur == 99703)
                {
                    //	סידור 99703 (8567) – קייטנה השתלמות טרום: 
                    if (fMichsaYomit > 0)
                    {
                        ////אם קיימת מכסה יומית מחושבת (רכיב 126): הנמוך מבין (נוכחות מחושבת, מכסה יומית מחושבת (רכיב 126)).
                        fErechRechiv = Math.Min(fErechRechiv, fMichsaYomit);
                    }
                    else
                    {
                        // לא קיימת מכסה יומית מחושבת (רכיב 126) וזה יום שישי : הנמוך מבין (נוכחות מחושבת,390 [שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 18 ])
                        if (clCalcData.CheckYomShishi(iSugYom))
                        {
                            fErechRechiv = Math.Min(fErechRechiv, int.Parse(drSidur["max_shaot_byom_shishi"].ToString()));
                        }
                        //-	אם לא קיימת מכסה וזה שבת/שבתון [זיהוי שבת/ון]: הנמוך מבין (נוכחות מחושבת,480 [שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 19 ]).
                        else if (clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, iSugYom, dTaarich))
                        {
                            fErechRechiv = Math.Min(fErechRechiv, int.Parse(drSidur["max_shaot_beshabaton"].ToString()));
                        }
                    }
                }
                else if (iMisparSidur == 99700)
                {
                    //•	סידור 99700 (8589) – אירועי קיץ 
                    //-	אם לא שבת/שבתון: מכסה יומית (רכיב 126) + 390 [שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 62 ]). 
                    if (!clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, iSugYom, dTaarich))
                    {
                        fErechRechiv = fMichsaYomit + int.Parse(drSidur["dakot_n_letashlum_hol"].ToString());
                    }
                    else
                    {
                        //-	אם שבת/שבתון [זיהוי שבת/ון]: 240 [שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 19 )].
                        fErechRechiv = int.Parse(drSidur["max_shaot_beshabaton"].ToString());
                    }
                }
                else if (iMisparSidur == 99711)
                {
                    //•	סידור 99711 (חדש)– אירועי קיץ – חוצה ישראל 
                    //-	אם לא שבת/שבתון : מכסה יומית (רכיב 126) + 390 [שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 62 )]). 
                    if (!clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, iSugYom, dTaarich))
                    {
                        fErechRechiv = fMichsaYomit + int.Parse(drSidur["dakot_n_letashlum_hol"].ToString());
                    }
                    else
                    {
                        //-	בשבת/שבתון: 420 שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 19 ).
                        fErechRechiv = int.Parse(drSidur["max_shaot_beshabaton"].ToString());
                    }
                }
                else if (iMisparSidur == 99006)
                {//•	סידור 99006 (8554) – שליחות בחו"ל: 
                    if (iSugYom == clGeneral.enSugYom.Chol.GetHashCode())
                    {
                        //-	ימים א – ה : מכסה יומית (רכיב 126) + 120 דקות שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 62 ). 
                        fErechRechiv = fMichsaYomit + int.Parse(drSidur["dakot_n_letashlum_hol"].ToString());

                    }
                    //-	שישי/ערב חג [זיהוי ערב חג] ושבת/שבתון [זיהוי שבת/ון]: מעמד חוזה (מעמד 23) = 240 דקות ,  שאר המעמדות = 300 דקות שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 19 ).
                    else if (clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, iSugYom, dTaarich) || clCalcData.CheckErevChag(iSugYom))
                    {
                        fErechRechiv = int.Parse(drSidur["max_shaot_beshabaton"].ToString());
                    }

                }
                else if (iMisparSidur == 99014)
                {
                    //•	סידור 99014 (8547) – תרבות : 
                    //-	שישי/ערב חג [זיהוי ערב חג] ושבת/שבתון [זיהוי שבת/ון]: הנמוך מבין (נוכחות מחושבת, 240 שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 19 )) 
                    if (clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, iSugYom, dTaarich) || clCalcData.CheckErevChag(iSugYom))
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
                else if (iMisparSidur == 99207 || iMisparSidur == 99011 || iMisparSidur == 99007)
                {
                    //•	99207 (8512) – קורס, 99011 (8513) – קורס
                    if (iSugYom == clGeneral.enSugYom.Chol.GetHashCode() || clCalcData.CheckErevChag(iSugYom))
                    {
                        //-	ימים א – ה וערב חג [זיהוי ערב חג]: הנמוך מבין (נוכחות מחושבת, מכסה יומית (רכיב 126)).
                        if (fErechRechiv>=480)
                          fErechRechiv = fMichsaYomit;
                     }
                    else if (clCalcData.CheckYomShishi(iSugYom))
                    {
                        // -	שישי: אם מכסה יומית (רכיב 126) > 0 אזי הנמוך מבין (נוכחות מחושבת, מכסה יומית (רכיב 126)).
                        if (fMichsaYomit > 0)
                        {
                            if (fErechRechiv >= 480)
                                fErechRechiv = fMichsaYomit;
                            
                        }
                        else
                        {
                            //אחרת: הנמוך מבין (נוכחות מחושבת, 390 שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 18 ). 
                            fErechRechiv = Math.Min(fErechRechiv, int.Parse(drSidur["max_shaot_byom_shishi"].ToString()));
                        }
                    }
                }
                else if (iMisparSidur == 99013)
                {
                    //•	סידור 99013 (8585) – שעות 100% בלבד
                    // הנמוך מבין (נוכחות מחושבת, מכסה יומית (רכיב 126) ).
                    if (fMichsaYomit > 0)
                        fErechRechiv = Math.Min(fErechRechiv, fMichsaYomit);
                    else fErechRechiv = fErechRechiv;
                }
                else if (iMisparSidur == 99220)
                {
                    //•	סידור גרירה בפועל 99220 : ערך הרכיב = ערך רכיב זמן גרירות (רכיב 128) ברמת סידור 
                    fErechRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.ZmanGrirot.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                }

                oPeilut.CalcRechiv1(iMisparSidur, dShatHatchalaSidur);

                fErechPeiluyot = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_PEILUT"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                fErechPeiluyot = fErechPeiluyot - (int.Parse(drSidur["Hafhatat_Nochechut_Visa"].ToString()) * 60);

                //•	עבור סידורי ויזה "תיירות פנים": 
                if (!string.IsNullOrEmpty(drSidur["sidur_namlak_visa"].ToString()))
                {
                    if (int.Parse(drSidur["sidur_namlak_visa"].ToString()) == 2)
                    {
                        if (int.Parse(drSidur["yom_VISA"].ToString()) == 4)
                        {
                            fErechRechiv = Math.Min(fErechRechiv, _oGeneralData.objParameters.iMaxNochehutVisaBodedet);
                            if (fErechRechiv == _oGeneralData.objParameters.iMaxNochehutVisaBodedet)
                            { fErechRechiv = fErechRechiv + fErechPeiluyot; }
                        }
                        else if (int.Parse(drSidur["yom_VISA"].ToString()) == 1 && fMichsaYomit > 0)
                        {
                            dTempDtFrom = clGeneral.GetDateTimeFromStringHour("00:01", dTaarich.Date);
                            dTempDtTo = clGeneral.GetDateTimeFromStringHour("13:59", dTaarich.Date);

                            if (DateTime.Parse(drSidur["shat_hatchala_letashlum"].ToString()) >= dTempDtFrom && DateTime.Parse(drSidur["shat_hatchala_letashlum"].ToString()) <= dTempDtTo)
                            {
                                fErechRechiv = _oGeneralData.objParameters.iNuchehutVisa1;
                                fErechRechiv = fErechRechiv + fErechPeiluyot;
                            }

                            dTempDtFrom = clGeneral.GetDateTimeFromStringHour("14:00", dTaarich.Date);
                            dTempDtTo = clGeneral.GetDateTimeFromStringHour("24:00", dTaarich.Date);

                            if (DateTime.Parse(drSidur["shat_hatchala_letashlum"].ToString()) >= dTempDtFrom && DateTime.Parse(drSidur["shat_hatchala_letashlum"].ToString()) <= dTempDtTo)
                            {
                                fErechRechiv = Math.Min(_oGeneralData.objParameters.iMaxNuchehutVisaPnimRishon1, Math.Max(fErechRechiv, _oGeneralData.objParameters.iMinNuchehutVisaPnimRishon1));
                                if (fErechRechiv == _oGeneralData.objParameters.iMaxNuchehutVisaPnimRishon1)
                                { fErechRechiv = fErechRechiv + fErechPeiluyot; }
                            }

                            if (iSugYom == clGeneral.enSugYom.Shishi.GetHashCode())
                            {
                                fErechRechiv = Math.Min(_oGeneralData.objParameters.iMaxNuchehutVisaPnimRishon2, Math.Max(fErechRechiv, _oGeneralData.objParameters.iMinNuchehutVisaPnimRishon2));
                                if (fErechRechiv == _oGeneralData.objParameters.iMaxNuchehutVisaPnimRishon2)
                                { fErechRechiv = fErechRechiv + fErechPeiluyot; }
                            }

                            if (clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, iSugYom, dTaarich))
                            {
                                fErechRechiv = Math.Min(_oGeneralData.objParameters.iMaxNochehutVisaPnim, Math.Max(fErechRechiv, _oGeneralData.objParameters.iMinNochehutVisaPnim));
                                if (fErechRechiv == _oGeneralData.objParameters.iMaxNochehutVisaPnim)
                                { fErechRechiv = fErechRechiv + fErechPeiluyot; }
                            }
                        }
                        else if (int.Parse(drSidur["yom_VISA"].ToString()) == 2 && !clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, iSugYom, dTaarich) && (iSugYom == clGeneral.enSugYom.Chol.GetHashCode() || iSugYom == clGeneral.enSugYom.Shishi.GetHashCode()))
                        {
                            fErechRechiv = _oGeneralData.objParameters.iNochehutVisaPnimNoShabaton;
                            fErechRechiv = fErechRechiv - (int.Parse(drSidur["Hafhatat_Nochechut_Visa"].ToString()) * 60);
                        }
                        else if (int.Parse(drSidur["yom_VISA"].ToString()) == 2 && clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, iSugYom, dTaarich))
                        {
                            fErechRechiv = Math.Min(_oGeneralData.objParameters.iMaxNochehutVisaPnimEmtzai, Math.Max(fErechRechiv, _oGeneralData.objParameters.iMinNochehutVisaPnimEmtzai));
                            fErechRechiv = fErechRechiv - (int.Parse(drSidur["Hafhatat_Nochechut_Visa"].ToString()) * 60);
                        }
                        else if (int.Parse(drSidur["yom_VISA"].ToString()) == 3 && !clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, iSugYom, dTaarich) && (iSugYom == clGeneral.enSugYom.Chol.GetHashCode() || iSugYom == clGeneral.enSugYom.Shishi.GetHashCode()))
                        {
                            fErechRechiv = Math.Min(_oGeneralData.objParameters.iMaxNochehutVisaPnimAcharon, Math.Max(fErechRechiv, _oGeneralData.objParameters.iMinNochehutVisaPnimAcharon));
                            if (fErechRechiv == _oGeneralData.objParameters.iMaxNochehutVisaPnimAcharon)
                            { fErechRechiv = fErechRechiv + fErechPeiluyot; }
                        }
                        else if (int.Parse(drSidur["yom_VISA"].ToString()) == 3 && clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, iSugYom, dTaarich))
                        {
                            fErechRechiv = Math.Min(_oGeneralData.objParameters.iMaxNochehutVisaPnimEmtzai, Math.Max(fErechRechiv, _oGeneralData.objParameters.iMinNochehutVisaPnimAcharon));
                            if (fErechRechiv == _oGeneralData.objParameters.iMaxNochehutVisaPnimEmtzai)
                            { fErechRechiv = fErechRechiv + fErechPeiluyot; }
                        }

                    }
                    //עבור סידורי ויזה "צבאית"
                    else if (int.Parse(drSidur["sidur_namlak_visa"].ToString()) == 1)
                    {
                        if (int.Parse(drSidur["yom_VISA"].ToString()) == 4)
                        {
                            fErechRechiv = fErechRechiv - (int.Parse(drSidur["Hafhatat_Nochechut_Visa"].ToString()) * 60);
                        }
                        //•	: סידורים מיוחדים בעלי שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 45 ) עם ערך = 1. 
                        else if (int.Parse(drSidur["yom_VISA"].ToString()) == 1)
                        {
                            fErechRechiv = _oGeneralData.objParameters.iMaxNochehutVisaTzahalLoAcharon;
                            fErechRechiv = fErechRechiv + fErechPeiluyot;
                        }
                        if (int.Parse(drSidur["yom_VISA"].ToString()) == 2)
                        {
                            fErechRechiv = _oGeneralData.objParameters.iMaxNochehutVisaTzahalLoAcharon;
                            fErechRechiv = fErechRechiv - (int.Parse(drSidur["Hafhatat_Nochechut_Visa"].ToString()) * 60);
                        }
                        else if (int.Parse(drSidur["yom_VISA"].ToString()) == 3)
                        {
                            fErechRechiv = Math.Min(fErechRechiv, _oGeneralData.objParameters.iMaxNochehutVisaTzahalLoAcharon);
                            if (fErechRechiv == _oGeneralData.objParameters.iMaxNochehutVisaTzahalLoAcharon)
                            { fErechRechiv = fErechRechiv + fErechPeiluyot; }
                        }
                    }
                }
                return fErechRechiv;
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, 0, _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcRechiv1BySidur: " + ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv2()
        {
            //דקות נהגות בימי חול ( רכיב 2):
            // יש לבצע את החישוב עבור סידורים המוגדרים כסידורי נהגות – זיהוי סידורי נהיגה:
            //  ישנו מאפיין (קוד מאפיין = 3) לסוג סידור/סידור מיוחד המתאר את סוג העבודה   .
            //ערך מאפיין זה (סוג עבודה) = 5 = עבודת נהגות.
            int  iMisparSidur, iSugSidur;
            bool bYeshSidur;
            int  iDay, iSugYom, iMichutzLamichsa;
            float fTosefetGrirotHatchala, fTosefetGrirotSof, fZmanHafsaka;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            DataRow[] _drSidurMeyuchad,  _drSidurRagil;;
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
                                        
                        //SetSugSidur(ref _drSidurRagil[I],dTaarich, iMisparSidur);

                        iSugSidur = int.Parse(_drSidurRagil[I]["sug_sidur"].ToString());
                               
                                    bYeshSidur=CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(),clGeneral.enSectorAvoda.Nahagut.GetHashCode(),dTaarich,iSugSidur);
                                    if (bYeshSidur)
                                    {
                                        
                                        iDay = int.Parse(_drSidurRagil[I]["day_taarich"].ToString());
                                        iSugYom = clCalcData.iSugYom;
                                        dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                                        fTosefetGrirotHatchala = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                                        fTosefetGrirotSof = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                                        dShatHatchalaLetashlum = dShatHatchalaLetashlum.AddMinutes(-fTosefetGrirotSof);
                                        dShatGmarLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_gmar_letashlum"].ToString());
                                        dShatGmarLetashlum = dShatGmarLetashlum.AddMinutes(+fTosefetGrirotHatchala);         
                                        iMichutzLamichsa = int.Parse(_drSidurRagil[I]["out_michsa"].ToString());
                                        fZmanHafsaka = float.Parse(_drSidurRagil[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());
                                        CheckSidurCholToAdd(clGeneral.enRechivim.DakotNehigaChol.GetHashCode(), iMisparSidur, iDay, iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false, fZmanHafsaka);
            
                                     }
                                 }
                         
                 }

                //בדיקת סידורים מיוחדים
                // נלקחים רק סידורים מיוחדים מסקטור עבודה =  נהגות
               _drSidurMeyuchad = GetSidurimMeyuchadim("sector_avoda",clGeneral.enSectorAvoda.Nahagut.GetHashCode());
             
                for (int I=0;I<=_drSidurMeyuchad.Length-1;I++)
                  {
                     iMisparSidur=int.Parse(_drSidurMeyuchad[I]["mispar_sidur"].ToString());
                     dShatHatchalaSidur = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_sidur"].ToString());
                    
                    iDay = int.Parse(_drSidurMeyuchad[I]["day_taarich"].ToString());
                    iSugYom = clCalcData.iSugYom;
                     dShatHatchalaLetashlum=DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_letashlum"].ToString());
                     fTosefetGrirotHatchala = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                     fTosefetGrirotSof = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                     dShatHatchalaLetashlum = dShatHatchalaLetashlum.AddMinutes(-fTosefetGrirotSof);
                     dShatGmarLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_gmar_letashlum"].ToString());
                     dShatGmarLetashlum = dShatGmarLetashlum.AddMinutes(+fTosefetGrirotHatchala);     
                     iMichutzLamichsa = int.Parse(_drSidurMeyuchad[I]["out_michsa"].ToString());
                     fZmanHafsaka = float.Parse(_drSidurMeyuchad[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());
                      CheckSidurCholToAdd(clGeneral.enRechivim.DakotNehigaChol.GetHashCode(), iMisparSidur, iDay, iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false, fZmanHafsaka);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotNehigaChol.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv3()
        {
            //דקות בניהול תנועה בימי חול ( רכיב 3):
            // יש לבצע את החישוב עבור סידורים המוגדרים כסידורי נהגות – זיהוי סידורי נהיגה:
            //  ישנו מאפיין (קוד מאפיין = 3) לסוג סידור/סידור מיוחד המתאר את סוג העבודה   .
            //ערך מאפיין זה (סוג עבודה) = 4 =  ניהול תנועה.
            int iMisparSidur, iSugSidur;
            bool bYeshSidur;
            int  iDay, iSugYom, iMichutzLamichsa;
             DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            DataRow[] _drSidurMeyuchad,_drSidurRagil;
             iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            float fZmanHafsaka;
            try
            {
                //בדיקת סידורים רגילים
                // נלקחים הסידורים הרגילים שסקטור העבודה שלהם =ניהול תנועה
                _drSidurRagil = GetSidurimRegilim();
                if (_drSidurRagil.Length > 0)
                {
                    for (int I = 0; I < _drSidurRagil.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_sidur"].ToString());
                                    
                       //SetSugSidur(ref _drSidurRagil[I],dTaarich, iMisparSidur);

                        iSugSidur = int.Parse(_drSidurRagil[I]["sug_sidur"].ToString());
                                bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nihul.GetHashCode(), dTaarich, iSugSidur);
                                if (bYeshSidur)
                                {
                                    
                                    iDay = int.Parse(_drSidurRagil[I]["day_taarich"].ToString());
                                    iSugYom = clCalcData.iSugYom;
                                    dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                                    dShatGmarLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_gmar_letashlum"].ToString());
                                    iMichutzLamichsa = int.Parse(_drSidurRagil[I]["out_michsa"].ToString());
                                    fZmanHafsaka = float.Parse(_drSidurRagil[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());
                                    CheckSidurCholToAdd(clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode(), iMisparSidur, iDay, iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false, fZmanHafsaka);

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
                   
                    iDay = int.Parse(_drSidurMeyuchad[I]["day_taarich"].ToString());
                    iSugYom = clCalcData.iSugYom;
                    dShatHatchalaLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_letashlum"].ToString());
                    dShatGmarLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_gmar_letashlum"].ToString());
                    iMichutzLamichsa = int.Parse(_drSidurMeyuchad[I]["out_michsa"].ToString());
                    fZmanHafsaka = float.Parse(_drSidurMeyuchad[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());

                    CheckSidurCholToAdd(clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode(), iMisparSidur, iDay, iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false, fZmanHafsaka);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv4()
        {
            //דקות בתפקיד בימי חול ( רכיב 4):
            // יש לבצע את החישוב עבור סידורים המוגדרים כסידורי נהגות – זיהוי סידורי נהיגה:
            //  ישנו מאפיין (קוד מאפיין = 3) לסוג סידור/סידור מיוחד המתאר את סוג העבודה   .
            //ערך מאפיין זה (סוג עבודה) = 1 =  תפקיד.
            int iMisparSidur, iSugSidur;
            bool bYeshSidur;
            float fZmanHafsaka;
            int  iDay, iSugYom, iMichutzLamichsa;
            DataRow[] _drSidurMeyuchad, _drSidurRagil;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                //בדיקת סידורים רגילים
                // נלקחים הסידורים הרגילים שסקטור העבודה שלהם =תפקיד
                _drSidurRagil = GetSidurimRegilim();
                if (_drSidurRagil.Length > 0)
                {
                    for (int I = 0; I < _drSidurRagil.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_sidur"].ToString());
                                  
                       //SetSugSidur(ref _drSidurRagil[I],dTaarich, iMisparSidur);

                        iSugSidur = int.Parse(_drSidurRagil[I]["sug_sidur"].ToString());
                                bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Tafkid.GetHashCode(), dTaarich, iSugSidur);
                                if (bYeshSidur)
                                {
                                    
                                    iDay = int.Parse(_drSidurRagil[I]["day_taarich"].ToString());
                                    iSugYom = clCalcData.iSugYom;
                                    dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                                    dShatGmarLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_gmar_letashlum"].ToString());
                                    iMichutzLamichsa = int.Parse(_drSidurRagil[I]["out_michsa"].ToString());
                                    fZmanHafsaka = float.Parse(_drSidurRagil[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());

                                    CheckSidurCholToAdd(clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), iMisparSidur, iDay, iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false, fZmanHafsaka);

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
                    
                    iDay = int.Parse(_drSidurMeyuchad[I]["day_taarich"].ToString());
                    iSugYom = clCalcData.iSugYom;

                    dShatHatchalaLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_letashlum"].ToString());
                    dShatGmarLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_gmar_letashlum"].ToString());
                    iMichutzLamichsa = int.Parse(_drSidurMeyuchad[I]["out_michsa"].ToString());
                    fZmanHafsaka = float.Parse(_drSidurMeyuchad[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());

                    CheckSidurCholToAdd(clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), iMisparSidur, iDay, iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, true, fZmanHafsaka);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotTafkidChol.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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
        //        if (clCalcGeneral.objMeafyeneyOved.iMeafyen33 == 40)
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
        //        throw new Exception("Error Calc Sidur Rechiv 5: " + ex.Message);
        //    }
        //}

        public void CalcRechiv7()
        {
            //  דקות זיכוי חופש (רכיב 7)  
            //הרכיב רלוונטי רק כאשר לעובד יש מאפיין המרה, מאפיין ביצוע קוד= 31 עם ערך = 1. אחרת, אין לפתוח רשומה עבור הרכיב בכל הרמות.
            int iMisparSidur;
            int iDay, iSugYom, iMichutzLamichsa;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            DataRow[] _drSidurim;
            iMisparSidur = 0;
            float fZmanHafsaka;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {

                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime') and Hamarat_shabat=1");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    iMichutzLamichsa = int.Parse(_drSidurim[I]["out_michsa"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    fZmanHafsaka = float.Parse(_drSidurim[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());
                    iDay = int.Parse(_drSidurim[I]["day_taarich"].ToString());
                    iSugYom = clCalcData.iSugYom;
                    dShatHatchalaLetashlum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                    dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());

                    CheckSidurShabatToAdd(clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode(), iMisparSidur, iDay, iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false, fZmanHafsaka);
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv10()
        {
            //  דקות לתוספת מיוחדת בתפקיד  - תמריץ (רכיב 10) 
            //יש לפתוח רשומה לרכיב ברמת הסידור רק עבור 
            //סידורים מזכים לתמריץ לפי [שליפת סידורים מיוחדים לרכיב (קוד רכיב=10)] 
            float fZmanHafsaka;
            int iMisparSidur;
            int iDay, iSugYom, iMichutzLamichsa;
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
                    _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and MISPAR_SIDUR IN(" + sSidurim + ") and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                    for (int I = 0; I < _drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                        iMichutzLamichsa = int.Parse(_drSidurim[I]["out_michsa"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                        iMichutzLamichsa = int.Parse(clCalcData.CheckOutMichsa(_iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, iMichutzLamichsa).GetHashCode().ToString());                
                      
                        iDay = int.Parse(_drSidurim[I]["day_taarich"].ToString());
                        iSugYom = clCalcData.iSugYom;
                        dShatHatchalaLetashlum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                        dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());

                        fZmanHafsaka = float.Parse(_drSidurim[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());
                        bMezake = false;
                        if (iMisparSidur == 99400 || iMisparSidur == 99402)
                        {
                            oPeilut.dTaarich= dTaarich;
                            //dtPeiluyot = oPeilut.GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);
                            //drPeiluyot = dtPeiluyot.Select("SUBSTRING(makat_nesia,1,1)=7 and sector_zvira_zman_haelement=6");
                            sQury = "MISPAR_SIDUR=" + iMisparSidur + " AND taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime') and ";
                            sQury += "SHAT_HATCHALA_SIDUR=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and (SUBSTRING(makat_nesia,1,1)=7 and sector_zvira_zman_haelement=6)";
                            drPeiluyot = clCalcData.DtPeiluyotOved.Select(sQury, "shat_yetzia asc");
                            if (drPeiluyot.Length > 0)
                            {
                                bMezake = true;
                            }
                        }
                        else
                        { bMezake = true; }

                        if (bMezake)
                        { CheckSidurCholToAdd(clGeneral.enRechivim.DakotTamritzTafkid.GetHashCode(), iMisparSidur, iDay, iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false, fZmanHafsaka); }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotTamritzTafkid.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv12()
        {
            //  דקות לתוספת משק  (רכיב 12) 
            //  	יש לפתוח רשומות לרכיב זה רק אם העובד זכאי לקבל תמריץ: לפי [שליפת מאפיין ביצוע (קוד מאפיין = 45, מ.א., תאריך)] עם גדול מ-0 - עובד משק המזכה לתוספת משק.
          
            try
            {
                if (_oGeneralData.objMeafyeneyOved.sMeafyen45.Trim() != "")
                {
                    if (int.Parse(_oGeneralData.objMeafyeneyOved.sMeafyen45) >0)
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
            DateTime  dShatGmarSidur, dShatHatchalaSidur;
            DataRow[] _drSidurim;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                for (int I = 0; I < _drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
   
                        dShatGmarSidur= DateTime.Parse(_drSidurim[I]["shat_gmar_sidur"].ToString());

                        fErech = float.Parse(_drSidurim[I]["ZMAN_LELO_HAFSAKA"].ToString()); //float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString());
                       // fErech = fErech - oPeilut.getZmanHafsakaBesidur(iMisparSidur, dShatHatchalaSidur);
                        addRowToTable(clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                    }
           
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv22()
        {
            //כמות גמול חסכון (רכיב 22
           //יש לשלוף את רשימת הסידורים המיוחדים וסוגי הסידור הנחשבים לסידורים המזכים לגמול חיסכון כדלקמן:
            // סידורים מיוחדים לרכיב (קוד רכיב) המסתכמים לרכיב קוד רכיב = 22.
            // סוגי סידור לרכיב (קוד רכיב) המסתכמים לרכיב קוד רכיב = 22.
            string sSidurimMeyuchadim, sSugeySidur;
            int iMisparSidur, iKodRechiv,  iSugSidur;
            float fErech, fSumDakotRechiv;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            DataRow[] drSidurim,dr;
            iKodRechiv = clGeneral.enRechivim.KamutGmulChisachon.GetHashCode();
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                sSidurimMeyuchadim = GetSidurimMeyuchRechiv(iKodRechiv);
                sSugeySidur = GetSugeySidurRechiv(iKodRechiv);
                if (sSidurimMeyuchadim.Length > 0 )
                {
                    drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ") and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                       
                        dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                        dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());
                         //א.	סידור ועד עובדים 99008 – הסידור מוגדר כסידור מזכה, אולם יש לפתוח רכיב לסידור זה רק אם מתקיימים התנאים הבאים :
                        //	אם קיים סידור זה לפחות ב- 6 ימים בחודש המחושב.
                        //	Y = סכום הרכיב דקות נוכחות לתשלום (רכיב 1) לכל הסידורים 99008 (ועד עובדים) ביום אליו הם שייכים. 
                        //אם ביום של הסידור הזה לא קיימים סידורים אחרים אזי יש לפתוח רשומה רכיב לסידור רק אם Y >=210 דקות
                        //אחרת: יש לפתוח רשומה לרכיב לסידור זה רק אם Y >=300 דקות

                        if (iMisparSidur == 99008)
                        {
                            if (clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and MISPAR_SIDUR=99008").Length >= 6)
                            {
                                fSumDakotRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                                if (clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and MISPAR_SIDUR<>99008 and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')").Length == 0)
                                {
                                    if (fSumDakotRechiv >= 210)
                                    { addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fSumDakotRechiv); }
                                }
                                else
                                {
                                    if (fSumDakotRechiv >= 300)
                                    { addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fSumDakotRechiv); }
                                }
                            }
                        }
                        else
                        {

                            dr = _dsChishuv.Tables["CHISHUV_SIDUR"].Select( "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                            if (dr.Length > 0)
                                fErech = float.Parse(dr[0]["ERECH_RECHIV"].ToString());
                            else fErech = 0;
                           // fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());

                            addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fErech);
                        }
                    }
                }
                if (sSugeySidur.Length > 0)
                {
                    drSidurim = GetSidurimRegilim();
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                                    
                       //SetSugSidur(ref drSidurim[I],dTaarich, iMisparSidur);

                        iSugSidur = int.Parse(drSidurim[I]["sug_sidur"].ToString());
                        if (sSugeySidur.IndexOf("," + iSugSidur.ToString() + ",") > -1)
                                {
                                   
                                    dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                                    dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());
                                    //fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());
                                    dr = _dsChishuv.Tables["CHISHUV_SIDUR"].Select("MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                                    if (dr.Length > 0)
                                        fErech = float.Parse(dr[0]["ERECH_RECHIV"].ToString());
                                    else fErech = 0;
                                    addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fErech);
                                }
                            }
                       
              
                } 
                 
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, iKodRechiv, _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv23()
        {
            //דקות סיכון (רכיב 23) 
           
            int iMisparSidur;
            float fErech;
            DateTime  dShatHatchalaSidur;
            DataRow[] _drSidurim;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                oPeilut.dTaarich=dTaarich;

                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                    oPeilut.CalcRechiv23(iMisparSidur, dShatHatchalaSidur);


                    fErech = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_PEILUT"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotSikun.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                   if (fErech>0)
                   {
                    addRowToTable(clGeneral.enRechivim.DakotSikun.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                   }
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotSikun.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }


        public void CalcRechiv26()
        {
            //דקות פרמיה שבת  (רכיב 26) 

            int iMisparSidur, iSugSidur;
            bool bYeshSidur;
            int  iDay, iSugYom;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            DataRow[] _drSidurMeyuchad, _drSidurRagil;
            float fErechRechiv, fDakotNochehutSidur;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                iSugYom = clCalcData.iSugYom;

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

                            //SetSugSidur(ref _drSidurRagil[I],dTaarich, iMisparSidur);

                            iSugSidur = int.Parse(_drSidurRagil[I]["sug_sidur"].ToString());
                            bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nahagut.GetHashCode(), dTaarich, iSugSidur);
                            if (bYeshSidur)
                            {

                                iDay = int.Parse(_drSidurRagil[I]["day_taarich"].ToString());
                                dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                                dShatGmarLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_gmar_letashlum"].ToString());
                                fDakotNochehutSidur = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                                fErechRechiv = 0;
                                //if (clCalcData.CheckErevChag(iSugYom) || clCalcData.CheckYomShishi(iSugYom))
                                //{
                                //    if (dShatHatchalaLetashlum >= _oGeneralData.objParameters.dKnisatShabat)
                                //    {
                                //        fErechRechiv = fDakotNochehutSidur;
                                //    }
                                //}
                                //else if (clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, iSugYom, dTaarich) && dShatHatchalaLetashlum <= _oGeneralData.objParameters.dEndShabat)
                                if (clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, iSugYom, dTaarich))
                                {
                                    fErechRechiv = fDakotNochehutSidur;
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
                        fDakotNochehutSidur = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                        //if (clCalcData.CheckErevChag(iSugYom) || clCalcData.CheckYomShishi(iSugYom))
                        //{
                        //    if (dShatHatchalaLetashlum >=_oGeneralData.objParameters.dKnisatShabat)
                        //    {
                        //        fErechRechiv = fDakotNochehutSidur;
                        //    }
                        //}
                        //else if (clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, iSugYom, dTaarich) && dShatHatchalaLetashlum <= _oGeneralData.objParameters.dEndShabat)
                        if (clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, iSugYom, dTaarich))
                        {
                            fErechRechiv = fDakotNochehutSidur;
                        }

                        addRowToTable(clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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
                oPeilut.dTaarich = dTaarich;

                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                    iErechElementimReka = oPeilut.CalcElementReka(iMisparSidur, dShatHatchalaSidur);
                    
                   fErech216 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and KOD_RECHIV=" + clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                   if (fErech216 > 0)
                    {
                        addRowToTable(clGeneral.enRechivim.DakotPremiaVisa.GetHashCode(), dShatHatchalaSidur, iMisparSidur, float.Parse(iErechElementimReka.ToString()));
                    }
                }
               
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotPremiaVisa.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }


        public void CalcRechiv29()
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
                oPeilut.dTaarich = dTaarich;

                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                    if (dShatHatchalaSidur >= _oGeneralData.objParameters.dKnisatShabat || clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, clCalcData.iSugYom, dTaarich))
                    {
                        iErechElementimReka = oPeilut.CalcElementReka(iMisparSidur, dShatHatchalaSidur);

                        fErech216 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and KOD_RECHIV=" + clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                        if (fErech216 > 0)
                        {
                            addRowToTable(clGeneral.enRechivim.DakotPremiaVisaShabat.GetHashCode(), dShatHatchalaSidur, iMisparSidur, float.Parse(iErechElementimReka.ToString()));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotPremiaVisaShabat.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv30(out string sMispareySidur)
        {
            //דקות פרמיה יומית  (רכיב 30) 

            int iMisparSidur, iSugSidur;
            bool bYeshSidur;
            int  iDay, iSugYom;
             DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            DataRow[] _drSidurMeyuchad, _drSidurRagil;
            float fErechRechiv, fDakotNochehutSidur;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                iSugYom = clCalcData.iSugYom;
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

                        //SetSugSidur(ref _drSidurRagil[I],dTaarich, iMisparSidur);
                       
                            iSugSidur = int.Parse(_drSidurRagil[I]["sug_sidur"].ToString());
                            bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nahagut.GetHashCode(), dTaarich, iSugSidur);
                            if (bYeshSidur)
                            {
                                iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());
                                dShatHatchalaSidur = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_sidur"].ToString());

                                iDay = int.Parse(_drSidurRagil[I]["day_taarich"].ToString());
                                dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                                dShatGmarLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_gmar_letashlum"].ToString());
                                fDakotNochehutSidur = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                                fErechRechiv = 0;
                                sMispareySidur += "," + iMisparSidur.ToString();
                                //if (clCalcData.CheckErevChag(iSugYom) || clCalcData.CheckYomShishi(iSugYom))
                                //    {
                                //        if (dShatHatchalaLetashlum < _oGeneralData.objParameters.dKnisatShabat)
                                //        {
                                //            fErechRechiv = fDakotNochehutSidur;
                                //        }
                                //    }
                                //    else
                                //    {
                                fErechRechiv = fDakotNochehutSidur;
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
                            fDakotNochehutSidur = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                            //if (clCalcData.CheckErevChag(iSugYom) || clCalcData.CheckYomShishi(iSugYom))
                            //{
                            //    if (dShatHatchalaLetashlum < _oGeneralData.objParameters.dKnisatShabat)
                            //    {
                            //        fErechRechiv = fDakotNochehutSidur; 
                            //    }
                            //}
                            //else
                            //{
                            fErechRechiv = fDakotNochehutSidur;
                            //}
                            sMispareySidur += "," + iMisparSidur.ToString();

                            addRowToTable(clGeneral.enRechivim.DakotPremiaYomit.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotPremiaYomit.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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
            int iDay, iSugYom, iMichutzLamichsa;
            float fTosefetGrirotHatchala, fTosefetGrirotSof, fErechRechiv, fZmanHafsaka;
            DataRow[] _drSidurMeyuchad, _drSidurRagil;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
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
                        iMichutzLamichsa = int.Parse(_drSidurRagil[I]["out_michsa"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_sidur"].ToString());

                        //SetSugSidur(ref _drSidurRagil[I], dTaarich, iMisparSidur);

                        iSugSidur = int.Parse(_drSidurRagil[I]["sug_sidur"].ToString());
                        bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nahagut.GetHashCode(), dTaarich, iSugSidur);
                        if (bYeshSidur)
                        {
                            iDay = int.Parse(_drSidurRagil[I]["day_taarich"].ToString());
                            iSugYom = clCalcData.iSugYom;
                            dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                            fTosefetGrirotHatchala = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                            fTosefetGrirotSof = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                            dShatHatchalaLetashlum = dShatHatchalaLetashlum.AddMinutes(-fTosefetGrirotSof);
                            dShatGmarLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_gmar_letashlum"].ToString());
                            dShatGmarLetashlum = dShatGmarLetashlum.AddMinutes(+fTosefetGrirotHatchala);
                            fZmanHafsaka = float.Parse(_drSidurRagil[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());

                            CheckSidurShabatToAdd(clGeneral.enRechivim.DakotNehigaShabat.GetHashCode(), iMisparSidur, iDay, iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false, fZmanHafsaka);

                            
                            if (iSugSidur == 69)
                            {
                                fErechRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaShabat.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                           
                                if (fErechRechiv > _oGeneralData.objParameters.iMinZmanGriraDarom)
                                {
                                    if (int.Parse(iMisparSidur.ToString().Substring(0, 2)) > 11)
                                    {
                                        fErechRechiv = _oGeneralData.objParameters.iMinZmanGriraDarom;
                                    }
                                    else
                                    {
                                        fErechRechiv = _oGeneralData.objParameters.iMinZmanGriraTzafon;
                                    }
                                }
                                else if (fErechRechiv > _oGeneralData.objParameters.iMinZmanGriraTzafon && fErechRechiv <= _oGeneralData.objParameters.iMinZmanGriraDarom)
                                {
                                    if (int.Parse(iMisparSidur.ToString().Substring(0, 2)) <= 11)
                                    {
                                        fErechRechiv = _oGeneralData.objParameters.iMinZmanGriraTzafon;
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
                    iMisparSidur = int.Parse(_drSidurMeyuchad[I]["mispar_sidur"].ToString());
                    iMichutzLamichsa = int.Parse(_drSidurMeyuchad[I]["out_michsa"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_sidur"].ToString());
   
                    iDay = int.Parse(_drSidurMeyuchad[I]["day_taarich"].ToString());
                    iSugYom = clCalcData.iSugYom;
                    dShatHatchalaLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_letashlum"].ToString());
                    fTosefetGrirotHatchala = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                    fTosefetGrirotSof = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                    dShatHatchalaLetashlum = dShatHatchalaLetashlum.AddMinutes(-fTosefetGrirotSof);
                    dShatGmarLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_gmar_letashlum"].ToString());
                    dShatGmarLetashlum = dShatGmarLetashlum.AddMinutes(+fTosefetGrirotHatchala);
                    fZmanHafsaka = float.Parse(_drSidurMeyuchad[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());

                    CheckSidurShabatToAdd(clGeneral.enRechivim.DakotNehigaShabat.GetHashCode(), iMisparSidur, iDay, iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false, fZmanHafsaka);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotNehigaShabat.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv36()
        {
            //דקות בניהול תנועה בימי שבת ( רכיב 3):
            // יש לבצע את החישוב עבור סידורים המוגדרים כסידורי נהגות – זיהוי סידורי נהיגה:
            //  ישנו מאפיין (קוד מאפיין = 3) לסוג סידור/סידור מיוחד המתאר את סוג העבודה   .
            //ערך מאפיין זה (סוג עבודה) = 4 =  ניהול תנועה.
            int iMisparSidur, iSugSidur;
            bool bYeshSidur;
            float fZmanHafsaka;
            int  iDay, iSugYom, iMichutzLamichsa;
            DataRow[] _drSidurMeyuchad, _drSidurRagil;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                //בדיקת סידורים רגילים
                // נלקחים הסידורים הרגילים שסקטור העבודה שלהם =ניהול תנועה
                _drSidurRagil = GetSidurimRegilim();
                if (_drSidurRagil.Length > 0)
                {
                    for (int I = 0; I < _drSidurRagil.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());
                        iMichutzLamichsa = int.Parse(_drSidurRagil[I]["out_michsa"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_sidur"].ToString());

                        iMichutzLamichsa = int.Parse(clCalcData.CheckOutMichsa(_iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, iMichutzLamichsa).GetHashCode().ToString());                
                      
                   //SetSugSidur(ref _drSidurRagil[I],dTaarich, iMisparSidur);

                        iSugSidur = int.Parse(_drSidurRagil[I]["sug_sidur"].ToString());
                                bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nihul.GetHashCode(), dTaarich, iSugSidur);
                                if (bYeshSidur)
                                {
                                     iDay = int.Parse(_drSidurRagil[I]["day_taarich"].ToString());
                                     iSugYom = clCalcData.iSugYom;
                                    dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                                    dShatGmarLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_gmar_letashlum"].ToString());
                                    fZmanHafsaka = float.Parse(_drSidurRagil[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());
                                    CheckSidurShabatToAdd(clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode(), iMisparSidur, iDay, iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false, fZmanHafsaka);

                                }
                            }
                            
                     
                }

                //בדיקת סידורים מיוחדים
                // נלקחים רק סידורים מיוחדים מסקטור עבודה =  ניהול
                _drSidurMeyuchad = GetSidurimMeyuchadim("sector_avoda", clGeneral.enSectorAvoda.Nihul.GetHashCode());

                for (int I = 0; I < _drSidurMeyuchad.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurMeyuchad[I]["mispar_sidur"].ToString());
                    iMichutzLamichsa = int.Parse(_drSidurMeyuchad[I]["out_michsa"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_sidur"].ToString());
                    iMichutzLamichsa = int.Parse(clCalcData.CheckOutMichsa(_iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, iMichutzLamichsa).GetHashCode().ToString());                
                      
                        iDay = int.Parse(_drSidurMeyuchad[I]["day_taarich"].ToString());
                        iSugYom = clCalcData.iSugYom;
                        dShatHatchalaLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_letashlum"].ToString());
                        dShatGmarLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_gmar_letashlum"].ToString());
                        fZmanHafsaka = float.Parse(_drSidurMeyuchad[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());
                        CheckSidurShabatToAdd(clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode(), iMisparSidur, iDay, iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false, fZmanHafsaka);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv37()
        {
            //דקות בתפקיד בשבתון ( רכיב 4):
            // יש לבצע את החישוב עבור סידורים המוגדרים כסידורי נהגות – זיהוי סידורי נהיגה:
            //  ישנו מאפיין (קוד מאפיין = 3) לסוג סידור/סידור מיוחד המתאר את סוג העבודה   .
            //ערך מאפיין זה (סוג עבודה) = 1 =  תפקיד.
            int iMisparSidur, iSugSidur;
            bool bYeshSidur;
            int  iDay, iSugYom, iMichutzLamichsa;
             DataRow[] _drSidurMeyuchad, _drSidurRagil;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                //בדיקת סידורים רגילים
                // נלקחים הסידורים הרגילים שסקטור העבודה שלהם =תפקיד
                _drSidurRagil = GetSidurimRegilim();
                if (_drSidurRagil.Length > 0)
                {
                    for (int I = 0; I < _drSidurRagil.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());
                        iMichutzLamichsa = int.Parse(_drSidurRagil[I]["out_michsa"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_sidur"].ToString());

                        iMichutzLamichsa = int.Parse(clCalcData.CheckOutMichsa(_iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, iMichutzLamichsa).GetHashCode().ToString());                
                      
                            //SetSugSidur(ref _drSidurRagil[I],dTaarich, iMisparSidur);

                        iSugSidur = int.Parse(_drSidurRagil[I]["sug_sidur"].ToString());
                                    bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Tafkid.GetHashCode(), dTaarich, iSugSidur);
                                    if (bYeshSidur)
                                    {
                                        iDay = int.Parse(_drSidurRagil[I]["day_taarich"].ToString());
                                        iSugYom = clCalcData.iSugYom;
                                        dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                                        dShatGmarLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_gmar_letashlum"].ToString());
                                        
                                        CheckSidurShabatToAdd(clGeneral.enRechivim.DakotTafkidShabat.GetHashCode(), iMisparSidur, iDay, iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa,false,0);

                                    }
                                }
                       
                }

                //בדיקת סידורים מיוחדים
                // נלקחים רק סידורים מיוחדים מסקטור עבודה =  תפקיד
                _drSidurMeyuchad = GetSidurimMeyuchadim("sector_avoda", clGeneral.enSectorAvoda.Tafkid.GetHashCode());

                for (int I = 0; I < _drSidurMeyuchad.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurMeyuchad[I]["mispar_sidur"].ToString());
                    iMichutzLamichsa = int.Parse(_drSidurMeyuchad[I]["out_michsa"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_sidur"].ToString());
                    iMichutzLamichsa = int.Parse(clCalcData.CheckOutMichsa(_iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, iMichutzLamichsa).GetHashCode().ToString());                
                      
                        iDay = int.Parse(_drSidurMeyuchad[I]["day_taarich"].ToString());
                        iSugYom = clCalcData.iSugYom;
                        dShatHatchalaLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_letashlum"].ToString());
                        dShatGmarLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_gmar_letashlum"].ToString());
                        
                        CheckSidurShabatToAdd(clGeneral.enRechivim.DakotTafkidShabat.GetHashCode(), iMisparSidur, iDay, iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa,true,0);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotTafkidShabat.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
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
                drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                for (int I = 0; I < drSidurim.Length; I++)
                {
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                       
                     iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                     dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());
                     dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                      
                    bSidurMezake = ZihuyMezakeEshel(ref drSidurim[I]);

                    if (dShatHatchalaLetashlum >= _oGeneralData.objParameters.dStartSidurEshelBoker && dShatHatchalaLetashlum <= _oGeneralData.objParameters.dEndSidurEshelBoker && bSidurMezake)
                    {
                          fErech = 1;

                          addRowToTable(clGeneral.enRechivim.SachEshelBoker.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.SachEshelBoker.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        private bool ZihuyMezakeEshel(ref  DataRow drSidur)
        {
            bool bSidurMezake = false;
            DataRow[]  drPeiluyot;
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
           if (iMisparSidur.ToString().Substring(0, 2) == "99" && sSidurVisa!="")
           {
               fKmPremia = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.SachKM.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')")); ;
               if (fKmPremia >= _oGeneralData.objParameters.iKmMinTosafetEshel)
               {
                   return true;
               }
           }

           //ג.	קיים לפחות סידור מפה אחד (סידורים שמס' הסידור שלהם אינו מתחיל ב- "99") עבורו סוג סידור [שליפת נתונים מסידורים-מפה (מס' סידור,תאריך)]  = 02 או 07.
            if (iMisparSidur.ToString().Substring(0,2)!= "99" )
            {
                //SetSugSidur(ref drSidur, dShatHatchalaSidur, iMisparSidur);

                iSugSidur = int.Parse(drSidur["sug_sidur"].ToString());
                if (iSugSidur == 2 || iSugSidur == 7)
                 {
                     return true;
                 }
                    
            }

            //א.	קיימת לפחות פעילות אחת בסידור המזכה באש"ל. הבדיקה האם פעילות מזכה באשל: עבוור כל הפעילויות שבסידור שהמק"ט שלהן מתחיל ב- 0,1,2,3,4,8,9 יש לפנות לשירות [שליפת נתונים מקטלוג הנסיעות (מק"ט פעילות)] [פותח ע"י ורד לשגויים] ולשלוף את שדה "אש"ל" Eshel אם שדה זה = 1 או 3 אזי הפעילות מזכה באש"ל. 
            oPeilut.dTaarich = dTaarich;
            //dtPeiluyot = oPeilut.GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);
            //drPeiluyot = dtPeiluyot.Select("SUBSTRING(makat_nesia,1,1) in(0,1,2,3,4,8,9)");
            sQury = "MISPAR_SIDUR=" + iMisparSidur + " AND taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime') and ";
            sQury += "SHAT_HATCHALA_SIDUR=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and (SUBSTRING(makat_nesia,1,1) in(0,1,2,3,4,8,9))";
            drPeiluyot = clCalcData.DtPeiluyotOved.Select(sQury, "shat_yetzia asc");
                           
             if (drPeiluyot.Length > 0)
                {
                    for (int J = 0; J < drPeiluyot.Length; J++)
                    {
                        drDetailsPeilut = oPeilut.GetDetailsFromCatalaog(dTaarich, long.Parse(drPeiluyot[J]["MAKAT_NESIA"].ToString()));
                      
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
             sQury = "MISPAR_SIDUR=" + iMisparSidur + " AND taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime') and ";
             sQury += "SHAT_HATCHALA_SIDUR=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and (SUBSTRING(makat_nesia,1,3) in(791,792,744,785))";
             drPeiluyot = clCalcData.DtPeiluyotOved.Select(sQury, "shat_yetzia asc");
            if (drPeiluyot.Length > 0)
            {
                for (int J = 0; J < drPeiluyot.Length ; J++)
                {
                    if (_oGeneralData.CalcKm(long.Parse(drPeiluyot[0]["MAKAT_NESIA"].ToString())) >= _oGeneralData.objParameters.iKmMinTosafetEshel)
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
                drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                for (int I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                        
                    dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());

                    bSidurMezake = ZihuyMezakeEshel(ref drSidurim[I]);

                    if (bSidurMezake &&  dShatGmarLetashlum >= _oGeneralData.objParameters.dStartSidurEshelErev)
                    {
                        dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                        fErech = 1;

                        addRowToTable(clGeneral.enRechivim.SachEshelErev.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                              
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.SachEshelErev.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
          }

        public void CalcRechiv42()
        {
            //סה"כ לאשל צהרים (רכיב 42
            DataRow[] drSidurim;
            DateTime dShatHatchalaLetashlum,dShatGmarSidur, dShatHatchalaSidur, dShatGmarLetashlum;
            float fErech;
            int iMisparSidur;
            bool bSidurMezake;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                for (int I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                    dShatGmarSidur = DateTime.Parse(drSidurim[I]["shat_gmar_sidur"].ToString());
                       
                    dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());
                    dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());

                    bSidurMezake = ZihuyMezakeEshel(ref drSidurim[I]);

                    if (bSidurMezake && ((dShatHatchalaLetashlum <= _oGeneralData.objParameters.dStartSidurEshelTzaharayim && dShatGmarLetashlum >= _oGeneralData.objParameters.dEndSidurEshelTzaharayim) ||
                        (dShatGmarLetashlum >= _oGeneralData.objParameters.dStartSidurEshelTzaharayim && dShatGmarLetashlum <= _oGeneralData.objParameters.dEndSidurEshelTzaharayim) ||
                         (dShatHatchalaLetashlum >= _oGeneralData.objParameters.dStartSidurEshelTzaharayim && dShatHatchalaLetashlum <= _oGeneralData.objParameters.dEndSidurEshelTzaharayim)))
                    {
                         fErech = 1;

                         addRowToTable(clGeneral.enRechivim.SachEshelTzaharayim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.SachEshelTzaharayim.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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
                drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                for (int I = 0; I < drSidurim.Length; I++)
                {
                    if (!string.IsNullOrEmpty(drSidurim[I]["KOD_SIBA_LEDIVUCH_YADANI_IN"].ToString()))
                    {
                    if (int.Parse(drSidurim[I]["KOD_SIBA_LEDIVUCH_YADANI_IN"].ToString()) == 10 || int.Parse(drSidurim[I]["KOD_SIBA_LEDIVUCH_YADANI_OUT"].ToString()) == 10)
                    {
                        iMisparSidur = int.Parse(drSidurim[I]["MISPAR_SIDUR"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                        fDakotNochehutSidur = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                        dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                        dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());
                        
                        //ב.	אם דקות נוכחות לתשלום (רכיב 1) של סידור > 0 וגם דקות נוכחות לתשלום (רכיב 1) של סידור < 240 דקות [שליפת פרמטר (קוד פרמטר = 65)] וגם שעת התחלה לתשלום של סידור < 04:01 [שליפת פרמטר (קוד פרמטר = 70)] וגם שעת גמר לתשלום של סידור > 04:59 [שליפת פרמטר (קוד פרמטר = 71)] אזי  רכיב אש"ל בוקר מבקרים (רכיב 39) עם ערך = 1. אין לפתוח רשומה לשאר הרכיבים (זכאי רק לאש"ל בוקר). 
                        if ((fDakotNochehutSidur > 0) && (fDakotNochehutSidur < _oGeneralData.objParameters.iNochehutMinRetzufa) &&
                            (dShatHatchalaLetashlum < _oGeneralData.objParameters.dHatchalaEshelBoker) &&
                             (dShatGmarLetashlum > _oGeneralData.objParameters.dGmarEshelBoker))
                        {
                            addRowToTable(clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                        }

                        //ג.	אם דקות נוכחות לתשלום (רכיב 1) של סידור >= 240 דקות [שליפת פרמטר (קוד פרמטר = 65)] וגם דקות נוכחות לתשלום (רכיב 1) של סידור <= 479 דקות [שליפת פרמטר (קוד פרמטר = 66)] 
                        if ((fDakotNochehutSidur >= _oGeneralData.objParameters.iNochehutMinRetzufa) &&
                            (fDakotNochehutSidur <= _oGeneralData.objParameters.iNochehutMaxEshel1))
                        {
                            if ((dShatHatchalaLetashlum <= _oGeneralData.objParameters.dZmanHatchalaBoker) &&
                            (dShatGmarLetashlum < _oGeneralData.objParameters.dZmanSiyumBoker))
                            {
                                addRowToTable(clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                            }

                            if ((dShatHatchalaLetashlum <= _oGeneralData.objParameters.dZmanHatchalaBoker) &&
                           (dShatGmarLetashlum > _oGeneralData.objParameters.dZmanSiyumBoker))
                            {
                                addRowToTable(clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                addRowToTable(clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                             }

                            if ((dShatHatchalaLetashlum > _oGeneralData.objParameters.dZmanHatchalaBoker) &&
                          (dShatHatchalaLetashlum <= _oGeneralData.objParameters.dZmanSiyumTzharayim) && 
                          (dShatGmarLetashlum > _oGeneralData.objParameters.dZmanSiyumBoker) && 
                          (dShatGmarLetashlum < _oGeneralData.objParameters.dZmanHatchalaErev))
                            {
                                addRowToTable(clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                            }

                            if ((dShatHatchalaLetashlum <= _oGeneralData.objParameters.dZmanSiyumTzharayim) &&
                         (dShatGmarLetashlum > _oGeneralData.objParameters.dZmanHatchalaErev))
                            {
                                addRowToTable(clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                addRowToTable(clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                            }

                            if ((dShatHatchalaLetashlum > _oGeneralData.objParameters.dZmanSiyumTzharayim) &&
                        (dShatGmarLetashlum > _oGeneralData.objParameters.dZmanHatchalaErev))
                            {
                                addRowToTable(clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                            }
                        }

                        //ד.	אם דקות נוכחות לתשלום (רכיב 1) של סידור > 479 דקות [שליפת פרמטר (קוד פרמטר = 66)]   וגם דקות נוכחות לתשלום (רכיב 1) של סידור <= 599 דקות [שליפת פרמטר (קוד פרמטר = 67)]  
                        if ((fDakotNochehutSidur >= _oGeneralData.objParameters.iNochehutMaxEshel1) &&
                           (fDakotNochehutSidur <= _oGeneralData.objParameters.iNochehutMaxEshel))
                        {
                            if ((dShatHatchalaLetashlum >= _oGeneralData.objParameters.dZmanHatchalaBoker2) &&
                            (dShatGmarLetashlum < _oGeneralData.objParameters.dZmanSiyumBoker))
                            {
                                addRowToTable(clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                            }

                            if ((dShatHatchalaLetashlum <= _oGeneralData.objParameters.dZmanSiyumBoker) &&
                           (dShatGmarLetashlum > _oGeneralData.objParameters.dZmanSiyumBoker) && (dShatGmarLetashlum< _oGeneralData.objParameters.dZmanHatchalaErev))
                            {
                                addRowToTable(clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                addRowToTable(clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                            }

                            if ((dShatHatchalaLetashlum > _oGeneralData.objParameters.dZmanSiyumBoker) &&
                         (dShatHatchalaLetashlum <= _oGeneralData.objParameters.dZmanSiyumTzharayim) &&
                         (dShatGmarLetashlum > _oGeneralData.objParameters.dZmanHatchalaErev))
                            {
                                addRowToTable(clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                addRowToTable(clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);

                            }

                         
                            if ((dShatHatchalaLetashlum > _oGeneralData.objParameters.dZmanSiyumTzharayim) &&
                        (dShatGmarLetashlum > _oGeneralData.objParameters.dZmanHatchalaErev))
                            {
                                addRowToTable(clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                            }

                            if ((dShatHatchalaLetashlum <= _oGeneralData.objParameters.dZmanSiyumBoker) &&
                        (dShatGmarLetashlum > _oGeneralData.objParameters.dZmanHatchalaErev))
                            {
                                addRowToTable(clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                addRowToTable(clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                addRowToTable(clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                            }
                        }

                        //ה.	אם דקות נוכחות לתשלום (רכיב 1) של סידור >= 600 דקות  (10 שעות) [שליפת פרמטר (קוד פרמטר = 68)] 
                        if (fDakotNochehutSidur >= _oGeneralData.objParameters.iNochehueMinEshel)
                        {
                            if ((dShatHatchalaLetashlum >= _oGeneralData.objParameters.dZmanHatchalaBoker2) &&
                            (dShatGmarLetashlum < _oGeneralData.objParameters.dZmanSiyumBoker))
                            {
                                addRowToTable(clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                addRowToTable(clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                            }

                            if ((dShatHatchalaLetashlum <= _oGeneralData.objParameters.dZmanSiyumBoker) &&
                           (dShatGmarLetashlum > _oGeneralData.objParameters.dZmanSiyumBoker)&&
                           (dShatGmarLetashlum < _oGeneralData.objParameters.dZmanSiyumTzharayim))
                            {
                                addRowToTable(clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                addRowToTable(clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                            }

                            if ((dShatHatchalaLetashlum > _oGeneralData.objParameters.dZmanSiyumBoker) &&
                                (dShatHatchalaLetashlum < _oGeneralData.objParameters.dZmanSiyumTzharayim) &&
                          (dShatHatchalaLetashlum > _oGeneralData.objParameters.dZmanHatchalaErev))
                            {
                                addRowToTable(clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                addRowToTable(clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                            }

                            if ((dShatHatchalaLetashlum <= _oGeneralData.objParameters.dZmanSiyumBoker) &&
                         (dShatGmarLetashlum > _oGeneralData.objParameters.dZmanSiyumTzharayim))
                            {
                                addRowToTable(clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                addRowToTable(clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                                addRowToTable(clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, 1);
                            }

                            if (dShatHatchalaLetashlum > _oGeneralData.objParameters.dZmanSiyumTzharayim) 
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
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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
                   
                if (_oGeneralData.objMeafyeneyOved.iMeafyen60 != 2)
                {
                    sSidurimMeyuchadim = GetSidurimMeyuchRechiv(iKodRechiv);

                    if (sSidurimMeyuchadim.Length > 0)
                    {
                        drSidurim =  clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ") and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                        for (int I = 0; I < drSidurim.Length; I++)
                        {

                            iMisparSidur = int.Parse(drSidurim[I]["MISPAR_SIDUR"].ToString());
                            dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                            fDakotNochehutSidur = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                            
                            addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fDakotNochehutSidur);

                        }
                    }

                    sSugeySidur = GetSugeySidurRechiv(iKodRechiv);
                    if (sSugeySidur.Length > 0)
                    {
                        drSidurim = GetSidurimRegilim();
                        for (int I = 0; I < drSidurim.Length; I++)
                        {
                            iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                            dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                            //SetSugSidur(ref drSidurim[I], dTaarich, iMisparSidur);

                            iSugSidur = int.Parse(drSidurim[I]["sug_sidur"].ToString());

                            if (sSugeySidur.IndexOf("," + iSugSidur.ToString() + ",") > -1)
                            {
                                fDakotNochehutSidur = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                                
                                addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fDakotNochehutSidur);

                            }
                        }
                          

                    } 
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, iKodRechiv, _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        private void CalcDakotMachlif(int iKodRechiv)
        { 
           string sSidurimMeyuchadim;
            DataRow[] drSidurim;
            int iMisparSidur;
            float fErech;
            DateTime dShatHatchalaSidur; //dShatHatchalaLetashlum, dShatGmarLetashlum,
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                sSidurimMeyuchadim = GetSidurimMeyuchRechiv(iKodRechiv);
                if (sSidurimMeyuchadim.Length > 0)
                {
                    drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ") and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                       iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                       dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                       
                       // dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                        //dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());
                        fErech = float.Parse(drSidurim[I]["ZMAN_LELO_HAFSAKA"].ToString()); //float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());

                        addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fErech);

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, iKodRechiv, _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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
                oPeilut.dTaarich = dTaarich;

                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                    fDakotElementim = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_PEILUT"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotElementim.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                    
                    fErech = fDakotElementim;

                    addRowToTable(clGeneral.enRechivim.SachDakotLepremia.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.SachDakotLepremia.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

       
        public void CalcRechiv53()
        {
            DataRow[] drSidurim;
            int iMisparSidur, iSugSidur,iMichutzLamichsa, iDay, iSugYom;
             DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            bool bYeshSidur;
            float fZmanHafsaka;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {

                drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and hamarat_shabat=1 and zakay_lehamara=1 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                        iMichutzLamichsa = int.Parse(drSidurim[I]["out_michsa"].ToString());

                        iDay = int.Parse(drSidurim[I]["day_taarich"].ToString());
                        iSugYom = clCalcData.iSugYom;
                        dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                        dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());
                        fZmanHafsaka = float.Parse(drSidurim[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());
                        CheckSidurShabatToAdd(clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode(), iMisparSidur, iDay, iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false, fZmanHafsaka);
                  

                    }

                    drSidurim = GetSidurimRegilim();
                    if (drSidurim.Length > 0)
                    {
                        for (int I = 0; I < drSidurim.Length; I++)
                        {
                            iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                            iMichutzLamichsa = int.Parse(drSidurim[I]["out_michsa"].ToString());
                            dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                            //SetSugSidur(ref drSidurim[I], dTaarich, iMisparSidur);

                            iSugSidur = int.Parse(drSidurim[I]["sug_sidur"].ToString());

                                    bYeshSidur = CheckSugSidur(clGeneral.enMeafyenim.HamaratShaot.GetHashCode(), 1, dTaarich, iSugSidur);
                                    if (bYeshSidur)
                                    {
                                        iDay = int.Parse(drSidurim[I]["day_taarich"].ToString());
                                        iSugYom = clCalcData.iSugYom;
                                        dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                                        dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());
                                        fZmanHafsaka = float.Parse(drSidurim[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());
                                        CheckSidurShabatToAdd(clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode(), iMisparSidur, iDay, iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false, fZmanHafsaka);

                                    }
                              
                        }
                    }
               
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enMeafyenim.HamaratShaot.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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
            try
            {

                drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                
                if (drSidurim.Length > 0)
                {
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        if (drSidurim[I]["ein_leshalem_tos_lila"].ToString()=="")
                        {
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                        fHalbashaTchilatYom = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.HalbashaTchilatYom.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                        fHalbashaSofYom = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.HalbashaSofYom.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                        dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString()).AddMinutes( - fHalbashaTchilatYom);
                        dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString()).AddMinutes(fHalbashaSofYom);
                  
                         fErech = 0;
                        if (dShatHatchalaLetashlum >= _oGeneralData.objParameters.dTchilatTosefetLaila && dShatGmarLetashlum <= _oGeneralData.objParameters.dSiyumTosefetLaila)
                        {
                            fErech = float.Parse((dShatGmarLetashlum-dShatHatchalaLetashlum).TotalMinutes.ToString());
                        }
                        else if (dShatHatchalaLetashlum <= _oGeneralData.objParameters.dTchilatTosefetLaila && dShatGmarLetashlum <= _oGeneralData.objParameters.dSiyumTosefetLaila)
                        {
                            fErech =float.Parse( (dShatGmarLetashlum - _oGeneralData.objParameters.dTchilatTosefetLaila).TotalMinutes.ToString());
                        }
                        else if (dShatHatchalaLetashlum >= _oGeneralData.objParameters.dTchilatTosefetLaila && dShatGmarLetashlum >= _oGeneralData.objParameters.dSiyumTosefetLaila)
                        {
                            fErech =float.Parse( (_oGeneralData.objParameters.dSiyumTosefetLaila - dShatHatchalaLetashlum).TotalMinutes.ToString());
                        }
                        else if (dShatHatchalaLetashlum <= _oGeneralData.objParameters.dTchilatTosefetLaila && dShatGmarLetashlum >= _oGeneralData.objParameters.dTchilatTosefetLaila)
                        {
                            fErech = float.Parse((_oGeneralData.objParameters.dSiyumTosefetLaila - _oGeneralData.objParameters.dTchilatTosefetLaila).TotalMinutes.ToString());
                        }
                      

                          addRowToTable(clGeneral.enRechivim.ZmanLailaEgged.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                        }
                           
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.ZmanLailaEgged.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv55()
        {
            DataRow[] drSidurim;
            int iMisparSidur,iIsuk;
            DateTime dShatHatchalaSidur, dShatHatchalaLetashlum, dShatGmarLetashlum, dZmanSiyuomTosLila, dZmatTchilatTosLila;
            float fErech, fZmanLilaSidureyBoker;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            float fHalbashaTchilatYom, fHalbashaSofYom;
            dZmatTchilatTosLila = DateTime.MaxValue;
            dZmanSiyuomTosLila = DateTime.MaxValue;
            try
            {

                drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                if (drSidurim.Length > 0)
                {
                    for (int I = 0; I < drSidurim.Length; I++)
                    {

                        if (drSidurim[I]["ein_leshalem_tos_lila"].ToString() == "")
                        {
                            iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                            dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                            fZmanLilaSidureyBoker = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_PEILUT"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.ZmanLilaSidureyBoker.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                            //	אין להתייחס בחישוב לסידורים עבורם קיים רכיב 271
                            if (fZmanLilaSidureyBoker == 0)
                            {
                                fHalbashaTchilatYom = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.HalbashaTchilatYom.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                                fHalbashaSofYom = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.HalbashaSofYom.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                               
                                dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString()).AddMinutes(-fHalbashaTchilatYom);
                                dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString()).AddMinutes(fHalbashaSofYom);
                                
                                fErech = 0;
                                if (!(_oGeneralData.objPirteyOved.iDirug == 85 && _oGeneralData.objPirteyOved.iDarga == 30))
                                {
                                    dZmatTchilatTosLila = _oGeneralData.objParameters.dTchilatTosefetLailaChok;
                                    dZmanSiyuomTosLila = _oGeneralData.objParameters.dSiyumTosefetLailaChok;
                                    
                                    //אם סידור הינו סידור ויזה צבאית - סידור מיוחד בעל שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 45 ) עם ערך = 1 זהו יום אחרון של הוויזה - TB_Sidurim_Ovedim.Yom_Visa= 3 אזי יש לבצע את בדיקת זמן הסידור מול שעות לילה חוק לפי שעת התחלה של סידור TB_Sidurim_Ovedim. Shat_hatchala  ולא שעת התחלה לתשלום של סידור.
                                    if (drSidurim[I]["sidur_namlak_visa"].ToString() == "1" && drSidurim[I]["yom_visa"].ToString() == "3")
                                    {
                                        dShatHatchalaLetashlum = dShatHatchalaSidur;
                                    }

                                    iIsuk = _oGeneralData.objPirteyOved.iIsuk;
                                    iIsuk = _oGeneralData.objPirteyOved.iIsuk;
                                    if ((iIsuk == 122 || iIsuk == 123 || iIsuk == 124 || iIsuk == 127) && iMisparSidur == 99001 && clDefinitions.GetSugMishmeret(_iMisparIshi, dTaarich, clCalcData.iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, _oGeneralData.objParameters) == clGeneral.enSugMishmeret.Liyla.GetHashCode())
                                    {
                                         dZmanSiyuomTosLila = _oGeneralData.objParameters.dSiyumMishmeretLilaMafilim;
                                    }
                                    else
                                    {
                                        dZmanSiyuomTosLila = _oGeneralData.objParameters.dSiyumTosefetLailaChok;
                                    }

                                }
                                else if (clCalcData.iSugYom < clGeneral.enSugYom.Shishi.GetHashCode())
                                {
                                    dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                                    dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());

                                    if (_oGeneralData.objPirteyOved.iMikumYechida == 141)
                                    {
                                        dZmatTchilatTosLila = DateTime.Parse(dTaarich.ToShortDateString() + " 21:00");
                                        dZmanSiyuomTosLila = _oGeneralData.objParameters.dSiyumTosLilaTaavura;
                                    }
                                    else
                                    {
                                        dZmatTchilatTosLila = _oGeneralData.objParameters.dTchilatTosLilaTaavura;
                                        dZmanSiyuomTosLila = _oGeneralData.objParameters.dSiyumTosLilaTaavura;
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



                                addRowToTable(clGeneral.enRechivim.ZmanLailaChok.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.ZmanLailaChok.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
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
                    drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ") and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
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
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.YomMiluim.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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
                    drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ") and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
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
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.YomAvodaBechul.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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
                    drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ") and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                        if (!clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, clCalcData.iSugYom, dTaarich))
                        {
                            fErech += 1;
                        }
                    }
                }
                return fErech;
            }

            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.YomChofesh.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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

                drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null  and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                for (int I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                    iOutMichsa = int.Parse(drSidurim[I]["out_michsa"].ToString());

                    if (clCalcData.CheckOutMichsa(_iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, iOutMichsa))
                    {
                        fErech = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidChol.GetHashCode().ToString() + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and mispar_sidur=" + iMisparSidur + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                        if (fErech > 0)
                        {

                            addRowToTable(clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotMichutzTafkidChol.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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

                drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                for (int I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                    fDakotMichutzTafkid = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaTafkidShabat.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                    fDakotMichutzNihul = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichutzLamichsaNihulShabat.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                    fErech = fDakotMichutzTafkid + fDakotMichutzNihul;
                    
                    addRowToTable(clGeneral.enRechivim.DakotMichutzLamichsaShabat.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotMichutzLamichsaShabat.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv85()
        {
            float fErech,fRetzifutNehiga , fRetzifutTafkid;
            DateTime dShatHatchalaSidur = DateTime.MinValue;
            int iMisparSidur = 0;
            DataRow[] drSidurim;
            try
            {

                drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                for (int I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                    fRetzifutNehiga = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                    fRetzifutTafkid = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                    fErech = fRetzifutNehiga + fRetzifutTafkid;
                    
                     addRowToTable(clGeneral.enRechivim.SachTosefetRetzifut.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                    
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.SachTosefetRetzifut.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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

                drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                for (int I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());

                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                    dShatGmarSidur = DateTime.Parse(drSidurim[I]["shat_gmar_sidur"].ToString());
                    dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                    dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());

                    fErech = float.Parse((dShatHatchalaLetashlum-dShatHatchalaSidur).TotalMinutes.ToString()) + float.Parse((dShatGmarSidur - dShatGmarLetashlum).TotalMinutes.ToString());
                   
                    addRowToTable(clGeneral.enRechivim.KizuzDakotHatchalaGmar.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.KizuzDakotHatchalaGmar.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv88()
        {
            DataRow[] drSidurim;
            int iMisparSidur, iHashlama;
            float fErech,fTempX;
            DateTime dShatHatchalaSidur, dShatHatchalaLetashlum, dShatGmarLetashlum;
            DateTime dStartHafsaketZaharim, dEndHafsaketZaharim;
             dShatHatchalaSidur = DateTime.MinValue;
             iMisparSidur = 0;
             string sSidurim;
            try
            {
                sSidurim = GetSidurimMeyuchRechiv(clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode());
                if (sSidurim.Length > 0)
                {
                    drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0  AND MISPAR_SIDUR IN(" + sSidurim + ") and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
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

                                if (_oGeneralData.objPirteyOved.iEzor == clGeneral.enEzor.Yerushalim.GetHashCode() || _oGeneralData.objPirteyOved.iMikumYechida == 180)
                                {
                                    dStartHafsaketZaharim =_oGeneralData.objParameters.dStartAruchatTzaharayim246;
                                    dEndHafsaketZaharim = _oGeneralData.objParameters.dEndAruchatTzaharayim247;
                                }
                                else
                                {
                                     dStartHafsaketZaharim = _oGeneralData.objParameters.dStartAruchatTzaharayim;
                                    dEndHafsaketZaharim = _oGeneralData.objParameters.dEndAruchatTzaharayim;
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

                                    if (fTempX > 18)
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
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.ZmanAruchatTzaraim.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv89()
        {
            DataRow[] drSidurim;
            int iMisparSidur ;
            float fErech, fNochehutLetashlum, fNochehutBefoal;
            DateTime dShatHatchalaSidur;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0  and mispar_sidur is not null and (sidur_namlak_visa=1 or sidur_namlak_visa=2) and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                for (int I = 0; I < drSidurim.Length; I++)
                {
                   
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                     
                    fNochehutLetashlum = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                    fNochehutBefoal = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                     fErech = fNochehutBefoal - fNochehutLetashlum;
                     addRowToTable(clGeneral.enRechivim.KizuzBevisa.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                 }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.KizuzBevisa.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv92(float fMichsaYomit,float fNochehutLetashlum)
        {
            DataRow[] drSidurim;
            DateTime dShatHatchalaSidur, dShatGmarLetaslum,dShatHatchalaLetashlum,dTemp;
            int iMisparSidur;
            float fErech;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {

                if (fMichsaYomit > 0 && fNochehutLetashlum >= fMichsaYomit)
                {
                    drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0  and MISPAR_SIDUR=99001 and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur ASC");
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                        dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["SHAT_HATCHALA_LETASHLUM"].ToString());

                        //if (dShatHatchalaLetashlum >= _oGeneralData.objMeafyeneyOved.ConvertMefyenShaotValid(dTaarich, _oGeneralData.objMeafyeneyOved.sMeafyen25))
                        //{
                        dShatGmarLetaslum = DateTime.Parse(drSidurim[I]["SHAT_GMAR_LETASHLUM"].ToString());
                        dTemp = _oGeneralData.objParameters.dTchlatTashlumTosefetLilaMafilim;
                        fErech = 0;
                        if (dShatHatchalaLetashlum < dTemp)
                        {
                            fErech = float.Parse((_oGeneralData.objParameters.dTchlatTashlumTosefetLilaMafilim - dShatHatchalaLetashlum).TotalMinutes.ToString());
                            //if (dShatHatchalaLetashlum < _oGeneralData.objParameters.dTchilatMishmeretLilaMafilim)
                            //    fErech = float.Parse((_oGeneralData.objParameters.dTchlatTashlumTosefetLilaMafilim - dShatHatchalaLetashlum).TotalMinutes.ToString());
                            //else
                            //    fErech = float.Parse((_oGeneralData.objParameters.dTchlatTashlumTosefetLilaMafilim - _oGeneralData.objParameters.dTchilatMishmeretLilaMafilim).TotalMinutes.ToString());
                        }
                        
                        addRowToTable(clGeneral.enRechivim.Shaot50.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                        //}                                
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.Shaot50.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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
        //        drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and Hashlama>0 and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
        //        for (int I = 0; I < drSidurim.Length; I++)
        //        {
        //            iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
        //            dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
        //            if (clCalcData.CheckUshraBakasha(clGeneral.enKodIshur.HashlamaLeshaot.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur))
        //            {
        //                fDakotNochehut = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

        //                fErech = Math.Max(0, (int.Parse(drSidurim[I]["Hashlama"].ToString()) * 60) - fDakotNochehut);
                        
        //                //אם X > 0 יש לבדוק האם יש סידור עוקב לסידור עם ההשלמה אם כן, 
        //                //ערך הרכיב = הנמוך מבין (X , שעת התחלה לתשלום של הסידור העוקב פחות שעות גמר לתשלום של הסידור הנבדק)
        //                //אם אין סידור עוקב, ערך הרכיב = X

        //                if (fErech > 0)
        //                {
        //                    drSidurimLeyom = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur asc");
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
        //        clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.ZmanHashlama.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
        //        throw (ex);
        //    }
        //}

        public float CalcRechiv96(ref float fSumDakotLaylaEgged, ref float fSumDakotLaylaChok, ref float fSumDakotLaylaBoker, ref float fSumDakotShabat)
        {
            DataRow[] drSidurim;
            int iMisparSidur, J ;
            DateTime dShatHatchalaSidur, dShatHatchalaLetashlum, dShatGmarLetashlum;
      
            float fErech, fErechSidur, fTempX, fSachDakotTafkid;
            float fErechLaylaEgged, fErechSidurLaylaEgged;
            float fErechLaylaChok, fErechSidurLaylaChok;
             float fErechShabat;
            float fErecBoker,fErechSidurBoker;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            bool bSidurNehigaFirst = false;
            bool bSidurNehigaSecond = false;
            bool bSidurNihulOrTafkidFirst = false;
            bool bSidurNihulOrTafkidSecond = false;
       
            try
            {
                fTempX = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode().ToString()));
               
                drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0  and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur ASC");
                fErech=0;
                fErechLaylaEgged = 0;
                fErechLaylaChok = 0;
                fErecBoker =0;
                fErechShabat = 0;

                for (int I = 0; I < drSidurim.Length;I++)
                {
                    bSidurNehigaFirst = false;
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                    bSidurNehigaFirst = isSidurNehiga(ref drSidurim[I]);
                    bSidurNihulOrTafkidFirst = isSidurNihulTnuaOrTafkidMezakeRezifut(ref drSidurim[I]);
                   
                    if (bSidurNehigaFirst || bSidurNihulOrTafkidFirst)
                    {
                        J = I + 1;
                        if (J < drSidurim.Length)
                        {
                             
                            bSidurNehigaSecond = isSidurNehiga(ref drSidurim[J]);
                            bSidurNihulOrTafkidSecond = isSidurNihulTnuaOrTafkidMezakeRezifut(ref drSidurim[J]);
                            
                            if ((bSidurNehigaFirst && (bSidurNehigaSecond || bSidurNihulOrTafkidSecond)) || (bSidurNehigaSecond && (bSidurNehigaFirst || bSidurNihulOrTafkidFirst)))
                            {
                                dShatHatchalaLetashlum = DateTime.Parse(drSidurim[J]["shat_hatchala_letashlum"].ToString());
                                dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());

                                fErechSidur = float.Parse((dShatHatchalaLetashlum - dShatGmarLetashlum).TotalMinutes.ToString());
                                fSachDakotTafkid = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotTafkid.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
               
                                if (fErechSidur >= 1 && fErechSidur <= _oGeneralData.objParameters.iMinTimeBetweenSidurim)
                                {
                                    if (fSachDakotTafkid >= _oGeneralData.objParameters.iMinYomAvodaForHalbasha &&
                                        (drSidurim[I]["Mezake_Halbasha"].ToString() == "2" || drSidurim[I]["Mezake_Halbasha"].ToString() == "3" || drSidurim[J]["Mezake_Halbasha"].ToString() == "1" || drSidurim[J]["Mezake_Halbasha"].ToString() == "3"))
                                    {
                                        if (fErechSidur >= 10)
                                            fErechSidur = fErechSidur - 10;
                                        else fErechSidur = 0;
                                    }

                                    if ((fTempX+fErech) < _oGeneralData.objParameters.iMaxRetzifutChodshitLetashlum)
                                    {
                                        if ((fErech + fErechSidur + fTempX) > _oGeneralData.objParameters.iMaxRetzifutChodshitImGlisha)
                                            fErech += _oGeneralData.objParameters.iMaxRetzifutChodshitLetashlum - (fTempX + fErech);
                                        else fErech += fErechSidur;
                                    }
                                    // fErech += fErechSidur;

                                    //רציפות לילה  
                                    if (fSachDakotTafkid >= _oGeneralData.objParameters.iMinYomAvodaForHalbasha)
                                    {
                                        if (drSidurim[I]["Mezake_Halbasha"].ToString() == "2" || drSidurim[I]["Mezake_Halbasha"].ToString() == "3")
                                            dShatGmarLetashlum = dShatGmarLetashlum.AddMinutes(10) < dShatHatchalaLetashlum ? dShatGmarLetashlum.AddMinutes(10) : dShatHatchalaLetashlum;
                                        if (drSidurim[J]["Mezake_Halbasha"].ToString() == "1" || drSidurim[J]["Mezake_Halbasha"].ToString() == "3")
                                            dShatHatchalaLetashlum = dShatHatchalaLetashlum.AddMinutes(-10) > dShatGmarLetashlum ? dShatHatchalaLetashlum.AddMinutes(-10) : dShatGmarLetashlum;
                                    }       
                                    fErechSidurLaylaEgged = getDakotRezifutLayla(dShatHatchalaLetashlum, dShatGmarLetashlum, _oGeneralData.objParameters.dTchilatTosefetLaila, _oGeneralData.objParameters.dSiyumTosefetLaila);//9,10
                                    fErechSidurLaylaChok = getDakotRezifutLayla(dShatHatchalaLetashlum, dShatGmarLetashlum, _oGeneralData.objParameters.dSiyumTosefetLaila, _oGeneralData.objParameters.dSiyumTosefetLailaChok);//10,12
                                    fErechSidurBoker = getDakotRezifutBoker(dShatHatchalaLetashlum, dShatGmarLetashlum);   
                                    
                                    fErechLaylaEgged += fErechSidurLaylaEgged;
                                    fErechLaylaChok += fErechSidurLaylaChok;
                                    fErecBoker += fErechSidurBoker;

                                    if (dShatGmarLetashlum >= _oGeneralData.objParameters.dKnisatShabat)
                                        fErechShabat += float.Parse((dShatHatchalaLetashlum - dShatGmarLetashlum).TotalMinutes.ToString());

                                    
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
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.ZmanRetzifutNehiga.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        private bool isSidurNehiga(ref DataRow drSidurim)
        {
            int iMisparSidur=0;
            bool bSidurNehiga = false;
            bool bYeshSidur = false;
            int iSugSidur;
            DateTime dShatHatchalaSidur=DateTime.MinValue;
            try{
                iMisparSidur = int.Parse(drSidurim["mispar_sidur"].ToString());
                dShatHatchalaSidur = DateTime.Parse(drSidurim["shat_hatchala_sidur"].ToString());

                if (iMisparSidur.ToString().Substring(0, 2) == "99" && drSidurim["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString())
                    bSidurNehiga = true;
                else if (iMisparSidur.ToString().Substring(0, 2) != "99")
                {
                    //SetSugSidur(ref drSidurim, dTaarich, iMisparSidur);
                    iSugSidur = int.Parse(drSidurim["sug_sidur"].ToString());

                    bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nahagut.GetHashCode(), dTaarich, iSugSidur);
                    if (bYeshSidur)
                        bSidurNehiga = true;
                }
                return bSidurNehiga;
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, 0, _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }
        private bool isSidurNihulTnuaOrTafkidMezakeRezifut(ref DataRow drSidurim)
        {
            int iMisparSidur = 0;
            bool bSidurNihulOrTafkid = false;
            bool bYeshSidur = false;
            int iSugSidur;
            string sQury = "";
            DataRow[]   drPeiluyot;
           // DataTable dtPeiluyot;
            DateTime dShatHatchalaSidur = DateTime.MinValue;
            int zakay = 0;
            try
            {
                iMisparSidur = int.Parse(drSidurim["mispar_sidur"].ToString());
                dShatHatchalaSidur = DateTime.Parse(drSidurim["shat_hatchala_sidur"].ToString());
                if (drSidurim["zakay_lechishuv_retzifut"].ToString() != "")
                    zakay = int.Parse(drSidurim["zakay_lechishuv_retzifut"].ToString());
                if (iMisparSidur.ToString().Substring(0, 2) == "99" && (drSidurim["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nihul.GetHashCode().ToString() || zakay == 1))
                    bSidurNihulOrTafkid = true;
                else if (iMisparSidur.ToString().Substring(0, 2) != "99")
                {
                    //SetSugSidur(ref drSidurim , dTaarich, iMisparSidur);
                    iSugSidur = int.Parse(drSidurim["sug_sidur"].ToString());

                    bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nihul.GetHashCode(), dTaarich, iSugSidur);
                    if (bYeshSidur)
                        bSidurNihulOrTafkid = true;
                    else
                    {
                        //dtPeiluyot = oPeilut.GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);
                        //drPeiluyot = dtPeiluyot.Select("sector_zvira_zman_haelement=4");
                        sQury = "MISPAR_SIDUR=" + iMisparSidur + " AND taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime') and ";
                        sQury += "SHAT_HATCHALA_SIDUR=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and (sector_zvira_zman_haelement=4)";
                        drPeiluyot = clCalcData.DtPeiluyotOved.Select(sQury, "shat_yetzia asc");

                        bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nihul.GetHashCode(), dTaarich, iSugSidur);
                        if (bYeshSidur || (iMisparSidur == 99301 && drPeiluyot.Length > 0)) // && clCalcGeneral.objMeafyeneyOved.iMeafyen33))
                            bSidurNihulOrTafkid = true;

                    }
                }
                return bSidurNihulOrTafkid;
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, 0, _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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
                drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0  and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur ASC");
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
                    else if (iMisparSidur.ToString().Substring(0, 2) == "99" && (drSidurim[I]["sector_avoda"].ToString()== clGeneral.enSectorAvoda.Nihul.GetHashCode().ToString() || int.Parse(drSidurim[I]["zakay_lepizul"].ToString()) == 1))
                        bSidurNihulOrTafkid = true;
                    else if (iMisparSidur.ToString().Substring(0, 2) != "99" )
                    {
                        //SetSugSidur(ref drSidurim[I], dTaarich, iMisparSidur);
                        iSugSidur = int.Parse(drSidurim[I]["sug_sidur"].ToString());

                        bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nahagut.GetHashCode(), dTaarich, iSugSidur);
                        if (bYeshSidur)
                            bSidurNehiga = true;
                        else
                        {
                            //dtPeiluyot = oPeilut.GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);
                            //drPeiluyot = dtPeiluyot.Select("sector_zvira_zman_haelement=4");
                            sQury = "MISPAR_SIDUR=" + iMisparSidur + " AND taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime') and ";
                            sQury += "SHAT_HATCHALA_SIDUR=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and (sector_zvira_zman_haelement=4)";
                            drPeiluyot = clCalcData.DtPeiluyotOved.Select(sQury, "shat_yetzia asc");
           
                            bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nihul.GetHashCode(), dTaarich, iSugSidur);
                            if (bYeshSidur || (iMisparSidur == 99301 && drPeiluyot.Length>0)) // && clCalcGeneral.objMeafyeneyOved.iMeafyen33))
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
                                //SetSugSidur(ref drSidurim[J], dTaarich, iMisparSidurNext);
                                iSugSidur = int.Parse(drSidurim[J]["sug_sidur"].ToString());

                                bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nihul.GetHashCode(), dTaarich, iSugSidur);
                                if (bSidurNehiga && bYeshSidur)
                                    bSidurMezake = true;
                                else if (bSidurNihulOrTafkid)
                                {
                                    bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nahagut.GetHashCode(), dTaarich, iSugSidur);
                                    if (bYeshSidur)
                                        bSidurMezake = true;
                                }
                            }

                            if (bSidurMezake)
                            {
                                dShatHatchalaLetashlum = DateTime.Parse(drSidurim[J]["shat_hatchala_letashlum"].ToString());
                                dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());

                                fErechSidur = float.Parse((dShatHatchalaLetashlum - dShatGmarLetashlum).TotalMinutes.ToString());
                                if (fErechSidur >= 1 && fErechSidur <= _oGeneralData.objParameters.iMinTimeBetweenSidurim)
                                {
                                    if (drSidurim[I]["Mezake_Halbasha"].ToString() == "2" || drSidurim[I]["Mezake_Halbasha"].ToString() == "3" || drSidurim[J]["Mezake_Halbasha"].ToString() == "1" || drSidurim[J]["Mezake_Halbasha"].ToString() == "3")
                                    {
                                        if (fErechSidur > 10)
                                            fErechSidur = fErechSidur - 10;
                                        else fErechSidur = 0;
                                    }

                                    fErech += fErechSidur;

                                    //רציפות לילה                                
                                    fErechSidurLaylaEgged = getDakotRezifutLayla(dShatHatchalaLetashlum, dShatGmarLetashlum, _oGeneralData.objParameters.dTchilatTosefetLaila, _oGeneralData.objParameters.dSiyumTosefetLaila);//9,10
                                    fErechSidurLaylaChok = getDakotRezifutLayla(dShatHatchalaLetashlum, dShatGmarLetashlum, _oGeneralData.objParameters.dSiyumTosefetLaila, _oGeneralData.objParameters.dSiyumTosefetLailaChok);//10,12
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
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.ZmanRetzifutTafkid.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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
                    drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ") and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        bNotCalc = false;
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                        drSidureyGrira = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and shat_hatchala_letashlum<=Convert('" + drSidurim[I]["shat_gmar_letashlum"].ToString() + "', 'System.DateTime')  and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
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

                                    if (CheckSugSidur(clGeneral.enMeafyen.SugAvoda.GetHashCode(), clGeneral.enSugAvoda.Grira.GetHashCode(), dTaarich, iSugSidur))
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
                            if (_oGeneralData.GetSugYomLemichsa(_iMisparIshi, dTaarich) < 10)
                            {
                                fDakotNochehut = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and mispar_sidur=" + iMisparSidur + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                                fErech = fDakotNochehut;
                                addRowToTable(clGeneral.enRechivim.MishmeretShniaBameshek.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.MishmeretShniaBameshek.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv128()
        {
            DataRow[] drSidurim,drSidurimLeyom;
            DataRow RowKodem, RowNext, drConenutGrira;
            int iMisparSidur;
            DateTime dShatHatchalaSidur, dShatHatchalaLetashlum, dShatGmarLetashlum;
            float fErech, fTempX, fTempY, fTempDakot;
            string sSidurimMeyuchadim;
            dShatHatchalaSidur = DateTime.MinValue;
            DateTime dShaa = dTaarich;
            iMisparSidur = 0;
            bool bConenutGrira, bGriraInConenutGrira;
            int iMikumKnisa=0;
            int iMikumYetzia=0;
            try
            {
              
                drConenutGrira = null;
                sSidurimMeyuchadim = GetSidurimMeyuchRechiv(clGeneral.enRechivim.ZmanGrirot.GetHashCode());
                if (sSidurimMeyuchadim.Length > 0)
                {
                    bConenutGrira = IsSidurConenutGriraExist(ref drConenutGrira);
                    drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ") and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        fTempX = 0;
                        fTempY = 0;
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                        
                        if (bConenutGrira){
                           dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                           dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());
                           bGriraInConenutGrira = cheakSidurGrirainConenutGrira(dShatHatchalaLetashlum, dShatGmarLetashlum, drConenutGrira);
                      
                            dShaa = DateTime.Parse(dTaarich.ToShortDateString() + " 15:30:00");

                            if (drSidurim[I]["MIKUM_SHAON_KNISA"].ToString() != "")
                                iMikumKnisa = int.Parse(drSidurim[I]["MIKUM_SHAON_KNISA"].ToString());
                            if (drSidurim[I]["MIKUM_SHAON_YETZIA"].ToString() != "")
                                iMikumYetzia = int.Parse(drSidurim[I]["MIKUM_SHAON_YETZIA"].ToString());
                        
                            if (dShatHatchalaSidur >= dShaa && bGriraInConenutGrira && (iMikumKnisa>0  || iMikumYetzia>0))
                            {
                                drSidurimLeyom = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0  and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur asc");
                                if (int.Parse(drSidurimLeyom[0]["mispar_sidur"].ToString()) == iMisparSidur && DateTime.Parse(drSidurimLeyom[0]["shat_hatchala_sidur"].ToString()) == dShatHatchalaSidur && iMikumKnisa>0)
                                {
                                    fTempY = _oGeneralData.objParameters.iTosefetZmanGrira;
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
                                    fTempY = Math.Min(_oGeneralData.objParameters.iTosefetZmanGrira, fTempDakot);
                                }
                       
                      
                                drSidurimLeyom = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null  and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur desc");
                               if (int.Parse(drSidurimLeyom[0]["mispar_sidur"].ToString()) == iMisparSidur && DateTime.Parse(drSidurimLeyom[0]["shat_hatchala_sidur"].ToString()) == dShatHatchalaSidur && iMikumYetzia>0)
                               {
                                   fTempX = _oGeneralData.objParameters.iTosefetZmanGrira;
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
                                   fTempX = Math.Min(_oGeneralData.objParameters.iTosefetZmanGrira, fTempDakot);
                              }
                            }
                        }
                        addRowToTable(clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fTempY);
                        addRowToTable(clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fTempX);
                    
                       dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                       dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());

                        fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString()) + fTempX + fTempY;

                        addRowToTable(clGeneral.enRechivim.ZmanGrirot.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                    
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.ZmanGrirot.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv131()
        {
            DataRow[] _drSidurim;
            int iMisparSidur,iMichutzLamichsa,iDay,iSugYom;
            DateTime dShatHatchalaLetashlum,dShatGmarLetashlum,dShatHatchalaSidur;
            float fMichsaYomit, fErech, fZmanHafsaka;
            int iIsuk;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                iSugYom = clCalcData.iSugYom;
                
             
                    _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime') and Hamarat_shabat=1");

                    for (int I = 0; I < _drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                        iMichutzLamichsa = int.Parse(_drSidurim[I]["out_michsa"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                        iDay = int.Parse(_drSidurim[I]["day_taarich"].ToString());
                        dShatHatchalaLetashlum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                        dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                        fZmanHafsaka = float.Parse(_drSidurim[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());
                        CheckSidurShabatToAdd(clGeneral.enRechivim.ShaotShabat100.GetHashCode(), iMisparSidur, iDay, iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, iMichutzLamichsa, false, fZmanHafsaka);
                    }
                 

                fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                        
                if ((fMichsaYomit == 0) && (_oGeneralData.objPirteyOved.iKodMaamdMishni == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode()))
                {
                    _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime') and MISPAR_SIDUR IN(99703,99202,99701,99010 ,99006)");
                    for (int I = 0; I < _drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                        fErech = 0;
                        dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                        fErech = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                        if (fErech > 0)
                        {
                            addRowToTable(clGeneral.enRechivim.ShaotShabat100.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                        }
                       
                    }
                }

                if (clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, iSugYom, dTaarich))
                {
                    _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime') and MISPAR_SIDUR IN(99012)");
                    for (int I = 0; I < _drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                        fErech = 0;
                        dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                        fErech = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                        if (fErech > 0)
                        {
                            addRowToTable(clGeneral.enRechivim.ShaotShabat100.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                        }

                    }

                    _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                    fErech = 0;
                    for (int I = 0; I < _drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());

                        dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                        dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                        dShatHatchalaLetashlum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                        fZmanHafsaka = float.Parse(_drSidurim[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());
                      
                        fErech = 0;
                        if ((dShatHatchalaLetashlum < _oGeneralData.objParameters.dShatMaavarYom) && dShatGmarLetashlum > _oGeneralData.objParameters.dShatMaavarYom)
                        {
                            iIsuk=_oGeneralData.objPirteyOved.iIsuk;
                             if (iIsuk!=122 && iIsuk!=123 && iIsuk!= 124 && iIsuk!= 127) 
                            {
                            fErech = float.Parse((dShatGmarLetashlum - _oGeneralData.objParameters.dShatMaavarYom).TotalMinutes.ToString());
                            fErech -= fZmanHafsaka;
                            }
                        }          
                           
                        if (fErech > 0)
                        {
                            addRowToTable(clGeneral.enRechivim.ShaotShabat100.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                        }

                    }
                   
                }

               // if ((clCalcData.CheckYomShishi(iSugYom) || clCalcData.CheckErevChag(iSugYom)) && fMichsaYomit == 0)
               // {
               //     _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime') and MISPAR_SIDUR IN(99011,99207,99007)");
                    
               //     for (int I = 0; I < _drSidurim.Length; I++)
               //     {
               //         iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
               //         fErech = 0;
               //         dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

               //         fErech = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

               //         if (fErech > 0)
               //         {
               //             addRowToTable(clGeneral.enRechivim.ShaotShabat100.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
               //         }

               //     }
               //} 

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.ShaotShabat100.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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

                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                    for (int I = 0; I < _drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                       dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                       fDakotHamaraShabat = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                       fDakotZikuyChofesh = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotZikuyChofesh.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                       fErech = fDakotHamaraShabat + fDakotZikuyChofesh;
                       addRowToTable(clGeneral.enRechivim.ChofeshZchut.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                    }
               
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.ChofeshZchut.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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

                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    fDakotPremiaShabat = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaShabat.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                    fDakotPremiaYomit = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaYomit.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                    fDakotPremiaBeshishi = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                    fErech = fDakotPremiaShabat + fDakotPremiaYomit + fDakotPremiaBeshishi;
                    addRowToTable(clGeneral.enRechivim.PremyaRegila.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.PremyaRegila.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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

                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                for (int I = 0; I < _drSidurim.Length ; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    fPremiaVisa = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaVisa.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                    fPremiaVisaShabat = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaVisaShabat.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                    fPremiaVisaBeShishi = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotPremiaVisaShishi.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                    fErech = fPremiaVisaShabat + fPremiaVisa + fPremiaVisaBeShishi;
                    addRowToTable(clGeneral.enRechivim.PremyaNamlak.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.PremyaNamlak.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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

                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    fDakotNochehutBefoal = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                    fDakotNochehutLetashlum = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                    fErech = fDakotNochehutBefoal - fDakotNochehutLetashlum;
                    addRowToTable(clGeneral.enRechivim.KizuzNochehut.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.KizuzNochehut.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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
                drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null  and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                for (int I = 0; I < drSidurim.Length ; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                    iOutMichsa = int.Parse(drSidurim[I]["out_michsa"].ToString());

                    if (clCalcData.CheckOutMichsa(_iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, iOutMichsa))
                    {
                        fDakotNihulTnua = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

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
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotMichutzLamichsaNihulTnua.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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

                drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                for (int I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                    iOutMichsa = int.Parse(drSidurim[I]["out_michsa"].ToString());

                    if (clCalcData.CheckOutMichsa(_iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, iOutMichsa))
                    {
                        fErech = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidShabat.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                        if (fErech > 0)
                        {
                            addRowToTable(clGeneral.enRechivim.MichutzLamichsaTafkidShabat.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.MichutzLamichsaTafkidShabat.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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

                drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                for (int I = 0; I < drSidurim.Length ; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                    iOutMichsa = int.Parse(drSidurim[I]["out_michsa"].ToString());

                    if (clCalcData.CheckOutMichsa(_iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, iOutMichsa))
                    {
                        fErech = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                        if (fErech > 0)
                        {
                            addRowToTable(clGeneral.enRechivim.MichutzLamichsaNihulShabat.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.MichutzLamichsaNihulShabat.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        private void CalcDakotMechutzLemichsa(int iKodRechiv)
        {
            string sSidurimMeyuchadim, sSugeySidur;
            DataRow[] drSidurim;
            int iMisparSidur,  iSugSidur;
            float fErech;
            DateTime  dShatHatchalaSidur;
            iMisparSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            try
            {
                sSidurimMeyuchadim = GetSidurimMeyuchRechiv(iKodRechiv);
                if (sSidurimMeyuchadim.Length > 0)
                {
                    drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and out_michsa=1 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ") and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                    for (int I = 0; I < drSidurim.Length ; I++)
                    {
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                        fErech = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                        addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fErech);

                    }
                }

                sSugeySidur = GetSugeySidurRechiv(iKodRechiv);
                if (sSugeySidur.Length > 0)
                {
                    drSidurim = GetSidurimRegilim();
                    for (int I = 0; I < drSidurim.Length; I++)
                    {
                        if ((int)drSidurim[I]["out_michsa"] == 1)
                        {
                            iMisparSidur = int.Parse(drSidurim[I]["MISPAR_SIDUR"].ToString());
                            dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                            
                            //SetSugSidur(ref drSidurim[I],dShatHatchalaSidur, iMisparSidur);

                            iSugSidur = int.Parse(drSidurim[I]["sug_sidur"].ToString());

                                    if (sSugeySidur.IndexOf("," + iSugSidur.ToString() + ",") > -1)
                                    {
                                      
                                        fErech = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                                        addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fErech);

                                    }
                                }
                           
                    }

                } 
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, iKodRechiv, _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv189()
        {
            // יש לבצע את החישוב עבור סידורים המוגדרים כסידורי נהגות – זיהוי סידורי נהיגה:
            //  ישנו מאפיין (קוד מאפיין = 3) לסוג סידור/סידור מיוחד המתאר את סוג העבודה   .
            //ערך מאפיין זה (סוג עבודה) = 5 = עבודת נהגות.
            int iMisparSidur, iSugSidur;
            bool bYeshSidur;
            float fMichsaYomit, fDakotRechiv, fDakotChol, fZmanHafsaka;
            int  iSugYom, iSugYomNext;
            float fTosefetGrirotHatchala, fTosefetGrirotSof, fErechRechiv;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, dShatHatchlaShabat;
            DataRow[] _drSidurMeyuchad, _drSidurRagil;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                fDakotChol = 0;
                dShatHatchlaShabat = _oGeneralData.objParameters.dKnisatShabat;
                iSugYom = _oGeneralData.GetSugYomLemichsa(_iMisparIshi, dTaarich);               
               //בדיקת סידורים רגילים
                // נלקחים הסידורים הרגילים שסקטור העבודה שלהם =נהגות
                _drSidurRagil = GetSidurimRegilim();
                fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                        
                if (_drSidurRagil.Length > 0)
                {
                    for (int I = 0; I < _drSidurRagil.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());
                        fDakotRechiv = 0;
                        dShatHatchalaSidur = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_sidur"].ToString());

                        //SetSugSidur(ref _drSidurRagil[I], dTaarich, iMisparSidur);

                        iSugSidur = int.Parse(_drSidurRagil[I]["sug_sidur"].ToString());

                        bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nahagut.GetHashCode(), dTaarich, iSugSidur);
                        if (bYeshSidur)
                        {
                            dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                            fTosefetGrirotHatchala = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                            fTosefetGrirotSof = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                            dShatHatchalaLetashlum = dShatHatchalaLetashlum.AddMinutes(-fTosefetGrirotSof);
                            dShatGmarLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_gmar_letashlum"].ToString());
                            dShatGmarLetashlum = dShatGmarLetashlum.AddMinutes(+fTosefetGrirotHatchala);
                            fZmanHafsaka = float.Parse(_drSidurRagil[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());
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
                                fDakotRechiv -= fZmanHafsaka;
                                addRowToTable(clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fDakotRechiv);
                            }
                            iSugYomNext = clGeneral.GetSugYom(clCalcData.DtYamimMeyuchadim, DateTime.Parse(dTaarich.ToShortDateString()).AddDays(1), clCalcData.DtSugeyYamimMeyuchadim);//, _oGeneralData.objMeafyeneyOved.iMeafyen56);
                            if (iSugYomNext == clGeneral.enSugYom.Shishi.GetHashCode() && fMichsaYomit == 0)
                            {
                                CalcGlishaLeyomShishi(dShatHatchalaLetashlum, dShatGmarLetashlum, ref fDakotChol, ref fDakotRechiv, fZmanHafsaka);
                                addRowToTable(clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fDakotRechiv);

                            }

                           
                            if (iSugSidur == 69)
                            {
                                fErechRechiv = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                                if (fErechRechiv > _oGeneralData.objParameters.iMinZmanGriraDarom)
                                {
                                    if (int.Parse(iMisparSidur.ToString().Substring(0, 2)) > 11)
                                    {
                                        fErechRechiv = _oGeneralData.objParameters.iMinZmanGriraDarom;
                                    }
                                    else
                                    {
                                        fErechRechiv = _oGeneralData.objParameters.iMinZmanGriraTzafon;
                                    }
                                }
                                else if (fErechRechiv > _oGeneralData.objParameters.iMinZmanGriraTzafon && fErechRechiv <= _oGeneralData.objParameters.iMinZmanGriraDarom)
                                {
                                    if (int.Parse(iMisparSidur.ToString().Substring(0, 2)) <= 11)
                                    {
                                        fErechRechiv = _oGeneralData.objParameters.iMinZmanGriraTzafon;
                                    }
                                }
                                fDakotRechiv -= fZmanHafsaka;
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
                        fTosefetGrirotHatchala = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGririoTchilatSidur.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                        fTosefetGrirotSof = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.TosefetGrirotSofSidur.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                        dShatHatchalaLetashlum = dShatHatchalaLetashlum.AddMinutes(-fTosefetGrirotSof);
                        dShatGmarLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_gmar_letashlum"].ToString());
                        dShatGmarLetashlum = dShatGmarLetashlum.AddMinutes(+fTosefetGrirotHatchala);

                        fZmanHafsaka = float.Parse(_drSidurMeyuchad[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());
                        fDakotRechiv = 0;
                        iSugYom = _oGeneralData.GetSugYomLemichsa(_iMisparIshi, dTaarich);
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
                            fDakotRechiv -= fZmanHafsaka;
                            addRowToTable(clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fDakotRechiv);
                        }

                        iSugYomNext = clGeneral.GetSugYom(clCalcData.DtYamimMeyuchadim, DateTime.Parse(dTaarich.ToShortDateString()).AddDays(1), clCalcData.DtSugeyYamimMeyuchadim);//, _oGeneralData.objMeafyeneyOved.iMeafyen56);
                        if (iSugYomNext == clGeneral.enSugYom.Shishi.GetHashCode() && fMichsaYomit == 0)
                        {
                            CalcGlishaLeyomShishi(dShatHatchalaLetashlum, dShatGmarLetashlum, ref fDakotChol, ref fDakotRechiv, fZmanHafsaka);
                            addRowToTable(clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fDakotRechiv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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
                dShatHatchlaShabat = _oGeneralData.objParameters.dKnisatShabat;
                iSugYom = _oGeneralData.GetSugYomLemichsa(_iMisparIshi, dTaarich);
                fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
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
                                  
                       //SetSugSidur(ref _drSidurRagil[I],dTaarich, iMisparSidur);

                        iSugSidur = int.Parse(_drSidurRagil[I]["sug_sidur"].ToString());

                                bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nihul.GetHashCode(), dTaarich, iSugSidur);
                                if (bYeshSidur)
                                {
                                    dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                                    dShatGmarLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_gmar_letashlum"].ToString());
                                    fZmanHafsaka = float.Parse(_drSidurRagil[I]["ZMAN_HAFSAKA_BESIDUR"].ToString());
                                    fDakotRechiv=0;
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
                                    iSugYomNext = clGeneral.GetSugYom(clCalcData.DtYamimMeyuchadim, DateTime.Parse(dTaarich.ToShortDateString()).AddDays(1), clCalcData.DtSugeyYamimMeyuchadim);//, _oGeneralData.objMeafyeneyOved.iMeafyen56);
                                    if (iSugYomNext == clGeneral.enSugYom.Shishi.GetHashCode() && fMichsaYomit == 0)
                                    {
                                        CalcGlishaLeyomShishi(dShatHatchalaLetashlum, dShatGmarLetashlum, ref fDakotChol, ref fDakotRechiv, fZmanHafsaka);
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
                            fDakotRechiv =float.Parse( (dShatHatchlaShabat - dShatHatchalaLetashlum).TotalMinutes.ToString());
                        }
                        else if (dShatHatchalaLetashlum < dShatHatchlaShabat && dShatGmarLetashlum <= dShatHatchlaShabat)
                        {
                            fDakotRechiv = float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString());
                        }
                        fDakotRechiv -= fDakotRechiv;
                        addRowToTable(clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fDakotRechiv);
                    }

                    iSugYomNext = clGeneral.GetSugYom(clCalcData.DtYamimMeyuchadim, DateTime.Parse(dTaarich.ToShortDateString()).AddDays(1), clCalcData.DtSugeyYamimMeyuchadim);//, _oGeneralData.objMeafyeneyOved.iMeafyen56);
                    if (iSugYomNext == clGeneral.enSugYom.Shishi.GetHashCode() && fMichsaYomit == 0)
                    {
                        CalcGlishaLeyomShishi(dShatHatchalaLetashlum, dShatGmarLetashlum, ref fDakotChol, ref fDakotRechiv, fZmanHafsaka);
                        addRowToTable(clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fDakotRechiv);
                    }  
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv192()
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur;
            float fErech, fDakotTafkidChol , fDakotTafkidShabat, fDakotTafkidShishi;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {

                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    fDakotTafkidChol = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidChol.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                    fDakotTafkidShabat = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidShabat.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                    fDakotTafkidShishi = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                    fErech = fDakotTafkidChol + fDakotTafkidShabat + fDakotTafkidShishi;
                   
                    addRowToTable(clGeneral.enRechivim.SachDakotTafkid.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.SachDakotTafkid.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv193()
        {
             // יש לבצע את החישוב עבור סידורים המוגדרים כסידורי נהגות – זיהוי סידורי נהיגה:
            //  ישנו מאפיין (קוד מאפיין = 3) לסוג סידור/סידור מיוחד המתאר את סוג העבודה   .
            //ערך מאפיין זה (סוג עבודה) = 1 =  תפקיד.
            int iMisparSidur, iSugSidur;
            bool bYeshSidur;
            int  iSugYom, iSugYomNext;
            float fDakotRechiv, fMichsaYomit, fDakotChol;
            DataRow[] _drSidurMeyuchad, _drSidurRagil;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur, dShatHatchlaShabat;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                //בדיקת סידורים רגילים
                // נלקחים הסידורים הרגילים שסקטור העבודה שלהם =תפקיד
                fDakotChol = 0;
                fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                        
                _drSidurRagil = GetSidurimRegilim();
                if (_drSidurRagil.Length > 0)
                {
                    for (int I = 0; I < _drSidurRagil.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());
                        fDakotRechiv = 0;
                       //SetSugSidur(ref _drSidurRagil[I],dTaarich, iMisparSidur);

                        iSugSidur = int.Parse(_drSidurRagil[I]["sug_sidur"].ToString());
                                bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Tafkid.GetHashCode(), dTaarich, iSugSidur);
                                if (bYeshSidur)
                                {
                                    iMisparSidur = int.Parse(_drSidurRagil[I]["mispar_sidur"].ToString());
                                    dShatHatchalaSidur = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_sidur"].ToString());
                                    dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                                    dShatGmarLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_gmar_letashlum"].ToString());
                                    dShatHatchlaShabat = _oGeneralData.objParameters.dKnisatShabat;
                                    fDakotRechiv = 0;
                                    iSugYom = _oGeneralData.GetSugYomLemichsa(_iMisparIshi, dTaarich);
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

                                    iSugYomNext = clGeneral.GetSugYom(clCalcData.DtYamimMeyuchadim, DateTime.Parse(dTaarich.ToShortDateString()).AddDays(1), clCalcData.DtSugeyYamimMeyuchadim);//, _oGeneralData.objMeafyeneyOved.iMeafyen56);
                                    if (iSugYomNext == clGeneral.enSugYom.Shishi.GetHashCode() && fMichsaYomit == 0)
                                    {
                                        CalcGlishaLeyomShishi(dShatHatchalaLetashlum, dShatGmarLetashlum, ref fDakotChol, ref fDakotRechiv, 0);
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
                   
                    dShatHatchalaLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_letashlum"].ToString());
                    dShatGmarLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_gmar_letashlum"].ToString());
                    dShatHatchlaShabat = _oGeneralData.objParameters.dKnisatShabat;
                    fDakotRechiv = 0;
                    iSugYom = _oGeneralData.GetSugYomLemichsa(_iMisparIshi, dTaarich);
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

                    iSugYomNext = clGeneral.GetSugYom(clCalcData.DtYamimMeyuchadim, DateTime.Parse(dTaarich.ToShortDateString()).AddDays(1), clCalcData.DtSugeyYamimMeyuchadim);//, _oGeneralData.objMeafyeneyOved.iMeafyen56);
                    if (iSugYomNext == clGeneral.enSugYom.Shishi.GetHashCode() && fMichsaYomit == 0)
                    {
                        CalcGlishaLeyomShishi(dShatHatchalaLetashlum, dShatGmarLetashlum, ref fDakotChol, ref fDakotRechiv,0);
                        addRowToTable(clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fDakotRechiv);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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

                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    fNehigaChol = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaChol.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                    fNihulTnua = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaChol.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                    fTafkidChol = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidChol.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                    fErech = fNehigaChol + fNihulTnua + fTafkidChol;
                    addRowToTable(clGeneral.enRechivim.NochehutChol.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.NochehutChol.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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

                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    fNehigaShishi = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNehigaShishi.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                    fNihulShishi = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                    fTafkidShishi = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                    fErech = fNehigaShishi + fNihulShishi + fTafkidShishi;
                    addRowToTable(clGeneral.enRechivim.NochehutBeshishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.NochehutBeshishi.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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

                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    fNehigaShabat = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNehigaShabat.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                    fNihulShabat = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNihulTnuaShabat.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                    fTafkidShabat = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidShabat.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                    fErech = fNehigaShabat + fNihulShabat + fTafkidShabat;
                    addRowToTable(clGeneral.enRechivim.NochehutShabat.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.NochehutShabat.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv202()
        {
            //דקות פרמיה בשישי  (רכיב 202 ) :  

            int iMisparSidur, iSugSidur;
            bool bYeshSidur;
            int iDay, iSugYom;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum, dShatHatchalaSidur;
            DataRow[] _drSidurMeyuchad, _drSidurRagil;
            float fErechRechiv, fDakotNochehutSidur, fMichsaYomit;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                iSugYom = clCalcData.iSugYom;
                fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
               
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

                            //SetSugSidur(ref _drSidurRagil[I],dTaarich, iMisparSidur);

                            iSugSidur = int.Parse(_drSidurRagil[I]["sug_sidur"].ToString());
                            bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nahagut.GetHashCode(), dTaarich, iSugSidur);
                            if (bYeshSidur)
                            {
                                iDay = int.Parse(_drSidurRagil[I]["day_taarich"].ToString());
                                dShatHatchalaLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_hatchala_letashlum"].ToString());
                                dShatGmarLetashlum = DateTime.Parse(_drSidurRagil[I]["shat_gmar_letashlum"].ToString());
                                fDakotNochehutSidur = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                                fErechRechiv = 0;

                                //if (dShatHatchalaLetashlum < _oGeneralData.objParameters.dKnisatShabat)
                                if ((clCalcData.CheckErevChag(iSugYom) || clCalcData.CheckYomShishi(iSugYom)) && fMichsaYomit == 0)
                                {
                                    fErechRechiv = fDakotNochehutSidur;
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
                        fDakotNochehutSidur = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                        //if (dShatHatchalaLetashlum < _oGeneralData.objParameters.dKnisatShabat)
                        if ((clCalcData.CheckErevChag(iSugYom) || clCalcData.CheckYomShishi(iSugYom)) && fMichsaYomit == 0)
                        {
                            fErechRechiv = fDakotNochehutSidur;
                        }

                        addRowToTable(clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErechRechiv);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotPremiaBeShishi.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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
                oPeilut.dTaarich = dTaarich;

                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                    if (dShatHatchalaSidur < _oGeneralData.objParameters.dKnisatShabat)
                    {
                        iErechElementimReka = oPeilut.CalcElementReka(iMisparSidur, dShatHatchalaSidur);

                        fErech216 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and KOD_RECHIV=" + clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                        if (fErech216 > 0)
                        {
                            addRowToTable(clGeneral.enRechivim.DakotPremiaVisaShishi.GetHashCode(), dShatHatchalaSidur, iMisparSidur, float.Parse(iErechElementimReka.ToString()));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotPremiaVisaShishi.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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

                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null  and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    iOutMichsa = int.Parse(_drSidurim[I]["out_michsa"].ToString());

                    if (clCalcData.CheckOutMichsa(_iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, iOutMichsa))
                    {
                        fTafkidShishi = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

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
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.MichutzLamichsaTafkidShishi.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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

                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null  and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    iOutMichsa = int.Parse(_drSidurim[I]["out_michsa"].ToString());

                    if (clCalcData.CheckOutMichsa(_iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, iOutMichsa))
                    {
                        fTnuaShishi = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotNihulShishi.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

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
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.MichutzLamichsaTnuaShishi.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        public float CalcRechiv209()
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur, dShatGmarSidur,dTempM1,dTempM2;
            float fErech;
            float fZmanAruchatTzharyim=0;
            float fSumZmanAruchatTzharyim = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {

                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and mispar_sidur=99001  and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    dShatGmarSidur = DateTime.Parse(_drSidurim[I]["shat_gmar_sidur"].ToString());
                   
                    fErech = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString());
                   addRowToTable(clGeneral.enRechivim.NochehutPremiaLerishum.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                   dTempM1 = clGeneral.GetDateTimeFromStringHour("12:30", dTaarich.Date);
                   dTempM2 = clGeneral.GetDateTimeFromStringHour("13:30", dTaarich.Date);

                   if (dShatHatchalaSidur <= dTempM1 && dShatGmarSidur >= dTempM1)
                   { fZmanAruchatTzharyim = float.Parse((dShatGmarSidur - dTempM1).TotalMinutes.ToString()); }
                   if (dShatHatchalaSidur >= dTempM1 && dShatHatchalaSidur <= dTempM2 && dShatGmarSidur >= dTempM2)
                   { fZmanAruchatTzharyim =float.Parse( (dTempM2-dShatHatchalaSidur).TotalMinutes.ToString()); }
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
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.NochehutPremiaLerishum.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv210(out float fZmanAruchatBoker,out float fZmanAruchatTzharim,out float fZmanAruchatErev)
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur, dShatGmarSidur,dShatHatchalaLetashlum, dTempM1, dTempM2;
            float fErech;
             fZmanAruchatBoker = 0;
             fZmanAruchatTzharim  = 0;
             fZmanAruchatErev = 0;
             float fZmanAruchatBokerSidur = 0;
             float fZmanAruchatTzharimSidur = 0;
             float fZmanAruchatErevSidur = 0;
             dShatHatchalaSidur = DateTime.MinValue;
             iMisparSidur = 0;
            try
            {
                if (_oGeneralData.objMeafyeneyOved.iMeafyen100 > 0)
                {
                    _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur=99001  and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                    for (int I = 0; I < _drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                        dShatGmarSidur = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                        dShatHatchalaLetashlum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                        
                        fErech = float.Parse((dShatGmarSidur - dShatHatchalaLetashlum).TotalMinutes.ToString());
                        addRowToTable(clGeneral.enRechivim.NochehutPremiaMevakrim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                        //חישוב זמן ארוחת בוקר
                        dTempM1 = clGeneral.GetDateTimeFromStringHour("08:30", dTaarich.Date);
                        dTempM2 = clGeneral.GetDateTimeFromStringHour("09:30", dTaarich.Date);

                        if (dShatHatchalaLetashlum <= dTempM1 && dShatGmarSidur >= dTempM1)
                        { fZmanAruchatBokerSidur = float.Parse((dShatGmarSidur - dTempM1).TotalMinutes.ToString()); }
                        if (dShatHatchalaLetashlum >= dTempM1 && dShatHatchalaLetashlum <= dTempM2 && dShatGmarSidur >= dTempM2)
                        { fZmanAruchatBokerSidur = float.Parse((dTempM2 - dShatHatchalaSidur).TotalMinutes.ToString()); }
                        if (dShatHatchalaLetashlum >= dTempM1 && dShatGmarSidur <= dTempM2)
                        { fZmanAruchatBokerSidur = float.Parse((dShatGmarSidur - dShatHatchalaLetashlum).TotalMinutes.ToString()); }
                        if (fZmanAruchatBokerSidur > 20)
                        { fZmanAruchatBokerSidur = 20; }
                        else { fZmanAruchatBokerSidur = 0; }

                        //חישוב זמן ארוחת צהריים
                        dTempM1 = _oGeneralData.objParameters.dStartAruchatTzaharayim;
                        dTempM2 = _oGeneralData.objParameters.dEndAruchatTzaharayim;

                        if (dShatHatchalaSidur <= dTempM1 && dShatGmarSidur >= dTempM1)
                        { fZmanAruchatTzharimSidur =float.Parse( (dShatGmarSidur - dTempM1).TotalMinutes.ToString()); }
                        if (dShatHatchalaSidur >= dTempM1 && dShatHatchalaSidur <= dTempM2 && dShatGmarSidur >= dTempM2)
                        { fZmanAruchatTzharimSidur =float.Parse( (dTempM2 - dShatHatchalaSidur).TotalMinutes.ToString()); }
                        if (dShatHatchalaSidur >= dTempM1 && dShatGmarSidur <= dTempM2)
                        { fZmanAruchatTzharimSidur = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString()); }
                        if (fZmanAruchatTzharimSidur > 30)
                        { fZmanAruchatTzharimSidur = 30; }
                        else { fZmanAruchatTzharimSidur = 0; }

                        //חישוב זמן ארוחת ערב
                        dTempM1 = clGeneral.GetDateTimeFromStringHour("18:00", dTaarich.Date);
                        dTempM2 = clGeneral.GetDateTimeFromStringHour("19:30", dTaarich.Date);

                        if (dShatHatchalaSidur <= dTempM1 && dShatGmarSidur >= dTempM1)
                        { fZmanAruchatErevSidur = float.Parse((dShatGmarSidur - dTempM1).TotalMinutes.ToString()); }
                        if (dShatHatchalaSidur >= dTempM1 && dShatHatchalaSidur <= dTempM2 && dShatGmarSidur >= dTempM2)
                        { fZmanAruchatErevSidur = float.Parse((dTempM2 - dShatHatchalaSidur).TotalMinutes.ToString()); }
                        if (dShatHatchalaSidur >= dTempM1 && dShatGmarSidur <= dTempM2)
                        { fZmanAruchatErevSidur = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString()); }
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
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.NochehutPremiaMevakrim.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }


        public void CalcRechiv211_212(out float fZmanAruchatBoker,out float fZmanAruchatTzharim,out float fZmanAruchatErev,out int iBokerRechiv,out int iTzharyimRechiv,out int iErevRechiv)
        {
            DataRow[] _drSidurim;
            int iMisparSidur,iKodRechiv;
            DateTime dShatHatchalaSidur, dShatGmarSidur,dShatGmarLetashlum, dTempM1, dTempM2,dShatHatchalaLetaslum;
            float fErech;
             float fZmanAruchatBokerSidur = 0;
             float fZmanAruchatTzharimSidur = 0;
             float fZmanAruchatErevSidur = 0;
             dShatHatchalaSidur = DateTime.MinValue;
             iMisparSidur = 0;
            try
            {
                 fZmanAruchatBoker = 0;
                 fZmanAruchatTzharim  = 0;
                 fZmanAruchatErev = 0;
                 iBokerRechiv = 0;
                 iErevRechiv = 0;
                 iTzharyimRechiv = 0;

                 _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null  and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur asc");

                    for (int I = 0; I < _drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                        dShatGmarSidur = DateTime.Parse(_drSidurim[I]["shat_gmar_sidur"].ToString());
                        dShatGmarLetashlum=DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                        dShatHatchalaLetaslum=DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                         fErech=0;
                        iKodRechiv=0;
                        if (iMisparSidur==99001)
                        {
                           if (_oGeneralData.objPirteyOved.iAchsana == 1 || _oGeneralData.objPirteyOved.iAchsana == 2)
                            {
                                  fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaSidur).TotalMinutes.ToString());
                               iKodRechiv=clGeneral.enRechivim.NochehutPremiaMeshekAchsana.GetHashCode();
                            }
                            else
                            {
                                    fErech =float.Parse( (dShatGmarLetashlum - dShatHatchalaLetaslum).TotalMinutes.ToString());
                          iKodRechiv=clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode();
                            }
                        }
                        else  if (iMisparSidur==99222)
                        {
                          fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaLetaslum).TotalMinutes.ToString());
                          iKodRechiv=clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode();
                        }
                        else  if (iMisparSidur==99221)
                        {
                               fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaSidur).TotalMinutes.ToString());
                             iKodRechiv=clGeneral.enRechivim.NochehutPremiaMeshekAchsana.GetHashCode();
                        }

                        addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fErech);
       
                        if ( iKodRechiv==clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode())
                        {dShatHatchalaSidur=dShatHatchalaLetaslum;}

                        dTempM1 = clGeneral.GetDateTimeFromStringHour("09:00", dTaarich.Date);
                        dTempM2 = clGeneral.GetDateTimeFromStringHour("09:20", dTaarich.Date);

                         if (dShatHatchalaSidur <= dTempM1 && dShatGmarLetashlum >= dTempM1)
                        { fZmanAruchatBokerSidur = float.Parse((dShatGmarLetashlum - dTempM1).TotalMinutes.ToString()); }
                        if (dShatHatchalaSidur >= dTempM1 && dShatHatchalaSidur <= dTempM2 && dShatGmarLetashlum >= dTempM2)
                        { fZmanAruchatBokerSidur = float.Parse((dTempM2 - dShatHatchalaSidur).TotalMinutes.ToString()); }
                        if (dShatHatchalaSidur >= dTempM1 && dShatGmarLetashlum <= dTempM2)
                        { fZmanAruchatBokerSidur = float.Parse((dShatGmarLetashlum - dShatHatchalaSidur).TotalMinutes.ToString()); }
                        if (fZmanAruchatBokerSidur > 20)
                        { fZmanAruchatBokerSidur = 20; }
                        else { fZmanAruchatBokerSidur = 0; }

                        //חישוב זמן ארוחת צהריים
                        dTempM1 = _oGeneralData.objParameters.dStartAruchatTzaharayim;
                        dTempM2 = _oGeneralData.objParameters.dEndAruchatTzaharayim;

                        if (dShatHatchalaSidur <= dTempM1 && dShatGmarLetashlum >= dTempM1)
                        { fZmanAruchatTzharimSidur =float.Parse( (dShatGmarLetashlum - dTempM1).TotalMinutes.ToString()); }
                        if (dShatHatchalaSidur >= dTempM1 && dShatHatchalaSidur <= dTempM2 && dShatGmarLetashlum >= dTempM2)
                        { fZmanAruchatTzharimSidur = float.Parse((dTempM2 - dShatHatchalaSidur).TotalMinutes.ToString()); }
                        if (dShatHatchalaSidur >= dTempM1 && dShatGmarLetashlum <= dTempM2)
                        { fZmanAruchatTzharimSidur = float.Parse((dShatGmarLetashlum - dShatHatchalaSidur).TotalMinutes.ToString()); }
                        if (fZmanAruchatTzharimSidur > 30)
                        { fZmanAruchatTzharimSidur = 30; }
                        else { fZmanAruchatTzharimSidur = 0; }

                        //חישוב זמן ארוחת ערב
                        dTempM1 = clGeneral.GetDateTimeFromStringHour("19:00", dTaarich.Date);
                        dTempM2 = clGeneral.GetDateTimeFromStringHour("19:20", dTaarich.Date);

                        if (dShatHatchalaSidur <= dTempM1 && dShatGmarLetashlum >= dTempM1)
                        { fZmanAruchatErevSidur = float.Parse((dShatGmarLetashlum - dTempM1).TotalMinutes.ToString()); }
                        if (dShatHatchalaSidur >= dTempM1 && dShatHatchalaSidur <= dTempM2 && dShatGmarLetashlum >= dTempM2)
                        { fZmanAruchatErevSidur = float.Parse((dTempM2 - dShatHatchalaSidur).TotalMinutes.ToString()); }
                        if (dShatHatchalaSidur >= dTempM1 && dShatGmarLetashlum <= dTempM2)
                        { fZmanAruchatErevSidur = float.Parse((dShatGmarLetashlum - dShatHatchalaSidur).TotalMinutes.ToString()); }
                        if (fZmanAruchatErevSidur > 20)
                        { fZmanAruchatErevSidur = 20; }
                        else { fZmanAruchatErevSidur = 0; }

                        if (fZmanAruchatBoker==0 && fZmanAruchatBokerSidur>0)
                        {
                        iBokerRechiv=iKodRechiv;
                        }
                         if (fZmanAruchatTzharim==0 && fZmanAruchatTzharimSidur>0)
                        {
                         iTzharyimRechiv=iKodRechiv;
                         }
                         if (fZmanAruchatErev==0 && fZmanAruchatErevSidur>0)
                        {
                         iErevRechiv=iKodRechiv;
                         }
                        fZmanAruchatBoker += fZmanAruchatBokerSidur;
                        fZmanAruchatTzharim += fZmanAruchatTzharimSidur;
                        fZmanAruchatErev += fZmanAruchatErevSidur;
                   
                }
              
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.NochehutPremiaMeshekMusachim.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
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
                oPeilut.dTaarich = dTaarich;
                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

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
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.SachNesiot.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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
               oPeilut.dTaarich=dTaarich;

               _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    bSidurNehiga = false;
                    bSidurNehiga = isSidurNehiga(ref _drSidurim[I]);
                    if (bSidurNehiga)
                    {
                        iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                        oPeilut.CalcRechiv214(iMisparSidur, dShatHatchalaSidur);
                        fErech = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_PEILUT"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND KOD_RECHIV=" + clGeneral.enRechivim.DakotHistaglut.GetHashCode().ToString() + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                        addRowToTable(clGeneral.enRechivim.DakotHistaglut.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                    }
                }  

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotHistaglut.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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
                 oPeilut.dTaarich=dTaarich;

                 _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                    oPeilut.CalcRechiv215(iMisparSidur, dShatHatchalaSidur);
                     fErech = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_PEILUT"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND KOD_RECHIV=" + clGeneral.enRechivim.SachKM.GetHashCode().ToString() + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                  
                    addRowToTable(clGeneral.enRechivim.SachKM.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.SachKM.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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

                _drSidurMeyuchad = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime') and not sidur_namlak_visa is null");

                for (int I = 0; I <= _drSidurMeyuchad.Length - 1; I++)
                {
                    iMisparSidur = int.Parse(_drSidurMeyuchad[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_sidur"].ToString());
                    dShatHatchalaLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_hatchala_letashlum"].ToString());
                    dShatGmarLetashlum = DateTime.Parse(_drSidurMeyuchad[I]["shat_gmar_letashlum"].ToString());

                    oPeilut.CalcRechiv216(iMisparSidur, dShatHatchalaSidur);
                    fErech = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_PEILUT"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND KOD_RECHIV=" + clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode().ToString() + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                  
                    fErech = fErech * (1 - float.Parse(_drSidurMeyuchad[I]["Achuz_knas_lepremyat_visa"].ToString())/100) * (1 + float.Parse(_drSidurMeyuchad[I]["ACHUZ_VIZA_BESIKUN"].ToString())/100);

                    addRowToTable(clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                   
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.SachKMVisaLepremia.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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
            bool bFirstSidur=false;
            iMisparSidurFirst = 0;
            try
            {
                oPeilut.dTaarich=dTaarich;

                _drSidurim = clCalcData.DtYemeyAvoda.Select("mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur asc");
                if (_drSidurim.Length > 0)
                {
                    iMisparSidurFirst = int.Parse(_drSidurim[0]["mispar_sidur"].ToString());
                    dShatHatchalaSidurFirst = DateTime.Parse(_drSidurim[0]["shat_hatchala_sidur"].ToString());
                 } 

                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    if (iMisparSidur == iMisparSidurFirst && dShatHatchalaSidur==dShatHatchalaSidurFirst)
                        bFirstSidur=true;
                    oPeilut.CalcRechiv217(iMisparSidur, dShatHatchalaSidur,bFirstSidur);
                    fErech = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_PEILUT"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND KOD_RECHIV=" + clGeneral.enRechivim.DakotHagdara.GetHashCode().ToString() + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                    addRowToTable(clGeneral.enRechivim.DakotHagdara.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotHagdara.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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
                oPeilut.dTaarich = dTaarich;

                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                    oPeilut.CalcRechiv218(iMisparSidur, dShatHatchalaSidur);
                    fErech = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_PEILUT"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND KOD_RECHIV=" + clGeneral.enRechivim.DakotKisuiTor.GetHashCode().ToString() + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                    addRowToTable(clGeneral.enRechivim.DakotKisuiTor.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                }

            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotKisuiTor.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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

                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur=99001  and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    dShatGmarSidur = DateTime.Parse(_drSidurim[I]["shat_gmar_sidur"].ToString());
                    
                    fErech = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString());
                    addRowToTable(clGeneral.enRechivim.NochehutLepremiaMachsanKartisim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                    dTempM1 = clGeneral.GetDateTimeFromStringHour("12:30", dTaarich.Date);
                    dTempM2 = clGeneral.GetDateTimeFromStringHour("13:30", dTaarich.Date);

                    if (dShatHatchalaSidur <= dTempM1 && dShatGmarSidur >= dTempM1)
                    { fZmanAruchatTzharyim =float.Parse( (dShatGmarSidur - dTempM1).TotalMinutes.ToString()); }
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
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.NochehutLepremiaMachsanKartisim.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

       

        public void CalcRechiv235()
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur, dShatGmarLetashlum, dShatHatchalaLetashlum;
            float fErech;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
              
              _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

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
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.NochehutLePremyatNehageyTenderim.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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

                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur=99001  and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    dShatGmarSidur = DateTime.Parse(_drSidurim[I]["shat_gmar_sidur"].ToString());
                   
                    fErech = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString());
                    addRowToTable(clGeneral.enRechivim.NochechutLepremyaMenahel.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                    dTempM1 = clGeneral.GetDateTimeFromStringHour("12:30", dTaarich.Date);
                    dTempM2 = clGeneral.GetDateTimeFromStringHour("13:30", dTaarich.Date);

                    if (dShatHatchalaSidur <= dTempM1 && dShatGmarSidur >= dTempM1)
                    { fZmanAruchatTzharyim =float.Parse( (dShatGmarSidur - dTempM1).TotalMinutes.ToString()); }
                    if (dShatHatchalaSidur >= dTempM1 && dShatHatchalaSidur <= dTempM2 && dShatGmarSidur >= dTempM2)
                    { fZmanAruchatTzharyim = float.Parse((dTempM2 - dShatHatchalaSidur).TotalMinutes.ToString()); }
                    if (dShatHatchalaSidur >= dTempM1 && dShatGmarSidur <= dTempM2)
                    { fZmanAruchatTzharyim =float.Parse( (dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString()); }
                    if (fZmanAruchatTzharyim > 30)
                    { fZmanAruchatTzharyim = 30; }
                    fSumZmanAruchatTzharyim += fZmanAruchatTzharyim;
                }
                return fSumZmanAruchatTzharyim;
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.NochechutLepremyaMenahel.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        public void CalcRechiv256( out float fZmanAruchatTzharim,   out int iErevRechiv)
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur, dShatGmarSidur, dShatGmarLetashlum,  dShatHatchalaLetaslum;
            float fErech;
            float fZmanAruchatTzharimSidur = 0;
            
            string sSidurimLerchiv="";
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                fZmanAruchatTzharim = 0;
                iErevRechiv = 0;
               
                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null  and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur asc");
                sSidurimLerchiv=GetSidurimMeyuchRechiv(clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode());

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    dShatGmarSidur = DateTime.Parse(_drSidurim[I]["shat_gmar_sidur"].ToString());
                    dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                    dShatHatchalaLetaslum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());

                    if ((!clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, clCalcData.iSugYom, dTaarich) && (iMisparSidur == 99001 && (_oGeneralData.objPirteyOved.iIsuk==17 || _oGeneralData.objPirteyOved.iIsuk==18 || _oGeneralData.objPirteyOved.iIsuk==19 || _oGeneralData.objPirteyOved.iIsuk==20)) || (sSidurimLerchiv.IndexOf("," + iMisparSidur.ToString() + ",") > -1)))
                    {
                        fErech = float.Parse(_drSidurim[I]["ZMAN_LELO_HAFSAKA"].ToString());// float.Parse((dShatGmarLetashlum - dShatHatchalaLetaslum).TotalMinutes.ToString());

                        addRowToTable(clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                        if (!clCalcData.CheckYomShishi(clCalcData.iSugYom))  
                        {
                            CalcZmaneyAruchot(dShatHatchalaLetaslum, dShatGmarLetashlum,  out fZmanAruchatTzharimSidur);
     
                            fZmanAruchatTzharim += fZmanAruchatTzharimSidur;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }
       

        private void CalcZmaneyAruchot(DateTime dShatHatchalaLetaslum, DateTime dShatGmarLetashlum,  out float fZmanAruchatTzharim)
        {
            float fTempX = 0;
            DateTime dTempM1, dTempM2;

            try
            {
                //dTempM1 = clGeneral.GetDateTimeFromStringHour("08:00", dTaarich.Date);
                //dTempM2 = clGeneral.GetDateTimeFromStringHour("09:30", dTaarich.Date);
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
                dTempM1 = _oGeneralData.objParameters.dStartAruchatTzaharayim;
                dTempM2 = _oGeneralData.objParameters.dEndAruchatTzaharayim;
                fTempX = 0;
                if (dShatHatchalaLetaslum <= dTempM1 && dShatGmarLetashlum >= dTempM1)
                { fTempX = float.Parse((dShatGmarLetashlum - dTempM1).TotalMinutes.ToString()); }
                if (dShatHatchalaLetaslum >= dTempM1 && dShatHatchalaLetaslum <= dTempM2 && dShatGmarLetashlum >= dTempM2)
                { fTempX = float.Parse((dTempM2 - dShatHatchalaLetaslum).TotalMinutes.ToString()); }
                if (dShatHatchalaLetaslum >= dTempM1 && dShatGmarLetashlum <= dTempM2)
                { fTempX = float.Parse((dShatGmarLetashlum - dShatHatchalaLetaslum).TotalMinutes.ToString()); }
                if (fTempX > 30)
                { fZmanAruchatTzharim = 30; }
                else { fZmanAruchatTzharim = fTempX; }

                ////חישוב זמן ארוחת ערב
                //dTempM1 = clGeneral.GetDateTimeFromStringHour("18:00", dTaarich.Date);
                //dTempM2 = clGeneral.GetDateTimeFromStringHour("19:30", dTaarich.Date);
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


        public void CalcRechiv257( out float fZmanAruchatTzharim,   out int iErevRechiv)
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur, dShatGmarSidur, dShatGmarLetashlum, dShatHatchalaLetaslum;
            float fErech;
            float fZmanAruchatTzharimSidur = 0;
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            try
            {
                fZmanAruchatTzharim = 0;
                iErevRechiv = 0;
               
                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null  and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur asc");
                
                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    dShatGmarSidur = DateTime.Parse(_drSidurim[I]["shat_gmar_sidur"].ToString());
                    dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                    dShatHatchalaLetaslum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());

                    if ((!clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, clCalcData.iSugYom, dTaarich) && iMisparSidur == 99001 && (_oGeneralData.objPirteyOved.iIsuk==181 || _oGeneralData.objPirteyOved.iIsuk==191)))
                    {
                        fErech = float.Parse(_drSidurim[I]["ZMAN_LELO_HAFSAKA"].ToString()); // float.Parse((dShatGmarLetashlum - dShatHatchalaLetaslum).TotalMinutes.ToString());

                        addRowToTable(clGeneral.enRechivim.NochehutLepremiaMetachnenTnua.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                        if (!clCalcData.CheckYomShishi(clCalcData.iSugYom))
                        {
                            CalcZmaneyAruchot(dShatHatchalaLetaslum, dShatGmarLetashlum, out fZmanAruchatTzharimSidur);

                            fZmanAruchatTzharim += fZmanAruchatTzharimSidur;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.NochehutLepremiaMetachnenTnua.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }


         public void CalcRechiv258( out float fZmanAruchatTzharim,   out int iErevRechiv)
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur, dShatGmarSidur, dShatGmarLetashlum,  dShatHatchalaLetaslum;
            float fErech;
            float fZmanAruchatTzharimSidur = 0;
           string sSidurimLerchiv="";
            dShatHatchalaSidur = DateTime.MinValue;
            iMisparSidur = 0;
            int iSugSidur;
            bool bYeshSidur;
            string sIsuk, sQury;
            DataRow[]  drPeiluyot;
           // DataTable dtPeiluyot;
            try
            {
              
                fZmanAruchatTzharim = 0;
                iErevRechiv = 0;
               
                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null  and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur asc");
                sSidurimLerchiv=GetSidurimMeyuchRechiv(clGeneral.enRechivim.NochehutLepremiaManasim.GetHashCode());

                for (int I = 0; I < _drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                    dShatGmarSidur = DateTime.Parse(_drSidurim[I]["shat_gmar_sidur"].ToString());
                    dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                    dShatHatchalaLetaslum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                    fErech  =0;
                    if (!clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, clCalcData.iSugYom, dTaarich) )
                     {
                         if ((iMisparSidur == 99204 || iMisparSidur == 99224) && _oGeneralData.objPirteyOved.iIsuk == 420)
                         {
                             fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaLetaslum).TotalMinutes.ToString());
                             CalcZmaneyAruchot(dShatHatchalaLetaslum, dShatGmarLetashlum,out  fZmanAruchatTzharimSidur);
                             
                         }
                         else
                         {
                             sIsuk = ",401, 402, 403,421, 422 ,404,412,420,";
                             oPeilut.dTaarich = dTaarich;
                            // dtPeiluyot = oPeilut.GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);
                             sQury = "MISPAR_SIDUR=" + iMisparSidur + " AND taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime') and ";
                             sQury += "SHAT_HATCHALA_SIDUR=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and (SUBSTRING(makat_nesia,1,3)='740') ";
                             drPeiluyot = clCalcData.DtPeiluyotOved.Select(sQury, "shat_yetzia asc");

                             if (iMisparSidur.ToString().Substring(0, 2) != "99")
                             {
                                 //SetSugSidur(ref _drSidurim[I], dTaarich, iMisparSidur);

                                 iSugSidur = int.Parse(_drSidurim[I]["sug_sidur"].ToString());
                                 bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nihul.GetHashCode(), dTaarich, iSugSidur);
                                 if (bYeshSidur)
                                 {
                                    
                                     fErech = clCalcData.GetSumErechRechiv(clCalcData.DtPeiluyotOved.Compute("sum(zmanElement)", sQury));

                                     oPeilut.CalcZmaneyAruchot(drPeiluyot, dShatHatchalaLetaslum, dShatGmarLetashlum, out  fZmanAruchatTzharimSidur);
                             
                                    // fErech = clCalcData.GetSumErechRechiv(dtPeiluyot.Compute("sum(zmanElement)", "SUBSTRING(makat_nesia,1,3)='740'"));
                                    // oPeilut.CalcZmaneyAruchot(dtPeiluyot, 740, dShatHatchalaLetaslum, dShatGmarLetashlum, out  fZmanAruchatBokerSidur, out  fZmanAruchatTzharimSidur, out fZmanAruchatErevSidur);
                             
                                 }
                             }
                             else if (iMisparSidur == 99402 && sIsuk.IndexOf("," + _oGeneralData.objPirteyOved.iIsuk.ToString() + ",") > -1)
                             {
                                 fErech = clCalcData.GetSumErechRechiv(clCalcData.DtPeiluyotOved.Compute("sum(zmanElement)", sQury));
                                 oPeilut.CalcZmaneyAruchot(drPeiluyot, dShatHatchalaLetaslum, dShatGmarLetashlum, out  fZmanAruchatTzharimSidur);
                        
                                 //fErech = clCalcData.GetSumErechRechiv(dtPeiluyot.Compute("sum(zmanElement)", "SUBSTRING(makat_nesia,1,3)='740'"));
                                 //oPeilut.CalcZmaneyAruchot(dtPeiluyot, 740, dShatHatchalaLetaslum, dShatGmarLetashlum, out  fZmanAruchatBokerSidur, out  fZmanAruchatTzharimSidur, out fZmanAruchatErevSidur);
                             }
                         }
                        
                        addRowToTable(clGeneral.enRechivim.NochehutLepremiaSadran.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                        if (!clCalcData.CheckYomShishi(clCalcData.iSugYom))
                        {
                            fZmanAruchatTzharim += fZmanAruchatTzharimSidur;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.NochehutLepremiaSadran.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }


         public void CalcRechiv259( out float fZmanAruchatTzharim,  out int iErevRechiv)
         {
             DataRow[] _drSidurim;
             int iMisparSidur;
             DateTime dShatHatchalaSidur, dShatGmarSidur, dShatGmarLetashlum, dShatHatchalaLetaslum;
             float fErech;
             float fZmanAruchatTzharimSidur = 0;
             string sSidurimLerchiv = "";
             dShatHatchalaSidur = DateTime.MinValue;
             iMisparSidur = 0;
             int iSugSidur;
             bool bYeshSidur;
             string sIsuk, sQury;
             DataRow[] drPeiluyot;
          //   DataTable dtPeiluyot;
             try
             {
                 fZmanAruchatTzharim = 0;
                 iErevRechiv = 0;

                 _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null  and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur asc");
                 sSidurimLerchiv = GetSidurimMeyuchRechiv(clGeneral.enRechivim.NochehutLepremiaRakaz.GetHashCode());

                 for (int I = 0; I < _drSidurim.Length; I++)
                 {
                     iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                     dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                     dShatGmarSidur = DateTime.Parse(_drSidurim[I]["shat_gmar_sidur"].ToString());
                     dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                     dShatHatchalaLetaslum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                     fErech = 0;
                     if (!clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, clCalcData.iSugYom, dTaarich))
                     {
                        
                             sIsuk = ",401, 402, 403,421, 422 ,404,412,420,";
                             oPeilut.dTaarich = dTaarich;
                          //   dtPeiluyot = oPeilut.GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);
                             sQury = "MISPAR_SIDUR=" + iMisparSidur + " AND taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime') and ";
                             sQury += "SHAT_HATCHALA_SIDUR=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and (SUBSTRING(makat_nesia,1,3)='730')";
                             drPeiluyot = clCalcData.DtPeiluyotOved.Select(sQury, "shat_yetzia asc");

                             if (iMisparSidur.ToString().Substring(0, 2) != "99")
                             {
                                 //SetSugSidur(ref _drSidurim[I], dTaarich, iMisparSidur);

                                 iSugSidur = int.Parse(_drSidurim[I]["sug_sidur"].ToString());
                                 bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nihul.GetHashCode(), dTaarich, iSugSidur);
                                 if (bYeshSidur)
                                 {
                                     fErech = clCalcData.GetSumErechRechiv(clCalcData.DtPeiluyotOved.Compute("sum(zmanElement)", sQury));
                                     oPeilut.CalcZmaneyAruchot(drPeiluyot, dShatHatchalaLetaslum, dShatGmarLetashlum,  out  fZmanAruchatTzharimSidur);

                                 }
                             }
                             else if (iMisparSidur == 99402 && sIsuk.IndexOf("," + _oGeneralData.objPirteyOved.iIsuk.ToString() + ",") > -1)
                             {
                                 fErech = clCalcData.GetSumErechRechiv(clCalcData.DtPeiluyotOved.Compute("sum(zmanElement)", sQury));
                                 oPeilut.CalcZmaneyAruchot(drPeiluyot, dShatHatchalaLetaslum, dShatGmarLetashlum, out  fZmanAruchatTzharimSidur);
                             }
                        
                         addRowToTable(clGeneral.enRechivim.NochehutLepremiaRakaz.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                         if (!clCalcData.CheckYomShishi(clCalcData.iSugYom))
                         {      
                             fZmanAruchatTzharim += fZmanAruchatTzharimSidur;
                         }

                     }
                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.NochehutLepremiaRakaz.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                 throw (ex);
             }
         }


         public void CalcRechiv260( out float fZmanAruchatTzharim, out int iErevRechiv)
         {
             DataRow[] _drSidurim;
             int iMisparSidur;
             DateTime dShatHatchalaSidur, dShatGmarSidur, dShatGmarLetashlum, dShatHatchalaLetaslum;
             float fErech;
             float fZmanAruchatTzharimSidur = 0;
             string sSidurimLerchiv = "";
             dShatHatchalaSidur = DateTime.MinValue;
             iMisparSidur = 0;
             int iSugSidur;
             bool bYeshSidur;
             string sIsuk, sQury;
             DataRow[] drPeiluyot;
           //  DataTable dtPeiluyot;
             try
             {  
                 fZmanAruchatTzharim = 0;
                 iErevRechiv = 0;

                 _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null  and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur asc");
                 sSidurimLerchiv = GetSidurimMeyuchRechiv(clGeneral.enRechivim.NochehutLepremiaPakach.GetHashCode());

                 for (int I = 0; I < _drSidurim.Length; I++)
                 {
                     iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                     dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                     dShatGmarSidur = DateTime.Parse(_drSidurim[I]["shat_gmar_sidur"].ToString());
                     dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                     dShatHatchalaLetaslum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                     fErech = 0;
                     if (!clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, clCalcData.iSugYom, dTaarich))
                     {
                         if ((iMisparSidur == 99205 || iMisparSidur == 99225) && (_oGeneralData.objPirteyOved.iIsuk == 401 || _oGeneralData.objPirteyOved.iIsuk == 422))
                         {
                             fErech = float.Parse((dShatGmarLetashlum - dShatHatchalaLetaslum).TotalMinutes.ToString());
                             CalcZmaneyAruchot(dShatHatchalaLetaslum, dShatGmarLetashlum,out  fZmanAruchatTzharimSidur);

                         }
                         else 
                         {
                             sIsuk = ",401, 402, 403,421, 422 ,404,412,420,";
                             oPeilut.dTaarich = dTaarich;
                            // dtPeiluyot = oPeilut.GetPeiluyLesidur(iMisparSidur, dShatHatchalaSidur);
                             sQury = "MISPAR_SIDUR=" + iMisparSidur + " AND taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime') and ";
                             sQury += "SHAT_HATCHALA_SIDUR=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and (SUBSTRING(makat_nesia,1,3)='750')";
                             drPeiluyot = clCalcData.DtPeiluyotOved.Select(sQury, "shat_yetzia asc");

           
                             if (iMisparSidur.ToString().Substring(0, 2) != "99")
                             {
                                 //SetSugSidur(ref _drSidurim[I], dTaarich, iMisparSidur);

                                 iSugSidur = int.Parse(_drSidurim[I]["sug_sidur"].ToString());
                                 bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(), clGeneral.enSectorAvoda.Nihul.GetHashCode(), dTaarich, iSugSidur);
                                 if (bYeshSidur)
                                 {
                                     fErech = clCalcData.GetSumErechRechiv(clCalcData.DtPeiluyotOved.Compute("sum(zmanElement)", sQury));
                                     oPeilut.CalcZmaneyAruchot(drPeiluyot, dShatHatchalaLetaslum, dShatGmarLetashlum, out  fZmanAruchatTzharimSidur);

                                 }
                             }
                             else if (iMisparSidur == 99402 && sIsuk.IndexOf("," + _oGeneralData.objPirteyOved.iIsuk.ToString() + ",") > -1)
                             {
                                 fErech = clCalcData.GetSumErechRechiv(clCalcData.DtPeiluyotOved.Compute("sum(zmanElement)", sQury));
                                 oPeilut.CalcZmaneyAruchot(drPeiluyot, dShatHatchalaLetaslum, dShatGmarLetashlum, out  fZmanAruchatTzharimSidur);
                             }
                         }

                         addRowToTable(clGeneral.enRechivim.NochehutLepremiaPakach.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                         if (!clCalcData.CheckYomShishi(clCalcData.iSugYom))
                         {
                             fZmanAruchatTzharim += fZmanAruchatTzharimSidur;
                         }

                     }
                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.NochehutLepremiaPakach.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                 throw (ex);
             }
         }

         public void CalcRechiv263()
         {
             int iMisparSidur, iSugHashlama;
             DateTime  dShatHatchalaSidur;
             DataRow[] drSidurim,drSidurimLeyom;
             DataRow RowNext;
             iMisparSidur = 0;
             dShatHatchalaSidur = DateTime.MinValue;
             float fErech;
             float fDakotNochehut;
             try
             {

                  drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and Hashlama>0 and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                for (int I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                    iSugHashlama = int.Parse(drSidurim[I]["sug_Hashlama"].ToString());

                    if (iSugHashlama==1 || clCalcData.CheckUshraBakasha(clGeneral.enKodIshur.HashlamaLeshaot.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur))
                    {
                        fDakotNochehut = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                        if (CheckSidurBySectorAvoda(ref drSidurim[I], iMisparSidur, clGeneral.enSectorAvoda.Nahagut.GetHashCode()))
                        {
                            fErech = Math.Max(0, (int.Parse(drSidurim[I]["Hashlama"].ToString()) * 60) - fDakotNochehut);

                            //אם X > 0 יש לבדוק האם יש סידור עוקב לסידור עם ההשלמה אם כן, 
                            //ערך הרכיב = הנמוך מבין (X , שעת התחלה לתשלום של הסידור העוקב פחות שעות גמר לתשלום של הסידור הנבדק)
                            //אם אין סידור עוקב, ערך הרכיב = X

                            if (fErech > 0)
                            {
                                drSidurimLeyom = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur asc");
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
                 clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.HashlamaBenahagut.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                 throw (ex);
             }
         }

         public void CalcRechiv264()
         {
             int iMisparSidur, iSugHashlama;
             DateTime  dShatHatchalaSidur;
             DataRow[] drSidurim,drSidurimLeyom;
             DataRow RowNext;
             iMisparSidur = 0;
             dShatHatchalaSidur = DateTime.MinValue;
             float fErech;
             float fDakotNochehut;
             try
             {

                  drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and Hashlama>0 and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                for (int I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                    iSugHashlama = int.Parse(drSidurim[I]["sug_Hashlama"].ToString());

                    if (iSugHashlama == 1 || clCalcData.CheckUshraBakasha(clGeneral.enKodIshur.HashlamaLeshaot.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur))
                    {
                        fDakotNochehut = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                        if(CheckSidurBySectorAvoda(ref drSidurim[I], iMisparSidur, clGeneral.enSectorAvoda.Nihul.GetHashCode()))
                        {
                            fErech = Math.Max(0, (int.Parse(drSidurim[I]["Hashlama"].ToString()) * 60) - fDakotNochehut);
                       
                        //אם X > 0 יש לבדוק האם יש סידור עוקב לסידור עם ההשלמה אם כן, 
                        //ערך הרכיב = הנמוך מבין (X , שעת התחלה לתשלום של הסידור העוקב פחות שעות גמר לתשלום של הסידור הנבדק)
                        //אם אין סידור עוקב, ערך הרכיב = X

                        if (fErech > 0)
                        {
                            drSidurimLeyom = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur asc");
                            RowNext = drSidurimLeyom[0];
                            for (int J = 0; J < drSidurimLeyom.Length; J++)
                            {
                                RowNext = drSidurimLeyom[J];
                                if ((drSidurim[I]["mispar_sidur"].ToString() == drSidurimLeyom[J]["mispar_sidur"].ToString()) && (drSidurim[I]["shat_hatchala_sidur"].ToString() == drSidurimLeyom[J]["shat_hatchala_sidur"].ToString()))
                                {
                                    if (J < (drSidurimLeyom.Length-1))
                                    {
                                        RowNext = drSidurimLeyom[J + 1];
                                    }

                                    break;
                                }
                            }
                            if ((drSidurim[I]["mispar_sidur"].ToString() != RowNext["mispar_sidur"].ToString()) && (drSidurim[I]["shat_hatchala_sidur"].ToString() != RowNext["shat_hatchala_sidur"].ToString()))
                            {
                                fErech = Math.Min(fErech,float.Parse((DateTime.Parse(RowNext["shat_hatchala_letashlum"].ToString())-DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString())).TotalMinutes.ToString()));
                            }

                            addRowToTable(clGeneral.enRechivim.HashlamaBenihulTnua.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                        }
                    }    
                    }                          
                }

             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.HashlamaBenihulTnua.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                 throw (ex);
             }
         }

         public void CalcRechiv265()
         {
             int iMisparSidur, iSugHashlama;
             DateTime  dShatHatchalaSidur;
             DataRow[] drSidurim,drSidurimLeyom;
             DataRow RowNext;
             iMisparSidur = 0;
             dShatHatchalaSidur = DateTime.MinValue;
             float fErech;
             float fDakotNochehut;
             try
             {

                  drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and Hashlama>0 and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                for (int I = 0; I < drSidurim.Length; I++)
                {
                    iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                    iSugHashlama = int.Parse(drSidurim[I]["sug_Hashlama"].ToString());

                    if (iSugHashlama == 1 || clCalcData.CheckUshraBakasha(clGeneral.enKodIshur.HashlamaLeshaot.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur))
                    {
                        fDakotNochehut = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutBefoal.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                        if (CheckSidurBySectorAvoda(ref drSidurim[I], iMisparSidur, clGeneral.enSectorAvoda.Tafkid.GetHashCode()))
                        {
                            fErech = Math.Max(0, (int.Parse(drSidurim[I]["Hashlama"].ToString()) * 60) - fDakotNochehut);
                       
                        //אם X > 0 יש לבדוק האם יש סידור עוקב לסידור עם ההשלמה אם כן, 
                        //ערך הרכיב = הנמוך מבין (X , שעת התחלה לתשלום של הסידור העוקב פחות שעות גמר לתשלום של הסידור הנבדק)
                        //אם אין סידור עוקב, ערך הרכיב = X

                        if (fErech > 0)
                        {
                            drSidurimLeyom = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur asc");
                            RowNext = drSidurimLeyom[0];
                            for (int J = 0; J < drSidurimLeyom.Length; J++)
                            {
                                RowNext = drSidurimLeyom[J];
                                if ((drSidurim[I]["mispar_sidur"].ToString() == drSidurimLeyom[J]["mispar_sidur"].ToString()) && (drSidurim[I]["shat_hatchala_sidur"].ToString() == drSidurimLeyom[J]["shat_hatchala_sidur"].ToString()))
                                {
                                    if (J < (drSidurimLeyom.Length-1))
                                    {
                                        RowNext = drSidurimLeyom[J + 1];
                                    }

                                    break;
                                }
                            }
                            if ((drSidurim[I]["mispar_sidur"].ToString() != RowNext["mispar_sidur"].ToString()) && (drSidurim[I]["shat_hatchala_sidur"].ToString() != RowNext["shat_hatchala_sidur"].ToString()))
                            {
                                fErech = Math.Min(fErech,float.Parse((DateTime.Parse(RowNext["shat_hatchala_letashlum"].ToString())-DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString())).TotalMinutes.ToString()));
                            }

                            addRowToTable(clGeneral.enRechivim.HashlamaBetafkid.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                        }
                    }    
                    }                          
                }

             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.HashlamaBetafkid.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                 throw (ex);
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
                     drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ") and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                     for (int I = 0; I < drSidurim.Length; I++)
                     {
                         iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                         dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());

                         fErech += _oGeneralData.objParameters.fChelkiyutAchuzMiluim;
                      }
                 }
                 return fErech;
             }

             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.YomMiluimChelki.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                 throw (ex);
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
                 oPeilut.dTaarich = dTaarich;

                 _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                 for (int I = 0; I < _drSidurim.Length; I++)
                 {
                     iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                     dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                     oPeilut.CalcRechiv267(iMisparSidur, dShatHatchalaSidur);
                     
                     fErech = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_PEILUT"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotElementim.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                     addRowToTable(clGeneral.enRechivim.DakotElementim.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                 }

             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotElementim.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                 throw (ex);
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
                 oPeilut.dTaarich = dTaarich;

                 _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                 for (int I = 0; I < _drSidurim.Length; I++)
                 {
                     iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                     dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());

                     oPeilut.CalcRechiv268(iMisparSidur, dShatHatchalaSidur);
                     
                     fErech = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_PEILUT"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNesiaLepremia.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                     addRowToTable(clGeneral.enRechivim.DakotNesiaLepremia.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                 }

             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.DakotNesiaLepremia.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                 throw (ex);
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
             try
             {

                 drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                 if (drSidurim.Length > 0)
                 {
                     for (int I = 0; I < drSidurim.Length; I++)
                     {
                         if (drSidurim[I]["ein_leshalem_tos_lila"].ToString() == "")
                         {
                             iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                             dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                             
                             fHalbashaTchilatYom = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.HalbashaTchilatYom.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                             fHalbashaSofYom = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') AND KOD_RECHIV=" + clGeneral.enRechivim.HalbashaSofYom.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                             dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString()).AddMinutes(-fHalbashaTchilatYom);
                             dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString()).AddMinutes(fHalbashaSofYom);
                             fErech = 0;

                             //אם סידור הינו סידור ויזה צבאית - סידור מיוחד בעל שליפת מאפיינים (מס' סידור מיוחד, קוד מאפיין = 45 ) עם ערך = 1 זהו יום אחרון של הוויזה - TB_Sidurim_Ovedim.Yom_Visa= 3 אזי יש לבצע את בדיקת זמן הסידור מול שעות לילה חוק לפי שעת התחלה של סידור TB_Sidurim_Ovedim. Shat_hatchala  ולא שעת התחלה לתשלום של סידור.
                             if (drSidurim[I]["sidur_namlak_visa"].ToString() == "1" && drSidurim[I]["yom_visa"].ToString() == "3")
                             {
                                 dShatHatchalaLetashlum = dShatHatchalaSidur;
                             }

                             iIsuk = _oGeneralData.objPirteyOved.iIsuk;
                             iSugYomLemichsa = _oGeneralData.GetSugYomLemichsa(_iMisparIshi, dTaarich);
                      
                             if ((iIsuk == 122 || iIsuk == 123 || iIsuk == 124 || iIsuk == 127) && iMisparSidur == 99001 && clDefinitions.GetSugMishmeret(_iMisparIshi, dTaarich, iSugYomLemichsa, dShatHatchalaSidur, DateTime.Parse(drSidurim[I]["shat_gmar_sidur"].ToString()), _oGeneralData.objParameters) == clGeneral.enSugMishmeret.Liyla.GetHashCode())
                             {
                                 dZmanSiyuomTosLila = _oGeneralData.objParameters.dSiyumMishmeretLilaMafilim.AddDays(-1); 
                               }
                             else
                             {
                                 dZmanSiyuomTosLila = _oGeneralData.objParameters.dSiyumTosefetLailaChok.AddDays(-1);
                           
                            }

                             dZmanTchilatTosLila = clGeneral.GetDateTimeFromStringHour("00:01", dTaarich);
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


                                 addRowToTable(clGeneral.enRechivim.ZmanLilaSidureyBoker.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);
                             }
                         

                     }
                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.ZmanLilaSidureyBoker.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                 throw (ex);
             }
         }

         public void CalcRechiv931()
         {
             DataRow[] drSidurim;
             int iMisparSidur=0;
             DateTime dShatHatchalaSidur = DateTime.MinValue;
             DateTime dShatGmarSidurKodem;
             float fErech;
             int iMisparSidurKodem;
             try
             {
                 drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur asc");

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
                                     addRowToTable(clGeneral.enRechivim.HalbashaTchilatYom.GetHashCode(), dShatHatchalaSidur, iMisparSidur, _oGeneralData.objParameters.iZmanHalbash);
                                 else
                                 {
                                     dShatGmarSidurKodem = dShatHatchalaSidur;
                                      iMisparSidurKodem = int.Parse(drSidurim[I - 1]["mispar_sidur"].ToString());
                                      if ((iMisparSidurKodem == 99500 || iMisparSidurKodem == 99501) &&  (I - 2)>=0)
                                          dShatGmarSidurKodem = DateTime.Parse(drSidurim[I - 2]["shat_gmar_sidur"].ToString());
                                      else if ((I - 1)>=0) dShatGmarSidurKodem = DateTime.Parse(drSidurim[I - 1]["shat_gmar_sidur"].ToString());

                                     fErech = Math.Min(_oGeneralData.objParameters.iZmanHalbash, float.Parse((dShatHatchalaSidur - dShatGmarSidurKodem).TotalMinutes.ToString()));
                                     addRowToTable(clGeneral.enRechivim.HalbashaTchilatYom.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                                 }
                             }
                         }
                     }
                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.HalbashaTchilatYom.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                 throw (ex);
             }
         }

         public void CalcRechiv932()
         {
             DataRow[] drSidurim;
             int iMisparSidur=0;
             DateTime dShatHatchalaSidur = DateTime.MinValue;
             DateTime dShatHatchalaSidurNext,dShatGmarSidur;
             float fErech;
             int iMisparSidurNext;
            try
            {
                drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')", "shat_hatchala_sidur asc");

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
                                    addRowToTable(clGeneral.enRechivim.HalbashaSofYom.GetHashCode(), dShatHatchalaSidur, iMisparSidur, _oGeneralData.objParameters.iZmanHalbash);
                                else
                                {
                                    dShatGmarSidur = DateTime.Parse(drSidurim[I]["shat_gmar_sidur"].ToString());
                                    dShatHatchalaSidurNext = dShatGmarSidur;
                                    iMisparSidurNext = int.Parse(drSidurim[I+1]["mispar_sidur"].ToString());
                                    if ((iMisparSidurNext == 99500 || iMisparSidurNext == 99501) && (I + 2)<drSidurim.Length)
                                        dShatHatchalaSidurNext = DateTime.Parse(drSidurim[I + 2]["shat_hatchala_sidur"].ToString());
                                    else if ((I + 1) < drSidurim.Length) dShatHatchalaSidurNext = DateTime.Parse(drSidurim[I + 1]["shat_hatchala_sidur"].ToString());
                                   
                                    
                                    fErech = Math.Min(_oGeneralData.objParameters.iZmanHalbash, float.Parse((dShatHatchalaSidurNext - dShatGmarSidur).TotalMinutes.ToString()));
                                    addRowToTable(clGeneral.enRechivim.HalbashaSofYom.GetHashCode(), dShatHatchalaSidur, iMisparSidur, fErech);

                                }
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, clGeneral.enRechivim.HalbashaSofYom.GetHashCode(), _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
         }

       public void CalcNochehutPremia(int iKodRechiv,Boolean bMeafyenPremia,out float fZmanAruchatBoker, out float fZmanAruchatTzharim, out float fZmanAruchatErev)
        {
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur, dShatGmarSidur, dTempM1, dTempM2, dShatHatchalaLethaslum;
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
                    _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur=99001  and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

                    for (int I = 0; I < _drSidurim.Length; I++)
                    {
                        iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                        dShatGmarSidur = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());
                        dShatHatchalaLethaslum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                       
                        fErech = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString());
                        addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fErech);

                        if (iKodRechiv == clGeneral.enRechivim.NochehutLePremyatDfus.GetHashCode() || iKodRechiv == clGeneral.enRechivim.NochehutLePremyatNehageyTovala.GetHashCode())
                        {dShatHatchalaLethaslum=dShatHatchalaSidur;}

                        //חישוב זמן ארוחת בוקר
                        dTempM1 = clGeneral.GetDateTimeFromStringHour("09:00", dTaarich.Date);
                        dTempM2 = clGeneral.GetDateTimeFromStringHour("09:20", dTaarich.Date);

                        if (dShatHatchalaLethaslum <= dTempM1 && dShatGmarSidur >= dTempM1)
                        { fZmanAruchatBokerSidur = float.Parse((dShatGmarSidur - dTempM1).TotalMinutes.ToString()); }
                        if (dShatHatchalaLethaslum >= dTempM1 && dShatHatchalaLethaslum <= dTempM2 && dShatGmarSidur >= dTempM2)
                        { fZmanAruchatBokerSidur = float.Parse((dTempM2 - dShatHatchalaLethaslum).TotalMinutes.ToString()); }
                        if (dShatHatchalaLethaslum >= dTempM1 && dShatGmarSidur <= dTempM2)
                        { fZmanAruchatBokerSidur = float.Parse((dShatGmarSidur - dShatHatchalaLethaslum).TotalMinutes.ToString()); }
                        if (fZmanAruchatBokerSidur > 20)
                        { fZmanAruchatBokerSidur = 20; }
                        else { fZmanAruchatBokerSidur = 0; }

                        //חישוב זמן ארוחת צהריים
                        dTempM1 = _oGeneralData.objParameters.dStartAruchatTzaharayim;
                        dTempM2 = _oGeneralData.objParameters.dEndAruchatTzaharayim;

                        if (dShatHatchalaSidur <= dTempM1 && dShatGmarSidur >= dTempM1)
                        { fZmanAruchatTzharimSidur =float.Parse( (dShatGmarSidur - dTempM1).TotalMinutes.ToString()); }
                        if (dShatHatchalaSidur >= dTempM1 && dShatHatchalaSidur <= dTempM2 && dShatGmarSidur >= dTempM2)
                        { fZmanAruchatTzharimSidur = float.Parse((dTempM2 - dShatHatchalaSidur).TotalMinutes.ToString()); }
                        if (dShatHatchalaSidur >= dTempM1 && dShatGmarSidur <= dTempM2)
                        { fZmanAruchatTzharimSidur = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString()); }
                        if (fZmanAruchatTzharimSidur > 30)
                        { fZmanAruchatTzharimSidur = 30; }
                        else { fZmanAruchatTzharimSidur = 0; }

                        //חישוב זמן ארוחת ערב
                        dTempM1 = clGeneral.GetDateTimeFromStringHour("19:00", dTaarich.Date);
                        dTempM2 = clGeneral.GetDateTimeFromStringHour("19:20", dTaarich.Date);

                        if (dShatHatchalaSidur <= dTempM1 && dShatGmarSidur >= dTempM1)
                        { fZmanAruchatErevSidur = float.Parse((dShatGmarSidur - dTempM1).TotalMinutes.ToString()); }
                        if (dShatHatchalaSidur >= dTempM1 && dShatHatchalaSidur <= dTempM2 && dShatGmarSidur >= dTempM2)
                        { fZmanAruchatErevSidur =float.Parse( (dTempM2 - dShatHatchalaSidur).TotalMinutes.ToString()); }
                        if (dShatHatchalaSidur >= dTempM1 && dShatGmarSidur <= dTempM2)
                        { fZmanAruchatErevSidur = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString()); }
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
                clLogBakashot.SetError(_lBakashaId, "E", null, iKodRechiv, _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        public float CalcYemeyHeadrut(int iKodRechiv,out int  iCountSidurim,float fMichsaYomit)
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
                iCountSidurim = 0;
                sSidurimMeyuchadim = GetSidurimMeyuchRechiv(iKodRechiv);
                if (sSidurimMeyuchadim.Length > 0)
                {
                    drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                    iCountSidurim = drSidurim.Length;
                   
                    drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ") and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                    for (int I = 0; I < drSidurim.Length ; I++)
                    {
                      iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                      dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                      fDakotNochehut = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                      dShatHatchalaLetashlum = DateTime.Parse(drSidurim[I]["shat_hatchala_letashlum"].ToString());
                      dShatGmarLetashlum = DateTime.Parse(drSidurim[I]["shat_gmar_letashlum"].ToString());
                     
                      if (fDakotNochehut==0)
                          fDakotNochehut =Math.Min(fMichsaYomit, float.Parse((dShatGmarLetashlum - dShatHatchalaLetashlum).TotalMinutes.ToString()));
                         
                      fErech += fDakotNochehut;
                    }
                }
                return fErech;
            }

            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, iKodRechiv, _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
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
                    drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and MISPAR_SIDUR IN(" + sSidurimMeyuchadim + ") and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
                    for (int I = 0; I < drSidurim.Length ; I++)
                    {
                        iMisparSidur = int.Parse(drSidurim[I]["mispar_sidur"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                        fErech = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                        addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fErech);

                    }
                }

                sSugeySidur = GetSugeySidurRechiv(iKodRechiv);
                if (sSugeySidur.Length > 0)
                {
                    drSidurim = GetSidurimRegilim();
                    for (int I = 0; I < drSidurim.Length ; I++)
                    {
                        iMisparSidur = int.Parse(drSidurim[I]["MISPAR_SIDUR"].ToString());
                        dShatHatchalaSidur = DateTime.Parse(drSidurim[I]["shat_hatchala_sidur"].ToString());
                        //SetSugSidur(ref drSidurim[I],dTaarich, iMisparSidur);

                        iSugSidur = int.Parse(drSidurim[I]["sug_sidur"].ToString());
                                if (sSugeySidur.IndexOf("," + iSugSidur.ToString() + ",") > -1)
                                {
                                    
                                    fErech = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_SIDUR"].Compute("SUM(ERECH_RECHIV)", "MISPAR_SIDUR=" + iMisparSidur + " AND KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                                    addRowToTable(iKodRechiv, dShatHatchalaSidur, iMisparSidur, fErech);

                                }
                         
                    }

                }
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", null, iKodRechiv, _iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, null, null, "CalcSidur: " + ex.Message, null);
                throw (ex);
            }
        }

        private void CheckSidurCholToAdd(int iRechiv, int iMisparSidur, int iDay, int iSugYom, DateTime dShatHatchalaLetashlum, DateTime dShatGmarLetashlum, DateTime dShatHatchalaSidur, int iMichutzLamichsa, bool bCheckMafil, float ZmanHafsaka)
        {
            bool bIsShabat;
            float fErechShabat, fErech, fMichsaYomit126, fErechShishi;
            int iSugYomNext;
            fErech = 0;
            fErechShabat = 0;
            fErechShishi=0;
            
            //בדיקת יוצאים מן הכלל לחישוב
            //סידורים שבוצעו בשבת או בערב שבת או חג לא נכללים
            //יש לחשב רק החלק היחסי שבוצע בחול
            try
            {

                bIsShabat = CheckShabat(iMisparSidur, iDay, iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, ref fErech, ref fErechShabat, bCheckMafil, ZmanHafsaka);
                if (!bIsShabat)
                {
                    fMichsaYomit126 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                    if (!CheckErevShabatChag(iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, ref fErech, ref fErechShabat, ZmanHafsaka))
                    {
                        //בדיקת גלישת סידור מיום חול ליום ו'/ערב חג 
                        iSugYomNext = clGeneral.GetSugYom(clCalcData.DtYamimMeyuchadim, DateTime.Parse(dTaarich.ToShortDateString()).AddDays(1), clCalcData.DtSugeyYamimMeyuchadim );//, _oGeneralData.objMeafyeneyOved.iMeafyen56);
                        if (clCalcData.CheckErevChag(iSugYomNext) || clCalcData.CheckYomShishi(iSugYomNext))
                        {
                            fMichsaYomit126 = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.AddDays(1).ToShortDateString() + "', 'System.DateTime')"));
                            if (fMichsaYomit126 > 0)
                            {
                                if (fErech > 0)
                                {
                                    addRowToTable(iRechiv, dShatHatchalaSidur, iMisparSidur, fErech, iMichutzLamichsa);
                                }
                            }
                            else
                            {
                                //אם מכסה יומית מחושבת = 0 יש לחשב את החלק היחסי שבוצע בחול
                                CalcGlishaLeyomShishi(dShatHatchalaLetashlum, dShatGmarLetashlum, ref fErech, ref fErechShishi, ZmanHafsaka);
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

                        addRowToTable(iRechiv, dShatHatchalaSidur, iMisparSidur, fErech, iMichutzLamichsa);
                    }
                }
                else if (fErech > 0)
                {
                    //אם הסידור מתחיל בשבת

                    addRowToTable(iRechiv, dShatHatchalaSidur, iMisparSidur, fErech, iMichutzLamichsa);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CheckSidurShabatToAdd(int iRechiv, int iMisparSidur, int iDay, int iSugYom, DateTime dShatHatchalaLetashlum, DateTime dShatGmarLetashlum, DateTime dShatHatchalaSidur, int iMichutzLamichsa, bool bCheckMafil,float ZmanHafsaka)
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
                bIsShabat = CheckShabat(iMisparSidur, iDay, iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, ref fErech, ref fErechShabat, bCheckMafil, ZmanHafsaka);
                if (!bIsShabat)
                {
                    if (CheckErevShabatChag(iSugYom, dShatHatchalaLetashlum, dShatGmarLetashlum, ref fErech, ref fErechShabat, ZmanHafsaka))
                    {
                        //אם הסידור מתחיל בערב חג או שבת  

                        if (fErechShabat > 0)
                        {

                            addRowToTable(iRechiv, dShatHatchalaSidur, iMisparSidur, fErechShabat, iMichutzLamichsa);
                        }
                    }
                   
                }
                else if (fErechShabat > 0)
                {
                    //אם הסידור מתחיל בשבת

                    addRowToTable(iRechiv, dShatHatchalaSidur, iMisparSidur, fErechShabat, iMichutzLamichsa);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CheckSugSidur(int iMeafyen, int iErech, DateTime dDate,int iSugSidur)
        {   //הפונקציה מקבלת קוד מאפיין,סוג סידור,ערך מאפיין ותאריך ומחזירה האם קיים כזה סוג סידור

            DataRow[] dr;

            dr = clCalcData.DtMeafyeneySugSidur.Select("kod_meafyen=" + iMeafyen.ToString() + " and erech=" + iErech + " and Convert('" + dDate.ToShortDateString() + "','System.DateTime') >= me_taarich and Convert('" + dDate.ToShortDateString() + "', 'System.DateTime') <= ad_taarich and sug_sidur=" + iSugSidur.ToString());

            return ((dr.Length > 0) ? true : false);
        }

       

        private DataRow[] GetSidurimMeyuchadim()
        {   
            DataRow[] dr;

            dr = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

            return dr;
        }

        private DataRow[] GetSidurimRegilim()
        {
            DataRow[] dr;

            dr = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)<>99 and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");

            return dr;
        }


        private DataRow[] GetSidurimMeyuchadim(string sMeafyen, int iErech)
        {   
            DataRow[] dr;

            dr = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and SUBSTRING(convert(mispar_sidur,'System.String'),1,2)=99 and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime') and " + sMeafyen + "=" + iErech);

            return dr;
        }

        private DataRow[] GetSidurimMeyuchadim(string sMeafyen, int iErech,int iMisparSidur)
        {
            DataRow[] dr;

            dr = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur=" + iMisparSidur + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime') and " + sMeafyen + "=" + iErech);

            return dr;
        }

        //private void //SetSugSidur(ref DataRow drSidur,DateTime dSidurDate, int iMisparSidur)
        //{
        //    try
        //    {
        //        //סידורים רגילים
        //        //נבדוק מול התנועה
        //        DataRow[] rowSidur;
        //        if (drSidur["sug_sidur"].ToString() == "0" || drSidur["sug_sidur"].ToString() == "")
        //        {
        //            drSidur["sug_sidur"] = 0;
        //            rowSidur = clCalcData.DtSugeySidur.Select("taarich=Convert('" + dSidurDate.ToShortDateString() + "', 'System.DateTime') and mispar_sidur=" + iMisparSidur);
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

        //}

       
        //private bool CheckSidurMeyuchad(int iMeafyen,int  iErech,DateTime dDate,int iMisparSidur)
        //{   // בדיקה האם קיים סידור מיוחד לפי: מספר סידור, מאפיין , ערך ותאריך

        //    DataRow[] dr;
            
        //    dr = _dtSidurimMeyuchadim.Select("kod_meafyen=" + iMeafyen.ToString() + " and erech=" + iErech + " and Convert('" + dDate.ToShortDateString() +  "','System.DateTime') >= me_taarich and Convert('" + dDate.ToShortDateString() + "', 'System.DateTime') <= ad_taarich and mispar_sidur=" + iMisparSidur.ToString());

        //    return ((dr.Length>0) ? true : false);
        //}

        private void addRowToTable(int iKodRechiv,DateTime dShatHatchala,int iMisparSidur,float fErechRechiv)
       {
           DataRow drChishuv;

           if (_dsChishuv.Tables["CHISHUV_SIDUR"].Select("KOD_RECHIV=" + iKodRechiv + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')").Length == 0)
           {
               if (fErechRechiv > 0)
               {
                   drChishuv = _dtChishuvSidur.NewRow();
                   drChishuv["BAKASHA_ID"] = _lBakashaId;
                   drChishuv["MISPAR_ISHI"] = _iMisparIshi;
                   drChishuv["TAARICH"] = dTaarich;
                   drChishuv["MISPAR_SIDUR"] = iMisparSidur;
                   drChishuv["SHAT_HATCHALA"] = dShatHatchala;
                   drChishuv["KOD_RECHIV"] = iKodRechiv;
                   drChishuv["ERECH_RECHIV"] = fErechRechiv;
                   _dtChishuvSidur.Rows.Add(drChishuv);
               }
           }
           else
           {
               UpdateRowInTable(iKodRechiv, fErechRechiv,dShatHatchala,iMisparSidur);
           }
     
       }

        private void UpdateRowInTable(int iKodRechiv, float fErechRechiv, DateTime dShatHatchala, int iMisparSidur)
        {
            DataRow drChishuv;
            drChishuv = _dtChishuvSidur.Select("KOD_RECHIV=" + iKodRechiv + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')")[0];
            drChishuv["ERECH_RECHIV"] = fErechRechiv;
        }

        private void addRowToTable(int iKodRechiv, DateTime dShatHatchala, int iMisparSidur, float fErechRechiv, int iOutMichsa)
        {
            DataRow drChishuv;

            if (_dsChishuv.Tables["CHISHUV_SIDUR"].Select("KOD_RECHIV=" + iKodRechiv + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')").Length == 0)
            {
                if (fErechRechiv > 0)
                {
                    drChishuv = _dtChishuvSidur.NewRow();
                    drChishuv["BAKASHA_ID"] = _lBakashaId;
                    drChishuv["MISPAR_ISHI"] = _iMisparIshi;
                    drChishuv["TAARICH"] = dTaarich;
                    drChishuv["MISPAR_SIDUR"] = iMisparSidur;
                    drChishuv["SHAT_HATCHALA"] = dShatHatchala;
                    drChishuv["KOD_RECHIV"] = iKodRechiv;
                    drChishuv["ERECH_RECHIV"] = fErechRechiv;
                    drChishuv["OUT_MICHSA"] = iOutMichsa;
                    _dtChishuvSidur.Rows.Add(drChishuv);
                }
            }
            else
            {
                UpdateRowInTable(iKodRechiv, fErechRechiv, dShatHatchala, iMisparSidur);
            }
        }




        private bool CheckShabat(int iMisparSidur, int iDay, int iSugYom, DateTime dShatHatchalaSidur, DateTime dShatGmarSidur, ref float fErechChol, ref float fErechShabat, bool bCheckMafil, float ZmanHafsaka)
     {
        bool bShabat,bNextShabat;
        DateTime dShatGmarShabat, dNextDay;

        bNextShabat = false;
        bShabat = false;

        if (iDay == clGeneral.enDay.Shabat.GetHashCode())
        {bShabat = true;}
        else
        {
            if (clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, iSugYom, dTaarich))
            { bShabat = true;}
            else {bShabat = false;}
        }

        if (bShabat)
        {
            if (bCheckMafil && (_oGeneralData.objPirteyOved.iIsuk == 122 || _oGeneralData.objPirteyOved.iIsuk == 123 || _oGeneralData.objPirteyOved.iIsuk == 124 || _oGeneralData.objPirteyOved.iIsuk == 127) && iMisparSidur == 99001)
            {
                dShatGmarShabat = _oGeneralData.objParameters.dSiyumLilaMotsashMafil;
            }
            else
            {
                dShatGmarShabat = _oGeneralData.objParameters.dShatMaavarYom;
            }

           dNextDay = dTaarich.AddDays(1);
           
            if ((dNextDay.DayOfWeek.GetHashCode() + 1) == clGeneral.enDay.Shabat.GetHashCode())
            { bNextShabat = true; }
            else { bNextShabat = clDefinitions.CheckShaaton(clCalcData.DtSugeyYamimMeyuchadim, clGeneral.GetSugYom(clCalcData.DtYamimMeyuchadim, dNextDay, clCalcData.DtSugeyYamimMeyuchadim),dNextDay); } //, _oGeneralData.objMeafyeneyOved.iMeafyen56), dNextDay); }
            
            if (bNextShabat)
            { dShatGmarShabat=dShatGmarShabat.AddDays(1); }

            if (dShatGmarSidur > dShatGmarShabat)
            {
                fErechShabat = float.Parse((dShatGmarShabat - dShatHatchalaSidur).TotalMinutes.ToString());
                fErechChol = float.Parse((dShatGmarSidur - dShatGmarShabat).TotalMinutes.ToString());
            }
            else
            {
                fErechChol = 0;
                fErechShabat =float.Parse( (dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString());
            }
        }
        else
        {
            fErechChol = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString());
            fErechShabat = 0;
        }
        if (fErechChol > 0)
            fErechChol -= ZmanHafsaka;
        if (fErechShabat > 0)
            fErechShabat -= ZmanHafsaka;
        return bShabat;
    }

        private bool CheckErevShabatChag(int iSugYom, DateTime dShatHatchalaSidur, DateTime dShatGmarSidur, ref float fErechChol, ref float fErechShabat, float ZmanHafsaka)
    {
        bool bErevShabatChag;
        DateTime dShatHatchlaShabat;

        if (clCalcData.CheckErevChag(iSugYom) || clCalcData.CheckYomShishi(iSugYom))
        {
            bErevShabatChag=true;
            dShatHatchlaShabat = _oGeneralData.objParameters.dKnisatShabat;
            if (dShatHatchalaSidur < dShatHatchlaShabat && dShatGmarSidur > dShatHatchlaShabat)
            {
                fErechShabat = float.Parse((dShatGmarSidur - dShatHatchlaShabat).TotalMinutes.ToString());
                fErechChol =float.Parse( (dShatHatchlaShabat - dShatHatchalaSidur).TotalMinutes.ToString());
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
        if (fErechChol > 0)
            fErechChol -= ZmanHafsaka;
        if (fErechShabat > 0)
            fErechShabat -= ZmanHafsaka;
        return bErevShabatChag;
    }

        private void CalcGlishaLeyomShishi(DateTime dShatHatchalaSidur, DateTime dShatGmarSidur, ref float fErechChol, ref float fErechShishi, float ZmanHafsaka)
    {
        DateTime dShatHatchlaErevShabat;

        
            dShatHatchlaErevShabat = DateTime.Parse(dTaarich.AddDays(1).ToShortDateString() + " 08:00");
            if (dShatGmarSidur < dShatHatchlaErevShabat)
            {
                fErechShishi = 0;
                fErechChol = float.Parse((dShatGmarSidur - dShatHatchalaSidur).TotalMinutes.ToString());
            }
            else 
            {
                dShatHatchlaErevShabat = DateTime.Parse(dTaarich.AddDays(1).ToShortDateString() +  _oGeneralData.objParameters.GetOneParam(32,dShatHatchlaErevShabat));
                fErechShishi =float.Parse( (dShatGmarSidur - dShatHatchlaErevShabat).TotalMinutes.ToString());
                fErechChol = float.Parse((dShatHatchlaErevShabat - dShatHatchalaSidur).TotalMinutes.ToString());
            }
            if (fErechShishi > 0)
                fErechShishi -= ZmanHafsaka;
            if (fErechChol > 0)
                fErechChol -= ZmanHafsaka;
        }

       public float GetSumSidurim100()
        {
            float fDakotNochehutSidur, fErech=0;
            DataRow[] _drSidurim;
            int iMisparSidur;
            DateTime dShatHatchalaSidur;
            try
            {
                _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime') and sug_shaot_byom_hol_if_migbala=100");
                for (int I = 0; I <_drSidurim.Length ; I++)
                {
                    iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                    dShatHatchalaSidur = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                       
                    fDakotNochehutSidur = clCalcData.GetSumErechRechiv(_dtChishuvSidur.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and mispar_sidur=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchalaSidur.ToString() + "', 'System.DateTime') and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));

                    fErech += fDakotNochehutSidur;
                }

                return fErech;
            }
            catch (Exception ex)
            {
                throw ex;
            }
       }

       public string GetSidurimMeyuchRechiv(int iKodRechiv)
       {
           DataRow[] drSidurim;
           int I;
           string sSidurim = "";

           drSidurim = clCalcData.DtSidurimMeyuchRechiv.Select("kod_rechiv=" + iKodRechiv + " and me_taarich<=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')  and ad_taarich>=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
           if (drSidurim.Length > 0)
           {
               for (I = 0; I < drSidurim.Length; I++)
               {
                   sSidurim = sSidurim + drSidurim[I]["mispar_sidur"] + ",";
               }
               sSidurim = sSidurim.Substring(0,sSidurim.Length - 1);

           }

           return sSidurim;
       }

       private string GetSugeySidurRechiv(int iKodRechiv)
       {
           DataRow[] drSidurim;
           int I;
           string sSugeySidur = "";

           drSidurim = clCalcData.DtSugeySidurRechiv.Select("kod_rechiv=" + iKodRechiv + " and me_taarich<=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')  and ad_taarich>=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')");
           if (drSidurim.Length > 0)
           {
               for (I = 0; I < drSidurim.Length; I++)
               {
                   sSugeySidur = sSugeySidur + drSidurim[I]["sug_sidur"] + ",";
               }

           }
           sSugeySidur = "," + sSugeySidur;
           return sSugeySidur;
       }

       private bool CheckSidurBySectorAvoda(ref DataRow drSidur,int iMisparSidur,int iSectorAvoda)
       {
           bool bCheck= false, bYeshSidur;
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
                   //SetSugSidur(ref drSidur, dTaarich, iMisparSidur);

                  iSugSidur = int.Parse(drSidur["sug_sidur"].ToString());
                            bYeshSidur = CheckSugSidur(clGeneral.enMeafyen.SectorAvoda.GetHashCode(),iSectorAvoda, dTaarich, iSugSidur);
                            if (bYeshSidur) bCheck = true;
                      
                                   
               }
               return bCheck;

           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public float GetSumShaotShabat100()
       {
           float fDakotNochehutSidur,fMichsaYomit, fErech = 0;
           DataRow[] _drSidurim;
           int iMisparSidur,iSugYom;
           DateTime dShatSiyum,dTemp, dShatHatchala, dShatHatchalaLetashlum, dShatGmarLetashlum;
           try
           {
               iSugYom = clCalcData.iSugYom;
               if (clCalcData.CheckYomShishi(iSugYom) || clCalcData.CheckErevChag(iSugYom))
               {
                   fMichsaYomit = clCalcData.GetSumErechRechiv(_dsChishuv.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode().ToString() + " and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')"));
                        
                   if (fMichsaYomit > 0)
                   {
                       _drSidurim = clCalcData.DtYemeyAvoda.Select("Lo_letashlum=0 and mispar_sidur is not null and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime') and sug_shaot_byom_hol_if_migbala=100");
                       for (int I = 0; I < _drSidurim.Length ; I++)
                       {
                           iMisparSidur = int.Parse(_drSidurim[I]["mispar_sidur"].ToString());
                           dShatSiyum = DateTime.Parse(_drSidurim[I]["shat_siyum"].ToString());
                           dShatHatchala = DateTime.Parse(_drSidurim[I]["shat_hatchala_sidur"].ToString());
                           dShatHatchalaLetashlum = DateTime.Parse(_drSidurim[I]["shat_hatchala_letashlum"].ToString());
                           dShatGmarLetashlum = DateTime.Parse(_drSidurim[I]["shat_gmar_letashlum"].ToString());

                           if (dShatSiyum > _oGeneralData.objParameters.dKnisatShabat && dShatHatchala < _oGeneralData.objParameters.dKnisatShabat)
                           {
                               dTemp = clDefinitions.GetMinDate(dShatGmarLetashlum, _oGeneralData.objParameters.dKnisatShabat);
                               fDakotNochehutSidur = float.Parse((dTemp - dShatHatchalaLetashlum).TotalMinutes.ToString());
                               fErech += fDakotNochehutSidur;
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
       }

         public float getDakotRezifutLayla(DateTime dShatHatchalaLetashlum,DateTime dShatGmarLetashlum,DateTime Param1,DateTime Param2) 
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
                    //else if (dShatHatchalaLetashlum.Date == dTaarich && dShatGmarLetashlum.Date==dTaarich &&
                    //         dShatGmarLetashlum < Param2.AddDays(-1) && dShatHatchalaLetashlum <= Param2.AddDays(-1))
                    //{
                    //    fErech = float.Parse((dShatHatchalaLetashlum - dShatGmarLetashlum).TotalMinutes.ToString());
                    //}
                    //else if (dShatHatchalaLetashlum.Date == dTaarich && dShatGmarLetashlum.Date == dTaarich &&
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
                 param12 = _oGeneralData.objParameters.dSiyumTosefetLailaChok;
                 if (dShatHatchalaLetashlum.Date == dTaarich && dShatGmarLetashlum.Date == dTaarich &&
                         dShatGmarLetashlum < param12.AddDays(-1) && dShatHatchalaLetashlum <= param12.AddDays(-1))
                 {
                     fErech = float.Parse((dShatHatchalaLetashlum - dShatGmarLetashlum).TotalMinutes.ToString());
                 }
                 else if (dShatHatchalaLetashlum.Date == dTaarich && dShatGmarLetashlum.Date == dTaarich &&
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
             DataRow[] drSugSidur,drSidurim;
             string sSql = "";
             try
             {
                 //sSql="sug_sidur=69 and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime')";
                 //drSugSidur = clCalcData.DtSugeySidur.Select(sSql);
                 sSql = "sug_sidur=69 and taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime') and lo_letashlum=1";// and mispar_sidur=" + drSugSidur[0]["mispar_sidur"];
                 drSidurim = clCalcData.DtYemeyAvoda.Select(sSql);
                // if (drSugSidur.Length > 0)
                 if (drSidurim.Length > 0)
                 {
                     //sSql = "taarich=Convert('" + dTaarich.ToShortDateString() + "', 'System.DateTime') and lo_letashlum=1 and mispar_sidur=" + drSugSidur[0]["mispar_sidur"];
                     //drSidurim = clCalcData.DtYemeyAvoda.Select(sSql);
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
         }

         private bool cheakSidurGrirainConenutGrira(DateTime dShatHatchalaGrira, DateTime dShatGmarGrira, DataRow drConenutGrira)
         {
             DateTime dShatHatchalaConenutGrira, dShatGmarConenutGrira;
             try
             {
                 dShatHatchalaConenutGrira =DateTime.Parse(drConenutGrira["shat_hatchala_letashlum"].ToString());
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
    }
 }

