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
    public class SidurErrorSidurEilatValid55 : SidurErrorBase
    {
        public SidurErrorSidurEilatValid55(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            bool isValid = true;
            bool bCurrSidurEilat = false;
            string par = string.Empty;
            SidurDM oPrevSidur = null;
            PeilutDM tmpPeilut = null;
            float SachHamtana = 0;
            if (input.curSidur.bSidurEilat && input.curSidur.IsLongEilatTrip(out tmpPeilut, input.oParameters.fOrechNesiaKtzaraEilat))
            {
                bCurrSidurEilat = true;
            }

            // if the current sidur isn't SidurEilat then we shouldn't check anything
            if (!bCurrSidurEilat) isValid = false;
            else
            {
                bool bPrevSidurEilat = false;

                for (int index = 0; index < input.iSidur; index++)
                {
                    oPrevSidur = (SidurDM)input.htEmployeeDetails[index];

                    if (oPrevSidur.bSidurEilat && oPrevSidur.IsLongEilatTrip( out tmpPeilut, input.oParameters.fOrechNesiaKtzaraEilat))
                    {
                        bPrevSidurEilat = true;
                    }

                    if (bPrevSidurEilat) break;
                }

                if (bPrevSidurEilat && bCurrSidurEilat)
                {
                    SachHamtana = oPrevSidur.htPeilut.Values.Cast<PeilutDM>().ToList().Sum(peilut =>
                    {
                        if (peilut.bElementHamtanaExists && peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "735")
                            return Int32.Parse(peilut.lMakatNesia.ToString().PadLeft(8).Substring(3, 3));
                        else return 0;
                    });

                    if ((input.curSidur.dFullShatHatchala.Subtract(oPrevSidur.dFullShatGmar).TotalMinutes < 60)
                            && ((SachHamtana + input.curSidur.dFullShatHatchala.Subtract(oPrevSidur.dFullShatGmar).TotalMinutes) < 60))
                    {
                        isValid = false;
                        AddNewError(input);
                    }
                    
                }
         }
     
            return isValid;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errSidurEilatNotValid; }
        }
    }
}