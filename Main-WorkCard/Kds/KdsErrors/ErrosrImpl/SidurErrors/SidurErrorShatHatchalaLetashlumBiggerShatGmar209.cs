using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsErrors.ErrosrImpl.SidurErrors
{
    public class SidurErrorShatHatchalaLetashlumBiggerShatGmar209 : SidurErrorBase
    {
        public SidurErrorShatHatchalaLetashlumBiggerShatGmar209(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (!string.IsNullOrEmpty(input.curSidur.sShatGmarLetashlum) && !string.IsNullOrEmpty(input.curSidur.sShatHatchalaLetashlum) && input.curSidur.dFullShatHatchalaLetashlum >= input.curSidur.dFullShatGmarLetashlum)
           {
                    AddNewError(input);
                    return false;
           }
            
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errShatHatchalaLetashlumBiggerShatGmar; }
        }
    }
}
