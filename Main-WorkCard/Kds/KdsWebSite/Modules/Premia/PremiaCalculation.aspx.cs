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
using KdsBatch.Premia;

public partial class Modules_Premia_PremiaCalculation : KdsLibrary.UI.KdsPage
{
    private const int STAGES_COUNT = 3;
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ddPeriods.SelectedIndexChanged += new EventHandler(ddPeriods_SelectedIndexChanged);
        ddBatchRequests.SelectedIndexChanged += new EventHandler(ddBatchRequests_SelectedIndexChanged);
        btnAdvance.Click += new EventHandler(btnAdvance_Click);
    }

    void btnAdvance_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(hdErrorMessage.Value))
            ErrorStage();
        else AdvanceToNextStage();

    }

    void ddBatchRequests_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reset();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        PageHeader = "חישוב פרמיות ניהול תנועה";
        if (!IsPostBack)
        {
            ServicePath = "~/Modules/WebServices/wsBatch.asmx";
            BindData();
        }
        hdUserId.Value = LoginUser.UserInfo.EmployeeNumber;
    }

    void ddPeriods_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBatchRequests();
        
    }

    private void Reset()
    {
        for (int i = 1; i <= STAGES_COUNT; ++i)
        {
            ResetStage(i, false);
        }
        if (ddBatchRequests.SelectedIndex >= 0)
            ResetStage(1, true);
        hdCurrentStage.Value = "1";
    }

    private void ResetStage(int stage, bool enable)
    {
        ((Button)upMain.FindControl("btnStage" + stage)).Enabled = enable;
        ((Image)upMain.FindControl("imgOK" + stage)).Visible = false;
        ((Image)upMain.FindControl("imgError" + stage)).Visible = false;
        ((Label)upMain.FindControl("lblMessage" + stage)).Text = String.Empty;
    }

    private void BindBatchRequests()
    {
        PremiaDataSource source = new PremiaDataSource();
        ddBatchRequests.DataTextField = "Teur_Bakasha";
        ddBatchRequests.DataValueField = "Bakasha_ID";
        ddBatchRequests.DataSource = source.GetPeriodBatchRequests(
            DateTime.Parse(ddPeriods.SelectedItem.Value));
        ddBatchRequests.DataBind();
        Reset();
    }

    private void ErrorStage()
    {
        int currentStage = int.Parse(hdCurrentStage.Value);
        ((Image)upMain.FindControl("imgOK" + currentStage)).Visible = false;
        ((Image)upMain.FindControl("imgError" + currentStage)).Visible = true;
        ((Label)upMain.FindControl("lblMessage" + currentStage)).Text += hdErrorMessage.Value;
        if (currentStage < STAGES_COUNT)
        {
            for (int i = currentStage + 1; i <= STAGES_COUNT; ++i)
                ResetStage(i, false);
        }
    }

    private void AdvanceToNextStage()
    {
        int currentStage = int.Parse(hdCurrentStage.Value);
        ((Image)upMain.FindControl("imgError" + currentStage)).Visible = false;
        ((Image)upMain.FindControl("imgOK" + currentStage)).Visible = true;
        ((Label)upMain.FindControl("lblMessage" + currentStage)).Text = String.Empty;
        int nextStage = currentStage + 1;
        if (nextStage <= STAGES_COUNT)
        {
            hdCurrentStage.Value = nextStage.ToString();
            ResetStage(nextStage, true);
        }
    }

    private void BindData()
    {
        PremiaDataSource source = new PremiaDataSource();
        foreach(DateTime period in source.GetPeriods())
        {
            ddPeriods.Items.Add(new ListItem(period.ToString("MM/yyyy"), 
                period.ToString(period.ToString("MM/yyyy"))));
        }
        BindBatchRequests();
    }

}
