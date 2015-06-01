using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;

namespace KDSCommon.Interfaces.Errors
{
    public interface ISubErrorFlowManager
    {
        void ExecuteFlow(ErrorInputData input,int stage);
    }
}
