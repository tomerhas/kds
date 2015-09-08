using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KdsLibrary;
using KdsLibrary.UI;


public partial class Modules_Ovdim_MonthlyQuotaAgafit : KdsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ServicePath = "~/Modules/WebServices/wsGeneral.asmx";
        PageHeader = "מחוץ למכסה - אגפי";
        UcMonthlyQuota.Init(LoginUser.UserInfo.EmployeeNumber, clGeneral.enMonthlyQuotaForm.Agafit);
     //   UcMonthlyQuota.ErrorRaised += new EventHandler(UcMonthlyQuota_ErrorRaised);
    }

    //private void UcMonthlyQuota_ErrorRaised(object sender, EventArgs e)
    //{
    //    HttpContext.Current.Response.Redirect(String.Format("{0}/{1}", "~",  NotAuthorizedRedirectPage));
    //}
}
