<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HosafatPeiluyot.aspx.cs" Inherits="Modules_Ovdim_HosafatPeiluyot" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>חיפוש והוספת פעילויות</title>
     <script src='../../js/jquery.js' type='text/javascript'></script>
    <link id="Link1" runat="server" href="~/StyleSheet.css" type="text/css" rel="stylesheet" />
     <base target="_self" /> 
 </head>
<body dir="rtl" onload="return window_onload()"  onkeydown="if (event.keyCode==107) {event.keyCode=9; return event.keyCode }" >
    <form id="form1" runat="server">
     <script type="text/javascript" language="javascript">
         var col_HosefPeilut = "<%=HOSEF_PEILUT %>";
         var col_MisRechev = "<%=MISPAR_RECHEV %>";
         var col_Mispar_Knisa = "<%=MISPAR_KNISA %>";
         var col_Makat = "<%=MAKAT %>";
         var col_DakotHagdara = "<%=DAKOT_HAGDARA %>";
   
         var iRowIndexNochehi = 0;
         var bShowMassage = false;

        

    </script>
     <asp:ScriptManager  runat="server"  id="ScriptManagerKds" EnablePartialRendering="true" EnablePageMethods="true">        
      <Scripts >
        <asp:ScriptReference Path="~/Js/String.js" />
        <asp:ScriptReference Path="~/Js/GeneralFunction.js" />
        <asp:ScriptReference Path="~/Js/jquery.js" />
          <asp:ScriptReference Path="../../Js/HosafatPeiluyot.js" />
       </Scripts>
      </asp:ScriptManager>
               
       <asp:UpdateProgress  runat="server" id="UpdateProgress1" DisplayAfter="0" >
            <ProgressTemplate>
                <div id="divProgress" class="Progress"  style="text-align:right;position:absolute;left:52%;top:48%; z-index:1000"   >
                      <asp:Image ID="Image2" runat="server" ImageUrl="../../Images/progress.gif" style="width: 100px; height: 100px" />
                </div>        
            </ProgressTemplate>
       </asp:UpdateProgress>
     <table style="width:100%" >
              <tr class="GridHeader"><td > חיפוש והוספת פעילויות</td></tr>
            <tr>
                <td>
               
                    <fieldset  style="height:42px" >  <legend   id="LegendFilter" style="background-color:White" >סוג פעילות לחיפוש </legend> 
                            &nbsp;&nbsp;
                            <input type="button" id="btnHosafatNesiaa" class="ImgButtonSearch"  value="חיפוש נסיעה מקטלוג"  style="width:160px" onclick="btnHosafatNesiaa_Click();" tabindex="1" />
                             &nbsp;&nbsp;
                            <input type="button" id="btnElement" class="ImgButtonSearch" value="חיפוש אלמנט" style="width:120px" onclick="btnElement_Click();" tabindex="2"  />
                        
                    </fieldset>  
                </td>
            </tr>
      <tr><td>
 
         <div id="divHosafatElement" runat="server" >
             <fieldset class="FilterFieldSet"   >
                 <asp:UpdatePanel ID="upRdoId" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                            <ContentTemplate> 
                               <table border="0" cellpadding="0" cellspacing="0" >
                                   <tr>
                                        <td style="width:23%" > 
                                             <asp:RadioButton runat="server" Checked="true" ID="rdKod"  EnableViewState="true" GroupName="grpSearch" Text="קוד אלמנט:" tabindex="3"  > </asp:RadioButton>                                        
                                             <asp:TextBox ID="txtMisparElement" runat="server"  width="80px" MaxLength="2" onChange="CheckKodElement()"  tabindex="4"  ></asp:TextBox>
                                                  <cc1:AutoCompleteExtender id="AutoMisElement"  runat="server" CompletionInterval="0" CompletionSetCount="25" UseContextKey="false"  
                                                    TargetControlID="txtMisparElement" MinimumPrefixLength="1" ServiceMethod="GetElements" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                                    EnableCaching="true"  CompletionListCssClass="autocomplete_completionListElement" 
                                                    CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select" OnClientHidden ="OnShown"
                                                    CompletionListItemCssClass="autocomplete_completionListItemElement"   >                               
                                                </cc1:AutoCompleteExtender>  
                                             <asp:CustomValidator runat="server" id="vldKod"   ControlToValidate="txtMisparElement" ErrorMessage=""   Display="None"   ></asp:CustomValidator>
                                             <cc1:ValidatorCalloutExtender runat="server" ID="vldExK" BehaviorID="vldExKod"   TargetControlID="vldKod" Width="200px" PopupPosition="Left"  ></cc1:ValidatorCalloutExtender>                                                
                                        </td>
                                        <td style="width:33%">  
                                             <asp:RadioButton runat="server" ID="rdTeur" EnableViewState="true"  GroupName="grpSearch" Text="תאור אלמנט:" tabindex="5"  > </asp:RadioButton>
                                             <asp:TextBox ID="txtTeurElement" runat="server" onChange="CheckTeurElement()"  width="141px" tabindex="6"  ></asp:TextBox>
                                                 <cc1:AutoCompleteExtender id="AautoTeurElement"  runat="server" CompletionInterval="0" CompletionSetCount="25" UseContextKey="false"  
                                                    TargetControlID="txtTeurElement" MinimumPrefixLength="1" ServiceMethod="GetTeurElements" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                                    EnableCaching="true"  CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                                                    CompletionListItemCssClass="autocomplete_completionListItemElement">                               
                                                </cc1:AutoCompleteExtender>   
                                             <asp:CustomValidator runat="server" id="vldTeur" ControlToValidate="txtTeurElement" ErrorMessage=""   Display="None"   ></asp:CustomValidator>
                                             <cc1:ValidatorCalloutExtender  runat="server" ID="vldExT" BehaviorID="vldExTeur"   TargetControlID="vldTeur" Width="200px" PopupPosition="Right" ></cc1:ValidatorCalloutExtender>                                                
                                        </td>
                                        <td style="width:19%"> 
                                             <label for="txtSugElement" class="InternalLabel">סוג אלמנט :</label>
                                             <asp:TextBox ID="txtSugElement" runat="server" Enabled="false" width="60px" tabindex="7"  ></asp:TextBox>
                                          
                                        </td>
                                        <td style="width:15%"> 
                                             <label for="txtErechElement" class="InternalLabel">&nbsp;&nbsp;&nbsp;&nbsp;  ערך :</label>
                                             <asp:TextBox ID="txtErechElement" runat="server"  onChange="CheckErechLeElement()" tabindex="8"   width="55px" MaxLength="3" onfocus="document.getElementById('btnSgor').style.border='none';"  ></asp:TextBox>
                                             <asp:CustomValidator runat="server" id="vldErech" ControlToValidate="txtErechElement" ErrorMessage=""   Display="None"   ></asp:CustomValidator>
                                             <cc1:ValidatorCalloutExtender  runat="server" ID="VceErech" BehaviorID="vldExErech"   TargetControlID="vldErech" Width="200px" PopupPosition="Right" ></cc1:ValidatorCalloutExtender>                                                
                                       
                                        </td>
                                        <td> &nbsp;  <asp:Button ID="btnAdd" runat="server" Text="הוסף"  style="width:auto;" tabindex="9"  OnClick="btnAddElement_OnClick" onblur="this.style.border='none';"   CssClass="ImgButtonSearch" OnClientClick="if (!checkMustErech()) return false; else return true;" CausesValidation="false"  />
                                        </td>
                                     </tr>
                               </table> 
                        </ContentTemplate>
                    </asp:UpdatePanel>
              </fieldset>
            </div> 
         
           <div id="divPeilutKatalog" runat="server" style="display:none">
            <fieldset class="FilterFieldSet">
                         <asp:UpdatePanel ID="UpKatalog" runat="server" UpdateMode="Conditional" >
                                    <ContentTemplate> 
                                         <table >
                                            <tr>
                                            <td><label for="txtMakat" class="InternalLabel"> מק''ט:</label></td>
                                             <td  width="150px"> <asp:TextBox ID="txtMakat" runat="server"     Enabled="true" width="125px" MaxLength="8" ></asp:TextBox>
                                                  <asp:CustomValidator runat="server" id="vldMakat" ControlToValidate="txtMakat" ErrorMessage=""   Display="None"   ></asp:CustomValidator>
                                                  <cc1:ValidatorCalloutExtender  runat="server" ID="exvldMakat" BehaviorID="vldExMakat"   TargetControlID="vldMakat" Width="200px" PopupPosition="Left" ></cc1:ValidatorCalloutExtender>                                                
                                               </td>
                                              <td><asp:Button ID="btnAddFromKatalog" runat="server" Text="הוסף"   style="width:auto;" OnClick="btnAddNesia_OnClick"   CssClass="ImgButtonSearch" OnClientClick="if (!checkMakat()) return false; else return true;"
                                                onfocusout="this.style.border='none';" />
                                                </td>
                                            </tr>
                                         </table>
                                          
                                         
                                 </ContentTemplate>
                            </asp:UpdatePanel>
                      </fieldset>
             </div>
             <div style="text-align:center" >
              <br />     
              <asp:UpdatePanel ID="upTblPeiluyot" runat="server" UpdateMode="Always" >
                  <ContentTemplate>  
                    <asp:Panel ID="pnlgrdPeiluyot"    height="410px" width="845px" dir="ltr"  runat="server" ScrollBars="Auto" >   
                 <div  id="dd" runat="server" dir="rtl" onkeydown="if (event.keyCode==13){return false}" >
                   <asp:GridView ID="grdPeiluyot" runat="server" GridLines="None" 
                                 AutoGenerateColumns="False" width="827px"
                                 ShowHeader="true" 
                                 HeaderStyle-CssClass="GridHeaderSecondary"
                                 OnRowDataBound="grdPeiluyot_RowDataBound" >
                              <Columns>
                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="true" ItemStyle-CssClass="ItemRow" HeaderText="כיסוי תור" >
                                       <ItemTemplate>
                                             <asp:TextBox ID="txtKisuiTor" runat="server" Width="40px"></asp:TextBox>
                                             <input type="hidden" id="KisuyTorHidden" runat="server" />
                                             <cc1:MaskedEditExtender ID="extKisuiTor" runat="server" TargetControlID="txtKisuiTor" MaskType="Time" UserTimeFormat="TwentyFourHour" Mask="99:99"  ></cc1:MaskedEditExtender>
                                             <asp:RegularExpressionValidator  runat="server" id="vldKisuiTor" EnableClientScript="true"  Display="none" ErrorMessage="יש להקליד שעת יציאה בטווח 00:00-23:59" ControlToValidate="txtKisuiTor"   ValidationExpression="^([0-1]?\d|2[0-3])(:[0-5]\d){1,2}$"></asp:RegularExpressionValidator>
                                             <cc1:ValidatorCalloutExtender runat="server" ID="exvldKisuiTor" BehaviorID="vldExvldKisuiTor"  TargetControlID="vldKisuiTor" Width="200px" PopupPosition="Left"></cc1:ValidatorCalloutExtender>         
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                    <asp:BoundField DataField="SHAT_YETZIA" ItemStyle-CssClass="ItemRow"  />
                                   <asp:TemplateField HeaderStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="true" ItemStyle-CssClass="ItemRow" HeaderText="שעת יציאה" >
                                       <ItemTemplate>
                                            <asp:TextBox ID="txtShatYezia" runat="server" Width="40px"  ></asp:TextBox>
                                               <cc1:MaskedEditExtender ID="extMaskShatYezia" runat="server" TargetControlID="txtShatYezia" MaskType="Time" UserTimeFormat="TwentyFourHour" Mask="99:99"  ></cc1:MaskedEditExtender>
                                              <asp:RegularExpressionValidator  runat="server" id="vldShatYezia" EnableClientScript="true"  Display="none" ErrorMessage="יש להקליד שעת יציאה בטווח 00:00-23:59" ControlToValidate="txtShatYezia"   ValidationExpression="^([0-1]?\d|2[0-3])(:[0-5]\d){1,2}$"></asp:RegularExpressionValidator>
                                              <cc1:ValidatorCalloutExtender runat="server" ID="exvldShatYezia" BehaviorID="vldExvldShatYezia"  TargetControlID="vldShatYezia" Width="200px" PopupPosition="Left"></cc1:ValidatorCalloutExtender>     
                                        </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:BoundField DataField="TEUR" HeaderText="תיאור" ItemStyle-CssClass="ItemRow" ItemStyle-HorizontalAlign="Right" />
                                  
                                    <asp:BoundField DataField="KAV" HeaderText="קו"  ItemStyle-CssClass="ItemRow"   />
                                  
                                    <asp:BoundField DataField="SUG" HeaderText="סוג" ItemStyle-CssClass="ItemRow"  />
                          
                                    <asp:BoundField DataField="MISPAR_RECHEV" ItemStyle-CssClass="ItemRow"  />
                                   <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="ItemRow" HeaderStyle-Wrap="true" HeaderText="מספר רכב" >
                                       <ItemTemplate>
                                            <asp:TextBox ID="txtMisRechev" runat="server"  MaxLength="6" Width="42px"></asp:TextBox>
                                           <asp:TextBox ID="lblMisparRishuy" runat="server" ></asp:TextBox>
                                            <asp:CustomValidator runat="server" id="vldMisRechev" ControlToValidate="txtMisRechev" ErrorMessage=""   Display="None"   ></asp:CustomValidator>
                                             <cc1:ValidatorCalloutExtender runat="server" ID="exvMisRechev" BehaviorID="vldExvldMisRechev"  TargetControlID="vldMisRechev" Width="200px" PopupPosition="Left"></cc1:ValidatorCalloutExtender>  
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                  <asp:BoundField DataField="MAKAT"  ItemStyle-CssClass="ItemRow" HeaderStyle-Wrap="true" HeaderText="מק''ט" />
                                    <asp:BoundField DataField="DAKOT_HAGDARA" HeaderText="דקות הגדרה" ItemStyle-CssClass="ItemRow"  />
                                 
                                  <asp:BoundField DataField="DAKOT_BAFOAL" ItemStyle-CssClass="ItemRow"  />
                                   <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="ItemRow"  HeaderText="דקות בפועל" >
                                       <ItemTemplate>
                                            <asp:TextBox ID="txtDakotBafoal" runat="server"  Width="30px"></asp:TextBox>
                                            <asp:CustomValidator runat="server" id="vldDakot" ControlToValidate="txtDakotBafoal" ErrorMessage=""   Display="None"   ></asp:CustomValidator>
                                            <cc1:ValidatorCalloutExtender runat="server" ID="exvdakot" BehaviorID="vldExvldDakot"  TargetControlID="vldDakot" Width="200px" PopupPosition="Right"></cc1:ValidatorCalloutExtender>  
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                     <asp:BoundField DataField="HOSEF_PEILUT" ItemStyle-CssClass="ItemRow"  />
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="ItemRowLast"
                                             HeaderStyle-Wrap="true"  >
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
                                       <asp:BoundField DataField="IS_VALID_MIS_RECHEV" ItemStyle-CssClass="ItemRow"  />
                                      <asp:TemplateField  >
                                           <ItemTemplate>
                                                <asp:TextBox ID="txtIsMisRechevValid" runat="server" Width="40px"></asp:TextBox>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:BoundField DataField="SHAT_YEZIA_DATE" ItemStyle-CssClass="ItemRow"  />
                                      <asp:TemplateField  >
                                           <ItemTemplate>
                                                <asp:TextBox ID="txtShatYeziaDate" runat="server"></asp:TextBox>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                        <asp:BoundField DataField="MISPAR_RISHUY" ItemStyle-CssClass="ItemRow"  />
                                       <asp:BoundField DataField="MISPAR_KNISA" ItemStyle-CssClass="ItemRow"  />
                                       <asp:BoundField DataField="ROW" ItemStyle-CssClass="ItemRow"  />
                                       <asp:BoundField DataField="SUG_KNISA" ItemStyle-CssClass="ItemRow"  />
                                    </Columns>
                                    <AlternatingRowStyle CssClass="GridAltRow" />
                                    <RowStyle CssClass="GridRow" />
                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                            </asp:GridView>
                            </div>
                            </asp:Panel>
                             <input type="button" ID="btnShowMessage" runat="server" onclick="btnShowMessage_Click()" style="display: none;" />
                        <cc1:ModalPopupExtender ID="ModalPopupEx" DropShadow="false" X="300" Y="200" PopupControlID="paMessage"
                            TargetControlID="btnShowMessage"  runat="server">
                        </cc1:ModalPopupExtender>
                        <asp:Panel runat="server" Style="display: none" ID="paMessage" CssClass="PanelMessage" Width="350px">
                             <asp:Label ID="lblHeaderMessage" runat="server" Width="97%" BackColor="#696969" ForeColor="White">קביעת תאריך</asp:Label>
                            <br />
                            <br />
                            <br />
                            יש לקבוע את תאריך שעת היציאה:
                            <br />
                            <br />
                            <input type="button" ID="btnNochachi" runat="server" value="יום נוכחי" class="ImgButtonMake"
                                Width="150px" onclick="btnNochachi_click()" CausesValidation="false" />
                            <input type="button" ID="btnHaba"  runat="server" onclick="btnHaba_click()"  value="יום הבא" class="ImgButtonMake"
                                Width="150px" CausesValidation="false" /></asp:Panel>
                     </ContentTemplate>
                 </asp:UpdatePanel>
             </div>
             <br />
              <asp:UpdatePanel ID="UPbuttons" runat="server" UpdateMode="Always">
                 <ContentTemplate>      
                        <table width="98%">
                          <tr>
                              <td align="right"  style="width:50%"> &nbsp; &nbsp;
                                     <input id="btnSgor" type="button" class="ImgButtonSearch" value="סגור" onclick="window.returnValue = ''; window.close();" onblur="on_blur_btn(this);"/>   
                                 </td>
                                <td align="left" style="width:50%">
                                <asp:Button ID="btnHosafa" runat="server"   style="display:none;width:228px"   Text="הוסף פעילויות לכרטיס עבודה"  OnClick="btnHosafa_OnClick" 
                                  CssClass="ImgButtonSearch" OnClientClick="if (!checkFileds()) return false; else return true;" onblur="on_blur_btn(this);" />
                                 </td>
                            </tr>
                          </table>
                 </ContentTemplate>
           </asp:UpdatePanel> 
           <br />
        <input id="Sug_Peilut" name="Sug_Peilut" runat="server" type="hidden"  />
        <input id="txtHiddenMisparSidur" name="txtHiddenMisparSidur" runat="server" type="hidden" />
        <input id="txtHiddenTaarichCA" name="txtHiddenTaarichCA" runat="server" type="hidden"  />
         <input id="txtHiddenHourHatchaltSidur" name="txtHiddenHourHatchaltSidur" runat="server" type="hidden" />
        <input id="txtHiddenMisparIshi" name="txtHiddenMisparIshi" runat="server" type="hidden" />
         <input id="ElementsRelevants" name="ElementsRelevants" runat="server" type="hidden"  />
         <input id="MustErech" name="MustErech" runat="server" type="hidden"  />
         <input id="MustRechev" name="MustRechev" runat="server" type="hidden"  />
         <input type="hidden" id="Params" name="Params"  runat="server" />
          <input type="hidden" id="DestTime" name="DestTime"  runat="server" />
           <input id="SavePeilut" name="SavePeilut" runat="server" type="hidden"  />
             <input type="hidden" id="txtSidurDate" name="SidurDate"  runat="server" />
              <input type="hidden" id="txtMisRechev" name="txtMisRechev"  runat="server" />
                 <input type="hidden" id="txtMisRishuy" name="txtMisRechev"  runat="server" />
         </td></tr>
          </table>
       
    </form>
   
      
</body>
</html>
