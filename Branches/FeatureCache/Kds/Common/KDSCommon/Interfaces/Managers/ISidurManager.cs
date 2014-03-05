using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KDSCommon.DataModels;
using System.Collections.Specialized;

namespace KDSCommon.Interfaces.Managers
{
    public interface ISidurManager
    {
        double CalculatePremya(OrderedDictionary htPeilut, out double dElementsHamtanaReshut);
        SidurDM CreateClsSidurFromEmployeeDetails(DataRow dr);
        SidurDM CreateClsSidurFromSidurayGrira(DataRow dr);
        SidurDM CreateClsSidurFromSidurMeyuchad(SidurDM oSidurKodem, DateTime dTaarich, int iMisparSidurNew, DataRow dr);
        bool IsSidurChofef(int iMisparIshi, DateTime dCardDate, int iMisparSidur, DateTime dShatHatchala, DateTime dShatGmar, int iParamChafifa, DataTable dt);
        float GetMeshechSidur(int iMisparIshi, int iMisparSidur, DateTime taarich_me, DateTime taarich_ad);

        bool IsSidurNahagut(DataRow[] drSugSidur, SidurDM curSidur);
        DataRow[] GetOneSugSidurMeafyen(int iSugSidurRagil, DateTime CardDate);
        DataTable GetMeafyeneySidurById(DateTime dCardDate, int iSidurNumber);
        bool IsSidurShonim(DataRow[] drSugSidur, SidurDM oSidur);
    }
}
