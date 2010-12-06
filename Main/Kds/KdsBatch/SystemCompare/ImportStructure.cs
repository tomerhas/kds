using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml.Serialization;
using System.Xml;
using System.Configuration;
using System.IO;
using KdsLibrary.DAL;
using KdsLibrary;

namespace KdsBatch.SystemCompare
{
    public delegate void ParserEventHandler(object sender, ParserEventArgs e);
    /// <summary>
    /// Interface for parsing text row and storing the
    /// values in DataRow
    /// </summary>
    public interface ITextRowImportParser
    {
        event ParserEventHandler NotifyParseError;
        string ParserID { get; set; }
        void FillDataRow(DataRow dr, string textRow);
    }

    public abstract class BaseParser
    {
        protected virtual object IsParsedValueValid(object parsedValue, Type dataType, out bool isValid)
        {
            isValid = true;
            if (parsedValue == DBNull.Value) return parsedValue;
            switch (dataType.ToString())
            {
                case "System.Double":
                    double tryValue;
                    if (double.TryParse(parsedValue.ToString(), out tryValue))
                        return tryValue;
                    else
                    {
                        isValid = false;
                        return DBNull.Value;
                    }

                default: return parsedValue;
            }
        }
    }
    /// <summary>
    /// Base class for importing a text file into 
    /// database table
    /// </summary>
    /// <typeparam name="T">Type implementing ITextRowImportParser interface for
    /// parsing text rows</typeparam>
    public abstract class ImportStructure<T> where T:ITextRowImportParser
    {
        #region Fields
        protected DataTable _dtImport;
        protected List<T> _parsers;
        protected List<ImportColumn> _columns;
        #endregion

        #region Properties
        [XmlAttribute("TargetTable")]
        public string TargetTable { get; set; }

        [XmlAttribute("SourceFile")]
        public string SourceFile { get; set; }

        [XmlAttribute("Truncate")]
        public bool TruncateTarget { get; set; }

        [XmlIgnore]
        public CompareSide CompareSide { get; set; }
        [XmlIgnore]
        public long BatchRequest { get; set; }
        [XmlIgnore]
        public clGeneral.enGeneralBatchType BatchType { get; set; }

        public bool SourceExists
        {
            get { return File.Exists(GetSourceFullPath()); }
        }

        #endregion

        #region Methods
        protected virtual void CreateTableStructure()
        {
            _dtImport = new DataTable();
            _columns.ForEach(delegate(ImportColumn col)
            {
                _dtImport.Columns.Add(new DataColumn(col.ColumnName, col.ColumnType));
            });
        }

        protected virtual string GetEventCode(string textRow)
        {
            return textRow.Substring(0, 3);
        }

        protected virtual T GetRowParser(string textRow)
        {
            string eventCode = GetEventCode(textRow);
            T foundParser = default(T);

            var founds = from p in _parsers
                         where p.ParserID.Equals(eventCode)
                         select p;
            if (founds.Count() > 0) foundParser = founds.FirstOrDefault();
            else
            {
                var defaults = from p in _parsers
                               where p.ParserID.Equals("*")
                               select p;
                if (defaults.Count() > 0) foundParser = defaults.FirstOrDefault();
            }
            return foundParser;
        }

        protected virtual bool FillNewRow(DataRow newRow, string textRow)
        {
            var parser = GetRowParser(textRow);
            bool result = false;
            if (parser != null)
            {
                parser.FillDataRow(newRow, textRow);
                result = true;
            }
            return result;
        }

        protected virtual void StoreData()
        {
            if (TruncateTarget)
            {
                clDal oDal = new clDal();
                string cmdDelete = GetDeleteCommandText();
                oDal.ExecuteSQL(cmdDelete);
            }
            string cmdText = GetInsertCommandText();
            foreach (DataRow dr in _dtImport.Rows)
            {
                StoreDataRow(cmdText, dr);
            }
        }

        private void StoreDataRow(string cmdText, DataRow dr)
        {
            clDal oDal = new clDal();
            foreach (DataColumn dc in _dtImport.Columns)
            {
                oDal.AddParameter(dc.ColumnName, GetParameterType(dc),
                    dr[dc.ColumnName], ParameterDir.pdInput);
            }

            try
            {
                oDal.ExecuteSQL(cmdText);
            }
            catch (Exception ex)
            {
                int? employeeNumber;
                string eventCode;
                SetErrorDetails(dr, out employeeNumber, out eventCode);
                CompareScope<T>.Log(BatchRequest, "E",
                    CompareScope<T>.GetErrorMessage(ex, this) + eventCode, BatchType, 
                    DateTime.Now, employeeNumber);
            }
        }

        private void SetErrorDetails(DataRow dr, out int? employeeNumber, 
            out string eventCode)
        {
            employeeNumber = null;
            eventCode = String.Empty;
            if (_dtImport.Columns.Contains("Mispar_Ishi"))
            {
                int tryEmpNum;
                if (int.TryParse(dr["Mispar_Ishi"].ToString(), out tryEmpNum))
                    employeeNumber = tryEmpNum;
            }
            if (_dtImport.Columns.Contains("Kod_Erua"))
                eventCode = String.Format(" Kod Erua:{0}", dr["Kod_Erua"].ToString());
        }
        
        private string GetDeleteCommandText()
        {
            return String.Format("delete {0}_{1}", CompareSide, TargetTable);
        }

        private ParameterType GetParameterType(DataColumn dc)
        {
            if (dc.DataType == typeof(Int32)) return ParameterType.ntOracleInteger;
            else return ParameterType.ntOracleVarchar;
        }

        private string GetInsertCommandText()
        {
            StringBuilder sbCmd = new StringBuilder();
            StringBuilder sbValues = new StringBuilder();
            sbCmd.AppendFormat("insert into {0}_{1} (", CompareSide, TargetTable);

            foreach (DataColumn dc in _dtImport.Columns)
            {
                sbCmd.AppendFormat("{0},", dc.ColumnName);
                sbValues.AppendFormat(":{0},", dc.ColumnName);
            }
            sbCmd.Remove(sbCmd.Length - 1, 1);
            sbValues.Remove(sbValues.Length - 1, 1);
            sbCmd.Append(") values(");
            sbValues.Append(")");
            sbCmd.Append(sbValues.ToString());
            return sbCmd.ToString();
        }

        public virtual void ImportFile()
        {
            CreateTableStructure();
            _parsers.ForEach(delegate(T parser)
            {
                parser.NotifyParseError += new ParserEventHandler(Parser_NotifyParseError);
            });
            string filePath = GetSourceFullPath();
            StreamReader reader = new StreamReader(filePath);
            while (reader.Peek() > 0)
            {
                string textRow = reader.ReadLine();
                DataRow newRow = _dtImport.NewRow();
                if (FillNewRow(newRow, textRow))
                    _dtImport.Rows.Add(newRow);
            }
            reader.Close();
            StoreData();
        }

        void Parser_NotifyParseError(object sender, ParserEventArgs e)
        {
            int? employeeNumber;
            string eventCode;
            SetErrorDetails(e.NewDataRow, out employeeNumber, out eventCode);
            CompareScope<T>.Log(BatchRequest, "W",
                String.Format("{0} at row:{1} {2}",
                CompareScope<T>.GetErrorMessage(e.Message, this), e.FileRowIndex, eventCode),
                BatchType, DateTime.Now,
                employeeNumber);
        }

        private string GetSourceFullPath()
        {
            return String.Concat(ConfigurationManager.AppSettings["CompareImportPath"], "\\",
                CompareSide.ToString(), "_", SourceFile);
        }

        #endregion
      
    }
    
    /// <summary>
    /// Parses text row by a specified fixed columns length
    /// Implements ITextRowImportParser
    /// </summary>
    public class FixedLengthParser : BaseParser, ITextRowImportParser
    {
        public event ParserEventHandler NotifyParseError;
        [XmlAttribute("ID")]
        public string ParserID { get; set; }
        [XmlElement("Column", typeof(FixedColumn))]
        public List<FixedColumn> FixedColumns { get; set; }
        public void FillDataRow(DataRow dr, string textRow)
        {
            FixedColumns.ForEach(delegate(FixedColumn fCol)
            {
                var parsedValue = SetParsedValueAccordingToType(ParseValue(fCol, textRow),
                    dr.Table.Columns[fCol.ColumnName].DataType, fCol, dr);
                
                dr[fCol.ColumnName] = parsedValue;
            });
        }

        private object SetParsedValueAccordingToType(object parsedValue, Type columnType,
            FixedColumn fCol, DataRow dr)
        {
            bool valid = true;
            object returnVal = IsParsedValueValid(parsedValue, columnType, out valid);
            if (!valid)
            {
                if (NotifyParseError != null)
                    NotifyParseError(this,
                        GetEventArgs(parsedValue, fCol, dr));
            }
            return returnVal;
        }

        private ParserEventArgs GetEventArgs(object parsedValue, FixedColumn fCol, DataRow dr)
        {
            ParserEventArgs e = new ParserEventArgs();
            e.Message = String.Format("Invalid value: {0} for Column {1} at char {2} and {3}",
                        parsedValue, fCol.ColumnName, fCol.DeclarationFrom,
                        fCol.DeclarationTo);
            e.NewDataRow = dr;
            e.FileRowIndex = dr.Table.Rows.Count + 1;
            return e;
        }

        private object ParseValue(FixedColumn fCol, string textRow)
        {
            object value = DBNull.Value;
            if (textRow.Length >= fCol.To + 1 && fCol.To >= fCol.From)
            {
                value = textRow.Substring(fCol.From, fCol.To - fCol.From + 1);
                if (String.IsNullOrEmpty(value.ToString().Trim())) value = DBNull.Value;
            }
            return value;
        }
    }
    
    /// <summary>
    /// Parses character separated values text row
    /// Implements ITextRowImportParser
    /// </summary>
    public class SeparatedValuesParser : BaseParser, ITextRowImportParser
    {
        public event ParserEventHandler NotifyParseError;
        [XmlAttribute("ID")]
        public string ParserID { get; set; }
        [XmlAttribute("Separator")]
        public string Separator { get; set; }
        public void FillDataRow(DataRow dr, string textRow)
        {
            string[] rowValues = textRow.Split(Separator.ToCharArray());
            int i = 0;
            foreach (DataColumn dc in dr.Table.Columns)
            {
                var parsedValue = SetParsedValueAccordingToType(rowValues[i], dc, dr);
                dr[dc.ColumnName] = parsedValue;
                i++;
            }
        }

        private object SetParsedValueAccordingToType(object parsedValue, DataColumn dc,
            DataRow dr)
        {
            bool valid = true;
            object returnVal = IsParsedValueValid(parsedValue, dc.DataType, out valid);
            if (!valid)
            {
                if (NotifyParseError != null)
                    NotifyParseError(this, GetEventArgs(parsedValue, dc, dr));
            }
            return returnVal;
        }

        private ParserEventArgs GetEventArgs(object parsedValue, DataColumn dc, DataRow dr)
        {
            ParserEventArgs e = new ParserEventArgs();
            e.Message = String.Format("Invalid value: {0} for Column {1}",
                                 parsedValue, dc.ColumnName);
            e.NewDataRow = dr;
            e.FileRowIndex = dr.Table.Rows.Count + 1;
            return e;
        }
    }


    /// <summary>
    /// Imports Egged_Sys data file 
    /// </summary>
    public class EggedImport : ImportStructure<FixedLengthParser>
    {
        [XmlElement("FixedLengthParser", typeof(FixedLengthParser))]
        public List<FixedLengthParser> Parsers
        {
            get { return _parsers; }
            set { _parsers = value; }
        }
        [XmlElement("Column", typeof(ImportColumn))]
        public List<ImportColumn> Columns 
        {
            get { return _columns; }
            set { _columns = value; }
        }
    }
    
    /// <summary>
    /// Imports Taavora Calculation data file
    /// </summary>
    public class TaavoraCalcImport : ImportStructure<FixedLengthParser>
    {
        [XmlElement("FixedLengthParser", typeof(FixedLengthParser))]
        public List<FixedLengthParser> Parsers
        {
            get { return _parsers; }
            set { _parsers = value; }
        }
        [XmlElement("Column", typeof(ImportColumn))]
        public List<ImportColumn> Columns
        {
            get { return _columns; }
            set { _columns = value; }
        }
    }

    /// <summary>
    /// Imports Taavora Bakara data file
    /// </summary>
    public class TaavoraBakaraImport : ImportStructure<SeparatedValuesParser>
    {
        [XmlElement("SeparatedValuesParser", typeof(SeparatedValuesParser))]
        public List<SeparatedValuesParser> Parsers
        {
            get { return _parsers; }
            set { _parsers = value; }
        }
        [XmlElement("Column", typeof(ImportColumn))]
        public List<ImportColumn> Columns
        {
            get { return _columns; }
            set { _columns = value; }
        }
    }

    public class ParserEventArgs
    {
        public string Message { get; set; }
        public int FileRowIndex { get; set; }
        public DataRow NewDataRow { get; set; }
    }
}
