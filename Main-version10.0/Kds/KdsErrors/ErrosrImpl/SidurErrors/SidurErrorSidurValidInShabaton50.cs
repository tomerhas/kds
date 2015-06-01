using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using Microsoft.Practices.Unity;
using KDSCommon.Helpers;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorSidurValidInShabaton50 : SidurErrorBase
    {
        public SidurErrorSidurValidInShabaton50(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (input.curSidur.bSidurMyuhad) //וסידור מיוחד
            {   //אם שבתון וקיים מאפיין 9, כלומר סידור אזור בשבתון, אז נעלה שגיאה
                if (DateHelper.CheckShaaton(input.iSugYom, input.CardDate, input.SugeyYamimMeyuchadim) && input.curSidur.bSidurNotInShabtonKodExists)
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