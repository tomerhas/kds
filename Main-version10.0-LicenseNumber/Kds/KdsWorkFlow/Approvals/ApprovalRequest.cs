using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary.BL;
using System.Data;
using System.Collections.Specialized;
using System.Configuration;
using DalOraInfra.DAL;

namespace KdsWorkFlow.Approvals
{
    public delegate void ApprovalRequestEventHandler(object sender, ApprovalRequestEventArgs e);
    /// <summary>
    /// Represents Request for an Approval
    /// </summary>
    public class ApprovalRequest
    {
        public event ApprovalRequestEventHandler ApprovalRequestException;
        public event ApprovalRequestEventHandler InvalidValueFromHR;
        #region Fields
        private ApprovalKey _approvalKey;
        private ApprovalFactor[] _factors;
        private ApprovalRequestState _state;
        private Dictionary<string, object> _additionalData;
        private Dictionary<string, object> _additionalDataForRollBack;
        private string _errorMessage;
        Dictionary<string, object> _hrInParams;
        #endregion

        #region Constractors
        
        private ApprovalRequest(Employee employee, Approval approval, WorkCard workCard,
             RequestValues requestValues,
             bool callHR, params object[] additionalData)
        {
            _approvalKey = new ApprovalKey();
            _approvalKey.Approval = approval;
            _approvalKey.Employee = employee;
            _approvalKey.WorkCard = workCard;
            _approvalKey.RequestValues = requestValues;
            _state = ApprovalRequestState.Empty;
            _additionalData = new Dictionary<string,object>();
            _additionalDataForRollBack = new Dictionary<string, object>();
            StoreAdditionalData(additionalData);
            if (callHR) GetApprovalFactorsFromHR();
            InvalidValueFromHR += new ApprovalRequestEventHandler(ApprovalRequest_InvalidValueFromHR);
        }
        
        private ApprovalRequest(Employee employee, Approval approval, WorkCard workCard,
           RequestValues requestValues,
           bool callHR, Dictionary<string, object> additionalData)
        {
            _approvalKey = new ApprovalKey();
            _approvalKey.Approval = approval;
            _approvalKey.Employee = employee;
            _approvalKey.WorkCard = workCard;
            _approvalKey.RequestValues = requestValues;
            _state = ApprovalRequestState.Empty;
            _additionalData = additionalData;
            _additionalDataForRollBack = new Dictionary<string, object>();
            if (callHR) GetApprovalFactorsFromHR();
            InvalidValueFromHR += new ApprovalRequestEventHandler(ApprovalRequest_InvalidValueFromHR);
        }

       
        #endregion

        #region Methods
        public static ApprovalRequest CreateApprovalRequest(string employeeNumber,
            int approvalCode, WorkCard workCard, RequestValues requestValues, bool callHR, 
            params object[] additionalData)
        {
            return CreateApprovalRequest(employeeNumber, approvalCode,
                Approval.FIRST_APPROVAL_LEVEL, workCard, requestValues, callHR, additionalData);
        }

        public static ApprovalRequest CreateApprovalRequest(string employeeNumber,
            int approvalCode, int level, WorkCard workCard, RequestValues requestValues, 
            bool callHR, params object[] additionalData)
        {
            return new ApprovalRequest(new Employee(employeeNumber),
                new Approval(approvalCode, level), workCard, requestValues, callHR,
                additionalData);
        }

        public static ApprovalRequest CreateApprovalRequest(string employeeNumber,
           int approvalCode, int jobCode, int level, WorkCard workCard, RequestValues requestValues, 
           bool callHR, params object[] additionalData)
        {
            return new ApprovalRequest(new Employee(employeeNumber),
                new Approval(approvalCode, level, jobCode), workCard, requestValues, callHR,
                additionalData);
        }

        public static ApprovalRequest CreateApprovalRequest(string employeeNumber,
            int approvalCode, int level, WorkCard workCard, RequestValues requestValues, 
            bool callHR, Dictionary<string, object> additionalData)
        {
            return new ApprovalRequest(new Employee(employeeNumber),
                new Approval(approvalCode, level), workCard, requestValues, callHR, additionalData);
        }

        public static ApprovalRequest GetExistingApprovalRequest(ApprovalKey approvalKey)
        {
            ApprovalRequest appRequest = CreateApprovalRequest(
                approvalKey.Employee.EmployeeNumber,
                approvalKey.Approval.Code, approvalKey.Approval.Level,
                approvalKey.WorkCard, approvalKey.RequestValues, false);
            appRequest.InitializeExisitngRequest();
            return appRequest;
        }

        public ApprovalRequest GetPreviousLevelApprovalRequest()
        {
            ApprovalKey approvalKey = new ApprovalKey();
            approvalKey.Approval = new Approval(_approvalKey.Approval.Code,
                _approvalKey.Approval.GetRealLevel() - 1);
            approvalKey.Employee = _approvalKey.Employee;
            approvalKey.WorkCard = _approvalKey.WorkCard;
            approvalKey.RequestValues = _approvalKey.RequestValues;
            return ApprovalRequest.GetExistingApprovalRequest(approvalKey);
        }

        /// <summary>
        /// Finds all requests matching the Employee Number and Work Date
        /// with the Approval Request Status from the highest Level registered in DB
        /// for the request
        /// </summary>
        /// <param name="employeeNumber">Employee Number</param>
        /// <param name="workDate">Work Date</param>
        /// <returns>Array of ApprovalRequest</returns>
        public static ApprovalRequest[] GetMatchingApprovalRequestsWithStatuses(
            int employeeNumber, DateTime workDate)
        {
            List<ApprovalRequest> matchingReq = new List<ApprovalRequest>();
            DataTable dt = new DataTable();
            clDal dal = new clDal();
            dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger,
                employeeNumber, ParameterDir.pdInput);
            dal.AddParameter("p_taarich", ParameterType.ntOracleDate,
                workDate, ParameterDir.pdInput);
            dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
               
            dal.ExecuteSP("PKG_APPROVALS.get_matching_approval_requests", ref dt);
            BuildApprovalRequestsListFromDataTable(dt, matchingReq);
            
            return matchingReq.ToArray();
        }

        /// <summary>
        /// Finds all requests matching the provided ApprovalKey instance
        /// in order to find if a similar approval request already exists
        /// </summary>
        /// <param name="approvalKey">Instance of ApprovalKey to match</param>
        /// <returns>Array of ApprovalRequest</returns>
        public static ApprovalRequest[] GetSimilarApprovalRequests(
            ApprovalKey approvalKey)
        {
            List<ApprovalRequest> matchingReq = new List<ApprovalRequest>();
            DataTable dt = new DataTable();
            clDal dal = new clDal();
            dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger,
                approvalKey.Employee.EmployeeNumber, ParameterDir.pdInput);
            dal.AddParameter("p_taarich", ParameterType.ntOracleDate,
                approvalKey.WorkCard.WorkDate, ParameterDir.pdInput);
            
            dal.AddParameter("p_kod_ishur", ParameterType.ntOracleInteger,
                approvalKey.Approval.Code > 0 ? (object)approvalKey.Approval.Code : null, 
                ParameterDir.pdInput);
        
            dal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger,
                approvalKey.WorkCard.SidurNumber > 0 ? (object)approvalKey.WorkCard.SidurNumber : null,
                ParameterDir.pdInput);
        
            dal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate,
                approvalKey.WorkCard.SidurStart != DateTime.MinValue ?
                    (object)approvalKey.WorkCard.SidurStart : null,
                ParameterDir.pdInput);
        
            dal.AddParameter("p_shat_yetzia", ParameterType.ntOracleDate,
                approvalKey.WorkCard.ActivityStart != DateTime.MinValue ?
                    (object)approvalKey.WorkCard.ActivityStart : null,
                ParameterDir.pdInput);
        
            dal.AddParameter("p_mispar_knisa", ParameterType.ntOracleInteger,
                approvalKey.WorkCard.ActivityNumber > 0 ?
                    (object)approvalKey.WorkCard.ActivityNumber : null,
                ParameterDir.pdInput);
           
            dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor,
               null, ParameterDir.pdOutput);
            dal.ExecuteSP("PKG_APPROVALS.get_similar_approval_requests", ref dt);
            BuildApprovalRequestsListFromDataTable(dt, matchingReq);
            
            return matchingReq.ToArray();
        }

        private static void BuildApprovalRequestsListFromDataTable(DataTable dt,
            List<ApprovalRequest> matchingReq)
        {
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ApprovalKey key = GetApprovalKeyFromDataRow(dr);
                    ApprovalRequest request = ApprovalRequest.CreateApprovalRequest(
                        key.Employee.EmployeeNumber, key.Approval.Code,
                        int.Parse(dr["rama"].ToString()), key.WorkCard, 
                        key.RequestValues, false);
                    request._state = (ApprovalRequestState)
                        int.Parse(dr["kod_status_ishur"].ToString());
                    matchingReq.Add(request);
                }
            }
        }
            
        internal static ApprovalKey GetApprovalKeyFromDataRow(DataRow dr)
        {
            ApprovalKey key = new ApprovalKey();
            key.Employee = new Employee(dr["mispar_ishi"].ToString());
            if (dr.Table.Columns.Contains("rama"))
                key.Approval = new Approval(int.Parse(dr["kod_ishur"].ToString()),
                    int.Parse(dr["rama"].ToString()));
            else
                key.Approval = new Approval(int.Parse(dr["kod_ishur"].ToString()));
            key.WorkCard = new WorkCard();
            key.WorkCard.WorkDate = DateTime.Parse(dr["taarich"].ToString());
            key.WorkCard.SidurNumber = int.Parse(dr["mispar_sidur"].ToString());
            key.WorkCard.SidurStart = DateTime.Parse(dr["shat_hatchala"].ToString());
            key.WorkCard.ActivityStart = DateTime.Parse(dr["shat_yetzia"].ToString());
            key.WorkCard.ActivityNumber = int.Parse(dr["mispar_knisa"].ToString());
            if (dr.Table.Columns.Contains(RequestValues.FIRST_REQUEST_VALUE_KEY))
                key.RequestValues.SetValue(RequestValues.FIRST_REQUEST_VALUE_KEY,
                    dr[RequestValues.FIRST_REQUEST_VALUE_KEY]);
            if (dr.Table.Columns.Contains(RequestValues.SECOND_REQUEST_VALUE_KEY))
                key.RequestValues.SetValue(RequestValues.SECOND_REQUEST_VALUE_KEY,
                    dr[RequestValues.SECOND_REQUEST_VALUE_KEY]);
            return key;
        }
        
        //ProcessRequest method is overloaded because there are
        //cases when it is known that the request was already Approved
        public void ProcessRequest()
        {
            ProcessRequest(ApprovalRequestState.Pending);
        }
        /// <summary>
        /// Register the Approval Request(instance) in DB
        /// </summary>
        /// <param name="initialRequestState">
        /// Approval Request State which will be registered in DB
        /// The default is Pending
        /// </param>
        public void ProcessRequest(ApprovalRequestState initialRequestState)
        {
            _errorMessage = String.Empty;
            ApprovalRequestState result = ApprovalRequestState.Failed;
            try
            {
                if (!_approvalKey.Approval.IsExist) result = ApprovalRequestState.InvalidCode;
                else
                {
                    bool changesApplied = false;
                    //Check if employee/workcard belongs to sub company (like egged taavora)
                    if (CheckWorkCardSubCompanies(ref initialRequestState, out changesApplied))
                    {
                        //Get primary and secondary managers for 
                        //approval request
                        if (!changesApplied) GetApprovalFactorsFromHR();
                        //Check if same request already exists
                        ApprovalRequest requestToUpdate = null;
                        if (!IsRequestAlreadyExists(out requestToUpdate))
                            //Register Request in DB
                            result = RegisterRequest(initialRequestState);
                        else if (requestToUpdate != null)
                            //Update Request Values for existing request if they changed and the 
                            //request in first level and status is Pending
                            result = UpdateRequestValues(requestToUpdate);
                        else result = ApprovalRequestState.AlreadyExists;
                    }
                    else result = ApprovalRequestState.NotAllowed;
                }
            }
            catch (Exception exc)
            {
                result = ApprovalRequestState.Failed;
                _errorMessage = exc.Message;
                GenerateErrorLogMessage(exc.ToString());
            }
            _state = result;
            if (_state == ApprovalRequestState.Approved)
                ForwardToNextLevel(initialRequestState);
        }

        private bool CheckWorkCardSubCompanies(ref ApprovalRequestState defaultRequestState, 
            out bool changesApplied)
        {
            changesApplied = false;
            bool allowContinue = false;
            //check if employee 
            bool fromSubCompany = IsFromSubCompany() || IsReinforceSubCompany();
            if (fromSubCompany)
            {
                if (_approvalKey.Approval.IsForSubCompany)
                {
                    ConvertApprovalCodeForSubCompany();
                    changesApplied = true;
                    defaultRequestState = ApprovalRequestState.Approved;
                    SetDefaultFactors("SubCompanyFactor");
                    allowContinue = true;
                }
                else allowContinue = false;
            }
            else allowContinue = true;
            return allowContinue;
        }

        private void SetDefaultFactors(string setting)
        {
            _factors = new ApprovalFactor[2] { new ApprovalFactor(), 
                new ApprovalFactor() };
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings[setting]))
                _factors[0].EmployeeNumber = int.Parse(
                    ConfigurationManager.AppSettings[setting]);
        }

        /// <summary>
        /// Adds additional data for the request like remarks,
        /// erech_mevukash,erech_meushar
        /// </summary>
        /// <param name="paramData">object array of data which consists of
        /// name and value, when the name is the Column name from DB
        /// </param>
        public void StoreAdditionalData(params object[] paramData)
        {
            if (paramData != null)
            {
                for (int i = 0; i < paramData.Length - 1; i += 2)
                {
                    if (!_additionalData.ContainsKey(paramData[i].ToString().ToLower()))
                    {
                        _additionalData.Add(paramData[i].ToString().ToLower(),
                            paramData[i + 1]);
                        SaveAdditionalDataValueForRollback(paramData[i].ToString().ToLower(),
                            null);
                    }
                    else
                    {
                        SaveAdditionalDataValueForRollback(paramData[i].ToString().ToLower(),
                            _additionalData[paramData[i].ToString().ToLower()]);
                        _additionalData[paramData[i].ToString().ToLower()] = paramData[i + 1];
                    }

                }
            }
        }

        private void SaveAdditionalDataValueForRollback(string key, object value)
        {
            if (_additionalDataForRollBack.ContainsKey(key))
                _additionalDataForRollBack[key] = value;
            else _additionalDataForRollBack.Add(key, value);
        }
        
        /// <summary>
        /// Changes state of the request
        /// </summary>
        /// <param name="approvalRequestState">State to be updated to</param>
        /// <returns>if success then the new approval state 
        /// otherwise state indicating error occured</returns>
        internal ApprovalRequestState ChangeApprovalState(
            ApprovalRequestState approvalRequestState)
        {
            ApprovalRequestState result = ApprovalRequestState.Failed;
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger,
                    _approvalKey.Employee.EmployeeNumber, ParameterDir.pdInput);
                dal.AddParameter("p_kod_ishur", ParameterType.ntOracleInteger,
                    _approvalKey.Approval.Code, ParameterDir.pdInput);
                dal.AddParameter("p_taarich", ParameterType.ntOracleDate,
                    _approvalKey.WorkCard.WorkDate, ParameterDir.pdInput);
                dal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger,
                    _approvalKey.WorkCard.SidurNumber, ParameterDir.pdInput);
                dal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate,
                    _approvalKey.WorkCard.SidurStart, ParameterDir.pdInput);
                dal.AddParameter("p_shat_yetzia", ParameterType.ntOracleDate,
                    _approvalKey.WorkCard.ActivityStart, ParameterDir.pdInput);
                dal.AddParameter("p_mispar_knisa", ParameterType.ntOracleInteger,
                    _approvalKey.WorkCard.ActivityNumber, ParameterDir.pdInput);
                dal.AddParameter("p_status", ParameterType.ntOracleInteger,
                   (int)approvalRequestState, ParameterDir.pdInput);
                dal.AddParameter("p_rama", ParameterType.ntOracleInteger,
                    _approvalKey.Approval.Level, ParameterDir.pdInput);
                dal.AddParameter("p_erech_mevukash", ParameterType.ntOracleDecimal,
                    _approvalKey.RequestValues[RequestValues.FIRST_REQUEST_VALUE_KEY], 
                    ParameterDir.pdInput);
                dal.AddParameter("p_erech_mevukash2", ParameterType.ntOracleDecimal,
                    _approvalKey.RequestValues[RequestValues.SECOND_REQUEST_VALUE_KEY], 
                    ParameterDir.pdInput);
                dal.AddParameter("p_heara", ParameterType.ntOracleVarchar,
                    _additionalData["heara"].ToString(), ParameterDir.pdInput);
                dal.AddParameter("p_erech_meushar", ParameterType.ntOracleDecimal,
                    _additionalData["erech_meushar"], ParameterDir.pdInput);
                dal.AddParameter("p_meadken_acharon", ParameterType.ntOracleInteger,
                    KdsLibrary.Security.LoginUser.GetLoginUser().UserInfo.EmployeeNumber, 
                    ParameterDir.pdInput);
                dal.AddParameter("p_rows_affected", ParameterType.ntOracleInteger,
                    null, ParameterDir.pdOutput);
                dal.ExecuteSP("PKG_APPROVALS.change_approval_request_status");
                int rowsAffected = 0;
                if (!int.TryParse(dal.GetValParam("p_rows_affected"), out rowsAffected))
                    result = ApprovalRequestState.Failed;
                else if (rowsAffected == 1)
                    result = approvalRequestState;
                else
                {
                    result = ApprovalRequestState.Failed;
                    _errorMessage = "Invalid request key";
                }

                return approvalRequestState;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                GenerateErrorLogMessage(ex.ToString());
                result = ApprovalRequestState.Failed;
            }
            return result;
        }

        /// <summary>
        /// Updates remarks for the request
        /// </summary>
        internal void UpdateRemarks()
        {
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger,
                    _approvalKey.Employee.EmployeeNumber, ParameterDir.pdInput);
                dal.AddParameter("p_kod_ishur", ParameterType.ntOracleInteger,
                    _approvalKey.Approval.Code, ParameterDir.pdInput);
                dal.AddParameter("p_taarich", ParameterType.ntOracleDate,
                    _approvalKey.WorkCard.WorkDate, ParameterDir.pdInput);
                dal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger,
                    _approvalKey.WorkCard.SidurNumber, ParameterDir.pdInput);
                dal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate,
                    _approvalKey.WorkCard.SidurStart, ParameterDir.pdInput);
                dal.AddParameter("p_shat_yetzia", ParameterType.ntOracleDate,
                    _approvalKey.WorkCard.ActivityStart, ParameterDir.pdInput);
                dal.AddParameter("p_mispar_knisa", ParameterType.ntOracleInteger,
                    _approvalKey.WorkCard.ActivityNumber, ParameterDir.pdInput);
                dal.AddParameter("p_rama", ParameterType.ntOracleInteger,
                    _approvalKey.Approval.Level, ParameterDir.pdInput);
                dal.AddParameter("p_erech_mevukash", ParameterType.ntOracleDecimal,
                    _approvalKey.RequestValues[RequestValues.FIRST_REQUEST_VALUE_KEY], 
                    ParameterDir.pdInput);
                dal.AddParameter("p_erech_mevukash2", ParameterType.ntOracleDecimal,
                    _approvalKey.RequestValues[RequestValues.SECOND_REQUEST_VALUE_KEY], 
                    ParameterDir.pdInput);
                dal.AddParameter("p_heara", ParameterType.ntOracleVarchar,
                    _additionalData["heara"].ToString(), ParameterDir.pdInput);
                dal.AddParameter("p_rows_affected", ParameterType.ntOracleInteger,
                    null, ParameterDir.pdOutput);
                dal.ExecuteSP("PKG_APPROVALS.update_approval_request_remark");
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                GenerateErrorLogMessage(ex.ToString());
            }
        }

        /// <summary>
        /// Forwards request to next level if it's been approved
        /// </summary>
        /// <param name="originalRequestState">Previous state of the request before it was approved</param>
        /// <returns>ApprovalRequest instance pointing to request for the next level</returns>
        internal ApprovalRequest ForwardToNextLevel(ApprovalRequestState originalRequestState)
        {
            ApprovalRequest nextRequest = null;
            //forward request to next level
            if (_approvalKey.Approval.IsNextLevelExists())
            {
                nextRequest =
                    ApprovalRequest.CreateApprovalRequest(_approvalKey.Employee.EmployeeNumber,
                    _approvalKey.Approval.Code, _approvalKey.Approval.GetRealLevel() + 1,
                    _approvalKey.WorkCard, _approvalKey.RequestValues, false, _additionalData);
                nextRequest.ProcessRequest();
            }
            else
            {
                if (PerformExternalProcessing())
                {
                    ReleasePayment();
                    UpdatePaymentHours();
                    UpdateDeviation(_approvalKey.RequestValues[RequestValues.FIRST_REQUEST_VALUE_KEY]);
                }
                else
                {
                    RollbackLastApproval(originalRequestState);
                }
            }
            return nextRequest;
        }

        /// <summary>
        /// Performs additional calculation and data update
        /// </summary>
        /// <returns>true if successful otherwise false</returns>
        internal bool PerformExternalProcessing()
        {
            bool success = false;
            if (_approvalKey.Approval.RequiresExternalProcessOnFinish)
            {
                double[] values = GetCodeComponentsValues();
                double erechMeushar = 0;
                double.TryParse(GetAdditionalDataValue("erech_meushar").ToString(),
                    out erechMeushar);

                //14,13 for approval 34; 16,17 for approvals 44,45
                int[] charCodes = _approvalKey.Approval.CharacteristicCodesForUpdate;
                if (erechMeushar > values[0] && values[0] > 0)
                    //for 34: 14:=147+143; 13:=253+meushar-147
                    //for 44,45: 16:=161+254; 17:=255+meushar-161
                    success = UpdateCharacteristicValues(charCodes[0], values[0] + values[2],
                        charCodes[1], values[1] + erechMeushar - values[0]);
                else if (values[0] == 0)
                    //for 34: 13:=253+meushar;
                    //for 44,45: 17:=255+meushar
                    success = UpdateCharacteristicValues(charCodes[1], values[1] + erechMeushar);
                else
                    //for 34: 14:=143+meushar; 13:=253
                    //for 44,45: 16:=254+meushar; 17:=255
                    success = UpdateCharacteristicValues(charCodes[0], values[2] + erechMeushar,
                        charCodes[1], values[1]);
            }
            else success = true;
            return success;
        }

        private double[] GetCodeComponentsValues()
        {
            var compValues = GetAllComponentValues();
            int[] codes = _approvalKey.Approval.ComponentCodes;
            double[]  values = new double[codes.Length]; 
            for (int i = 0; i < codes.Length; ++i)
            {
                if (compValues.ContainsKey(codes[i]))
                    values[i] = compValues[codes[i]];
            }
            return values;           
        }

        private bool UpdateCharacteristicValues(params object[] paramArray)
        {
            bool success = false;
            clTxDal txDal = new clTxDal();
            txDal.TxBegin();
            for (int i = 0; i < paramArray.Length - 1; i+=2)
            {
                int code = (int)paramArray[i];
                double value = (double)paramArray[i + 1];
                success = UpdateCharacteristicValue(txDal, code, value);
                if (!success) break;
            }
            if (success) txDal.TxCommit();
            else txDal.TxRollBack();
            
            return success;
        }

        private bool UpdateCharacteristicValue(clTxDal txDal, int charCode, double charValue)
        {
            bool updateSucceded=false;
            try
            {
                txDal.ClearCommand();
                DateTime firstDayOfMonth = new DateTime(_approvalKey.WorkCard.WorkDate.Year,
                     _approvalKey.WorkCard.WorkDate.Month, 1);
                txDal.AddParameter("retcode", ParameterType.ntOracleInteger, null,
                    ParameterDir.pdOutput);
                txDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleVarchar,
                         _approvalKey.Employee.EmployeeNumber.ToString(), ParameterDir.pdInput);
                txDal.AddParameter("p_date", ParameterType.ntOracleDate,
                    firstDayOfMonth, ParameterDir.pdInput);
                txDal.AddParameter("p_approve_value", ParameterType.ntOracleDecimal,
                  charValue, ParameterDir.pdInput);
                txDal.AddParameter("p_kod_meafien", ParameterType.ntOracleInteger,
                   charCode, ParameterDir.pdInput);
                txDal.ExecuteSP("EGD_MAFIENIM_NEW.Tipul_Mafienim@KDS_GW");
                string retVal = txDal.GetValParam("retcode");
                int retCode = -1;
                if (int.TryParse(retVal, out retCode))
                    updateSucceded = retCode == 0;
                if (!updateSucceded)
                    _errorMessage =
                            GenerateErrorLogMessage(String.Format(@"Error in Updating Meafyen {0} to value {1}. 
                            Error Code returned from EGD_MAFIENIM_NEW.Tipul_Mafienim@KDS_GW is {2}",
                            charCode, charValue, retVal));
            }
            catch (Exception ex)
            {
                _errorMessage = GenerateErrorLogMessage(ex.Message);
            }
            return updateSucceded;
        }

        private string GenerateErrorLogMessage(string errorMessage)
        {
            string logMess = String.Format("Apporval error in: {0}. Details: {1}", errorMessage,
                DetailsForErrorMessage);
            KdsLibrary.clGeneral.LogError(logMess);
            if (ApprovalRequestException != null)
            {
                ApprovalRequestException(this, new ApprovalRequestEventArgs(_approvalKey,
                    logMess, ApprovalExceptionType.Error));
            }
            return logMess;
        }

        /// <summary>
        /// Returns Approval State and Additional Data to previous value
        /// </summary>
        /// <param name="statetoRollback">Previous state</param>
        internal void RollbackLastApproval(ApprovalRequestState statetoRollback)
        {
            RestoreAdditionalDataForRollback();
            ChangeApprovalState(statetoRollback);
        }

        private void RestoreAdditionalDataForRollback()
        {
            var enm  = _additionalDataForRollBack.GetEnumerator();
            while (enm.MoveNext())
            {
                if (!_additionalData.ContainsKey(enm.Current.Key))
                    _additionalData.Add(enm.Current.Key,
                        enm.Current.Value);
                else
                    _additionalData[enm.Current.Key] = enm.Current.Value;
            }
        }
        
        private Dictionary<int,double> GetAllComponentValues()
        {
            var compValues = new Dictionary<int, double>(); ;
            try
            {
                clDal dal = new clDal();
                DataTable dt = new DataTable();
                dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger,
                    _approvalKey.Employee.EmployeeNumber, ParameterDir.pdInput);
                dal.AddParameter("p_taarich", ParameterType.ntOracleDate,
                    _approvalKey.WorkCard.WorkDate, ParameterDir.pdInput);
                dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null,
                    ParameterDir.pdOutput);
                dal.ExecuteSP("PKG_APPROVALS.get_erech_meafyen", ref dt);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        compValues.Add(Convert.ToInt32(dr["Kod_Rechiv"]),
                            Convert.ToDouble(dr["Erech_Rechiv"]));
                    }
                }
                
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                GenerateErrorLogMessage(ex.ToString());
            }
            return compValues;
        }

        private void UpdatePaymentHours()
        {
            if (_approvalKey.Approval.RequiresPaymentHoursUpdate)
            {
                clDal dal = new clDal();
                dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger,
                    _approvalKey.Employee.EmployeeNumber, ParameterDir.pdInput);
                dal.AddParameter("p_taarich", ParameterType.ntOracleDate,
                    _approvalKey.WorkCard.WorkDate, ParameterDir.pdInput);
                dal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger,
                    _approvalKey.WorkCard.SidurNumber, ParameterDir.pdInput);
                dal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate,
                    _approvalKey.WorkCard.SidurStart, ParameterDir.pdInput);
                dal.ExecuteSP("PKG_APPROVALS.update_shat_letashlum");
            }
        }
        
        private bool IsRequestAlreadyExists(out ApprovalRequest requestToUpdate)
        {
            bool exists = false;
            requestToUpdate = null;
            if (!Approval.IsForwardToAnotherFactor)
            {
                var similarRequests = GetSimilarApprovalRequests(_approvalKey);
                foreach(ApprovalRequest similarRequest in similarRequests)
                {
                    bool areRequestValuesEqual =
                        _approvalKey.RequestValues.IsEqual(similarRequest.RequestValues);
                    exists = (similarRequest.HasApprovalProcessStarted || areRequestValuesEqual)
                        && Approval.Level <= similarRequest.Approval.Level;
                    if (requestToUpdate == null && 
                        similarRequest.Approval.Level == Approval.FIRST_APPROVAL_LEVEL &&
                        similarRequest.HasApprovalProcessStarted && !areRequestValuesEqual)
                        requestToUpdate = similarRequest;
                }
            }
            return exists;
        }

        private ApprovalRequestState RegisterRequest(ApprovalRequestState initialRequestState)
        {
            //If the approval code is not active and this is a new request
            //don't allow to add it
            if (!_approvalKey.Approval.IsActiveForInsert) 
                return ApprovalRequestState.NotActive;

            bool success = AddApprovalRequestToDB(initialRequestState);
            if (success)
            {
                SuspendPayment(SuspendPaymentReason.Pending);
                if (!_factors[0].EmployeeNumber.HasValue)
                    RaiseInvalidValueFromHR();
                return initialRequestState;
            }
            else return ApprovalRequestState.Failed;
        }

        private ApprovalRequestState UpdateRequestValues(ApprovalRequest originalRequest)
        {
            var state = ApprovalRequestState.Pending;
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger,
                    originalRequest.Employee.EmployeeNumber, ParameterDir.pdInput);
                dal.AddParameter("p_kod_ishur", ParameterType.ntOracleInteger,
                    originalRequest.Approval.Code, ParameterDir.pdInput);
                dal.AddParameter("p_taarich", ParameterType.ntOracleDate,
                    originalRequest.WorkCard.WorkDate, ParameterDir.pdInput);
                dal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger,
                    originalRequest.WorkCard.SidurNumber, ParameterDir.pdInput);
                dal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate,
                    originalRequest.WorkCard.SidurStart, ParameterDir.pdInput);
                dal.AddParameter("p_shat_yetzia", ParameterType.ntOracleDate,
                    originalRequest.WorkCard.ActivityStart, ParameterDir.pdInput);
                dal.AddParameter("p_mispar_knisa", ParameterType.ntOracleInteger,
                    originalRequest.WorkCard.ActivityNumber, ParameterDir.pdInput);
                dal.AddParameter("p_rama", ParameterType.ntOracleInteger,
                    originalRequest.Approval.Level, ParameterDir.pdInput);
                dal.AddParameter("p_erech_mevukash", ParameterType.ntOracleDecimal,
                    originalRequest.RequestValues[RequestValues.FIRST_REQUEST_VALUE_KEY],
                    ParameterDir.pdInput);
                dal.AddParameter("p_erech_mevukash2", ParameterType.ntOracleDecimal,
                    originalRequest.RequestValues[RequestValues.SECOND_REQUEST_VALUE_KEY],
                    ParameterDir.pdInput);
                dal.AddParameter("p_new_erech_mevukash", ParameterType.ntOracleDecimal,
                    _approvalKey.RequestValues[RequestValues.FIRST_REQUEST_VALUE_KEY],
                    ParameterDir.pdInput);
                dal.AddParameter("p_new_erech_mevukash2", ParameterType.ntOracleDecimal,
                    _approvalKey.RequestValues[RequestValues.SECOND_REQUEST_VALUE_KEY],
                    ParameterDir.pdInput);
                dal.AddParameter("p_rows_affected", ParameterType.ntOracleInteger,
                    null, ParameterDir.pdOutput);
                dal.ExecuteSP("PKG_APPROVALS.update_erech_mevukash");
            }
            catch (Exception ex)
            {
                state = ApprovalRequestState.Failed;
                _errorMessage = ex.Message;
                GenerateErrorLogMessage(ex.ToString());
            }
            return state;
        }

        /// <summary>
        /// Gets Approval Factors for this request
        /// </summary>
        internal void GetApprovalFactorsFromHR()
        {
            GetApprovalFactorsFromHR(int.Parse(_approvalKey.Employee.EmployeeNumber));
        }
        internal void GetApprovalFactorsFromHR(int empNumber)
        {
            _factors = new ApprovalFactor[2] { new ApprovalFactor(), 
                new ApprovalFactor() };
           
            if (_approvalKey.Approval.AccessTypeToHR == AccessTypeToHR.Simple)
                FillFactorsFromView(empNumber);
            else FillFactorsFromSP(empNumber);
            
            
        }

        private void RaiseInvalidValueFromHR()
        {
            string errorMess = String.Format("Null value from HR for: {0}",
                DetailsFromInvalidValueFromHR);
            if (InvalidValueFromHR != null)
                InvalidValueFromHR(this, new ApprovalRequestEventArgs(_approvalKey,
                    errorMess, ApprovalExceptionType.InvalidValueFromHR));
            
        }

        private void ApprovalRequest_InvalidValueFromHR(object sender, ApprovalRequestEventArgs e)
        {
            if (!IsFromBatchProcess && e.ExceptionType == ApprovalExceptionType.InvalidValueFromHR)
            {
                NotificationSender notify = new NotificationSender();
                InvalidHrValuesMail mail = new InvalidHrValuesMail(_approvalKey, e.Message);
                notify.SendNotification(mail);
            }

        }
        

        private void ReadEmployeeDetails(int empNumber)
        {
            clDal dal = new clDal();
            DataTable dt = new DataTable();
            dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger,
               empNumber, ParameterDir.pdInput);
            dal.AddParameter("p_taarich", ParameterType.ntOracleDate,
                _approvalKey.WorkCard.WorkDate, ParameterDir.pdInput);
            dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null,
                ParameterDir.pdOutput);
            dal.ExecuteSP("PKG_APPROVALS.get_employee_details", ref dt);
            foreach (DataRow dr in dt.Rows)
            {
                if (!String.IsNullOrEmpty(dr["Kod_Natun"].ToString()))
                {
                    EmployeeDetailsCodes empCode = (EmployeeDetailsCodes)
                        int.Parse(dr["Kod_Natun"].ToString());
                    if (!_approvalKey.Employee.EmployeeDetails.ContainsKey(empCode))
                        _approvalKey.Employee.EmployeeDetails.Add(empCode,
                            dr["Erech"]);
                    else _approvalKey.Employee.EmployeeDetails[empCode] = dr["Erech"];
                }
                if (!_approvalKey.Employee.EmployeeDetails.ContainsKey(
                    EmployeeDetailsCodes.CompanyCode))
                    _approvalKey.Employee.EmployeeDetails.Add(EmployeeDetailsCodes.CompanyCode,
                       dr["Kod_Hevra"]);
            }
        }

        private void FillFactorsFromSP(int empNumber)
        {
            bool valueIsEqual = false;
            ReadEmployeeDetails(empNumber);
            _hrInParams = GetInputParamValuesByAccessTypeToHR(out valueIsEqual);
            //if location values are equal for activity and job is marked to 
            //use Simple access type to HR then get approving factors from View
            if (valueIsEqual && Approval.UseSimpleAccessIfNoChanges)
                FillFactorsFromView(empNumber);
            else
            {
                clDal dal = new clDal();
                AddOutParametersForHR(dal);
                AddInParameters(dal, _hrInParams);
                dal.ExecuteSP("Egd_Kdam_Sachar_Ishurim.Meashrim@KDS_GW");
                string outNumber = dal.GetValParam("l_employee");
                if (!String.IsNullOrEmpty(outNumber) && !outNumber.Equals("null"))
                    _factors[0].EmployeeNumber =
                        int.Parse(outNumber);
            }
        }

        private void SetParameterValue(Dictionary<string, object> inParams,
            string paramName, EmployeeDetailsCodes detailCode, out bool valueIsEqual)
        {
            valueIsEqual = false;
            // check if the value in EmployeeDetails equals value where the Activity
            // was preformed. if it equals pass EmployeeNumber, else
            // pass the value where the activity was performed
            if (_approvalKey.Employee.EmployeeDetails.ContainsKey(
               EmployeeDetailsCodes.ActivityRegion))
            {
                var locationVal = GetActivityLocationValue(detailCode);
                if (locationVal != null &&
                    !_approvalKey.Employee.EmployeeDetails[detailCode].ToString().Equals(locationVal.ToString()))
                    inParams[paramName] = locationVal;
                else valueIsEqual = true;
            }
            else valueIsEqual = true;
            if(valueIsEqual)
                inParams["p_employee"] = _approvalKey.Employee.EmployeeNumber;
        }

        private object GetActivityLocationValue(EmployeeDetailsCodes detailCode)
        {
            object locationValue = null;
            DataTable dt = GetSnifTnua(false);
            if (dt != null && dt.Rows.Count > 0)
            {
                string colname = null;
                switch (detailCode)
                {
                    case EmployeeDetailsCodes.ActivityRegion:
                        colname = "ezor";
                        break;
                    case EmployeeDetailsCodes.ActivityBranch:
                        colname = "snif_tnua"; //actualy the value that is in the DataTable is kod_snif_av
                                               //but for compatibility it is given alias snif_tnua
                        break;
                    case EmployeeDetailsCodes.ActivityUnit:
                        colname = "snif_tnua"; //actualy the value that is in the DataTable is kod_snif_av
                                               //but for compatibility it is given alias snif_tnua
                        break;
                }
                if (!String.IsNullOrEmpty(colname))
                    locationValue = dt.Rows[0][colname];
            }
            return locationValue;
        }

        private DataTable GetSnifTnua(bool forSubCompanies)
        {
            string spName = forSubCompanies ? "PKG_APPROVALS.get_snif_tnua_hevrot_lelo_ish" :
                "PKG_APPROVALS.get_snif_tnua_and_ezor";
            clDal dal = new clDal();
            DataTable dt = new DataTable();
            dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger,
                _approvalKey.Employee.EmployeeNumber, ParameterDir.pdInput);
            dal.AddParameter("p_taarich", ParameterType.ntOracleDate,
                _approvalKey.WorkCard.WorkDate, ParameterDir.pdInput);
            dal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger,
                _approvalKey.WorkCard.SidurNumber, ParameterDir.pdInput);
            dal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate,
                _approvalKey.WorkCard.SidurStart, ParameterDir.pdInput);
            dal.AddParameter("p_shat_yetzia", ParameterType.ntOracleDate,
                _approvalKey.WorkCard.ActivityStart, ParameterDir.pdInput);
            dal.AddParameter("p_mispar_knisa", ParameterType.ntOracleInteger,
               _approvalKey.WorkCard.ActivityNumber, ParameterDir.pdInput);
            dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor,
                null, ParameterDir.pdOutput);
            dal.ExecuteSP(spName, ref dt);
            return dt;
        }

        private Dictionary<string, object> GetInputParamValuesByAccessTypeToHR(out bool valueIsEqual)
        {
            valueIsEqual = false;
            Dictionary<string, object> inParams = 
                new Dictionary<string, object>();
            if (_approvalKey.Employee.EmployeeDetails.ContainsKey(EmployeeDetailsCodes.CompanyCode))
                inParams.Add("p_bgi",
                    _approvalKey.Employee.EmployeeDetails[EmployeeDetailsCodes.CompanyCode]);
            else inParams.Add("p_bgi", null);
            inParams.Add("p_employee", null);
            inParams.Add("p_ezor", null);
            inParams.Add("p_org_id", null);
            inParams.Add("p_snif_av", null);
            
            switch (_approvalKey.Approval.AccessTypeToHR)
            {
                case AccessTypeToHR.ByNumberOrRegion:
                    SetParameterValue(inParams, "p_ezor",
                        EmployeeDetailsCodes.ActivityRegion, out valueIsEqual);
                    break;
                case AccessTypeToHR.ByNumberOrBranch:
                case AccessTypeToHR.ByNumberOrBranchExt:
                    SetParameterValue(inParams, "p_snif_av",
                       EmployeeDetailsCodes.ActivityBranch, out valueIsEqual);
                    break;
                case AccessTypeToHR.WithoutParameters:
                    break;
                case AccessTypeToHR.OrganizationUnit:
                    SetParameterValue(inParams, "p_org_id",
                        EmployeeDetailsCodes.ActivityUnit, out valueIsEqual);
                    break;
            }
            return inParams;
        }

        private void AddInParameters(clDal dal, Dictionary<string,object> inParams)
        {
            dal.AddParameter("p_kod_measher", ParameterType.ntOracleInteger,
                _approvalKey.Approval.JobCode, ParameterDir.pdInput);
            dal.AddParameter("p_date", ParameterType.ntOracleDate,
                _approvalKey.WorkCard.WorkDate, ParameterDir.pdInput);
            dal.AddParameter("p_bgi", ParameterType.ntOracleInteger,
                inParams["p_bgi"], ParameterDir.pdInput);
            dal.AddParameter("p_employee", ParameterType.ntOracleVarchar,
                inParams["p_employee"], ParameterDir.pdInput);
            dal.AddParameter("p_ezor", ParameterType.ntOracleInteger,
                inParams["p_ezor"], ParameterDir.pdInput);
            dal.AddParameter("p_org_id", ParameterType.ntOracleInteger,
                inParams["p_org_id"], ParameterDir.pdInput);
            dal.AddParameter("p_snif_av", ParameterType.ntOracleInteger,
                inParams["p_snif_av"], ParameterDir.pdInput);
        }

        private void AddOutParametersForHR(clDal dal)
        {
            dal.AddParameter("retcode", ParameterType.ntOracleInteger, null,
                ParameterDir.pdOutput, 100);
            dal.AddParameter("l_message", ParameterType.ntOracleVarchar, null,
               ParameterDir.pdOutput, 100);
            dal.AddParameter("l_employee", ParameterType.ntOracleVarchar, null,
               ParameterDir.pdOutput, 100);
        }

        private void FillFactorsFromView(int empNumber)
        {
            clDal dal = new clDal();
            DataTable dt = new DataTable();
            dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger,
                empNumber, ParameterDir.pdInput);
            dal.AddParameter("p_taarich", ParameterType.ntOracleDate,
                _approvalKey.WorkCard.WorkDate, ParameterDir.pdInput);
            dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null,
                ParameterDir.pdOutput);
            dal.ExecuteSP("PKG_APPROVALS.get_factors_from_meashrim", ref dt);

            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["menahel_yashir_rashi"]!=DBNull.Value)
                    _factors[0].EmployeeNumber = 
                        int.Parse(dt.Rows[0]["menahel_yashir_rashi"].ToString());
                _factors[0].IsPrimary = true;
                if (dt.Rows[0]["menahel_yashir_mishni"] != DBNull.Value)
                    _factors[1].EmployeeNumber = 
                        int.Parse(dt.Rows[0]["menahel_yashir_mishni"].ToString());
            }
        }

        private bool AddApprovalRequestToDB(ApprovalRequestState initialRequestState)
        {
            clDal dal = new clDal();
            bool success = false;
            dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger,
                _approvalKey.Employee.EmployeeNumber, ParameterDir.pdInput);
            dal.AddParameter("p_kod_ishur", ParameterType.ntOracleInteger,
                _approvalKey.Approval.Code, ParameterDir.pdInput);
            dal.AddParameter("p_taarich", ParameterType.ntOracleDate,
                _approvalKey.WorkCard.WorkDate, ParameterDir.pdInput);
            dal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger,
                _approvalKey.WorkCard.SidurNumber, ParameterDir.pdInput);
            dal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate,
                _approvalKey.WorkCard.SidurStart, ParameterDir.pdInput);
            dal.AddParameter("p_shat_yetzia", ParameterType.ntOracleDate,
                _approvalKey.WorkCard.ActivityStart, ParameterDir.pdInput);
            dal.AddParameter("p_mispar_knisa", ParameterType.ntOracleInteger,
                _approvalKey.WorkCard.ActivityNumber, ParameterDir.pdInput);
            dal.AddParameter("p_measher_rashi", ParameterType.ntOracleInteger,
                _factors[0].EmployeeNumber, ParameterDir.pdInput);
            dal.AddParameter("p_measher_mishni", ParameterType.ntOracleInteger,
                _factors[1].EmployeeNumber, ParameterDir.pdInput);
            dal.AddParameter("p_status", ParameterType.ntOracleInteger,
                (int)initialRequestState, ParameterDir.pdInput);
            dal.AddParameter("p_rama", ParameterType.ntOracleInteger,
                _approvalKey.Approval.Level, ParameterDir.pdInput);
            dal.AddParameter("p_erech_mevukash", ParameterType.ntOracleDecimal,
                _approvalKey.RequestValues[RequestValues.FIRST_REQUEST_VALUE_KEY], 
                ParameterDir.pdInput);
            dal.AddParameter("p_erech_mevukash2", ParameterType.ntOracleDecimal,
                _approvalKey.RequestValues[RequestValues.SECOND_REQUEST_VALUE_KEY], 
                ParameterDir.pdInput);
            dal.AddParameter("p_erech_meushar", ParameterType.ntOracleDecimal,
               _additionalData.ContainsKey("erech_meushar") ?
               _additionalData["erech_meushar"] : null,
                ParameterDir.pdInput);
            dal.AddParameter("p_siba", ParameterType.ntOracleVarchar,
                _additionalData.ContainsKey("siba") ?
                _additionalData["siba"] : null, 
                ParameterDir.pdInput);
            dal.AddParameter("p_heara", ParameterType.ntOracleVarchar,
                _additionalData.ContainsKey("heara") ?
                _additionalData["heara"] : null,
                ParameterDir.pdInput);
            if (Approval.IsForwardToAnotherFactor)
                dal.AddParameter("p_gorem_nosaf", ParameterType.ntOracleInteger,
                1, ParameterDir.pdInput);

            dal.ExecuteSP("PKG_APPROVALS.add_approval_request");
            success = true;
            return success;
        }

        /// <summary>
        /// Marks indication to suspend payment when Approval Request Workflow starts 
        /// </summary>
        internal void SuspendPayment(SuspendPaymentReason? reason)
        {
            if (_approvalKey.Approval.SuspendsPayment != 0 
                && !_approvalKey.Approval.IsForwardToAnotherFactor
                && !HasSupendingApprovals())
                UpdateSuspendPayment(true, reason);
        }

        /// <summary>
        /// Checks if the similar EmployeeNumber and WorkCard of this request
        /// has another suspend payment requests 
        /// (different ApprovalCode) without Status Approved
        /// </summary>
        /// <returns>True if open suspend payment requests exist, otherwise False</returns>
        private bool HasSupendingApprovals()
        {
            ApprovalRequest[] matchingApprovals = GetMatchingApprovalRequestsWithStatuses(
                int.Parse(_approvalKey.Employee.EmployeeNumber), _approvalKey.WorkCard.WorkDate);
            var suspendApp = from ap in matchingApprovals
                             where ap.Employee.IsEqual(_approvalKey.Employee)
                             && ap.WorkCard.IsEqual(_approvalKey.WorkCard)
                             && !ap.Approval.IsEqual(_approvalKey.Approval)
                             && ap.Approval.SuspendsPayment != 0
                             && ap.State != ApprovalRequestState.Approved
                             select ap.Approval.Code;
            return suspendApp.ToArray().Length > 0;

        }

        /// <summary>
        /// Marks indication to release payment when Approval Request Workflow finish 
        /// </summary>
        internal void ReleasePayment()
        {
            if (_approvalKey.Approval.SuspendsPayment != 0)
                UpdateSuspendPayment(false, null);
        }

        internal void UpdateDeviation()
        {
            UpdateDeviation(0);
        }
        internal void UpdateDeviation(object value)
        {
            if (_approvalKey.Approval.NeedsDeviationUpdating)
            {
                clDal dal = new clDal();
                dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger,
                    _approvalKey.Employee.EmployeeNumber, ParameterDir.pdInput);
                dal.AddParameter("p_taarich", ParameterType.ntOracleDate,
                    _approvalKey.WorkCard.WorkDate, ParameterDir.pdInput);
                dal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger,
                    _approvalKey.WorkCard.SidurNumber, ParameterDir.pdInput);
                dal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate,
                    _approvalKey.WorkCard.SidurStart, ParameterDir.pdInput);
                dal.AddParameter("p_value", ParameterType.ntOracleInteger,
                    value, ParameterDir.pdInput);
                dal.ExecuteSP("PKG_APPROVALS.update_chariga");
            }
        }
        private void UpdateSuspendPayment(bool isSupsends, 
            SuspendPaymentReason? reason)
        {
            clDal dal = new clDal();
            dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger,
                _approvalKey.Employee.EmployeeNumber, ParameterDir.pdInput);
            dal.AddParameter("p_taarich", ParameterType.ntOracleDate,
                _approvalKey.WorkCard.WorkDate, ParameterDir.pdInput);
            dal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger,
                _approvalKey.WorkCard.SidurNumber, ParameterDir.pdInput);
            dal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate,
                _approvalKey.WorkCard.SidurStart, ParameterDir.pdInput);
            dal.AddParameter("p_value", ParameterType.ntOracleInteger,
                isSupsends ? 1 : 0, ParameterDir.pdInput);
            if (isSupsends && reason.HasValue)
                dal.AddParameter("p_kod_siba", ParameterType.ntOracleInteger,
                    (int)reason.Value, ParameterDir.pdInput);
            dal.ExecuteSP("PKG_APPROVALS.update_lo_letashlum");
            
        }

        private void InitializeExisitngRequest()
        {
            DataTable dt = GetApprovalRequestDetailsFromDB();
            if (dt.Rows.Count > 0)
            {
                _state = (ApprovalRequestState)
                    int.Parse(dt.Rows[0]["KOD_STATUS_ISHUR"].ToString());
                StoreAdditionalData("erech_meushar", dt.Rows[0]["erech_meushar"],
                    "heara", dt.Rows[0]["heara"],
                    "siba", dt.Rows[0]["siba"]);

            }
        }

        private DataTable GetApprovalRequestDetailsFromDB()
        {
            clDal dal = new clDal();
            DataTable dt = new DataTable();
            dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger,
                _approvalKey.Employee.EmployeeNumber, ParameterDir.pdInput);
            dal.AddParameter("p_kod_ishur", ParameterType.ntOracleInteger,
                _approvalKey.Approval.Code, ParameterDir.pdInput);
            dal.AddParameter("p_taarich", ParameterType.ntOracleDate,
                _approvalKey.WorkCard.WorkDate, ParameterDir.pdInput);
            dal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger,
                _approvalKey.WorkCard.SidurNumber, ParameterDir.pdInput);
            dal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate,
                _approvalKey.WorkCard.SidurStart, ParameterDir.pdInput);
            dal.AddParameter("p_shat_yetzia", ParameterType.ntOracleDate,
                _approvalKey.WorkCard.ActivityStart, ParameterDir.pdInput);
            dal.AddParameter("p_mispar_knisa", ParameterType.ntOracleInteger,
               _approvalKey.WorkCard.ActivityNumber, ParameterDir.pdInput);
            dal.AddParameter("p_rama", ParameterType.ntOracleInteger,
               _approvalKey.Approval.Level, ParameterDir.pdInput);
            dal.AddParameter("p_erech_mevukash", ParameterType.ntOracleDecimal,
                _approvalKey.RequestValues[RequestValues.FIRST_REQUEST_VALUE_KEY],
                ParameterDir.pdInput);
            dal.AddParameter("p_erech_mevukash2", ParameterType.ntOracleDecimal,
                _approvalKey.RequestValues[RequestValues.SECOND_REQUEST_VALUE_KEY],
                ParameterDir.pdInput);
            dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, 
                null, ParameterDir.pdOutput);
            dal.ExecuteSP("PKG_APPROVALS.get_approval_request", ref dt);
            return dt;
        }

        /// <summary>
        /// Saves Another Factor and Job Code details
        /// </summary>
        /// <param name="jobCode">Job Code of the Another Factor</param>
        /// <param name="approvalFactor">Another Factor</param>
        internal void SaveFactorNumberAndJobCode(int jobCode, ApprovalFactor approvalFactor)
        {
            clDal dal = new clDal();
            dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger,
                _approvalKey.Employee.EmployeeNumber, ParameterDir.pdInput);
            dal.AddParameter("p_kod_ishur", ParameterType.ntOracleInteger,
                _approvalKey.Approval.Code, ParameterDir.pdInput);
            dal.AddParameter("p_taarich", ParameterType.ntOracleDate,
                _approvalKey.WorkCard.WorkDate, ParameterDir.pdInput);
            dal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger,
                _approvalKey.WorkCard.SidurNumber, ParameterDir.pdInput);
            dal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate,
                _approvalKey.WorkCard.SidurStart, ParameterDir.pdInput);
            dal.AddParameter("p_shat_yetzia", ParameterType.ntOracleDate,
                _approvalKey.WorkCard.ActivityStart, ParameterDir.pdInput);
            dal.AddParameter("p_mispar_knisa", ParameterType.ntOracleInteger,
                _approvalKey.WorkCard.ActivityNumber, ParameterDir.pdInput);
            dal.AddParameter("p_rama", ParameterType.ntOracleInteger,
                _approvalKey.Approval.Level, ParameterDir.pdInput);
            dal.AddParameter("p_erech_mevukash", ParameterType.ntOracleDecimal,
                _approvalKey.RequestValues[RequestValues.FIRST_REQUEST_VALUE_KEY], 
                ParameterDir.pdInput);
            dal.AddParameter("p_erech_mevukash2", ParameterType.ntOracleDecimal,
                _approvalKey.RequestValues[RequestValues.SECOND_REQUEST_VALUE_KEY], 
                ParameterDir.pdInput);
            dal.AddParameter("p_kod_status_ishur", ParameterType.ntOracleInteger,
               (int)ApprovalRequestState.TransferToAnother, ParameterDir.pdInput);
            dal.AddParameter("p_kod_tafkid_nosaf", ParameterType.ntOracleInteger,
                jobCode, ParameterDir.pdInput);
            dal.AddParameter("p_gorem_nosaf", ParameterType.ntOracleInteger,
                approvalFactor.EmployeeNumber, ParameterDir.pdInput);
            dal.AddParameter("p_meadken_acharon", ParameterType.ntOracleInteger,
                KdsLibrary.Security.LoginUser.GetLoginUser().UserInfo.EmployeeNumber, 
                ParameterDir.pdInput);

            dal.ExecuteSP("PKG_APPROVALS.set_approval_forward_data");
        }

        /// <summary>
        /// Gets Additional data value specified by the valueKey
        /// </summary>
        /// <param name="valueKey">key for value</param>
        /// <returns>value of key in Additional Data</returns>
        public object GetAdditionalDataValue(string valueKey)
        {
            if (_additionalData.ContainsKey(valueKey)) 
                return _additionalData[valueKey];
            else return null;
        }

        private void ConvertApprovalCodeForSubCompany()
        {
            if (_approvalKey.Approval.IsForSubCompany)
            {
                if (!_approvalKey.Approval.Code.ToString().EndsWith(
                    Approval.SUB_COMPANY_CODE_ADDITION.ToString())
                    && _approvalKey.Approval.Code.ToString().Length <=
                    Approval.SUB_COMPANY_CODE_ADDITION.ToString().Length + 1)
                    _approvalKey.Approval.Code = _approvalKey.Approval.CalculateSubCompanyCode();
            }
        }

        private bool IsFromSubCompany()
        {
            //check if employee belongs or borrowed to sub company
            bool result = false;
            DataTable dt = new DataTable();
            var dal = new clDal();
            dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, Employee.EmployeeNumber,
                ParameterDir.pdInput);
            dal.AddParameter("p_date", ParameterType.ntOracleDate, WorkCard.WorkDate,
                ParameterDir.pdInput);
            dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
            dal.ExecuteSP("pkg_approvals.get_hevrot_lelo_ishurim_leoved", ref dt);

            if (dt.Rows.Count > 0)
            {
                result = dt.Rows[0]["kod_hevra"] != null && dt.Rows[0]["kod_hevra"] != DBNull.Value;
            }
            return result;
        }

        private bool IsReinforceSubCompany()
        {
            //check if employee reinforcing sub company by checking if the approval code is signed as
            //reinforce type and the employee has sidur as reinforce
            bool forSubCompany = false;
            if (_approvalKey.Approval.SubCompanyApprovalType == SubCompanyApprovalType.ReinforceSubCompany)
            {
                DataTable dt = GetSnifTnua(true);
                if (dt != null) forSubCompany = dt.Rows.Count > 0;
            }
            return forSubCompany;
        }

        #endregion

        #region Properties
        public ApprovalRequestState State
        {
            get { return _state; }
        }
        public Employee Employee
        {
            get { return _approvalKey.Employee; }
        }
        public Approval Approval
        {
            get { return _approvalKey.Approval; }
        }
        public WorkCard WorkCard
        {
            get { return _approvalKey.WorkCard; }
        }
        public RequestValues RequestValues
        {
            get { return _approvalKey.RequestValues; }
        }
        public bool HasApprovalProcessStarted
        {
            get
            {
                //Indication that the approval process already started
                return _state == ApprovalRequestState.Pending;
            }
        }
        public string ErrorMessage
        {
            get { return _errorMessage; }
        }
        public ApprovalFactor MainFactor
        {
            get { return _factors[0]; }
        }
        public ApprovalFactor SecondaryFactor
        {
            get { return _factors[1]; }
        }
        public Dictionary<string, object> InputParamsForHR
        {
            get { return _hrInParams; }
        }
        public string DetailsForErrorMessage
        {
            get 
            {
                return String.Format("EmpID:{0} Date:{1} AppCode:{2} Sidur:{3} Activity:{4}",
                    _approvalKey.Employee.EmployeeNumber,
                    _approvalKey.WorkCard.WorkDate.ToShortDateString(),
                    _approvalKey.Approval.Code,
                    _approvalKey.WorkCard.SidurNumber,
                    _approvalKey.WorkCard.ActivityNumber);
            }
        }

        public string DetailsFromInvalidValueFromHR
        {
            get
            {
                return String.Format("{0} Job Code: {1}", DetailsForErrorMessage,
                    _approvalKey.Approval.JobCode);
            }
        }

        public bool IsFromBatchProcess { get; internal set; }
        #endregion


        
    }


    public class ApprovalRequestEventArgs : EventArgs
    {
        public ApprovalExceptionType ExceptionType { get; set; }
        public ApprovalKey ApprovalKey { get; set; }
        public string Message { get; set; }
        public ApprovalRequestEventArgs(ApprovalKey key, string message, ApprovalExceptionType type)
        {
            ApprovalKey = key;
            Message = message;
            ExceptionType = type;
        }
        public string LogType
        {
            get
            {
                switch(ExceptionType)
                {
                    case ApprovalExceptionType.InvalidValueFromHR:
                        return "W"; 
                    case ApprovalExceptionType.Error:
                        return "E";
                    default: return "E";
                }
            }
        }
        public bool HasRequestFailed
        {
            get
            {
                return ExceptionType == ApprovalExceptionType.Error;
            }
        }
    }
    /// <summary>
    /// State of Approval Request
    /// </summary>
    public enum ApprovalRequestState
    {
        NotActive = -300,
        NotAllowed = -200,
        Empty = -100,
        InvalidCode = -3,
        AlreadyExists = -2,
        Failed = -1,
        Pending = 0,
        Approved = 1,
        Declined = 2,
        TransferToAnother = 3
    }

    public enum SuspendPaymentReason
    {
        Pending = 1,
        Declined = 11
    }

    public enum ApprovalExceptionType
    {
        Error,
        InvalidValueFromHR
    }
}
