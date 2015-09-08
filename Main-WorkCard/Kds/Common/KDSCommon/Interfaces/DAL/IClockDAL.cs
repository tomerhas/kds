using KDSCommon.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.Interfaces.DAL
{
    public interface IClockDAL
    {
        void SaveHarmonyMovment(long BakashaId,COLL_HARMONY_MOVMENT_ERR_MOV collHarmonyMovment);
        void InsertControlClockRecord(DateTime  taarich, int status , string teur);
        void UpdateControlClockRecord(DateTime taarich, int status, string teur);
        string GetLastHourClock();
    }
}
