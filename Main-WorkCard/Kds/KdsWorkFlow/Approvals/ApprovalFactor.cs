using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsWorkFlow.Approvals
{
    /// <summary>
    /// Represents a factor(manager) who is going to handle
    /// the approve request
    /// </summary>
    public class ApprovalFactor
    {
        public string Mail { get; set; }

        public int? EmployeeNumber { get; set; }

        public bool IsPrimary { get; set; }
    }
}
