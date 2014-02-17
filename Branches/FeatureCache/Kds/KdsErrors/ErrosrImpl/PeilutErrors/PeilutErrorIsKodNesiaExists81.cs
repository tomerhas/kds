using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.PeilutErrors
{
    public class PeilutErrorIsKodNesiaExists81 : PeilutErrorBase
    {
        public PeilutErrorIsKodNesiaExists81(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            bool isValid = true;
            //נבדוק מול התנועה אם סוג קוד נסיעה תקין
            //בדיקה ברמת פעילות  
            if (input.curPeilut.lMakatNesia.ToString().Length < 6)
                isValid = false;
            else if (input.curPeilut.iMakatType == enMakatType.mKavShirut.GetHashCode() || input.curPeilut.iMakatType == enMakatType.mNamak.GetHashCode() || input.curPeilut.iMakatType == enMakatType.mEmpty.GetHashCode())
            {
                if (input.curPeilut.iMakatValid != 0)
                    isValid = false;
            }
            else if (input.curPeilut.iMakatType == enMakatType.mElement.GetHashCode() && input.curPeilut.lMakatNesia.ToString().Substring(0, 3) != "700" && (input.curPeilut.lMakatNesia.ToString().Length < 8 || input.curPeilut.iMakatValid != 0))
                isValid = false;

            if (!isValid)
            {
                AddNewError(input);
                return false;
            }
           
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errKodNesiaNotExists; }
        }
    }
}


