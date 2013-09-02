﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
using System.Diagnostics;

namespace KdsBatch.Premia
{
    /// <summary>
    /// Responsible for running premia macro calculation routine
    /// </summary>
    public class PremiaCalculation:PremiaCalcRoutine
    {
       
        #region Constractor
        public PremiaCalculation(DateTime period, int userId, long processBtchNumber)
            : base(period, userId, processBtchNumber)
        {

        }

        #endregion

        #region Methods

        private void RunMacroWithScript()
        {
            try
            {
                //clLogBakashot.InsertErrorToLog(58, 75757, "I", 0, null, "In RunMacroWithScript");
          
                string filename = _settings.GetMacroFullPath(_periodDate);
                //clLogBakashot.InsertErrorToLog(58, 75757, "I", 0, null, "before IsMacroFileExists file=" + filename);
          
                if (!_settings.IsMacroFileExists(_periodDate))
                    throw new Exception(String.Format("Path {0} does not exist",
                        filename));
                Process scriptProc = new Process();

                scriptProc.StartInfo.FileName = "cscript";
                scriptProc.StartInfo.Arguments =
                    String.Format("//B //Nologo {0}runmacro.vbs {1}",
                    AppDomain.CurrentDomain.BaseDirectory, filename);
                //clLogBakashot.InsertErrorToLog(58, 75757, "I", 0, null, scriptProc.StartInfo.Arguments);
                //clLogBakashot.InsertErrorToLog(58, 75757, "I", 0, null, "before scriptProc.Start()");
          
                scriptProc.Start();
                //clLogBakashot.InsertErrorToLog(58, 75757, "I", 0, null, "before  scriptProc.WaitForExit()");
                scriptProc.WaitForExit();
                //clLogBakashot.InsertErrorToLog(58, 75757, "I", 0, null, "before scriptProc.Close()");
                scriptProc.Close();
            }
            catch (Exception ex)
            {
                Log(_processBtchNumber, "E", ex.Message, BatchType, _periodDate);
                System.Diagnostics.EventLog.WriteEntry("KDS", ex.ToString());
                throw ex;
            }
        }
        private void RunMacro()
        {
           // clLogBakashot.InsertErrorToLog(58, 75757, "I", 0, null, "In RunMacro before new ExcelAdapter");
            var exAdpt = new ExcelAdapter(_settings.GetMacroFullPath(_periodDate));
           // clLogBakashot.InsertErrorToLog(58, 75757, "I", 0, null, "In RunMacro after new ExcelAdapter");
           
            try
            {
                if(!_settings.IsMacroFileExists(_periodDate))
                    throw new Exception(String.Format("Path {0} does not exist",
                        _settings.GetMacroFullPath(_periodDate)));
               // clLogBakashot.InsertErrorToLog(58, 75757, "I", 0, null, "After IsMacroFileExists");
                exAdpt.OpenExistingWorkBook();
               // clLogBakashot.InsertErrorToLog(58, 75757, "I", 0, null, "After OpenExistingWorkBook");
                bool saved = false;
                int attempts=0;
                while (!saved && attempts < 3)
                {
                    try
                    {
                     //   clLogBakashot.InsertErrorToLog(58, 75757, "I", 0, null, "Before SaveExistingWorkBook");
                        exAdpt.SaveExistingWorkBook();
                     //   clLogBakashot.InsertErrorToLog(58, 75757, "I", 0, null, "After SaveExistingWorkBook");
                        saved = true;
                    }
                    catch (System.Runtime.InteropServices.COMException comEx)
                    {
                        Log(_processBtchNumber, "E", comEx.Message, BatchType, _periodDate);
                        System.Diagnostics.EventLog.WriteEntry("KDS", comEx.ToString());
                        attempts++;
                        if (attempts >= 3) throw comEx;
                    }
                }
                //exAdpt.SaveExistingWorkBook();
                exAdpt.CloseWorkBook();
            }
            catch (Exception ex)
            {
                Log(_processBtchNumber, "E", ex.Message, BatchType, _periodDate);
                System.Diagnostics.EventLog.WriteEntry("KDS", ex.ToString());
                throw ex;
            }
            finally
            {
                exAdpt.Quit();
                exAdpt.Dispose();
                exAdpt = null;
            }
        }

        protected override void RunRoutine()
        {
            RunMacroWithScript();
        }

        #endregion

        #region Properties
        protected override clGeneral.enGeneralBatchType BatchType
        {
            get { return clGeneral.enGeneralBatchType.ExecutePremiaCalculationMacro; }
        } 
        #endregion
    }
}
