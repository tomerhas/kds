using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsWorkFlow.Approvals
{
    public class Employee
    {
        private string _employeeNumber;
        private Dictionary<EmployeeDetailsCodes, object> _employeeDetails;
        public string EmployeeNumber
        {
            get { return _employeeNumber; }
            set { _employeeNumber = value; }
        }

        public Employee()
        {
            _employeeDetails = new Dictionary<EmployeeDetailsCodes, object>();
        }

        public Employee(string employeeNumber)
        {
            _employeeNumber = employeeNumber;
            _employeeDetails = new Dictionary<EmployeeDetailsCodes, object>();
        }

        public bool IsEqual(Employee emp)
        {
            if (emp == null) return false;
            else return _employeeNumber.Equals(emp.EmployeeNumber);
        }

        public Dictionary<EmployeeDetailsCodes, object> EmployeeDetails
        {
            get { return _employeeDetails; }
        }
    }

    public enum EmployeeDetailsCodes
    {
        ActivityUnit=1,
        ActivityRegion=3,
        ActivityBranch=4,
        CompanyCode=-1
    }
}
