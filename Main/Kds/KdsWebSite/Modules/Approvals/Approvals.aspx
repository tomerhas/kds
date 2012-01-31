<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Approvals.aspx.cs" Inherits="Modules_Approvals_Approvals" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="kds" TagName="GridViewPager" Src="~/UserControls/GridViewPager.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style type="text/css">
    .Remarks
    {
        background-color:Green;
    }
    .list
    {
        border-color:Silver;
        border-style:solid;
        border-width:1px;
    }
    .warning
    {
        color:Red;
        font-weight:bold;
    }
    .ErrorAppr
    {
        color:Red;
        background-color:White;
    }
    .textRtl
    {
        direction:rtl;
        text-align:right;
    }
    .hpLink
    {
        text-decoration:underline;
        color:#0066cc;
        cursor:hand;
    }
    .bold
    {
        font-weight:bold;
    }
</style>

<!-- Contact Form JS and CSS files -->
<link type="text/css" href="../../css/basic.css" rel="stylesheet" media="screen" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server" >
   <script type="text/javascript" language="javascript">
       var oTxtId = "<%=txtId.ClientID%>";
       var oTxtName = "<%=txtName.ClientID%>";   
</script>
    <asp:UpdateProgress  runat="server" id="GridProgress" DisplayAfter="0" 
                AssociatedUpdatePanelID="upShow"  >
    <ProgressTemplate>
        <div id="divProgress" class="Progress"  style="text-align:right;position:absolute;left:52%;top:40%; z-index:1000" >
              <img src="../../Images/spinner.gif" alt=""/> טוען...
        </div>        
    </ProgressTemplate>
 </asp:UpdateProgress>

 <asp:UpdateProgress  runat="server" id="GridProgress2" DisplayAfter="0" 
                AssociatedUpdatePanelID="udSubmitButtons"  >
    <ProgressTemplate>
        <div id="divProgress" class="Progress"  style="text-align:right;position:absolute;left:52%;top:40%; z-index:1000" >
              <img src="../../Images/spinner.gif" alt=""/> טוען...
        </div>        
    </ProgressTemplate>
 </asp:UpdateProgress>
 
    <fieldset class="FilterFieldSet" >          
        <legend>סינון לפי</legend>
        <asp:UpdatePanel ID="upSite" runat="server" RenderMode="Inline" >
            <ContentTemplate> 
                <table class="FilterTable" style="width:50%;"> 
                    <tr>
                        <td class="InternalLabel" style="width:40px">
                            סטטוס:
                        </td>
                        <td>
                            <asp:DropDownList runat="server" AutoPostBack="true" 
                                ID="ddStatuses" DataTextField="teur_status_ishur" 
                                DataValueField="kod_status_ishur">
                             </asp:DropDownList>
                        </td>
                        <td class="InternalLabel" style="width:40px">
                            חודש:
                        </td>
                        <td>
                            <asp:DropDownList runat="server" AutoPostBack="false" 
                                ID="ddMonths" DataTextField="request_month" 
                                DataValueField="request_month">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="upShow" runat="server" RenderMode="Inline">
                                <ContentTemplate>
                                    <asp:Button Text="הצג" ID="btnExecute" runat="server" 
                                        CssClass ="ImgButtonSearch"  />                            
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddStatuses" />
                <asp:AsyncPostBackTrigger ControlID="btnExecute" />
            </Triggers>
        </asp:UpdatePanel>        
   </fieldset  >
    <fieldset class="FilterFieldSet"> 
        <legend>חיפוש לפי</legend>      
        <table class="FilterTable" style="width:70%;"> 
            <tr>
                <td class="InternalLabel" style="width:80px">
                    <asp:RadioButton runat="server" Checked="true" ID="rdoId" GroupName="grpSearch" Text="מספר אישי"  > </asp:RadioButton>
                </td>
                <td style="width:200px;">
                <asp:UpdatePanel ID="upId" runat="server" RenderMode="Inline" UpdateMode="Conditional" >
                        <ContentTemplate> 
                            <asp:TextBox ID="txtId" runat="server" AutoComplete="Off" dir="rtl" ></asp:TextBox>                            
                            <cc1:AutoCompleteExtender id="AutoCompleteExtenderID" runat="server" CompletionInterval="0" CompletionSetCount="25" UseContextKey="true"  
                                TargetControlID="txtId" MinimumPrefixLength="1" ServiceMethod="GetOvdimById" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                EnableCaching="true"  CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                                CompletionListItemCssClass="autocomplete_completionListItemElement"
                                OnClientHidden="GetOvedNameById">                               
                            </cc1:AutoCompleteExtender>                              
                       </ContentTemplate>
                       <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnExecute" />    
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />                                                                 
                       </Triggers> 
                 </asp:UpdatePanel>           
                </td>                
                <td class="InternalLabel" style="width:30px">
                     <asp:RadioButton runat="server" ID="rdoName" GroupName="grpSearch" Text="שם" > </asp:RadioButton>
                </td>               
                <td style="width:200px">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional"  >
                        <ContentTemplate> 
                            <asp:TextBox ID="txtName" runat="server" AutoComplete="Off" style="width:200px" ></asp:TextBox>
                            <cc1:AutoCompleteExtender id="AutoCompleteExtenderByName" runat="server" CompletionInterval="0" CompletionSetCount="12" UseContextKey="true"  
                                        TargetControlID="txtName" MinimumPrefixLength="1" ServiceMethod="GetOvdimByName" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                        EnableCaching="true"  CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                                        CompletionListItemCssClass="autocomplete_completionListItemElement" 
                                         OnClientHidden="GetOvedIdByName">                              
                            </cc1:AutoCompleteExtender> 
                         </ContentTemplate>
                       <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnExecute" />                                                                    
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" /> 
                       </Triggers> 
                 </asp:UpdatePanel>       
                </td>
                <td colspan="5">
                    <asp:UpdatePanel ID="upSearch" runat="server" RenderMode="Inline" UpdateMode="Conditional" >
                        <ContentTemplate> 
                            <asp:button ID="btnSearch" runat="server" text="חפש"  
                                CssClass ="ImgButtonSearch"  />
                        </ContentTemplate>
                         <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnExecute" />                                                                                               
                       </Triggers> 
                    </asp:UpdatePanel>                                    
                </td>                
            </tr>     
            <tr>
                <td colspan="9"> 
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional" >
                        <ContentTemplate> 
                            <asp:CustomValidator ID="vldEmpNotExists" CssClass="ErrorMessage" 
                                        runat="server" Display="Dynamic" ErrorMessage="עובד לא נמצא בתחום!" 
                                        onservervalidate="vldEmpNotExists_ServerValidate" >
                            </asp:CustomValidator>
                         </ContentTemplate>
                         <Triggers>                                                                                               
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                         </Triggers> 
                     </asp:UpdatePanel> 
                  </td>                                         
            </tr>                 
        </table>
   </fieldset>  
   
   <br />
   <div>
        <label class="SubTitleLabel">אישורים:</label>
   </div>
    <table style="width:70%;">  
    <tr>
        <td>
            <asp:UpdatePanel ID="udApproveButtons" runat="server" RenderMode="Block" UpdateMode="Conditional" >
            <ContentTemplate>
                  
                 <cc1:CollapsiblePanelExtender ID="cpe" runat="Server" 
                            TargetControlID="pnlChooseCodes"
                            ExpandControlID="TitlePanel" 
                            CollapseControlID="TitlePanel" 
                            Collapsed="True"
                            TextLabelID="Label2" 
                            ExpandedText="" 
                            CollapsedText=""
                            ImageControlID="Image1" 
                            ExpandedImage="../../images/collapse_blue.jpg" 
                            CollapsedImage="../../images/expand_blue.jpg"
                            SuppressPostBack="true" 
                            ExpandDirection="Vertical" 
                            ScrollContents="false" ExpandedSize="200" >
                        </cc1:CollapsiblePanelExtender>    
                 <asp:Panel ID="TitlePanel" runat="server" style="visibility:hidden" > 
                       <asp:Image ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg"/>&nbsp;&nbsp;
                       <asp:Label ID="Label2" runat="server" CssClass="link" Font-Underline="true">אישור גורף על פי סוג</asp:Label>
                </asp:Panel>
                 <asp:Panel runat="server" ID="pnlChooseCodes"  Height="0px" style="overflow:hidden;" >
                     <div id="basicModalContent" style='text-align:right;overflow-y:hidden;' >
                           
                            <asp:Label ID="Label1" Text="בחר מקרה/מקרים לאישור" Width="100%" runat="server" CssClass="GridHeader"></asp:Label>
                              <asp:LinkButton runat="server" Font-Bold="true" 
                           ID="btnOK" Text="סמן" style="padding-top:5px;"></asp:LinkButton>

                            <div style="text-align:right;overflow-y:scroll;overflow-x:hidden;
                                height:150px;" class="list">
                               
                                  <asp:CheckBoxList  runat="server" ID="chkCodesToMark" 
                                    RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="3"  
                                    DataTextField="teur_ishur" DataValueField="kod_ishur"  >
                                       
                                    </asp:CheckBoxList>
                                  
                                 
                                  
                            </div>
                            
                                                         
                      </div>
                </asp:Panel>
             </ContentTemplate>  
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnExecute" />
             </Triggers>
            </asp:UpdatePanel>
         </td>
    </tr>
    <tr>
        <td>            
            <asp:UpdatePanel ID="udGrid" runat="server" RenderMode="Inline" UpdateMode="Conditional" >
                 <ContentTemplate> 

                    <asp:GridView ID="grdRequests" runat="server" AllowSorting="true" 
                         AllowPaging="true" PageSize="6" AutoGenerateColumns="false" CssClass="Grid"  
                         Width="960px" EmptyDataText="לא נמצאו נתונים!" Height="70px"
                         HeaderStyle-CssClass="GridHeader"
                         HeaderStyle-ForeColor="White"  
                         DataKeyNames="Mispar_Ishi,Kod_Ishur,Taarich,Mispar_Sidur,Shat_hatchala,Shat_yetzia,Mispar_Knisa,Rama,Erech_Mevukash,Erech_Mevukash2">
                        <Columns> 
                            <asp:TemplateField HeaderText="מאשר">
                                <ItemTemplate>
                                    <asp:CheckBox AutoPostBack="true" ID="chkAccept" runat="server" OnCheckedChanged="chkAccept_CheckChanged" 
                                        Checked=<%# int.Parse(Eval("kod_status_ishur").ToString())==(int)KdsWorkFlow.Approvals.ApprovalRequestState.Approved %> 
                                         />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="לא מאשר">
                                <ItemTemplate>
                                    <asp:CheckBox AutoPostBack="true" ID="chkDecline" runat="server" OnCheckedChanged="chkAccept_CheckChanged" 
                                        Checked=<%# int.Parse(Eval("kod_status_ishur").ToString())==(int)KdsWorkFlow.Approvals.ApprovalRequestState.Declined %> 
                                        />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="מ.א." DataField="Mispar_Ishi" />
                            <asp:BoundField HeaderText="שם" DataField="SHEM" />
                            <asp:BoundField HeaderText="תאור אישור" DataField="Teur_Ishur" />
                            <asp:TemplateField HeaderText="תאריך יום עבודה" >
                                <ItemTemplate>
                                    <asp:HyperLink runat="server" ID="hpWorkDate" 
                                        Text=<%# String.Format("{1} - {0}",System.Globalization.DateTimeFormatInfo.CurrentInfo.ShortestDayNames[(int)((DateTime)Eval("Taarich")).DayOfWeek],Eval("Taarich","{0:dd/MM/yyyy}"))%> 
                                        Target="_blank"
                                        NavigateUrl=<%# KdsWorkFlow.Approvals.ApprovalManager.ShowDateDetailedDescription((int)Eval("kod_ishur"))?String.Format("~/Modules/Ovdim/WorkCard.aspx?EmpID={0}&WCardDate={1}",Eval("Mispar_Ishi"),Eval("Taarich","{0:dd/MM/yyyy}")):"" %>
                                        CssClass=<%# KdsWorkFlow.Approvals.ApprovalManager.ShowDateDetailedDescription((int)Eval("kod_ishur"))?"":"InternalLabel" %>
                                    ></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="סידור">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblSidur" 
                                        Text=<%# String.Format("({1}) {0}",Eval("Mispar_Sidur"),Eval("Shat_Hatchala","{0:HH:mm}")) %> ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="פעילות">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblYetzia" 
                                        Text=<%# String.Format("{1} ({0})",Eval("Mispar_Knisa"),Eval("Shat_yetzia","{0:HH:mm}")) %> ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="תאריך בקשה" DataField="Taarich_Bakashat_Ishur" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:TemplateField HeaderText="העבר לגורם נוסף">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddFactor" DataSource=<%# dtAuthorizationJobs%>
                                                     DataTextField="Teur_Tafkid_Measher" DataValueField="Kod_Tafkid_Measher"
                                                     SelectedValue=<%# Eval("kod_tafkid_measher_nosaf")%>
                                                     OnSelectedIndexChanged="ddFactor_SelectedIndexChanged" 
                                                     AutoPostBack="true"></asp:DropDownList>
                                             </td>
                                             <td>
                                                <asp:ImageButton ID="imgForwardInfo" runat="server" OnClick="imgForwardInfo_Click" ImageUrl="~/Images/questionbtn.jpg"  
                                                    Visible=<%# !String.IsNullOrEmpty(Eval("mispar_ishi_nosaf").ToString()) || !String.IsNullOrEmpty(Eval("mispar_ishi_makor").ToString())%> >
                                                </asp:ImageButton>
                                                <asp:HiddenField runat="server" ID="hdForwardNum" Value=<%# Eval("mispar_ishi_nosaf").ToString() %> />
                                                <asp:HiddenField runat="server" ID="hdForwardName" Value=<%# Eval("SHEM_NOSAF").ToString() %> />
                                                <asp:HiddenField runat="server" ID="hdForwardStatus" Value=<%# Eval("Status_Ishur_nosaf").ToString() %> />
                                                <asp:HiddenField runat="server" ID="hdForwardRemark" Value=<%# Eval("heara_nosaf").ToString() %> />
                                                <asp:HiddenField runat="server" ID="hdForwardDate" Value=<%# Eval("taarich_ishur_nosaf","{0:dd/MM/yyyy}").ToString() %> />
                                                
                                                <asp:HiddenField runat="server" ID="hdOrigNum" Value=<%# Eval("mispar_ishi_makor").ToString() %> />
                                                <asp:HiddenField runat="server" ID="hdOrigName" Value=<%# Eval("SHEM_MAKOR").ToString() %> />
                                                <asp:HiddenField runat="server" ID="hdOrigStatus" Value=<%# Eval("Status_Ishur_makor").ToString() %> />
                                                <asp:HiddenField runat="server" ID="hdOrigRemark" Value=<%# Eval("heara_makor").ToString() %> />
                                                <asp:HiddenField runat="server" ID="hdOrigDate" Value=<%# Eval("taarich_ishur_makor","{0:dd/MM/yyyy}").ToString() %> />

                                            </td>
                                        </tr>
                                    </table>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="הערות">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkRemarks" runat="server" Text="..."  OnClick="lnkRemarks_Click" 
                                        CssClass=<%#!String.IsNullOrEmpty(Eval("Heara").ToString().Trim())?"Remarks":"" %>></asp:LinkButton>
                                    <asp:HiddenField runat="server" ID="hdRemarks" Value=<%# Eval("Heara")%> />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="hdCode" runat="server" Text=<%# Eval("kod_ishur") %> />
                                    <asp:Label ID="hdLevel" runat="server" Text=<%# Eval("rama")%> />
                                    <asp:Label ID="hdStatus" runat="server" Text=<%# Eval("kod_status_ishur") %> />
                                    <asp:Label ID="hdNextLevelStatus" runat="server" Text=<%# Eval("Next_level_status")%> />
                                    <asp:Label ID="hdBakashaID" runat="server" Text=<%# Eval("Bakasha_ID")%> />
                                    <asp:Label ID="hdChanged" runat="server"></asp:Label>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle CssClass="GridAltRow"  />
                        <RowStyle CssClass="GridRow"   />
                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center"  />                          
                        <EmptyDataRowStyle CssClass="GridEmptyData" height="20px" Wrap="False"/>                                                    
                        <SelectedRowStyle CssClass="SelectedGridRowNoIndent" />
                        <PagerTemplate>
                             <kds:GridViewPager runat="server" ID="ucGridPager" />
                        </PagerTemplate>
                    </asp:GridView>
                    
                    <div id="dvRemarks" style='display:none;'>
                        <asp:TextBox runat="server" ID="txtEditRemarks" CssClass="textRtl" 
                        TextMode="MultiLine" MaxLength="100" />
                            <div style="text-align:center">
                                <asp:Button runat="server" ID="btnTmp"  
                                    CssClass="ImgButtonSearch" Text="אישור" OnClientClick="closeEditRemarks(); return true;" />
                                <input type="button" class="ImgButtonSearch" value="סגור" onclick="closeRemarksWithoutSave();" />
                                    
                            </div>
                      </div>
                      
                       <div id="dvForwardInfo" style='display:none;'>
                            <table>
                                <tr>
                                    <td class="bold">
                                        <asp:Label runat="server" ID="lblForwardTitle" ></asp:Label>
                                    
                                    </td>
                                    <td>
                                        <asp:Label ID="lblForwadName" runat="server"></asp:Label>
                                    </td>
                                    <td align="left"><img src="../../Images/x.png" alt="close" style="cursor:pointer;width:25xp;height:25px;" 
                                        onclick="closeRemarksWithoutSave();" /> </td>
                                </tr>
                                <%--<tr>
                                    <td></td>
                                    <td colspan="2"><asp:Label ID="lblForwardNum" runat="server"></asp:Label></td>
                                </tr>--%>
                                <tr>
                                    <td class="bold">סטטוס:</td>
                                    <td colspan="2"><asp:Label ID="lblForwardStatus" runat="server"></asp:Label></td>
                                </tr>
                                 <tr>
                                    <td class="bold">הערה:</td>
                                    <td colspan="2"><asp:Label ID="lblForwardRemark" runat="server" ForeColor="Blue"></asp:Label></td>
                                </tr>
                                 <tr>
                                    <td class="bold">תאריך פתיחה:</td>
                                    <td colspan="2"><asp:Label ID="lblForwardDate" runat="server" ></asp:Label></td>
                                </tr>
                            </table>
                            
                            <%--<div style="text-align:center">
                                
                                <input type="button" class="ImgButtonSearch" value="סגור" onclick="closeRemarksWithoutSave();" />
                                    
                            </div>--%>
                      </div>
                      
                 </ContentTemplate>
                 <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSubmit" />    
                    <asp:AsyncPostBackTrigger ControlID="btnExecute" />    
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                    <asp:AsyncPostBackTrigger ControlID="btnOK" />
                    <asp:AsyncPostBackTrigger ControlID="btnSaveRemarks" />
                </Triggers> 
                
                
             </asp:UpdatePanel>      
        </td>     
    </tr> 
   
    <tr>
        <td>
             <asp:UpdatePanel ID="udSubmitButtons" runat="server" RenderMode="Inline" UpdateMode="Conditional" >
             <ContentTemplate>
                 <asp:Button Text="בצע" ID="btnSubmit" runat="server" 
                                    CssClass ="ImgButtonSearch"  />   
             </ContentTemplate>
             <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnExecute" />
             </Triggers>
             </asp:UpdatePanel>
        </td>
    </tr>          
    <tr>
        <td>
            <asp:UpdatePanel ID="upErrors" runat="server" RenderMode="Block" UpdateMode="Conditional" >
                <ContentTemplate>
                     <cc1:CollapsiblePanelExtender ID="peErrors" runat="Server" 
                            TargetControlID="pnlErrorDetails"
                            ExpandControlID="ErrorTitlePanel" 
                            CollapseControlID="ErrorTitlePanel" 
                            Collapsed="True"
                            TextLabelID="Label3" 
                            ExpandedText="" 
                            CollapsedText=""
                            ImageControlID="Image2" 
                            ExpandedImage="../../images/collapse_blue.jpg" 
                            CollapsedImage="../../images/expand_blue.jpg"
                            SuppressPostBack="true" 
                            ExpandDirection="Vertical" 
                            ScrollContents="true" ExpandedSize="100" >
                        </cc1:CollapsiblePanelExtender>    
                 <asp:Panel ID="ErrorTitlePanel" runat="server" > 
                       <asp:Image ID="Image2" runat="server" ImageUrl="~/images/expand_blue.jpg"/>&nbsp;&nbsp;
                       <asp:Label ID="Label3" runat="server" ForeColor="Red" CssClass="link" Font-Underline="true">שגיאות</asp:Label>
                </asp:Panel>
                 <asp:Panel runat="server" ID="pnlErrorDetails"  Height="0px" style="overflow:hidden;" >
                     <asp:BulletedList ID="lstErrors" runat="server" ForeColor="Red">
                     </asp:BulletedList>
                </asp:Panel>
                </ContentTemplate>
                 <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSubmit" />    
                    <asp:AsyncPostBackTrigger ControlID="btnExecute" />    
                </Triggers> 
            </asp:UpdatePanel>
        </td>
    </tr>
   </table>
  <asp:Button runat="server" Text="ttt" ID="btnSaveRemarks"  CssClass="Hidden"  OnClick="btnSaveRemarks_Click" />
  
      
   <script language="javascript" type="text/javascript">
   function openEditRemarks(){
       $('#dvRemarks').modal({opacity : 0, persist: true});
   }
  
   function showForwardInfo(){
        $('#dvForwardInfo').modal({opacity : 0, persist: true});
   }
  
   function closeRemarksWithoutSave(){
        $.modal.close();
   }
   function closeEditRemarks(){
        setTimeout('saveRemark()',500);
        $.modal.close();
   }
   function saveRemark(){
        $get('ctl00_KdsContent_btnSaveRemarks').click();
   }
  
   function closeModal(){
        $.modal.close();
   }
   
    function load()
    {
        window.moveTo(0,0);
        window.resizeTo(screen.availWidth,screen.availHeight);
       
       SetTextBox();
    }
    function SetTextBox()
    {
        var rdo = document.getElementById("ctl00_KdsContent_rdoId");
        if (rdo.checked)
        {           
         document.getElementById("ctl00_KdsContent_txtId").disabled=false;           
         document.getElementById("ctl00_KdsContent_txtName").disabled=true;
        }
        else{         
            document.getElementById("ctl00_KdsContent_txtName").disabled=false;           
            document.getElementById("ctl00_KdsContent_txtId").disabled=true;
        }
    }
    
 
//    function onClientHiddenHandler_getID(sender, eventArgs)
//    {
//     GetOvedName(document.getElementById("ctl00_KdsContent_txtId"));
//    }     
//    
//    function onClientHiddenHandler_getName(sender, eventArgs)
//    {
//     var iMisparIshi, iPos;
//     var sOvedName=document.getElementById("ctl00_KdsContent_txtName").value;  
//     if (sOvedName!='')
//      {  
//         iPos = sOvedName.indexOf('(');
//         if (iPos==-1)
//         {           

//         }
//         else{
//            iMisparIshi = sOvedName.substr(iPos+1, sOvedName.length-iPos-2);
//            document.getElementById("ctl00_KdsContent_txtId").value=iMisparIshi;
//            document.getElementById("ctl00_KdsContent_txtName").value=sOvedName.substr(0,iPos-1);
//            }
//       }     
//    }
//      
//     function GetOvedMisparIshiByName()
//     {
//      GetOvedMisparIshi(document.getElementById("ctl00_KdsContent_txtName"));
//     } 
   
    function GetOvedMisparSucc(result)
    {
        if (result==''){
            alert('שם לא נמצא');                                    
            document.getElementById("ctl00_KdsContent_txtName").value='';
            document.getElementById("ctl00_KdsContent_txtName").select();
        }
        else{
            document.getElementById("ctl00_KdsContent_txtId").value=result;
        }
    }
    
    function GetOvedNameSucceeded(result)
    {
         if (result==''){
            alert('מספר אישי לא קיים');                        
            document.getElementById("ctl00_KdsContent_txtId").select();
         }
         else{
             document.getElementById("ctl00_KdsContent_txtName").value = result;           
         }
    }
    
    function OpenExtraHoursApproval(url){
        //alert(url);
        var openWin=window.showModalDialog(url,'bakasha','dialogHeight:520px;dialogWidth:470px;status:no;scroll:no;');
        if(openWin==true)
        {
            document.getElementById('<%=btnExecute.ClientID%>').click();
        }
    }

    function continue_click() {
        SetTextBox();
    }
   </script>
</asp:Content>

