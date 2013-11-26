using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KdsLibrary.BL;
using System.Data;
using KDSCommon.Enums;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.DayErrors
{
    public class DayErrorDuplicateShatYetiza103 : CardErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            bool isValid =true;
            Dictionary<string, clPeilut> peiluyot = new Dictionary<string, clPeilut>();
            bool shouldProcess = true;
            clKavim oKavim = new clKavim();
            DataTable dtMeafyenim = null;

            input.htEmployeeDetails.Values.Cast<clSidur>().ToList().ForEach(sidur =>
                {
                    sidur.htPeilut.Values.Cast<clPeilut>().ToList().ForEach(peilut =>
                        {
                            if (isValid)
                            {
                                shouldProcess = ComputeShouldProcess(oKavim, ref dtMeafyenim, peilut,input.CardDate);

                                if (shouldProcess)
                                {
                                    if (!peiluyot.ContainsKey(peilut.dFullShatYetzia.ToString()))
                                    {
                                        peiluyot.Add(peilut.dFullShatYetzia.ToString(), peilut);
                                    }
                                    else
                                    {
                                        if (peilut.iMisparKnisa == 0 && peiluyot[peilut.dFullShatYetzia.ToString()].iMisparKnisa == 0)
                                        {
                                            isValid = false;
                                        }
                                    }
                                }
                            }
                        }
                    );
                }
            );

            return isValid;
        }

        private bool ComputeShouldProcess(clKavim oKavim, ref DataTable dtMeafyenim, clPeilut peilut, DateTime cardDate)
        {
            bool shouldProcess = true;
            if ((clKavim.enMakatType)peilut.iMakatType == clKavim.enMakatType.mElement)
            {
                dtMeafyenim = oKavim.GetMeafyeneyElementByKod(peilut.lMakatNesia, cardDate);
                if (dtMeafyenim.Select("KOD_MEAFYEN = 9") != null)
                {
                    shouldProcess = false;
                }
                else
                {
                    shouldProcess = true;
                }
            }
            else
            {
                shouldProcess = true;
            }
            return shouldProcess;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errDuplicateShatYetiza; }
        }
    }
}
