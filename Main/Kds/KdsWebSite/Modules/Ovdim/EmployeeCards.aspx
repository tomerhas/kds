<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmployeeCards.aspx.cs" Inherits="Modules_Ovdim_EmployeeCards" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="kds" TagName="GridViewPager" Src="~/UserControls/GridViewPager.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script src='../../js/jquery.js' type='text/javascript'></script>
<script src='../../js/jquery.simplemodal.js' type='text/javascript'></script>
<script src='../../js/basic.js' type='text/javascript'></script>

<link type='text/css' href='../../css/basic.css' rel='stylesheet' media='screen' />
    <script src="../../Js/GeneralFunction.js" type="text/javascript"></script>
    <style type="text/css">
            .GridPagerLarge td
            {
                  padding-left: 10px;      
              }
      
        .style1
        {
            color: Black;
            font-family: Arial;
            width: 50px;
        }
        .style2
        {
            width: 126px;
        }
        .style3
        {
            width: 77px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">
<div class="Progress" id="divHourglass"  style="display:none;text-align:center;position:absolute;left:52%;top:48%; z-index:1000;width:150px" >
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/EggedProgress.gif" style="width: 100px; height: 100px" /><br /> 
</div> 
<div id="divSinun" runat="server" onkeydown="if (event.keyCode==107) {event.keyCode=9; return event.keyCode }">
      <fieldset class="FilterFieldSet" style="width:933px;height:80px">
        <legend>רשימת כרטיסי עבודה עבור</legend> 
        <table cellpadding="2" cellspacing="0" border="0" style="margin-top:4px"  >
            <tr>
                <td>
                    <asp:UpdatePanel ID="upRdoId" runat="server" RenderMode="Inline">
                        <ContentTemplate> 
                        <table>
                            <tr>                                
                                <td class="InternalLabel" style="width:90px">                                       
                                  <asp:RadioButton runat="server" Checked="true" ID="rdoId"   EnableViewState="true" GroupName="grpSearch" Text="מספר אישי:"    > </asp:RadioButton>                                        
                                </td>
                                                         
                                <td style="width:120px;">
                                    <%--<asp:UpdatePanel ID="upId" runat="server" RenderMode="Inline"  >
                                        <ContentTemplate> --%>
                                           <asp:TextBox ID="txtId" runat="server" AutoComplete="Off" dir="rtl" ontextchanged="txtId_TextChanged"  MaxLength="5" style="width:100px;" TabIndex="1" autopostback="true" onfocusout=" this.value=this.value; setMonthFocus();"></asp:TextBox>                            
                                           <cc1:AutoCompleteExtender id="AutoCompleteExtenderID" runat="server" CompletionInterval="0" CompletionSetCount="25" UseContextKey="true"  
                                            TargetControlID="txtId" MinimumPrefixLength="1" ServiceMethod="GetOvdimToUser" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                            EnableCaching="true"  CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                                            CompletionListItemCssClass="autocomplete_completionListItemElement" OnClientHidden="onClientHiddenHandler_getID"  OnClientShown="onClientShownHandler_getID">                               
                                          </cc1:AutoCompleteExtender>                      
                                      <%-- </ContentTemplate>                       
                                    </asp:UpdatePanel>    --%>       
                                </td>                
                                <td class="style1">
                                   <%--<asp:UpdatePanel ID="upRdoName" runat="server" RenderMode="Inline">
                                       <ContentTemplate>  --%>                      
                                         <asp:RadioButton runat="server" ID="rdoName" EnableViewState="true" GroupName="grpSearch" Text="שם:" > </asp:RadioButton>
                                   <%-- </ContentTemplate>
                                  </asp:UpdatePanel> --%>
                                </td>   
                                
                                <td style="width:220px;">
                                   <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline"  >
                                        <ContentTemplate>--%> 
                                            <asp:TextBox ID="txtName" runat="server" AutoComplete="Off" style="width:180px;" TabIndex="2"   onchange="setMonthFocus();" autopostback="true" ontextchanged="txtName_TextChanged"></asp:TextBox>
                                            <cc1:AutoCompleteExtender id="AutoCompleteExtenderByName" runat="server" CompletionInterval="0" CompletionSetCount="12" UseContextKey="true"  
                                                        TargetControlID="txtName" MinimumPrefixLength="1" ServiceMethod="GetOvdimToUserByName" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                                        EnableCaching="true"  CompletionListCssClass="autocomplete_completionListElement" FirstRowSelected="true"
                                                        CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                                                        CompletionListItemCssClass="autocomplete_completionListItemElement" OnClientHidden="onClientHiddenHandler_getName" >                                                             
                                            </cc1:AutoCompleteExtender> 
                                        <%-- </ContentTemplate>                       
                                 </asp:UpdatePanel>     --%>  
                                </td>
                                <td style="width:80px;">
                                    סניף/יחידה:
                                </td> 
                                <td>
                                   <asp:textbox runat="server" ID="txtSnifUnit" Enabled="false" TabIndex="4" 
                                        style="width:300px;"></asp:textbox>
                                </td>                                     
                            </tr>        
                        </table>  
                   </ContentTemplate>
                  </asp:UpdatePanel>        
                </td>                               
            </tr>
           </table>
           <table border="0"> 
            <tr>
                <td class="style3"><asp:RadioButton runat="server" ID="rdoMonth" GroupName="grpCardType" Text="חודש" > </asp:RadioButton></td>
                <td class="style2"> 
                   <asp:DropDownList runat="server" ID="ddlMonth"  onchange="document.getElementById('ctl00_KdsContent_txtPageIndex').value = '0';"  TabIndex="3" 
                        style="width:103px; margin-right: 15px;"></asp:DropDownList></td>
              
            
                <td style="width:182px;">&nbsp;&nbsp;<asp:RadioButton runat="server" ID="rdoCards" GroupName="grpCardType" Text="כל כרטיסי העבודה בטיפול" onclick="document.getElementById('ctl00_KdsContent_txtPageIndex').value='0';"> </asp:RadioButton></td>                          
          
                <td style="width:60px;">
                <asp:UpdatePanel ID="upExecute" runat="server" RenderMode="Inline" UpdateMode="Conditional" >
                       <ContentTemplate>
                            <asp:Button Text="הצג" ID="btnExecute" runat="server" TabIndex="5" 
                                CssClass ="ImgButtonSearch" autopostback="true" onclick="btnExecute_Click" 
                                Width="62px" onfocusin="this.style.border ='1px solid black';" onfocusout="this.style.border ='none';" />                                                                                                               
                       </ContentTemplate>                                   
                    </asp:UpdatePanel>    
                </td>
              <td style="width:60px;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional" >
                       <ContentTemplate>
                            <asp:Button Text="הצג אישורים" ID="btnShowApproval" runat="server" TabIndex="6" style="display:none"
                                CssClass ="ImgButtonSearch" autopostback="true" onclick="btnShowApproval_Click" 
                                Width="112px" onfocusin="this.style.border ='1px solid black';" onfocusout="this.style.border ='none';" /> 
                                                                                                                                            
                       </ContentTemplate>                                              
                    </asp:UpdatePanel>    
                </td>
             
            </tr>
         </table> 
       
     </fieldset>   
 </div>     
     <table>
        <tr>
            <td>
                <asp:UpdatePanel ID="upGrid" runat="server" RenderMode="Inline" >
                   <ContentTemplate> 
                     <div id="divNetunim" runat="server"  style="text-align:right;width:950px;overflow-y:scroll;overflow-x:hidden;height:330px;">
                        <asp:GridView ID="grdEmployee" runat="server" AllowSorting="true" 
                                 AllowPaging="true" PageSize="8" AutoGenerateColumns="false" CssClass="Grid"  
                                 Width="950px" EmptyDataText="לא נמצאו נתונים!" 
                                 OnRowDataBound="grdEmployee_RowDataBound" OnSorting="grdEmployee_Sorting" OnPageIndexChanging="grdEmployee_PageIndexChanging">
                                <Columns>                                                            
                                    <asp:HyperLinkField DataTextField="taarich" ItemStyle-CssClass="ItemRow"  ItemStyle-Font-Size="Larger" HeaderStyle-CssClass="GridHeader"   HeaderText="תאריך" SortExpression="taarich" NavigateUrl="#"  ItemStyle-Width="220px"   />                                          
                                    <asp:BoundField DataField="status" HeaderText="דרוש עדכון" SortExpression="status"  ItemStyle-Font-Size="Larger"  ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"   />
                                    <asp:BoundField DataField="kartis_without_peilut" HeaderText="ללא דיווח"  ItemStyle-Font-Size="Larger" SortExpression="kartis_without_peilut" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                                    <asp:BoundField DataField="measher_o_mistayeg_key" HeaderText="מסתייג/מאשר/ללא התייחסות"  ItemStyle-Font-Size="Larger"  SortExpression="measher_o_mistayeg_key" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                                    <asp:BoundField DataField="status_tipul_key" HeaderText="ממתין לאישור"  ItemStyle-Font-Size="Larger" SortExpression="status_tipul_key" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                               <%--     <asp:BoundField DataField="status_tipul_key" HeaderText="ממתין לחישוב שכר" SortExpression="status_tipul_key" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />                            
                               --%> </Columns>
                                <AlternatingRowStyle CssClass="GridAltRow" Height="25px" />
                                <RowStyle CssClass="GridRow" Height="25px" />
                                <PagerStyle CssClass="GridPagerLarge" HorizontalAlign="Center"  />                          
                                <EmptyDataRowStyle CssClass="GridEmptyData" height="10px" Wrap="False"/> 
                                <PagerTemplate>
                                     <kds:GridViewPager runat="server" ID="ucGridPager" />
                                </PagerTemplate>                                                   
                         </asp:GridView>
                        </div> 
                         <input type="hidden" runat="server" id="txtPageIndex" value="0"/>  
                     </ContentTemplate>
                     <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnExecute" />                     
                     </Triggers> 
                  </asp:UpdatePanel>       
            </td>
        </tr>
     </table>
     <div id="divHeadruyot_Parent" style="display:none;">
        <div id="divHeadruyot">
            <iframe id="frameHeadruyot" frameborder="0" style="overflow:hidden;"></iframe>
        </div>
     </div>     
     
       <script type="text/javascript" language="javascript" >        
       function checkID()
       {
        var iId = document.getElementById("ctl00_KdsContent_txtId").value;
        if (iId.length==0)
        {
         document.getElementById("ctl00_KdsContent_btnExecute").className= "ImgButtonSearchDisable";
         document.getElementById("ctl00_KdsContent_btnExecute").disabled = true;
        }
        else{
            document.getElementById("ctl00_KdsContent_btnExecute").className= "ImgButtonSearch";
            document.getElementById("ctl00_KdsContent_btnExecute").disabled = false;
        }
      //  document.getElementById("ctl00_KdsContent_divNetunim").style
       }
       function ClearScreen()
       {
            if (document.getElementById("ctl00_KdsContent_grdEmployee")!=null)
            {
                document.getElementById("ctl00_KdsContent_grdEmployee").style.display='none';
                document.getElementById("ctl00_KdsContent_ddlMonth").selectedIndex=0;
                document.getElementById("ctl00_KdsContent_rdoMonth").checked=true;                
            }          
        }      
        
        function onClientShownHandler_getID(sender,eventArgs)
        {
         //document.getElementById("ctl00_KdsContent_ddlMonth").style.display='none';   
        }
        
        function onClientHiddenHandler_getID(sender, eventArgs)
        {
         GetOvedName(document.getElementById("ctl00_KdsContent_txtId"));
         document.getElementById("ctl00_KdsContent_ddlMonth").style.display='block';
        }     
        
        function onClientHiddenHandler_getName(sender, eventArgs)
        {          
         var iMisparIshi, iPos;
         var sOvedName=document.getElementById("ctl00_KdsContent_txtName").value;
         
         if (sOvedName!='')
         {
             iPos = sOvedName.indexOf('(');
             if (iPos==-1)
             {
                //GetOvedMisparSucc();
              //  GetOvedMisparIshi(document.getElementById("ctl00_KdsContent_txtName"));
             }
             else{
                iMisparIshi = sOvedName.substr(iPos+1, sOvedName.length-iPos-2);
                document.getElementById("ctl00_KdsContent_txtId").value=iMisparIshi;
                document.getElementById("ctl00_KdsContent_txtName").value =  sOvedName.substr(0,iPos-1);
                wsGeneral.GetOvedSnifAndUnit(Number(iMisparIshi),GetOvedSnifAndUnitSucceeded);
                }
          }      
        }
        
        function GetOvedNameByMisparIshi()
        {
          //document.getElementById("ctl00_KdsContent_txtName").value="";              
        }

        function setMonthFocus() {
            if (document.getElementById("ctl00_KdsContent_rdoMonth").checked) {
              if (document.getElementById("ctl00_KdsContent_btnExecute").disabled == false)
                document.getElementById("ctl00_KdsContent_ddlMonth").focus();
            }
            else document.getElementById("ctl00_KdsContent_btnExecute").focus();

        }                
        function GetOvedMisparSucc(result)
        {
            if (result==''){
                alert('שם לא נמצא');                        
                document.getElementById("ctl00_KdsContent_txtName").value='';
                document.getElementById("ctl00_KdsContent_txtName").select();                
                document.getElementById("ctl00_KdsContent_txtId").value='';
               // document.getElementById("ctl00_KdsContent_txtSnifUnit").value = '';
                document.getElementById("ctl00_KdsContent_txtSnifUnit").innerText = '';
                document.getElementById("ctl00_KdsContent_btnExecute").className= "ImgButtonSearchDisable";
                document.getElementById("ctl00_KdsContent_btnExecute").disabled = true;
               
            }
            else {
                
                document.getElementById("ctl00_KdsContent_txtId").value = result;
                document.getElementById("ctl00_KdsContent_ddlMonth").focus();
              wsGeneral.GetOvedSnifAndUnit(Number(result),GetOvedSnifAndUnitSucceeded);
            }
        }
        
        function GetOvedNameSucceeded(result) {
             
           if ((result=='') || (result=='null')){
                alert('מספר אישי לא קיים');                        
                document.getElementById("ctl00_KdsContent_txtId").select();
                document.getElementById("ctl00_KdsContent_txtName").value='';
              //  document.getElementById("ctl00_KdsContent_txtSnifUnit").value = '';
                document.getElementById("ctl00_KdsContent_txtSnifUnit").innerText = '';
                document.getElementById("ctl00_KdsContent_btnExecute").className= "ImgButtonSearchDisable";
                document.getElementById("ctl00_KdsContent_btnExecute").disabled = true;
            }
            else{
                document.getElementById("ctl00_KdsContent_txtName").value=result;
                var iMisparIshi = document.getElementById("ctl00_KdsContent_txtId").value;
                document.getElementById("ctl00_KdsContent_txtName").disabled = true;
                document.getElementById("ctl00_KdsContent_ddlMonth").focus();
                wsGeneral.GetOvedSnifAndUnit(iMisparIshi,GetOvedSnifAndUnitSucceeded);
            }
        }
        
        function GetOvedSnifAndUnitSucceeded(result)
        {
         if ((result!='') && (result.toString().length>1)){
           //  document.getElementById("ctl00_KdsContent_txtSnifUnit").value = result;
             document.getElementById("ctl00_KdsContent_txtSnifUnit").innerText = result;
         }
         else
         {
          //   document.getElementById("ctl00_KdsContent_txtSnifUnit").value = '';
             document.getElementById("ctl00_KdsContent_txtSnifUnit").innerText = '';
         }
         document.getElementById("ctl00_KdsContent_btnExecute").className= "ImgButtonSearch";
         document.getElementById("ctl00_KdsContent_btnExecute").disabled = false;
   
     //    ClearScreen();
        }
      function SetTextBox() 
      {
        var rdo = document.getElementById("ctl00_KdsContent_rdoId");
        if (rdo.checked) {
             
            document.getElementById("ctl00_KdsContent_txtId").disabled = false;
            document.getElementById("ctl00_KdsContent_txtName").disabled = true;
            document.getElementById("ctl00_KdsContent_txtId").select();
        }
        else {
            document.getElementById("ctl00_KdsContent_txtName").disabled = false;
            document.getElementById("ctl00_KdsContent_txtId").disabled = true;
                document.getElementById("ctl00_KdsContent_txtName").select();
        }
    }
    

    function CheckEmployeeId() {
        if (document.getElementById("ctl00_KdsContent_txtId").value.length == 0 && document.getElementById("ctl00_KdsContent_txtId").value.length == 0) {
            alert('חובה להזין מספר אישי או שם');
            return false;
            }
        else { return true };

    }

    function refresh() {
      $get("<%=btnExecute.ClientID %>").click();
    }


    function OpenEmpWorkCard(RowDate) {
       // debugger;
        var EmpId = document.getElementById("ctl00_KdsContent_txtId").value;
        var WCardDate = RowDate;
        var sQuryString = "?EmpID=" + EmpId + "&WCardDate=" + WCardDate + "&dt=" + Date();
        document.getElementById("divHourglass").style.display = 'block';
        var ReturnWin = window.showModalDialog('WorkCard.aspx' + sQuryString, window, "dialogHeight: 700px; dialogWidth: 1010px; scroll: no");
        if (ReturnWin == '' || ReturnWin == 'undefined') ReturnWin = false;
        document.getElementById("divHourglass").style.display = 'none';
        document.getElementById("ctl00_KdsContent_btnExecute").click();
        return ReturnWin;
     }

   
   </script>
</asp:Content>

