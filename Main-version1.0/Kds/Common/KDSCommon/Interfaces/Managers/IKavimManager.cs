using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;

namespace KDSCommon.Interfaces.Managers
{
    public interface IKavimManager
    {
        DataTable GetMakatDetails(long lNewMakat, DateTime dDate);
        bool IsElementValid(long lMakatNesia, DateTime dDate);
        string GetMasharCarNumbers(OrderedDictionary htEmployeeDetails);
        DataTable GetKatalogKavim(int iMisparIshi, DateTime dFromDate, DateTime dToDate);
    }
}
