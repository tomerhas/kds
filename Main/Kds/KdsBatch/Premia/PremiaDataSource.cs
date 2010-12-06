using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;

namespace KdsBatch.Premia
{
    /// <summary>
    /// Provides Data Source for UI controls for Premia Calculation module
    /// </summary>
    public class PremiaDataSource
    {
        private const int PREMIA_PERIODS_LENGTH = 10;
        public DateTime[] GetPeriods()
        {
            List<DateTime> periods = new List<DateTime>(PREMIA_PERIODS_LENGTH);
            DateTime firstPeriod = DateTime.Now.Date.AddMonths(-1);
            for (int i = 0; i < PREMIA_PERIODS_LENGTH; ++i)
            {
                periods.Add(new DateTime(firstPeriod.Year, firstPeriod.Month, 1));
                firstPeriod = firstPeriod.AddMonths(-1);
            }
            return periods.ToArray();
        }

        public DataTable GetPeriodBatchRequests(DateTime period)
        {
            DataTable dt = new DataTable();
            clDal dal = new clDal();
            dal.AddParameter("p_taarich", ParameterType.ntOracleDate, period,
                ParameterDir.pdInput);
            dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null,
                ParameterDir.pdOutput);
            dal.ExecuteSP("pkg_batch.pro_get_premia_bakashot", ref dt);
            return dt;
        }
    }

    
}
