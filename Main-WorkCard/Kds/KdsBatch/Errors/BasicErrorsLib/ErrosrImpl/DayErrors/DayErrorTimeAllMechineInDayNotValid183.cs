using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.DayErrors
{
    public class DayErrorTimeAllMechineInDayNotValid183 : DayErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (input.iTotalTimePrepareMechineForDay > input.oParameters.iPrepareAllMechineTotalMaxTimeInDay)
                return false;
          
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errTimeAllMechineInDayNotValid; }
        }
    }
}

