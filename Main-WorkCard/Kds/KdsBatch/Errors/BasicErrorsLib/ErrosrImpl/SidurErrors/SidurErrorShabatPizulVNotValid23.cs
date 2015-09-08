﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.SidurErrors
{
    public class SidurErrorShabatPizulVNotValid23 : SidurErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (CheckShaaton(input.iSugYom, input.CardDate, input) && (!string.IsNullOrEmpty(input.curSidur.sPitzulHafsaka)) && input.curSidur.sPitzulHafsaka != "0")
            {
                AddNewError(input);
                return false;
            }
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errShabatPizulValueNotValid; }
        }
    }
}