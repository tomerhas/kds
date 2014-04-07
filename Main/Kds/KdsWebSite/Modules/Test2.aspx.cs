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
using System.Data;
//using Lesnikowski.Barcode; 

public partial class Modules_Test2 : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        checkBoxes1.DataTextField = "Value";
        checkBoxes1.DataValueField = "ID";
        checkBoxes1.DataSource = GetListItem();
        checkBoxes1.DataBind();

        DropDownCheckBoxes1.DataTextField = "Value";
        DropDownCheckBoxes1.DataValueField = "ID";
        DropDownCheckBoxes1.DataSource = GetListItem();
        DropDownCheckBoxes1.DataBind();
       
        //usTest.kodNameCol = "ID";
        //usTest.valNameCol = "Value";
        //usTest.SetDataSource(GetListItem());
     //   CheckBoxListDropDown.SetDataSource(GetListItem()); 
        //DropDownList ddl = new DropDownList();
        //ddl.ID = "ddlChkList";
        //ListItem lstItem = new ListItem();
        //ddl.Items.Insert(0, lstItem);
        //ddl.Width = new Unit(155);
        //ddl.Attributes.Add("onmousedown", "showdivonClick()");
        //CheckBoxList chkBxLst = new CheckBoxList();
        //chkBxLst.ID = "chkLstItem";
        //chkBxLst.Attributes.Add("onmouseover", "showdiv()");
        //DataTable dtListItem = GetListItem();
        //int rowNo = dtListItem.Rows.Count;
        //string lstValue = string.Empty;
        //string lstID = string.Empty;
        //for (int i = 0; i < rowNo - 1; i++)
        //{
        //    lstValue = dtListItem.Rows[i]["Value"].ToString();
        //    lstID = dtListItem.Rows[i]["ID"].ToString();
        //    lstItem = new ListItem("<a href=\"javascript:void(0)\" id=\"alst\" style=\"text-decoration:none;color:Black; \" onclick=\"getSelectedItem(' " + lstValue + "','" + i + "','" + lstID + "','anchor');\">" + lstValue + "</a>", dtListItem.Rows[i]["ID"].ToString());
        //    lstItem.Attributes.Add("onclick", "getSelectedItem('" + lstValue + "','" + i + "','" + lstID + "','listItem');");
        //    chkBxLst.Items.Add(lstItem);
        //}
        //System.Web.UI.HtmlControls.HtmlGenericControl div = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
        //div.ID = "divChkList";
        //div.Controls.Add(chkBxLst);
        //div.Style.Add("border", "black 1px solid");
        //div.Style.Add("width", "160px");
        //div.Style.Add("height", "180px");
        //div.Style.Add("overflow", "AUTO");
        //div.Style.Add("display", "none");
        //phDDLCHK.Controls.Add(ddl);
        //phDDLCHK.Controls.Add(div);

        //List<ListItem> Items = new List<ListItem>();
        //Items.Add(new ListItem("אגב", "1"));
        //Items.Add(new ListItem("אכב", "2"));
        //Items.Add(new ListItem("אגד", "3"));


        //Items.ForEach(delegate(ListItem Item)
        //{
        //    ListBox1.Items.Add(Item);
        //});
       //btnHeadrut.Attributes.Add("onClick", "OpenDivuachHeadrut()");
        //string[] ds = new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten" };
        //ddlchklst.DataSource = ds;
        //ddlchklst.DataBind();
        ////ddlchklst.DataSource = ds;
        ////this.ddlchklst.DataBind();

        //ListItem itemL = new ListItem();
        //CheckBox item = new CheckBox();
        //item.ID = "1"  ;
        //item.Text = "sunday";
 
        //ddyom.Items.Add(item);
    }

    DataTable GetListItem()
    {
        DataTable table = new DataTable();
        table.Columns.Add("ID", typeof(int));
        table.Columns.Add("Value", typeof(string));
        table.Rows.Add(1, "ListItem1");
        table.Rows.Add(2, "ListItem2");
        table.Rows.Add(3, "ListItem3");
        table.Rows.Add(4, "My ListItem Wraps also");
        table.Rows.Add(5, "My New ListItem5");
        table.Rows.Add(6, "ListItem6");
        table.Rows.Add(7, "ListItem7");
        table.Rows.Add(8, "ListItem8");
        return table;
    }
    protected void Page_PreRender(object sender, EventArgs e)
    { 
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        string a = checkBoxes1.getValues();
        string b = DropDownCheckBoxes1.getValues();
        //CheckBoxListDropDown.GetSelectedItemsList();
        KdsBatch.TaskManager.Utils oUtils = new KdsBatch.TaskManager.Utils();
        oUtils.RunShguimLechishuv();
    //    int num;
    //    KdsLibrary.BL.clBatch x = new KdsLibrary.BL.clBatch();
    //    num = x.GetNumChangesHrToShguim();
    ////    clBatchManager oBatchManager = new clBatchManager(int.Parse(txtId.Text), DateTime.Parse(clnFromDate.Text));
    //   // oBatchManager.MainInputData(int.Parse(txtId.Text), DateTime.Parse(clnFromDate.Text));
    //   // oBatchManager.MainOvedErrors(int.Parse(txtId.Text), DateTime.Parse(clnFromDate.Text));
    }


    protected void OnClick_ShinuyHR(object sender, EventArgs e)
    {
        KdsBatch.HrWorkersChanges.clMain obClManager = new KdsBatch.HrWorkersChanges.clMain();
        obClManager.HRChangesMatzavPirteyBrerotmechdal();

        //KdsBatch.HrWorkersChanges.clMain obClManager = new KdsBatch.HrWorkersChanges.clMain();
        //obClManager.HRChangesMeafyenim();
    }

    //protected void OnClick_ShinuyMeafyenim(object sender, EventArgs e)
    //{
    //    KdsBatch.HrWorkersChanges.clMain obClManager = new KdsBatch.HrWorkersChanges.clMain();
    //    obClManager.HRChangesMeafyenim();
    //}

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
    //protected void OnClick_PrintWC(object sender, EventArgs e)
    //{
    //    Dictionary<string, string> ReportParameters = new Dictionary<string, string>();
    //    //ReportParameters.Add("P_MISPAR_ISHI", "65099");
    //    //ReportParameters.Add("P_TAARICH", "08/03/2010");
    //    //ReportParameters.Add("P_EMDA", "0");
    //    //OpenReport(ReportParameters, (Button)sender, ReportName.WorkCard.ToString());

    //   // ReportParameters = new Dictionary<string, string>();
    //    ReportParameters.Add("P_MISPAR_ISHI", "65099");
    //    ReportParameters.Add("P_TAARICH", "08/03/2010");
    //    ReportParameters.Add("P_EMDA", "0");
    //    ReportParameters.Add("P_SIDUR_VISA", "1");
    //    OpenReport(ReportParameters, (Button)sender, ReportName.PrintWorkCard.ToString());
    //}

    //protected void OpenReport(Dictionary<string, string> ReportParameters, Button btnScript, string sRdlName)
    //{                        
    //    KdsReport _Report;
    //    string UrlRoot = string.Empty;
    //    KdsDynamicReport _KdsDynamicReport;

    //    _KdsDynamicReport = KdsDynamicReport.GetKdsReport();
    //    _Report = new KdsReport();
    //    _Report = _KdsDynamicReport.FindReport(sRdlName);
    //    Session["Report"] = _Report;

    //    Session["ReportParameters"] = ReportParameters;

       
    //    UrlRoot = Request.Url.AbsoluteUri.Replace(Request.RawUrl, (Request.Url.Host == "localhost") ? Request.ApplicationPath : "");
    //    string sScript = "window.showModalDialog('" + UrlRoot + "/modules/reports/ShowReport.aspx?Dt=" + DateTime.Now.ToString() + "&RdlName=" + sRdlName + "','','dialogwidth:900px;dialogheight:690px;dialogtop:10px;dialogleft:100px;status:no;resizable:no;scroll:no;');";
    //    ScriptManager.RegisterStartupScript(btnScript, this.GetType(), "ReportViewer", sScript, true);

    //}


    //protected void btnSrokTachograf_Click(object sender, EventArgs e)
    //{

    //     KdsLibrary.BL.clReport obj = new KdsLibrary.BL.clReport();
    //     obj.getRikuzPdfTest(76452, DateTime.Parse("01/08/2010"), 0);
    //    //BaseBarcode barcode = BarcodeFactory.GetBarcode(Symbology.EAN13);

    //    //barcode.Number = "123456789012";

    //    //barcode.ChecksumAdd = true;

    //    //// Render barcode:  

    // //   Bitmap bitmap = barcode.Render();

    //    //// You can also save it to file:  

    //    //barcode.Save("c:\\barcode.gif", ImageType.Gif);  


    //}
    //protected void btnShowTachograf_Click(object sender, EventArgs e)
    //{
    //    KdsBatch.TaskManager.Utils oUtils = new KdsBatch.TaskManager.Utils();
    //    oUtils.RunShguimOfPremiyotMusachim();// RunShguimLechishuv();
    //    oUtils.RunCalcPremiyotMusachim();// RunShguimLechishuv();
    //    //MainCalc objMainCalc = new MainCalc(0, DateTime.Parse("01/03/2010"), DateTime.Parse("31/08/2010"), "0,1", false, clGeneral.TypeCalc.Batch);
    //    //foreach (Oved oOved in objMainCalc.Ovdim)
    //    //{
    //    //    objMainCalc.CalcOved(oOved);
    //    //}
    //    //MainCalc objMainCalc = new MainCalc();
    //    //objMainCalc.PremiaCalc();

    //}
    protected void TextChange(object sender, EventArgs e)
    {

    }
}
