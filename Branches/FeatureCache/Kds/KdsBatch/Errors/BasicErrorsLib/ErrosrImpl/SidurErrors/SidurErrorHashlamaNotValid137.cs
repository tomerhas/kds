using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.SidurErrors
{
    public class SidurErrorHashlamaNotValid137 : SidurErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            string sLookUp = "";
            if (input.curSidur.bHashlamaExists && string.IsNullOrEmpty(input.curSidur.sHashlama))
            {
                return false;
            }
            else
            {
                sLookUp = "0,1,2,9";
                if (sLookUp.IndexOf(input.curSidur.sHashlama) == -1)
                {
                    AddNewError(input);
                    return false;
                }
            }
      
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errHashlamaNotValid; }
        }
    }
}