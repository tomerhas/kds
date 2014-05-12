<%@ Page Language="C#" AutoEventWireup="true" Inherits="Modules_ScrollTest" Codebehind="ScrollTest.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptManager" runat="server" EnablePartialRendering="True" />
<script type="text/javascript" language="javascript">
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_pageLoaded(pageLoaded);



    function pageLoaded(sender, args) {

        Func()

    }
    function Func() {//document.getElementById("hdnScrollTop").value
        document.getElementById("divScroll").scrollTop = document.getElementById("hdnScrollTop").value;
    }
    function Func2() {
        var s = document.getElementById("divScroll").scrollTop;

        document.getElementById('hdnScrollTop').value = s;

    }
</script> 


<input type="hidden" id="hdnScrollTop" runat="server" value="0"/>
<div>
    <asp:UpdatePanel UpdateMode="Conditional" ID="UpdatePanel1" runat="server" >
        <ContentTemplate>                
         <div runat="server" id="divScroll" style="width:350px;height:200px; overflow-x:hidden; overflow-y:scroll;" onscroll="Func2()">
            <asp:gridview id="grdOrders" runat="server" width="95%"  cellpadding="3" GridLines="Horizontal">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
            </asp:gridview>  
        </div>
        <asp:Button ID="Button1" runat="server" Text="Button" />           
       </ContentTemplate>
    </asp:UpdatePanel>
</div>
<asp:Button ID="Button2" runat="server" Text="Button" />       
       <script type="text/jscript" >
           Func()
       </script>
    </form>
</body>


</html>
