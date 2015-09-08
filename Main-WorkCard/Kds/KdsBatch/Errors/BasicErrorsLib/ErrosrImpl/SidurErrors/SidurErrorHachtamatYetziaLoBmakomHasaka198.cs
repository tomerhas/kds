using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.SidurErrors
{
    public class SidurErrorHachtamatYetziaLoBmakomHasaka198 : SidurErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            bool bError = false;

            bError = !(input.curSidur.bIsYetziaTkina_err198);
            if (bError)
            {
                AddNewError(input);
                return false;
            }
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errHachtamatYetziaLoBmakomHasaka198; }
        }
    }
}