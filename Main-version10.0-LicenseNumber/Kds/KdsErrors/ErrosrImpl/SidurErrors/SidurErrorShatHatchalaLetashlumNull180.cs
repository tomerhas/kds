using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorShatHatchalaLetashlumNull180 : SidurErrorBase
    {
        public SidurErrorShatHatchalaLetashlumNull180(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            //בדיקה ברמת סידור         

            if (input.curSidur.dFullShatHatchalaLetashlum == DateTime.MinValue || input.curSidur.dFullShatHatchalaLetashlum.Date == DateTime.MinValue.Date)
            {
                AddNewError(input);
                return false;
            }
                
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.IsShatHatchalaLetashlumNull; }
        }
    }
}