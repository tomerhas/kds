<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" CodeBehind="NochechutMerukezet.aspx.cs" Inherits="KdsWebApplication.Modules.Ovdim.NochechutMerukezet" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="KdsLibrary" Namespace="KdsLibrary.Controls" TagPrefix="KdsCalendar"%>
<%@ Register TagPrefix="kds" TagName="GridViewPager" Src="~/UserControls/GridViewPager.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <script src="../../Js/GeneralFunction.js" type="text/javascript"></script>
    <style type="text/css">
        .GridPagerNew
        {
	        border-color:#808080;	
	        color:Black;
	        background-color:#FCF9CD;
	        font-size:15px;
	        font-weight:bold;
	        padding-right:300px;
	        border-style:solid;
	        border-width:1px;
            text-align:center;    
        }

      .GridHeaderChild
            {
	            color:Black;
	            border-right:solid 1px #3F3F3F;
	            height:20px;
	            font-size:12px;
	            font-weight:bold;
                 border-right:hidden;
                 border-left:hidden;
                border-bottom:solid 1px #3F3F3F;
                text-align:center;
                vertical-align:middle;
                background-color:white;	

             }

        .trGridChild
            {
                background-color:white;	
                border:hidden;
             }
        .GridChild
        {
	        text-align:center;
            border:hidden; 
        }
       .ItemRowChild
        {
         border-right:hidden;
         border-left:hidden;
         border-bottom:hidden;
         padding-right:3px;
         font-size:14px;
         font-family:Arial;
        }
      .GridHeader2
        {
	        background-color: #808080;	
	        border-left:hidden;
         }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">
   <script type="text/javascript" language="javascript">
       var userId = iUserId;
  </script>
   

<div class="Progress" id="divHourglass"  style="display:none;text-align:center;position:absolute;left:52%;top:48%; z-index:1000;width:150px" >
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/progress.gif" style="width: 100px; height: 100px" /><br /> 
</div>  
    <fieldset class="FilterFieldSet" style="width:970px"> 
       <legend> בחירת נתונים להצגה </legend>      
     <table style="margin-top:4px" border="0">
        <tr>
            <td style="width:10px"></td>
            <td class="InternalLabel" style="width:50px"> מתאריך:</td> 
            <td align="right" dir="ltr" style="width:160px">                            
                 <KdsCalendar:KdsCalendar runat="server" ID="clnFromDate" CalenderTabIndex="4"  AutoPostBack="false" OnChangeCalScript="onChange_FromDate();"  dir="rtl" PopupPositionCallOut="Left" ></KdsCalendar:KdsCalendar>           
                 <asp:CustomValidator runat="server" id="vldFrom"   ControlToValidate="clnFromDate" ErrorMessage=""  Display="None"    ></asp:CustomValidator>
                 <cc1:ValidatorCalloutExtender runat="server" ID="vldExFrom" BehaviorID="vldExFromDate"   TargetControlID="vldFrom" Width="200px" PopupPosition="Left"  ></cc1:ValidatorCalloutExtender>                                                
               
            </td> 
            <td style="width:10px"></td>
            <td class="InternalLabel" style="width:70px"> עד תאריך:</td> 
            <td align="right" dir="ltr" style="width:160px"> 
                 <KdsCalendar:KdsCalendar runat="server" ID="clnToDate"  AutoPostBack="false"  dir="rtl" OnChangeCalScript="onChange_ToDate();" PopupPositionCallOut="Left" ></KdsCalendar:KdsCalendar> 
                 <asp:CustomValidator runat="server" id="vldTo"   ControlToValidate="clnToDate" ErrorMessage=""  Display="None"    ></asp:CustomValidator>
                 <cc1:ValidatorCalloutExtender runat="server" ID="vldExTo" BehaviorID="vldExToDate"   TargetControlID="vldTo" Width="200px" PopupPosition="Left"  ></cc1:ValidatorCalloutExtender>                                                
                                          
            </td> 
            <td style="width:10px"></td>

            <td id="rbAllTD" runat="server" style="width:150px;">&nbsp;&nbsp;
                <input type="radio" id="rdoAll" runat="server" onclick="RbChange()" value="1"   name="grpCardType" />כל העובדים הכפופים 
              </td>
              <%--  <asp:RadioButton runat="server"  ID="rdoAll" Checked="true"   OnClick="RbChange()"    GroupName="grpCardType" Text="כל העובדים הכפופים"> </asp:RadioButton></td>   --%>
             <td> 
                    <input type="radio" id="rdoMi" runat="server" onclick="RbChange()"  value="2"    name="grpCardType"  />מספר אישי
                 <%-- <asp:RadioButton runat="server" ID="rdoMi"   GroupName="grpCardType"  OnClick="RbChange()"  Text="מספר אישי">   --%>     <%-- </asp:RadioButton>--%>
            </td>                          
          <td>
                 <asp:TextBox ID="txtId" runat="server" AutoComplete="Off" dir="rtl"    MaxLength="5" style="width:100px;" TabIndex="1"></asp:TextBox>                            
                        <cc1:AutoCompleteExtender id="AutoCompleteExtenderID" runat="server" CompletionInterval="0" CompletionSetCount="25" UseContextKey="true"  
                        TargetControlID="txtId" MinimumPrefixLength="1" ServiceMethod="GetOvdimToUser" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                        EnableCaching="true"  CompletionListCssClass="ACLst"
                        CompletionListHighlightedItemCssClass="ACLstItmSel"
                        CompletionListItemCssClass="ACLstItmE"   >
                     <%--   OnClientHidden="SimunExtendeIdClose"  OnClientShowing="SimunExtendeOpen"  >  --%>                      
                        </cc1:AutoCompleteExtender>    
                        
              </td>   
             <td style="width:20px"></td>           
            <td> 
            
                <asp:UpdatePanel ID="upBtnShow" runat="server" RenderMode="Inline"  >
                  <ContentTemplate> 
                      <input type="button" id="btnShow" name="btnShow" value="הצג" class="ImgButtonSearch" onclick="CheckOvedId();" />
                      <%--  <asp:button ID="btnShow" runat="server" text="הצג" CssClass ="ImgButtonSearch" OnClientClick="CheckOvedId();"  />  --%>
                   </ContentTemplate>
              </asp:UpdatePanel> 
             
               </td>
            
        </tr>                
    </table>  
        </fieldset>
 <br />
 
   <asp:UpdatePanel ID="udGrid"  runat="server" RenderMode="Inline" UpdateMode="Always"  >
                     <ContentTemplate>     
    <table > 
        <tr style="border:none">
            <td  style="border:none">          
               
                      <%--  <asp:Button ID="btnRedirect" runat="server"  OnCommand="btnRedirect_Click"  />
                        <asp:textbox  ID="txtRowSelected" runat="server"  />--%>
                           <div id="divNetunim" runat="server"  dir="ltr" style="text-align:right;width:970px;overflow-x:hidden;overflow-y:scroll;border:none;height:600px">
                        <asp:GridView ID="grdEmployee" runat="server"  ShowHeader="False" style="background-color:white"
                             AllowPaging="true"  AutoGenerateColumns="false" CssClass="Grid"
                             Width="965px" EmptyDataText="לא נמצאו נתונים!" EmptyDataRowStyle-HorizontalAlign="Center"
                             OnRowDataBound="grdEmployee_RowDataBound" OnPageIndexChanging="grdEmployee_PageIndexChanging">
                            <Columns>

                                 <asp:TemplateField  ItemStyle-CssClass="GridHeader2"   >

                                    <ItemTemplate>

                                        <img alt = "" style="cursor: pointer;display: none" src="../../Images/minus.png" />
                         
                                        <asp:Panel ID="pnlNochechut" runat="server" Style="display: none">

                                            <asp:GridView ID="gvNochechut" runat="server" CssClass="GridChild"  Width="960px"   OnRowDataBound="gvNochechut_RowDataBound" AutoGenerateColumns="false">

                                                <Columns>

                                                    <asp:HyperLinkField DataTextField="taarich" HeaderText="תאריך"   ItemStyle-CssClass="ItemRowChild" HeaderStyle-CssClass="GridHeaderChild"  NavigateUrl="#" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="TEUR_SIDUR_MEYCHAD" HeaderText="סידור"   ItemStyle-CssClass="ItemRowChild" HeaderStyle-CssClass="GridHeaderChild" ItemStyle-HorizontalAlign="Center" />           
                                                    <asp:BoundField DataField="mispar_sidur"  ItemStyle-Width="0px" />
                                                    <asp:BoundField DataField="SHAT_HATCHALA" HeaderText="שעת התחלה" ItemStyle-Width="0px"  ItemStyle-CssClass="ItemRowChild" HeaderStyle-CssClass="GridHeaderChild"  DataFormatString="{0:HH:mm dd/MM}" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:TemplateField ItemStyle-CssClass="ItemRowChild" HeaderStyle-CssClass="GridHeaderChild" ItemStyle-HorizontalAlign="Center" HeaderText="שעת התחלה" >
                                                       <ItemTemplate>
                                                             <asp:Label ID="HShatHatchala" runat="server" Font-Bold="true"></asp:Label>
                                                            <asp:Label ID="TShatHatchala" runat="server"  ></asp:Label>
                                                       </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="SHAT_GMAR" HeaderText="שעת גמר" ItemStyle-Width="0px"  ItemStyle-CssClass="ItemRowChild" HeaderStyle-CssClass="GridHeaderChild" DataFormatString="{0:HH:mm dd/MM}" ItemStyle-HorizontalAlign="Center" />
                                                     <asp:TemplateField ItemStyle-CssClass="ItemRowChild" HeaderStyle-CssClass="GridHeaderChild" ItemStyle-HorizontalAlign="Center" HeaderText="שעת גמר" >
                                                       <ItemTemplate>
                                                           <asp:Label ID="HShatGmar" runat="server" Font-Bold="true" ></asp:Label>
                                                           <asp:Label ID="TShatGmar" runat="server"  ></asp:Label>
                                                       </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="teur_in" HeaderText="אי החתמה התחלה"   ItemStyle-CssClass="ItemRowChild" HeaderStyle-CssClass="GridHeaderChild" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="teur_out" HeaderText="אי החתמה גמר"   ItemStyle-CssClass="ItemRowChild" HeaderStyle-CssClass="GridHeaderChild" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="SHAT_HATCHALA_LETASHLUM" HeaderText="ש. התחלה לתשלום" ItemStyle-CssClass="ItemRowChild" HeaderStyle-CssClass="GridHeaderChild"  ItemStyle-HorizontalAlign="Center" />
                                              <%--      <asp:TemplateField ItemStyle-CssClass="ItemRowChild" HeaderStyle-CssClass="GridHeaderChild" ItemStyle-HorizontalAlign="Center" HeaderText="שעת התחלה לתשלום" >
                                                       <ItemTemplate>
                                                           <asp:Label ID="HShatHatchalaLetashlum" runat="server" Font-Bold="true"></asp:Label>
                                                           <asp:Label ID="TShatHatchalaLetashlum" runat="server"  ></asp:Label>
                                                       </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <asp:BoundField DataField="SHAT_GMAR_LETASHLUM" HeaderText="ש. גמר לתשלום"   ItemStyle-CssClass="ItemRowChild" HeaderStyle-CssClass="GridHeaderChild" ItemStyle-HorizontalAlign="Center" />
                                               <%--     <asp:TemplateField ItemStyle-CssClass="ItemRowChild" HeaderStyle-CssClass="GridHeaderChild" ItemStyle-HorizontalAlign="Center" HeaderText="שעת גמר לתשלום" >
                                                       <ItemTemplate>
                                                           <asp:Label ID="HShatGmarLetashlum" runat="server" Font-Bold="true"></asp:Label>
                                                           <asp:Label ID="TShatGmarLetashlum" runat="server"  ></asp:Label>
                                                       </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:BoundField DataField="TEUR_DIVUCH" HeaderText="חריגה"   ItemStyle-CssClass="ItemRowChild" HeaderStyle-CssClass="GridHeaderChild" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px"  />
                                                    <asp:BoundField DataField="LO_LETASHLUM"  ItemStyle-Width="0px" />
                                                

                                                    <asp:TemplateField HeaderText="לא לתשלום"    ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="ItemRowChild" HeaderStyle-CssClass="GridHeaderChild" HeaderStyle-Width="50px"   >
                                                         <ItemTemplate>
                                                                <asp:Image runat="server" ID="imgOK1" Visible="false"   ImageUrl="~/Images/tick.png" />
                                                          <%-- <asp:HyperLink  ID="imgButton" runat="server" NavigateUrl="#" ></asp:HyperLink>--%>
                                                         </ItemTemplate>           
                                                    </asp:TemplateField>  
                                                  
                                                    <asp:BoundField DataField="noch_yomit" HeaderText="נוספות יומי"   ItemStyle-CssClass="ItemRowChild" HeaderStyle-CssClass="GridHeaderChild" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" />
                                                    <asp:BoundField DataField="out_michsa" ItemStyle-Width="0px" />
                                                    <asp:BoundField DataField="noch_codshit" HeaderText="נוספות חודשי"   ItemStyle-CssClass="ItemRowChild" HeaderStyle-CssClass="GridHeaderChild" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="80px"  />
   

                                                </Columns>

                                            </asp:GridView>

                                        </asp:Panel>

                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:BoundField DataField="mispar_ishi"   ItemStyle-CssClass="GridHeader"   HeaderStyle-CssClass="GridHeader"   HeaderStyle-BackColor="Gray"  HeaderText="מספר אישי"  ItemStyle-HorizontalAlign="Center" />                                          
                                <asp:BoundField DataField="full_name" HeaderText="שם" ItemStyle-CssClass="GridHeader" HeaderStyle-CssClass="GridHeader"  ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="TEUR_YECHIDA" HeaderText="יחידה ארגונית"  ItemStyle-CssClass="GridHeader" HeaderStyle-CssClass="GridHeader" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="START_TIME_ALLOWED" HeaderText="שעת התחלה מותרת" ItemStyle-CssClass="GridHeader" HeaderStyle-CssClass="GridHeader" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="END_TIME_ALLOWED" HeaderText="שעת גמר מותרת" ItemStyle-CssClass="GridHeader" HeaderStyle-CssClass="GridHeader" ItemStyle-HorizontalAlign="Center"  />                             
                                <asp:BoundField DataField="TEUR_KOD_GIL" HeaderText="גיל"   ItemStyle-CssClass="GridHeader" HeaderStyle-CssClass="GridHeader" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="TEUR_ISUK" HeaderText="עיסוק"   ItemStyle-CssClass="GridHeader" HeaderStyle-CssClass="GridHeader" ItemStyle-HorizontalAlign="Center" />
                                

                           </Columns>
                          

                            <RowStyle CssClass="GridRow"   />
                            <PagerStyle CssClass="GridPager" HorizontalAlign="Center"  />    
                                            
                            <EmptyDataRowStyle CssClass="GridEmptyData" height="20px" Wrap="False"/>                                                    
                            <PagerTemplate>
                                         <kds:GridViewPager runat="server" ID="ucGridPager" />
                                      
                            </PagerTemplate>  
                        </asp:GridView>
                       </div>        
            </td>     
        </tr>           
   </table>
  <input type="button" ID="hidBtnShow"  runat="server"  onserverclick="btnShow_Click"  style="display:none" />
  <input type="hidden" ID="hidRefresh" name="hidRefresh" runat="server"  value="false"  style="display:none" />
  <input type="hidden" ID="hidScrollPos" name="hidScrollPos" runat="server"  value="0"  style="display:none" />
   </ContentTemplate>    
 </asp:UpdatePanel>   
    
     <input type="hidden" id="Params" name="Params"  runat="server" />
<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>

<script type="text/javascript">

  //  var lastScrollPos = 0;

    function CheckOvedId() {
        $('#<%=divNetunim.ClientID %>').css("display", "none");
        
        $('#trPagerGrid').remove();
       // var selected = $("input[type='radio'][name='grpCardType']:checked"); 
       // var selectedVal = selected.val();
       // alert($("input[type='radio'][name='grpCardType']:checked"));
        if ($("input[type='radio']")[1].checked) {
            var iKodOved = $('#<%=txtId.ClientID %>').val();
            if (iKodOved != "") {
                if (IsNumeric(trim(iKodOved))) {
                    if (userId > 0)
                        wsGeneral.GetOvedToUser(iKodOved, userId, CheckOvedIdSucceeded);
                    else
                        wsGeneral.GetOvedName(iKodOved, CheckOvedIdSucceeded);
                }
                else {
                    alert("מספר אישי לא חוקי");
                    $('#<%=txtId.ClientID %>').val('');
                    $('#<%=txtId.ClientID %>').focus();
                }
            }
            else {
                //alert("1212");
                alert("יש להזין מספר אישי");
                $('#<%=txtId.ClientID %>').val('');
                $('#<%=txtId.ClientID %>').focus();
            }
        }
        else $('#<%=hidBtnShow.ClientID %>').click();

    }

    function CheckOvedIdSucceeded(result) {
        var txtIdObj = $('#<%=txtId.ClientID %>');
        if (result == '') {
            alert('מספר אישי לא קיים/אינך מורשה לצפות בעובד זה');
            txtIdObj.focus();
        }
        else $('#<%=hidBtnShow.ClientID %>').click();
 
    }

    function openDetails() {

        if ($('#<%=divNetunim.ClientID %>').parent().next().length>0)
            $('#<%=divNetunim.ClientID %>').parent().next().remove();

        items = $("[src*=minus]");

        items.each(function () {
            $(this).closest("tr").after("<tr  Width='960px'  class='trGridChild'><td class='ItemRowChild'></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");

           // $(this).attr("src", "../../Images/minus.png");
            $(this).closest("tr").next().css('visibility', 'visible');

        });

      
        var mis= $('#ctl00_KdsContent_grdEmployee')[0].lastChild.children.length-1;
        var pager = $('#ctl00_KdsContent_grdEmployee')[0].lastChild.children[mis];
        if (pager.innerHTML.indexOf("GridPager")>-1) {
            $('#ctl00_KdsContent_grdEmployee')[0].lastChild.removeChild(pager);//[mis].removeChild();    
            $('#<%=divNetunim.ClientID %>').parent().after("<div/>");
            $('#<%=divNetunim.ClientID %>').closest("tr").after("<tr id='trPagerGrid'  class='GridPagerNew'>" + pager.innerHTML + "</tr>");
        }
       
        $('#<%=divNetunim.ClientID %>').scrollTop($('#<%=hidScrollPos.ClientID %>').val());
     //   $('#<%=divNetunim.ClientID %>').parent().next().append("<tr><td>" + pager + "</td></tr>").addClass('GridPagerNew');

    }
    function RbChange() {
      
     //   var selected = $("input[type='radio'][name='grpCardType']:checked"); 
       // var selectedVal = selected.val();
        var txtIdObj = $('#<%=txtId.ClientID %>');
        if ($("input[type='radio']")[0].checked) {
            txtIdObj.val('');
            txtIdObj.prop("disabled", true);
         
        }
        else {           
             txtIdObj.prop("disabled", false);
        }
        
    }

  
   

    function OpenEmpWorkCard(RowData) {
        // debugger;

        var EmpId = RowData.split(',')[0];
        var WCardDate = RowData.split(',')[1];
        var sQuryString = "?EmpID=" + EmpId + "&WCardDate=" + WCardDate + "&dt=" + Date();
       document.getElementById("divHourglass").style.display = 'block';
        var ReturnWin = window.showModalDialog('WorkCard.aspx' + sQuryString, window, "dialogHeight: 680px; dialogWidth: 1010px; scroll: no;status: 1;");
       
        if (ReturnWin == '' || ReturnWin == 'undefined' || ReturnWin == undefined)
            ReturnWin = false;
       
        document.getElementById("divHourglass").style.display = 'none';
        $('#<%=hidRefresh.ClientID %>').val("true");
        $('#<%=hidScrollPos.ClientID %>').val( $('#<%=divNetunim.ClientID %>').scrollTop());
       // alert(lastScrollPos);
        $('#<%=hidBtnShow.ClientID %>').click();
       // $('#<%=divNetunim.ClientID %>').scrollTop(lastScrollPos);
       // alert($('#<%=divNetunim.ClientID %>').scrollTop());
        //document.getElementById("ctl00_KdsContent_btnShow").click();
        return ReturnWin;
    }

    function onChange_FromDate() {
        var sBehaviorId = 'vldExFromDate';
        var FromDate = document.getElementById('ctl00_KdsContent_clnFromDate').value;
        if (FromDate == "") {
            document.getElementById("ctl00_KdsContent_vldFrom").errormessage = "חובה להזין תאריך";
            $find(sBehaviorId)._ensureCallout();
            $find(sBehaviorId).show(true);
            $('#btnShow').prop("disabled", true);//*
        }
        else {
            var Param100 = document.getElementById("ctl00_KdsContent_Params").attributes["Param100"].value;//($('[id$=Params]')[0]).attributes["Param100"].value;// document.getElementById("ctl00_KdsContent_Params").attributes("Param100").value;
            var StartDateSplit = FromDate.split('/');
            var StartDate = new Date(StartDateSplit[2], StartDateSplit[1] - 1, StartDateSplit[0], 0, 0, 0, 0);
            var minDate = new Date();
            minDate.setDate(1);
            minDate.setMonth(minDate.getMonth() - Param100);
            minDate.setHours(0);
            minDate.setMinutes(0);
            minDate.setSeconds(0);
            minDate.setMilliseconds(0);

            if (StartDate.getTime() < minDate.getTime()) {
                document.getElementById("ctl00_KdsContent_vldFrom").errormessage = " לא ניתן להזין תאריך מעבר ל " + Param100 + " חודשים אחורה";
                $find(sBehaviorId)._ensureCallout();
                $find(sBehaviorId).show(true);
                $('#<%=clnFromDate.ClientID %>').focus();
                $('#btnShow').prop("disabled", true);//*
            }
            else
                $('#btnShow').prop("disabled", false);//*
      

            onChange_ToDate();
        }
    }

    function onChange_ToDate() {
        var flag = false;
        var msg;
        var sBehaviorId = 'vldExToDate';
        var ToDate = document.getElementById('ctl00_KdsContent_clnToDate').value;
        if (ToDate == "") {
            document.getElementById("ctl00_KdsContent_vldTo").errormessage = "חובה להזין תאריך";
            $find(sBehaviorId)._ensureCallout();
            $find(sBehaviorId).show(true);
            $('#btnShow').prop("disabled", true);
        
        }
        else {
            var StartDateSplit = document.getElementById('ctl00_KdsContent_clnFromDate').value.split('/');
            var StartDate = new Date(StartDateSplit[2], StartDateSplit[1] - 1, StartDateSplit[0], 0, 0, 0, 0);
            var EndDateSplit = ToDate.split('/');
            var EndtDate = new Date(EndDateSplit[2], EndDateSplit[1] - 1, EndDateSplit[0], 0, 0, 0, 0);

            if (StartDate.getTime() > EndtDate.getTime()) {
                flag = true;
                msg = "עד תאריך קטן מתאריך מ";
            } else if( EndtDate.getTime()>(new Date()))
            {
                flag = true;
                msg = "עד תאריך גדול מתאריך נוכחי";
            }
            if (flag) {
                var sBehaviorId = 'vldExToDate';
                document.getElementById("ctl00_KdsContent_vldTo").errormessage = msg;//"תאריך עד קטן מתאריך מ";
                $find(sBehaviorId)._ensureCallout();
                $find(sBehaviorId).show(true);
                $('#btnShow').prop("disabled", true);
                $('#<%=clnToDate.ClientID %>').focus();
            }
            else
                $('#btnShow').prop("disabled", false);
         
        }
    }
    
   
    //$(document).ready(function () {
    //  //  alert($('#<%=grdEmployee.ClientID %>'));
  //  $('#<%=grdEmployee.ClientID %>').Scrollable({

    //    ScrollHeight: 100,

    //    IsInUpdatePanel: true

    //});
   // });
    //function btnHidden_OnClick() {
        
     

    //    $("[src*=Plus]").on("click", function () {

    //        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")

    //        $(this).attr("src", "../../Images/minus.png");

    //    });

       


            

    //    $("[src*=minus]").on("click", function () {

    //        $(this).attr("src", "../../Images/Plus.png");

    //        $(this).closest("tr").next().remove();

    //    });
    //}

</script>
</asp:Content>