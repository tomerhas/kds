﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.PeilutErrors
{
    public class PeilutErrorIsTeoodatNesiaValid52 : PeilutErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if ((!(input.curSidur.bSidurMyuhad && (input.curSidur.bSidurVisaKodExists))) && (input.curPeilut.lMisparVisa > 0))
            {
                AddNewError(input);
                return false;
            }
            
            //תעודת נסיעה לא בסדור ויזה
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errTeoodatNesiaNotInVisa; }
        }
    }
}
