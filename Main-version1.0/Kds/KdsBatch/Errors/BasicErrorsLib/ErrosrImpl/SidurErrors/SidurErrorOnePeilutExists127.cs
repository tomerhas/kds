using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.SidurErrors
{
    public class SidurErrorOnePeilutExists127 : SidurErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (((input.curSidur.bSidurMyuhad) && (input.curSidur.bPeilutRequiredKodExists) && (input.curSidur.iMisparSidurMyuhad > 0)) || (!(input.curSidur.bSidurMyuhad)))
            {
                //אם אין פעילויות נעלה שגיאה
                if (input.curSidur.htPeilut.Count == 0)
                {
                    AddNewError(input);
                    return false;
                }
            }    
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errAtLeastOnePeilutRequired; }
        }
    }
}