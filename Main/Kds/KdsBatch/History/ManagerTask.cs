﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using KdsLibrary;
using KdsLibrary.BL;
using System.IO;
using KdsLibrary.DAL;
using System.Data;
namespace KdsBatch.History
{
    public class ManagerTask
    {
       private long _lRequestNum;
        public ManagerTask(long lRequestNum)
        {
           _lRequestNum = lRequestNum;
        }
        public ManagerTask()
        {
            //   _lRequestNum = lRequestNum;
        }

        public void Run()
        {
            List<string[]> FilesName;
            string[] patterns = new string[3];
            string path, FileNameOld;
            string[] files;
            int iStatus = 0;
            try
            {
                FilesName = new List<string[]>();
                patterns[0] = "BZAY"; patterns[1] = "BZAS"; patterns[2] = "BZAP";
                path = ConfigurationSettings.AppSettings["PathFileMF"];
                clLogBakashot.InsertErrorToLog(_lRequestNum, "I", 0, " START RunInsetRecordsToHistory");
                if (Directory.Exists(path))
                {
                    foreach (string pattern in patterns)
                    {
                        FilesName.Add(Directory.GetFiles(path, pattern + "*.txt", SearchOption.TopDirectoryOnly));
                    }

                    for (int i = 0; i < FilesName.Count; i++)
                    {
                        files = FilesName[i];
                        foreach (string file in files)
                        {
                            try
                            {
                                switch (i)
                                {
                                    case 0:
                                        KdsBatch.History.TaskDay oTaskY = new TaskDay(_lRequestNum,file, ';');
                                        oTaskY.Run();
                                        break;
                                    case 1:
                                        KdsBatch.History.TaskSidur oTaskS = new TaskSidur(_lRequestNum,file, ';');
                                        oTaskS.Run();
                                        break;
                                    case 2:
                                        KdsBatch.History.TaskPeilut oTaskP = new TaskPeilut(_lRequestNum,file, ';');
                                        oTaskP.Run();
                                        break;
                                }


                                FileNameOld = file.Replace(".TXT", ".old");
                                FileNameOld = FileNameOld.Replace(".txt", ".old");
                                File.Copy(file, FileNameOld);
                                File.Delete(file);
                            }
                            catch (Exception ex)
                            {
                                clLogBakashot.InsertErrorToLog(_lRequestNum, "E", 0, "RunInsetRecordsToHistory: " + ex.Message + " file=" + file);
                            }
                        }
                    }
                    iStatus = clGeneral.enStatusRequest.ToBeEnded.GetHashCode();
                }
                else clLogBakashot.InsertErrorToLog(_lRequestNum, "I", 0, "path not exist");

                clLogBakashot.InsertErrorToLog(_lRequestNum, "I", 0, " END RunInsetRecordsToHistory");
            }
            catch (Exception ex)
            {
                iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                clGeneral.LogError("History Error:  " + ex);
                clLogBakashot.InsertErrorToLog(_lRequestNum, "E", 0, "RunInsetRecordsToHistory: " + ex.Message);
            }
            finally
            {
                clDefinitions.UpdateLogBakasha(_lRequestNum, DateTime.Now, iStatus);
            }
        } 

        ////public void Run()
        ////{
        ////    BaseTask oTask;
        ////    int iStatus = 0;
        ////   // long _lRequestNum;
        ////    clBatch objBatch = new clBatch();
        ////    try
        ////    {
        ////     //  _lRequestNum = clGeneral.OpenBatchRequest(clGeneral.enGeneralBatchType.HasavatNetuniToOracle, "נתוני אורקל", -12);
        ////        oTask = new KdsBatch.History.TaskDay(_lRequestNum, ';');
        ////         oTask.Run();
        ////         GC.Collect();
        ////         oTask = null;
        ////         oTask = new KdsBatch.History.TaskSidur(_lRequestNum, ';');
        ////         oTask.Run();
        ////         GC.Collect();
        ////         oTask = null;
        ////         oTask = new KdsBatch.History.TaskPeilut(_lRequestNum, ';');
        ////         oTask.Run();
        ////         GC.Collect();
        ////         oTask = null;

        ////         iStatus = clGeneral.enStatusRequest.ToBeEnded.GetHashCode();
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
        ////        clGeneral.LogError("History Error:  " + ex);
        ////        clLogBakashot.InsertErrorToLog(_lRequestNum, "E", 0, "RunInsetRecordsToHistory: " + ex.Message);
        ////    }
        ////    finally
        ////    {
        ////        clDefinitions.UpdateLogBakasha(_lRequestNum, DateTime.Now, iStatus);
        ////    }
        ////}

        public void RunHistory()
        {
            List<string[]> FilesName;
            string[] patterns = new string[3];
            string path, FileNameOld;
            string[] files;
            int iStatus = 0;
            try
            {
                FilesName = new List<string[]>();
                patterns[0] = "BZAY"; patterns[1] = "BZAS"; patterns[2] = "BZAP";
                path = ConfigurationSettings.AppSettings["PathFileMF"];
                clLogBakashot.InsertErrorToLog(_lRequestNum, "I", 0, " START RunInsetRecordsToHistory");
                if (Directory.Exists(path))
                {
                    foreach (string pattern in patterns)
                    {
                        FilesName.Add(Directory.GetFiles(path, pattern + "*.txt", SearchOption.TopDirectoryOnly));
                    }

                    for (int i = 0; i < FilesName.Count; i++)
                    {
                        files = FilesName[i];
                        foreach (string file in files)
                        {
                           
                            try{

                                InsertToDB(file);

                                FileNameOld = file.Replace(".TXT", ".old");
                                FileNameOld = FileNameOld.Replace(".txt", ".old");
                                File.Copy(file, FileNameOld);
                                File.Delete(file);
                            }
                            catch (Exception ex)
                            {
                                clLogBakashot.InsertErrorToLog(_lRequestNum, "E", 0, "RunInsetRecordsToHistory: " + ex.Message + " file=" + file);
                            }
                        }
                    }
                    iStatus = clGeneral.enStatusRequest.ToBeEnded.GetHashCode();
                }
                else clLogBakashot.InsertErrorToLog(_lRequestNum, "I", 0, "path not exist");

                clLogBakashot.InsertErrorToLog(_lRequestNum, "I", 0, " END RunInsetRecordsToHistory");
            }
            catch (Exception ex)
            {
                iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                clGeneral.LogError("History Error:  " + ex);
                clLogBakashot.InsertErrorToLog(_lRequestNum, "E", 0, "RunInsetRecordsToHistory: " + ex.Message);
            }
            finally
            {
                clDefinitions.UpdateLogBakasha(_lRequestNum, DateTime.Now, iStatus);
            }
        }

        private void InsertToDB(string fileName)
        {
            clDal objDal = new clDal();
            string name;
            try
            {
                name = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                objDal.AddParameter("bakasha_id", ParameterType.ntOracleInt64, _lRequestNum, ParameterDir.pdInput);
                objDal.AddParameter("p_file_name", ParameterType.ntOracleVarchar, name, ParameterDir.pdInput);
                objDal.ExecuteSP(clGeneral.cProInsNetuneyHistory);
               //GC.Collect();
            }
            catch (Exception ex)
            {
                throw new Exception("InsertToDB Error: " + ex.Message + "\n" + ex.StackTrace + " type: " + TypeName);
            }
        }
    }
}
