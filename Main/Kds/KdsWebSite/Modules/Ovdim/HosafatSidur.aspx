 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="HosafatSidur.aspx.cs" Inherits="Modules_Ovdim_HosafatSidur" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>חיפוש והוספת סידור</title>
    <script src='../../js/jquery.js' type='text/javascript'></script>
    <script src='../../Js/GeneralFunction.js' type='text/javascript'></script>
    <link id="Link1" runat="server" href="~/StyleSheet.css" type="text/css" rel="stylesheet" />
     <base target="_self" />  
   
    <style type="text/css">
        .style1
        {
            width: 333px;
        }
    </style>
   
</head>



<body dir="rtl" onload="return window_onload()" onkeydown="return ChangeKeyCode(event.keyCode);">
 <script type="text/javascript">
     var iRowIndexNochehi = 0;
     var col_ShatYetzia = "<%=SHAT_YETZIA %>";
     var col_Teur = "<%=TEUR %>";
     var col_MisRechev = "<%=MISPAR_RECHEV %>";
     var col_hosefPeilut = "<%=HOSEF_PEILUT %>";
     var col_MisRishuy = "<%=MISPAR_RISHUY %>";
     var col_Mispar_Knisa = "<%=MISPAR_KNISA %>";
     var col_Makat = "<%=MAKAT %>";
     var col_Pratim = "<%=PRATIM %>";
     var Col_PeilutChova = "<%=PEILUT_CHOVA %>"
     var Col_txt_shat_yetzia = "<%=TXT_SHAT_YETZIA %>"


     function ChangeKeyCode(keyCode) {
        // debugger;
         if (keyCode == 107) {
             event.keyCode = 9;
             return event.keyCode;
         }
         else if (keyCode == 110) {
         if (document.getElementById("btnHosafa").disabled == false)
             document.getElementById("btnHosafa").focus();
         }

     }
     
     function btnMapa_Click() {
         //*    document.getElementById("txtMisSiduri").style.display = "none";
         document.getElementById("txtMisSidurMapa").style.display = "inline";
        //* document.getElementById("btnMapa").disabled = "disabled";
         //* document.getElementById("btnMeyuchad").disabled = "";
       //  document.getElementById("btnMapa").style.border = '1px solid black';
       //  document.getElementById("btnMeyuchad").style.border = 'none';

         //*      document.getElementById("cbMisSidur").checked = true;
      //*   document.getElementById("cbTeurSidur").checked = false;

        //* document.getElementById("cbTeurSidur").disabled = "disabled";
         //*   document.getElementById("txtTeurSidur").disabled = true; //true;

         //*    document.getElementById("cbMisSidur").disabled = false;
         //*  document.getElementById("txtMisSiduri").disabled = false;

         //*  document.getElementById("txtMisSiduri").value = "";
         //* document.getElementById("txtTeurSidur").value = "";
         document.getElementById("txtMisSidurMapa").value = "";
         document.getElementById("sugSidur").value = 1;
         document.getElementById("btnShow").disabled = true;
         $find("vldExSidur").hide();
         document.getElementById("pirteySidur").style.display = "none";
         document.getElementById("txtMisSidurMapa").focus();
       
     }

     function btnMeyuchad_Click() {
         //*   document.getElementById("txtMisSiduri").style.display = "inline";
      //*   document.getElementById("txtMisSidurMapa").style.display = "none";


         //*     document.getElementById("btnMeyuchad").disabled = "disabled";
         //* document.getElementById("btnMapa").disabled = "";
      //   document.getElementById("btnMeyuchad").style.border = '1px solid black';
      //   document.getElementById("btnMapa").style.border = 'none';

         //*       document.getElementById("cbMisSidur").checked = true;
       //*  document.getElementById("cbTeurSidur").checked = false;

     //*    document.getElementById("cbTeurSidur").disabled = "";
         //*  document.getElementById("txtTeurSidur").disabled = true;   //false;

         //*      document.getElementById("cbMisSidur").disabled = false;
         document.getElementById("txtMisSiduri").disabled = true; //false;

         //*document.getElementById("txtMisSiduri").value = "";
         //*  document.getElementById("txtTeurSidur").value = "";
         document.getElementById("txtMisSidurMapa").value = "";
         document.getElementById("sugSidur").value = 2;
        document.getElementById("btnShow").disabled = true;
         document.getElementById("pirteySidur").style.display = "none";
          $find("vldExSidurMapa").hide();
         cbMis_OnChange();
         //*document.getElementById("txtMisSiduri").focus();
         
     }
     function MisSiduri_onChange() {
         var misSidur;
         var sugSidur = document.getElementById("sugSidur").value;
       
         if (sugSidur == 1) {
             misSidur = document.getElementById("txtMisSidurMapa").value;
           //  document.getElementById("vldMis").controltovalidate = "txtMisSidurMapa";
         }
//*         else {
//             misSidur = document.getElementById("txtMisSiduri").value;
//            // document.getElementById("vldMis").controltovalidate = "txtMisSiduri";
//*         }
         var taarich = document.getElementById("TaarichCA").value;
         //*    document.getElementById("txtTeurSidur").value = "";
         if (misSidur != "") {
             if (IsNumeric(misSidur)) {
                 if (sugSidur == 1 && misSidur.substr(0, 2) == "99") {
                     document.getElementById("vldMisMapa").errormessage = "יש להזין מספר סידורי שאינו מתחיל בספרות 99";
                     ShowValidatorCalloutExtender("vldExSidurMapa");
                      document.getElementById("btnShow").disabled = true;
                   
                 }
//                 else if (sugSidur == 2 && misSidur.substr(0, 1) != "9") {
//                     document.getElementById("vldMis").errormessage = "יש להזין מספר סידורי המתחיל בספרה 9 ";
//                     ShowValidatorCalloutExtender("vldExSidur");
//                      document.getElementById("btnShow").disabled = true;
//                 }
                 else {
                     if (sugSidur == 1) {
//                        // alert(document.getElementById("SidureyEadrut").value);
////                         if (document.getElementById("SidureyEadrut").value.indexOf("," + misSidur + ",") > -1) {
////                             if (misSidur == "99200" || misSidur == "99214")
////                                 document.getElementById("vldMis").errormessage = "לא ניתן לדווח סידור התייצבות";
////                             else document.getElementById("vldMis").errormessage = "יש לדווח במסך הוסף דיווח היעדרות";
////                             ShowValidatorCalloutExtender("vldExSidur");
//                                document.getElementById("btnShow").disabled = true;
//     
//                         }
//                            else
//                                if (document.getElementById("StatusCard").value == -1)
//                                    wsGeneral.MeafyenSidurRagilExists(misSidur, taarich, 99, 1, MeafyenSidurExistsSucceded);
//                                else wsGeneral.getTeurSidurByKod(misSidur, CheckTeurSucceded);
//                     }
//                     else {//מפה
                         if (document.getElementById("StatusCard").value == -1)
                             wsGeneral.MeafyenSidurMapaExists(misSidur, taarich, 99, 1, MeafyenSidurExistsSucceded);
                         else wsGeneral.GetSidurDetailsFromTnua(misSidur, taarich, CheckTeurSucceded);
                     }
                 }
             }
             else {
                 if (sugSidur == 2) {
                     document.getElementById("vldMis").errormessage = "יש להזין מספר סידורי חיובי ושלם בלבד ";
                     ShowValidatorCalloutExtender("vldExSidur");
           
                 } else {
                     document.getElementById("vldMisMapa").errormessage = "יש להזין מספר סידורי חיובי ושלם בלבד ";
                     ShowValidatorCalloutExtender("vldExSidurMapa");
                
                     
                 }

               document.getElementById("btnShow").disabled = true;
             }
         }
     }

     function MeafyenSidurExistsSucceded(result) {
         var sugSidur = document.getElementById("sugSidur").value;
         var taarich = document.getElementById("TaarichCA").value;
         var misSidur;  
         if (sugSidur == 1) 
             misSidur = document.getElementById("txtMisSidurMapa").value;
//*         else
//*             misSidur = document.getElementById("txtMisSiduri").value;
      
         if (result == 1) {
             if (sugSidur == 2)
                wsGeneral.getTeurSidurByKod(misSidur, CheckTeurSucceded);
              else 
                 wsGeneral.GetSidurDetailsFromTnua(misSidur, taarich, CheckTeurSucceded);
         }
         else if (result == -1) {

             if (sugSidur == 1) {
//                 document.getElementById("vldMis").errormessage = "כרטיס ללא התייחסות, לא ניתן להוסיף סידור זה";
//                 ShowValidatorCalloutExtender("vldExSidur");

//             } else {
                 document.getElementById("vldMisMapa").errormessage = "כרטיס ללא התייחסות, לא ניתן להוסיף סידור זה";
                 ShowValidatorCalloutExtender("vldExSidurMapa");
             }
             document.getElementById("btnShow").disabled = true;
         }
     }

     function CheckTeurSucceded(result) {
         var sugSidur = document.getElementById("sugSidur").value;

         if (result != -1) {
//             if (sugSidur == 2)
//                 document.getElementById("txtTeurSidur").value = result;
//             else
//                 document.getElementById("txtTeurSidur").value = "";
             document.getElementById("btnShow").disabled = false;
             document.getElementById("btnShow").focus();
         }
         else {
             document.getElementById("btnShow").disabled = true;
             if (sugSidur == 1) {
//                 document.getElementById("vldMis").errormessage = "מספר סידור שגוי";
//                 ShowValidatorCalloutExtender("vldExSidur");


//             } else {
                 document.getElementById("vldMisMapa").errormessage = "מספר סידור שגוי או לא קיים לתאריך כרטיס עבודה";
                 ShowValidatorCalloutExtender("vldExSidurMapa");

             }
         }
     }

     function ShowValidatorCalloutExtender(sBehaviorId) {
         $find(sBehaviorId)._ensureCallout();
         $find(sBehaviorId).show(true);
     }
 
//     function cbTeur_OnChange() {
//         var sugSidur = document.getElementById("sugSidur").value;
//         if (document.getElementById("cbTeurSidur").checked) {
//             document.getElementById("txtTeurSidur").disabled = false;
//             document.getElementById("cbMisSidur").checked = false;
// //*            document.getElementById("txtMisSiduri").disabled = true;
//         }
//         else {
//             document.getElementById("txtTeurSidur").disabled = true;
//             document.getElementById("txtTeurSidur").value = "";
//          //*   document.getElementById("txtMisSiduri").value = "";
//            document.getElementById("btnShow").disabled = true;
//         }
//     }
     function cbMis_OnChange() {
         var sugSidur = document.getElementById("sugSidur").value;
//         if (document.getElementById("cbMisSidur").checked) {
//          //*   document.getElementById("txtMisSiduri").disabled = false;
//            //* document.getElementById("cbTeurSidur").checked = false;
//          //*    document.getElementById("txtTeurSidur").disabled = true;
//            // if (sugSidur == 2)
//            //     document.getElementById("cbTeurSidur").disabled = true;
//         }
//         else {
             //*    document.getElementById("txtMisSiduri").disabled = true;
             //*   document.getElementById("txtMisSiduri").value = "";
            //*  document.getElementById("txtTeurSidur").value = "";
           //  if (sugSidur == 2)
           //      document.getElementById("cbTeurSidur").disabled = false;
            document.getElementById("btnShow").disabled = true;
//         }
     }

//     function txtTeurSidur_OnChange() {
//         var teur = document.getElementById("txtTeurSidur").value;
//         if (teur != "") {
//             wsGeneral.getKodSidurByTeur(teur, CheckKodSucceded);
//         }
//     }
     function CheckKodSucceded(result) {
         if (result != -1) {
             //*    document.getElementById("txtMisSiduri").value = result;
            // document.getElementById("btnShow").disabled = false;
            MisSiduri_onChange();
         }
         else {
          //*    document.getElementById("vldTeur").errormessage = "תאור סידור שגוי";
             document.getElementById("btnShow").disabled = true;
             //*    document.getElementById("txtMisSiduri").value = "";
          //*   ShowValidatorCalloutExtender("vldExTeur");
         }
     }
     /**************** כפתורים במסך *******************/
     function btnHosafa_OnClick() { //הוספת סידור
         var numRows = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.length;  //document.getElementById("grdPeiluyot").childNodes.length;
         var row;var tempRow;
         var is_valid;
         var vldArr;
         var Rechev;
         var checked;
        // debugger;
         is_valid = onchange_txtShatHatchala(false, "");
           if (is_valid) {
               is_valid = onchange_txtShatGmar(false, "");
                if (is_valid && document.getElementById("tsEmpty") == null) {
                    for (var i = 1; i < numRows; i++) {
                        row = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i);
                        if (row.childNodes.item(col_hosefPeilut).childNodes.item(0).disabled == false)
                           checked = row.childNodes.item(col_hosefPeilut).childNodes.item(0).checked;
                       else if (row.childNodes.item(col_Teur).innerHTML.indexOf("לפי צורך") > -1)
                                checked = row.childNodes.item(col_hosefPeilut).childNodes.item(0).checked;
                            else 
                                checked = row.childNodes.item(col_hosefPeilut).childNodes.item(0).childNodes.item(0).checked ;

                        if (checked != false) {
                            is_valid = onchange_txtShatYezia(row, false, "");
                            if (!is_valid)
                                break;

                            for (var j = 1; j < numRows; j++) {
                                tempRow = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(j);
                                if (i != j && document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(j).childNodes.item(col_hosefPeilut).childNodes.item(0).checked != false &&
                                   Number(row.childNodes.item(col_Mispar_Knisa).innerHTML) == 0 && Number(tempRow.childNodes.item(col_Mispar_Knisa).innerHTML) == 0 &&
                                   document.getElementById(row.id + "_txtShatYeziaDate").value == document.getElementById(tempRow.id + "_txtShatYeziaDate").value ) {
       
                                  //   if (document.getElementById(row.id + "_txtShatYeziaDate").value == document.getElementById(tempRow.id + "_txtShatYeziaDate").value) {
                                        document.getElementById(row.id + "_vldShatYezia").errormessage = "קיימות שתי פעילויות עם שעת יציאה זהה, יש לעדכן אחת מהן ";
                                        ShowValidatorCalloutExtender(row.id + "_vldExvldShatYezia");
                                        is_valid = false;
                                   // }
                                }
                                if (!is_valid)
                                    break;
                            }
                            if (!is_valid)
                                break;

    
  
                            is_valid = onchange_txtKisuyTor(row)
                            if (!is_valid)
                                break;

                            vldArr = document.getElementById(row.id + "_txtIsMakatValid").value.split(';');
                            if (vldArr.length > 1) {
                                if (vldArr[1] == "0") {
                                    document.getElementById(row.id + "_vldMakat").errormessage = vldArr[0];
                                    ShowValidatorCalloutExtender(row.id + "_vldExvldMakat");
                                    is_valid = false;
                                    break;
                                }
                            }

                            Rechev = document.getElementById(row.id + "_txtMisRechev");
                            if (Rechev.disabled == false) {
                                if ((trim(Rechev.value) == "" || trim(Rechev.value) == "0") && Rechev.attributes("Is_Required").value == "1") {
                                    document.getElementById(row.id + "_vldMisRechev").errormessage = 'חובה להכניס מספר רכב';
                                    ShowValidatorCalloutExtender(row.id + "_vldExvldMisRechev");
                                    is_valid = false;
                                    break;
                                }
                                else {
                                    vldArr = document.getElementById(row.id + "_txtIsMisRechevValid").value.split(';');
                                    if (vldArr.length > 1) {
                                        if (vldArr[1] == "0") {
                                            document.getElementById(row.id + "_vldMisRechev").errormessage = vldArr[0];
                                            ShowValidatorCalloutExtender(row.id + "_vldExvldMisRechev");
                                            is_valid = false;
                                            break;
                                        }
                                    }
                                }
                            }


                            is_valid = onchange_txtDakot(row);
                            if (!is_valid)
                                break;
                        }
                    }
                }
           }
           if (is_valid) {
               document.getElementById("HosafatElement").value = "sof";
               document.getElementById("btnIdkunGridHidden").click();
           }
       }

    
     function OpenHosefPeilut() { //הוספת אלמנט
           var sQueryString;
           var retvalue;
           var pratim;
           var flag = false;
        //   "?EmpID=19813&SidurID=99213&CardDate=21/06/2009&SidurHour=10:00";
           sQueryString = "?SidurID=" + document.getElementById("lblMisSidur").innerText;            
           sQueryString = sQueryString + "&CardDate=" + document.getElementById("TaarichCA").value;  //+ document.all.ctl00_KdsContent_clnFromDate.value;
           sQueryString = sQueryString + "&NoSavePeilut=NO";
           sQueryString = sQueryString + "&EmpID=" + document.getElementById("MisparIshi").value;
           sQueryString = sQueryString + "&SidurHour=" + document.getElementById("txtShatHatchala").value;
           sQueryString = sQueryString + "&SidurDate=" + document.getElementById("TaarichCA").value;
           sQueryString = sQueryString + "&dt=" + Date();
           retvalue = window.showModalDialog('HosafatPeiluyot.aspx' + sQueryString, '', 'dialogwidth:900px;dialogheight:680px;dialogtop:280px;dialogleft:480px;status:no;resizable:yes;');
           document.getElementById("HosafatElement").value = retvalue;
           if (document.getElementById("HosafatElement").value != "undefined" &&
                document.getElementById("HosafatElement").value != "") {
            /*   document.getElementById("DestTime").value = document.getElementById("TaarichCA").value;
               if (document.getElementById("txtShatGmar").value != "") {
                   if (IsShatGmarInNextDay(document.getElementById("txtShatGmar").value)) {
                       pratim = retvalue.split(';');
                       if (IsShatGmarInNextDay((pratim[2]))) {
                           document.getElementById("DestTime").value = "peilut";
                           document.getElementById("btnShowMessage").click();
                           flag = true;
                           //alert(document.getElementById("DestTime").value);
                       }
                   }
               }
               if (!flag)*/
                   document.getElementById("btnIdkunGridHidden").click();
               //alert(document.getElementById("HosafatElement").value);
            //   document.getElementById("btnIdkunGridHidden").click();
           }
           document.getElementById("txtShatGmar").title = document.getElementById("TaarichGmar").value;
      }

      /**************** שעת התחלה *******************/
      function onchange_txtShatHatchala(ask, choice) {
           var shaa = document.getElementById("txtShatHatchala").value;   
           var taarich = document.getElementById("TaarichCA").value.split('/');
           var StaratDate;
           var EndDate;
           var shatHatchalaDate;
           var shatGmarDate;
           var shatGmar = document.getElementById("txtShatGmar").value;
           var flag = true;
           var message;
           var stop = false;
         
           var sugGmar =document.getElementById("txtShatGmar").attributes("SugGmar").value ;
      //   debugger
           if (shaa != "") {
               if (IsValidTime(shaa)) {
                   
                   var Param1 = document.getElementById("Params").attributes("Param1").value;
                   var Param93 = document.getElementById("Params").attributes("Param93").value;
                   shatHatchalaDate = new Date(taarich[2], taarich[1] - 1, taarich[0], shaa.split(':')[0], shaa.split(':')[1], '00');
                   if (ask) {
                       if (sugGmar == "5" || sugGmar == "4" || document.getElementById("sugSidur").value == "1") {
                           if (IsShatGmarInNextDay(shaa) || shaa=="00:00") {
                               StaratDate = new Date(taarich[2], taarich[1] - 1, taarich[0], '00', '00', '00');
                               StaratDate = new Date(StaratDate.setDate(StaratDate.getDate() + 1));
                               var Param244 = document.getElementById("Params").attributes("Param244").value;
                               EndDate =new Date(taarich[2], taarich[1] - 1, taarich[0], Param244.split(':')[0], Param244.split(':')[1], '00');
                               EndDate = new Date(EndDate.setDate(EndDate.getDate() + 1));

                               shatHatchalaDate = new Date(shatHatchalaDate.setDate(shatHatchalaDate.getDate() + 1)); 
                               if (shatHatchalaDate >= StaratDate && shatHatchalaDate <= EndDate) {
                                   if (choice == "") {
                                       stop = true;
                                       document.getElementById("DestTime").value = "start";
                                       document.getElementById("btnShowMessage").click();
                                       }
                                   else if (choice == "1") {
                                       shatHatchalaDate = new Date(shatHatchalaDate.setDate(shatHatchalaDate.getDate() - 1));
                                   }
                               }
                               else
                                   shatHatchalaDate = new Date(shatHatchalaDate.setDate(shatHatchalaDate.getDate() - 1));
                           } 
                       } else {
                           //shatHatchalaDate = new Date(taarich[2], taarich[1]-1, taarich[0], shaa.split(':')[0], shaa.split(':')[1], '00');
                           StaratDate = new Date(taarich[2], taarich[1] - 1, taarich[0], Param1.split(':')[0], Param1.split(':')[1], '00');
                           EndDate = new Date(taarich[2], taarich[1] - 1, taarich[0], Param93.split(':')[0], Param93.split(':')[1], '00');
                           if (IsShatGmarInNextDay(Param93)) {
                               EndDate = new Date(EndDate.setDate(EndDate.getDate() + 1));
                           }
                           if (shatHatchalaDate < StaratDate || shatHatchalaDate > EndDate) {
                               document.getElementById("vldShatHatchala").errormessage = "הוקלד ערך שגוי. יש להקליד שעת התחלה בין " + Param1 + " עד " + Param93;
                               ShowValidatorCalloutExtender("vldExvldShatHatchala");
                               flag = false;
                           }
                       }
                       document.getElementById("txtShatHatchala").title = shatHatchalaDate.format("HH:mm:ss dd/MM/yyyy");
                       document.getElementById("TaarichHatchala").value = shatHatchalaDate.format("HH:mm:ss dd/MM/yyyy");
                   }

                   if (!stop){
                       if (document.getElementById("sugSidur").value == "2") {
                       if (document.getElementById("MustMeafyenim").value == "1") {
                           var Meafyen7;
                           var Meafyen8;
                           //var endTime, startTime;
                           if (document.getElementById("MustMeafyenim").outerHTML.indexOf("Meafyen7") > -1)
                               Meafyen7 = document.getElementById("MustMeafyenim").attributes("Meafyen7").value;
                           else Meafyen7 = Param1;
                           if (document.getElementById("MustMeafyenim").outerHTML.indexOf("Meafyen8") > -1) {
                               Meafyen8 = document.getElementById("MustMeafyenim").attributes("Meafyen8").value;
                               if (IsShatGmarInNextDay(Meafyen8))
                                   Meafyen8 = Param93; 
                           }
                           else Meafyen8 = Param93; 
                      
                          if (document.getElementById("MustMeafyenim").outerHTML.indexOf("Meafyen7")>-1) {
                           //    alert("7");
                            //   Meafyen7 = document.getElementById("MustMeafyenim").attributes("Meafyen7").value;
                               StaratDate = new Date(taarich[2], taarich[1] - 1, taarich[0], Meafyen7.split(':')[0], Meafyen7.split(':')[1], '00');
                               if (shatHatchalaDate < StaratDate) {
                                   message = "";
                                   message = message.concat(" הוקלד ערך שגוי. יש להקליד שעת התחלה תקינה: החל מ ", Meafyen7, " עד ", Meafyen8);
                                   document.getElementById("vldShatHatchala").errormessage = message;  //" הוקלד ערך שגוי. יש להקליד שעת התחלה תקינה: החל מ " + Meafyen7 + " עד " + endTime;
                                   ShowValidatorCalloutExtender("vldExvldShatHatchala");
                                   flag = false;
                               }
                           }
                           if (document.getElementById("MustMeafyenim").outerHTML.indexOf("Meafyen8")>-1) {
                             //  alert("8");
                             //  Meafyen8 = endTime;  //document.getElementById("MustMeafyenim").attributes("Meafyen8").value;
                               EndDate = new Date(taarich[2], taarich[1] - 1, taarich[0], Meafyen8.split(':')[0], Meafyen8.split(':')[1], '00');
                               if (IsShatGmarInNextDay(Meafyen8))
                                   EndDate = new Date(EndDate.setDate(EndDate.getDate() + 1));
                               if (shatHatchalaDate > EndDate) {
                                   message = "";
                                   message = message.concat(" הוקלד ערך שגוי. יש להקליד שעת התחלה תקינה: החל מ ", Meafyen7, " עד ", Meafyen8);
                                   document.getElementById("vldShatHatchala").errormessage = message;  //" הוקלד ערך שגוי. יש להקליד שעת התחלה תקינה עד " + Meafyen8 + " עד " + endTime;
                                   ShowValidatorCalloutExtender("vldExvldShatHatchala");
                                   flag = false;
                               }    
                             }  
                           }
                        }

                   if (flag) {
                       //debugger;
          
                       if (document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.length > 0)
                           var row = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(1).childNodes;
                       if (row.item(col_ShatYetzia).childNodes.item(0).value == "") {
                           row.item(col_ShatYetzia).childNodes.item(0).value = shaa;
                           row.item(Col_txt_shat_yetzia).childNodes.item(0).value = shatHatchalaDate.format("dd/MM/yyyy HH:mm:ss");
                           row.item(col_ShatYetzia).childNodes.item(0).title = " תאריך שעת היציאה הוא " + shatHatchalaDate.format("HH:mm:ss dd/MM/yyyy");
                       }


                           if (shatGmar != "") {
                              taarich = document.getElementById("TaarichGmar").value.split(' ')[1].split('/');
                               shatGmarDate = new Date(taarich[2], taarich[1] - 1, taarich[0], shatGmar.split(':')[0], shatGmar.split(':')[1], '00');
                               taarich = document.getElementById("TaarichHatchala").value.split(' ')[1].split('/');
                               shatHatchalaDate =  new Date(taarich[2], taarich[1] - 1, taarich[0], shaa.split(':')[0], shaa.split(':')[1], '00');
                               if (shatHatchalaDate >= shatGmarDate) {
                                   document.getElementById("vldShatHatchala").errormessage = " שעת ההתחלה אינה יכולה להיות גדולה או שווה לשעת הגמר ";
                                   ShowValidatorCalloutExtender("vldExvldShatHatchala");
                                   return false;
                               }
                           }
                       }
                       else return false;
                  }
               }
               else {
                   document.getElementById("vldShatHatchala").errormessage = " שעה לא תקינה ";
                   ShowValidatorCalloutExtender("vldExvldShatHatchala");
                   return false;
               }
           }
           else {
               document.getElementById("vldShatHatchala").errormessage = " חובה להכניס שעת התחלה ";
               ShowValidatorCalloutExtender("vldExvldShatHatchala");
               return false;
           }
          
         
           return true;   
       }
       /**************** שעת גמר *******************/
       function onchange_txtShatGmar(ask, choice) {
           var shaa = document.getElementById("txtShatGmar").value;
           var taarich = document.getElementById("TaarichCA").value.split('/');
           var taarichTmp;
           var StaratDate;
           var EndDate;
           var shatGmarDate;
           var Pend,Pstart;
           var shatHatchalaDate;
           var shatHatchala = document.getElementById("txtShatHatchala").value;
           var flag = true; var flagEnd = true; var next = false; var mafil = false;
           var stop = false;
           var message;
           var sugGmar;
           if (shaa != "") {
               if (IsValidTime(shaa)) {    
                   var Param1 = document.getElementById("Params").attributes("Param1").value;
                   var Param3 = document.getElementById("Params").attributes("Param3").value;
                   var Param4 = document.getElementById("Params").attributes("Param4").value;
                   var Param80 = document.getElementById("Params").attributes("Param80").value;
                   var Param29 = document.getElementById("Params").attributes("Param29").value;
                   Pstart = Param1;
                   if (ask) {
                       shatGmarDate = new Date(taarich[2], taarich[1] - 1, taarich[0], shaa.split(':')[0], shaa.split(':')[1], '00');
                       if (shaa == Param29)
                           shatGmarDate = new Date(shatGmarDate.setDate(shatGmarDate.getDate() + 1));
                       if (IsShatGmarInNextDay(shaa)) {
                           if (choice == "") {
                               stop = true;
                               document.getElementById("DestTime").value = "gmar";
                               document.getElementById("btnShowMessage").click();
                           }
                           else {
                               if (choice == "2") {
                                   shatGmarDate = new Date(shatGmarDate.setDate(shatGmarDate.getDate() + 1));
                                   next = true;
                               }
                           }
                       }
                       document.getElementById("txtShatGmar").title = shatGmarDate.format("HH:mm:ss dd/MM/yyyy");
                       // document.getElementById("txtShatGmar").attributes("Date").value = shatGmarDate.format("HH:mm:ss dd/MM/yyyy");
                       document.getElementById("TaarichGmar").value = shatGmarDate.format("HH:mm:ss dd/MM/yyyy");
                        
                   }
                   else {
                       //  taarichTmp = document.getElementById("txtShatGmar").title.split(' ')[1].split('/');
                       // taarichTmp = document.getElementById("txtShatGmar").attributes("Date").value.split(' ')[1].split('/');
                       taarichTmp = document.getElementById("TaarichGmar").value.split(' ')[1].split('/');
                       shatGmarDate = new Date(taarichTmp[2], taarichTmp[1]-1, taarichTmp[0], shaa.split(':')[0], shaa.split(':')[1], '00');
                   }

                   if (!stop) {

                       if (document.getElementById("sugSidur").value == "2") {
                           sugGmar = document.getElementById("txtShatGmar").attributes("SugGmar").value;
                           if (sugGmar == "0122" || sugGmar == "0123" || sugGmar == "0124" || sugGmar == "0127") { //מפעיל
                               mafil = true;
                               StaratDate = new Date(taarich[2], taarich[1] - 1, taarich[0], Param1.split(':')[0], Param1.split(':')[1], '00');                
                               EndDate = new Date(taarich[2], taarich[1] - 1, taarich[0], Param4.split(':')[0], Param4.split(':')[1], '00');
                               if (IsShatGmarInNextDay(Param4))
                                   EndDate = new Date(EndDate.setDate(EndDate.getDate() + 1));

                               if (shatGmarDate < StaratDate || shatGmarDate > EndDate) {
                                   message = "";
                                   message = message.concat(" הוקלד ערך שגוי. יש להקליד שעת גמר תקינה: החל מ ", Param1, " עד ", Param4);
                                   document.getElementById("vldShatGmar").errormessage = message; //"הוקלד ערך שגוי. יש להקליד שעת גמר בטווח" + Param1 + " - " + Pend;
                                   //  +  " עד " + Pend;
                                   ShowValidatorCalloutExtender("vldExvldShatGmar");
                                   return false;
                                  // flag = false;
                               }
                              // Pend = Param4;
                           }
                           else if (document.getElementById("MustMeafyenim").value == "1") {
                              
                               if (document.getElementById("MustMeafyenim").outerHTML.indexOf("Meafyen7") > -1)
                                   Pstart = document.getElementById("MustMeafyenim").attributes("Meafyen7").value;
                               else Pstart = Param1;
                               if (document.getElementById("MustMeafyenim").outerHTML.indexOf("Meafyen8") > -1)
                                   Pend = document.getElementById("MustMeafyenim").attributes("Meafyen8").value;
                               else Pend = Param29;
////                               message = "";
////                               message = message.concat(" הוקלד ערך שגוי. יש להקליד שעת גמר תקינה: החל מ ", Pstart, " עד ", Pend);

                               if (document.getElementById("MustMeafyenim").outerHTML.indexOf("Meafyen7") > -1) {
                                   //   Meafyen7 = document.getElementById("MustMeafyenim").attributes("Meafyen7").value;

                                   StaratDate = new Date(taarich[2], taarich[1] - 1, taarich[0], Pstart.split(':')[0], Pstart.split(':')[1], '00');
                                   if (shatGmarDate < StaratDate) {
////                                       document.getElementById("vldShatGmar").errormessage = message; //" הוקלד ערך שגוי. יש להקליד שעת גמר תקינה: החל מ " + Meafyen7;
////                                       ShowValidatorCalloutExtender("vldExvldShatGmar");
                                       flag = false;
                                   }
                               }
                               if (document.getElementById("MustMeafyenim").outerHTML.indexOf("Meafyen8") > -1) {
                                   //  Meafyen8 = document.getElementById("MustMeafyenim").attributes("Meafyen8").value;
                                   //Pend = Meafyen8;
                                   EndDate = new Date(taarich[2], taarich[1] - 1, taarich[0], Pend.split(':')[0], Pend.split(':')[1], '00');
                                   if (IsShatGmarInNextDay(Pend))
                                       EndDate = new Date(EndDate.setDate(EndDate.getDate() + 1));
                                   if (shatGmarDate > EndDate) {
////                                       document.getElementById("vldShatGmar").errormessage = message;  //" הוקלד ערך שגוי. יש להקליד שעת גמר תקינה עד " + Meafyen8;
////                                       ShowValidatorCalloutExtender("vldExvldShatGmar");
                                       flagEnd = false;
                                   }
                               }
                           }
                       }
                       
                       if (flagEnd && !(mafil)) {
                           StaratDate = new Date(taarich[2], taarich[1] - 1, taarich[0], Param1.split(':')[0], Param1.split(':')[1], '00');
                           if (document.getElementById("txtShatGmar").attributes("SugGmar").value == "5") {//נהגות
                               EndDate = new Date(taarich[2], taarich[1] - 1, taarich[0], Param80.split(':')[0], Param80.split(':')[1], '00');
                               if (IsShatGmarInNextDay(Param80))
                                   EndDate = new Date(EndDate.setDate(EndDate.getDate() + 1));
                               Pend = Param80;
                           }
                           else {//מנהל
                               EndDate = new Date(taarich[2], taarich[1] - 1, taarich[0], Param3.split(':')[0], Param3.split(':')[1], '00');
                               if (IsShatGmarInNextDay(Param3))
                                   EndDate = new Date(EndDate.setDate(EndDate.getDate() + 1));
                               Pend = Param3;

                           }
                           if (shatGmarDate < StaratDate || shatGmarDate > EndDate) {
////                               message = "";
////                               message = message.concat(" הוקלד ערך שגוי. יש להקליד שעת גמר תקינה: החל מ ", Pstart, " עד ", Pend);
////                               document.getElementById("vldShatGmar").errormessage = message; //"הוקלד ערך שגוי. יש להקליד שעת גמר בטווח" + Param1 + " - " + Pend;
////                               //  +  " עד " + Pend;
////                               ShowValidatorCalloutExtender("vldExvldShatGmar");
                               flag = false;
                           }
                       }
                       if (flag && flagEnd) {
                           if (shatHatchala != "") {
                               shatHatchalaDate = new Date(taarich[2], taarich[1] - 1, taarich[0], shatHatchala.split(':')[0], shatHatchala.split(':')[1], '00');
                               if (shatGmarDate <= shatHatchalaDate) {
                                   document.getElementById("vldShatGmar").errormessage = " שעת גמר אינה יכולה להיות קטנה או שווה לשעת התחלה ";
                                   ShowValidatorCalloutExtender("vldExvldShatGmar");
                                   return false;
                               }
                           }
                       }
                       else {
                           message = "";
                           message = message.concat(" הוקלד ערך שגוי. יש להקליד שעת גמר תקינה: החל מ ", Pstart, " עד ", Pend);
                           document.getElementById("vldShatGmar").errormessage = message; 
                           ShowValidatorCalloutExtender("vldExvldShatGmar");
                           return false;
                       }
                   }
               }
               else {
                   document.getElementById("vldShatGmar").errormessage = " שעה לא תקינה ";
                   ShowValidatorCalloutExtender("vldExvldShatGmar");
                   return false;
               }
           }
           else {
               if (document.getElementById("sugSidur").value == "2") {
                   if (IsChovaShatGmar()) {
                       document.getElementById("vldShatGmar").errormessage = " חובה להכניס שעת גמר לסידור ";
                       ShowValidatorCalloutExtender("vldExvldShatGmar");
                       return false;
                   }
               }
           }
           
           if (next) 
             if (document.getElementById("tsEmpty") == null)
               if( document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.length > 0)
                       alert(" .סידור מסתיים בתאריך " + shatGmarDate.format("dd/MM/yyyy") + "," + " כדאי לבדוק את תאריכי שעות היציאה של הפעילויות בסידור ");
            return true;   
       }
       /*****/
       function IsChovaShatGmar() {
           var pratim;
           if (document.getElementById("tsEmpty") != null)//סידור ללא פעילויות
               return true;
           
           var makat,row,makatType;
           var numRows = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.length;  //document.getElementById("grdPeiluyot").childNodes.length;
           for (var i = 1; i < numRows; i++) {
               row = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i);
               if (document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i).childNodes.item(col_hosefPeilut).childNodes.item(0).checked != false) {
                   makat = document.getElementById(row.id + "_txtMakat").value;
                   if (makat != "" && makat.substr(0, 1) == "5")
                       return true;
                   pratim = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i).childNodes.item(col_Pratim).innerText;
                   makatType = pratim.split(';')[1].split('=')[1];
                   if (makatType ==1 || makatType ==2 || makatType==3)
                         return false
                     if (makatType == 5) {
                         if (pratim.split(';')[4].split('=')[1] == "FALSE")
                             return false;
                     }         
               }
           }
           return true;
       }
     
        /****************  כיסוי תור *******************/
       function onchange_txtKisuyTor(row) {
           var KisuyTor = document.getElementById(row.id + "_txtKisuiTor").value;
           var vld = document.getElementById(row.id + "_vldKisuiTor");
           var taarich = document.getElementById("TaarichCA").value.split('/');
           var taarichTmp; 
           var shaa;
           var shatKisuyTor;
           var shatYezia;
           var shatHatchala;
           var dakot;
           if (KisuyTor != "") {
               if (IsValidTime(KisuyTor)) {
                   shaa = document.getElementById(row.id + "_txtShatYezia").value;
                   shatKisuyTor = new Date(taarich[2], taarich[1] - 1, taarich[0], KisuyTor.split(':')[0], KisuyTor.split(':')[1], '00');
                  
                   if (shaa != "") {
                       taarichTmp = document.getElementById(row.id + "_txtShatYeziaDate").value.split(' ')[0].split('/'); 
                       shatYezia = new Date(taarichTmp[2], taarichTmp[1]-1, taarichTmp[0], shaa.split(':')[0], shaa.split(':')[1], '00');
                       if (shatYezia.getDate() != shatKisuyTor.getDate()) {//שעת יציאה ביום הבא
                           if (IsShatGmarInNextDay(KisuyTor) || KisuyTor =="00:00")
                                 shatKisuyTor = new Date(shatKisuyTor.setDate(shatKisuyTor.getDate() + 1)); 
                       }

                       dakot = document.getElementById(row.id + "_txtKisuiTor").attributes("Kisuy_Tor").value;
                       if (trim(dakot) == "")
                           dakot = "0";
                       if (shatYezia < shatKisuyTor)
                       {
                           vld.errormessage = " שעת כסוי תור לא יכולה להיות לאחר שעת יציאה";
                           ShowValidatorCalloutExtender(row.id + "_vldExvldKisuiTor");
                           return false;
                       }
                       else if(((shatYezia - shatKisuyTor)/(60*1000)) > Number(dakot))
                       {
                           vld.errormessage = " כיסוי התור שהוקלד הינו מעל המותר.מותר עד  " + dakot +" דקות פחות משעת היציאה";
                           ShowValidatorCalloutExtender(row.id + "_vldExvldKisuiTor");
                           return false;
                       }
                   }
                   shaa = document.getElementById("txtShatHatchala").value;
                   if (shaa != "") {
                       shatHatchala = new Date(taarich[2], taarich[1]-1, taarich[0], shaa.split(':')[0], shaa.split(':')[1], '00');
                       if (shatKisuyTor < shatHatchala) {
                           vld.errormessage = " שעת כסוי תור לא יכולה להיות לפני שעת התחלה לסידור";
                           ShowValidatorCalloutExtender(row.id + "_vldExvldKisuiTor");
                           return false;
                       }
                   } 
               }
               else {
                   vld.errormessage = " שעה לא תקינה ";
                   ShowValidatorCalloutExtender(row.id + "_vldExvldKisuiTor");
                   return false;
               }
           }
         //  document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i).childNodes.item(col_Pratim).innerText
           return true;
       }
       /****************  מספר רכב/רישוי *******************/
       function onchange_txtMisparRechev(row,IndexRow) {
           var idMisRechev = row.id + "_txtMisRechev";
           var Mis_Rechev = document.getElementById(idMisRechev).value;
           var vld;
           iRowIndexNochehi = IndexRow; 
        //   var oCell =  row.childNodes.item(9).innerText;
           var oCell = document.getElementById(row.id + "_lblMisparRishuy");
           var ObjIsValidMis = document.getElementById(row.id + "_txtIsMisRechevValid");
           var Makat = document.getElementById(row.id + "_txtMakat").value;
             
           if (IsNumeric(Mis_Rechev)) {
               if (Mis_Rechev != "")
                   wsGeneral.CheckOtoNo(Mis_Rechev, CheckMisRechevSucceded, null, row);
           }
           else {
               oCell.value = "";
               document.getElementById(idMisRechev).title = "";
               vld = document.getElementById(row.id + "_vldMisRechev");
               vld.errormessage = "יש להזין ערך מספרי חיובי ושלם בלבד";
               ShowValidatorCalloutExtender(row.id + "_vldExvldMisRechev");
               ObjIsValidMis.value = vld.errormessage + ";0";
           }
       }
       function CheckMisRechevSucceded(result, oRow) {
           var iRowIndex = iRowIndexNochehi;
           var oCell = document.getElementById(oRow.id + "_lblMisparRishuy");
           //var oCell = oRow.childNodes.item(9).innerText;
           var ObjIsValidMis = document.getElementById(oRow.id + "_txtIsMisRechevValid");
           var vld;
           if (result != 0) {
               oCell.value = result;
               //oRow.childNodes.item(8).title = result;
               oRow.cells[col_MisRechev].childNodes[0].title = result;  
               ObjIsValidMis.value = "1";
               changeMisRechevForAllPeiluyot(oRow, iRowIndex);  
           }
           else {
               oCell.value = "";
               oRow.cells[col_MisRechev].childNodes[0].title = "";
               vld = document.getElementById(oRow.id + "_vldMisRechev");
               vld.errormessage = "!מספר רכב לא קיים";
               ShowValidatorCalloutExtender(oRow.id + "_vldExvldMisRechev");
               ObjIsValidMis.value =vld.errormessage + ";0";  
           }
       }


       function changeMisRechevForAllPeiluyot(oRow, iRowIndex) {
           if (document.getElementById(oRow.id).nextSibling != null) {
               var oMisRechev = document.getElementById(oRow.id + "_txtMisRechev");
               var iMisRechev = document.getElementById(oRow.id + "_txtMisRechev").value;
               var OrgMisRechev = oMisRechev.getAttribute("OldV");
               var MustCarNum = oMisRechev.getAttribute("Is_Required");
               var NextPeilut = document.getElementById(oRow.id).nextSibling.cells[col_MisRechev].childNodes[0];
               var NextCarNum = NextPeilut.value;
               var misRishuy = document.getElementById(oRow.id + "_lblMisparRishuy").value;
               if (NextCarNum != undefined) {
                   var NextMustCarNum = NextPeilut.getAttribute("Is_Required");
                   if (NextCarNum == '') { NextCarNum = '0'; }
                   if (iMisRechev != '') {
                       if ((MustCarNum == '1') && (((NextCarNum == OrgMisRechev) || (Number(NextCarNum) == 0))) && (NextMustCarNum)) {
                           //     document.getElementById("lblCarNumQ").innerText = "האם להחליף את מספר הרכב בכל הפעילויות בסידור בהן מספר הרכב הוא ריק או ".concat(String(OrgMisRechev));
                           document.getElementById("lblCarNumQ").innerText = "האם להחליף את מספר הרכב בכל הפעילויות בסידור בהן מספר הרכב הוא ריק ";
                           if (String(OrgMisRechev) != "") {
                              document.getElementById("lblCarNumQ").innerText =  document.getElementById("lblCarNumQ").innerText.concat(" או ", String(OrgMisRechev));  
                           }
                           document.getElementById("hidCarKey").value = OrgMisRechev + ',' + iMisRechev + ',' + oRow.id + "," + misRishuy;
                           document.getElementById("btnCopy").click();
                       }
                   }
               }
           }
       }

       function btnCopyOtoNum(iAction) {
           $find("pBehvCopy").hide();
           var arrKey = document.getElementById("hidCarKey").value.split(",");
           var oId = arrKey[2];
           var _OrgCarNum = arrKey[0];
           var _CurrCarNum = arrKey[1];
           if (iAction == 1) {         
               var _NextPeilutCarNum, _MustCarNum;
               var _CarNum = document.getElementById(oId).cells[col_MisRechev].childNodes[0];
               var _CarNumToolTip = arrKey[3];
               var _NextPeilut = document.getElementById(oId).nextSibling;
               while (_NextPeilut != null) {
                   _NextPeilutCarNum = _NextPeilut.cells[col_MisRechev].childNodes[0].value;
                   if (_NextPeilutCarNum != undefined) {
                       _MustCarNum = _NextPeilut.cells[col_MisRechev].childNodes[0].getAttribute("Is_Required");

                       if (((_NextPeilutCarNum == _OrgCarNum) || (_NextPeilutCarNum == _CurrCarNum) || (Number(_NextPeilutCarNum) == 0) || (_NextPeilutCarNum == '')) && (_MustCarNum == '1')) {
                           if ((_NextPeilut.cells[col_MisRechev].childNodes[0].disabled != true)) {
                               _CarNum.setAttribute("OldV", _CurrCarNum);
                               _NextPeilut.cells[col_MisRechev].childNodes[0].setAttribute("OldV", _CurrCarNum);
                               _NextPeilut.cells[col_MisRechev].childNodes[0].value = _CurrCarNum;
                               _NextPeilut.cells[col_MisRechev].childNodes[0].title = _CarNumToolTip;
                                    _NextPeilut.cells[col_MisRishuy].childNodes[0].value = _CarNumToolTip;
                           }
                       }
                       else {//אם נתקלים במספר רכב שונה עוצרים. אם נתקלים בפעילות שאינה דורשת מספר רכב או ריקה או 0 ממשיכים
                           if ((_NextPeilutCarNum != '0') && (_NextPeilutCarNum != '') && (_MustCarNum != '0')) {
                               break;
                           }
                       }
                   }
                   _NextPeilut = _NextPeilut.nextSibling;
               }
           }
           else {
               document.getElementById(oId).cells[col_MisRechev].childNodes[0].setAttribute("OldV", _CurrCarNum);
           }
       }  
        
       /**************** מקט *******************/
       function onchange_txtMakat(row) {
           var vld = document.getElementById(row.id + "_vldMakat");
           var kodElement;
           var oldMakat = row.childNodes.item(11).innerText;
           var newMakat = document.getElementById(row.id + "_txtMakat").value;
           var ObjIsValidMakat = document.getElementById(row.id + "_txtIsMakatValid");
           if (IsNumeric(newMakat)) {
               if (Number(newMakat) > 9999999) {// אורך מקט חייב להיות לפחות 8 ספרות
                   if (oldMakat.substr(0, 3) != newMakat.substr(0, 3))
                       document.getElementById(row.id + "_txtMakat").value = oldMakat;
                   else {
                       kodElement = newMakat.substr(1, 2);
                       wsGeneral.getMigbalaLeErechElement(kodElement, document.getElementById("TaarichCA").value, ErechLeElemntSucceded, null, row);      
                   }
                   //     wsGeneral.CheckMakat(oldMakat, newMakat, document.getElementById("TaarichCA").value, CheckMakatTakin, null, row);
               }
               else {

                   vld.errormessage = " יש להזין מק''ט באורך 8 ספרות";
                   ShowValidatorCalloutExtender(row.id + "_vldExvldMakat");
                   ObjIsValidMakat.value = vld.errormessage + ";0;short";
               }
           }
           else {

               vld.errormessage = "יש להזין ערך מספרי חיובי ושלם בלבד";
               ShowValidatorCalloutExtender(row.id + "_vldExvldMakat");
               ObjIsValidMakat.value = vld.errormessage + ";0";
           }

       }
       function CheckMakatTakin(result, orow) {
           var vld;
           var Makat = document.getElementById(orow.id + "_txtMakat").value;
           var ObjIsValidMakat = document.getElementById(orow.id + "_txtIsMakatValid");
           if (result == "0") {
               vld = document.getElementById(orow.id + "_vldMakat");
               vld.errormessage = "המק''ט שהוקלד שגוי";
               ShowValidatorCalloutExtender(orow.id + "_vldExvldMakat");
               ObjIsValidMakat.value = vld.errormessage + ";0";
           }
           else wsGeneral.GetMakatType(Makat, CheckMigbalaLemakat, null, orow);
       }
       function CheckMigbalaLemakat(result, orow) {
           var kodElement;
           var Makat = document.getElementById(orow.id + "_txtMakat").value;
           var ObjIsValidMakat = document.getElementById(orow.id + "_txtIsMakatValid");
           if (result == "5") //elemnt
           {
               kodElement = Makat.substr(1, 2);
               wsGeneral.getMigbalaLeErechElement(kodElement, document.getElementById("TaarichCA").value, ErechLeElemntSucceded, null, orow);
          }
           else {
               ObjIsValidMakat.value = "1";
               document.getElementById(orow.id + "_txtMisRechev").attributes("Is_Required").value = "1";
           }
       }
       function ErechLeElemntSucceded(result, orow) {
           var pirteyMigbala;
           var erech = document.getElementById(orow.id + "_txtMakat").value.substr(3, 3);
           var ObjIsValidMakat = document.getElementById(orow.id + "_txtIsMakatValid");
           pirteyMigbala = result.split("-");
           if (Number(erech) < Number(pirteyMigbala[0]) || Number(erech) > Number(pirteyMigbala[1])) {
               document.getElementById(orow.id + "_vldMakat").errormessage = "יש להזין ערך לאלמנט בטווח " + pirteyMigbala[1] + " - " + pirteyMigbala[0];
               ShowValidatorCalloutExtender(orow.id + "_vldExvldMakat");
               ObjIsValidMakat.value = ObjIsValidMakat.value + "1";
           }
           else ObjIsValidMakat.value = "1";
       }

       /**************** דקות *******************/
       function onchange_txtDakot(row) {
           var vld = document.getElementById(row.id + "_vldDakot");
           var dakotHagdara = row.childNodes.item(13).title;
           var Param98 = document.getElementById("Params").attributes("Param98").value;
           var dakotBafoal = document.getElementById(row.id + "_txtDakotBafoal").value;
          // debugger
           if (trim(dakotBafoal) == "")
               dakotBafoal = 0;
           if (IsNumeric(dakotBafoal)) {
               if (trim(dakotHagdara) == "")
                   dakotHagdara = 0;
               //כניסה (mispar_knisa>0) -  יש לאפשר להקליד ערך רק עבור כניסות מסוג לפי צורך (SugKnisa= 3), ערך בין 0  ולא גדול מהערך בפרמטר 98 (מכסימום זמן כניסה לישוב).
               if (Number(row.childNodes.item(col_Mispar_Knisa).innerText) > 0)
                   dakotHagdara = Param98;

               if (Number(dakotBafoal) > Number(dakotHagdara)) {
                   if (Number(row.childNodes.item(col_Mispar_Knisa).innerText) > 0)
                       vld.errormessage = "ערך דקות בפועל לא יכול לחרוג ממקסימום " + Param98 + "  דקות ";
                   else
                       vld.errormessage = "ערך דקות בפועל לא יכול לחרוג מ-" + dakotHagdara + "  דקות ";
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
       /**************** שעת יציאה *******************/
       function onchange_txtShatYezia(row, ask, choice) {
            var shaa = document.getElementById(row.id + "_txtShatYezia").value;
           var vld = document.getElementById(row.id + "_vldShatYezia");
            var Param29 = document.getElementById("Params").attributes("Param29").value;
           var taarichTmp;
           var taarich = document.getElementById("TaarichCA").value.split('/');
           var StaratDate;
           var EndDate;
           var shatYeziaDate;
           var shatGmarDate;
           var shatHatchalaDate;
           var tmp;
           var stop = false;
           var shatGmar = document.getElementById("txtShatGmar").value;
           var shatHatcahla = document.getElementById("txtShatHatchala").value;
           if (trim(shaa) != "") {
               if (IsValidTime(shaa)) {
                   shatYeziaDate = new Date(taarich[2], taarich[1] - 1, taarich[0], shaa.split(':')[0], shaa.split(':')[1], '00');
                   if (shaa == Param29)
                       shatYeziaDate = new Date(shatYeziaDate.setDate(shatYeziaDate.getDate() + 1));
                       if (ask) {
                           if (IsShatGmarInNextDay(shaa)) {
                              //** if (shatGmar != "" && IsShatGmarInNextDay(shatGmar)){
                                   if (choice == "") {
                                       stop = true;
                                       document.getElementById("DestTime").value = "yezia;"+ row.id;
                                       document.getElementById("btnShowMessage").click();
                                   }
                                   else {
                                       if (choice == "2")
                                           shatYeziaDate = new Date(shatYeziaDate.setDate(shatYeziaDate.getDate() + 1));
                                   }
                             //**  }
                           }
                     //      document.getElementById(row.id + "_DateHidden").value = shatYeziaDate.format("HH:mm:ss dd/MM/yyyy");
                           document.getElementById(row.id + "_txtShatYeziaDate").value = shatYeziaDate.format("dd/MM/yyyy HH:mm:ss");
                           if (document.getElementById(row.id + "_txtKisuiTor").disabled == false) {
                               var KisuiTor = new Date();
                               var iKisuiTor = document.getElementById(row.id + "_txtKisuiTor").attributes("Kisuy_Tor").value;
                               if (IsNumeric(iKisuiTor)) {
                                   if (iKisuiTor > 0) {
                                       KisuiTor.setHours(shaa.substr(0, 2));
                                       KisuiTor.setMinutes(shaa.substr(shaa.length - 2, 2));
                                       KisuiTor.setMinutes(KisuiTor.getMinutes() - Number(iKisuiTor));
                                       document.getElementById(row.id + "_txtKisuiTor").value = KisuiTor.format("HH:mm");
                                   }
                               }
                           }
                       }
                       else {
                           taarichTmp = document.getElementById(row.id + "_txtShatYeziaDate").value.split(' ')[0].split('/');
                       //    taarichTmp = document.getElementById(row.id + "_txtShatYezia").attributes("Date").value.split(' ')[1].split('/');
                           shatYeziaDate = new Date(taarichTmp[2], taarichTmp[1] - 1, taarichTmp[0], shaa.split(':')[0], shaa.split(':')[1], '00');
                      //     alert(shatYeziaDate);
                       }
                      
                       if (!stop) {
                           if (shatHatcahla != "") {
                             //  debugger;
                               var startHour = document.getElementById("TaarichHatchala").value.split(' ')[1].split('/');
                               shatHatchalaDate = new Date(startHour[2], startHour[1] - 1, startHour[0], shatHatcahla.split(':')[0], shatHatcahla.split(':')[1], '00');
                               if (shatYeziaDate < shatHatchalaDate) {
                                   vld.errormessage = " לא ניתן להקליד שעת יציאה הקטנה משעת התחלה של הסידור ";
                                   ShowValidatorCalloutExtender(row.id + "_vldExvldShatYezia");
                                   return false;
                               }
                           }
                     /**      if (shatGmar != "") {
                               //   taarich = document.getElementById("txtShatGmar").title.split(' ')[1].split('/');
                              // taarich = document.getElementById("txtShatGmar").attributes("Date").value.split(' ')[1].split('/');
                               taarich = document.getElementById("TaarichGmar").value.split(' ')[1].split('/');
               
                               shatGmarDate = new Date(taarich[2], taarich[1]-1 , taarich[0], shatGmar.split(':')[0], shatGmar.split(':')[1], '00');
                               if (shatYeziaDate > shatGmarDate) {
                                   vld.errormessage = " לא ניתן להקליד שעת יציאה הגדולה משעת גמר של הסידור ";
                                   ShowValidatorCalloutExtender(row.id + "_vldExvldShatYezia");
                                   return false;
                               }
                           }**/
                       }
               }
              else {
                   vld.errormessage = " שעה לא תקינה ";
                   ShowValidatorCalloutExtender(row.id + "_vldExvldShatYezia");
                   return false;
               }
           }
           else {
           //    CopyShatYetziaToPeiluyot(row, "");
               vld.errormessage = " חובה להכניס שעת יציאה לפעילות ";
               ShowValidatorCalloutExtender(row.id + "_vldExvldShatYezia");
               return false;
           }

           document.getElementById(row.id + "_txtShatYezia").title = " תאריך שעת היציאה הוא " + shatYeziaDate.format("HH:mm:ss dd/MM/yyyy");
           CopyShatYetziaToPeiluyot(row,shatYeziaDate);
           return true;
       }

       function CopyShatYetziaToPeiluyot(oRowAv, ShatYetziaDate) {

           var numRows = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.length;
           //  var oRowAv = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(iRowIndex);
           for (var i = 1; i < numRows; i++) {
               oRow = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i);
               if (oRow.id == oRowAv.id) {
                   iRowIndex = i;
                   break;
               }
           }
           if (numRows != (iRowIndex + 1)) {
               var i = iRowIndex + 1;
               oRow = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i);
               while (oRow.childNodes.item(col_Makat).childNodes.item(0).value == oRowAv.childNodes.item(col_Makat).childNodes.item(0).value && Number(oRow.childNodes.item(col_Mispar_Knisa).innerHTML) > 0 && i < numRows) {
                     document.getElementById(oRow.id + "_txtShatYeziaDate").value = ShatYetziaDate.format("dd/MM/yyyy HH:mm");
                     document.getElementById(oRow.id + "_txtShatYezia").value = ShatYetziaDate.format("HH:mm");
                     document.getElementById(oRow.id + "_txtShatYezia").title = " תאריך שעת היציאה הוא " + ShatYetziaDate.format("HH:mm:ss dd/MM/yyyy");
                   i += 1;
                   if (i < numRows)
                       oRow = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i);
               }
           }    
//           for (i = iRowIndex + 1; i < numRows; i++) {
//               oRow = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i);
//               if (oRow.childNodes.item(col_Makat).childNodes.item(0).value == oRowAv.childNodes.item(col_Makat).childNodes.item(0).value && Number(oRow.childNodes.item(col_Mispar_Knisa).innerHTML) > 0) {
//                   document.getElementById(oRow.id + "_txtShatYeziaDate").value = ShatYetziaDate.format("dd/MM/yyyy HH:mm");
//                   document.getElementById(oRow.id + "_txtShatYezia").value = ShatYetziaDate.format("HH:mm");
//                   document.getElementById(oRow.id + "_txtShatYezia").title = " תאריך שעת היציאה הוא " + ShatYetziaDate.format("HH:mm:ss dd/MM/yyyy");
//               }
//           }
       }
       function SamenHakol_OnClick() {
           var num = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.length;
           for (var i = 1; i < num; i++) {
               if (document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i).childNodes.item(col_hosefPeilut).childNodes.item(0).disabled == false)
                    document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i).childNodes.item(col_hosefPeilut).childNodes.item(0).checked = true;
               else
                   document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i).childNodes.item(col_hosefPeilut).childNodes.item(0).childNodes.item(0).checked = true;
           }
       }
       function NakeHakol_OnClick() {
           var num = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.length;
           for (var i = 1; i < num; i++) {
               if (document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i).childNodes.item(Col_PeilutChova).innerHTML != "1") {
                   if (document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i).childNodes.item(col_hosefPeilut).childNodes.item(0).disabled == false)
                       document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i).childNodes.item(col_hosefPeilut).childNodes.item(0).checked = false;
                   else
                       document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i).childNodes.item(col_hosefPeilut).childNodes.item(0).childNodes.item(0).checked = false;
               }
           }
       }

       function btnShowMessage_Click() {
          
//               document.getElementById("lblMessage").value = ":יש לקבוע את תאריך שעת יציאה של הסידור";
           $find('ModalPopupEx').show;
           document.getElementById("btnHosafa").disabled = true;
        //   document.getElementById("btnHosafatPeilut").disabled = true;
       }
       function btnNochachi_click() {
           $find('ModalPopupEx').hide();
           var obj;
           document.getElementById("btnHosafa").disabled = false;
        //   document.getElementById("btnHosafatPeilut").disabled = false;
            if (document.getElementById("DestTime").value == "gmar") {
                onchange_txtShatGmar(true, "1");
            }
            if (document.getElementById("DestTime").value.split(';')[0] == "start") {
                onchange_txtShatHatchala(true, "1")
            }
           /* else if (document.getElementById("DestTime").value == "peilut") {
            document.getElementById("DestTime").value = document.getElementById("TaarichCA").value;
            document.getElementById("btnIdkunGridHidden").click();
            }*/
            if (document.getElementById("DestTime").value.split(';')[0] == "yezia") {
                obj = document.getElementById(document.getElementById("DestTime").value.split(';')[1]);
                onchange_txtShatYezia(obj, true, "1");
            }
            
       }
       function btnHaba_click() {
           $find('ModalPopupEx').hide();

           var taarich;
           var shatYeziaDate; //= document.getElementById("txtShatHatchala").value;
           var obj;
           document.getElementById("btnHosafa").disabled = false;
         //  document.getElementById("btnHosafatPeilut").disabled = false;
           if (document.getElementById("DestTime").value.split(';')[0] == "gmar") {
               onchange_txtShatGmar(true, "2")
           }
           if (document.getElementById("DestTime").value.split(';')[0] == "start") {
               onchange_txtShatHatchala(true, "2")
           }
         /*  else if (document.getElementById("DestTime").value == "peilut") {
               taarich=document.getElementById("TaarichCA").value.split('/');
               shatYeziaDate = new Date(taarich[2], taarich[1] -1, taarich[0]);
               document.getElementById("DestTime").value = ( new Date(shatYeziaDate.setDate(shatYeziaDate.getDate() + 1))).format("dd/MM/yyyy");
               document.getElementById("btnIdkunGridHidden").click();
           }*/
           if (document.getElementById("DestTime").value.split(';')[0] == "yezia") {
               obj = document.getElementById(document.getElementById("DestTime").value.split(';')[1]);
               onchange_txtShatYezia(obj, true, "2");
           }
               
       }
       function window_onload() {
           //*     document.getElementById("txtMisSiduri").style.display = "none";
           document.getElementById("txtMisSidurMapa").style.display = "inline";
           document.getElementById("txtMisSidurMapa").focus();
           //*    document.getElementById("txtMisSiduri").disabled = false;
           //*  document.getElementById("btnMapa").disabled = "disabled";
           //*        document.getElementById("cbMisSidur").disabled = false;
          //* document.getElementById("cbTeurSidur").disabled = "disabled";
           //*   document.getElementById("btnMapa").style.border = '1px solid black';
        
             setBorderBtns();
             load();
       }
       
       function setBorderBtns(){
       var aButton=document.getElementsByTagName('input');
        for(var i=0; i<aButton.length; i++) {
            if (aButton[i].type =="button" || aButton[i].type =="submit")
                    aButton[i].onfocus=function() {setFunctionsButton(this);}; // Show button change class 
            }
       }
       
     function setFunctionsButton(obj){
         var aButton=document.getElementsByTagName('input');
           for(var i=0; i< aButton.length; i++) {
                if (aButton[i].type =="button" || aButton[i].type =="submit"){
                    if (aButton[i].id == obj.id)// && obj.id != "btnMapa" && obj.id != "btnMeyuchad")
                          aButton[i].style.border ="1px solid black";
                   else aButton[i].style.border ="none";
            }
     }
       } 

     function EndRequestHandler(sender, args) {
        if (args.get_error() == undefined){ 
                 setBorderBtns();
          }
         else
            alert("There was an error" + args.get_error().message);
    }
    function load() {
         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
     }
/********************************/

       function SamenKnisot(iIndexRow) {
           var oRowAv, j, oRow;

           var num = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.length;
           oRowAv = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(iIndexRow);
           if (Number(oRowAv.childNodes.item(col_Mispar_Knisa).innerHTML) == 0) {
               for (j = iIndexRow + 1; j < num; j++) {
                   oRow = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(j);
                   if ((oRow.childNodes.item(col_Makat).childNodes[0].value == oRowAv.childNodes.item(col_Makat).childNodes[0].value && Number(oRow.childNodes.item(col_Mispar_Knisa).innerHTML) > 0 && document.getElementById(oRow.id + "_txtDakotBafoal").style.display == 'none') ||
                   oRow.childNodes.item(col_Makat).childNodes[0].value.substr(0, 3) == "700" || oRow.childNodes.item(col_Makat).childNodes[0].value.substr(0, 3) == "761"
                   || oRow.childNodes.item(col_Makat).childNodes[0].value.substr(0, 3) == "784")
                       oRow.childNodes.item(col_hosefPeilut).childNodes.item(0).childNodes.item(0).checked = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(iIndexRow).childNodes.item(col_hosefPeilut).childNodes.item(0).checked;
                   //else if (oRow.childNodes.item(col_Makat).childNodes[0].value.substr(0,3)== "700" || oRow.childNodes.item(col_Makat).childNodes[0].value.substr(0,3)=="743")
                    //   oRow.childNodes.item(col_hosefPeilut).childNodes.item(0).childNodes.item(0).checked = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(iIndexRow).childNodes.item(col_hosefPeilut).childNodes.item(0).checked;
                   else if (oRow.childNodes.item(col_Makat).childNodes[0].value == oRowAv.childNodes.item(col_Makat).childNodes[0].value && Number(oRow.childNodes.item(col_Mispar_Knisa).innerHTML) > 0) {
                   if (document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(iIndexRow).childNodes.item(col_hosefPeilut).childNodes.item(0).checked == true)
                           oRow.childNodes.item(col_hosefPeilut).childNodes.item(0).disabled = false;
                       else {
                           oRow.childNodes.item(col_hosefPeilut).childNodes.item(0).disabled = true;
                           oRow.childNodes.item(col_hosefPeilut).childNodes.item(0).checked = false;
                       }
                   }
                   else if (oRow.childNodes.item(col_Makat).childNodes[0].value != oRowAv.childNodes.item(col_Makat).childNodes[0].value)
                       break;
               }
           }
       }
       function onSadeFocus(object) {
           document.getElementById(object.id).select();
       }
       function moveFocus(obj) {
           if (obj != "btnShow") {
               if (document.getElementById('btnShow').disabled == true)
                   document.getElementById(obj).focus();
           }
//*           else {
//               if (document.getElementById('btnMapa').disabled == true)
//                   document.getElementById('btnMeyuchad').focus();
//               else document.getElementById('btnMapa').focus();
//*           }
       }
 </script>
    <form id="form1" runat="server">
             <asp:ScriptManager  runat="server"  id="ScriptManagerKds" EnablePartialRendering="true" EnablePageMethods="true">        
               </asp:ScriptManager>
               <asp:UpdateProgress  runat="server" id="UpdateProgress1" DisplayAfter="0" >
                    <ProgressTemplate>
                        <div id="divProgress" class="Progress"  style="text-align:right;position:absolute;left:52%;top:48%; z-index:1000"   >
                              <asp:Image ID="Image2" runat="server" ImageUrl="../../Images/Eggedprogress.gif" style="width: 100px; height: 100px" />טוען...
                        </div>        
                    </ProgressTemplate>
               </asp:UpdateProgress>
    <asp:UpdatePanel  ID="upSidur" runat="server"  UpdateMode="Always">
                   <ContentTemplate>
            <div>
                <table style="width:100%" >
                      <tr class="GridHeader"><td > חיפוש והוספת סידור</td></tr>
                      <tr>
                        <td>
                            <fieldset  style="height:50px" >  <legend   id="LegendFilter" style="background-color:White" >הוספת סידור מפה  : </legend> 
                                
                                     <table>
                                        <tr>
                                            <td>&nbsp;&nbsp;</td>
                                         <%--   <td><input id="btnMapa" type="button" runat="server" value="מפה" class="ImgButtonSearch"  onclick="btnMapa_Click()" style="width:auto;"  tabindex="1" onfocusout="this.style.border ='none';"  /></td>
                                              <td>&nbsp;&nbsp;</td>        
                                            <td><input id="btnMeyuchad"  type="button"  runat="server" value="מיוחד" class="ImgButtonSearch"  onclick="btnMeyuchad_Click()" style="width:auto;"   tabindex="2"   onfocusout="this.style.border ='none';" /></td>
                                           
                                              <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>      --%>         
                                            <td >
                                          <%--  <asp:RadioButton  ID="cbMisSidur" runat="server" onclick="cbMis_OnChange()"   GroupName="chkDefault" Checked="true" Text="מספר סידור"  tabindex="3"    />--%>
<%--                                            <asp:CheckBox runat="server" Text="מספר סידור" onclick="cbMis_OnChange()"   ID="cbMisSidur" />--%>
                                           <%--   <input type="checkbox" ID="cbMisSidur" runat="server" onclick="cbMis_OnChange()"  style="background-color:red;width:150px"   title="מספר סידורי"   value="מספר סידורי"   />--%>
                                         <asp:Label ID="lblmis" runat="server" Text="מספר סידור"></asp:Label>
                                        <%-- <asp:TextBox ID="txtMisSiduri" runat="server" Enabled="false"  onChange="MisSiduri_onChange()"     width="130px" MaxLength="5"  tabindex="4"    ></asp:TextBox>--%>
                                                <asp:TextBox ID="txtMisSidurMapa" runat="server"   onchange="MisSiduri_onChange()"  width="130px" MaxLength="5"  tabindex="5"     ></asp:TextBox>
                                             <%--    <asp:CustomValidator runat="server" id="vldMis"   ControlToValidate="txtMisSiduri" ErrorMessage=""   Display="None"    ></asp:CustomValidator>
                                                 <cc1:ValidatorCalloutExtender runat="server" ID="vldExMis" BehaviorID="vldExSidur"   TargetControlID="vldMis" Width="200px" PopupPosition="Left"  ></cc1:ValidatorCalloutExtender>                                                
                                                --%>
                                                 <asp:CustomValidator runat="server" id="vldMisMapa"   ControlToValidate="txtMisSidurMapa" ErrorMessage=""></asp:CustomValidator>
                                                 <cc1:ValidatorCalloutExtender runat="server" ID="vldExMisMapa" BehaviorID="vldExSidurMapa"  TargetControlID="vldMisMapa" Width="200px" PopupPosition="Left"  ></cc1:ValidatorCalloutExtender>                                                
                                                 <%--<cc1:AutoCompleteExtender id="AautoKodSidur"  runat="server" CompletionInterval="0"   CompletionSetCount="25" UseContextKey="true"  
                                                        TargetControlID="txtMisSiduri" MinimumPrefixLength="1" ServiceMethod="getKodSidurimWhithOutList" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                                        EnableCaching="true"  CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                                                        CompletionListItemCssClass="autocomplete_completionListItemElement"  >                               
                                                 </cc1:AutoCompleteExtender> --%> 
                                            </td>
                                               <td>&nbsp;&nbsp;</td>    
                                            <td>
                                       <%--     <asp:RadioButton  ID="cbTeurSidur" GroupName="chkDefault"  onclick="cbTeur_OnChange()" runat="server" Text="תאור הסידור" tabindex="6"     />--%>
                                     <%--           <asp:CheckBox runat="server" Text="תאור הסידור" onclick="cbTeur_OnChange()" ID="cbTeurSidur" />--%>
                                                 <%-- <input type="checkbox" ID="cbTeurSidur" runat="server" onclick="cbTeur_OnChange()"  value="תאור הסידור"  disabled />
                                    --%>
                                               <%-- <asp:TextBox ID="txtTeurSidur" runat="server" Enabled="false" width="130px" onchange="txtTeurSidur_OnChange()"  tabindex="7"   ></asp:TextBox>
                                                      <cc1:AutoCompleteExtender id="AautoTeurSidur"  runat="server" CompletionInterval="0" CompletionSetCount="25" UseContextKey="true"  
                                                        TargetControlID="txtTeurSidur" MinimumPrefixLength="1" ServiceMethod="getTeureySidurimWhithOutList" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                                        EnableCaching="true"  CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                                                        CompletionListItemCssClass="autocomplete_completionListItemElement"  >                               
                                                    </cc1:AutoCompleteExtender>   
                                                     <asp:CustomValidator runat="server" id="vldTeur"   ControlToValidate="txtTeurSidur" ErrorMessage=""   Display="None"   ></asp:CustomValidator>
                                                 <cc1:ValidatorCalloutExtender runat="server" ID="vldExTeur" BehaviorID="vldExTeur"   TargetControlID="vldTeur" Width="200px" PopupPosition="Left"  ></cc1:ValidatorCalloutExtender>                                                --%>
                                            </td>
                                            <%--  <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>  --%>
                                            <td><asp:Button ID="btnShow" runat="server" Text="הצג"  style="width:auto;" Enabled="false" OnClick="btnShow_OnClick"   tabindex="8"   CssClass="ImgButtonSearch" onblur="moveFocus('btnShow');" /></td>
                                        </tr>
                                      </table>
                                   
                            </fieldset>  
                        </td>
                      </tr>
                </table>
                <br />          
            </div>
            <div style="width:100%">
            
             <input type="button" ID="btnShowMessage" runat="server" onclick="btnShowMessage_Click()" style="display: none;" />
                        <cc1:ModalPopupExtender ID="ModalPopupEx" DropShadow="false" X="300" Y="200" PopupControlID="paMessage"
                            TargetControlID="btnShowMessage"  runat="server">
                        </cc1:ModalPopupExtender>
                        <asp:Panel runat="server" Style="display: none" ID="paMessage" CssClass="PanelMessage" Width="350px">
                             <asp:Label ID="lblHeaderMessage" runat="server"  BackColor="#696969" ForeColor="White">קביעת תאריך</asp:Label>
                            <br />
                            <br />
                            <br />
                            יש לקבוע את היום של השעה שהוקלדה:
                            <br />
                            <br />
                            <input type="button" ID="btnNochachi" runat="server" value="יום נוכחי" CssClass="ImgButtonMake"
                                Width="150px" onclick="btnNochachi_click()" CausesValidation="false" />
                            <input type="button" ID="btnHaba"  runat="server" onclick="btnHaba_click()"  value="יום הבא" CssClass="ImgButtonMake"
                                Width="150px" CausesValidation="false" /></asp:Panel>

            
            </div>
            <div id="pirteySidur" runat="server" style="display:none;width:990px">
                <table style="border-color:Black;border-width:1px;border-style:outset;"  >
                    <tr class="GridHeader">
                        <td style="width:52px">מספר סידור</td>
                        <td style="width:52px">שעת התחלה</td>
                        <td style="width:52px">שעת גמר</td>
                        <td style="width:808px"></td>
                    </tr>
                    <tr>
                        <td valign="top" align="center"><asp:Label ID="lblMisSidur" runat="server" Font-Bold="true"  ></asp:Label></td>
                        <td  valign="top" align="center">
                              <asp:TextBox ID="txtShatHatchala" runat="server" Width="40px" MaxLength="5"  onchange="onchange_txtShatHatchala(true,'')" ></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="extMaskStartTime" runat="server" TargetControlID="txtShatHatchala" MaskType="Time" UserTimeFormat="TwentyFourHour" Mask="99:99"  ></cc1:MaskedEditExtender>
                                    <asp:RegularExpressionValidator  runat="server" id="vldShatHatchala" EnableClientScript="true" Display="none" ErrorMessage="" ControlToValidate="txtShatHatchala"   ValidationExpression="^([0-1]?\d|2[0-3])(:[0-5]\d){1,2}$"></asp:RegularExpressionValidator>
                                    <cc1:ValidatorCalloutExtender runat="server" ID="exvldShatHatchala" BehaviorID="vldExvldShatHatchala"  TargetControlID="vldShatHatchala" Width="240px" PopupPosition="Left"></cc1:ValidatorCalloutExtender>     
                              </td>
                        <td  valign="top"  align="center">
                                <asp:TextBox ID="txtShatGmar" runat="server" Width="40px" ToolTip="" MaxLength="5"  onchange="onchange_txtShatGmar(true,'')" ></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="extMaskEndTime" runat="server" TargetControlID="txtShatGmar" MaskType="Time" UserTimeFormat="TwentyFourHour" Mask="99:99"  ></cc1:MaskedEditExtender>
                                    <asp:RegularExpressionValidator  runat="server" id="vldShatGmar" EnableClientScript="true" Display="none" ErrorMessage="" ControlToValidate="txtShatGmar"   ValidationExpression="^([0-1]?\d|2[0-3])(:[0-5]\d){1,2}$"></asp:RegularExpressionValidator>
                                    <cc1:ValidatorCalloutExtender runat="server" ID="exvldShatGmar" BehaviorID="vldExvldShatGmar"  TargetControlID="vldShatGmar" Width="200px" PopupPosition="Left"></cc1:ValidatorCalloutExtender>     
                              </td>
                        <td>
                        <asp:Panel ID="pnlgrdPeiluyot"    height="330px" width="808px" runat="server" ScrollBars="Auto">   
                        <asp:GridView ID="grdPeiluyot" runat="server" GridLines="None" 
                                 AutoGenerateColumns="False" width="790px"
                                 ShowHeader="true"  ShowFooter="false" AllowPaging="false" 
                                 HeaderStyle-CssClass="GridHeaderSecondary"
                                 OnRowDataBound="grdPeiluyot_RowDataBound" >
                              <Columns>
                                    <asp:BoundField DataField="KiSUY_TOR" ItemStyle-CssClass="ItemRow"  />
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="true" ItemStyle-CssClass="ItemRow" HeaderText="כיסוי תור" >
                                       <ItemTemplate>
                                            <asp:TextBox ID="txtKisuiTor" runat="server"  Width="40px"></asp:TextBox>
                                             <cc1:MaskedEditExtender ID="extKisuiTor" runat="server" TargetControlID="txtKisuiTor" MaskType="Time" UserTimeFormat="TwentyFourHour" Mask="99:99"  ></cc1:MaskedEditExtender>
                                             <asp:RegularExpressionValidator  runat="server" id="vldKisuiTor" EnableClientScript="true"  Display="none" ErrorMessage="יש להקליד שעת יציאה בטווח 00:00-23:59" ControlToValidate="txtKisuiTor"   ValidationExpression="^([0-1]?\d|2[0-3])(:[0-5]\d){1,2}$"></asp:RegularExpressionValidator>
                                             <cc1:ValidatorCalloutExtender runat="server" ID="exvldKisuiTor" BehaviorID="vldExvldKisuiTor"  TargetControlID="vldKisuiTor" Width="200px" PopupPosition="Left"></cc1:ValidatorCalloutExtender>         
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                    <asp:BoundField DataField="SHAT_YETZIA" ItemStyle-CssClass="ItemRow"  />
                                   <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="true" ItemStyle-CssClass="ItemRow" HeaderText="שעת יציאה" >
                                       <ItemTemplate>
                                            <asp:TextBox ID="txtShatYezia" runat="server" Width="40px"></asp:TextBox>
                                               <cc1:MaskedEditExtender ID="extMaskShatYezia" runat="server" TargetControlID="txtShatYezia" MaskType="Time" UserTimeFormat="TwentyFourHour" Mask="99:99"  ></cc1:MaskedEditExtender>
                                              <asp:RegularExpressionValidator  runat="server" id="vldShatYezia" EnableClientScript="true"  Display="none" ErrorMessage="יש להקליד שעת יציאה בטווח 00:00-23:59" ControlToValidate="txtShatYezia"   ValidationExpression="^([0-1]?\d|2[0-3])(:[0-5]\d){1,2}$"></asp:RegularExpressionValidator>
                                              <cc1:ValidatorCalloutExtender runat="server" ID="exvldShatYezia" BehaviorID="vldExvldShatYezia"  TargetControlID="vldShatYezia" Width="200px" PopupPosition="Left"></cc1:ValidatorCalloutExtender>     
                                              <input type="hidden" id="DateHidden" runat="server" />
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:BoundField DataField="TEUR" HeaderText="תיאור" ItemStyle-CssClass="ItemRow"  />
                                   <asp:BoundField DataField="KAV" HeaderText="קו"  ItemStyle-CssClass="ItemRow"   />
                                    <asp:BoundField DataField="SUG" HeaderText="סוג" ItemStyle-CssClass="ItemRow"  />
                                      <asp:BoundField DataField="MISPAR_RECHEV" ItemStyle-CssClass="ItemRow"  />
                                   <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="ItemRow" HeaderStyle-Wrap="true" HeaderText="מספר רכב" >
                                       <ItemTemplate>
                                            <asp:TextBox ID="txtMisRechev" runat="server" MaxLength="6" Width="42px"></asp:TextBox>
                                            <asp:CustomValidator runat="server" id="vldMisRechev" ControlToValidate="txtMisRechev" ErrorMessage=""   Display="None"   ></asp:CustomValidator>
                                             <cc1:ValidatorCalloutExtender runat="server" ID="exvMisRechev" BehaviorID="vldExvldMisRechev"  TargetControlID="vldMisRechev" Width="200px" PopupPosition="Left"></cc1:ValidatorCalloutExtender>  
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                  
                                    <asp:BoundField DataField="MISPAR_RISHUY" ItemStyle-CssClass="ItemRow"  />
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center"  ItemStyle-CssClass="ItemRow" HeaderStyle-Wrap="true" HeaderText="מספר רישוי" >
                                       <ItemTemplate>
                                        <asp:TextBox ID="lblMisparRishuy" runat="server" ></asp:TextBox>
                                     <%--  <input  type="text" ID="lblMisparRishuy" runat="server" readonly="readonly" style="width:50px;border:none;background:none;" />--%>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:BoundField DataField="MAKAT" ItemStyle-CssClass="ItemRow"  />
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="ItemRow" HeaderStyle-Wrap="true" HeaderText="מק''ט" >
                                       <ItemTemplate>
                                            <asp:TextBox ID="txtMakat" runat="server" MaxLength="8" Width="60px"></asp:TextBox>
                                             <asp:CustomValidator runat="server" id="vldMakat" ControlToValidate="txtMakat" ErrorMessage=""   Display="None"   ></asp:CustomValidator>
                                             <cc1:ValidatorCalloutExtender runat="server" ID="exvMakat" BehaviorID="vldExvldMakat"  TargetControlID="vldMakat" Width="200px" PopupPosition="Left"></cc1:ValidatorCalloutExtender>  
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                     <asp:BoundField DataField="DAKOT_HAGDARA" HeaderText="דקות הגדרה" ItemStyle-CssClass="ItemRow"  ItemStyle-Width="50px"/>
                                 
                                  <asp:BoundField DataField="DAKOT_BAFOAL" ItemStyle-CssClass="ItemRow"  />
                                   <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="ItemRow"  ItemStyle-Width="50px" HeaderStyle-Wrap="true" HeaderText="דקות בפועל" >
                                       <ItemTemplate>
                                            <asp:TextBox ID="txtDakotBafoal" runat="server" Width="40px"></asp:TextBox>
                                            <asp:CustomValidator runat="server" id="vldDakot" ControlToValidate="txtDakotBafoal" ErrorMessage=""   Display="None"   ></asp:CustomValidator>
                                            <cc1:ValidatorCalloutExtender runat="server" ID="exvdakot" BehaviorID="vldExvldDakot"  TargetControlID="vldDakot" Width="200px" PopupPosition="Right"></cc1:ValidatorCalloutExtender>  
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                     <asp:BoundField DataField="HOSEF_PEILUT" ItemStyle-CssClass="ItemRow"  />
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="ItemRowLast"
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
                                         <%-- <input type="checkbox"  ID="cbHosef" checked="checked" runat="server" />--%>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                      <asp:BoundField DataField="PEILUT_CHOVA" ItemStyle-CssClass="ItemRow"  />
                                      <asp:BoundField DataField="IS_VALID_MAKAT" ItemStyle-CssClass="ItemRow"  />
                                      <asp:TemplateField  >
                                           <ItemTemplate>
                                                <asp:TextBox ID="txtIsMakatValid" runat="server" Width="40px"></asp:TextBox>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:BoundField DataField="IS_VALID_MIS_RECHEV" ItemStyle-CssClass="ItemRow"  />
                                      <asp:TemplateField  >
                                           <ItemTemplate>
                                                <asp:TextBox ID="txtIsMisRechevValid" runat="server" Width="40px"></asp:TextBox>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:BoundField DataField="PRATIM" ItemStyle-CssClass="ItemRow"  />
                                      <asp:BoundField DataField="SHAT_YEZIA_DATE" ItemStyle-CssClass="ItemRow"  />
                                      <asp:TemplateField  >
                                           <ItemTemplate>
                                                <asp:TextBox ID="txtShatYeziaDate" runat="server" Width="40px"></asp:TextBox>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:BoundField DataField="MISPAR_KNISA" ItemStyle-CssClass="ItemRow"  />
                                    </Columns>
                                    <AlternatingRowStyle CssClass="GridAltRow" />
                                    <RowStyle CssClass="GridRow" />
                             </asp:GridView>
                             <asp:Panel ID="tsEmpty" runat="server" HorizontalAlign="Center" Font-Bold="true"  visible="false" >
                                סידור ללא פעילויות</asp:Panel> 
                            </asp:Panel>
                        </td>
                    </tr>
                     <tr id="trMsgNextDay" runat="server" style="display:none;">
                        <td colspan ="4" style="font-size:small"  >
                           <asp:Label ID="lblYomHaba" runat="server" Font-Bold="true" CssClass="ErrorMessage" Text="סידור זה ייכנס לכרטיס עבודה של היום הבא " ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan ="4" style="font-size:small" >
                            ש.גמר תחושב אוטומטית לאחר הוספת הסידור לכ''ע (מלבד סידורי ויזה או סידורים ללא פעילויות)
                        </td>
                    </tr>
                     <%--<tr id="tsEmpty" runat="server" visible="false" ><td colspan="3"></td>
                     <td align="center" valign="top" style="font-weight:bold" >סידור ללא פעילויות</td></tr>--%>
                 </table>
                   <br />
                <table style="width:100%" >
                      <tr> 
                          <td align="right">
                                <input type="button" class="ImgButtonSearch" value="סגור" style="width:100px" onclick="window.close();" />
                          </td>
                          
                         <td align="left">
                             <asp:Button ID="btnIdkunGridHidden" runat="server" onclick="BtIdkunGrid_Click"  />
                            <input type="button" id="btnHosafatPeilut"  class="ImgButtonSearch"  runat="server" value="הוסף/חפש פעילות" style="width:150px;display:none" onclick="OpenHosefPeilut();"   causesvalidation="false"  />
                            <input id="btnHosafa" runat="server" type="button"  class="ImgButtonSearch" style="width:238px"  value="הוסף את הסידור לכרטיס העבודה"  
                              onclick="btnHosafa_OnClick();" />
                 <%--
                             <asp:Button id="btnHosafa" runat="server" Text="הוסף את הסידור לכרטיס העבודה" style="width:238px" class="ImgButtonSearch" OnClientClick="btnHosafa_OnClick();"  OnClick="btnHosafatSidur_OnClick" />
                  --%>    </td> 
                   </tr>
                </table>
            </div>
        <input type="hidden" id="TaarichHatchala" name="TaarichHatchala"  runat="server" />
        <input type="hidden" id="TaarichGmar" name="TaarichGmar"  runat="server" />
        <input type="hidden" id="DestTime" name="DestTime"  runat="server" />
        <input type="hidden" id="sugSidur" name="sugSidur"  runat="server" />
        <input type="hidden" id="TaarichCA" name="TaarichCA"  runat="server" />
        <input type="hidden" id="MisparIshi" name="MisparIshi"  runat="server" />
        <input type="hidden" id="HosafatElement" name="HosafatElement"  runat="server" />
        <input type="hidden" id="Params" name="Params"  runat="server" />
        <input type="hidden" id="MustMeafyenim" name="MustMeafyenim"  runat="server"  />
        <input type="hidden" id="SidureyEadrut" name="SidureyEadrut"  runat="server"  />
        <input type="hidden" id="HiddenTakin" name="HiddenTakin"  runat="server"  />
        <input type="hidden" id="StatusCard" name="StatusCard"  runat="server"  />
        <input type="hidden" id="sug_sidur_tnua" name="sug_sidur_tnua"  runat="server"  />
         </ContentTemplate>
    </asp:UpdatePanel>
       <input type="button" ID="btnCopy" runat="server" style="display: none;" />
      <cc1:ModalPopupExtender ID="MPECopy" dropshadow="false" X="300" Y="180" PopupControlID="paCopy"
         TargetControlID="btnCopy"  runat="server" behaviorid="pBehvCopy">
      </cc1:ModalPopupExtender>
      <asp:Panel runat="server" Style="display: none" ID="paCopy" CssClass="PanelMessage" Width="350px">
        <asp:Label ID="Label2" runat="server" Width="100%" BackColor="#696969" ForeColor="White">העתקת מספר רכב</asp:Label>
        <input type="hidden" id="hidCarKey" />        
        <br />
            <asp:Label ID="lblCarNumQ" runat="server" Width="100%"></asp:Label>    
        <br />
        <br />
        <input type="button" id="btnYes" runat="server" value="כן" onclick="btnCopyOtoNum(1)" CausesValidation="false" class="ImgButtonEndUpdate" style="width:80px" />       
        <input type="button" id="btnNo"  runat="server" onclick="btnCopyOtoNum(0)"  value="לא" CausesValidation="false" class="ImgButtonEndUpdate" style="width:80px"/>       
     </asp:Panel>   
      <%--  <input type="hidden" id="MisSidur" name="MisSidur"  runat="server" />
        <input type="hidden" id="teurSidur" name="sugSidur"  runat="server" />--%>
    </form>
</body>
</html>
