<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" CodeFile="EmployeTotalMonthly.aspx.cs" Inherits="Modules_Ovdim_EmployeTotalMonthly" Title="סיכום חודשי לעובד" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            color: Black;
            font-family: Arial;
            width: 45px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">
  
   <script type="text/javascript" language="javascript">
       var oTxtId = "<%=txtEmpId.ClientID%>";
       var oTxtName = "<%=txtName.ClientID%>";
       var flag = false;
       var userId = iUserId;
</script>
<div class="Progress" id="divHourglass"  style="display:none;text-align:center;position:absolute;left:52%;top:48%; z-index:1000;width:150px" >
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/progress.gif" style="width: 100px; height: 100px" /><br /> 
</div>  
    <fieldset class="FilterFieldSet" style="width:933px"> 
       <legend> בחירת נתונים להצגה </legend>      
     <table style="margin-top:4px" border="0">
        <tr>
            <td class="InternalLabel" style="width:90px">
                   <asp:UpdatePanel ID="UpdatePanel4" runat="server" RenderMode="Inline">
                        <ContentTemplate> 
                        <asp:RadioButton runat="server" Checked="true" ID="rdoId"  EnableViewState="true" GroupName="grpSearch" Text="מספר אישי:"  > </asp:RadioButton>
                    </ContentTemplate>
                  </asp:UpdatePanel> 
                </td>
            <td dir="rtl">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server"  RenderMode="Inline">
                        <ContentTemplate> 
                            <asp:TextBox ID="txtEmpId" runat="server" AutoComplete="Off" dir="rtl"    
                                Width="80px"   EnableViewState="true" onfocus="this.select();"  onkeydown="return ChangeKeyCode(event);"  onchange="onchange_txtid();"></asp:TextBox>                            
                            <cc1:AutoCompleteExtender id="AutoCompleteExtenderID" runat="server" CompletionInterval="0" CompletionSetCount="25" UseContextKey="true"  
                                TargetControlID="txtEmpId" MinimumPrefixLength="1" ServiceMethod="GetOvdimToUser" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                EnableCaching="true"  CompletionListCssClass="ACLst"  EnableViewState="true"
                                CompletionListHighlightedItemCssClass="ACLstItmSel"
                                CompletionListItemCssClass="ACLstItmE"  
                                 OnClientHidden="SimunExtendeIdClose"  OnClientShowing="SimunExtendeOpen"  >                                
                            </cc1:AutoCompleteExtender>                              
                       </ContentTemplate>
                  </asp:UpdatePanel> 
            </td>
            <td style="width:10px"></td>
              <td class="style1">
                   <asp:UpdatePanel ID="UpdatePanel5" runat="server" RenderMode="Inline">
                       <ContentTemplate> 
                       
                         <asp:RadioButton runat="server" ID="rdoName" EnableViewState="true" GroupName="grpSearch" Text="שם:" > </asp:RadioButton>
                    </ContentTemplate>
                  </asp:UpdatePanel> 
            </td>   
            <td style="width:200px">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                       <ContentTemplate> 
                            <asp:TextBox ID="txtName" runat="server"  AutoComplete="Off" style="width:200px"   onfocus="this.select();" EnableViewState="true"  CausesValidation="false" onkeydown="return ChangeKeyCode(event);" 
                            onchange="document.getElementById('ctl00_KdsContent_btnPrint').disabled=true;document.getElementById('ctl00_KdsContent_btnShow').disabled=true;document.getElementById('ctl00_KdsContent_btnShow').className ='ImgButtonSearchDisable'; GetOvedIdByName();"></asp:TextBox>
                          
                            <cc1:AutoCompleteExtender id="AutoCompleteExtenderByName" runat="server" CompletionInterval="0" CompletionSetCount="12" UseContextKey="true"  
                                        TargetControlID="txtName" MinimumPrefixLength="1" ServiceMethod="GetOvdimToUserByName" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                        EnableCaching="true"  CompletionListCssClass="ACLst"  EnableViewState="true"
                                           CompletionListHighlightedItemCssClass="ACLstItmSel" 
                                        CompletionListItemCssClass="ACLstItmE"
                                         OnClientHidden="SimunExtendeNameClose"  OnClientShowing="SimunExtendeOpen"  >                          
                            </cc1:AutoCompleteExtender> 
                         </ContentTemplate>
                   </asp:UpdatePanel>    
                </td>
            <td style="width:10px"></td>
            <td  class="InternalLabel">חודש:</td>
            <td> <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline">
                  <ContentTemplate> 
                     <asp:DropDownList id="ddlMonth" runat="server"  Width="100px"  
                          DataTextField="month_year" DataValueField="month_year" 
                          onselectedindexchanged="ddlMonth_SelectedIndexChanged" AutoPostBack="true" CausesValidation="false">
                            </asp:DropDownList>
                </ContentTemplate>
              </asp:UpdatePanel> 
            </td>
            <td style="width:10px"></td>
            <td> 
            
                <asp:UpdatePanel ID="upBtnShow" runat="server" RenderMode="Inline"  >
                  <ContentTemplate> 
                        <asp:button ID="btnShow" runat="server" text="הצג" CssClass ="ImgButtonSearch"  onclick="btnShow_Click"   OnClientClick="if (!CheckEmployeeId()) {return false;} else {return true;}" />
                <asp:button ID="btnPrint" runat="server" text="הדפסת ריכוז" CssClass ="ImgButtonSearch"  onclick="btnPrint_Click" Width="100px"   OnClientClick="if (!CheckEmployeeId()) {return false;} else {return true;}" />
                 </ContentTemplate>
              </asp:UpdatePanel> 
             
               </td>
            <td> <iframe runat="server" id="iFramePrint" style="display:none" height="5px" width="5px"></iframe></td>
        </tr>                
    </table>  

 </fieldset>
 <br />

  <asp:UpdatePanel ID="upCalc" runat="server" RenderMode="Inline">
      <ContentTemplate> 
               <div class="Progress" id="DivCalc"  style="display:none;text-align:center;position:absolute;left:53%;top:48%; z-index:1000;width:150px" >
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/progress.gif" style="width: 100px; height: 100px" /><br /> מבצע חישוב אנא המתן...
                </div>  
                <asp:button ID="btnCalc" runat="server" text="" CssClass ="ImgButtonSearch" onclick="btnCalc_Click"/>
                 <asp:Button ID="btnHidden" runat="server" OnClick="btnHidden_OnClick"  />
     </ContentTemplate>
  </asp:UpdatePanel> 
           
<asp:UpdatePanel ID="upDivNetunim" runat="server" RenderMode="Inline">
    <ContentTemplate> 
         <div id="divNetunim" runat="server"  style="text-align:right;width:950px">
            <span class="TitleLable">פרטי העובד:</span><br />
            <table width="950px"  class="Grid" cellpadding="1" cellspacing="0">
                <tr class="GridHeader" >
                    <td class="ItemRow">מספר אישי</td>
                    <td class="ItemRow">שם משפחה</td>
                    <td class="ItemRow">שם פרטי</td>
                    <td class="ItemRow">חודש-שנה</td>
                    <td class="ItemRow" style="width:80px;">מעמד</td>
                     <td class="ItemRow" style="width:80px;">עיסוק</td>
                    <td class="ItemRow" style="width:60px;">אזור</td>
                    <td class="ItemRow">קוד גיל</td>
                    <td class="ItemRow">תחנת שכר</td>
                    <td class="ItemRow">עובד 5/6 ימים</td>
                    <td class="ItemRow">תאריך חישוב</td>
                    <td class="ItemRow">סוג החישוב</td>
                </tr>
                <tr class="GridAltRow">
                    <td class="ItemRow"><asp:Label ID="lblEmployeId" runat="server"></asp:Label></td>
                    <td class="ItemRow"><asp:Label ID="lblLastName" runat="server"></asp:Label></td>
                    <td class="ItemRow"><asp:Label ID="lblFirstName" runat="server"></asp:Label></td>
                    <td class="ItemRow"><asp:Label ID="lblMonthYear" runat="server"></asp:Label></td>
                    <td class="ItemRow"><asp:Label ID="lblMaamad" runat="server"></asp:Label></td>
                     <td class="ItemRow"><asp:Label ID="lblIsuk" runat="server"></asp:Label></td>
                    <td class="ItemRow"> <asp:Label ID="lblEzor" runat="server"></asp:Label></td>
                    <td class="ItemRow"><asp:Label ID="lblGil" runat="server"></asp:Label></td>
                    <td class="ItemRow"><asp:Label ID="lblStationSalary" runat="server"></asp:Label></td>
                    <td class="ItemRow"><asp:Label ID="lblWorkDay" runat="server"></asp:Label></td>
                    <td class="ItemRow"><asp:Label ID="lblCalcMonth" runat="server"></asp:Label></td>
                    <td class="ItemRow"><asp:Label ID="lblCalcType" runat="server"></asp:Label></td>
               </tr>
            </table>
            <br />
               <span class="TitleLable">ריכוז עבודה חודשי:</span><br />                            
               <asp:Panel ID="pnlTotalMonthly"  height="129px" width="950px" dir="ltr"  runat="server" ScrollBars="Vertical">   
                 <asp:GridView ID="grdTotalMonthly" runat="server"  GridLines="none" 
                       CssClass="Grid"   AllowPaging="false" AutoGenerateColumns="false"  
                       Width="933px"  onrowdatabound="grdTotalMonthly_RowDataBound" EmptyDataText="לא נמצאו נתונים!" EmptyDataRowStyle-CssClass="GridHeader">
                    <Columns>
                        <asp:BoundField DataField="TEUR_RECHIV" HeaderText="רכיב/יום"  ItemStyle-Width="145px"  ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                        <asp:BoundField DataField="TOTAL" HeaderText="סה''כ "  ItemStyle-Width="65px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"/>
                        <asp:BoundField DataField="TOTAL_IN_HOUR" HeaderText="סה''כ בשעות" ItemStyle-CssClass="ItemRow"  HeaderStyle-CssClass="GridHeader"/>
                        <asp:BoundField DataField="KIZUZ_MEAL_MICHSA" HeaderText="קיזוז מעל מכסה" ItemStyle-Width="75px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"/>
                        <asp:BoundField DataField="TOTAL_TO_PAY" HeaderText="סה''כ לתשלום" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"/>
                        <asp:BoundField DataField="YOM1" HeaderText="1"   ItemStyle-Width="45px"  ItemStyle-Wrap="false" ItemStyle-CssClass="ItemRow"  HeaderStyle-CssClass="GridHeader" />
                        <asp:BoundField DataField="YOM2" HeaderText="2"  ItemStyle-Width="45px"  ItemStyle-Wrap="false" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"/>
                        <asp:BoundField DataField="YOM3" HeaderText="3"  ItemStyle-Width="45px"  ItemStyle-Wrap="false" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"/>
                        <asp:BoundField DataField="YOM4" HeaderText="4"   ItemStyle-Width="45px"  ItemStyle-Wrap="false" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader" />
                        <asp:BoundField DataField="YOM5" HeaderText="5"  ItemStyle-Width="45px"  ItemStyle-Wrap="false" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"/>
                        <asp:BoundField DataField="YOM6" HeaderText="6"  ItemStyle-Width="45px"  ItemStyle-Wrap="false" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"/>
                        <asp:BoundField DataField="YOM7" HeaderText="7"  ItemStyle-Width="45px"  ItemStyle-Wrap="false"  ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader" />
                        <asp:BoundField DataField="YOM8" HeaderText="8"  ItemStyle-Width="45px"  ItemStyle-Wrap="false" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"/>
                        <asp:BoundField DataField="YOM9" HeaderText="9"  ItemStyle-Width="45px"  ItemStyle-Wrap="false" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"/>
                         <asp:BoundField DataField="YOM10" HeaderText="10"  ItemStyle-Width="45px"  ItemStyle-Wrap="false" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"/>
                          <asp:BoundField DataField="YOM11" HeaderText="11"  ItemStyle-Width="45px" ItemStyle-Wrap="false" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"/>
                     </Columns> 
                    <RowStyle CssClass="GridAltRow"   />
                 </asp:GridView>
               </asp:Panel>  
                <br />
              <table width="950px" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td  valign="top" style="width:350px" align="right" rowspan="2">
                         <span class="TitleLable">רכיבים חודשיים:</span><br />
                           <asp:Panel ID="pnlMonthlyComponents"  height="94px" width="270px" dir="ltr"  runat="server" ScrollBars="Vertical">   
                            <asp:GridView ID="grdMonthlyComponents" runat="server"   GridLines="none" CssClass="Grid"  EmptyDataText="לא נמצאו נתונים!" EmptyDataRowStyle-CssClass="GridHeader" AllowPaging="false" AutoGenerateColumns="false"  Width="250px">
                                <Columns>
                                    <asp:BoundField DataField="TEUR_RECHIV" HeaderText="רכיב"  ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                                    <asp:BoundField DataField="ERECH_RECHIV" HeaderText="ערך" ItemStyle-Width="100px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"/>
                                </Columns> 
                                    <RowStyle CssClass="GridAltRow"   />
                                    <HeaderStyle CssClass="GridHeader" /> 
                             </asp:GridView>
                           </asp:Panel >  
                    </td>
                    <td valign="top" style="width:400px"><br /></td>
                    <td  valign="top" style="width:60px" align="left"> <asp:ImageButton ID="Day1to10" runat="server" ImageUrl="~/Images/1-10.jpg" 
                            onclick="Day1to10_Click" /></td>
                    <td  valign="top" style="width:60px" align="left"><asp:ImageButton runat="server" ID="Day11to20" 
                            ImageUrl="~/Images/11-20.jpg" onclick="Day11to20_Click" /></td>
                    <td  valign="top" style="width:60px" align="left"><asp:ImageButton runat="server" ID="Day21to31" 
                            ImageUrl="~/Images/21-31.jpg" onclick="Day21to31_Click" /></td>
                </tr>
                <tr>
                    <td colspan="4" style="width:350px">
                            <br />
                           <span class="TitleLable">נתוני חופשה והיעדרות:</span><br />
                           <asp:GridView ID="grdAbsenceData" runat="server"  GridLines="none" CssClass="Grid"   AllowPaging="false" AutoGenerateColumns="false"  Width="620px" EmptyDataText="לא נמצאו נתונים!" EmptyDataRowStyle-CssClass="GridHeader">
                                <Columns>
                                    <asp:BoundField DataField="CHOFESH_ZCHUT" HeaderText="חופש זכות"  ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                                    <asp:BoundField DataField="CHOFESH_CHOVA" HeaderText="חופש חובה" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"/>
                                    <asp:BoundField DataField="YEMEY_HEADRUT" HeaderText="ימי היעדרות" ItemStyle-CssClass="ItemRow"  HeaderStyle-CssClass="GridHeader" />
                                    <asp:BoundField DataField="YEMEY_MILUIM" HeaderText="ימי מילואים" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"/>
                                    <asp:BoundField DataField="YOM_MACHALA_BODED" HeaderText="יום מחלה בודד" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"/>
                                     <asp:BoundField DataField="YEMEY_MACHALA" HeaderText="ימי מחלה" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"/>
                                    <asp:BoundField DataField="YEMEY_TEUNA" HeaderText="ימי תאונה" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"/>
                                 </Columns> 
                                 <RowStyle CssClass="GridAltRow"   />
                                 <HeaderStyle CssClass="GridHeader" />
                              </asp:GridView>
                    </td>
                </tr>
               </table>
               <br />
          </div>
         
   </ContentTemplate>
</asp:UpdatePanel> 
<script language="javascript" type="text/javascript">

    function SetTextBox() {
        var rdo = document.getElementById("ctl00_KdsContent_rdoId");
        if (rdo.checked) {
            document.getElementById("ctl00_KdsContent_txtEmpId").disabled = false;
            document.getElementById("ctl00_KdsContent_txtName").disabled = true;
        }
        else {
            document.getElementById("ctl00_KdsContent_txtName").disabled = false;
            document.getElementById("ctl00_KdsContent_txtEmpId").disabled = true;
        }
    }
    

    function CheckEmployeeId() {
        if (document.getElementById("ctl00_KdsContent_txtEmpId").value.length == 0 && document.getElementById("ctl00_KdsContent_txtEmpId").value.length == 0) {
            alert('חובה להזין מספר אישי או שם');
            return false;
            }
        else { return true };

    }

    function OpenEmpWorkCard(EmpId,WCardDate) {
        var sQuryString = "?EmpID=" + EmpId + "&WCardDate=" + WCardDate + "&dt=" + Date();
        document.getElementById("divHourglass").style.display = 'block';
        var ReturnWin = ;status: 1;window.showModalDialog('WorkCard.aspx' + sQuryString, window, "dialogHeight: 680px; dialogWidth: 1010px; scroll: no;status: 1;");
        if (ReturnWin == '' || ReturnWin == 'undefined') ReturnWin = false;
        document.getElementById("divHourglass").style.display = 'none';
        return ReturnWin;
    }

    function onchange_txtid() {
        document.getElementById('ctl00_KdsContent_btnShow').disabled = true;
        document.getElementById('ctl00_KdsContent_btnPrint').disabled = true;
        document.getElementById('ctl00_KdsContent_btnShow').className ='ImgButtonSearchDisable';
        GetOvedNameById();
//        if (trim(document.getElementById("ctl00_KdsContent_txtEmpId").value) == "") {
//            document.getElementById("ctl00_KdsContent_txtEmpId").select();
//        }
    }
    
    function continue_click() {
        document.getElementById("ctl00_KdsContent_btnHidden").click();
    }  
   </script>

</asp:Content>

