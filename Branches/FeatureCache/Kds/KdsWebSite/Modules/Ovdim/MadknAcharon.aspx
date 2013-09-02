<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MadknAcharon.aspx.cs" Inherits="Modules_Ovdim_MadknAcharon" %>
   <link href="../../StyleSheet.css"type="text/css" rel="stylesheet" />
   <script src="../../Js/GeneralFunction.js"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>

<body class="WorkCardRechivim">
      <form id="MadknAcharon" runat="server">
     <asp:ScriptManager  runat="server"  id="ScriptManagerKds" EnablePartialRendering="true" >        
     </asp:ScriptManager>

    <div style="overflow:auto" width="100%">
      <table cellspacing="0" cellpadding="0"  width="100%" >
            <tr>
                <td >
                     <asp:Label runat="server" ID="lblPageHeader"   width="100%" CssClass = "WorkCardRechivimHeader" >עדכונים בכרטיס העבודה   </asp:Label>
                       <asp:Panel ID="pnlUpdators"  height="250px" width="100%" dir="rtl"  runat="server" ScrollBars="Auto">   
                        <asp:GridView ID="grdUpdators" runat="server"  width="100%" GridLines="none" CssClass="WorkCardRechivimGridRow"   
                                EmptyDataText="לא נמצאו נתונים!"  
                                AllowPaging="false" AutoGenerateColumns="false"  
                                   HeaderStyle-CssClass="WorkCardRechivimGridHeader" >
                            <Columns>
                                <asp:BoundField DataField="UPDATEDATE" HeaderText="תאריך עדכון"  ItemStyle-CssClass="WorkCardRechivimGridRow"  ItemStyle-Width="33%" />
                                <asp:BoundField DataField="IDNUM" HeaderText="מ.א. מעדכן" ItemStyle-CssClass="WorkCardRechivimGridRow" ItemStyle-Width="33%" />
                                <asp:BoundField DataField="FULLNAME" HeaderText="שם המעדכן"  ItemStyle-CssClass="WorkCardRechivimGridRow" ItemStyle-Width="33%"/>
                            </Columns> 
                                 <EmptyDataRowStyle CssClass="GridEmptyData" height="20px"/>                            
                         </asp:GridView>
                       </asp:Panel > 
                </td>
            </tr>
                   
       </table>
    </div>
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
   EnablebtnUpdate();
}
//function CmpValidApprovedInZmanM(oSrc, args)
//{
//    var  ZNMavoda = parseInt(document.getElementById("txtZNMavoda").value);
//  //  var FormType = parseInt(document.getElementById("TxtFormType").value);
//    var ApprovedValue ;
//    ApprovedValue= document.getElementById("txtZNMavodaMax");
//    if (ZNMavoda > (parseInt(ApprovedValue.value)))
//    {args.IsValid = false;
//     page_isvalid= false;
//     }
//    else { args.IsValid = true;
//    page_isvalid=true;
//    }
//    EnablebtnUpdate();
//}
//function CmpValidApprovedInZmanL(oSrc, args)
//{
//    var  ZNLavoda = parseInt(document.getElementById("txtZNLavoda").value);
//  //  var FormType = parseInt(document.getElementById("TxtFormType").value);
//    var ApprovedValue ;
//    ApprovedValue= document.getElementById("txtZNLavodaMax");
//    if (ZNLavoda > (parseInt(ApprovedValue.value)))
//    {args.IsValid = false;
//    page_isvalid=false ;
//     }
//    else { args.IsValid = true;
//    page_isvalid=true;
//    }
//    EnablebtnUpdate();
//}
function EnablebtnUpdate()
{
    var btnUpdate = document.getElementById("btnUpdate");
    btnUpdate.disabled =!Page_IsValid;
    btnUpdate.className = Page_IsValid? "ImgButtonSearch" : "ImgButtonSearchDisable" ; 
     
//      if (Page_IsValid == true)
//      {
//      //alert()
     // var answer = confirm("האם ברצונך לעדכן את השינויים שביצעת בזמני נסיעות בפועל ?");
      
        
}
function  setChanged ()
{
  // alert(document.getElementById("txtChanged").value);
    document.getElementById("txtChanged").value  = "1" ;
  //    alert(document.getElementById("txtChanged").value);
}


  
//  function ConfirmAndClose()
//    {
////        var ApprovedInUnit = document.getElementById("txtZNLavoda").value;
////        var AavaratToVaadP = document.getElementById("txtZNLavodaMax").value;
////        var ApprovedInVaadatP = document.getElementById("txtZNLavodaMax").value;
////        ReturnValue = ApprovedInUnit + ',' + AavaratToVaadP + "," + ApprovedInVaadatP;
//        window.close();
//    }


</script>
</html>
