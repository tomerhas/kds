<%@ Page Language="C#"  AutoEventWireup="true" Inherits="ErrorPage" Codebehind="ErrorPage.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error Page</title>
    <style type="text/css">
        .message
        {
            padding-right:200px;
        }
        .collapsePanelHeader	
        {
            height:20px;
	        width:565px;		
	        float:left;
	        padding:5px; 
	        font-family:Arial;
            background-repeat:no-repeat;
            cursor: pointer; 
            color: Gray;
	        font-weight:bold;
        }
        .targetPanel
        {
            font-family:Arial;
        }
    </style>
</head>
<body  dir="rtl">
    <form id="form1" runat="server">
    <asp:ScriptManager  runat="server"  id="ScriptManagerKds" EnablePartialRendering="true" >        
    </asp:ScriptManager>
    <table width="780px" cellpadding="0" cellspacing="0" border="0">                              
        <tr>
            <td>
                <asp:Image runat="server" ID="imgTopBanner" ImageUrl="~/Images/head.jpg"  ImageAlign="Top" />
            </td>    
        </tr>           
     </table>
    <div>
        <table style="height:480px;width:980px;border:solid 1px;margin-top:6px;">
            <tr>
                <td align="center" >
                    <div style="text-align:right;font-family:Arial; color:White;width:581px;height:185px;background-image:url(<%=GetImagePath()%>images/error.jpg);background-repeat:no-repeat;">
                       <span id="error" class="message" style="display:<%= ErrorDisplay%>">
                            <br /><br />
                            <b>משתמש יקר,</b>
                            <br />
                            לא ניתן לצפות בדף זה עקב תקלה במערכת.
                            <br />
                            אנא נסה שנית מאוחר יותר. במידה והתקלה אינה 
                            <br />
                            נפתרת אנא פנה לתמיכה הטכנית.
                        </span>
                       <span id="security" class="message" style="display:<%= SecurityDisplay%>">
                            <br /><br />
                            <b>
                                אינך מורשה לצפות בדף זה.
                            </b>
                            <br /><br />
                            לקבלת הרשאות אנא פנה למנהל מערכת
                        </span>
                       <span id="resource" class="message" style="display:<%= NoResourceDisplay%>">
                             <br /><br />
                             <b>
                                הדף המבוקש לא קיים במערכת.
                             </b>
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="left" dir="ltr" style="vertical-align:top;">
                    <div style="padding-left:200px;display:<%=DetailsDisplay%>">
                        <ajaxToolkit:CollapsiblePanelExtender ID="cpe" runat="Server" 
                            TargetControlID="ContentPanel"
                            ExpandControlID="TitlePanel" 
                            CollapseControlID="TitlePanel" 
                            Collapsed="True"
                            TextLabelID="Label1" 
                            ExpandedText="Hide Details..." 
                            CollapsedText="Show Details..."
                            ImageControlID="Image1" 
                            ExpandedImage="~/images/collapse_blue.jpg" 
                            CollapsedImage="~/images/expand_blue.jpg"
                            SuppressPostBack="true" 
                            ExpandDirection="Vertical" 
                            ScrollContents="true" ExpandedSize="200" >
                        </ajaxToolkit:CollapsiblePanelExtender>
                        <asp:Panel ID="TitlePanel" runat="server" CssClass="collapsePanelHeader"> 
                               <asp:Image ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg"/>&nbsp;&nbsp;
                               <asp:Label ID="Label1" runat="server" Font-Underline="true">Show Details...</asp:Label>
                        </asp:Panel>
                        <asp:Panel ID="ContentPanel" runat="server" CssClass="targetPanel"  Width="565">
                            <asp:Label ID="lblDetails" runat="server"></asp:Label>
                        </asp:Panel>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
