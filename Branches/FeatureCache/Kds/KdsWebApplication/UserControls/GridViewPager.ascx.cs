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


public partial class UserControls_GridViewPager : System.Web.UI.UserControl, IGridViewPager
{
    public event GridViewPageEventHandler PageIndexChanged;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        btnFirst.Click += new EventHandler(btnFirst_Click);
        btnPrevious.Click += new EventHandler(btnPrevious_Click);
        btnNext.Click += new EventHandler(btnNext_Click);
        btnLast.Click += new EventHandler(btnLast_Click);
        ddlPageSelector.SelectedIndexChanged += new EventHandler(ddlPageSelector_SelectedIndexChanged);
        this.DataBinding += new EventHandler(UserControls_GridViewPager_DataBinding);
    }

    void UserControls_GridViewPager_DataBinding(object sender, EventArgs e)
    {
        SetPagerButtons(this.NamingContainer.Parent.Parent as GridView);
    }

    void ddlPageSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewPageEventArgs evArgs = 
            new GridViewPageEventArgs(ddlPageSelector.SelectedIndex);
        PageIndexChanged(this, evArgs);
    }

    void btnLast_Click(object sender, EventArgs e)
    {
        GridViewPageEventArgs evArgs =
            new GridViewPageEventArgs(ddlPageSelector.Items.Count);
        PageIndexChanged(this, evArgs);
    }

    void btnNext_Click(object sender, EventArgs e)
    {
        GridViewPageEventArgs evArgs =
            new GridViewPageEventArgs(ddlPageSelector.SelectedIndex + 1);
        PageIndexChanged(this, evArgs);
    }

    void btnPrevious_Click(object sender, EventArgs e)
    {
        GridViewPageEventArgs evArgs =
            new GridViewPageEventArgs(ddlPageSelector.SelectedIndex - 1);
        PageIndexChanged(this, evArgs);
    }

    void btnFirst_Click(object sender, EventArgs e)
    {
        GridViewPageEventArgs evArgs =
            new GridViewPageEventArgs(0);
        PageIndexChanged(this, evArgs);
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    private void SetPagerButtons(GridView gridView)
    {
        int pageIndex = gridView.PageIndex;
        int pageCount = gridView.PageCount;

        btnFirst.Enabled = btnPrevious.Enabled = (pageIndex != 0);
        btnNext.Enabled = btnLast.Enabled = (pageIndex < (pageCount - 1));

        ddlPageSelector.Items.Clear();

        for (int i = 1; i <= gridView.PageCount; i++)
        {
            ddlPageSelector.Items.Add(i.ToString());
        }

        ddlPageSelector.SelectedIndex = pageIndex;
    }

    public int SelectedPageIndex
    {
        get { return ddlPageSelector.SelectedIndex; }
    }
}

