using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public class ShinuyFixedPitzulHafsaka06 : ShinuyBase
    {

        public ShinuyFixedPitzulHafsaka06(IUnityContainer container)
            : base(container)
        {

        }

        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.ShinuyFixedPitzulHafsaka06; } }

        public override void ExecShinuy(ShinuyInputData inputData)
        {
            SidurDM oSidurFirst = new SidurDM();
            SidurDM oSidurFirstSecond = new SidurDM();
            int indexSidurFirst = 0;
            try{
                  oSidurFirst = GetFirstSidurLetashulum(inputData.htEmployeeDetails, out  indexSidurFirst);

                if (oSidurFirst == null)
                    return;

                for (int i = indexSidurFirst; i < inputData.htEmployeeDetails.Count; i++)
                {
                    if (i < (inputData.htEmployeeDetails.Count - 1))
                    {
                        oSidurFirstSecond = (SidurDM)inputData.htEmployeeDetails[i + 1];
                        if (oSidurFirst.iLoLetashlum == 0 && oSidurFirstSecond.iLoLetashlum == 0)
                        {
                            if (!CheckIdkunRashemet("PITZUL_HAFSAKA", oSidurFirst.iMisparSidur, oSidurFirst.dFullShatHatchala,inputData))
                            {
                                FixedPitzulHafsaka06(oSidurFirst, i + 1, inputData);                       
                            }
                            oSidurFirst = oSidurFirstSecond; 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ShinuyFixedLina07: " + ex.Message);
            }
        }

        private SidurDM GetFirstSidurLetashulum(OrderedDictionary htEmployeeDetails, out int indexSidurFirst)
        {
            try
            {
                indexSidurFirst = -1;
                for (int i = 0; i < htEmployeeDetails.Count; i++)
                {
                    SidurDM oSidur = (SidurDM)htEmployeeDetails[i];
                    if (oSidur.iLoLetashlum == 0)
                    {
                        indexSidurFirst = i;
                        return oSidur;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void FixedPitzulHafsaka06(SidurDM oSidurFirst, int iNextSidur, ShinuyInputData inputData)
        {
            int iCountPitzul, iMinPaar;
            SidurDM oNextSidur = null;
            double dMinutsPitzul;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
            try
            {
                oObjSidurimOvdimUpd = GetUpdSidurObject(oSidurFirst,inputData);
                oObjSidurimOvdimUpd.PITZUL_HAFSAKA = 0;
                oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                oSidurFirst.sPitzulHafsaka = "0";
                //לא ניתן פיצול במידה וקיים סידור יחיד ביום.
                if (inputData.htEmployeeDetails.Count > 1)
                {
                    iCountPitzul = 0;
                   
                    iCountPitzul = inputData.oCollSidurimOvdimUpdRecorder.Count(sidur => sidur.ContainedItem.PITZUL_HAFSAKA == 1 && sidur.ContainedItem.UPDATE_OBJECT == 1);
                    
                    // העובד זכאי למקסימום פיצולים ביום עבודה לפי פרמטר 170. 
                    if (iCountPitzul <  inputData.oParam.iMaxPitzulLeyom && (oSidurFirst.dFullShatGmar != DateTime.MinValue && oSidurFirst.dFullShatHatchala != DateTime.MinValue))
                    {
                        //אין פיצול בשבתון 
                        if (!DateHelper.CheckShaaton(inputData.iSugYom, inputData.CardDate, inputData.SugeyYamimMeyuchadim))
                        {
                            if (iNextSidur < inputData.htEmployeeDetails.Values.Cast<SidurDM>().ToList().Count)
                            {
                                oNextSidur = (SidurDM)inputData.htEmployeeDetails[iNextSidur];
                                if (oNextSidur.dFullShatHatchala == DateTime.MinValue) return;
                                dMinutsPitzul = (oNextSidur.dFullShatHatchala - oSidurFirst.dFullShatGmar).TotalMinutes;
                            }
                            else
                            {
                                return;
                            }

                            //אם שני הסידורים מסומנים " לתשלום" - לא מגיע פיצול.
                            if (oSidurFirst.iLoLetashlum == 0 && oNextSidur.iLoLetashlum == 0)
                            {
                                // אם 2 הסידורים הם סידורי מפה (לא סידורי ***99) ו-2 מספרי הסידור זהים - לא מגיע פיצול .
                                if (!oSidurFirst.bSidurMyuhad && !oNextSidur.bSidurMyuhad && oSidurFirst.iMisparSidur == oNextSidur.iMisparSidur)
                                {
                                    return;
                                }

                                iMinPaar = GetMinPaar(oSidurFirst, oNextSidur, inputData);
                                if(iMinPaar==0)
                                    return;

                                if (dMinutsPitzul >= iMinPaar)
                                {
                                    SimunPizul(oSidurFirst, oNextSidur, inputData, oObjSidurimOvdimUpd);
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

        private void SimunPizul(SidurDM oSidurFirst, SidurDM oNextSidur, ShinuyInputData inputData, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            DataRow[] drSugSidur;
            bool bSidurNahagut = false, bNextSidurNahagut = false;
            try
            {
                var sidurManager = _container.Resolve<ISidurManager>();
                drSugSidur = sidurManager.GetOneSugSidurMeafyen(oSidurFirst.iSugSidurRagil, inputData.CardDate);
                if (sidurManager.IsSidurNahagut(drSugSidur, oSidurFirst) || IsSidurNihulTnua(drSugSidur, oSidurFirst) || IsSugAvodaKupai(oSidurFirst, inputData.CardDate))
                    bSidurNahagut = true;

                drSugSidur = sidurManager.GetOneSugSidurMeafyen(oNextSidur.iSugSidurRagil, inputData.CardDate);
                if (sidurManager.IsSidurNahagut(drSugSidur, oNextSidur) || IsSidurNihulTnua(drSugSidur, oNextSidur) || IsSugAvodaKupai(oSidurFirst, inputData.CardDate))
                    bNextSidurNahagut = true;

                if (bSidurNahagut && bNextSidurNahagut)
                {
                    oObjSidurimOvdimUpd.PITZUL_HAFSAKA = 1;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                    oSidurFirst.sPitzulHafsaka = "1";
                }
                else if (inputData.oMeafyeneyOved.IsMeafyenExist(41) && (oSidurFirst.bSidurTafkid && oSidurFirst.iZakayLepizul > 0 && oNextSidur.bSidurTafkid && oNextSidur.iZakayLepizul > 0))
                {
                    oObjSidurimOvdimUpd.PITZUL_HAFSAKA = 1;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                    oSidurFirst.sPitzulHafsaka = "1";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int GetMinPaar(SidurDM oSidurFirst, SidurDM oNextSidur, ShinuyInputData inputData)
        {
            int iMinPaar;
            try
            {
                if (oSidurFirst.dFullShatGmar >= inputData.oParam.dSummerStart && oSidurFirst.dFullShatGmar < inputData.oParam.dSummerEnd.AddDays(1))
                {
                    iMinPaar = inputData.oParam.iMinHefreshSidurimLepitzulSummer;
                }
                else
                {
                    iMinPaar = inputData.oParam.iMinHefreshSidurimLeptzulWinter;
                }

                //ביום שישי/ערב חג (לפי ה- oracle או טבלת ימים מיוחדים) יחושב פיצול רק במידה ושעת הגמר של הסידור השני היא לפני כניסת שבת. 
                if (oSidurFirst.sSidurDay == enDay.Shishi.GetHashCode().ToString() || oSidurFirst.sErevShishiChag == "1")
                {
                    if (oNextSidur.dFullShatHatchala < inputData.oParam.dKnisatShabat)
                        iMinPaar = inputData.oParam.iMinHefreshSidurimLepitzulSummer;
                    else iMinPaar = 0;  
                }
            }
            catch (Exception ex)
            {
                 throw ex;
            }
            return iMinPaar;
        }
        
        //private bool IsSidurNihulTnua(DataRow[] drSugSidur, SidurDM oSidur)
        //{
        //    bool bSidurNihulTnua = false;
        //    bool bElementZviraZman = false;  
        //    //הפונקציה תחזיר TRUE אם הסידור הוא סידור נהגות

        //    try
        //    {
        //        if (oSidur.bSidurMyuhad)
        //        {//סידור מיוחד
        //            bSidurNihulTnua = (oSidur.sSectorAvoda == enSectorAvoda.Nihul.GetHashCode().ToString());
        //            if (!bSidurNihulTnua)
        //               if (oSidur.iMisparSidur == 99301){
                    
        //                PeilutDM oPeilut = null;
        //                for (int i = 0; i < oSidur.htPeilut.Count; i++)
        //                {
        //                     oPeilut = (PeilutDM)oSidur.htPeilut[i];
        //                     if ( !string.IsNullOrEmpty(oPeilut.sElementZviraZman))
        //                         if (int.Parse(oPeilut.sElementZviraZman) == 4)
        //                         {
        //                             bElementZviraZman = true;
        //                             break;
        //                         }
        //                }
        //                if( bElementZviraZman)
        //                    bSidurNihulTnua = true;
        //            }
        //        }
        //        else
        //        {//סידור רגיל
        //            if (drSugSidur.Length > 0)
        //            {
        //                bSidurNihulTnua = (drSugSidur[0]["sector_avoda"].ToString() == enSectorAvoda.Nihul.GetHashCode().ToString());
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return bSidurNihulTnua;
        //}

        private bool IsSugAvodaKupai(SidurDM oSidur, DateTime dCardDate)
        {
            DataRow[] drSugSidur;
            bool bSidurKupai = false;
            try
            {
                //נבדוק אם סידור הוא מסוג קופאי
                if (!oSidur.bSidurMyuhad)
                {//סידור מיוחד
                    bSidurKupai = (oSidur.sSugAvoda == enSugAvoda.Kupai.GetHashCode().ToString());
                }
                else
                {//סידור רגיל
                    drSugSidur = _container.Resolve<ISidurManager>().GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, dCardDate);
                    if (drSugSidur.Length > 0)
                    {
                        bSidurKupai = (drSugSidur[0]["sug_Avoda"].ToString() == enSugAvoda.Kupai.GetHashCode().ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bSidurKupai;
        }

    }
}
