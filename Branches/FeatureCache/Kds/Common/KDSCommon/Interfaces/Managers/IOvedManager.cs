using System;
using KDSCommon.DataModels;
using System.Data;
using System.Collections.Specialized;
using KDSCommon.Enums;
namespace KDSCommon.Interfaces.Managers
{
    public interface IOvedManager
    {
        OvedYomAvodaDetailsDM CreateOvedDetails(int iMisparIshi, DateTime dDate);
        DataTable GetOvedDetails(int iMisparIshi, DateTime dCardDate);
        MeafyenimDM CreateMeafyenyOved(int iMisparIshi, DateTime dDate);
        MeafyenimDM CreateMeafyenyOved(int iMisparIshi, DateTime dDate, DataTable meafyenim);
        OrderedDictionary GetEmployeeDetails(DataTable dtDetails, DateTime dCardDate, int misparIshi, out int iLastMisaprSidur);

        void UpdateCardStatus(int iMisparIshi, DateTime dCardDate, CardStatus oCardStatus, int iUserId);
       
    }
}
