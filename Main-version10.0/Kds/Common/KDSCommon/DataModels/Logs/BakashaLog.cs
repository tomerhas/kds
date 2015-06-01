using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels.Logs
{
    public class BakashaLog
    {
        public long RequestId { get; set; }
        public string SugHodaa { get; set; }
        public int? KodTahalich { get; set; }
        public int KodYeshut { get; set; }
        public int? MisparIshi { get; set; }
        public DateTime? Taarich { get; set; }
        public int? MisparSidur { get; set; }
        public DateTime? ShatHatchalaSidur { get; set; }
        public DateTime? ShatYetzia { get; set; }
        public int? MisparKnisa { get; set; }
        public string TeurHodaa { get; set; }
        public int? KodHodaa { get; set; }

        public override string  ToString()
        {
            return "";
        }
    }

}
