using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KdsLibrary;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorTotalHashlamotInCardValid142 : SidurErrorBase
    {
        public SidurErrorTotalHashlamotInCardValid142(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            
            int iHashlama = string.IsNullOrEmpty(input.curSidur.sHashlama) ? 0 : int.Parse(input.curSidur.sHashlama);
            if (iHashlama > 0)
            {
                input.iTotalHashlamotForSidur += 1;
                int iZmanMaximum = 0;

                //יש מספר מקסימלי של השלמות המותר ליום, תלוי בסוג היום. בודקים את סוג היום ולפי סוג היום בודקים בטבלת פרמטרים חיצוניים מה מקסימום ההשלמות המותר ליום. 108 (מכסימום השלמות ביום חול), 109 (מכסימום השלמות בשישי/ע.ח), 110 (מכסימום השלמות בשבתון). אם בודקים יום אל מול טבלת סוגי ימים מיוחדים והוא אינו מוגדר כשבתון או ערב שבת/חג  - יום זה הוא יום חול.
                if (CheckShaaton(input.iSugYom, input.CardDate, input))
                {
                    iZmanMaximum = input.oParameters.iHashlamaMaxShabat;
                }
                else
                {
                    if ((input.curSidur.sErevShishiChag == "1") || (input.curSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()))
                    {
                        iZmanMaximum = input.oParameters.iHashlamaMaxShisi;
                    }
                    else
                    {
                        iZmanMaximum = input.oParameters.iHashlamaMaxYomRagil;
                    }
                }
                if (input.iTotalHashlamotForSidur > iZmanMaximum)
                {
                    AddNewError(input);
                    return false;
                }

            }
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errTotalHashlamotBiggerThanAllow; }
        }
    }
}