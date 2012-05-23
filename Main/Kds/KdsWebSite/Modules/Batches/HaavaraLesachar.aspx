<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="HaavaraLesachar.aspx.cs" Inherits="Modules_Batches_HaavaraLesachar" %>
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
             <%--  <asp:RequiredFieldValidator ID="vldReqFromDate" runat="server" Display="Dynamic" CssClass="ErrorMessage"  ErrorMessage="!חובה להזין מתאריך " ControlToValidate="clnFromDate"></asp:RequiredFieldValidator>--%>
          <%--     <asp:CompareValidator ID="vldTypeFromDate" runat="server" Display="Dynamic" ControlToValidate="clnFromDate" ErrorMessage="!מתאריך לא תקין" Type="Date" Operator="DataTypeCheck"></asp:CompareValidator>--%>
                </td>  
                 <td style="width:10px"></td>
                <td class="InternalLabel" style="width:60px">עד תאריך:</td> 
                <td align="right" dir="ltr" style="width:150px">        
                 <KdsCalendar:KdsCalendar runat="server" ID="clnToDate"  AutoPostBack="false"  dir="rtl" PopupPositionCallOut="Left" ></KdsCalendar:KdsCalendar>                       
                    <%--<wccEgged:wccCalendar runat="server" ID="clnToDate"   BasePath="../../EggedFramework" AutoPostBack="false" Width="80px" dir="rtl"></wccEgged:wccCalendar>--%>                                                                                                                      
              <%--  <asp:RequiredFieldValidator ID="vldReqToDate" runat="server" Display="Dynamic" CssClass="ErrorMessage"  ErrorMessage="!חובה להזין עד תאריך" ControlToValidate="clnToDate"></asp:RequiredFieldValidator>
                   <asp:CompareValidator ID="vldComToDate" runat="server" Display="Dynamic" CssClass="ErrorMessage"  ErrorMessage="!עד תאריך חייב להיות גדול ממתאריך" ControlToValidate="clnToDate" ControlToCompare="clnFromDate" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>--%>
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
                         <asp:BoundField DataField="ZMAN_HATCHALA" HeaderText="תאריך הרצה" SortExpression="ZMAN_HATCHALA" ItemStyle-Width="70px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  DataFormatString="{0:dd/MM/yyyy}" HtmlEncodeFormatString="true"/>
                        <asp:BoundField DataField="bakasha_id" HeaderText="מספר ריצת חישוב" SortExpression="bakasha_id" ItemStyle-Width="70px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                        <asp:BoundField DataField="TEUR" HeaderText="תאור" SortExpression="TEUR" ItemStyle-CssClass="ItemRow" ItemStyle-Width="260px" HeaderStyle-CssClass="GridHeader" />
                        <asp:BoundField DataField="auchlusia" HeaderText="אוכלוסיה לריצה" SortExpression="auchlusia" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  ItemStyle-Width="120px" />
                        <asp:BoundField DataField="tkufa" HeaderText="תקופת הריצה - עד חודש (כולל)" SortExpression="tkufa_date" ItemStyle-Width="130px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                        <asp:BoundField DataField="tkufa_date" ItemStyle-CssClass="ItemRow"  ItemStyle-Width="0px"  DataFormatString="{0:dd/MM/yyyy}" HtmlEncodeFormatString="true"/>
                        <asp:BoundField DataField="ritza_gorfet" HeaderText="ריצה גורפת" SortExpression="ritza_gorfet" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  ItemStyle-Width="35px"/>
                        <asp:BoundField DataField="HUAVRA_LESACHAR" ItemStyle-CssClass="ItemRow"  ItemStyle-Width="0px"/>
                        <asp:BoundField DataField="ISHUR_HILAN" ItemStyle-CssClass="ItemRow"  ItemStyle-Width="0px"/>
                        <asp:TemplateField  HeaderText="יצירת קבצים לשכר">
                            <HeaderStyle CssClass="GridHeader" />
                            <ItemStyle CssClass="ItemRow" Width="40px" />
                           <ItemTemplate>
                                       <asp:button ID="btnTransfer" runat="server" text="העבר" CssClass ="ImgButtonSearch"  OnClick="TransferRitza" />
                                       <asp:button ID="btnCancel" runat="server" text="בטל" CssClass ="ImgButtonSearch"  OnClick="CancelRitza" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="status_haavara_lesachar" ItemStyle-CssClass="ItemRow"  ItemStyle-Width="0px"/>
                        <asp:BoundField  HeaderText="סטטוס העברה לשכר" SortExpression="status_haavara_lesachar" ItemStyle-CssClass="ItemRow" ItemStyle-Width="55px" HeaderStyle-CssClass="GridHeader" />
                         <asp:TemplateField  HeaderText="ריכוזי עבודה">
                            <HeaderStyle CssClass="GridHeader" />
                            <ItemStyle CssClass="ItemRow"  />
                           <ItemTemplate>
                                       <asp:button ID="btnYeziratRikuzim"  runat="server"  Width="95px" text="יצירת ריכוזים"  CssClass ="ImgButtonSearch"  OnClick="YeziratRikuzim" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="status_yezirat_rikuzim" ItemStyle-CssClass="ItemRow"  ItemStyle-Width="0px"/>
                        <asp:TemplateField  HeaderText="שליחה למייל">
                            <HeaderStyle CssClass="GridHeader" />
                            <ItemStyle CssClass="ItemRow" Width="40px" />
                           <ItemTemplate>
                                       <asp:button ID="btnSendMail"  runat="server"  Width="40px" text="שלח" Enabled="false" CssClass ="ImgButtonSearch"  OnClick="ShlichatRikuzimMail" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="rizot_zehot" ItemStyle-CssClass="ItemRow"  ItemStyle-Width="0px"/>
                        
                        <asp:TemplateField  HeaderText="אישור חילן">
                            <HeaderStyle CssClass="GridHeader" />
                            <ItemStyle CssClass="ItemRow" Width="90px" />
                           <ItemTemplate>
                                       <asp:button ID="btnYes"  runat="server"  Width="30px" text="כן" Enabled="true" CssClass ="ImgButtonSearch"  OnClick="TransferRitza" />
                                       <asp:button ID="btnNo"  runat="server"  Width="30px" text="לא" Enabled="true" CssClass ="ImgButtonSearch"  OnClick="TransferRitza" />
                            </ItemTemplate>
                        </asp:TemplateField>
                     </Columns> 
                    <RowStyle CssClass="GridAltRow"   />
                 </asp:GridView>
             </div>
        </div>
       <input type="hidden" id="inputHiddenBakasha" name="inputHiddenBakasha" runat="server" />
       <input type="hidden" id="inputSourceBtnHilan" name="inputSourceBtnHilan" runat="server" />
      <%--  <asp:Button  ID="btnHiddenTransfer" runat="server" onclick="Transfer_Click"  style="display:none;"/> --%>
        <%--<asp:Button  ID="btnHiddenIshurHilan" runat="server" onclick="IshurHilan_Click"  style="display:none;"/>       --%>
    </ContentTemplate>
</asp:UpdatePanel> 
<asp:UpdatePanel ID="upMessage" runat="server" RenderMode="Inline" UpdateMode="Always">
  <ContentTemplate>
     <asp:Button  ID="btnShowMessage" runat="server" onclick="btnShowMessage_Click"  style="display:none;"/> 
        <cc1:ModalPopupExtender ID="ModalPopupEx" OkControlID="btnShowMessage" CancelControlID="btnConfirm" 
                DropShadow="false" X="250" Y="200" PopupControlID="paMessage" TargetControlID="btnShowMessage"  runat="server" >
        </cc1:ModalPopupExtender>
       <asp:Panel runat="server" width="450px" height="150px" style="display:none"   ID="paMessage" CssClass="PanelMessage"  >
        <asp:Label ID="lblHeaderMessage" runat ="server" Width="97%" BackColor="#696969"></asp:Label>
        <br /><br />
        <asp:Label ID="lblMessage" runat ="server" Width="90%"></asp:Label>
       <br />  <br />

        <input type="button" id="btnConfirm" value="אישור" class="ImgButtonMake" name="btnConfirm" onserverclick="btnConfirm_Click" runat="server" />
        <input type="button" id="btnYesTransfer" value="כן" class="ImgButtonMake" name="btnYesTransfer" onserverclick="Transfer_Click" runat="server" />
        <input type="button" id="btnYesHilan" value="כן" class="ImgButtonMake" name="btnYesHilan" onserverclick="IshurHilan_Click" runat="server" />
        <input type="button" id="btnNoTransfer" value="לא" class="ImgButtonMake" name="btnNoTransfer" runat="server" onclick="CloseMessage();" />

<%--       <asp:Button ID="btnConfirm" runat="server" Text="אישור"  CssClass="ImgButtonMake" onclick="btnConfirm_Click"/>
        <asp:Button ID="btnYesTransfer" runat="server" Text="כן"   CssClass="ImgButtonMake" onclick="Transfer_Click"/>
        <asp:Button ID="btnNoTransfer" runat="server" Text="לא"   CssClass="ImgButtonMake"  />--%>
      </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
 
 
 <script language="javascript" type="text/javascript">


function TransetToSachar(iRequestId,iRequestIdToTtansfer) 
{
  wsBatch.TransferToHilan(iRequestId,iRequestIdToTtansfer);//, TransetToSacharSucceeded);
}

function YeziratRikuzim(iRequestId, iRequestIdForRikuzim) {
    wsBatch.YeziratRikuzim(iRequestId, iRequestIdForRikuzim); //, TransetToSacharSucceeded);
}

function ShlichatRikuzimMail(iRequestId, iRequestIdForRikuzim) {
    wsBatch.ShlichatRikuzimMail(iRequestId, iRequestIdForRikuzim); //, TransetToSacharSucceeded);
}

//function ShowMessage(rizot) {
// var answer = confirm("קיימות ריצות קודמות " + rizot + "עם פרמטרים זהים ונוצרו עבורן קבצי העברה לשכר\n.  שים לב!!! יש לבטל העברה לשכר בריצות שלא חושבו בגינן משכורת,אי ביטול ריצות אלו יגרור יצירה לא תקינה של קבצי שכר מהריצה הנוכחית. \nהאם להמשיך ליצור קבצי שכר מהריצה הנוכחית?");
//        if (answer) {
//            document.getElementById("ctl00_KdsContent_btnHiddenTransfer").click();
//        }
//    }

function CloseMessage() {
    $find("ctl00_KdsContent_ModalPopupEx").hide();
}

//    function ShowMessageHilan(msg,source) {
//        var answer = confirm(msg);

//        if (source == "NO") {
//            if (answer) {
//                document.getElementById("ctl00_KdsContent_btnHiddenIshurHilan").click();
//            }
//        }
//        else {
//            if (answer) {
//                document.getElementById("ctl00_KdsContent_btnHiddenIshurHilan").click();
//            }
//        }
//       
//    }

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

