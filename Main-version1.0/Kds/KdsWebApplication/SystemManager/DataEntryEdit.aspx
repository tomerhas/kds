<%@ Page Language="C#" AutoEventWireup="true" Inherits="SystemManager_DataEntryEdit" Codebehind="DataEntryEdit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../StyleSheet.css" type="text/css" rel="stylesheet" />
</head>
<body dir="rtl" onload="load()">
    <form id="form1" runat="server">
     <asp:ScriptManager  runat="server"  id="ScriptManagerKds" EnablePartialRendering="true" >        
     </asp:ScriptManager>
      <asp:UpdatePanel ID="upMessage" runat="server">
     <ContentTemplate>
     <div id="divContent" class="content">
        <asp:HiddenField ID="hdState" runat="server" />
        <asp:FormView runat="server" ID="FormView1" CssClass="title" DefaultMode="Edit"></asp:FormView>
        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label> 
     </div>
     </ContentTemplate></asp:UpdatePanel>
    </form>
</body>
<script type="text/javascript">
    
    
    function EndRequestHandler(sender, args) {
       if (args.get_error() == undefined){
           if($get('<%=hdState.ClientID %>').value=='OK'){
            window.returnValue="OK";
            window.close();
           }
       }
       else
           alert("There was an error" + args.get_error().message);
    }
    
    function load() {
       Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
       resizeWin();
    }
    function resizeWin()
    {
        if(document.all)
        {
            winHeight=document.all['divContent'].offsetHeight;
            winWidth=document.all['divContent'].offsetWidth;
        }
        window.dialogHeight=(winHeight+offset)+'px';
        window.dialogWidth=(winWidth+offset)+'px';
        
}
</script>
</html>
