using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KDSCommon.DataModels;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.SidurErrors
{
    public class SidurErrorKupaiWithNihulTnua187 : SidurErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            
            string sug_sidur;
            PeilutDM oFirstPeilut;

            if (input.drSugSidur.Length > 0)
            {
                sug_sidur = input.drSugSidur[0]["sug_sidur"].ToString();
                if (sug_sidur == "70" || sug_sidur == "71" || sug_sidur == "72")
                {
                    oFirstPeilut = input.curSidur.htPeilut.Values.Cast<PeilutDM>().ToList().FirstOrDefault(peilut => (peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "730" ||
                                            peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "740" ||
                                            peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "750"));

                    if (oFirstPeilut != null)
                    {
                        AddNewError(input);
                        return false;
                    }
                }
            }
                 
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errKupaiWithNihulTnua; }
        }
    }
}