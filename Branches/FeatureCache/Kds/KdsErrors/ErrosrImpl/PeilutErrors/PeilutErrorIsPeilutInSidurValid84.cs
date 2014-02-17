using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.PeilutErrors
{
    public class PeilutErrorIsPeilutInSidurValid84 : PeilutErrorBase
    {
        public PeilutErrorIsPeilutInSidurValid84(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            //בדיקה ברמת סידור          
            int iPeilutMisparSidur;
            bool flag = true;
          
            //סידור שאסור לדווח בו פעילויות. נבדק רק מול סידורים מיוחדים.                                   
            //
            if ((input.curSidur.bSidurMyuhad) && (input.curSidur.bNoPeilotKodExists))
            {
                iPeilutMisparSidur = input.curPeilut.iPeilutMisparSidur;
                if (iPeilutMisparSidur > 0)
                {
                    if (input.curSidur.htPeilut.Count == 1)
                    {
                        if (input.curPeilut.iMakatType == enMakatType.mElement.GetHashCode() && input.curPeilut.bMisparSidurMatalotTnuaExists && input.curPeilut.iMisparSidurMatalotTnua == iPeilutMisparSidur)
                            flag = false;
                    }
                        
                    if (flag)
                    {
                        AddNewError(input);
                        return false;
                    }
                }
                    
            }
            //פעילות אסורה בסדור תפקיד
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errPeilutForSidurNonValid; }
        }
    }
}


