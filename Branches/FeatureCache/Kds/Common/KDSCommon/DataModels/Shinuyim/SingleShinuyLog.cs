using KdsShinuyim.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels.Shinuyim
{
    public class SingleShinuyLog
    {
        public ShinuyTypes ShinuyType { get; set; }

        public int MisparIshi { get; set; }
        public DateTime Taarich { get; set; }
        public int MisparSidur { get; set; }
        public DateTime ShatHatchala { get; set; }
        public DateTime ShatGmar { get; set; }

        public DateTime ShatYetzia { get; set; }
    }

}
