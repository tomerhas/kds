using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using KDSCommon.Enums;
using System.Configuration;
using System.Web;
using KDSCommon.Interfaces.Managers;

namespace KDSBLLogic.Managers
{
    public class MailManager : IMailManager
    {
        private SmtpClient client;
        private MailAddress _mailFromAddress;
        public MailManager()
        {
            SetSMTP();
            InitFromMail();
        }

        private void InitFromMail()
        {
            var displayNameServer = ConfigurationManager.AppSettings["DisplayNameServer"];
            var fromMail = displayNameServer + "<" + ConfigurationManager.AppSettings["NoRep"].ToString() + ">";
            _mailFromAddress = new MailAddress(fromMail);
        }


        private void SetSMTP()
        {
            var username = ConfigurationManager.AppSettings["UserMail"].ToString();
            var password = ConfigurationManager.AppSettings["PasswordMail"].ToString();
            var host = ConfigurationManager.AppSettings["SmtpHost"];
            client = new SmtpClient(host);
            client.Credentials = new System.Net.NetworkCredential(username, password);
           
           // _attachFilePath = "";
        }

        public void SendMessage(MailMessage message, DirectionType direction= DirectionType.Ltr)
        {
            if (direction == DirectionType.Rtl)
            {
                message.Body = "<div dir='rtl' style='Text-align:Right'>" + message.Body + "</Div>";
            }
            message.Body = message.Body.Replace("\n", "<br/>");
            message.From = _mailFromAddress;
            client.Send(message);
        }

        public MailAddressCollection CreateAddressesFromAppSettings(string appSettingKey)
        {
            string RecipientsList = ConfigurationManager.AppSettings[appSettingKey];
            return CreateAddressesFromString(RecipientsList, ';');
        }

        public MailAddressCollection CreateAddressesFromString(string mailList, char delimiter)
        {
            string[] RecipientsList = mailList.Split(delimiter);
            MailAddressCollection collection = new MailAddressCollection();
            foreach (var recipt in RecipientsList)
            {
                collection.Add(recipt);
            }
            return collection;
        }

        //public void SendMail(string[] To, string Subject, string Body, DirectionType? Direction = null, bool? IsHtmlBody = false)
        //{
        //    Init();

        //    To.ToList().ForEach(recipient =>
        //    {
        //        SetMessageDetails(Subject, Body, recipient, IsHtmlBody.Value, Direction);
        //        SendMail();
        //    });
          
        //}

        //private void SetMessageDetails(string Subject, string Body, string recipient, bool IsHtmlBody, DirectionType? Direction = null)
        //{
        //    _mailToAddress = new MailAddress(recipient);
        //    message = new MailMessage(_mailFromAddress, _mailToAddress);
        //    if (Direction.HasValue)
        //        _Direction = Direction.Value;
        //    message.IsBodyHtml = IsHtmlBody;

        //    if (_attachFilePath != "")
        //    {
        //        Attachment attached = new Attachment(_attachFilePath);
        //        message.Attachments.Add(attached);
        //    }

        //    message.Subject = Subject;
        //    message.Body = (_Direction == DirectionType.Rtl) ? "<div dir='rtl' style='Text-align:Right'>" + Body + "</Div>" : Body;
        //    message.Body = message.Body.Replace("\n", "<br/>");
        //}

        //public void SendMail(string To, string Subject, string Body)
        //{
        //    Init(To, Subject);
        //    message.Body = (_Direction == DirectionType.Rtl) ? "<div dir='rtl' style='Text-align:Right'>" + Body + "</Div>" : Body;
        //    message.Body = message.Body.Replace("\n", "<br/>");

        //    SendMail();
        //}

        //public void attachFile(string Path)
        //{
        //    _attachFilePath = Path;
        //    //Attachment attached = new Attachment(Path);
        //    //message.Attachments.Add(attached);
        //}

        //private void SendMail()
        //{
        //    client.Send(message);
        //}

        //public void IsHtmlBody(bool isHtml)
        //{
        //    message.IsBodyHtml = isHtml;
        //}

        //#region IDisposable Members

        //private void Dispose()
        //{
        //    message.Dispose();
        //}

       // #endregion
    }

//    [XmlRoot("mail")]
//    public class ReportMail
//    {
//        [XmlAttribute("divFolder")]
//        public string divFolder { get; set; }
//        [XmlAttribute("FolderPath")]
//        public string FolderPath { get; set; }
//        [XmlAttribute("NameFolder")]
//        public string NameFolder { get; set; }


//        public string GetMessageBody(string folderName)
//        {
//            string Path;
//            FolderPath = folderName;
//            NameFolder = folderName.Split('/')[folderName.Split('/').Length - 2];
//            string serialized = KdsLibrary.Utils.KdsExtensions.SerializeObject(this);

//            //XsltArgumentList xslArgs = new XsltArgumentList();
//            //xslArgs.AddParam("FolderPath","", folderName);
//#if DEBUG
//            Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Visual Studio 2008\Projects\Kds\KdsBatch\Reports\";
//#else
//            Path = AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\";           
//#endif
//            return KdsLibrary.Utils.KdsExtensions.TransformXml(serialized, Path + "ReportMail.xsl");
//        }
//    }
}
