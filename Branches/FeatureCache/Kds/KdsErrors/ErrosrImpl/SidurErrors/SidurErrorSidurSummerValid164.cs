using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KdsLibrary;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorSidurSummerValid164 : SidurErrorBase
    {
        public SidurErrorSidurSummerValid164(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (input.curSidur.bSidurMyuhad)
            {
                if (input.curSidur.bSidurInSummerExists && (input.curSidur.sSidurInSummer != "1" && input.curSidur.sSidurInSummer != "2" && input.curSidur.sSidurInSummer != "3" && input.curSidur.sSidurInSummer != "4"))
                {
                    //סידור של ארועי קיץ חייב להיות לעובד אשר הוגדר עובד 6 ימים (מזהים לפי ערך 61, 62) במאפיין 56 במאפייני עובדים. סידור של ארועי קיץ = סידור מיוחד עם מאפיין 73
                    if (((!input.oMeafyeneyOved.IsMeafyenExist(56))) || ((input.oMeafyeneyOved.IsMeafyenExist(56)) && (input.oMeafyeneyOved.GetMeafyen(56).IntValue != enMeafyenOved56.enOved6DaysInWeek1.GetHashCode())
                        && (input.oMeafyeneyOved.GetMeafyen(56).IntValue != enMeafyenOved56.enOved6DaysInWeek2.GetHashCode())))
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
            get { return ErrorTypes.errSidurSummerNotValidForOved; }
        }
    }
}