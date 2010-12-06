using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;
using KdsLibrary.UDT;

namespace KdsLibrary.BL
{
    public class clBatch
    {
        public long RunCalcBatch(clGeneral.enGeneralBatchType iTypeRequest, string sDescription, clGeneral.enStatusRequest iStatus, int iUserId, string sMaamad, string sMonth, int iRunAll, int iRunTest)
        {
            long iRequestId;

            clTxDal objDal = new clTxDal();
            try
            {
                objDal.TxBegin();
                iRequestId = InsertBakasha(ref objDal,iTypeRequest, sDescription, iStatus, iUserId);
                objDal.ClearCommand();
                InsertBakashaParam(ref objDal, iRequestId,1,sMaamad);
                objDal.ClearCommand();
                InsertBakashaParam(ref objDal, iRequestId,2,sMonth);
                objDal.ClearCommand();
                InsertBakashaParam(ref objDal, iRequestId,3,iRunAll.ToString());
                objDal.ClearCommand();
                InsertBakashaParam(ref objDal, iRequestId,5, iRunTest.ToString());
                objDal.TxCommit();
            }
            catch (Exception ex)
            {
                objDal.TxRollBack();
                throw ex;
            }

            return iRequestId;
        }

        public long InsertBakasha(ref clTxDal objTx, clGeneral.enGeneralBatchType iTypeRequest, string sDescription, clGeneral.enStatusRequest iStatus, int iUserId)
        {
            long iRequestId;

            try {
                objTx.AddParameter("p_sug_bakasha", ParameterType.ntOracleInteger, iTypeRequest.GetHashCode(), ParameterDir.pdInput);
                objTx.AddParameter("p_teur", ParameterType.ntOracleVarchar, sDescription, ParameterDir.pdInput, 100);
                objTx.AddParameter("p_status", ParameterType.ntOracleInteger, iStatus.GetHashCode(), ParameterDir.pdInput);
                objTx.AddParameter("p_user_id", ParameterType.ntOracleInteger, iUserId, ParameterDir.pdInput);
                objTx.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, "", ParameterDir.pdOutput);

                objTx.ExecuteSP(clGeneral.cProInsBakasha);

                iRequestId = long.Parse(objTx.GetValParam("p_bakasha_id"));

               
             } 

            catch (Exception ex)
            {
                throw ex;
            }

            return iRequestId;
        
    }


        private void InsertBakashaParam(ref clTxDal objTx, long iRequestId, int iParam, string sErech)
        {
           
            try
            {
                objTx.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, iRequestId, ParameterDir.pdInput);
                objTx.AddParameter("p_param_id", ParameterType.ntOracleInteger, iParam, ParameterDir.pdInput);
                objTx.AddParameter("p_erech", ParameterType.ntOracleVarchar, sErech, ParameterDir.pdInput, 50);

                objTx.ExecuteSP(clGeneral.cProInsBakashaParam);

            }

            catch (Exception ex)
            {
                throw ex;
            }

         
        }


        public long RunErrorBatch(clGeneral.enGeneralBatchType iTypeRequest, string sDescription, clGeneral.enStatusRequest iStatus, int iUserId)
        {
            long iRequestId;

            clTxDal objDal = new clTxDal();
            try
            {
                objDal.TxBegin();
                iRequestId = InsertBakasha(ref objDal, iTypeRequest, sDescription, iStatus, iUserId);
               
                objDal.TxCommit();
            }
            catch (Exception ex)
            {
                objDal.TxRollBack();
                throw ex;
            }

            return iRequestId;
        }

        public long RunTransferToSachar(clGeneral.enGeneralBatchType iTypeRequest, string sDescription, clGeneral.enStatusRequest iStatus, int iUserId, long iRequestToSachar)
        {
            long iRequestId;

            clTxDal objDal = new clTxDal();
            try
            {
                objDal.TxBegin();
                iRequestId = InsertBakasha(ref objDal, iTypeRequest, sDescription, iStatus, iUserId);
                
                objDal.ClearCommand();
                InsertBakashaParam(ref objDal, iRequestId, 4, iRequestToSachar.ToString());
                
                objDal.TxCommit();
            }
            catch (Exception ex)
            {
                objDal.TxRollBack();
                throw ex;
            }

            return iRequestId;
        }

        public DataTable GetPirteyRitzotChishuv(DateTime dTaarichMe, DateTime dTaarichAd,Boolean bGetAll )
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //פונקציה המחזירה ריצות חישוב
                oDal.AddParameter("p_taarich_me", ParameterType.ntOracleDate, dTaarichMe, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich_ad", ParameterType.ntOracleDate, dTaarichAd, ParameterDir.pdInput);
                oDal.AddParameter("p_get_all", ParameterType.ntOracleInteger, bGetAll.GetHashCode(), ParameterDir.pdInput);

                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetPirteyRitzotChishuv, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public long RunReportsBatch(clGeneral.enGeneralBatchType iTypeRequest, string sDescription, clGeneral.enStatusRequest iStatus, int iUserId, string sMonth)
        {
            long iRequestId;

            clTxDal objDal = new clTxDal();
            try
            {
                objDal.TxBegin();

                iRequestId = InsertBakasha(ref objDal, iTypeRequest, sDescription, iStatus, iUserId);
                objDal.ClearCommand();
                InsertBakashaParam(ref objDal, iRequestId, 1, sMonth);
                objDal.ClearCommand();

                objDal.TxCommit();
            }
            catch (Exception ex)
            {
                objDal.TxRollBack();
                throw ex;
            }

            return iRequestId;
        }
        public long RunReportsBatch(clGeneral.enGeneralBatchType iTypeRequest, string sDescription, clGeneral.enStatusRequest iStatus, int UserId, COLL_REPORT_PARAM ColUdt, string ReportName, int Extension, string DestinationFolder, bool SendToMail)
        {
            long iRequestId;

            clTxDal objDal = new clTxDal();
            try
            {
                objDal.TxBegin();
                iRequestId = InsertBakasha(ref objDal, iTypeRequest, sDescription, iStatus, UserId);
                objDal.ClearCommand();
                InsertHeavyReport(iRequestId, ReportName, ColUdt, UserId, Extension, DestinationFolder, SendToMail);
                objDal.ClearCommand();
                objDal.TxCommit();
            }
            catch (Exception ex)
            {
                objDal.TxRollBack();
                throw ex;
            }

            return iRequestId;
        }

        public void InsertHeavyReport(long BakashaId, string ReportName, COLL_REPORT_PARAM colUdt,
                            int MisparIshi, int Extension, string DestinationFolder, bool SendToMail)
        {
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("p_BakashaId", ParameterType.ntOracleInt64, BakashaId, ParameterDir.pdInput);
                oDal.AddParameter("p_ReportName", ParameterType.ntOracleVarchar, ReportName, ParameterDir.pdInput);
                oDal.AddParameter("p_ReportParams", ParameterType.ntOracleArray, colUdt, ParameterDir.pdInput, "COLL_REPORT_PARAM");
                oDal.AddParameter("p_MisparIshi", ParameterType.ntOracleInt64, MisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_Extension", ParameterType.ntOracleInteger, Extension, ParameterDir.pdInput);
                oDal.AddParameter("p_DestinationFolder", ParameterType.ntOracleVarchar, DestinationFolder, ParameterDir.pdInput);
                oDal.AddParameter("p_SendToMail", ParameterType.ntOracleInteger, (SendToMail == true) ? 1 : 0, ParameterDir.pdInput);
                oDal.ExecuteSP(clGeneral.cProInsertHeavyReports);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable GetShinuyMatsavOvdim()
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {
             //   oDal.AddParameter("p_direction", ParameterType.ntOracleInteger, Direction, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetShinuyMatsavOvdim, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetShinuyMeafyeneyBizua()
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {
              //  oDal.AddParameter("p_direction", ParameterType.ntOracleInteger, Direction, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetShinuyMeafyeneyBizua, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetShinuyPireyOved()
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {
                //oDal.AddParameter("p_direction", ParameterType.ntOracleInteger, Direction, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetShinuyPireyOved, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetShinuyBrerotMechdal()
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {
               // oDal.AddParameter("p_direction", ParameterType.ntOracleInteger, Direction, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetShinuyBrerotMechdal, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SaveShinuyimHR(List<COLL_OVDIM_IM_SHINUY_HR> ArrCollShinuyim, string sTable)
        {
            clTxDal objDal = new clTxDal();
            int i = 0;
            try
            {

                objDal.TxBegin();
                foreach (COLL_OVDIM_IM_SHINUY_HR oCollOvdimImShinuyHR in ArrCollShinuyim)
                {
                    i++;
                    objDal.AddParameter("p_coll_obj_ovdim_im_shinuy_hr", ParameterType.ntOracleArray, oCollOvdimImShinuyHR, ParameterDir.pdInput, "COLL_OVDIM_IM_SHINUY_HR");
                    objDal.AddParameter("p_tavla", ParameterType.ntOracleVarchar, sTable, ParameterDir.pdInput, 100);
                    objDal.ExecuteSP(clGeneral.cProInsOvdimImShinuyHR);
                    objDal.ClearCommand();
                }
                objDal.TxCommit();
            }
            catch (Exception ex)
            {
                objDal.TxRollBack();
                throw ex;
            }
        }


        public void SaveChangesDefaultsHR(COLL_BREROT_MECHDAL_MEAFYENIM collDefaults)
        {
            clDal oDal = new clDal();

            try
            {
                oDal.AddParameter("p_coll_obj_brerot_mechdal_hr", ParameterType.ntOracleArray, collDefaults, ParameterDir.pdInput, "COLL_BREROT_MECHDAL_MEAFYENIM");
                oDal.ExecuteSP(clGeneral.cProInsDefaultsHR);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void KdsWriteProcessLog(int KodTahalich, int KodPeilut,int KodStatus,string TeurTech)
        {
            clDal oDal = new clDal();
             try
            {
               oDal.AddParameter("p_KodTahalich", KdsLibrary.DAL.ParameterType.ntOracleInt64, KodTahalich, KdsLibrary.DAL.ParameterDir.pdInput);
               oDal.AddParameter("p_KodPeilut", KdsLibrary.DAL.ParameterType.ntOracleInt64, KodPeilut, KdsLibrary.DAL.ParameterDir.pdInput);
               oDal.AddParameter("p_KodStatus", KdsLibrary.DAL.ParameterType.ntOracleInt64, KodStatus, KdsLibrary.DAL.ParameterDir.pdInput);
               if (TeurTech.Length  > 100 )
                    TeurTech = TeurTech.Substring(0,100);
               oDal.AddParameter("p_TeurTech", KdsLibrary.DAL.ParameterType.ntOracleVarchar, TeurTech, KdsLibrary.DAL.ParameterDir.pdInput);
               oDal.ExecuteSP("PKG_BATCH.pro_ins_log_tahalich");
      
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MoveNewMatzavOvdimToOld()
        {
            clDal oDal = new clDal();
            try
            {
                oDal.ExecuteSP(clGeneral.cProMoveNewMatzavOvdimToOld);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void MoveNewPirteyOvedToOld()
        {
            clDal oDal = new clDal();
            try
            {
                oDal.ExecuteSP(clGeneral.cProMoveNewPirteyOvedToOld);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void MoveNewMeafyenimOvdimToOld()
        {
            clDal oDal = new clDal();
            try
            {
                oDal.ExecuteSP(clGeneral.cProMoveNewMeafyenimOvdimToOld);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void MoveNewBrerotMechdalToOld()
        {
            clDal oDal = new clDal();
            try
            {
                oDal.ExecuteSP(clGeneral.cProMoveNewBrerotMechdalToOld);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void MoveRecordsToHistory(DateTime date)
        {
            clDal oDal = new clDal();file:///C:\Documents and Settings\meravn\My Documents\Visual Studio 2008\Projects\kds\KdsWebSite\Bin\
            try
            {
                oDal.AddParameter("p_taarich", KdsLibrary.DAL.ParameterType.ntOracleDate, date, KdsLibrary.DAL.ParameterDir.pdInput);
                oDal.ExecuteSP(clGeneral.cProMoveRecordsToHistory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetTavlaotToRefresh()
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {
                //oDal.AddParameter("p_direction", ParameterType.ntOracleInteger, Direction, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetTavlaotToRefresh, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
