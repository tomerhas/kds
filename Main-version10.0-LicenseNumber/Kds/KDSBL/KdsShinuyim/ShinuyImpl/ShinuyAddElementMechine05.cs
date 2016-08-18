using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.Enums;
using KDSCommon.Helpers;
using KDSCommon.Interfaces.Managers;
using KDSCommon.UDT;
using KdsShinuyim.DataModels;
using KdsShinuyim.Enums;
using Microsoft.Practices.Unity;

namespace KdsShinuyim.ShinuyImpl
{
    public class ShinuyAddElementMechine05 : ShinuyBase
    {

        public ShinuyAddElementMechine05(IUnityContainer container)
            : base(container)
        {

        }

        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.ShinuyAddElementMechine05; } }

        public override void ExecShinuy(ShinuyInputData inputData)
        {
            try
            {
                AddElementMechine05(inputData);
            }
            catch (Exception ex)
            {
                throw new Exception("ShinuyAddElementMechine05: " + ex.Message);
            }
        }

        private void AddElementMechine05(ShinuyInputData inputData)
        {
            SidurDM oSidur;
            int iMeshechHachanotMechona = 0;
            int iMeshechHachanotMechonaNosafot = 0;
            int iNumHachanotMechonaForSidur = 0;
            DateTime dShatYetzia, dShatYetziaFirst;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
            int iIndexFirstElementMachine = 0;
            bool bHaveFirstElementMachine = false;
            int i, iIndexElement;
            iIndexElement = -1;
         
            bool bUsedMazanTichnunInSidur = false;
            //הוספת אלמנט הכנת מכונה אם העובד החליף רכב ולא מופיע אלמנט זה או בתחילת יום
            try
            {
                inputData.bUsedMazanTichnun = false;
                DeletePeiluyotHachanatMechona(inputData);

                for (i = 0; i < inputData.htEmployeeDetails.Count; i++)
                {
                    oSidur = (SidurDM)inputData.htEmployeeDetails[i];
                    dShatYetzia = oSidur.dFullShatHatchala;
                    dShatYetziaFirst = oSidur.dFullShatHatchala;
                    oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur, inputData);
                    iNumHachanotMechonaForSidur = 0;
                    iIndexFirstElementMachine = 0;
                    bHaveFirstElementMachine = false;
                    //אם סידור ראשון ביום קיימים שני מקרים
                    if (iMeshechHachanotMechona == 0)
                    {
                        AddElementMachineForFirstSidur(oSidur, inputData, i, ref dShatYetziaFirst, ref iMeshechHachanotMechona, ref iNumHachanotMechonaForSidur, ref iIndexElement, ref iIndexFirstElementMachine, ref bUsedMazanTichnunInSidur, oObjSidurimOvdimUpd);
                        iIndexFirstElementMachine += 1;
                        if (iIndexElement == 0) bHaveFirstElementMachine = true;
                    }

                    AddElementMachineForNextSidur(oSidur, inputData,ref dShatYetzia, i, iIndexFirstElementMachine, ref iMeshechHachanotMechona, ref iNumHachanotMechonaForSidur, ref iMeshechHachanotMechonaNosafot, ref iIndexElement,  ref bUsedMazanTichnunInSidur, oObjSidurimOvdimUpd);
                    if (!bHaveFirstElementMachine)
                    {
                        dShatYetziaFirst = dShatYetzia;
                    }
                    inputData.htEmployeeDetails[i] = oSidur;
                    if (bUsedMazanTichnunInSidur)
                          inputData.bUsedMazanTichnun = true;
                 
                    if (dShatYetziaFirst != oSidur.dFullShatHatchala && (!CheckIdkunRashemet("SHAT_HATCHALA", oSidur.iMisparSidur, oSidur.dFullShatHatchala, inputData)))
                    {

                        CreateNewSidur(inputData, oSidur, dShatYetziaFirst, oObjSidurimOvdimUpd, i);
                    }
                    else if (dShatYetziaFirst < oSidur.dFullShatHatchala && (CheckIdkunRashemet("SHAT_HATCHALA", oSidur.iMisparSidur, oSidur.dFullShatHatchala, inputData)))
                    {
                        UpdatePeiluyot(inputData, oSidur, dShatYetziaFirst, oObjSidurimOvdimUpd, i);
                    }
                   // inputData.htEmployeeDetails[i] = oSidur;

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdatePeiluyot(ShinuyInputData inputData, SidurDM oSidur, DateTime dShatYetziaFirst, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, int i)
        {
            OBJ_PEILUT_OVDIM oObjPeilutUpd;
            PeilutDM oPeilut;
            int j,  idakot,  iMeshechElement,l, iCountPeiluyotIns;
            SourceObj SourceObject;
            int iMinuts;
            try
            {
                for (j = 0; j < oSidur.htPeilut.Count; j++)
                {
                    oPeilut = (PeilutDM)oSidur.htPeilut[j];

                    if (oPeilut.dFullShatYetzia == dShatYetziaFirst)
                    {
                        iMinuts = int.Parse((oSidur.dFullShatHatchala - dShatYetziaFirst).TotalMinutes.ToString());

                        if (int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3)) > iMinuts)
                        {
                            oObjPeilutUpd = GetUpdPeilutObject(i, oPeilut, inputData, oObjSidurimOvdimUpd, out SourceObject);

                            idakot = FindDuplicatPeiluyot(j, inputData, oSidur.dFullShatHatchala, i, oSidur, oObjSidurimOvdimUpd);
                            if (idakot > 0)
                            {
                                iMeshechElement = int.Parse(oObjPeilutUpd.MAKAT_NESIA.ToString().Substring(3, 3));
                                if (idakot <= iMeshechElement)
                                    oObjPeilutUpd.MAKAT_NESIA = long.Parse(String.Concat("701", (iMeshechElement - idakot).ToString().PadLeft(3, (char)48), "00"));
                                else oObjPeilutUpd.MAKAT_NESIA = long.Parse(String.Concat("701", "000", "00"));
                                oObjPeilutUpd.SHAT_YETZIA = oSidur.dFullShatHatchala.AddMinutes(idakot);
                            }
                            else oObjPeilutUpd.SHAT_YETZIA = oSidur.dFullShatHatchala;

                            oPeilut.dFullShatYetzia = oObjPeilutUpd.SHAT_YETZIA;
                            oPeilut.lMakatNesia = oObjPeilutUpd.MAKAT_NESIA;
                            oPeilut.sShatYetzia = oPeilut.dFullShatYetzia.ToString("HH:mm");
                            iMinuts = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3)) - iMinuts;
                            oObjPeilutUpd.MAKAT_NESIA = long.Parse(string.Concat(oPeilut.lMakatNesia.ToString().Substring(0, 3), iMinuts.ToString().PadLeft(3, (char)48), oPeilut.lMakatNesia.ToString().Substring(6, 2)));
                        }
                        else
                        {
                            oSidur.htPeilut.RemoveAt(j);
                            l = 0;
                            iCountPeiluyotIns = inputData.oCollPeilutOvdimIns.Count;
                            if (l < iCountPeiluyotIns)
                            {
                                do
                                {
                                    if ((inputData.oCollPeilutOvdimIns.Value[l].MISPAR_SIDUR == oSidur.iMisparSidur) && (inputData.oCollPeilutOvdimIns.Value[l].SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
                                            && (inputData.oCollPeilutOvdimIns.Value[l].SHAT_YETZIA == oPeilut.dFullShatYetzia) && (inputData.oCollPeilutOvdimIns.Value[l].MISPAR_KNISA == oPeilut.iMisparKnisa))
                                    {
                                        inputData.oCollPeilutOvdimIns.RemoveAt(l);
                                        l -= 1;
                                    }
                                    l += 1;
                                    iCountPeiluyotIns = inputData.oCollPeilutOvdimIns.Count;
                                } while (l < iCountPeiluyotIns);
                            }
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CreateNewSidur(ShinuyInputData inputData, SidurDM oSidur, DateTime dShatYetziaFirst, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, int i)
        {
            OBJ_PEILUT_OVDIM oObjPeilutUpd;
            PeilutDM oPeilut;
            int j;
            SourceObj SourceObject;
            string oldVal;

            try{
                oldVal = oSidur.dFullShatHatchala.ToString();
               
                NewSidur oNewSidurim = FindSidurOnHtNewSidurim(oSidur.iMisparSidur, oSidur.dFullShatHatchala, inputData.htNewSidurim);

                oNewSidurim.SidurIndex = i;
                oNewSidurim.ShatHatchalaNew = dShatYetziaFirst;
                oNewSidurim.SidurNew = oSidur.iMisparSidur;

                InsertLogSidur(inputData, oSidur.iMisparSidur, dShatYetziaFirst, oldVal, dShatYetziaFirst.ToString(), 5, i, 14, null);

                UpdateObjectUpdSidurim(oNewSidurim, inputData.oCollSidurimOvdimUpdRecorder);
                for (j = 0; j < oSidur.htPeilut.Count; j++)
                {
                    oPeilut = (PeilutDM)oSidur.htPeilut[j];

                    if (!CheckPeilutObjectDelete(i, j, inputData))
                    {
                        oObjPeilutUpd = GetUpdPeilutObject(i, oPeilut, inputData, oObjSidurimOvdimUpd, out SourceObject);
                        if (SourceObject == SourceObj.Insert)
                        {
                            oObjPeilutUpd.SHAT_HATCHALA_SIDUR = oNewSidurim.ShatHatchalaNew;
                        }
                        else
                        {
                            oObjPeilutUpd.NEW_SHAT_HATCHALA_SIDUR = oNewSidurim.ShatHatchalaNew;
                            oObjPeilutUpd.UPDATE_OBJECT = 1;
                        }
                    }

                    InsertLogPeilut(inputData, oPeilut.iPeilutMisparSidur, oNewSidurim.ShatHatchalaNew, oPeilut.dFullShatYetzia, oPeilut.lMakatNesia, oldVal, oNewSidurim.ShatHatchalaNew.ToString(), 5, i, j,14);

                }
                //UpdatePeiluyotMevutalotYadani(i, oNewSidurim, oObjSidurimOvdimUpd);
                UpdateIdkunRashemet(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oNewSidurim.ShatHatchalaNew, inputData);
                UpdateApprovalErrors(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oNewSidurim.ShatHatchalaNew, inputData);

                oSidur.dFullShatHatchala = oNewSidurim.ShatHatchalaNew;
                oSidur.sShatHatchala = oSidur.dFullShatHatchala.ToString("HH:mm");
                oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA = oNewSidurim.ShatHatchalaNew;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DeletePeiluyotHachanatMechona(ShinuyInputData inputData)
        {
            SidurDM oSidur;
            OBJ_PEILUT_OVDIM oObjPeilutDel;
            PeilutDM oPeilut;
            int iCountPeiluyot, j, i, iCountPeiluyotIns, l;
            try{
                //מחיקת כל פעילויות הכנת מכונה 
                for (i = 0; i < inputData.htEmployeeDetails.Count; i++)
                {
                    oSidur = (SidurDM)inputData.htEmployeeDetails[i];
                    j = 0;
                    iCountPeiluyot = oSidur.htPeilut.Count;
                    if (j < iCountPeiluyot)
                    {
                        do
                        {
                            oPeilut = (PeilutDM)oSidur.htPeilut[j];
                            if (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == enElementHachanatMechona.Element701.GetHashCode().ToString() || oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == enElementHachanatMechona.Element712.GetHashCode().ToString() || oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == enElementHachanatMechona.Element711.GetHashCode().ToString())
                            {
                                oObjPeilutDel = InsertToObjPeilutOvdimForDelete(oPeilut, oSidur, inputData);
                                oObjPeilutDel.BITUL_O_HOSAFA = 3;// int.Parse(clGeneral.enBitulOHosafa.BitulAutomat.ToString());
                                inputData.oCollPeilutOvdimDel.Add(oObjPeilutDel);

                                oSidur.htPeilut.RemoveAt(j);
                                InsertLogPeilut(inputData, oPeilut.iPeilutMisparSidur, oSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, oPeilut.lMakatNesia, "", "", 5, i, j, null, "peilut deleted");
                               
                                j -= 1;
                                for (l = 0; l <= inputData.oCollPeilutOvdimUpdRecorder.Count - 1; l++)
                                {
                                    if ((inputData.oCollPeilutOvdimUpdRecorder[l].ContainedItem.NEW_MISPAR_SIDUR == oSidur.iMisparSidur) && (inputData.oCollPeilutOvdimUpdRecorder[l].ContainedItem.NEW_SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
                                        && (inputData.oCollPeilutOvdimUpdRecorder[l].ContainedItem.NEW_SHAT_YETZIA == oPeilut.dFullShatYetzia) && (inputData.oCollPeilutOvdimUpdRecorder[l].ContainedItem.MISPAR_KNISA == oPeilut.iMisparKnisa))
                                    {
                                        //inputData.oCollPeilutOvdimUpd.Value[l].UPDATE_OBJECT = 0;
                                        inputData.oCollPeilutOvdimUpdRecorder.RemoveAt(l);
                                    }
                                }
                                l = 0;
                                iCountPeiluyotIns = inputData.oCollPeilutOvdimIns.Count;
                                if (l < iCountPeiluyotIns)
                                {
                                    do
                                    {
                                        if ((inputData.oCollPeilutOvdimIns.Value[l].MISPAR_SIDUR == oSidur.iMisparSidur) && (inputData.oCollPeilutOvdimIns.Value[l].SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
                                               && (inputData.oCollPeilutOvdimIns.Value[l].SHAT_YETZIA == oPeilut.dFullShatYetzia) && (inputData.oCollPeilutOvdimIns.Value[l].MISPAR_KNISA == oPeilut.iMisparKnisa))
                                        {
                                            inputData.oCollPeilutOvdimIns.RemoveAt(l);
                                            l -= 1;
                                        }
                                        l += 1;
                                        iCountPeiluyotIns = inputData.oCollPeilutOvdimIns.Count;
                                    } while (l < iCountPeiluyotIns);
                                }
                            }
                            j += 1;
                            iCountPeiluyot = oSidur.htPeilut.Count;
                        } while (j < iCountPeiluyot);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddElementMachineForFirstSidur( SidurDM oSidur, ShinuyInputData inputData, int iIndexSidur, ref DateTime dShatYetzia, ref  int iMeshechHachanotMechona, ref int iNumHachanotMechonaForSidur, ref int iIndexElement, ref int iPeilutNesiaIndex,  ref bool bUsedMazanTichnunInSidur,  OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            bool bPeilutNesiaMustBusNumber = false;
            long lOtoNo = 0;

            try
            {
                IsSidurMustHachanatMechonaFirst( oSidur,inputData.oParam.iVisutMustRechevWC, ref bPeilutNesiaMustBusNumber, ref iPeilutNesiaIndex, ref lOtoNo);

                if ((bPeilutNesiaMustBusNumber))
                {
                    AddElementHachanatMechine701( oSidur,inputData, iIndexSidur, ref dShatYetzia, ref iPeilutNesiaIndex, ref iMeshechHachanotMechona, ref iNumHachanotMechonaForSidur, ref iIndexElement,  oObjSidurimOvdimUpd, ref bUsedMazanTichnunInSidur);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void IsSidurMustHachanatMechonaFirst(SidurDM oSidur,int iVisutMustRechevWC,
                                                   ref bool bPeilutNesiaMustBusNumber,
                                                   ref int iPeilutNesiaIndex,
                                                   ref long lOtoNo)
        {
            PeilutDM oPeilut;

            try
            {
                for (int j = 0; j < oSidur.htPeilut.Count; j++)
                {
                    oPeilut = (PeilutDM)oSidur.htPeilut[j];
                    var peilutManager = _container.Resolve<IPeilutManager>();
                    if (peilutManager.IsMustBusNumber(oPeilut,iVisutMustRechevWC))
                    {
                        bPeilutNesiaMustBusNumber = true;
                        iPeilutNesiaIndex = j;
                        lOtoNo = oPeilut.lOtoNo;
                        break;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddElementHachanatMechine701(SidurDM oSidur,ShinuyInputData inputData, int iIndexSidur, ref DateTime dShatYetiza, ref int iPeilutNesiaIndex, ref  int iMeshechHachanotMechona, ref int iNumHachanotMechonaForSidur, ref int iIndexElement,    OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, ref bool bUsedMazanTichnunInSidur)
        {
            OBJ_PEILUT_OVDIM oObjPeilutOvdimIns = new OBJ_PEILUT_OVDIM();
            PeilutDM oPeilut;
            int idakot, iMeshechElement;
            DateTime dRefferenceDate, dShatYetziaPeilut, dShatKisuyTor;
            try
            {
                oPeilut = (PeilutDM)oSidur.htPeilut[iPeilutNesiaIndex];
                InsertToObjPeilutOvdimForInsert(oSidur, oObjPeilutOvdimIns, inputData.UserId);
                // if (!CheckHaveElementHachanatMechona_2(ref oSidur, iPeilutNesiaIndex))
                //  {
                //אם מספר הכנות המכונה (מכל סוג שהוא) שנוספו עד כה ליום העבודה גדול שווה לערך בפרמטר 123 (מכסימום יומי להכנות מכונה) או מספר הכנות המכונה בסידור גדול שווה לערך בפרמטר 124 (מכסימום הכנות מכונה בסידור אחד)- לא מעדכנים זמן לאלמנט. 
                //if (iNumHachanotMechona <inputData.oParam.iPrepareAllMechineTotalMaxTimeInDay || iNumHachanotMechonaForSidur <inputData.oParam.iPrepareAllMechineTotalMaxTimeForSidur)
                oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("701", inputData.oParam.iPrepareFirstMechineMaxTime.ToString().PadLeft(3, (char)48), "00"));
                // else oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("701", "000", "00"));

                dRefferenceDate = DateHelper.GetDateTimeFromStringHour("08:00", oPeilut.dFullShatYetzia);
                dShatKisuyTor = oPeilut.dFullShatYetzia.AddMinutes(-oPeilut.iKisuyTor);
                if (oPeilut.dFullShatYetzia > dRefferenceDate && dShatKisuyTor > dRefferenceDate && (!DateHelper.CheckShaaton(inputData.iSugYom, inputData.CardDate,inputData.SugeyYamimMeyuchadim)))
                {
                    oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("701", inputData.oParam.iPrepareOtherMechineMaxTime.ToString().PadLeft(3, (char)48), "00"));
                    //    dShatYetziaPeilut = dShatYetziaPeilut.AddMinutes(-3);
                }
                oObjPeilutOvdimIns.OTO_NO = oPeilut.lOtoNo;
                oObjPeilutOvdimIns.LICENSE_NUMBER = oPeilut.lLicenseNumber;

                PeilutDM oPeilutNew = CreatePeilut(inputData.iMisparIshi, inputData.CardDate, oObjPeilutOvdimIns, inputData.dtTmpMeafyeneyElements);

                oObjPeilutOvdimIns.BITUL_O_HOSAFA = 4;
                inputData.oCollPeilutOvdimIns.Add(oObjPeilutOvdimIns);
                oPeilutNew.iBitulOHosafa = 4;
                oSidur.htPeilut.Insert(iPeilutNesiaIndex, dShatYetiza.ToString("HH:mm:ss").Replace(":", "") + iPeilutNesiaIndex + 1, oPeilutNew);
                iIndexElement = iPeilutNesiaIndex;
                iPeilutNesiaIndex += 1;

                dShatYetziaPeilut = GetShatHatchalaElementMachine(inputData,iIndexSidur, iPeilutNesiaIndex, oSidur, oPeilutNew, true,  ref bUsedMazanTichnunInSidur);

                //dRefferenceDate = DateHelper.GetDateTimeFromStringHour("08:00", dShatYetziaPeilut);
                //if (dShatYetziaPeilut >= dRefferenceDate && (!clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, _iSugYom, _dCardDate)))
                //{
                //    oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("701", "005", "00"));
                //    dShatYetziaPeilut = dShatYetziaPeilut.AddMinutes(-3);
                //}

                idakot = FindDuplicatPeiluyot(iPeilutNesiaIndex - 1,inputData, dShatYetziaPeilut, iIndexSidur,  oSidur,  oObjSidurimOvdimUpd);
                if (idakot > 0)
                {
                    iMeshechElement = int.Parse(oObjPeilutOvdimIns.MAKAT_NESIA.ToString().Substring(3, 3));
                    if (idakot <= iMeshechElement)
                        oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("701", (iMeshechElement - idakot).ToString().PadLeft(3, (char)48), "00"));
                    else oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("701", "000", "00"));
                    dShatYetziaPeilut = dShatYetziaPeilut.AddMinutes(idakot);
                }
                oObjPeilutOvdimIns.SHAT_YETZIA = dShatYetziaPeilut;

                oPeilutNew.dFullShatYetzia = oObjPeilutOvdimIns.SHAT_YETZIA;
                oPeilutNew.lMakatNesia = oObjPeilutOvdimIns.MAKAT_NESIA;
                oPeilutNew.sShatYetzia = oPeilutNew.dFullShatYetzia.ToString("HH:mm");


                if (iIndexElement == 0) dShatYetiza = oObjPeilutOvdimIns.SHAT_YETZIA;
                iNumHachanotMechonaForSidur += 1;
                iMeshechHachanotMechona += int.Parse(oObjPeilutOvdimIns.MAKAT_NESIA.ToString().Substring(3, 3));

                InsertLogPeilut(inputData, oPeilutNew.iPeilutMisparSidur, oPeilutNew.dFullShatYetzia, oPeilutNew.dFullShatYetzia, oPeilutNew.lMakatNesia, "", "", 5, iIndexSidur, 0, null,"peilut added");

                //  }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddElementMachineForNextSidur(SidurDM oSidur, ShinuyInputData inputData,ref DateTime dShatYetzia, int iSidurIndex, int iIndexFirstElementMachine, ref  int iMeshechHachanotMechona, ref int iNumHachanotMechonaForSidur, ref int iMeshechHachanotMechonaNosafot, ref int iIndexElement, ref bool bUsedMazanTichnunInSidur,  OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            PeilutDM oPeilut;
            SidurDM oLocalSidur;
            DateTime dShatYetziaPeilut;
            int iPeilutNesiaIndex = 0;
            long lOtoNo = 0;
            long lincesNumber = 0;
            int i = 0;
            int iIndexPeilut;
            bool bHavePeilutMustRechev = false;
            int l, iCountPeiluyot;
            bool bAddElementPitzul = false;
            bool bAddElementHamtana = false;
            int j = 0;
            long lMakat;
            try
            {
                //סידור אינו הראשון ביום ויש בו פעילות "דורשת מספר רכב" ואין לפניה אלמנט הכנת מכונה מכל סוג שהוא (701, 711, 712). מחפשים פעילות אחרת שהיא "דורשת מספר רכב" ששעת היציאה שלה קטנה משעת היציאה של הפעילות אותה אנו בודקים.
                //אם הפעילות אותה אנו בודקים היא הראשונה בסידור מחפשים פעילות "דורשת מספר רכב" בסידור קודם. אם בשתי הפעילויות מספר הרכב לא זהה (ערך שאינו null או 0 ובעל 5 ספרות)  אז מוסיפים אלמנט הכנת מכונה (71100000) לפני הפעילות אותה בדקנו.

                l = iIndexFirstElementMachine;
                iCountPeiluyot = oSidur.htPeilut.Count;
                if (iCountPeiluyot > 0 && l < iCountPeiluyot)
                {
                    do
                    {
                        oPeilut = (PeilutDM)oSidur.htPeilut[l];
                        var peilutManager = _container.Resolve<IPeilutManager>();
                        if (peilutManager.IsMustBusNumber(oPeilut,  inputData.oParam.iVisutMustRechevWC))
                        {
                            iPeilutNesiaIndex = l;
                            lOtoNo = oPeilut.lOtoNo;
                            lincesNumber = oPeilut.lLicenseNumber;
                            bHavePeilutMustRechev = false;
                            dShatYetziaPeilut = oPeilut.dFullShatYetzia;

                            for (i = iSidurIndex; i >= 0; i--)
                            {
                                oLocalSidur = (SidurDM)inputData.htEmployeeDetails[i];
                                if (!bHavePeilutMustRechev && !CheckHaveElementHachanatMechona(oSidur, iPeilutNesiaIndex))
                                {
                                    if (iSidurIndex == i)
                                    { iIndexPeilut = iPeilutNesiaIndex - 1; }
                                    else
                                    { iIndexPeilut = oLocalSidur.htPeilut.Count - 1; }
                                    for (j = iIndexPeilut; j >= 0; j--)
                                    {
                                        if (!bHavePeilutMustRechev)
                                        {
                                            oPeilut = (PeilutDM)oLocalSidur.htPeilut[j];
                                            if (peilutManager.IsMustBusNumber(oPeilut, inputData.oParam.iVisutMustRechevWC))
                                            {
                                               /* if((inputData.CardDate< inputData.oParam.dParam319 && oPeilut.lOtoNo != lOtoNo && oPeilut.lOtoNo > 0 && lOtoNo > 0 && oPeilut.lOtoNo.ToString().Length >= 5)
                                                  || (inputData.CardDate >= inputData.oParam.dParam319 && oPeilut.lLicenseNumber != lincesNumber && oPeilut.lLicenseNumber > 0 && lincesNumber > 0 && oPeilut.lLicenseNumber.ToString().Length >= 5))*/
                                                
                                                if (oPeilut.lOtoNo != lOtoNo && oPeilut.lOtoNo > 0 && lOtoNo > 0 && oPeilut.lOtoNo.ToString().Length >= 5)
                                                {
                                                    //אם אין להן אותו מספר רכב אז מוסיפים אלמנט הכנת מכונה (71100000).
                                                    AddElementHachanatMechine711( oSidur,inputData, iSidurIndex, ref dShatYetzia, ref iPeilutNesiaIndex, ref iMeshechHachanotMechona, ref iNumHachanotMechonaForSidur, ref iMeshechHachanotMechonaNosafot, ref  iIndexElement, ref  bUsedMazanTichnunInSidur,  oObjSidurimOvdimUpd);
                                                    inputData.htEmployeeDetails[iSidurIndex] = oSidur;
                                                    if (i == iSidurIndex)
                                                        l += 1;
                                                }
                                                else if (oLocalSidur != oSidur && l == 0 && isPeilutMashmautit((PeilutDM)oSidur.htPeilut[l]) && 
                                                  //**((iputData.CardDate < inputData.oParam.dParam319 && oPeilut.lOtoNo == lOtoNo) || (inputData.CardDate >= inputData.oParam.dParam319 && oPeilut.lLicenseNumber== lincesNumber))
                                                    oPeilut.lOtoNo == lOtoNo && 
                                                    (dShatYetziaPeilut - oLocalSidur.dFullShatGmar).TotalMinutes > inputData.oParam.iMinTimeBetweenSidurim
                                                   && ((dShatYetziaPeilut - oLocalSidur.dFullShatGmar).TotalMinutes - inputData.oParam.iPrepareOtherMechineMaxTime) > inputData.oParam.iMinTimeBetweenSidurim)
                                                {
                                                    AddElementHachanatMechine711( oSidur,inputData, iSidurIndex, ref dShatYetzia, ref iPeilutNesiaIndex, ref iMeshechHachanotMechona, ref iNumHachanotMechonaForSidur, ref iMeshechHachanotMechonaNosafot, ref  iIndexElement, ref  bUsedMazanTichnunInSidur,  oObjSidurimOvdimUpd);
                                                    if (i == iSidurIndex)
                                                        l += 1;
                                                }

                                                bHavePeilutMustRechev = true;
                                                break;

                                            }
                                        }
                                    }

                                    if (!CheckHaveElementHachanatMechona( oSidur, iPeilutNesiaIndex) && !bAddElementPitzul)
                                    {
                                        if (oLocalSidur != oSidur && (oSidur.dFullShatHatchala - oLocalSidur.dFullShatGmar).TotalMinutes > inputData.oParam.iMinTimeBetweenSidurim
                                            && ((oSidur.dFullShatHatchala - oLocalSidur.dFullShatGmar).TotalMinutes - inputData.oParam.iPrepareOtherMechineMaxTime) > inputData.oParam.iMinTimeBetweenSidurim)
                                        {
                                            AddElementHachanatMechine711( oSidur, inputData, iSidurIndex, ref dShatYetzia, ref iPeilutNesiaIndex, ref iMeshechHachanotMechona, ref  iNumHachanotMechonaForSidur, ref iMeshechHachanotMechonaNosafot, ref  iIndexElement, ref bUsedMazanTichnunInSidur,  oObjSidurimOvdimUpd);
                                            inputData.htEmployeeDetails[iSidurIndex] = oSidur;
                                            bAddElementPitzul = true;
                                        }
                                    }

                                    if (!CheckHaveElementHachanatMechona( oSidur, iPeilutNesiaIndex) && !bAddElementHamtana)
                                    {
                                        if (oLocalSidur.htPeilut.Count > 0)
                                        {
                                            lMakat = ((PeilutDM)oLocalSidur.htPeilut[oLocalSidur.htPeilut.Count - 1]).lMakatNesia;
                                            if (oLocalSidur != oSidur && iSidurIndex == i + 1 && (oSidur.dFullShatHatchala - oLocalSidur.dFullShatGmar).TotalMinutes <= inputData.oParam.iMinTimeBetweenSidurim
                                                 && (lMakat.ToString().PadLeft(8).Substring(0, 3) == "724" || lMakat.ToString().PadLeft(8).Substring(0, 3) == "735") && int.Parse(lMakat.ToString().PadLeft(8).Substring(3, 3)) > 60)
                                            {
                                                AddElementHachanatMechine711(oSidur, inputData,iSidurIndex, ref dShatYetzia, ref iPeilutNesiaIndex, ref iMeshechHachanotMechona, ref iNumHachanotMechonaForSidur, ref iMeshechHachanotMechonaNosafot, ref  iIndexElement, ref bUsedMazanTichnunInSidur, oObjSidurimOvdimUpd);
                                                inputData.htEmployeeDetails[iSidurIndex] = oSidur;
                                                bAddElementHamtana = true;
                                            }
                                        }
                                    }
                                }


                            }
                        }
                        l += 1;

                        iCountPeiluyot = oSidur.htPeilut.Count;
                    } while (l < iCountPeiluyot);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CheckHaveElementHachanatMechona(SidurDM oSidur, int iIndexPeilutMustAutoNum)
        {
            bool bHave = false;

            try
            {
                if (iIndexPeilutMustAutoNum > 0)
                {
                    PeilutDM oPeilut = (PeilutDM)oSidur.htPeilut[iIndexPeilutMustAutoNum - 1];
                    if (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == enElementHachanatMechona.Element712.GetHashCode().ToString() || oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == enElementHachanatMechona.Element711.GetHashCode().ToString())
                    { bHave = true; }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bHave;
        }

        private void AddElementHachanatMechine711( SidurDM oSidur, ShinuyInputData inputData,int iIndexSidur, ref DateTime dShatYetiza, ref int iPeilutNesiaIndex, ref int iMeshechHachanotMechona, ref int iNumHachanotMechonaForSidur, ref int iMeshechHachanotMechonaNosafot, ref int iIndexElement, ref bool bUsedMazanTichnunInSidur,  OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            OBJ_PEILUT_OVDIM oObjPeilutOvdimIns = new OBJ_PEILUT_OVDIM();
            PeilutDM oPeilut;
            int idakot, iMeshechElement;
            DateTime dShatYetziaPeilut;
            try
            {
                oPeilut = (PeilutDM)oSidur.htPeilut[iPeilutNesiaIndex];
                InsertToObjPeilutOvdimForInsert(oSidur, oObjPeilutOvdimIns, inputData.UserId);
                if (oSidur.dSidurDate >= inputData.oParam.dTaarichmichsatHachanatMechona)
                {
                    if (iMeshechHachanotMechona < inputData.oParam.iPrepareAllMechineTotalMaxTimeInDay &&
                        iNumHachanotMechonaForSidur < inputData.oParam.iPrepareAllMechineTotalMaxTimeForSidur &&
                        iMeshechHachanotMechonaNosafot < inputData.oParam.iPrepareOtherMechineTotalMaxTime)
                    {
                        oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("711", inputData.oParam.iPrepareOtherMechineMaxTime.ToString().PadLeft(3, (char)48), "00"));
                    }
                    else oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("711", "000", "00"));
                }
                else
                    oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("711", inputData.oParam.iPrepareOtherMechineMaxTime.ToString().PadLeft(3, (char)48), "00"));
               
               // oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("711",inputData.oParam.iPrepareOtherMechineMaxTime.ToString().PadLeft(3, (char)48), "00"));
                oObjPeilutOvdimIns.OTO_NO = oPeilut.lOtoNo;
                oObjPeilutOvdimIns.LICENSE_NUMBER = oPeilut.lLicenseNumber;

                PeilutDM oPeilutNew = CreatePeilut(inputData.iMisparIshi, inputData.CardDate, oObjPeilutOvdimIns, inputData.dtTmpMeafyeneyElements);

                oObjPeilutOvdimIns.BITUL_O_HOSAFA = 4;
                inputData.oCollPeilutOvdimIns.Add(oObjPeilutOvdimIns);
                oPeilutNew.iBitulOHosafa = 4;
                oSidur.htPeilut.Insert(iPeilutNesiaIndex, dShatYetiza.ToString("HH:mm:ss").Replace(":", "") + iPeilutNesiaIndex + 11, oPeilutNew);
                iIndexElement = iPeilutNesiaIndex;
                iPeilutNesiaIndex += 1;

                dShatYetziaPeilut = GetShatHatchalaElementMachine(inputData,iIndexSidur, iPeilutNesiaIndex,  oSidur, (PeilutDM)oSidur.htPeilut[iIndexElement], false, ref bUsedMazanTichnunInSidur);
                idakot = FindDuplicatPeiluyot(iPeilutNesiaIndex - 1,inputData, dShatYetziaPeilut, iIndexSidur,  oSidur,  oObjSidurimOvdimUpd);

                if (idakot > 0)
                {
                    iMeshechElement = int.Parse(oObjPeilutOvdimIns.MAKAT_NESIA.ToString().Substring(3, 3));
                    if (idakot <= iMeshechElement)
                        oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat(oObjPeilutOvdimIns.MAKAT_NESIA.ToString().Substring(0, 3), (iMeshechElement - idakot).ToString().PadLeft(3, (char)48), "00"));
                    else oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat(oObjPeilutOvdimIns.MAKAT_NESIA.ToString().Substring(0, 3), "000", "00"));
                    dShatYetziaPeilut = dShatYetziaPeilut.AddMinutes(idakot);
                }

                oObjPeilutOvdimIns.SHAT_YETZIA = dShatYetziaPeilut;
                oPeilutNew.dFullShatYetzia = oObjPeilutOvdimIns.SHAT_YETZIA;
                oPeilutNew.lMakatNesia = oObjPeilutOvdimIns.MAKAT_NESIA;
                oPeilutNew.sShatYetzia = oPeilutNew.dFullShatYetzia.ToString("HH:mm");
                if (iIndexElement == 0) dShatYetiza = oObjPeilutOvdimIns.SHAT_YETZIA;

                iNumHachanotMechonaForSidur += 1;
                iMeshechHachanotMechona += int.Parse(oObjPeilutOvdimIns.MAKAT_NESIA.ToString().Substring(3, 3));
                iMeshechHachanotMechonaNosafot += int.Parse(oObjPeilutOvdimIns.MAKAT_NESIA.ToString().Substring(3, 3)); ;

                InsertLogPeilut(inputData, oPeilutNew.iPeilutMisparSidur, oPeilutNew.dFullShatYetzia, oPeilutNew.dFullShatYetzia, oPeilutNew.lMakatNesia, "", "", 5, iIndexSidur, 0, null, "peilut added");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DateTime GetShatHatchalaElementMachine(ShinuyInputData inputData,int iIndexSidur, int iPeilutNesiaIndex, SidurDM oSidur, PeilutDM oPeilutMachine, bool bFirstElementMachine, ref bool bUsedMazanTichnunInSidur)
        {
            PeilutDM oNextPeilut, oPeilut, oPeilutRekaFirst, oFirstPeilutMashmautit;
            DateTime dShatHatchala = DateTime.MinValue;
            int i, j, iIndexPeilutMashmautit, iMeshechPeilut;
            DateTime dRefferenceDate;
            string sSugMechona;

            try
            {
                oFirstPeilutMashmautit = null;
                oPeilutRekaFirst = null;
                iIndexPeilutMashmautit = -1;
                for (i = iPeilutNesiaIndex; i <= oSidur.htPeilut.Values.Count - 1; i++)
                {
                    oNextPeilut = (PeilutDM)oSidur.htPeilut[i];

                    //  if (oNextPeilut.iMakatType == enMakatType.mVisa.GetHashCode() || oNextPeilut.iMakatType == enMakatType.mKavShirut.GetHashCode() || oNextPeilut.iMakatType == enMakatType.mNamak.GetHashCode() || (oNextPeilut.iMakatType == enMakatType.mElement.GetHashCode() && (oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != enElementHachanatMechona.Element701.GetHashCode().ToString() && oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != enElementHachanatMechona.Element712.GetHashCode().ToString() && oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != enElementHachanatMechona.Element711.GetHashCode().ToString()) && (oNextPeilut.iElementLeShatGmar > 0 || oNextPeilut.iElementLeShatGmar == -1 || oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "700")))
                    if (isPeilutMashmautit(oNextPeilut))
                    {
                        oFirstPeilutMashmautit = oNextPeilut;
                        iIndexPeilutMashmautit = i;
                        break;
                    }
                }

                sSugMechona = oPeilutMachine.lMakatNesia.ToString().PadLeft(8).Substring(0, 3);

                //קיימת פעילות משמעותית ראשונה ):
                if (oFirstPeilutMashmautit != null)
                {
                    if (iPeilutNesiaIndex == iIndexPeilutMashmautit)
                    {
                        iMeshechPeilut = GetMeshechPeilutHachnatMechona(iIndexSidur, oPeilutMachine, oSidur,inputData,  ref bUsedMazanTichnunInSidur);
                        if (sSugMechona == "711" && iMeshechPeilut == 0)
                            iMeshechPeilut = 1;
                        dShatHatchala = oFirstPeilutMashmautit.dFullShatYetzia.AddMinutes(-(iMeshechPeilut + oFirstPeilutMashmautit.iKisuyTor));
                    }
                    else
                    {

                        dRefferenceDate = DateHelper.GetDateTimeFromStringHour("08:00", oSidur.dFullShatHatchala);
                        j = iIndexPeilutMashmautit - 1;
                        oNextPeilut = (PeilutDM)oSidur.htPeilut[j];
                        while (oNextPeilut.lMakatNesia != oPeilutMachine.lMakatNesia)
                        {
                            //if (oNextPeilut.iMakatType == enMakatType.mEmpty.GetHashCode())
                            //    oPeilutRekaFirst = oNextPeilut;
                            j -= 1;
                            oNextPeilut = (PeilutDM)oSidur.htPeilut[j];
                        }

                        oPeilutRekaFirst = (PeilutDM)oSidur.htPeilut[j + 1];
                        if (sSugMechona == "711" || (sSugMechona == "701" &&
                                                    ((oFirstPeilutMashmautit.dFullShatYetzia <= dRefferenceDate || (oFirstPeilutMashmautit.dFullShatYetzia > dRefferenceDate &&  DateHelper.CheckShaaton(inputData.iSugYom, inputData.CardDate, inputData.SugeyYamimMeyuchadim)) || (oFirstPeilutMashmautit.dFullShatYetzia > dRefferenceDate && oFirstPeilutMashmautit.dFullShatYetzia.AddMinutes(-oFirstPeilutMashmautit.iKisuyTor) <= dRefferenceDate)) &&
                                                     (GetMeshechPeilutHachnatMechona(iIndexSidur, oPeilutRekaFirst, oSidur,inputData,   ref bUsedMazanTichnunInSidur) >inputData.oParam.iMaxZmanRekaAdShmone))
                                                 || ((oFirstPeilutMashmautit.dFullShatYetzia > dRefferenceDate && oFirstPeilutMashmautit.dFullShatYetzia.AddMinutes(-oFirstPeilutMashmautit.iKisuyTor) > dRefferenceDate && !DateHelper.CheckShaaton(inputData.iSugYom, inputData.CardDate,inputData.SugeyYamimMeyuchadim)) &&
                                                     (GetMeshechPeilutHachnatMechona(iIndexSidur, oPeilutRekaFirst, oSidur,inputData,  ref bUsedMazanTichnunInSidur) >inputData.oParam.iMaxZmanRekaNichleletafter8))))
                        {
                            j = iIndexPeilutMashmautit - 1; ;// -1;
                            oNextPeilut = (PeilutDM)oSidur.htPeilut[j];
                            dShatHatchala = oFirstPeilutMashmautit.dFullShatYetzia.AddMinutes(-(GetMeshechPeilutHachnatMechona(iIndexSidur, oPeilutMachine, oSidur,inputData,   ref bUsedMazanTichnunInSidur) + oFirstPeilutMashmautit.iKisuyTor));

                            while (oNextPeilut.lMakatNesia != oPeilutMachine.lMakatNesia)
                            {
                                if (isElemntLoMashmauti(oNextPeilut) || oNextPeilut.iMakatType == enMakatType.mEmpty.GetHashCode())
                                {
                                    iMeshechPeilut = (GetMeshechPeilutHachnatMechona(iIndexSidur, oNextPeilut, oSidur, inputData, ref bUsedMazanTichnunInSidur));
                                    if (sSugMechona == "711" && iMeshechPeilut == 0)
                                        iMeshechPeilut = 1;
                                    dShatHatchala = dShatHatchala.AddMinutes(-iMeshechPeilut);
                                }
                                j -= 1;
                                oNextPeilut = (PeilutDM)oSidur.htPeilut[j];
                            }

                        }
                        else
                        {
                            j = iIndexPeilutMashmautit - 1;
                            oNextPeilut = (PeilutDM)oSidur.htPeilut[j];
                            dShatHatchala = oFirstPeilutMashmautit.dFullShatYetzia.AddMinutes(-(GetMeshechPeilutHachnatMechona(iIndexSidur, oPeilutMachine, oSidur,inputData, ref bUsedMazanTichnunInSidur) + oFirstPeilutMashmautit.iKisuyTor));

                            while (oNextPeilut != oPeilutRekaFirst)
                            {
                                if (isElemntLoMashmauti(oNextPeilut) || oNextPeilut.iMakatType == enMakatType.mEmpty.GetHashCode())
                                    dShatHatchala = dShatHatchala.AddMinutes(-(GetMeshechPeilutHachnatMechona(iIndexSidur, oNextPeilut, oSidur,inputData, ref bUsedMazanTichnunInSidur)));

                                j -= 1;
                                oNextPeilut = (PeilutDM)oSidur.htPeilut[j];
                            }
                            //if (oPeilutRekaFirst.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "709")
                            //    dShatHatchala = dShatHatchala.AddMinutes(-int.Parse(oPeilutRekaFirst.lMakatNesia.ToString().PadLeft(8).Substring(3, 3)));            
                        }
                    }
                }
                else
                {//לא קיימת פעילות משמעותית:

                    for (i = iPeilutNesiaIndex; i <= oSidur.htPeilut.Values.Count - 1; i++)
                    {
                        //: ריקה, אלמנט ללא מאפיין 37.
                        oPeilut = (PeilutDM)oSidur.htPeilut[i];
                        //if ((oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && oPeilut.iElementLeShatGmar == 0) || oPeilut.iMakatType == enMakatType.mEmpty.GetHashCode())
                        if (isElemntLoMashmauti(oPeilut) || oPeilut.iMakatType == enMakatType.mEmpty.GetHashCode())
                        {// יש לעדכן את שעת היציאה של הכנת המכונה לשעת יציאה של הפעילות שאינה משמעותית הראשונה פחות משך הכנת המכונה.
                            iMeshechPeilut = (GetMeshechPeilutHachnatMechona(iIndexSidur, oPeilutMachine, oSidur, inputData, ref bUsedMazanTichnunInSidur));
                            if (iMeshechPeilut == 0)
                                iMeshechPeilut = 1;
                            dShatHatchala = oPeilut.dFullShatYetzia.AddMinutes(-iMeshechPeilut);
                            break;
                        }
                    }

                }
                return dShatHatchala;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int FindDuplicatPeiluyot(int iPeilutNesiaIndex,ShinuyInputData inputData, DateTime dShatYetzia, int iSidurIndex, SidurDM oSidur,  OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            int j;
            PeilutDM oPeilut;
            SourceObj SourceObject;
            OBJ_PEILUT_OVDIM oObjPeilutUpd;
            DateTime tmpDateShatYetzia;
            int iDakot = 0, iMoneKidum = 0;

            try
            {
                for (j = 0; j < oSidur.htPeilut.Count; j++)
                {
                    if (iPeilutNesiaIndex != j)
                    {
                        oPeilut = (PeilutDM)oSidur.htPeilut[j];
                        if (oPeilut.dFullShatYetzia == dShatYetzia)
                        {
                            if (!CheckPeilutObjectDelete(iSidurIndex, j, inputData))
                            {
                                tmpDateShatYetzia = dShatYetzia;
                                while (!CheckIdkunRashemet("SHAT_YETZIA", oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, inputData) && !isPeilutMashmautit(oPeilut) && oPeilut.dFullShatYetzia == tmpDateShatYetzia)
                                {
                                    oObjPeilutUpd = GetUpdPeilutObject(iSidurIndex, oPeilut, inputData, oObjSidurimOvdimUpd, out SourceObject);
                                    if (SourceObject == SourceObj.Insert)
                                    {
                                        oObjPeilutUpd.SHAT_YETZIA = oPeilut.dFullShatYetzia.AddMinutes(1);
                                        oPeilut.dFullShatYetzia = oObjPeilutUpd.SHAT_YETZIA;
                                    }
                                    else
                                    {
                                        oObjPeilutUpd.NEW_SHAT_YETZIA = oPeilut.dFullShatYetzia.AddMinutes(1);
                                        oObjPeilutUpd.UPDATE_OBJECT = 1;
                                        UpdateIdkunRashemet(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutUpd.NEW_SHAT_YETZIA, 0, inputData);
                                        UpdateApprovalErrors(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutUpd.NEW_SHAT_YETZIA, inputData);

                                        oPeilut.dFullShatYetzia = oObjPeilutUpd.NEW_SHAT_YETZIA;
                                    }

                                    oPeilut.sShatYetzia = oPeilut.dFullShatYetzia.ToString("HH:mm");
                                    oSidur.htPeilut[j] = oPeilut;
                                    iMoneKidum++;
                                    j++;
                                    oPeilut = (PeilutDM)oSidur.htPeilut[j];
                                    tmpDateShatYetzia = tmpDateShatYetzia.AddMinutes(1);
                                }
                                if (oPeilut.dFullShatYetzia == tmpDateShatYetzia)
                                {
                                    if (CheckIdkunRashemet("SHAT_YETZIA", oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, inputData) || isPeilutMashmautit(oPeilut))
                                    {
                                        // for (int i = j - 1; i >= 0; i--)
                                        int i = j - 1;
                                        while (iMoneKidum > 0)
                                        {
                                            //if (iPeilutNesiaIndex != i)
                                            //{
                                            oPeilut = (PeilutDM)oSidur.htPeilut[i];
                                            //if (oPeilut.dFullShatYetzia.Subtract(oPeilut.dOldFullShatYetzia).TotalMinutes == 1)
                                            //{
                                            oObjPeilutUpd = GetUpdPeilutObject(iSidurIndex, oPeilut, inputData, oObjSidurimOvdimUpd, out SourceObject);
                                            if (SourceObject == SourceObj.Insert)
                                            {
                                                oObjPeilutUpd.SHAT_YETZIA = oPeilut.dFullShatYetzia.AddMinutes(-1);
                                                oPeilut.dFullShatYetzia = oObjPeilutUpd.SHAT_YETZIA;
                                            }
                                            else
                                            {
                                                oObjPeilutUpd.NEW_SHAT_YETZIA = oPeilut.dFullShatYetzia.AddMinutes(-1);
                                                oObjPeilutUpd.UPDATE_OBJECT = 1;
                                                UpdateIdkunRashemet(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutUpd.NEW_SHAT_YETZIA, 1, inputData);
                                                UpdateApprovalErrors(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutUpd.NEW_SHAT_YETZIA, inputData);

                                                oPeilut.dFullShatYetzia = oObjPeilutUpd.NEW_SHAT_YETZIA;
                                            }
                                            oPeilut.sShatYetzia = oPeilut.dFullShatYetzia.ToString("HH:mm");
                                            oSidur.htPeilut[i] = oPeilut;
                                            iMoneKidum--;
                                            i--;
                                            //}
                                            //}
                                        }
                                        iDakot += 1;
                                        dShatYetzia = dShatYetzia.AddMinutes(1);
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }

                int iMisparSidur = oSidur.iMisparSidur;
                DateTime dShatHatchalaSidur = oSidur.dFullShatHatchala;
                return iDakot;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
