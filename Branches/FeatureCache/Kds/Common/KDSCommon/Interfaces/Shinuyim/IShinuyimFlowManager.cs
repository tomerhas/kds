using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Shinuyim;

namespace KDSCommon.Interfaces.Shinuyim
{
    public interface IShinuyimFlowManager
    {
        FlowShinuyResult ExecShinuyim(int misparIshi, DateTime cardDate, long? btchRequest = null, int? userId = null);
    }
}
