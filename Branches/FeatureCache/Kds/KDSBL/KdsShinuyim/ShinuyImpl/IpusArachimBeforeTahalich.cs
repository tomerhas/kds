using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.UDT;
using KdsShinuyim.Enums;
using Microsoft.Practices.Unity;

namespace KdsShinuyim.ShinuyImpl
{
    public class IpusArachimBeforeTahalich: ShinuyBase
    {

        public IpusArachimBeforeTahalich(IUnityContainer container)
            : base(container)
        {

        }

        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.IpusArachimBeforeTahalich; } }

        public override void ExecShinuy(ShinuyInputData inputData)
        {
            try
            {
                IpusArachimBeforeTchilatTahalich(inputData);
            }
            catch (Exception ex)
            {
                throw new Exception("IpusArachimBeforeTahalich: " + ex.Message);
            }
        }


        private void IpusArachimBeforeTchilatTahalich(ShinuyInputData inputData)
        {
            SidurDM oSidur;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
            try
            {
                for (int i = 0; i < inputData.htEmployeeDetails.Count; i++)
                {
                    oSidur = (SidurDM)inputData.htEmployeeDetails[i];
                    oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur, inputData);
                    if (!CheckIdkunRashemet("LO_LETASHLUM", oSidur.iMisparSidur, oSidur.dFullShatHatchala, inputData))
                    {
                        if (oSidur.iLoLetashlum == 1 && !(oSidur.iKodSibaLoLetashlum == 1 || oSidur.iKodSibaLoLetashlum == 11 || oSidur.iKodSibaLoLetashlum == 20 || oSidur.iKodSibaLoLetashlum == 19))
                        {
                            oSidur.iLoLetashlum = 0;
                            oSidur.iKodSibaLoLetashlum = 0;
                            oObjSidurimOvdimUpd.LO_LETASHLUM = 0;
                            oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM = 0;
                        }
                        if (!CheckIdkunRashemet("PITZUL_HAFSAKA", oSidur.iMisparSidur, oSidur.dFullShatHatchala, inputData))
                        {
                            oObjSidurimOvdimUpd.PITZUL_HAFSAKA = 0;
                            oSidur.sPitzulHafsaka = "0";
                        }
                    }
                    oObjSidurimOvdimUpd.MEZAKE_NESIOT = 0;
                    oObjSidurimOvdimUpd.MEZAKE_HALBASHA = 0;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;

                    // htEmployeeDetails[i] = oSidur;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
