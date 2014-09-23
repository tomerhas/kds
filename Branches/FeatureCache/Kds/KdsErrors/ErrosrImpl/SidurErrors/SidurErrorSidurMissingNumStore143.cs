using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorSidurMissingNumStore143 : SidurErrorBase
    {
        public SidurErrorSidurMissingNumStore143(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (input.curSidur.bSidurMyuhad)
            {//בסידור "מתגבר מחסן" (מזהים לפי מאפיין 36 (חובה לדווח מספר מחסן) בטבלת מאפייני סידורים מיוחדים) בודקים האם הוכנס מספר מחסן, אם חסר - שגוי. (כדי שניתן יהיה לצרפו למערכת חישוב פרמיות אחסנה). מספר מחסן זה לא שדה שמגיע מהסדרן. 
                if (input.curSidur.bHovaMisparMachsan && input.curSidur.iMisparMusachOMachsan == 0)
                {
                    input.curSidur.bSadotNosafim = true;
                    AddNewError(input);
                    return false;
                }
            }
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errMissingNumStore; }
        }
    }
}