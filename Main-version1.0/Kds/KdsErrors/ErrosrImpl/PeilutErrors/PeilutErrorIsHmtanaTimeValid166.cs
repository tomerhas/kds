using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.PeilutErrors
{
    public class PeilutErrorIsHmtanaTimeValid166 : PeilutErrorBase
    {
        public PeilutErrorIsHmtanaTimeValid166(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            //בדיקה ברמת פעילות
            int iTimeInMinutes = 0;
            PeilutDM tmpPeilut;
            bool bCurrSidurEilat = false;
            bool bhaveHamtana = false;
            int iParamHamtana = 0;
            
            //נסיעת אילת ארוכה – פעילות שעונה על שני התנאים: א.שדה EilatTrip=1  ב. הערך בשדה Km הוא מעל הערך בפרמטר 149.
            if (input.curSidur.bSidurEilat && input.curSidur.IsLongEilatTrip(out tmpPeilut, input.oParameters.fOrechNesiaKtzaraEilat))
            {
                bCurrSidurEilat = true;
            }
            if (input.curPeilut.lMakatNesia.ToString().Length > 5)
                iTimeInMinutes = int.Parse(input.curPeilut.lMakatNesia.ToString().Substring(3, 3));

            //1. אם בסידור אליו משויכת פעילות ההמתנה קיימת נסיעת אילת ארוכה ומשך ההמתנה (הערך בפוזיציות 4-6) הוא מעל הערך בפרמטר 148 - שגוי.
            //2. אם בסידור אליו משויכת פעילות ההמתנה לא קיימת נסיעת אילת ארוכה ומשך ההמתנה (הערך בפוזיציות 4-6) הוא מעל הערך בפרמטר 161 - שגוי.
            if (bCurrSidurEilat && input.curPeilut.iMakatType == enMakatType.mElement.GetHashCode() && input.curPeilut.bElementHamtanaExists)
            {
                iParamHamtana = input.oParameters.iMaxZmanHamtanaEilat;
                bhaveHamtana = true;
            }
            else if (!bCurrSidurEilat && input.curPeilut.iMakatType == enMakatType.mElement.GetHashCode() && input.curPeilut.bElementHamtanaExists)
            {
                iParamHamtana = input.oParameters.iMaximumHmtanaTime;
                bhaveHamtana = true;
            }

            if (bhaveHamtana && iTimeInMinutes > iParamHamtana)
            {
                AddNewError(input);
                return false;
            }
            //עיכוב ארוך מעל המותר
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errHmtanaTimeNotValid; }
        }
    }
}

