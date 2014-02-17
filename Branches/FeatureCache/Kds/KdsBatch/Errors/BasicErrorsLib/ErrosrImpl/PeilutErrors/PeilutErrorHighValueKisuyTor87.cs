using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.PeilutErrors
{
    public class PeilutErrorHighValueKisuyTor87 : PeilutErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if ((input.curPeilut.iMakatType == enMakatType.mKavShirut.GetHashCode() && input.curPeilut.iMisparKnisa == 0) || input.curPeilut.iMakatType == enMakatType.mNamak.GetHashCode())
            {
                if (input.curPeilut.iKisuyTor > input.curPeilut.iKisuyTorMap)
                {
                    AddNewError(input);
                    return false;
                }
            }

            //כסוי תור מעל המותר
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errHighValueKisuyTor; }
        }
    }
}

