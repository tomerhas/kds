using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary.BL;
using KdsLibrary.UDT;
using KdsLibrary.DAL;

namespace KdsBatch.HrWorkersChanges
{
    public class clMain
    {
        clBatch obatch;
        clDal oDal;
        KdsBatch.HrWorkersChanges.clManager obClManager;
        bool flag;

        public clMain() 
        {
            obatch = new clBatch();
            oDal = new clDal();
        }
    
        public void HafalatBatchShinuyimHR()
        {
           // obatch.KdsWriteProcessLog(3, 31, 1, "start matzav - merav");
            HafalatMatzavOvdim();
           // obatch.KdsWriteProcessLog(3, 32, 1, "start pirtey-oved - merav");
            HafalatPirteyOved();
           // obatch.KdsWriteProcessLog(3, 33, 1, "start meafyenim - merav");
          //  HafalatMeafyenim();
          //  obatch.KdsWriteProcessLog(3, 34, 1, "start brerot - merav");
            HafalatBrerotMechdal();
          //  obatch.KdsWriteProcessLog(3, 34, 1, "end brerot - merav");
        }
        public void HafalatShinuyimHRatMeafyenim()
        {
            HafalatMeafyenim();
        }

        private void HafalatMatzavOvdim()
        {
            try
            {
                //matzav ovdim
                obatch.KdsWriteProcessLog(3, 31, 1, "start bdika shinuyim hr matzav ovdim");
                flag = true;
                obClManager = new KdsBatch.HrWorkersChanges.clManager(KdsBatch.HrWorkersChanges.TableType.State, ref flag);
                obClManager.InsertPeriod();
                obClManager.SaveShinuyimHR(ref flag, "matzav_ovdim");
                obatch.KdsWriteProcessLog(3, 31, 1, "end bdika shinuyim hr matzav ovdim");
                if (flag)
                {
                    //sub_tahalich = 31
                    obatch.KdsWriteProcessLog(3, 31, 1, "start upd matzav");
                //    obatch.MoveNewMatzavOvdimToOld();
                    obatch.KdsWriteProcessLog(3, 31, 2, "end ok upd matzav");
                }
                obClManager = null;
            }
            catch (Exception ex)
            {
                obClManager = null;
                obatch.KdsWriteProcessLog(3, 31, 1, "Matzav Ovdim Err:" + ex.Message);
                //KdsLibrary.clGeneral.LogMessage("matzav ovdim:" + ex.Message,System.Diagnostics.EventLogEntryType.Error);
            } 
        }

        private void HafalatPirteyOved()
        {
            try
            {
                // pirtey oved
                obatch.KdsWriteProcessLog(3, 32, 1, "start bdika shinuyim hr pirtey oved");
                flag = true;
                obClManager = new KdsBatch.HrWorkersChanges.clManager(KdsBatch.HrWorkersChanges.TableType.Details, ref flag);
                obClManager.InsertPeriod();
                obClManager.SaveShinuyimHR(ref flag, "pirtey_oved");
                obatch.KdsWriteProcessLog(3, 32, 1, "end bdika shinuyim hr pirtey oved");
                if (flag)
                {
                    //sub_tahalich = 326
                    obatch.KdsWriteProcessLog(3, 32, 1, "start upd pirtey_ovdim");
                 //   obatch.MoveNewPirteyOvedToOld();
                    obatch.KdsWriteProcessLog(3, 32, 2, "end ok upd pirtey_ovdim");
                }
                obClManager = null;
            }
            catch (Exception ex)
            {
                obClManager = null;
                obatch.KdsWriteProcessLog(3, 32, 1, "Pirtey Oved Err:" + ex.Message);
            //    KdsLibrary.clGeneral.LogMessage("pirtey oved:" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
        }


        private void HafalatMeafyenim()
        {
            try
            {
                //meafyenim
                obatch.KdsWriteProcessLog(3, 33, 1, "start bdika shinuyim hr meafyenim");
                flag = true;
                obClManager = new KdsBatch.HrWorkersChanges.clManager(KdsBatch.HrWorkersChanges.TableType.Properties,ref flag);
              //  obatch.KdsWriteProcessLog(3, 33, 1, "before InsertPeriod meafyenim");
                obClManager.InsertPeriod();
               // obatch.KdsWriteProcessLog(3, 33, 1, "before SaveShinuyimHR meafyenim");
                obClManager.SaveShinuyimHR(ref flag, "meafyenim");
                obatch.KdsWriteProcessLog(3, 33, 1, "end bdika shinuyim hr meafyenim");
                if (flag)
                {
                    //sub_tahalich = 33
                    obatch.KdsWriteProcessLog(3, 33, 1, "start upd meafyenim_ovdim");
                //    obatch.MoveNewMeafyenimOvdimToOld();
                    obatch.KdsWriteProcessLog(3, 33, 2, "end ok upd meafyenim_ovdim");
                }
                obClManager = null;
                obatch.KdsWriteProcessLog(3, 33, 1, "sof meafyenim_ovdim");
            }
            catch (Exception ex)
            {
                obClManager = null;
                obatch.KdsWriteProcessLog(3, 33, 1, "upd Meafyenim Err:" + ex.Message);
          //    KdsLibrary.clGeneral.LogMessage("meafyenim:" + ex.Message,System.Diagnostics.EventLogEntryType.Error);
            }
        }

        private void HafalatBrerotMechdal()
        {
            try
            {
                // brerot mechdal
                obatch.KdsWriteProcessLog(3, 34, 1, "start bdika shinuyim hr brerot mechdal");
                flag = true;
                obClManager = new KdsBatch.HrWorkersChanges.clManager(KdsBatch.HrWorkersChanges.TableType.Defaults, ref flag);
                obClManager.TipulTableDefaults(ref flag);
                obatch.KdsWriteProcessLog(3, 34, 1, "end bdika shinuyim hr brerot mechdal");
                if (flag)
                {
                    //sub_tahalich = 34
                    obatch.KdsWriteProcessLog(3, 34, 1, "start upd Brerot_Mechdal_Meafyenim");
                  //  obatch.MoveNewBrerotMechdalToOld();
                    obatch.KdsWriteProcessLog(3, 34, 2, "end ok upd Brerot_Mechdal_Meafyenim");
                }
                obClManager = null;
            }
            catch (Exception ex)
            {
                obClManager = null;
                obatch.KdsWriteProcessLog(3, 34, 1, "upd Brerot Mechdal Err:" + ex.Message);
              //  KdsLibrary.clGeneral.LogMessage("brerot mechdal:" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
        }
    }
}
