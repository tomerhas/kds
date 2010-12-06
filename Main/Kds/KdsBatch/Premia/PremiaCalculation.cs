using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;

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
        
        private void RunMacro()
        {
            var exAdpt = new ExcelAdapter(_settings.GetMacroFullPath(_periodDate));

            try
            {
                if(!_settings.IsMacroFileExists(_periodDate))
                    throw new Exception(String.Format("Path {0} does not exist",
                        _settings.GetMacroFullPath(_periodDate)));
                exAdpt.OpenExistingWorkBook();
                exAdpt.SaveExistingWorkBook();
                exAdpt.CloseWorkBook();
            }
            catch (Exception ex)
            {
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
            RunMacro();
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
