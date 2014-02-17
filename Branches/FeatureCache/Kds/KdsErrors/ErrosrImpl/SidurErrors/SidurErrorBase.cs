using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public abstract class SidurErrorBase : CardErrorBase
    {
        public SidurErrorBase(IUnityContainer container)
            : base(container)
        {

        }

        public override ErrorSubLevel CardErrorSubLevel
        {
            get { return ErrorSubLevel.Sidur; }
        }

        protected void AddNewError(ErrorInputData input)
        {
            var drNew = input.dtErrors.NewRow();
            drNew["mispar_ishi"] = input.iMisparIshi;
            drNew["check_num"] = (int)CardErrorType;
            drNew["taarich"] = input.CardDate.ToShortDateString();
            drNew["mispar_sidur"] = input.curSidur.iMisparSidur;
            drNew["shat_hatchala"] = (input.curSidur.sShatHatchala == null ? DateTime.MinValue : input.curSidur.dFullShatHatchala);

            input.dtErrors.Rows.Add(drNew);
        }
    }
}
