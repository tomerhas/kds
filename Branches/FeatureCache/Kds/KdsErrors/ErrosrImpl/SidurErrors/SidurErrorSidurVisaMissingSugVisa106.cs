using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorSidurVisaMissingSugVisa106 : SidurErrorBase
    {
        public SidurErrorSidurVisaMissingSugVisa106(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (input.curSidur.bSidurMyuhad)
            {//בסידור ויזה (לפי מאפיין 45 בטבלת סידורים מיוחדים עם ערך 2 ) חובה לדווח סוג ויזה. אם שדה ריק - שגוי. 
                if (input.curSidur.sSidurVisaKod == "2" && input.curSidur.iSugHazmanatVisa == 0)
                {
                    AddNewError(input);
                    return false;
                }

            }
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errMissingSugVisa; }
        }
    }
}