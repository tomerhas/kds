<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test4.aspx.cs" Inherits="Modules_Test4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src='../../js/jquery.js' type='text/javascript'></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input type="text" id="txt1" />
        <input type="text" id="txt2" />

 

        <input type="button" value="Add" id="btnAdd" runat="server" />

        <input type="button" value="Sub" id="btnSub" runat="server" />

        <input type="button" value="Multi" id="btnMulti" runat="server" />

        <input type="button" value="Div" id="btnDiv" runat="server" />
 
        <span id="sp"></span>


    </div>
    </form>
    <script language="javascript" type="text/javascript">
        function GetArgs() {
            var num1 = document.getElementById('txt1').value;
            var num2 = document.getElementById('txt2').value;
            var method = event.srcElement.value;

            // 2:4:Add
            return num1 + ':' + num2 + ':' + method;
        }
        
        function onSuccess(res) {
            document.getElementById('sp').innerHTML = res;
        }

        function onFailed(res) {
            alert(res);
        }
       

    </script>
</body>
</html>
