using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using KdsLibrary.UI;
using KdsLibrary;
using KdsLibrary.BL;
using KdsLibrary.Security;

public partial class _Main : KdsPage
{
    public const int COL_TEUR_TECHNI = 4;
    public  string sUserId;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            lblHellowUser.Text = "  ωμεν  " + LoginUser.UserInfo.EmployeeFullName;
            sUserId = LoginUser.UserInfo.EmployeeNumber;
            DisplayDivMessages = false;
            lblDate.Text = DateTime.Now.ToShortDateString();
            LoadProcessLog();
            LoadMessages(lstMessages);
            MasterPage mp = (MasterPage)Page.Master;
            SetFixedHeaderGrid(pnlProcessLog.ClientID, mp.HeadPage);
            KdsSecurityLevel iSecurity = LoginUser.GetControlSecurityLevel("tblLogTahalichim");
            if (iSecurity == KdsSecurityLevel.ViewAll)
            {
                tblLogTahalichim.Style["display"] = "block";

            }
            else
            {
                tblLogTahalichim.Style["display"] = "none";

            }
        }
        catch (Exception ex)
        {
            clGeneral.BuildError(Page, ex.Message);
        }
    }

    public override  bool EnableControlSecurity
    {
        get { return true; }
    }

    private void LoadProcessLog()
     {
         DataTable dtProcessLog;
         clUtils oUtils = new clUtils();
         try
         {
             dtProcessLog = oUtils.GetLogTahalich();
             grdProcessLog.DataSource = dtProcessLog;
             grdProcessLog.DataBind();
         }
         catch (Exception ex)
         {
             throw ex;
         }
    }
    protected void grdProcessLog_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        KdsSecurityLevel iSecurity = LoginUser.GetControlSecurityLevel("ColTeurTechni");
        if (iSecurity == KdsSecurityLevel.ViewAll)
        {
            e.Row.Cells[COL_TEUR_TECHNI].Style["display"] = "block";

        }
        else
        {
            e.Row.Cells[COL_TEUR_TECHNI].Style["display"] = "none";

        }
    }
}