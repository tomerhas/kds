using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsWorkFlow.Approvals
{
    /// <summary>
    /// Represents a key to identify the approval request
    /// </summary>
    public class ApprovalKey
    {
        private Employee _employee;
        private Approval _approval;
        private WorkCard _workCard;
        private RequestValues _requestValues = RequestValues.Empty;
        public Employee Employee
        {
            get { return _employee; }
            set { _employee = value; }
        }

        public Approval Approval
        {
            get { return _approval; }
            set { _approval = value; }
        }

        public WorkCard WorkCard
        {
            get { return _workCard; }
            set { _workCard = value; }
        }

        public RequestValues RequestValues
        {
            get { return _requestValues; }
            set { _requestValues = value; }
        }

        public bool IsEqual(ApprovalKey key)
        {
            if (key == null) return false;
            else
                return _employee.IsEqual(key.Employee) &&
                    _approval.IsEqual(key.Approval) &&
                    _workCard.IsEqual(key.WorkCard) &&
                    _requestValues.IsEqual(key.RequestValues);
        }
    }

    public class ApprovalKeyWithTag : ApprovalKey
    {
        public ApprovalTag AppTag { get; set; }
        public ApprovalKeyWithTag()
        {
            AppTag = new ApprovalTag();
        }
    }

    public class ApprovalTag
    {
        public string Remark { get; set; }
        public bool Changed { get; set; }
        public int Factor { get; set; }
        public string EmployeeName { get; set; }
        public string ApprovalCodeDescriptiom { get; set; }
        public ApprovalTag()
        {
            Remark = String.Empty;
        }
       
    }
}
