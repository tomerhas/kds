using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace KDSCommon.Interfaces.DAL
{
    public interface IKavimDAL
    {
        int CheckHityazvutNehag(int? iNekudatTziyunTnua, int iRadyus, int iSnif, int? iMikumShaon, int? iMisparSiduriShaon);
        DataTable GetBusesDetailsLeOvedForMonth(DateTime dTarMe, DateTime dTarAd, int iMispar_ishi);
        void GetBusLicenseNumber(long lOtoNo, ref long lLicenseNumber);
        DataTable GetElementDetails(long lNewMakat);
        DataTable GetKatalogKavim(int iMisparIshi, DateTime dFromDate, DateTime dToDate);
        DataSet GetKavimDetailsFromTnuaDS(long lMakatNesia, DateTime dDate, out int iResult, int? reciveVisut);
        DataTable GetKavimDetailsFromTnuaDT(long lMakatNesia, DateTime dDate, out int iResult);
        DataTable GetMakatimLeTkinut(DateTime dTaarich);
        DataTable GetMakatKavNamak(long lMakatNesia, DateTime dDate);
        DataTable GetMakatKavReka(long lMakatNesia, DateTime dDate);
        DataTable GetMakatKavShirut(long lMakatNesia, DateTime dDate);
        DataTable GetMakatNamakDetailsFromTnua(long lMakatNesia, DateTime dDate, out int iResult);
        DataTable GetMakatRekaDetailsFromTnua(long lMakatNesia, DateTime dDate, out int iResult);
        DataTable GetMasharData(string sCarsNumbers);
        DataTable GetMeafyeneyElementByKod(long lMakatNesia, DateTime dDate);
        DataTable GetRekaDetailsByXY(DateTime dDate, long lMokedStart, long lMokedEnd, out int iResult);
        DataSet GetSidurAndPeiluyotFromTnua(int iMisparSidur, DateTime dDate, int? iKnisaVisut, out int iResult);
        DataTable GetSidurDetailsFromTnua(int iMisparSidur, DateTime dDate, out int iResult);
        DataTable GetVisutDetails(long lNewMakat);
        int IsBusNumberValid(long lOtoNo, DateTime dCardDate);

        int IsRechevValid(long lLicenseNumber, DateTime dCardDate,int type);
    }
}
