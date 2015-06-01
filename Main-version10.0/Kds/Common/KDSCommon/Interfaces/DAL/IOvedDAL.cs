using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Specialized;
using KDSCommon.Enums;

namespace KDSCommon.Interfaces.DAL
{
    public interface IOvedDAL
    {
        DataTable GetOvedDetails(int iMisparIshi, DateTime dCardDate);
        DataTable GetOvedYomAvodaDetails(int iMisparIshi, DateTime dCardDate);
        DataTable GetMeafyeneyBitzuaLeOved(int iMisparIshi, DateTime dTaarich);
        int GetZmanNesia(int iMerkazErua, int iMikumYaad, DateTime dDate);
        DataTable GetOvedMatzav(int iMisparIshi, DateTime dDate);
        void UpdateCardStatus(int iMisparIshi, DateTime dCardDate, CardStatus oCardStatus, int iUserId);

    }
}
