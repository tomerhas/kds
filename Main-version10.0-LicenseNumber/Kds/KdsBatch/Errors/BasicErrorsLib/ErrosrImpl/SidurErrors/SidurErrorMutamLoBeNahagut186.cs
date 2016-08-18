using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.SidurErrors
{
    public class SidurErrorMutamLoBeNahagut186 : SidurErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            int iMutamut;
            bool bSidurNahagut;
            if (input.OvedDetails.sMutamut.Trim() != "")
            {
                iMutamut = int.Parse(input.OvedDetails.sMutamut);
                bSidurNahagut = IsSidurNahagut(input.drSugSidur, input.curSidur);
                if (bSidurNahagut && (iMutamut == 4 || iMutamut == 5 || iMutamut == 9))
                {
                    AddNewError(input);
                    return false;
                }
            }

            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errMutamLoBeNahagutBizeaNahagut; }
        }
    }
}