using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary.DAL;

namespace KdsLibrary
{
    public static class clLogBakashot
    {
        private static long _RequestId=-1;
        private static string _SugHodaa;
        private static int? _KodTahalich;
        private static int _KodYeshut;
        private static int? _MisparIshi;
        private static DateTime? _Taarich;
        private static int? _MisparSidur;
        private static DateTime? _ShatHatchalaSidur;
        private static DateTime? _ShatYetzia;
        private static int? _MisparKnisa;
        private static string _TeurHodaa;
        private static int? _KodHodaa;

        public static void SetError(long lRequestId,  string iSugHodaa, int? iKodTahalich, int iKodYeshut, int? iMisparIshi, DateTime? dTaarich, int? iMisparSidur, DateTime? dShatHatchalaSidur, DateTime? dShatYetzia, int? iMisparKnisa, string sTeurHodaa, int? iKodHodaa)
        {
            if (_RequestId == -1)
            {
                _RequestId = lRequestId;
                _SugHodaa = iSugHodaa;
                _KodTahalich = iKodTahalich;
                _KodYeshut = iKodYeshut;
                _MisparIshi = iMisparIshi;
                _Taarich = dTaarich;
                _MisparSidur = iMisparSidur;
                _ShatHatchalaSidur = dShatHatchalaSidur;
                _Taarich = dTaarich;
                _MisparSidur = iMisparSidur;
                _ShatHatchalaSidur = dShatHatchalaSidur;
                _ShatYetzia = dShatYetzia;
                _MisparKnisa = iMisparKnisa;
                _TeurHodaa = sTeurHodaa;
                _KodHodaa = iKodHodaa;
            }
        }

        public static void SetError(long lRequestId, int? iMisparIshi, string iSugHodaa, int iKodYeshut, DateTime? dTaarich, string sTeurHodaa)
        {
            if (_RequestId == -1)
            {
                _RequestId = lRequestId;
                _SugHodaa = iSugHodaa;
                _KodTahalich = null;
                _KodYeshut = iKodYeshut;
                _MisparIshi = iMisparIshi;
                _Taarich = dTaarich;
                _MisparSidur = null;
                _ShatHatchalaSidur = null;
                _Taarich = dTaarich;
                _MisparSidur = null;
                _ShatHatchalaSidur = null;
                _ShatYetzia = null;
                _MisparKnisa = null;
                _TeurHodaa = sTeurHodaa;
                _KodHodaa = null;
            }
        }

        public static void SetError(long lRequestId, string iSugHodaa, int iKodYeshut,  string sTeurHodaa)
        {
            if (_RequestId == -1)
            {
                _RequestId = lRequestId;
                _SugHodaa = iSugHodaa;
                _KodTahalich = null;
                _KodYeshut = iKodYeshut;
                _MisparIshi = null;
                _Taarich = null;
                _MisparSidur = null;
                _ShatHatchalaSidur = null;
                _Taarich = null;
                _MisparSidur = null;
                _ShatHatchalaSidur = null;
                _ShatYetzia = null;
                _MisparKnisa = null;
                _TeurHodaa = sTeurHodaa;
                _KodHodaa = null;
            }
        }

        private static void ResetError()
        {
            _RequestId = -1;
            _SugHodaa = null;
            _KodTahalich = null;
            _KodYeshut = 0;
            _MisparIshi = null;
            _Taarich = null;
            _MisparSidur = null;
            _ShatHatchalaSidur = null;
            _Taarich = null;
            _MisparSidur = null;
            _ShatHatchalaSidur = null;
            _ShatYetzia = null;
            _MisparKnisa = null;
            _TeurHodaa = null;
            _KodHodaa = null;
        }

        private static void InsertLogBakasha()
        {
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, _RequestId, ParameterDir.pdInput);
                dal.AddParameter("p_taarich_idkun", ParameterType.ntOracleDate, DateTime.Now, ParameterDir.pdInput);
                dal.AddParameter("p_sug_hodaa", ParameterType.ntOracleChar, _SugHodaa, ParameterDir.pdInput, 1);
                if (_KodTahalich.HasValue)
                {
                    dal.AddParameter("p_kod_tahalich", ParameterType.ntOracleInteger, _KodTahalich.Value, ParameterDir.pdInput);
                }
                else
                {
                    dal.AddParameter("p_kod_tahalich", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                dal.AddParameter("p_kod_yeshut", ParameterType.ntOracleInteger, _KodYeshut, ParameterDir.pdInput);
                if (_MisparIshi.HasValue)
                {
                    dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, _MisparIshi.Value, ParameterDir.pdInput);
                }
                else
                {
                    dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                if (_Taarich.HasValue)
                {
                    dal.AddParameter("p_taarich", ParameterType.ntOracleDate, _Taarich.Value, ParameterDir.pdInput);
                }
                else
                {
                    dal.AddParameter("p_taarich", ParameterType.ntOracleDate, null, ParameterDir.pdInput);
                }       
                if (_MisparSidur.HasValue)
                {
                    dal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, _MisparSidur.Value, ParameterDir.pdInput);
                }
                else
                {
                    dal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                if (_ShatHatchalaSidur.HasValue)
                {
                dal.AddParameter("p_shat_hatchala_sidur", ParameterType.ntOracleDate,  _ShatHatchalaSidur.Value , ParameterDir.pdInput);
                }
                else{dal.AddParameter("p_shat_hatchala_sidur", ParameterType.ntOracleDate, null, ParameterDir.pdInput);
                }
                if (_ShatYetzia.HasValue)
               {
                dal.AddParameter("p_shat_yetzia", ParameterType.ntOracleDate,  _ShatYetzia.Value, ParameterDir.pdInput);
               }
               else
               { dal.AddParameter("p_shat_yetzia", ParameterType.ntOracleDate, null, ParameterDir.pdInput);
               }
                if (_MisparKnisa.HasValue)
                {
                dal.AddParameter("p_mispar_knisa", ParameterType.ntOracleInteger,  _MisparKnisa.Value, ParameterDir.pdInput);
                }
                else
                {
                    dal.AddParameter("p_mispar_knisa", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }

                if (_TeurHodaa.Length > 4000)
                { _TeurHodaa = _TeurHodaa.Substring(0, 4000); }

                dal.AddParameter("p_teur_hodaa", ParameterType.ntOracleVarchar, _TeurHodaa, ParameterDir.pdInput,4000);
                if (_KodHodaa.HasValue)
                {
                    dal.AddParameter("p_kod_hodaa", ParameterType.ntOracleInteger, _KodHodaa.Value, ParameterDir.pdInput);
                }
                else
                {
                    dal.AddParameter("p_kod_hodaa", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                dal.AddParameter("p_mispar_siduri", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
                dal.ExecuteSP(KdsLibrary.clGeneral.cProInsLogBakasha);

            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
            }
        }

        public static void InsertErrorToLog()
        {
            InsertLogBakasha();
            ResetError();
        }

        public static void InsertErrorToLog(long lRequestId, string iSugHodaa, int iKodYeshut, string sTeurHodaa)
        {
            SetError(lRequestId, iSugHodaa, iKodYeshut, sTeurHodaa);
            InsertErrorToLog();
        }

        public static void InsertErrorToLog(long lRequestId, int? iMisparIshi, string iSugHodaa, int iKodYeshut, DateTime? dTaarich, string sTeurHodaa)
        {
            SetError(lRequestId, iMisparIshi, iSugHodaa, iKodYeshut, dTaarich, sTeurHodaa);
            InsertErrorToLog();
        }

        public static void InsertErrorToLog(long lRequestId, string iSugHodaa, int? iKodTahalich, int iKodYeshut, int? iMisparIshi, DateTime? dTaarich, int? iMisparSidur, DateTime? dShatHatchalaSidur, DateTime? dShatYetzia, int? iMisparKnisa, string sTeurHodaa, int? iKodHodaa)
        {
            SetError(lRequestId, iSugHodaa, iKodTahalich, iKodYeshut, iMisparIshi, dTaarich, iMisparSidur, dShatHatchalaSidur, dShatYetzia, iMisparKnisa, sTeurHodaa, iKodHodaa);
            InsertErrorToLog();
        }
    }
}
