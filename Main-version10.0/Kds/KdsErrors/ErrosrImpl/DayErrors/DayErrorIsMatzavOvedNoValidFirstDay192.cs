using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KDSCommon.DataModels;
using Microsoft.Practices.Unity;
using KDSCommon.Interfaces.Managers;

namespace KdsErrors.ErrosrImpl.DayErrors
{
    public class DayErrorIsMatzavOvedNoValidFirstDay192 : DayErrorBase
    {
        public DayErrorIsMatzavOvedNoValidFirstDay192(IUnityContainer container)
            : base(container)
        {
        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            int iCountSidurim;
           
            if (input.dTarTchilatMatzav == input.CardDate)
            {
                iCountSidurim = input.htEmployeeDetails.Values.Cast<SidurDM>().ToList().Count(Sidur => Sidur.iNitanLedaveachBemachalaAruca == 0 && Sidur.iLoLetashlum == 0);
                //עובד לא יכול לעבוד בכל עבודה באגד אם במחלה. מחלה זה סטטוס ב- HR. מזהים שהעובד עבד ביום מסוים אם יש לו לפחות סידור אחד ביום זה.  
                if ((_container.Resolve<IOvedManager>().IsOvedMatzavExists("5", input.dtMatzavOved)) && (iCountSidurim > 0))
                {
                    AddNewError(input);
                    return false;
                }
            }
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errMatzavOvedNotValidFirstDay; }
        }
    }
}
