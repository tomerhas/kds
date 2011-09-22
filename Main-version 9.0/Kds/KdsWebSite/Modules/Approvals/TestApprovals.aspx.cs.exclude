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
using KdsWorkFlow.Approvals;

public partial class Modules_Approvals_Test : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        btnApproval.Click += new EventHandler(btnApproval_Click);
        btnMail.Click += new EventHandler(btnMail_Click);
        btnHR.Click += new EventHandler(btnHR_Click);
    }

    void btnHR_Click(object sender, EventArgs e)
    {
        //input
        WorkCard workCard = new WorkCard();
        workCard.WorkDate = DateTime.Parse(calTaarich.Date);
        workCard.SidurStart = DateTime.Parse(calShatHatchala.Date);
        workCard.SidurNumber = int.Parse(txtMisparSidur.Text);
        workCard.ActivityStart = DateTime.Parse(calShatYetzia.Date);
        workCard.ActivityNumber = int.Parse(txtMisparKnisa.Text);
        
        var request = ApprovalRequest.CreateApprovalRequest(txtMisparIshi.Text, int.Parse(txtKodIshur.Text),
            workCard, RequestValues.Empty, true);
        
        //output
        txtGoremRashi.Text = request.MainFactor.EmployeeNumber.ToString();
        //approval
        lblKodTafkid.Text = request.Approval.JobCode.ToString();
        lblSugIshur.Text = request.Approval.ApprovalType.ToString();
        lblMeakevTashlum.Text = request.Approval.SuspendsPayment.ToString();
        lblSugPeilut.Text = ((int)request.Approval.AccessTypeToHR).ToString();
        //employee
        var kodNatuns = request.Employee.EmployeeDetails.GetEnumerator();
        while (kodNatuns.MoveNext())
        {
            TableRow tr = new TableRow();
            tblKodNatun.Rows.Add(tr);
            TableCell tc = new TableCell();
            tr.Cells.Add(tc);
            Label lblTitle = new Label();
            lblTitle.Text = kodNatuns.Current.Key.ToString();
            tc.Controls.Add(lblTitle);
            tc = new TableCell();
            tr.Cells.Add(tc);
            Label lblValue = new Label();
            tc.Controls.Add(lblValue);
            lblValue.Text = kodNatuns.Current.Value.ToString();
        }

        //hr sp access
        var hrParams = request.InputParamsForHR.GetEnumerator();
        while (hrParams.MoveNext())
        {
            TableRow tr = new TableRow();
            tblHrInput.Rows.Add(tr);
            TableCell tc = new TableCell();
            tr.Cells.Add(tc);
            Label lblTitle = new Label();
            lblTitle.Text = hrParams.Current.Key.ToString();
            tc.Controls.Add(lblTitle);
            tc = new TableCell();
            tr.Cells.Add(tc);
            Label lblValue = new Label();
            tc.Controls.Add(lblValue);
            if (hrParams.Current.Value != null)
                lblValue.Text = hrParams.Current.Value.ToString();
        }

    }

    void btnMail_Click(object sender, EventArgs e)
    {
        KdsBatch.Premia.PremiaFileImporter create = new KdsBatch.Premia.PremiaFileImporter(1253,
            new DateTime(2009, 11, 1), -1, 0);
        
        create.Execute();
       // var mailsFactory = new MailsFactory(0);
       // mailsFactory.SendApprovalMails();
        //ApprovalProcessPostActions pa = new ApprovalProcessPostActions(0);
        //pa.RunPostActions();
    }

    void btnApproval_Click(object sender, EventArgs e)
    {
        WorkCard workCard = new WorkCard();
        workCard.WorkDate = new DateTime(2010, 1, 11);
        //workCard.SidurNumber = 1;
        //workCard.SidurStart = DateTime.Now;
        //workCard.ActivityNumber = 1;
        //workCard.ActivityStart = DateTime.Now;
        //ApprovalRequest appr = ApprovalRequest.CreateApprovalRequest("69211", 10, workCard, false);
        //string errorMessage = String.Empty;
        //appr.ProcessRequest();
        //Response.Write(String.Format("{0} {1}", appr.State, appr.ErrorMessage));


        //int emp = 72064;
        //DateTime workdate = new DateTime(2009, 5, 14);
        //var requests =
        //    ApprovalRequest.GetMatchingApprovalRequestsWithStatuses(emp, workdate);

        //ApprovalFactory.ApprovalsEndOfDayProcess(DateTime.Now.Date);
       // ApprovalFactory.RaiseAllWorkDayApprovalCodes(DateTime.Now.Date, 10000);
        ApprovalRequest AppRequest;
        AppRequest = ApprovalRequest.CreateApprovalRequest("12090", 35, workCard, RequestValues.WithFirstRequestValues(20), true, "Siba", "My Siba");
        AppRequest.ProcessRequest();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
