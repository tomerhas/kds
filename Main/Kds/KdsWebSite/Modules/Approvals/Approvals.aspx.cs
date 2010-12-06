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
using System.Drawing;
using System.Collections.Generic;
using KdsLibrary.UI;
using KdsLibrary.Utils;
using KdsWorkFlow.Approvals;
using KdsLibrary.UI.SystemManager;

public partial class Modules_Approvals_Approvals : KdsPage
{
    #region Fields
    ApprovalManager _approvManage;
    private const string ACCEPT_ITEMS = "AcceptItems";
    private const string DECLINE_ITEMS = "DeclineItems";
    private const string SAVED_ERRORS = "ApprovalErrors";
    private const string SAVED_TAG = "SavedTags";
    protected DataTable dtAuthorizationJobs;
    private bool _refreshOnLoad;
    #endregion

    #region Events
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ddStatuses.SelectedIndexChanged += new EventHandler(ddStatuses_SelectedIndexChanged);
        btnExecute.Click += new EventHandler(btnExecute_Click);
        btnSearch.Click += new EventHandler(btnSearch_Click);
        grdRequests.RowDataBound += new GridViewRowEventHandler(grdRequests_RowDataBound);
        grdRequests.DataBound += new EventHandler(grdRequests_DataBound);
        grdRequests.PageIndexChanging += new GridViewPageEventHandler(grdRequests_PageIndexChanging);
        grdRequests.RowCreated += new GridViewRowEventHandler(grdRequests_RowCreated);
        btnOK.Click += new EventHandler(btnOK_Click);
        chkCodesToMark.SelectedIndexChanged += new EventHandler(chkCodesToMark_SelectedIndexChanged);
        btnSubmit.Click += new EventHandler(btnSubmit_Click);
    }

    protected override void ButImpersonate_Click(object sender, EventArgs e)
    {
        base.ButImpersonate_Click(sender, e);
        BindRequestsDates();
    }

    void grdRequests_RowCreated(object sender, GridViewRowEventArgs e)
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
                    ChangeGridPage(pagerArgs.NewPageIndex);
                };
            }
        }
    }

    void btnSubmit_Click(object sender, EventArgs e)
    {
        Session[SAVED_ERRORS] = null;
        //Collect Accepted/Declined/Forwarded Requests and run approval process
        List<ApprovalKeyWithTag> accceptRequests = new List<ApprovalKeyWithTag>();
        List<ApprovalKeyWithTag> declineRequests = new List<ApprovalKeyWithTag>();
        List<ApprovalKeyWithTag> forwardRequests = new List<ApprovalKeyWithTag>();
        List<ApprovalKeyWithTag> errorRequests = null;
        CollectApprovalRequestsForProcess(accceptRequests, declineRequests, 
            forwardRequests);
        ProcessAprovalRequests(accceptRequests, declineRequests, forwardRequests,
            out errorRequests);
        Session[SAVED_ERRORS] = errorRequests;
        RefreshData();
        ShowErrors();
    }

    void grdRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ChangeGridPage(e.NewPageIndex);
    }

    void chkCodesToMark_SelectedIndexChanged(object sender, EventArgs e)
    {
        int x = chkCodesToMark.SelectedIndex;
    }

    void btnOK_Click(object sender, EventArgs e)
    {
        MarkAllRelevantCodes();
        cpe.Collapsed = true;
        cpe.ClientState = "true";
    }
   
    void grdRequests_DataBound(object sender, EventArgs e)
    {
        EnableActionButtons(grdRequests.Rows.Count > 0);
        BindApprovalCodes();
    }

    void grdRequests_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            EnableCheckBoxes(e.Row);
            EnableFactorsDropDown(e.Row);
            CreateExtraHoursApprovalLink(e.Row);
        }

    }

   
    void btnSearch_Click(object sender, EventArgs e)
    {
        SearchOvedInGrid();
    }

    void btnExecute_Click(object sender, EventArgs e)
    {
        ClearSessionLists();
        RefreshData();
        ShowErrors();
    }
    
    protected void ddFactor_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddFactor = sender as DropDownList;
        GridViewRow row = ddFactor.NamingContainer as GridViewRow;
        EnableCheckBoxes(row);
        MarkRowStatusChanged(row);
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        DisableMasterUpdateProgress();
        if (TxtImpersonate != null && !String.IsNullOrEmpty(TxtImpersonate.Text))
            LoginUser.InjectEmployeeNumber(TxtImpersonate.Text);
        _approvManage = new ApprovalManager(LoginUser);
        if (!IsPostBack)
        {
            ServicePath = "~/Modules/WebServices/wsGeneral.asmx";
            PageHeader = "אישורים נדרשים";
            BindStatuses();
            BindRequestsDates();
            EnableActionButtons(false);
            SetFields();
            Session[SAVED_ERRORS] = null;
        }
        Title = PageHeader;
        RegisterBodyOnloadEvent();
        BindApprovalJobs();
        if (_refreshOnLoad) RefreshData();
        ShowErrors();
    }

    private void DisableMasterUpdateProgress()
    {
        UpdateProgress updPrgrs = Page.Master.FindControl("Progress1") as UpdateProgress;
        updPrgrs.Visible = false;
    }

    private void BindApprovalJobs()
    {
        dtAuthorizationJobs = ViewState["AuthorizationJobs"] as DataTable;
        if (dtAuthorizationJobs == null)
        {
            dtAuthorizationJobs = _approvManage.GetApprovalJobs();
            var dr = dtAuthorizationJobs.NewRow();
            dr["Kod_Tafkid_Measher"] = -1;
            dr["Teur_Tafkid_Measher"] = String.Empty;
            dtAuthorizationJobs.Rows.InsertAt(dr, 0);
            ViewState["AuthorizationJobs"] = dtAuthorizationJobs;
        }
    }
    
    void ddStatuses_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddMonths.Enabled = ddStatuses.SelectedIndex == 0 ||
            ApprovalManager.AllowMonthFilteringForStatus(
            (ApprovalRequestState)int.Parse(ddStatuses.SelectedValue));
    }

    protected void chkAccept_CheckChanged(object sender, EventArgs e)
    {
        var chk = (CheckBox)sender;
        SynchronizeRadioButtons(chk, true);
        EnableFactorsDropDown(chk.NamingContainer as GridViewRow);
        MarkRowStatusChanged(chk.NamingContainer as GridViewRow);
    }

    protected void vldEmpNotExists_ServerValidate(object source, ServerValidateEventArgs args)
    {

    }

    protected void lnkRemarks_Click(object sender, EventArgs e)
    {
        var lnkRemarks = sender as Control;
        if (lnkRemarks != null)
        {
            var row = lnkRemarks.NamingContainer as GridViewRow;
            if (row != null)
            {
                var hdRemarks = row.FindControl("hdRemarks") as HiddenField;
                txtEditRemarks.Text = hdRemarks.Value;
                grdRequests.SelectedIndex = row.RowIndex;
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "editRemarks",
                    "openEditRemarks();", true);
            }
        }
    }

    protected void imgForwardInfo_Click(object sender, EventArgs e)
    {
        var imgForward = sender as Control;
        if (imgForward != null)
        {
            var row = imgForward.NamingContainer as GridViewRow;
            if (row != null)
            {
                int level = int.Parse((row.FindControl("hdLevel") as Label).Text);
                string ctlName = level % 10 == 0 ? "Orig" : "Forward";
                lblForwardTitle.Text = level % 10 == 0 ? "הועבר מ:" : "גורם נוסף:";
                lblForwadName.Text = (row.FindControl(String.Format("hd{0}Name", ctlName)) as HiddenField).Value;
                //lblForwardNum.Text = (row.FindControl("hdForwardNum") as HiddenField).Value;
                lblForwardStatus.Text = (row.FindControl(String.Format("hd{0}Status", ctlName)) as HiddenField).Value;
                lblForwardRemark.Text = (row.FindControl(String.Format("hd{0}Remark", ctlName)) as HiddenField).Value;
                lblForwardDate.Text = (row.FindControl(String.Format("hd{0}Date", ctlName)) as HiddenField).Value;
                grdRequests.SelectedIndex = row.RowIndex;
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "showForwardInfo",
                    "showForwardInfo();", true);
            }
        }
    }
    protected void btnSaveRemarks_Click(object sender, EventArgs e)
    {
        var row = grdRequests.SelectedRow;
        var hdRemarks = row.FindControl("hdRemarks") as HiddenField;
        hdRemarks.Value = txtEditRemarks.Text;
        _approvManage.UpdateRemark(GetRowKey(grdRequests.SelectedDataKey), 
            hdRemarks.Value);
        RefreshData(true);
    }
    #endregion

    #region Methods

    private void EnableFactorsDropDown(GridViewRow row)
    {
        bool isChecked = ((CheckBox)row.FindControl("chkAccept")).Checked ||
            ((CheckBox)row.FindControl("chkDecline")).Checked;
        ApprovalKeyWithTag key = GetRowKey(grdRequests.DataKeys[row.RowIndex]);
        ((DropDownList)row.FindControl("ddFactor")).Enabled = !isChecked
            && (key.Approval.Level % 10 != 0) &&
            !ApprovalManager.RequiresAdditional(key.Approval.Code) &&
            String.IsNullOrEmpty(((HiddenField)row.FindControl("hdForwardNum")).Value);
    }

    private void EnableCheckBoxes(GridViewRow row)
    {
        bool enabledAllow = false;
        bool enabledDecline = false;
        int code = int.Parse(((Label)row.FindControl("hdCode")).Text);
        int level = int.Parse(((Label)row.FindControl("hdLevel")).Text);
        ApprovalRequestState status = (ApprovalRequestState)
            int.Parse(((Label)row.FindControl("hdStatus")).Text);
        ApprovalRequestState nextLevelStatus = (ApprovalRequestState)
            int.Parse(((Label)row.FindControl("hdNextLevelStatus")).Text);
        enabledAllow = KdsWorkFlow.Approvals.ApprovalManager.AllowRegularApproval(
            code, status, nextLevelStatus);
        DropDownList ddFactors = row.FindControl("ddFactor") as DropDownList;
        if (enabledAllow)
            enabledAllow = !KdsWorkFlow.Approvals.ApprovalManager.RequiresAdditional(code);
        enabledDecline = !KdsWorkFlow.Approvals.ApprovalManager.RequiresAdditional(code);
        ((CheckBox)row.FindControl("chkAccept")).Enabled = enabledAllow;
        ((CheckBox)row.FindControl("chkDecline")).Enabled = enabledDecline;
    }

    private void CreateExtraHoursApprovalLink(GridViewRow row)
    {
        int code = int.Parse(((Label)row.FindControl("hdCode")).Text);
        if (ApprovalManager.ShowCodeDescription(code))
        {
            var key = GetRowKey(grdRequests.DataKeys[row.RowIndex]);
            ApprovalRequestState status = (ApprovalRequestState)
                int.Parse(((Label)row.FindControl("hdStatus")).Text);
            ApprovalRequestState nextLevelStatus = (ApprovalRequestState)
                int.Parse(((Label)row.FindControl("hdNextLevelStatus")).Text);
            bool enabledAllow = KdsWorkFlow.Approvals.ApprovalManager.AllowSpecialApproval(
                code, status, nextLevelStatus);
            string extraNavUrl = String.Format(@"../Ovdim/BakashatTashlum.aspx?MisparIshi={0}&Taarich={1}&BakashaId={2}&UserId={3}&SidurNum={4}&SidurStart={5}&ActivityStart={6}&ActivityNum={7}&KodIshur={8}&Level={9}&AllowAccept={10}&Tzuga={11}&ErechMevukash={12}&ErechMevukash2={13}&Approve=true",
                key.Employee.EmployeeNumber, key.WorkCard.WorkDate.ToShortDateString(), ((Label)row.FindControl("hdBakashaID")).Text,
                LoginUser.UserInfo.EmployeeNumber,
                key.WorkCard.SidurNumber, key.WorkCard.SidurStart, key.WorkCard.ActivityStart, key.WorkCard.ActivityNumber,
                key.Approval.Code, key.Approval.Level, enabledAllow,
                ApprovalManager.ExtraHoursDisplayValue(key.Approval.Code),
                key.RequestValues[RequestValues.FIRST_REQUEST_VALUE_KEY],
                key.RequestValues[RequestValues.SECOND_REQUEST_VALUE_KEY]);
            
            row.Cells[4].CssClass = "hpLink";
            row.Cells[4].Attributes.Add("onclick",
                String.Format("OpenExtraHoursApproval('{0}')",
                extraNavUrl.Replace(Environment.NewLine, String.Empty)));
                
        }
    }


    private void RegisterBodyOnloadEvent()
    {
        HtmlGenericControl body = null;
        if (Master != null)
            body = (HtmlGenericControl)Page.Master.FindControl("MasterBody");
        if (body != null) body.Attributes.Add("onload", "load()");
    }

    private void BindRequestsDates()
    {
        ddMonths.DataSource = _approvManage.GetApprovalRequestsDates();
        ddMonths.DataBind();
        if (!String.IsNullOrEmpty(Request.QueryString["month"]))
        {
            foreach (ListItem item in ddMonths.Items)
            {
                if (item.Text.Equals(Request.QueryString["month"]))
                {
                    ddMonths.SelectedIndex = ddMonths.Items.IndexOf(item);
                    _refreshOnLoad = true;
                    break;
                }
            }
        }
    }

    private void BindStatuses()
    {
        DataTable dt = _approvManage.GetApprovalStatuses();
        DataRow dr = dt.NewRow();
        dr[0] = -1;
        dr[1] = "כל הסטטוסים";
        dt.Rows.InsertAt(dr, 0);
        ddStatuses.DataSource = dt;
        ddStatuses.DataBind();
        ddStatuses.SelectedIndex = ddStatuses.Items.Count - 1;
        ddStatuses_SelectedIndexChanged(ddStatuses, EventArgs.Empty);
    }

    private void BindApprovalCodes()
    {
        var dt = ViewState["GridSource"] as DataTable;
        chkCodesToMark.DataSource = dt.SelectDistinct("Kod_Ishur", "Teur_Ishur");
        chkCodesToMark.DataBind();
    }

    private void SynchronizeRadioButtons(CheckBox chkSource)
    {
        SynchronizeRadioButtons(chkSource, false);
    }

    private void SynchronizeRadioButtons(CheckBox chkSource, bool performLevelCheck)
    {
        GridViewRow row = chkSource.NamingContainer as GridViewRow;
        grdRequests.SelectedIndex = row.RowIndex;
        if (performLevelCheck && chkSource.ID.Contains("Decline"))
        {
            if (IsRequestForwardedAndAnswered(row))
            {
                chkSource.Checked = false;
                ShowWarning();
                return;
            }
        }
        string targetID = chkSource.ID.Contains("Accept") ? "chkDecline" : "chkAccept";
        var chkTarget = row.FindControl(targetID) as CheckBox;
        if (chkSource.Checked) chkTarget.Checked = false;
    }

    private void ShowWarning()
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
            "forwardWarning", "showWarning();", true);
    }

    private bool IsRequestForwardedAndAnswered(GridViewRow row)
    {
        var approvalKey = GetRowKey(grdRequests.DataKeys[row.RowIndex]);
        string level = GetValueFromRow(row, "hdLevel");
        return _approvManage.IsRequestForwardedAndAnswered(approvalKey, 
            int.Parse(level));
    }

    private void SearchOvedInGrid()
    {
        int iPageIndex = 0;
        int iPos = 0;

        DataView dv;

        //Find Employee In Grid
        if (!String.IsNullOrEmpty(txtId.Text))
        {
            int iMisparIshi = int.Parse(txtId.Text);
            if (ViewState["GridSource"] != null)
            {
                dv = (ViewState["GridSource"] as DataTable).DefaultView;
                dv.Sort = "Mispar_Ishi";
                iPos = dv.Find(iMisparIshi) + 1;
                iPageIndex = iPos / grdRequests.PageSize;
                if ((iPos % grdRequests.PageSize) == 0)
                {
                    iPageIndex = iPageIndex - 1;
                    iPos = grdRequests.PageSize - 1;
                }
                else
                {
                    iPos = (iPos % grdRequests.PageSize) - 1;
                }
                if (iPageIndex >= 0)
                {
                    ChangeGridPage(iPageIndex);
                    grdRequests.SelectedIndex = iPos;
                }
                else
                {
                    grdRequests.SelectedIndex = -1;
                }
            }
            else
            {
                txtName.Text = "";
                txtId.Text = "";
                vldEmpNotExists.IsValid = false;
            }
        }

    }

    private void EnableActionButtons(bool enable)
    {
        TitlePanel.Enabled = enable;
        Label2.Enabled = enable;
        cpe.Enabled = enable;
        btnSubmit.CssClass = enable ? "ImgButtonSearch" : "ImgButtonSearchDisable";
        btnSubmit.Enabled = enable;
    }

    private void SetFields()
    {

        rdoId.Checked = true;
        rdoId.Attributes.Add("onclick", "SetTextBox();");
        rdoName.Attributes.Add("onclick", "SetTextBox();");
    }

    private void MarkAllRelevantCodes()
    {
        List<string> selectedCodes = (from ListItem li in chkCodesToMark.Items
                                      where li.Selected == true
                                      select li.Value).ToList();
       

       for (int i = 0; i < grdRequests.PageCount; ++i)
       {
           ChangeGridPage(i);
           for (int j = 0; j < grdRequests.Rows.Count; ++j)
           {
               string codeInRow = GetValueFromRow(grdRequests.Rows[j], "hdCode");
               if (selectedCodes.Find(delegate(string codeInList)
               {
                   return codeInList.Equals(codeInRow);
               }) != null) MarkRow(grdRequests.Rows[j]);
           }
       }
       ChangeGridPage(0);
       
    }

    private void MarkRow(GridViewRow gridViewRow)
    {
        var chkAccept = gridViewRow.FindControl("chkAccept") as CheckBox;
        if (chkAccept != null && chkAccept.Enabled)
        {
            var key = GetRowKey(grdRequests.DataKeys[gridViewRow.RowIndex]);
            chkAccept.Checked = true;
            SynchronizeRadioButtons(chkAccept);
        }
    }

    private void ChangeGridPage(int newPageIndex)
    {
        RememberOldValues();
        grdRequests.PageIndex = newPageIndex;
        grdRequests.DataSource = ViewState["GridSource"];
        grdRequests.DataBind();
        RePopulateValues();
        if (grdRequests.Rows.Count > 0) grdRequests.SelectedIndex = 0;
    }

    private string GetValueFromRow(GridViewRow gridViewRow, string ctlName)
    {
        string value = null;
        var ctl = gridViewRow.FindControl(ctlName) as Label;
        if (ctl != null) value = ctl.Text;
        return value;
    }
    
    private void RememberOldValues()
    {
        List<ApprovalKey> checkedAccept = new List<ApprovalKey>();
        List<ApprovalKey> checkedDecline = new List<ApprovalKey>();
        List<ApprovalKeyWithTag> saved = null;
        GetListFromSession(ref saved, SAVED_TAG);
        foreach (GridViewRow row in grdRequests.Rows)
        {
            ApprovalKeyWithTag key = GetRowKey(grdRequests.DataKeys[row.RowIndex]);
            
            SaveTag(saved, row, key);
            bool enabled = ((CheckBox)row.FindControl("chkAccept")).Enabled;
            if (enabled)
            {

                bool isAccept = ((CheckBox)row.FindControl("chkAccept")).Checked;
                bool isDecline = ((CheckBox)row.FindControl("chkDecline")).Checked;

                // Check in the Session
                if (Session[ACCEPT_ITEMS] != null)
                    checkedAccept = (List<ApprovalKey>)Session[ACCEPT_ITEMS];
                if (Session[DECLINE_ITEMS] != null)
                    checkedDecline = (List<ApprovalKey>)Session[DECLINE_ITEMS];

                ApplyItemToList(key, checkedAccept, isAccept);
                ApplyItemToList(key, checkedDecline, isDecline);
               
            }
        }
        AddListToSession(checkedAccept, ACCEPT_ITEMS);
        AddListToSession(checkedDecline, DECLINE_ITEMS);
        AddListToSession(saved, SAVED_TAG);
    }
    
    private void AddListToSession<T>(List<T> list, string key)
    {
        if (list != null && list.Count > 0)
            Session[key] = list;
    }

    private void GetListFromSession<T>(ref List<T> list, string key)
    {
        list = Session[key]
            as List<T>;
        if (list == null) list = new List<T>();
    }
    
    private void RePopulateValues()
    {
        List<ApprovalKey> checkedAccept = 
            (List<ApprovalKey>)Session[ACCEPT_ITEMS];
        List<ApprovalKey> checkedDecline =
             (List<ApprovalKey>)Session[DECLINE_ITEMS];
        List<ApprovalKeyWithTag> errors =
           (List<ApprovalKeyWithTag>)Session[SAVED_ERRORS];
        List<ApprovalKeyWithTag> saved =
             (List<ApprovalKeyWithTag>)Session[SAVED_TAG];
        foreach (GridViewRow row in grdRequests.Rows)
        {
                
            ApprovalKeyWithTag key = GetRowKey(grdRequests.DataKeys[row.RowIndex]);
            RestoreTag(saved, row, key);
            bool enabled = ((CheckBox)row.FindControl("chkAccept")).Enabled;
            if (enabled)
            {
             
                ApplyValueFromList(key, checkedAccept, "chkAccept", row);
                ApplyValueFromList(key, checkedDecline, "chkDecline", row);
            }
            EnableCheckBoxes(row);
            EnableFactorsDropDown(row);
            MarkErrorRow(errors, key, row);
        }
        
    }

    private void MarkErrorRow(List<ApprovalKeyWithTag> errors, 
        ApprovalKeyWithTag key, GridViewRow row)
    {
        if (errors != null && errors.Count > 0)
        {
            if (errors.Find(delegate(ApprovalKeyWithTag keyInList)
            {
                return key.IsEqual(keyInList);
            }) != null)
            {
                row.CssClass = "ErrorAppr";
            }
        }
    }

    private void FillTagForApprovalKey(ApprovalKeyWithTag key, GridViewRow row)
    {
        key.AppTag.Remark = ((HiddenField)row.FindControl("hdRemarks")).Value;
        key.AppTag.Factor = int.Parse(((DropDownList)row.FindControl("ddFactor")).SelectedValue);
        key.AppTag.Changed = !String.IsNullOrEmpty(((Label)row.FindControl("hdChanged")).Text);
        key.AppTag.EmployeeName = row.Cells[3].Text;
        key.AppTag.ApprovalCodeDescriptiom = row.Cells[4].Text;
    }

    private void SaveTag(List<ApprovalKeyWithTag> saved, GridViewRow row,
        ApprovalKeyWithTag key)
    {
        if (saved != null)
        {
            FillTagForApprovalKey(key, row);

            var foundKey =
                    saved.Find(delegate(ApprovalKeyWithTag keyInList)
                    {
                        return key.IsEqual(keyInList);
                    });
            if (foundKey != null)
                saved.Remove(foundKey);
            saved.Add(key);
        }
    }

    private void RestoreTag(List<ApprovalKeyWithTag> saved, GridViewRow row,
        ApprovalKeyWithTag key)
    {
        if (saved != null)
        {
            var foundKey =
                    saved.Find(delegate(ApprovalKeyWithTag keyInList)
                    {
                        return key.IsEqual(keyInList);
                    });
            if (foundKey != null)
            {
                ((HiddenField)row.FindControl("hdRemarks")).Value =
                    foundKey.AppTag.Remark;
                ((Label)row.FindControl("hdChanged")).Text = foundKey.AppTag.Changed ? "1" : String.Empty;
                ((DropDownList)row.FindControl("ddFactor")).SelectedValue =
                    foundKey.AppTag.Factor.ToString();

            }
        }
    }

    private void ApplyValueFromList(ApprovalKey key, List<ApprovalKey> list,
        string controlName, GridViewRow row)
    {
        if (list != null && list.Count > 0)
        {
            if (list.Find(delegate(ApprovalKey keyInList)
            {
                return key.IsEqual(keyInList);
            }) != null)
            {
                CheckBox chkbox = (CheckBox)row.FindControl(controlName);
                chkbox.Checked = true;
                SynchronizeRadioButtons(chkbox);
            }
        }
    }

    private void ApplyItemToList(ApprovalKey key, List<ApprovalKey> list, 
        bool shouldBeInList)
    {
        if (shouldBeInList)
        {
            if (list.Find(delegate(ApprovalKey keyInList)
            {
                return key.IsEqual(keyInList);
            }) == null)
            {
                list.Add(key);
            }
        }
        else
            list.Remove(list.Find(delegate(ApprovalKey keyInList)
            {
                return key.IsEqual(keyInList);
            }));
    }

    private ApprovalKeyWithTag GetRowKey(DataKey dataKey)
    {
        ApprovalKeyWithTag key = new ApprovalKeyWithTag();
        key.Employee = new Employee(dataKey.Values[0].ToString());
        key.Approval = new Approval();
        key.Approval.Code = (int)dataKey.Values[1];
        key.Approval.Level = int.Parse(dataKey.Values[7].ToString());
        key.WorkCard = new WorkCard();
        key.WorkCard.WorkDate = (DateTime)dataKey.Values[2];
        key.WorkCard.SidurNumber = (int)dataKey.Values[3];
        key.WorkCard.SidurStart = (DateTime)dataKey.Values[4];
        key.WorkCard.ActivityStart = (DateTime)dataKey.Values[5];
        key.WorkCard.ActivityNumber = int.Parse(dataKey.Values[6].ToString());
        key.RequestValues.SetValue(RequestValues.FIRST_REQUEST_VALUE_KEY, dataKey.Values[8]);
        key.RequestValues.SetValue(RequestValues.SECOND_REQUEST_VALUE_KEY, dataKey.Values[9]);
        return key;
    }

    private void RefreshData()
    {
        RefreshData(false);
    }
    private void RefreshData(bool savePage)
    {
        ApprovalRequestState? status = null;
        int? month = null, year = null;
        if (ddStatuses.SelectedIndex > 0)
            status = (ApprovalRequestState)int.Parse(ddStatuses.SelectedValue);
        if (ddMonths.Enabled && ddMonths.SelectedIndex >= 0)
        {
            month = int.Parse(ddMonths.SelectedValue.Substring(0, 2));
            year = int.Parse(ddMonths.SelectedValue.Substring(3));
        }
        DataTable dtSource = _approvManage.GetApprovalRequests(status, month, year);
        ViewState["GridSource"] = dtSource;
        if (!savePage) ChangeGridPage(0);
        grdRequests.DataSource = dtSource;
        grdRequests.DataBind();
        if(!savePage) 
            if (grdRequests.Rows.Count > 0) grdRequests.SelectedIndex = 0;
    }

    private void CollectApprovalRequestsForProcess(
        List<ApprovalKeyWithTag> accceptRequests, 
        List<ApprovalKeyWithTag> declineRequests,
        List<ApprovalKeyWithTag> forwardRequests)
    {
        for (int i = 0; i < grdRequests.PageCount; ++i)
        {
            ChangeGridPage(i);
            for (int j = 0; j < grdRequests.Rows.Count; ++j)
            {
                if (!IsStatusInRowChanged(grdRequests.Rows[j])) continue;

                ApprovalKeyWithTag key = GetRowKey(grdRequests.DataKeys[j]);
                FillTagForApprovalKey(key, grdRequests.Rows[j]);
                //key.Tag = 
                //    ((HiddenField)grdRequests.Rows[j].FindControl("hdRemarks")).Value;
                bool isAccept =
                    ((CheckBox)grdRequests.Rows[j].FindControl("chkAccept")).Checked;
                bool isDecline =
                    ((CheckBox)grdRequests.Rows[j].FindControl("chkDecline")).Checked;
                //Forward Factor
                bool isForward =
                    ((DropDownList)
                    grdRequests.Rows[j].FindControl("ddFactor")).SelectedIndex > 0;
                if (isAccept) accceptRequests.Add(key);
                else if (isDecline) declineRequests.Add(key);
                else if (isForward)
                {
                    //object[] keyTag = new object[2];
                    //keyTag[0] = key.Tag.ToString();
                    //keyTag[1] = ((DropDownList)
                    //    grdRequests.Rows[j].FindControl("ddFactor")).SelectedValue;
                    //key.Tag = keyTag;
                    forwardRequests.Add(key);
                }
            }
        }
        //if (grdRequests.PageCount > 1) ChangeGridPage(0);
    }

    private void ProcessAprovalRequests(List<ApprovalKeyWithTag> accceptRequests, 
        List<ApprovalKeyWithTag> declineRequests,
        List<ApprovalKeyWithTag> forwardRequests,
        out List<ApprovalKeyWithTag> errorRequests)
    {
        var errList = new List<ApprovalKeyWithTag>();
        //approve requests
        accceptRequests.ForEach(delegate(ApprovalKeyWithTag key)
        {
            ApprovalRequest outRequest = null, nextRequest = null;
            if (!_approvManage.AcceptApprovalRequest(key,
                out outRequest, out nextRequest, "heara", key.AppTag.Remark))
            {
                errList.Add(key);
            }
            else RemoveFromChangedList(key);
        });

        //decline requests
        declineRequests.ForEach(delegate(ApprovalKeyWithTag key)
        {
            ApprovalRequest outRequest = null;
            if (!_approvManage.DeclineApprovalRequest(key,
                out outRequest, "heara", key.AppTag.Remark))
            {
                errList.Add(key);
            }
            else RemoveFromChangedList(key);
        });

        //forward request
        forwardRequests.ForEach(delegate(ApprovalKeyWithTag key)
        {
            ApprovalRequest outRequest = null;
            //string remarks = key.App
            //int jobCode = int.Parse(((object[])key.Tag)[1].ToString());
            if (!_approvManage.ForwardApprovalRequestToAnotherFactor(key,
               key.AppTag.Remark, key.AppTag.Factor, out outRequest))
            {
                errList.Add(key);
            }
            else RemoveFromChangedList(key);
        });

        errorRequests = errList;
    }

    private void RemoveFromChangedList(ApprovalKeyWithTag key)
    {
        List<ApprovalKeyWithTag> saved = null;
        GetListFromSession(ref saved, SAVED_TAG);
        var foundKey =
                    saved.Find(delegate(ApprovalKeyWithTag keyInList)
                    {
                        return key.IsEqual(keyInList);
                    });
        if (foundKey != null && foundKey.AppTag.Changed) foundKey.AppTag.Changed = false;
            
    }

    private bool IsStatusInRowChanged(GridViewRow row)
    {
        return !String.IsNullOrEmpty(((Label)row.FindControl("hdChanged")).Text);
    }

    private void MarkRowStatusChanged(GridViewRow row)
    {
        ((Label)row.FindControl("hdChanged")).Text = "1";
    }

    private void ClearSessionLists()
    {
        Session[ACCEPT_ITEMS] = null;
        Session[DECLINE_ITEMS] = null;
        Session[SAVED_TAG] = null;
        Session[SAVED_ERRORS] = null;
    }

    private void ShowErrors()
    {
        peErrors.Enabled = Session[SAVED_ERRORS] != null &&
            ((List<ApprovalKeyWithTag>)Session[SAVED_ERRORS]).Count > 0;
        ErrorTitlePanel.Visible = peErrors.Enabled;
        if (ErrorTitlePanel.Visible)
        {
            var errors = (List<ApprovalKeyWithTag>)Session[SAVED_ERRORS];
            lstErrors.Items.Clear();
            errors.ForEach(delegate(ApprovalKeyWithTag key)
            {
                lstErrors.Items.Add(String.Format("{0} {1} {2} {3}",
                    key.Employee.EmployeeNumber,key.AppTag.EmployeeName,
                    key.AppTag.ApprovalCodeDescriptiom,
                    key.WorkCard.WorkDate.ToString("dd/MM/yyyy")));
            });
        }
    }
    #endregion
}
