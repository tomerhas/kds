using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.DayErrors
{
    public class DayErrorAvodatNahagutValid165 : DayErrorBase
    {
        public DayErrorAvodatNahagutValid165(IUnityContainer container)
            : base(container)
        {
        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (input.oMeafyeneyOved.IsMeafyenExist(51)) //|| (oMeafyeneyOved.IsMeafyenExist(61)))
            {
                if (HaveSidurNahgutInDay(input))
                {
                    AddNewError(input);
                    return false;
                }
            }

            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errAvodatNahagutNotValid; }
        }
    }
}
