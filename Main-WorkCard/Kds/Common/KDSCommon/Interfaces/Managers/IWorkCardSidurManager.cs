using KDSCommon.DataModels.WorkCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.Interfaces.Managers
{
    public interface IWorkCardSidurManager
    {
        SidurimWC GetSidurim(int misparIshi, DateTime cardDate, int userId = -2, int batchRequest = 0, bool isFromEmda = false);
    }
}
