using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using KdsLibrary.BL;
using KdsBatch; 
using KdsLibrary;
using KdsLibrary.UI;
using KdsLibrary.Security;

public partial class Modules_Ovdim_Rechivim : KdsLibrary.UI.KdsPage
{
  private int iMisparIshi;
  private DateTime dTarich;
  private DataSet    dsRechivim;
  private DataTable  dt;

  private const int COL_shat_hatchala = 2;
  private const int COL_erech_rechiv = 3;
  private const int COL_KOD_RECHIV = 4;
  private const int COL_row_type = 5;

    

    protected void Page_Load(object sender, EventArgs e)
    {
        // List of Kod-Reciv We show in this routin [18,23,52,105,133,134, 213, 214, 215, 216];
        if (!Page.IsPostBack)
        {
            SetFixedHeaderGrid(pnlRechivim.ClientID, Header);
        }
        BoundTable();

    }

    private void BoundTable()
    {
        clOvdim oOvdim = new clOvdim();
        DataTable dtBound = new DataTable();
         string sDate;
         string sMessage;
           bool bValid= false;

        iMisparIshi = Int32.Parse(Request.QueryString["id".ToLower()]);
        sDate = Request.QueryString["date".ToLower()].ToString();
         dTarich = new DateTime(int.Parse(sDate.Substring(6, 4)), int.Parse(sDate.Substring(3, 2)), int.Parse(sDate.Substring(0, 2)));
        
        dtBound.Columns.Add("kod_rechiv", typeof(Int32));
        dtBound.Columns.Add("teur_rechiv", typeof(String));
        dtBound.Columns.Add("mispar_sidur", typeof(Int32));
        dtBound.Columns.Add("shat_hatchala",typeof(String));// typeof(DateTime));
        dtBound.Columns.Add("erech_rechiv", typeof(Decimal)); //typeof(Int32));
        dtBound.Columns.Add("row_type", typeof(Int32));

        DataTable dtRechivim = oOvdim.GetRechivim();

        //clCalculation objCalc = new clCalculation();
        //dsRechivim = objCalc.CalcDayInMonth(iMisparIshi, dTarich, 0, out bValid);

        MainCalc objMainCalc = new MainCalc();
        dsRechivim = objMainCalc.CalcDayInMonth(iMisparIshi, dTarich, 0, out bValid);
             

        if (!bValid)
        {
            sMessage = "קיימות שגיאות בכרטיס/בעיה בחישוב ולא ניתן לחשב את הרכיבים. יש לתקן את השגיאות ולפתוח מחדש את מסך רכיבים מחושבים";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err", "alert('" + sMessage + "');window.close();", true);
          }
        else
        {
            if (dsRechivim != null)
            {
                foreach (DataRow dr in dtRechivim.Rows)
                {
                    DataView dvSidurim = new DataView(dsRechivim.Tables["CHISHUV_SIDUR"]);
                    dvSidurim.RowFilter = "kod_rechiv=" + dr["kod_rechiv"];
                    for (int i = 0; i < dvSidurim.Count; ++i)
                    {
                        DataRow newRow = dtBound.NewRow();
                        newRow["kod_rechiv"] = dr["kod_rechiv"];
                        if (i == 0) newRow["teur_rechiv"] = dr["teur_rechiv"];
                        newRow["mispar_sidur"] = dvSidurim[i]["mispar_sidur"];
                        newRow["shat_hatchala"] = DateTime.Parse(dvSidurim[i]["shat_hatchala"].ToString()).ToShortTimeString();
                        newRow["erech_rechiv"] = dvSidurim[i]["erech_rechiv"];
                        newRow["row_type"] = 0;
                        dtBound.Rows.Add(newRow);
                    }
                    DataRow[] sumRows = dsRechivim.Tables["CHISHUV_YOM"].Select("kod_rechiv=" + dr["kod_rechiv"]);
                    if (sumRows.Length > 0)
                    {
                        DataRow sumRow = dtBound.NewRow();
                        sumRow["kod_rechiv"] = dr["kod_rechiv"];
                        sumRow["erech_rechiv"] =
                            dsRechivim.Tables["CHISHUV_YOM"].Select("kod_rechiv=" + dr["kod_rechiv"])[0]["erech_rechiv"];
                        sumRow["row_type"] = 1;
                        dtBound.Rows.Add(sumRow);
                    }
                }
            }


            grdRechivim.DataSource = dtBound;
            grdRechivim.DataBind();
           
        }
    }

    protected void grdRechivim_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[COL_KOD_RECHIV].Style.Add("display", "none");
            e.Row.Cells[COL_row_type].Style.Add("display", "none");

        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[COL_KOD_RECHIV].Style.Add("display", "none");
            e.Row.Cells[COL_row_type].Style.Add("display", "none");

            if (e.Row.Cells[COL_row_type].Text == "1")
            {
                e.Row.Cells[COL_erech_rechiv].Text = "";
                e.Row.Cells[COL_shat_hatchala].Text = "חישוב ברמת יום עבודה";
                e.Row.Cells[COL_shat_hatchala].Style.Add("font-weight", "bold");
                e.Row.Cells[COL_erech_rechiv].Text = dsRechivim.Tables["CHISHUV_YOM"].Select("kod_rechiv=" + e.Row.Cells[COL_KOD_RECHIV].Text)[0]["erech_rechiv"].ToString();
                e.Row.Cells[COL_erech_rechiv].Style.Add("font-weight", "bold");
            }
        }
    }

    //protected void grdRechivim_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    GridView grdSidurim;
    //    Label lblErechYom;
    //    DataView dvSidurim = new DataView(dsRechivim.Tables["CHISHUV_SIDUR"]);
    //    try
    //    {
    //        e.Row.Cells[COL_KOD_RECHIV].Style.Add("display", "none");
    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            grdSidurim = (GridView)(e.Row.Cells[COL_GRID].Controls[1]);
    //            dvSidurim.RowFilter="kod_rechiv=" + e.Row.Cells[COL_KOD_RECHIV].Text;
    //            if (dvSidurim.Count == 0)
    //            {
    //                e.Row.Style.Add("display", "none");
    //            }
    //            else
    //            {
    //                grdSidurim.DataSource = dvSidurim;
    //                grdSidurim.DataBind();
    //                lblErechYom = (Label)(e.Row.Cells[COL_GRID].Controls[3]);
    //                lblErechYom.Text= dsRechivim.Tables["CHISHUV_YOM"].Select("kod_rechiv=" + e.Row.Cells[COL_KOD_RECHIV].Text)[0]["erech_rechiv"].ToString();
    //           }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw (ex); 
    //    }
       
    //}
}



      







