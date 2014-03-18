using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DalOraInfra.DAL;
using KDSCommon.DataModels.Logs;
using KDSCommon.Interfaces.DAL;
using KDSCommon.Interfaces.Logs;
using Microsoft.Practices.Unity;

namespace KDSBLLogic.DAL
{
    public class LogDAL : ILogDAL
    {
        public const string cProInsLogBakasha = "pkg_batch.pro_ins_log_bakasha";
        private IUnityContainer _container;

        public LogDAL(IUnityContainer container)
        {
            _container = container;
        }

    
        public void InsertLog(BakashaLog Log)
        {
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, Log.RequestId, ParameterDir.pdInput);
                dal.AddParameter("p_taarich_idkun", ParameterType.ntOracleDate, DateTime.Now, ParameterDir.pdInput);
                dal.AddParameter("p_sug_hodaa", ParameterType.ntOracleChar, Log.SugHodaa, ParameterDir.pdInput, 1);
                if (Log.KodTahalich.HasValue)
                {
                    dal.AddParameter("p_kod_tahalich", ParameterType.ntOracleInteger, Log.KodTahalich.Value, ParameterDir.pdInput);
                }
                else
                {
                    dal.AddParameter("p_kod_tahalich", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                dal.AddParameter("p_kod_yeshut", ParameterType.ntOracleInteger, Log.KodYeshut, ParameterDir.pdInput);
                if (Log.MisparIshi.HasValue)
                {
                    dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, Log.MisparIshi.Value, ParameterDir.pdInput);
                }
                else
                {
                    dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                if (Log.Taarich.HasValue)
                {
                    dal.AddParameter("p_taarich", ParameterType.ntOracleDate, Log.Taarich.Value, ParameterDir.pdInput);
                }
                else
                {
                    dal.AddParameter("p_taarich", ParameterType.ntOracleDate, null, ParameterDir.pdInput);
                }
                if (Log.MisparSidur.HasValue)
                {
                    dal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, Log.MisparSidur.Value, ParameterDir.pdInput);
                }
                else
                {
                    dal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                if (Log.ShatHatchalaSidur.HasValue)
                {
                    dal.AddParameter("p_shat_hatchala_sidur", ParameterType.ntOracleDate, Log.ShatHatchalaSidur.Value, ParameterDir.pdInput);
                }
                else
                {
                    dal.AddParameter("p_shat_hatchala_sidur", ParameterType.ntOracleDate, null, ParameterDir.pdInput);
                }
                if (Log.ShatYetzia.HasValue)
                {
                    dal.AddParameter("p_shat_yetzia", ParameterType.ntOracleDate, Log.ShatYetzia.Value, ParameterDir.pdInput);
                }
                else
                {
                    dal.AddParameter("p_shat_yetzia", ParameterType.ntOracleDate, null, ParameterDir.pdInput);
                }
                if (Log.MisparKnisa.HasValue)
                {
                    dal.AddParameter("p_mispar_knisa", ParameterType.ntOracleInteger, Log.MisparKnisa.Value, ParameterDir.pdInput);
                }
                else
                {
                    dal.AddParameter("p_mispar_knisa", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }

                if (Log.TeurHodaa.Length > 4000)
                { Log.TeurHodaa = Log.TeurHodaa.Substring(0, 4000); }

                dal.AddParameter("p_teur_hodaa", ParameterType.ntOracleVarchar, Log.TeurHodaa, ParameterDir.pdInput, 4000);
                if (Log.KodHodaa.HasValue)
                {
                    dal.AddParameter("p_kod_hodaa", ParameterType.ntOracleInteger, Log.KodHodaa.Value, ParameterDir.pdInput);
                }
                else
                {
                    dal.AddParameter("p_kod_hodaa", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                dal.AddParameter("p_mispar_siduri", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
                dal.ExecuteSP(cProInsLogBakasha);

            }
             
            catch (Exception ex)
            {
                _container.Resolve<ILogger>().LogError(ex.Message);
            }
         
        }
    }
}
