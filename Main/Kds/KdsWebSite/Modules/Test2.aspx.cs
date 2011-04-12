using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KdsBatch;
using KdsLibrary.UDT;
using KdsLibrary.Utils.Reports;
using System.IO;
using KdsLibrary.UI;
using KdsLibrary.Security;

using KdsLibrary;
using System.Configuration;
//using Lesnikowski.Barcode; 

public partial class Modules_Test2 : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //List<ListItem> Items = new List<ListItem>();
        //Items.Add(new ListItem("אגב", "1"));
        //Items.Add(new ListItem("אכב", "2"));
        //Items.Add(new ListItem("אגד", "3"));


        //Items.ForEach(delegate(ListItem Item)
        //{
        //    ListBox1.Items.Add(Item);
        //});
       btnHeadrut.Attributes.Add("onClick", "OpenDivuachHeadrut()");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        int num;
        KdsLibrary.BL.clBatch x = new KdsLibrary.BL.clBatch();
        num = x.GetNumChangesHrToShguim();
    //    clBatchManager oBatchManager = new clBatchManager(int.Parse(txtId.Text), DateTime.Parse(clnFromDate.Text));
       // oBatchManager.MainInputData(int.Parse(txtId.Text), DateTime.Parse(clnFromDate.Text));
       // oBatchManager.MainOvedErrors(int.Parse(txtId.Text), DateTime.Parse(clnFromDate.Text));
    }
    
    
    protected void OnClick_ShinuyHR(object sender, EventArgs e)
    {
        KdsBatch.HrWorkersChanges.clMain obClManager = new KdsBatch.HrWorkersChanges.clMain();
        obClManager.HafalatBatchShinuyimHR();
    }

    protected void OnClick_ShinuyMeafyenim(object sender, EventArgs e)
    {
        KdsBatch.HrWorkersChanges.clMain obClManager = new KdsBatch.HrWorkersChanges.clMain();
        obClManager.HafalatShinuyimHRatMeafyenim();
    }

   /* protected void OnClick_PrintWC(object sender, EventArgs e)
    {
        CreatReport();
    }

    private void CreatReport()
    {
        byte[] s;
        ReportModule Report = ReportModule.GetInstance();
        string sFileName, sPathFile;
        FileStream fs;
        string sScript, sIp;
        string sPathFilePrint = "\\" + System.Environment.MachineName + "\\kdsPrints\\" + "75290\\";
        try
        {
           Report.AddParameter("P_TAARICH", "06/04/2010");
            Report.AddParameter("P_MISPAR_ISHI","65099");
            Report.AddParameter("P_EMDA", "1");

            s = Report.CreateReport("/KdsReports/WorkCard", eFormat.PDF, true);

            sFileName = "WorkCard" + DateTime.Now.Minute + ".pdf";

            sPathFile = ConfigurationManager.AppSettings["PathFileTransfer"] + "75290\\";
            if (!Directory.Exists(sPathFile))
                Directory.CreateDirectory(sPathFile);

            fs = new FileStream(sPathFile + sFileName, FileMode.Create, FileAccess.Write);
            fs.Write(s, 0, s.Length);
            fs.Flush();
            fs.Close();

            sIp = "";
            sScript = "PrintDoc('" + sIp + "' ,'" + sPathFilePrint + sFileName + "');";
            ScriptManager.RegisterStartupScript(BtnPrint, this.GetType(), "PrintPdf", sScript, true);
           
          
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    */
    protected void OnClick_PrintWC(object sender, EventArgs e)
    {
        Dictionary<string, string> ReportParameters = new Dictionary<string, string>();
        //ReportParameters.Add("P_MISPAR_ISHI", "65099");
        //ReportParameters.Add("P_TAARICH", "08/03/2010");
        //ReportParameters.Add("P_EMDA", "0");
        //OpenReport(ReportParameters, (Button)sender, ReportName.WorkCard.ToString());

       // ReportParameters = new Dictionary<string, string>();
        ReportParameters.Add("P_MISPAR_ISHI", "65099");
        ReportParameters.Add("P_TAARICH", "08/03/2010");
        ReportParameters.Add("P_EMDA", "0");
        ReportParameters.Add("P_SIDUR_VISA", "1");
        OpenReport(ReportParameters, (Button)sender, ReportName.PrintWorkCard.ToString());
    }

    protected void OpenReport(Dictionary<string, string> ReportParameters, Button btnScript, string sRdlName)
    {                        
        KdsReport _Report;
        string UrlRoot = string.Empty;
        KdsDynamicReport _KdsDynamicReport;

        _KdsDynamicReport = KdsDynamicReport.GetKdsReport();
        _Report = new KdsReport();
        _Report = _KdsDynamicReport.FindReport(sRdlName);
        Session["Report"] = _Report;

        Session["ReportParameters"] = ReportParameters;

       
        UrlRoot = Request.Url.AbsoluteUri.Replace(Request.RawUrl, (Request.Url.Host == "localhost") ? Request.ApplicationPath : "");

        string sScript = "window.showModalDialog('" + UrlRoot + "/modules/reports/ShowReport.aspx?Dt=" + DateTime.Now.ToString() + "&RdlName=" + sRdlName + "','','dialogwidth:900px;dialogheight:690px;dialogtop:10px;dialogleft:100px;status:no;resizable:no;scroll:no;');";
        ScriptManager.RegisterStartupScript(btnScript, this.GetType(), "ReportViewer", sScript, true);

    }


    protected void btnSrokTachograf_Click(object sender, EventArgs e)
    {

        
        //BaseBarcode barcode = BarcodeFactory.GetBarcode(Symbology.EAN13);

        //barcode.Number = "123456789012";

        //barcode.ChecksumAdd = true;

        //// Render barcode:  

        //Bitmap bitmap = barcode.Render();

        //// You can also save it to file:  

        //barcode.Save("c:\\barcode.gif", ImageType.Gif);  


    }
    protected void btnShowTachograf_Click(object sender, EventArgs e)
    {
        MainCalc objMainCalc = new MainCalc(0, DateTime.Parse("01/01/2011"), DateTime.Parse("31/01/2011"), "0,1", false, clGeneral.TypeCalc.Batch);
        //foreach (Oved oOved in objMainCalc.Ovdim)
        //{
        //    objMainCalc.CalcOved(oOved);
        //}
        //MainCalc objMainCalc = new MainCalc();
        //objMainCalc.PremiaCalc();

    }
}
