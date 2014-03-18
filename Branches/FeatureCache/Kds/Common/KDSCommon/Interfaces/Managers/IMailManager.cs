using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using KDSCommon.Enums;

namespace KDSCommon.Interfaces.Managers
{
    public interface IMailManager
    {
        void SendMessage(MailMessage message, DirectionType direction = DirectionType.Ltr);
        MailAddressCollection CreateAddressesFromAppSettings(string appSettingKey);
        MailAddressCollection CreateAddressesFromString(string mailList, char delimiter);
      
    }
}
