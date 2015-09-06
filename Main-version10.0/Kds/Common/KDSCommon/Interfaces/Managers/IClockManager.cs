using KDSCommon.UDT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace KDSCommon.Interfaces.Managers
{
    public interface IClockManager
    {
        void InsertControlClockRecord(DateTime taarich, int status, string teur);
        void UpdateControlClockRecord(DateTime taarich, int status, string teur);
        string GetLastHourClock();
        void SaveShaonimMovment(long BakashaId,COLL_HARMONY_MOVMENT_ERR_MOV collHarmonyMovment);
        void InsertToCollMovment(COLL_HARMONY_MOVMENT_ERR_MOV collHarmony, DataTable dt);
        void InsertToCollErrMovment(COLL_HARMONY_MOVMENT_ERR_MOV collHarmony, DataTable dt);

    }
}
