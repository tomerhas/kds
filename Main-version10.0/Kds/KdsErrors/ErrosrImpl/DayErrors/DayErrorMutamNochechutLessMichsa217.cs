using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using KDSCommon.Helpers;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsErrors.ErrosrImpl.DayErrors
{
    public class DayErrorMutamNochechutLessMichsa217 : DayErrorBase
    {
        public DayErrorMutamNochechutLessMichsa217(IUnityContainer container)
            : base(container)
        {
        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
               
            if (input.OvedDetails.sMutamut.Trim() == "1" && input.curSidur.sSidurDay != enDay.Shishi.GetHashCode().ToString() &&
                input.curSidur.sSidurDay != enDay.Shabat.GetHashCode().ToString() && !DateHelper.CheckShaaton(input.iSugYom, input.CardDate, input.SugeyYamimMeyuchadim))
            {
                var nochechut = GetSachNochechutDay(input);
                var michsa = GetMichsaYomit(input);
                if (nochechut > 0 && michsa > 0 && nochechut < michsa && (michsa-nochechut)<30)
                {
                    AddNewError(input);
                    return false;
                }
            }

            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errMutamNahagutNochechutLessMichsa; }
        }
    }
}
