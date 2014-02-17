using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.PeilutErrors
{
    public abstract class PeilutErrorBase : CardErrorBase
    {
        public PeilutErrorBase(IUnityContainer container)
            : base(container)
        {

        }
        public override ErrorSubLevel CardErrorSubLevel
        {
            get { return ErrorSubLevel.Peilut; }
        }

       
        protected void AddNewError(ErrorInputData input)
        {
            var drNew = input.dtErrors.NewRow();
            drNew["mispar_ishi"] = input.iMisparIshi;
            drNew["check_num"] = ErrorTypes.errHrStatusNotValid.GetHashCode();
            drNew["taarich"] = input.CardDate.ToShortDateString();
            drNew["mispar_sidur"] = input.curSidur.iMisparSidur;
            drNew["shat_hatchala"] = (input.curSidur.sShatHatchala == null ? DateTime.MinValue : input.curSidur.dFullShatHatchala);
            drNew["Shat_Yetzia"] = string.IsNullOrEmpty(input.curPeilut.sShatYetzia) ? DateTime.MinValue : input.curPeilut.dFullShatYetzia;
            drNew["mispar_knisa"] =input.curPeilut.iMisparKnisa;
            drNew["makat_nesia"] = input.curPeilut.lMakatNesia;

            input.dtErrors.Rows.Add(drNew);
        }
    }
}
