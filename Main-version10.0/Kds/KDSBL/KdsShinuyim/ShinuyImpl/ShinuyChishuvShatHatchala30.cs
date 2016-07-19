using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.Enums;
using KDSCommon.Interfaces.Managers;
using KDSCommon.UDT;
using KdsShinuyim.DataModels;
using KdsShinuyim.Enums;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;


namespace KdsShinuyim.ShinuyImpl
{
    public class ShinuyChishuvShatHatchala30: ShinuyBase
    {

        public ShinuyChishuvShatHatchala30(IUnityContainer container)
            : base(container)
        {

        }

        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.ShinuyChishuvShatHatchala30; } }

        public override void ExecShinuy(ShinuyInputData inputData)
        {
            try
            {
              //  inputData.bUsedMazanTichnun = false;
                for (int i = 0; i < inputData.htEmployeeDetails.Count; i++)
                {
                    inputData.bUsedMazanTichnun = false;
                    SidurDM curSidur = (SidurDM)inputData.htEmployeeDetails[i];
                    if (!CheckIdkunRashemet("SHAT_HATCHALA", curSidur.iMisparSidur, curSidur.dFullShatHatchala, inputData))
                    {
                        ChishuvShatHatchala30(curSidur, i, inputData);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ShinuyChishuvShatHatchala30: " + ex.Message);
            }
        }

        private void ChishuvShatHatchala30(SidurDM curSidur, int iIndexSidur, ShinuyInputData inputData)
        {
            int i,iIndexPeilutMashmautit = -1;
            PeilutDM oPeilut, oFirstPeilutMashmautit;       
            bool bPeilutHachanatMechona = false;
            DateTime dShatHatchala;
            bool bUsedMazanTichnunInSidur = false;
            try
            {
                if (curSidur.htPeilut.Count > 0 && curSidur.iMisparSidur > 1000 && string.IsNullOrEmpty(curSidur.sSidurVisaKod))
                {
                    bPeilutHachanatMechona = HasHachanatMechona(curSidur, inputData);

                    if (!bPeilutHachanatMechona)
                    {
                        //קיימת פעילות משמעותית
                        oFirstPeilutMashmautit = null;
                        for (i = 0; i <= curSidur.htPeilut.Values.Count - 1; i++)
                        {
                            oPeilut = (PeilutDM)curSidur.htPeilut[i];
                            if (isPeilutMashmautit(oPeilut)) //oPeilut.iMakatType == enMakatType.mVisa.GetHashCode() || oNextPeilut.iMakatType == enMakatType.mKavShirut.GetHashCode() || oNextPeilut.iMakatType == enMakatType.mNamak.GetHashCode() || (oNextPeilut.iMakatType == enMakatType.mElement.GetHashCode() && (oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != enElementHachanatMechona.Element701.GetHashCode().ToString() && oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != enElementHachanatMechona.Element712.GetHashCode().ToString() && oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != enElementHachanatMechona.Element711.GetHashCode().ToString()) && (oNextPeilut.iElementLeShatGmar > 0 || oNextPeilut.iElementLeShatGmar == -1 || oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "700")))
                            {
                                oFirstPeilutMashmautit = oPeilut;
                                iIndexPeilutMashmautit = i;
                                break;
                            }
                        }

                        if (oFirstPeilutMashmautit != null)
                        {
                            if (iIndexPeilutMashmautit == 0)
                            {
                                dShatHatchala = oFirstPeilutMashmautit.dFullShatYetzia.AddMinutes(-oFirstPeilutMashmautit.iKisuyTor);
                            }
                            else
                            {
                                dShatHatchala = oFirstPeilutMashmautit.dFullShatYetzia.AddMinutes(-oFirstPeilutMashmautit.iKisuyTor);

                                for (i = iIndexPeilutMashmautit - 1; i >= 0; i--)
                                {
                                    oPeilut = (PeilutDM)curSidur.htPeilut[i];
                                    if ((isElemntLoMashmauti(oPeilut) && string.IsNullOrEmpty(oPeilut.sLoNitzbarLishatGmar)) || oPeilut.iMakatType == enMakatType.mEmpty.GetHashCode())
                                        dShatHatchala = dShatHatchala.AddMinutes(-(GetMeshechPeilutHachnatMechona(iIndexSidur, oPeilut, curSidur, inputData, ref bUsedMazanTichnunInSidur)));
                                    //if (bUsedMazanTichnunInSidur)
                                    //    inputData.bUsedMazanTichnun = true;   
                                }
                            }
                        }
                        else
                        {
                            oPeilut = (PeilutDM)curSidur.htPeilut[0];
                            dShatHatchala = oPeilut.dFullShatYetzia;
                        }

                        UpdateShatHatchala(curSidur, iIndexSidur, dShatHatchala, inputData);
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool HasHachanatMechona(SidurDM curSidur, ShinuyInputData inputData)
        {
            int i = 0, indexPeilutMechona = -1;
            bool bPeilutHachanatMechona = false;
            long oMakatType, otoNum,licenseNumber;
            PeilutDM oPeilut;
            try
            {
                while (indexPeilutMechona < 0 && i < curSidur.htPeilut.Count)
                {
                    oPeilut = (PeilutDM)curSidur.htPeilut[i];
                    oMakatType = oPeilut.lMakatNesia;
                    if (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "701" || oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "712" || oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "711")
                        indexPeilutMechona = i;
                    i++;
                }
                if (indexPeilutMechona == 0)
                    bPeilutHachanatMechona = true;
                else if (indexPeilutMechona > 0)
                {
                    oPeilut = (PeilutDM)curSidur.htPeilut[0];
                    var peilutManager = ServiceLocator.Current.GetInstance<IPeilutManager>();
                    if (peilutManager.IsMustBusNumber(oPeilut, inputData.oParam.iVisutMustRechevWC) && !isPeilutMashmautit(oPeilut))
                    {
                        oPeilut = (PeilutDM)curSidur.htPeilut[indexPeilutMechona];
                        otoNum = oPeilut.lOtoNo;
                        licenseNumber = oPeilut.lLicenseNumber;
                        i = indexPeilutMechona - 1;
                        while (i >= 0)
                        {
                            oPeilut = (PeilutDM)curSidur.htPeilut[i];
                            if (peilutManager.IsMustBusNumber(oPeilut, inputData.oParam.iVisutMustRechevWC) && oPeilut.lOtoNo != otoNum)
                              //**  ((inputData.CardDate < inputData.oParam.dParam319 && oPeilut.lOtoNo != otoNum) || (inputData.CardDate >= inputData.oParam.dParam319 && oPeilut.lLicenseNumber != licenseNumber))
                                break;
                            i--;
                        }
                        if (i == -1)
                            bPeilutHachanatMechona = true;
                    }
                }

                return bPeilutHachanatMechona;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateShatHatchala(SidurDM curSidur, int iSidurIndex, DateTime dShatHatchalaNew,ShinuyInputData inputData)
        {
            OBJ_PEILUT_OVDIM oObjPeilutUpd;
            PeilutDM oPeilut;
            SourceObj SourceObject;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
            string oldVal;
            try
            {
                if (dShatHatchalaNew != curSidur.dFullShatHatchala)
                {
                    oldVal=curSidur.dFullShatHatchala.ToString();
                    InsertLogSidur(inputData, curSidur.iMisparSidur, dShatHatchalaNew, oldVal, dShatHatchalaNew.ToString(), 30, iSidurIndex, 14);

                    oObjSidurimOvdimUpd = GetUpdSidurObject(curSidur, inputData);
                    NewSidur oNewSidurim = FindSidurOnHtNewSidurim(curSidur.iMisparSidur, curSidur.dFullShatHatchala, inputData.htNewSidurim);

                    oNewSidurim.SidurIndex = iSidurIndex;
                    oNewSidurim.SidurNew = curSidur.iMisparSidur;
                    oNewSidurim.ShatHatchalaNew = dShatHatchalaNew;

                    UpdateObjectUpdSidurim(oNewSidurim, inputData.oCollSidurimOvdimUpdRecorder);

                    //עדכון שעת התחלה סידור של כל הפעילויות לסידור
                    for (int j = 0; j < curSidur.htPeilut.Count; j++)
                    {
                        oPeilut = (PeilutDM)curSidur.htPeilut[j];
                       
                        if (!CheckPeilutObjectDelete(iSidurIndex, j,inputData))
                        {
                            
                            oObjPeilutUpd = GetUpdPeilutObject(iSidurIndex, oPeilut, inputData, oObjSidurimOvdimUpd, out SourceObject);
                                      
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

                        InsertLogPeilut(inputData, oPeilut.iPeilutMisparSidur, dShatHatchalaNew, oPeilut.dFullShatYetzia,oPeilut.lMakatNesia, oldVal,  dShatHatchalaNew.ToString(), 30, iSidurIndex, j,14);

                    }
                    //UpdatePeiluyotMevutalotYadani(iSidurIndex,oNewSidurim, oObjSidurimOvdimUpd);
                    UpdateIdkunRashemet(curSidur.iMisparSidur, curSidur.dFullShatHatchala, dShatHatchalaNew, inputData);
                    UpdateApprovalErrors(curSidur.iMisparSidur, curSidur.dFullShatHatchala, dShatHatchalaNew, inputData);

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
    }
}