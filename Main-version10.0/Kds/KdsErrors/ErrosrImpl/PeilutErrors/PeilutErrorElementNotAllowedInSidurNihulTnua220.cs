
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using KDSCommon.Interfaces.Managers;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
namespace KdsErrors.ErrosrImpl.PeilutErrors
{
    public class PeilutErrorElementNotAllowedInSidurNihulTnua220 : PeilutErrorBase
    {
        public PeilutErrorElementNotAllowedInSidurNihulTnua220(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            string kod;
            int sug_sidur,mfyn21;
            bool bError = false;
            if (input.CardDate>=input.oParameters.dParam320 && input.drSugSidur.Length > 0)
            {
                if (input.drSugSidur[0]["sector_avoda"].ToString() == enSectorAvoda.Nihul.GetHashCode().ToString())
                {
                    if (input.curPeilut.iMakatType == enMakatType.mElement.GetHashCode())
                    {
                        kod= input.curPeilut.lMakatNesia.ToString().Substring(1,2);
                        var peilutManager = _container.Resolve<IPeilutManager>();
                        var dtTmpMeafyeneyElements = peilutManager.GetTmpMeafyeneyElements(input.CardDate, input.CardDate);
                        DataRow drMeafyeneyElements = dtTmpMeafyeneyElements.Select("kod_element=" + kod)[0];
                        
                        if (string.IsNullOrEmpty(drMeafyeneyElements["nihul_tnua"].ToString()))
                            bError = true;
                        else
                        {
                            sug_sidur = int.Parse(input.drSugSidur[0]["sug_sidur"].ToString());
                            mfyn21 = int.Parse(drMeafyeneyElements["nihul_tnua"].ToString());
                            if (sug_sidur < mfyn21 || sug_sidur > (mfyn21 + 2))
                                bError = true;
                        }
                    }
                }
             }

            if(bError)
                AddNewError(input);
            return bError;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errElementNotAllowedInSidurNihulTnua220; }
        }
    }
}
