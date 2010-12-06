using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_UserControl_FilterOvedName : System.Web.UI.UserControl
{
    public string IdNumber
    {
        get { return txtId.Text; }
    }
    public string WorkerName
    {
        get { return txtName.Text; }
    }
    public AjaxControlToolkit.AutoCompleteExtender AutoCompleteByName
    {
        get { return AutoCompleteExtenderByName; }
    }

    public AjaxControlToolkit.AutoCompleteExtender AutoCompleteById
    {
        get { return AutoCompleteExtenderID; }
    }

}
