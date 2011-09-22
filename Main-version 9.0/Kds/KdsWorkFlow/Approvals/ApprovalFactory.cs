using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;
using KdsLibrary;

namespace KdsWorkFlow.Approvals
{
    //approvals 25,26,27,28,34,35,37 will be created manually from application
    public static class ApprovalFactory
    {
        private const string BATCH_DESCRIPTION = "ריצת אישורים";
        /// <summary>
        /// Opens new batch id, raise approval requests, close batch, send mails
        /// </summary>
        /// <param name="workDate">Work date</param>
        public static void ApprovalsEndOfDayProcess(DateTime workDate, bool sendMails)
        {
            //Write BatchID to TB_Bakashot
            long btchRequest = KdsLibrary.clGeneral.OpenBatchRequest(
                KdsLibrary.clGeneral.enGeneralBatchType.Approvals,
                BATCH_DESCRIPTION, -1);
            var status = RaiseAllWorkDayApprovalCodes(workDate, btchRequest);
            var postActions = new ApprovalProcessPostActions(btchRequest);
            postActions.RunPostActions();
            KdsLibrary.clGeneral.CloseBatchRequest(btchRequest, status);
            if (sendMails)
            {
                var mailsFactory = new MailsFactory(btchRequest);
                mailsFactory.SendApprovalMails();
            } 
        }
        
        /// <summary>
        /// Finds all needed new approval requests for Work Date and all relevant Employees 
        /// and registers them
        /// </summary>
        /// <param name="workDate">Work date</param>
        public static clGeneral.enBatchExecutionStatus RaiseAllWorkDayApprovalCodes(DateTime workDate,
            long btchRequest)
        {
            clGeneral.enBatchExecutionStatus status = clGeneral.enBatchExecutionStatus.Succeeded;           
            System.Reflection.Assembly asmb =
               System.Reflection.Assembly.GetExecutingAssembly();
            List<Type> types = new List<Type>();
            foreach (Type type in asmb.GetTypes())
            {
                if (!type.IsAbstract && type.IsSubclassOf(typeof(ApprovalPopulation)))
                {
                    types.Add(type);
                }
            }
            
            types.ForEach(delegate(Type typeOfApprovalPopulation)
            {
                var population = ApprovalPopulation.GetPopulationGroup(
                    typeOfApprovalPopulation, workDate, btchRequest);
                if (population != null)
                {
                    DataTable dt = population.GetPopulation();
                    if (dt != null)
                    {
                        LogPopulationStartInfo(population, dt, btchRequest);
                        foreach (DataRow dr in dt.Rows)
                        {
                            DateTime procWorkDate = Convert.ToDateTime(dr["taarich"]);
                            int employeeNum = Convert.ToInt32(dr["mispar_ishi"]);
                            var args = population.GetApprovalFactoryArgs();
                            args.BatchRequest = btchRequest;
                            clGeneral.LogBatchPopulation(employeeNum, procWorkDate, DateTime.Now.Date,
                                btchRequest, clGeneral.enGeneralBatchType.Approvals,
                                typeOfApprovalPopulation.Name);
                            if (!RaiseEmployeeWorkDayApprovalCodes(procWorkDate, employeeNum, args)) 
                                status = clGeneral.enBatchExecutionStatus.PartialyFinished;
                        }
                        LogPopulationEndInfo(population, btchRequest);
                    }
                }
            });
            return status;
        }

        private static void LogPopulationStartInfo(ApprovalPopulation population, DataTable data, 
            long btchRequest)
        {
            string message = String.Format("{0} Started. Processing {1} rows.", 
                population.GetType().Name, data.Rows.Count);
            LogErrorOfApprovalBatchPrc(btchRequest, "I", message);
        }

        private static void LogPopulationEndInfo(ApprovalPopulation population, long btchRequest)
        {
            string message = String.Format("{0} Finished.", population.GetType().Name);
            LogErrorOfApprovalBatchPrc(btchRequest, "I", message);
        }

        public static bool RaiseEmployeeWorkDayApprovalCodes(
           DateTime workDate, int employeeNumber, long btchRequest, bool isGarageEmployee)
        {
            var args = new ApprovalFactoryArgs();
            args.BatchRequest = btchRequest;
            args.CheckGarageManagerConfirmation = isGarageEmployee;

            return RaiseEmployeeWorkDayApprovalCodes(workDate, employeeNumber, args/*btchRequest, isGarageEmployee,
                false, false*/
                              );
        }

        /// <summary>
        /// Finds all needed new approval requests for Work Date and Employee Number and
        /// registers them
        /// </summary>
        /// <param name="workDate">Work date</param>
        /// <param name="employeeNumber">Employee Number</param>
        /// <param name="btchRequest">Number of Batch Execution</param>
        /// <param name="isGarageEmployee">Indication if the employee is marked as garage employee</param>
        /// <param name="generateAutoApproval">Indication if generate approvals with status Approved</param>
        /// <returns>true if all requests passed without exceptions, otherwise false</returns>
        public static bool RaiseEmployeeWorkDayApprovalCodes(DateTime workDate, int employeeNumber,
            ApprovalFactoryArgs args)
        {
            bool noFails = true;
            System.Reflection.Assembly asmb =
                System.Reflection.Assembly.GetExecutingAssembly();
            List<Type> types = new List<Type>();
            foreach (Type type in asmb.GetTypes())
            {
                if (!type.IsAbstract && type.IsSubclassOf(typeof(ApprovalSource)))
                {
                    types.Add(type);
                }
            }
            ApprovalSource lastSource = null;
            types.ForEach(delegate(Type typeOfApprovalSource)
            {
                ApprovalSource appSource =
                   Activator.CreateInstance(typeOfApprovalSource, workDate, 
                    employeeNumber, args.BatchRequest) as ApprovalSource;
                if (appSource != null)
                    appSource.CollectApprovals(args);
                if (appSource.HasFailedRequest) noFails = false;
                lastSource = appSource;
            });
            if (lastSource != null)
                lastSource.UpdateLastApprovalProcessDate();
            return noFails;
        }

        

        /// <summary>
        /// Wrapper method to log exception during the approval process
        /// </summary>
        /// <param name="btchRequest">ID of the batch</param>
        /// <param name="message">Error message</param>
        internal static void LogErrorOfApprovalBatchPrc(long btchRequest, string message)
        {
            LogErrorOfApprovalBatchPrc(btchRequest, "E", message);
        }
        /// <summary>
        /// Wrapper method to log exception during the approval process
        /// </summary>
        /// <param name="btchRequest">ID of the batch</param>
        /// <param name="msgType">E - error, W - warning, I - information</param>
        /// <param name="message">Error message</param>
        internal static void LogErrorOfApprovalBatchPrc(long btchRequest, string msgType, string message)
        {
            clLogBakashot.SetError(btchRequest, msgType, (int)clGeneral.enGeneralBatchType.Approvals,
                       message);
            clLogBakashot.InsertErrorToLog();
        }

        internal static void LogErrorOfApprovalBatchPrc(long btchRequest, string msgType, string message,
            int employeeNumber, DateTime workDate)
        {
            int? logEmpNumber = null;
            if (employeeNumber != 0) logEmpNumber = employeeNumber;
            clLogBakashot.SetError(btchRequest, logEmpNumber, msgType, (int)clGeneral.enGeneralBatchType.Approvals,
                       workDate, message);
            clLogBakashot.InsertErrorToLog();
        }

    }

    /// <summary>
    /// Abstract class that provides pattern to collect
    /// approvals codes for specific date and employee.
    /// If employeeNumber equals 0 , all employees will be checked 
    /// Types that inherits this class are executed for 
    /// collecting approval requests by ApprovalFactory
    /// </summary>
    public abstract class ApprovalSource
    {
        #region Fields
        protected DateTime _workDate;
        protected int _employeeNumber;
        protected long _btchRequest;
        #endregion

        #region Constractors
        public ApprovalSource(DateTime workDate, int employeeNumber,long btchRequest)
        {
            _workDate = workDate;
            _employeeNumber = employeeNumber;
            _btchRequest = btchRequest;
        } 
        #endregion

        #region Methods
        protected virtual void DefineApprovalCode(ApprovalKey approvalKey, DataRow dr)
        {
            int characteristic = Convert.ToInt32(dr["kod_meafyen"]);
            switch (characteristic)
            {
                case -1: //sidurim viza hofshit,sidur matala
                case 66:
                    //sidurim meyuchadim
                    break;
                case 52:
                    //sidurim meyuchadim sport
                    if (ChangeApprovalCode(approvalKey, 2, 9)) break;
                    //sidurim meyuchadim kaitana
                    if (ChangeApprovalCode(approvalKey, 4, 24)) break;
                    break;
                case 45:
                    //sidurim meyuchadim visa zvait
                    if (ChangeApprovalCode(approvalKey, 1, 31)) break;
                    break;
            }
        }

        protected virtual bool ChangeApprovalCode(ApprovalKey approvalKey, int oldCode, int newCode)
        {
            bool changed = false;
            if (approvalKey.Approval.Code == oldCode)
            {
                approvalKey.Approval.Code = newCode;
                approvalKey.Approval.Init();
                changed = true;
            }
            return changed;
        }

        protected virtual DataTable GetData()
        {
            try
            {
                clDal dal = new clDal();
                DataTable dt = new DataTable();
                dal.AddParameter("p_taarich", ParameterType.ntOracleDate,
                    _workDate, ParameterDir.pdInput);
                if (_employeeNumber != 0)
                    dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger,
                        _employeeNumber, ParameterDir.pdInput);
                else dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger,
                        null, ParameterDir.pdInput);
                dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor,
                    null, ParameterDir.pdOutput);
                dal.ExecuteSP(DataMethodName, ref dt);
                return dt;
            }
            catch (Exception ex) 
            {
                ApprovalFactory.LogErrorOfApprovalBatchPrc(_btchRequest, "E", ex.ToString(),
                    _employeeNumber, _workDate);
                HasFailedRequest = true;
                return null; 
            }
        }

        public virtual void CollectApprovals(ApprovalFactoryArgs args)
        {
            DataTable dt = GetData();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        var approvalKey =
                            ApprovalRequest.GetApprovalKeyFromDataRow(dr);
                        //if it as a batch run then for garage employee collect only approvals that are marked
                        // as HasGarageManagerConfirmation=true
                        if (args.BatchRequest > 0 && args.CheckGarageManagerConfirmation &&
                            !approvalKey.Approval.HasGarageManagerConfirmation) continue;
                        if (IsApprovalCodeDefintionNeeded)
                            DefineApprovalCode(approvalKey, dr);

                        var appRequest = ApprovalRequest.CreateApprovalRequest(
                            approvalKey.Employee.EmployeeNumber,
                            approvalKey.Approval.Code,
                            approvalKey.WorkCard, approvalKey.RequestValues,
                            false, null);
                        appRequest.IsFromBatchProcess = true;
                        appRequest.ApprovalRequestException += new ApprovalRequestEventHandler(
                            ApprovalRequest_ApprovalRequestException);
                        appRequest.InvalidValueFromHR += new ApprovalRequestEventHandler(
                            ApprovalRequest_ApprovalRequestException);

                        
                        
                        //if the approval code indicates that it can be approved
                        //by garage manager and the garage manager approved
                        //and the employee from population that marked as allowed to auto confirm
                        //and the request is raised by batch process 
                        //then the approval is already confirmed
                        bool managerConfirmed = Convert.ToInt32(dr["musach"]) != 0;
                        if (approvalKey.Approval.HasGarageManagerConfirmation
                            && managerConfirmed && args.GenerateAutoApproval
                            && args.BatchRequest > 0)
                            appRequest.ProcessRequest(ApprovalRequestState.Approved);
                        else
                            appRequest.ProcessRequest();
                        
                    }
                    catch (Exception ex)
                    {
                        ApprovalFactory.LogErrorOfApprovalBatchPrc(_btchRequest, ex.ToString());
                        HasFailedRequest = true;
                    }
                }
            }
        }

        void ApprovalRequest_ApprovalRequestException(object sender, ApprovalRequestEventArgs e)
        {
            ApprovalFactory.LogErrorOfApprovalBatchPrc(_btchRequest, e.LogType, e.Message);
            HasFailedRequest = e.HasRequestFailed;
        }

        internal void UpdateLastApprovalProcessDate()
        {
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger,
                    _employeeNumber, ParameterDir.pdInput);
                dal.AddParameter("p_taarich", ParameterType.ntOracleDate,
                    _workDate, ParameterDir.pdInput);
                dal.ExecuteSP("PKG_APPROVALS.update_ritzat_ishurim_acharona");
            }
            catch (Exception ex)
            {
                ApprovalFactory.LogErrorOfApprovalBatchPrc(_btchRequest, ex.ToString());
                HasFailedRequest = true;
            }
        }

        #endregion

        #region Properties
        protected abstract string DataMethodName { get; }

        protected virtual bool IsApprovalCodeDefintionNeeded
        {
            get { return false; }
        }
        public bool HasFailedRequest { get; set; }
        #endregion
    }

    /// <summary>
    /// Find all needed approval requests for Sidurim Meuhadim
    /// Approval Codes: 7,8,9,11,12,13,14,16,17,18,19,20,22,
    ///                 23,24,29,30,31,40,48
    /// </summary>
    public class SidurimMeuhadimApprovals : ApprovalSource
    {
        public SidurimMeuhadimApprovals(DateTime workDate, int employeeNumber, long btchRequest)
            : base(workDate, employeeNumber, btchRequest)
        {
        }

        protected override string DataMethodName
        {
            get { return "PKG_APPROVALS.get_sidur_meuhad_approvals"; }
        }

        protected override bool IsApprovalCodeDefintionNeeded
        {
            get
            {
                return true;
            }
        }
    }

    /// <summary>
    /// Find all needed approval requests for Sidur Matala
    /// Approval Codes: 15
    /// </summary>
    public class SidurMatalaApprovals : ApprovalSource
    {
        public SidurMatalaApprovals(DateTime workDate, int employeeNumber, long btchRequest)
            : base(workDate, employeeNumber, btchRequest)
        {
        }

        protected override string DataMethodName
        {
            get { return "PKG_APPROVALS.get_sidur_matala_approvals"; }
        }
    }

    /// <summary>
    /// Find all needed approval requests for Missing Clock 
    /// Approval Codes: 1,101,102,2,3,301,302,4,36
    /// </summary>
    public class ClockMissingApprovals : ApprovalSource
    {
        public ClockMissingApprovals(DateTime workDate, int employeeNumber, long btchRequest)
            : base(workDate, employeeNumber, btchRequest)
        {
        }

        protected override string DataMethodName
        {
            get { return "PKG_APPROVALS.get_harigot_shaon"; }
        }
    }

    /// <summary>
    /// Find all needed approval requests for Sidur Tafkid 
    /// Approval Codes: 10
    /// </summary>
    public class SidurTafkidApprovals : ApprovalSource
    {
        public SidurTafkidApprovals(DateTime workDate, int employeeNumber, long btchRequest)
            : base(workDate, employeeNumber, btchRequest)
        {
        }

        protected override string DataMethodName
        {
            get { return "PKG_APPROVALS.get_nahag_sidur_tafkid"; }
        }
    }

    /// <summary>
    /// Find all needed approval requests for Hashlamat shaot/yom
    /// Approval Codes: 32,38,39
    /// </summary>
    public class HashlamaApprovals : ApprovalSource
    {
        public HashlamaApprovals(DateTime workDate, int employeeNumber, long btchRequest)
            : base(workDate, employeeNumber, btchRequest)
        {
        }

        protected override string DataMethodName
        {
            get { return "PKG_APPROVALS.get_hashlama_approvals"; }
        }
    }

    /// <summary>
    /// Find all needed approval requests for Toranut Mosach Shishi/Shabat 
    /// Approval Codes: 6
    /// </summary>
    public class MosachShishiShabatApprovals : ApprovalSource
    {
        public MosachShishiShabatApprovals(DateTime workDate, int employeeNumber, long btchRequest)
            : base(workDate, employeeNumber, btchRequest)
        {
        }

        protected override string DataMethodName
        {
            get { return "PKG_APPROVALS.get_mosach_shabaton_approvals"; }
        }
    }

    /// <summary>
    /// Find all needed approval requests for Shaot Avoda Shabat 
    /// without clearence (meafyen 7)
    /// Approval Codes: 5
    /// </summary>
    public class ShaotAvodaShabatApprovals : ApprovalSource
    {
        public ShaotAvodaShabatApprovals(DateTime workDate, int employeeNumber, long btchRequest)
            : base(workDate, employeeNumber, btchRequest)
        {
        }

        protected override string DataMethodName
        {
            get { return "PKG_APPROVALS.get_shaot_avoda_shabat"; }
        }
    }

    /// <summary>
    /// Find all needed approval requests for Hamtana
    /// Approval Codes: 33
    /// </summary>
    public class HamtanaApprovals : ApprovalSource
    {
        public HamtanaApprovals(DateTime workDate, int employeeNumber, long btchRequest)
            : base(workDate, employeeNumber, btchRequest)
        {
        }

        protected override string DataMethodName
        {
            get { return "PKG_APPROVALS.get_hamtana_approvals"; }
        }
    }

    /// <summary>
    /// Arguments for Executing Approval Batch for a specific population
    /// </summary>
    public class ApprovalFactoryArgs
    {
        public long BatchRequest { get; set; }
        public bool CheckGarageManagerConfirmation { get; set; }
        public bool GenerateAutoApproval { get; set; }
    }
}
