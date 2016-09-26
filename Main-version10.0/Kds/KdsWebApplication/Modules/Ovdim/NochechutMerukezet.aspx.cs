using KdsLibrary.BL;
using KdsLibrary.Security;
using KdsLibrary.UI;
using KdsLibrary.UI.SystemManager;
using KdsLibrary.Utils.Reports;
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
            TeurSidur,
            MisparSidurHid,
            ShatHatchalaHid,
            ShatHatchala,
            ShatGmarHid,
            ShatGmar,
            KodSibaIN ,
            KodSibaOUT,
            ShatHatchalaLetashlum,
            ShatGmarLetashlum,
            Hariga,
            LoLetashlumHid,
            LoLetashlumImg,
            noch_yomit,
            out_michsaHid,
            noch_chodshit,
            out_michsa_monthHid
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            grdEmployee.RowCreated += new GridViewRowEventHandler(grdEmployee_RowCreated);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dtParametrim = new DataTable();
            clUtils oUtils = new clUtils();
            DateTime date;
            bool flag = false;
            if (!Page.IsPostBack)
            {

                ServicePath = "~/Modules/WebServices/wsGeneral.asmx";
                PageHeader = "נוכחות מרוכזת";
                KdsSecurityLevel iSecurity = PageModule.SecurityLevel;
                if (iSecurity == KdsSecurityLevel.ViewAll) //|| iSecurity == KdsSecurityLevel.ViewOnlyEmployeeData)
                {
                    AutoCompleteExtenderID.ContextKey = "";
                    flag = true;
                }
                else if ((iSecurity == KdsSecurityLevel.UpdateEmployeeDataAndViewOnlySubordinates) || (iSecurity == KdsSecurityLevel.UpdateEmployeeDataAndSubordinates))
                {
                    AutoCompleteExtenderID.ContextKey = LoginUser.UserInfo.EmployeeNumber;
                }
                else
                {
                    AutoCompleteExtenderID.ContextKey = "";// LoginUser.UserInfo.EmployeeNumber;
                    flag = true;
                   
                }

                if (flag)
                {
                    rbAllTD.Style.Add("display", "none");
                    rdoMi.Style.Add("display", "none");
                    // rdoMi.Style.Add("checked", "checked");
                    rdoMi.Checked = true;
                    //  txtId.Style["disabled"] = "false";
                    //  txtId.Enabled = true;
                }
                else
                {
                    rdoAll.Checked = true;
                    txtId.Enabled = false;
                }
               
                dtParametrim = oUtils.getErechParamByKod("100", DateTime.Now.ToShortDateString());
                for (int i = 0; i < dtParametrim.Rows.Count; i++)
                    Params.Attributes.Add("Param" + dtParametrim.Rows[i]["KOD_PARAM"].ToString(), dtParametrim.Rows[i]["ERECH_PARAM"].ToString());

                if (Request.QueryString["mispar_ishi"] != null)
                {
                    txtId.Text = Request.QueryString["mispar_ishi"].ToString();
                    date = DateTime.Parse(Request.QueryString["chodesh"].ToString());
                    clnFromDate.Text =Request.QueryString["chodesh"].ToString();
                    clnToDate.Text = DateTime.Now.AddDays(-1) < date.AddMonths(1).AddDays(-1) ? DateTime.Now.AddDays(-1).ToShortDateString() : date.AddMonths(1).AddDays(-1).ToShortDateString();
                    rdoMi.Checked = true;
                    btnShow_Click(sender,e);
                }
                else
                {
                    clnFromDate.Text = DateTime.Now.AddDays(-1).ToShortDateString();
                    clnToDate.Text = DateTime.Now.AddDays(-1).ToShortDateString();
                }
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
                grdEmployee.PageSize = 30;
                mis = int.Parse(LoginUser.UserInfo.EmployeeNumber);
                if (AutoCompleteExtenderID.ContextKey == "")
                    mis = 0;
                // ds = oOvdim.GetNochechutMerukezet(mis,ovd_id, DateTime.Parse("01/05/2015"), DateTime.Parse("01/05/2015"));
                ds = oOvdim.GetNochechutMerukezet(mis, ovd_id, DateTime.Parse(clnFromDate.Text), DateTime.Parse(clnToDate.Text));
                Session["Employees"] = new DataView(ds.Tables[0]);
                Session["Nochechut"] = ds.Tables[1];
                grdEmployee.DataSource = ds.Tables[0];

                if (ds.Tables[0].Rows.Count > 0)
                    btnPrint.Visible = true;
                if (hidRefresh.Value == "false")
                {
                    hidScrollPos.Value = "0";
                    grdEmployee.PageIndex = 0;
                }

                grdEmployee.DataBind();
                divNetunim.Style["display"] = "block";
                ScriptManager.RegisterStartupScript(hidBtnShow, this.GetType(), "", "openDetails();", true);    
              
                hidRefresh.Value = "false";
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
            bool LoLetashlum = false;
            if (e.Row.RowType != DataControlRowType.EmptyDataRow && e.Row.RowType != DataControlRowType.Pager)
            {
                e.Row.Cells[enGrdNochechut.LoLetashlumHid.GetHashCode()].Style.Add("display", "none");
                e.Row.Cells[enGrdNochechut.MisparSidurHid.GetHashCode()].Style.Add("display", "none");
                e.Row.Cells[enGrdNochechut.ShatHatchalaHid.GetHashCode()].Style.Add("display", "none");
                e.Row.Cells[enGrdNochechut.ShatGmarHid.GetHashCode()].Style.Add("display", "none");
                e.Row.Cells[enGrdNochechut.out_michsaHid.GetHashCode()].Style.Add("display", "none");
                e.Row.Cells[enGrdNochechut.out_michsa_monthHid.GetHashCode()].Style.Add("display", "none");  
                
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                object[] row = ((DataRowView)(e.Row.DataItem)).Row.ItemArray;

                if (e.Row.Cells[enGrdNochechut.LoLetashlumHid.GetHashCode()].Text == "1")
                {
                    LoLetashlum = true;
                    ((Image)(e.Row.Cells[enGrdNochechut.LoLetashlumImg.GetHashCode()].FindControl("imgOK1"))).Visible = true; //Style["dispaly"]="inline";
                }//if (LoginUser.IsLimitedUser)

                ((HyperLink)e.Row.Cells[enGrdNochechut.Taarich.GetHashCode()].Controls[0]).Attributes.Add("OnClick", "javascript:OpenEmpWorkCard('" + row[0] + ", " + row[1].ToString().Split(' ')[0] + "')");

                ((WebControl)(e.Row.Cells[enGrdNochechut.TeurSidur.GetHashCode()])).ToolTip = e.Row.Cells[enGrdNochechut.MisparSidurHid.GetHashCode()].Text;
                if (!string.IsNullOrEmpty(row[6].ToString()))
                {
                    txt = e.Row.Cells[enGrdNochechut.ShatHatchalaHid.GetHashCode()].Text;
                    ((Label)(e.Row.Cells[enGrdNochechut.ShatHatchala.GetHashCode()].Controls[3])).Text = txt.Split(' ')[0];
                    ((Label)(e.Row.Cells[enGrdNochechut.ShatHatchala.GetHashCode()].Controls[1])).Text = txt.Split(' ')[1];

                }
                else if (e.Row.Cells[enGrdNochechut.MisparSidurHid.GetHashCode()].Text != "&nbsp;" && !(LoLetashlum))
                {
                    e.Row.Cells[enGrdNochechut.ShatHatchala.GetHashCode()].Style.Add("background-color", "red");
                }


                if (!string.IsNullOrEmpty(row[7].ToString()))
                {
                    txt = e.Row.Cells[enGrdNochechut.ShatGmarHid.GetHashCode()].Text;
                    ((Label)(e.Row.Cells[enGrdNochechut.ShatGmar.GetHashCode()].Controls[3])).Text = txt.Split(' ')[0];
                    ((Label)(e.Row.Cells[enGrdNochechut.ShatGmar.GetHashCode()].Controls[1])).Text = txt.Split(' ')[1];
                }
                else if (e.Row.Cells[enGrdNochechut.MisparSidurHid.GetHashCode()].Text != "&nbsp;" && !(LoLetashlum))
                {
                    e.Row.Cells[enGrdNochechut.ShatGmar.GetHashCode()].Style.Add("background-color", "red");
                }
                var out_michsa= e.Row.Cells[enGrdNochechut.out_michsaHid.GetHashCode()].Text.ToString();
                if (!string.IsNullOrEmpty(out_michsa) && out_michsa !=  "&nbsp;")
                {
                    e.Row.Cells[enGrdNochechut.noch_yomit.GetHashCode()].Text += " (מ.ל " + e.Row.Cells[enGrdNochechut.out_michsaHid.GetHashCode()].Text.ToString() + ")";
                }
                out_michsa = e.Row.Cells[enGrdNochechut.out_michsa_monthHid.GetHashCode()].Text.ToString();
                if (!string.IsNullOrEmpty(out_michsa) && out_michsa != "&nbsp;")
                {
                    e.Row.Cells[enGrdNochechut.noch_chodshit.GetHashCode()].Text += " (מ.ל " + e.Row.Cells[enGrdNochechut.out_michsa_monthHid.GetHashCode()].Text.ToString() + ")";
                }
               
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
            hidScrollPos.Value = "0";
            ScriptManager.RegisterStartupScript(hidBtnShow, this.GetType(), "", "openDetails();", true);


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
        protected void btnPrint_OnClick(object sender, EventArgs e)
        {
            //Dictionary<string, string> ReportParameters = new Dictionary<string, string>();
            string listMI = "";
            string rptName = ReportName.Presence.ToString();
            if (txtId.Text.Length > 0)
                listMI = txtId.Text;
            else
            {
                DataView view = (DataView)Session["Employees"];
                DataTable dt = view.ToTable(true,"mispar_ishi");
                foreach (DataRow dr in dt.Rows)
                    listMI += "," + dr[0];
                if(listMI.Length>0)
                   listMI = listMI.Substring(1);
            }

            clReportOnLine oReportOnLine = new clReportOnLine(rptName, eFormat.PDF);

            oReportOnLine.ReportParams.Add(new clReportParam("P_MISPAR_ISHI", listMI));
            oReportOnLine.ReportParams.Add(new clReportParam("P_STARTDATE", DateTime.Parse(clnFromDate.Text).ToShortDateString()));
            oReportOnLine.ReportParams.Add(new clReportParam("P_ENDDATE",  DateTime.Parse(clnToDate.Text).ToShortDateString()));

            //  ScriptManager.RegisterStartupScript(hidBtnShow, this.GetType(), "", "openDetails();", true);
            //  OpenReport(ReportParameters, (Button)sender, ReportName.Presence.ToString());

            OpenReportFile(oReportOnLine, (Button)sender, rptName, eFormat.PDF);


        }
    }


}