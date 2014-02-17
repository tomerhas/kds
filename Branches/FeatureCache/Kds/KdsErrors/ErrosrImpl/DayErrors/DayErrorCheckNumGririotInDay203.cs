using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KDSCommon.DataModels;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.DayErrors
{
    public class DayErrorCheckNumGririotInDay203 : DayErrorBase
    {
        public DayErrorCheckNumGririotInDay203(IUnityContainer container)
            : base(container)
        {
        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            int iCountSidurim = 0;

            iCountSidurim = input.htEmployeeDetails.Values.Cast<SidurDM>().ToList().Count(Sidur => Sidur.iSugSidurRagil == 69 && Sidur.iLoLetashlum == 0);

            //בדיקה ברמת יום עבודה
            if (iCountSidurim > 1)
            {
                AddNewError(input);
                return false;
            }
            return true;

        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errConenutGriraMealHamutar; }
        }
    }
}
