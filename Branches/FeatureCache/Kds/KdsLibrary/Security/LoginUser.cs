using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using KdsLibrary.Utils;

namespace KdsLibrary.Security
{
    /// <summary>
    /// Represents logged on user and his role in application
    /// </summary>
    public class LoginUser
    {
        #region Fields
        private UserInfo _userInfo;
        private List<UserProfile> _profiles;
        private Exchange.ExchangeInfoServiceSoapClient _exchangeSrv;
        private MatchNameList<KdsModule> _modules;
        private bool _isLimitedUser;
        #endregion

        #region Properties
        public UserInfo UserInfo
        {
            get { return _userInfo; }
        }
        public UserProfile[] UserProfiles
        {
            get { return _profiles.ToArray(); }
        }
        internal MatchNameList<KdsModule> KdsModules
        {
            get { return _modules; }
        }
        internal bool IsProfileExists
        {
            get { return _profiles.Count > 0; }
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
            _profiles = new List<UserProfile>();
            CreateUserInfo();
        }


        #endregion

        #region Methods
       
        private void CreateUserInfo()
        {
            _userInfo = new UserInfo();
            _userInfo.Username =
                (ConfigurationManager.AppSettings["DebugModeUserName"] == "true") ? ConfigurationManager.AppSettings["DebugUserName"] : HttpContext.Current.Request.ServerVariables["LOGON_USER"];
            _userInfo.EmployeeNumber =
                _exchangeSrv.getEmpNumByUserName(_userInfo.Username);
            _userInfo.EmployeeFullName =
                _exchangeSrv.getEmpFullName(_userInfo.Username);
        }

        internal void AddProfileToUser(UserProfile profile)
        {
            _profiles.Add(profile);
        }
        internal void SetModulesList(MatchNameList<KdsModule> modules)
        {
            _modules = modules;
        }
        public static LoginUser GetLoginUser()
        {
            LoginUser user = (LoginUser)HttpContext.Current.Session["LoginUser"];
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
                for (int i = 0; i < oLoginUser.UserProfiles.Length; i++)
                {
                    if ((oLoginUser.UserProfiles[i].Role == clGeneral.enProfile.enRashemet.GetHashCode())
                         || (oLoginUser.UserProfiles[i].Role == clGeneral.enProfile.enRashemetAll.GetHashCode())
                         || (oLoginUser.UserProfiles[i].Role == clGeneral.enProfile.enSystemAdmin.GetHashCode()

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
                for (int i = 0; i < oLoginUser.UserProfiles.Length; i++)
                {
                    if (oLoginUser.UserProfiles[i].Role == clGeneral.enProfile.enMenahelBankMeshek.GetHashCode())
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
