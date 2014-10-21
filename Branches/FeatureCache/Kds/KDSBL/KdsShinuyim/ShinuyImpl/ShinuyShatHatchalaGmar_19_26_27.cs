using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.Enums;
using KDSCommon.Helpers;
using KDSCommon.Interfaces.Managers;
using KDSCommon.UDT;
using KdsShinuyim.Enums;
using Microsoft.Practices.Unity;

namespace KdsShinuyim.ShinuyImpl
{
    public class ShinuyShatHatchalaGmar_19_26_27 : ShinuyBase
    {

        public ShinuyShatHatchalaGmar_19_26_27(IUnityContainer container)
            : base(container)
        {

        }
        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.ShinuyShatHatchalaGmar_19_26_27; } }

        public override void ExecShinuy(ShinuyInputData inputData)
        {
            bool bIdkunRashShatHatchala;
            bool bIdkunRashShatGmar;
            try
            {
                for (int i = 0; i < inputData.htEmployeeDetails.Count; i++)
                {
                    SidurDM curSidur = (SidurDM)inputData.htEmployeeDetails[i];
                    OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd = GetUpdSidurObject(curSidur, inputData);

                    bIdkunRashShatHatchala = false;
                    bIdkunRashShatGmar = false;

                    if (CheckIdkunRashemet("SHAT_HATCHALA_LETASHLUM", curSidur.iMisparSidur, curSidur.dFullShatHatchala, inputData))
                    { bIdkunRashShatHatchala = true; }
                    if (CheckIdkunRashemet("SHAT_GMAR_LETASHLUM", curSidur.iMisparSidur, curSidur.dFullShatHatchala, inputData))
                    { bIdkunRashShatGmar = true; }

                    //שינוי 19
                    SetHourToSidur19(curSidur, oObjSidurimOvdimUpd, bIdkunRashShatHatchala, bIdkunRashShatGmar, inputData);

                    //שינוי 26
                    FixedShatHatchalaAndGmarToMafilim26(curSidur, oObjSidurimOvdimUpd, bIdkunRashShatHatchala, bIdkunRashShatGmar, inputData);

                    //שינוי 27
                    FixedShatHatchalaAndGmarSidurMapa27(curSidur, oObjSidurimOvdimUpd);

                }
            }
             
            catch (Exception ex)
            {
                throw new Exception("ShinuyShatHatchalaGmar_19_26_27: " + ex.Message);
            }
        }

        private void SetHourToSidur19(SidurDM curSidur, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, bool bIdkunRashShatHatchala, bool bIdkunRashShatGmar, ShinuyInputData inputData)
        {
            DateTime dShatHatchalaLetashlumToUpd;
            DateTime dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR;
            DateTime dShatHatchalaLetashlum = DateTime.MinValue;
            DateTime dShatGmarLetashlum = DateTime.MinValue;
            bool bFromMeafyenHatchala, bFromMeafyenGmar;//,bLoLeadken=false;
            //קביעת שעות לסידורים שזמן ההתחלה/גמר מותנה במאפיין אישי
            try
            {
                if (oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA.ToShortDateString() != DateTime.MinValue.ToShortDateString())
                    dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA;
                else if (oObjSidurimOvdimUpd.SHAT_HATCHALA.ToShortDateString() != DateTime.MinValue.ToShortDateString())
                    dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;
                else dShatHatchalaLetashlumToUpd = DateTime.MinValue;

                //אתחול שעת התחלה וגמר לפי מאפיינים וסוגי ימים
                GetOvedShatHatchalaGmar(oObjSidurimOvdimUpd.SHAT_GMAR, inputData.oMeafyeneyOved, curSidur, inputData, ref dShatHatchalaLetashlum, ref dShatGmarLetashlum, out bFromMeafyenHatchala, out  bFromMeafyenGmar);
                //סידור עם מאפיין 78 - קיזוז
                SetShatHatchalaGmarKizuz(curSidur, oObjSidurimOvdimUpd, dShatHatchalaLetashlum, dShatGmarLetashlum, inputData, ref dShatHatchalaLetashlumToUpd, ref dShatGmarLetashlumToUpd);

                //כל סידור מיוחד שלא עודכנו לו שעות התחלה/גמר לתשלום באחד הסעיפים הקודמים, יש לעדכן לו:
                //שעת התחלה לתשלום לשעת התחלה סידור
                //שעת גמר לתשלום לשעת גמר סידור
                if (dShatHatchalaLetashlumToUpd == DateTime.MinValue)
                {
                    if (curSidur.dFullShatHatchala.ToShortDateString() == DateTime.MinValue.ToShortDateString())
                    {
                        dShatHatchalaLetashlumToUpd = DateTime.MinValue;
                        oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = DateTime.MinValue;
                    }
                    else dShatHatchalaLetashlumToUpd = curSidur.dFullShatHatchala;
                }

                if (dShatGmarLetashlumToUpd == DateTime.MinValue)
                    dShatGmarLetashlumToUpd = curSidur.dFullShatGmar;

                ChangeShaot(curSidur, oObjSidurimOvdimUpd, bIdkunRashShatHatchala, bIdkunRashShatGmar, dShatHatchalaLetashlumToUpd, dShatGmarLetashlumToUpd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ChangeShaot(SidurDM curSidur, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, bool bIdkunRashShatHatchala, bool bIdkunRashShatGmar, DateTime dShatHatchalaLetashlumToUpd, DateTime dShatGmarLetashlumToUpd)
        {

            try{
                if (curSidur.dShatHatchalaMenahelMusach != DateTime.MinValue)
                {
                    oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = curSidur.dShatHatchalaMenahelMusach;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                    curSidur.dFullShatHatchalaLetashlum = oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM;
                    curSidur.sShatHatchalaLetashlum = curSidur.dFullShatHatchalaLetashlum.ToString("HH:mm");
                }
                else if (!bIdkunRashShatHatchala && dShatHatchalaLetashlumToUpd != oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM)// && !bLoLeadken)
                {
                    oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = dShatHatchalaLetashlumToUpd;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                    curSidur.dFullShatHatchalaLetashlum = oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM;
                    curSidur.sShatHatchalaLetashlum = curSidur.dFullShatHatchalaLetashlum.ToString("HH:mm");
                    if (curSidur.dFullShatHatchalaLetashlum.Year < DateHelper.cYearNull)
                        curSidur.sShatHatchalaLetashlum = "";
                }

                if (curSidur.dShatGmarMenahelMusach != DateTime.MinValue)
                {
                    oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = curSidur.dShatGmarMenahelMusach;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                    curSidur.dFullShatGmarLetashlum = oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM;
                    curSidur.sShatGmarLetashlum = curSidur.dFullShatGmarLetashlum.ToString("HH:mm");
                }
                else if (!bIdkunRashShatGmar && dShatGmarLetashlumToUpd != oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM)// && !bLoLeadken)
                {
                    oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = dShatGmarLetashlumToUpd;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                    curSidur.dFullShatGmarLetashlum = oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM;
                    curSidur.sShatGmarLetashlum = curSidur.dFullShatGmarLetashlum.ToString("HH:mm");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetShatHatchalaGmarKizuz(SidurDM curSidur, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, DateTime dShatHatchalaLetashlum, DateTime dShatGmarLetashlum, ShinuyInputData inputData, ref DateTime dShatHatchalaLetashlumToUpd, ref DateTime dShatGmarLetashlumToUpd)
        {
            //5. סידור מסומן במאפיין 78
            //אם הסידור מסומן במאפיין 78 (קיזוז התחלה / גמר)
            //ואינו מסומן "לא לתשלום", ישנם כמה מקרים:
            bool flag = false;
            DateTime dShatHatchalaMeafyen, dShatGmarMeafyen;
            try
            {
                if (curSidur.bKizuzAlPiHatchalaGmarExists && oObjSidurimOvdimUpd.LO_LETASHLUM == 0)//|| (oObjSidurimOvdimUpd.LO_LETASHLUM == 1 && oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM == 1)))
                {
                    //1 .אם אין סימון "קוד חריגה" (אין = null או 0) ואין סימון "מחוץ למיכסה" (אין = null או 0), שלושה מקרים:                                                                               
                    if (oObjSidurimOvdimUpd.CHARIGA == 0)
                    {
                        SetShaotHatchalaGmar_2(dShatHatchalaLetashlum, dShatGmarLetashlum, oObjSidurimOvdimUpd, ref dShatHatchalaLetashlumToUpd, ref dShatGmarLetashlumToUpd);
                        //אם חריגה=0 וגם עובד עם מאפייני משמרת שניה
                        SetShaotLovedMishmeret2(curSidur, oObjSidurimOvdimUpd, inputData, ref dShatHatchalaLetashlumToUpd, ref dShatGmarLetashlumToUpd, out flag);

                    }
                    else //chariga>0
                    {
                        SetShaotLovedMishmeret2(curSidur, oObjSidurimOvdimUpd, inputData, ref dShatHatchalaLetashlumToUpd, ref dShatGmarLetashlumToUpd, out flag);
                        dShatHatchalaMeafyen = flag ? dShatHatchalaLetashlumToUpd : dShatHatchalaLetashlum;
                        dShatGmarMeafyen = flag ? dShatGmarLetashlumToUpd : dShatGmarLetashlum;
                        //3. אם יש סימון "קוד חריגה" ולא קבענו את הסידור "לא לתשלום", שלושה מקרים:                                                           
                        if ((oObjSidurimOvdimUpd.CHARIGA != 0) && (oObjSidurimOvdimUpd.LO_LETASHLUM == 0))
                        {
                            //ראשית יש לבדוק האם החריגה אושרה במנגנון האישורים, רק אם כן יש לבצע את השינוי.
                            //if (CheckApprovalStatus("2,211,4,5,511,6,10,1011", curSidur.iMisparSidur, curSidur.dFullShatHatchala) == 1)
                            //{
                            //א. אם מסומן קוד חריגה "1"  (חריגה משעת התחלה) אזי שעת התחלה לתשלום = שעת תחילת הסידור.
                            //אם שעת הגמר של הסידור גדולה משעת מאפיין הגמר המותר (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים): שעת גמר לתשלום = השעה המוגדרת במאפיין. 

                            if (oObjSidurimOvdimUpd.CHARIGA == 1)
                            {
                                dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;
                                if (oObjSidurimOvdimUpd.SHAT_GMAR > dShatGmarMeafyen)
                                    dShatGmarLetashlumToUpd = dShatGmarMeafyen;
                                else
                                    dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR;
                            }
                            //ב. אם מסומן קוד חריגה "2"  (חריגה משעת גמר) אזי שעת גמר לתשלום = שעת גמר הסידור.
                            //אם שעת התחלה של הסידור קטנה משעת מאפיין התחלה המותרת (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים) : שעת התחלה לתשלום = השעה המוגדרת במאפיין. 
                            if (oObjSidurimOvdimUpd.CHARIGA == 2)
                            {
                                dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR;
                                if (curSidur.dFullShatHatchala < dShatHatchalaMeafyen)
                                    dShatHatchalaLetashlumToUpd = dShatHatchalaMeafyen;
                                else
                                    dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;
                            }
                            //ג. אם מסומן קוד חריגה "3"  (חריגה משעת התחלה וגמר) אזי :
                            //שעת התחלה לתשלום = שעת תחילת הסידור.
                            //שעת גמר לתשלום = שעת גמר הסידור
                            if (oObjSidurimOvdimUpd.CHARIGA == 3)
                            {
                                dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;
                                dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetShaotLovedMishmeret2(SidurDM oSidur, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, ShinuyInputData inputData, ref DateTime dShatHatchalaLetashlumToUpd, ref DateTime dShatGmarLetashlumToUpd, out bool bflag)
        {
            DateTime shaa24, shaa23, shaa;
            bflag = false;
            try
            {
                shaa = DateTime.Parse(oObjSidurimOvdimUpd.SHAT_GMAR.ToShortDateString() + " 18:00:00");
                if (!inputData.oMeafyeneyOved.IsMeafyenExist(42) && inputData.oMeafyeneyOved.IsMeafyenExist(23) && inputData.oMeafyeneyOved.IsMeafyenExist(24))
                    if (oSidur.bKizuzAlPiHatchalaGmarExists)
                        if ((oObjSidurimOvdimUpd.SHAT_HATCHALA.Hour >= 11 && oObjSidurimOvdimUpd.SHAT_HATCHALA.Hour <= 17 && oObjSidurimOvdimUpd.SHAT_GMAR > shaa)
                                && (oSidur.sShabaton != "1" && inputData.iSugYom >= enSugYom.Chol.GetHashCode() && inputData.iSugYom < enSugYom.Shishi.GetHashCode())) //|| ((iSugYom == enSugYom.Shishi.GetHashCode()  && oObjSidurimOvdimUpd.SHAT_GMAR > shaa.AddHours(-5)))))
                        {
                            shaa23 = DateTime.Parse(oObjSidurimOvdimUpd.SHAT_HATCHALA.ToShortDateString() + " " + inputData.oMeafyeneyOved.GetMeafyen(23).Value.Substring(0, 2) + ":" + inputData.oMeafyeneyOved.GetMeafyen(23).Value.Substring(2, 2));
                            shaa24 = DateTime.Parse(oObjSidurimOvdimUpd.SHAT_GMAR.ToShortDateString() + " " + inputData.oMeafyeneyOved.GetMeafyen(24).Value.Substring(0, 2) + ":" + inputData.oMeafyeneyOved.GetMeafyen(24).Value.Substring(2, 2));
                            dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA > shaa23 ? oObjSidurimOvdimUpd.SHAT_HATCHALA : shaa23;
                            dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR < shaa24 ? oObjSidurimOvdimUpd.SHAT_GMAR : shaa24;
                            bflag = true;
                        }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetShaotHatchalaGmar_2(DateTime dShatHatchalaLetashlum, DateTime dShatGmarLetashlum, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, ref DateTime dShatHatchalaLetashlumToUpd, ref DateTime dShatGmarLetashlumToUpd)
        {
            //א. אם שעת הגמר של הסידור לא גדולה משעת מאפיין ההתחלה המותרת (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים): יש לסמן את הסידור "לא לתשלום"
            //ב. אם שעת ההתחלה של הסידור לא קטנה משעת מאפיין הגמר המותרת (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים) - יש לסמן את הסידור "לא לתשלום                            
            try
            {
                if ((oObjSidurimOvdimUpd.SHAT_GMAR != DateTime.MinValue && (oObjSidurimOvdimUpd.SHAT_GMAR <= dShatHatchalaLetashlum)) || (oObjSidurimOvdimUpd.SHAT_HATCHALA >= dShatGmarLetashlum))
                {
                    if (dShatHatchalaLetashlum.Year > DateHelper.cYearNull)
                    {
                        dShatHatchalaLetashlumToUpd = dShatHatchalaLetashlum;
                    }
                    if (dShatGmarLetashlum.Year > DateHelper.cYearNull)
                    {
                        dShatGmarLetashlumToUpd = dShatGmarLetashlum;
                    }
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                    //oSidur.iLoLetashlum = 1;
                }
                else
                {
                    if (oObjSidurimOvdimUpd.SHAT_HATCHALA.ToShortDateString() != DateTime.MinValue.ToShortDateString() && oObjSidurimOvdimUpd.SHAT_HATCHALA < dShatHatchalaLetashlum)
                    {
                        //ג. אם שעת ההתחלה של הסידור קטנה משעת מאפיין ההתחלה המותרת (תלוי בסוג היום, ראה עמודה שדות מעורבים): שעת התחלה לתשלום = השעה המוגדרת במאפיין. 
                        dShatHatchalaLetashlumToUpd = dShatHatchalaLetashlum;
                    }
                    else if (oObjSidurimOvdimUpd.SHAT_HATCHALA.ToShortDateString() != DateTime.MinValue.ToShortDateString())
                    {   //ד. אם שעת ההתחלה של הסידור אינה קטנה משעת מאפיין ההתחלה המותרת (תלוי בסוג היום, ראה עמודה שדות מעורבים): שעת התחלה לתשלום = שעת התחלת הסידור. 
                        dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;
                    }

                    if (oObjSidurimOvdimUpd.SHAT_GMAR > dShatGmarLetashlum)
                    {
                        dShatGmarLetashlumToUpd = dShatGmarLetashlum;
                    }
                    else
                    {
                        //ה. אם שעת הגמר של הסידור אינה גדולה משעת מאפיין הגמר המותר (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים) : שעת גמר לתשלום = שעת גמר הסידור
                        dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FixedShatHatchalaAndGmarToMafilim26(SidurDM oSidur, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, bool bIdkunRashShatHatchala, bool bIdkunRashShatGmar, ShinuyInputData inputData)
        {
            DateTime dRequiredShatHatchala = DateTime.MinValue;
            DateTime dRequiredShatGmar = DateTime.MinValue;
            DateTime dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM;
            DateTime dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM;
            bool bFromMeafyenHatchala, bFromMeafyenGmar;
            try
            {
                if ((inputData.OvedDetails.iIsuk == 122 || inputData.OvedDetails.iIsuk == 123 || inputData.OvedDetails.iIsuk == 124 || inputData.OvedDetails.iIsuk == 127)
                    && oSidur.sSugAvoda == enSugAvoda.Shaon.GetHashCode().ToString())
                {

                    if (oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA.ToShortDateString() != DateTime.MinValue.ToShortDateString())
                        dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA;
                    else if (oObjSidurimOvdimUpd.SHAT_HATCHALA.ToShortDateString() != DateTime.MinValue.ToShortDateString())
                        dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;
                    else dShatHatchalaLetashlumToUpd = DateTime.MinValue;

                    GetMeafyeneyMafilim(oSidur.sShabaton, oSidur.dFullShatHatchala, oSidur.dFullShatGmar,inputData, out  bFromMeafyenHatchala, out  bFromMeafyenGmar, ref dRequiredShatHatchala, ref dRequiredShatGmar);

                    SetShatHatchalaGmarKizuz(oSidur, oObjSidurimOvdimUpd, dRequiredShatHatchala, dRequiredShatGmar, inputData, ref dShatHatchalaLetashlumToUpd, ref dShatGmarLetashlumToUpd);

                    if (dShatHatchalaLetashlumToUpd == DateTime.MinValue)
                    {
                        if (oSidur.dFullShatHatchala.ToShortDateString() == DateTime.MinValue.ToShortDateString())
                        {
                            dShatHatchalaLetashlumToUpd = DateTime.MinValue;
                            oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = DateTime.MinValue;
                        }
                        else dShatHatchalaLetashlumToUpd = oSidur.dFullShatHatchala;
                    }

                    if (dShatGmarLetashlumToUpd == DateTime.MinValue)
                        dShatGmarLetashlumToUpd = oSidur.dFullShatGmar;

                    if (!bIdkunRashShatHatchala && dShatHatchalaLetashlumToUpd != oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM)
                    {
                        oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = dShatHatchalaLetashlumToUpd;
                        oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                        oSidur.dFullShatHatchalaLetashlum = oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM;
                        oSidur.sShatHatchalaLetashlum = oSidur.dFullShatHatchalaLetashlum.ToString("HH:mm");
                        if (oSidur.dFullShatHatchalaLetashlum.Year < DateHelper.cYearNull)
                            oSidur.sShatHatchalaLetashlum = "";
                    }

                    if (!bIdkunRashShatGmar && dShatGmarLetashlumToUpd != oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM)
                    {
                        oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = dShatGmarLetashlumToUpd;
                        oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                        oSidur.dFullShatGmarLetashlum = oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM;
                        oSidur.sShatGmarLetashlum = oSidur.dFullShatGmarLetashlum.ToString("HH:mm");
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FixedShatHatchalaAndGmarSidurMapa27(SidurDM curSidur, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            string sug_sidur = "";
            try
            {
                DataRow[] drSugSidur = _container.Resolve<ISidurManager>().GetOneSugSidurMeafyen(curSidur.iSugSidurRagil, curSidur.dSidurDate);
                if (drSugSidur.Length > 0)
                    sug_sidur = drSugSidur[0]["sug_sidur"].ToString();

                if (!curSidur.bSidurMyuhad && sug_sidur != "69")
                {
                    oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = curSidur.dFullShatHatchala;
                    oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = curSidur.dFullShatGmar;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;

                    curSidur.dFullShatHatchalaLetashlum = curSidur.dFullShatHatchala;
                    curSidur.sShatHatchalaLetashlum = curSidur.dFullShatHatchalaLetashlum.ToString("HH:mm");
                    curSidur.dFullShatGmarLetashlum = curSidur.dFullShatGmar;
                    curSidur.sShatGmarLetashlum = curSidur.dFullShatGmarLetashlum.ToString("HH:mm");
                    if (curSidur.dFullShatHatchalaLetashlum.Year < DateHelper.cYearNull)
                        curSidur.sShatHatchalaLetashlum = "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
