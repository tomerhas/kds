using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using KDSCommon.DataModels;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.DayErrors
{
    public class dayErrorIsOvedPeilutValid172 : DayErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (IsOvedInMatzav("5,6,8", input.dtMatzavOved) && input.htEmployeeDetails.Values.Cast<SidurDM>().ToList().Any(sidur => !IsSidurHeadrut(sidur)))
            {
                AddNewError(input);
                return false;
            }

            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errOvedPeilutNotValid; }
        }
    }
}
