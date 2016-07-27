using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using KdsLibrary.Security;
using KdsLibrary.BL;
using KdsLibrary.Utils.Reports;
using System.Collections.Generic;
using System.Diagnostics;

namespace KdsLibrary.UI
{
    /// <summary>
    /// Base page for all pages in Kds WebSite
    /// </summary>
    public abstract class KdsPage : Page
    {
        #region Fields
        private string _Header;
        private string _ServicePath;
        private LoginUser _loginUser;
        private SecurityManager _secManager;
        private KdsModule _pageModule;
        private string _Taarich;
        #endregion

        #region Events
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //get logged on user
            CreateUser();
            //check if user is in role for current page
            _secManager = new SecurityManager(_loginUser);

        }

        protected virtual void CreateUser()
        {
            _loginUser = LoginUser.GetLoginUser();
        }

        protected override void OnLoad(EventArgs e)
        {
            
            _secManager.AuthorizePage(this);
            
            base.OnLoad(e);
            if (!IsPostBack)
            {
                SetHeader(PageHeader);
                SetDateHeader(DateHeader);
                SetServiceRefference();                
            }
            Page.Title = "מערכת קדם שכר";

        }

     

        #endregion

        #region Methods

        protected void GoToHomePage()
        {
            Response.Redirect("~/Main.aspx", false);
        }


        protected void OpenReport(Dictionary<string, string> ReportParameters, Button btnScript, string sRdlName)
        {
            KdsReport _Report;
            KdsDynamicReport _KdsDynamicReport;
            string sDomain = "";
            _KdsDynamicReport = KdsDynamicReport.GetKdsReport();
            _Report = new KdsReport();
            _Report = _KdsDynamicReport.FindReport(sRdlName);

            KdsLibrary.BL.clReport rep = new KdsLibrary.BL.clReport();
            DataTable dt = rep.GetReportDetails(((ReportName)Enum.Parse(typeof(ReportName), sRdlName)).GetHashCode());
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                //_Report.PageHeader = dr["PageHeader"].ToString();
                _Report.RSVersion = dr["RS_VERSION"].ToString();
                _Report.URL_CONFIG_KEY = dr["URL_CONFIG_KEY"].ToString();
                _Report.SERVICE_URL_CONFIG_KEY = dr["SERVICE_URL_CONFIG_KEY"].ToString();
                _Report.EXTENSION = dr["EXTENSION"].ToString();
                //_Report.RdlName = RdlName;
            }

            Session["Report"] = _Report;
            Session["ReportParameters"] = ReportParameters;

            sDomain = clGeneral.AsDomain(Request.UrlReferrer.ToString()) + Request.ApplicationPath;
           // EventLog.WriteEntry("kds", "Url: " + sDomain);
           // string sScript = "window.showModalDialog('" + sDomain + "/modules/reports/ShowReport.aspx?Dt=" + DateTime.Now.ToString() + "&RdlName=" + sRdlName + "','','dialogwidth:1200px;dialogheight:800px;dialogtop:10px;dialogleft:100px;status:no;resizable:no;scroll:no;');";
            string sScript = "window.open('" + sDomain + "/modules/reports/ShowReport.aspx?Dt=" + DateTime.Now.ToString() + "&RdlName=" + sRdlName + "');";

            ScriptManager.RegisterStartupScript(btnScript, this.GetType(), "ReportViewer", sScript, true);

        }

        public void ProvideMenuForRole()
        {
            _secManager.ProvideMenuForRole(this);
        }

        public void SetServiceRefference()
        {

            try
            {
                if (_ServicePath != null)
                {
                    ScriptManager objScriptManager;
                    ServiceReference objRefService = new ServiceReference(_ServicePath);
                    if (Master != null)
                    {
                        objScriptManager = (ScriptManager)Master.FindControl("ScriptManagerKds");
                    }
                     else
                    {
                        objScriptManager = (ScriptManager)FindControl("ScriptManagerKds");                       
                    }
                    objScriptManager.Services.Add(objRefService);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadMessages( DataList lstMessages)
        {
            try
            {
                clUtils objUtils = new clUtils();
                DataTable dtAllMessages = new DataTable();
                dtAllMessages.Columns.Add("Melel_Hodaa", typeof(System.String));
                dtAllMessages.Columns.Add("Kod_Hodaa", typeof(System.Int32));
                foreach (UserProfile profile in LoginUser.UserProfiles)
                {
                    DataTable dtMessages = objUtils.GetHodaotToProfil(profile.Role, PageModule.ModuleID);
                    foreach (DataRow dr in dtMessages.Rows)
                    {
                        DataRow newRow = dtAllMessages.NewRow();
                        foreach (DataColumn dc in dtAllMessages.Columns)
                        {
                            newRow[dc.ColumnName] = dr[dc.ColumnName];
                        }
                        dtAllMessages.Rows.Add(newRow);
                    }
                }
                lstMessages.DataSource = dtAllMessages;
                lstMessages.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void SetHeader(string sHeader)
        {
            if (Master != null)
            {
                Label oblLabelHeader = (Label)Master.FindControl("lblHeader");
                oblLabelHeader.Text = sHeader;
            }
        }

        protected void SetFixedHeaderGrid(String sNameDivGrid,HtmlHead PageHead)
        {
            string sStyle;
            sStyle = "div#" + sNameDivGrid + " th {   " +
                       "top: expression(document.getElementById('" + sNameDivGrid + "').scrollTop-1); " +
                       "left: expression(parentNode.parentNode.parentNode.parentNode.scrollLeft); " +
                       "position: relative; " +
                       "}";
            Literal litCss = new Literal();
            litCss.Text = "<style type=" + '"' + "text/css" + '"' + ">" + sStyle + "</style>";
            PageHead.Controls.Add(litCss); 
        }

        protected void SetDateHeader(string sDateHeader)
        {
            if (Master != null)
            {
                Label oblLabelHeader = (Label)Master.FindControl("lblTaarich");
                oblLabelHeader.Text = sDateHeader;
            }
        }

        internal void SetPageModule(KdsModule pageModule)
        {
            _pageModule = pageModule;
            ViewState["PageModule"] = _pageModule;
        }
        #endregion

        #region Properties
        //protected TextBox TxtImpersonate
        //{
        //    get
        //    {
        //        return (TextBox)Master.FindControl("txtImpersonate");
        //    }
        //}
       
        public string PageHeader
        {
            get { return _Header; }
            set { _Header = value; }
        }

        public string DateHeader
        {
            get { return _Taarich; }
            set { _Taarich = value; }
        }

        public string ServicePath
        {
            get { return _ServicePath; }
            set { _ServicePath = value; }
        }

        public virtual bool EnableControlSecurity
        {
            get { return true; }
        }

        public virtual bool DisabledControls
        {
            get { return true; }            
        }
        public virtual string NotAuthorizedRedirectPage
        {
            get { return "NotAuthorizedLogin.aspx"; }
        }

        public virtual string MenuControlID
        {
            get { return "MenuMain"; }
        }
        /// <summary>
        /// Get the url root of the application according to his area the application is running 
        /// Local(development mode) /Server mode 
        /// </summary>
        public virtual string PureUrlRoot
        {
            get
            {
                string UrlRoot = string.Empty;
                UrlRoot = Page.Request.Url.AbsoluteUri.Remove(Page.Request.Url.AbsoluteUri.IndexOf(Page.Request.Url.AbsolutePath)) + Page.Request.ApplicationPath;
                return UrlRoot; 
            }
        }

        public bool DisplayDivMessages
        {
            set
            {
                ((HtmlGenericControl)Master.FindControl("DivMessages")).Visible = value;
            }
        }
        
        public LoginUser LoginUser
        {
            get { return _loginUser; }
            set { _loginUser = value; }
        }

        public KdsModule PageModule
        {
            get 
            {
                if (_pageModule == null) 
                    _pageModule = ViewState["PageModule"] as KdsModule;
                return _pageModule; 
            }
        }

        protected SecurityManager SecurityManager
        {
            get { return _secManager; }
        }

        protected internal virtual bool RequiresAuthorization
        {
            get { return true; }
        }
        #endregion
    }
}