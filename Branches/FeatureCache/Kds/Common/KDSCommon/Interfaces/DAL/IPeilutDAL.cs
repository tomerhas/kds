using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace KDSCommon.Interfaces.DAL
{
    public interface IPeilutDAL
    {
        bool IsDuplicateTravle(int iMisparIshi, DateTime dCardDate, long lMakatNesia, DateTime dShatYetzia, int iMisparKnisa, ref DataTable dt);
        DataTable GetTmpMeafyeneyElements(DateTime dTarMe, DateTime dTarAd);
 
    }
}
