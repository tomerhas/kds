using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using KdsLibrary.Utils;
using KDSCommon.DataModels.Security;

namespace KdsLibrary.Security
{
    /// <summary>
    /// Represents logged on user and his role in application
    /// </summary>
    public class LoginUser
    {
        #region Fields
        private UserInfo _userInfo;
        private Exchange.ExchangeInfoServiceSoapClient _exchangeSrv;
        private MatchNameList<KdsModule> _modules;
        private bool _isLimitedUser;
        #endregion

        #region Properties
        public UserInfo UserInfo
        {
            get { return _userInfo; }
        }
     
        internal MatchNameList<KdsModule> KdsModules
        {
            get { return _modules; }
        }
        internal bool IsProfileExists
        {
            get { return UserInfo.UserProfiles.Count > 0; }
        }
        internal bool IsModulesListExists
        {

            get { return _modules != null; }
        }

        public KdsSecurityLevel GetControlSecurityLevel(string controlName)
        {
            KdsModule module = _modules[controlName.ToLower()];
            if (module != null) return module.SecurityLevel;
            else return KdsSecurityLevel.NoPermission;
        }

        public bool IsLimitedUser
        {
            get { return _isLimitedUser; }
        }

        #endregion

        #region Constractor
        private LoginUser()
        {
            _exchangeSrv = new Exchange.ExchangeInfoServiceSoapClient();
            CreateUserInfo();
        }


        #endregion

        #region Methods
       
        private void CreateUserInfo()
        {
            _userInfo = new UserInfo();
            if (HttpContext.Current.Session["Inject_User"] != null && HttpContext.Current.Session["Inject_User"] != "")
                _userInfo.Username = HttpContext.Current.Session["Inject_User"].ToString();
            else  _userInfo.Username = 
                (ConfigurationManager.AppSettings["DebugModeUserName"] == "true") ? ConfigurationManager.AppSettings["DebugUserName"] : HttpContext.Current.Request.ServerVariables["LOGON_USER"];
            _userInfo.EmployeeNumber =
                _exchangeSrv.getEmpNumByUserName(_userInfo.Username);
            _userInfo.EmployeeFullName =
                _exchangeSrv.getEmpFullName(_userInfo.Username);
        }

        internal void AddProfileToUser(UserProfile profile)
        {
            UserInfo.UserProfiles.Add(profile);
        }
        internal void SetModulesList(MatchNameList<KdsModule> modules)
        {
            _modules = modules;
        }
        public static LoginUser GetLoginUser()
        {
            LoginUser user = null;
            if (HttpContext.Current != null)
            {
                user = (LoginUser)HttpContext.Current.Session["LoginUser"];
            }
            if (user == null)
            {
                user = new LoginUser();
                HttpContext.Current.Session.Add("LoginUser", user);
            }
            return user;
        }

        public void InjectEmployeeNumber(string empNumber)
        {
            _userInfo.EmployeeNumber = empNumber;
        }

        public static LoginUser GetLimitedUser(string empNumber)
        {
            var user = new LoginUser();
            user.UserInfo.EmployeeNumber = empNumber;
            user._isLimitedUser = true;
            return user;
        }
        public static bool IsRashemetProfile(LoginUser oLoginUser)
        {
            bool bRashemet = false;
            try
            {
                for (int i = 0; i < oLoginUser.UserInfo.UserProfiles.Count; i++)
                {
                    if ((oLoginUser.UserInfo.UserProfiles[i].Role == clGeneral.enProfile.enRashemet.GetHashCode())
                         || (oLoginUser.UserInfo.UserProfiles[i].Role == clGeneral.enProfile.enRashemetAll.GetHashCode())
                         || (oLoginUser.UserInfo.UserProfiles[i].Role == clGeneral.enProfile.enSystemAdmin.GetHashCode()

                         ))
                    {
                        bRashemet = true;
                        break;
                    }
                }
                return bRashemet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IsMenahelBankShaot(LoginUser oLoginUser)
        {
            bool bMenahelBankShaot = false;
            try
            {
                for (int i = 0; i < oLoginUser.UserInfo.UserProfiles.Count; i++)
                {
                    if (oLoginUser.UserInfo.UserProfiles[i].Role == clGeneral.enProfile.enMenahelBankMeshek.GetHashCode())
                    {
                        bMenahelBankShaot = true;
                        break;
                    }
                }
                return bMenahelBankShaot;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        
    }
}
