using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.DayErrors
{
    public abstract class DayErrorBase : CardErrorBase
    {
        public DayErrorBase(IUnityContainer container)
            : base(container)
        {
        }
        public override ErrorSubLevel CardErrorSubLevel 
        {
            get { return ErrorSubLevel.Yomi; }
        }

        protected void AddNewError(ErrorInputData input)
        {
            var drNew = input.dtErrors.NewRow();
            drNew["mispar_ishi"] = input.iMisparIshi;
            drNew["check_num"] = ErrorTypes.errHrStatusNotValid.GetHashCode();
            drNew["taarich"] = input.CardDate.ToShortDateString();

            input.dtErrors.Rows.Add(drNew);
        }
    }
}
