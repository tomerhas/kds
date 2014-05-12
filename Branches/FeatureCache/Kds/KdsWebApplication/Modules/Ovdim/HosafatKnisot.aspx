<%@ Page Language="C#" AutoEventWireup="true" Inherits="Modules_Ovdim_HosafatKnisot" Codebehind="HosafatKnisot.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>הוספת כניסות</title>
      <script src='../../js/jquery.js' type='text/javascript'></script>
   <link id="Link1" runat="server" href="~/StyleSheet.css" type="text/css" rel="stylesheet" />
   <base target="_self" />
 </head>
<body  dir="rtl" class="WorkCardRechivim" onkeydown="if (event.keyCode==107) {event.keyCode=9; return event.keyCode }">
    <form id="form1" runat="server">
    <asp:ScriptManager  runat="server"  id="ScriptManagerKds" EnablePartialRendering="true" EnablePageMethods="true">        
      <Scripts >
        <asp:ScriptReference Path="~/Js/String.js" />
        <asp:ScriptReference Path="~/Js/GeneralFunction.js" />
        <asp:ScriptReference Path="~/Js/jquery.js" />
       </Scripts>
      </asp:ScriptManager>
      <table style="width:100%" cellpadding="0" cellspacing="0">
        <tr class="WorkCardKnisotHeader" ><td id="tdHeader" runat="server" style="height:22px"> </td></tr>
      </table>
      <table  style="width:100%; height:325px" cellpadding="2" cellspacing="2" >
        <tr>
        <td align="center"> 
        <br />                  
        <asp:UpdatePanel ID="upTblKnisot" runat="server" UpdateMode="Always" >
            <ContentTemplate>  
            <asp:Panel ID="pnlgrdKnisot" height="292px" width="542px" dir="ltr"  runat="server" >   
            <div dir="rtl" style="overflow:auto" >
            <asp:GridView ID="grdKnisot" runat="server" GridLines="None"
                    AutoGenerateColumns="False"  width="520px" 
                    ShowHeader="true"    
                    HeaderStyle-CssClass="WorkCardRechivimGridHeader"
                    OnRowDataBound="grdKnisot_RowDataBound">
                <Columns>                                  
                    <asp:BoundField DataField="Siduri" HeaderText="מספר" ItemStyle-CssClass="WorkCardRechivimGridRow"  />                                  
                    <asp:BoundField DataField="MokedTchilaName" HeaderText="תאור"  ItemStyle-CssClass="WorkCardRechivimGridRow"   />                                  
                    <asp:BoundField DataField="SugKnisa" HeaderText="סוג" ItemStyle-CssClass="WorkCardRechivimGridRow" ItemStyle-Width="70px"  />                          
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="WorkCardRechivimGridRow"  HeaderText="דקות בפועל" >
                        <ItemTemplate>
                            <asp:TextBox ID="txtDakotBafoal" runat="server" Width="50px" CssClass="WCPilutTxt"></asp:TextBox>
                            <asp:CustomValidator runat="server" id="vldDakot" ControlToValidate="txtDakotBafoal" ErrorMessage=""   Display="None"   ></asp:CustomValidator>
                            <cc1:ValidatorCalloutExtender runat="server" ID="exvdakot" BehaviorID="vldExvldDakot"  TargetControlID="vldDakot" Width="200px" PopupPosition="Right"></cc1:ValidatorCalloutExtender>  
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="WorkCardRechivimGridRow" HeaderStyle-Wrap="true" >                                
                        <HeaderTemplate>
                            <table align="center">
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Label ID="Label1" runat="server" Text="הוסף פעילות"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <a id="lbSamenHakol" href="#" runat="server" onclick="SamenHakol_OnClick()">סמן הכל</a>/
                                        <a id="lbNake" href="#" runat="server"  onclick="NakeHakol_OnClick()">נקה</a>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbHosef" AutoPostBack="false" runat="server" />
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SugKnisa"  ItemStyle-CssClass="WorkCardRechivimGridRow"  />
                        <asp:BoundField DataField="YeshuvName"  ItemStyle-CssClass="WorkCardRechivimGridRow"   />                                  
                    </Columns>
                    <AlternatingRowStyle CssClass="WorkCardRechivimGridRow" />
                    <RowStyle CssClass="WorkCardRechivimGridRow" />
                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
            </asp:GridView></div>
            </asp:Panel>                       
        </ContentTemplate>
    </asp:UpdatePanel>                
       </td>
    </tr>
       </table>
          
       <table style="width:100%">
        <asp:UpdatePanel ID="UPbuttons" runat="server" UpdateMode="Always">
            <ContentTemplate>      
                <table width="100%" >
                    <tr>
                        <td align="left"  style="width:80%">
                            <input type="button" class="btnWorkCardCloseWin" style="width:72px; height:30px" value="סגור" onclick="window.returnValue = ''; window.close();" />
                         </td>
                        <td align="left" style="width:20%">
                        <asp:Button ID="btnUpdateKnisot" runat="server" style="display:none;width:105px; height:30px"   Text="עדכן כניסות"  OnClick="btnUpdate_OnClick"   CssClass="btnWorkCardUpdate" OnClientClick="if (!checkFileds()) return false; else return true;" />
                            </td>
                    </tr>
                 </table>
            </ContentTemplate>
         </asp:UpdatePanel>    
       </table>
       <input type="hidden" id="Params" name="Params"  runat="server" />
    </form>
    <script type="text/javascript" language="javascript">
        var col_HosefPeilut = "<%=HOSEF_PEILUT %>";
        var col_Mispar_Knisa = "<%=MISPAR_KNISA %>";
        var col_Sug_Knisa = "<%=SUG_KNISA %>";
        function SamenHakol_OnClick() {
            var num = document.getElementById("grdKnisot").childNodes.item(0).childNodes.length;
            for (var i = 1; i < num; i++) {
                document.getElementById("grdKnisot").childNodes.item(0).childNodes.item(i).childNodes.item(col_HosefPeilut).childNodes.item(0).checked = true;
            }
        }
        function NakeHakol_OnClick() {
            var num = document.getElementById("grdKnisot").childNodes.item(0).childNodes.length;
            for (var i = 1; i < num; i++) {
                if (document.getElementById("grdKnisot").childNodes.item(0).childNodes.item(i).childNodes.item(col_Sug_Knisa).innerHTML!="2")
                document.getElementById("grdKnisot").childNodes.item(0).childNodes.item(i).childNodes.item(col_HosefPeilut).childNodes.item(0).checked = false;
            }
        }

        /**************** דקות *******************/
        function onchange_txtDakot(row) {
            var vld = document.getElementById(row.id + "_vldDakot");
           var dakotBafoal = document.getElementById(row.id + "_txtDakotBafoal").value;
            var Param98 = document.getElementById("Params").attributes("Param98").value;
            //כניסה (mispar_knisa>0) -  יש לאפשר להקליד ערך רק עבור כניסות מסוג לפי צורך (SugKnisa= 3), ערך בין 0  ולא גדול מהערך בפרמטר 98 (מכסימום זמן כניסה לישוב).
            if (trim(dakotBafoal) == "")
                dakotBafoal = 0;
            if (IsNumeric(dakotBafoal)) {
                if (Number(dakotBafoal) > Number(Param98)) {
                    if (Number(row.childNodes.item(col_Mispar_Knisa).innerText) > 0)
                        vld.errormessage = "ערך דקות בפועל לא יכול לחרוג ממקסימום " + Param98 + "  דקות ";
                   
                    ShowValidatorCalloutExtender(row.id + "_vldExvldDakot");
                    return false;
                }

            }
            else {
                vld.errormessage = "יש להזין ערך מספרי חיובי ושלם בלבד";
                ShowValidatorCalloutExtender(row.id + "_vldExvldDakot");
                return false;
            }
            return true;
        }

        function ShowValidatorCalloutExtender(sBehaviorId) {
            $find(sBehaviorId)._ensureCallout();
            $find(sBehaviorId).show(true);
        }

        function checkFileds() {
            var numRows = document.getElementById("grdKnisot").childNodes.item(0).childNodes.length; 
            var row; 
            var is_valid = true;
            var iCountLinesChk = 0;
            for (var i = 1; i < numRows; i++) {
                row = document.getElementById("grdKnisot").childNodes.item(0).childNodes.item(i);
                if (document.getElementById("grdKnisot").childNodes.item(0).childNodes.item(i).childNodes.item(col_HosefPeilut).childNodes.item(0).checked != false) {
                    iCountLinesChk += 1;
                   
                    is_valid = onchange_txtDakot(row);
                    if (!is_valid)
                        break;
                }
            }
            if (iCountLinesChk == 0) {
                alert("!לא נבחרו כניסות להוספה ");
                return false;
            }
            else {
                if (is_valid)
                    return true;
                else return false;
            }

        }
    </script>
</body>
</html>
