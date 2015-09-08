using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.SidurErrors
{
    public class SidurErrorSidurLoTakefLetarich190 : SidurErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (input.curSidur.sTokefHatchala.Length > 0 && input.curSidur.sTokefSiyum.Length > 0 && (input.CardDate < DateTime.Parse(input.curSidur.sTokefHatchala) || input.CardDate > DateTime.Parse(input.curSidur.sTokefSiyum)))
            {
                AddNewError(input);
                return false;
            }
            
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errSidurLoTakefLetaarich; }
        }
    }
}