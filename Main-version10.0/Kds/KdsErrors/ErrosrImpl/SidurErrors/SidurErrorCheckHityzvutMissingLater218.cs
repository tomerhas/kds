using KDSCommon.DataModels;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsErrors.ErrosrImpl.SidurErrors
{
    public class SidurErrorCheckHityzvutMissingLater218 : SidurErrorBase
    {
        public SidurErrorCheckHityzvutMissingLater218(IUnityContainer container)
            : base(container)
        {

        }

        public override bool InternalIsCorrect(ErrorInputData input)
        {
            bool SidurLeBdika = false;
            if (input.curSidur.bSidurMyuhad)
            {
                if (input.curSidur.bSidurLebdikatHityazvut)
                    SidurLeBdika = true;
            }
            else
            {//סידור רגיל
                if (input.drSugSidur.Length > 0)
                {
                    if (input.drSugSidur[0]["sidur_lebdikat_hityazvut"].ToString() !="" && int.Parse(input.drSugSidur[0]["sidur_lebdikat_hityazvut"].ToString())>0)
                        SidurLeBdika = true;
                }
            }

            if (SidurLeBdika && (input.curSidur.iNidreshetHitiatzvut==1 || input.curSidur.iNidreshetHitiatzvut == 2))
            {
                if (input.curSidur.dShatHitiatzvut == DateTime.MinValue || input.curSidur.dShatHitiatzvut > input.curSidur.dFullShatHatchala)
                {
                    AddNewError(input);
                    return false;
                }
            }

            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errHityzvutMissingLater218; }
        }
    }
}
