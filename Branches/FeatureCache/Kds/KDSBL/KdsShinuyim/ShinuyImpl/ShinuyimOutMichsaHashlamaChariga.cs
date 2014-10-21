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
    public class ShinuyimOutMichsaHashlamaChariga : ShinuyBase
    {

        public ShinuyimOutMichsaHashlamaChariga(IUnityContainer container)
            : base(container)
        {

        }
        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.ShinuyimOutMichsaHashlamaChariga; } }

        public override void ExecShinuy(ShinuyInputData inputData)
        {
            try
            {
                for (int i = 0; i < inputData.htEmployeeDetails.Count; i++)
                {
                    SidurDM curSidur = (SidurDM)inputData.htEmployeeDetails[i];
                    OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd = GetUpdSidurObject(curSidur, inputData);

                    //מחוץ למכסה
                    if (!CheckIdkunRashemet("OUT_MICHSA", curSidur.iMisparSidur, curSidur.dFullShatHatchala, inputData))
                        UpdateOutMichsa(inputData,curSidur, oObjSidurimOvdimUpd);

                    //חריגה
                    if (!CheckIdkunRashemet("CHARIGA", curSidur.iMisparSidur, curSidur.dFullShatHatchala, inputData)) //!CheckApproval("2,4,5,6,10", oSidur.iMisparSidur, oSidur.dFullShatHatchala) &&
                        UpdateChariga(curSidur, oObjSidurimOvdimUpd);

                    //השלמה
                    if (!CheckIdkunRashemet("HASHLAMA", curSidur.iMisparSidur, curSidur.dFullShatHatchala, inputData))
                        UpdateHashlamaForSidur(curSidur, i, oObjSidurimOvdimUpd, inputData);

                }
            }
            catch (Exception ex)
            {
                throw new Exception("ShinuyimOutMichsaHashlamaChariga: " + ex.Message);
            }
        }

        private void UpdateOutMichsa(ShinuyInputData inputData,SidurDM curSidur, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
              DataRow[] drSugSidur;
            //שינוי ברמת סידור
            //עדכון שדה מחוץ למכסה
            try
            {

                if (inputData.OvedDetails.iKodHevra == enEmployeeType.enEggedTaavora.GetHashCode())
                {
                    oObjSidurimOvdimUpd.OUT_MICHSA = 0;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                    //oObjSidurimOvdimUpd.bUpdate = true;
                    curSidur.sOutMichsa = "0";
                }
                else
                {
                    if (oObjSidurimOvdimUpd.LO_LETASHLUM == 0 || (oObjSidurimOvdimUpd.LO_LETASHLUM == 1 && oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM == 1))
                    {
                        if (!curSidur.bSidurMyuhad)
                        {
                            //  drSugSidur = clDefinitions.GetOneSugSidurMeafyen(curSidur.iSugSidurRagil, curSidur.dSidurDate, _dtSugSidur);
                            drSugSidur = _container.Resolve<ISidurManager>().GetOneSugSidurMeafyen(curSidur.iSugSidurRagil, inputData.CardDate);
                            if (drSugSidur.Length > 0)
                            {
                                curSidur.sZakayMichutzLamichsa = drSugSidur[0]["zakay_michutz_lamichsa"].ToString();
                            }
                        }

                        if ((curSidur.sZakayMichutzLamichsa == enMeafyenSidur25.enZakaiAutomat.GetHashCode().ToString()))
                        {   //אם סידור הוא סידור מיוחד/מפה ויש לו ערך 3 במאפיין 25 (זכאי אוטומטית "מחוץ למכסה")
                            //וגם הוא לא מאגד תעבורה.                                               
                            oObjSidurimOvdimUpd.OUT_MICHSA = 1;
                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                            curSidur.sOutMichsa = "1";
                        }
                        else
                        {
                            oObjSidurimOvdimUpd.OUT_MICHSA = 0;
                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                            curSidur.sOutMichsa = "0";
                        }
                    }
                    else
                    {
                        oObjSidurimOvdimUpd.OUT_MICHSA = 0;
                        oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                        curSidur.sOutMichsa = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateChariga(SidurDM curSidur, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            //עדכון שדה חריגה ברמת סידור
            //חריגה משעת התחלה/גמר	שינוי נתוני קלט:
            //1. הסידור מזכה אוטומטית (ערך 3 במאפיין 35) 
            try
            {
                if ((oObjSidurimOvdimUpd.LO_LETASHLUM == 0 || (oObjSidurimOvdimUpd.LO_LETASHLUM == 1 && oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM == 1)) && (curSidur.sZakaiLeChariga == enMeafyenSidur35.enCharigaAutomat.GetHashCode().ToString()))
                {
                    oObjSidurimOvdimUpd.CHARIGA = 3;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                    curSidur.sChariga = "3";

                }
            }
            catch (Exception ex)
            {
                throw ex;
                
            }
        }

        private void UpdateHashlamaForSidur(SidurDM curSidur, int iSidurIndex, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, ShinuyInputData inputData)
        {
            //עדכון שדה השלמה ברמת סידור
            //עדכון שדה השלמה
            //int iParamHashlama;
            //double dZmanSidur, dZmanHashlama;
            //bool bValidHashlama = false;
            //DataRow[] drSugSidur;
            try
            {
                //float fSidurTime = 0;
                //0	אין השלמה	שינוי נתוני קלט:
                //1. ברירת מחדל אלא אם כן מתקיימים תנאים לעדכון ערך אחר.
                //2. עבור אגד תעבורה, תמיד לא מקבלים השלמה (מזהים אגד תעבורה לפי קוד חברה של העובד - 4895 ).
                if (curSidur.sHashlama != "0")
                {
                    oObjSidurimOvdimUpd.HASHLAMA = 0;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                    curSidur.sHashlama = "0";
                }

                //if (inputData.OvedDetails.iKodHevra != enEmployeeType.enEggedTaavora.GetHashCode())
                //{
                //    //התנאים לקבלת ערך 1 - השלמה לשעה                          
                //    //כל הסידורים מסומנים ללא ערך בשדה השלמה (השלמה כלשהיא, לא משנה איזה ערך), אם קיימת -  עוצרים.
                //    if (!inputData.htEmployeeDetails.Values.Cast<SidurDM>().ToList().Any(sidur => sidur.sHashlama != "0" && !string.IsNullOrEmpty(sidur.sHashlama)))
                //    {
                //        //מחפשים את הסידור הראשון ביום שהמשך שלו קטן מהערך בפרמטרים 101 - 103.
                //        //הסידור (101 (זמן מינימום לסידור חול להשלמה, 102 - זמן מינימום לסידור שישי/ע.ח להשלמה, 103 - זמן מינימום לסידור שבת/שבתון
                //        if (DateHelper.CheckShaaton(inputData.iSugYom, curSidur.dSidurDate, inputData.SugeyYamimMeyuchadim))
                //        { iParamHashlama = inputData.oParam.iHashlamaShabat; }
                //        else if ((curSidur.sErevShishiChag == "1") || (curSidur.sSidurDay == enDay.Shishi.GetHashCode().ToString()))
                //        { iParamHashlama = inputData.oParam.iHashlamaShisi; }
                //        else { iParamHashlama = inputData.oParam.iHashlamaYomRagil; }

                //        dZmanSidur = curSidur.dFullShatGmar.Subtract(curSidur.dFullShatHatchala).TotalMinutes;
                //        //אם משך הסידור או הערך בפרמטר המתאים ליום העבודה שווה או גדול משעה - עוצרים.
                //        if (dZmanSidur < iParamHashlama && iParamHashlama <= 60 && dZmanSidur <= 60)
                //        {

                //            //. לסידור מאפיין 40 (לפי מספר סידור או סוג סידור)עם ערך 2 (זכאי אוטומטי) והסידור אינו מבוטל
                //            if (curSidur.bSidurMyuhad && curSidur.sHashlamaKod == "2")
                //            { bValidHashlama = true; }
                //            else //if (oSidur.bSidurRagilExists)
                //            {
                //                drSugSidur = _container.Resolve<ISidurManager>().GetOneSugSidurMeafyen(curSidur.iSugSidurRagil, curSidur.dSidurDate);
                //                if (drSugSidur[0]["zakay_lehashlama_avur_sidur"].ToString() == "2")
                //                {
                //                    bValidHashlama = true;
                //                }
                //            }

                //            //אם הסידור אינו אחרון או בודד ביום
                //            if (inputData.htEmployeeDetails.Values.Count > 1 && iSidurIndex < inputData.htEmployeeDetails.Values.Count - 1)
                //            {
                //                //יש לבדוק האם שעת ההתחלה של הסידור העוקב שווה לשעת הגמר של הסידור שלנו +משך ההשלמה. אם כן - עוצרים.
                //                dZmanHashlama = double.Parse("60") - double.Parse(dZmanSidur.ToString());
                //                if (curSidur.dFullShatGmar.AddMinutes(dZmanHashlama) > ((SidurDM)inputData.htEmployeeDetails[iSidurIndex + 1]).dFullShatHatchala)
                //                {
                //                    bValidHashlama = false;
                //                }
                //            }

                //            if (bValidHashlama)
                //            {
                //                oObjSidurimOvdimUpd.HASHLAMA = 1;
                //                oObjSidurimOvdimUpd.SUG_HASHLAMA = 1;
                //                oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;

                //                curSidur.sHashlama = "1";
                //                curSidur.iSugHashlama = 1;
                //            }
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}