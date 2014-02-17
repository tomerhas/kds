using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KdsLibrary;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.SidurErrors
{
    public class SidurErrorStartHourMissing15 : SidurErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (input.curSidur.dFullShatHatchala.Year < clGeneral.cYearNull)
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