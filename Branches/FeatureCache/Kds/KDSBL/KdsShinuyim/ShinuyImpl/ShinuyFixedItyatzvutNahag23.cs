using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.Enums;
using KDSCommon.Interfaces.DAL;
using KDSCommon.Interfaces.Managers;
using KDSCommon.UDT;
using KdsShinuyim.Enums;
using Microsoft.Practices.Unity;

namespace KdsShinuyim.ShinuyImpl
{
    public class ShinuyFixedItyatzvutNahag23: ShinuyBase
    {

        public ShinuyFixedItyatzvutNahag23(IUnityContainer container)
            : base(container)
        {

        }

        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.ShinuyFixedItyatzvutNahag23 ; } }

        public override void ExecShinuy(ShinuyInputData inputData)
        {
            bool bFirstHayavHityazvut = false;
            bool bSecondHayavHityazvut = false;
            SidurDM oSidurNidrashHityatvut = null;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
            try
            {
                for (int i = 0; i < inputData.htEmployeeDetails.Count; i++)
                {
                    SidurDM curSidur = (SidurDM)inputData.htEmployeeDetails[i];
                    oObjSidurimOvdimUpd = GetUpdSidurObject(curSidur, inputData);
                   // oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);
                    if (!(CheckIdkunRashemet("KOD_SIBA_LEDIVUCH_YADANI_IN", curSidur.iMisparSidur, curSidur.dFullShatHatchala, inputData) && (curSidur.iNidreshetHitiatzvut == 1 || curSidur.iNidreshetHitiatzvut == 2)))
                    {
                        FixedItyatzvutNahag23(i, curSidur, oObjSidurimOvdimUpd,inputData, ref oSidurNidrashHityatvut, ref bFirstHayavHityazvut, ref bSecondHayavHityazvut);
                    }
                    else
                    {
                        bool bHayavHityazvut=false; 
                        CheckHovatHityazvut(curSidur, oObjSidurimOvdimUpd, 1, true, inputData.CardDate, ref bHayavHityazvut);
                        if (bHayavHityazvut)
                            if (!bFirstHayavHityazvut)
                                bFirstHayavHityazvut = true;
                            else if (!bSecondHayavHityazvut)
                                bSecondHayavHityazvut = true;
                    }
                }
            }
            catch (Exception ex)
            {
                inputData.IsSuccsess = false;
            }
        }
        //?? מלא פרמטרים
        private void FixedItyatzvutNahag23(int iIndexSidur, SidurDM curSidur, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, ShinuyInputData inputData, ref SidurDM oSidurNidrashHityatvut, ref bool bFirstHayavHityazvut, ref bool bSecondHayavHityazvut)
        {
            bool bNidrashHityazvut = false;
            bool bHayavHityazvut = false;
          
            try
            {
                
                //. איפוס שדות לפני השינוי
                IpusFields(curSidur, oObjSidurimOvdimUpd);

                //התייצבות ראשונה
                if (!bFirstHayavHityazvut)
                { // מחפשים סידור שמצריך התייצבות )

                    bNidrashHityazvut = CheckHovatHityazvut(curSidur, oObjSidurimOvdimUpd, 1, false, inputData.CardDate,ref bHayavHityazvut);
                    if (bHayavHityazvut) bFirstHayavHityazvut = true;

                    if (bNidrashHityazvut)
                    {
                        oObjSidurimOvdimUpd.NIDRESHET_HITIATZVUT = 1;
                        oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;

                        CheckHityazvut(curSidur, oObjSidurimOvdimUpd, inputData);
                    }
                }
                else if (iIndexSidur > 0 && !bSecondHayavHityazvut)
                {//התייצבות שניה

                    // עדכון שדה התייצבות שניה - עוברים לפי השלבים הבאים:
                    //א. מחפשים סידור שמצריך התייצבות שניה  (סידור מפה) - עוברים על הסידורים, במידה ונתקלים בפער של יותר מהערך בפרמטר 91 (פער בין סידורי נהגות המחייב התייצבות חוזרת) 
                    //בין שעת הגמר של סידור (מפה או מיוחד) לבין שעת ההתחלה של סידור מפה הבא אחריו, פער זמנים כזה מחייב התייצבות נוספת בסידור שאחרי הפער.
                    SidurDM oSidurPrev = (SidurDM)inputData.htEmployeeDetails[iIndexSidur - 1];
                    if ((curSidur.dFullShatHatchala - oSidurPrev.dFullShatGmar).TotalMinutes > inputData.oParam.iPaarBeinSidurimMechayevHityazvut)
                    {
                        oSidurNidrashHityatvut = curSidur;
                    }

                    if (oSidurNidrashHityatvut != null)
                    {
                        bNidrashHityazvut = CheckHovatHityazvut(curSidur, oObjSidurimOvdimUpd, 2,  false, inputData.CardDate,ref bHayavHityazvut);
                        if (bNidrashHityazvut)
                        {
                            bSecondHayavHityazvut = true;

                            oObjSidurimOvdimUpd.NIDRESHET_HITIATZVUT = 2;
                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                            CheckHityazvut(curSidur,  oObjSidurimOvdimUpd,inputData);
                            oSidurNidrashHityatvut = null;
                        }
                        else if (bHayavHityazvut) { bSecondHayavHityazvut = true; }
                    }
                }

                curSidur.iPtorMehitiatzvut = oObjSidurimOvdimUpd.PTOR_MEHITIATZVUT;
                curSidur.iNidreshetHitiatzvut = oObjSidurimOvdimUpd.NIDRESHET_HITIATZVUT;
                curSidur.dShatHitiatzvut = oObjSidurimOvdimUpd.SHAT_HITIATZVUT;

                if (!string.IsNullOrEmpty(oObjSidurimOvdimUpd.HACHTAMA_BEATAR_LO_TAKIN))
                {
                    curSidur.iHachtamaBeatarLoTakin = int.Parse(oObjSidurimOvdimUpd.HACHTAMA_BEATAR_LO_TAKIN);
                }
            }
            catch (Exception ex)
            {
                //clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 23, oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "FixedItyatzvutNahag23: " + ex.Message, null);
                throw ex;
            }
        }

        private void IpusFields(SidurDM curSidur, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            if (!oObjSidurimOvdimUpd.PTOR_MEHITIATZVUTIsNull)
            {
                oObjSidurimOvdimUpd.PTOR_MEHITIATZVUT = 0;
                oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
            }

            if (!oObjSidurimOvdimUpd.NIDRESHET_HITIATZVUTIsNull)
            {
                oObjSidurimOvdimUpd.NIDRESHET_HITIATZVUT = 0;
                oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
            }

            if (!oObjSidurimOvdimUpd.SHAT_HITIATZVUTIsNull)
            {
                oObjSidurimOvdimUpd.SHAT_HITIATZVUT = DateTime.MinValue;
                oObjSidurimOvdimUpd.SHAT_HITIATZVUTIsNull = true;
                oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
            }

            if (!oObjSidurimOvdimUpd.HACHTAMA_BEATAR_LO_TAKINIsNull)
            {
                oObjSidurimOvdimUpd.HACHTAMA_BEATAR_LO_TAKIN = "";
                curSidur.iHachtamaBeatarLoTakin = 0;
                oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
            }
        }

        private bool CheckHovatHityazvut(SidurDM curSidur, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, int iHityazvut, bool bHaveIdkunRashamet, DateTime CardDate,ref bool bHayavHityazvut)
        {
            bool bHaveMatala;
            bool bNidrashHityazvut = false;

            try
            {
                bHayavHityazvut = false;
                // מחפשים סידור שמצריך התייצבות (סידור מפה)
                if (curSidur.bSidurMyuhad)
                {
                    bHaveMatala = false;
                    foreach (PeilutDM oPeilut in curSidur.htPeilut.Values)
                    {
                        if (oPeilut.lMisparMatala > 0)
                        {
                            bHaveMatala = true;
                            break;
                        }
                    }
                    //סידור מיוחד שמקורו במטלה. אם לסידור זה אין מאפיין 83 (חובה להחתים התייצבות):
                    //יש לעדכן 1 בשדה פטור מהתייצבות 
                    if (bHaveMatala)
                    {
                        bHayavHityazvut = true;
                        if (curSidur.sHovatHityatzvut == "" && !bHaveIdkunRashamet)
                        {
                            oObjSidurimOvdimUpd.PTOR_MEHITIATZVUT = 1;
                            oObjSidurimOvdimUpd.NIDRESHET_HITIATZVUT = iHityazvut;
                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                        }
                        else { bNidrashHityazvut = true; }
                    }
                }
                else //if (oSidur.bSidurRagilExists)
                {

                    //- עוברים על הסידורים עד שנתקלים בסידור מפה הראשון ביום. 
                    DataRow[] drSugSidur;

                    //אם לסידור זה יש מאפיין 76 (לא נדרשת החתמת התייצבות):
                    //יש לעדכן 1 בשדה פטור מהתייצבות TB_Sidurim_Ovedim.Ptor_Mehitiatzvut=1    
                    //וסיימנו את הבדיקה.  
                    bNidrashHityazvut = true;
                    bHayavHityazvut = true;
                    drSugSidur = _container.Resolve<ISidurManager>().GetOneSugSidurMeafyen(curSidur.iSugSidurRagil, CardDate);
                    if (drSugSidur.Length > 0)
                    {
                        if (drSugSidur[0]["lo_nidreshet_hityazvut"].ToString() != "" && !bHaveIdkunRashamet)
                        {
                            oObjSidurimOvdimUpd.PTOR_MEHITIATZVUT = 1;
                            oObjSidurimOvdimUpd.NIDRESHET_HITIATZVUT = iHityazvut;
                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                            bNidrashHityazvut = false;
                        }
                    }


                }

                return bNidrashHityazvut;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CheckHityazvut(SidurDM curSidur, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, ShinuyInputData inputData)
        {
            DateTime dShatHatchalaSidur, dShatGmarSidur;
            bool bCheckSidurNosaf, bHaveSidur;
            dShatHatchalaSidur = curSidur.dFullShatHatchala;
            dShatGmarSidur = curSidur.dFullShatGmar;
            int iCountHachtamaLoTakin = 0;
            //לחפש סידור התייצבות (99200):
            try
            {
                bHaveSidur = false;
                bCheckSidurNosaf = true;
                if (inputData.htSpecialEmployeeDetails.Count > 0)
                {

                    foreach (SidurDM oSidurHityatvut in inputData.htSpecialEmployeeDetails.Values)
                    {
                        if (oSidurHityatvut.iMisparSidur == 99200 && bCheckSidurNosaf)
                        {
                            if ((dShatHatchalaSidur >= oSidurHityatvut.dFullShatHatchala && (dShatHatchalaSidur - oSidurHityatvut.dFullShatHatchala).TotalMinutes <= 60) ||
                                                                 (oSidurHityatvut.dFullShatHatchala >= dShatHatchalaSidur && (oSidurHityatvut.dFullShatHatchala - dShatHatchalaSidur).TotalMinutes <= inputData.oParam.iBdikatIchurLesidurHityatzvut))
                            {
                                bHaveSidur = true;
                                bCheckSidurNosaf = false;

                                int iPeletTnua = CheckPtorHityatzvutTnua(curSidur, oSidurHityatvut.sMikumShaonKnisa,inputData);
                                if (iPeletTnua == 0)
                                {
                                    oObjSidurimOvdimUpd.PTOR_MEHITIATZVUT = 1;
                                    break;
                                }
                                if (iPeletTnua == 1)
                                {
                                    oObjSidurimOvdimUpd.SHAT_HITIATZVUT = oSidurHityatvut.dFullShatHatchala;
                                    break;
                                }
                                if (iPeletTnua == 2)
                                {
                                    oObjSidurimOvdimUpd.SHAT_HITIATZVUT = oSidurHityatvut.dFullShatHatchala;
                                    oObjSidurimOvdimUpd.HACHTAMA_BEATAR_LO_TAKIN = "1";
                                    if (iCountHachtamaLoTakin == 0) bCheckSidurNosaf = true;
                                    iCountHachtamaLoTakin += 1;
                                }
                            }
                        }
                    }
                }
                if (!bHaveSidur)
                {
                    if (CheckPtorHityatzvutTnua(curSidur, "",inputData) == 0)
                    {
                        oObjSidurimOvdimUpd.PTOR_MEHITIATZVUT = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int CheckPtorHityatzvutTnua(SidurDM oSidur, string sMikumShaonKnisa, ShinuyInputData inputData)
        {
            int iKodHityazvut = -1;
            int iNekudatTziyunPeilut;
            PeilutDM oPeilutRishona = null;
            try
            {
                if (oSidur.htPeilut.Count > 0)
                {
                    for (int i = 0; i < oSidur.htPeilut.Count; i++)
                    {
                        oPeilutRishona = (PeilutDM)oSidur.htPeilut[i];
                        if (oPeilutRishona.iMakatType == enMakatType.mElement.GetHashCode() && oPeilutRishona.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "700")
                        {
                            DataRow[] drMeafyeneyElements =inputData.dtTmpMeafyeneyElements.Select("kod_element=" + int.Parse(oPeilutRishona.lMakatNesia.ToString().PadLeft(8).Substring(1, 2)));
                            if (drMeafyeneyElements.Length > 0)
                            {
                                //עבור אלמנט עם מאפיין 36 (בדיקת התייצבות לפי הפעילות הבאה) יש לגשת לבדיקה עם הפעילות הבאה אחרי האלמנט).        
                                if (drMeafyeneyElements[0]["bdikat_nz_hityazvut_next"].ToString() == "")
                                {
                                    break;
                                }
                            }
                            else break;
                        }
                        else { break; }
                    }

                    if (oPeilutRishona != null && !string.IsNullOrEmpty(oPeilutRishona.sSnifTnua))
                    {
                        var kavimDal = _container.Resolve<IKavimDAL>();
                        if (oPeilutRishona.iMakatType == enMakatType.mKavShirut.GetHashCode() || oPeilutRishona.iMakatType == enMakatType.mEmpty.GetHashCode() || oPeilutRishona.iMakatType == enMakatType.mNamak.GetHashCode())
                        {
                            iNekudatTziyunPeilut = oPeilutRishona.iXyMokedTchila;

                            if (sMikumShaonKnisa.Length >= 5)
                            {

                                iKodHityazvut = kavimDal.CheckHityazvutNehag(iNekudatTziyunPeilut, inputData.oParam.iRadyusMerchakMeshaonLehityazvut, int.Parse(oPeilutRishona.sSnifTnua), int.Parse(sMikumShaonKnisa.Substring(0, 3)), int.Parse(sMikumShaonKnisa.Substring(3, 2)));
                            }
                            else { iKodHityazvut = kavimDal.CheckHityazvutNehag(iNekudatTziyunPeilut, inputData.oParam.iRadyusMerchakMeshaonLehityazvut, int.Parse(oPeilutRishona.sSnifTnua), null, null); }
                        }
                        else if (oPeilutRishona.iMakatType == enMakatType.mElement.GetHashCode())
                        {
                            if (sMikumShaonKnisa.Length >= 5)
                            {
                                iKodHityazvut = kavimDal.CheckHityazvutNehag(null, inputData.oParam.iRadyusMerchakMeshaonLehityazvut, int.Parse(oPeilutRishona.sSnifTnua), int.Parse(sMikumShaonKnisa.Substring(0, 3)), int.Parse(sMikumShaonKnisa.Substring(3, 2)));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iKodHityazvut;
        }

    }
}