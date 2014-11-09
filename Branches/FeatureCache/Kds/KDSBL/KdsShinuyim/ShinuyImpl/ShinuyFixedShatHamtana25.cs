using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.Enums;
using KDSCommon.UDT;
using KdsShinuyim.Enums;
using Microsoft.Practices.Unity;

namespace KdsShinuyim.ShinuyImpl
{
    class ShinuyFixedShatHamtana25: ShinuyBase
    {
        public ShinuyFixedShatHamtana25(IUnityContainer container)
            : base(container)
        {

        }

        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.ShinuyFixedShatHamtana25; } }

        public override void ExecShinuy(ShinuyInputData inputData)
        {
            try
            {
                FixedShatHamtana25(inputData);
            } 
            catch (Exception ex)
            {
                throw new Exception("ShinuyFixedShatHamtana25: " + ex.Message);
            }
        }

        private void FixedShatHamtana25(ShinuyInputData inputData)
        {
            try
            {
                bool hasSidurEilat = false;
                SidurDM oFirstSidurEilat = null;
                SidurDM oSecondSidurEilat = null;
               // PeilutDM oFirstPeilutEilat = null;
                PeilutDM oSecondPeilutEilat = null;
                PeilutDM tmpPeilut = null;

                inputData.htEmployeeDetails.Values
                                 .Cast<SidurDM>()
                                 .ToList()
                                 .ForEach
                                 (
                                    sidur =>
                                    {
                                        if (sidur.IsLongEilatTrip(out tmpPeilut, inputData.oParam.fOrechNesiaKtzaraEilat))
                                        {
                                            if (!hasSidurEilat)
                                            {
                                                hasSidurEilat = true;
                                                oFirstSidurEilat = sidur;
                                             //   oFirstPeilutEilat = tmpPeilut;
                                            }
                                            else
                                            {
                                                oSecondSidurEilat = sidur;
                                                oSecondPeilutEilat = tmpPeilut;
                                            }
                                        }
                                    }
                                 );

                if (oFirstSidurEilat != null && oSecondSidurEilat != null)
                {
                    int firstSidurIndex = inputData.htEmployeeDetails.Values.Cast<SidurDM>().ToList().FindIndex(sidur => (sidur.iMisparSidur == oFirstSidurEilat.iMisparSidur && sidur.dFullShatHatchala == oFirstSidurEilat.dFullShatHatchala));
                    int secondSidurIndex = inputData.htEmployeeDetails.Values.Cast<SidurDM>().ToList().FindIndex(sidur => (sidur.iMisparSidur == oSecondSidurEilat.iMisparSidur && sidur.dFullShatHatchala == oSecondSidurEilat.dFullShatHatchala));
                    if (firstSidurIndex + 1 == secondSidurIndex && !(inputData.OvedDetails.iSnifTnua == oSecondPeilutEilat.iSnifAchrai))
                    {
                        HosafatHamtana(firstSidurIndex, secondSidurIndex, inputData);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void HosafatHamtana(int iIndexFrom, int iIndexTo, ShinuyInputData inputData)
        {
            int iSumElement = 0, indexPeilut;
            int iSumElementLesidur = 0;
            SidurDM curSidur = null;
            SidurDM oSidurNext = null;
            PeilutDM oElement;
            double dPaar, dZmanElement;
            string sZmanElement;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
          
            try
            {
                for (int I = iIndexFrom; I < iIndexTo; I++)
                {
                    oElement = null;
                    curSidur = (SidurDM)inputData.htEmployeeDetails[I];
                    oObjSidurimOvdimUpd = GetUpdSidurObject(curSidur, inputData);

                    oElement = GetElement735(curSidur, out indexPeilut);
                    
                    iSumElementLesidur = 0;
                    if (oElement != null)
                    {
                        iSumElementLesidur = int.Parse(oElement.lMakatNesia.ToString().Substring(3, 3));
                    }

                    iSumElement += iSumElementLesidur;

                    oSidurNext = (SidurDM)inputData.htEmployeeDetails[I + 1];

                    //ב.	לוקחים את שני הסידורים הראשונים. מחשבים שעת התחלה של השני פחות שעת גמר של הראשון, אם התוצאה גדולה מ- 1 וגם סה"כ ההמתנה שניתנה עד כה קטנה מהערך בפרמטר 148ובמידה 
                    dPaar = (oSidurNext.dFullShatHatchala - curSidur.dFullShatGmar).TotalMinutes;
                    if (dPaar >= 1 && iSumElement < inputData.oParam.iMaxZmanHamtanaEilat)
                    {
                        if (!CheckIdkunRashemet("SHAT_GMAR", curSidur.iMisparSidur, curSidur.dFullShatHatchala, inputData))
                        {
                            //במידה ובסידור הראשון מביניהם לא קיים אלמנט - מוסיפים אלמנט המתנה 724xxxxx.
                            dZmanElement = Math.Min(iSumElementLesidur + dPaar, inputData.oParam.iMaxZmanHamtanaEilat);
                            sZmanElement = dZmanElement.ToString().PadLeft(3, (char)48);

                            if (iSumElementLesidur == 0)
                            {
                                if (!IsDuplicateShatYeziaHamtana(curSidur))
                                {
                                    HosafatPeilutHamtana(inputData, curSidur, sZmanElement);
                                }   
                            }
                            else
                            {
                                UpdateHamtana(oElement, sZmanElement, oObjSidurimOvdimUpd, I, indexPeilut, inputData, curSidur);
                            }
                            curSidur.dFullShatGmar = curSidur.dFullShatGmar.AddMinutes(dPaar); //dZmanElement);
                            curSidur.sShatGmar = curSidur.dFullShatGmar.ToString("HH:mm");
                            oObjSidurimOvdimUpd.SHAT_GMAR = curSidur.dFullShatGmar;
                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                           // htEmployeeDetails[I] = oSidur;
                            //}
                        }
                    }
                }

            }
            catch (Exception ex)
            {
               throw ex;
            }
        }

        private void UpdateHamtana(PeilutDM oElement, string sZmanElement, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, int IndexSidur,int IndexPeilut, ShinuyInputData inputData, SidurDM curSidur)
        {
            string oVal;
            try{
                oVal = oElement.lMakatNesia.ToString();
                SourceObj SourceObject;
                OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd = GetUpdPeilutObject(IndexSidur, oElement, inputData, oObjSidurimOvdimUpd, out SourceObject);
                oElement.lMakatNesia = long.Parse(oElement.lMakatNesia.ToString().Replace(oElement.lMakatNesia.ToString().Substring(3, 3), sZmanElement));
                oObjPeilutOvdimUpd.MAKAT_NESIA = oElement.lMakatNesia;
                oObjPeilutOvdimUpd.UPDATE_OBJECT = 1;
               // oElement = CreatePeilut(inputData.iMisparIshi, inputData.CardDate, oElement, oObjPeilutOvdimUpd.MAKAT_NESIA, inputData.dtTmpMeafyeneyElements);
                UpdatePeilut(inputData.iMisparIshi, inputData.CardDate, oElement, oObjPeilutOvdimUpd.MAKAT_NESIA, inputData.dtTmpMeafyeneyElements);
                curSidur.htPeilut[IndexPeilut] = oElement;

                InsertLogPeilut(inputData, oObjSidurimOvdimUpd.MISPAR_SIDUR, oObjSidurimOvdimUpd.SHAT_HATCHALA, oElement.dFullShatYetzia, oElement.lMakatNesia, oVal, oElement.lMakatNesia.ToString(), 25, IndexSidur, IndexPeilut, "MAKAT_NESIA");            
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private void HosafatPeilutHamtana(ShinuyInputData inputData, SidurDM curSidur, string sZmanElement)
        {
            try{
                OBJ_PEILUT_OVDIM oObjPeilutOvdimIns = new OBJ_PEILUT_OVDIM();
                InsertToObjPeilutOvdimForInsert(curSidur, oObjPeilutOvdimIns, inputData.UserId);
                oObjPeilutOvdimIns.MISPAR_SIDUR = curSidur.iMisparSidur;
                oObjPeilutOvdimIns.TAARICH = inputData.CardDate;
                oObjPeilutOvdimIns.SHAT_YETZIA = curSidur.dFullShatGmar;
                oObjPeilutOvdimIns.SHAT_HATCHALA_SIDUR = curSidur.dFullShatHatchala;
                oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse("735" + sZmanElement + "00");
                oObjPeilutOvdimIns.BITUL_O_HOSAFA = 4;
                oObjPeilutOvdimIns.MISPAR_KNISA = 0;
                inputData.oCollPeilutOvdimIns.Add(oObjPeilutOvdimIns);

                PeilutDM oPeilutNew = CreatePeilut(inputData.iMisparIshi, inputData.CardDate, oObjPeilutOvdimIns, inputData.dtTmpMeafyeneyElements);
                oPeilutNew.iBitulOHosafa = 4;
                curSidur.htPeilut.Add(25 + curSidur.htPeilut.Count + 1, oPeilutNew);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private PeilutDM GetElement735(SidurDM curSidur, out int indexPeilut)
        {
            try
            {
                indexPeilut = -1;
                for (int j = 0; j <= curSidur.htPeilut.Values.Count - 1; j++)
                {
                    PeilutDM oPeilut = (PeilutDM)curSidur.htPeilut[j];
                    if (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "735")
                    {
                        indexPeilut = j;
                        return oPeilut;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private bool IsDuplicateShatYeziaHamtana(SidurDM curSidur)
        {
            PeilutDM oPeilut;
            DateTime dShatYezia = new DateTime();
            try
            {//בדיקה אם קיימת פעילות בשעת יציאה של פעילות ההמתנה שרוצים להוסיף
                dShatYezia = curSidur.dFullShatGmar;
                for (int i = 0; i <= curSidur.htPeilut.Values.Count - 1; i++)
                {
                    oPeilut = (PeilutDM)curSidur.htPeilut[i];
                    if (oPeilut.dFullShatYetzia == dShatYezia)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
       
    }
}