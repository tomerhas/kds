﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KDSCommon.DataModels;
using KdsLibrary;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.SidurErrors
{
    public class SidurErrorChofeshAlCheshbonShaotNosafot188 : SidurErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            int iCountSidurim;
           
                if (input.curSidur.iMisparSidur == 99822)
                {

                    iCountSidurim = input.htEmployeeDetails.Values.Cast<SidurDM>().ToList().Count(Sidur => ((Sidur.iMisparSidur != input.curSidur.iMisparSidur || Sidur.dFullShatHatchala != input.curSidur.dFullShatHatchala) && Sidur.iLoLetashlum == 0));
                    if (iCountSidurim > 0 || input.oMeafyeneyOved.GetMeafyen(56).IntValue != clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
                    {
                        AddNewError(input);
                        return false;
                    }
                }

            
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errChofeshAlCheshbonShaotNosafot; }
        }
    }
}