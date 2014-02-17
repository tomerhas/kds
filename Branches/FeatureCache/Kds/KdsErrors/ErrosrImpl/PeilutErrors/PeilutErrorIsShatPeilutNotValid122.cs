using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using KdsLibrary;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.PeilutErrors
{
    public class PeilutErrorIsShatPeilutNotValid122 : PeilutErrorBase
    {
        public PeilutErrorIsShatPeilutNotValid122(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {

            long lMakatNesia = input.curPeilut.lMakatNesia;

            if ((enMakatType)input.curPeilut.iMakatType == enMakatType.mElement
                && input.curPeilut.iElementLeYedia == 2 && lMakatNesia > 0)
            {
                return true;
            }

            if (!(string.IsNullOrEmpty(input.curPeilut.sShatYetzia)))
            {
                if ((!((input.curPeilut.iMakatType == (long)enMakatType.mElement.GetHashCode()) && (input.curPeilut.iElementLeYedia == 2))) && (lMakatNesia > 0))
                {
                    if (input.curSidur.dFullShatGmar != DateTime.MinValue)
                    {//בדיקה 122
                        if (input.curPeilut.dFullShatYetzia > input.curSidur.dFullShatGmar)
                        {
                            AddNewError(input);
                            return false;
                        }
                    }
                }
            }
            //שעת פעילות גדולה משעת סיום הסידור
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errShatPeilutBiggerThanShatGmarSidur; }
        }
    }
}

