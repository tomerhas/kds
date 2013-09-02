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
        protected string _sTeur;
        private int _iKodReport;
        private bool _HasPeriodParameters  = false;
        private eFormat _Extension;
        private long _BakashaId;
        private int _MisparIshi;

        //private DateTime _Month; //for rikuzim
        //private int _sug_chishuv;
        //private int _maamad;
        //private int _ezor;
        //private int _hevra;
        //private DateTime _TarChishuv;

        //public DateTime Month
        //{
        //    get { return _Month; }
        //}
        //public int sug_chishuv
        //{
        //    get { return _sug_chishuv; }
        //}

        //public int Hevra
        //{
        //    get { return _hevra; }
        //}

        //public int Ezor
        //{
        //    get { return _ezor; }
        //}

        //public int Maamad
        //{
        //    get { return _maamad; }
        //}

        //public DateTime TarChishuv
        //{
        //    get { return _TarChishuv; }
        //}

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
        public clReport() { }
        public clReport(string name, int kod, string teur, long BakashaId, eFormat Extension, int MisparIshi)
        {
            _ReportParams = new List<clReportParam>();
            _sRdlName = name;
            _iKodReport = kod;
            _sTeur = " דו''ח " + teur;
            _Extension = Extension;
            _BakashaId = BakashaId;
            _MisparIshi = MisparIshi;
        }
        public clReport(string name, int kod, string teur, int HasPeriodParameters,int MisparIshi): this(name, kod,teur,0, eFormat.EXCEL,MisparIshi)
        {
            _MisparIshi = MisparIshi;
            _HasPeriodParameters = (HasPeriodParameters == 1) ? true : false;
        }

        public clReport(string RdlName, eFormat format, long BakashaId, int MisparIshi,int KodReport)//for rikuzim
        {
            _ReportParams = new List<clReportParam>();
            _sRdlName = RdlName;
            _Extension = format;
            _BakashaId = BakashaId;
            _MisparIshi = MisparIshi;
            _iKodReport = KodReport;
        }


        //public clReport(long BakashaId, int MisparIshi, DateTime Month, int sug_chishuv, int iEzor, int iMaamad, int iHevra, DateTime dTarChishuv)//for rikuzim
        //{
        //    _ReportParams = new List<clReportParam>();
        //    _sRdlName = "RikuzAvodaChodshi2";
        //    _Extension = eFormat.PDF ;
        //    _BakashaId = BakashaId;
        //    _MisparIshi = MisparIshi;
        //    _Month = Month;
        //    _sug_chishuv = sug_chishuv;
        //    _ezor = iEzor;
        //    _maamad = iMaamad;
        //    _hevra = iHevra;
        //    _TarChishuv = dTarChishuv;
        //    _sTeur = " ריכוזים ל" + _Month.ToShortDateString();
        //    _iKodReport = 0;
        //}

     
        public void Add(string ParamName, string ParamValue)
        {
            _ReportParams.Add(new clReportParam(ParamName, ParamValue));
        }

    }


}
