using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.SidurErrors
{
    public class SidurErrorHachtamaYadanitYetziaMissing176: SidurErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {

            if (input.curSidur.iKodSibaLedivuchYadaniOut == 0)
                if (input.curSidur.bSidurMyuhad && !string.IsNullOrEmpty(input.curSidur.sShaonNochachut) && (string.IsNullOrEmpty(input.curSidur.sMikumShaonYetzia) || input.curSidur.sMikumShaonYetzia == "0") && CheckHourValid(input.curSidur.sShatGmar))
                {
                    AddNewError(input);
                    return false;
                }

            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errHachtamaYadanitYetzia; }
        }
    }
}
