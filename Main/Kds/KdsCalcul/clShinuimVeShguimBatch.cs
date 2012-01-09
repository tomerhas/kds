﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsBatch;
using KdsLibrary;
using KdsLibrary.BL;
using KdsLibrary.DAL;
using System.Threading.Tasks;
using System.Data;

namespace KdsCalcul
{
    public static class clShinuimVeShguimBatch
    {
        public static void ExecuteInputDataAndErrors(clGeneral.BatchRequestSource requestSource,
          clGeneral.BatchExecutionType execType, DateTime workDate, long btchRequest, int iNumProcess)
        {
            ExecuteInputDataAndErrors(requestSource, execType, workDate, btchRequest, false, iNumProcess);
        }
        public static void ExecuteInputDataAndErrors(clGeneral.BatchRequestSource requestSource,
            clGeneral.BatchExecutionType execType, DateTime workDate, long btchRequest, bool logPopulationOnly, int iNumProcess)
        {
            clBatchProcess btchProc = GetBatchProcess(requestSource, execType, workDate, btchRequest);
            if (btchProc != null)
            {
                if (btchProc.LoadData(iNumProcess, btchRequest))
                {
                    var btchStatus = btchProc.Execute(logPopulationOnly);
                    clGeneral.CloseBatchRequest(btchRequest, btchStatus);
                }
                else if (btchProc.IsEmptyData)
                {
                    clLogBakashot.SetError(btchRequest, "W", (int)requestSource,
                       String.Format("Empty data for batch {0}", requestSource.ToString()));
                    clLogBakashot.InsertErrorToLog();
                    clGeneral.CloseBatchRequest(btchRequest,
                        clGeneral.enBatchExecutionStatus.Failed);
                }
                else
                {
                    clLogBakashot.SetError(btchRequest, "E", (int)requestSource,
                       btchProc.ErrorMessage);
                    clLogBakashot.InsertErrorToLog();
                    clGeneral.CloseBatchRequest(btchRequest,
                        clGeneral.enBatchExecutionStatus.Failed);
                }
            }
        }

        private static clBatchProcess GetBatchProcess(clGeneral.BatchRequestSource requestSource,
            clGeneral.BatchExecutionType execType, DateTime workDate, long btchRequest)
        {
            switch (requestSource)
            {
                case clGeneral.BatchRequestSource.ImportProcess:
                    return new clBatchProcessFromInput(requestSource, execType, workDate, btchRequest);
                //case BatchRequestSource.ErrorExecutionFromUI:
                //    return new clBatchProcessFromUI(requestSource, execType, btchRequest);
                case clGeneral.BatchRequestSource.ImportProcessForChangesInHR:
                    return new clBatchProcessFromInputForChangesInHR(requestSource, execType, workDate,
                        btchRequest);
                case clGeneral.BatchRequestSource.ImportProcessForPremiot:
                    return new clBatchProcessFromInputForPremiot(requestSource, execType, workDate,
                        btchRequest);
                default: return null;
            }


        }

    }


    public abstract class clBatchProcess
    {
        #region Fields
        private string _errorMessage;
        private DataTable _dtParameters;
        protected clGeneral.BatchExecutionType _executionType;
        protected int _userID;
        protected DataTable _data;
        protected clGeneral.BatchRequestSource _batchSource;
        protected long _btchRequest;
        #endregion

        #region Constractor
        public clBatchProcess(clGeneral.BatchRequestSource batchSource, clGeneral.BatchExecutionType execType, long btchRequest)
        {
            _executionType = execType;
            _batchSource = batchSource;
            _btchRequest = btchRequest;

            Init();
        }
        #endregion

        #region Methods
        private void Init()
        {
            _userID = KdsLibrary.clGeneral.GetEmployeeNumberByContext();
            _dtParameters = GetParametersTable();
        }

        private DataTable GetParametersTable()
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetParameters, ref dt);
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_btchRequest, "E", (int)_batchSource,
                         ex.ToString());
                clLogBakashot.InsertErrorToLog();
            }
            return dt;
        }

        protected abstract DataTable GetData(out string errorMessage, int iNumProcess, long btchRequest);

        public clGeneral.enBatchExecutionStatus Execute(bool logPopulationOnly)
        {
            var status = clGeneral.enBatchExecutionStatus.Failed;
            if (_data != null)
            {
                //LogPopulation();
                if (logPopulationOnly) return clGeneral.enBatchExecutionStatus.PartialyFinished;
                int successCount = 0;
                bool isSuccessForCount = false;
                int notIncludeInTotal = 0;
                foreach (DataRow dr in _data.Rows)
                {
                    int employeeID = Convert.ToInt32(dr["mispar_ishi"]);
                    DateTime date = Convert.ToDateTime(dr["taarich"]);
                    if (ExecuteProcessForEmployee(employeeID, date, out isSuccessForCount))
                        successCount++;
                    if (!isSuccessForCount) notIncludeInTotal++;

                }
                double successParam = GetSuccessBatchParam(DateTime.Now.Date);
                double successRatio = ((double)successCount / (double)(_data.Rows.Count - notIncludeInTotal))
                    * 100.0;
                clLogBakashot.SetError(_btchRequest, "I", (int)_batchSource,
                    String.Format(@"Total rows to process={0}. Succesful rows={1}. I(HR) rows={4}. Parameter={2}. 
                            Ratio={3}. Bakasha_id = {5}. process ={6}.",
                    _data.Rows.Count, successCount, successParam, successRatio, notIncludeInTotal, _data.Rows[0]["bakasha_id"], _data.Rows[0]["num_pack"]));
                clLogBakashot.InsertErrorToLog();
                if (successRatio >= successParam)
                {
                    status = successCount == _data.Rows.Count ?
                        clGeneral.enBatchExecutionStatus.Succeeded :
                        clGeneral.enBatchExecutionStatus.PartialyFinished;
                }
            }
            return status;
        }

        //private void LogPopulation()
        //{
        //    foreach (DataRow dr in _data.Rows)
        //    {
        //        int employeeID = Convert.ToInt32(dr["mispar_ishi"]);
        //        DateTime date = Convert.ToDateTime(dr["taarich"]);
        //        clGeneral.LogBatchPopulation(employeeID, date, ExecutionDate, _btchRequest,
        //        _batchSource == BatchRequestSource.ErrorExecutionFromUI ?
        //                KdsLibrary.clGeneral.enGeneralBatchType.InputDataAndErrorsFromUI :
        //                KdsLibrary.clGeneral.enGeneralBatchType.InputDataAndErrorsFromInputProcess);
        //    }
        //}

        private double GetSuccessBatchParam(DateTime date)
        {
            double paramVal = 100.0;
            if (_dtParameters != null)
            {
                DataRow[] dr =
                    _dtParameters.Select(string.Concat("kod_param=",
                        clGeneral.cSuccessBatchRecordsParameterCode,
                        " and Convert('", date.ToShortDateString(),
                        "','System.DateTime') >= me_taarich and Convert('",
                        date.ToShortDateString(), "', 'System.DateTime') <= ad_taarich"));
                if (dr.Length > 0) paramVal = Convert.ToDouble(dr[0]["erech_param"]);
            }
            return paramVal;
        }

        public bool LoadData(int iNumProcess, long btchRequest)
        {
            _data = GetData(out _errorMessage, iNumProcess, btchRequest);
            clLogBakashot.SetError(btchRequest, "W", (int)btchRequest, "data.Rows=" + _data.Rows.Count + " iNumProcess=" + iNumProcess);
            clLogBakashot.InsertErrorToLog();
            return (_data != null && _data.Rows.Count > 0);
        }

        private bool ExecuteProcessForEmployee(int employeeID, DateTime date, out bool successCount)
        {
            bool nextStep = false;
            //   bool nextStep1 = false;
            successCount = false;
            clBatchManager btchMan = new clBatchManager(this._btchRequest);
            //    MainErrors oErrors = new MainErrors(date);
            try
            {
                if (_executionType == clGeneral.BatchExecutionType.InputData ||
                    _executionType == clGeneral.BatchExecutionType.All)
                {
                    nextStep = btchMan.MainInputData(employeeID, date, out successCount);
                    //clLogBakashot.SetError(_btchRequest, "I", (int)_batchSource,
                    //   String.Format("{0}: InputData result:{1}",
                    //   employeeID, nextStep));
                    //clLogBakashot.InsertErrorToLog();
                }

                if (_executionType == clGeneral.BatchExecutionType.ErrorIdentification ||
                    (_executionType == clGeneral.BatchExecutionType.All && nextStep))
                {
                    nextStep = btchMan.MainOvedErrors(employeeID, date);
                    // nextStep = oErrors.HafelShguim(employeeID, date);

                    //clLogBakashot.SetError(_btchRequest, "I", (int)_batchSource,
                    //   String.Format("{0}: OvedErrors result:{1}",
                    //   employeeID, nextStep));
                    //clLogBakashot.InsertErrorToLog();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                btchMan.Dispose();

            }
            return nextStep;
        }
        #endregion

        #region Properties
        public int UserID
        {
            get { return _userID; }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
        }

        public bool IsEmptyData
        {
            get { return _data != null && _data.Rows.Count == 0; }
        }

        protected virtual DateTime ExecutionDate
        {
            get { return DateTime.Now.Date; }
        }
        #endregion
    }

    /// <summary>
    /// Run batch process for InputData and/or Errors from Import process
    /// </summary>
    public class clBatchProcessFromInput : clBatchProcess  //שגויים אחרי סדרן
    {
        private DateTime _workdate;

        public clBatchProcessFromInput(clGeneral.BatchRequestSource batchSource,
            clGeneral.BatchExecutionType execType, DateTime workDate, long btchRequest) :
            base(batchSource, execType, btchRequest)
        {
            _workdate = workDate;

        }
        protected override DataTable GetData(out string errorMessage, int iNumProcess, long btchRequest)
        {
            DataTable dt = new DataTable();
            errorMessage = String.Empty;
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_num_process", ParameterType.ntOracleInteger, iNumProcess, ParameterDir.pdInput);
               // dal.AddParameter("p_type", ParameterType.ntOracleInteger, BatchRequestSource.ImportProcess.GetHashCode(), ParameterDir.pdInput);
                dal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, btchRequest, ParameterDir.pdInput);

                dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor,
                   null, ParameterDir.pdOutput);
                dal.ExecuteSP(KdsLibrary.clGeneral.cProproGetNetunimForProcess, ref dt);
            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
                errorMessage = ex.ToString();
            }
            return dt;
        }

        protected override DateTime ExecutionDate
        {
            get
            {
                return _workdate;
            }
        }
    }

    /// <summary>
    /// run batch process for InputData and/or Errors from UI
    /// </summary>
    //public class clBatchProcessFromUI : clBatchProcess
    //{
    //    public clBatchProcessFromUI(BatchRequestSource batchSource, BatchExecutionType execType, long btchRequest)
    //        : base(batchSource, execType, btchRequest)
    //    {
    //    }

    //    protected override DataTable GetData(out string errorMessage, int iNumProcess)
    //    {
    //        DataTable dt = new DataTable();
    //        errorMessage = String.Empty;
    //        try
    //        {
    //            clDal dal = new clDal();
    //            dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor,
    //                null, ParameterDir.pdOutput);
    //            dal.ExecuteSP(KdsLibrary.clGeneral.cProGetAllYameiAvoda, ref dt);
    //        }
    //        catch (Exception ex)
    //        {

    //            clGeneral.LogError(ex);
    //            errorMessage = ex.ToString();
    //        }
    //        return dt;
    //    }
    //}

    /// <summary>
    /// Run batch process for InputData and/or Errors from Import process
    /// for population that has changes in HR
    /// </summary>
    public class clBatchProcessFromInputForChangesInHR : clBatchProcessFromInput //שגויים אחרי hr
    {
        public clBatchProcessFromInputForChangesInHR(clGeneral.BatchRequestSource batchSource,
            clGeneral.BatchExecutionType execType, DateTime workDate, long btchRequest) :
            base(batchSource, execType, workDate, btchRequest)
        {


        }
        protected override DataTable GetData(out string errorMessage, int iNumProcess, long btchRequest)
        {
            DataTable dt = new DataTable();
            errorMessage = String.Empty;
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_num_process", ParameterType.ntOracleInteger, iNumProcess, ParameterDir.pdInput);
               // dal.AddParameter("p_type", ParameterType.ntOracleInteger, BatchRequestSource.ImportProcessForChangesInHR.GetHashCode(), ParameterDir.pdInput);
                dal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, btchRequest, ParameterDir.pdInput);
                dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor,
                   null, ParameterDir.pdOutput);
                dal.ExecuteSP(KdsLibrary.clGeneral.cProproGetNetunimForProcess, ref dt);
            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
                errorMessage = ex.ToString();
            }
            return dt;
        }
    }



    /// <summary>
    /// Run batch process for InputData and/or Errors from Import process
    /// for population that has Premiot
    /// </summary>
    public class clBatchProcessFromInputForPremiot : clBatchProcessFromInput //שגויים אחרי hr
    {
        public clBatchProcessFromInputForPremiot(clGeneral.BatchRequestSource batchSource,
            clGeneral.BatchExecutionType execType, DateTime workDate, long btchRequest) :
            base(batchSource, execType, workDate, btchRequest)
        {


        }
        protected override DataTable GetData(out string errorMessage, int iNumProcess, long btchRequest)
        {
            DataTable dt = new DataTable();
            errorMessage = String.Empty;
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_num_process", ParameterType.ntOracleInteger, iNumProcess, ParameterDir.pdInput);
                // dal.AddParameter("p_type", ParameterType.ntOracleInteger, BatchRequestSource.ImportProcessForChangesInHR.GetHashCode(), ParameterDir.pdInput);
                dal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, btchRequest, ParameterDir.pdInput);
                dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor,
                   null, ParameterDir.pdOutput);
                dal.ExecuteSP(KdsLibrary.clGeneral.cProproGetNetunimForProcess, ref dt);
            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
                errorMessage = ex.ToString();
            }
            return dt;
        }
    }
   

}