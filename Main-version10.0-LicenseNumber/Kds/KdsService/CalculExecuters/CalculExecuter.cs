using KDSCommon.Enums;
using KdsService.CalculBL;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace KdsService
{
    public class CalculExecuter : BaseBlockingExecuter<CalcParam>, ICalcExecuter
    {
        private IUnityContainer _container;
        private int _completeCounter = 0;
        private int _completeLimit = 0;
        public CalculExecuter(IUnityContainer container)
        {
            _container = container;
            int num = int.Parse((string)ConfigurationManager.AppSettings["ExeccutionNumThreds"]);
            Log(string.Format("Initializing the execution queue with {0} thread.", num.ToString()));
            Init(num);
        }

        public void UpdateOnCompleted(int count)
        {
            _completeLimit = count;
        }

        public event EventHandler<CompletedExecutionEventArgs> Completed;

        protected override BaseResult TaskStartExecute(CalcParam item)
        {
            Log(string.Format("CalculExecuter: Starting to work on item id {0}", item.ProcessId));
            try
            {
                clCalculMain oCalculMain;
                switch (item.TypeCalc)
                {
                    case enCalcType.MonthlyCalc:
                        //Thread.Sleep(int.Parse(args[7]) * DelayBetweenConnections * 1000);
                        oCalculMain = new clCalculMain(item.RequestNum, item.From, item.To, item.Mamad,
                                                           item.RitzaTest, item.RitzaGorefet, item.ProcessId);

                        oCalculMain.RunCalcBatchProcess();
                        break;
                    case enCalcType.PremiotCalc:
                        oCalculMain = new clCalculMain(item.RequestNum, item.ProcessId);
                        oCalculMain.RunCalcBatchProcessPremiyot();
                        break;
                    case enCalcType.ShinuyimVeShguyim:
                        oCalculMain = new clCalculMain(item.RequestNum, item.ProcessId, item.ExecutionType);
                        // oCalculMain = new clCalculMain(5789,1,clGeneral.BatchExecutionType.All);//(long)int.Parse(args[1]), int.Parse(args[3]),(BatchExecutionType)int.Parse(args[2]));
                        oCalculMain.RunShinuyimVeShguim();
                        break;

                    case enCalcType.ShinuyimVeSghuimHR:
                        oCalculMain = new clCalculMain(item.RequestNum, item.ProcessId, item.ExecutionType);
                        oCalculMain.RunShinuyimVeShguimHR();
                        break;
                    case enCalcType.ShinuyimVeSghuimPremiot:
                        //  oCalculMain = new clCalculMain(7970,1, clGeneral.BatchExecutionType.All);
                        oCalculMain = new clCalculMain(item.RequestNum, item.ProcessId, item.ExecutionType);
                        oCalculMain.RunShinuyimVeShguimPremiot();
                        break;
                    case enCalcType.Rikuzim:
                        clTask _Task = new clTask();
                        _Task.RunRikuzim(item.RequestNum, item.RequzimId, item.ProcessId);
                        break;
                }
            }
            catch (Exception ex)
            {
                return new BaseResult() { IsSucceeded = false, ErrorMsg = ex.ToString() };
            }
            return new BaseResult() { IsSucceeded = true };
        }

        protected override void TaskCompletExecute(CalcParam task, BaseResult result)
        {
            _completeCounter++;
            if (result.IsSucceeded == false)
            {
                Log(string.Format("CalculExecuter: Completed work on item id {0}. Exception occured: {1}", task.ProcessId, result.ErrorMsg));
            }
            else
            {
                Log(string.Format("CalculExecuter: Completed work on item id {0}", task.ProcessId));
            }

            if (_completeCounter == _completeLimit)
            {
                if (Completed != null)
                {
                    Completed(this, new CompletedExecutionEventArgs() { BatchId = task.RequestNum });
                }
                _completeCounter = 0;
            }
        }

        protected override bool TaskException(CalcParam task, Exception ex)
        {
            //If exception occured - continue on
            return true;
        }

        protected override void InitInternal()
        {
            
        }

        private void Log(string log)
        {
            Debug.WriteLine(log);
            Console.WriteLine(log);
        }
    }

    public class CalcParam
    {
        public enCalcType TypeCalc { get; set; }
        public long RequestNum { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Mamad { get; set; }
        public bool RitzaTest { get; set; }
        public bool RitzaGorefet { get; set; }
        public int ProcessId { get; set; }
        public BatchExecutionType ExecutionType { get; set; }
        public long RequzimId { get; set; }

        public CalcParam Clone()
        {
            CalcParam copy = new CalcParam();
            copy.TypeCalc = this.TypeCalc;
            copy.RequestNum = this.RequestNum;
            copy.From = this.From;
            copy.To = this.To;
            copy.Mamad = this.Mamad;
            copy.RitzaTest = this.RitzaTest;
            copy.RitzaGorefet = this.RitzaGorefet;
            copy.ProcessId = this.ProcessId;
            copy.ExecutionType = this.ExecutionType;
            copy.RequzimId = this.RequzimId;
            return copy;
        }
    }

    public class CompletedExecutionEventArgs : EventArgs
    {
        public long BatchId { get; set; }
    }
}
