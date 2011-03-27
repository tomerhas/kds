using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.IO;
using KdsLibrary.BL;


namespace KdsLibrary.TaskManager
{
    public class Utils
    {
        public void MoveRecordsToHistory()
        {
            int iMoveRecordsToHistory = 0;
            clBatch oBatch = new clBatch();
            try
            {
                iMoveRecordsToHistory = oBatch.InsertProcessLog(99, 0, KdsLibrary.BL.RecordStatus.Wait, "MoveRecordsToHistory", 0);
                oBatch.MoveRecordsToHistory(DateTime.Now.AddMonths(-11));
                oBatch.UpdateProcessLog(iMoveRecordsToHistory, KdsLibrary.BL.RecordStatus.Finish, "MoveRecordsToHistory", 0);
            }
            catch (Exception ex)
            {
                oBatch.UpdateProcessLog(iMoveRecordsToHistory, KdsLibrary.BL.RecordStatus.Faild, "MoveRecordsToHistory faild: " + ex.Message, 14);
            }
        }

        public void DeleteLogTahalich()
        {
            //  Dim oKDs As KdsLibrary.BL.clBatch
            int iDeleteSeq = 0;
            clBatch oBatch = new clBatch();
            try
            {
                iDeleteSeq = oBatch.InsertProcessLog(97, 0, KdsLibrary.BL.RecordStatus.Wait, "DeleteLogThalich", 0);
                //  oKDs = New KdsLibrary.BL.clBatch
                oBatch.DeleteLogTahalichRecords();
                oBatch.UpdateProcessLog(iDeleteSeq, KdsLibrary.BL.RecordStatus.Finish, "DeleteLogThalich", 0);
            }
            catch (Exception ex)
            {
                //'**oKDsData = New KdsDataImport.ClKds
                oBatch.UpdateProcessLog(iDeleteSeq, KdsLibrary.BL.RecordStatus.Faild, "MoveRecordsToHistory faild: " + ex.Message, 14);
                //'**oKDsData.KdsWriteProcessLog(99, 0, 3, "MoveRecordsToHistory faild: " + ex.Message, 9)
            }
        }

        public void DeleteHeavyReport()
        {
            DataTable dt = null;
            clReport oKDs = new clReport();
            // Dim oKDsData As KdsDataImport.ClKds
            string path = null;
            string startPath = null;
            int i = 0;
            int iDeleteHeavyReport = 0;
            clBatch oBatch = new clBatch();
            try
            {
                iDeleteHeavyReport = oBatch.InsertProcessLog(20, 1, KdsLibrary.BL.RecordStatus.Wait, "DeleteHeavyReport", 0);
                startPath = ConfigurationSettings.AppSettings["HeavyReportsPath"];
                oKDs = new KdsLibrary.BL.clReport();

                dt = oKDs.GetHeavyReportsToDelete();
                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i <= (dt.Rows.Count - 1); i++)
                    {
                        path = startPath + dt.Rows[i]["ReportName"].ToString();
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                    }
                }
                oBatch.UpdateProcessLog(iDeleteHeavyReport, KdsLibrary.BL.RecordStatus.Finish, "DeleteHeavyReport", 0);
            }
            catch (Exception ex)
            {
                //oKDsData = New KdsDataImport.ClKds
                oBatch.UpdateProcessLog(iDeleteHeavyReport, KdsLibrary.BL.RecordStatus.Faild, "DeleteHeavyReport faild: " + ex.Message, 9);
                //'**oKDsData.KdsWriteProcessLog(20, 1, 3, "DeleteHeavyReport faild: " + ex.Message, 9)
            }
        }
    }
}
