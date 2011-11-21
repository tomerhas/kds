<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZmaniNesiot.aspx.cs" Inherits="Modules_Ovdim_ZmaniNesiot" %>
       <link href="../../StyleSheet.css"type="text/css" rel="stylesheet" />
   <script src="../../Js/GeneralFunction.js"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <base target="_self" />  
    <script type="text/javascript">
        var Page_IsValid;

        function confirmUpdate()
        {
            var answer = confirm("האם ברצונך לעדכן את השינויים שביצעת בזמני נסיעות בפועל ?");
            
            if(!answer) window.close();
            else
            {
                document.getElementById("txtChanged").value  = "0" ;
            //    document.getElementById("txtSwitchClose").value  = "1" ;
                Page_IsValid=true;
                EnablebtnUpdate();                
                document.getElementById("btnUpdate").click();
            }
        }
        
        function EnablebtnUpdate()
        {
            var btnUpdate = document.getElementById("btnUpdate");
            btnUpdate.disabled =!Page_IsValid;
            btnUpdate.className = Page_IsValid? "ImgButtonSearch" : "ImgButtonSearchDisable" ; 
                  
        }

    </script>
</head>

<body style="margin:0px" dir="ltr">
  <form id="ZmaneyNesiot" runat="server" style="width:100%; height:100%">
    <asp:ScriptManager  runat="server"  id="ScriptManagerKds" EnablePartialRendering="true"></asp:ScriptManager>                
      <table style="width:649px; height:100%" align="center" border="0" cellpadding="0" cellspacing="0"  >
        <tr class="WorkCardRechivimGridHeader" >
           <td  colspan="7" align = "center"  >
            <input type="hidden" runat="server" id="txtMisparIshi" /> 
            <input type="hidden" runat="server" id="txtTaarich" /> 
            <input type="hidden" runat="server" id="txtChanged" text= "0"/> 
            <input type="hidden" runat="server" id="txtSwitchClose" text= "0"/> 
            <asp:Label runat="server" ID="lblPageHeader" width="100%" Font-Bold="true" >זמני נסיעות </asp:Label>
           </td>
        </tr>
        <tr class="WorkCardRechivim">
            <td style="text-align:right;width:160px"><asp:Label ID="lbl1" runat="server" Font-Bold="true" Font-Underline="true">זמני נסיעות לפי מאפיין:</asp:Label></td>
            <td  style="text-align:left;width:140px; font-weight:normal">זמן נסיעה לעבודה:</td>
            <td ><asp:TextBox Width="70px" Height="30px" valign="middle" ID="txtZNLavodaMax" runat="server" ReadOnly="true" CssClass="WorkCardPeilutTextBox"></asp:TextBox></td>
            <td style="text-align:right;width:45px">דקות</td> 
           
            <td style="text-align:left;width:140px;font-weight:normal ">זמן נסיעה מהעבודה:</td> 
            <td><asp:TextBox Width="70px" Height="30px" ID="txtZNMavodaMax" runat="server" ReadOnly="true" CssClass="WorkCardPeilutTextBox"></asp:TextBox> </td>  
            <td style="text-align:right;width:40px">דקות</td>     
        </tr>
        <tr class="WorkCardRechivim">
            <td style="text-align:right;width:160px"><asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Underline="true">זמני נסיעות בפועל:</asp:Label></td>
            <td style="text-align:left;width:140px;font-weight:normal">זמן נסיעה לעבודה:</td>
            <td><asp:TextBox Width="70px" Height="30px" ID="txtZNLavoda" runat="server" onChange="setChanged()" CssClass="WorkCardPeilutTextBox"></asp:TextBox></td>
                <asp:CustomValidator ID="CmpValidApprovedInVaadatP_1" runat="server" Display="Dynamic" CssClass="ErrorMessage" ErrorMessage="לא ניתן לאשר ערך הגדול מהערך לפי מאפיין זמן נסיעה לעבודה" ControlToValidate="txtZNLavoda" ClientValidationFunction="CmpValidApprovedInZman"></asp:CustomValidator>
                <asp:CompareValidator ID="CmpValidApprovedInVaadatP_2" runat="server" Display="Dynamic" CssClass="ErrorMessage" ErrorMessage="יש להקליד ערך מספרי בלבד" Type="Integer" Operator="DataTypeCheck" ControlToValidate="txtZNLavoda"></asp:CompareValidator>
                <asp:CompareValidator ID="CmpValidApprovedInVaadatP_3" runat="server" Display="Dynamic" CssClass="ErrorMessage" ErrorMessage="לא ניתן להגדיר ערך שלילי"  Operator="GreaterThanEqual"  ValueToCompare="0"  ControlToValidate="txtZNLavoda"></asp:CompareValidator>
            <td style="text-align:right;width:40px">דקות</td> 
            
            <td style="text-align:left;width:140px;font-weight:normal">זמן נסיעה מהעבודה:</td>
            <td> <asp:TextBox  Width="70px" Height="30px" ID="txtZNMavoda" runat="server" onChange="setChanged()" CssClass="WorkCardPeilutTextBox"></asp:TextBox></td>
            <asp:CustomValidator ID="CustomValidator1" runat="server" Display="Dynamic" CssClass="ErrorMessage" ErrorMessage="לא ניתן לאשר ערך הגדול מהערך לפי מאפיין זמן נסיעה מהעבודה" ControlToValidate="txtZNMavoda" ClientValidationFunction="CmpValidApprovedInZman"></asp:CustomValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" Display="Dynamic" CssClass="ErrorMessage" ErrorMessage="יש להקליד ערך מספרי בלבד" Type="Integer" Operator="DataTypeCheck" ControlToValidate="txtZNMavoda"></asp:CompareValidator>
            <asp:CompareValidator ID="CompareValidator2" runat="server" Display="Dynamic" CssClass="ErrorMessage" ErrorMessage="לא ניתן להגדיר ערך שלילי"  Operator="GreaterThanEqual"  ValueToCompare="0"  ControlToValidate="txtZNMavoda"></asp:CompareValidator>
            <td style="text-align:right;width:40px">דקות</td> 
            
        </tr>
        <tr class="WorkCardRechivim">           
            <td colspan="7" align="center">
                <br />
                <asp:Button Text="עדכן זמן נסיעות" id="btnUpdate" runat="server" Enabled="false"  
                    CssClass="btnWorkCardAddSpecial" Width="120px" 
                    onclick="btnUpdate_Click" />                            
            </td>          
        </tr>
      </table>   
    </form>
</body>
<script type="text/javascript">

var  ReturnValue ;
function CmpValidApprovedInZman(oSrc, args)
{
    var  ZNMavoda = parseInt(document.getElementById("txtZNMavoda").value);
    var  ZNLavoda = parseInt(document.getElementById("txtZNLavoda").value);
    
  //  var FormType = parseInt(document.getElementById("TxtFormType").value);
    var ApprovedValueM ;
    var ApprovedValueL ;
    ApprovedValueM= document.getElementById("txtZNMavodaMax");
    ApprovedValueL= document.getElementById("txtZNLavodaMax")
    if ((ZNMavoda)  > (parseInt(ApprovedValueM.value))  || 
        (ZNLavoda)  >  (parseInt(ApprovedValueL.value)))
    {args.IsValid = false ;
     }
    else { 
    args.IsValid = true ;
    } 
    Page_IsValid=args.IsValid;
   EnablebtnUpdate();
}


function  setChanged ()
{
    document.getElementById("txtChanged").value  = "1" ;
  
}

</script>
</html>
