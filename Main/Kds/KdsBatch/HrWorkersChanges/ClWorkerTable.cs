using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsBatch.HrWorkersChanges
{
    public enum TableType
    {
        State,
        Properties,
        Details,
        Defaults
    }
 
    public class ClWorkerTable
    {
        private int _IdNumber, _Code;
        private string _ValueA = string.Empty, _ValueB = string.Empty;
        private DateTime _FromDate, _ToDateA, _ToDateB;

        public ClWorkerTable(int IdNumber, DateTime FromDate, DateTime ToDateA, 
                             DateTime ToDateB, string ValueA, string ValueB,int kod)
        {
            _IdNumber = IdNumber;  
            _ValueA = ValueA; 
            _ValueB = ValueB; 
            _FromDate = FromDate;
            _ToDateA = ToDateA;
            _ToDateB = ToDateB;
            _Code = kod;
        }

     
        #region Properties
        public int IdNumber
        {
            get { return _IdNumber; }
        }

        public int Code
        {
            get { return _Code; }
        }
        public DateTime FromDate
        {
            get { return _FromDate; }
        }
        public DateTime ToDateA
        {
            get { return _ToDateA; }
        }
        public DateTime ToDateB
        {
            get { return _ToDateB; }
        }
        public string ValueA
        {
            get { return _ValueA; }
        }
        public string ValueB
        {
            get { return _ValueB; }
        }
        #endregion 

    }
}
