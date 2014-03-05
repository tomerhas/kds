using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using KDSCommon.Helpers;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.PeilutErrors
{
    public class PeilutErrorIsTimeForPrepareMechineValid86 : PeilutErrorBase
    {
        public PeilutErrorIsTimeForPrepareMechineValid86(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            int iElementTime = 0;
            int iElementType = 0;
            bool bError = false; 
          
            //בדיקה ברמת פעילות            
            
            //זמן הכנת מכונה באלמנט הוא מוגבל בזמן הזמן משתנה אם זו הכנת מכונה ראשונה (אלמנט 701xxx00) ביום או נוספת (711xxx00). זיהוי הכנה ראשונה/נוספת ביום - אם הסידור בו מדווחת הכנת מכונה התחיל עד 8 בבוקר (לא כולל) זוהי הכנת מכונה ראשונה. כל הכנת מכונה נוספת/מאוחרת משעה 8 בבוקר (כולל) נחשבת להכנת מכונה נוספת. זמן תקין להכנת מכונה  ראשונה הוא עד הערך בפרמטר 120 (זמן הכנת מכונה ראשונה), זמן תקין להכנת מכונה נוספת הוא עד הערך בפרמטר 121 (זמן הכנת מכונה נוספת). ביום עבודה יש מקסימום זמן לסה"כ הכנות מכונה נוספות, זמן תקין לפי פרמטר 122 (מכסימום יומי להכנות מכונה נוספות דקות). ביום עבודה יש מקסימום זמן לסה"כ הכנות מכונה (ראשונה ונוספות), זמן תקין לפי פרמטר 123 (מכסימום יומי להכנות מכונה  דקות).      מקסימום הכנות מכונה מותר בסידור         יש מקסימום למספר הכנות מכונה מותרות בסידור, נבדק לפי פרמטר 124 (מכסימום הכנות מכונה בסידור אחד), לא משנה מה הסוג שלהן.      
            if ((input.curPeilut.iMakatType == enMakatType.mElement.GetHashCode()) && (!String.IsNullOrEmpty(input.curPeilut.sShatYetzia)))
            {
                iElementType = int.Parse(input.curPeilut.lMakatNesia.ToString().Substring(0,3));                    
                if ((iElementType == 701) || (iElementType == 711))
                {
                    iElementTime = int.Parse(input.curPeilut.lMakatNesia.ToString().PadLeft(8).Substring(3, 3));
                    if ((iElementType == 701) && input.curPeilut.dFullShatYetzia < input.curPeilut.dFullShatYetzia.Date.AddHours(8) || DateHelper.CheckShaaton(input.iSugYom, input.CardDate, input.SugeyYamimMeyuchadim))
                    {
                        //מכונה ראשונה ביום- נשווה לפרמטר 120
                        bError = (iElementTime > input.oParameters.iPrepareFirstMechineMaxTime);
                        //צבירת זמן כל המכונות ביום ראשונה ונוספות נשווה לפרמטר 123
                        input.iTotalTimePrepareMechineForDay = input.iTotalTimePrepareMechineForDay + iElementTime;
                        //צבירת זמן כלל המכונות לסידור נשווה מול פרמטר 124
                        //iTotalTimePrepareMechineForSidur = iTotalTimePrepareMechineForSidur + iElementTime;
                        input.iTotalTimePrepareMechineForSidur = input.iTotalTimePrepareMechineForSidur + 1 ;
                    }
                    else if ((iElementType == 701) && input.curPeilut.dFullShatYetzia >= input.curPeilut.dFullShatYetzia.Date.AddHours(8) && !DateHelper.CheckShaaton(input.iSugYom, input.CardDate, input.SugeyYamimMeyuchadim))
                    {
                            //מכונות נוספות נשווה לפרמטר 121
                        bError = (iElementTime > input.oParameters.iPrepareOtherMechineMaxTime);
                            //צבירת זמן כל המכונות ביום ראשונה ונוספות נשווה לפרמטר 123
                            input.iTotalTimePrepareMechineForDay = input.iTotalTimePrepareMechineForDay + iElementTime;
                            //צבירת זמן כלל המכונות לסידור נשווה מול פרמטר 124
                            //iTotalTimePrepareMechineForSidur = iTotalTimePrepareMechineForSidur + iElementTime;
                            input.iTotalTimePrepareMechineForSidur = input.iTotalTimePrepareMechineForSidur + 1;

                            if (iElementType == 711)
                            {
                                //צבירת זמן כל המכונות הנוספות - נשווה בסוף מול פרמטר 122
                                input.iTotalTimePrepareMechineForOtherMechines = input.iTotalTimePrepareMechineForOtherMechines + iElementTime;
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
            get { return ErrorTypes.errTimeMechineInPeilutNotValid; }
        }
    }
}

