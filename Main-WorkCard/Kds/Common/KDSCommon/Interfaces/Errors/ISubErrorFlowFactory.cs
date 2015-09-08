using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;

namespace KDSCommon.Interfaces.Errors
{
    public interface ISubErrorFlowFactory
    {
        ISubErrorFlowManager GetFlowManager(SubFlowManagerTypes managerType);
    }
}
