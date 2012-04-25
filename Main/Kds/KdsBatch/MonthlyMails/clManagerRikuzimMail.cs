using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary;
using KdsLibrary.Utils;
using KdsLibrary.Utils.Reports;
using System.IO;
using KdsLibrary.UI;
using KdsLibrary.BL;
using System.Configuration;
using KdsLibrary.Security;
using System.Windows.Forms;


namespace KdsBatch.MonthlyMails
{
    public class clManagerRikuzimMail : clFactoryMail 
    {
        private long _lBakashaIdForRikuzim;
        protected KdsLibrary.BL.clReport _BlReport;

        public clManagerRikuzimMail(long iRequestId, long iRequestIdForRikuzim)
        {
             
            _enTypeMail = clGeneral.enMouthlyMailsType.Rikuzim;
            _lBakashaIdForRikuzim = iRequestIdForRikuzim;
            _StatusProces = clGeneral.enStatusRequest.ToBeEnded;

            _Mails = new List<Mail>();
            _dtDetailsMails = new DataTable();
            GetDataFromDb();
            CreateMails();
        }


        protected override void GetDataFromDb()
        {
            _dtDetailsMails = _BlReport.getEmailOvdimLeRikuzim(_lBakashaIdForRikuzim);
        }

        protected override void CreateMails()
        {
            try
            {
                foreach (DataRow dr in _dtDetailsMails.Rows)
                {
                    _Mails.Add(new Mail(clGeneral.GetIntegerValue(dr["MISPAR_ISHI"].ToString()), 
                                        _lBakashaIdForRikuzim,
                                        DateTime.Parse(dr["TAARICH"].ToString()), 
                                        dr["EMAIL"].ToString() ,
                                        (byte[])(dr["RIKUZ_PDF"]) ));
                                 //int.Parse(dr["sug_chishuv"].ToString())));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
