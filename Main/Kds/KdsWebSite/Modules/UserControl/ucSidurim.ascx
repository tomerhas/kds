<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSidurim.ascx.cs" Inherits="Modules_UserControl_ucSidurim" %>
<%@ Register TagPrefix="uc" Src="~/Modules/UserControl/ucPeiluyot.ascx" TagName="ucPeiluyot" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<link href="../../StyleSheet.css" type="text/css" rel="stylesheet" />
<table runat="server" id="tbSidurimHeader" width="100%" class="Grid" cellpadding="4" cellspacing="1"></table>
<div class="lstSidurDiv" id="dvS">     
    <asp:Table runat="server" id="tbSidurim" Width="100%"  cellpadding="0" cellspacing="0"></asp:Table>        
    <input type="hidden" runat="server" id="hidItmAddKey" />
</div>   
 <input type="button" ID="btnShowMessage" runat="server" style="display: none;"/>
 <ajaxToolkit:ModalPopupExtender ID="ModalPopupEx" dropshadow="false" X="300" Y="280" PopupControlID="paMessage"
    TargetControlID="btnShowMessage"  runat="server" BehaviorID="pBehvDate" BackgroundCssClass="modalBackground">
 </ajaxToolkit:ModalPopupExtender>
 <asp:Panel runat="server" Style="display: none" ID="paMessage" CssClass="modalPopup" Width="350px" >
    <asp:Label ID="lblHeaderMessage" runat="server" Width="97%" BackColor="#696969" ForeColor="White">קביעת תאריך</asp:Label>   
    <br />
    <br />
    יש לקבוע את היום של השעה שהוקלדה:
    <br />
    <br />
    <input type="button" ID="btnCurrentDay" runat="server" value="יום נוכחי" class="ImgButtonEndUpdate"
        style="width:80px" onclick="btnDay_click(0)" CausesValidation="false" />
    <input type="button" ID="btnNextDay"  runat="server" onclick="btnDay_click(1)"  value="יום הבא" class="ImgButtonEndUpdate"
        style="width:80px" CausesValidation="false" />
</asp:Panel>

<input type="hidden" runat="server" id="hidCurrIndx"/>
<input type="hidden" runat="server" id="hidParam29"/>
<input type="hidden" runat="server" id="hidNumOfHashlama"/>
<input type="hidden" runat="server" id="hidParam41"/>
<input type="hidden" runat="server" id="hidParam98"/>
<input type="hidden" runat="server" id="hidParam242"/>

<script type="text/javascript" language="javascript">
var _COL_KISUY_TOR=<%=_COL_KISUY_TOR %>;var _COL_SHAT_YETIZA= <%=_COL_SHAT_YETIZA %>;var _COL_CAR_NUMBER= <%=_COL_CAR_NUMBER %>;var _COL_MAKAT= <%=_COL_MAKAT %>;var _COL_ACTUAL_MINUTES= <%=_COL_ACTUAL_MINUTES %>;var _COL_CANCEL= <%=_COL_CANCEL %>;var _COL_CANCEL_PEILUT = <%=_COL_CANCEL_PEILUT %>;var _COL_DEF_MINUTES= <%=_COL_DEF_MINUTES %>;var _COL_DAY_TO_ADD=<%=_COL_DAY_TO_ADD %>; var _COL_LINE_DESCRIPTION=<%=_COL_LINE_DESCRIPTION %>; var _COL_LINE=<%=_COL_LINE %>; var _COL_LINE_TYPE=<%=_COL_LINE_TYPE %>; var _COL_MAZAN_TASHLUM=<%=_COL_MAZAN_TASHLUM %>; var _COL_NETZER=<%=_COL_NETZER %>; var _COL_PEILUT_STATUS=<%=_COL_PEILUT_STATUS %>; var _COL_KNISA=<%=_COL_KNISA %>; var _COL_ADD_NESIA_REKA=<%=_COL_ADD_NESIA_REKA %>;var _COL_KISUY_TOR_MAP=<%=_COL_KISUY_TOR_MAP%>

function window.onload()
{    
    SetSidurimCollapseImg();
    HasSidurHashlama();  
}
</script>
<script src="../../Js/Sidurim.js" type="text/javascript"></script>


