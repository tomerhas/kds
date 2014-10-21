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
    public class DeleteElementRechev07: ShinuyBase
    {

        public DeleteElementRechev07(IUnityContainer container)
            : base(container)
        {

        }
        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.DeleteElementRechev07; } }

        public override void ExecShinuy(ShinuyInputData inputData)
        {
            int iCountPeiluyot = 0;
            int j = 0;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd = null;
            OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd;
            SourceObj SourceObject;
            try
            {
                for (int i = 0; i < inputData.htEmployeeDetails.Count; i++)
                {
                    SidurDM curSidur = (SidurDM)inputData.htEmployeeDetails[i];
                    oObjSidurimOvdimUpd = GetUpdSidurObject(curSidur, inputData);
                    iCountPeiluyot = ((SidurDM)(inputData.htEmployeeDetails[i])).htPeilut.Count;
                    j = 0;

                    if (iCountPeiluyot > 0)
                    {
                        do
                        {
                            PeilutDM oPeilut = (PeilutDM)((SidurDM)(inputData.htEmployeeDetails[i])).htPeilut[j];
                            oObjPeilutOvdimUpd = GetUpdPeilutObject(i, oPeilut, inputData, oObjSidurimOvdimUpd, out SourceObject);

                            DeleteElementRechev(curSidur, oPeilut, j, oObjSidurimOvdimUpd, inputData);
                            //ChangeElement06(ref oPeilut, ref oSidur, j);

                            j += 1;
                            iCountPeiluyot = ((SidurDM)(inputData.htEmployeeDetails[i])).htPeilut.Count;
                        }
                        while (j < iCountPeiluyot);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DeleteElementRechev07: " + ex.Message);
            }
        }

        private void DeleteElementRechev(SidurDM oSidur, PeilutDM oPeilut, int iIndexPeilut, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, ShinuyInputData inputData)
        {
            //ביטול אלמנט השאלת רכב
            //תמיד לבטל  אלמנט "השאלת רכב" (73800000). 
            //אלא אם זוהי פעילות יחידה בסידור – לא לבטל אלא לסמן את הסידור "לא לתשלום" .
            //int i, iCountInsPeiluyot;
            try
            {
                if (oPeilut.lMakatNesia.ToString().Length >= 3)
                {
                    if (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "738")
                    {
                        //אם זוהי פעילות יחידה בסידור – לא לבטל אלא לסמן את הסידור "לא לתשלום
                        if (oSidur.htPeilut.Count > 1)
                        {   
                            OBJ_PEILUT_OVDIM oObjPeilutOvdimDel =  InsertToObjPeilutOvdimForDelete( oPeilut,  oSidur, inputData);
                            inputData.oCollPeilutOvdimDel.Add(oObjPeilutOvdimDel);
                            oSidur.htPeilut.RemoveAt(iIndexPeilut);
                            iIndexPeilut = iIndexPeilut - 1;

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