using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KdsLibrary;
using KdsLibrary.UI;
using KdsLibrary.Security;
using KdsLibrary.BL;
using KdsLibrary.Utils;
using KdsLibrary.Utils.Reports;
using System.Data;

public partial class Modules_Ovdim_NetuneyOved : KdsPage
{
    private bool bShowData = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            MasterPage mp = (MasterPage)Page.Master;
            mp.ImagePrintClick.Click += new ImageClickEventHandler(ImagePrintClick_Click);

            if (!Page.IsPostBack)
            {
                PageHeader = "נתוני עובד";
                DateHeader = DateTime.Today.ToShortDateString();
                LoadMessages((DataList)Master.FindControl("lstMessages"));
                ServicePath = "~/Modules/WebServices/wsGeneral.asmx";

                LoadMessages((DataList)Master.FindControl("lstMessages"));
                 divNetunim.Visible = false;
                 btnHidden.Style.Add("Display", "None");
                
                SetFixedHeaderGrid("ctl00_KdsContent_TabContainer1_pMeafyeneyBitzua_ucMeafyeneyBitzua_pnlContainer", mp.HeadPage);
                SetFixedHeaderGrid("ctl00_KdsContent_TabContainer1_pMeafyeneyBitzua_ucMeafyeneyBitzua_pnlGridHistoria", mp.HeadPage);
                SetFixedHeaderGrid("ctl00_KdsContent_TabContainer1_pPirteyOved_ucPirteyOved_pnlContainerDetails", mp.HeadPage);
                SetFixedHeaderGrid("ctl00_KdsContent_TabContainer1_pPirteyOved_ucPirteyOved_pnlgrdHistoria", mp.HeadPage);
                SetFixedHeaderGrid("ctl00_KdsContent_TabContainer1_pStatusOved_ucStatusOved_pnlGridStatus", mp.HeadPage);
                
                rdoId.Attributes.Add("onclick", "SetTextBox();");
                rdoName.Attributes.Add("onclick", "SetTextBox();");

                
                KdsSecurityLevel iSecurity = PageModule.SecurityLevel;
                if (iSecurity == KdsSecurityLevel.ViewAll || iSecurity == KdsSecurityLevel.ViewOnlyEmployeeData)
                {
                    AutoCompleteExtenderByName.ContextKey = "";
                    AutoCompleteExtenderID.ContextKey = "";
                }
                else
                {
                    AutoCompleteExtenderByName.ContextKey = ((LoginUser)LoginUser.GetLoginUser()).UserInfo.EmployeeNumber;
                    AutoCompleteExtenderID.ContextKey = ((LoginUser)LoginUser.GetLoginUser()).UserInfo.EmployeeNumber;
                }
             
            }
       //     TabContainer1.Style.Add("cursor", "pointer");
        }
        catch (Exception ex)
        { throw ex; }

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        divNetunim.Visible = true;
        TabContainer1.ActiveTabIndex = 0;
        ShowData();
     
    }

    void LoadDdlMonth()
    {
        try
        {
            DataTable dtParametrim;
            clUtils oUtils = new clUtils();
           // DataTable dtMonth;
           // clOvdim oOvdim = new clOvdim();
            if (txtEmpId.Text.Length > 0 && clGeneral.IsNumeric(txtEmpId.Text))
            {
                //dtMonth = oOvdim.GetMonthsToOved(int.Parse(txtEmpId.Text));
                //ddlMonth.DataSource = dtMonth;
                //ddlMonth.DataBind();
                dtParametrim = oUtils.getErechParamByKod("100", DateTime.Now.ToShortDateString());
                clGeneral.LoadDateCombo(ddlMonth, int.Parse(dtParametrim.Rows[0]["ERECH_PARAM"].ToString()));
               
                ddlMonth.Enabled = true;
            }
            else { ddlMonth.Enabled = false; }
        }
        catch (Exception ex)
        { clGeneral.BuildError(Page, ex.Message); }

    }

    protected void txtEmpId_TextChanged(object sender, EventArgs e)
    {
        DataTable dt;
        string sOvedName = "";
        clOvdim oOvdim = new clOvdim();

        try
        {
        if (rdoId.Checked)
        {
            if ((txtEmpId.Text).Length == 0)
            {
                btnShow.ControlStyle.CssClass = "ImgButtonSearchDisable";
                btnShow.Enabled = false;
                txtName.Text = "";
            }
            else if (!(clGeneral.IsNumeric(txtEmpId.Text)))
            {
                btnShow.ControlStyle.CssClass = "ImgButtonSearchDisable";
                btnShow.Enabled = false;
                txtName.Text = "";

                ScriptManager.RegisterStartupScript(txtEmpId, this.GetType(), "errName", "alert('!מספר אישי לא חוקי');", true);

            }
            else
            {
                if (AutoCompleteExtenderID.ContextKey.Length > 0)
                {
                    dt = oOvdim.GetOvdimToUser(txtEmpId.Text, int.Parse(AutoCompleteExtenderID.ContextKey));
                    if (dt.Rows.Count > 0)
                    {
                        sOvedName = dt.Rows[0]["OVED_NAME"].ToString();
                    }
                }
                else
                {
                    sOvedName = oOvdim.GetOvedFullName(int.Parse(txtEmpId.Text));
                }

                if (sOvedName.Length > 0 && sOvedName !="null")
                {
                    txtName.Text = sOvedName;
                    btnShow.ControlStyle.CssClass = "ImgButtonSearch";
                    btnShow.Enabled = true;
                }
                else
                {
                    btnShow.ControlStyle.CssClass = "ImgButtonSearchDisable";
                    btnShow.Enabled = false;
                    txtName.Text = "";

                    ScriptManager.RegisterStartupScript(txtEmpId, this.GetType(), "errName", "alert('!מספר אישי לא קיים');", true);

                }
            }
            LoadDdlMonth();


            divNetunim.Visible = false;

            if (rdoId.Checked)
            {
                txtName.Enabled = false;
            }
            txtEmpId.Enabled = true;
        }
    }
    catch (Exception ex)
        { clGeneral.BuildError(Page, ex.Message); }

    }

    protected void txtName_TextChanged(object sender, EventArgs e)
    {
        DataTable dt;
        string sMisparIshi = "";
        try
        {

            clOvdim oOvdim = new clOvdim();
            if (rdoName.Checked && (txtName.Text).Length > 0)
            {
                if (txtName.Text.IndexOf("(") == -1)
                {
                    if (AutoCompleteExtenderID.ContextKey.Length > 0)
                    {
                        dt = oOvdim.GetOvdimToUserByName(txtName.Text, int.Parse(AutoCompleteExtenderID.ContextKey));
                        if (dt.Rows.Count > 0)
                        {
                            sMisparIshi = dt.Rows[0]["MISPAR_ISHI"].ToString();
                        }
                    }
                    else
                    {
                        sMisparIshi = oOvdim.GetOvedMisparIshi(txtName.Text);
                    }
                }
                else
                {
                    sMisparIshi = (txtName.Text.Substring(txtName.Text.IndexOf("(") + 1)).Replace(")", "");
                }

                if (sMisparIshi.Length > 0 && sMisparIshi != "null")
                {
                    txtEmpId.Text = sMisparIshi;
                    btnShow.ControlStyle.CssClass = "ImgButtonSearch";
                    btnShow.Enabled = true;
                }
                else
                {
                    btnShow.ControlStyle.CssClass = "ImgButtonSearchDisable";
                    btnShow.Enabled = false;
                    txtEmpId.Text = "";

                    ScriptManager.RegisterStartupScript(txtName, this.GetType(), "errName", "alert('!שם לא קיים');", true);
                }

            }
            if (rdoName.Checked && txtName.Text.Length > 0)
            {
                txtEmpId.Enabled = false;
            }
            txtName.Enabled = true;
            if ((txtName.Text).Length == 0)
            {
                btnShow.ControlStyle.CssClass = "ImgButtonSearchDisable";
                btnShow.Enabled = false;
                txtEmpId.Text = "";
            }
            ucMeafyeneyBitzua.VisibleDivNetunim = false;
            LoadDdlMonth();

        }
        catch (Exception ex)
        { clGeneral.BuildError(Page, ex.Message); }


    }


    protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
    {
        if (bShowData == true)
        {
            ShowData();
            bShowData = false;
        }
        else { bShowData = true; }
    }

    private void ShowData()
    {
        try
        {
            ucMeafyeneyBitzua.VisibleDivNetunim = false;
            ucPirteyOved.VisibleDivNetunim = false;
            ucStatusOved.VisibleDivNetunim = false;

            if (ddlMonth.SelectedValue.Length > 0 && clGeneral.IsNumeric(txtEmpId.Text))
            {
                MasterPage mp = (MasterPage)Page.Master;

                switch (TabContainer1.ActiveTabIndex)
                {
                    case 0:

                        ucMeafyeneyBitzua.EmployeeId = int.Parse(txtEmpId.Text);
                        ucMeafyeneyBitzua.MonthToShow = ddlMonth.SelectedValue;
                        ucMeafyeneyBitzua.ElementId = "ctl00_KdsContent_TabContainer1_pMeafyeneyBitzua_ucMeafyeneyBitzua";
                        txtElement.Value = "ctl00_KdsContent_TabContainer1_pMeafyeneyBitzua_ucMeafyeneyBitzua";

                        ucMeafyeneyBitzua.ShowBreratMechdal = chkWithDefault.Checked;

                        ucMeafyeneyBitzua.ShowData();
                        break;
                    case 1:
                        ucPirteyOved.SecurityPage = PageModule.SecurityLevel;
                        ucPirteyOved.EmployeeId = int.Parse(txtEmpId.Text);
                        ucPirteyOved.MonthToShow = ddlMonth.SelectedValue;
                        ucPirteyOved.ElementId = "ctl00_KdsContent_TabContainer1_pPirteyOved_ucPirteyOved";
                        txtElement.Value = "ctl00_KdsContent_TabContainer1_pPirteyOved_ucPirteyOved";

                        ucPirteyOved.ShowData();
                        break;
                    case 2:

                        ucStatusOved.EmployeeId = int.Parse(txtEmpId.Text);
                        ucStatusOved.MonthToShow = ddlMonth.SelectedValue;
                        ucStatusOved.ElementId = "ctl00_KdsContent_TabContainer1_pStatusOved_ucStatusOved";
                        txtElement.Value = "ctl00_KdsContent_TabContainer1_pStatusOved_ucStatusOved";

                        ucStatusOved.ShowData();
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }

    void ImagePrintClick_Click(object sender, ImageClickEventArgs e)
    {
        byte[] s;
        string sScript;
        ReportModule Report = new ReportModule();//  ReportModule.GetInstance();
        try
        {
            if (Page.IsValid)
            {
                if (txtEmpId.Text.Length > 0)
                {

                    Report.AddParameter("P_MISPAR_ISHI", txtEmpId.Text);
                    Report.AddParameter("P_TAARICH", clGeneral.GetEndMonthFromStringMonthYear(1, ddlMonth.SelectedValue).ToString());

                    s = Report.CreateReport("/KdsReports/NetuneyOved", eFormat.PDF, true);

                    Session["BinaryResult"] = s;
                    Session["TypeReport"] = "PDF";
                    Session["FileName"] = "NetuneyOved";

                    sScript = "window.showModalDialog('../../modalshowprint.aspx','','dialogwidth:800px;dialogheight:690px;dialogtop:10px;dialogleft:100px;status:no;resizable:yes;');";
                    MasterPage mp = (MasterPage)Page.Master;

                    ScriptManager.RegisterStartupScript(mp.ImagePrintClick, this.GetType(), "PrintPdf", sScript, true);
                }
            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }

    protected void btnHidden_OnClick(object sender, EventArgs e)
    {
        divNetunim.Visible = false;
        LoadDdlMonth();
        if (rdoId.Checked)
        {
            txtName.Enabled = false;
            txtEmpId.Enabled = true;
        }
        else
        {
            txtName.Enabled = true;
            txtEmpId.Enabled = false;
        }
    }
}
