using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary.Utils.Reports;
using System.Configuration;

namespace KdsLibrary.Utils.Reports
{
    class clRikuz : clReport
    {

        private DateTime _Month; //for rikuzim
        private int _sug_chishuv;
        private int _maamad;
        private int _ezor;
        private int _hevra;
        private DateTime _TarChishuv;

        public DateTime Month
        {
            get { return _Month; }
        }
        public int sug_chishuv
        {
            get { return _sug_chishuv; }
        }

        public int Hevra
        {
            get { return _hevra; }
        }

        public int Ezor
        {
            get { return _ezor; }
        }

        public int Maamad
        {
            get { return _maamad; }
        }

        public DateTime TarChishuv
        {
            get { return _TarChishuv; }
        }

        public clRikuz(long BakashaId, int MisparIshi, DateTime Month, int sug_chishuv, int iEzor, int iMaamad, int iHevra, DateTime dTarChishuv, string sRSVersion, string sUrlRSConfig, string sServiceUrlConfig)
            : base(ConfigurationSettings.AppSettings["RikuzRdlName"], eFormat.PDF, BakashaId, MisparIshi, 0)//for rikuzim
        {
            _sTeur = " ריכוזים ל " + Month.ToShortDateString();
            _Month = Month;
            _sug_chishuv = sug_chishuv;
            _ezor = iEzor;
            _maamad = iMaamad;
            _hevra = iHevra;
            _TarChishuv = dTarChishuv;

            RSVersion = sRSVersion;
            UrlConfigKey = sUrlRSConfig;
            ServiceUrlConfigKey = sServiceUrlConfig;
        }
    }
}
