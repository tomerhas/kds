using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Mails;

namespace KDSCommon.Interfaces.Managers
{
    public interface IMailManager
    {
        void SendMessage(MailMessageWrapper message, DirectionType direction = DirectionType.Ltr);
        MailAddressCollection CreateAddressesFromAppSettings(string appSettingKey);
        MailAddressCollection CreateAddressesFromString(string mailList, char delimiter);
      
    }
}
