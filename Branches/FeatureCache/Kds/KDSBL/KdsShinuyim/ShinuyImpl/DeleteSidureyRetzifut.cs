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
    public class DeleteSidureyRetzifut: ShinuyBase
    {

        public DeleteSidureyRetzifut(IUnityContainer container)
            : base(container)
        {

        }

        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.DeleteSidureyRetzifut; } }

        public override void ExecShinuy(ShinuyInputData inputData)
        {
            try
            {
                DeleteSidureyRetzifutFromCollection(inputData);
            }
            catch (Exception ex)
            {
                throw new Exception("DeleteSidureyRetzifut: " + ex.Message);
            }
        }

        private void DeleteSidureyRetzifutFromCollection(ShinuyInputData inputData)
        {
            SidurDM oSidur;
            int iCountSidurim, I;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimDel;
            try
            {
                iCountSidurim = inputData.htEmployeeDetails.Values.Count;
                I = 0;

                if (iCountSidurim > 0)
                {
                    do
                    {
                        oSidur = (SidurDM)inputData.htEmployeeDetails[I];
                        if (oSidur.iMisparSidur == 99500 || oSidur.iMisparSidur == 99501)
                        {
                            oObjSidurimOvdimDel =InsertToObjSidurimOvdimForDelete( oSidur,  inputData);
                            oObjSidurimOvdimDel.UPDATE_OBJECT = 2;
                            inputData.oCollSidurimOvdimDel.Add(oObjSidurimOvdimDel);
                            inputData.htEmployeeDetails.RemoveAt(I);
                            iCountSidurim -= 1;
                            I -= 1;
                        }
                        I += 1;
                    } while (I < iCountSidurim);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}