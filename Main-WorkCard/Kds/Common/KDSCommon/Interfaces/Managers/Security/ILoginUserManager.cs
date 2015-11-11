using KDSCommon.DataModels.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSCommon.Interfaces.Managers.Security
{
    public interface ILoginUserManager
    {
        UserInfo GetLoggedUser();
    }
}
