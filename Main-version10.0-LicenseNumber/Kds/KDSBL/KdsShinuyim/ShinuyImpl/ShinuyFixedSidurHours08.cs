using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.Enums;
using KDSCommon.Helpers;
using KDSCommon.UDT;
using KdsShinuyim.DataModels;
using KdsShinuyim.Enums;
using Microsoft.Practices.Unity;

namespace KdsShinuyim.ShinuyImpl
{
    public class ShinuyFixedSidurHours08 : ShinuyBase
    {

        public ShinuyFixedSidurHours08(IUnityContainer container)
            : base(container)
        {

        }

        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.ShinuyFixedSidurHours08; } }

        public override void ExecShinuy(ShinuyInputData inputData)
        {
            //תיקון שעות בחפיפה בין סידורים
            SidurDM oSidurPrev;
            DateTime datePrevShatGmar;
            DateTime dateCurrShatHatchala;
            SidurDM curSidur;
            OBJ_SIDURIM_OVDIM oPrevObjSidurimOvdimUpd;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
            int i;
            try
            {
                for (i = inputData.htEmployeeDetails.Count - 1; i > 0; i--)
                {
                    curSidur = (SidurDM)inputData.htEmployeeDetails[i];
                    //נקרא את נתוני הסידור הקודם
                    oSidurPrev = (SidurDM)inputData.htEmployeeDetails[i - 1];

                    if (curSidur.iLoLetashlum == 0 && oSidurPrev.iLoLetashlum == 0 &&
                      (!curSidur.bSidurMyuhad || (curSidur.bSidurMyuhad && IsMatala(curSidur))) &&
                       (!oSidurPrev.bSidurMyuhad || (oSidurPrev.bSidurMyuhad && IsMatala(oSidurPrev))))
                    {
                        oObjSidurimOvdimUpd = GetUpdSidurObject(curSidur, inputData);
                        oPrevObjSidurimOvdimUpd = GetSidurOvdimObject(oSidurPrev.iMisparSidur, oSidurPrev.dFullShatHatchala,inputData);

                        if ((oPrevObjSidurimOvdimUpd.SHAT_GMAR != DateTime.MinValue) && (oObjSidurimOvdimUpd.SHAT_HATCHALA.Year > DateHelper.cYearNull))
                        {
                            datePrevShatGmar = oPrevObjSidurimOvdimUpd.SHAT_GMAR;
                            dateCurrShatHatchala = oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA;
                            if (dateCurrShatHatchala < datePrevShatGmar)
                            {//קיימת חפיפה בין הסידורים 
                                dateCurrShatHatchala = ChangeShatHatchala(inputData, datePrevShatGmar, dateCurrShatHatchala, curSidur, oObjSidurimOvdimUpd, i);
                            }

                            if (dateCurrShatHatchala < datePrevShatGmar)
                            {
                                 ChangeShatGmar(inputData, oSidurPrev, datePrevShatGmar, dateCurrShatHatchala, oPrevObjSidurimOvdimUpd, i);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ShinuyFixedSidurHours08: " + ex.Message);
            }
        }

        private bool IsMatala(SidurDM oSidur)
        {
            PeilutDM oPeilut;
            if (oSidur.bSidurMyuhad)
            {
                for (int j = 0; j < oSidur.htPeilut.Count; j++)
                {
                    oPeilut = (PeilutDM)oSidur.htPeilut[j];

                    if (oPeilut.lMisparMatala > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private DateTime ChangeShatHatchala(ShinuyInputData inputData, DateTime datePrevShatGmar, DateTime dateCurrShatHatchala, SidurDM curSidur, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, int i)
        {
            OBJ_PEILUT_OVDIM oObjPeilutUpd;
            PeilutDM oPeilut;
            SourceObj SourceObject;
            string oldVal;
            try
            {
                if (!CheckIdkunRashemet("SHAT_HATCHALA", curSidur.iMisparSidur, curSidur.dFullShatHatchala, inputData))
                {
                    if (curSidur.htPeilut.Values.Count > 0)
                    {
                        oPeilut = (PeilutDM)curSidur.htPeilut[0];
                        if (oPeilut.dFullShatYetzia.AddMinutes(-oPeilut.iKisuyTor) > curSidur.dFullShatHatchala)
                        {
                            oldVal = curSidur.dFullShatHatchala.ToString();
                            NewSidur oNewSidurim = FindSidurOnHtNewSidurim(curSidur.iMisparSidur, curSidur.dFullShatHatchala, inputData.htNewSidurim);

                            oNewSidurim.SidurIndex = i;
                            oNewSidurim.SidurNew = curSidur.iMisparSidur;
                            oNewSidurim.ShatHatchalaNew = curSidur.dFullShatHatchala.AddMinutes(Math.Min(Math.Abs((datePrevShatGmar - dateCurrShatHatchala).TotalMinutes), (oPeilut.dFullShatYetzia.AddMinutes(-oPeilut.iKisuyTor) - curSidur.dFullShatHatchala).TotalMinutes));

                            InsertLogSidur(inputData, curSidur.iMisparSidur, curSidur.dFullShatHatchala, oldVal.ToString(), oNewSidurim.ShatHatchalaNew.ToString(), 8, i, 14, null);

                            UpdateObjectUpdSidurim(oNewSidurim, inputData.oCollSidurimOvdimUpdRecorder);
                            for (int j = 0; j < curSidur.htPeilut.Count; j++)
                            {
                                oPeilut = (PeilutDM)curSidur.htPeilut[j];

                                if (!CheckPeilutObjectDelete(i, j, inputData))
                                {

                                    oObjPeilutUpd = GetUpdPeilutObject(i, oPeilut, inputData, oObjSidurimOvdimUpd, out SourceObject);

                                    InsertLogPeilut(inputData, curSidur.iMisparSidur, curSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, oPeilut.lMakatNesia, curSidur.dFullShatHatchala.ToString(), oNewSidurim.ShatHatchalaNew.ToString(), 8,i, j, 14, null);
                                   
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

                            }
                            //UpdatePeiluyotMevutalotYadani(i,oNewSidurim, oObjSidurimOvdimUpd);
                            UpdateIdkunRashemet(curSidur.iMisparSidur, curSidur.dFullShatHatchala, oNewSidurim.ShatHatchalaNew, inputData);
                            UpdateApprovalErrors(curSidur.iMisparSidur, curSidur.dFullShatHatchala, oNewSidurim.ShatHatchalaNew, inputData);

                            curSidur.dFullShatHatchala = oNewSidurim.ShatHatchalaNew;
                            curSidur.sShatHatchala = curSidur.dFullShatHatchala.ToString("HH:mm");    
                            oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA = oNewSidurim.ShatHatchalaNew;

                            dateCurrShatHatchala = oNewSidurim.ShatHatchalaNew;

                        }
                    }
                }
                return dateCurrShatHatchala;
            }
            catch (Exception ex)
            {
                 throw ex;
            }
        }

        private void ChangeShatGmar(ShinuyInputData inputData, SidurDM oSidurPrev, DateTime datePrevShatGmar, DateTime dateCurrShatHatchala, OBJ_SIDURIM_OVDIM oPrevObjSidurimOvdimUpd, int i)
        {
            PeilutDM oPeilut;
            string oldVal;
            try{
                if (!CheckIdkunRashemet("SHAT_GMAR", oSidurPrev.iMisparSidur, oSidurPrev.dFullShatHatchala, inputData))
                {
                    if (oSidurPrev.htPeilut.Values.Count > 0)
                    {
                        oldVal = oSidurPrev.dFullShatGmar.ToString();
                        oPeilut = (PeilutDM)oSidurPrev.htPeilut[oSidurPrev.htPeilut.Values.Count - 1];

                        oPrevObjSidurimOvdimUpd.SHAT_GMAR = oSidurPrev.dFullShatGmar.AddMinutes(-(Math.Min(Math.Abs((datePrevShatGmar - dateCurrShatHatchala).TotalMinutes), Math.Abs((oSidurPrev.dFullShatGmar - oPeilut.dFullShatYetzia).TotalMinutes))));
                        oPrevObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                        oSidurPrev.dFullShatGmar = oPrevObjSidurimOvdimUpd.SHAT_GMAR;
                        oSidurPrev.sShatGmar = oSidurPrev.dFullShatGmar.ToString("HH:mm");

                        InsertLogSidur(inputData, oSidurPrev.iMisparSidur, oSidurPrev.dFullShatHatchala, oldVal.ToString(), oSidurPrev.dFullShatGmar.ToString(), 8, i, 15, null);

                       
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