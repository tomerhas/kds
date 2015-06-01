using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.DayErrors
{
    public class DayErrorIsHalbashValid36 : DayErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {

            string sLookUp = "";
            bool bError = false;

            sLookUp = GetLookUpKods("ctb_zmaney_halbasha", input);
            if (string.IsNullOrEmpty(input.OvedDetails.sHalbasha))
            {
                bError = true;                  
            }
            else
            {
                if ((sLookUp.IndexOf(input.OvedDetails.sHalbasha) == -1) || input.OvedDetails.sHalbasha == ZmanHalbashaType.CardError.GetHashCode().ToString())
                {
                    bError = true;
                }
            }
            if (bError)
            {
                AddNewError(input);
                return false;
            }

            return true;     
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errHalbashaNotvalid; }
        }
    }
}
