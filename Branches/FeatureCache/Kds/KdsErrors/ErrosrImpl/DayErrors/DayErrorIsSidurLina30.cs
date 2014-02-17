﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.DayErrors
{
    public class DayErrorIsSidurLina30 : DayErrorBase
    {
        public DayErrorIsSidurLina30(IUnityContainer container)
            : base(container)
        {
        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            string sLookUp = "";
            bool bError;
            bool isValid = true;

                //בדיקה ברמת יום עבודה                
            if (string.IsNullOrEmpty(input.OvedDetails.sLina))
            {
                bError = true;
            }
            else
            {
                sLookUp = GetLookUpKods("ctb_lina", input);
                bError = (sLookUp.IndexOf(input.OvedDetails.sLina) == -1);
            }
                             
            if (bError)
            {
                AddNewError(input);
                return false;
            }
                
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errLinaValueNotValid; }
        }
    }
    
}
