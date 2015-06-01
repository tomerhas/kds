using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.DayErrors
{
    public class DayErrorTimeAllMechineInDayNotValid183 : DayErrorBase
    {
        public DayErrorTimeAllMechineInDayNotValid183(IUnityContainer container)
            : base(container)
        {
        }
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

