using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.SidurErrors
{
    public class SidurErrorHachtamatKnisaLoBmakomHasaka197 : SidurErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            //בדיקה ברמת סידור         
            bool bError = false;
            
            bError = !(input.curSidur.bIsKnisaTkina_err197);
            if (bError)
            {
                AddNewError(input);
                return false;
            }
            
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errHachtamatKnisaLoBmakomHasaka197; }
        }
    }
}