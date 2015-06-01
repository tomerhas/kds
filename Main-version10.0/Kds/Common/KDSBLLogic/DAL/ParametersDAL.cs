using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KDSCommon.Interfaces.DAL;
using DalOraInfra.DAL;

namespace KdsLibrary.KDSLogic.DAL
{
    public class ParametersDAL : IParametersDAL
    {
        public const string cProGetParameters = "PKG_UTILS.pro_get_parameters_table";
        public DataTable GetKdsParametrs()
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {   //מחזיר טבלת פרמטרים:                 
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProGetParameters, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
