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
using ObjectCompare;

namespace KdsShinuyim.ShinuyImpl
{
    public class ShinuyKonnutGrira03 : ShinuyBase
    {
        public ShinuyKonnutGrira03(IUnityContainer container)
            : base(container)
        {

        }

        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.ShinuyKonnutGrira03; } }


        public override void ExecShinuy(ShinuyInputData inputData)
        {
            try
            {
                KonnutGrira03(inputData);
            }
            catch (Exception ex)
            {
                throw new Exception("ShinuyKonnutGrira03: " + ex.Message);
            }
        }

        private void KonnutGrira03(ShinuyInputData inputData)
        {               
            bool bSidurKonnutGrira = false;
            SidurDM oSidurKonenutGrira = null;
            int iSidurKonnutGrira = 0;
            SidurDM curSidur= null;
            OBJ_SIDURIM_OVDIM oObjSidurimConenutGriraUpd;
            try
            {
                if (inputData.htEmployeeDetails != null)
                {
                    if (HaveKonenutGrira(inputData, ref curSidur,ref iSidurKonnutGrira))
                    {
                        bSidurKonnutGrira = true;
                        UpdateNetunim(inputData,curSidur, bSidurKonnutGrira, iSidurKonnutGrira, ref oSidurKonenutGrira);
                    }
                    //לשאול אם כוננות לא לתשלום =0
                    if ((bSidurKonnutGrira))
                    {
                        oSidurKonenutGrira = (SidurDM)inputData.htEmployeeDetails[iSidurKonnutGrira];
                        oObjSidurimConenutGriraUpd = GetSidurOvdimObject(oSidurKonenutGrira.iMisparSidur, oSidurKonenutGrira.dFullShatHatchala,inputData);
                        if (oObjSidurimConenutGriraUpd.LO_LETASHLUM == 0)
                        {
                            UdateKonenutGrira(curSidur,inputData, oSidurKonenutGrira, oObjSidurimConenutGriraUpd);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UdateKonenutGrira(SidurDM curSidur,ShinuyInputData inputData, SidurDM oSidurKonenutGrira, OBJ_SIDURIM_OVDIM oObjSidurimConenutGriraUpd)
        {
            float Minutes, iNumSidur2;

            try{
                oObjSidurimConenutGriraUpd.UPDATE_OBJECT = 1;
                Minutes = float.Parse((oSidurKonenutGrira.dFullShatGmar - oSidurKonenutGrira.dFullShatHatchala).TotalMinutes.ToString());
                iNumSidur2 = int.Parse(oSidurKonenutGrira.iMisparSidur.ToString().PadLeft(5, '0').Substring(0, 2));
                if (Minutes > inputData.oParam.iMinZmanGriraDarom)
                {
                    if (iNumSidur2 >= 25 || iNumSidur2 == 4 || (iNumSidur2 == 22 &&
                                  (curSidur.sSidurDay == enDay.Shishi.GetHashCode().ToString() || curSidur.sErevShishiChag == "1" || curSidur.sSidurDay == enDay.Shabat.GetHashCode().ToString() || curSidur.sShabaton == "1")))
                   // if (int.Parse(oSidurKonenutGrira.iMisparSidur.ToString().Substring(0, 2)) >= 25)
                    {
                        oObjSidurimConenutGriraUpd.SHAT_HATCHALA_LETASHLUM = oSidurKonenutGrira.dFullShatHatchala;
                        oObjSidurimConenutGriraUpd.SHAT_GMAR_LETASHLUM = oSidurKonenutGrira.dFullShatHatchala.AddMinutes(inputData.oParam.iMinZmanGriraDarom);
                    }
                    else if (iNumSidur2 < 25 && (iNumSidur2 == 1 || iNumSidur2 == 22) && (curSidur.sSidurDay != enDay.Shabat.GetHashCode().ToString() &&
                                            curSidur.sSidurDay != enDay.Shishi.GetHashCode().ToString() &&
                                            !curSidur.sErevShishiChag.Equals("1") && !curSidur.sShabaton.Equals("1")))
                    {
                        oObjSidurimConenutGriraUpd.SHAT_HATCHALA_LETASHLUM = oSidurKonenutGrira.dFullShatHatchala;
                        oObjSidurimConenutGriraUpd.SHAT_GMAR_LETASHLUM = oSidurKonenutGrira.dFullShatHatchala.AddMinutes(inputData.oParam.iMinZmanGriraTzafon);
                    }
                }
                else if (Minutes > inputData.oParam.iMinZmanGriraTzafon && Minutes <= inputData.oParam.iMinZmanGriraDarom)
                {
                   // if (int.Parse(oSidurKonenutGrira.iMisparSidur.ToString().Substring(0, 2)) >= 25)
                    if (iNumSidur2 >= 25 || iNumSidur2 == 4 || (iNumSidur2 == 22 &&
                                   (curSidur.sSidurDay == enDay.Shishi.GetHashCode().ToString() || curSidur.sErevShishiChag == "1" || curSidur.sSidurDay == enDay.Shabat.GetHashCode().ToString() || curSidur.sShabaton == "1")))
                               
                    {
                        oObjSidurimConenutGriraUpd.SHAT_HATCHALA_LETASHLUM = oSidurKonenutGrira.dFullShatHatchala;
                        oObjSidurimConenutGriraUpd.SHAT_GMAR_LETASHLUM = oSidurKonenutGrira.dFullShatGmar;
                    }
                    else if (iNumSidur2 < 25 && (iNumSidur2 == 1 || iNumSidur2 == 22) && (curSidur.sSidurDay != enDay.Shabat.GetHashCode().ToString() &&
                                                curSidur.sSidurDay != enDay.Shishi.GetHashCode().ToString() &&
                                                !curSidur.sErevShishiChag.Equals("1") && !curSidur.sShabaton.Equals("1")))
                    {
                        oObjSidurimConenutGriraUpd.SHAT_HATCHALA_LETASHLUM = oSidurKonenutGrira.dFullShatHatchala;
                        oObjSidurimConenutGriraUpd.SHAT_GMAR_LETASHLUM = oSidurKonenutGrira.dFullShatHatchala.AddMinutes(inputData.oParam.iMinZmanGriraTzafon);
                    }
                }
                else if (Minutes <= inputData.oParam.iMinZmanGriraTzafon)
                {
                    oObjSidurimConenutGriraUpd.SHAT_HATCHALA_LETASHLUM = oSidurKonenutGrira.dFullShatHatchala;
                    oObjSidurimConenutGriraUpd.SHAT_GMAR_LETASHLUM = oSidurKonenutGrira.dFullShatGmar;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateNetunim(ShinuyInputData inputData,SidurDM curSidur, bool bSidurKonnutGrira, int iSidurKonnutGrira, ref SidurDM oSidurKonenutGrira)
        {
           
            DataTable dtSidurGrira = new DataTable();
            int iTypeGrira, i;
            int iGriraNum = 0;
            SidurDM oSidurGrira = null;
            OBJ_SIDURIM_OVDIM oObjSidurGriraUpd = null;
            try{

                for (int j = 0; j < inputData.htEmployeeDetails.Count; j++)
                {
                    oSidurGrira = (SidurDM)inputData.htEmployeeDetails[j];

                    //אם נמצא  סידור של כוננות גרירה, נחפש סידורים של כוננות גרירה בפועל ונשמור את האינדקס שלהם במערך
                    //נבדוק אם סידור גרירה בפועל בטווח הזמן של סידור הגרירה
                    if (iSidurKonnutGrira != j)
                    {     
                        if (IsActualKonnutGrira(oSidurGrira, inputData,iSidurKonnutGrira, out iTypeGrira))
                        {
                            oObjSidurGriraUpd = GetUpdSidurObject(oSidurGrira, inputData);
                            iGriraNum += 1;

                            //אם יש סידור כוננות גרירה  וגם לפחות סידור גרירה בפועל אחד 
                            if ((bSidurKonnutGrira) && (oObjSidurGriraUpd != null))
                            {
                                oSidurKonenutGrira = (SidurDM)inputData.htEmployeeDetails[iSidurKonnutGrira];
                                if (!CheckIdkunRashemet("LO_LETASHLUM", oSidurKonenutGrira.iMisparSidur, oSidurKonenutGrira.dFullShatHatchala, inputData))
                                {
                                    SetLoLetashlum(oSidurKonenutGrira,inputData);
                                }

                                if (iTypeGrira == 2)
                                {
                                    SetShatGmarGrira(oObjSidurGriraUpd, oSidurKonenutGrira, oSidurGrira);
                                    if (oObjSidurGriraUpd.UPDATE_OBJECT == 1)
                                    {
                                        oSidurGrira.dFullShatGmar = oObjSidurGriraUpd.SHAT_GMAR;
                                        oSidurGrira.sShatGmar = oSidurGrira.dFullShatGmar.ToString("HH:mm");
                                        inputData.htEmployeeDetails[j] = oSidurGrira;
                                    }
                                }
                                if (iTypeGrira == 1)
                                {
                                    SetBitulZmanNesiot(oObjSidurGriraUpd, oSidurKonenutGrira, oSidurGrira);

                                    SetZmanHashlama(oObjSidurGriraUpd, oSidurKonenutGrira, oSidurGrira, iGriraNum, inputData);
                                }
                            }

                            break;
                        }
                    }


                    //בכל המקומות בהם מחפשים סידור גרירה בפועל בטווח הזמן של כוננות הגרירה וסידור כוננות הגרירה מתחיל לפני חצות ומסתיים אחרי חצות, 
                    if (bSidurKonnutGrira && curSidur.dFullShatHatchala < DateHelper.GetDateTimeFromStringHour("24:00", inputData.CardDate) && curSidur.dFullShatGmar > DateHelper.GetDateTimeFromStringHour("24:00", inputData.CardDate))
                    {
                        // יש לחפש סידור גרירה בתאריך כרטיס העבודה ובתאריך כרטיס העבודה +1.
                        if (_container.Resolve<ISidurManager>().CheckHaveSidurGrira(inputData.iMisparIshi, inputData.CardDate.AddDays(1), ref dtSidurGrira))
                        {
                            for (i = 0; i < dtSidurGrira.Rows.Count; i++)
                            {
                                oSidurGrira = _container.Resolve<ISidurManager>().CreateClsSidurFromSidurayGrira(dtSidurGrira.Rows[i]);

                                if (IsActualKonnutGrira(oSidurGrira,inputData, iSidurKonnutGrira, out iTypeGrira))
                                {
                                    oObjSidurGriraUpd = InsertToObjSidurimOvdimForUpdate(oSidurGrira, inputData.UserId);
                                    ModificationRecorder<OBJ_SIDURIM_OVDIM> recorder = new ModificationRecorder<OBJ_SIDURIM_OVDIM>(oObjSidurGriraUpd, true);
                                    inputData.oCollSidurimOvdimUpdRecorder.Add(recorder);

                                    iGriraNum += 1;

                                    //אם יש סידור כוננות גרירה  וגם לפחות סידור גרירה בפועל אחד 
                                    if ((bSidurKonnutGrira) && (oObjSidurGriraUpd != null))
                                    {
                                        oSidurKonenutGrira = (SidurDM)inputData.htEmployeeDetails[iSidurKonnutGrira];
                                        if (!CheckIdkunRashemet("LO_LETASHLUM", oSidurKonenutGrira.iMisparSidur, oSidurKonenutGrira.dFullShatHatchala, inputData))
                                        {
                                            SetLoLetashlum(oSidurKonenutGrira,inputData);
                                        }

                                        if (iTypeGrira == 2)
                                            SetShatGmarGrira( oObjSidurGriraUpd, oSidurKonenutGrira, oSidurGrira);
                                        if (iTypeGrira == 1)
                                        {
                                            SetBitulZmanNesiot(oObjSidurGriraUpd, oSidurKonenutGrira, oSidurGrira);

                                            SetZmanHashlama(oObjSidurGriraUpd, oSidurKonenutGrira, oSidurGrira,iGriraNum ,inputData);
                                        }
                                    }
                                }
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

        private bool HaveKonenutGrira(ShinuyInputData inputData,ref SidurDM curSidur, ref int iSidurKonnutGrira)
        {
            try{
                for (int i = 0; i < inputData.htEmployeeDetails.Count; i++)
                {
                    curSidur = (SidurDM)inputData.htEmployeeDetails[i];

                    //נבדוק אם סידור כוננות גרירה
                    if (IsKonnutGrira(curSidur, inputData.CardDate))
                    {
                        iSidurKonnutGrira = i;
                        return true; ;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsKonnutGrira(SidurDM curSidur, DateTime dCardDate)
        {
            DataRow[] drSugSidur;
            bool bSidurGrira = false;
            try
            {
                //נבדוק אם סידור הוא מסוג כוננות גרירה
                if (!curSidur.bSidurMyuhad)
                {//סידור רגיל
                    drSugSidur = _container.Resolve<ISidurManager>().GetOneSugSidurMeafyen(curSidur.iSugSidurRagil, dCardDate);
                    if (drSugSidur.Length > 0)
                    {
                        bSidurGrira = (drSugSidur[0]["sug_Avoda"].ToString() == enSugAvoda.Grira.GetHashCode().ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bSidurGrira;
        }

        private void SetLoLetashlum(SidurDM oSidurKonenutGrira, ShinuyInputData inputData)
        {
            //נמצא את סידור כוננות הגרירה ונסמן לא לתשלום
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
            try
            {
                oObjSidurimOvdimUpd = GetSidurOvdimObject(oSidurKonenutGrira.iMisparSidur, oSidurKonenutGrira.dFullShatHatchala, inputData);

                if (oObjSidurimOvdimUpd != null)
                {
                    oObjSidurimOvdimUpd.LO_LETASHLUM = 1;
                    oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM = 7;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                    // oCollSidurimOvdimUpd.Add(oObjSidurimOvdimUpd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetShatGmarGrira(OBJ_SIDURIM_OVDIM oObjSidurGriraUpd, SidurDM oSidurKonenutGrira, SidurDM oSidurGrira)
        {
            int iMerchav;
            float fTime;
            try
            {

                iMerchav = int.Parse((oSidurKonenutGrira.iMisparSidur).ToString().Substring(0, 2));

                fTime = float.Parse((oSidurGrira.dFullShatGmar - oSidurKonenutGrira.dFullShatHatchala).TotalMinutes.ToString());

                if (iMerchav < enMerchav.Tzafon.GetHashCode() && fTime < 60)
                {
                    oObjSidurGriraUpd.SHAT_GMAR = oSidurKonenutGrira.dFullShatHatchala.AddMinutes(60);
                    oObjSidurGriraUpd.UPDATE_OBJECT = 1;

                }
                else if (iMerchav > enMerchav.Tzafon.GetHashCode() && fTime < 120)
                {
                    oObjSidurGriraUpd.SHAT_GMAR = oSidurKonenutGrira.dFullShatHatchala.AddMinutes(120);
                    oObjSidurGriraUpd.UPDATE_OBJECT = 1;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsActualKonnutGrira(SidurDM curSidur, ShinuyInputData inputData, int iSidurKonnutGrira, out int iTypeGrira)
        {
            SidurDM oSidurKonenutGrira;
            bool bSidurActualGrira = false;
            DateTime dKonenutShatHatchala, dKonenutShatGmar, dGriraShatHatchala, dGriraShatGmar;
            try
            {
                iTypeGrira = 0;
                if (curSidur.bSidurMyuhad)
                {//סידור מיוחד
                    if (curSidur.sSugAvoda == enSugAvoda.ActualGrira.GetHashCode().ToString())
                    {
                        oSidurKonenutGrira = (SidurDM)inputData.htEmployeeDetails[iSidurKonnutGrira];
                        dKonenutShatHatchala = oSidurKonenutGrira.dFullShatHatchala;
                        dKonenutShatGmar = oSidurKonenutGrira.dFullShatGmar;
                        dGriraShatHatchala = curSidur.dFullShatHatchala;
                        dGriraShatGmar = curSidur.dFullShatGmar;

                        //גרירה בפועל מוכל בתוך כוננות גרירה: 
                        //ש.התחלה גרירה בפועל >=  ש.התחלה כוננות גרירה וגם  
                        //ש. גמר גרירה בפועל <=  ש.גמר כוננות גרירה.
                        if ((dGriraShatHatchala >= dKonenutShatHatchala) && (dGriraShatGmar <= dKonenutShatGmar))
                        {
                            bSidurActualGrira = true;
                            iTypeGrira = 1;
                        }
                        // גרירה בפועל מתחילה לפני כוננות הגרירה ומסתיימת לפני סיום הכוננות    
                        //ש.התחלה גרירה בפועל <  ש.התחלה כוננות גרירה וגם  
                        //ש.גמר גרירה בפועל > ש.התחלה כוננות גרירה וגם  
                        //ש.גמר גרירה בפועל <=  ש.גמר כוננות גרירה.
                        else if ((dGriraShatHatchala < dKonenutShatHatchala) && (dGriraShatGmar > dKonenutShatHatchala) && (dGriraShatGmar <= dKonenutShatGmar))
                        {
                            bSidurActualGrira = true;
                            iTypeGrira = 2;
                        }
                        // . גרירה בפועל מתחילה אחרי כוננות הגרירה ומסתיימת אחרי סיום הכוננות  
                        //ש.התחלה גרירה בפועל >=  ש.התחלה כוננות גרירה וגם  
                        //ש.גמר גרירה בפועל >  ש.גמר כוננות גרירה. 
                        else if ((dGriraShatHatchala >= dKonenutShatHatchala) && (dGriraShatGmar > dKonenutShatGmar))
                        {
                            bSidurActualGrira = true;
                            iTypeGrira = 3;
                        }
                        //גרירה בפועל מתחילה לפני כוננות הגרירה ומסתיימת  אחרי הכוננות
                        //ש.התחלה גרירה בפועל < ש.התחלה כוננות גרירה וגם  
                        //ש.גמר סידור גרירה בפועל >  שעת גמר כוננות גרירה

                        else if ((dGriraShatHatchala < dKonenutShatHatchala) && (dGriraShatGmar > dKonenutShatGmar))
                        {
                            bSidurActualGrira = true;
                            iTypeGrira = 4;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bSidurActualGrira;
        }

        private void SetBitulZmanNesiot(OBJ_SIDURIM_OVDIM oObjSidurGriraUpd, SidurDM oSidurKonenutGrira, SidurDM oSidurGrira)
        {
            DateTime dKonenutShatHatchala, dKonenutShatGmar;
            int iZmanNesia = 0;
            try
            {
                dKonenutShatHatchala = oSidurKonenutGrira.dFullShatHatchala;
                dKonenutShatGmar = oSidurKonenutGrira.dFullShatGmar;

                if (!String.IsNullOrEmpty(oSidurGrira.sMikumShaonKnisa) && dKonenutShatHatchala != DateTime.MinValue)
                {
                    if (int.Parse(oSidurGrira.sMikumShaonKnisa) > 0)
                    {
                        iZmanNesia = 1;
                    }
                }


                //אם כוננות גרירה בפועל מוכלת לגמרי בכוננות גרירה
                if (dKonenutShatHatchala != DateTime.MinValue && dKonenutShatGmar != DateTime.MinValue)
                {
                    if (oSidurGrira.dFullShatHatchala > dKonenutShatHatchala)
                    {
                        if ((!String.IsNullOrEmpty(oSidurGrira.sMikumShaonKnisa)) && (int.Parse(oSidurGrira.sMikumShaonKnisa) > 0) && (!String.IsNullOrEmpty(oSidurGrira.sMikumShaonYetzia)) && (int.Parse(oSidurGrira.sMikumShaonYetzia) > 0))
                        {
                            if (iZmanNesia == 1)
                                iZmanNesia = 3;
                            else
                                iZmanNesia = 2;
                        }
                    }
                }

                if (iZmanNesia > 0)
                    SetBitulZmanNesiotObject( oObjSidurGriraUpd,  iZmanNesia);
            }


            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetBitulZmanNesiotObject(OBJ_SIDURIM_OVDIM oObjSidurGriraUpd, int iZmanNesia)
        {
            try
            {
                oObjSidurGriraUpd.MEZAKE_NESIOT = iZmanNesia;
                oObjSidurGriraUpd.UPDATE_OBJECT = 1;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetZmanHashlama(OBJ_SIDURIM_OVDIM oObjSidurGriraUpd, SidurDM oSidurKonenutGrira, SidurDM oSidurGrira, int iGriraNum, ShinuyInputData inputData)
        {
            int iZmanHashlama = 0;
            int iMerchav = 0;
            float fSidurTime = 0;
            try
            {
                //אם זוהי גרירה ראשונה בפועל (זיהוי הסידור לפי הלוגיקה בסעיף סימון כוננות גרירה לא לתשלום) בתוך זמן סידור כוננות גרירה (זיהוי הסידור לפי הלוגיקה בסעיף סימון כוננות גרירה לא לתשלום) ואם סידור הכוננות הוא "מרחב צפון" (קוד הסניף שהוא 2 הספרות הראשונות של מספר הסידור קטן מ-25) וזמן הסידור (גמר - התחלה) פחות מהזמן המוגדר בפרמטר 164 (זמן גרירה מינימלי באזור צפון) אזי יש לסמן "2" בשדה "קוד השלמה". 

                if (!CheckIdkunRashemet("HASHLAMA", oSidurGrira.iMisparSidur, oSidurGrira.dFullShatHatchala, inputData))
                {
                    iMerchav = int.Parse((oSidurKonenutGrira.iMisparSidur).ToString().Substring(0, 2));

                    fSidurTime = float.Parse((oSidurGrira.dFullShatGmar - oSidurGrira.dFullShatHatchala).TotalMinutes.ToString());

                    if (iGriraNum == 1) //גרירה ראשונה
                    {
                        if ((iMerchav < enMerchav.Tzafon.GetHashCode()) && (fSidurTime < inputData.oParam.iMinZmanGriraTzafon))
                        {
                            iZmanHashlama = 1;
                        }
                        else
                        {
                            //איזור דרום/ירושלים
                            if (((iMerchav >= enMerchav.Tzafon.GetHashCode()) && (iMerchav < enMerchav.Darom.GetHashCode())) && (fSidurTime < inputData.oParam.iMinZmanGriraDarom))
                            {
                                iZmanHashlama = 2;
                            }
                            else
                            {
                                if ((iMerchav >= enMerchav.Darom.GetHashCode()) && ((fSidurTime < inputData.oParam.iMinZmanGriraJrusalem)))
                                {
                                    iZmanHashlama = 2;
                                }
                            }
                        }
                        if (iZmanHashlama > 0)
                        {

                            oObjSidurGriraUpd.SUG_HASHLAMA = 1;
                            oObjSidurGriraUpd.HASHLAMA = iZmanHashlama;
                            oObjSidurGriraUpd.UPDATE_OBJECT = 1;

                        }
                    }
                    else //גרירה שניה ומעלה
                    {
                        //אם זוהי אינה גרירה ראשונה בפועל בתוך זמן הכוננות
                        //וגם זמן הסידור (גמר - התחלה) פחות מהזמן המוגדר בפרמטר 181 (זמן גרירה נוספת מינימלי בזמן כוננות גרירה) אזי יש לסמן "1" בשדה "קוד השלמה". 

                        if (fSidurTime < inputData.oParam.iGriraAddMinTime)
                        {

                            oObjSidurGriraUpd.SUG_HASHLAMA = 1;
                            oObjSidurGriraUpd.HASHLAMA = 1;
                            oObjSidurGriraUpd.UPDATE_OBJECT = 1;

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
