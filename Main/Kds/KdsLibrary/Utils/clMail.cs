using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Xsl;
using System.Xml.Serialization;

namespace KdsLibrary.Utils
{
    public class clMail : IDisposable
    {
        private MailMessage message;
        private MailAddress _mailFromAddress, _mailToAddress;
        private string _FromMail;
        private string _host;
        private string _DisplayNameServer; 

        public clMail(string To, string Subject, string Body)
        {
            _FromMail = ConfigurationSettings.AppSettings["NoRep"];
            _host = ConfigurationSettings.AppSettings["SmtpHost"];
            _DisplayNameServer = ConfigurationSettings.AppSettings["DisplayNameServer"];
            _mailFromAddress = new MailAddress(_FromMail, _DisplayNameServer);
            _mailToAddress = new MailAddress(To);
            message = new MailMessage(_mailFromAddress, _mailToAddress);
            message.Subject = Subject;
            message.IsBodyHtml = true;
            message.Body = Body;
        }

        
        public void attachFile(string Path)
        {
            Attachment attached = new Attachment(Path); 
            message.Attachments.Add(attached);
        }

        public void SendMail()
        {
            SmtpClient client = new SmtpClient(_host);
            client.UseDefaultCredentials = true;
            client.Send(message);
        }
        public void IsHtmlBody(bool isHtml)
        {
            message.IsBodyHtml = isHtml;
        }

        #region IDisposable Members

        public void Dispose()
        {
            message.Dispose();
        }

        #endregion
    }

    [XmlRoot("mail")]
    public class ReportMail
    {
        [XmlAttribute("divFolder")]
        public string divFolder { get; set; }
        [XmlAttribute("FolderPath")]
        public string FolderPath { get; set; }
        [XmlAttribute("NameFolder")]
        public string NameFolder { get; set; }


       public string GetMessageBody(string folderName)
        {
            string Path;
            FolderPath = folderName;
            NameFolder = folderName.Split('/')[folderName.Split('/').Length - 2];
            string serialized = KdsLibrary.Utils.KdsExtensions.SerializeObject(this);
          
            //XsltArgumentList xslArgs = new XsltArgumentList();
            //xslArgs.AddParam("FolderPath","", folderName);
#if DEBUG
            Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Visual Studio 2008\Projects\Kds\KdsBatch\Reports\";
#else
            Path = AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\";           
#endif
            return KdsLibrary.Utils.KdsExtensions.TransformXml(serialized, Path + "ReportMail.xsl");
        }
    }
}
