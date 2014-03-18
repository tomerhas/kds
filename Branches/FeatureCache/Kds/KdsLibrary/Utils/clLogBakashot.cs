using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DalOraInfra.DAL;
using KDSCommon.DataModels.Exceptions;
using KDSCommon.DataModels.Logs;

namespace KdsLibrary
{
    public class clLogBakashot
    {
        //This function will only save information from te first exception caught. 
        //The exception itself will be placed in the inner exception for later logging
        public static BakashotException SetError(long lRequestId,  string iSugHodaa, int? iKodTahalich, int iKodYeshut, int? iMisparIshi, DateTime? dTaarich, int? iMisparSidur, DateTime? dShatHatchalaSidur, DateTime? dShatYetzia, int? iMisparKnisa, string sTeurHodaa, int? iKodHodaa, Exception ex)
        {
            BakashaLog log;
            BakashotException excep = new BakashotException(ex);
            if (ex is BakashotException)
            {
                log = ((BakashotException)ex).Log;
            }
            else
            {
                log = new BakashaLog();
                
            }
            log.RequestId = lRequestId;
            log.SugHodaa = iSugHodaa;

            log.KodYeshut = iKodYeshut;
            log.KodTahalich = InsertIfNotNull(log.KodTahalich, iKodTahalich);
            log.MisparIshi = InsertIfNotNull(log.MisparIshi, iMisparIshi);
            log.Taarich = InsertIfNotNull(log.Taarich, dTaarich);
            log.MisparSidur = InsertIfNotNull(log.MisparSidur, iMisparSidur);
            log.ShatHatchalaSidur = InsertIfNotNull(log.ShatHatchalaSidur, dShatHatchalaSidur);

            log.ShatYetzia = InsertIfNotNull(log.ShatYetzia, dShatYetzia);
            log.MisparKnisa = InsertIfNotNull(log.MisparKnisa, iMisparKnisa);
            log.TeurHodaa = InsertIfNotNull(log.TeurHodaa, sTeurHodaa);
            log.KodHodaa = InsertIfNotNull(log.KodHodaa, iKodHodaa);
            excep.Log = log;

            return excep;
            
        }

        private static Nullable<int> InsertIfNotNull(Nullable<int> orig, Nullable<int> newVal)
        {
            return newVal.HasValue ? newVal : orig;
        }

        private static Nullable<DateTime> InsertIfNotNull(Nullable<DateTime> orig, Nullable<DateTime> newVal)
        {
            return newVal.HasValue ? newVal : orig;
        }

        private static string InsertIfNotNull(string orig, string newVal)
        {
            return string.IsNullOrWhiteSpace(newVal) ? newVal : orig;
        }

        public static BakashotException SetError(long lRequestId, int? iMisparIshi, string iSugHodaa, int iKodYeshut, DateTime? dTaarich, string sTeurHodaa, Exception ex)
        {
            return  SetError(lRequestId,iSugHodaa,null, iKodYeshut,  iMisparIshi, dTaarich,null,null,null,null,sTeurHodaa,null, ex);
        }

        public static BakashotException SetError(long lRequestId, string iSugHodaa, int iKodYeshut, string sTeurHodaa,  Exception ex)
        {
          return  SetError(lRequestId,iSugHodaa,null, iKodYeshut,  null, null,null,null,null,null,sTeurHodaa,null, ex);
        }
      
    }
}
