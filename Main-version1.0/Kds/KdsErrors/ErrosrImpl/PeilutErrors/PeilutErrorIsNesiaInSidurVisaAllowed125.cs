using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.PeilutErrors
{
    public class PeilutErrorIsNesiaInSidurVisaAllowed125 : PeilutErrorBase
    {
        public PeilutErrorIsNesiaInSidurVisaAllowed125(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            //בסידורי ויזה מותר רק פעילויות שמתחילות ב- 5 (תעודת ויזה) או 7 (אלמנט). עבור אלמנט יש לבדוק אם הוא מותר בסידור ויזה (לפי ערך 2 (רשאי) במאפיין 12 (דיווח בסידור ויזה) בטבלת מאפייני אלמנטים). טבלת מאפייני אלמנטים תכיל מאפיינים לכל האלמנטים במערכת. מזהים סידור ויזה לפי מאפיין 45 בסידורים מיוחדים. 
            if (input.curSidur.bSidurVisaKodExists)
            {
                if (!(input.curPeilut.iMakatType == enMakatType.mVisa.GetHashCode() || (input.curPeilut.iMakatType == enMakatType.mElement.GetHashCode() && input.curPeilut.sDivuchInSidurVisa == "2")))
                {
                    AddNewError(input);
                    return false;
                }
            }
            //נסיעה אסורה בסידור ויזה
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errNesiaInSidurVisaNotAllowed; }
        }
    }
}


