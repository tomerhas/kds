<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Test2.aspx.cs" Inherits="Modules_Test2" Title="Untitled Page" AsyncTimeout=10000 %>
<%@ Register Assembly="KdsLibrary" Namespace="KdsLibrary.Controls" TagPrefix="KdsCalendar"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" runat="Server" ContentPlaceHolderID="KdsContent" >

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
            
        <table>
            <tr>
                <td style="padding-top:150px">בדיקת תאריך
             </td>
             <td>
                <KdsCalendar:KdsCalendar runat="server" ID="KdsCalendar1"  AutoPostBack="false"   dir="rtl" PopupPositionCallOut="Left" ></KdsCalendar:KdsCalendar>           
             </td>
            </tr>
         </table>
        <table>
           
            <tr>   
                <td>               
                    מספר אישי:
                </td> 
                <td>
                    <asp:TextBox runat="server" ID="txtId" Text="75290" ></asp:TextBox>               
                </td>
            </tr> 
             <tr>   
                <td>               
                    מספר סידור:
                </td> 
                <td>
                    <asp:TextBox runat="server" ID="txtMisparSidur" Text="99814" ></asp:TextBox>               
                </td>
            </tr> 
             <tr>   
                <td>               
                   שעת התחלה סידור:
                </td> 
                <td>
                    <asp:TextBox runat="server" ID="txtShatHatchala" Text="07:00" ></asp:TextBox>   
                                          
                </td>
            </tr>
            <tr>   
                <td>               
                   שעת גמר סידור:
                </td> 
                <td>
                    <asp:TextBox runat="server" ID="txtShatGmar" Text="16:50" ></asp:TextBox>               
                </td>
            </tr> 
            
        </table>
        <br />
  
        <input  id="btnHeadrut" runat="server" value="דיווח העדרות"   type="button"  CausesValidation="false"  />
        <input type="button" id="btnHosafatPeilut" runat="server" value="הוספת פעילות" onclick="OpenHosefPeilut();"   causesvalidation="false"  />
          <input type="button" id="Button3" runat="server" value="שדות נוספים לסידור" onclick="OpenSadotNosafim();"   causesvalidation="false"  />
           <input type="button" id="Button4" runat="server" value="הוספת סידור" onclick="HosafatSidur();"   causesvalidation="false"  />
            <input type="button" id="Button2" runat="server" value="הוספת כניסות" onclick="HosafatKnisot();"   causesvalidation="false"  />
   <input id="PirteySidurLeHosafa" name="PirteySidurLeHosafa"  runat="server" type="hidden" value="fff" />
   
 <input type="button" id="Button5" runat="server" value="בדיקת פרמטרים מוחזרים" onclick="AlertParams();"   causesvalidation="false"  />
 <asp:Button id="Button6" runat="server" OnClick="OnClick_ShinuyHR" class="ImgButtonSearch" Width="120px" Text="שינוי hr" />
 <asp:Button id="Button7" runat="server" OnClick="OnClick_ShinuyMeafyenim" class="ImgButtonSearch" Width="120px" Text="הפעלת מאפיינים" />
     <asp:UpdatePanel ID="UpdatePanel4" runat="server" RenderMode="Inline" UpdateMode="Always">
                <ContentTemplate> 
  <asp:Button id="BtnPrint" runat="server" OnClick="OnClick_PrintWC" class="ImgButtonSearch" Width="120px" Text="הדפסת כ''ע" />
      
      
<asp:Button ID="btnSrokTachograf" runat="server"  Text="סרוק טכוגרף"  
                        CssClass="ImgButtonApprovalRegular"  Width="120px" 
                        onclick="btnSrokTachograf_Click"    />
<asp:Button ID="btnShowTachograf" runat="server"  Text="צפה בטכוגרף"   
                        CssClass="ImgButtonApprovalRegular"  Width="120px" 
                        onclick="btnShowTachograf_Click"  />     
   </ContentTemplate>
               </asp:UpdatePanel>  
               
               
   
 <asp:UpdatePanel ID="UpdatePanel6" runat="server" RenderMode="Inline" UpdateMode="Conditional">
     <ContentTemplate> 
      
        <div id="divNetunim" runat="server"  style="text-align:right;width:945px;">
            <cc1:TabContainer ID="TabContainer1"  AutoPostBack="true"    runat="server"  
                 Font-Size="14px"   EnableViewState="true"  ActiveTabIndex="0"  >
                <cc1:TabPanel ID="pMeafyeneyBitzua"  HeaderText=" פרטי העובד:" runat="server"  >
                 <HeaderTemplate>
                        <label ID="lblMeafyeneyBizua"  runat="server"  style="cursor:pointer"> פרטי העובד:</label> 
                    </HeaderTemplate>
                  <ContentTemplate>
                  

                        <div  id="divEmployeeDetails">
                          <table width="100%" cellpadding="1">
                                            <tr>
                                                <td width="25%">
                                                    עיסוק:
                                                    <asp:TextBox runat="server" CssClass="WorkCardTextBox" ID="txtIsuk" Width="150px"
                                                        ReadOnly="true"> </asp:TextBox>
                                                </td>
                                                <td width="25%">
                                                    חברה:
                                                    <asp:TextBox runat="server" CssClass="WorkCardTextBox" ID="txtCompany" Width="150px"
                                                        ReadOnly="true"> </asp:TextBox>
                                                </td>
                                                <td width="25%">
                                                    סניף:
                                                    <asp:TextBox runat="server" CssClass="WorkCardTextBox" ID="txtSnif" Width="150px"
                                                        ReadOnly="true"> </asp:TextBox>
                                                </td>
                                                <td width="25%">
                                                    איזור:
                                                    <asp:TextBox runat="server" CssClass="WorkCardTextBox" ID="txtArea" Width="150px"
                                                        ReadOnly="true"> </asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                            
                              
                        </div>
                  
                  
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel  ID="pPirteyOved" runat="server"   HeaderText="fgh"  >
                    <HeaderTemplate>
                        <label ID="lblPirteyOved"  runat="server"  style="cursor:pointer">gfh</label> 
                    </HeaderTemplate>
                    <ContentTemplate>
                  </ContentTemplate>
                </cc1:TabPanel>
                  <cc1:TabPanel ID="pStatusOved" runat="server"  HeaderText=" התייצבות" >
                   <HeaderTemplate>
                        <label ID="lblStatus"  runat="server"  style="cursor:pointer"> התייצבות</label> 
                    </HeaderTemplate>
                         <ContentTemplate>
                         

          
                                        <table width="97%" cellpadding="0">
                                            <tr>                                               
                                                <td style="width: 23.25%">
                                                    התייצבות ראשונה:
                                                    <asp:TextBox runat="server" ID="txtFirstPart" CssClass="WorkCardTextBox" Width="100px" > </asp:TextBox>                                                                                                       
                                                </td>
                                                <td style="width: 26.75%">
                                                    סיבת אי התייצבות:
                                                    <asp:DropDownList runat="server" ID="ddlFirstPart" CssClass="WorkCardDDL" ></asp:DropDownList>                                                    
                                                </td>
                                                <td style="width: 23%">
                                                    התייצבות שניה:
                                                    <asp:TextBox runat="server" ID="txtSecPart" CssClass="WorkCardTextBox" Width="100px" > </asp:TextBox>                                                    
                                                </td>
                                                <td style="width: 27%">
                                                    סיבת אי התייצבות:
                                                    <asp:DropDownList runat="server" ID="ddlSecPart" CssClass="WorkCardDDL" ></asp:DropDownList>                                                    
                                                </td>
                                            </tr>
                                        </table>
                              
                        </ContentTemplate>
                  </cc1:TabPanel>
            </cc1:TabContainer>
        
      </div>
   
   </ContentTemplate>
</asp:UpdatePanel>                
               
               
               
 <script language="javascript" type="text/javascript">
    function OpenDivuachHeadrut() {
        var sQueryString;
        sQueryString = "?dt=" + Date();
        sQueryString = sQueryString + "&MisparIshi=" + document.all.ctl00_KdsContent_txtId.value;
        sQueryString = sQueryString + "&DateCard=" + document.all.ctl00_KdsContent_cal1.value;
        sQueryString = sQueryString + "&MisparSidur=" + document.all.ctl00_KdsContent_txtMisparSidur.value;
        sQueryString = sQueryString + "&TimeStart=" + document.all.ctl00_KdsContent_cal1.value + " " + document.all.ctl00_KdsContent_txtShatHatchala.value;
        sQueryString = sQueryString + "&TimeEnd=" + document.all.ctl00_KdsContent_cal1.value + " " + document.all.ctl00_KdsContent_txtShatGmar.value;
       
       window.showModalDialog('../Modules/Ovdim/DivuachHeadrut.aspx?' + sQueryString, '', 'dialogwidth:555px;dialogheight:390px;dialogtop:280px;dialogleft:480px;status:no;resizable:yes;');

    }

    function OpenHosefPeilut() {
        var sQueryString;
        var retvalue;
       // sQueryString = "?Taarich=01/01/2009";
      //  sQueryString = sQueryString + "&MisparIshi=" + document.all.ctl00_KdsContent_txtId.value;
        sQueryString = "?EmpID=26506&SidurID=62058&CardDate=13/07/2010 &SidurDate=13/07/2010&SidurHour=16:55:00&dt=" + Date();  //+ document.all.ctl00_KdsContent_clnFromDate.value;

        retvalue = window.showModalDialog('../Modules/Ovdim/HosafatPeiluyot.aspx' + sQueryString, '', 'dialogwidth:900px;dialogheight:680px;dialogtop:180px;dialogleft:240px;status:no;resizable:yes;');
      //  alert(retvalue);
        document.getElementById("ctl00_KdsContent_PirteySidurLeHosafa").value = retvalue;
    }

    function HosafatKnisot() {
        var sQueryString;
        var retvalue;
        sQueryString = "?EmpID=77104&SidurID=32045&CardDate=14/08/2009&SidurDate=14/08/2009&SidurHour=06:29:00&ShatYetzia=14:15:00&MakatNesia=43220332&OtoNo=96313&dt=" + Date();  
  
       retvalue = window.showModalDialog('../Modules/Ovdim/HosafatKnisot.aspx' + sQueryString, '', 'dialogwidth:500px;dialogheight:380px;dialogtop:280px;dialogleft:340px;status:no;resizable:yes;');
//        window.open("../Modules/Ovdim/HosafatKnisot.aspx" + sQueryString, "", "dialogwidth:960px;dialogheight:690px;dialogtop:" + (document.body.clientWidth / 10) + "px;dialogleft:250px;status:no")
    }
    
    function OpenSadotNosafim() {
        var sQueryString;
        sQueryString = "?EmpID=19813&SidurID=99100&CardDate=26/05/2009&ShatHatchala=26/05/2009 12:00:00&ShatGmar=26/05/2009 16:00:00&dt=" + Date();
////        sQueryString = "?dt=" + Date();
////        sQueryString = sQueryString + "&MisparIshi=" + document.all.ctl00_KdsContent_txtId.value;
////        sQueryString = sQueryString + "&DateCard=" + document.all.ctl00_KdsContent_clnFromDate.value;

        window.showModalDialog('../Modules/Ovdim/SadotNosafimLeSidur.aspx' + sQueryString, '', 'dialogwidth:900px;dialogheight:1000px;dialogtop:280px;dialogleft:480px;status:no;resizable:yes;');
    }

    function HosafatSidur() {
       var sQueryString;
//        sQueryString = "?dt=" + Date();
//        sQueryString = sQueryString + "&MisparIshi=" + document.all.ctl00_KdsContent_txtId.value;
//        sQueryString = sQueryString + "&DateCard=" + document.all.ctl00_KdsContent_clnFromDate.value;
        sQueryString = "?EmpID=19813&CardDate=26/05/2009&dt=" + Date();  //+ document.all.ctl00_KdsContent_clnFromDate.value;

        retvalue = window.showModalDialog('../Modules/Ovdim/HosafatSidur.aspx' + sQueryString, '', 'dialogwidth:950px;dialogheight:750px;dialogtop:280px;dialogleft:380px;status:no;resizable:yes;');
        document.getElementById("ctl00_KdsContent_PirteySidurLeHosafa").value = retvalue;
    }
    
//    function ValidateCheckBoxList(sender, args) {
//        var ValidatorName = sender.id;
//      
//        var TargetId = Mid(ValidatorName, 0, 3);
//        var objCheckBoxList = document.getElementById('CheckBoxList1');
//                            args.IsValid = false;

//    }

//    function AlertParams() {
//        alert(document.getElementById("ctl00_KdsContent_PirteySidurLeHosafa").value);
//    }


   function OnClick_ImgCalendar() {
       var DateCard = document.getElementById("ctl00_KdsContent_cal1").value;
       var d =new Date(Number(DateCard.substr(6, 4)), Number(DateCard.substr(3, 2)) - 1, Number(DateCard.substr(0, 2)),0,0);
      $find('ctl00_KdsContent_Ex_cal1').set_selectedDate(d);

}


     
//       debugger;
//        if (document.getElementById('ctl00_KdsContent_cal1').disabled == true)
//            $find('ctl00_KdsContent_Ex_cal1')._enabled = false;
//        else {
//            $find('ctl00_KdsContent_Ex_cal1')._enabled = true;
//            if (document.getElementById("ctl00_KdsContent_cal1").value != "") {
//                $find('ctl00_KdsContent_Ex_cal1')._visibleDate = document.getElementById("ctl00_KdsContent_cal1").value;

//                $find('ctl00_KdsContent_Ex_cal1').set_selectedDate($find('ctl00_KdsContent_Ex_cal1').get_selectedDate());
//                $find('ctl00_KdsContent_Ex_cal1').raiseDateSelectionChanged();
//             //   $find('ctl00_KdsContent_Ex_cal1').set_visibleDate($find('ctl00_KdsContent_Ex_cal1').get_selectedDate());
//                $find('ctl00_KdsContent_Ex_cal1').updated();
//               //* $find('ctl00_KdsContent_Ex_cal1')._selectedDateChanging = true;
//               //* $find('ctl00_KdsContent_Ex_cal1').set_defaultView($find('ctl00_KdsContent_Ex_cal1').get_selectedDate());
//               //* $find('ctl00_KdsContent_Ex_cal1').set_selectedDate($find('ctl00_KdsContent_Ex_cal1').get_selectedDate());
//                //$find('ctl00_KdsContent_Ex_cal1').set_visibleDate($find('ctl00_KdsContent_Ex_cal1').get_selectedDate());
//               // $find('ctl00_KdsContent_Ex_cal1').set_defaultView(document.getElementById("ctl00_KdsContent_cal1").value);
//               // $find('ctl00_KdsContent_Ex_cal1').set_visibleDate(document.getElementById("ctl00_KdsContent_cal1").value);
//            }
//        }
  //}


 /*   var flag = false;
    var hasMovedInside = false
    function pageLoad() {
       // debugger;
        $find("Bhvddd").add_shown(enableMouseMove);
        $find("Bhvddd").add_hidden(disableMouseMove)
    }
    function enableMouseMove() {
      //  debugger;
        flag = true;
    }
    function disableMouseMove() {
      //  debugger;
        flag = false;
    }
    document.onclick = function(sender, args) {

    if (!flag) return;
        if (event.srcElement.tagName.toLowerCase() == "div" && event.srcElement.outerHTML.indexOf("ajax__calendar") > 0) hasMovedInside = true;
        if (hasMovedInside && event.srcElement.tagName.toLowerCase() != "div") {
            hasMovedInside = false;
            $find("Bhvddd").hide();
        }
    }
*/
//    document.onclick = function(sender, args) {
//     //   debugger;
//        //  if (event.srcElement.tagName.toLowerCase() == "div" && event.srcElement.outerHTML.indexOf("ajax__calendar") > 0) hasMovedInside = true;
//      // if (event.srcElement.tagName.toLowerCase() != "div") {
//            //  hasMovedInside = false;
//            $find("Bhvddd").hide();
//       // }
//    }
 </script>
</asp:Content>
