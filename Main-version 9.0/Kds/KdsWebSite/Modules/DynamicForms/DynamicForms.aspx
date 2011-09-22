<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DynamicForms.aspx.cs" Inherits="DataEntryView" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjxToolKit" %>
<%@ Register TagPrefix="kds" TagName="GridViewPager" Src="~/UserControls/GridViewPager.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="../../Js/SystemManager.js" type="text/javascript"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">
    <asp:UpdatePanel ID="upFilter" runat="server">
     <ContentTemplate>
    <div id="DivRefreshTable" runat="server" style="display:none">
    <asp:Button Text="רענן טבלה" ID="BntRefreshTable"   runat="server" 
             CssClass="ImgButtonSearch" Width="100px"   />
    <AjxToolKit:ModalPopupExtender ID="ModPopupdExt" runat="server"
        TargetControlID="BntRefreshTable"
        PopupControlID="ModalPopupPanel"
        DropShadow="true" 
        CancelControlID="BtnCancel" 
         />
    <asp:Panel  runat="server" ID="ModalPopupPanel"  CssClass="PanelMessage" Width="220px" Height="90px" style="display:none" >
        <table width="100%">
        <tr><td colspan="2" align="center" ><asp:Label runat="server" BackColor="#696969" Text="רענון טבלה"  Width="100%"></asp:Label></td></tr>
        <tr><td align="center" colspan="2">האם ברצונך לרענן את טבלת ה- tmp?</td></tr>
        <tr><td align="center"><asp:Button ID="BtnOk" CssClass="ImgButtonSearch" runat="server" Text="כן" OnClick="BntRefreshTable_Click" /></td>
            <td align="center"><button id="BtnCancel" class="ImgButtonSearch" >לא</button></td>
        </tr>
        </table>
    </asp:Panel>        
    </div>
             
     <fieldset class="FilterFieldSet" style="height:50px"> 
        <legend id="LegendFilter" style="background-color:White" >בחירת תצוגה ל </legend> 
       
        <table class="FilterTable" >
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline" UpdateMode="Always"><ContentTemplate>
                <asp:RadioButton Checked="true"  ID="rdoFilterKod"  OnClick="InitTextBox()" runat="server" GroupName="grpFilterKodDesc" Text="מספר סידורי מיוחד"  > </asp:RadioButton>
                <asp:TextBox runat="server" ID="txtFilterKod" style="width:70px" onChange="CheckKod()" AutoComplete="Off"></asp:TextBox>
                <AjxToolKit:AutoCompleteExtender id="AutoCompleteKod"  runat="server" CompletionInterval="0" CompletionSetCount="12" UseContextKey="true"  DelimiterCharacters=";" 
                    TargetControlID="txtFilterKod" MinimumPrefixLength="1" ServiceMethod="GetMatchingKod"  ServicePath ="~/Modules/WebServices/wsDynamicForms.asmx" 
                    EnableCaching="true"  CompletionListCssClass="autocomplete_completionListElement"
                    CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                    CompletionListItemCssClass="autocomplete_completionListItemElement" >                                
                </AjxToolKit:AutoCompleteExtender>
                </ContentTemplate></asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Always">
     <ContentTemplate>
                <asp:RadioButton ID="rdoFilterDescription"  OnClick="InitTextBox()" runat="server" GroupName="grpFilterKodDesc" Text="תיאור" > </asp:RadioButton>
                <asp:TextBox Enabled="false" runat="server" ID="txtFilterDescription"  style="width:450px"  onChange="CheckDescription()" ></asp:TextBox>
                <AjxToolKit:AutoCompleteExtender id="AutoCompleteDescription"  runat="server" CompletionInterval="0" CompletionSetCount="12" UseContextKey="true"  DelimiterCharacters=";" 
                    TargetControlID="txtFilterDescription" MinimumPrefixLength="1" ServiceMethod="GetMatchingDescription" ServicePath ="~/Modules/WebServices/wsDynamicForms.asmx"
                    EnableCaching="true"  CompletionListCssClass="autocomplete_completionListElement"
                    CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                    CompletionListItemCssClass="autocomplete_completionListItemElement" >                                
                </AjxToolKit:AutoCompleteExtender>
                </ContentTemplate>
                </asp:UpdatePanel>
                

            </td>
            <td>
                <asp:Label runat="server" ID="lblMonth" Text="חודש"></asp:Label>
                <asp:DropDownList runat="server" ID="ddlMonth" style="width:103px;"></asp:DropDownList>
            </td>
            <td><asp:Button Text="הצג" ID="btnDisplay" OnClick="btnDisplay_Click" runat="server" CssClass="ImgButtonSearch" />
            <asp:TextBox runat="server" ID="txtFormType" style="display:none;"></asp:TextBox> </td>
        </tr>
        </table>
        </fieldset>
   </ContentTemplate>
 </asp:UpdatePanel>  
 
   <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
      <ContentTemplate>    
      <table>
                <tr>
                    <td><asp:Button Text="הוסף" ID="btnNew" runat="server" CssClass="ImgButtonSearch" /></td>
                    <td>    <asp:TextBox runat="server" ID= "TxtSelectedRow" style="display:none;"></asp:TextBox></td>
                    <td>    <asp:Button runat="server" ID="btnGetHistory" OnClick ="btnGetHistory_Click" style="display:none;" /> </td>
                   <td> <asp:HiddenField ID="hdQueryString" runat="server"  />  <asp:Button ID="btnRefresh" runat="server" CssClass="Hidden" /></td>
            <td>
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label> 
            </td>
                </tr>
      <tr><td colspan="2">
         <div ID="DivData" runat="server" Visible="false">
            <table>
                <tr>
                    <td>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" 
                                AllowPaging="true" AllowSorting="true" 
                                EmptyDataText="לא נמצאו נתונים!" 
                                CssClass="Grid" 
                                HeaderStyle-CssClass="GridHeader"
                                HeaderStyle-ForeColor="White" 
                                PageSize="8" onrowdatabound="GridView1_RowDataBound" SelectedIndex="0" 
                             ondatabound="GridView1_DataBound">
                                <Columns>
                                    <asp:TemplateField ItemStyle-CssClass="ItemRow">
                                    <HeaderTemplate>X</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="lbDelete" ImageUrl="~/Images/delete.png" CommandName="Select" OnClick="PromptDelete" />
                                    <AjxToolKit:ConfirmButtonExtender ID="cbDelete" runat="server"
                                    TargetControlID="lbDelete" DisplayModalPopupID="mpConfirm" />
                                    <AjxToolKit:ModalPopupExtender ID="mpConfirm" runat="server"
                                    TargetControlID="lbDelete"
                                    PopupControlID="pnlConfirm"
                                    OkControlID="OkConfButton" 
                                    CancelControlID="CancelConfButton"  />
                                    <asp:Panel runat="server" ID="pnlConfirm" CssClass="PanelMessage" Width="200px" style="display:none;position:static;" >
                                        <asp:Label ID="lblHeaderMessage" runat="server" Width="97%" BackColor="#696969" ForeColor="White">מחיקה</asp:Label>
                                        <br /> <br /> 
                                        <asp:Label runat="server" ID="lblPrompt"></asp:Label>
                                        <br /><br /> 
                                        <asp:Button ID="OkConfButton" runat="server" Text="OK" CssClass="ImgButtonMake" CausesValidation="false" OnClick="PromptDelete" CommandName="Select" />
                                        <asp:Button ID="CancelConfButton" runat="server" CssClass="ImgButtonMake" CausesValidation="false" Text="Cancel" />
                                    </asp:Panel>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="ItemRow">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbSelect" runat="server" CommandName="Select"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="ItemRow" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="GridHeader" ForeColor="White" />
                                <AlternatingRowStyle CssClass="GridAltRow"  />
                                <RowStyle CssClass="GridRow"   />
                                <SelectedRowStyle CssClass="SelectedGridRowNoIndent" />
                                <PagerStyle CssClass="GridPager" HorizontalAlign="Center"  />                          
                                <EmptyDataRowStyle CssClass="GridEmptyData" height="20px" Wrap="False"/> 
                                <PagerTemplate>
                                     <kds:GridViewPager runat="server" ID="ucGridPager" />
                                </PagerTemplate>
                         </asp:GridView>
                       </td>
                </tr>
            </table>
           <asp:UpdatePanel  ID="UpdatePanel5" runat="server" RenderMode="Inline"  UpdateMode="Conditional">
                <ContentTemplate>   
              <AjxToolKit:CollapsiblePanelExtender ID="cpe" runat="Server"   
                                    TargetControlID="PnlHistory"
                                    ExpandControlID="LblHistory" 
                                    CollapseControlID="LblHistory" 
                                    Collapsed="True"
                                    TextLabelID="LblHistory" 
                                    CollapsedSize="0"
                                    ExpandedText="הסתר נתונים היסטוריים ..." 
                                    CollapsedText="הצג נתונים היסטוריים ..."
                                    ImageControlID="Image1" 
                                    ExpandedImage="../../images/collapse_blue.jpg" 
                                    CollapsedImage="../../images/expand_blue.jpg"
                                    SuppressPostBack="false" 
                                    ExpandDirection="Vertical" 
                                    ScrollContents="false" ExpandedSize="130"  >
                                </AjxToolKit:CollapsiblePanelExtender>

           <table width="100%" >
                <tr  >
                <td >
                    <asp:Panel Width="100%" ID="TitleHistoryPanel" runat="server" CssClass="collapsePanelHeader" > 
                        &nbsp;<asp:Image ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg"/>&nbsp;&nbsp;
                        <asp:Label ID="LblHistory"    runat="server" Font-Underline="true"></asp:Label>
                    </asp:Panel>
                </td></tr> 
                <tr><td  >
                    <asp:Panel ID="pnlHistory" runat="server"  Height="0px" style="overflow: hidden;" ScrollBars=None  >
                          <asp:UpdatePanel ID="upGrid" runat="server" RenderMode="Inline"  >
                            <ContentTemplate> 
                            <table width="100%" height="10px" align="left" >
                                <tr><td>
                                    <asp:Panel runat="server" ID="PnlKodDescription">
                                        <table><tr>
                                                <td><asp:Label ID="LblKodHistory"  runat="server">מספר קוד</asp:Label></td>
                                                <td><asp:TextBox ID="TxtKodHistory" style="text-align:right" Enabled="false" Width="40px" runat="server"></asp:TextBox></td>
                                                <td ><asp:Label ID="LblDescriptionHistory" runat="server">תיאור קוד</asp:Label></td>
                                                <td><asp:TextBox ID="TxtDescriptionHistory" style="text-align:right" Enabled="false" 
                                                        Width="480px"  runat="server"></asp:TextBox>
                                            </tr></table>
                                    </asp:Panel>
                                </td></tr>
                                <tr><td align="right">
                                    <asp:Panel runat="server"  Height=80px ID="PnlGridHistory"  ScrollBars=none Direction=LeftToRight>
                                        <asp:GridView ID="grdHistory" runat="server" AllowSorting="true"    
                                            AllowPaging="true" PageSize="4" 
                                            AutoGenerateColumns="false" ItemStyle-CssClass="ItemRow" 
                                            CssClass="Grid"  EmptyDataText="לא קיימת היסטוריה לקוד שנבחר !"  
                                            onpageindexchanging="grdHistory_PageIndexChanging"
                                            HtmlEncodeFormatString="true" onsorting="grdHistory_Sorting" 
                                            onrowdatabound="grdHistory_RowDataBound" 
                                            onrowcreated="grdHistory_RowCreated"    >
                                        <Columns>
                                        </Columns>
                                        <HeaderStyle CssClass="GridHeader" ForeColor="White" />
                                        <AlternatingRowStyle CssClass="GridAltRow"  />
                                        <RowStyle CssClass="GridRow"   />
                                        <PagerStyle  CssClass="GridPager" HorizontalAlign="Center"  />                          
                                        <EmptyDataRowStyle CssClass="GridEmptyData" height="10px" Wrap="False"/>                                                    
                                        <PagerTemplate>
                                            <kds:GridViewPager runat="server" ID="ucGridHistoryPager" />
                                        </PagerTemplate>
                                        </asp:GridView>
                                    </asp:Panel>
                                 </td></tr>
                            </table>
                            </ContentTemplate>
                          </asp:UpdatePanel>       
                    </asp:Panel>
                </td></tr>
            </table>
               </ContentTemplate>
           </asp:UpdatePanel>        
      </div>
      </td></tr></table>
   
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnDisplay"  />
    </Triggers>
 </asp:UpdatePanel>
    <script type="text/javascript">
        function refresh(){
            $get("<%=btnRefresh.ClientID %>").click();
        }

        function InitTextBox() {

            document.getElementById("ctl00_KdsContent_txtFilterKod").value = '';
            document.getElementById("ctl00_KdsContent_txtFilterDescription").value = '';
            var FilterKod =  document.getElementById("ctl00_KdsContent_rdoFilterKod").checked;
            if (FilterKod == true )
            {
                document.getElementById("ctl00_KdsContent_txtFilterKod").disabled  = false ;
                document.getElementById("ctl00_KdsContent_txtFilterDescription").disabled  = true;
            }
            else 
            {
                document.getElementById("ctl00_KdsContent_txtFilterKod").disabled  = true ;
                document.getElementById("ctl00_KdsContent_txtFilterDescription").disabled  = false;
            }
            EnableButtons();
        }
        
        function CheckDescription() {
            var sDescription=document.getElementById("ctl00_KdsContent_txtFilterDescription").value;
            var sCurrentFormType =parseInt(document.getElementById("ctl00_KdsContent_txtFormType").value);
            if (sDescription!='')
            {
                var sKod = wsDynamicForms.GetKodOfDescription(sDescription,sCurrentFormType,CheckDescriptionSucceded);
            }
        }
        function CheckDescriptionSucceded(result){
            if ((result=='') || (result=='null'))
            {
            AlertAndClearDescription('תיאור לא קיים');
            }
            else if (result== '-1') 
            {
            AlertAndClearDescription('קוד לא פעיל');
            }
            else 
            {
                setTimeout("setFilterKod('"+result+"')",500);
            }
        }
        function AlertAndClearDescription(AlertString){
            alert(AlertString);
            document.getElementById("ctl00_KdsContent_txtFilterDescription").value='';
            document.getElementById("ctl00_KdsContent_txtFilterDescription").focus();        
        }
        
        function CheckKod()
        {
            var sKod=document.getElementById("ctl00_KdsContent_txtFilterKod").value;
            var sCurrentFormType =parseInt(document.getElementById("ctl00_KdsContent_txtFormType").value);
            if (!isNaN(sKod) )
            {
                if (sKod!='')
                {                      
                    wsDynamicForms.GetDescriptionOfKod(parseInt(sKod),sCurrentFormType,CheckKodSucceded);
                }
            } 
            else 
            {
                document.getElementById("ctl00_KdsContent_txtFilterDescription").value='';
            }  
        }
        function CheckKodSucceded(result)
        {
            if ((result=='') || (result=='null'))
            {
            AlertAndClearKod('קוד לא קיים');
            }
            else if (result== '-1') 
            {
            AlertAndClearKod('קוד לא פעיל');
            }
            else 
            {
                setTimeout("setFilterDesc('"+result+"')",500);
            }
        }
        function setFilterDesc(result)
        {
            document.getElementById("ctl00_KdsContent_txtFilterDescription").value=result;
            EnableButtons();
        }
         function setFilterKod(result)
        {
            document.getElementById("ctl00_KdsContent_txtFilterKod").value=result;
            EnableButtons();
        }
        function AlertAndClearKod(AlertString) {
            alert(AlertString);
            document.getElementById("ctl00_KdsContent_txtFilterKod").value = '';
            document.getElementById("ctl00_KdsContent_txtFilterKod").focus();
        }

        function CheckChanged(RowSelected) {
                document.getElementById("ctl00_KdsContent_TxtSelectedRow").value = RowSelected;
                var but = document.getElementById("ctl00_KdsContent_btnGetHistory");
                but.click();
        }
        function EnableButtons()
        {
            var sCurrentFormType =parseInt(document.getElementById("ctl00_KdsContent_txtFormType").value);
            var FilterKod = document.getElementById("ctl00_KdsContent_txtFilterKod").value;
            var btnNew = document.getElementById("ctl00_KdsContent_btnNew");
            var btnDisplay = document.getElementById("ctl00_KdsContent_btnDisplay");
            if (sCurrentFormType == 4)
            {   
                btnDisplay.disabled = false;
                btnNew.disabled = false;
            }
            else 
            {
                btnDisplay.disabled = (FilterKod=='') ? true : false ;
                btnNew.disabled = (FilterKod=='') ? true : false ;
            }
            btnDisplay.className = !btnDisplay.disabled? "ImgButtonSearch" : "ImgButtonSearchDisable" ; 
            btnNew.className = !btnNew.disabled? "ImgButtonSearch" : "ImgButtonSearchDisable" ; 
        }


    </script>
</asp:Content>

