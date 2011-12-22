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
using KdsLibrary.DAL;
using KdsLibrary.UI;
using KdsLibrary.Security;
public partial class Modules_Ovdim_WorkCardErrors : KdsPage
{
    private DataTable _dtErrorCodes;
    //private DataTable _dtErrorDictionary;
    private const string ERROR_CODE_COLUMN = "Kod_Shgia";
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
        grdWorkDayErrors.PageIndexChanging += new GridViewPageEventHandler(grdWorkDayErrors_PageIndexChanging);
        grdSidurErrors.PageIndexChanging += new GridViewPageEventHandler(grdSidurErrors_PageIndexChanging);
    }

    void grdSidurErrors_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSidurErrors.PageIndex = e.NewPageIndex;
        grdSidurErrors.DataSource = ViewState["SidurErrors"];
        grdSidurErrors.DataBind();
    }

    void grdWorkDayErrors_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdWorkDayErrors.PageIndex = e.NewPageIndex;
        grdWorkDayErrors.DataSource = ViewState["WorkDayErrors"];
        grdWorkDayErrors.DataBind();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtAllErrors = Session["Errors"] as DataTable;
            if (dtAllErrors == null)
                lblError.Visible = true;
            else BindData(dtAllErrors);
        }
    }

    private void BindData(DataTable dtAllErrors)
    {
        LoadDictionaryTables();
        DataTable dtWorkDayErrors = dtAllErrors.Clone();
        DataTable dtSidurErrors = dtAllErrors.Clone();
        AddAdditionalColumns(dtWorkDayErrors);
        AddAdditionalColumns(dtSidurErrors);
        SplitData(dtAllErrors, dtWorkDayErrors, dtSidurErrors);
        ViewState["WorkDayErrors"] = dtWorkDayErrors;
        ViewState["SidurErrors"] = dtSidurErrors;
        grdWorkDayErrors.DataSource = dtWorkDayErrors;
        grdSidurErrors.DataSource = dtSidurErrors;
        grdWorkDayErrors.DataBind();
        grdSidurErrors.DataBind();
    }

    private void LoadDictionaryTables()
    {
        _dtErrorCodes = GetTableFromCache("ErrorCodes", clGeneral.cProGetAllErrorsAndFields);
        //_dtErrorDictionary = GetTableFromCache("ErrorDictionary", clGeneral.cProGetCtbShgiot);
    }

    private DataTable GetTableFromCache(string key, string proc)
    {
        DataTable dt = Cache[key] as DataTable;
        if (dt == null)
        {
            dt = new DataTable();
            clDal dal = new clDal();
            dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
            dal.ExecuteSP(proc, ref dt);
        }
        return dt;
    }

    private void AddAdditionalColumns(DataTable dt)
    {
        dt.Columns.Add(ERROR_FIELD_COLUMN, typeof(String));
        dt.Columns.Add(ERROR_DESC_COLUMN, typeof(String));
    }

    private void SplitData(DataTable dtAllErrors, DataTable dtWorkDayErrors, 
        DataTable dtSidurErrors)
    {
        bool checkEmployeeError = !String.IsNullOrEmpty(Request.QueryString["laoved"]) &&
            Request.QueryString["laoved"] == "1";
        
        foreach (DataRow row in dtAllErrors.Rows)
        {
            if (checkEmployeeError)
            {
                var forEmployeeRows = _dtErrorCodes.Select(String.Format("{0}={1} and Letezuga_LaOved=1",
                    ERROR_CODE_COLUMN, row["check_num"]));
                if (forEmployeeRows.Length == 0) continue;
            }
            DataTable dtTarget;
            if (IsWorkDayError(row))
                dtTarget = dtWorkDayErrors;
            else dtTarget = dtSidurErrors;
            var newRow = dtTarget.NewRow();
            CopyValues(row, newRow);
            dtTarget.Rows.Add(newRow);
        }
    }

    private void CopyValues(DataRow row, DataRow newRow)
    {
        foreach (DataColumn col in row.Table.Columns)
            newRow[col.ColumnName] = row[col.ColumnName];
        var errorDefs = _dtErrorCodes.Select(String.Format("{0}={1}", ERROR_CODE_COLUMN,
            row["check_num"]));
        if (errorDefs.Length > 0)
        {
            newRow[ERROR_FIELD_COLUMN] = errorDefs[0][ERROR_FIELD_COLUMN];
            newRow[ERROR_DESC_COLUMN] = errorDefs[0][ERROR_DESC_COLUMN];
        }
    }

    private bool IsWorkDayError(DataRow row)
    {
        bool forWorkday = true;
        
        foreach (DataColumn col in row.Table.Columns)
        {
            if (!col.ColumnName.ToLower().Equals(ERROR_CODE_COLUMN.ToLower()))
            {
                if (row[col.ColumnName] != DBNull.Value || !row[col.ColumnName].ToString().Equals("0"))
                {
                    forWorkday = false;
                    break;
                }
            }
        }
        return forWorkday;
    }

    protected override bool RequiresAuthorization
    {
        get
        {
            return false;
        }
    }
}
