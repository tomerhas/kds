using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels.WorkCard
{
    public class StatusCard
    {
        public int Kodstatus { get; set; }
        public string  TeurStatus { get; set; }
        public string ClassStr { get; set; }

        public StatusCard()
        {
            TeurStatus = "";
            ClassStr = "";
        }
    }
}
