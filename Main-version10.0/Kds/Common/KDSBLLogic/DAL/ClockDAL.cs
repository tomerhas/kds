using DalOraInfra.DAL;
using KDSCommon.Interfaces.DAL;
using KDSCommon.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSBLLogic.DAL
{
    public class ClockDAL : IClockDAL
    {
        public const string cProSaveHarmonyMovment = "Pkg_Attendance.pro_ins_shaonim_movment";

        public void SaveHarmonyMovment(COLL_HARMONY_MOVMENT_ERR_MOV collHarmonyMovment)
        {
            clDal oDal = new clDal();
            try
            {

                oDal.AddParameter("p_coll_Harmony_movment", ParameterType.ntOracleArray, collHarmonyMovment, ParameterDir.pdInput, "COLL_HARMONY_MOVMENT_ERR_MOV");
               
                
                oDal.ExecuteSP(cProSaveHarmonyMovment);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
