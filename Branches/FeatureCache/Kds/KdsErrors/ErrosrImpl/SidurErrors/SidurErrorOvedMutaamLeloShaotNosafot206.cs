using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using KDSCommon.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors
{
    public class SidurErrorOvedMutaamLeloShaotNosafot206 : SidurErrorBase
    {
        public SidurErrorOvedMutaamLeloShaotNosafot206(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            //בדיקה ברמת סידור         
            bool isValid = true;
            int iMutamut;
            int iIsurShaotNosafot = 0;
            DataRow[] dr;

            if (input.curSidur.iLoLetashlum == 0 && input.OvedDetails.sMutamut.Trim() != "")
            {
                iMutamut = int.Parse(input.OvedDetails.sMutamut);
                if (iMutamut > 0)
                {
                    var cache = ServiceLocator.Current.GetInstance<IKDSCacheManager>();
                    dr =  cache.GetCacheItem<DataTable>(CachedItems.Mutamut).Select(string.Concat("kod_mutamut=", iMutamut));
                    if (dr.Length > 0)
                    {
                        iIsurShaotNosafot = string.IsNullOrEmpty(dr[0]["isur_shaot_nosafot"].ToString()) ? 0 : int.Parse(dr[0]["isur_shaot_nosafot"].ToString());
                    }

                    if (iIsurShaotNosafot > 0)
                    {
                        if ((input.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || input.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek2.GetHashCode()) &&
                            (CheckShaaton(input.iSugYom, input.CardDate, input) || input.curSidur.sSidurDay ==  "6"))
                            isValid = false;

                        if ((input.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved6DaysInWeek1.GetHashCode() || input.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved6DaysInWeek2.GetHashCode()) &&
                                CheckShaaton(input.iSugYom, input.CardDate, input))
                            isValid = false;
                    }
                }
            }

            if (!isValid)
            {
                AddNewError(input);
                return false;
            }
          
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errOvedMutaamLeloShaotNosafot206; }
        }
    }
}