using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels.WorkCard
{
    /// <summary>
    /// class for engular version
    /// </summary>

    public class WorkCardResultContainer
    {
        public SidurimWC Sidurim { get; set; }
        public HityazvutInfo FirstHityazvut { get; set; }
        public HityazvutInfo SecondHityazvut { get; set; }
        public NetuneyYomAvoda DayDetails { get; set; }
        public StatusCard CardStatus { get; set; }
        public bool  bRashemet { get; set; }
        public bool bMenahelBankShaot { get; set; }
        public clParametersDM oParams { get; set; }
    }
}
