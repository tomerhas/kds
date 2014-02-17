using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace KDSCommon.Interfaces.Managers
{
    public interface IKavimManager
    {
        DataTable GetMakatDetails(long lNewMakat, DateTime dDate);
        bool IsElementValid(long lMakatNesia, DateTime dDate);
    }
}
