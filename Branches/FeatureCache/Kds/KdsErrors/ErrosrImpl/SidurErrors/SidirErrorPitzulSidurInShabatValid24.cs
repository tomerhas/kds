using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KDSCommon.DataModels;
using KdsLibrary;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    class SidirErrorPitzulSidurInShabatValid24 : SidurErrorBase
    {
        public SidirErrorPitzulSidurInShabatValid24(IUnityContainer container)
            : base(container)
        {

        }

        public override bool InternalIsCorrect(ErrorInputData input)
        {
            bool isValid = true;
            DateTime dSidurPrevShatGmar;
            DateTime dSidurShatHatchala;
            SidurDM oPrevSidur = (SidurDM)input.htEmployeeDetails[input.iSidur - 1];
            string sSidurPrevShatGmar = oPrevSidur.sShatGmar;
            int iSidurPrevPitzulHafsaka = string.IsNullOrEmpty(oPrevSidur.sPitzulHafsaka) ? 0 : int.Parse(oPrevSidur.sPitzulHafsaka);

            if (!(string.IsNullOrEmpty(input.curSidur.sShatHatchala)))
            {
                //אם  יום שישי או ערב חג אבל  לא בשבתון
                if ((((input.curSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()) || ((input.curSidur.sErevShishiChag == "1") && (input.curSidur.sSidurDay != clGeneral.enDay.Shabat.GetHashCode().ToString())))) && (iSidurPrevPitzulHafsaka > 0))
                {
                    //נקרא את שעת כניסת השבת                   
                    //אם ביום שהוא ערב שבת/חג יש סידור אחד שמסתיים לפני כניסת שבת ויש לו סימון בשדה פיצול והסידור העוקב אחריו התחיל אחרי כניסת השבת  - זו שגיאה. (מצב תקין הוא שהסידור העוקב התחיל לפני כניסת שבת וגלש/לא גלש לשבת). 
                    //if (((int.Parse(sSidurPrevShatGmar.Remove(2, 1)) > iShabatStart)) || (int.Parse(input.curSidur.sShatHatchala.Remove(2, 1)) <= iShabatStart))
                    dSidurPrevShatGmar = GetDateTimeFromStringHour(sSidurPrevShatGmar, input.CardDate);
                    dSidurShatHatchala = GetDateTimeFromStringHour(input.curSidur.sShatHatchala, input.CardDate);
                    if ((dSidurPrevShatGmar <= input.oParameters.dKnisatShabat) && (dSidurShatHatchala > input.oParameters.dKnisatShabat))
                    {
                        isValid = false;
                    }
                    if ((dSidurPrevShatGmar > input.oParameters.dKnisatShabat) && (dSidurShatHatchala > input.oParameters.dKnisatShabat))
                      isValid = false;
                }
            }
            if(!isValid)
             AddNewError(input);
             
            return isValid;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errPitzulSidurInShabat; }
        }
    }
}

 
