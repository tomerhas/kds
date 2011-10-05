using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsBatch.Entities
{
    public class CardError
    {
        public int check_num { get; set; }
        public int mispar_ishi { get; set; }
        public int mispar_sidur { get; set; }
        public DateTime taarich { get; set; }
        public DateTime shat_hatchala { get; set; }
        public DateTime Shat_Yetzia { get; set; }
        public int mispar_knisa { get; set; }
        public long makat_nesia { get; set; }
    }
}
