<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="NetuneyOved.aspx.cs" Inherits="Modules_Ovdim_NetuneyOved" %>
<%@ Register TagPrefix="uc"   Src="~/Modules/UserControl/MeafyeneyBizua.ascx" TagName="ucMeafyenim"  %>
<%@ Register TagPrefix="uc"   Src="~/Modules/UserControl/StatusOved.ascx" TagName="ucStatus"  %>
<%@ Register TagPrefix="uc"   Src="~/Modules/UserControl/PirteyOved.ascx" TagName="ucDetails"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">      
    
.ajax__myTab .ajax__tab_outer 
{  
	 font-family:arial;
   font-size:17px;
   font-weight:bolder;
   text-align:center;
   width:169px;
   height:31px;
   background:url("../../Images/grey.jpg");
   color:White;
    background-repeat:no-repeat;
    
}
     
  .ajax__myTab .ajax__tab_active .ajax__tab_outer
   {      
   	background:url("../../Images/white.jpg");
    color:Gray;
     background-repeat:no-repeat;
     background-position:right;
     margin-bottom:0px;
    width:169px;
    height:31px;
  } 
    
  .ajax__myTab .ajax__tab_body     
  {
    background-color: #F3F3F3;
    border-color:#BDBDBD;
    border-style: solid;
    border-width: 1px;
    width:950px;
    padding-right:7px;
}      

.ajax__myTab .ajax__tab_inner .ajax__tab_tab 
{
   vertical-align:bottom;
   padding-top:3px;
   text-align:center;   
}

.ajax__myTab .ajax__tab_hover .ajax__tab_outer
{ 
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
   <fieldset class="FilterFieldSet" style="height:50px;width:940px" > 
       <legend> בחירת עובד לתצוגה </legend>      
    <table style="margin-top:4px">
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
                            <asp:TextBox ID="txtEmpId" runat="server" AutoComplete="Off" dir="rtl" onfocus="this.select();" onchange="GetOvedNameById();"
                                Width="70px"   EnableViewState="true" ></asp:TextBox>                            
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
              <td class="InternalLabel" style="width:30px">
                   <asp:UpdatePanel ID="UpdatePanel5" runat="server" RenderMode="Inline">
                       <ContentTemplate> 
                       
                         <asp:RadioButton runat="server" ID="rdoName" Width="49px" EnableViewState="true" 
                               GroupName="grpSearch" Text="שם:" > </asp:RadioButton>
                    </ContentTemplate>
                  </asp:UpdatePanel> 
            </td>   
            <td class="style2">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                       <ContentTemplate> 
                            <asp:TextBox ID="txtName" runat="server" onfocus="this.select();" onchange="GetOvedIdByName();"   AutoComplete="Off" style="width:160px" EnableViewState="true" ></asp:TextBox>
                          
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
            <td class="style1"></td>
            <td  class="InternalLabel">חודש:</td>
            <td><asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline">
                  <ContentTemplate> 
                     <asp:DropDownList id="ddlMonth" runat="server"  Width="80px" DataTextField="month_year" DataValueField="month_year" >
                            </asp:DropDownList>
                </ContentTemplate>
             </asp:UpdatePanel> 
            </td>
            <td style="width:20px"></td>
            <td id="tdchkNoDefault"><asp:RadioButton  ID="chkNoDefault" runat="server" GroupName="chkDefault" Checked="true" Text="ללא ב.מ. מערכת"/></td>    
           <td style="width:10px"></td> 
             <td id="tdchkWithDefault"><asp:RadioButton  ID="chkWithDefault" GroupName="chkDefault" runat="server" Text="כולל ב.מ. מערכת"/></td>    
                <td style="width:40px"></td> 
                 <td> 
                <asp:UpdatePanel ID="upBtnShow" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                  <ContentTemplate> 
                        <asp:button ID="btnShow" runat="server" text="הצג" CssClass="ImgButtonSearch" onclick="btnShow_Click" OnClientClick="if (!CheckEmployeeId()) return false;" />
                          <asp:Button ID="btnHidden" runat="server" OnClick="btnHidden_OnClick"  />
                </ContentTemplate>
                 <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="txtEmpId" />
                      <asp:AsyncPostBackTrigger ControlID="txtName" />
                      <asp:AsyncPostBackTrigger ControlID="ddlMonth" />
                </Triggers>
              </asp:UpdatePanel> 
             </td>
        </tr>                
    </table>  
 </fieldset>
 <br /> <br />
     
 <asp:UpdatePanel ID="UpdatePanel6" runat="server" RenderMode="Inline" UpdateMode="Conditional">
     <ContentTemplate> 
      
        <div id="divNetunim" runat="server"  style="text-align:right;width:945px;">
             <input type="hidden" runat="server" id="txtElement" /> 
        
            <cc1:TabContainer ID="TabContainer1"  AutoPostBack="true"    runat="server"  
                 Font-Size="14px"   EnableViewState="true"  ActiveTabIndex="0" 
                CssClass="ajax__myTab"  
                 OnActiveTabChanged="TabContainer1_ActiveTabChanged"  
                 OnClientActiveTabChanged="CheckEmployeeId">
                <cc1:TabPanel ID="pMeafyeneyBitzua"  HeaderText="מאפייני ביצוע" runat="server"  OnClientClick="ShowRdoCheck" >
                 <HeaderTemplate>
                        <label ID="lblMeafyeneyBizua"  runat="server"  style="cursor:pointer">מאפייני ביצוע</label> 
                    </HeaderTemplate>
                  <ContentTemplate>
                     <uc:ucMeafyenim runat="server" ID="ucMeafyeneyBitzua" ></uc:ucMeafyenim>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel  ID="pPirteyOved" runat="server"   HeaderText="פרטי העובד" OnClientClick="HiddenRdoCheck" >
                    <HeaderTemplate>
                        <label ID="lblPirteyOved"  runat="server"  style="cursor:pointer">פרטי העובד</label> 
                    </HeaderTemplate>
                    <ContentTemplate>
                      <uc:ucDetails runat="server" ID="ucPirteyOved" > </uc:ucDetails>
                  </ContentTemplate>
                </cc1:TabPanel>
                  <cc1:TabPanel ID="pStatusOved" runat="server"  HeaderText="סטטוס העובד" OnClientClick="HiddenRdoCheck">
                   <HeaderTemplate>
                        <label ID="lblStatus"  runat="server"  style="cursor:pointer">סטטוס העובד</label> 
                    </HeaderTemplate>
                         <ContentTemplate>
                            <uc:ucStatus runat="server" ID="ucStatusOved"></uc:ucStatus>
                        </ContentTemplate>
                  </cc1:TabPanel>
            </cc1:TabContainer>
        
      </div>
   
   </ContentTemplate>
   <Triggers>
      <asp:AsyncPostBackTrigger ControlID="btnShow" />
       <asp:AsyncPostBackTrigger ControlID="btnHidden" />
       <asp:AsyncPostBackTrigger ControlID="txtEmpId" />
       <asp:AsyncPostBackTrigger ControlID="txtName" />
   </Triggers>
</asp:UpdatePanel>  
      <script src="../../Js/NetuneyOved.js" type="text/javascript" language="javascript"></script>
       
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


    function ItemSelectedOved(sender, e) {
        document.getElementById("ctl00_KdsContent_ddlMonth").focus();
    }
    function continue_click() {
       // debugger;
        document.getElementById("ctl00_KdsContent_btnHidden").click();
    } 
   </script>
      
</asp:Content>

