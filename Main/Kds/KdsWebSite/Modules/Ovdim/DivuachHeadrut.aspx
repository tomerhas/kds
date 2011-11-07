<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DivuachHeadrut.aspx.cs" Inherits="Modules_Ovdim_DivuachHeadrut" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="KdsLibrary" Namespace="KdsLibrary.Controls" TagPrefix="KdsCalendar"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>דיווח העדרות</title>
     <script src='../../js/jquery.js' type='text/javascript'></script>
      <link id="Link1" runat="server" href="~/StyleSheet.css" type="text/css" rel="stylesheet" />
      <base target="_self" />
</head>
<body  onkeydown="return ChangeKeyCode(event.keyCode);"> 
<script type="text/javascript">
    var selectedSidur;
    var DateCard = "<%=sDateCard %>";

    function ChangeKeyCode(keyCode) {
        // debugger;
        if (keyCode == 107) {
            event.keyCode = 9;
            return event.keyCode;
        }
        else if (keyCode == 110) {
            if (document.getElementById("btnUpdate").disabled == false)
                document.getElementById("btnUpdate").focus();
        }
    }
//   function window.onload() {
//        if (window.dialogArguments != undefined) {
//            if (window.dialogArguments.document.getElementById("divHourglass") != null)
//                window.dialogArguments.document.getElementById("divHourglass").style.display = 'none';
//        }
//    }   
   function HideShaotRow(selectedItem) {
        selectedSidur = selectedItem;
        if ($(selectedItem).attr('value') == -1) {
            document.all("btnUpdate").disabled = true;
            document.all("btnUpdate").className = "btnWorkCardLongDis";
            document.all("txtStartTime").value = "";
            document.all("txtEndTime").value = "";
            document.all("txtStartTime").disabled = true;
            document.all("txtEndTime").disabled = true;
            $("#drHeaara").css("display", "none");
            $("#drTaarichAd").css("display", "none");
            $("#drVldTaarichAd").css("display", "none");
            ValidatorEnable(document.all("vldStartTime"), false);
            ValidatorEnable(document.all("vldEndTime"), false);
            ValidatorEnable(document.all("vldReqStartTime"), false);
            ValidatorEnable(document.all("vldReqEndTime"), false);
            ValidatorEnable(document.all("vldEndHeadrut"), false);
        }
        else {

            if ($(selectedItem).attr('headrut_hova_ledaveach_shaot') == "True") {
                document.all("txtEndTime").value = "";
                document.all("txtStartTime").value = "";
            }
            else {
                document.all("txtEndTime").value = $(selectedItem).attr('max_gmar_muteret').substring(11, 16);
                document.all("txtStartTime").value = $(selectedItem).attr('max_shat_hatchala').substring(11, 16);
            }
            document.all("btnUpdate").disabled = false;
            document.all("btnUpdate").className = "btnWorkCardLong";
            document.all("txtStartTime").disabled = false;
            document.all("txtEndTime").disabled = false;
            document.all("clnEndDateHeadrut").disabled = false;
           
            ValidatorEnable(document.all("vldCalDate_clnEndDateHeadrut"), false);
            ValidatorEnable(document.all("vldEndHeadrut"), false);
            document.all("vldStartTime").errormessage = "שעת התחלה מותרת מ - " + $(selectedItem).attr('max_shat_hatchala').substring(11, 16);
            document.all("vldEndTime").errormessage = "שעת גמר מותרת עד - " + $(selectedItem).attr('max_gmar_muteret').substring(11, 16);

            ValidatorEnable(document.all("vldStartTime"), true);
            ValidatorEnable(document.all("vldEndTime"), true);
           ValidatorEnable(document.all("vldReqStartTime"), true);
            ValidatorEnable(document.all("vldReqEndTime"), true);
               
           
            
            if ($(selectedItem).attr('nitan_ledaveach_ad_taarich') == "True") {
                $("#drHeaara").css("display", "");
                $("#drTaarichAd").css("display", "");
                $("#drVldTaarichAd").css("display", "");
                document.all("clnEndDateHeadrut").value = "";
                ValidatorEnable(document.all("vldCalDate_clnEndDateHeadrut"), true);
                ValidatorEnable(document.all("vldEndHeadrut"),true);
                ValidatorEnable(document.all("vldStartTime"), false);
                ValidatorEnable(document.all("vldEndTime"), false);
                ValidatorEnable(document.all("vldReqStartTime"), false);
                ValidatorEnable(document.all("vldReqEndTime"), false);
            }
            else {
                $("#drHeaara").css("display", "none");
                $("#drTaarichAd").css("display", "none");
                $("#drVldTaarichAd").css("display", "none");
            }

            document.all("txtStartTime").focus();
        }
        
    }

    function CheckDateStart(val, args) {
        var dStartTime = $(selectedSidur).attr('max_shat_hatchala');
        var dSidurDate = new Date(Number(DateCard.substr(6, 4)), Number(DateCard.substr(3, 2)) - 1, Number(DateCard.substr(0, 2)), Number(args.Value.substring(0, 2)), Number(args.Value.substring(3, 5)));
        var dSidurToCompar = new Date(Number(dStartTime.substr(6, 4)), Number(dStartTime.substr(3, 2)) - 1, Number(dStartTime.substr(0, 2)), Number(dStartTime.substring(11, 13)), Number(dStartTime.substring(14, 16)));
        if (dSidurDate < dSidurToCompar) {
             args.IsValid = false; }
        else {
            args.IsValid = true;
        }
    }

    function CheckValidHour(val, args) {
        if (args.Value != "__:__") {
            var rgExpression = new RegExp("^([0-1]?[0-9]|2[0-4]):([0-5][0-9])(:[0-5][0-9])?$");
            if (!rgExpression.test(args.Value)) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        } else {
            args.IsValid = true;
        }
    }
    
    function CheckDateEnd(val, args) {
        var dEndTime = $(selectedSidur).attr('max_gmar_muteret');
        var dSidurDate = new Date(Number(DateCard.substr(6, 4)), Number(DateCard.substr(3, 2)) - 1, Number(DateCard.substr(0, 2)), Number(args.Value.substring(0, 2)), Number(args.Value.substring(3, 5)));
        if ((Number(args.Value.substring(0, 2))>1 && Number(args.Value.substring(0, 2))<8) || (Number(args.Value.substring(0, 2))==0 && Number(args.Value.substring(3, 5))==1))
        { dSidurDate.setDate(dSidurDate.getDate() + 1); }
        var dSidurToCompar = new Date(Number(dEndTime.substr(6, 4)), Number(dEndTime.substr(3, 2)) - 1, Number(dEndTime.substr(0, 2)), Number(dEndTime.substring(11, 13)), Number(dEndTime.substring(14, 16)));
         if (dSidurDate > dSidurToCompar) {
            args.IsValid = false;
        }
        else {
            args.IsValid = true;
        }
    }

function CheckDateGreaterStart(val, args) {
    var dStartTime = document.all("txtStartTime").value;
        var dSidurDate = new Date(Number(DateCard.substr(6, 4)), Number(DateCard.substr(3, 2)) - 1, Number(DateCard.substr(0, 2)), Number(args.Value.substring(0, 2)), Number(args.Value.substring(3, 5)));
        if ((Number(args.Value.substring(0, 2)) > 1 && Number(args.Value.substring(0, 2)) < 8) || (Number(args.Value.substring(0, 2)) == 0 && Number(args.Value.substring(3, 5)) == 1))
        { dSidurDate.setDate(dSidurDate.getDate() + 1);  }
         var dSidurToCompar = new Date(Number(DateCard.substr(6, 4)), Number(DateCard.substr(3, 2)) - 1, Number(DateCard.substr(0, 2)), Number(dStartTime.substring(0, 2)), Number(dStartTime.substring(3, 5)));
        if (dSidurDate < dSidurToCompar) {
            args.IsValid = false;
        }
        else {
            args.IsValid = true;
        }
    }

    function EnableButton() {
        if (document.getElementById("clnEndDateHeadrut").value != "__/__/____")
    {
//        if (Page_ClientValidate()) {
            ValidatorEnable(document.all("vldStartTime"), false);
            ValidatorEnable(document.all("vldEndTime"), false);

            //             if (Page_ClientValidate("vldHeadrut")) {
            var dCardDate = new Date(Number(DateCard.substr(6, 4)), Number(DateCard.substr(3, 2)) - 1, Number(DateCard.substr(0, 2)), 0, 0);
            var dSidurDate = new Date(Number(document.getElementById("clnEndDateHeadrut").value.substr(6, 4)), Number(document.getElementById("clnEndDateHeadrut").value.substr(3, 2)) - 1, Number(document.getElementById("clnEndDateHeadrut").value.substr(0, 2)), 0, 0);
            dCardDate = Date.UTC(dCardDate.getFullYear(), dCardDate.getMonth() + 1, dCardDate.getDate(), 0, 0, 0);
            dSidurDate = Date.UTC(dSidurDate.getFullYear(), dSidurDate.getMonth() + 1, dSidurDate.getDate(), 0, 0, 0);

            if (dSidurDate > 0 && dCardDate >= dSidurDate) {
                ValidatorEnable(document.all("vldEndHeadrut"), true);

                document.all("btnUpdate").disabled = true;
                document.all("btnUpdate").className = "btnWorkCardLongDis";
                $find("CalloutEndHeadrut")._ensureCallout();
                $find("CalloutEndHeadrut").show(true);
            }
            else {
                document.all("btnUpdate").disabled = false;
                document.all("btnUpdate").className = "btnWorkCardLong";

                //                }
//            } 
        }
     }
    }

</script>
    <form id="form1" runat="server">
      <asp:ScriptManager  runat="server"  id="ScriptManagerKds" EnablePartialRendering="true" EnablePageMethods="true"  EnableScriptGlobalization="true"  EnableScriptLocalization="true" >        
     </asp:ScriptManager>
    <div style="text-align:center" class="WCard_GridRow">
     <asp:UpdatePanel ID="upBody" runat="server" UpdateMode="Conditional">
          <ContentTemplate>
               <table width="651px" cellpadding="1" cellspacing="1" class= "WorkCardDivuchHeadrutTopTable">
                   <tr style="height:22px" >
                     <td colspan="7" class = "WorkCardTable1Label"> דיווח העדרות</td>
                   </tr>
               </table> 
               <table width="651px" cellpadding="0" cellspacing="0" border="0">   
                   <tr><td width="20px" class="WorkCardTable1Label"></td>
                       <td colspan="6" class="WorkCardTable1Label"></td>
                   </tr>
                   <tr class = "WorkCardTable1Label" style="height:42px; width:651px">
                         <td></td>
                         <td width="27px" >סוג העדרות:</td> 
                         <td colspan="4"  align="right">  
                             <asp:DropDownList ID="ddlHeadrutType" runat="server" CssClass="WorkCardSidurDropDown"  Width="360px" Height="25px" TabIndex="1"  EnableViewState="true" AutoPostBack="false"
                                 OnDataBound="ddlHeadrutType_DataBound" onchange="HideShaotRow(this.options[this.selectedIndex]);" CausesValidation="false"></asp:DropDownList>
                           
                         </td>
                         <td></td>
                   </tr>
                   <tr class="WCard_GridRow" style="height:22px; width:651px"><td  colspan="7" ></td></tr>
                   <tr class="WCard_GridRow" style="height:266px; width:651px">
                        <td width="50px"><br /></td>
                        <td width="130px" valign="top" >שעת התחלה:</td>
                        <td  width="150px" valign="top" align="right">                             
                            <asp:TextBox runat="server" MaxLength="5" ID="txtStartTime" Width="100px" TabIndex="2" CssClass="WorkCardDivuchHeadrutTextBox"  AutoPostBack="false" onblur="if(this.value=='__:__' && $(selectedSidur).attr('headrut_hova_ledaveach_shaot')!='True'){this.value=$(selectedSidur).attr('max_shat_hatchala').substring(11, 16);}" ></asp:TextBox>
                            <cc1:MaskedEditExtender ID="extMaskStartTime"  runat="server" TargetControlID="txtStartTime" MaskType="Time"  UserTimeFormat="TwentyFourHour" Mask="99:99" ClearMaskOnLostFocus="true" ClearTextOnInvalid="true"  >
                            </cc1:MaskedEditExtender><br />
                            <asp:RequiredFieldValidator   ID="vldReqStartTime"  runat="server" Display="None"  SetFocusOnError="true" TabIndex="3" CssClass="ErrorMessage" ErrorMessage="! חובה להזין שעת התחלה " ControlToValidate="txtStartTime"  ></asp:RequiredFieldValidator>                               
                                <asp:CustomValidator  runat="server" id="RgVldsStartTime" EnableClientScript="true" TabIndex="4" SetFocusOnError="true" Display="none" ErrorMessage="שעת התחלה לא תקינה" ControlToValidate="txtStartTime"   ClientValidationFunction="CheckValidHour"></asp:CustomValidator >
                                <asp:CustomValidator ID="vldStartTime"  EnableClientScript="true" TabIndex="5"  SetFocusOnError="true" ErrorMessage="שעת התחלה מותרת מ - "  runat="server" Display="None" ControlToValidate="txtStartTime" ClientValidationFunction="CheckDateStart"></asp:CustomValidator>
                            <cc1:ValidatorCalloutExtender runat="server" ID="exvldReqStartTime" TargetControlID="vldReqStartTime"  Width="220px" PopupPosition="Left"></cc1:ValidatorCalloutExtender>     
                            <cc1:ValidatorCalloutExtender runat="server" ID="exvldStartTime" TargetControlID="vldStartTime" Width="200px" PopupPosition="Left"></cc1:ValidatorCalloutExtender>                                             
                            <cc1:ValidatorCalloutExtender runat="server" ID="exRgVldsStartTime" TargetControlID="RgVldsStartTime" Width="200px" PopupPosition="Left"></cc1:ValidatorCalloutExtender>                                                                    
                        </td>                           
                        <td></td>
                        <td width="80px" valign="top"> שעת גמר: &nbsp;</td>
                        <td width="150px" valign="top"  align="right">                          
                            <asp:TextBox runat="server" MaxLength="5" ID="txtEndTime" Width="100px" AutoPostBack="false" TabIndex="6"  CssClass="WorkCardDivuchHeadrutTextBox"  onblur="if(this.value=='__:__' && $(selectedSidur).attr('headrut_hova_ledaveach_shaot')!='True'){this.value=$(selectedSidur).attr('max_gmar_muteret').substring(11, 16);}"></asp:TextBox>
                                <cc1:MaskedEditExtender ID="extMaskEndTime" runat="server" MaskType="Time" TargetControlID="txtEndTime" UserTimeFormat="TwentyFourHour" Mask="99:99"  ClearMaskOnLostFocus="true">
                                </cc1:MaskedEditExtender><br />
                                    <asp:CustomValidator  runat="server" id="rgVldEndTime" EnableClientScript="true" TabIndex="8" SetFocusOnError="true" Display="none" ErrorMessage="שעה גמר לא תקינה" ControlToValidate="txtEndTime"    ClientValidationFunction="CheckValidHour" ></asp:CustomValidator >
                                        <asp:CustomValidator ID="vldEndTimeGreater" runat="server" EnableClientScript="true" SetFocusOnError="true" TabIndex="9"  ErrorMessage="שעת הגמר צריכה להיות גדולה משעת ההתחלה" Display="none" ControlToValidate="txtEndTime" ClientValidationFunction="CheckDateGreaterStart" ></asp:CustomValidator>
                            
                                    <asp:CustomValidator ID="vldEndTime" runat="server" EnableClientScript="true" SetFocusOnError="true"   ErrorMessage="" Display="none" ControlToValidate="txtEndTime" ClientValidationFunction="CheckDateEnd" ></asp:CustomValidator>
                            <cc1:ValidatorCalloutExtender runat="server" ID="exvldEndTime" TargetControlID="vldEndTime" Width="200px" PopupPosition="Right"></cc1:ValidatorCalloutExtender>                                             
                            <cc1:ValidatorCalloutExtender runat="server" ID="exVldreqEndTime" TargetControlID="vldreqEndTime"  Width="200px" PopupPosition="Right"></cc1:ValidatorCalloutExtender>     
                            <asp:RequiredFieldValidator ID="vldreqEndTime"  runat="server" Display="None"   SetFocusOnError="true" CssClass="ErrorMessage" TabIndex="7" ErrorMessage="! חובה להזין שעת גמר "  ControlToValidate="txtEndTime" ></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender runat="server" ID="exRgVldEndTime" TargetControlID="rgVldEndTime" Width="200px" PopupPosition="Right"></cc1:ValidatorCalloutExtender>                                             
                        <cc1:ValidatorCalloutExtender runat="server" ID="exvldEndTimeGreater" TargetControlID="vldEndTimeGreater" Width="200px" PopupPosition="Right"></cc1:ValidatorCalloutExtender>                                                                    
                        </td>
                           
                        <td width="60px"></td>
                      </tr>                      
                      <tr class="WorkCardDivuchHeadrutMiddleTable" id="drHeaara" runat="server" style="width:651px">
                        <td  width="50px"><br /></td>
                        <td colspan="5" align="right">הסידור שבחרת יתווסף לכרטיס העבודה אותו פתחת.  ניתן להאריך את ההיעדרות על ידי בחירת תאריך סיום היעדרות מתוך התאריכון</td>
                        <td  width="60px"></td>
                      </tr>                    
                     <tr class="WorkCardDivuchHeadrutMiddleTable" id="drTaarichAd" runat="server" style="width:651px">
                            <td colspan="3" valign="top" align="left"> תאריך סיום היעדרות:</td>
                            <td colspan="4" dir="ltr" align="right">
                              <KdsCalendar:KdsCalendar runat="server" ID="clnEndDateHeadrut"  TabIndex="10"    AutoPostBack="false"  dir="rtl" CausesValidation="false" PopupPositionCallOut="left"  ></KdsCalendar:KdsCalendar> 
                            <%--<wccEgged:wccCalendar  runat="server" ID="clnEndDateHeadrut"  BasePath="../../EggedFramework" AutoPostBack="false" Width="80px" dir="rtl" CausesValidation="false"></wccEgged:wccCalendar>--%>                                                                      
                            </td>                            
                      </tr>
                     <tr class="WorkCardDivuchHeadrutMiddleTable" id="drVldTaarichAd" runat="server" style="width:651px">
                        <td  width="50px"><br /></td>
                        <td colspan="6"> 
                     <%-- <asp:RequiredFieldValidator ID="vldReqEndDateHeadrut" EnableClientScript="true"  runat="server" Display="None" CssClass="ErrorMessage"   ErrorMessage="חובה להזין מתאריך !" ControlToValidate="clnEndDateHeadrut"></asp:RequiredFieldValidator>
                     --%>   <%--<asp:CompareValidator ID="vldTypeEndDateHeadrut" EnableClientScript="true" runat="server"  Display="None" ControlToValidate="clnEndDateHeadrut" ErrorMessage="  תאריך סיום העדרות לא תקין !" Type="Date" Operator="DataTypeCheck"></asp:CompareValidator>--%>
                          <asp:CustomValidator ID="vldEndHeadrut" runat="server" Display="None"  EnableClientScript="true" ControlToValidate="clnEndDateHeadrut"  ErrorMessage="יש לבחור תאריך גדול מתאריך כרטיס העבודה"></asp:CustomValidator>
                          <cc1:ValidatorCalloutExtender runat="server" ID="exEndHeadrut" TargetControlID="vldEndHeadrut" BehaviorID="CalloutEndHeadrut" Width="200px" PopupPosition="Left"></cc1:ValidatorCalloutExtender>                                                
                              <%--<cc1:ValidatorCalloutExtender runat="server" ID="exTypeEndDateHeadrut" TargetControlID="vldTypeEndDateHeadrut" Width="200px" PopupPosition="Left"></cc1:ValidatorCalloutExtender>--%>                                             
                     <%--           <cc1:ValidatorCalloutExtender runat="server" ID="exReqEndDateHeadrut" TargetControlID="vldReqEndDateHeadrut"  Width="200px" PopupPosition="Left"></cc1:ValidatorCalloutExtender>                                                                                     
                    --%> <br /><br /><br /></td>
                    </tr>
                    </table>  
                    <table class="WorkCardDivuchHeadrutButtomTable">
                     <tr class="WorkCardTable1" style="height:44px; width:651px">
                        <td  style="width:320px"><input type="button" class="btnWorkCardCloseCard" causesvalidation="false" value="סגור" tabindex="12" style="width:80px" onclick="window.close();" 
                            onfocusin="this.style.border ='1px solid black';" onfocusout="this.style.border ='none';" /> </td>
                        <td style="width:331px"> <asp:Button ID="btnUpdate" runat="server" Width="190px" 
                            CausesValidation="true"  CssClass="btnWorkCardCalculate"  TabIndex="11" Text="עדכון סידור בכרטיס עבודה" onclick="btnUpdate_Click"  
                                        onfocusin="this.style.border ='1px solid black';" onfocusout="this.style.border ='none';" />    
                                     
                        </td>
                      </tr>
                    </table>                                      
                </ContentTemplate>
           </asp:UpdatePanel>
        <br /><br />
    </div>
    </form>
   
</body>
</html>
