using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels
{
    public class EtHefreshLineDM
    {
        public int MisparErua { get; set; }
        public int MisparHevra { get; set; }
        public int Tz { get; set; }

        public int Mn { get; set; }

        public string OvedName { get; set; }
        public int Semel { get; set; }
        public DateTime TokefMe { get; set; }

        public DateTime TokefAd { get; set; }
        public int KodIdkun { get; set; }
        public decimal Schum { get; set; }
        public decimal Kamut { get; set; }

        public string Achuz { get; set; }
        public string Taarif { get; set; }

        public object[] GetExcelRowValues()
        {
            return new object[]{ MisparErua, MisparHevra, Tz,Mn,OvedName, Semel, TokefMe, TokefAd, KodIdkun, Schum, Kamut , Achuz , Taarif };
        }
    }
}
