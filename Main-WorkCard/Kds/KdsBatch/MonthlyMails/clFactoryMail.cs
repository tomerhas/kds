using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary;
using KdsLibrary.Utils;
using KdsLibrary.Utils.Reports;
using System.IO;
using KdsLibrary.UI;
using System.Configuration;
using KdsLibrary.Security;
using System.Windows.Forms;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.Interfaces.Managers;
using KDSCommon.Interfaces.Logs;
using System.Net.Mail;
using KDSCommon.DataModels.Mails;

namespace KdsBatch.MonthlyMails
{
    public abstract class clFactoryMail
    {
        protected List<Mail> _Mails;
        protected clGeneral.enStatusRequest _StatusProces;
        protected clGeneral.enMouthlyMailsType _enTypeMail;
        protected DataTable _dtDetailsMails;
        protected KdsLibrary.BL.clReport _BlReport;

        public clFactoryMail()
        {
            _BlReport = KdsLibrary.BL.clReport.GetInstance();
        }

        public void SendMails(long iRequestId)
        {
             string path;
             FileStream fs;
             try{
                path = ConfigurationSettings.AppSettings["PathFileReports"];
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                foreach (Mail eMail in _Mails)
                {
                    fs = new FileStream(path + eMail.file_name, FileMode.Create, FileAccess.Write);
                    fs.Write(eMail.file, 0, eMail.file.Length);
                    fs.Flush();
                    fs.Close();

                    SendMail(path + eMail.file_name, eMail.Email, eMail.mail_subject);
                    File.Delete(path + eMail.file_name);
                }

                clDefinitions.UpdateLogBakasha(iRequestId, DateTime.Now, _StatusProces.GetHashCode());
            }
            catch (Exception ex)
            {
                clGeneral.LogMessage(ex.Message, System.Diagnostics.EventLogEntryType.Error, true);
                clDefinitions.UpdateLogBakasha(iRequestId, DateTime.Now, clGeneral.enStatusRequest.Failure.GetHashCode());
                ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(iRequestId, "E", 0, "Send Mails Fail");
               
            }
        }

        protected void SendMail(string Path, string eMail, string teur)
        {
            try
            {
                var mailManager = ServiceLocator.Current.GetInstance<IMailManager>();

                MailMessageWrapper message = new MailMessageWrapper(eMail) { Subject = teur };
                message.Attachments.Add(new Attachment(Path));
                
                mailManager.SendMessage(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected abstract void GetDataFromDb();
        protected abstract void CreateMails();
    }
}
