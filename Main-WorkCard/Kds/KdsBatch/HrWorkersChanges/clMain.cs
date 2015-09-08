using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DalOraInfra.DAL;
using KdsLibrary.BL;

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

        public void HRChangesMatzavPirteyBrerotmechdal() //HafalatBatchShinuyimHR()
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
        public void HRChangesMeafyenim() //HafalatShinuyimHRatMeafyenim()
        {
            HafalatMeafyenim();
        }

        private void HafalatMatzavOvdim()
        {
            int iseq=0;
            try
            {
                //matzav ovdim
                //**obatch.KdsWriteProcessLog(3, 31, 1, "start bdika shinuyim hr matzav ovdim");
                iseq = obatch.InsertProcessLog(3, 31, RecordStatus.Wait, "start bdika shinuyim hr matzav ovdim",0);
                flag = true;
                if (!obatch.CheckViewEmpty("NEW_MATZAV_OVDIM"))
                {
                    obClManager = new KdsBatch.HrWorkersChanges.clManager(KdsBatch.HrWorkersChanges.TableType.State, ref flag);
                    obClManager.InsertPeriod();
                    obClManager.SaveShinuyimHR(ref flag);
                    obatch.UpdateProcessLog(iseq, RecordStatus.Finish, "end bdika shinuyim hr matzav ovdim", 0);
                    //** obatch.KdsWriteProcessLog(3, 31, 1, "end bdika shinuyim hr matzav ovdim");
                    if (flag)
                    {
                        //sub_tahalich = 31
                        iseq = obatch.InsertProcessLog(3, 31, RecordStatus.Wait, "start upd matzav", 0);
                        //** obatch.KdsWriteProcessLog(3, 31, 1, "start upd matzav");
                        obatch.MoveNewMatzavOvdimToOld();
                        obatch.UpdateProcessLog(iseq, RecordStatus.Finish, "end ok upd matzav", 0);
                        //** obatch.KdsWriteProcessLog(3, 31, 2, "end ok upd matzav");
                    }
                    obClManager = null;
                }
                else
                {
                    obClManager = null;
                    obatch.UpdateProcessLog(iseq, RecordStatus.Faild, "Matzav Ovdim Err: View Empty", 0);
                }
            }
            catch (Exception ex)
            {
                obClManager = null;
                obatch.UpdateProcessLog(iseq, RecordStatus.Faild, "Matzav Ovdim Err:" + ex.Message, 0);
               //** obatch.KdsWriteProcessLog(3, 31, 1, "Matzav Ovdim Err:" + ex.Message);
                //KdsLibrary.clGeneral.LogMessage("matzav ovdim:" + ex.Message,System.Diagnostics.EventLogEntryType.Error);
            } 
        }

        private void HafalatPirteyOved()
        {
            int iseq = 0;
            try
            {
                // pirtey oved
                iseq = obatch.InsertProcessLog(3, 32, RecordStatus.Wait, "start bdika shinuyim hr pirtey oved", 0); 
               //** obatch.KdsWriteProcessLog(3, 32, 1, "start bdika shinuyim hr pirtey oved");
                flag = true;
                if (!obatch.CheckViewEmpty("NEW_PIRTEY_OVDIM"))
                {
                    obClManager = new KdsBatch.HrWorkersChanges.clManager(KdsBatch.HrWorkersChanges.TableType.Details, ref flag);
                    obClManager.InsertPeriod();
                    obClManager.SaveShinuyimHR(ref flag);
                    obatch.UpdateProcessLog(iseq, RecordStatus.Finish, "end bdika shinuyim hr pirtey oved", 0);
                    //**  obatch.KdsWriteProcessLog(3, 32, 1, "end bdika shinuyim hr pirtey oved");
                    if (flag)
                    {
                        //sub_tahalich = 326
                        iseq = obatch.InsertProcessLog(3, 32, RecordStatus.Wait, "start upd pirtey_ovdim", 0);
                        //** obatch.KdsWriteProcessLog(3, 32, 1, "start upd pirtey_ovdim");
                        obatch.MoveNewPirteyOvedToOld();
                        obatch.UpdateProcessLog(iseq, RecordStatus.Finish, "end ok upd pirtey_ovdim", 0);
                        //** obatch.KdsWriteProcessLog(3, 32, 2, "end ok upd pirtey_ovdim");
                    }
                    obClManager = null;
                }
                else
                {
                    obClManager = null;
                    obatch.UpdateProcessLog(iseq, RecordStatus.Faild, "Pirtey Oved Err: View Empty", 0);
                }
            }
            catch (Exception ex)
            {
                obClManager = null;
                obatch.UpdateProcessLog(iseq, RecordStatus.Faild, "Pirtey Oved Err:" + ex.Message, 0);
             //**   obatch.KdsWriteProcessLog(3, 32, 1, "Pirtey Oved Err:" + ex.Message);
            //    KdsLibrary.clGeneral.LogMessage("pirtey oved:" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
        }


        private void HafalatMeafyenim()
        {
            int iseq = 0;
            try
            {
                //meafyenim
                iseq = obatch.InsertProcessLog(3, 33, RecordStatus.Wait, "start bdika shinuyim hr meafyenim", 0);    
              //**  obatch.KdsWriteProcessLog(3, 33, 1, "start bdika shinuyim hr meafyenim");
                flag = true;
                if (!obatch.CheckViewEmpty("NEW_MEAFYENIM_OVDIM"))
                {
                    obClManager = new KdsBatch.HrWorkersChanges.clManager(KdsBatch.HrWorkersChanges.TableType.Properties, ref flag);
                    //  obatch.KdsWriteProcessLog(3, 33, 1, "before InsertPeriod meafyenim");
                    obClManager.InsertPeriod();
                    // obatch.KdsWriteProcessLog(3, 33, 1, "before SaveShinuyimHR meafyenim");
                    obClManager.SaveShinuyimHR(ref flag);
                    obatch.UpdateProcessLog(iseq, RecordStatus.Finish, "end bdika shinuyim hr meafyenim", 0);
                    //**  obatch.KdsWriteProcessLog(3, 33, 1, "end bdika shinuyim hr meafyenim");
                    if (flag)
                    {
                        //sub_tahalich = 33
                        iseq = obatch.InsertProcessLog(3, 33, RecordStatus.Wait, "start upd meafyenim_ovdim", 0);
                        //**   obatch.KdsWriteProcessLog(3, 33, 1, "start upd meafyenim_ovdim");
                        obatch.MoveNewMeafyenimOvdimToOld();
                        obatch.UpdateProcessLog(iseq, RecordStatus.Finish, "end ok upd meafyenim_ovdim", 0);
                        //**   obatch.KdsWriteProcessLog(3, 33, 2, "end ok upd meafyenim_ovdim");
                    }
                    obClManager = null;
                }
                else
                {
                    obClManager = null;
                    obatch.UpdateProcessLog(iseq, RecordStatus.Faild, "Meafyenim Err: View Empty", 0);
                }
              //**  obatch.KdsWriteProcessLog(3, 33, 1, "sof meafyenim_ovdim");
            }
            catch (Exception ex)
            {
                obClManager = null;
                obatch.UpdateProcessLog(iseq, RecordStatus.Faild, "upd Meafyenim Err:" + ex.Message, 0);
             //**   obatch.KdsWriteProcessLog(3, 33, 1, "upd Meafyenim Err:" + ex.Message);
          //    KdsLibrary.clGeneral.LogMessage("meafyenim:" + ex.Message,System.Diagnostics.EventLogEntryType.Error);
            }
        }

        private void HafalatBrerotMechdal()
        {
            int iseq = 0;
            try
            {
                // brerot mechdal
                iseq = obatch.InsertProcessLog(3, 34, RecordStatus.Wait, "start bdika shinuyim hr brerot mechdal", 0);    
               //** obatch.KdsWriteProcessLog(3, 34, 1, "start bdika shinuyim hr brerot mechdal");
                flag = true;
                if (!obatch.CheckViewEmpty("NEW_BREROT_MECHDAL_MEAFYENIM"))
                {
                    obClManager = new KdsBatch.HrWorkersChanges.clManager(KdsBatch.HrWorkersChanges.TableType.Defaults, ref flag);
                    obClManager.TipulTableDefaults(ref flag);
                    obatch.UpdateProcessLog(iseq, RecordStatus.Finish, "end bdika shinuyim hr brerot mechdal", 0);
                    //**  obatch.KdsWriteProcessLog(3, 34, 1, "end bdika shinuyim hr brerot mechdal");
                    if (flag)
                    {
                        //sub_tahalich = 34
                        iseq = obatch.InsertProcessLog(3, 34, RecordStatus.Wait, "start upd Brerot_Mechdal_Meafyenim", 0);
                        //** obatch.KdsWriteProcessLog(3, 34, 1, "start upd Brerot_Mechdal_Meafyenim");
                        obatch.MoveNewBrerotMechdalToOld();
                        obatch.UpdateProcessLog(iseq, RecordStatus.Finish, "end ok upd Brerot_Mechdal_Meafyenim", 0);
                        //**  obatch.KdsWriteProcessLog(3, 34, 2, "end ok upd Brerot_Mechdal_Meafyenim");
                    }
                    obClManager = null;
                }
                else
                {
                    obClManager = null;
                    obatch.UpdateProcessLog(iseq, RecordStatus.Faild, "Brerot Mechdal Err: View Empty", 0);
                }
            }
            catch (Exception ex)
            {
                obClManager = null;
                obatch.UpdateProcessLog(iseq, RecordStatus.Faild, "upd Brerot Mechdal Err:" + ex.Message, 0);
              //**  obatch.KdsWriteProcessLog(3, 34, 1, "upd Brerot Mechdal Err:" + ex.Message);
              //  KdsLibrary.clGeneral.LogMessage("brerot mechdal:" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
        }
    }
}
