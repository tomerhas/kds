using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.DayErrors
{
    public class DayErrorAvodatNahagutValid165 : DayErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (input.oMeafyeneyOved.IsMeafyenExist(51)) //|| (oMeafyeneyOved.IsMeafyenExist(61)))
            {
                if (input.bHaveSidurNahagut)
                    return false;
            }

            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errAvodatNahagutNotValid; }
        }
    }
}
