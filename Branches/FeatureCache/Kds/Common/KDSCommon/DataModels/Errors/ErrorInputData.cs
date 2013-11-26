using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Specialized;
using KDSCommon.Enums;

namespace KDSCommon.DataModels.Errors
{
    public class ErrorInputData
    {
        public DataTable dtMatzavOved { get; set; }
        public DataTable dtErrors { get; set; }
        public int iMisparIshi { get; set; }
        public ErrorTypes ErrorTypes { get; set; }
        public DateTime CardDate { get; set; }
        public long BtchRequestId { get; set; }
        public bool IsSuccsess { get; set; }
        public OvedYomAvodaDetailsDM OvedDetails { get; set; }
        public OrderedDictionary htEmployeeDetails{ get; set; }
    }
}
