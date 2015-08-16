using KDSCommon.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.Interfaces.Managers
{
    public interface IClockManager
    {
        void SaveShaonimMovment(COLL_HARMONY_MOVMENT_ERR_MOV collHarmonyMovment);
    }
}
