using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Exceptions;
using KDSCommon.DataModels.Logs;

namespace KDSCommon.Interfaces.Logs
{
    public interface ILogBakashot
    {
        void InsertLog(BakashaLog Log, Exception ex = null);
        void InsertLog(long lRequestId, string iSugHodaa, int iKodYeshut, string sTeurHodaa, Exception ex = null);
        void InsertLog(long lRequestId, string iSugHodaa, int iKodYeshut, string sTeurHodaa, int? iMisparIshi, DateTime? dTaarich, Exception ex = null);
        void InsertLog(long lRequestId, string iSugHodaa, int iKodYeshut, string sTeurHodaa, int? iMisparIshi, DateTime? dTaarich, int? iKodTahalich, int? iMisparSidur, DateTime? dShatHatchalaSidur, DateTime? dShatYetzia, int? iMisparKnisa, int? iKodHodaa, Exception ex = null);   
        BakashotException SetError(long lRequestId, string iSugHodaa, int? iKodTahalich, int iKodYeshut, int? iMisparIshi, DateTime? dTaarich, int? iMisparSidur, DateTime? dShatHatchalaSidur, DateTime? dShatYetzia, int? iMisparKnisa, string sTeurHodaa, int? iKodHodaa, Exception ex);
        BakashotException SetError(long lRequestId, int? iMisparIshi, string iSugHodaa, int iKodYeshut, DateTime? dTaarich, string sTeurHodaa, Exception ex);
        BakashotException SetError(long lRequestId, string iSugHodaa, int iKodYeshut, string sTeurHodaa, Exception ex);

        void UpdateLogBakasha(long lRequestNum, DateTime dZmanSiyum, int iStatus);
    }
}
