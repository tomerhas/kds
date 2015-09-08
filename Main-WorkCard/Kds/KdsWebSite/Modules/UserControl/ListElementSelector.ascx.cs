using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data; 
using System.Web.UI.WebControls;
using KdsLibrary.Utils.Reports;
using KdsLibrary;



public partial class Modules_UserControl_ListElementSelector : System.Web.UI.UserControl
{
  
    public enum DisplayType
    {
        Unvisible,
        Visible
    }
    /// <summary>
    /// Initialize the "panel" with a title and fill the list of elements to choise according to the "TableOfElement",
    /// when the first column is the DataValueField amd the 2nd is the DataTextField of the list . 
    /// </summary>
    /// <param name="title"></param>
    /// <param name="TableOfElement"></param>
    public void Initialize(string title)
    {
        Title.InnerHtml = title;
    }
    public void Initialize(string title, DisplayType DisplayList)
    {
        Initialize(title);
        ListVisibled = DisplayList;
    }
    public string ServiceMethod
    {
        set { AutoCompleteExtenderID.ServiceMethod = value; }
    }
    public DisplayType ListVisibled
    {
        get
        {
            return (DisplayType)clGeneral.GetIntegerValue(HidListVisible.Value);
        }
        set
        {
            HidListVisible.Value = ((int)((DisplayType)value)).ToString();
            if (value == DisplayType.Unvisible)
                DivListElement.Style.Add("Display", "none");
            else
                DivListElement.Style.Add("Display", "block");
        }
    }

    public AjaxControlToolkit.AutoCompleteExtender AutoCompleteById
    {
        get { return AutoCompleteExtenderID; }
    }

    public string ItemSelected
    {
        get {
            return HiddenElementSelected.Value;
        }
    }

    public DataTable Data
    {
        set
        {
            try
            {
                LstBxOfElement.Items.Clear();
                LstBxOfElement.DataSource = value;
                LstBxOfElement.SelectionMode = ListSelectionMode.Multiple;
                LstBxOfElement.DataValueField = value.Columns[0].Caption;
                LstBxOfElement.DataTextField = value.Columns[1].Caption;

                LstBxOfElementSelected.SelectionMode = ListSelectionMode.Multiple;
                LstBxOfElementSelected.DataValueField = value.Columns[0].Caption;
                LstBxOfElementSelected.DataTextField = value.Columns[1].Caption;

                LstBxOfElement.DataBind();
                HiddenElementSelected.Value = "";
            }
            catch
            {
            }
        }
    }

    protected void BtAddAll_Click(object sender, EventArgs e)
    {
        foreach (ListItem Item in LstBxOfElement.Items)
            LstBxOfElementSelected.Items.Add(Item);
        LstBxOfElement.Items.Clear();
    }
    protected void BtDelAll_Click(object sender, EventArgs e)
    {
        foreach (ListItem Item in LstBxOfElementSelected.Items)
            LstBxOfElement.Items.Add(Item);
        LstBxOfElementSelected.Items.Clear();
    }
    //protected void BtAddSelected_Click(object sender, EventArgs e)
    //{
    //    FillListBox(GetSelectedItems(LstBxOfElement));
    //}

    private List<string> GetSelectedItems(ListBox List)
    {
        List<string> ItemSelected = new List<string>(); 
        foreach (ListItem Item in List.Items)
        {
            if (Item.Selected)
                ItemSelected.Add(Item.Value);
        }
        return ItemSelected;
    }


    protected void BtAddSingle_Click(object sender, EventArgs e)
    {
        List<string> ItemSelected = new List<string>(); 
        ItemSelected.Add(txtIdDetail.Text);
        FillListBox(ItemSelected);
    }

    private void FillListBox(List<string> ItemsSelected)
    {
        if (ItemsSelected.Count > 0)
        {
            ItemsSelected.ForEach(delegate(string ItemValue)
            {
                LstBxOfElementSelected.Items.Add(LstBxOfElement.Items.FindByValue(ItemValue));
                LstBxOfElement.Items.Remove(LstBxOfElement.Items.FindByValue(ItemValue));
            });
        }
    }
}
