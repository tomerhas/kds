<%@ Page Language="C#" AutoEventWireup="true" Inherits="ApprovalMonthlyQuota" Title="בקשה לתשלום שעות נוספות מעל המכסה" Codebehind="ApprovalMonthlyQuota.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title runat="server" id="TitleWindow">בקשה לתשלום שעות נוספות מעל המכסה</title>
    <link href="../../StyleSheet.css"type="text/css" rel="stylesheet" />
    <script src="../../Js/GeneralFunction.js"></script>
    </head>
<body dir="rtl" onunload="return window_onunload()" onload="return window_onload()" >
    <form id="form1" runat="server">
    
    <asp:Label ID="lblHeaderMessage" runat ="server" CssClass="GridHeader" Width="100%" Height="20px" >בקשה לתשלום שעות נוספות מעל המכסה</asp:Label>
        <br />
             <asp:ScriptManager  runat="server"  id="ScriptManagerKds" EnablePartialRendering="true" >        
     </asp:ScriptManager>
      <asp:UpdatePanel ID="upBody" runat="server">
         <ContentTemplate>
            <table  width="100%" style="margin-left: 0px ">
                <tr>
                    <td width="20px" rowspan="9"></td>
                    <td width="50px" align="right">מ.א.:</td>
                    <td width= "60px" align="right" dir="ltr"><asp:Label ID="TxtMisparIshi"  runat="server" Width="70px"/></td>
                    <td width="40px" align="right" >שם:</td>
                    <td align="right" width="130px"><asp:Label ID="TxtFullName"   runat="server" Width="120px"/></td>
                    <td width="20px" rowspan="5" ></td>
                </tr>
                <tr><td colspan="5" >
                    <div id="DivDetailsForVaadatPnim" runat="server" >
                        <table  width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="50px">חודש:</td>
                                <td width="50px" align="right"><asp:Label ID="TxtMonth" runat="server" width="50px"></asp:Label></td>
                            </tr>
                            <tr>
                                <td width="120px">אגף:</td>
                                <td  width="120px"  align="right">
                                    <asp:Label ID="TxtUnit" runat="server" Width="100px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="180px">הועבר לאישור ו. פנים:</td>
                                <td>
                                <asp:Label ID="TxtUavarLevaadatPnim" runat="server" Width="30px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td >מחוץ למכסה מאושר ו. פנים:</td>
                                <td >
                                <asp:TextBox ID="TxtApprovedInVaadatP" onchange="Page_ClientValidate();EnableBtnSend();" runat="server" Width="30px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div> 
                </td></tr>
                <tr><td colspan="5">
                    <div id="DivDetailsForAgaf" runat="server">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="260px" >מחוץ למכסה מבוקש:</td>
                                <td >
                                    <asp:Label ID="TxtMevukach" runat="server" Width="30px"></asp:Label>
                                    <asp:Label ID="TxtBalance" style="display:none" runat="server" Width="30px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td >מחוץ למכסה מאושר אגפית: </td>
                                <td >
                                    <asp:TextBox ID="TxtApprovedInUnit" onchange="CheckApprovedInUnit();"  runat="server" Width="30px"></asp:TextBox>
                             </td>
                            </tr>
                            <tr>
                                 <td >העברה לאישור ו.פנים :</td>
                                <td ><asp:TextBox ID="TxtAavaratToVaadP" onchange="Page_ClientValidate();EnableBtnSend();" runat="server" Width="30px">0</asp:TextBox></td>
                            </tr>
                        </table>
                    </div>
                </td></tr>
                <tr><td colspan="5" height="15px">
                        <asp:RequiredFieldValidator ID="RequiredValidApprovedInUnit" runat="server" Display="Dynamic" ControlToValidate="TxtApprovedInUnit"/>
                        <asp:RequiredFieldValidator ID="RequiredApprovedInVaadatP" runat="server" Display="Dynamic" ControlToValidate="TxtApprovedInVaadatP"/>
                        
                        <asp:CustomValidator ID="CmpValidApprovedInUnit" runat="server" Display="Dynamic" CssClass="ErrorMessage" ErrorMessage="coucou"  ControlToValidate="TxtApprovedInUnit" EnableViewState="true" ClientValidationFunction="CompareAllValid" ></asp:CustomValidator>
                        <asp:CustomValidator ID="CmpValidApprovedInVaadatP" runat="server" Display="Dynamic" CssClass="ErrorMessage" ErrorMessage="coucou"  ControlToValidate="TxtApprovedInVaadatP" EnableViewState="true" ClientValidationFunction="CompareAllValid" ></asp:CustomValidator>
                </td></tr>
                <tr>
                    <td align="center" colspan="2" height="30px">
                         <asp:Button Text="אשר" ID="btnSend" runat="server" CssClass="ImgButtonSearch" onclick="btnSend_Click" /> 
                    </td>
                    <td><asp:TextBox ID="TxtFormType" style="display:none" runat="server"></asp:TextBox></td>
                    <td align="center"  colspan="2" height="30px">
                         <asp:Button Text="בטל"  ID="btnCancel" OnClientClick="window_onunload();" runat="server" CssClass="ImgButtonSearch" /> 
                    </td>
             </tr>
            </table>
         </ContentTemplate>
     </asp:UpdatePanel>
    </form>
</body>
<script type="text/javascript">

    var ReturnValue;
    function CompareAllValid(oSrc, args) {
        var CurrentObj = document.getElementById(oSrc.controltovalidate);
        if (!IsNumber(CurrentObj.value)) {
            oSrc.innerText = "יש להקליד ערך מספרי בלבד";
            args.IsValid = false;
        }
        else {
            if (parseInt(CurrentObj.value) < 0) {
                oSrc.innerText = "לא ניתן להגדיר ערך שלילי";
                args.IsValid = false;
            }
            else {
                var OriginValue = CurrentObj.OriginalValue;
                if (parseInt(CurrentObj.value) > OriginValue) {
                    oSrc.innerText = "לא ניתן לאשר ערך הגדול מהערך המבוקש";
                    args.IsValid = false;
                }
                else if (parseInt(CurrentObj.value) == OriginValue) {
                    oSrc.innerText = "אין לאשר את הבקשה, סגור חלון זה וציין " + "\"\"לא מאשר";
                    args.IsValid = false;
                }
            }
        }
    }
    function IsOriginValueChanged(oSrc, args) {
        var CurrentObj = document.getElementById(oSrc.controltovalidate);
        var OriginValue = CurrentObj.OriginalValue;
        args.IsValid = (OriginValue != CurrentObj.value);
        EnableBtnSend();        
    }



    function CmpValidApprovedInUnit(oSrc, args) {
        var Mevukach = parseInt(document.getElementById("TxtMevukach").childNodes[0].nodeValue);
        var UavarLevaadatPnim = parseInt(document.getElementById("TxtUavarLevaadatPnim").childNodes[0].nodeValue);
        var FormType = parseInt(document.getElementById("TxtFormType").value);
        var ApprovedValue;
        if (FormType == 1) {
            ApprovedValue = document.getElementById("TxtApprovedInUnit");
            args.IsValid = (parseInt(ApprovedValue.value) < Mevukach);
        }
        else {
            ApprovedValue = document.getElementById("TxtApprovedInVaadatP");
            args.IsValid = (parseInt(ApprovedValue.value) < UavarLevaadatPnim);
        }
        EnableBtnSend();
    }
    function CmpValidTxtAavaratToVaadP(oSrc, args) {
        var Mevukach = parseInt(document.getElementById("TxtMevukach").childNodes[0].nodeValue);
        var ApprovedInUnit = parseInt(document.getElementById("TxtApprovedInUnit").value);
        var AavaratToVaadP = parseInt(document.getElementById("TxtAavaratToVaadP").value);
        args.IsValid = (AavaratToVaadP + ApprovedInUnit < Mevukach)
        EnableBtnSend();
    }
function CheckApprovedInUnit() {
    var Mevukach = parseInt(document.getElementById("TxtMevukach").childNodes[0].nodeValue);
    var Balance = parseInt(document.getElementById("TxtBalance").childNodes[0].nodeValue);
    var ApprovedInUnit = document.getElementById("TxtApprovedInUnit");
    var AavaratToVaadP = document.getElementById("TxtAavaratToVaadP");
    if ((Mevukach > Balance) && (parseInt(ApprovedInUnit.value) > Balance)) {
        var answer = confirm("?הנך חורג מהמכסה המבוקשת, האם תרצה להעביר את הסכום מעבר למכסה לאישור ועדת פנים");
        if (answer) {
            AavaratToVaadP.value = parseInt(Mevukach) - Balance;
            ApprovedInUnit.value = Balance;
        }
    }
    EnableBtnSend();
}

function EnableBtnSend() {
    var btnSend = document.getElementById("btnSend");
    btnSend.disabled =!Page_IsValid;
    btnSend.className = Page_IsValid? "ImgButtonSearch" : "ImgButtonSearchDisable" ; 

}
  
function window_onunload() {
    window.returnValue= ReturnValue ;
    window.close();
}

function ConfirmAndClose()
{
    var  ApprovedInUnit = document.getElementById("TxtApprovedInUnit").value ;
    var  AavaratToVaadP = document.getElementById("TxtAavaratToVaadP").value ;
    var  ApprovedInVaadatP = document.getElementById("TxtApprovedInVaadatP").value ;
    ReturnValue = ApprovedInUnit  + ',' + AavaratToVaadP + "," + ApprovedInVaadatP;
    window.close();
}


function window_onload() {
    //var btnSend = document.getElementById("btnSend");
    //btnSend.disabled = true; 
    ReturnValue = -1;
}

</script>
</html>
