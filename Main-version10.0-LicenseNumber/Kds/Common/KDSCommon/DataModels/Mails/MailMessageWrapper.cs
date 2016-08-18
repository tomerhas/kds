using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace KDSCommon.DataModels.Mails
{
    public class MailMessageWrapper
    {
        public string  RcipientsList { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<Attachment> Attachments { get; set; }
        public MailMessageWrapper(string rcipientsList)
        {
            Attachments = new List<Attachment>();
            RcipientsList = rcipientsList;
        }
        public MailMessageWrapper(string rcipientsList, string subject)
            : this(rcipientsList)
        {
            Subject = subject; 
        }

        public MailMessageWrapper(string rcipientsList, string subject, string body)
            : this(rcipientsList,subject)
        {
            Body = body;
        }

      
    }
}
