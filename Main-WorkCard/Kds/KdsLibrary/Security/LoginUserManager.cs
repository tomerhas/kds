using KDSCommon.DataModels.Security;
using KDSCommon.Interfaces.Managers.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsLibrary.Security
{
    /// <summary>
    /// This class is ment to replace the static class LoginUser which cannot be replaced currently since it is used widley in the app
    /// </summary>
    public class LoginUserManager : ILoginUserManager
    {
        private LoginUser _connectedUser;

        public UserInfo GetLoggedUser()
        {
            if (_connectedUser != null)
                return _connectedUser.UserInfo;
            _connectedUser = LoginUser.GetLoginUser();
            if (_connectedUser != null)
            {
                return _connectedUser.UserInfo;
            }
            else
            {
                return null;
            }
        }
    }
}
