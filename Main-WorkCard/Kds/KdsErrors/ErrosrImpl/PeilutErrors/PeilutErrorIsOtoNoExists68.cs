using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.PeilutErrors
{
    public class PeilutErrorIsOtoNoExists68 : PeilutErrorBase
    {
        public PeilutErrorIsOtoNoExists68(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
           
            //בדיקה ברמת פעילות
          
            //אסור לדווח רכב בסידור שיש לו מאפיין 43 (אסור לדווח מספר רכב). הבדיקה רלוונטית גם לסידורים מיוחדים וגם לסידורים רגילים.                
           
                //TB_Sidurim_Meyuchadim נבדוק אם קיים מאפיין 43: לסידורים מיוחדים נבדוק בטבלת 
                //לסידורים רגילים נבדוק את המאפיין מהתנועה
                if (input.curSidur.bSidurMyuhad)
                {
                    if ((input.curSidur.bNoOtoNoExists) && (input.curSidur.sNoOtoNo == "1") && IsPeilutDoreshetMisparRechev(input.curPeilut))
                    {
                        AddNewError(input);
                        return false;
                    }
                    
                }
                else //סידור רגיל
                {
                    if (input.drSugSidur.Length > 0)
                    {
                        if ((!String.IsNullOrEmpty(input.drSugSidur[0]["asur_ledaveach_mispar_rechev"].ToString())) && (input.drSugSidur[0]["asur_ledaveach_mispar_rechev"].ToString() == "1") && IsPeilutDoreshetMisparRechev(input.curPeilut))
                        {
                            AddNewError(input);
                            return false;
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


