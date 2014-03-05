using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.Enums;
using KDSCommon.Helpers;
using KDSCommon.Interfaces.DAL;
using KDSCommon.UDT;
using KdsShinuyim.Enums;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace KdsShinuyim.ShinuyImpl
{
    public class ShinuyMergerSiduryMapa28 : ShinuyBase
    {
        public ShinuyMergerSiduryMapa28(IUnityContainer container)
        : base(container)
        {

        }

        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.ShinuyMergerSiduryMapa28; } }


        public override void ExecShinuy(ShinuyInputData inputData)
        {
            MergerSiduryMapa28(inputData);
        }

        private void MergerSiduryMapa28(ShinuyInputData inputData)
        {
            SidurDM curSidur, oSidurPutzal;
            bool bHaveSidur;
            int l, I, iCountSidurim;
            List<SidurDM> ListSidurim;
         
            try
            {
                iCountSidurim = inputData.htEmployeeDetails.Values.Count;
                I = 0;

                if (iCountSidurim > 0)
                {
                    do
                    {
                        curSidur = (SidurDM)inputData.htEmployeeDetails[I];
                        bHaveSidur = false;
                        
                        if (!curSidur.bSidurMyuhad)
                        {
                            //אם מזהים בטבלת סידורים עובדים TB_SIDURIM_OVDIM עבור מ.א. + תאריך את אותו מס' סידור עם שעת התחלה שונה אז בודקים האם זה סידור שפוצל.
                            oSidurPutzal = curSidur;

                            ListSidurim = inputData.htEmployeeDetails.Values.Cast<SidurDM>().ToList().FindAll(Sidur => (Sidur.dFullShatHatchala < curSidur.dFullShatHatchala)).ToList();
                            if (ListSidurim.Count > 1)
                            {
                                ListSidurim.Sort(delegate(SidurDM first, SidurDM second)
                                {
                                    return first.dFullShatHatchala.CompareTo(second.dFullShatHatchala);
                                });
                            }
                            if (ListSidurim.Count > 0)
                            {
                                l = ListSidurim.Count - 1;
                                oSidurPutzal = (SidurDM)ListSidurim[l];

                                if (oSidurPutzal.iMisparSidur == curSidur.iMisparSidur)
                                    bHaveSidur = true;

                                if (bHaveSidur && oSidurPutzal.htPeilut.Values.Count > 0 && curSidur.htPeilut.Values.Count > 0)
                                {
                                    MargeSidurim(inputData, curSidur, oSidurPutzal, ref  iCountSidurim, ref  I); //?
                                }
                            }
                        }
                        I += 1;
                    } while (I < iCountSidurim);
                }

            }
            catch (Exception ex)
            {
               // clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, _iMisparIshi, "E", 28, _dCardDate, "MergerSiduryMapa28: " + ex.Message);
                inputData.IsSuccsess = false;
            }
        }

        private void MargeSidurim(ShinuyInputData inputData, SidurDM curSidur, SidurDM oSidurPutzal, ref int iCountSidurim, ref int I)
        {
            PeilutDM oLastPeilut, oFirstPeilut;
            bool bSidurOkev = false; ;
            int iResult;
            DataSet dsSidur;
           

            try
            {
                oLastPeilut = (PeilutDM)oSidurPutzal.htPeilut[oSidurPutzal.htPeilut.Count - 1];// oSidurPutzal.htPeilut.Values.Cast<PeilutDM>().ToList().LastOrDefault(peilut => (peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "756"));
                oFirstPeilut = curSidur.htPeilut.Values.Cast<PeilutDM>().ToList().FirstOrDefault(peilut => (peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != enElementHachanatMechona.Element701.GetHashCode().ToString() && peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != enElementHachanatMechona.Element712.GetHashCode().ToString() && peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != enElementHachanatMechona.Element711.GetHashCode().ToString() && peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "756"));

                if (oFirstPeilut != null && oLastPeilut != null)
                {
                    var kavimDal = ServiceLocator.Current.GetInstance<IKavimDAL>();
                    dsSidur = kavimDal.GetSidurAndPeiluyotFromTnua(oSidurPutzal.iMisparSidur, oSidurPutzal.dSidurDate, 1, out iResult);
                    if (iResult == 0)
                    {
                        bSidurOkev = IsSidurOkev(curSidur, dsSidur, oLastPeilut, oFirstPeilut);
                    }
                }

                if (bSidurOkev)
                {
                     Marge(inputData, curSidur, oSidurPutzal);
                    inputData.htEmployeeDetails.RemoveAt(I);
                    iCountSidurim -= 1;
                    I -= 1;
                }
            }
            catch (Exception ex)
            {
                //  clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 2, _iMisparIshi, _dCardDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "FixedMisparMatalatVisa02: " + ex.Message, null);
                throw ex;
            }
        }

        private void Marge(ShinuyInputData inputData, SidurDM curSidur, SidurDM oSidurPutzal)
        {
            int iCountPeiluyot;
            bool bCancelHachanatMechona;
            string sMakat;

            try
            {
                OBJ_SIDURIM_OVDIM oObjSidurimOvdimDel = InsertToObjSidurimOvdimForDelete(curSidur, inputData);
                new OBJ_SIDURIM_OVDIM();

                inputData.oCollSidurimOvdimDel.Add(oObjSidurimOvdimDel);
                bCancelHachanatMechona = false;
                iCountPeiluyot = curSidur.htPeilut.Values.Count;
                int j = 0;

                if (iCountPeiluyot > 0)
                {
                    do
                    {
                        PeilutDM oPeilut = (PeilutDM)curSidur.htPeilut[j];
                        sMakat = oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3);

                        OBJ_PEILUT_OVDIM oObjPeilutOvdimIns = new OBJ_PEILUT_OVDIM();
                        oObjPeilutOvdimIns = InsertToObjPeilutOvdimForUpdate(oPeilut, oObjSidurimOvdimDel, inputData.UserId);
                        oObjPeilutOvdimIns.MISPAR_SIDUR = oSidurPutzal.iMisparSidur;
                        oObjPeilutOvdimIns.SHAT_HATCHALA_SIDUR = oSidurPutzal.dFullShatHatchala;
                        oObjPeilutOvdimIns.BITUL_O_HOSAFA = 4;
                        CopyPeilutToObj( oObjPeilutOvdimIns,  oPeilut);//??
                        inputData.oCollPeilutOvdimIns.Add(oObjPeilutOvdimIns);

                        PeilutDM oPeilutNew = CreatePeilut(inputData.iMisparIshi, inputData.CardDate, oPeilut, oPeilut.lMakatNesia, inputData.dtTmpMeafyeneyElements);
                        oPeilutNew.iBitulOHosafa = 4;
                        oPeilutNew.iPeilutMisparSidur = oSidurPutzal.iMisparSidur;
                        oSidurPutzal.htPeilut.Add(28 * oSidurPutzal.htPeilut.Count + 1, oPeilut);


                        OBJ_PEILUT_OVDIM oObjPeilutOvdimDel = InsertToObjPeilutOvdimForDelete(oPeilut, curSidur, inputData);
                        inputData.oCollPeilutOvdimDel.Add(oObjPeilutOvdimDel);
                        curSidur.htPeilut.RemoveAt(j);

                        iCountPeiluyot -= 1;

                    }
                    while (j < iCountPeiluyot);
                }
               
            }
            catch (Exception ex)
            {
                //  clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 2, _iMisparIshi, _dCardDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "FixedMisparMatalatVisa02: " + ex.Message, null);
                throw ex;
            }
        }

        private bool IsSidurOkev(SidurDM curSidur, DataSet dsSidur, PeilutDM oLastPeilut, PeilutDM oFirstPeilut)
        {
            string sShaa;
            DateTime dShatYetzia;
            long lMakatNesia;
            try
            {
                for (int i = 0; i < dsSidur.Tables[1].Rows.Count; i++)
                {

                    sShaa = dsSidur.Tables[1].Rows[i]["SHAA"].ToString();
                    lMakatNesia = long.Parse(dsSidur.Tables[1].Rows[i]["MAKAT8"].ToString());
                    dShatYetzia = DateHelper.GetDateTimeFromStringHour(sShaa, curSidur.dFullShatHatchala);
                    if (oLastPeilut.lMakatNesia == lMakatNesia && oLastPeilut.dFullShatYetzia == dShatYetzia && i + 1 < dsSidur.Tables[1].Rows.Count)
                    {
                        lMakatNesia = long.Parse(dsSidur.Tables[1].Rows[i + 1]["MAKAT8"].ToString());
                        sShaa = dsSidur.Tables[1].Rows[i + 1]["SHAA"].ToString();

                        if ((lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == enElementHachanatMechona.Element712.GetHashCode().ToString() || lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == enElementHachanatMechona.Element711.GetHashCode().ToString()) && i + 2 < dsSidur.Tables[1].Rows.Count)
                        {
                            lMakatNesia = long.Parse(dsSidur.Tables[1].Rows[i + 2]["MAKAT8"].ToString());

                            sShaa = dsSidur.Tables[1].Rows[i + 2]["SHAA"].ToString();
                        }

                        dShatYetzia = DateHelper.GetDateTimeFromStringHour(sShaa, curSidur.dFullShatHatchala);
                        if (oFirstPeilut.lMakatNesia == lMakatNesia && oFirstPeilut.dFullShatYetzia == dShatYetzia)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                //  clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 2, _iMisparIshi, _dCardDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "FixedMisparMatalatVisa02: " + ex.Message, null);
                throw ex;
            }
        }
    }
}
