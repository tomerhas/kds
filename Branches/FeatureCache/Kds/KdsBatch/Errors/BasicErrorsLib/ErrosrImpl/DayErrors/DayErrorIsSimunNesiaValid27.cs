using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.DayErrors
{
    public class DayErrorIsSimunNesiaValid27 : DayErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            string sLookUp;
            string sBitulZmanNesia = string.Empty;
            //בדיקה ברמת כרטיס עבודה
            sLookUp = GetLookUpKods("ctb_zmaney_nesiaa",input);

            sBitulZmanNesia = input.OvedDetails.sBitulZmanNesiot;
            if (!(string.IsNullOrEmpty(sBitulZmanNesia)))
            {   //נעלה שגיאה אם ערך לא קיים בטבלת פענוח
                if (sLookUp.IndexOf(sBitulZmanNesia) == -1)
                {
                    AddNewError(input);
                    return false;
                }
            }
            else
            {
                AddNewError(input);
                return false;
            }
            return true;

        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errSimunNesiaNotValid; }
        }
    }
}
