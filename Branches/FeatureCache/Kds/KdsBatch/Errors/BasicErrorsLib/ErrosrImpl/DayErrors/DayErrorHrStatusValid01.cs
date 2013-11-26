using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.DayErrors
{
    public class DayErrorHrStatusValid01 : CardErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (!IsOvedInMatzav("1,3,4,5,6,7,8,10", input.dtMatzavOved))
            {
                AddNewError(input);
                return false;
            }
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errHrStatusNotValid; }
        }
    }
}
