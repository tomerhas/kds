using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using KdsLibrary;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using DalOraInfra.DAL;



public partial class TestScan : System.Web.UI.Page
{
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!Page.IsPostBack)
                {
                    hiddenPathBarcodes.Value = ConfigurationManager.AppSettings["ScannedFilesUrl"].ToString();
                    hiddenFileExportPath.Value = ConfigurationManager.AppSettings["ScanFileExportPath"].ToString();
                }
        }
      
        protected void InsertBarcodeTable(int barcode)
        {
            clDal oDal = new clDal();
            DateTime taarich= new DateTime();
            int mis_ishi;
            try
            {

               mis_ishi= int.Parse(misIshi.Text);
               taarich = DateTime.Parse(Taarich.Text);

               oDal.ClearCommand();
               oDal.AddParameter("p_mispar_ishi ", ParameterType.ntOracleInteger,mis_ishi  , ParameterDir.pdInput);
               oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, taarich, ParameterDir.pdInput);
               oDal.AddParameter("p_Barkod", ParameterType.ntOracleInt64, barcode, ParameterDir.pdInput);
               oDal.ExecuteSP("PKG_UTILS.pro_insert_barkod_Tachograf");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ShowTachograf_Click(object sender, EventArgs e)
        {
           
            DataTable dt = new DataTable();
            DataView dv;
            try
            {
                dt = getBarcodesLeOved();
                dv = new DataView(dt);
                grdBarcodes.DataSource = dv;
                grdBarcodes.DataBind();
                divgrdBarcodes.Style["display"] = "inline";
            }
            catch (Exception ex)
            {
                throw ex;
            }         
        }
        protected DataTable getBarcodesLeOved()
        {
            clDal oDal = new clDal();
            DateTime taarich = new DateTime();
            int mis_ishi, barcode;
            DataTable dt = new DataTable();
            try
            {
                mis_ishi = int.Parse(misIshi.Text);
                taarich = DateTime.Parse(Taarich.Text);

                oDal.ClearCommand();
                oDal.AddParameter("p_mispar_ishi ", ParameterType.ntOracleInteger, mis_ishi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, taarich, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP("PKG_UTILS.fun_get_barkod_Tachograf",ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void grdBarcodes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Separator)
            {
                ((HyperLink)e.Row.Cells[0].Controls[0]).Attributes.Add("OnClick", string.Concat("javascript:OpenTachograf('", ((System.Web.UI.WebControls.HyperLink)(e.Row.Cells[0].Controls[0])).Text, "')"));
            }
        }       
        protected void hideBtn_onClick(object sender, EventArgs e)
        {
            int barcode;
            string[] barcodes;
            try
            {
                barcodes = hiddenBarcodes.Value.Split(';');
                for (int i = 0; i < barcodes.Length; i++)
                {
                    barcode = int.Parse(barcodes[i].ToString());
                    InsertBarcodeTable(barcode);
                }
                ScriptManager.RegisterStartupScript(hideBtn,hideBtn.GetType(), "", " alert('הטכוגרפים נסרקו בהצלחה');", true);
          
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(hideBtn, hideBtn.GetType(), "", " alert('הסריקה נכשלה,נסה שנית.');", true);
                throw ex;
            }

        }
     
    //protected int slofBarcodeFromFile()
    //{
    //    try
    //    {
    //              //url = @"C:\Documents and Settings\meravn\Desktop\myExport.txt";
    //// barcode = slofBarcodeFromFile();
    // StreamReader reader = new StreamReader(url);
    // while (reader.Peek() > 0)
    // {
    //     textRow = reader.ReadLine();
    //     barcode =int.Parse(textRow.Split(',')[0].ToString());
    //     InsertBarcodeTable(barcode);
    // }
    // reader.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
       
}

