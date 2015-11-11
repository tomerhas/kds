using KDSCommon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels.Security
{
    public class UserProfile : IMatchName
    {
        #region Fields
        private int _role;
        private string _profileGroup;
        #endregion

        #region Properties
        public int Role
        {
            get { return _role; }
            set { _role = value; }
        }
        public string ProfileGroup
        {
            get { return _profileGroup; }
            set { _profileGroup = value; }
        }
        #endregion

        #region IMatchName Members

        public string Name
        {
            get { return _profileGroup; }
        }

        #endregion
    }
}
