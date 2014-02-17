using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KDSCommon.DataModels;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.Interfaces.Managers;
using KdsLibrary.BL;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorNegativePremya154 : SidurErrorBase
    {
        public SidurErrorNegativePremya154(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            bool isSidurValid = true;
            bool isValid = true;
            double dTempPremia = 0;
            float fZmanSidur;
            double dElementsHamtanaReshut;
            PeilutDM oPeilutAchrona;
            bool bSidurNahagut;
           
            if (input.curSidur.htPeilut.Count > 0)
            {
                bSidurNahagut = IsSidurNahagut(input.drSugSidur, input.curSidur);
                if (bSidurNahagut)
                {
                    oPeilutAchrona = GetLastPeilutNoElementLeyedia(input.curSidur);
                    fZmanSidur = float.Parse(((oPeilutAchrona.dFullShatYetzia.AddMinutes(oPeilutAchrona.iMazanTichnun)) - input.curSidur.dFullShatHatchala).TotalMinutes.ToString());

                    //אם זמן הנוכחות (שעת גמר פחות שעת התחלה של  סידור) קטן מ- 350 - לא ממשיכים בבדיקה 
                    if (fZmanSidur >= 350)
                    {
                        var manager = ServiceLocator.Current.GetInstance<ISidurManager>();
                        dTempPremia = manager.CalculatePremya(input.curSidur.htPeilut, out dElementsHamtanaReshut);

                        if (!input.curSidur.bSidurMyuhad)
                        {//סידורי מפה

                            if (!input.bCheckBoolSidur)
                            {
                                if (dTempPremia < fZmanSidur)
                                {
                                    //אם הפער בין הפרמיה היא  שווה או פחות מ- % 20 מזמן הנוכחות - לא ממשיכים בבדיקה 
                                    if (dTempPremia - fZmanSidur > ((fZmanSidur * 20) / 100))
                                    {
                                        //בודקים האם סה"כ זמן האלמנטים מסוג המתנה ולרשות קטן מהפרמיה שחושבה בא' -שגוי.
                                        if (dElementsHamtanaReshut < dTempPremia)
                                        {
                                            isSidurValid = false;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (dTempPremia < fZmanSidur)
                            {
                                //הפער בין הפרמיה  היא שווה או פחות מ- % 20 מזמן הנוכחות - לא ממשיכים בבדיקה 
                                if (dTempPremia - fZmanSidur > ((fZmanSidur * 20) / 100))
                                {
                                    //בודקים האם סה"כ זמן האלמנטים מסוג המתנה ולרשות קטן מהפרמיה שחושבה בא' -שגוי.
                                    if (dElementsHamtanaReshut < dTempPremia)
                                    {
                                        isSidurValid = false;
                                    }
                                }
                            }

                        }
                        if (!isSidurValid)
                        {
                            AddNewError(input);
                            isValid = false;
                        }
                    }
                }
            }
         
            return isValid;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errNegativePremya; }
        }

        private PeilutDM GetLastPeilutNoElementLeyedia(SidurDM oSidur)
        {
            try
            {
                PeilutDM oPeilutAchrona = null;

                for (int i = oSidur.htPeilut.Count - 1; i >= 0; i--)
                {
                    oPeilutAchrona = (PeilutDM)oSidur.htPeilut[i];
                    if (oPeilutAchrona.iMakatType == enMakatType.mElement.GetHashCode())
                    {
                        if (oPeilutAchrona.iElementLeYedia != 2)
                        {
                            break;
                        }
                    }
                    else { break; }
                }

                return oPeilutAchrona;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}