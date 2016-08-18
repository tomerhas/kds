using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KdsLibrary;
using Microsoft.Practices.Unity;
using KDSCommon.Helpers;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorStartHourMissing15 : SidurErrorBase
    {
        public SidurErrorStartHourMissing15(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (input.curSidur.dFullShatHatchala.Year < DateHelper.cYearNull)
            {
                AddNewError(input);
                return false;
            }

            return true;           
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errStartHourMissing; }
        }
    }
}