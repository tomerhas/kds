using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Types;
using KdsLibrary.DAL;
using System.Data;
using KdsLibrary.UDT;

namespace KdsBatch.History
{
     public abstract class BaseTask
    {
        private string _pathFile;
        private char _del;

        protected string ProcedureName;
        protected string TypeName;
        protected string ParameterName;
        protected IOracleCustomType CollObject;
        protected TypeTask CollType;
        private Builder oBuild;

        public BaseTask() { }
        public BaseTask(string path,char del) {
            _pathFile = path;
            _del = del;
        }

        protected abstract void FillItemsToCollection(List<string[]> Items);
        

        public void Run()
        {
            oBuild = new Builder(_pathFile, _del);
            try
            {
                oBuild.Build();
                FillItemsToCollection(oBuild.Items);
                InsertToDB();
            }
            catch (Exception ex)
            {
            }
        }

        protected DateTime GetDateTime(string sDate)
        {
            string sTaarich;
            sTaarich = sDate.Substring(6, 2) + "/" + sDate.Substring(4, 2) + "/" + sDate.Substring(0, 4);
            if (sDate.Length > 8)
            {
                sTaarich += " " + sDate.Substring(8, 2) + ":" + sDate.Substring(10, 2) + ":00";
            }
            return DateTime.Parse(sTaarich);
        }

        private void InsertToDB()
        {
            clDal objDal = new clDal();
            try
            {
                objDal.AddParameter(ParameterName, ParameterType.ntOracleArray, CollObject, ParameterDir.pdInput, TypeName);
                objDal.ExecuteSP(ProcedureName);
            }
            catch (Exception ex)
            {
               // throw ex;
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
