﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.PeilutErrors
{
    public class PeilutErrorElementInSpecialSidurNotAllowed123 : PeilutErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
           
            if (input.curSidur.bSidurMyuhad)
            {
                if (input.curPeilut.iMakatType == enMakatType.mElement.GetHashCode() && input.curPeilut.sDivuchInSidurMeyuchad == "1")
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
            get { return ErrorTypes.errElementInSpecialSidurNotAllowed; }
        }
    }
}
