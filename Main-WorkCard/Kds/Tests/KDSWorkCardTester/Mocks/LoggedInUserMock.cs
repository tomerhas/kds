using KDSCommon.DataModels.Security;
using KDSCommon.Interfaces.Managers.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KDSWorkCardTester.Mocks
{
    public class LoggedInUserMock : ILoginUserManager
    {
        public UserInfo GetLoggedUser()
        {
            UserInfo user = new UserInfo();
            user.UserProfiles.Add(new UserProfile() { Role = 2, ProfileGroup = "kds_rashemet" });
            user.UserProfiles.Add(new UserProfile() { Role = 1, ProfileGroup = "kds_administrators" });
            user.UserProfiles.Add(new UserProfile() { Role = 9, ProfileGroup = "kds_MenahelBameshek" });

            return user;
        }
    }
}
