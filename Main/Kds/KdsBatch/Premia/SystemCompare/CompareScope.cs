using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using KdsLibrary;

namespace KdsBatch.SystemCompare
{
    /// <summary>
    /// Represents a scope of 2 files to be imported for 
    /// a later data comparison
    /// </summary>
    /// <typeparam name="T">Type implementing ITextRowImportParser interface for
    /// parsing text rows</typeparam>
    public class CompareScope<T> where T : ITextRowImportParser
    {
        #region Fields
        private long _btchRequest;
        private clGeneral.enGeneralBatchType _btchType; 
        #endregion

        #region Properties
        public ImportStructure<T> OldImport { get; set; }
        public ImportStructure<T> NewImport { get; set; }
        public long BatchRequest
        {
            get { return _btchRequest; }
            set 
            { 
                _btchRequest = value;
                if (OldImport != null) OldImport.BatchRequest = _btchRequest;
                if (NewImport != null) NewImport.BatchRequest = _btchRequest;
            }
        }
        public clGeneral.enGeneralBatchType BatchType
        {
            get { return _btchType; }
            set
            {
                _btchType = value;
                if (OldImport != null) OldImport.BatchType = _btchType;
                if (NewImport != null) NewImport.BatchType = _btchType;
            }
        }
        #endregion

        #region Constractor
        private CompareScope(Type ImportType, XmlDocument xDoc)
        {
            XmlNode xNode = xDoc.SelectSingleNode(String.Concat("Imports/", ImportType.Name));
            if (xNode != null)
            {
                OldImport = KdsLibrary.Utils.KdsExtensions.DeserializeObject(ImportType,
                    xNode.OuterXml) as ImportStructure<T>;
                OldImport.CompareSide = CompareSide.Old;
                NewImport = KdsLibrary.Utils.KdsExtensions.DeserializeObject(ImportType,
                    xNode.OuterXml) as ImportStructure<T>;
                NewImport.CompareSide = CompareSide.New;
            }
        } 
        #endregion

        #region Methods
        public static CompareScope<T> GetInstance(Type ImportType, XmlDocument xDefinitionDoc,
            long btchRequest, clGeneral.enGeneralBatchType btchType)
        {
            var scope = new CompareScope<T>(ImportType, xDefinitionDoc);
            scope.BatchRequest = btchRequest;
            scope.BatchType = btchType;
            return scope;
        }

        public clGeneral.enBatchExecutionStatus Execute()
        {
            clGeneral.enBatchExecutionStatus status = clGeneral.enBatchExecutionStatus.Succeeded;
            bool oldExists = OldImport.SourceExists;
            bool newExists = NewImport.SourceExists;
            if (oldExists && newExists)
            {
                try
                {
                    OldImport.ImportFile();
                }
                catch (Exception ex)
                {
                    Log(_btchRequest, "E", GetErrorMessage(ex, OldImport), _btchType, DateTime.Now);
                    status = clGeneral.enBatchExecutionStatus.PartialyFinished;
                }

                try
                {
                    NewImport.ImportFile();
                }
                catch (Exception ex)
                {
                    Log(_btchRequest, "E", GetErrorMessage(ex, NewImport), _btchType, DateTime.Now);
                    status = clGeneral.enBatchExecutionStatus.PartialyFinished;
                }
            }
            else
            {
                if (!oldExists) LogMissingFile(OldImport);
                if (!newExists) LogMissingFile(NewImport);
                status = clGeneral.enBatchExecutionStatus.Failed;
            }
            return status;
        }

        private void LogMissingFile(ImportStructure<T> importStruc)
        {
            Log(_btchRequest, "E", String.Format("{0}, {1}:Missing File {2}", importStruc.GetType().ToString(),
                importStruc.CompareSide.ToString(), importStruc.SourceFile), _btchType, DateTime.Now);
        }

        internal static string GetErrorMessage(Exception ex, ImportStructure<T> importStruc)
        {
            return String.Format("{0}, {1}: {2}", importStruc.GetType().ToString(),
                importStruc.CompareSide.ToString(), ex.Message);
        }

        internal static string GetErrorMessage(string message, ImportStructure<T> importStruc)
        {
            return String.Format("{0}, {1}: {2}", importStruc.GetType().ToString(),
                importStruc.CompareSide.ToString(), message);
        }

        internal static void Log(long btchRequest, string msgType, string message,
            clGeneral.enGeneralBatchType batchType, DateTime period)
        {
            clLogBakashot.SetError(btchRequest, null, msgType, (int)batchType, period, message);
            clLogBakashot.InsertErrorToLog();
        }

        internal static void Log(long btchRequest, string msgType, string message,
            clGeneral.enGeneralBatchType batchType, DateTime period, int? employeeNum)
        {
            clLogBakashot.SetError(btchRequest, employeeNum, msgType, (int)batchType, period, message);
            clLogBakashot.InsertErrorToLog();
        }

        #endregion
    }

    /// <summary>
    /// Represents a batch process for importing different sets of files 
    /// for a later data comparison
    /// </summary>
    public class CompareImporter
    {
        #region Fields
        private XmlDocument _xDefinition;
        private long _btchRequest;
        private clGeneral.enBatchExecutionStatus _btchStatus;
        private clGeneral.enGeneralBatchType _btchType; 
        #endregion

        #region Constractor
        public CompareImporter()
        {
            Init();
        } 
        #endregion

        #region Methods
        private void Init()
        {
            _btchType = KdsLibrary.clGeneral.enGeneralBatchType.DataComparisonImport;
            _xDefinition = new XmlDocument();
            _xDefinition.Load(AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\CompareImport.xml");
        }

        public void ExecuteImport()
        {
            _btchRequest = KdsLibrary.clGeneral.OpenBatchRequest(_btchType, _btchType.ToString(), -1);
            _btchStatus = clGeneral.enBatchExecutionStatus.Running;
            ExecuteScope<FixedLengthParser>(typeof(EggedImport));
            ExecuteScope<FixedLengthParser>(typeof(TaavoraCalcImport));
            ExecuteScope<SeparatedValuesParser>(typeof(TaavoraBakaraImport));
            KdsLibrary.clGeneral.CloseBatchRequest(_btchRequest, _btchStatus);
        }

        private void ExecuteScope<T>(Type importType) where T : ITextRowImportParser
        {
            var scope = CompareScope<T>.GetInstance(importType, _xDefinition,
                _btchRequest, _btchType);
            var status = scope.Execute();
            _btchStatus = DecideNewStatus(status, _btchStatus);
        }

        internal static clGeneral.enBatchExecutionStatus DecideNewStatus(
            clGeneral.enBatchExecutionStatus current,
            clGeneral.enBatchExecutionStatus previous)
        {
            if (current == clGeneral.enBatchExecutionStatus.Failed &&
                previous != clGeneral.enBatchExecutionStatus.Failed)
                return current;
            if ((int)current > (int)previous) return current;
            else return previous;
        }

        #endregion
        
    }

    /// <summary>
    /// Side in data file comparison, represents old system and new system
    /// </summary>
    public enum CompareSide
    {
        Old,
        New
    }
}
