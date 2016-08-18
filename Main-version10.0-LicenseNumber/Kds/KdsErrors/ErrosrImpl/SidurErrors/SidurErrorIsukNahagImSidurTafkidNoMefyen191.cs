using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorIsukNahagImSidurTafkidNoMefyen191 : SidurErrorBase
    {
        public SidurErrorIsukNahagImSidurTafkidNoMefyen191(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {

            if (input.OvedDetails.iIsuk >= 500 && input.OvedDetails.iIsuk <= 600 && input.curSidur.iLoLetashlum == 1 && (input.curSidur.iKodSibaLoLetashlum == 4 || input.curSidur.iKodSibaLoLetashlum == 5 || input.curSidur.iKodSibaLoLetashlum == 17))
            {
                AddNewError(input);
                return false;
            }
            
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errIsukNahagImSidurTafkidNoMefyen; }
        }
    }
}