using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace KDSCommon.Interfaces.DAL
{
    public interface ISidurDAL
    {
        bool IsSidurChofef(int iMisparIshi, DateTime dCardDate, int iMisparSidur, DateTime dShatHatchala, DateTime dShatGmar, int iParamChafifa, DataTable dt);
        float GetMeshechSidur(int iMisparIshi, int iMisparSidur, DateTime taarich_me, DateTime taarich_ad);
        DataTable GetMeafyeneySidurById(DateTime dCardDate, int iSidurNumber);
        bool CheckHaveSidurGrira(int iMisparIshi, DateTime dDateToCheck, ref DataTable dt);
        DataTable GetTmpSidurimMeyuchadim(DateTime dTarMe, DateTime dTarAd);
       
       // DataSet GetSidurAndPeiluyotFromTnua(int iMisparSidur, DateTime dDate, int? iKnisaVisut, out int iResult);
    }
}
