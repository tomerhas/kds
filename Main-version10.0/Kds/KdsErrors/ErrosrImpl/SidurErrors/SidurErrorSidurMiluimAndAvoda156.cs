using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KDSCommon.DataModels;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorSidurMiluimAndAvoda156 : SidurErrorBase
    {
        public SidurErrorSidurMiluimAndAvoda156(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (input.curSidur.sHeadrutTypeKod == "3")
            {
                if (input.htEmployeeDetails.Values.Cast<SidurDM>().ToList().Any(sidur => !IsSidurHeadrut(sidur) && (sidur.iLoLetashlum != 1 || sidur.iLoLetashlum == 0 && sidur.iKodSibaLoLetashlum == 1) && sidur.iMisparSidur != input.curSidur.iMisparSidur && sidur.dFullShatHatchala != input.curSidur.dFullShatHatchala))
                {
                    AddNewError(input);
                    return false;
                }

            }
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errMiluimAndAvoda; }
        }
    }
}