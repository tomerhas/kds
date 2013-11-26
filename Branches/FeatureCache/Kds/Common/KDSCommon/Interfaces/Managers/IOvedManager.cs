using System;
using KDSCommon.DataModels;
using System.Data;
namespace KDSCommon.Interfaces.Managers
{
    public interface IOvedManager
    {
        OvedYomAvodaDetailsDM CreateOvedDetails(int iMisparIshi, DateTime dDate);
        DataTable GetOvedDetails(int iMisparIshi, DateTime dCardDate);
    }
}
