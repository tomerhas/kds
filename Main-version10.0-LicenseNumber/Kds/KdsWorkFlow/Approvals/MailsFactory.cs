using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Xml.Serialization;
using System.Net.Mail;
using System.Xml.Xsl;
using System.DirectoryServices;
using DalOraInfra.DAL;

namespace KdsWorkFlow.Approvals
{
    public abstract class ApprovalMailSender
    {
        protected ApprovalMailSetup _mailSetup = new ApprovalMailSetup();
        protected virtual string FromAddress
        {
            get { return _mailSetup.AdminAddress; }
        }
        protected virtual void Send(IApprovalMail mail)
        {
            if (mail != null)
            {
                System.Net.Mail.MailMessage mailMessage =
                    new System.Net.Mail.MailMessage(new MailAddress(FromAddress,
                        _mailSetup.DisplayName), new MailAddress(mail.Email));
                mailMessage.Subject = mail.Subject;
                LinkedResource objLinkedRes = new LinkedResource(
                    _mailSetup.Path + "head.jpg",
                    "image/jpeg");
                objLinkedRes.ContentId = "head";
                AlternateView objHTLMAltView = AlternateView.CreateAlternateViewFromString(
                   mail.GetMessageBody(_mailSetup), System.Text.Encoding.GetEncoding(1255),
                    "text/html");
                objHTLMAltView.LinkedResources.Add(objLinkedRes);
                mailMessage.AlternateViews.Add(objHTLMAltView);
                AlternateView plainView = AlternateView.CreateAlternateViewFromString(
                    mail.AlternateBody, null, "text/plain");
                mailMessage.AlternateViews.Add(plainView);
                mailMessage.IsBodyHtml = true;
                _mailSetup.Smtp.Send(mailMessage);
            }
        }
    }
    public class MailsFactory : ApprovalMailSender
    {
        private long _btchRequest;

        public MailsFactory(long btchRequest)
        {
            _btchRequest = btchRequest;
        }

        public void SendApprovalMails()
        {
            DataTable dtMails = GetMailsFromLDAP();
            SendMailsToApprovalFactors(true, dtMails);
            SendMailsToApprovalFactors(false, dtMails);
        }

        private void SendMailsToApprovalFactors(bool forMainFactor, DataTable dtMails)
        {
            int managerNumber = -1;
            DataTable dt = GetData(forMainFactor);
            ApprovalMail mail = null;
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    int tmpManager = Convert.ToInt32(dr["gorem_measher"]);
                    if (managerNumber != tmpManager)
                    {
                        managerNumber = tmpManager;
                        SendMail(mail);
                        mail = new ApprovalMail();
                        mail.ManagerNumber = managerNumber;
                        mail.Firstname = dr["shem_prat"].ToString();
                        mail.Lastname = dr["shem_mish"].ToString();
                        mail.Email = GetMail(managerNumber, dtMails);
                        mail.Subject = _mailSetup.Subject;
                    }
                    ApprovalMailEntry entry = new ApprovalMailEntry();
                    entry.PendingApprovals = Convert.ToInt32(dr["approvals_count"]);
                    entry.Month = dr["approvals_month"].ToString();
                    mail.MailEntries.Add(entry);
                }
                SendMail(mail);
            }
        }

        private DataTable GetData(bool forMainFactor)
        {
            try
            {
                clDal dal = new clDal();
                DataTable dt = new DataTable();
                dal.AddParameter("p_rashi", ParameterType.ntOracleInteger,
                    forMainFactor ? 1 : 0, ParameterDir.pdInput);
                dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor,
                    null, ParameterDir.pdOutput);
                dal.ExecuteSP("PKG_APPROVALS.get_pending_approvals", ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                ApprovalFactory.LogErrorOfApprovalBatchPrc(_btchRequest, "W", ex.ToString());
                return null;
            }
        }

        private void SendMail(ApprovalMail mail)
        {
            try
            {
                Send(mail);
            }
            catch (Exception ex)
            {
                ApprovalFactory.LogErrorOfApprovalBatchPrc(_btchRequest, "W", ex.ToString());
            }
        }

        private DataTable GetMailsFromLDAP()
        {
            DataTable dtMails = new DataTable();
            dtMails.Columns.Add("EmployeeNumber", typeof(Int32));
            dtMails.Columns.Add("Mail", typeof(String));
            try
            {
                DirectoryEntry entry = GetDirectoryEntry();
                DirectorySearcher searcher = new DirectorySearcher(entry);
                searcher.SearchScope = SearchScope.Subtree;
                searcher.Filter = "(&(objectClass=user))";
                searcher.PropertiesToLoad.Add("EmployeeID");
                searcher.PropertiesToLoad.Add("Mail");
                SearchResultCollection results = searcher.FindAll();
                foreach (SearchResult result in results)
                {
                    if (result.Properties["EmployeeID"].Count > 0 &&
                        result.Properties["Mail"].Count > 0)
                    {
                        DataRow dr = dtMails.NewRow();
                        dr["EmployeeNumber"] =
                            int.Parse(result.Properties["EmployeeID"][0].ToString());
                        dr["Mail"] = result.Properties["Mail"][0].ToString();
                        dtMails.Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                ApprovalFactory.LogErrorOfApprovalBatchPrc(_btchRequest, "W", ex.ToString());
            }
            return dtMails;
        }

        private DirectoryEntry GetDirectoryEntry()
        {
            DirectoryEntry de = new DirectoryEntry();
            de.Path = ConfigurationManager.AppSettings["LDAP"];
            de.AuthenticationType = AuthenticationTypes.None;
            return de;
        }

        private string GetMail(int managerNumber, DataTable dtMails)
        {
            string email = String.Empty;
            if (dtMails != null)
            {
                DataRow[] rows =
                    dtMails.Select(String.Format("EmployeeNumber={0}", managerNumber));
                if (rows.Length > 0)
                    email = rows.First()["Mail"].ToString();
            }
            return email;
        }

    }

    public class NotificationSender : ApprovalMailSender
    {
        public void SendNotification(IApprovalMail mail)
        {
            if (String.IsNullOrEmpty(mail.Email))
                mail.Email = FromAddress;
            try
            {
                Send(mail);
            }
            catch (Exception ex)
            {
                KdsLibrary.clGeneral.LogMessage(ex.ToString(),
                    System.Diagnostics.EventLogEntryType.Warning);
            }
        }

        protected override string FromAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["tammy"];
            }
        }
    }

    public interface IApprovalMail
    {
        string GetMessageBody(ApprovalMailSetup mailSetup);
        string AlternateBody { get; }
        string Email { get; set; }
        string Subject { get; }
    }
    public class ApprovalMailSetup
    {
        public ApprovalMailSetup()
        {
            AdminAddress = ConfigurationManager.AppSettings["AdminAddress"];
            DisplayName = ConfigurationManager.AppSettings["DisplayName"];
            Subject = ConfigurationManager.AppSettings["Subject"];
            Smtp = new SmtpClient(
                    ConfigurationManager.AppSettings["SmtpHost"]);
            Url = ConfigurationManager.AppSettings["ApprovalsPage"];
            //AdminAddress = "gregorip@egged.co.il";
            //DisplayName = "Mail Master";
            //Subject = "Hello!!!!!";
            //Smtp = new SmtpClient(
            //        "Mail01");
//#if DEBUG
//            Path = @"C:\Documents and Settings\GregoriP\My Documents\Visual Studio 2008\Projects\Kds\KdsWorkFlow\Resources\";
//            Url = "http://localhost:3687/Kds/Modules/Approvals/Approvals.aspx";
            
//#else
            Path = AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\";           
//#endif
        }
        
        public string AdminAddress { get; set; }
        public SmtpClient Smtp { get; set; }
        public string DisplayName { get; set; }
        public string Subject { get; set; }
        public string Path { get; set; }
        public string Url { get; set; }
    }

    [XmlRoot("mail")]
    public class ApprovalMail : IApprovalMail
    {
        public ApprovalMail()
        {
            MailEntries = new List<ApprovalMailEntry>();
        }
        [XmlAttribute("main")]
        public bool IsMainFactor { get; set; } 
        public int ManagerNumber { get; set; }
        [XmlAttribute("firstname")]
        public string Firstname { get; set; }
        [XmlAttribute("lastname")]
        public string Lastname { get; set; }
        public string Email { get; set; }
        [XmlElement("entry", typeof(ApprovalMailEntry))]
        public List<ApprovalMailEntry> MailEntries { get; set; }

        public string GetMessageBody(ApprovalMailSetup mailSetup)
        {
            string serialized = KdsLibrary.Utils.KdsExtensions.SerializeObject(this);
            XsltArgumentList xslArgs = new XsltArgumentList();
            xslArgs.AddParam("adminAddress", "", mailSetup.AdminAddress);
            xslArgs.AddParam("subject", "", mailSetup.Subject);
            xslArgs.AddParam("url", "", mailSetup.Url);

            return KdsLibrary.Utils.KdsExtensions.TransformXml(serialized, mailSetup.Path 
                + "ApprovalMail.xsl",
                xslArgs);
        }

        public string AlternateBody
        {
            get { return "Approvals"; }
        }
        public string Subject { get; set; }
    }

    [XmlRoot("mail")]
    public class InvalidHrValuesMail : IApprovalMail
    {
        public InvalidHrValuesMail()
        {
        }
        public InvalidHrValuesMail(ApprovalKey approvalKey, string alternateMessage)
        {
            ApprovalKey = approvalKey;
            AlternateBody = alternateMessage;
            Subject = "HR";
        }
        public string GetMessageBody(ApprovalMailSetup mailSetup)
        {
            string serialized = KdsLibrary.Utils.KdsExtensions.SerializeObject(this);
           
            return KdsLibrary.Utils.KdsExtensions.TransformXml(serialized, mailSetup.Path
                + "InvalidHrMail.xsl", null);
        }
        [XmlIgnore]
        public string AlternateBody { get; set; }
        

        public string Email { get; set; }
        public string Subject { get; set; }
        [XmlAttribute("employeeNumber")]
        public string EmployeeNumber
        {
            get { return ApprovalKey.Employee.EmployeeNumber; }
            set { }
        }
        [XmlAttribute("workDate")]
        public string WorkDate
        {
            get { return ApprovalKey.WorkCard.WorkDate.ToString("dd/MM/yyyy"); }
            set { }
        }
        [XmlAttribute("code")]
        public int ApprovalCode
        {
            get { return ApprovalKey.Approval.Code; }
            set { }
        }
        [XmlAttribute("jobCode")]
        public int JobCode
        {
            get { return ApprovalKey.Approval.JobCode; }
            set { }
        }
        [XmlIgnore]
        public ApprovalKey ApprovalKey { get; set; }
        
    }
    public class ApprovalMailEntry
    {
        [XmlAttribute("approvals")]
        public int PendingApprovals { get; set; }
        [XmlAttribute("month")]
        public string Month { get; set; }
    }
}
