using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.UDT;

namespace KDSCommon.Interfaces.Managers
{
    public interface IShinuyimManager
    {
        DataTable GetIdkuneyRashemet(int iMisparIshi, DateTime dTaarich);
        DataTable GetApprovalErrors(int iMisparIshi, DateTime dTaarich);
        void SaveIdkunRashemet(COLL_IDKUN_RASHEMET oCollIdkunRashemet);
        void DeleteIdkunRashemet(COLL_IDKUN_RASHEMET oCollIdkunRashemetDel);
        void UpdateAprrovalErrors(COLL_SHGIOT_MEUSHAROT oCollShgiotMeusharot);
        void SaveShinuyKelet(ShinuyInputData inputData);
    }
}
