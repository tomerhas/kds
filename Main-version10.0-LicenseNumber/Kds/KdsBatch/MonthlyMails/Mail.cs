using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsBatch.MonthlyMails
{
    public class Mail
    {
        private int _MisparIshi;
        private long _BakashaId;
        private DateTime _month;
        private string _sMailSub;
        private string _sFileName;
        private string _Email;
      //  private int _SugChishuv;
        private byte[] _pFile;

      
        public int mispar_ishi
        {
            get { return _MisparIshi; }
        }

        public long bakasha_id
        {
            get { return _BakashaId; }
        }

        public string  mail_subject
        {
            get { return _sMailSub; }
        }

        public string file_name
        {
            get { return _sFileName; }
        }

        public string Email
        {
            get { return _Email; }
        }
        
        public byte[] file
        {
            get { return _pFile; }
        }

        public DateTime month
        {
            get { return _month; }
        }
        
        public Mail(int p_mispar_ishi,long p_bakasha_id,DateTime p_month,string p_email,byte[] p_file)
        {
            _MisparIshi = p_mispar_ishi;
            _BakashaId = p_bakasha_id;
            _month = p_month;
            _Email = p_email;
            _sMailSub = "  ריכוזים ל  " + _month.ToString("MM/yyyy");
            _sFileName = "Rikuz_" + _month.Year + _month.Month.ToString().PadLeft(2, '0') + _MisparIshi.ToString().PadLeft(5, '0') +".pdf";
            _pFile = p_file;
        }
    }
}
