using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace KDSCommon.Interfaces.DAL
{
    public interface IOvedDAL
    {
        DataTable GetOvedDetails(int iMisparIshi, DateTime dCardDate);
        DataTable GetOvedYomAvodaDetails(int iMisparIshi, DateTime dCardDate);
    }
}
