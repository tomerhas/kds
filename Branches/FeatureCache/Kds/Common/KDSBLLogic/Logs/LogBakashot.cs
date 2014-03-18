using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Exceptions;
using KDSCommon.DataModels.Logs;
using KDSCommon.Interfaces.DAL;
using KDSCommon.Interfaces.Logs;
using Microsoft.Practices.Unity;

namespace KDSBLLogic.Logs
{
    public class LogBakashot : ILogBakashot
    {

        private IUnityContainer _container;

        public LogBakashot(IUnityContainer container)
        {
            _container = container;
        }

        public void InsertLog(BakashaLog log, Exception ex=null)
        {
            log.TeurHodaa = GetExceptionLog(log,ex);
             _container.Resolve<ILogDAL>().InsertLog(log);
            // return log;
        }

        private string GetExceptionLog(BakashaLog log, Exception ex)
        {
            if (ex == null)
                return log.TeurHodaa;
            Exception inner = GetInnerException(ex);
            return string.Format("TeurHoddaa: {0}. Inner Exception message: {1}",log.TeurHodaa, inner.ToString());

        }

        private Exception GetInnerException(Exception ex)
        {
            if (ex.InnerException != null)
                return GetInnerException(ex.InnerException);
            return ex;

        }

        public void InsertLog(long lRequestId, string iSugHodaa, int iKodYeshut, string sTeurHodaa, Exception ex = null)
        {
             InsertLog(lRequestId, iSugHodaa, iKodYeshut, sTeurHodaa, null, null, null, null, null, null, null, null,ex); 
        }

        public void InsertLog(long lRequestId, string iSugHodaa, int iKodYeshut, string sTeurHodaa, int? iMisparIshi, DateTime? dTaarich, Exception ex = null)
        {
              InsertLog(lRequestId, iSugHodaa, iKodYeshut, sTeurHodaa, iMisparIshi, dTaarich,null,null,null,null,null,null,ex); 
        }

        public void InsertLog(long lRequestId, string iSugHodaa, int iKodYeshut, string sTeurHodaa, int? iMisparIshi, DateTime? dTaarich, int? iKodTahalich, int? iMisparSidur, DateTime? dShatHatchalaSidur, DateTime? dShatYetzia, int? iMisparKnisa, int? iKodHodaa, Exception ex=null)
        {
            var bakashotEx = SetError(lRequestId, iSugHodaa, iKodTahalich, iKodYeshut, iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, dShatYetzia, iMisparKnisa, sTeurHodaa, iKodHodaa, ex);
            InsertLog(bakashotEx.Log, ex);
        }

        private  Nullable<int> InsertIfNotNull(Nullable<int> orig, Nullable<int> newVal)
        {
            if (!orig.HasValue && newVal.HasValue)
                return newVal;
            else 
                return orig;
             // return newVal.HasValue ? newVal : orig;
        }

        private  Nullable<DateTime> InsertIfNotNull(Nullable<DateTime> orig, Nullable<DateTime> newVal)
        {
            if (!orig.HasValue && newVal.HasValue)
                return newVal;
            else
                return orig;
        }

        private  string InsertIfNotNull(string orig, string newVal)
        {
            if (string.IsNullOrWhiteSpace(orig) && !string.IsNullOrWhiteSpace(newVal))
                return newVal;
            else
                return orig;
        }

        //Set error will merge between new data and existing data.
        //The original state of log data will be placed inside the exception if passed to func
        public BakashotException SetError(long lRequestId, string iSugHodaa, int? iKodTahalich, int iKodYeshut, int? iMisparIshi, DateTime? dTaarich, int? iMisparSidur, DateTime? dShatHatchalaSidur, DateTime? dShatYetzia, int? iMisparKnisa, string sTeurHodaa, int? iKodHodaa, Exception ex)
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

        public BakashotException SetError(long lRequestId, int? iMisparIshi, string iSugHodaa, int iKodYeshut, DateTime? dTaarich, string sTeurHodaa, Exception ex)
        {
            return SetError(lRequestId, iSugHodaa, null, iKodYeshut, iMisparIshi, dTaarich, null, null, null, null, sTeurHodaa, null, ex);
        }

        public BakashotException SetError(long lRequestId, string iSugHodaa, int iKodYeshut, string sTeurHodaa, Exception ex)
        {
            return SetError(lRequestId, iSugHodaa, null, iKodYeshut, null, null, null, null, null, null, sTeurHodaa, null, ex);
        }
      
   
    }
}
