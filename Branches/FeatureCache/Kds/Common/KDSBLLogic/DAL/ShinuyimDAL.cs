using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DalOraInfra.DAL;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.Interfaces.DAL;
using KDSCommon.UDT;
using ObjectCompare;

namespace KDSBLLogic.DAL
{
    public class ShinuyimDAL : IShinuyimDAL
    {
        public const string cProGetIdkuneyRashemet = "pkg_errors.get_idkuney_rashemet";
        public const string cProGetApprovalErrors = "pkg_errors.get_approval_errors";
        public const string cProUpdApprovalErrors = "pkg_errors.pro_upd_approval_errors";
        public const string cProUpdIdkunRashemet = "pkg_ovdim.pro_ins_idkuney_rashemet";
        public const string cProDeleteIdkunRashemet = "pkg_ovdim.pro_delete_idkuney_rashemet";
        public const string cProShinuyKelet = "pkg_errors.pro_shinuy_kelet";
        public const string cProSaveLogShinuyKelet = "pkg_errors.pro_save_log_shinuy_kelet";

        public DataTable GetIdkuneyRashemet(int iMisparIshi, DateTime dTaarich)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);

                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProGetIdkuneyRashemet, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetApprovalErrors(int iMisparIshi, DateTime dTaarich)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);

                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProGetApprovalErrors, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  void SaveIdkunRashemet(COLL_IDKUN_RASHEMET oCollIdkunRashemet)
        {
            clDal Dal = new clDal();
            try
            {
                Dal.AddParameter("p_coll_idkun_rashemet", ParameterType.ntOracleArray, oCollIdkunRashemet, ParameterDir.pdInput, "COLL_IDKUN_RASHEMET");
                Dal.ExecuteSP(cProUpdIdkunRashemet);
            }
            catch (Exception ex)
            {
                
                //clMail omail;
                //string[] RecipientsList = (ConfigurationManager.AppSettings["MailErrorWorkCard"].ToString()).Split(';');
                //RecipientsList.ToList().ForEach(recipient =>
                //{
                //    omail = new clMail(recipient, "תקלה בשמירת עדכוני רשמת למספר אישי: " + oCollIdkunRashemet.Value[0].MISPAR_ISHI + "  תאריך:" + oCollIdkunRashemet.Value[0].TAARICH.ToShortDateString(), ex.Message);
                //    omail.SendMail();
                //});

                throw ex;
            }
        }

        public void DeleteIdkunRashemet(COLL_IDKUN_RASHEMET oCollIdkunRashemetDel)
        {
            clDal Dal = new clDal();
            try
            {
                Dal.AddParameter("p_coll_idkun_rashemet", ParameterType.ntOracleArray, oCollIdkunRashemetDel, ParameterDir.pdInput, "COLL_IDKUN_RASHEMET");
                Dal.ExecuteSP(cProDeleteIdkunRashemet);
            }
            catch (Exception ex)
            {  
                throw ex;
            }
        }
        public  void UpdateAprrovalErrors(COLL_SHGIOT_MEUSHAROT oCollShgiotMeusharot)
        {
            clDal Dal = new clDal();
            try
            {
                Dal.AddParameter("p_coll_shgiot_meusharot", ParameterType.ntOracleArray, oCollShgiotMeusharot, ParameterDir.pdInput, "COLL_SHGIOT_MEUSHAROT");
                Dal.ExecuteSP(cProUpdApprovalErrors);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void SaveShinuyKelet(ShinuyInputData inputData, COLL_YAMEY_AVODA_OVDIM collYemeyAvodaOvdimUpd, COLL_SIDURIM_OVDIM oraSidurimCollUpd, COLL_OBJ_PEILUT_OVDIM oraPeilutCollUpd)
        {
            clDal oDal = new clDal();
            try
            {
                //EventLog kdsLog = new EventLog();
                //kdsLog.Source = "KDS";

                oDal.AddParameter("p_coll_yamey_avoda_ovdim", ParameterType.ntOracleArray, collYemeyAvodaOvdimUpd, ParameterDir.pdInput, "COLL_YAMEY_AVODA_OVDIM");
                oDal.AddParameter("p_coll_sidurim_ovdim_upd", ParameterType.ntOracleArray, oraSidurimCollUpd, ParameterDir.pdInput, "COLL_SIDURIM_OVDIM");
                oDal.AddParameter("p_coll_sidurim_ovdim_ins", ParameterType.ntOracleArray, inputData.oCollSidurimOvdimIns, ParameterDir.pdInput, "COLL_SIDURIM_OVDIM");
                oDal.AddParameter("p_coll_sidurim_ovdim_del", ParameterType.ntOracleArray, inputData.oCollSidurimOvdimDel, ParameterDir.pdInput, "COLL_SIDURIM_OVDIM");
                oDal.AddParameter("p_coll_obj_peilut_ovdim_upd", ParameterType.ntOracleArray, oraPeilutCollUpd, ParameterDir.pdInput, "COLL_OBJ_PEILUT_OVDIM");
                oDal.AddParameter("p_coll_obj_peilut_ovdim_ins", ParameterType.ntOracleArray, inputData.oCollPeilutOvdimIns, ParameterDir.pdInput, "COLL_OBJ_PEILUT_OVDIM");
                oDal.AddParameter("p_coll_obj_peilut_ovdim_del", ParameterType.ntOracleArray, inputData.oCollPeilutOvdimDel, ParameterDir.pdInput, "COLL_OBJ_PEILUT_OVDIM");
                //kdsLog.WriteEntry("10.1", EventLogEntryType.Error);
                oDal.ExecuteSP(cProShinuyKelet);
                //kdsLog.WriteEntry("10.2", EventLogEntryType.Error);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void SaveLogsShinuyKelet(ShinuyInputData inputData)
        {
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("p_coll_log_day_kelet", ParameterType.ntOracleArray, inputData.oCollLogsDay, ParameterDir.pdInput, "COLL_LOG_DAY_KELET");
                oDal.AddParameter("p_coll_log_sidur_kelet", ParameterType.ntOracleArray, inputData.oCollLogsSidur, ParameterDir.pdInput, "COLL_LOG_SIDUR_KELET");
                oDal.AddParameter("p_coll_log_peilut_kelet", ParameterType.ntOracleArray, inputData.oCollLogsPeilut, ParameterDir.pdInput, "COLL_LOG_PEILUT_KELET");

                oDal.ExecuteSP(cProSaveLogShinuyKelet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
