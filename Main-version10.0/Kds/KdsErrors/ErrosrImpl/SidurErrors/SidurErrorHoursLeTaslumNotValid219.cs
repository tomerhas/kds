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
    public class SidurErrorHoursLeTaslumNotValid219 : SidurErrorBase
    {
        public SidurErrorHoursLeTaslumNotValid219(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (input.iSidur > 0)//לא נבצע את הבדיקה לסידור הראשון
            {
                SidurDM oPrevSidur = (SidurDM)input.htEmployeeDetails[input.iSidur - 1];
                int iPrevLoLetashlum = oPrevSidur.iLoLetashlum;

                for (int i = (input.iSidur - 1); i >= 0; i--)
                {
                    oPrevSidur = (SidurDM)input.htEmployeeDetails[i];

                    iPrevLoLetashlum = oPrevSidur.iLoLetashlum;
                    if (iPrevLoLetashlum == 0)
                    {
                        break;
                    }
                }

                string sShatGmarPrev = oPrevSidur.sShatGmar;
                DateTime dShatHatchalaSidur = input.curSidur.dFullShatHatchalaLetashlum;
                DateTime dShatGmarPrevSidur = oPrevSidur.dFullShatGmarLetashlum;

                //  if (dShatHatchalaSidur != DateTime.MinValue && dShatGmarPrevSidur != DateTime.MinValue)
                if (dShatHatchalaSidur.Date != DateTime.MinValue.Date && dShatGmarPrevSidur.Date != DateTime.MinValue.Date)
                {
                    DateTime dPrevTime = new DateTime(dShatGmarPrevSidur.Year, dShatGmarPrevSidur.Month, dShatGmarPrevSidur.Day, int.Parse(dShatGmarPrevSidur.ToString("HH:mm").Substring(0, 2)), int.Parse(dShatGmarPrevSidur.ToString("HH:mm").Substring(3, 2)), 0);
                    DateTime dCurrTime = new DateTime(dShatHatchalaSidur.Year, dShatHatchalaSidur.Month, dShatHatchalaSidur.Day, int.Parse(dShatHatchalaSidur.ToString("HH:mm").Substring(0, 2)), int.Parse(dShatHatchalaSidur.ToString("HH:mm").Substring(3, 2)), 0);
                    //אם גם הסידור הקודם וגם הסידור הנוכחי הם לתשלום, נבצע את הבדיקה
                    if (input.curSidur.iLoLetashlum == 0 && iPrevLoLetashlum == 0)
                    {
                        if (dCurrTime < dPrevTime)
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
            get { return ErrorTypes.errSidurimChofefimBeshaotLetashlum219; }
        }
    }
}