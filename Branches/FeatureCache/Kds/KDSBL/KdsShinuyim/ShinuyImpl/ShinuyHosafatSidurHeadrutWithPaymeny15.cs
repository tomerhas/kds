using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.Enums;
using KDSCommon.Interfaces.DAL;
using KDSCommon.UDT;
using KdsShinuyim.Enums;
using Microsoft.Practices.Unity;

namespace KdsShinuyim.ShinuyImpl
{
    class ShinuyHosafatSidurHeadrutWithPaymeny15 : ShinuyBase
    {
        private const int SIDUR_HEADRUT_BETASHLUM = 99801;
        public ShinuyHosafatSidurHeadrutWithPaymeny15(IUnityContainer container)
            : base(container)
        {

        }
        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.ShinuyHosafatSidurHeadrutWithPaymeny15; } }

        public override void ExecShinuy(ShinuyInputData inputData)
        {
            NewSidurHeadrutWithPaymeny15(inputData);
        }

        private void NewSidurHeadrutWithPaymeny15(ShinuyInputData inputData)
        {
            DataTable dt;
            //לעובדים להם יש מאפיין אישי 63 (משפחה שכולה) , אם סוג יום = 17 (יום הזכרון) ואין להם סידור עבודה אחר באות יום (שאינו מסומן לא לתשלום) , יש לפתוח להם סידור 99801 (העדרות בתשלום יום עבודה) עם שעות מ-0400 – 2800 (כדי שדיווח אחר יצא לשגוי בחפיפה אם ידווח).            
            try
            {

                if ((inputData.oMeafyeneyOved.IsMeafyenExist(63)) && (inputData.iSugYom == enSugYom.ErevYomHatsmaut.GetHashCode()) && (!inputData.bLoLetashlum))
                {
                    if (!IsSidurExits(99801, inputData.htEmployeeDetails))
                    {
                        dt = _container.Resolve<ISidurDAL>().GetMeafyeneySidurById(inputData.CardDate, 99801);
                        OBJ_SIDURIM_OVDIM oObjSidurimOvdimIns = new OBJ_SIDURIM_OVDIM();
                        //InsertToObjSidurimOvdimForInsert(ref oSidur, ref oObjSidurimOvdimIns);
                        oObjSidurimOvdimIns.TAARICH = inputData.CardDate;
                        oObjSidurimOvdimIns.MISPAR_ISHI = inputData.iMisparIshi;
                        oObjSidurimOvdimIns.MISPAR_SIDUR = SIDUR_HEADRUT_BETASHLUM;
                        oObjSidurimOvdimIns.LO_LETASHLUM = 0;
                        oObjSidurimOvdimIns.HASHLAMA = 0;
                        oObjSidurimOvdimIns.PITZUL_HAFSAKA = 0;
                        oObjSidurimOvdimIns.CHARIGA = 0;
                        oObjSidurimOvdimIns.OUT_MICHSA = 0;
                        oObjSidurimOvdimIns.SHAT_HATCHALA = (String.IsNullOrEmpty(dt.Rows[0]["shat_hatchala_muteret"].ToString())) ? inputData.oParam.dSidurStartLimitHourParam1 : DateTime.Parse(inputData.CardDate.ToShortDateString() + " " + DateTime.Parse(dt.Rows[0]["shat_hatchala_muteret"].ToString()).ToLongTimeString());
                        oObjSidurimOvdimIns.SHAT_GMAR = (String.IsNullOrEmpty(dt.Rows[0]["shat_gmar_muteret"].ToString())) ? inputData.oParam.dSidurLimitShatGmar : DateTime.Parse(inputData.CardDate.ToShortDateString() + " " + DateTime.Parse(dt.Rows[0]["shat_gmar_muteret"].ToString()).ToLongTimeString());
                        oObjSidurimOvdimIns.SHAT_HATCHALA_LETASHLUM = oObjSidurimOvdimIns.SHAT_HATCHALA;
                        oObjSidurimOvdimIns.SHAT_GMAR_LETASHLUM = oObjSidurimOvdimIns.SHAT_GMAR;
                        inputData.oCollSidurimOvdimIns.Add(oObjSidurimOvdimIns);
                    }
                }
            }
            catch (Exception ex)
            {
                //clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, iMisparIshi, "E", 15, dCardDate, "NewSidurHeadrutWithPaymeny15: " + ex.Message);
                inputData.IsSuccsess = false;
            }
        }

        private bool IsSidurExits(int mispar_sidur, OrderedDictionary htEmployeeDetails)
        {
            SidurDM oSidur;
            for (int i = 0; i < htEmployeeDetails.Count; i++)
            {
                oSidur = (SidurDM)htEmployeeDetails[i];
                if (oSidur.iMisparSidur == mispar_sidur)
                    return true;
            }
            return false;
        }
    }
}