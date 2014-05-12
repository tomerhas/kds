using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_Test4 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    

        //string callBackStr =
        //ClientScript.GetCallbackEventReference(this,
        //                                        "GetArgs()",
        //                                        "onSuccess",
        //                                        null,
        //                                        "onFailed",
        //                                        true);


        //btnAdd.Attributes["onclick"] = callBackStr;

        //btnSub.Attributes["onclick"] = callBackStr;

        //btnMulti.Attributes["onclick"] = callBackStr;

        //btnDiv.Attributes["onclick"] = callBackStr;
    }
    int num1, num2;

    string method;



    #region ICallbackEventHandler Members



    public void RaiseCallbackEvent(string eventArgument)
    {

        // 2:4:Add

        string[] args = eventArgument.Split(':');



        num1 = int.Parse(args[0]);

        num2 = int.Parse(args[1]);

        method = args[2];

    }



    public string GetCallbackResult()
    {

        if (method == "Add")

            return (num1 + num2).ToString();



        if (method == "Sub")

            return (num1 - num2).ToString();



        if (method == "Multi")

            return (num1 * num2).ToString();



        if (method == "Div")

            return (num1 / num2).ToString();



        return string.Empty;

    }



    #endregion


}