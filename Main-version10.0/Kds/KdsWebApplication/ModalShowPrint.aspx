﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="ModalShowPrint" Codebehind="ModalShowPrint.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <meta http-equiv="X-UA-Compatible" content="IE=8" />
     <script src="Js/GeneralFunction.js" type="text/javascript"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table >
            <tr>
               <td>   <input type="button" onclick="PrintReport()" value="הדפסה" /></td>
               <td dir="rtl">  <label id="lblPrint" style="background-color:lightgray;display:none;font-family:Arial;font-size:medium"> הדף נשלח להדפסה אנא המתן... &nbsp;&nbsp;</label></td>
            </tr>
        </table>

        <input type="hidden" runat="server" id="idpath" />
       <%--  <input type="hidden" runat="server" onclick="" id="btnDelete" />--%>
        <iframe src="ShowPrint.aspx" id="printf" name="printf" height="800px" width="100%">
    
    </iframe>
    </form>
    <script type="text/javascript">
        function PrintReport() {
          

            var path = document.getElementById("idpath").value;
            PrintDoc('', path);
            document.all('lblPrint').style.display = 'block';
            setTimeout(function () {
                document.all('lblPrint').style.display = 'none';
              //  document.all('btnCloseCard').click();
            }, 5000);


        }
        

    </script>
</body>
</html>
