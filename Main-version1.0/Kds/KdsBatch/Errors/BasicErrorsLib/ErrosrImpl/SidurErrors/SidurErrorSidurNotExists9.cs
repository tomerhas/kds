using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.SidurErrors
{
    public class SidurErrorSidurNotExists9 : SidurErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if ((input.curSidur.bSidurMyuhad && input.curSidur.iMisparSidurMyuhad == 0) || input.curSidur.iMisparSidur.ToString().Length < 4)
            {
                AddNewError(input);
                return false;
            }

            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errSidurNotExists; }
        }
    }
}