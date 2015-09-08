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
using KdsLibrary.UI.SystemManager;

public partial class SystemManager_DataEntryNavigation : KdsLibrary.UI.KdsPage
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int? categoryID = GetCategoryID();
            if (categoryID.HasValue)
            {
                BuildNavigation(categoryID.Value);
            }
        }
    }
    protected void grdNavigation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((HyperLink)e.Row.Cells[0].Controls[0]).NavigateUrl =
                String.Format("DataEntryView.aspx?table={0}", 
                ((Label)e.Row.FindControl("lblTableName")).Text);
        }
    }
    private void BuildNavigation(int categoryID)
    {
        KdsTableCategory category = new KdsTableCategory(categoryID);
        grdNavigation.DataSource = category.TableList.Items;
        grdNavigation.DataMember = "TableCategoryItem";
        grdNavigation.DataBind();
        
        PageHeader = category.CategoryName;
        Page.Header.Title = category.CategoryName;
    }

    private int? GetCategoryID()
    {
        int? categoryID = null;
        int parseValue;
        if (int.TryParse(Request.QueryString["category"], out parseValue))
            categoryID = parseValue;
        return categoryID;
    }
}
