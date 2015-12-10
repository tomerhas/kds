using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels.WorkCard
{
    /// <summary>
    /// This is a list of the users sidurim
    /// This is a view model requested by the new workcard display (angular) 
    /// and contains only the data related to the display
    /// </summary>
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
