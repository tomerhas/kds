using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.PeilutErrors
{
    public class PeilutErrorIsOtoNoExists68 : PeilutErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            bool isValid = true;

            //בדיקה ברמת פעילות
          
            //אסור לדווח רכב בסידור שיש לו מאפיין 43 (אסור לדווח מספר רכב). הבדיקה רלוונטית גם לסידורים מיוחדים וגם לסידורים רגילים.                
            if (input.curPeilut.lOtoNo > 0)
            {
                //TB_Sidurim_Meyuchadim נבדוק אם קיים מאפיין 43: לסידורים מיוחדים נבדוק בטבלת 
                //לסידורים רגילים נבדוק את המאפיין מהתנועה
                if (input.curSidur.bSidurMyuhad)
                {
                    if ((input.curSidur.bNoOtoNoExists) && (input.curSidur.sNoOtoNo == "1"))
                    {
                        AddNewError(input);
                        return false;
                    }
                    
                }
                else //סידור רגיל
                {
                    if (input.drSugSidur.Length > 0)
                    {
                        if ((!String.IsNullOrEmpty(input.drSugSidur[0]["asur_ledaveach_mispar_rechev"].ToString())) && (input.drSugSidur[0]["asur_ledaveach_mispar_rechev"].ToString() == "1"))
                        {
                            AddNewError(input);
                            return false;
                        }
                    }
                }
            }
            //מספר רכב בסידור תפקיד
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errOtoNoExists; }
        }
    }
}


