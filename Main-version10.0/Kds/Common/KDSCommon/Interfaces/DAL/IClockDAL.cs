using KDSCommon.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.Interfaces.DAL
{
    public interface IClockDAL
    {
        void SaveHarmonyMovment(COLL_HARMONY_MOVMENT_ERR_MOV collHarmonyMovment);
    }
}
