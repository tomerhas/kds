using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorHashlamaForComputerAndAccidentValid45 : SidurErrorBase
    {
        public SidurErrorHashlamaForComputerAndAccidentValid45(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (input.iSidur == 0)// נבצע את הבדיקה לסידור הראשון
            {
                string sHashlamaLeyom;
                bool bError = false;

                // איפיון קודם, לא רלוונטי- השלמה ליום עבודה (ערך 9) מותרת בשני מקרים: מפעיל מחשב (עיסוק 122 או  123) וסידור הוא מנהל (סידור מיוחד עם ערך 1 (שעונים) במאפיין 52 (אופי עבודה)). מקרה שני הוא סידור תאונה (סידור מיוחד עם ערך 3 (תאונה) במאפיין 53 (אופי העדרות)). 
                //אם יש סימון של השלמה ליום עבודה והסידור הוא יחיד ביום והוא מסוג העדרות (לפי מאפיין 53, לא משנה מה הערך במאפיין) או שהסידור מסומן לא לתשלום (רלוונטי לכל הסידורים, מיוחדים ורגילים) - שגיאה.
                sHashlamaLeyom = input.OvedDetails.sHashlamaLeyom;
                if (sHashlamaLeyom == "1")
                {
                    if ((input.htEmployeeDetails.Count == 1))
                    {
                        if (input.curSidur.bSidurMyuhad)
                        {
                            if ((input.curSidur.bHeadrutTypeKodExists) || (input.curSidur.iLoLetashlum > 0 && input.curSidur.iKodSibaLoLetashlum != 1))
                            {
                                bError = true;
                            }
                        }
                        else
                        {
                            if (input.curSidur.iLoLetashlum > 0 && input.curSidur.iKodSibaLoLetashlum != 1)
                            {
                                bError = true;
                            }
                        }
                    }
                    if (bError)
                    {
                        AddNewError(input);
                        return false;
                    }
                }
            }
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errHashlamaForComputerWorkerAndAccident; }
        }
    }
}