using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Interfaces.Errors;
using KDSCommon.DataModels.Errors;
using System.Data;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.Interfaces;
using KDSCommon.Enums;
using KdsLibrary;

namespace KdsBatch.Errors.BasicErrorsLib
{
    public abstract class CardErrorBase : ICardError
    {
        private DataTable _dtErrorsNotActive;
        private DataTable _dtLookUp;
        public CardErrorBase()
        {
            _dtErrorsNotActive =  ServiceLocator.Current.GetInstance<IKDSCacheManager>().GetCacheItem<DataTable>(CachedItems.ErrorTable);
            _dtLookUp = ServiceLocator.Current.GetInstance<IKDSCacheManager>().GetCacheItem<DataTable>(CachedItems.LookUpTables);
        }

        public bool IsCorrect(ErrorInputData input)
        {
            if (IsActive())
            {
                try
                {
                    return InternalIsCorrect(input);
                }
                catch (Exception ex)
                {
                    AddLogErrorToDb(input, ex);
                    input.IsSuccsess = false;
                    return false;
                }
            }
            //If the error is not active - no need to commit validation and therefore return true
            return true;
        }

        public abstract bool InternalIsCorrect(ErrorInputData input);
        public abstract ErrorTypes CardErrorType { get; }

        protected bool IsActive()
        {
            DataRow[] drShgiaNotActive;
            drShgiaNotActive = _dtErrorsNotActive.Select("kod_shgia=" + (int)CardErrorType);
            if (drShgiaNotActive!=null && drShgiaNotActive.Length > 0)
                return false;
            return true;
        }

        protected bool IsOvedInMatzav(string sMatzavim, DataTable dtMatzavOved)
        {
            bool result = false;
            try
            {
                //return result;
                result = sMatzavim.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                               .Any(matzav => IsOvedMatzavExists(matzav, dtMatzavOved));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        protected bool IsOvedMatzavExists(string sKodMatzav, DataTable dtMatzavOved)
        {
            DataRow[] dr;
            bool bOvedMatzavExists;
            
            try
            {
                sKodMatzav = Append0ToNumber(sKodMatzav);

                dr = dtMatzavOved.Select(string.Concat("kod_matzav='",sKodMatzav+"'"));
                bOvedMatzavExists = dr.Length > 0;
                return bOvedMatzavExists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void AddNewError(ErrorInputData input)
        {
            var drNew = input.dtErrors.NewRow();
            drNew["mispar_ishi"] = input.iMisparIshi;
            drNew["check_num"] = ErrorTypes.errHrStatusNotValid.GetHashCode();
            drNew["taarich"] = input.CardDate.ToShortDateString();

            input.dtErrors.Rows.Add(drNew);
        }

        protected void AddLogErrorToDb(ErrorInputData input, Exception ex)
        {
            clLogBakashot.InsertErrorToLog(input.BtchRequestId, input.iMisparIshi, "E"
                , (int)CardErrorType, input.CardDate, CardErrorType.ToString()+ ": " + ex.Message);
        }

        protected string GetLookUpKods(string sTableName)
        {
            //The function get lookup table name and return all kods in string, separate by comma
            string sLookUp = "";
            DataRow[] drLookUpAll;
            try
            {
                drLookUpAll = _dtLookUp.Select(string.Concat("table_name='", sTableName,"'"));
                foreach (DataRow drLookUp in drLookUpAll)
                {
                    sLookUp = string.Concat(sLookUp, drLookUp["Kod"].ToString(), ",");
                }
                sLookUp = sLookUp.Substring(0, sLookUp.Length - 1);

                return sLookUp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string Append0ToNumber(string val)
        {
            if(IsNumber(val))
                string.Format("{0:00}", val);
            return val;
        }

        private bool IsNumber(string val)
        {
            int temp = 0;
            return int.TryParse(val, out temp);
        }
    }
}
