using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.PeilutErrors
{
    public class PeilutErorMisparSiduriOtoNotExists139 : PeilutErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            //בסידור המכיל נסיעת שירות עבורה קיים 71=Onatiut  - חובה שידווח מספר סידורי של רכב  - גדול או שווה ל- 1, אחרת - שגוי.
            if (input.curPeilut.iMakatType == enMakatType.mKavShirut.GetHashCode() && input.curPeilut.iMisparKnisa == 0 &&
                input.curPeilut.iOnatiyut == 71 && input.curPeilut.lMisparSiduriOto == 0 && input.curPeilut.bPeilutEilat)
            {
                AddNewError(input);
                return false;
            }
            // נסיעה ללא רכב סידורי 
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errMisparSiduriOtoNotExists; }
        }
    }
}

