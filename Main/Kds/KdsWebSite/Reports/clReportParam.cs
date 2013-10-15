using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsBatch.Reports
{
    public class clReportParam
    {
        private string _sName;
        private string _sValue;

        public string Name
        {
            get { return _sName; }
          //  set { _sName = value; }
        }

        public string Value
        {
            get { return _sValue; }
          //  set { _sValue = value; }
        }

        public clReportParam(string name, string value)
        {
            _sName = name;
            _sValue = value;
        }
            
    }
}
