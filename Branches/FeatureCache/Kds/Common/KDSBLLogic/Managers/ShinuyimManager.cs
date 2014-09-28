using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using KDSBLLogic.DAL;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.Enums;
using KDSCommon.Interfaces.DAL;
using KDSCommon.Interfaces.Managers;
using KDSCommon.UDT;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using ObjectCompare;
using KDSCommon.DataModels.Mails;

namespace KDSBLLogic.Managers
{
    public class ShinuyimManager : IShinuyimManager
    {
        private IUnityContainer _container;

        public ShinuyimManager(IUnityContainer container)
        {
            _container = container;
        }

        public DataTable GetIdkuneyRashemet(int iMisparIshi, DateTime dTaarich)
        {
            return _container.Resolve<IShinuyimDAL>().GetIdkuneyRashemet(iMisparIshi, dTaarich);
        }
        public DataTable GetApprovalErrors(int iMisparIshi, DateTime dTaarich)
        {
            return _container.Resolve<IShinuyimDAL>().GetApprovalErrors(iMisparIshi, dTaarich);
        }

        public void SaveIdkunRashemet(COLL_IDKUN_RASHEMET oCollIdkunRashemet)
        {
            try
            {
                _container.Resolve<IShinuyimDAL>().SaveIdkunRashemet(oCollIdkunRashemet);
            }
            catch (Exception ex)
            {
                string rcipientsList = ConfigurationManager.AppSettings["MailErrorWorkCard"];

                var mailManager = ServiceLocator.Current.GetInstance<IMailManager>();
                string subject = "תקלה בשמירת עדכוני רשמת למספר אישי: " + oCollIdkunRashemet.Value[0].MISPAR_ISHI + "  תאריך:" + oCollIdkunRashemet.Value[0].TAARICH.ToShortDateString();

                mailManager.SendMessage(new MailMessageWrapper(rcipientsList)
                {
                    Subject =subject,
                    Body = ex.Message
                });
            }

        }
        public void DeleteIdkunRashemet(COLL_IDKUN_RASHEMET oCollIdkunRashemetDel)
        {
            try
            {
                _container.Resolve<IShinuyimDAL>().DeleteIdkunRashemet(oCollIdkunRashemetDel);
            }
            catch (Exception ex)
            {
                string rcipientsList = ConfigurationManager.AppSettings["MailErrorWorkCard"];

                var mailManager = ServiceLocator.Current.GetInstance<IMailManager>();
                string subject = "תקלה במחיקת עדכוני רשמת למספר אישי: " + oCollIdkunRashemetDel.Value[0].MISPAR_ISHI + "  תאריך:" + oCollIdkunRashemetDel.Value[0].TAARICH.ToShortDateString();

                mailManager.SendMessage(new MailMessageWrapper(rcipientsList)
                {
                    Subject = subject,
                    Body = ex.Message
                });
            }

        }
        public void UpdateAprrovalErrors(COLL_SHGIOT_MEUSHAROT oCollShgiotMeusharot)
        {
            _container.Resolve<IShinuyimDAL>().UpdateAprrovalErrors(oCollShgiotMeusharot);
        }

        public void SaveShinuyKelet(ShinuyInputData inputData)
        {
            try
            {
                var oraYamimCollUpd = GetYemeyAvodaColl(inputData.oCollYameyAvodaUpdRecorder);
                var oraSidurimCollUpd = GetYemeyAvodaColl(inputData.oCollSidurimOvdimUpdRecorder);
                var oraPeilutCollUpd = GetPeilutAvodaColl(inputData.oCollPeilutOvdimUpdRecorder);
                _container.Resolve<IShinuyimDAL>().SaveShinuyKelet(inputData, oraYamimCollUpd, oraSidurimCollUpd, oraPeilutCollUpd);
            }
            catch (Exception ex)
            {
                IMailManager mailManager = _container.Resolve<IMailManager>();
               // string from = ConfigurationManager.AppSettings["NoRep"].ToString();
                string RecipientsList = ConfigurationManager.AppSettings["MailErrorWorkCard"];
                string subject = "תקלה בשמירת נתונים למספר אישי: " + inputData.iMisparIshi + "  תאריך:" + inputData.CardDate;
                mailManager.SendMessage(new MailMessageWrapper(RecipientsList) { Subject = subject, Body = ex.Message });
                throw ex;
            }

            //IMailManager mail = _container.Resolve<IMailManager>();
            //mail.attachFile("hhhh");
            //string zzz = "meravn@walla.com";
            //string[] RecipientsList = (ConfigurationManager.AppSettings["MailErrorWorkCard"].ToString()).Split(';');
            //mail.SendMail(new[]{ zzz}, "תקלה בשמירת נתונים למספר אישי: " + inputData.iMisparIshi + "  תאריך:" + inputData.CardDate, "ex.Message");

           
        }

        private COLL_YAMEY_AVODA_OVDIM GetYemeyAvodaColl(ModificationRecorderCollection<OBJ_YAMEY_AVODA_OVDIM> oCollYameyAvodaUpdRecorder)
        {
            COLL_YAMEY_AVODA_OVDIM oraColl = new COLL_YAMEY_AVODA_OVDIM();
            oCollYameyAvodaUpdRecorder.ForEach(x =>
            {
                //First filter out all items that their update object is not = to 1 and then filter items that have not been modified internaly
                Func<OBJ_YAMEY_AVODA_OVDIM, bool> predicate = y => y.UPDATE_OBJECT == 1;
                if (x.HasChanged(predicate))
                {
                    oraColl.Add(x.ContainedItem);
                }
            });
            return oraColl;
        }

        private COLL_SIDURIM_OVDIM GetYemeyAvodaColl(ModificationRecorderCollection<OBJ_SIDURIM_OVDIM> oCollSidurimOvdimUpdRecorder)
        {
            COLL_SIDURIM_OVDIM oraColl = new COLL_SIDURIM_OVDIM();
            oCollSidurimOvdimUpdRecorder.ForEach(x =>
            {
                //First filter out all items that their update object is not = to 1 and then filter items that have not been modified internaly
                Func<OBJ_SIDURIM_OVDIM, bool> predicate = y => y.UPDATE_OBJECT == 1;
                if (x.HasChanged(predicate))
                {
                    oraColl.Add(x.ContainedItem);
                }
            });
            return oraColl;
        }

        private COLL_OBJ_PEILUT_OVDIM GetPeilutAvodaColl(ModificationRecorderCollection<OBJ_PEILUT_OVDIM> oCollPeilutOvdimUpdRecorder)
        {
            COLL_OBJ_PEILUT_OVDIM oraColl = new COLL_OBJ_PEILUT_OVDIM();
            oCollPeilutOvdimUpdRecorder.ForEach(x =>
            {
                //First filter out all items that their update object is not = to 1 and then filter items that have not been modified internaly
                Func<OBJ_PEILUT_OVDIM, bool> predicate = y => y.UPDATE_OBJECT == 1;
                if (x.HasChanged(predicate))
                {
                    oraColl.Add(x.ContainedItem);
                }
            });
            return oraColl;
        }
    }
}
   