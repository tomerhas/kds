using KdsLibrary.BL;
using KdsLibrary.Security;
using KdsLibrary.UI;
using KdsLibrary.UI.SystemManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KdsWebApplication.Modules.Ovdim
{
    public partial class NochechutMerukezet : KdsPage
    {
 
         private enum enGrdOvdim
        {
            Sidurim=0,
            MisparIshi,
            Name ,
            YechidaIrgunit,
            StartTime,
            Endtime ,
            Gil,
            Isuk
        }
        private enum enGrdNochechut
        {
            Taarich=0,
            MisparSidur,
            TeurSidurHid ,
            ShatHatchalaHid,
            ShatHatchala,
            ShatGmarHid,
            ShatGmar,
            KodSibaIN ,
            KodSibaOUT,
            ShatHatchalaLetashlumHid,
            ShatHatchalaLetashlum,
            ShatGmarLetashlumHid,
            ShatGmarLetashlum,
            Hariga,
            LoLetashlumHid,
            LoLetashlumImg,
            noch_chodshit
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            grdEmployee.RowCreated += new GridViewRowEventHandler(grdEmployee_RowCreated);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {



                KdsSecurityLevel iSecurity = PageModule.SecurityLevel;
                if (iSecurity == KdsSecurityLevel.ViewAll) //|| iSecurity == KdsSecurityLevel.ViewOnlyEmployeeData)
                {
                    AutoCompleteExtenderID.ContextKey = "";
                }
                else if ((iSecurity == KdsSecurityLevel.UpdateEmployeeDataAndViewOnlySubordinates) || (iSecurity == KdsSecurityLevel.UpdateEmployeeDataAndSubordinates))
                {
                    AutoCompleteExtenderID.ContextKey = LoginUser.UserInfo.EmployeeNumber;

                }
                else
                {
                    AutoCompleteExtenderID.ContextKey = "";// LoginUser.UserInfo.EmployeeNumber;
                }

                clnFromDate.Text = DateTime.Now.AddDays(-1).ToShortDateString();
                clnToDate.Text = DateTime.Now.ToShortDateString();
            }

        }


        protected void btnShow_Click(object sender, EventArgs e)
        {
            clOvdim oOvdim = new clOvdim();
            DataSet ds;
            int mis,ovd_id=0;
            try
            {
                if (txtId.Text.Length > 0)
                    ovd_id = int.Parse(txtId.Text);
                grdEmployee.PageSize  =30;
                mis = int.Parse(LoginUser.UserInfo.EmployeeNumber);
               // ds = oOvdim.GetNochechutMerukezet(mis,ovd_id, DateTime.Parse("01/05/2015"), DateTime.Parse("01/05/2015"));
                ds = oOvdim.GetNochechutMerukezet(mis,ovd_id, DateTime.Parse(clnFromDate.Text), DateTime.Parse(clnToDate.Text));
                Session["Employees"] = new DataView(ds.Tables[0]);
                Session["Nochechut"] = ds.Tables[1]; 
                grdEmployee.DataSource =ds.Tables[0];
                grdEmployee.DataBind();

                ScriptManager.RegisterStartupScript(btnShow, this.GetType(), "", "openDetails()", true);

               

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grdEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType != DataControlRowType.EmptyDataRow && e.Row.RowType != DataControlRowType.Pager)
            //{
            //    e.Row.Cells[0].Style.Add("display", "none");
            //}
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //if (LoginUser.IsLimitedUser)
                //{
                //    ((HyperLink)e.Row.Cells[0].Controls[0]).NavigateUrl = "WorkCard.aspx?EmpID=" + txtId.Text + "&WCardDate=" + DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "sDate").ToString()).ToShortDateString() + "&Page=1&dt=" + DateTime.Now;
                //    ((HyperLink)e.Row.Cells[0].Controls[0]).Attributes.Add("OnClick", string.Concat("javascript:document.getElementById('divHourglass').style.display = 'block';"));

                //}
                //else
                //{
                //    ((HyperLink)e.Row.Cells[0].Controls[0]).Attributes.Add("OnClick", string.Concat("javascript:OpenEmpWorkCard('", DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "sDate").ToString()).ToShortDateString(), "')"));
                //}

                int mispar_ishi = int.Parse(((DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString());// grdEmployee.DataKeys[e.Row.RowIndex].Value.ToString();

                ((WebControl)(e.Row.Cells[enGrdOvdim.YechidaIrgunit.GetHashCode()])).ToolTip = "יחידה ארגונית";
              
                ((WebControl)(e.Row.Cells[enGrdOvdim.StartTime.GetHashCode()])).ToolTip = "מאפיין שעת התחלה מותרת חול";
                ((WebControl)(e.Row.Cells[enGrdOvdim.Endtime.GetHashCode()])).ToolTip = "מאפיין שעת גמר מותרת חול";
                ((WebControl)(e.Row.Cells[enGrdOvdim.Gil.GetHashCode()])).ToolTip = "גיל";
                ((WebControl)(e.Row.Cells[enGrdOvdim.Isuk.GetHashCode()])).ToolTip = "עיסוק";

                GridView gvNochechut = e.Row.FindControl("gvNochechut") as GridView;
                DataTable dt = GetData(mispar_ishi);
                if (dt.Rows.Count > 1)
                {
                    gvNochechut.DataSource = GetData(mispar_ishi);

                    gvNochechut.DataBind();
                }

            }

        }

        protected void gvNochechut_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string txt;
            if (e.Row.RowType != DataControlRowType.EmptyDataRow && e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[enGrdNochechut.LoLetashlumHid.GetHashCode()].Style.Add("display", "none");
                e.Row.Cells[enGrdNochechut.TeurSidurHid.GetHashCode()].Style.Add("display", "none");
                e.Row.Cells[enGrdNochechut.ShatHatchalaHid.GetHashCode()].Style.Add("display", "none");
                e.Row.Cells[enGrdNochechut.ShatGmarHid.GetHashCode()].Style.Add("display", "none");
                e.Row.Cells[enGrdNochechut.ShatHatchalaLetashlumHid.GetHashCode()].Style.Add("display", "none");
                e.Row.Cells[enGrdNochechut.ShatGmarLetashlumHid.GetHashCode()].Style.Add("display", "none");
                
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                object[] row = ((DataRowView)(e.Row.DataItem)).Row.ItemArray;
                
                if (e.Row.Cells[enGrdNochechut.LoLetashlumHid.GetHashCode()].Text == "1")
                    ((Image)(e.Row.Cells[enGrdNochechut.LoLetashlumImg.GetHashCode()].FindControl("imgOK1"))).Visible = true; //Style["dispaly"]="inline";                //if (LoginUser.IsLimitedUser)

                ((HyperLink)e.Row.Cells[enGrdNochechut.Taarich.GetHashCode()].Controls[0]).Attributes.Add("OnClick", "javascript:OpenEmpWorkCard('" + row[0] + ", " + row[1].ToString().Split(' ')[0] + "')");

                ((WebControl)(e.Row.Cells[enGrdNochechut.MisparSidur.GetHashCode()])).ToolTip = e.Row.Cells[enGrdNochechut.TeurSidurHid.GetHashCode()].Text;
                if (!string.IsNullOrEmpty(row[6].ToString()))
                {
                    txt = e.Row.Cells[enGrdNochechut.ShatHatchalaHid.GetHashCode()].Text;
                    ((Label)(e.Row.Cells[enGrdNochechut.ShatHatchala.GetHashCode()].Controls[3])).Text = txt.Split(' ')[0];
                    ((Label)(e.Row.Cells[enGrdNochechut.ShatHatchala.GetHashCode()].Controls[1])).Text = txt.Split(' ')[1];
                   
                }
                if (!string.IsNullOrEmpty(row[7].ToString()))
                {
                    txt = e.Row.Cells[enGrdNochechut.ShatGmarHid.GetHashCode()].Text;
                    ((Label)(e.Row.Cells[enGrdNochechut.ShatGmar.GetHashCode()].Controls[3])).Text = txt.Split(' ')[0];
                    ((Label)(e.Row.Cells[enGrdNochechut.ShatGmar.GetHashCode()].Controls[1])).Text = txt.Split(' ')[1];
                }
                if (!string.IsNullOrEmpty(row[8].ToString()))
                {
                    txt = e.Row.Cells[enGrdNochechut.ShatHatchalaLetashlumHid.GetHashCode()].Text;
                    ((Label)(e.Row.Cells[enGrdNochechut.ShatHatchalaLetashlum.GetHashCode()].Controls[3])).Text = txt.Split(' ')[0];
                    ((Label)(e.Row.Cells[enGrdNochechut.ShatHatchalaLetashlum.GetHashCode()].Controls[1])).Text = txt.Split(' ')[1];
                }
                if (!string.IsNullOrEmpty(row[9].ToString()))
                {
                    txt = e.Row.Cells[enGrdNochechut.ShatGmarLetashlumHid.GetHashCode()].Text;
                    ((Label)(e.Row.Cells[enGrdNochechut.ShatGmarLetashlum.GetHashCode()].Controls[3])).Text = txt.Split(' ')[0];
                    ((Label)(e.Row.Cells[enGrdNochechut.ShatGmarLetashlum.GetHashCode()].Controls[1])).Text = txt.Split(' ')[1];
                }
              //  ((TextBox)e.Row.Cells[SHAT_YETZIA].FindControl("txtShatYezia")).ToolTip = e.Row.Cells[SHAT_YEZIA_DATE].Text.Split(' ')[1] + " " +
                //                                                                                                 e.Row.Cells[SHAT_YEZIA_DATE].Text.Split(' ')[0];
            }
        }

           
       

        private  DataTable GetData(int mispar_ishi)
        {
            DataRow[] drs;
            DataTable dt=new DataTable();
            drs = ((DataTable)Session["Nochechut"]).Select("mispar_ishi="+mispar_ishi);
            if(drs.Length>0)
             dt = drs.CopyToDataTable();
           // var dr=dt.NewRow();
            dt.Rows.Add(dt.NewRow());
            return dt;// drs.CopyToDataTable();
           

        }

        protected void grdEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdEmployee.PageIndex = e.NewPageIndex;
            //LoadOvdimGrid("", "");
            grdEmployee.DataSource = (DataView)Session["Employees"];
            grdEmployee.DataBind();
           
        }

        //*******Pager Functions*********/
        void grdEmployee_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                IGridViewPager gridPager = e.Row.FindControl("ucGridPager")
                as IGridViewPager;
                if (gridPager != null)
                {
                    gridPager.PageIndexChanged += delegate(object pagerSender,
                        GridViewPageEventArgs pagerArgs)
                    {
                        ChangeGridPage(pagerArgs.NewPageIndex, grdEmployee,
                           (DataView)Session["Employees"], "SortDirection",
                       "SortExp");
                    };
                }
            }
        }
        private void ChangeGridPage(int pageIndex, GridView grid, DataView dataView,
                                    string sortDirViewStateKey, string sortExprViewStateKey)
        {
            //   SetChangesOfGridInDataview(grid, ref dataView);


            grid.PageIndex = pageIndex;
           // Session["Params"] = Session["Params"].ToString().Substring(0, Session["Params"].ToString().LastIndexOf(';')) + ";" + pageIndex;

            string sortExpr = String.Empty;
            SortDirection sortDir = SortDirection.Ascending;
            if (ViewState[sortExprViewStateKey] != null)
            {
                sortExpr = ViewState[sortExprViewStateKey].ToString();
                if (ViewState[sortDirViewStateKey] != null)
                    sortDir = (SortDirection)ViewState[sortDirViewStateKey];
                dataView.Sort = String.Format("{0} {1}", sortExpr,
                    ConvertSortDirectionToSql(sortDir));
            }
            grid.DataSource = dataView;
            grid.DataBind();
            ScriptManager.RegisterStartupScript(btnShow, this.GetType(), "", "openDetails();", true);


        }
        private string ConvertSortDirectionToSql(SortDirection sortDirection)
        {
            string newSortDirection = String.Empty;

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    newSortDirection = "ASC";
                    break;

                case SortDirection.Descending:
                    newSortDirection = "DESC";
                    break;
            }

            return newSortDirection;
        }
        /********************/
    }


}