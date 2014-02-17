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
    public class SidurErrorSidurNAhagutValid161 : SidurErrorBase
    {
        public SidurErrorSidurNAhagutValid161(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            bool bError = false;

            //לעובד אסור לבצע סידור נהיגה (עבור סידורים מיוחדים, מזהים סידור נהגות לפי ערך 5 במאפיין  3 בטבלת סידורים מיוחדים. עבור סידורים רגילים מזהים סידור נהגות לפי ערך 5 ב מאפיין 3 בטבלת מאפייני סידורים) במקרים הבאים: א. לעובד אין רישיון נהיגה באוטובוס (יודעים אם לעובד יש רישיון לפי ערכים 6, 10, 11 בקוד נתון 7 (קוד רישיון אוטובוס) בטבלת פרטי עובדים) ב. עובד הוא מותאם שאסור לו לנהוג (יודעים שעובד הוא מותאם שאסור לו לנהוג לפי ערכים 4, 5 בקוד נתון 8 (קוד עובד מותאם) בטבלת פרטי עובדים) ג. עובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה (יודעים שעובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה לפי ערכים 6, 7 בקוד נתון 8 (קוד עובד מותאם) בטבלת פרטי עובדים) במקרה זה יש לבדוק אם הסידור מכיל רק נסיעות ריקות, מפעילים את הרוטינה לזיהוי מקט ד. עובד הוא בשלילה (יודעים שעובד הוא בשלילה לפי ערך 1 בקוד בנתון 21 (שלילת   רשיון) בטבלת פרטי עובדים) 
            if (input.curSidur.bSidurMyuhad)
            {
                if (input.curSidur.sSugAvoda != clGeneral.enSugAvoda.ActualGrira.GetHashCode().ToString())
                {
                    if (input.curSidur.sSectorAvoda == enSectorAvoda.Nahagut.GetHashCode().ToString())
                    {
                        bError = CheckConditionsAllowSidur(input);
                    }
                }
            }
            else
            {//סידור רגיל
                if (input.drSugSidur.Length > 0)
                {
                    if (input.drSugSidur[0]["sug_Avoda"].ToString() != clGeneral.enSugAvoda.Grira.GetHashCode().ToString() && input.drSugSidur[0]["sector_avoda"].ToString() == enSectorAvoda.Nahagut.GetHashCode().ToString())
                    {
                        bError = CheckConditionsAllowSidur(input);
                    }
                }
            }
                
            if (bError)
            {
                AddNewError(input);
                return false;
            }

            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errOvedNotAllowedToDoSidurNahagut; }
        }
    }
}