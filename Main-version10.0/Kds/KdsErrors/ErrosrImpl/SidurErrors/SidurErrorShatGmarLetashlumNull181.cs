﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorShatGmarLetashlumNull181 : SidurErrorBase
    {
        public SidurErrorShatGmarLetashlumNull181(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (input.curSidur.dFullShatGmarLetashlum == DateTime.MinValue || input.curSidur.dFullShatGmarLetashlum.Date == DateTime.MinValue.Date)
            {
                AddNewError(input);
                return false;
            }
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.IsShatGmarLetashlumNull; }
        }
    }
}