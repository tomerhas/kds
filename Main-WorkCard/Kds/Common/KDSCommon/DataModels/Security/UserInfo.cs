using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels.Security
{
    public class UserInfo
    {
        public UserInfo()
        {
            UserProfiles = new List<UserProfile>();
        }
        private string _username;
        private string _employeeNumber;
        private string _employeeFullName;

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }
        public string EmployeeNumber
        {
            get { return _employeeNumber; }
            set { _employeeNumber = value; }
        }
        public string EmployeeFullName
        {
            get { return _employeeFullName; }
            set { _employeeFullName = value; }
        }

        public List<UserProfile> UserProfiles { get; set; }
    }
}
