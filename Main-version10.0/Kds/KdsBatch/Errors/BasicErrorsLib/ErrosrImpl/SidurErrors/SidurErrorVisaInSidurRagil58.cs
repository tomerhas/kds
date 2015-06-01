using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.SidurErrors
{
    public class SidurErrorVisaInSidurRagil58 : SidurErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if ((!input.curSidur.bSidurVisaKodExists) && (!String.IsNullOrEmpty(input.curSidur.sVisa)) && Int32.Parse(input.curSidur.sVisa) > 0)
            {
                AddNewError(input);
                return false;
            }
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errSidurVisaNotValid; }
        }
    }
}