using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsErrors.ErrosrImpl.PeilutErrors
{
    public class PeilutErrorElementNehigaForMatalaLoNahagut213 : PeilutErrorBase
    {
        public PeilutErrorElementNehigaForMatalaLoNahagut213(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {

            if (input.curSidur.bMatalaKlalitLeloRechev)
            {
                if (input.curPeilut.iMakatType == enMakatType.mElement.GetHashCode() && input.curPeilut.iSectorZviraZmanEelement == 5)
                {
                    AddNewError(input);
                    return false;
                }
            }
            //אלמנט אסור בסדור מיוחד
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errElementNehigaForMatalaLoNahagut213; }
        }
    }
}
