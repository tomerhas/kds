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
    public class ShinuySidureyMapaWhithStatusNullLoLetashlum29 : ShinuyBase
    {

        public ShinuySidureyMapaWhithStatusNullLoLetashlum29(IUnityContainer container)
            : base(container)
        {

        }

        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.ShinuySidureyMapaWhithStatusNullLoLetashlum29; } }


        public override void ExecShinuy(ShinuyInputData inputData)
        {
            try
            {
                SiduryMapaWhithStatusNullLoLetashlum29(inputData);
            }
            catch (Exception ex)
            {
                throw new Exception("ShinuySidureyMapaWhithStatusNullLoLetashlum29: " + ex.Message);
            }
        }

        private void SiduryMapaWhithStatusNullLoLetashlum29(ShinuyInputData inputData)
        {
            SidurDM curSidur = null;
            PeilutDM oPeilut = null;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd = null;
            bool bHaveSidurFromMatala = false;
            bool bHaveSidurVisaFromMapa = false;
            int i = 0;
            string oVal;
            try
            {
                if (inputData.htEmployeeDetails != null)
                    for (i = 0; i < inputData.htEmployeeDetails.Count; i++)
                    {
                        curSidur = (SidurDM)inputData.htEmployeeDetails[i];
                        bHaveSidurVisaFromMapa = false;
                        bHaveSidurFromMatala = false;
                        //סידור מפה: אינו מתחיל בספרות 99/
                        //סידור מיוחד שמקורו במטלה מהמפה (באחת הרשומות של הפעילויות בסידור0< TB_peilut_Ovdim. Mispar_matala)/
                        //סידור ויזה שהגיע מהמפה (בפעילות שהיא מסוג מק"ט 5 קיים ערך בשדה MISPAR_VISA
                        if (curSidur.bSidurMyuhad)
                        {
                            for (int j = 0; j < curSidur.htPeilut.Count; j++)
                            {
                                oPeilut = (PeilutDM)curSidur.htPeilut[j];

                                if (oPeilut.lMisparMatala > 0)
                                {
                                    bHaveSidurFromMatala = true;
                                }
                                if (curSidur.bSidurVisaKodExists || curSidur.iSectorVisa == 0 || curSidur.iSectorVisa == 1)
                                {
                                    if (oPeilut.iMakatType == enMakatType.mVisa.GetHashCode() && oPeilut.lMisparVisa > 0)
                                    {
                                        bHaveSidurVisaFromMapa = true;
                                    }
                                }
                            }
                        }

                        if (((!curSidur.bSidurMyuhad && curSidur.iSugSidurRagil != 73) || (curSidur.bSidurMyuhad && bHaveSidurFromMatala) || bHaveSidurVisaFromMapa) && inputData.OvedDetails.iMeasherOMistayeg == -1)
                        {
                            if (!CheckIdkunRashemet("LO_LETASHLUM", curSidur.iMisparSidur, curSidur.dFullShatHatchala, inputData))
                            {
                                oVal = curSidur.iLoLetashlum.ToString();
                                oObjSidurimOvdimUpd = GetUpdSidurObject(curSidur, inputData);

                                curSidur.iLoLetashlum = 1;
                                curSidur.iKodSibaLoLetashlum = 16;
                                oObjSidurimOvdimUpd.LO_LETASHLUM = 1;
                                oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM = 16;
                                oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;

                                inputData.htEmployeeDetails[i] = curSidur;

                                InsertLogSidur(inputData, curSidur.iMisparSidur, curSidur.dFullShatHatchala, oVal, oObjSidurimOvdimUpd.LO_LETASHLUM.ToString(), 29, i, "LO_LETASHLUM");
                  
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