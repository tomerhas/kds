using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using System.Data;
using KdsLibrary.BL;
using KDSCommon.DataModels;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.DayErrors
{
    public class DayErrorHasHafifa167 : DayErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            bool hasHafifa = false;
            string hafifaDescription = string.Empty;

            clOvdim ovdim = new clOvdim();
            DateTime shatHatchalaOfPrevDay = DateTime.MinValue;
            DateTime shatGmarOfPrevDay = DateTime.MinValue;
            DateTime shatHatchalaOfNextDay = DateTime.MinValue;
            DateTime shatGmarOfNextDay = DateTime.MinValue;
            DataTable dtSidur = ovdim.GetSidur("last", input.iMisparIshi, input.CardDate.AddDays(-1));
            if (dtSidur != null && dtSidur.Rows.Count > 0)
            {
                DateTime.TryParse(dtSidur.Rows[dtSidur.Rows.Count - 1]["shat_hatchala"].ToString(), out shatHatchalaOfPrevDay);
                DateTime.TryParse(dtSidur.Rows[dtSidur.Rows.Count - 1]["shat_gmar"].ToString(), out shatGmarOfPrevDay);
            }
            if (input.htEmployeeDetails.Count > 0)
            {
                SidurDM firstSidurOfTheDay = input.htEmployeeDetails[0] as SidurDM;
                SidurDM lastSidurOfTheDay = input.htEmployeeDetails[input.htEmployeeDetails.Count - 1] as SidurDM;
                dtSidur = ovdim.GetSidur("first", input.iMisparIshi, input.CardDate.AddDays(1));
                if (dtSidur != null && dtSidur.Rows.Count > 0)
                {
                    DateTime.TryParse(dtSidur.Rows[0]["shat_hatchala"].ToString(), out shatHatchalaOfNextDay);
                    DateTime.TryParse(dtSidur.Rows[0]["shat_gmar"].ToString(), out shatGmarOfNextDay);
                }

                if (firstSidurOfTheDay.iLoLetashlum == 0 && shatGmarOfPrevDay != DateTime.MinValue &&
                    shatGmarOfPrevDay.Date == firstSidurOfTheDay.dFullShatHatchala.Date &&
                    (shatGmarOfPrevDay - firstSidurOfTheDay.dFullShatHatchala) > TimeSpan.Zero)
                {
                    hasHafifa = true;
                    hafifaDescription = "חפיפה עם יום קודם";
                }

                if (lastSidurOfTheDay.iLoLetashlum == 0 && shatHatchalaOfNextDay != DateTime.MinValue &&
                    lastSidurOfTheDay.dFullShatGmar != DateTime.MinValue &&
                    lastSidurOfTheDay.dFullShatGmar.Date == shatHatchalaOfNextDay.Date &&
                    (lastSidurOfTheDay.dFullShatGmar - shatHatchalaOfNextDay) > TimeSpan.Zero)
                {
                    hasHafifa = true;
                    hafifaDescription = "חפיפה עם יום עוקב";
                }

                if (hasHafifa)
                {
                    AddNewError(input);
                    return false;
                }
                 
            }
            
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errHafifaBetweenSidurim; }
        }
    }
}
