using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary.Utils.Reports;

namespace KdsBatch.Reports
{
    public class clReport
    {
        private List<clReportParam> _ReportParams;
        private string _sRdlName;
        private string _sTeur;
        private int _iKodReport;
        private bool _HasPeriodParameters  = false;
        private eFormat _Extension;
        private long _BakashaId;
        private int _MisparIshi; 

        public string RdlName
        {
            get { return _sRdlName; }    
        }

        public string Teur
        {
            get { return _sTeur; }
        }

        public int KodReport
        {
            get { return _iKodReport; }
        }
        public eFormat Extension
        {
            get { return _Extension; }
        }

        public bool HasPeriodParameters
        {
            get { return _HasPeriodParameters; }
        }

        public long BakashaId
        {
            get { return _BakashaId; }
        }


        public int MisparIshi
        {
            get { return _MisparIshi; }
        }
        public List<clReportParam> ReportParams
        {
            get { return _ReportParams; }
            //  set { _sRdlName = value; }
        }

        public clReport(string name, int kod, string teur, long BakashaId, eFormat Extension, int MisparIshi)
        {
            _ReportParams = new List<clReportParam>();
            _sRdlName = name;
            _iKodReport = kod;
            _sTeur = teur;
            _Extension = Extension;
            _BakashaId = BakashaId;
            _MisparIshi = MisparIshi;
        }
        public clReport(string name, int kod, string teur, int HasPeriodParameters,int MisparIshi): this(name, kod,teur,0, eFormat.EXCEL,MisparIshi)
        {
            _MisparIshi = MisparIshi;
            _HasPeriodParameters = (HasPeriodParameters == 1) ? true : false;
        }

        public void Add(string ParamName, string ParamValue)
        {
            _ReportParams.Add(new clReportParam(ParamName, ParamValue));
        }

    }


}
