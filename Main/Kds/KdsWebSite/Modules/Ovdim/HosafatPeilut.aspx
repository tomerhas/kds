<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="HosafatPeilut.aspx.cs" Inherits="Modules_Ovdim_HosafatPeilut" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>חיפוש והוספת פעילות</title>
    <script src='../../js/jquery.js' type='text/javascript'></script>
    <link id="Link1" runat="server" href="~/StyleSheet.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript">
      /******************** כללי/סינון***************************/
           function btnElement_Click() {
               $("#divHosafatElement").css("display", "");
               $("#divPeilutKatalog").css("display", "none");
              // $("#divButtons").css("display", "");
               NikuySadot(true);
               document.getElementById("divButtons").style.display = "block";
               document.getElementById("Sug_Peilut").value = "2";
               document.getElementById("txtMisRechev").title = "";
               document.getElementById("btnElement").disabled = true;
               document.getElementById("btnHosafatNesiaa").disabled = false;
               document.getElementById("txtMisparElement").focus();
             
           }
        function btnHosafatNesiaa_Click() {
            $("#divPeilutKatalog").css("display", "inline");
            $("#divHosafatElement").css("display", "none");
            $("#tblPeilutNesiaa").css("display", "none");
            //     $("#divButtons").css("display", "none");
            document.getElementById("divButtons").style.display = "none";
            NikuySadotNesiaa();
            document.getElementById("Sug_Peilut").value = "1";
            document.getElementById("btnElement").disabled = false;
            document.getElementById("btnHosafatNesiaa").disabled = true;
            document.getElementById("txtMakat").focus();
             
        }
           function SetTextBox() {
               var rdo = document.getElementById("rdKod");
               if (rdo.checked) {
                   document.getElementById("txtMisparElement").disabled = false;
                   document.getElementById("txtTeurElement").disabled = true;
               }
               else {
                   document.getElementById("txtTeurElement").disabled = false;
                   document.getElementById("txtMisparElement").disabled = true;
               }
           }
           function ShowValidatorCalloutExtender(sBehaviorId) {
               $find(sBehaviorId)._ensureCallout();
               $find(sBehaviorId).show(true);
           }
           function NikuySadotNesiaa() {
               document.getElementById("txtMakat").value = "";
               document.getElementById("txtKisuiTor").value = "";
               document.getElementById("txtShatYeziaKatalog").value = "";
               document.getElementById("txtMisRechevKatalog").value = "";
               document.getElementById("txtDakotBafoal").value = "";
           }
           /************************** תאור אלמנט*********************************/
	        function CheckTeurElement() {
	            NikuySadot(false);
	            var teur = document.getElementById("txtTeurElement").value;
	            if (teur != "")
	                wsGeneral.GetKodElementByTeur(teur, HachnesKodElemnt);
	          
	        }
	        function HachnesKodElemnt(result) {
	            if (result == -1) {
	                document.getElementById("vldTeur").errormessage = "אלמנט לא קיים";
	                document.getElementById("txtMisparElement").value = "";
	                ShowValidatorCalloutExtender("vldExTeur");
	                document.getElementById("txtTeurElement").attributes("TeurValid").value = "false";
	            }
	            else {
	                document.getElementById("txtMisparElement").value = result;
	                document.getElementById("txtTeurElement").attributes("TeurValid").value = "true";
	                if (result != "")
	                    CheakKodElement();
	            }

	        }
	        /************************** קוד אלמנט *********************************/
           function CheakKodElement() 
            {
               NikuySadot(false);
               var Sidur = document.getElementById("txtHiddenMisparSidur").value;
               var taarichCA = document.getElementById("txtHiddenTaarichCA").value;
               var Kod = document.getElementById("txtMisparElement").value;

               if (!IsNumeric(Kod)) {
                   document.getElementById("vldKod").errormessage = "יש להזין ערך מספרי חיובי ושלם בלבד";
                   ShowValidatorCalloutExtender("vldExKod");
                   document.getElementById("txtMisparElement").value = "";
                   document.getElementById("txtMisparElement").attributes("KodValid").value = "false";
               }
               else if (Kod  != "")
                   if (document.getElementById("ElementsRelevants").value.indexOf("," + Kod + ",") == -1) {
                       document.getElementById("vldKod").errormessage = "אלמנט שגוי";
                       ShowValidatorCalloutExtender("vldExKod");
                       document.getElementById("txtMisparElement").attributes("KodValid").value = "false";
                       
                   }
                   else {
                       wsGeneral.CheckKodElement(Kod, Sidur, taarichCA, CheckKodSucceded);
                       }
               
            }
            function CheckKodSucceded(result) {
                var flag = false;
                var sBehaviorId;
                var vldId;
                if (document.getElementById("rdKod").checked) {
                    sBehaviorId = "vldExKod";
                    vldId = "vldKod";
                }
                else {
                    sBehaviorId = "vldExTeur";
                    vldId = "vldTeur";
                }
               
                switch(result)
                {
                    case "1":
                        document.getElementById(vldId).errormessage = "מספר האלמנט שגוי";
                        flag = true;
                        break;
                    case "2":
                        document.getElementById(vldId).errormessage = "אסור לדווח אלמנט זה בסידור ויזה";
                        flag = true;
                        break;
                    case "3":
                        document.getElementById(vldId).errormessage = "אסור לדווח אלמנט זה בסידור מיוחד";
                        flag = true;
                        break;
                    default:
                        flag = false;
              }
              if (flag) {
                  ShowValidatorCalloutExtender(sBehaviorId);
                  document.getElementById("txtMisparElement").attributes("KodValid").value = "false";
              }
              else {
                  
                      MiluyPirteyElement(result);
                      document.getElementById("txtMisparElement").attributes("KodValid").value = "true";
                      document.getElementById("txtTeurElement").attributes("TeurValid").value = "true";
                  }
              }
       

            /************************** מילוי פרטים *********************************/
            function MiluyPirteyElement(result) {
                var Pratim;
             
                Pratim = result.split(';'); 
                document.getElementById("txtTeurElement").value = Pratim[0];
                document.getElementById("txtHosefErechElement").disabled = false;
                document.getElementById("txtShatYezia").disabled = false;
                switch (Pratim[1]) {
                    case "1":
                        document.getElementById("txtSugElement").value = "דקות";
                        break;
                    case "2":
                        document.getElementById("txtSugElement").value = "כמות";
                        break;
                    case "3":
                        document.getElementById("txtSugElement").value = "קוד";
                        break;
                    default:
                        {
                            document.getElementById("txtSugElement").value = "";
                            document.getElementById("txtHosefErechElement").disabled = true;
                          //  document.getElementById("txtShatYezia").disabled = true;
                         //   document.getElementById("btnHosafa").disabled = false;
                        }
                }
               if (Pratim[2] =="1") //חובה רכב
                   document.getElementById("txtMisRechev").disabled = false;
               else
                       document.getElementById("txtMisRechev").disabled = true;
            }
            /************************** ניקוי פרטי אלמנט *********************************/
            function NikuySadot(hakol) {
                //     document.getElementById("btnHosafa").disabled = true;
                document.getElementById("txtMisRechev").title = "";
                 document.getElementById("txtSugElement").value = "";
                 document.getElementById("txtHosefErechElement").value = "";
                 document.getElementById("txtShatYezia").value = "";
                 document.getElementById("txtHosefErechElement").disabled = true;
                 document.getElementById("txtMisRechev").value = "";
                 document.getElementById("txtMisRechev").disabled = true;
                if (document.getElementById("rdKod").checked)
                     document.getElementById("txtTeurElement").value = "";

                 if (hakol) {
                     document.getElementById("txtMisparElement").value = "";
                     document.getElementById("txtTeurElement").value = "";
                     document.getElementById("rdKod").checked = true;
                     SetTextBox();
                 }
             }
             /************************** ערך אלמנט *********************************/
             function CheakErechLeElemnt() {
                 var kod = document.getElementById("txtMisparElement").value;
                 var taarichCA = document.getElementById("txtHiddenTaarichCA").value;
                 if (kod != "")
                     wsGeneral.getMigbalaLeErechElement(kod, taarichCA,ErechLeElemntSucceded);
              
             }
             function ErechLeElemntSucceded(result) {
                 var pirteyMigbala;
                 var erech = document.getElementById("txtHosefErechElement").value;
                 pirteyMigbala = result.split("-");
                 if (!IsNumeric(erech)) {
                     document.getElementById("vldErech").errormessage = "יש להזין ערך מספרי חיובי ושלם בלבד";
                     ShowValidatorCalloutExtender("vldExErech");
                     document.getElementById("txtHosefErechElement").attributes("ErechValid").value = "false";
                 //?    document.getElementById("WhithOutErrors").value = "false";
                 }
                 else if (erech < Number(pirteyMigbala[0]) || erech > Number(pirteyMigbala[1])) {
                     document.getElementById("vldErech").errormessage = "יש להזין ערך לאלמנט בטווח " + pirteyMigbala[1] + " - " + pirteyMigbala[0];
                     ShowValidatorCalloutExtender("vldExErech");
                     document.getElementById("txtHosefErechElement").attributes("ErechValid").value = "false";
                  //?   document.getElementById("WhithOutErrors").value = "false";
                 }
                 else document.getElementById("txtHosefErechElement").attributes("ErechValid").value = "true";
               //?  else document.getElementById("WhithOutErrors").value = "true";
                 //  else document.getElementById("btnHosafa").disabled = false;
          //?       ShowBtnHosafa();
             }
             /************************** מספר רכב *********************************/
             function onChange_txtMisRechev() {
                 var mis_rechev = document.getElementById("txtMisRechev").value;
                 if (IsNumeric(mis_rechev) && mis_rechev != "") {
                     wsGeneral.CheckOtoNo(mis_rechev, CheckMisRechevSucc);     
                 }
                 else {
                     document.getElementById("vldMisRechev").errormessage = "יש להזין ערך מספרי חיובי ושלם בלבד";
                     ShowValidatorCalloutExtender("vldExMisRechev");
                     document.getElementById("txtMisRechev").attributes("MisRechevValid").value = "false";
                  //   document.getElementById("btnHosafa").disabled = false;
                 }
             }
             function CheckMisRechevSucc(result) {
                 if (result == 0) {
                     document.getElementById("vldMisRechev").errormessage = "מספר רכב לא קיים!";
                     ShowValidatorCalloutExtender("vldExMisRechev");
                     document.getElementById("txtMisRechev").attributes("MisRechevValid").value = "false";
                     //     document.getElementById("btnHosafa").disabled = false;
                 } else {
                    document.getElementById("txtMisRechev").title = result;
                     document.getElementById("txtMisRechev").attributes("MisRechevValid").value = "true";
                 }
                 
             }
             /************************** כפתור הוספה *********************************/
             function btnHosafa_OnClick() {
                 var makat;
                 var erech;
                 var flag = false;
                 var shatYetzia;
                 if (document.getElementById("ShaatYeziaDate").value != "")
                     shatYetzia = document.getElementById("ShaatYeziaDate").value;
                 else shatYetzia = document.getElementById("txtHiddenTaarichCA").value;
              //   alert(shatYetzia);
                 if (document.getElementById("Sug_Peilut").value == "2") {
                     if (BdikatElemnt()) {
                         makat = 7;
                         if (trim(document.getElementById("txtMisparElement").value).length == 1)
                             makat += "0" + trim(document.getElementById("txtMisparElement").value);
                         else makat += trim(document.getElementById("txtMisparElement").value);
                      
                         erech = document.getElementById("txtHosefErechElement").value;      
                          if (erech.length == 0)  
                                makat += "000";  
                          else if (erech.length == 1)
                                 makat += "00";
                             else if (erech.length == 2)
                                 makat += "0";
                         makat += erech + "00";

                       /*  if (document.getElementById("txtShatYezia").disabled == true)
                             shatYetzia ="";
                         else*/
                            // shatYetzia = document.getElementById("txtShatYezia").value;

                       window.returnValue = "0;;" + document.getElementById("txtShatYezia").value + ";" +
                                               document.getElementById("txtTeurElement").value + ";;;" +
                                               document.getElementById("txtMisRechev").value + ";"+
                                               document.getElementById("txtMisRechev").title + ";" + makat +
                                               ";;;0;2;;" + shatYetzia; //פעילות מסוג אלמנט
                         flag = true;     
                     }
                 }
                 else if (BdikatNesiaa()) {
                    window.returnValue = "1;" + document.getElementById("txtKisuiTor").value + ";" +
                                                document.getElementById("txtShatYeziaKatalog").value + ";" +
                                                document.getElementById("lblTeur").innerText + ";" +
                                                document.getElementById("lblKav").innerText + ";" +
                                                document.getElementById("lblSug").innerText + ";" +
                                                document.getElementById("txtMisRechevKatalog").value + ";" +
                                                document.getElementById("txtMisRechevKatalog").title + ";" +
                                              //  document.getElementById("lblMisRishuy").innerText + ";" +
                                                document.getElementById("lblMakatKatalog").innerText + ";" +
                                                document.getElementById("lblDakotHagdara").innerText + ";" +
                                                document.getElementById("txtDakotBafoal").value + ";" +
                                                "0;2;" +
                                                document.getElementById("txtKisuiTor").attributes("kisuy_tor").value+
                                                ";" + shatYetzia;

                     flag = true;     
                }
                if (flag) {
                    if (document.getElementById("SavePeilut").value == "NO")
                        window.close();
                    else if (document.getElementById("SavePeilut").value == "YES") {
                        document.getElementById("ReturnValue").value = window.returnValue;
                        document.getElementById("btnSave").click();
                     //   window.close();
                    }
                }     
             }
             function BdikatElemnt() {
                 var obj;
                 obj = document.getElementById("txtMisparElement");
                 if (obj.value != "") {
                     if (obj.attributes("KodValid").value == "false") {
                         ShowValidatorCalloutExtender("vldExKod");
                         return false;
                     }
                 }
                 else {
                     document.getElementById("vldKod").errormessage = "חובה להכניס מספר אלמנט";
                     ShowValidatorCalloutExtender("vldExKod");
                     return false;
                 }

                 obj = document.getElementById("txtTeurElement");
                 if (obj.value != "") {
                     if (obj.attributes("TeurValid").value == "false") {
                         ShowValidatorCalloutExtender("vldExTeur");
                         return false;
                     }
                 }
                 else {
                     document.getElementById("vldTeur").errormessage = "חובה להכניס מספר אלמנט";
                     ShowValidatorCalloutExtender("vldExTeur");
                     return false;
                 }

                 obj = document.getElementById("txtHosefErechElement");
                 if (obj.disabled == false) {
                     if (obj.value != "") {
                         if (obj.attributes("ErechValid").value == "false") {
                             ShowValidatorCalloutExtender("vldExErech");
                             return false;
                         }
                     }
                     else {
                         document.getElementById("vldErech").errormessage = "חובה להכניס ערך לאלמנט";
                         ShowValidatorCalloutExtender("vldExErech");
                         return false;
                     }
                 }

                 obj = document.getElementById("txtShatYezia");
                 if (obj.disabled == false) {
                     if (obj.value != "") {
                         if (!IsValidTime(obj.value)) {
                             document.getElementById("vldShatYezia").errormessage = "שעה לא תקינה";
                             ShowValidatorCalloutExtender("vldExShaa");
                             return false;
                         }
                     }
                     else {
                         document.getElementById("vldShatYezia").errormessage = "יש להקליד שעת יציאה בטווח 00:00-23:59";
                         ShowValidatorCalloutExtender("vldExShaa");
                         return false;
                     }
                 }

                 obj = document.getElementById("txtMisRechev");
                 if (obj.disabled == false) {
                     if (obj.value != "") {
                         if (obj.attributes("MisRechevValid").value == "false") {
                             ShowValidatorCalloutExtender("vldExMisRechev");
                             return false;
                         }
                     }
                     else {
                         document.getElementById("vldMisRechev").errormessage = "חובה להכניס מספר רכב";
                         ShowValidatorCalloutExtender("vldExMisRechev");
                         return false;
                     }
                 }
                 return true;
             }

             /*******************************************קטלוג*************************************/
             /*************************************************************************************/
             /**************** מקט *******************/
             function onchange_txtMakat() {
                 $("#tblPeilutNesiaa").css("display", "none");
                 document.getElementById("btnShow").disabled = true;
                 document.getElementById("divButtons").style.display = "none";
                 var Makat = document.getElementById("txtMakat").value;
                 if (IsNumeric(Makat)) {
                     if (Number(Makat) > 99999) {// אורך מקט חייב להיות לפחות 6 ספרות
                         if (Number(Makat).toString().substr(0, 1) == "5" || Number(Makat).toString().substr(0, 1) == "7") {
                             document.getElementById("vldMakat").errormessage = " אין אפשרות להזין מק''ט מסוג ויזה או אלמנט";
                             ShowValidatorCalloutExtender("vldExMakat");
                             document.getElementById("btnShow").disabled = true;
                         }
                         else document.getElementById("btnShow").disabled = false;
                     }
                     //wsGeneral.CheckMakat(Makat, Makat, document.getElementById("txtHiddenTaarichCA").value, CheckMakatTakin);
                     else {
                         document.getElementById("vldMakat").errormessage = " יש להזין מק''ט באורך 6 עד 8 ספרות";
                         ShowValidatorCalloutExtender("vldExMakat");
                         document.getElementById("btnShow").disabled = true;
                     }
                 }
                 else {
                     document.getElementById("vldMakat").errormessage = "יש להזין ערך מספרי חיובי ושלם בלבד";
                     ShowValidatorCalloutExtender("vldExMakat");
                     document.getElementById("btnShow").disabled = true;
                 }
             }
             /**************** דקות *******************/
             function onchange_txtDakot() {
                 var dakotHagdara = document.getElementById("lblDakotHagdara").innerText;
                 var dakotBafoal = document.getElementById("txtDakotBafoal").value;
                 if (IsNumeric(dakotBafoal)) {
                     if (trim(dakotHagdara) == "")
                         dakotHagdara = 0;

                     if (Number(dakotBafoal) > Number(dakotHagdara)) {
                         document.getElementById("vldDakot").errormessage = "ערך דקות בפועל לא יכול לחרוג מדקות הגדרה";
                         ShowValidatorCalloutExtender("vldExDakot");
                         return false;
                     }
                 }
                 else {
                     document.getElementById("vldDakot").errormessage = "יש להזין ערך מספרי חיובי ושלם בלבד";
                     ShowValidatorCalloutExtender("vldExDakot");
                     return false;
                 }
                 return true;
             }
             /****************  מספר רכב/רישוי *******************/
             function onchange_txtMisparRechev() {
                 var Mis_Rechev = document.getElementById("txtMisRechevKatalog").value;
                 if (IsNumeric(Mis_Rechev)) {
                     if (Mis_Rechev != "")
                         wsGeneral.CheckOtoNo(Mis_Rechev, CheckMisRechevSucceded);
                 }
                 else {
                     //?document.getElementById("lblMisRishuy").innerText = "";
                     document.getElementById("txtMisRechevKatalog").title = "";
                     document.getElementById("vldMisRechevKatalog").errormessage = "יש להזין ערך מספרי חיובי ושלם בלבד";
                     ShowValidatorCalloutExtender("vldExMisRechevKatalog");
                 }
             }
             function CheckMisRechevSucceded(result) {
 
               if (result != 0) {
                   //? document.getElementById("lblMisRishuy").innerText = result;
                   document.getElementById("txtMisRechevKatalog").title = result;
                   document.getElementById("IsValidRechevKatalog").value = "true";
                 }
                 else {
                     document.getElementById("txtMisRechevKatalog").title = "";
                     document.getElementById("IsValidRechevKatalog").value = "false";
                    //? document.getElementById("lblMisRishuy").innerText = "";
                     document.getElementById("vldMisRechevKatalog").errormessage  = "מספר רכב לא קיים!";
                     ShowValidatorCalloutExtender("vldExMisRechevKatalog");
                 }
             }

             /****************  כיסוי תור *******************/
             function onchange_txtKisuyTor() {
                 var KisuyTor = document.getElementById("txtKisuiTor").value;
                 var taarich = document.getElementById("txtHiddenTaarichCA").value.split('/');
                 var shaa;
                 var shatKisuyTor;
                 var shatYezia;
                 var dakot;
                 if (KisuyTor != "") {
                     if (IsValidTime(KisuyTor)) {
                         shaa = document.getElementById("txtShatYeziaKatalog").value;
                         
                         if (shaa != "") {
                             if (document.getElementById("ShaatYeziaDate").value != "")
                                 taarich = document.getElementById("ShaatYeziaDate").value.split('/');
                             shatYezia = new Date(taarich[2], taarich[1], taarich[0], shaa.split(':')[0], shaa.split(':')[1], '00');
                             dakot = document.getElementById("txtKisuiTor").attributes("kisuy_tor").value;
                             if (trim(dakot) == "")
                                 dakot = "0";
                             shatKisuyTor = shatYezia;
                             shatKisuyTor.setMinutes(shatYezia.getMinutes() + (dakot * (-1)));
                            // shatKisuyTor = new Date(taarich[2], taarich[1], taarich[0], KisuyTor.split(':')[0], KisuyTor.split(':')[1], '00');
                             if (shatYezia < shatKisuyTor) {
                                 document.getElementById("vldKisuiTor").errormessage = " שעת כסוי תור לא יכולה להיות לאחר שעת יציאה";
                                 ShowValidatorCalloutExtender("vldExvldKisuiTor");
                                 return false;
                             }
                             else if (((shatYezia - shatKisuyTor) / (60 * 1000)) > Number(dakot)) {
                                  document.getElementById("vldKisuiTor").errormessage = " כיסוי התור שהוקלד הינו מעל המותר.מותר עד  " + dakot + " דקות פחות משעת היציאה";
                                 ShowValidatorCalloutExtender("vldExvldKisuiTor");
                                 return false;
                             }
                         }
                     }
                     else {
                         document.getElementById("vldKisuiTor").errormessage = " שעה לא תקינה ";
                         ShowValidatorCalloutExtender("vldExvldKisuiTor");
                         return false;
                     }
                 }
                 return true;
             }
             /*********************************/
             function BdikatNesiaa() {
                 var obj;
                 var flag;
                  obj = document.getElementById("txtKisuiTor");
                 if (obj.disabled == false) {
                     if (obj.value != "") {
                         flag = onchange_txtKisuyTor();
                         if (!flag)
                             return false;
                     }
                 }
                
                 obj = document.getElementById("txtShatYeziaKatalog");
                 if (obj.value != "") {
                     if (!IsValidTime(obj.value)) {
                         document.getElementById("reShatYeziaKatalog").errormessage = "שעה לא תקינה";
                         ShowValidatorCalloutExtender("vldExShatYeziaKatalog");
                         return false;
                     }
                 }
                 else {
                     document.getElementById("reShatYeziaKatalog").errormessage = "חובה להכניס  שעת יציאה";
                     ShowValidatorCalloutExtender("vldExShatYeziaKatalog");
                     return false;
                 }
                 if (document.getElementById("txtMisRechevKatalog").value == "") {
                     document.getElementById("vldMisRechevKatalog").errormessage = "חובה להכניס  מספר רכב";
                     ShowValidatorCalloutExtender("vldExMisRechevKatalog");
                     return false;
                 }
                 else{
                     onchange_txtMisparRechev();
                     //ShowValidatorCalloutExtender("vldExMisRechevKatalog");
                     if (document.getElementById("IsValidRechevKatalog").value == "false") 
                         return false;                
                 }
               /*  else if (document.getElementById("lblMisRishuy").innerText == "") {
                     ShowValidatorCalloutExtender("vldExMisRechevKatalog");
                     return false;
                 }*/
                
                 obj = document.getElementById("txtDakotBafoal");
                 if (obj.value != "") {
                     flag = onchange_txtDakot();
                     if (!flag)
                         return false;
                 }

                 return true;
             }

             function ShatYezia_onchange() {
                 var shaa;
                 var vld;
                 var vldEx;
                 var dakot;
                 var flag = true;
                 var Param29 = document.getElementById("Params").attributes("Param29").value;

                 if (document.getElementById("Sug_Peilut").value == "1") {
                     shaa = document.getElementById("txtShatYeziaKatalog").value;
                     dakot = document.getElementById("txtKisuiTor").attributes("kisuy_tor").value; 
                     vld = "reShatYeziaKatalog";
                     vldEx = "vldExShatYeziaKatalog";
                 }
                 else {
                     shaa = document.getElementById("txtShatYezia").value;
                     vld = "vldShatYezia";
                     vldEx = "vldExShaa";
                 }

                 if (!IsValidTime(shaa) && shaa != Param29) {
                     document.getElementById(vld).errormessage = "יש להקליד שעת יציאה בטווח  " + Param29 + "-23:59";
                     ShowValidatorCalloutExtender(vldEx);
                     flag = false;
                 }

                 if (flag) {
                     if (IsShatGmarInNextDay(shaa)) {
                         document.getElementById("btnHosafa").disabled = true;
                         document.getElementById("btnShowMessage").click();
                         flag = false;
                      }
                     else if (shaa == Param29) {
                         document.getElementById("ShaatYeziaDate").value = returnNextDate(); 
                     }
                    
                    if (flag) {
                        if (dakot != null && document.getElementById("txtKisuiTor").disabled == false)
                            setKisuiTor(dakot, shaa);
                    }
                    
                }
            }
            function setKisuiTor(dakot, shaa) {
                var shatYezia;
                var taarich;
                var shatKisuyTor;
                var kisuy_tor;
                if (document.getElementById("ShaatYeziaDate").value != "")
                    taarich = document.getElementById("ShaatYeziaDate").value.split('/');
                else taarich = document.getElementById("txtHiddenTaarichCA").value.split('/');
                
                shatYezia = new Date(taarich[2], taarich[1] - 1, taarich[0], shaa.split(':')[0], shaa.split(':')[1], '00');
                shatKisuyTor = shatYezia;
                shatKisuyTor.setMinutes(shatYezia.getMinutes() + (dakot * (-1)));
                
                if (shatYezia.getHours()> 9)
                    kisuy_tor = shatYezia.getHours() +":";
                else kisuy_tor = "0" + shatYezia.getHours() + ":";
                if (shatYezia.getMinutes() > 9)
                    kisuy_tor += shatYezia.getMinutes();
                else kisuy_tor += "0" + shatYezia.getMinutes();

                document.getElementById("txtKisuiTor").value = kisuy_tor;  
            }
            
             function btnShowMessage_Click() {
                 $find('ModalPopupEx').show;
             }
             function btnNochachi_click() {
                 var dakot;
                 var shaa;
                 $find('ModalPopupEx').hide();
                 document.getElementById("ShaatYeziaDate").value = document.getElementById("txtHiddenTaarichCA").value;
                 document.getElementById("btnHosafa").disabled = false;
                 if (document.getElementById("Sug_Peilut").value == "1") {
                     dakot = document.getElementById("txtKisuiTor").attributes("kisuy_tor").value;
                     shaa = document.getElementById("txtShatYeziaKatalog").value;
                     if (dakot != null && document.getElementById("txtKisuiTor").disabled == false)
                         setKisuiTor(dakot, shaa);
                 }
             }
             function btnHaba_click() {
                 var dakot;
                 var shaa;
                 $find('ModalPopupEx').hide();

                 document.getElementById("ShaatYeziaDate").value = returnNextDate();
                 document.getElementById("btnHosafa").disabled = false;
                 if (document.getElementById("Sug_Peilut").value == "1") {
                     dakot = document.getElementById("txtKisuiTor").attributes("kisuy_tor").value;
                     shaa = document.getElementById("txtShatYeziaKatalog").value;
                     if (dakot != null && document.getElementById("txtKisuiTor").disabled == false)
                         setKisuiTor(dakot, shaa);
                 }
             }
             function returnNextDate() {
                 var taarich;
                 var shatYeziaDate;

                 taarich = document.getElementById("txtHiddenTaarichCA").value.split('/');
                 shatYeziaDate = new Date(taarich[2], taarich[1] - 1, taarich[0]);

                 return (new Date(shatYeziaDate.setDate(shatYeziaDate.getDate() + 1))).format("dd/MM/yyyy");
             }
             function window_onload() {
                 SerCursor('wait');
                 document.getElementById("btnElement").focus();
                 document.getElementById("btnElement").click();
             }

             function OnShown(sender, eventargs) {
                // alert($find('AutoMisElement'));
             // $find('AutoMisElement').hidePopup();
                 if (document.getElementById("txtHosefErechElement").isDisabled == false &&
                        document.getElementById("btnHosafatNesiaa").disabled == false) {
                     document.getElementById("txtHosefErechElement").focus();
                 }
             }

             function SerCursor(sStatus) {
                 document.body.style.cursor = sStatus;
             }

    </script>
   
    <style type="text/css">
        .style1
        {
            width: 494px;
        }
    </style>
   
</head>
<body dir="rtl" onload="return window_onload()"  onkeydown="if (event.keyCode==107) {event.keyCode=9; return event.keyCode }" >
 
    <form id="form1" runat="server">
              <asp:ScriptManager  runat="server"  id="ScriptManagerKds" EnablePartialRendering="true" EnablePageMethods="true">        
                <Scripts >
                <asp:ScriptReference Path="~/Js/String.js" />
                <asp:ScriptReference Path="~/Js/GeneralFunction.js" />
                <asp:ScriptReference Path="~/Js/jquery.js" />
                </Scripts>
               </asp:ScriptManager>
               
               <asp:UpdateProgress  runat="server" id="UpdateProgress1" DisplayAfter="0" >
                    <ProgressTemplate>
                        <div id="divProgress" class="Progress"  style="text-align:right;position:absolute;left:52%;top:48%; z-index:1000"   >
                              <asp:Image ID="Image2" runat="server" ImageUrl="../../Images/Eggedprogress.gif" style="width: 100px; height: 100px" />טוען...
                        </div>        
                    </ProgressTemplate>
               </asp:UpdateProgress>
              
            <div>
            <table style="width:100%" >
                  <tr class="GridHeader"><td > חיפוש והוספת פעילות</td></tr>
                <tr>
                    <td>
                   
                        <fieldset  style="height:42px" >  <legend   id="LegendFilter" style="background-color:White" >סוג פעילות לחיפוש: </legend> 
                                &nbsp;&nbsp;
                                <input type="button" id="btnNesiot" class="ImgButtonSearch" value="חיפוש נסיעה" style="width:100px;" disabled="true" />
                                &nbsp;&nbsp;
                                <input type="button" id="btnHosafatNesiaa" class="ImgButtonSearch"  value="הוספת נסיעה מקטלוג"  style="width:160px" onclick="btnHosafatNesiaa_Click();" />
                                 &nbsp;&nbsp;
                                <input type="button" id="btnElement" class="ImgButtonSearch" value="הוספת אלמנט" style="width:120px" onclick="btnElement_Click();" />
                            
                        </fieldset>  
                    </td>
                </tr>
            </table>
            <br />          
        </div>
        
            <div style="width:100%" >
            
             <input type="button" ID="btnShowMessage" runat="server" onclick="btnShowMessage_Click()" Style="display: none;" />
                        <cc1:ModalPopupExtender ID="ModalPopupEx" DropShadow="false" X="300" Y="150" PopupControlID="paMessage"
                            TargetControlID="btnShowMessage"  runat="server">
                        </cc1:ModalPopupExtender>
                        <asp:Panel runat="server" Style="display: none" ID="paMessage" CssClass="PanelMessage" Width="350px">
                             <asp:Label ID="lblHeaderMessage" runat="server" Width="97%" BackColor="#696969" ForeColor="White">קביעת תאריך</asp:Label>
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
            
        <div id="divHosafatElement" runat="server" style="display:none">
    
             <fieldset class="FilterFieldSet">
                 <asp:UpdatePanel ID="upRdoId" runat="server" RenderMode="Inline">
                            <ContentTemplate> 
                               <table style="width:100%;border-color:Black;border-width:0px;border-style:outset;"  >
                                   <tr>
                                        <td style="width:30%"> 
                                             <asp:RadioButton runat="server" Checked="true" ID="rdKod"  EnableViewState="true" GroupName="grpSearch" Text="קוד אלמנט:"  > </asp:RadioButton>                                        
                                             <asp:TextBox ID="txtMisparElement" runat="server"  width="80px" MaxLength="2" onChange="CheakKodElement()"  TabIndex="1"  ></asp:TextBox>
                                                  <cc1:AutoCompleteExtender id="AutoMisElement"  runat="server" CompletionInterval="0" CompletionSetCount="25" UseContextKey="false"  
                                                    TargetControlID="txtMisparElement" MinimumPrefixLength="1" ServiceMethod="GetElements" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                                    EnableCaching="true"  CompletionListCssClass="autocomplete_completionListElement" 
                                                    CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select" OnClientHidden ="OnShown"
                                                    CompletionListItemCssClass="autocomplete_completionListItemElement"   >                               
                                                </cc1:AutoCompleteExtender>  
                                             <asp:CustomValidator runat="server" id="vldKod"   ControlToValidate="txtMisparElement" ErrorMessage=""   Display="None"   ></asp:CustomValidator>
                                             <cc1:ValidatorCalloutExtender runat="server" ID="vldExK" BehaviorID="vldExKod"   TargetControlID="vldKod" Width="200px" PopupPosition="Left"  ></cc1:ValidatorCalloutExtender>                                                
                                        </td>
                                        <td style="width:40%">  
                                             <asp:RadioButton runat="server" ID="rdTeur" EnableViewState="true" GroupName="grpSearch" Text="תאור אלמנט:" > </asp:RadioButton>
                                             <asp:TextBox ID="txtTeurElement" runat="server" onChange="CheckTeurElement()"  width="141px" TabIndex="2"></asp:TextBox>
                                                 <cc1:AutoCompleteExtender id="AautoTeurElement"  runat="server" CompletionInterval="0" CompletionSetCount="25" UseContextKey="false"  
                                                    TargetControlID="txtTeurElement" MinimumPrefixLength="1" ServiceMethod="GetTeurElements" ServicePath="~/Modules/WebServices/wsGeneral.asmx" 
                                                    EnableCaching="true"  CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListHighlightedItemCssClass="autocomplete_completionListItemElement_Select"
                                                    CompletionListItemCssClass="autocomplete_completionListItemElement">                               
                                                </cc1:AutoCompleteExtender>   
                                             <asp:CustomValidator runat="server" id="vldTeur" ControlToValidate="txtTeurElement" ErrorMessage=""   Display="None"   ></asp:CustomValidator>
                                             <cc1:ValidatorCalloutExtender  runat="server" ID="vldExT" BehaviorID="vldExTeur"   TargetControlID="vldTeur" Width="200px" PopupPosition="Right" ></cc1:ValidatorCalloutExtender>                                                
                                        </td>
                                        <td style="width:35%"> 
                                             <label for="txtSugElement" class="InternalLabel">סוג אלמנט:</label>
                                             <asp:TextBox ID="txtSugElement" runat="server" Enabled="false" width="60px" TabIndex="3"></asp:TextBox>
                                          
                                        </td>
                                   </tr>
                                     <tr>
                                        <td> 
                                             <label for="txtHosefErechElement" class="InternalLabel">&nbsp;&nbsp;&nbsp;&nbsp; הוסף ערך לאלמנט:</label>
                                             <asp:TextBox ID="txtHosefErechElement" runat="server"  onChange="CheakErechLeElemnt()"   Enabled="false" width="55px" MaxLength="3" TabIndex="4"  ></asp:TextBox>
                                             <asp:CustomValidator runat="server" id="vldErech" ControlToValidate="txtHosefErechElement" ErrorMessage=""   Display="None"   ></asp:CustomValidator>
                                             <cc1:ValidatorCalloutExtender  runat="server" ID="VceErech" BehaviorID="vldExErech"   TargetControlID="vldErech" Width="200px" PopupPosition="Left" ></cc1:ValidatorCalloutExtender>                                                
                                       
                                        </td>
                                        <td> 
                                             <label for="txtShatYezia" class="InternalLabel">&nbsp;&nbsp;&nbsp;&nbsp; שעת יציאה:&nbsp; </label>
                                            &nbsp;<asp:TextBox runat="server" MaxLength="5" ID="txtShatYezia" Width="73px"  onchange="ShatYezia_onchange()" TabIndex="5"
                                                 AutoPostBack="false"  ></asp:TextBox>
                                            <cc1:MaskedEditExtender ID="extMaskStartTime" runat="server" TargetControlID="txtShatYezia" MaskType="Time" UserTimeFormat="TwentyFourHour" Mask="99:99"  ></cc1:MaskedEditExtender>
                                             <asp:CustomValidator runat="server" id="vldShatYezia"   ControlToValidate="txtShatYezia" ErrorMessage=""   Display="None"   ></asp:CustomValidator>
                                        <%--   <asp:RegularExpressionValidator  runat="server" id="vldShatYezia" EnableClientScript="true" Display="none" ErrorMessage="יש להקליד שעת יציאה בטווח 00:00-23:59" ControlToValidate="txtShatYezia"   ValidationExpression="^([0-1]?\d|2[0-3])(:[0-5]\d){1,2}$"></asp:RegularExpressionValidator>--%>
                                            <cc1:ValidatorCalloutExtender runat="server" ID="exvldShatYezia" BehaviorID="vldExShaa"  TargetControlID="vldShatYezia" Width="200px" PopupPosition="Left"></cc1:ValidatorCalloutExtender>                                             

                                        </td>
                                        <td> 
                                             <label for="txtMisRechev" class="InternalLabel">מספר רכב:</label>
                                             <asp:TextBox ID="txtMisRechev" runat="server" onChange="onChange_txtMisRechev()"    Enabled="false" width="60px" MaxLength="6" TabIndex="6"  ></asp:TextBox>
                                          <%--   <asp:RegularExpressionValidator  runat="server" id="REVmisRechev" EnableClientScript="true" Display="none" ErrorMessage="יש להקליד שעת יציאה בטווח 00:00-23:59" ControlToValidate="txtMisRechev" ValidationExpression="^\d{0,9}$"></asp:RegularExpressionValidator>
                                         --%>  <asp:CustomValidator runat="server" id="vldMisRechev" ControlToValidate="txtMisRechev" ErrorMessage=""   Display="None"   ></asp:CustomValidator>
                                             <cc1:ValidatorCalloutExtender  runat="server" ID="exvldMisRechev" BehaviorID="vldExMisRechev"   TargetControlID="vldMisRechev" Width="200px" PopupPosition="Right" ></cc1:ValidatorCalloutExtender>                                                
                                        </td>
                                   </tr>
                               </table> 
                        </ContentTemplate>
                    </asp:UpdatePanel>
              </fieldset>
            </div> 
             <div id="divPeilutKatalog" runat="server" style="display:none">
    
                     <fieldset class="FilterFieldSet">
                         <asp:UpdatePanel ID="UpKatalog" runat="server" UpdateMode="Always" >
                                    <ContentTemplate> 
                                          <label for="txtMakat" class="InternalLabel">&nbsp;&nbsp;&nbsp;&nbsp; מק''ט:</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                          <asp:TextBox ID="txtMakat" runat="server" onchange="onchange_txtMakat()"    Enabled="true" width="125px" MaxLength="8"  ></asp:TextBox>
                                          <asp:CustomValidator runat="server" id="vldMakat" ControlToValidate="txtMakat" ErrorMessage=""   Display="None"   ></asp:CustomValidator>
                                          <cc1:ValidatorCalloutExtender  runat="server" ID="exvldMakat" BehaviorID="vldExMakat"   TargetControlID="vldMakat" Width="200px" PopupPosition="Left" ></cc1:ValidatorCalloutExtender>                                                
                                       
                                          &nbsp;&nbsp;
                                          <asp:Button ID="btnShow" runat="server" Text="הצג" Enabled="false" style="width:auto;" OnClick="btnShow_OnClick"   CssClass="ImgButtonSearch" />
                                          <br />  <br />        
                                     <table id="tblPeilutNesiaa" runat="server"  style="width:100%;border-color:Black;border-width:1px;border-style:outset;display:none"  >
                                           <tr class="GridHeaderSecondary" >
                                               <td>כיסוי תור</td>
                                               <td>שעת יציאה</td>
                                               <td>תאור</td>
                                               <td>קו</td>
                                               <td>סוג</td>
                                               <td>מספר רכב</td>
                                               <%--<td>מספר רישוי</td>--%>
                                               <td>מק''ט</td>
                                               <td>דקות הגדרה</td>
                                               <td>דקות בפועל</td>
                                           </tr>
                                           <tr>
                                               <td style="width:6%"><asp:TextBox ID="txtKisuiTor" runat="server" onchange="onchange_txtKisuyTor()" Width="45px" ></asp:TextBox>
                                                                 <cc1:MaskedEditExtender ID="extKisuiTor" runat="server" TargetControlID="txtKisuiTor" MaskType="Time" UserTimeFormat="TwentyFourHour" Mask="99:99"  ></cc1:MaskedEditExtender>
                                                                 <asp:RegularExpressionValidator  runat="server" id="vldKisuiTor" EnableClientScript="true"  Display="none" ErrorMessage="יש להקליד שעת יציאה בטווח 00:00-23:59" ControlToValidate="txtKisuiTor"   ValidationExpression="^([0-1]?\d|2[0-3])(:[0-5]\d){1,2}$"></asp:RegularExpressionValidator>
                                                                 <cc1:ValidatorCalloutExtender runat="server" ID="exvldKisuiTor" BehaviorID="vldExvldKisuiTor"  TargetControlID="vldKisuiTor" Width="200px" PopupPosition="Left"></cc1:ValidatorCalloutExtender>         
                                               </td>
                                               <td style="width:6%"><asp:TextBox ID="txtShatYeziaKatalog" runat="server" Width="45px" onchange="ShatYezia_onchange()"  ></asp:TextBox>
                                                                <cc1:MaskedEditExtender ID="MaskShaaKatalog" runat="server" TargetControlID="txtShatYeziaKatalog" MaskType="Time" UserTimeFormat="TwentyFourHour" Mask="99:99"  ></cc1:MaskedEditExtender>
                                                                 <asp:CustomValidator runat="server" id="reShatYeziaKatalog"   ControlToValidate="txtShatYeziaKatalog" ErrorMessage=""   Display="None"   ></asp:CustomValidator>
                                                                <%--<asp:RegularExpressionValidator  runat="server" id="reShatYeziaKatalog" EnableClientScript="true" Display="none" ErrorMessage="יש להקליד שעת יציאה בטווח 00:00-23:59" ControlToValidate="txtShatYeziaKatalog"   ValidationExpression="^([0-1]?\d|2[0-3])(:[0-5]\d){1,2}$"></asp:RegularExpressionValidator>--%>
                                                                <cc1:ValidatorCalloutExtender runat="server" ID="vceShatYeziaKatalog" BehaviorID="vldExShatYeziaKatalog"  TargetControlID="reShatYeziaKatalog" Width="200px" PopupPosition="Left"></cc1:ValidatorCalloutExtender>                                             

                                               </td>
                                               <td style="width:25%"><asp:Label ID="lblTeur" runat="server"  Width="200px"></asp:Label></td>
                                               <td style="width:7%"><asp:Label ID="lblKav" runat="server"  Width="56px"></asp:Label></td>
                                               <td style="width:10%"><asp:Label ID="lblSug" runat="server"  Width="77px"  ></asp:Label></td>
                                               <td style="width:10%"><asp:TextBox ID="txtMisRechevKatalog" runat="server" Width="75px" onchange="onchange_txtMisparRechev()"  ></asp:TextBox>
                                                     <asp:CustomValidator runat="server" id="vldMisRechevKatalog" ControlToValidate="txtMisRechevKatalog" ErrorMessage=""   Display="None"   ></asp:CustomValidator>
                                                 <cc1:ValidatorCalloutExtender  runat="server" ID="VCEMisRechevKatalog" BehaviorID="vldExMisRechevKatalog"   TargetControlID="vldMisRechevKatalog" Width="200px" PopupPosition="Left" ></cc1:ValidatorCalloutExtender>                                                
                                               </td>
                                          <%--     <td style="width:10%"><asp:Label ID="lblMisRishuy" runat="server" Width="70px"  ></asp:Label></td>--%>
                                               <td style="width:10%"><asp:Label ID="lblMakatKatalog" runat="server"  Width="79px" ></asp:Label></td>
                                               <td style="width:8%"><asp:Label ID="lblDakotHagdara" runat="server" Width="27px" ></asp:Label></td>
                                               <td style="width:8%"><asp:TextBox ID="txtDakotBafoal" runat="server" Width="62px" onchange="onchange_txtDakot()" ></asp:TextBox>
                                                 <asp:CustomValidator runat="server" id="vldDakot" ControlToValidate="txtDakotBafoal" ErrorMessage=""   Display="None"   ></asp:CustomValidator>
                                                 <cc1:ValidatorCalloutExtender  runat="server" ID="VCEdakot" BehaviorID="vldExDakot"   TargetControlID="vldDakot" Width="200px" PopupPosition="Right" ></cc1:ValidatorCalloutExtender>                                                
                                               </td>
                                               
                                           </tr>
                                       </table> 
                                </ContentTemplate>
                            </asp:UpdatePanel>
                      </fieldset>
             </div>
             <br />
             <asp:UpdatePanel ID="UPbuttons" runat="server" UpdateMode="Always">
                 <ContentTemplate>      
                        <div id="divButtons" runat="server" style="display:none" >
                          <table style="vertical-align:bottom">
                          
                            <tr>
                            
                                <td align="right" class="style1">
                                 <asp:Button ID="btnSave" runat="server"  onclick="btnSave_Click"  />
                                    <input type="button" class="ImgButtonSearch" value="סגור" onclick="window.returnValue = ''; window.close();" />
                                 </td>
                                <td align="left" style="width:50%">
                                <input id="btnHosafa" runat="server" type="button" class="ImgButtonSearch" 
                                        style="width:148px"  value="הוסף פעילות" onclick="btnHosafa_OnClick();" />
                                    
                                    
                                </td>
                            </tr>
                          </table>
                        </div>
                 </ContentTemplate>
           </asp:UpdatePanel>          
            <%--  <input id="WhithOutErrorsShaa" name="WhithOutErrorsShaa" runat="server" type="hidden" value="true" />
              <input id="WhithOutErrors" name="WhithOutErrors" runat="server" type="hidden" value="true" />
            --%>  <input id="txtHiddenMisparSidur" name="txtHiddenMisparSidur" runat="server" type="hidden" />
              <input id="txtHiddenTaarichCA" name="txtHiddenTaarichCA" runat="server" type="hidden"  />
            
          <input id="IsValidRechevKatalog" name="IsValidRechevKatalog" runat="server" type="hidden" />

          <input id="Sug_Peilut" name="Sug_Peilut" runat="server" type="hidden"  />
          <input id="SavePeilut" name="SavePeilut" runat="server" type="hidden"  />
          <input id="ReturnValue" name="ReturnValue" runat="server" type="hidden" />
          <input id="txtHiddenHourHatchaltSidur" name="txtHiddenHourHatchaltSidur" runat="server" type="hidden" />
          <input id="txtHiddenMisparIshi" name="txtHiddenMisparIshi" runat="server" type="hidden" />
          <input id="ElementsRelevants" name="ElementsRelevants" runat="server" type="hidden"  />
          <input id="ShaatYeziaDate" name="ShaatYeziaDate" runat="server" type="hidden"  />
         <input type="hidden" id="Params" name="Params"  runat="server" />
    </form>
    
   
    
</body>
</html>
