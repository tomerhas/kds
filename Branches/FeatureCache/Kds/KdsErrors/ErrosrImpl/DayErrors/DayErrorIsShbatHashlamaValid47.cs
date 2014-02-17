using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.DayErrors
{
    public class DayErrorIsShbatHashlamaValid47 : DayErrorBase
    {
        public DayErrorIsShbatHashlamaValid47(IUnityContainer container)
            : base(container)
        {
        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {

            if (input.OvedDetails.sHashlamaLeyom == "1" && CheckShaaton(input.iSugYom, input.CardDate,input))
            {
                AddNewError(input);
                return false;
            }

            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errShbatHashlamaNotValid; }
        }
    }
}
