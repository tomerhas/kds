using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KdsLibrary.BL;
using System.Data;
using KdsLibrary.UI;
using KdsBatch;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.Interfaces.DAL;
using KDSCommon.UDT;

public partial class Modules_Ovdim_HosafatKnisot : KdsPage
{
    public const int MISPAR_KNISA = 0;
    public const int TEUR = 1;
    public const int TEUR_SUG_KNISA = 2;
    public const int DAKOT_BAFOAL = 3;
    public const int HOSEF_PEILUT = 4;
    public const int SUG_KNISA = 5;
    public const int YESHUV_NAME = 6;

    protected void Page_Load(object sender, EventArgs e)
    {
        clUtils oUtils = new clUtils();
        DataSet dtMakat;
        DataTable dtParametrim;
        int i;
        int iResult = 0;
        long lMakat;
        string sTeur = "";
        try
        {
            SetFixedHeaderGrid(pnlgrdKnisot.ClientID, Header);
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["CardDate"] != null)
                {
                    ViewState["DateCard"] = Request.QueryString["CardDate"].ToString();
                }
                if (Request.QueryString["EmpID"] != null)
                {
                    ViewState["MisparIshi"] = Request.QueryString["EmpID"].ToString();
                }
                if (Request.QueryString["SidurID"] != null)
                {
                    ViewState["MisparSidur"] = Request.QueryString["SidurID"].ToString();
                }
                if (Request.QueryString["SidurDate"] != null)
                {
                    ViewState["SidurDate"] = Request.QueryString["SidurDate"].ToString();
                }
                if (Request.QueryString["SidurHour"] != null)
                {
                    ViewState["SidurHour"] = Request.QueryString["SidurHour"].ToString();
                }
                if (Request.QueryString["ShatYetzia"] != null)
                {
                    ViewState["ShatYetzia"] = Request.QueryString["ShatYetzia"].ToString();
                }
                if (Request.QueryString["OtoNo"] != null)
                {
                    ViewState["OtoNo"] = Request.QueryString["OtoNo"].ToString();
                }
                if (Request.QueryString["LicenseNumber"] != null)
                {
                    ViewState["LicenseNumber"] = Request.QueryString["LicenseNumber"].ToString();
                }
                
                lMakat = long.Parse(Request.QueryString["makatNesia"].ToString());
                ViewState["Makat"] = lMakat;
                tdHeader.InnerHtml = "  כניסות למק''ט " + lMakat;

                var kavimDal = ServiceLocator.Current.GetInstance<IKavimDAL>();
                dtMakat = kavimDal.GetKavimDetailsFromTnuaDS(lMakat, DateTime.Parse(ViewState["SidurDate"].ToString()), out iResult, 1);
                if (dtMakat.Tables[1].Rows.Count > 0)
                {
                    sTeur = dtMakat.Tables[0].Rows[0]["Description"].ToString();

                    tdHeader.InnerHtml = tdHeader.InnerHtml + " " + sTeur;

                    grdKnisot.DataSource=dtMakat.Tables[1];
                    grdKnisot.DataBind();

                    btnUpdateKnisot.Style["display"]= "block";
                }
                else if (iResult == 0)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "err", " alert('למק\"ט " + lMakat + " לא קיימות כניסות');window.close();", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "err", " alert('כעת לא ניתן להציג כניסות למק\"ט " + lMakat + ", יש לנסות במועד מאוחר יותר');window.close();", true);

                dtParametrim = oUtils.getErechParamByKod("98", ViewState["DateCard"].ToString());
                if (dtParametrim.Rows.Count > 0)
                {
                    for (i = 0; i < dtParametrim.Rows.Count; i++)
                        Params.Attributes.Add("Param" + dtParametrim.Rows[i]["KOD_PARAM"].ToString(), dtParametrim.Rows[i]["ERECH_PARAM"].ToString());
                }

         
                
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnUpdate_OnClick(object sender, EventArgs e)
    {
        OBJ_PEILUT_OVDIM oObjPeiluyotOvdimIns;
        COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimIns = new COLL_OBJ_PEILUT_OVDIM();
        bool check;
        try
        {
            for (int i = 0; i < grdKnisot.Rows.Count; i++)
            {
                check = ((CheckBox)grdKnisot.Rows[i].Cells[HOSEF_PEILUT].FindControl("cbHosef")).Checked;
                if (check)
                {
                    oObjPeiluyotOvdimIns = new OBJ_PEILUT_OVDIM();
                    oObjPeiluyotOvdimIns.MISPAR_ISHI = int.Parse(ViewState["MisparIshi"].ToString());
                    oObjPeiluyotOvdimIns.MISPAR_SIDUR = int.Parse(ViewState["MisparSidur"].ToString());
                    oObjPeiluyotOvdimIns.TAARICH = DateTime.Parse(ViewState["DateCard"].ToString());
                    oObjPeiluyotOvdimIns.SHAT_YETZIA = DateTime.Parse(ViewState["ShatYetzia"].ToString());
                    oObjPeiluyotOvdimIns.SHAT_HATCHALA_SIDUR = DateTime.Parse(ViewState["SidurDate"] + " " + ViewState["SidurHour"]);
                    oObjPeiluyotOvdimIns.MISPAR_KNISA = int.Parse(grdKnisot.Rows[i].Cells[MISPAR_KNISA].Text);
                    oObjPeiluyotOvdimIns.MAKAT_NESIA = long.Parse(ViewState["Makat"].ToString());
                    if (ViewState["OtoNo"] != null && ViewState["OtoNo"] != "")
                        oObjPeiluyotOvdimIns.OTO_NO = int.Parse(ViewState["OtoNo"].ToString());

                    if (ViewState["LicenseNumber"] != null && ViewState["LicenseNumber"] != "")
                        oObjPeiluyotOvdimIns.LICENSE_NUMBER = int.Parse(ViewState["LicenseNumber"].ToString());

                    oObjPeiluyotOvdimIns.BITUL_O_HOSAFA = 2;
                    oObjPeiluyotOvdimIns.MEADKEN_ACHARON = int.Parse(LoginUser.UserInfo.EmployeeNumber);
                    if (!string.IsNullOrEmpty(((TextBox)grdKnisot.Rows[i].Cells[DAKOT_BAFOAL].FindControl("txtDakotBafoal")).Text))
                        oObjPeiluyotOvdimIns.DAKOT_BAFOAL = int.Parse(((TextBox)grdKnisot.Rows[i].Cells[DAKOT_BAFOAL].FindControl("txtDakotBafoal")).Text);
                    oObjPeiluyotOvdimIns.TEUR_NESIA = Server.HtmlDecode(grdKnisot.Rows[i].Cells[TEUR].Text);
                      if (grdKnisot.Rows[i].Cells[SUG_KNISA].Text=="3")
                          oObjPeiluyotOvdimIns.TEUR_NESIA = oObjPeiluyotOvdimIns.TEUR_NESIA + " (" + grdKnisot.Rows[i].Cells[TEUR_SUG_KNISA].Text + ")";

                    oCollPeilutOvdimIns.Add(oObjPeiluyotOvdimIns);
                }
            }
            if (oCollPeilutOvdimIns.Count > 0)
            {
                clOvdim oOvdim = new clOvdim();
                oOvdim.DeleteInsertKnisotLePeilut(int.Parse(ViewState["MisparIshi"].ToString()), int.Parse(ViewState["MisparSidur"].ToString()), DateTime.Parse(ViewState["DateCard"].ToString()),
                                            DateTime.Parse(ViewState["SidurDate"] + " " + ViewState["SidurHour"]), DateTime.Parse(ViewState["ShatYetzia"].ToString()), long.Parse(ViewState["Makat"].ToString()), 
                                             int.Parse(LoginUser.UserInfo.EmployeeNumber),oCollPeilutOvdimIns);
                HttpRuntime.Cache.Remove(ViewState["MisparIshi"].ToString() + ViewState["DateCard"].ToString());
                ScriptManager.RegisterStartupScript(btnUpdateKnisot, btnUpdateKnisot.GetType(), "close", "window.returnValue=1;window.close();", true);
            }
        }
        catch 
        {
            ScriptManager.RegisterStartupScript(btnUpdateKnisot, btnUpdateKnisot.GetType(), "err", "alert('תקלה בשמירת הנתונים');window.close();", true);

        }
    }

    protected void grdKnisot_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Separator)
        {
            e.Row.Cells[SUG_KNISA].Style.Add("display", "none");
            e.Row.Cells[YESHUV_NAME].Style.Add("display", "none");
            ((AjaxControlToolkit.ValidatorCalloutExtender)e.Row.Cells[DAKOT_BAFOAL].FindControl("exvDakot")).BehaviorID = e.Row.ClientID + "_vldExvldDakot";
            ((TextBox)e.Row.Cells[DAKOT_BAFOAL].FindControl("txtDakotBafoal")).Attributes.Add("onchange", "onchange_txtDakot(" + e.Row.ClientID + ")");
            if (((DataRowView)e.Row.DataItem).Row["SugKnisa"].ToString() != "3")
                ((TextBox)e.Row.Cells[DAKOT_BAFOAL].FindControl("txtDakotBafoal")).Style.Add("display", "none");
            if (((DataRowView)e.Row.DataItem).Row["SugKnisa"].ToString() == "2")
            {
                e.Row.Cells[TEUR_SUG_KNISA].Text = "חובה";
                ((CheckBox)e.Row.Cells[DAKOT_BAFOAL].FindControl("cbHosef")).Checked = true;
                ((CheckBox)e.Row.Cells[DAKOT_BAFOAL].FindControl("cbHosef")).Enabled = false;
            }
            else
                e.Row.Cells[TEUR_SUG_KNISA].Text = "לפי-צורך";
            if (e.Row.Cells[YESHUV_NAME].Text != "&nbsp;")
            {
                if (e.Row.Cells[YESHUV_NAME].Text != e.Row.Cells[TEUR].Text)
                { 
                e.Row.Cells[TEUR].Text="כנ-" + e.Row.Cells[YESHUV_NAME].Text + ", " + e.Row.Cells[TEUR].Text;
                }
                else  e.Row.Cells[TEUR].Text="כנ-" + e.Row.Cells[TEUR].Text;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        { e.Row.Cells[SUG_KNISA].Style.Add("display", "none");
        e.Row.Cells[YESHUV_NAME].Style.Add("display", "none");
        }
    }
}
