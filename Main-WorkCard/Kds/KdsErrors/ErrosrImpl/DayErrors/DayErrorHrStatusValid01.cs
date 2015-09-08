using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.DayErrors
{
    public class DayErrorHrStatusValid01 : DayErrorBase
    {
        public DayErrorHrStatusValid01(IUnityContainer container)
            : base(container)
        {
        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            string lstStatus;
            //בדיקה ברמת יום עבודה
            if (input.CardDate < DateTime.Parse("12/09/2013"))
                lstStatus = "1,3,4,5,6,7,8,10,11";
            else lstStatus = "1,3,4,5,6,7,10,11";

            if (!IsOvedInMatzav(lstStatus, input.dtMatzavOved))
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
