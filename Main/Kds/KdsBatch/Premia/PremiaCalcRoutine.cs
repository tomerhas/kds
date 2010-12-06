using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;

namespace KdsBatch.Premia
{
    /// <summary>
    /// Base class for Premia Calculation sub routine to inherit behaviours
    /// </summary>
    public abstract class PremiaCalcRoutine
    {
        #region Fields
        protected PremiaSettings _settings;
        protected DateTime _periodDate;
        protected int _userId;
        protected string _errorMessage;
        protected long _processBtchNumber; 
        #endregion

        #region Constractors
        public PremiaCalcRoutine(DateTime period, int userId, long processBtchNumber)
        {
            _periodDate = period;
            _userId = userId;
            _processBtchNumber = processBtchNumber;
            _settings = new PremiaSettings();
        } 
        #endregion

        #region Methods
        public virtual bool Execute()
        {
            bool success = false;
            _errorMessage = String.Empty;
            if (_processBtchNumber == 0)
                _processBtchNumber = StartBatchRequest();
            KdsLibrary.clGeneral.enBatchExecutionStatus status;
            try
            {
                RunRoutine();
                status = KdsLibrary.clGeneral.enBatchExecutionStatus.Succeeded;
            }
            catch (Exception ex)
            {
                success = false;
                _errorMessage = ex.Message;
                Log(_processBtchNumber, "E", _errorMessage, BatchType, _periodDate);
                status = KdsLibrary.clGeneral.enBatchExecutionStatus.Failed;
            }
            CloseBatch(_processBtchNumber, status);
            return success;
        }

        protected abstract void RunRoutine();

        protected virtual void CloseBatch(long processBtchNumber,
           KdsLibrary.clGeneral.enBatchExecutionStatus status)
        {
            KdsLibrary.clGeneral.CloseBatchRequest(processBtchNumber, status);
        }

        protected virtual long StartBatchRequest()
        {
            return KdsLibrary.clGeneral.OpenBatchRequest(BatchType,
               String.Concat("Premia Calculation - ", BatchType.ToString()), _userId);
        }

        internal void Log(long btchRequest, string msgType, string message,
            clGeneral.enGeneralBatchType batchType, DateTime period)
        {
            clLogBakashot.SetError(btchRequest, null, msgType, (int)batchType, period, message);
            clLogBakashot.InsertErrorToLog();
        }
        internal void Log(long btchRequest, string msgType, string message,
           clGeneral.enGeneralBatchType batchType, DateTime period, int employeeNumber)
        {
            int? logEmpNumber = null;
            if (employeeNumber != 0) logEmpNumber = employeeNumber;
            clLogBakashot.SetError(btchRequest, logEmpNumber, msgType, (int)batchType, period, message);
            clLogBakashot.InsertErrorToLog();
        }  
        #endregion

        #region Properties
        protected abstract clGeneral.enGeneralBatchType BatchType { get; }
        public string ErrorMessage { get { return _errorMessage; } }
        #endregion
        
    }
}
