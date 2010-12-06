<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="HaavaraLesachar.aspx.cs" Inherits="Modules_Batches_HaavaraLesachar" %>
<%--<%@ Register Namespace="Egged.WebCustomControls" Assembly="Egged.WebCustomControls" TagPrefix="wccEgged" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="KdsLibrary" Namespace="KdsLibrary.Controls" TagPrefix="KdsCalendar"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">
    <fieldset class="FilterFieldSet" style="width:930px"> 
       <legend> בחירת נתונים להצגה </legend>      <br />
     <table  border="0">
        <tr valign="top">
            <td class="InternalLabel" style="width:140px">
                   <asp:RadioButton runat="server" Checked="true" ID="rdoRitzotTeremHuavru"  EnableViewState="true" GroupName="grpSearch" Text="ריצות שטרם הועברו"  > </asp:RadioButton>
                </td>
                 <td style="width:10px"></td>
                 <td class="InternalLabel" style="width:90px"> <asp:RadioButton runat="server" ID="rdoRitzotAll"  EnableViewState="true" GroupName="grpSearch" Text="הכל"  > </asp:RadioButton>
                </td>
                 <td style="width:10px"></td>
                 <td class="InternalLabel" style="width:50px"> מתאריך:</td>
                <td align="right" dir="ltr" style="width:160px"> 
                 <KdsCalendar:KdsCalendar runat="server" ID="clnFromDate"  AutoPostBack="false"  dir="rtl" PopupPositionCallOut="Left" ></KdsCalendar:KdsCalendar>                            
                  <%--<wccEgged:wccCalendar runat="server" ID="clnFromDate"   BasePath="../../EggedFramework" AutoPostBack="false" Width="80px" dir="rtl"></wccEgged:wccCalendar>--%>                                                                      
               <asp:RequiredFieldValidator ID="vldReqFromDate" runat="server" Display="Dynamic" CssClass="ErrorMessage"  ErrorMessage="!חובה להזין מתאריך " ControlToValidate="clnFromDate"></asp:RequiredFieldValidator>
          <%--     <asp:CompareValidator ID="vldTypeFromDate" runat="server" Display="Dynamic" ControlToValidate="clnFromDate" ErrorMessage="!מתאריך לא תקין" Type="Date" Operator="DataTypeCheck"></asp:CompareValidator>--%>
                </td>  
                 <td style="width:10px"></td>
                <td class="InternalLabel" style="width:60px">עד תאריך:</td> 
                <td align="right" dir="ltr" style="width:150px">        
                 <KdsCalendar:KdsCalendar runat="server" ID="clnToDate"  AutoPostBack="false"  dir="rtl" PopupPositionCallOut="Left" ></KdsCalendar:KdsCalendar>                       
                    <%--<wccEgged:wccCalendar runat="server" ID="clnToDate"   BasePath="../../EggedFramework" AutoPostBack="false" Width="80px" dir="rtl"></wccEgged:wccCalendar>--%>                                                                                                                      
                <asp:RequiredFieldValidator ID="vldReqToDate" runat="server" Display="Dynamic" CssClass="ErrorMessage"  ErrorMessage="!חובה להזין עד תאריך" ControlToValidate="clnToDate"></asp:RequiredFieldValidator>
                   <asp:CompareValidator ID="vldComToDate" runat="server" Display="Dynamic" CssClass="ErrorMessage"  ErrorMessage="!עד תאריך חייב להיות גדול ממתאריך" ControlToValidate="clnToDate" ControlToCompare="clnFromDate" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                    <%--<asp:CompareValidator ID="vldTypeToDate" runat="server" Display="Dynamic" ControlToValidate="clnToDate" ErrorMessage="!עד תאריך לא תקין" Type="Date" Operator="DataTypeCheck"></asp:CompareValidator>--%>
                 </td> 
                 <td style="width:10px"></td>
                <td> 
                    <asp:UpdatePanel ID="upBtnShow" runat="server" RenderMode="Inline"  >
                      <ContentTemplate> 
                            <asp:button ID="btnShow" runat="server" text="הצג" CssClass ="ImgButtonSearch"  onclick="btnShow_Click"  />
                     </ContentTemplate>
                  </asp:UpdatePanel> 
            </td> 
        </tr>                
    </table>  
   <%-- <input type="hidden" id="inputHiddenMinDate" name="inputHiddenMinDate" runat="server" />--%>
 </fieldset>

<asp:UpdatePanel ID="upDivNetunim" runat="server" RenderMode="Inline">
    <ContentTemplate> 
         <div id="divNetunim" runat="server"  style="text-align:right;">
             <br /> 
             <div runat="server" ID="pnlgrdRitzot" style="Height:270px;Width:952px;overflow-y:scroll;direction:ltr;">
                <asp:GridView ID="grdRitzot" runat="server"  GridLines="None"  ShowHeader="true"
                       CssClass="Grid"   AllowPaging="false" AutoGenerateColumns="false"   AllowSorting="true"
                       Width="930px"   EmptyDataText="לא נמצאו נתונים!"  EmptyDataRowStyle-CssClass="GridHeader"
                       onrowdatabound="grdRitzot_RowDataBound" onsorting="grdRitzot_Sorting" >
                    <Columns>
                         <asp:BoundField DataField="ZMAN_HATCHALA" HeaderText="תאריך הרצה" SortExpression="ZMAN_HATCHALA" ItemStyle-Width="95px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  DataFormatString="{0:dd/MM/yyyy}" HtmlEncodeFormatString="true"/>
                        <asp:BoundField DataField="bakasha_id" HeaderText="מספר ריצה" SortExpression="bakasha_id" ItemStyle-Width="90px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                        <asp:BoundField DataField="TEUR" HeaderText="תאור" SortExpression="TEUR" ItemStyle-CssClass="ItemRow" ItemStyle-Width="360px" HeaderStyle-CssClass="GridHeader" />
                        <asp:BoundField DataField="auchlusia" HeaderText="אוכלוסיה לריצה" SortExpression="auchlusia" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  ItemStyle-Width="120px" />
                        <asp:BoundField DataField="tkufa" HeaderText="תקופת הריצה - עד חודש (כולל)" SortExpression="tkufa" ItemStyle-Width="195px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                        <asp:BoundField DataField="ritza_gorfet" HeaderText="ריצה גורפת" SortExpression="ritza_gorfet" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  ItemStyle-Width="65px"/>
                        <asp:TemplateField>
                            <HeaderStyle CssClass="GridHeader" />
                            <ItemStyle CssClass="ItemRow" Width="65px" />
                           <ItemTemplate>
                                       <asp:button ID="btnTransfer" runat="server" text="העבר" CssClass ="ImgButtonSearch"  OnClick="TransferRitza" />
                            </ItemTemplate>
                        </asp:TemplateField>
                     </Columns> 
                    <RowStyle CssClass="GridAltRow"   />
                 </asp:GridView>
             </div>
        </div>
                   
    </ContentTemplate>
</asp:UpdatePanel> 
<asp:UpdatePanel ID="upMessage" runat="server">
  <ContentTemplate>
     <asp:Button  ID="btnShowMessage" runat="server" onclick="btnShowMessage_Click"  style="display:none;"/> 
        <cc1:ModalPopupExtender ID="ModalPopupEx" OkControlID="btnShowMessage" CancelControlID="btnConfirm" 
                DropShadow="false" X="400" Y="200" PopupControlID="paMessage" TargetControlID="btnShowMessage"  runat="server" >
        </cc1:ModalPopupExtender>
       <asp:Panel runat="server" style="display:none"   ID="paMessage" CssClass="PanelMessage"  >
        <asp:Label ID="lblHeaderMessage" runat ="server" Width="97%" BackColor="#696969"></asp:Label>
        <br /><br />
        <asp:Label ID="lblMessage" runat ="server" Width="90%"></asp:Label>
       <br />  <br />
        <asp:Button ID="btnConfirm" runat="server" Text="אישור" CssClass="ImgButtonMake" onclick="btnConfirm_Click"/>
     </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
 <script language="javascript" type="text/javascript">


function TransetToSachar(iRequestId,iRequestIdToTtansfer) 
{
  wsBatch.TransferToHilan(iRequestId,iRequestIdToTtansfer);//, TransetToSacharSucceeded);
}
/*
function OnChange_Taarich(id) {
    var minDate;
    var myDate = id.date;
    var arrDate;
    var todey = new Date();
   // debugger;
    if (myDate != "") {
        arrDate = document.getElementById('ctl00_KdsContent_inputHiddenMinDate').value.split('/');
        minDate = new Date(arrDate[2], arrDate[1] - 1, arrDate[0], '00', '00', '00');
        if (myDate < minDate || myDate > todey) {
            alert("טווח התאריכים האפשרי הוא חודש קודם מתאריך נוכחי ועד היום");
            document.getElementById('ctl00_KdsContent_btnShow').disabled = true;
        }
        else document.getElementById('ctl00_KdsContent_btnShow').disabled = false;
    }
}*/
//function TransetToSacharSucceeded(result)
// {
//    if (result == 'OK')
//     {
//        alert('הסתיים בהצלחה');
//    }
//}
 </script> 
</asp:Content>

