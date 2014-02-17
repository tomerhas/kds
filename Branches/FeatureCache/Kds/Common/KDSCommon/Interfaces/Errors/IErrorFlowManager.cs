using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;

namespace KDSCommon.Interfaces.Errors
{
    public interface IErrorFlowManager
    {
        FlowErrorResult ExecuteFlow(int misparIshi, DateTime cardDate, long? btchRequest = null, int? userId = null);
        CardStatus GetCardStatus(ErrorInputData inputData);
    }
}
