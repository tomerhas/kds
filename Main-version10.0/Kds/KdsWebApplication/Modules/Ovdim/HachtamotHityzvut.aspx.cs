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
using KdsLibrary;
using KdsLibrary.UI;
using KdsLibrary.Security;
using DalOraInfra.DAL;
using KdsLibrary.BL;

namespace KdsWebApplication.Modules.Ovdim
{
    public partial class HachtamotHityzvut : KdsPage
    {
        private string[] arrParams;
        private int iMisparIshiKiosk;

        private enum enGrdHityazvut
        {
            MisparSidur = 0,
            TeurSidur,
            ShatHityazvut,
            MikumHityazvut,
            FirstCol,
            SecondCol,
            FirstHid,
            SecHid
        }
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
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            DataView dv;
            DateTime taarich;
            if (!IsPostBack)
            {
                taarich = DateTime.Parse(Request.QueryString["CardDate"]);
                LabelHeader.Text = " החתמות התייצבות למספר אישי " + LoginUser.UserInfo.EmployeeNumber + " לתאריך " + taarich.ToShortDateString();                //DataTable dtAllErrors = Session["Errors"] as DataTable;
                dv = GetHityazvuyot(int.Parse(Request.QueryString["EmpID"]), DateTime.Parse(Request.QueryString["CardDate"]));
                grdHityazvut.DataSource = dv;
                grdHityazvut.DataBind();

                if (dv.Table.Rows.Count == 0)
                    LabelHeader.CssClass = "WorkCardRechivimGridHeader";
            }
        }

        private DataView GetHityazvuyot(int mispar_ishi , DateTime taarich)
        {
            clOvdim oOvdim = new clOvdim();
            DataTable dt;
            DataView dv;
       
             dt = oOvdim.GetOvedHityazvuyot(mispar_ishi, taarich);
             dv = new DataView(dt);
            return dv;
        }
        protected void grdHityazvut_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string txt;
           
            if (e.Row.RowType != DataControlRowType.EmptyDataRow && e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[enGrdHityazvut.FirstHid.GetHashCode()].Style.Add("display", "none");
                e.Row.Cells[enGrdHityazvut.SecHid.GetHashCode()].Style.Add("display", "none");
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
                txt = e.Row.Cells[enGrdHityazvut.FirstHid.GetHashCode()].Text;
                if (txt != "" && txt!="&nbsp;")
                {
                    ((Image)(e.Row.Cells[enGrdHityazvut.FirstHid.GetHashCode()].FindControl("imgFirst"))).Visible = true; //Style["dispaly"]="inline";
                }

                txt = e.Row.Cells[enGrdHityazvut.SecHid.GetHashCode()].Text;
                if (txt != "" && txt != "&nbsp;")
                {
                    ((Image)(e.Row.Cells[enGrdHityazvut.FirstHid.GetHashCode()].FindControl("imgSec"))).Visible = true; //Style["dispaly"]="inline";
                }

              
            }
        }

        protected override bool RequiresAuthorization
        {
            get
            {
                return false;
            }
        }

    }
}