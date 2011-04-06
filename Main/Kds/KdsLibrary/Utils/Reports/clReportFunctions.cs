using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data ;
using KdsLibrary.BL;
namespace KdsLibrary.Utils.Reports
{
    public class clReportFunctions
    {
        public static DataTable GetMonthsList(string Name,Boolean disdplayFirst, string firstText)
        {
            DataTable dt = null;
            ReportName rptName = (ReportName)Enum.Parse(typeof(ReportName), Name);
            DataTable dtParametrim = new DataTable();
            clUtils oUtils = new clUtils();
            int num;
            dtParametrim = oUtils.getErechParamByKod("100", DateTime.Now.ToShortDateString());
            num= int.Parse(dtParametrim.Rows[0]["ERECH_PARAM"].ToString())-1;

            dt = clGeneral.FillDateInDataTable(num, DateTime.Now.AddMonths(-1), disdplayFirst, firstText);

            //switch (rptName)
            //{
            //    case ReportName.PremiotPresence:
            //    case ReportName.DriverWithoutSignature:
            //    case ReportName.DriverWithoutTacograph:
            //    case ReportName.HashvaatChodsheyRizotChishuv:
            //    case ReportName.HashvaatRizotChishuv:
            //    case ReportName.DescriptionAllComponents:
            //    case ReportName.AbsentWorkers:
            //    case ReportName.Average:
            //    case ReportName.DriverWithPlaints:
            //        dt = clGeneral.FillDateInDataTable(13, DateTime.Now.AddMonths(-1), disdplayFirst, firstText);
            //        break;
            //    case ReportName.DisregardDrivers:
            //        dt = clGeneral.FillDateInDataTable(14, DateTime.Now.AddMonths(-1), disdplayFirst, firstText);
            //        break;
            //    //case ReportName.HashvaatRizotChishuv:
            //    //    dt = clGeneral.FillDateInDataTable(13, DateTime.Now.AddMonths(-1), true, firstText);
            //    //    break;
            //}
            return dt;
        }


        public static DataTable GetHoursDayList()
        {
            DataTable dt = new DataTable();
            int hour = 0; 
            string Erech;
            DataRow Dr;

            dt.Columns.Add("Value", typeof(String));
            for (int i = 0; i < 24; i++)
            {
                Dr = dt.NewRow();
                if (hour < 10)
                    Erech = "0" + hour + ":00";
                else
                    Erech = hour + ":00";
                Dr["Value"] = Erech;
            //    Dr["Text"] = Erech;
                dt.Rows.Add(Dr);
                hour += 1;
            }

            return dt;
        }
    }


    
      
}
