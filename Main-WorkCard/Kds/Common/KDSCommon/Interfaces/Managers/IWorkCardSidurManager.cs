using KDSCommon.DataModels.WorkCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.Interfaces.Managers
{
    public interface IWorkCardSidurManager
    {
        /// <summary>
        /// SidurimWC is the data related to the users sidurim and is extracted from the legacy WorkCardResult object
        /// </summary>
        /// <param name="misparIshi"></param>
        /// <param name="cardDate"></param>
        /// <param name="userId"></param>
        /// <param name="batchRequest"></param>
        /// <param name="isFromEmda"></param>
        /// <returns></returns>
        WorkCardResultContainer GetUserWorkCard(int misparIshi, DateTime cardDate, int userId = -2, int batchRequest = 0, bool isFromEmda = false);

        /// <summary>
        /// Returns a container with all the lookups required for the workcard view
        /// </summary>
        /// <returns></returns>
        WorkCardLookupsContainer GetWorkCardLookups();
    }
}
