using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using KDSCommon.DataModels;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.DayErrors
{
    public class dayErrorIsOvedPeilutValid172 : DayErrorBase
    {
        public dayErrorIsOvedPeilutValid172(IUnityContainer container)
            : base(container)
        {
        }
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
