<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ListElementSelector.ascx.cs" Inherits="Modules_UserControl_ListElementSelector" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script language="javascript" type="text/javascript" for="window" event="onload">
// <!CDATA[
return window_onload()
// ]]>
</script>



<link href="~/StyleSheet.css" rel="stylesheet" type="text/css" runat="server" id="styleMain" visible="false" />
<fieldset class="FilterFieldSet"> 
        <legend style="background-color:White" dir="rtl" id="Title" runat="server"></legend>      
        <table  width="100%"  cellpadding="0" cellspacing="0"  class="FilterTable" border="0">
          <tr>
                <td  dir="rtl">
                  <asp:Label ID="LblId" runat="server">פריט</asp:Label>
                         <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Always"  >
                            <ContentTemplate>
                    <asp:TextBox ID="txtIdDetail" runat="server" onchange="FindSingle()" AutoComplete="Off" dir="rtl" ></asp:TextBox>                            
                      <cc1:AutoCompleteExtender id="AutoCompleteExtenderID"  runat="server" CompletionInterval="0" CompletionSetCount="25" UseContextKey="true"  
                        TargetControlID="txtIdDetail" MinimumPrefixLength="1" ServiceMethod="GetOvdimToUser" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                        EnableCaching="true"  CompletionListCssClass="autocomplete_completionListElement"
                        CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                        CompletionListItemCssClass="autocomplete_completionListItemElement"  >                               
                    </cc1:AutoCompleteExtender>                              
                    <asp:HiddenField ID="HidListVisible" runat="server" />
                                                </ContentTemplate>
                        </asp:UpdatePanel>

               </td>                

             </tr>     
             <tr>
             <td><div id="DivListElement" runat="server" style="display:block">
                 <table width="100%"  cellpadding="0" cellspacing="0"    border="0">
                    <tr>
                         <td>
                         <asp:UpdatePanel ID="UpdPlnListElementSelector" runat="server" RenderMode="Inline" UpdateMode="Always"  >
                            <ContentTemplate>
                                <asp:Label ID="LblLstOfElement" Font-Underline="true" runat="server" Text="רשימת פריטים" ></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
            </td>
                         <td><asp:HiddenField ID="HiddenElementSelected" runat="server" /></td>
                         <td><asp:Label ID="LblLstOfElementSelected" Font-Underline="true" runat="server" Text="רשימת פריטים שנבחרו" ></asp:Label></td>
                     </tr>
                     <tr>
                        <td  rowspan="6" style="width:45%">
                            <asp:ListBox Width="100%" Height="150px" ID="LstBxOfElement" runat="server" EnableViewState="false" ></asp:ListBox>
                        </td>
                        <td style="height:10px;width:10%"></td>
                        <td    rowspan="6" style="width:45%">
                            <asp:ListBox  Width="100%" EnableViewState="true" Height="150px" ID="LstBxOfElementSelected"  runat="server"></asp:ListBox>
                        </td>
                     </tr>
                     <tr><td align="center"><input value=">>" type="button" class="ImgButtonSearch" style="width:30px"  onclick="SelAll(true);" /></td></tr>
                     <tr><td align="center"><input value=">" type="button" class="ImgButtonSearch" style="width:30px" onclick="listbox_moveacross('ctl00_KdsContent_ListOfElements_LstBxOfElement', 'ctl00_KdsContent_ListOfElements_LstBxOfElementSelected');" /> </td></tr>
                     <tr><td align="center"><input value="<" type="button" class="ImgButtonSearch" style="width:30px" onclick="listbox_moveacross('ctl00_KdsContent_ListOfElements_LstBxOfElementSelected', 'ctl00_KdsContent_ListOfElements_LstBxOfElement');" /> </td></tr>
                     <tr><td align="center"><input value="<<" type="button" class="ImgButtonSearch"  style="width:30px" onclick="SelAll(false);" /></td></tr>
                     <tr><td style="height:20px"></td></tr>
                 </table>
             </div></td>
             </tr>
        </table>
   </fieldset>
<script language="javascript" type="text/javascript">
    var txtId;

    function window_onload() {
        var src;
        var dest;
        var SelectedItems = document.getElementById("ctl00_KdsContent_ListOfElements_HiddenElementSelected").value;
        if (SelectedItems != "") {
            src = document.getElementById("ctl00_KdsContent_ListOfElements_LstBxOfElement");
            dest = document.getElementById("ctl00_KdsContent_ListOfElements_LstBxOfElementSelected");
            for (var count = 0; count < src.options.length; count++) {
                var option = src.options[count];
                if (SelectedItems.indexOf(option.value) > -1) {
                    var newOption = document.createElement("option");
                    newOption.value = option.value;
                    newOption.text = option.text;
                    dest.add(newOption);
                    src.remove(count);
                    count--;
                }
            }
        }
    }
       function listbox_moveacross(sourceID, destID) {
           txtId = document.getElementById("ctl00_KdsContent_ListOfElements_txtIdDetail");
           var src = document.getElementById(sourceID);
           var dest = document.getElementById(destID);
           var HiddenElementSelected = document.getElementById("ctl00_KdsContent_ListOfElements_HiddenElementSelected");
           var strSelectItems = "";
           var MyDest;
           for (var count = 0; count < src.options.length; count++) {
               if (src.options[count].selected == true) {
                   var option = src.options[count];
                   var newOption = document.createElement("option");
                   newOption.value = option.value;
                   newOption.text = option.text;
                   dest.add(newOption); 
                   src.remove(count);
                   count--;
               }  
           }
          
           MyDest = document.getElementById("ctl00_KdsContent_ListOfElements_LstBxOfElementSelected");    
           for (var count = 0; count < MyDest.options.length; count++) {
               var option = dest.options[count];
               strSelectItems = strSelectItems + option.value + ",";
           }
         
           if (strSelectItems != '')
               HiddenElementSelected.value = strSelectItems.substr(0, strSelectItems.length - 1);
           else
               HiddenElementSelected.value = "";
       
           sortlist(dest);
           txtId.value = '';
         
       }

   function sortlist(sourceID) {
       arrTexts = new Array();

       for (i = 0; i < sourceID.length; i++) {
           arrTexts[i] = new Array(2);
           arrTexts[i][0] = sourceID.options[i].text;
           arrTexts[i][1] = sourceID.options[i].value;
       }
       arrTexts.sort();
       for (i = 0; i < sourceID.length; i++) {
           sourceID.options[i].text = arrTexts[i][0];
           sourceID.options[i].value = arrTexts[i][1];
       }
   }

   function FindSingle() {
       txtId = document.getElementById("ctl00_KdsContent_ListOfElements_txtIdDetail");
       var src = document.getElementById('ctl00_KdsContent_ListOfElements_LstBxOfElement');
       HidListVisible = document.getElementById("ctl00_KdsContent_ListOfElements_HidListVisible");
       if (HidListVisible.value == "1") {
           for (i = 0; i < src.length; i++) {
               if (src.options[i].value == txtId.value) {
                   src.options[i].selected = true;
                   src.options[i].focus = true;
               }
           }
       }
       else {
           var HiddenElementSelected = document.getElementById("ctl00_KdsContent_ListOfElements_HiddenElementSelected");
           HiddenElementSelected.value = txtId.value;
       }
   }
   function SelAll(chk) {
       var src ;
       if (chk) {
           src = document.getElementById('ctl00_KdsContent_ListOfElements_LstBxOfElement');
       }
       else {
           src = document.getElementById('ctl00_KdsContent_ListOfElements_LstBxOfElementSelected');
       }
       for (var i = 0; i < src.options.length; i++) {
           src.options[i].selected = true;
       }
       if (chk) {
           listbox_moveacross('ctl00_KdsContent_ListOfElements_LstBxOfElement', 'ctl00_KdsContent_ListOfElements_LstBxOfElementSelected');
       }
       else {
           listbox_moveacross('ctl00_KdsContent_ListOfElements_LstBxOfElementSelected', 'ctl00_KdsContent_ListOfElements_LstBxOfElement');
       }
   }
</script>