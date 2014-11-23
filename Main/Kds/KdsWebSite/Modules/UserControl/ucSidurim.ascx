<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSidurim.ascx.cs" Inherits="Modules_UserControl_ucSidurim" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<link href="../../StyleSheet.css" type="text/css" rel="stylesheet" />
<table runat="server" id="tbSidurimHeader" width="100%" ></table>
<div class="lstSidurDiv" id="dvS" runat="server" onscroll="SaveScrollPosToHidden();">        
    <asp:Table runat="server" id="tbSidurim" Width="100%"  cellpadding="0" cellspacing="0"></asp:Table>        
    <input type="hidden" runat="server" id="hidItmAddKey" />   
</div>   
 <input type="button" ID="btnShowMessage" runat="server" style="display: none;"/>
 <ajaxToolkit:ModalPopupExtender ID="ModalPopupEx" dropshadow="false" X="300" Y="280" PopupControlID="paMessage"
    TargetControlID="btnShowMessage"  runat="server" BehaviorID="pBehvDate" BackgroundCssClass="modalBackground">
 </ajaxToolkit:ModalPopupExtender>
 <asp:Panel runat="server" Style="display: none" ID="paMessage" CssClass="WorkCardPanelMessage" Width="455px" Height="180px" >
    <table width="450px">
        <tr class="WorkCardPanelMessageBorder">
            <td colspan="2" width="380px" height="33px"  class="WorkCardPanelMessageHeader">
                <asp:Label ID="lblHeaderMessage" runat="server"  width="450px" >קביעת תאריך</asp:Label>
            </td>
        </tr>
        <tr class="WorkCardPanelMessageBorder">
            <td width="450px" height="100px" colspan="2">                                
                <asp:Label ID="lblMessage" runat="server" Width="90%"></asp:Label>
                <br/>
                יש לקבוע את היום של השעה שהוקלדה:
                <br/>                        
            </td>
        </tr>
        <tr class="WorkCardPanelMessageHeader">
            <td width="380px" align="left">
                 <input type="button" ID="btnCurrentDay" runat="server" value="יום נוכחי" class="btnWorkCardCloseWin"
                     style="width:74px; height:30px"  onclick="btnDay_click(0)" CausesValidation="false" />                     
            </td>
            <td align="left">
                   <input type="button" ID="btnNextDay"  runat="server" onclick="btnDay_click(1)"  value="יום הבא" class="btnWorkCardCloseWin"
                    style="width:74px; height:30px"  CausesValidation="false" />
            </td>
        </tr>
    </table>          
</asp:Panel>

<input type="hidden" runat="server" id="hidCurrIndx"/>
<input type="hidden" runat="server" id="hidParam29"/>
<input type="hidden" runat="server" id="hidNumOfHashlama"/>
<input type="hidden" runat="server" id="hidParam41"/>
<input type="hidden" runat="server" id="hidParam98"/>
<input type="hidden" runat="server" id="hidParam242"/>
<input type="hidden" runat="server" id="hidParam244"/>
<input type="hidden" runat="server" id="hidParam276"/>
<input type="hidden" runat="server" id="hidScrollPos"/>
<input type="hidden" runat="server" id="hidGeneralParam"/>

<script type="text/javascript" language="javascript">
var _COL_KISUY_TOR=<%=_COL_KISUY_TOR %>;var _COL_SHAT_YETIZA= <%=_COL_SHAT_YETIZA %>;var _COL_CAR_NUMBER= <%=_COL_CAR_NUMBER %>;var _COL_MAKAT= <%=_COL_MAKAT %>;var _COL_ACTUAL_MINUTES= <%=_COL_ACTUAL_MINUTES %>;var _COL_CANCEL= <%=_COL_CANCEL %>;var _COL_CANCEL_PEILUT = <%=_COL_CANCEL_PEILUT %>;var _COL_DEF_MINUTES= <%=_COL_DEF_MINUTES %>;var _COL_DAY_TO_ADD=<%=_COL_DAY_TO_ADD %>; var _COL_LINE_DESCRIPTION=<%=_COL_LINE_DESCRIPTION %>; var _COL_LINE=<%=_COL_LINE %>; var _COL_LINE_TYPE=<%=_COL_LINE_TYPE %>; var _COL_MAZAN_TASHLUM=<%=_COL_MAZAN_TASHLUM %>; var _COL_NETZER=<%=_COL_NETZER %>; var _COL_PEILUT_STATUS=<%=_COL_PEILUT_STATUS %>; var _COL_KNISA=<%=_COL_KNISA %>; var _COL_ADD_NESIA_REKA=<%=_COL_ADD_NESIA_REKA %>;var _COL_KISUY_TOR_MAP=<%=_COL_KISUY_TOR_MAP%>; var _COL_ADD_NESIA_REKA_UP=<%=_COL_ADD_NESIA_REKA_UP %>

function SaveScrollPosToHidden()
{ 
   $get('SD_hidScrollPos').value=$get("SD_dvS").scrollTop;
}
window.onload = function () {
    SetSidurimCollapseImg();
    HasSidurHashlama();     
};
</script>
<script src="../../Js/Sidurim.js" type="text/javascript"></script>


