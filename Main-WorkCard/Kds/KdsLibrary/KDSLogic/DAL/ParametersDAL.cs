using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;
using KDSCommon.Interfaces.DAL;

namespace KdsLibrary.KDSLogic.DAL
{
    public class ParametersDAL : IParametersDAL
    {
        public DataTable GetKdsParametrs()
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {   //מחזיר טבלת פרמטרים:                 
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetParameters, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
