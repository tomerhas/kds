using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KdsLibrary.BL;
using System.Data;
using KDSCommon.Enums;
using KDSCommon.DataModels;
using KDSCommon.Interfaces.DAL;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.DayErrors
{
    public class DayErrorDuplicateShatYetiza103 : DayErrorBase
    {
        public DayErrorDuplicateShatYetiza103(IUnityContainer container)
            : base(container)
        {
        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            bool isValid =true;
            Dictionary<string, PeilutDM> peiluyot = new Dictionary<string, PeilutDM>();
            bool shouldProcess = true;
            DataTable dtMeafyenim = null;

            input.htEmployeeDetails.Values.Cast<SidurDM>().ToList().ForEach(sidur =>
                {
                    sidur.htPeilut.Values.Cast<PeilutDM>().ToList().ForEach(peilut =>
                        {
                            if (isValid)
                            {
                                shouldProcess = ComputeShouldProcess( ref dtMeafyenim, peilut,input.CardDate);

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
                                            AddNewError(input);
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

        private bool ComputeShouldProcess( ref DataTable dtMeafyenim, PeilutDM peilut, DateTime cardDate)
        {
            bool shouldProcess = true;
            if ((enMakatType)peilut.iMakatType == enMakatType.mElement)
            {
                var kavimDal = ServiceLocator.Current.GetInstance<IKavimDAL>();
                dtMeafyenim = kavimDal.GetMeafyeneyElementByKod(peilut.lMakatNesia, cardDate);
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
