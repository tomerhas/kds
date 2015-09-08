using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.PeilutErrors
{
    public class PeilutErrorIsZakaiLina31 : PeilutErrorBase
    {
        public PeilutErrorIsZakaiLina31(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {

            //אם  יש ערך בשדה לינה ובסידור האחרון יש פעילות מסוג אלמנט (לפי רוטינת זיהוי מקט) ולאלמנט יש מאפיין המתנה (15) - יוצא לשגיאה
            if (!string.IsNullOrEmpty(input.OvedDetails.sLina))
            {
                if ((input.curSidur.iMisparSidur == input.iLastMisaprSidur) && (int.Parse(input.OvedDetails.sLina) > 0) && (input.curPeilut.iMakatType == enMakatType.mElement.GetHashCode()) && (input.curPeilut.bElementHamtanaExists) && input.curPeilut.sHamtanaEilat == "1")
                {
                    AddNewError(input);
                    return false;
                }
                
            }
         
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errLoZakaiLLina; }
        }
    }
}

