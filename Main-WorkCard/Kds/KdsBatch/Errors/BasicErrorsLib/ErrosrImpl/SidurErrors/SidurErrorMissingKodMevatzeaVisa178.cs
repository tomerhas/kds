using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.SidurErrors
{
    public class SidurErrorMissingKodMevatzeaVisa178 : SidurErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (input.curSidur.bSidurMyuhad)
            {//בסידור ויזה (לפי מאפיין 45 בטבלת סידורים מיוחדים עם ערך 2 ) חובה לדווח קוד מבצע ויזה. אם שדה ריק - שגוי. 
                if (input.curSidur.sSidurVisaKod == "2" && input.curSidur.iMivtzaVisa == 0 && input.curSidur.sLidroshKodMivtza != "")
                {
                    AddNewError(input);
                    return false;
                }  
            }
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errMissingKodMevatzaVisa; }
        }
    }
}