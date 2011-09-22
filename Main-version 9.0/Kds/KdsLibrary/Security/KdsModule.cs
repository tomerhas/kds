using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary.Utils;

namespace KdsLibrary.Security
{
    /// <summary>
    /// Defines module which requires role security
    /// </summary>
    [Serializable]
    public class KdsModule : IMatchName
    {
        #region Fields
        private string _name;
        private KdsModuleType _type;
        private KdsSecurityLevel _securityLevel;
        private int _moduleID;
        private int _pakadID;
        #endregion

        #region Properties
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public KdsModuleType ModuleType
        {
            get { return _type; }
            set { _type = value; }
        }
        public KdsSecurityLevel SecurityLevel
        {
            get { return _securityLevel; }
            set { _securityLevel = value; }
        }
        public int ModuleID
        {
            get { return _moduleID; }
            set { _moduleID = value; }
        }
        #endregion

    }

    /// <summary>
    /// Types of KdsModule
    /// </summary>
    public enum KdsModuleType
    {
        Page = 1,
        Control = 2
    }

    public enum KdsSecurityLevel
    {
        NoPermission=0,
        UpdateOrExecute=1,
        ViewAll=2,
        ViewOnlyEmployeeData=3,
        UpdateEmployeeData=4,
        UpdateEmployeeDataAndViewOnlySubordinates=5,
        UpdateEmployeeDataAndSubordinates=6
    }
}
