using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KDSCommon.UDT;
using DalOraInfra.DAL;

namespace KdsLibrary.BL
{
    public class clReport
    {
        private static clReport _Instance;

        public static clReport GetInstance()
        {
            if (_Instance == null)
                _Instance = new clReport();
            return _Instance;
        }

        public DataTable GetListReports(string profil)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {
                if (profil != "")
                    oDal.AddParameter("P_PROFIL", ParameterType.ntOracleVarchar, profil, ParameterDir.pdInput);
                else
                    oDal.AddParameter("P_PROFIL", ParameterType.ntOracleVarchar, null, ParameterDir.pdInput);

                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                // oDal.ExecuteSP(clGeneral.cProGetRequests, ref dt);
                oDal.ExecuteSP(clGeneral.cProGetListReports, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable GetProfilToDisplay(string Prefix)
        {
            DataTable dt = new DataTable();
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_ProfilFilter", ParameterType.ntOracleVarchar, Prefix, ParameterDir.pdInput);
                dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProGetProfilToDisplay, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

            
            public DataTable GetLicenceNumber(string Prefix)
        {
            DataTable dt = new DataTable();
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_Prefix", ParameterType.ntOracleVarchar, Prefix, ParameterDir.pdInput);
                dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProGetLicenceNumber, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetMakatOfActivity(DateTime FromDate, DateTime ToDate, string Prefix)
        {
            DataTable dt = new DataTable();
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_FromDate", ParameterType.ntOracleDate, FromDate, ParameterDir.pdInput);
                dal.AddParameter("p_ToDate", ParameterType.ntOracleDate, ToDate, ParameterDir.pdInput);
                dal.AddParameter("p_Prefix", ParameterType.ntOracleVarchar, Prefix, ParameterDir.pdInput);
                dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProGetMakatOfActivity, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetSidurimOvdim(DateTime FromDate, DateTime ToDate, string Prefix)
        {
            DataTable dt = new DataTable();
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_FromDate", ParameterType.ntOracleDate, FromDate, ParameterDir.pdInput);
                dal.AddParameter("p_ToDate", ParameterType.ntOracleDate, ToDate, ParameterDir.pdInput);
                dal.AddParameter("p_Prefix", ParameterType.ntOracleVarchar, Prefix, ParameterDir.pdInput);
                dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProGetSidurimOvdim, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetWorkStation(string Prefix)
        {
            DataTable dt = new DataTable();
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_Prefix", ParameterType.ntOracleVarchar, Prefix, ParameterDir.pdInput);
                dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProGetWorkStation, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }                


        public DataTable GetIdOfYameyAvoda(DateTime FromDate, DateTime ToDate, string Prefix)
        {
            return GetIdOfYameyAvoda(FromDate, ToDate, "", Prefix);
        }

        public DataTable GetIdOfYameyAvoda(DateTime FromDate, DateTime ToDate, string CompanyId, string Prefix)
        {
            DataTable dt = new DataTable();
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_FromDate", ParameterType.ntOracleDate, FromDate, ParameterDir.pdInput);
                dal.AddParameter("p_ToDate", ParameterType.ntOracleDate, ToDate, ParameterDir.pdInput);
                if (CompanyId != string.Empty)
                    dal.AddParameter("p_CompanyId", ParameterType.ntOracleVarchar, CompanyId, ParameterDir.pdInput);
                dal.AddParameter("p_Prefix", ParameterType.ntOracleVarchar, Prefix, ParameterDir.pdInput);
                dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProGetIdOfYameyAvoda, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataTable GetRizotChishuvLechodeshSucceeded(string p_chodesh)
        {
            DataTable dt = new DataTable();
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("P_CHODESH_HARAZA", ParameterType.ntOracleVarchar, p_chodesh, ParameterDir.pdInput);
                dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProGetRizotChishuvLechodeshSucceeded, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetRizotChishuvSucceeded(string p_chodesh)
        {
            DataTable dt = new DataTable();
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("P_CHODESH_HARAZA", ParameterType.ntOracleVarchar, p_chodesh, ParameterDir.pdInput);
                dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProGetRizotChishuvSucceeded, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetOvdim(DateTime FromDate, DateTime ToDate)
        {
            DataTable dt = new DataTable();
            try
            {
               return GetOvdim(FromDate, ToDate, "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
        public DataTable GetOvdim(DateTime FromDate, DateTime ToDate,string Prefix)
        {
            DataTable dt = new DataTable();
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_FromDate", ParameterType.ntOracleDate, FromDate, ParameterDir.pdInput);
                dal.AddParameter("p_ToDate", ParameterType.ntOracleDate, ToDate, ParameterDir.pdInput);
                if (Prefix !="")
                    dal.AddParameter("p_Prefix", ParameterType.ntOracleVarchar, Prefix, ParameterDir.pdInput);
                else
                    dal.AddParameter("p_Prefix", ParameterType.ntOracleVarchar, null, ParameterDir.pdInput);
                dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProGetOvdim, ref dt);
                
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }
            
        public DataTable getDefinitionReports(long BakashaId)
        {
            DataTable dt = new DataTable();
            try
            {
                clDal dal = new clDal();

                dal.AddParameter("p_BakashatId", ParameterType.ntOracleInt64, BakashaId, ParameterDir.pdInput);
                dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProGetDefinitionReports, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getDetailsReport(int KodReport)
        {
            DataTable dt = new DataTable();
            try
            {
                clDal dal = new clDal();

                dal.AddParameter("p_kodReport", ParameterType.ntOracleInt64, KodReport, ParameterDir.pdInput);
                dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProGetDetailsReport, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getDetailsReports(long BakashaId)
        {
            DataTable dt = new DataTable();
            try
            {
                clDal dal = new clDal();

                dal.AddParameter("p_BakashatId", ParameterType.ntOracleInt64, BakashaId, ParameterDir.pdInput);
                dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProGetDetailsReports, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getDestinationsReports(long BakashaId)
        {
            DataTable dt = new DataTable();
            try
            {
                clDal dal = new clDal();

                dal.AddParameter("p_BakashatId", ParameterType.ntOracleInt64, BakashaId, ParameterDir.pdInput);
                dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProGetDestinationsReports, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetPrepareReports(int p_Mispar_Ishi, string p_status_List)
        {
            DataTable dt = new DataTable();
            try
            {
                clDal dal = new clDal();

                dal.AddParameter("P_MISPAR_ISHI", ParameterType.ntOracleInt64, p_Mispar_Ishi, ParameterDir.pdInput);
                dal.AddParameter("P_STATUS_LIST", ParameterType.ntOracleVarchar, p_status_List, ParameterDir.pdInput);
                dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProGetPrepareReports, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void updatePrepareReports(int p_Mispar_Ishi)
        {
            DataTable dt = new DataTable();
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("P_MISPAR_ISHI", ParameterType.ntOracleInt64, p_Mispar_Ishi, ParameterDir.pdInput);
                dal.ExecuteSP(clGeneral.cProUpdatePrepareReports);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable GetHeavyReportsToDelete()
        {
            DataTable dt = new DataTable();
            try
            {
                clDal dal = new clDal();

                dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProGetHeavyReportsToDelete, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable GetSnifimByEzor(int p_ezor)
        {
            DataTable dt = new DataTable();
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_ezor", ParameterType.ntOracleInteger, p_ezor, ParameterDir.pdInput);
                dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProGetSnifeyTnuaByEzor, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }




        public bool GetProPrepareOvdimRikuzim(long BakashaId ,long RequestIdForRikuzim, int NumOfProcesses)
        {
            DataTable dt = new DataTable();
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, BakashaId, ParameterDir.pdInput);
                dal.AddParameter("p_RequestId", ParameterType.ntOracleInt64, RequestIdForRikuzim, ParameterDir.pdInput);
                dal.AddParameter("p_num_processes", ParameterType.ntOracleInt64, NumOfProcesses, ParameterDir.pdInput);
                dal.ExecuteSP(clGeneral.cProPrepareOvdimRikuzim, ref dt);
                return true; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            }

        public DataTable getDetailsOvdimLeRikuzim(long BakashaId, int numPack)
        {
            DataTable dt = new DataTable();
            try
            {
                clDal dal = new clDal();

                dal.AddParameter("p_bakasha_Id", ParameterType.ntOracleInt64, BakashaId, ParameterDir.pdInput);
                dal.AddParameter("p_Num_Pack", ParameterType.ntOracleInt64, numPack, ParameterDir.pdInput);
                
                dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProGetDetailsOvdimLeRikuzim, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable getEmailOvdimLeRikuzim(long BakashaId)
        {
            DataTable dt = new DataTable();
            try
            {
                clDal dal = new clDal();

                dal.AddParameter("p_BakashatId", ParameterType.ntOracleInt64, BakashaId, ParameterDir.pdInput);
                dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProGetEmailOvdimLeRikuzim, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveRikuzmPdf(COLL_RIKUZ_PDF oCollRikuzPdf, long BakashaId,int numPack)
        {
            try
            {
                clDal dal = new clDal();

                dal.AddParameter("p_BakashatId", ParameterType.ntOracleInt64, BakashaId, ParameterDir.pdInput);
                dal.AddParameter("p_coll_rikuz_pdf", ParameterType.ntOracleArray, oCollRikuzPdf, ParameterDir.pdInput, "COLL_RIKUZ_PDF");
                dal.AddParameter("p_Num_Pack", ParameterType.ntOracleInt64, numPack, ParameterDir.pdInput);
                dal.ExecuteSP(clGeneral.cProSaveRikuzPdf);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] getRikuzPdf(int mispar_ishi, DateTime taarich, long bakasha_id)
        {
            byte[] bytes;
            try
            {
                clDal dal = new clDal();

                dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInt64, mispar_ishi, ParameterDir.pdInput);
                dal.AddParameter("p_taarich", ParameterType.ntOracleDate, taarich, ParameterDir.pdInput);
                dal.AddParameter("p_BakashatId", ParameterType.ntOracleInt64, bakasha_id, ParameterDir.pdInput);
                dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
              
                Oracle.DataAccess.Client.OracleDataReader dataReader = dal.GetDataReader(clGeneral.cProGetRikuzPdf, CommandType.StoredProcedure);

                if (dataReader.Read())
                   bytes = (Byte[])dataReader["rikuz_pdf"];
                else bytes = null;
                
                dataReader.Close();
              
                return bytes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void getRikuzPdfTest(int mispar_ishi, DateTime taarich, int sug)
        {
          //  byte[] bytes;
            try
            {
                clDal dal = new clDal();

                dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInt64, mispar_ishi, ParameterDir.pdInput);
                dal.AddParameter("p_chodesh", ParameterType.ntOracleDate, taarich, ParameterDir.pdInput);
                dal.AddParameter("p_rikuz_type", ParameterType.ntOracleInt64, sug, ParameterDir.pdInput);
                dal.AddParameter("p_rc", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
                dal.AddParameter("p_rikuz_pdf", ParameterType.ntOracleBlob, null, ParameterDir.pdOutput);

                dal.ExecuteSP("PRO_GET_RIKUZ_PDF");
                //Oracle.DataAccess.Client.OracleDataReader dataReader = dal.GetDataReader(clGeneral.cProGetRikuzPdf, CommandType.StoredProcedure);

                //if (dataReader.Read())
                //    bytes = (Byte[])dataReader["rikuz_pdf"];
                //else bytes = null;

                //dataReader.Close();

                //return bytes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
