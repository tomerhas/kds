using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors
{
    public class SidurErrorShatHatchalaBiggerShatYetzia207 : SidurErrorBase
    {
        public SidurErrorShatHatchalaBiggerShatYetzia207(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {

            if (!string.IsNullOrEmpty(input.curSidur.sShatGmar) && !string.IsNullOrEmpty(input.curSidur.sShatHatchala) && input.curSidur.dFullShatHatchala >= input.curSidur.dFullShatGmar)
            {
                AddNewError(input);
                return false;
            }

            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errShatHatchalaBiggerShatYetzia; }
        }
    }
}
