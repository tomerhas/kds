using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels.WorkCard
{
    public class PeilutWC
    {
        public SinglePropertyData<bool> RekaDown { get; set; }
        public SinglePropertyData<bool> RekaUp { get; set; }

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
        public SinglePropertyData<int> MisparKnisa { get; set; }
        public SinglePropertyData<int> MakatType { get; set; }
        public SinglePropertyData<string> CancelPeilut { get; set; }
        public SinglePropertyData<bool> PeilutActive { get; set; }


        public PeilutWC()
        {
            RekaDown = new SinglePropertyData<bool>();
            RekaUp = new SinglePropertyData<bool>();
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
            MisparKnisa = new SinglePropertyData<int>();
            MakatType = new SinglePropertyData<int>();
            CancelPeilut = new SinglePropertyData<string>();
            PeilutActive = new SinglePropertyData<bool>(); 
        }
    }
}
