using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.PeilutErrors
{
    public class PeilutErrorKisuyTorLifneyHatchalatSidur189 : PeilutErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            DateTime dShatKisuyTor;
           
            if (input.curPeilut.iKisuyTor > 0)
            {
                dShatKisuyTor = input.curPeilut.dFullShatYetzia.AddMinutes(-input.curPeilut.iKisuyTor);
                if (dShatKisuyTor < input.curSidur.dFullShatHatchala)
                {
                    AddNewError(input);
                    return false;
                }
            }
            //כיסוי תור לפני תחילת סידור
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errKisuyTorLifneyHatchalatSidur; }
        }
    }
}

