using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.PeilutErrors
{
    public class PeilutErrorHighValueKisuyTor87 : PeilutErrorBase
    {
        public PeilutErrorHighValueKisuyTor87(IUnityContainer container)
            : base(container)
        {

        }
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

