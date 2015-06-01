using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using KdsLibrary;
using System.IO;
using DalOraInfra.DAL;

namespace KdsBatch.Premia
{
    /// <summary>
    /// Responsible for premia input file creation routine
    /// </summary>
    public class PremiaFileCreator:PremiaCalcRoutine
    {
        #region Fields
        private PremiaItemsCollection _items;
        private long _btchRequest;
        #endregion

        #region Constractor
        public PremiaFileCreator(long btchRequest, DateTime period, int userId, long processBtchNumber) :
            base(period, userId, processBtchNumber)
        {
            _btchRequest = btchRequest;
            _items = new PremiaItemsCollection();
        }

        #endregion

        #region Methods
        
        private void ExportToFile()
        {
            var exAdpt = new ExcelAdapter(_settings.GetInputFullFilePath(_periodDate));
            try
            {
                if (!_settings.IsInputFolderExists(_periodDate))
                    throw new Exception(String.Format("Path {0} does not exist",
                        _settings.GetInputFolderPath(_periodDate)));
               
                exAdpt.OpenNewWorkBook();
                AddTitles(exAdpt);
                int i = 2;
                string[] cols = GetExcelCols();
                foreach (PremiaItem item in _items.Items)
                {
                    exAdpt.AddRow(cols, i, item.GetExcelRowValues());
                    i++;
                }
                exAdpt.SaveNewWorkBook(_periodDate);
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

        private string[] GetExcelCols()
        {
            return new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O" };
        }

        private string[] GetTitleValuesForExcel()
        {
            return new string[]{TitleA, TitleB, TitleC, TitleD,
                 TitleE, TitleF, TitleG, TitleH, TitleI,
                 TitleJ, TitleK, TitleL, TitleM, TitleN,
                 TitleO};
        }
        
        private void AddTitles(ExcelAdapter exAdpt)
        {
            exAdpt.AddRow(GetExcelCols(), 1, GetTitleValuesForExcel());
            //int i = 1;
            //exAdpt.AddValue("A", i, PremiaItem.TitleA);
            //exAdpt.AddValue("B", i, PremiaItem.TitleB);
            //exAdpt.AddValue("C", i, PremiaItem.TitleC);
            //exAdpt.AddValue("D", i, PremiaItem.TitleD);
            //exAdpt.AddValue("E", i, PremiaItem.TitleE);
            //exAdpt.AddValue("F", i, PremiaItem.TitleF);
            //exAdpt.AddValue("G", i, PremiaItem.TitleG);
            //exAdpt.AddValue("H", i, PremiaItem.TitleH);
            //exAdpt.AddValue("I", i, PremiaItem.TitleI);
            //exAdpt.AddValue("J", i, PremiaItem.TitleJ);
            //exAdpt.AddValue("K", i, PremiaItem.TitleK);
            //exAdpt.AddValue("L", i, PremiaItem.TitleL);
            //exAdpt.AddValue("M", i, PremiaItem.TitleM);
            //exAdpt.AddValue("N", i, PremiaItem.TitleN);
            //exAdpt.AddValue("O", i, PremiaItem.TitleO);
        }

        private void LoadItemsFromDB()
        {
            try
            {
                DataTable dt = GetData();
                if (dt != null)
                {
                    PremiaItem lastItem = null;
                    int daysCounter = 0, minutesCounter = 0;
                    List<PremiaItem> stationItems = new List<PremiaItem>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        PremiaItem item = PremiaItem.GetItemFromDataRow(dr);

                        if (lastItem != null && (item.EmployeeNumber != lastItem.EmployeeNumber ||
                            !item.Station.Equals(lastItem.Station) || !item.PremiaCode.Equals(lastItem.PremiaCode)))
                        {
                            UpdateStationCounters(stationItems, daysCounter, minutesCounter);
                            stationItems.Clear();
                            daysCounter = 0;
                            minutesCounter = 0;
                            _items.AddItem(lastItem);
                        }
                        stationItems.Add(item);
                        if (daysCounter == 0 || (item.EmployeeNumber == lastItem.EmployeeNumber && item.WorkDate != lastItem.WorkDate))
                            daysCounter++;
                        minutesCounter += Convert.ToInt32(dr["ERECH_RECHIV"]);

                        lastItem = item;
                    }
                    if (lastItem != null) //מוסיפים את האחרון
                    {
                        UpdateStationCounters(stationItems, daysCounter, minutesCounter);
                        _items.AddItem(lastItem);
                    }
                    if (stationItems.Count > 0)
                        UpdateStationCounters(stationItems, daysCounter, minutesCounter);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateStationCounters(List<PremiaItem> stationItems, int daysCounter,
            int minutesCounter)
        {
            stationItems.ForEach(delegate(PremiaItem item)
            {
                item.MinutesQuantity = minutesCounter;
                item.DaysQuantity = daysCounter;
            });
        }

        private DataTable GetData()
        {
            DataTable dt = new DataTable();
            clDal dal = new clDal();
            dal.AddParameter("p_taarich", ParameterType.ntOracleDate, _periodDate, ParameterDir.pdInput);
            dal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, _btchRequest,
                ParameterDir.pdInput);
            dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null,
                ParameterDir.pdOutput);
            dal.ExecuteSP("pkg_batch.pro_get_premia_input", ref dt);
            return dt;
        }

        protected override void RunRoutine()
        {
            try
            {
                LoadItemsFromDB();
                ExportToFile();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Properties
        protected override clGeneral.enGeneralBatchType BatchType
        {
            get { return clGeneral.enGeneralBatchType.CreatePremiaExcelInput; }
        } 
        #endregion

        #region Constants
        private const string TitleA = "מספר אישי";
        private const string TitleB = "תחנה";
        private const string TitleC = "תאריך";
        private const string TitleD = "סוג פרמיה";
        private const string TitleE = "אזור";
        private const string TitleF = "מעמד";
        private const string TitleG = "מותאם";
        private const string TitleH = "גיל";
        private const string TitleI = "עיסוק";
        private const string TitleJ = "מאפיין 60";
        private const string TitleK = "מאפיין 74";
        private const string TitleL = "ימי נוכחות";
        private const string TitleM = "דקות נוכחות";
        private const string TitleN = "משפחה";
        private const string TitleO = "פרטי"; 
        #endregion

        
    }
}
