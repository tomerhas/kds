using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using KdsLibrary.UI;
using KdsLibrary.Utils;
using System.Web.UI;
using System.Configuration;
using System.Collections;
using System.DirectoryServices;
using System.Web.UI.HtmlControls;
using DalOraInfra.DAL;
using KDSCommon.DataModels.Security;

namespace KdsLibrary.Security
{
    /// <summary>
    /// Provides security authorization for application users
    /// </summary>
    public class SecurityManager
    {
        #region Fields
        private LoginUser _loginUser;
        private KdsPage _kdsPage;       
        private const string LIMITED_PROFILE = "kds_nahag";
        #endregion

        #region Constractor
        public SecurityManager(LoginUser loginUser)
        {
            _loginUser = loginUser;
        } 
        #endregion

        #region Methods
        public void AuthorizePage(KdsPage page)
        {
            _kdsPage = page;
            //load user profiles and modules
            if (!_loginUser.IsProfileExists)
                CreateProfile();
            if (!_loginUser.IsModulesListExists)
                CreateModulesList();
            if (!_kdsPage.IsPostBack)
            {
                //check if the page is allowed in this role
                if (page.RequiresAuthorization)
                {
                    KdsModule pageModule = null;
                    if (!IsPageInRole(out pageModule))
                        NonAuthorizedRedirect();
                    else page.SetPageModule(pageModule);
                }
                //Check authorization for other controls if the page requires
                if (page.EnableControlSecurity)
                {
                    EnableControls(page.Controls,_kdsPage);
                }
            }
        }
        public void AuthorizePage(KdsPage page, bool bAlwaysEnableControls)
        {
            _kdsPage = page;
            //load user profiles and modules
            if (!_loginUser.IsProfileExists)
                CreateProfile();
            if (!_loginUser.IsModulesListExists)
                CreateModulesList();
            if ((!_kdsPage.IsPostBack) || (bAlwaysEnableControls))
            {
                //check if the page is allowed in this role
                if (page.RequiresAuthorization)
                {
                    KdsModule pageModule = null;
                    if (!IsPageInRole(out pageModule))
                        NonAuthorizedRedirect();
                    else page.SetPageModule(pageModule);
                }
                //Check authorization for other controls if the page requires
                if (page.EnableControlSecurity)
                {
                    EnableControls(page.Controls, _kdsPage);
                }
            }
        }
        private KdsModule GetPakadModuleInRole(string pakadID, KdsPage page)
        {
            KdsModule foundModule = null;
            if (page.PageModule != null)
            {
                foreach (KdsModule module in _loginUser.KdsModules.Items)
                {
                    if ((module.ModuleID == page.PageModule.ModuleID) &&
                        //((module.Name.ToLower().Equals(pakadID.ToLower()))))
                        ((pakadID.ToLower().IndexOf(module.Name.ToLower())) > -1))
                    {
                        foundModule = module;
                        break;
                    }
                }
            }   
            return foundModule;
        }

        private void EnableControls(ControlCollection controls, KdsPage page)
        {
            _kdsPage = page;
            foreach (Control ctl in controls)
            {
                if (!String.IsNullOrEmpty(ctl.ID))
                {
                    KdsModule module = GetPakadModuleInRole(ctl.ID, page);//_loginUser.KdsModules[ctl.ID.ToLower()];
                    if (module != null)
                        if (page.DisabledControls){
                           if (module.SecurityLevel
                               == KdsSecurityLevel.NoPermission)
                            {
                                   if (ctl is HtmlControl)
                                       ((HtmlControl)ctl).Attributes.Add("disabled", "true");                              
                                   else if(ctl is WebControl)
                                      ((WebControl)ctl).Attributes.Add("disabled", "true");
                          
                            }
                        }
                        else{                        
                            ctl.Visible = module.SecurityLevel
                                != KdsSecurityLevel.NoPermission;
                        }
                }
                if (ctl.HasControls()) EnableControls(ctl.Controls, page);
            }
        }

        private void CreateProfile()
        {
            var profiles = new MatchNameList<UserProfile>(SecurityManager.GetProfiles());
            if (IsDebugProfileInjected)
            {
                string[] injectProfiles = 
                    ConfigurationManager.AppSettings["DebugProfile"].Split(',');
                foreach (string injProfile in injectProfiles)
                {
                    UserProfile profile = new UserProfile();
                    profile.ProfileGroup = injProfile;
                    profile.Role = profiles[profile.ProfileGroup].Role;
                    _loginUser.AddProfileToUser(profile);
                }
            }
            else
            {
                if (_loginUser.IsLimitedUser)
                {
                    UserProfile limitedProfile = profiles[LIMITED_PROFILE.ToLower()];
                    if (limitedProfile != null)
                    {
                        _loginUser.AddProfileToUser(limitedProfile);
                    }
                }
                else
                {
                    Exchange.ExchangeInfoServiceSoapClient exchangeSrv = new
                        Exchange.ExchangeInfoServiceSoapClient();
                    string[] userGroups =
                        exchangeSrv.getUserPropertyByUserName(_loginUser.UserInfo.Username,
                        "MemberOf").Split("|".ToCharArray());
                    foreach (string group in userGroups)
                    {
                        UserProfile matchProfile = profiles[group.ToLower()];
                        if (matchProfile != null)
                        {
                            _loginUser.AddProfileToUser(matchProfile);
                        }
                    }
                }
            }
            if (!_loginUser.IsProfileExists) NonAuthorizedRedirect();
        }
        private void CreateModulesList()
        {
            var modules = new MatchNameList<KdsModule>(
                SecurityManager.GetProfileModules(_loginUser.UserInfo.UserProfiles.ToArray()));
            _loginUser.SetModulesList(modules);
        }
        private void NonAuthorizedRedirect()
        {
            //HttpContext.Current.Response.Redirect(String.Format("{0}/{1}",
            //    HttpContext.Current.Request.ApplicationPath, 
            //    _kdsPage.NotAuthorizedRedirectPage));
            HttpContext.Current.Response.Redirect(String.Format("{0}/{1}",
               "~", 
                _kdsPage.NotAuthorizedRedirectPage));
           
        }
        private void AuthorizeMenus(MenuItemCollection menuItems)
        {
            //prepare list of non authorized menus
            var nonAuthorizedMenus =
                new List<MenuItem>(menuItems.Count);
            foreach (MenuItem menuItem in menuItems)
            {
                if (!String.IsNullOrEmpty(menuItem.NavigateUrl))
                {
                    KdsModule pageModule = null;
                    if (!IsPageInRole(menuItem.NavigateUrl, 
                            out pageModule))
                        nonAuthorizedMenus.Add(menuItem);
                }
                if (menuItem.ChildItems.Count > 0)
                    AuthorizeMenus(menuItem.ChildItems);
                //if parent menu has no authorized children and it doesn't have navigation url
                //remove it from menu
                if (menuItem.ChildItems.Count == 0 && String.IsNullOrEmpty(menuItem.NavigateUrl))
                    nonAuthorizedMenus.Add(menuItem);
            }

            //remove all non authorized menus from menu control
            //and clear the list
            while (nonAuthorizedMenus.Count > 0)
            {
                menuItems.Remove(nonAuthorizedMenus[0]);
                nonAuthorizedMenus.RemoveAt(0);
            }
            
        }
        public void ProvideMenuForRole(KdsPage page)
        {
            if (page.Master != null)
            {
                Menu menu = (Menu)page.Master.FindControl(page.MenuControlID);
                if (menu != null)
                {
                    AuthorizeMenus(menu.Items);
                }
            }
        }
        private bool IsPageInRole(out KdsModule pageModule)
        {
            return IsPageInRole(HttpContext.Current.Request.Path,
                out pageModule);
        }
        private bool IsPageInRole(string pageName,
            out KdsModule pageModule)
        {
            string realPage = pageName.Substring(pageName.LastIndexOf("/") + 1).ToLower();
            int qsIndex = realPage.IndexOf('?');
            if (qsIndex >= 0) realPage = realPage.Substring(0, qsIndex);
            pageModule =
                _loginUser.KdsModules[realPage];
            return pageModule != null && pageModule.ModuleType == KdsModuleType.Page && pageModule.SecurityLevel != KdsSecurityLevel.NoPermission;
        }
        public bool IsRoleExistsIn(string roles)
        {
            string[] rolesArray = roles.Split(",".ToCharArray());
            string foundRole = Array.Find<string>(rolesArray, delegate(string role)
            {
                bool found = false;
                foreach (UserProfile profile in _loginUser.UserInfo.UserProfiles)
                {
                    if (role.ToLower().Equals(profile.ProfileGroup)) found = true;
                }
                return found;
            });
            return !String.IsNullOrEmpty(foundRole);
        }
        public void CheckIfRoleExistsIn(string roles)
        {
            if (!IsRoleExistsIn(roles)) NonAuthorizedRedirect();
        }

        public static List<UserProfile> GetProfiles()
        {
            var lstProfiles = new List<UserProfile>();
            DataTable dtProfiles = new DataTable();
            clDal objDal = new clDal();
            objDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
            objDal.ExecuteSP(clGeneral.cProGetProfil, ref dtProfiles);
            if (dtProfiles != null)
            {
                foreach (DataRow dr in dtProfiles.Rows)
                {
                    var profile = new UserProfile();
                    int role;
                    if (int.TryParse(dr["kod_profil"].ToString(),
                        out role))
                    {
                        profile.Role = role;
                        profile.ProfileGroup = dr["teur_profil"].ToString().ToLower();
                        lstProfiles.Add(profile);
                    }
                    

                }
            }
            return lstProfiles;
        }
        public static List<KdsModule> GetProfileModules(UserProfile[] profiles)
        {
            var lstModule = new List<KdsModule>();
            foreach (UserProfile profile in profiles)
            {
                DataTable dtModules = new DataTable();
                clDal objDal = new clDal();
                objDal.AddParameter("p_kod_profil", ParameterType.ntOracleInteger, profile.Role, ParameterDir.pdInput);
                objDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                objDal.ExecuteSP(clGeneral.cProGetHarshaotToProfil, ref dtModules);
                if (dtModules != null)
                {
                    foreach (DataRow dr in dtModules.Rows)
                    {
                        var module = new KdsModule();
                        module.Name = dr["shem"].ToString().Trim().ToLower();                        
                        module.ModuleType = (KdsModuleType)int.Parse(dr["sug"].ToString());
                        module.SecurityLevel = (KdsSecurityLevel)int.Parse(dr["kod_harshaa"].ToString());
                        module.ModuleID = int.Parse(dr["masach_id"].ToString());
                        lstModule.Add(module);
                    }
                }
            }
            return lstModule;
        } 

        #endregion

        internal bool IsDebugProfileInjected
        {
            get 
            {
                if (ConfigurationManager.AppSettings["DebugModeProfile"] != null)
                {
                    return bool.Parse(ConfigurationManager.AppSettings["DebugModeProfile"]);
                }
                else return false;
            }
        }        
    }

    
}
