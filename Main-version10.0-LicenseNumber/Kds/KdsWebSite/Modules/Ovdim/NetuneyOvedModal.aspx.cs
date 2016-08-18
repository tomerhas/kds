using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KdsLibrary.UI;
using KdsLibrary.BL;
using System.Data;
using KdsLibrary;

public partial class Modules_Ovdim_NetuneyOvedModal : KdsPage 
{
    private bool bShowData = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Id"] != null)
            {
                if (Request.QueryString["Id"].ToString().Length > 0)
                {
                    clOvdim oOvdim = new clOvdim();
                    lblEmpId.Text = Request.QueryString["Id"].ToString();
                    lblName.Text = oOvdim.GetOvedFullName(clGeneral.GetIntegerValue(lblEmpId.Text));
                  
                   SetFixedHeaderGrid("TabContainer1_pMeafyeneyBitzua_ucMeafyeneyBitzua_pnlContainer", head1);
                   SetFixedHeaderGrid("TabContainer1_pMeafyeneyBitzua_ucMeafyeneyBitzua_pnlGridHistoria", head1);
                   SetFixedHeaderGrid("TabContainer1_pPirteyOved_ucPirteyOved_pnlContainerDetails", head1);
                   SetFixedHeaderGrid("TabContainer1_pPirteyOved_ucPirteyOved_pnlgrdHistoria", head1);
                   SetFixedHeaderGrid("TabContainer1_pStatusOved_ucStatusOved_pnlGridStatus", head1);
               
                    LoadDdlMonth();
                    ddlMonth.SelectedValue = Request.QueryString["Month"].ToString();
                    btnShow_Click(this, new EventArgs());
                 
                }

            }
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        ShowData();
     }


    void LoadDdlMonth()
    {
        DataTable dtMonth;
        clOvdim oOvdim = new clOvdim();

        dtMonth = oOvdim.GetMonthsToOved(int.Parse(lblEmpId.Text));
        ddlMonth.DataSource = dtMonth;
        ddlMonth.DataBind();
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

            if (ddlMonth.SelectedValue.Length > 0 && clGeneral.IsNumeric(lblEmpId.Text))
            {
                MasterPage mp = (MasterPage)Page.Master;

                switch (TabContainer1.ActiveTabIndex)
                {
                    case 0:

                        ucMeafyeneyBitzua.EmployeeId = int.Parse(lblEmpId.Text);
                        ucMeafyeneyBitzua.MonthToShow = ddlMonth.SelectedValue;
                        ucMeafyeneyBitzua.ElementId = "TabContainer1_pMeafyeneyBitzua_ucMeafyeneyBitzua";
                        txtElement.Value = "TabContainer1_pMeafyeneyBitzua_ucMeafyeneyBitzua";

                        ucMeafyeneyBitzua.ShowBreratMechdal = chkWithDefault.Checked;

                        ucMeafyeneyBitzua.ShowData();
                        break;
                    case 1:
                        ucPirteyOved.SecurityPage = PageModule.SecurityLevel;
                        ucPirteyOved.EmployeeId = int.Parse(lblEmpId.Text);
                        ucPirteyOved.MonthToShow = ddlMonth.SelectedValue;
                        ucPirteyOved.ElementId = "TabContainer1_pPirteyOved_ucPirteyOved";
                        txtElement.Value = "TabContainer1_pPirteyOved_ucPirteyOved";

                        ucPirteyOved.ShowData();
                        break;
                    case 2:

                        ucStatusOved.EmployeeId = int.Parse(lblEmpId.Text);
                        ucStatusOved.MonthToShow = ddlMonth.SelectedValue;
                        ucStatusOved.ElementId = "TabContainer1_pStatusOved_ucStatusOved";
                        txtElement.Value = "TabContainer1_pStatusOved_ucStatusOved";

                        ucStatusOved.ShowData();
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
