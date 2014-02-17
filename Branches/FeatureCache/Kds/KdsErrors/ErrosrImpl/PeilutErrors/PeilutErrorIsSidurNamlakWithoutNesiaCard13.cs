using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.PeilutErrors
{
    public class PeilutErrorIsSidurNamlakWithoutNesiaCard13 : PeilutErrorBase
    {
        public PeilutErrorIsSidurNamlakWithoutNesiaCard13(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if ((input.curSidur.bSidurMyuhad) && (input.curSidur.bSidurVisaKodExists) && (input.curSidur.iMisparSidurMyuhad > 0))
            {
                if ((input.curPeilut.lMisparVisa == 0) && (input.curPeilut.lMakatNesia > 0) && input.curPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 1) == "5")  //אין תעודת נסיעה
                {
                    AddNewError(input);
                    return false;
                }
                
            }
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errSidurNamlakWithoutNesiaCard; }
        }
    }
}


