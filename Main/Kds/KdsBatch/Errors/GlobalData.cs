using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsBatch.Entities;
using System.Data;

//using System.Collections.Specialized;

using KdsLibrary.BL;
using KdsLibrary.UDT;
using System.Collections;
using KdsLibrary.Utils;
using KdsLibrary;


namespace KdsBatch.Errors
{
    public static class GlobalData
    {

        public static DataTable dtLookUp { get; set; }
        public static DataTable dtYamimMeyuchadim { get; set; }
        public static DataTable dtSugeyYamimMeyuchadim { get; set; }
        public static DataTable dtSugSidur { get; set; }
        public static List<ErrorItem> ActiveErrors { get; set; }
        public static List<CardError> CardErrors { get; set; }
  
        public static void InitGlobalData()
        {
             clUtils oUtils = new clUtils();
            
             dtLookUp = oUtils.GetLookUpTables();
             dtYamimMeyuchadim = clGeneral.GetYamimMeyuchadim();
             dtSugeyYamimMeyuchadim = clGeneral.GetSugeyYamimMeyuchadim();
             dtSugSidur = clDefinitions.GetSugeySidur();
             SetActiveErrors();
         }

        private static void SetActiveErrors()
        {
           EntitiesDal oEntDal = new EntitiesDal();
           DataTable dtErrorsActive;
           TypeCheck type;
           OriginError origion;

           dtErrorsActive = oEntDal.GetErrorsActive();
           foreach (DataRow dr in dtErrorsActive.Rows)
           {
               type = (TypeCheck)Enum.Parse(typeof(TypeCheck) ,dr["kod_shgia"].ToString());
               origion = (OriginError)Enum.Parse(typeof(OriginError) ,dr["rama"].ToString());
               ActiveErrors.Add(new ErrorItem(type, origion));
           }
        }

        public static string GetLookUpKods(string sTableName)
        {
            //The function get lookup table name and return all kods in string, separate by comma
            string sLookUp = "";
            DataRow[] drLookUpAll;
            try
            {
                drLookUpAll = dtLookUp.Select(string.Concat("table_name='", sTableName, "'"));
                foreach (DataRow drLookUp in drLookUpAll)
                {
                    sLookUp = string.Concat(sLookUp, drLookUp["Kod"].ToString(), ",");
                }
                sLookUp = sLookUp.Substring(0, sLookUp.Length - 1);

                return sLookUp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
