using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.SidurErrors
{
    public class SidurErrorSidurAllowedForEggedTaavora148 : SidurErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (input.OvedDetails.iDirug == 85 && input.OvedDetails.iDarga == 30)
            {
                if ((input.curSidur.bSidurMyuhad) && (input.curSidur.bSidurNotValidKodExists))
                {
                    AddNewError(input);
                    return false;
                }
            }
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errNotAllowedSidurForEggedTaavora; }
        }
    }
}