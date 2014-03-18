using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using System.Data;
using KDSCommon.UDT;

namespace KDSCommon.Interfaces.Managers
{
    public interface IPeilutManager
    {
        PeilutDM CreateClsPeilut(int iMisparIshi, DateTime dDateCard, OBJ_PEILUT_OVDIM oObjPeilutOvdimIns, System.Data.DataTable dtMeafyeneyElements);
        PeilutDM CreateEmployeePeilut(DateTime CardDate,int iKey, System.Data.DataRow dr, System.Data.DataTable dtPeiluyot);
        PeilutDM CreatePeilutFromOldPeilut(int iMisparIshi, DateTime dDateCard, PeilutDM oPeilutOld, long lMakatNesiaNew, DataTable dtMeafyeneyElements);
        bool IsMustBusNumber(PeilutDM cls, int iVisutMustRechevWC);
        bool IsDuplicateTravle(int iMisparIshi, DateTime dCardDate, long lMakatNesia, DateTime dShatYetzia, int iMisparKnisa, ref DataTable dt);
        DataTable GetTmpMeafyeneyElements(DateTime dTarMe, DateTime dTarAd); 
    }
}
