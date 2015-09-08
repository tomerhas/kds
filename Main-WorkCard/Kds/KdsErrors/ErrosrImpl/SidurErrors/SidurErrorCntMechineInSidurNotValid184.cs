using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorCntMechineInSidurNotValid184 : SidurErrorBase
    {
        public SidurErrorCntMechineInSidurNotValid184(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {

            //נשווה את זמן כל האלמנטים של הכנת מכונה בסידור לפרמטר 124, אם עלה על המותר נעלה שגיאה
            if (input.iTotalTimePrepareMechineForSidur > input.oParameters.iPrepareAllMechineTotalMaxTimeForSidur)
            {
                AddNewError(input);
                return false;
            }
                      
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errCntMechineInSidurNotValid; }
        }
    }
}