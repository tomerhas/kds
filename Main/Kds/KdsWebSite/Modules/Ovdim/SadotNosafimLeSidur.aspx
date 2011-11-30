<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SadotNosafimLeSidur.aspx.cs" Inherits="Modules_Ovdim_SadotNosafimLeSidur" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>שדות נוספים לסידור</title>
    <script src='../../js/jquery.js' type='text/javascript'></script>
      <script src="../../Js/WorkCard.js" type="text/javascript"></script>
    <link id="Link1" runat="server" href="~/StyleSheet.css" type="text/css" rel="stylesheet" />
   <base target="_self" />  
    <script type="text/javascript">
      </script>
</head>
<body style="margin:0px;dir="ltr" onkeydown="return ChangeKeyCode(event.keyCode);">
<script type="text/javascript">
     function ChangeKeyCode(keyCode) {
         if (keyCode == 107) {
             event.keyCode = 9;
             return event.keyCode;
         }
         else if (keyCode == 110) {
            // if (document.getElementById("btnShow").disabled == false)
             document.getElementById("btnShow").focus();
           
         }
     }
     function onSadeFocus(object) {
         document.getElementById(object.id).style.border = "1px solid black";
     }
  </script>
    <form id="form1" runat="server" class="WorkCardRechivim" style="width:650px; height:350px">
         <asp:ScriptManager  runat="server"  id="ScriptManagerKds" EnablePartialRendering="true" EnablePageMethods="true">        
         </asp:ScriptManager>               
           <asp:UpdateProgress  runat="server" id="UpdateProgress1" DisplayAfter="0" >
                    <ProgressTemplate>
                        <div id="divProgress" class="Progress"  style="text-align:right;position:absolute;left:52%;top:48%; z-index:1000"   >
                              <asp:Image ID="Image2" runat="server" ImageUrl="../../Images/Eggedprogress.gif" style="width: 100px; height: 100px" />מעדכן...
                        </div>        
                    </ProgressTemplate>
           </asp:UpdateProgress>     
    
    <div id="divMeafyenim" runat="server" style="width:650px; height:350px">    
          <asp:Button ID="btnErrors" runat="server" CssClass="ImgButtonUpdate" CausesValidation="false"
            Style="display: none;" />
        <cc1:ModalPopupExtender ID="MPEErrors" DropShadow="false" CancelControlID="btnErrClose" BehaviorID="bMpeErr"
            X="10" Y="10" PopupControlID="paErrorMessage" TargetControlID="btnErrors" runat="server">
        </cc1:ModalPopupExtender>
        <asp:Panel runat="server" Style="display: none" ID="paErrorMessage" CssClass="WorkCardPanelMessageError" Width="520px" height="300px">            
            <table border="0" width="520px" >
              <tr style="height:40px">
                  <td class="WorkCardErrMsg" width="520px"><asp:Label ID="lblErrors" runat="server" >פירוט שגיאה</asp:Label></td>
              </tr>
              <tr style="height: 40px">
                 <td class="WorkCardPanelTopTableMessage" id="Td1" align="right" width="520px"><asp:Label ID="Label8" runat="server">תיאור השגיאה</asp:Label></td>                      
              </tr>
              <tr style="height: 170px" valign="top">                    
                   <td id="tbErr" align="right"></td>                      
              </tr>
            </table>
             <table style="height: 31px; width: 520px">
                <tr style="height: 31px" class="WorkCardTopBorder">
                    <td width="500px" align="left" >
                       <asp:Button ID="btnErrClose" runat="server" Text="סגור" CssClass="btnWorkCardCloseWin"
                            Width="75px" Height="30px"  CausesValidation="false" />
                            <input type="hidden" runat="server" id="hErrKey" width="0px" />
                    </td>                                                        
                </tr>              
            </table>           
        </asp:Panel> 
      <asp:UpdatePanel  ID="upSadot" runat="server"  UpdateMode="Always">
     <ContentTemplate>                   
       <table id="tblMeafyenim" runat="server" width="650px; height:300px" >
            <tr class="WorkCardRechivimGridHeader" style="font:bold"><td colspan="3"> שדות נוספים לסידור</td></tr>
            <tr class="WorkCardRechivim"><td colspan="3"><asp:Label ID="lblPizulSidur" runat="server" Font-Bold="true" Visible="false"  Text="זהו סידור ויזה. שעת גמר הסידור היא אחרי 06:00, שקול לפצל את הסידור."></asp:Label></td></tr>
            <tr class="WorkCardRechivim">       
                <td rowspan="2" width="10px"></td>   
                <td  id="tdPirteySidur" runat="server">
                    <fieldset id="fieldsetSidur" width="650px" runat="server"> <legend  style="background-color:White" >שדות ברמת סידור: </legend> 
                    <br />  
                    <asp:Label ID="lblEinSadotLesidur" runat="server" ForeColor="Red"  Visible="false" Text="לא קיימים שדות נוספים לעדכון ברמת סידור!"></asp:Label>
                    </fieldset>
                    <td rowspan="2" width="10px"></td> 
                </td>
            </tr>
            <tr class="WorkCardRechivim">
                <td id="tdFilter" runat="server">
                    <fieldset id="fieldsetPeilut" runat="server" width="650px"> <legend  style="background-color:White" >שדות ברמת פעילות: </legend> 
                        <br />    
                        <asp:Label ID="lblEinSadotLepeilut" runat="server" ForeColor="Red"  Visible="false" Text="לא קיימים שדות נוספים לעדכון ברמת פעילות!"></asp:Label>  
                    </fieldset>
                </td>
           </tr>
       </table>        
       <table width="650px" class="WorkCardRechivim">
              <tr>
                 <td width="10px"></td>  
                     <td><input type="button" id="btnClose" runat="server" class="btnWorkCardCloseWin" value="סגור" style="width:80px; height:32px" onfocus="onSadeFocus(this);"   onclick="window.close();" /> </td>
                     <td align="left"><asp:Button ID="btnShow" runat="server" Text="שמור"  style="width:80px; height:32px" OnClick="btnShmor_OnClick"  onfocus="onSadeFocus(this);"  CssClass="btnWorkCardCloseWin" /></td>
                     <td rowspan="2" width="10px"></td>  
               </tr>
           </table> 
        </ContentTemplate>
       </asp:UpdatePanel>
        <input type="hidden" id="clnDate" name="clnDate"  runat="server" />
        <input type="hidden" id="MisSidur" name="MisSidur"  runat="server" />
        <input type="hidden" id="txtId" name="txtId"  runat="server" />
        <input type="hidden" id="ShatHatchala" name="ShatHatchala"  runat="server" />
        <input type="hidden" id="ShatGmar" name="ShatGmar"  runat="server" />
        <input type="hidden" id="Params" name="Params"  runat="server" /> 
        <input type="hidden" id="HiddenSdotPeilutBeramatSidur" name="HiddenSdotPeilutBeramatSidur"  runat="server" /> 
      <input type="hidden" id="txtSidurDate" name="SidurDate"  runat="server" />
    </div>
    
    </form>
</body>
</html>
