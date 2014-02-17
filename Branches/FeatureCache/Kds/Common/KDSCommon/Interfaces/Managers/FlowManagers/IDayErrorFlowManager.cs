using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;

namespace KDSCommon.Interfaces.Managers.FlowManagers
{
    public interface IDayErrorFlowManager
    {
        void ValidateDayErrors(ErrorInputData input);
    }
}
