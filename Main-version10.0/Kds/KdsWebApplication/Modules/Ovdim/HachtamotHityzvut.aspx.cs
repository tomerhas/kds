using KdsLibrary.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using KdsLibrary.Security;
using System.Web.UI.WebControls;

namespace KdsWebApplication.Modules.Ovdim
{
    public partial class HachtamotHityzvut : KdsPage
    {
        private DataTable _dtErrorCodes;
        //private DataTable _dtErrorDictionary;
        private const string ERROR_CODE_COLUMN = "kod_shgia";
        private const string ERROR_CHECK_NUM_COLUMN = "check_num";
        private const string ERROR_MISPAR_ISHI_COLUMN = "mispar_ishi";
        private const string ERROR_TAARICH_COLUMN = "taarich";
        private const string ERROR_DESC_COLUMN = "Teur_Shgia";
        private const string ERROR_FIELD_COLUMN = "TEUR";
        private string[] arrParams;
        private int iMisparIshiKiosk;
        protected override void CreateUser()
        {
            if (((Session["arrParams"] != null) && (Request.QueryString["Page"] != null)) || ((((string)Session["hidSource"]) == "1") && (Session["arrParams"] != null)))
            {
                arrParams = (string[])Session["arrParams"];
                SetUserKiosk(arrParams);
            }
            else { base.CreateUser(); }
        }


        private void SetUserKiosk(string[] arrParamsKiosk)
        {
            iMisparIshiKiosk = int.Parse(arrParamsKiosk[0].ToString());

            LoginUser = LoginUser.GetLimitedUser(iMisparIshiKiosk.ToString());
            LoginUser.InjectEmployeeNumber(iMisparIshiKiosk.ToString());
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //grdWorkDayErrors.PageIndexChanging += new GridViewPageEventHandler(grdWorkDayErrors_PageIndexChanging);
            //grdSidurErrors.PageIndexChanging += new GridViewPageEventHandler(grdSidurErrors_PageIndexChanging);
        }

        //void grdSidurErrors_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    grdSidurErrors.PageIndex = e.NewPageIndex;
        //    grdSidurErrors.DataSource = ViewState["SidurErrors"];
        //    grdSidurErrors.DataBind();
        //}

        //void grdWorkDayErrors_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    grdWorkDayErrors.PageIndex = e.NewPageIndex;
        //    grdWorkDayErrors.DataSource = ViewState["WorkDayErrors"];
        //    grdWorkDayErrors.DataBind();
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblHeaderMessage.Text = LoginUser.UserInfo.EmployeeNumber +  "נחתמות התייצבות למספר אישי ";                //DataTable dtAllErrors = Session["Errors"] as DataTable;
                //if (dtAllErrors == null)
                //    lblError.Visible = true;
                //else BindData(dtAllErrors);
            }
        }

    }
}