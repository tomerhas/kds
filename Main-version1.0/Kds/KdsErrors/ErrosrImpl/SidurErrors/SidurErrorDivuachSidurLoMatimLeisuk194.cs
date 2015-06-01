using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorDivuachSidurLoMatimLeisuk194 : SidurErrorBase
    {
        public SidurErrorDivuachSidurLoMatimLeisuk194(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
          
            if (input.OvedDetails.iIsuk == 422 && input.curSidur.iMisparSidur == 99001 && input.curSidur.iKodSibaLedivuchYadaniIn > 0 && input.curSidur.iKodSibaLedivuchYadaniOut > 0)
            {
                AddNewError(input);
                return false;
            }
             
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errDivuachSidurLoMatimLeisuk422; }
        }
    }
}