<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NetuneyOvedModal.aspx.cs" Inherits="Modules_Ovdim_NetuneyOvedModal" %>
<%@ Register TagPrefix="uc"   Src="~/Modules/UserControl/MeafyeneyBizua.ascx" TagName="ucMeafyenim"  %>
<%@ Register TagPrefix="uc"   Src="~/Modules/UserControl/StatusOved.ascx" TagName="ucStatus"  %>
<%@ Register TagPrefix="uc"   Src="~/Modules/UserControl/PirteyOved.ascx" TagName="ucDetails"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="head1">
<base target ="_self" />
 <link id="Link1" runat="server" href="~/StyleSheet.css" type="text/css" rel="stylesheet" />
      <script src="../../Js/GeneralFunction.js" type="text/javascript"></script>

    <title>נתוני עובד</title>
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

</head>
<body>
    <form id="form1" runat="server">
      <asp:ScriptManager  runat="server"  id="ScriptManagerKds" EnablePartialRendering="true"   >        
       <Services >
            <asp:ServiceReference  Path="~/Modules/WebServices/wsGeneral.asmx" />
       </Services>
   </asp:ScriptManager>
    <div>
    <br />
    <fieldset  > 
    <table style="margin-top:4px">
        <tr>
             <td class="TitleLable">מספר אישי: </td>
            <td dir="rtl">
             <asp:Label ID="lblEmpId" runat="server"></asp:Label>
            </td>
            <td style="width:10px"></td>
            <td class="TitleLable"> שם: </td>
            <td style="width:200px">
                  <asp:Label ID="lblName" runat="server"></asp:Label>
                </td>
                <td  class="InternalLabel">חודש:</td>
            <td> <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Block">
                  <ContentTemplate> 
                     <asp:DropDownList id="ddlMonth" runat="server"  Width="100px" DataTextField="month_year" DataValueField="month_year" >
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
                <asp:UpdatePanel ID="upBtnShow" runat="server" RenderMode="Inline">
                  <ContentTemplate> 
                        <asp:button ID="btnShow" runat="server" text="הצג" CssClass="ImgButtonSearch" onclick="btnShow_Click"  />
                    <input type="hidden" runat="server" id="txtElement" /> 
  
                 </ContentTemplate>
              </asp:UpdatePanel> 
              
            
            </td>
        </tr>                
    </table>  
 </fieldset>
 <br />
       <br />
      
<asp:UpdatePanel ID="UpdatePanel6" runat="server" RenderMode="Inline" UpdateMode="Conditional">
     <ContentTemplate> 
       <input type="hidden" runat="server" id="Hidden1" /> 
        
        <div id="divNetunim" runat="server">
  
            <cc1:TabContainer ID="TabContainer1" AutoPostBack="true"  ActiveTabIndex="0" runat="server" Font-Size="14px"   EnableViewState="true"
                CssClass="ajax__myTab" OnActiveTabChanged="TabContainer1_ActiveTabChanged"  >
                <cc1:TabPanel ID="pMeafyeneyBitzua"  HeaderText="מאפייני ביצוע" runat="server"  OnClientClick="ShowRdoCheck" >
                 <HeaderTemplate>
                        <label ID="lblMeafyeneyBizua"  runat="server"  style="cursor:pointer">מאפייני ביצוע</label> 
                    </HeaderTemplate>
                  <ContentTemplate>
                        <uc:ucMeafyenim runat="server" id="ucMeafyeneyBitzua"></uc:ucMeafyenim>                 
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel  ID="pPirteyOved" runat="server"  HeaderText="פרטי העובד" OnClientClick="HiddenRdoCheck" >
                 <HeaderTemplate>
                        <label ID="lblPirteyOved"  runat="server"  style="cursor:pointer">פרטי העובד</label> 
                    </HeaderTemplate>
                    <ContentTemplate>
                        <uc:ucDetails runat="server" ID="ucPirteyOved" />            
                    </ContentTemplate>
                </cc1:TabPanel>
                  <cc1:TabPanel ID="pStatusOved" runat="server"  HeaderText="סטטוס העובד" OnClientClick="HiddenRdoCheck">
                    <HeaderTemplate>
                        <label ID="lblStatus"  runat="server"  style="cursor:pointer">סטטוס העובד</label> 
                    </HeaderTemplate>
                     <ContentTemplate>
                           <uc:ucStatus runat="server" id="ucStatusOved"></uc:ucStatus>
                     </ContentTemplate>
                  </cc1:TabPanel>
            </cc1:TabContainer>
      </div>
   
   </ContentTemplate>
   <Triggers>
      <asp:AsyncPostBackTrigger ControlID="btnShow" />
     <asp:AsyncPostBackTrigger ControlID="ddlMonth" />
   </Triggers>
</asp:UpdatePanel> 

  <script src="../../Js/NetuneyOved.js" type="text/javascript" language="javascript"></script>
        
    </div>
    </form>
 </body>
</html>
