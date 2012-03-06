using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using KdsLibrary.DAL;
using KdsLibrary;
using System.Configuration;
namespace KdsBatch.Premia
{
    /// <summary>
    /// Responsible for storing premia calculation output in database
    /// </summary>
    public class PremiaFileImporter:PremiaCalcRoutine
    {
        #region Fields
        private const string NOT_VISIBLE_ROW_COMPARE_VALUE = "x";
        private const int FIRST_DATA_ROW_INDEX = 6;
        private const int YEAR_VALUE_ROW = 3;
        private const string YEAR_VALUE_COL = "B";
        private const int MONTH_VALUE_ROW = 3;
        private const string MONTH_VALUE_COL = "C";
        private PremiaItemsCollection _items;
        private long _btchRequest;
        private DataTable _dtPremiaTypes;
        #endregion

        #region Constractor
        public PremiaFileImporter(long btchRequest, DateTime period, int userId, long processBtchNumber)
            : base(period, userId, processBtchNumber)
        {
            _btchRequest = btchRequest;
            _items = new PremiaItemsCollection();
        }

        #endregion

        protected override void RunRoutine()
        {
            LoadItemsFromFile();
            StoreItemsInDB();
        }

        private void StoreItemsInDB()
        {
            foreach (PremiaItem item in _items.Items.Where(
                delegate(PremiaItem pItem)
                { return !String.IsNullOrEmpty(pItem.RechivCode); }))
            {
                //System.Diagnostics.EventLog.WriteEntry("KDS",
                //  String.Format("p_taarich={0} bakasha_id={1} p_mispar_ishi={2} p_kod_rechiv={3} p_erech_rechiv={4}",
                //    item.WorkDate, _btchRequest, item.EmployeeNumber, item.RechivCode, item.MinutesQuantity));
                clDal dal = new clDal();
                dal.AddParameter("p_taarich", ParameterType.ntOracleDate, item.WorkDate,
                    ParameterDir.pdInput);
                dal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, _btchRequest,
                    ParameterDir.pdInput);
                dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, item.EmployeeNumber,
                    ParameterDir.pdInput);
                dal.AddParameter("p_kod_rechiv", ParameterType.ntOracleInteger, item.RechivCode,
                    ParameterDir.pdInput);
                dal.AddParameter("p_erech_rechiv", ParameterType.ntOracleDecimal, item.MinutesQuantity,
                    ParameterDir.pdInput);
                try
                {
                    dal.ExecuteSP("pkg_batch.pro_update_calc_premia");
                }
                catch (Exception ex)
                {
                    Log(_processBtchNumber, "E",
                        ex.Message, KdsLibrary.clGeneral.enGeneralBatchType.StorePremiaCalculationOutput,
                        item.WorkDate, item.EmployeeNumber);
                }
            }
        }

        private void LoadItemsFromFile()
        {
            LoadDictionaryTables();
            DataTable dt = GetDataTableFromExcel();
            System.Diagnostics.EventLog.WriteEntry("KDS",
                  String.Format("Found {0} rows in file", dt.Rows.Count));
            FillItems(dt);
           // ReloadPeriod(dt);
        }

        private void LoadDictionaryTables()
        {
            _dtPremiaTypes = new DataTable();
            clDal dal = new clDal();
            dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null,
                ParameterDir.pdOutput);
            dal.ExecuteSP("pkg_sysman.S_CTB_SUGEY_PREMIOT", ref _dtPremiaTypes);
        }

        private void FillItems(DataTable dt)
        {
            PremiaItem lastItem = null;
            double minutesCounter = 0;
            List<PremiaItem> stationItems = new List<PremiaItem>();
            string col_mi = ConfigurationManager.AppSettings["col_tozar_mi"];
            string col_date = ConfigurationManager.AppSettings["col_tozar_date"];
            string col_code = ConfigurationManager.AppSettings["col_tozar_prem_code"];
            foreach (DataRow dr in dt.Rows)
            {
              //  if (dr[GetExcelColumnIndex("W")].ToString().Equals(NOT_VISIBLE_ROW_COMPARE_VALUE) ||
                if (dt.Rows.IndexOf(dr)<FIRST_DATA_ROW_INDEX) continue;
                var item = PremiaItem.GetItemFromExcelDataRow(dr,col_mi,col_date,col_code);
                if (item != null)
                {
                    item.RechivCode = GetDictionaryValueOfPremiaCode(item.PremiaCode,
                        "KOD_RACHIV_Premia");

                    string excelCol = GetDictionaryValueOfPremiaCode(item.PremiaCode,
                       "EXCEL_FILE_COLUMN");
                    if (!String.IsNullOrEmpty(excelCol))
                        if (clGeneral.IsNumeric(dr[GetExcelColumnIndex(excelCol)].ToString()))
                        {
                         
                            _items.AddItem(item);

                            if (lastItem != null && (item.EmployeeNumber != lastItem.EmployeeNumber ||
                                   !item.PremiaCode.Equals(lastItem.PremiaCode)))//  !item.Station.Equals(lastItem.Station)))
                            {
                                UpdateStationCounters(stationItems, minutesCounter);
                                stationItems.Clear();
                                minutesCounter = 0;
                            }
                            minutesCounter += Convert.ToDouble(dr[GetExcelColumnIndex(excelCol)]);
                            stationItems.Add(item);
                            lastItem = item;
                        }
                      
                    //string excelCol = GetDictionaryValueOfPremiaCode(item.PremiaCode,
                    //    "EXCEL_FILE_COLUMN");
                    //if (!String.IsNullOrEmpty(excelCol))
                    //    minutesCounter += Convert.ToInt32(dr[GetExcelColumnIndex(excelCol)]);
                }
            }
            if (stationItems.Count > 0)
                UpdateStationCounters(stationItems, minutesCounter);
            System.Diagnostics.EventLog.WriteEntry("KDS",
                  String.Format("{0} items for import", _items.Items.Count()));

        }

        private string GetDictionaryValueOfPremiaCode(string premiaCode, string columnName)
        {
            var rows = _dtPremiaTypes.Select(String.Format("KOD_PREMIA={0}", premiaCode));
            if (rows.Length > 0)
                return rows[0][columnName].ToString();
            else return null;

        }

        private void UpdateStationCounters(List<PremiaItem> stationItems, double minutesCounter)
        {
            stationItems.ForEach(delegate(PremiaItem item)
            {
                item.MinutesQuantity = minutesCounter;
            });
        }

        public static int GetExcelColumnIndex(string strColumn)
        {
            int index;
	        strColumn = strColumn.ToUpper();

	        if (strColumn.Length == 1) 
	        {
		        index = Convert.ToByte(Convert.ToChar(strColumn)) - 64;
	        }		
	        else if (strColumn.Length == 2)
	        {
                index =
			        ((Convert.ToByte(strColumn[0]) - 64) * 26) +
			        (Convert.ToByte(strColumn[1]) - 64);

	        }
	        else if (strColumn.Length == 3)
	        {
                index =
                    ((Convert.ToByte(strColumn[0]) - 64) * 26 * 26) +
                    ((Convert.ToByte(strColumn[1]) - 64) * 26) +
                    (Convert.ToByte(strColumn[2]) - 64);
	        }
	        else
	        {
		        throw new ApplicationException("Column Length must be between 1 and 3.");
	        }
            return index - 1;
        }

        private DataTable GetDataTableFromExcel()
        {
            string paramExcelSheetName = "נוכחות"+"$";
            //to store the data in the excel sheet
            DataTable dtExcelData = null;
            DbProviderFactory dbFactory;

            //to store the connection object
            OleDbConnection objConnection = null;

            //to store the command to be executed
            OleDbCommand objCommand = null;

            //adapter object
            DbDataAdapter dbAdapter = null;

            //DbDataReader dbReader = null;
            DataSet excelDataSet = null;
            DataTable dtColumnNames = null;

            try
            {
                //connection string
                string strExcelConnectionString = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=YES;""",
                    _settings.GetMacroFullPath(_periodDate));
                dbFactory = DbProviderFactories.GetFactory("System.Data.OleDb");

                //opens the connection and check it
                using (objConnection = new OleDbConnection(strExcelConnectionString))
                {
                    //open the connection
                    objConnection.Open();

                    String[] restrection = { null, null, paramExcelSheetName, null };
                    dtColumnNames = objConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, 
                        restrection);
                    //query to be executed
                   

                    string strSqlStmt =
                            "select * from [" + paramExcelSheetName + "]";
                    //execute the query
                    objCommand = new OleDbCommand(strSqlStmt, objConnection);

                    try
                    {
                        dbAdapter = dbFactory.CreateDataAdapter();
                        dbAdapter.SelectCommand = objCommand;
                        excelDataSet = new DataSet();
                        //execute the qurey
                        dbAdapter.Fill(excelDataSet);
                        //get the data
                        dtExcelData = excelDataSet.Tables[0];
                    }
                    catch (Exception ex)
                    {
                        //rethrow the exception
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                //rethrow the exception
                throw ex;
            }
            finally
            {
                //release the used resources
                if (objConnection != null)
                {
                    objConnection.Close();
                    objConnection.Dispose();
                }
                if (objCommand != null)
                {
                    objCommand.Dispose();
                }
                if (dbAdapter != null)
                {
                    dbAdapter.Dispose();
                }
                if (excelDataSet != null)
                {
                    excelDataSet.Dispose();
                }
                if (dtColumnNames != null)
                {
                    dtColumnNames = null;
                }
            }
            return dtExcelData;
        }

        private void ReloadPeriod(DataTable dt)
        {
            try
            {
                int month = Convert.ToInt32(
                    dt.Rows[MONTH_VALUE_ROW][GetExcelColumnIndex(MONTH_VALUE_COL)]);
                int year = Convert.ToInt32(
                    dt.Rows[YEAR_VALUE_ROW][GetExcelColumnIndex(YEAR_VALUE_COL)]);
                _periodDate = new DateTime(year, month, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
            //var exlAdtp = new ExcelAdapter(_settings.GetMacroFullPath(_periodDate));
            //try
            //{
            //    exlAdtp.OpenExistingWorkBook();
            //    int month = Convert.ToInt32(exlAdtp.GetValue("C", 4));
            //    int year = Convert.ToInt32(exlAdtp.GetValue("C", 2));
            //    _periodDate = new DateTime(year, month, 1);
            //    bool saved = false;
            //    int attempts = 0;
            //    while (!saved && attempts < 10)
            //    {
            //        try
            //        {
            //            exlAdtp.SaveExistingWorkBook();
            //            saved = true;
            //        }
            //        catch (System.Runtime.InteropServices.COMException comEx)
            //        {
            //            attempts++;
            //            if (attempts >= 10) throw comEx;
            //        }
            //    }
            //    saved = false;
            //    attempts = 0;
            //    while (!saved && attempts < 10)
            //    {
            //        try
            //        {
            //            exlAdtp.CloseWorkBook();
            //            saved = true;
            //        }
            //        catch (System.Runtime.InteropServices.COMException comEx)
            //        {
            //            attempts++;
            //            if (attempts >= 10) throw comEx;
            //        }
            //    }
                
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    exlAdtp.Quit();
            //    exlAdtp.Dispose();
            //    exlAdtp = null;
            //}
        }

        protected override KdsLibrary.clGeneral.enGeneralBatchType BatchType
        {
            get { return KdsLibrary.clGeneral.enGeneralBatchType.StorePremiaCalculationOutput; }
        }
    }
}
