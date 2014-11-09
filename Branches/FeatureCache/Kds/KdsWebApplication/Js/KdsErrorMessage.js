//In order to use the error dialog need to do the following:
//in the relevant page - add <asp:ScriptReference Path="~/Js/KdsErrorMessage.js" />
//In the aspx page add a div like so: 
//        <div id="dialog" title="שגיאת מערכת">
//            <div >משתמש יקר ארעה שגיאה במערכת. אנא פנה למנהל המערכת.</div>
//            <br />
//            <a href="#" onclick="ToggleDisplay()">פרטי השגיאה:</a>
//            <div id="dialogContent" dir="ltr" style="width: 380px; height: 200px; overflow-y: scroll;overflow-x:scroll;display: none"></div>
//        </div>
// If the page includes a script manager - add attribute: OnAsyncPostBackError="ScriptManager_AsyncPostBackError"
//In the code behind add a handler for the error named: 
// protected void ScriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
//{
//    <script manager name>.AsyncPostBackErrorMessage = e.Exception.ToString();
//}

function pageLoad() {
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(onEndRequest);
}


function onEndRequest(sender, args) {
    var errObj = args.get_error();
    if (errObj) {
       // alert(errObj.message);
        $("#dialog")
            .data("errMsg", errObj.message)
            .dialog("open");
        args.set_errorHandled(true);
    }
}

$(function () {
    $("#dialog").dialog({
        autoOpen: false,
        width:400,
        open: function (event,ui) {
            $(".ui-dialog-titlebar-close").hide();
            var data = $("#dialog").data("errMsg");
            $("#dialogContent").text(data);
        },
        buttons: {
            "סגור": function () {
                $("#dialog").dialog("close");
            }
        }
    });
});

function ToggleDisplay() {
    $("#dialogContent").toggle();;
}
