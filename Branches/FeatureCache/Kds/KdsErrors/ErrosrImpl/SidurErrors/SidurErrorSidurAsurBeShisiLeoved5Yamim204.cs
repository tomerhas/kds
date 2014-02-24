using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors
{
    public class SidurErrorSidurAsurBeShisiLeoved5Yamim204: SidurErrorBase
    {
        public SidurErrorSidurAsurBeShisiLeoved5Yamim204(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {

            if (input.iSugYom == enSugYom.Shishi.GetHashCode())
            {
                if (input.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || input.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
                {
                    if (input.curSidur.bSidurAsurBeyomShishi)
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
            get { return ErrorTypes.errSidurAsurBeyomShishiLeoved5Yamim204; }
        }
    }
}