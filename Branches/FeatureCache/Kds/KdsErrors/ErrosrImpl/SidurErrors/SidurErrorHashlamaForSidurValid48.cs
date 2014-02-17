using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KdsLibrary;
using Microsoft.Practices.Unity;
using KDSCommon.Helpers;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorHashlamaForSidurValid48 : SidurErrorBase
    {
        public SidurErrorHashlamaForSidurValid48(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            float fSidurTime;
            fSidurTime = float.Parse((!string.IsNullOrEmpty(input.curSidur.sShatGmar) && !string.IsNullOrEmpty(input.curSidur.sShatHatchala) ? (input.curSidur.dFullShatGmar - input.curSidur.dFullShatHatchala).TotalMinutes : 0).ToString()); //clDefinitions.GetSidurTimeInMinuts(oSidur);

            if (!string.IsNullOrEmpty(input.curSidur.sHashlama))
            {
                if ((int.Parse(input.curSidur.sHashlama)) > 0)
                {
                    if ((!(string.IsNullOrEmpty(input.curSidur.sShatGmar))) && (input.curSidur.dFullShatHatchala.Year > DateHelper.cYearNull))
                    {
                        if (fSidurTime / 60 > int.Parse(input.curSidur.sHashlama))
                        {
                            AddNewError(input);
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errHashlamaForSidurNotValid; }
        }
    }
}