using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel;
using System.Threading;
using System.Globalization; 

namespace KdsBatch.Premia
{
    /// <summary>
    /// Exposes common API for working with Excel files
    /// </summary>
    public class ExcelAdapter:IDisposable
    {
        #region Fields
        Excel.Application _application;
        Excel.Workbook _workBook;
        Excel.Worksheet _excelSheet;
        string _filename; 
        #endregion

        #region Constractor
        public ExcelAdapter(string filename)
        {
            _application = new Excel.Application();
            _filename = filename;
        } 
        #endregion

        #region Methods
        public void OpenNewWorkBook()
        {
            CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            _workBook = _application.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Thread.CurrentThread.CurrentCulture = originalCulture;
            //_workBook = _application.Workbooks.Open(_filename, true, false, 5, "", "", 
            //    false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
        }

        public void SaveNewWorkBook()
        {
            object misValue = System.Reflection.Missing.Value;

            if (System.IO.File.Exists(_filename))
                System.IO.File.Delete(_filename);
            _excelSheet.SaveAs(_filename, misValue, misValue, misValue, misValue, misValue, misValue, misValue,
                misValue, misValue);
        }

        public void CloseWorkBook()
        {
            _workBook.Close(true, _filename, 0);
        }

        public void AddValue(string col, int row, object value)
        {
            string cell = String.Concat(col, row);
            _excelSheet = (Excel.Worksheet)_workBook.ActiveSheet;
            _excelSheet.get_Range(cell, System.Reflection.Missing.Value).Formula = value;

        }

        public object GetValue(string col, int row)
        {
            string cell = String.Concat(col, row);
            _excelSheet = (Excel.Worksheet)_workBook.ActiveSheet;
            return _excelSheet.get_Range(cell, System.Reflection.Missing.Value).Formula;
        }

        public void AddRow(string[] cols, int row, object[] values)
        {
            if (cols.Length != values.Length)
                throw new Exception("Columns and Values don't match!");
            for (int i = 0; i < cols.Length; ++i)
            {
                AddValue(cols[i], row, values[i]);
            }
        }

        public void Quit()
        {
            try
            {
                if (_workBook != null)
                {
                    _workBook.Close(false, _filename, 0);
                    _workBook = null;
                }
            }
            catch (Exception) { }
            _application.Quit();
        }

        public void OpenExistingWorkBook()
        {
            _workBook = _application.Workbooks.Open(_filename, true, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", false, false, 0, true, false, false);
        }

        public void SaveExistingWorkBook()
        {
            _workBook.Save();
        } 
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                if (_workBook != null)
                {
                    _workBook.Close(false, _filename, 0);
                    _workBook = null;
                }
                if (_application != null)
                {
                    _application = null;
                }
            }
            catch (Exception) { }
        }

        #endregion
    }
}
