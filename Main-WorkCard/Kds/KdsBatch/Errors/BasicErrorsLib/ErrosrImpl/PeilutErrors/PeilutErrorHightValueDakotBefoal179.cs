using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.PeilutErrors
{
    public class PeilutErrorHightValueDakotBefoal179 : PeilutErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            bool isValid = true;

            if ((input.curPeilut.iMakatType == enMakatType.mKavShirut.GetHashCode() && input.curPeilut.iMisparKnisa == 0) || input.curPeilut.iMakatType == enMakatType.mNamak.GetHashCode() || input.curPeilut.iMakatType == enMakatType.mEmpty.GetHashCode())
            {
                if (input.curPeilut.iDakotBafoal > input.curPeilut.iMazanTashlum)
                    isValid = false;

            }

            if (input.curPeilut.iMakatType == enMakatType.mKavShirut.GetHashCode() && input.curPeilut.iMisparKnisa > 0)
            {
                if (input.curPeilut.iDakotBafoal > input.oParameters.iMaxMinutsForKnisot)
                    isValid = false;
            }

            if (!isValid)
            {
                AddNewError(input);
                return false;
            }
            
            //ערך דקות בפועל גבוה מזמן לגמר או מהזמן המותר לכניסה
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errHightValueDakotBefoal; }
        }
    }
}

