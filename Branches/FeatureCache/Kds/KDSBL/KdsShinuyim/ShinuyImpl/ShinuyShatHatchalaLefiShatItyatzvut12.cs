using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.Enums;
using KDSCommon.UDT;
using KdsShinuyim.DataModels;
using KdsShinuyim.Enums;
using Microsoft.Practices.Unity;

namespace KdsShinuyim.ShinuyImpl
{
    public class ShinuyShatHatchalaLefiShatItyatzvut12 : ShinuyBase
    {

        public ShinuyShatHatchalaLefiShatItyatzvut12(IUnityContainer container)
            : base(container)
        {

        }

        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.ShinuyShatHatchalaLefiShatItyatzvut12; } }


        //bool bFirstHayavHityazvut = false;
        //bool bSecondHayavHityazvut = false;
        //SidurDM oSidurNidrashHityatvut = null;
        //bool bHayavHityazvut = false;
        public override void ExecShinuy(ShinuyInputData inputData)
        {
           
          try{
                for (int i = 0; i < inputData.htEmployeeDetails.Count; i++)
                {
                    SidurDM curSidur = (SidurDM)inputData.htEmployeeDetails[i];
                    FixedShatHatchalaLefiShatItyatzvut12(curSidur,i, inputData);             
                }
          }
          catch (Exception ex)
          {
              throw new Exception("ShinuyShatHatchalaLefiShatItyatzvut12: " + ex.Message);
          }

        }

        private void FixedShatHatchalaLefiShatItyatzvut12(SidurDM curSidur , int i, ShinuyInputData inputData)
        {
            int j, iCountPeiluyot;
            DateTime dShatHatchalaNew;
            bool bUpdateShatHatchala;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd = null;
            SourceObj SourceObject;
            try
            {
                oObjSidurimOvdimUpd = GetUpdSidurObject(curSidur, inputData);
                dShatHatchalaNew = curSidur.dFullShatHatchala;
                bUpdateShatHatchala = false;
                iCountPeiluyot = ((SidurDM)(inputData.htEmployeeDetails[i])).htPeilut.Count;
                j = 0;

                if (iCountPeiluyot > 0)
                {
                    do
                    {
                        PeilutDM oPeilut = (PeilutDM)((SidurDM)(inputData.htEmployeeDetails[i])).htPeilut[j];
                        OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd = GetUpdPeilutObject(i, oPeilut, inputData, oObjSidurimOvdimUpd, out SourceObject);

                        FixedShatHatchalaLefiShatHachtamatItyatzvut12(inputData,oPeilut, curSidur, oObjSidurimOvdimUpd, oObjPeilutOvdimUpd, SourceObject,ref j, ref  bUpdateShatHatchala, ref dShatHatchalaNew);

                        j += 1;
                        iCountPeiluyot = ((SidurDM)(inputData.htEmployeeDetails[i])).htPeilut.Count;
                    }
                    while (j < iCountPeiluyot);
                }

                if (!CheckIdkunRashemet("SHAT_HATCHALA", curSidur.iMisparSidur, curSidur.dFullShatHatchala, inputData))
                {
                    if (bUpdateShatHatchala)
                    {
                        FixedShatHatchalaLefiShatHachtamatItyatzvut12(curSidur, i, dShatHatchalaNew,  oObjSidurimOvdimUpd, inputData);
                    }
                }
             
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FixedShatHatchalaLefiShatHachtamatItyatzvut12(SidurDM curSidur, int iSidurIndex, DateTime dShatHatchalaNew, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, ShinuyInputData inputData)
        { 
            OBJ_PEILUT_OVDIM oObjPeilutUpd;
            PeilutDM oPeilut;
            SourceObj SourceObject;
            try
            {
                if (dShatHatchalaNew != curSidur.dFullShatHatchala)
                {
                    NewSidur oNewSidurim = FindSidurOnHtNewSidurim(curSidur.iMisparSidur, curSidur.dFullShatHatchala, inputData.htNewSidurim);

                    oNewSidurim.SidurIndex = iSidurIndex;
                    oNewSidurim.SidurNew = curSidur.iMisparSidur;
                    oNewSidurim.ShatHatchalaNew = dShatHatchalaNew;

                    UpdateObjectUpdSidurim(oNewSidurim, inputData.oCollSidurimOvdimUpdRecorder);
                    //עדכון שעת התחלה סידור של כל הפעילויות לסידור
                    for (int j = 0; j < curSidur.htPeilut.Count; j++)
                    {
                        oPeilut = (PeilutDM)curSidur.htPeilut[j];
                        if (!CheckPeilutObjectDelete(iSidurIndex, j, inputData))
                        {
                            oObjPeilutUpd = GetUpdPeilutObject(iSidurIndex, oPeilut, inputData,oObjSidurimOvdimUpd, out SourceObject);
                            if (SourceObject == SourceObj.Insert)
                            {
                                oObjPeilutUpd.SHAT_HATCHALA_SIDUR = dShatHatchalaNew;
                            }
                            else
                            {
                                oObjPeilutUpd.NEW_SHAT_HATCHALA_SIDUR = dShatHatchalaNew;
                                oObjPeilutUpd.UPDATE_OBJECT = 1;
                            }

                        }
                    }
                    //UpdatePeiluyotMevutalotYadani(iSidurIndex,oNewSidurim, oObjSidurimOvdimUpd);
                    UpdateIdkunRashemet(curSidur.iMisparSidur, curSidur.dFullShatHatchala, dShatHatchalaNew,inputData);
                    UpdateApprovalErrors(curSidur.iMisparSidur, curSidur.dFullShatHatchala, dShatHatchalaNew,inputData);

                    curSidur.dFullShatHatchala = dShatHatchalaNew;
                    curSidur.sShatHatchala = dShatHatchalaNew.ToString("HH:mm");
                    oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA = dShatHatchalaNew;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FixedShatHatchalaLefiShatHachtamatItyatzvut12(ShinuyInputData inputData, PeilutDM oPeilut, SidurDM oSidur, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd, SourceObj SourceObject, ref int j, ref bool bUpdateShatHatchala, ref DateTime dShatHatchalaNew)
        {
            string sTempTime, sNewTempTime;
            PeilutDM oNextPeilut = null;
            int iTempTime;
            sTempTime = "";
            Double dZmanLekizuz = 0;
            int i, iCountInsPeiluyot;
            OBJ_PEILUT_OVDIM oObjPeilutOvdimDel;
            try
            {
                if (oSidur.htPeilut.Count > j + 1)
                {
                    oNextPeilut = (PeilutDM)oSidur.htPeilut[j + 1];
                }

                if (oObjSidurimOvdimUpd.PTOR_MEHITIATZVUT == 1) return;

                if ((oObjSidurimOvdimUpd.NIDRESHET_HITIATZVUT == 1 || oObjSidurimOvdimUpd.NIDRESHET_HITIATZVUT == 2)
                    && (oObjSidurimOvdimUpd.SHAT_HITIATZVUT > dShatHatchalaNew))
                {
                    dZmanLekizuz = (oObjSidurimOvdimUpd.SHAT_HITIATZVUT - dShatHatchalaNew).TotalMinutes;
                    //יש לקזז/לבטל פעילויות ולתקן את שעת היציאה שלהן עד אשר שעת התחלת הסידור שווה לשעת ההתייצבות:
                    if (oPeilut.dFullShatYetzia < oObjSidurimOvdimUpd.SHAT_HITIATZVUT)
                    {
                        //1. מותר לבטל/לקזז זמנים לנסיעות ריקות (לפי רוטינת זיהוי מקט). 
                        //2. מותר לבטל/לקזז זמנים לאלמנטים שיש להם מאפיין 8 (ביטול בגלל איחור לסידור). 
                        if (oPeilut.iMakatType == enMakatType.mEmpty.GetHashCode() || ((oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && oPeilut.bBitulBiglalIchurLasidurExists)))
                        {
                            bUpdateShatHatchala = true;
                            if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && oPeilut.bBitulBiglalIchurLasidurExists)
                            {
                                sTempTime = oObjPeilutOvdimUpd.MAKAT_NESIA.ToString().Substring(3, 3);
                                iTempTime = int.Parse(sTempTime);
                            }
                            else
                            {
                                iTempTime = oPeilut.iMazanTashlum;
                            }

                            if (iTempTime <= dZmanLekizuz)
                            {
                                dShatHatchalaNew = dShatHatchalaNew.AddMinutes(iTempTime);

                                oObjPeilutOvdimDel = new OBJ_PEILUT_OVDIM();
                                oObjPeilutOvdimDel=InsertToObjPeilutOvdimForDelete(oPeilut, oSidur,inputData);
                                inputData.oCollPeilutOvdimDel.Add(oObjPeilutOvdimDel);
                                //DeleteIdkuneyRashemetLepeilut(oSidur.iMisparSidur, oSidur.dOldFullShatHatchala, oPeilut.dFullShatYetzia, inputData);                      
                                oSidur.htPeilut.RemoveAt(j);
                                j = j - 1;

                                for (i = 0; i <= inputData.oCollPeilutOvdimUpdRecorder.Count - 1; i++)
                                {
                                    if ((inputData.oCollPeilutOvdimUpdRecorder[i].ContainedItem.NEW_MISPAR_SIDUR == oSidur.iMisparSidur) && (inputData.oCollPeilutOvdimUpdRecorder[i].ContainedItem.NEW_SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
                                        && (inputData.oCollPeilutOvdimUpdRecorder[i].ContainedItem.NEW_SHAT_YETZIA == oPeilut.dFullShatYetzia) && (inputData.oCollPeilutOvdimUpdRecorder[i].ContainedItem.MISPAR_KNISA == oPeilut.iMisparKnisa))
                                    {
                                        //oCollPeilutOvdimUpd.Value[i].UPDATE_OBJECT = 0;
                                   //     DeleteIdkuneyRashemetLepeilut(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, inputData);
                                        inputData.oCollPeilutOvdimUpdRecorder.RemoveAt(i);
                                    }
                                }
                                i = 0;
                                iCountInsPeiluyot = inputData.oCollPeilutOvdimIns.Count;
                                if (iCountInsPeiluyot > 0)
                                {
                                    do
                                    {
                                        if ((inputData.oCollPeilutOvdimIns.Value[i].MISPAR_SIDUR == oSidur.iMisparSidur) && (inputData.oCollPeilutOvdimIns.Value[i].SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
                                            && (inputData.oCollPeilutOvdimIns.Value[i].SHAT_YETZIA == oPeilut.dFullShatYetzia) && (inputData.oCollPeilutOvdimIns.Value[i].MISPAR_KNISA == oPeilut.iMisparKnisa))
                                        {
                                         //   DeleteIdkuneyRashemetLepeilut(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, inputData);
                                            inputData.oCollPeilutOvdimIns.RemoveAt(i);
                                            i -= 1;
                                        }
                                        iCountInsPeiluyot = inputData.oCollPeilutOvdimIns.Count;
                                        i += 1;
                                    } while (i < iCountInsPeiluyot);
                                }
                            }
                            else
                            {
                                //במקרה של קיזוז זמנים,  יש לעדכן את הזמן המעודכן בפוזיציות 4-6 של האלמנט.                
                                if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && oPeilut.bBitulBiglalIchurLasidurExists)
                                {
                                    sNewTempTime = (iTempTime - dZmanLekizuz).ToString().PadLeft(3, (char)48);
                                    oObjPeilutOvdimUpd.MAKAT_NESIA = long.Parse(oObjPeilutOvdimUpd.MAKAT_NESIA.ToString().Replace(sTempTime, sNewTempTime));
                                   // oPeilut = CreatePeilut(inputData.iMisparIshi, inputData.CardDate, oPeilut, oObjPeilutOvdimUpd.MAKAT_NESIA, inputData.dtTmpMeafyeneyElements);
                                    UpdatePeilut(inputData.iMisparIshi, inputData.CardDate, oPeilut, oObjPeilutOvdimUpd.MAKAT_NESIA, inputData.dtTmpMeafyeneyElements);
                                    oSidur.htPeilut[j] = oPeilut;
                                }
                                dShatHatchalaNew = dShatHatchalaNew.AddMinutes(dZmanLekizuz);
                                if (oNextPeilut != null)
                                {
                                    if (oNextPeilut.dFullShatYetzia == oPeilut.dFullShatYetzia.AddMinutes(dZmanLekizuz))
                                    {
                                        oObjPeilutOvdimDel = new OBJ_PEILUT_OVDIM();
                                        oObjPeilutOvdimDel = InsertToObjPeilutOvdimForDelete(oPeilut, oSidur, inputData);
                                        inputData.oCollPeilutOvdimDel.Add(oObjPeilutOvdimDel);
                                        oSidur.htPeilut.RemoveAt(j);
                                        j = j - 1;
                                        for (i = 0; i <= inputData.oCollPeilutOvdimUpdRecorder.Count - 1; i++)
                                        {
                                            if ((inputData.oCollPeilutOvdimUpdRecorder[i].ContainedItem.NEW_MISPAR_SIDUR == oSidur.iMisparSidur) && (inputData.oCollPeilutOvdimUpdRecorder[i].ContainedItem.NEW_SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
                                                && (inputData.oCollPeilutOvdimUpdRecorder[i].ContainedItem.NEW_SHAT_YETZIA == oPeilut.dFullShatYetzia) && (inputData.oCollPeilutOvdimUpdRecorder[i].ContainedItem.MISPAR_KNISA == oPeilut.iMisparKnisa))
                                            {
                                                //oCollPeilutOvdimUpd.Value[i].UPDATE_OBJECT = 0;
                                            //    DeleteIdkuneyRashemetLepeilut(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, inputData);
                                                inputData.oCollPeilutOvdimUpdRecorder.RemoveAt(i);
                                            }
                                        }
                                        i = 0;
                                        iCountInsPeiluyot = inputData.oCollPeilutOvdimIns.Count;
                                        if (iCountInsPeiluyot > 0)
                                        {
                                            do
                                            {
                                                if ((inputData.oCollPeilutOvdimIns.Value[i].MISPAR_SIDUR == oSidur.iMisparSidur) && (inputData.oCollPeilutOvdimIns.Value[i].SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
                                                    && (inputData.oCollPeilutOvdimIns.Value[i].SHAT_YETZIA == oPeilut.dFullShatYetzia) && (inputData.oCollPeilutOvdimIns.Value[i].MISPAR_KNISA == oPeilut.iMisparKnisa))
                                                {
                                                  //  DeleteIdkuneyRashemetLepeilut(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, inputData);
                                                    inputData.oCollPeilutOvdimIns.RemoveAt(i);
                                                    i -= 1;
                                                }
                                                iCountInsPeiluyot = inputData.oCollPeilutOvdimIns.Count;
                                                i += 1;
                                            } while (i < iCountInsPeiluyot);
                                        }
                                    }
                                    else
                                    {
                                        if (SourceObject == SourceObj.Insert)
                                        {
                                            oObjPeilutOvdimUpd.SHAT_YETZIA = oPeilut.dFullShatYetzia.AddMinutes(dZmanLekizuz);
                                            oPeilut.dFullShatYetzia = oObjPeilutOvdimUpd.SHAT_YETZIA;

                                        }
                                        else
                                        {
                                            oObjPeilutOvdimUpd.NEW_SHAT_YETZIA = oPeilut.dFullShatYetzia.AddMinutes(dZmanLekizuz);
                                            oObjPeilutOvdimUpd.UPDATE_OBJECT = 1;
                                            UpdateIdkunRashemet(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutOvdimUpd.NEW_SHAT_YETZIA,inputData);
                                            UpdateApprovalErrors(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutOvdimUpd.NEW_SHAT_YETZIA,inputData);

                                            oPeilut.dFullShatYetzia = oObjPeilutOvdimUpd.NEW_SHAT_YETZIA;

                                        }
                                        oPeilut.sShatYetzia = oPeilut.dFullShatYetzia.ToString("HH:mm");
                                    }
                                }
                                else
                                {
                                    if (SourceObject == SourceObj.Insert)
                                    {
                                        oObjPeilutOvdimUpd.SHAT_YETZIA = oPeilut.dFullShatYetzia.AddMinutes(dZmanLekizuz);
                                        oPeilut.dFullShatYetzia = oObjPeilutOvdimUpd.SHAT_YETZIA;
                                    }
                                    else
                                    {
                                        oObjPeilutOvdimUpd.NEW_SHAT_YETZIA = oPeilut.dFullShatYetzia.AddMinutes(dZmanLekizuz);
                                        oObjPeilutOvdimUpd.UPDATE_OBJECT = 1;
                                        UpdateIdkunRashemet(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutOvdimUpd.NEW_SHAT_YETZIA, inputData);
                                        UpdateApprovalErrors(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutOvdimUpd.NEW_SHAT_YETZIA, inputData);

                                        oPeilut.dFullShatYetzia = oObjPeilutOvdimUpd.NEW_SHAT_YETZIA;
                                    }

                                    oPeilut.sShatYetzia = oPeilut.dFullShatYetzia.ToString("HH:mm");
                                }
                            }
                        }
                    }
                    else if (oPeilut.dFullShatYetzia == oObjSidurimOvdimUpd.SHAT_HITIATZVUT)
                        dShatHatchalaNew = dShatHatchalaNew.AddMinutes(dZmanLekizuz);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }
}
