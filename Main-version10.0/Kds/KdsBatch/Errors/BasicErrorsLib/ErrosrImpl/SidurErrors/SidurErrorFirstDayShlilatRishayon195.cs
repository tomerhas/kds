using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KdsLibrary;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.SidurErrors
{
    public class SidurErrorFirstDayShlilatRishayon195 : SidurErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            bool bError = false;
          
            //ד. עובד הוא בשלילה (יודעים שעובד הוא בשלילה לפי ערך 1 בקוד בנתון 21 (שלילת   רשיון) בטבלת פרטי עובדים) 
            if (input.curSidur.bSidurMyuhad)
            {
                if (input.curSidur.sSugAvoda != clGeneral.enSugAvoda.ActualGrira.GetHashCode().ToString())
                {
                    if (input.curSidur.sSectorAvoda == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString())
                    {
                        if (input.CardDate == input.OvedDetails.dTaarichMe)
                            bError = IsOvedBShlila(input);
                    }
                }
            }
            else
            {//סידור רגיל
                if (input.drSugSidur.Length > 0)
                {
                    if (input.drSugSidur[0]["sug_Avoda"].ToString() != clGeneral.enSugAvoda.Grira.GetHashCode().ToString() && input.drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString())
                    {
                        if (input.CardDate == input.OvedDetails.dTaarichMe)
                            bError = IsOvedBShlila(input);
                    }
                }
            }


            if (bError)
            {
                AddNewError(input);
                return false;
            }

            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errFirstDayShlilatRishayon195; }
        }
    }
}