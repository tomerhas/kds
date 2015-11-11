using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels.WorkCard
{
    public class SidurimWC
    {
        public SidurimWC()
        {
            SidurimList = new List<SidurWC>();
        }
        public List<SidurWC> SidurimList { get; set; }
        public bool HasShaguyNext { get; set; }
    }
}
