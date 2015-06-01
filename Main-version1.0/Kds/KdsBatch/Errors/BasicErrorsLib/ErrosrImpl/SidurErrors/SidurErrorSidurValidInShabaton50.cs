using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.SidurErrors
{
    public class SidurErrorSidurValidInShabaton50 : SidurErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (input.curSidur.bSidurMyuhad) //וסידור מיוחד
            {   //אם שבתון וקיים מאפיין 9, כלומר סידור אזור בשבתון, אז נעלה שגיאה
                if (CheckShaaton(input.iSugYom, input.CardDate, input) && input.curSidur.bSidurNotInShabtonKodExists)
                {//היום הוא יום שבתון ולסידור יש מאפיין אסור בשבתון, לכן נעלה שגיאה
                        AddNewError(input);
                        return false;                    
                }
            }               
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errSidurNotAllowedInShabaton; }
        }
    }
}