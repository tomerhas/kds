using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsErrors.ErrosrImpl.SidurErrors
{
    public class SidurErrorMushalETWithSidurNotAllowET208 : SidurErrorBase
    {
        public SidurErrorMushalETWithSidurNotAllowET208(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (input.OvedDetails.iKodHevra == enEmployeeType.enEgged.GetHashCode() && input.OvedDetails.iKodHevraHashala == enEmployeeType.enEggedTaavora.GetHashCode())
            {
                if (input.curSidur.bSidurMyuhad)
                {
                    if (input.curSidur.iMisparSidur == 99300 || input.curSidur.iMisparSidur == 99301 || input.curSidur.bSidurNotValidKodExists)
                    {
                        AddNewError(input);
                        return false;
                    }
                }
            }
            
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errMushalETWithSidurNotAllowET; }
        }
    }
}
