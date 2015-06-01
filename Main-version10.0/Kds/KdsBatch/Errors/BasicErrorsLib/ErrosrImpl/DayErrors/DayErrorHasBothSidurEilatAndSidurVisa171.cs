using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KDSCommon.DataModels;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.DayErrors
{
    public class DayErrorHasBothSidurEilatAndSidurVisa171 : DayErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            bool hasSidurEilat = false;
            bool hasVisa = false;
            bool isLongNesiaToEilat = false;
            PeilutDM tmpPeilut = null;

            input.htEmployeeDetails.Values.Cast<SidurDM>()
                                    .ToList()
                                    .ForEach(
                                        sidur =>
                                        {
                                            if (sidur.bSidurEilat)
                                            {
                                                hasSidurEilat = true;

                                                if (sidur.IsLongEilatTrip(out tmpPeilut, input.oParameters.fOrechNesiaKtzaraEilat))
                                                    isLongNesiaToEilat = true;
                                            }

                                            if (sidur.bSidurVisaKodExists) hasVisa = true;
                                        }
                                    );

            if (hasSidurEilat && isLongNesiaToEilat && hasVisa)
            {
                AddNewError(input);
                return false;
            }
                
            return true;
            
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errHasBothSidurEilatAndSidurVisa; }
        }
    }
}
