using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels.WorkCard
{
    public class PeilutWC
    {
        public SinglePropertyData<int> KisuyTor { get; set; }
        public SinglePropertyData<DateTime> ShatYezia { get; set; }
        public SinglePropertyData<string> Teur { get; set; }
        public SinglePropertyData<string> Kav { get; set; }
        public SinglePropertyData<string> Sug { get; set; }
        public SinglePropertyData<long> Makat { get; set; }
        public SinglePropertyData<int> DakotTichnun { get; set; }
        public SinglePropertyData<string> DakotBafoal { get; set; }
        public SinglePropertyData<long> NumOto { get; set; }
        public SinglePropertyData<string> Nezer { get; set; }

        public PeilutWC()
        {
            KisuyTor = new SinglePropertyData<int>(); 
            ShatYezia = new SinglePropertyData<DateTime>();
            Teur = new SinglePropertyData<string>();
            Kav = new SinglePropertyData<string>();
            Sug = new SinglePropertyData<string>();
            Makat = new SinglePropertyData<long>();
            DakotTichnun = new SinglePropertyData<int>();
            DakotBafoal = new SinglePropertyData<string>();
            NumOto = new SinglePropertyData<long>();
            Nezer = new SinglePropertyData<string>(); 
        }
    }
}
