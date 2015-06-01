<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" CodeBehind="NochechutMerukezet.aspx.cs" Inherits="KdsWebApplication.Modules.Ovdim.NochechutMerukezet" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="KdsLibrary" Namespace="KdsLibrary.Controls" TagPrefix="KdsCalendar"%>
<%@ Register TagPrefix="kds" TagName="GridViewPager" Src="~/UserControls/GridViewPager.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

  
    <style type="text/css">
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
       .ItemRowGrid
{
 border-right:hidden;
 border-left:hidden;
 border-bottom:hidden;
 padding-right:3px;
 font-size:14px;
 font-family:Arial;
   font-weight:bold;
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">
  
     <script src="../../Js/GeneralFunction.js" type="text/javascript"></script>

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
                 <KdsCalendar:KdsCalendar runat="server" ID="clnFromDate"  AutoPostBack="false"  dir="rtl" PopupPositionCallOut="Left" ></KdsCalendar:KdsCalendar>                            
            </td> 
            <td style="width:10px"></td>
            <td class="InternalLabel" style="width:70px"> עד תאריך:</td> 
            <td align="right" dir="ltr" style="width:160px"> 
                 <KdsCalendar:KdsCalendar runat="server" ID="clnToDate"  AutoPostBack="false"  dir="rtl" PopupPositionCallOut="Left" ></KdsCalendar:KdsCalendar>                            
            </td> 
            <td style="width:10px"></td>

            <td style="width:150px;">&nbsp;&nbsp;
                <input type="radio" id="rdoAll" onclick="RbChange()" value="1" checked="checked"    name="grpCardType" />כל העובדים הכפופים 
              </td>
              <%--  <asp:RadioButton runat="server"  ID="rdoAll" Checked="true"   OnClick="RbChange()"    GroupName="grpCardType" Text="כל העובדים הכפופים"> </asp:RadioButton></td>   --%>
             <td> 
                    <input type="radio" id="rdoMi" onclick="RbChange()"  value="2"    name="grpCardType"  />מספר אישי
                 <%-- <asp:RadioButton runat="server" ID="rdoMi"   GroupName="grpCardType"  OnClick="RbChange()"  Text="מספר אישי">   --%>     <%-- </asp:RadioButton>--%>
            </td>                          
          <td>
                 <asp:TextBox ID="txtId" runat="server" AutoComplete="Off" dir="rtl" disabled="true"   MaxLength="5" style="width:100px;" TabIndex="1"></asp:TextBox>                            
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
                        <asp:button ID="btnShow" runat="server" text="הצג" CssClass ="ImgButtonSearch"  onclick="btnShow_Click"  />
                   </ContentTemplate>
              </asp:UpdatePanel> 
             
               </td>
            
        </tr>                
    </table>  
        </fieldset>
 <br />
    <table > 
        <tr style="border:none">
            <td  style="border:none">          
                <asp:UpdatePanel ID="udGrid"  runat="server" RenderMode="Inline" UpdateMode="Always"  >
                     <ContentTemplate> 
                      <%--  <asp:Button ID="btnRedirect" runat="server"  OnCommand="btnRedirect_Click"  />
                        <asp:textbox  ID="txtRowSelected" runat="server"  />--%>
                           <div id="divNetunim" runat="server" dir="ltr" style="text-align:right;width:970px;overflow-x:hidden;overflow-y:scroll;height:500px;border:none">
                        <asp:GridView ID="grdEmployee" runat="server"  ShowHeader="False"
                             AllowPaging="true" PageSize="6" AutoGenerateColumns="false" CssClass="Grid"  
                             Width="965px" EmptyDataText="לא נמצאו נתונים!"  
                             OnRowDataBound="grdEmployee_RowDataBound" OnPageIndexChanging="grdEmployee_PageIndexChanging">
                            <Columns>

                                 <asp:TemplateField>

                                    <ItemTemplate>

                                        <img alt = "" style="cursor: pointer;display: none" src="../../Images/minus.png" />
                         
                                        <asp:Panel ID="pnlNochechut" runat="server" Style="display: none">

                                            <asp:GridView ID="gvNochechut" runat="server" CssClass="GridChild"  Width="960px"   OnRowDataBound="gvNochechut_RowDataBound" AutoGenerateColumns="false">

                                                <Columns>

                                                    <asp:HyperLinkField DataTextField="taarich" HeaderText="תאריך"   ItemStyle-CssClass="ItemRowChild" HeaderStyle-CssClass="GridHeaderChild"  NavigateUrl="#" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="mispar_sidur" HeaderText="מספר סידור"   ItemStyle-CssClass="ItemRowChild" HeaderStyle-CssClass="GridHeaderChild" ItemStyle-HorizontalAlign="Center" />           
                                                    <asp:BoundField DataField="TEUR_SIDUR_MEYCHAD"  ItemStyle-Width="0px" />
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
                                                    <asp:BoundField DataField="SHAT_HATCHALA_LETASHLUM" HeaderText="שעת התחלה לתשלום" ItemStyle-Width="0px"   ItemStyle-CssClass="ItemRowChild" HeaderStyle-CssClass="GridHeaderChild" DataFormatString="{0:HH:mm dd/MM}" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:TemplateField ItemStyle-CssClass="ItemRowChild" HeaderStyle-CssClass="GridHeaderChild" ItemStyle-HorizontalAlign="Center" HeaderText="שעת התחלה לתשלום" >
                                                       <ItemTemplate>
                                                           <asp:Label ID="HShatHatchalaLetashlum" runat="server" Font-Bold="true"></asp:Label>
                                                           <asp:Label ID="TShatHatchalaLetashlum" runat="server"  ></asp:Label>
                                                       </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="SHAT_GMAR_LETASHLUM" HeaderText="שעת גמר לתשלום" ItemStyle-Width="0px"     ItemStyle-CssClass="ItemRowChild" HeaderStyle-CssClass="GridHeaderChild" DataFormatString="{0:HH:mm dd/MM}" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:TemplateField ItemStyle-CssClass="ItemRowChild" HeaderStyle-CssClass="GridHeaderChild" ItemStyle-HorizontalAlign="Center" HeaderText="שעת גמר לתשלום" >
                                                       <ItemTemplate>
                                                           <asp:Label ID="HShatGmarLetashlum" runat="server" Font-Bold="true"></asp:Label>
                                                           <asp:Label ID="TShatGmarLetashlum" runat="server"  ></asp:Label>
                                                       </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="TEUR_DIVUCH" HeaderText="חריגה"   ItemStyle-CssClass="ItemRowChild" HeaderStyle-CssClass="GridHeaderChild" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="LO_LETASHLUM"  ItemStyle-Width="0px" />
                                                

                                                    <asp:TemplateField HeaderText="לא לתשלום"    ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="ItemRowChild" HeaderStyle-CssClass="GridHeaderChild"  >
                                                         <ItemTemplate>
                                                                <asp:Image runat="server" ID="imgOK1" Visible="false"   ImageUrl="~/Images/tick.png" />
                                                          <%-- <asp:HyperLink  ID="imgButton" runat="server" NavigateUrl="#" ></asp:HyperLink>--%>
                                                         </ItemTemplate>           
                                                    </asp:TemplateField>  
                                                  
                                                    <asp:BoundField DataField="noch_codshit" HeaderText="נוספות חודשי"   ItemStyle-CssClass="ItemRowChild" HeaderStyle-CssClass="GridHeaderChild" ItemStyle-HorizontalAlign="Center" />
   

                                                </Columns>

                                            </asp:GridView>

                                        </asp:Panel>

                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:BoundField DataField="mispar_ishi"   ItemStyle-CssClass="GridHeader"   HeaderStyle-CssClass="GridHeader"     HeaderText="מספר אישי"  ItemStyle-HorizontalAlign="Center" />                                          
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
                     </ContentTemplate>
                     
                 </asp:UpdatePanel>      
            </td>     
        </tr>           
   </table>
 
<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>

<script type="text/javascript">

    function openDetails() {

      

        items = $("[src*=minus]");

        items.each(function () {
            $(this).closest("tr").after("<tr  Width='960px'  class='trGridChild'><td class='ItemRowChild'></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")

           // $(this).attr("src", "../../Images/minus.png");
            $(this).closest("tr").next().css('visibility', 'visible');

        });
    }
    function RbChange() {
      
        var selected = $("input[type='radio'][name='grpCardType']:checked"); 
        var selectedVal = selected.val();
        var txtIdObj = $('#<%=txtId.ClientID %>');
        if (selectedVal == 1) {
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
        document.getElementById("ctl00_KdsContent_btnShow").click();
        return ReturnWin;
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