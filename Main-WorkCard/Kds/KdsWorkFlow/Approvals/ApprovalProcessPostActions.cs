using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DalOraInfra.DAL;

namespace KdsWorkFlow.Approvals
{
    public class ApprovalProcessPostActions
    {
        private long _btchRequest;
        public ApprovalProcessPostActions(long btchRequest)
        {
            _btchRequest = btchRequest;
        }

        private void MarkExpiredApprovals()
        {
            //get all  not marked and expired requests pending for 
            //main factor  
            DataTable dt = GetExpiredPendingRequests();
            if (dt != null)
            {
                ApprovalFactory.LogErrorOfApprovalBatchPrc(_btchRequest, "I",
                    String.Format("Marking expired approvals started. Total {0} rows.",
                    dt.Rows.Count));
                int managerNumber = -1;
                int? secondaryManagerNumber = null;
                foreach (DataRow dr in dt.Rows)
                {
                    var approvalKey =
                            ApprovalRequest.GetApprovalKeyFromDataRow(dr);
                    var appRequest = ApprovalRequest.CreateApprovalRequest(
                        approvalKey.Employee.EmployeeNumber,
                        approvalKey.Approval.Code, approvalKey.Approval.Level,
                        approvalKey.WorkCard, approvalKey.RequestValues, true);
                    int tmpManager = Convert.ToInt32(dr["gorem_measher_rashsi"]);
                    if (managerNumber != tmpManager)
                    {
                        managerNumber = tmpManager;
                        //find manager for gorem_measher_rashsi using access to HR
                        secondaryManagerNumber = GetSecondaryManager(appRequest, managerNumber);
                    }
                    //update secondary manager for current approval request
                    UpdateSecondaryFactor(appRequest, secondaryManagerNumber, managerNumber);
                }
                ApprovalFactory.LogErrorOfApprovalBatchPrc(_btchRequest, "I",
                    "Marking expired approvals finished.");
            }

        }

        private void UpdateSecondaryFactor(ApprovalRequest appRequest,
            int? secondaryManagerNumber, int managerNumber)
        {
            try
            {
                bool result;
                clDal dal = new clDal();
                dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger,
                    appRequest.Employee.EmployeeNumber, ParameterDir.pdInput);
                dal.AddParameter("p_kod_ishur", ParameterType.ntOracleInteger,
                    appRequest.Approval.Code, ParameterDir.pdInput);
                dal.AddParameter("p_taarich", ParameterType.ntOracleDate,
                    appRequest.WorkCard.WorkDate, ParameterDir.pdInput);
                dal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger,
                    appRequest.WorkCard.SidurNumber, ParameterDir.pdInput);
                dal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate,
                    appRequest.WorkCard.SidurStart, ParameterDir.pdInput);
                dal.AddParameter("p_shat_yetzia", ParameterType.ntOracleDate,
                    appRequest.WorkCard.ActivityStart, ParameterDir.pdInput);
                dal.AddParameter("p_mispar_knisa", ParameterType.ntOracleInteger,
                    appRequest.WorkCard.ActivityNumber, ParameterDir.pdInput);
                dal.AddParameter("p_rama", ParameterType.ntOracleInteger,
                    appRequest.Approval.Level, ParameterDir.pdInput);
                dal.AddParameter("p_erech_mevukash", ParameterType.ntOracleDecimal,
                    appRequest.RequestValues[RequestValues.FIRST_REQUEST_VALUE_KEY], 
                    ParameterDir.pdInput);
                dal.AddParameter("p_erech_mevukash2", ParameterType.ntOracleDecimal,
                    appRequest.RequestValues[RequestValues.SECOND_REQUEST_VALUE_KEY], 
                    ParameterDir.pdInput);
                dal.AddParameter("p_gorem_measher_mishni", ParameterType.ntOracleInteger,
                    secondaryManagerNumber, ParameterDir.pdInput);
                dal.AddParameter("p_rows_affected", ParameterType.ntOracleInteger,
                    null, ParameterDir.pdOutput);

                dal.ExecuteSP("PKG_APPROVALS.update_gorem_measher_mishni");
                int rowsAffected = 0;
                if (!int.TryParse(dal.GetValParam("p_rows_affected"), out rowsAffected))
                    result = false;
                else result = (rowsAffected == 1);


                if (!result)
                {
                    string warning = String.Concat("update secondary factor not succeded for mispar ishi:",
                        appRequest.Employee.EmployeeNumber.ToString(), " kod ishur:", appRequest.Approval.Code.ToString(),
                        " taarich:", appRequest.WorkCard.WorkDate.ToShortDateString(), " mispar sidur:",
                        appRequest.WorkCard.SidurNumber, " gorem mishni:", secondaryManagerNumber);
                    ApprovalFactory.LogErrorOfApprovalBatchPrc(_btchRequest, "W", warning);
                }
                else if (managerNumber > 0 && !secondaryManagerNumber.HasValue)
                {
                    string hrWarning = String.Format("Null value from HR for Manager: {0}, Approval details: {1}",
                        managerNumber, appRequest.DetailsForErrorMessage);
                    ApprovalFactory.LogErrorOfApprovalBatchPrc(_btchRequest, "W", hrWarning);
                }
                    
            }
            catch (Exception ex)
            {
                ApprovalFactory.LogErrorOfApprovalBatchPrc(_btchRequest, ex.ToString());
            }
        }

        private int? GetSecondaryManager(ApprovalRequest appRequest, int managerNumber)
        {
            appRequest.GetApprovalFactorsFromHR(managerNumber);
            return appRequest.MainFactor.EmployeeNumber;
        }

        private DataTable GetExpiredPendingRequests()
        {
            try
            {
                clDal dal = new clDal();
                DataTable dt = new DataTable();
                dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor,
                    null, ParameterDir.pdOutput);
                dal.ExecuteSP("PKG_APPROVALS.get_expired_pending_approvals", ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                ApprovalFactory.LogErrorOfApprovalBatchPrc(_btchRequest, ex.ToString());
                return null;
            }
        }

        public void RunPostActions()
        {
            MarkExpiredApprovals();
        }

    }
}
