using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary.Security;
using KdsLibrary.DAL;
using System.Data;

namespace KdsWorkFlow.Approvals
{
    /// <summary>
    /// Manages Apporval requests
    /// </summary>
    public class ApprovalManager
    {
        #region Fields
        LoginUser _user; 
        #endregion

        #region Constractors
        public ApprovalManager(LoginUser user)
        {
            _user = user;
        } 
        #endregion
       
        #region Methods
		/// <summary>
        /// Gets distinct list of months that the logged on user
        /// has Approval requests
        /// </summary>
        /// <returns></returns>
        public DataTable GetApprovalRequestsDates()
        {
            DataTable dt = new DataTable();
            var dal = new clDal();
            dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger,
                _user.UserInfo.EmployeeNumber, ParameterDir.pdInput);
            dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor,
                null, ParameterDir.pdOutput);
            dal.ExecuteSP("PKG_APPROVALS.get_approval_requests_dates", ref dt);
            return dt;
        }

        /// <summary>
        /// Gets list of Approval request states
        /// </summary>
        /// <returns></returns>
        public DataTable GetApprovalStatuses()
        {
            DataTable dt = new DataTable();
            var dal = new clDal();
            dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor,
                null, ParameterDir.pdOutput);
            dal.ExecuteSP("PKG_APPROVALS.get_approval_statuses", ref dt);
            
            return dt;
        }

        /// <summary>
        /// Gets list of all Approval requests for logged on user
        /// according to arguments
        /// </summary>
        /// <param name="status">Approval request state</param>
        /// <param name="month">Month when the request was registered</param>
        /// <param name="year">Year when the request was registered</param>
        /// <returns></returns>
        public DataTable GetApprovalRequests(ApprovalRequestState? status, 
            int? month, int? year)
        {
            if (status.HasValue && !ApprovalManager.AllowMonthFilteringForStatus(status.Value))
            {
                month = null;
                year = null;
            }
            DataTable dt = new DataTable();
            var dal = new clDal();
            dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger,
                _user.UserInfo.EmployeeNumber, ParameterDir.pdInput);
            dal.AddParameter("p_status", ParameterType.ntOracleInteger,
               status, ParameterDir.pdInput);
            dal.AddParameter("p_additional_status", ParameterType.ntOracleInteger,
               ApprovalManager.GetAdditinalFilterStatus(status), ParameterDir.pdInput);
            dal.AddParameter("p_month", ParameterType.ntOracleInteger,
               month, ParameterDir.pdInput);
            dal.AddParameter("p_year", ParameterType.ntOracleInteger,
               year, ParameterDir.pdInput);
            dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor,
                null, ParameterDir.pdOutput);
            dal.ExecuteSP("PKG_APPROVALS.get_all_approval_requests", ref dt);
            return dt;
        }

        public DataTable GetApprovalCodes()
        {
            DataTable dt = new DataTable();
            var dal = new clDal();
            dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor,
                null, ParameterDir.pdOutput);
            dal.ExecuteSP("PKG_APPROVALS.get_approval_codes", ref dt);

            return dt;
        }

        /// <summary>
        /// Get jobs for forwading the approval request
        /// to another factor
        /// </summary>
        /// <returns>DataTable of Jobs</returns>
        public DataTable GetApprovalJobs()
        {
            DataTable dt = new DataTable();
            var dal = new clDal();
            dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor,
                null, ParameterDir.pdOutput);
            dal.ExecuteSP("PKG_APPROVALS.get_approval_jobs", ref dt);

            return dt;
        }

        public static bool AllowMonthFilteringForStatus(ApprovalRequestState status)
        {
            return status != ApprovalRequestState.Pending;
        }

        public static ApprovalRequestState? GetAdditinalFilterStatus(ApprovalRequestState? status)
        {
            if (!status.HasValue) return null;
            switch(status.Value)
            {
                case ApprovalRequestState.Pending:
                    return ApprovalRequestState.TransferToAnother;
                default: return null;
            }
        }

        public static bool RequiresAdditional(int approvalCode)
        {
            return approvalCode == 34 || approvalCode == 35 ||
                approvalCode == 44 || approvalCode == 45;  
        }

        public static int ExtraHoursDisplayValue(int approvalCode)
        {
            switch (approvalCode)
            {
                case 34:
                case 35: return 1; //Chol
                case 44:
                case 45: return 2; //Shabat
                default: return 0;
            }
        }
        public static bool AllowRegularApproval(int approvalCode, 
            ApprovalRequestState requestState, ApprovalRequestState nextLevelState)
        {
            //if requestState==Approved then 
            // check if the next level has touched this request
            // if the status for next level on this request is NOT Pending
            // then the answer is false
           return !RequiresAdditional(approvalCode) && 
               (requestState == ApprovalRequestState.Pending ||
                requestState == ApprovalRequestState.Declined ||
                ((requestState == ApprovalRequestState.Approved || 
                    requestState == ApprovalRequestState.TransferToAnother) &&
                    nextLevelState == ApprovalRequestState.Pending));
        }

        public static bool AllowSpecialApproval(int approvalCode,
            ApprovalRequestState requestState, ApprovalRequestState nextLevelState)
        {
            return RequiresAdditional(approvalCode) &&
               (requestState == ApprovalRequestState.Pending ||
                requestState == ApprovalRequestState.Declined ||
                (requestState == ApprovalRequestState.Approved &&
                    nextLevelState == ApprovalRequestState.Pending));
        }

        public static bool ShowCodeDescription(int approvalCode)
        {
            return RequiresAdditional(approvalCode) ; 
        }

        public static bool ShowDateDetailedDescription(int approvalCode)
        {
            return !RequiresAdditional(approvalCode); 
        }

        
	    #endregion    
    
        /// <summary>
        /// Checks if the approval request was forwarded to the next level
        /// and has been asnwered by the next level
        /// </summary>
        /// <param name="approvalKey">Key of Approval request</param>
        /// <param name="level">Level of Approval Request</param>
        /// <returns>true if the request was forwarded and answered</returns>
        public bool IsRequestForwardedAndAnswered(ApprovalKey approvalKey, int level)
        {
            bool result = false;
            clDal dal = new clDal();
            DataTable dt = new DataTable();
            dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger,
                approvalKey.Employee.EmployeeNumber, ParameterDir.pdInput);
            dal.AddParameter("p_kod_ishur", ParameterType.ntOracleInteger,
                approvalKey.Approval.Code, ParameterDir.pdInput);
            dal.AddParameter("p_taarich", ParameterType.ntOracleDate,
                approvalKey.WorkCard.WorkDate, ParameterDir.pdInput);
            dal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger,
                approvalKey.WorkCard.SidurNumber, ParameterDir.pdInput);
            dal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate,
                approvalKey.WorkCard.SidurStart, ParameterDir.pdInput);
            dal.AddParameter("p_shat_yetzia", ParameterType.ntOracleDate,
                approvalKey.WorkCard.ActivityStart, ParameterDir.pdInput);
            dal.AddParameter("p_mispar_knisa", ParameterType.ntOracleInteger,
               approvalKey.WorkCard.ActivityNumber, ParameterDir.pdInput);
            dal.AddParameter("p_rama", ParameterType.ntOracleInteger,
               null, ParameterDir.pdInput);
            dal.AddParameter("p_erech_mevukash", ParameterType.ntOracleDecimal,
                approvalKey.RequestValues[RequestValues.FIRST_REQUEST_VALUE_KEY],
                ParameterDir.pdInput);
            dal.AddParameter("p_erech_mevukash2", ParameterType.ntOracleDecimal,
                approvalKey.RequestValues[RequestValues.SECOND_REQUEST_VALUE_KEY],
                ParameterDir.pdInput);
            dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor,
                null, ParameterDir.pdOutput);
            dal.ExecuteSP("PKG_APPROVALS.get_approval_request", ref dt);
            foreach (DataRow dr in dt.Rows)
            {
                if (int.Parse(dr["rama"].ToString()) == level + 1 &&
                    int.Parse(dr["KOD_STATUS_ISHUR"].ToString()) !=
                    (int)ApprovalRequestState.Pending)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool AcceptApprovalRequest(ApprovalKey requestKey,
            out ApprovalRequest appRequest,
            out ApprovalRequest nextLevelRequest, params object[] paramData)
        {
            nextLevelRequest = null;
            ApprovalRequestState originalState = ApprovalRequestState.Empty;
            bool success = ChangeApprovalState(requestKey, ApprovalRequestState.Approved,
                out appRequest, out originalState, paramData) == ApprovalRequestState.Approved;
            if (success)
            {
                //apply suspend payment reason to Pending(=1) if 
                //the request is accepted and being forwarded to next level
                //if there is no next level the reason will be removed
                appRequest.SuspendPayment(SuspendPaymentReason.Pending);
                if (!appRequest.Approval.IsForwardToAnotherFactor)
                {
                    nextLevelRequest = appRequest.ForwardToNextLevel(originalState);
                    success = (nextLevelRequest != null) ||
                        String.IsNullOrEmpty(appRequest.ErrorMessage);
                }
            }
            return success;
        }

       
        public bool DeclineApprovalRequest(ApprovalKey requestKey,
            out ApprovalRequest appRequest,params object[] paramData)
        {
            ApprovalRequestState originalState = ApprovalRequestState.Empty;
            bool success = ChangeApprovalState(requestKey, ApprovalRequestState.Declined,
                out appRequest, out originalState, paramData) == ApprovalRequestState.Declined;
            if(success)
            {
                appRequest.SuspendPayment(SuspendPaymentReason.Declined);
                appRequest.UpdateDeviation();
                if (!appRequest.Approval.IsForwardToAnotherFactor 
                    && !appRequest.Approval.IsNextLevelExists() 
                    && !appRequest.PerformExternalProcessing())
                {
                    appRequest.RollbackLastApproval(originalState);
                    success = false;
                }
            }
            return success;
        }

        public bool ForwardApprovalRequestToAnotherFactor(ApprovalKey requestKey,
            string remarks, int jobCode, out ApprovalRequest fwdRequest)
        {
            fwdRequest = ApprovalRequest.CreateApprovalRequest(
                requestKey.Employee.EmployeeNumber,
                requestKey.Approval.Code, jobCode, requestKey.Approval.Level * 10,
                requestKey.WorkCard, requestKey.RequestValues, false, "heara", remarks);
            fwdRequest.ProcessRequest();
            //update kod_tafkid and factor number for 
            //original request
            UpdateForwardedRequest(requestKey, fwdRequest, jobCode);
            return fwdRequest.State == ApprovalRequestState.Pending;
        }
        private void UpdateForwardedRequest(ApprovalKey requestKey,
            ApprovalRequest fwdRequest, int jobCode)
        {
            ApprovalRequest origRequest = 
                ApprovalRequest.GetExistingApprovalRequest(requestKey);
            origRequest.SaveFactorNumberAndJobCode(jobCode,
                fwdRequest.MainFactor);
        }
        private ApprovalRequestState ChangeApprovalState(ApprovalKey requestKey,
            ApprovalRequestState newState, out ApprovalRequest appRequest,
            out ApprovalRequestState originalState,
            params object[] paramData)
        {
            appRequest = ApprovalRequest.GetExistingApprovalRequest(requestKey);
            originalState = appRequest.State;
            if (paramData != null)
            {
                for (int i = 0; i < paramData.Length - 1; i += 2)
                {
                    appRequest.StoreAdditionalData(paramData[i].ToString().ToLower(), paramData[i + 1]);
                }
            }
            
            return appRequest.ChangeApprovalState(newState);
        }

        public void UpdateRemark(ApprovalKey requestKey, string remarks)
        {
            ApprovalRequest appRequest = ApprovalRequest.GetExistingApprovalRequest(requestKey);
            appRequest.StoreAdditionalData("heara", remarks);
            appRequest.UpdateRemarks();
        }
    }
}
