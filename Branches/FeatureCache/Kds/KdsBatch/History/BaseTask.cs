using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Oracle.DataAccess.Types;
using KdsLibrary.DAL;
using System.Data;
using KdsLibrary.UDT;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using KdsLibrary;

namespace KdsBatch.History
{
    public abstract class BaseTask
    {
        
        private char _del;
        protected long _lRequestNum;
        private string[] _files;
        private string _filename;

        protected string ProcedureName;
        protected string TypeName;
        protected string ParameterName;
        protected string Pattern;
        protected string PathDirectory;
        protected string PathDirectoryOld;

        protected IOracleCustomType CollObject;
        protected TypeTask CollType;
        private Builder oBuild;

        public BaseTask() { }
        //public BaseTask(long lRequestNum, char del)
        //{
        //     _del = del;
        //    _lRequestNum = lRequestNum;
        //}

        public BaseTask(long lRequestNum, string file,char del)
        {
            _del = del;
            _lRequestNum = lRequestNum;
            _filename = file;
        }
        protected abstract void FillItemsToCollection(string[] Item, int index);
        protected abstract void AllocateCollection();
        protected abstract void SetCollection();
        protected abstract void Dispose();

        protected int RecordsCount
        {
            get
            {
               return oBuild.Items.Count();
            }
        }

        public void Run()
        {
            try
            {

                oBuild = new Builder(_filename, _del);

                    oBuild.Build();
                    AllocateCollection();
                    for (int i = 0; i < oBuild.Items.Count; i++)
                    {
                        FillItemsToCollection(oBuild.Items[i], i);
                    }
                    //  oBuild.Items.ForEach(item => FillItemsToCollection(item,0));
                    clLogBakashot.InsertErrorToLog(_lRequestNum, "I", 0, "Items Count= " + oBuild.Items.Count.ToString());
                    oBuild.Dispose();
                    SetCollection();
                    InsertToDB(_filename);
                  //  MoveFileToOld(_filename);
                    Dispose();

                    clLogBakashot.InsertErrorToLog(_lRequestNum, "I", 0, _filename + " saved");
                
            }
            catch (Exception ex)
            {
                throw new Exception("Run History Error: " + ex.Message);
            }
        }

        //////public void Run()
        //////{
        //////    try
        //////    {
        //////        SetFiles();
        //////        foreach (string file in _files)
        //////        {
        //////            oBuild = new Builder(file, _del);
                    
        //////            oBuild.Build();
        //////            AllocateCollection();
        //////            for (int i=0;i<oBuild.Items.Count; i++)
        //////            {
        //////                FillItemsToCollection(oBuild.Items[i], i);
        //////            }
        //////          //  oBuild.Items.ForEach(item => FillItemsToCollection(item,0));
        //////            clLogBakashot.InsertErrorToLog(_lRequestNum, "I", 0, "Items Count= " + oBuild.Items.Count.ToString());
        //////            oBuild.Dispose();
        //////            SetCollection();
        //////            InsertToDB(file);     
        //////            MoveFileToOld(file);
        //////            Dispose();

        //////            clLogBakashot.InsertErrorToLog(_lRequestNum, "I", 0, file + " saved");
        //////        }
        //////    }
        //////    catch (Exception ex)
        //////    {
        //////        throw new Exception("Run History Error: " + ex.Message);
        //////    }
        //////}

        private void SetFiles()
        {
            try
            {
                _files = Directory.GetFiles(PathDirectory, Pattern + "*.txt", SearchOption.TopDirectoryOnly);
                clLogBakashot.InsertErrorToLog(_lRequestNum, "I", 0, "pattern=" + Pattern + " Files=" + _files.Length);
            }
            catch (Exception ex)
            {
                throw new Exception("getFiles Error: " + ex.Message + " Pttern=" + Pattern);
            }
        }

         private void MoveFileToOld(string fileName)
        {
            string FileNameOld;
            try
            {
                FileNameOld = fileName.Replace(".TXT", ".old");
                FileNameOld = FileNameOld.Replace(".txt", ".old");
                FileNameOld = PathDirectoryOld + FileNameOld.Substring(FileNameOld.LastIndexOf("\\") + 1);
                File.Move(fileName, FileNameOld);
            }
            catch (Exception ex)
            {
                throw new Exception("MoveFileToOld Error: " + ex.Message + "file=" +fileName);
            }
        }

        
        protected DateTime GetDateTime(string sDate)
        {
            //string sTaarich;
            try
            {
                string format = (sDate.Length > 8) ? "yyyyMMddHHmm" : "yyyyMMdd";
                return  DateTime.ParseExact(sDate, format, CultureInfo.InvariantCulture, DateTimeStyles.None);

                //sTaarich = sDate.Substring(6, 2) + "/" + sDate.Substring(4, 2) + "/" + sDate.Substring(0, 4);
                //if (sDate.Length > 8)
                //{
                //    sTaarich += " " + sDate.Substring(8, 2) + ":" + sDate.Substring(10, 2) + ":00";
                //}
                //return DateTime.Parse(sTaarich);
            }
            catch (Exception ex)
            {
                throw new Exception("GetDateTime Error: " + ex.Message + " Taarich: " + sDate);
            }
        }

        private void InsertToDB(string fileName)
        {
            clDal objDal = new clDal();
            string name;
            try
            {
                name =fileName.Substring(fileName.LastIndexOf("\\") + 1);
                objDal.AddParameter("bakasha_id", ParameterType.ntOracleInt64, _lRequestNum, ParameterDir.pdInput);
                objDal.AddParameter("p_file_name", ParameterType.ntOracleVarchar, name, ParameterDir.pdInput);
                objDal.AddParameter(ParameterName, ParameterType.ntOracleArray, CollObject, ParameterDir.pdInput, TypeName);
                objDal.ExecuteSP(ProcedureName);
                GC.Collect();
            }
            catch (Exception ex)
            {
                throw new Exception("InsertToDB Error: " + ex.Message + "\n" + ex.StackTrace + " type: " + TypeName);
            }
        }

    }
    public enum TypeTask
    {
        Day,
        Sidur,
        Peilut
    }


}
