﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorHachtamaYadanitKnisaMissing175 : SidurErrorBase
    {
        public SidurErrorHachtamaYadanitKnisaMissing175(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (input.curSidur.iKodSibaLedivuchYadaniIn == 0)
                if (input.curSidur.bSidurMyuhad && !string.IsNullOrEmpty(input.curSidur.sShaonNochachut) && (string.IsNullOrEmpty(input.curSidur.sMikumShaonKnisa) || input.curSidur.sMikumShaonKnisa == "0") && CheckHourValid(input.curSidur.sShatHatchala))
                {
                    AddNewError(input);
                    return false;
                }

            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errHachtamaYadanitKnisa; }
        }
    }
}