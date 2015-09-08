function window_onload() {
    document.getElementById("btnElement").focus();
    document.getElementById("btnElement").click();
    document.getElementById("btnAdd").disabled = true;
    
      setBorderBtns();
             load();
}
function btnElement_Click() {
    $("#divHosafatElement").css("display", "block");
    $("#divPeilutKatalog").css("display", "none");
    NikuySadot(true);
    document.getElementById("Sug_Peilut").value = "2";
    document.getElementById("btnElement").disabled = true;
    document.getElementById("btnHosafatNesiaa").disabled = false;
    document.getElementById("txtMisparElement").value = "";
    document.getElementById("txtTeurElement").value = "";
    document.getElementById("txtErechElement").value = "";
    document.getElementById("txtSugElement").value = "";
    document.getElementById("txtMisparElement").focus();

}
function btnHosafatNesiaa_Click() {
    $("#divPeilutKatalog").css("display", "block");
    $("#divHosafatElement").css("display", "none");
    document.getElementById("Sug_Peilut").value = "1";
    document.getElementById("btnElement").disabled = false;
    document.getElementById("btnHosafatNesiaa").disabled = true;
    document.getElementById("txtMakat").value = "";
    document.getElementById("txtMakat").focus();

}

function NikuySadot(all) {
    document.getElementById("txtSugElement").value = "";
    document.getElementById("txtErechElement").value = "";
    document.getElementById("txtErechElement").disabled = true;
    if (document.getElementById("rdKod").checked)
        document.getElementById("txtTeurElement").value = "";

    if (all) {
        document.getElementById("txtMisparElement").value = "";
        document.getElementById("txtTeurElement").value = "";
        document.getElementById("rdKod").checked = true;
        SetTextBox();
    }
}

function SamenHakol_OnClick() {
    var num = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.length;
    for (var i = 1; i < num; i++) {
        if (document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i).childNodes.item(col_HosefPeilut).childNodes.item(0).disabled == false)
            document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i).childNodes.item(col_HosefPeilut).childNodes.item(0).checked = true;
        else
            document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i).childNodes.item(col_HosefPeilut).childNodes.item(0).childNodes.item(0).checked = true;
        // document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i).childNodes.item(col_HosefPeilut).childNodes.item(0).checked = true;

    }
}

function NakeHakol_OnClick() {
    var num = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.length;
    for (var i = 1; i < num; i++) {
        if (document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i).childNodes.item(col_HosefPeilut).childNodes.item(0).disabled == false)
            document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i).childNodes.item(col_HosefPeilut).childNodes.item(0).checked = false;
        else
            document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i).childNodes.item(col_HosefPeilut).childNodes.item(0).childNodes.item(0).checked = false;
       
      //   document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i).childNodes.item(col_HosefPeilut).childNodes.item(0).checked = false;

    }
}

function SamenKnisot(iIndexRow) {
    var oRowAv, j, oRow;

    var num = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.length;
    oRowAv = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(iIndexRow);
    if (Number(oRowAv.childNodes.item(col_Mispar_Knisa).innerHTML) == 0) {
        for (j = iIndexRow + 1; j < num; j++) {
            oRow = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(j);
            if ((oRow.childNodes.item(col_Makat).innerHTML == oRowAv.childNodes.item(col_Makat).innerHTML && Number(oRow.childNodes.item(col_Mispar_Knisa).innerHTML) > 0 && document.getElementById(oRow.id + "_txtDakotBafoal").style.display == 'none') ||
               ((oRow.childNodes.item(col_Makat).innerHTML.substr(0, 3) == "700" || oRow.childNodes.item(col_Makat).innerHTML.substr(0, 3) == "761" ||
                 oRow.childNodes.item(col_Makat).innerHTML.substr(0, 3) == "784") &&
                oRow.childNodes.item(col_HosefPeilut).childNodes.item(0).disabled))
                oRow.childNodes.item(col_HosefPeilut).childNodes.item(0).childNodes.item(0).checked = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(iIndexRow).childNodes.item(col_HosefPeilut).childNodes.item(0).checked;
            else if (oRow.childNodes.item(col_Makat).innerHTML == oRowAv.childNodes.item(col_Makat).innerHTML && Number(oRow.childNodes.item(col_Mispar_Knisa).innerHTML) > 0) {
            if (document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(iIndexRow).childNodes.item(col_HosefPeilut).childNodes.item(0).checked == true)
                oRow.childNodes.item(col_HosefPeilut).childNodes.item(0).disabled = false;
            else {
                oRow.childNodes.item(col_HosefPeilut).childNodes.item(0).disabled = true;
                oRow.childNodes.item(col_HosefPeilut).childNodes.item(0).checked = false;
            }
            }
            else if (oRow.childNodes.item(col_Makat).innerHTML != oRowAv.childNodes.item(col_Makat).innerHTML)
                break;
        }
    }
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
function OnShown(sender, eventargs) {
    if (document.getElementById("txtErechElement").isDisabled == false &&
                        document.getElementById("btnHosafatNesiaa").disabled == false) {
        document.getElementById("txtErechElement").focus();
    }
}

function ShowValidatorCalloutExtender(sBehaviorId) {
    $find(sBehaviorId)._ensureCallout();
    $find(sBehaviorId).show(true);
}

function checkMustErech() {
    if (bShowMassage)
        return false;
    else {
        if (document.getElementById("MustErech").value == "true" && document.getElementById("txtErechElement").value.length == 0) {
            document.getElementById("vldErech").errormessage = "חובה להזין ערך";
            ShowValidatorCalloutExtender("vldExErech");

            return false;
        }
        else return true;
    }
}

/**************** מקט *******************/
function checkMakat() {
    document.getElementById("btnHosafa").style.display = "none";
    var Makat = document.getElementById("txtMakat").value;
    if (IsNumeric(trim(Makat))) {
        if (Number(Makat) < 99999) {// אורך מקט חייב להיות לפחות 6 ספרות
//            if ( Number(Makat).toString().substr(0, 1) == "7") {
//                document.getElementById("vldMakat").errormessage = " אין אפשרות להזין מק''ט מסוג אלמנט";
//                ShowValidatorCalloutExtender("vldExMakat");
//                return false;
//            }
//            else return true;
//        }
//        else {
            document.getElementById("vldMakat").errormessage = " יש להזין מק''ט באורך 6 עד 8 ספרות";
            ShowValidatorCalloutExtender("vldExMakat");
            return false;
        } else return true;
    }
    else {
        document.getElementById("vldMakat").errormessage = "יש להזין ערך מספרי חיובי ושלם בלבד";
        ShowValidatorCalloutExtender("vldExMakat");
        return false;
    }
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
        if (document.getElementById("txtMisparElement").value != "") {
            CheckKodElement();
        }
    }

}

/************************** קוד אלמנט *********************************/
function CheckKodElement() {
    NikuySadot(false);
    var Sidur = document.getElementById("txtHiddenMisparSidur").value;
    var taarichCA = document.getElementById("txtHiddenTaarichCA").value;
    var Kod = document.getElementById("txtMisparElement").value;
    if (!IsNumeric(Kod)) {
        document.getElementById("vldKod").errormessage = "יש להזין ערך מספרי חיובי ושלם בלבד";
        ShowValidatorCalloutExtender("vldExKod");
        document.getElementById("txtMisparElement").value = "";
        document.getElementById("txtMisparElement").attributes("KodValid").value = "false";
        document.getElementById("btnAdd").disabled = true;
    }
    else if (Kod != "")
        if (document.getElementById("ElementsRelevants").value.indexOf("," + Kod + ",") == -1) {
        document.getElementById("vldKod").errormessage = "אלמנט שגוי";
        ShowValidatorCalloutExtender("vldExKod");
        document.getElementById("txtMisparElement").attributes("KodValid").value = "false";
        document.getElementById("btnAdd").disabled = true;
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
    switch (result) {
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
        document.getElementById("btnAdd").disabled = true;
    }
    else {

        MiluyPirteyElement(result);
        document.getElementById("txtMisparElement").attributes("KodValid").value = "true";
        document.getElementById("btnAdd").disabled = false;
        focus_erech();
    }
  
}


function focus_erech() {
    if (document.getElementById("txtErechElement").disabled == false) 
        document.getElementById('txtErechElement').focus();
   else document.getElementById("btnAdd").focus();
}

function on_blur_btn(obj) {
    if (obj.id == "btnHosafa" || (obj.id == "btnSgor" && document.getElementById("btnHosafa").style.display == "none")) {
        if (document.getElementById("btnHosafatNesiaa").disabled == true)
            document.getElementById("btnElement").focus();
        else document.getElementById("btnHosafatNesiaa").focus();
    }
}
/************************** מילוי פרטים *********************************/
function MiluyPirteyElement(result) {
    var Pratim;
    Pratim = result.split(';');
    document.getElementById("txtTeurElement").value = Pratim[0];
    document.getElementById("txtErechElement").disabled = false;
    document.getElementById("MustErech").value = true;
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
                document.getElementById("txtSugElement").value = "לידיעה";
                document.getElementById("txtErechElement").disabled = true;
                document.getElementById("btnAdd").disabled = false;
                document.getElementById("MustErech").value = false;
            }
    }
    if (Pratim[2] == "1") //חובה רכב
        document.getElementById("MustRechev").value = true;
    else
        document.getElementById("MustRechev").value = false;
    
}

/************************** ערך אלמנט *********************************/
function CheckErechLeElement() {
    var kod = document.getElementById("txtMisparElement").value;
    var taarichCA = document.getElementById("txtHiddenTaarichCA").value;
    if (kod != "")
        wsGeneral.getMigbalaLeErechElement(kod, taarichCA, ErechLeElemntSucceded);

}
function ErechLeElemntSucceded(result) {
    var pirteyMigbala;
    var erech = document.getElementById("txtErechElement").value;
    pirteyMigbala = result.split("-");
    if (!IsNumeric(erech)) {
        document.getElementById("vldErech").errormessage = "יש להזין ערך מספרי חיובי ושלם בלבד";
        ShowValidatorCalloutExtender("vldExErech");
        document.getElementById("txtErechElement").attributes("ErechValid").value = "false";
        document.getElementById("btnAdd").disabled = true;
    }
    else if (erech < Number(pirteyMigbala[0]) || erech > Number(pirteyMigbala[1])) {
        document.getElementById("vldErech").errormessage = "יש להזין ערך לאלמנט בטווח " + pirteyMigbala[1] + " - " + pirteyMigbala[0];
        ShowValidatorCalloutExtender("vldExErech");
        document.getElementById("txtErechElement").attributes("ErechValid").value = "false";
        document.getElementById("btnAdd").disabled = true;
    }
    else {
        document.getElementById("txtErechElement").attributes("ErechValid").value = "true";
        document.getElementById("btnAdd").disabled = false;
    }
}

function onchange_txtShatYezia(row, ask, choice, iRowIndex, bChangKisuyTor) {
    var shaa = document.getElementById(row.id + "_txtShatYezia").value;
    var vld = document.getElementById(row.id + "_vldShatYezia");
    var Param29 = document.getElementById("Params").attributes("Param29").value;
    var taarich = document.getElementById(row.id + "_txtShatYeziaDate").value.split(' ')[0].split('/');
    var taarichCard = document.getElementById("txtHiddenTaarichCA").value.split('/');
    var DateCard = new Date(taarichCard[2], taarichCard[1] - 1, taarichCard[0], shaa.split(':')[0], shaa.split(':')[1], '00');

    var KisuiTor = new Date();
    iRowIndexNochehi = iRowIndex;
   
    if (trim(shaa) != "") {
        if (IsValidTime(shaa)) {
            
            shatYeziaDate = new Date(taarich[2], taarich[1] - 1, taarich[0], shaa.split(':')[0], shaa.split(':')[1], '00');

            var shatYeziaParam = new Date(taarich[2], taarich[1] - 1, taarich[0], Param29.split(':')[0], Param29.split(':')[1], '00');
            if (shatYeziaDate < shatYeziaParam) {
                document.getElementById(vld).errormessage = "יש להקליד שעת יציאה בטווח  " + Param29 + "-23:59";
                ShowValidatorCalloutExtender(vldEx);
                return false;
            }

            var shatYeziaTmp = new Date(taarich[2], taarich[1] - 1, taarich[0], '00', '00', '00');
           
            if (shatYeziaDate.format("dd/MM/yyyy HH:mm") == shatYeziaTmp.format("dd/MM/yyyy HH:mm"))
                shatYeziaDate = new Date(DateCard.setDate(DateCard.getDate() + 1));

            else if (ask) {
            if (choice != "2") {
                shatYeziaDate = DateCard;
            }
           
            if (IsShatGmarInNextDay(shaa)) {
             
                    if (choice == "") {
                        stop = true;
                        document.getElementById("DestTime").value = "yezia;" + row.id;
                        document.getElementById("btnShowMessage").click();
                    }
                    else {
                        if (choice == "2")
                            shatYeziaDate = new Date(DateCard.setDate(DateCard.getDate() + 1));
                    }

                }
            }
            document.getElementById(row.id + "_txtShatYeziaDate").value = shatYeziaDate.format("dd/MM/yyyy HH:mm");
               
            if (bChangKisuyTor) {
                var iKisuiTor = document.getElementById(row.id + "_txtShatYezia").attributes("kisuyTor").value;
                 if (IsNumeric(iKisuiTor)) {
                    if (iKisuiTor > 0) {
                        KisuiTor.setHours(shaa.substr(0, 2));
                        KisuiTor.setMinutes(shaa.substr(shaa.length - 2, 2));
                        KisuiTor.setMinutes(KisuiTor.getMinutes() - Number(iKisuiTor));
                        document.getElementById(row.id + "_txtKisuiTor").value = KisuiTor.format("HH:mm");
                    }
                }
            }
            if (ask) CopyShatYetziaToPeiluyot(shatYeziaDate, iRowIndex + 1);
        }
        else {
            vld.errormessage = " שעה לא תקינה ";
            ShowValidatorCalloutExtender(row.id + "_vldExvldShatYezia");
            return false;
        }
    }
    else {
        vld.errormessage = " חובה להכניס שעת יציאה לפעילות ";
        ShowValidatorCalloutExtender(row.id + "_vldExvldShatYezia");
        return false;
    }
    document.getElementById(row.id + "_txtShatYezia").title = " תאריך שעת היציאה הוא " + shatYeziaDate.format("HH:mm dd/MM/yyyy");
    return true;
}

function CopyShatYetziaToPeiluyot(ShatYetzia, iRowIndex) {
     var numRows = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.length;
     var oRowAv = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(iRowIndex);
     var makatAv = oRowAv.childNodes.item(col_Makat).innerHTML;
     var shatVisut = new Date(ShatYetzia)
      if (numRows != (iRowIndex+1)) {
        var i = iRowIndex + 1;
        oRow = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i);
        var makatBen = oRow.childNodes.item(col_Makat).innerHTML;
        while (((makatBen == makatAv && Number(oRow.childNodes.item(col_Mispar_Knisa).innerHTML) > 0) ||
                (makatBen.substr(0, 3) == "700" || makatBen.substr(0, 3) == "761" || makatBen.substr(0, 3) == "784") && makatAv.substr(0, 1) != "7") && i < numRows) {
            if (oRow.childNodes.item(col_Makat).innerHTML.substr(0, 3) == "700" || oRow.childNodes.item(col_Makat).innerHTML.substr(0, 3) == "761"
                || oRow.childNodes.item(col_Makat).innerHTML.substr(0, 3) == "784") {
                shatVisut.setTime(shatVisut.getTime() + (1 * 60 * 1000)); 
                document.getElementById(oRow.id + "_txtShatYeziaDate").value = shatVisut.format("dd/MM/yyyy HH:mm");
                document.getElementById(oRow.id + "_txtShatYezia").value = shatVisut.format("HH:mm");
                document.getElementById(oRow.id + "_txtShatYezia").title = " תאריך שעת היציאה הוא " + shatVisut.format("HH:mm dd/MM/yyyy");
            }
            else {
                document.getElementById(oRow.id + "_txtShatYeziaDate").value = ShatYetzia.format("dd/MM/yyyy HH:mm");
                document.getElementById(oRow.id + "_txtShatYezia").value = ShatYetzia.format("HH:mm");
                document.getElementById(oRow.id + "_txtShatYezia").title = " תאריך שעת היציאה הוא " + ShatYetzia.format("HH:mm dd/MM/yyyy");
            }
            i += 1;
            if (i < numRows) {
                oRow = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i);
                makatBen = oRow.childNodes.item(col_Makat).innerHTML;
            }
        }
    }
//    for (var i = iRowIndex + 1; i < numRows; i++) {
//        oRow = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i);
//        if (oRow.childNodes.item(col_Makat).innerHTML == oRowAv.childNodes.item(col_Makat).innerHTML && Number(oRow.childNodes.item(col_Mispar_Knisa).innerHTML) > 0) {
//            document.getElementById(oRow.id + "_txtShatYeziaDate").value = ShatYetzia.format("dd/MM/yyyy HH:mm");
//            document.getElementById(oRow.id + "_txtShatYezia").value = ShatYetzia.format("HH:mm");
//        }
//    }
}
function btnShowMessage_Click() {
    bShowMassage = true;
    $find('ModalPopupEx').show;
}

function btnNochachi_click() {
    bShowMassage = false;
    $find('ModalPopupEx').hide();
    var obj;
    if (document.getElementById("DestTime").value == "gmar") {
        onchange_txtShatGmar(true, "1");
    }
    else {
        obj = document.getElementById(document.getElementById("DestTime").value.split(';')[1]);
        onchange_txtShatYezia(obj, true, "1", iRowIndexNochehi, true);
    }
}

function btnHaba_click() {
    bShowMassage = false;
    $find('ModalPopupEx').hide();

    var taarich;
    var shatYeziaDate;

    var obj;
    if (document.getElementById("DestTime").value.split(';')[0] == "gmar") {
        onchange_txtShatGmar(true, "2")
    }
    else {
        obj = document.getElementById(document.getElementById("DestTime").value.split(';')[1]);
        onchange_txtShatYezia(obj, true, "2", iRowIndexNochehi, true);
    }

}


/**************** דקות *******************/
function onchange_txtDakot(row) {
     var vld = document.getElementById(row.id + "_vldDakot");
    var dakotHagdara = row.childNodes.item(col_DakotHagdara).title;
    var dakotBafoal = document.getElementById(row.id + "_txtDakotBafoal").value;
    var Param98 = document.getElementById("Params").attributes("Param98").value;

    //כניסה (mispar_knisa>0) -  יש לאפשר להקליד ערך רק עבור כניסות מסוג לפי צורך (SugKnisa= 3), ערך בין 0  ולא גדול מהערך בפרמטר 98 (מכסימום זמן כניסה לישוב).
    if (Number(row.childNodes.item(col_Mispar_Knisa).innerText) > 0)
        dakotHagdara = Param98;
    if (trim(dakotBafoal) == "")
        dakotBafoal = 0;
    if (IsNumeric(dakotBafoal)) {
        if (trim(dakotHagdara) == "")
            dakotHagdara = 0;
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

/****************  מספר רכב/רישוי *******************/
function onchange_txtMisparRechev(row, iRowIndex) {
    var idMisRechev = row.id + "_txtMisRechev";
    var Mis_Rechev = document.getElementById(idMisRechev).value;
    var vld;
    
    iRowIndexNochehi = iRowIndex + 1;
    var oCell = document.getElementById(row.id + "_lblMisparRishuy");
    var ObjIsValidMis = document.getElementById(row.id + "_txtIsMisRechevValid");
    if (IsNumeric(Mis_Rechev)) {
        if (Mis_Rechev != "")
            wsGeneral.CheckOtoNo(Mis_Rechev, CheckMisRechevSucceded, null, row);
        else
            document.getElementById(row.id + "_txtIsMisRechevValid").value = "";
    }
    else {
        oCell.value = "";
        CopyMisRechevToPeiluyot(Mis_Rechev, "", iRowIndexNochehi);
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
    var ObjIsValidMis = document.getElementById(oRow.id + "_txtIsMisRechevValid");
    var vld;
    var Mis_Rechev = document.getElementById(oRow.id + "_txtMisRechev").value;
    if (result != 0) {
        oCell.value = result;
        document.getElementById(oRow.id + "_txtMisRechev").title = result;
               
        ObjIsValidMis.value = "1";
        if (document.getElementById("txtMisRechev").value == 0) {
            document.getElementById("txtMisRechev").value = Mis_Rechev;
            document.getElementById("txtMisRishuy").value = result;
        }
        else if (document.getElementById("txtMisRechev").value != Mis_Rechev) {
            document.getElementById("txtMisRechev").value = -1;
            document.getElementById("txtMisRishuy").value = "";
        }
         CopyMisRechevToPeiluyot(Mis_Rechev, result, iRowIndex);
    }
    else {
        oCell.value = "";
        document.getElementById(oRow.id + "_txtMisRechev").title = "";
        vld = document.getElementById(oRow.id + "_vldMisRechev");
        vld.errormessage = "!מספר רכב לא קיים";
        ShowValidatorCalloutExtender(oRow.id + "_vldExvldMisRechev");
        ObjIsValidMis.value = vld.errormessage + ";0";
    }
}

function CopyMisRechevToPeiluyot(Mis_Rechev, Mis_Rishuy, iRowIndex) {

    var numRows = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.length;
    var oRowAv = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(iRowIndex);
    if (numRows != (iRowIndex + 1)) {
        var i = iRowIndex + 1;
        oRow = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i);
        while (((oRow.childNodes.item(col_Makat).innerHTML == oRowAv.childNodes.item(col_Makat).innerHTML && Number(oRow.childNodes.item(col_Mispar_Knisa).innerHTML) > 0) ||
            oRow.childNodes.item(col_Makat).innerHTML.substr(0, 3) == "700" || oRow.childNodes.item(col_Makat).innerHTML.substr(0, 3) == "761"
                    || oRow.childNodes.item(col_Makat).innerHTML.substr(0, 3) == "784") && i < numRows) {
            if (oRow.childNodes.item(col_Makat).innerHTML.substr(0, 3) != "761" && oRow.childNodes.item(col_Makat).innerHTML.substr(0, 3) != "784") {
                document.getElementById(oRow.id + "_lblMisparRishuy").value = Mis_Rishuy;
                document.getElementById(oRow.id + "_txtMisRechev").value = Mis_Rechev;
                document.getElementById(oRow.id + "_txtMisRechev").title = Mis_Rishuy;
            }
            i += 1;
            if (i < numRows)
                oRow = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i);
        }
    }
//    for (var i = iRowIndex + 1; i < numRows; i++) {
//        oRow = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i);
//        if (oRow.childNodes.item(col_Makat).innerHTML == oRowAv.childNodes.item(col_Makat).innerHTML && Number(oRow.childNodes.item(col_Mispar_Knisa).innerHTML) > 0) {
//            document.getElementById(oRow.id + "_lblMisparRishuy").value = Mis_Rishuy;
//            document.getElementById(oRow.id + "_txtMisRechev").value = Mis_Rechev;
//            document.getElementById(oRow.id + "_txtMisRechev").title = Mis_Rishuy;
//        }
//    }
}

/****************  כיסוי תור *******************/
function onchange_txtKisuyTor(row) {
    var KisuyTor = document.getElementById(row.id + "_txtKisuiTor").value;
    var vld = document.getElementById(row.id + "_vldKisuiTor");
    var taarich = document.getElementById("txtHiddenTaarichCA").value.split('/');
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
                shatYezia = new Date(taarichTmp[2], taarichTmp[1] - 1, taarichTmp[0], shaa.split(':')[0], shaa.split(':')[1], '00');
                if (shatYezia.getDate() != shatKisuyTor.getDate()) {//שעת יציאה ביום הבא
                    if (IsShatGmarInNextDay(KisuyTor))
                        shatKisuyTor = new Date(shatKisuyTor.setDate(shatKisuyTor.getDate() + 1));
                }

                dakot = document.getElementById(row.id + "_txtShatYezia").attributes("kisuyTor").value;
                if (trim(dakot) == "")
                    dakot = "0";
                if (shatYezia < shatKisuyTor) {
                    vld.errormessage = " שעת כסוי תור לא יכולה להיות לאחר שעת יציאה";
                    ShowValidatorCalloutExtender(row.id + "_vldExvldKisuiTor");
                    return false;
                }
                else if (((shatYezia - shatKisuyTor) / (60 * 1000)) > Number(dakot)) {
                    vld.errormessage = " כיסוי התור שהוקלד הינו מעל המותר.מותר עד  " + dakot + " דקות פחות משעת היציאה";
                    ShowValidatorCalloutExtender(row.id + "_vldExvldKisuiTor");
                    return false;
                }
                document.getElementById(row.id + "_KisuyTorHidden").value = ((shatYezia - shatKisuyTor) / (60 * 1000));

            }
            //                    shaa = document.getElementById("txtShatHatchala").value;
            //                    if (shaa != "") {
            //                        shatHatchala = new Date(taarich[2], taarich[1] - 1, taarich[0], shaa.split(':')[0], shaa.split(':')[1], '00');
            //                        if (shatKisuyTor < shatHatchala) {
            //                            vld.errormessage = " שעת כסוי תור לא יכולה להיות לפני שעת התחלה לסידור";
            //                            ShowValidatorCalloutExtender(row.id + "_vldExvldKisuiTor");
            //                            return false;
            //                        }
            //                    }
        }
        else {
            vld.errormessage = " שעה לא תקינה ";
            ShowValidatorCalloutExtender(row.id + "_vldExvldKisuiTor");
            return false;
        }
    }
    else document.getElementById(row.id + "_KisuyTorHidden").value = "0";
    return true;
}

function checkFileds() {
    var numRows = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.length;  //document.getElementById("grdPeiluyot").childNodes.length;
    var row; var tempRow;
    var is_valid = true;
    var vldArr;
    var Rechev;
    var iCountLinesChk = 0;
    var rowChecked;
    if (bShowMassage)
        return false;
    else {
        for (var i = 1; i < numRows; i++) {
            row = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(i);
            if (row.childNodes.item(col_HosefPeilut).childNodes.item(0).disabled == false)
                rowChecked = row.childNodes.item(col_HosefPeilut).childNodes.item(0).checked;
            else rowChecked = row.childNodes.item(col_HosefPeilut).childNodes.item(0).childNodes.item(0).checked;
            if (rowChecked == true) {
             //   row.childNodes.item(col_HosefPeilut).childNodes.item(0).childNodes.item(0).checked == true) {
               
                iCountLinesChk += 1;
                is_valid = onchange_txtShatYezia(row, false, "", false);
                if (!is_valid)
                    break;

                for (var j = 1; j < numRows; j++) {
                    if (i != j && document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(j).childNodes.item(col_HosefPeilut).childNodes.item(0).checked != false) {

                        tempRow = document.getElementById("grdPeiluyot").childNodes.item(0).childNodes.item(j);

                        if (Number(row.childNodes.item(col_Mispar_Knisa).innerHTML) == 0 &&
                           document.getElementById(row.id + "_txtShatYeziaDate").value == document.getElementById(tempRow.id + "_txtShatYeziaDate").value && 
                            Number(tempRow.childNodes.item(col_Mispar_Knisa).innerHTML) == 0 &&
                            tempRow.childNodes.item(col_Makat).innerHTML.substr(0, 3) != "700" &&
                            tempRow.childNodes.item(col_Makat).innerHTML.substr(0, 3) != "761" &&
                            tempRow.childNodes.item(col_Makat).innerHTML.substr(0, 3) != "784") {
                            document.getElementById(row.id + "_vldShatYezia").errormessage = "קיימות שתי פעילויות עם שעת יציאה זהה, יש לעדכן אחת מהן ";
                            ShowValidatorCalloutExtender(row.id + "_vldExvldShatYezia");
                            is_valid = false;
                        }
                    }
                    if (!is_valid)
                        break;
                }
                if (!is_valid)
                    break;

                is_valid = onchange_txtKisuyTor(row)
                if (!is_valid)
                    break;

                Rechev = document.getElementById(row.id + "_txtMisRechev");
                if (trim(Rechev.value) == "" && (Rechev.attributes("Is_Required").value == "True" || Rechev.attributes("Is_Required").value == "true")) {
                     document.getElementById(row.id + "_vldMisRechev").errormessage = 'חובה להכניס מספר רכב';
                    ShowValidatorCalloutExtender(row.id + "_vldExvldMisRechev");
                    is_valid = false;
                }
                else {
                    vldArr = document.getElementById(row.id + "_txtIsMisRechevValid").value.split(';');
                    if (vldArr.length > 1) {
                        if (vldArr[1] == "0") {
                            document.getElementById(row.id + "_vldMisRechev").errormessage = vldArr[0];
                            ShowValidatorCalloutExtender(row.id + "_vldExvldMisRechev");
                            is_valid = false;
                        }
                    }
                }
                if (!is_valid)
                    break;

                is_valid = onchange_txtDakot(row);
                if (!is_valid)
                    break;
            }
        }
         if (iCountLinesChk == 0) {
            alert("!לא נבחרו פעילויות להוספה ");
            return false;
        }
        else {
            if (is_valid)
                return true;
            else return false;
        }
    }

}
function onSadeFocus(object) {
    document.getElementById(object.id).select();
}
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded(); 


/*******************/

 function setBorderBtns(){
       var aButton=document.getElementsByTagName('input');
        for(var i=0; i<aButton.length; i++) {
            if (aButton[i].type =="button" || aButton[i].type =="submit")
                    aButton[i].onfocus=function() {setFunctionsButton(this);}; 
            }
       }
       
     function setFunctionsButton(obj){
         var aButton=document.getElementsByTagName('input');
           for(var i=0; i< aButton.length; i++) {
                if (aButton[i].type =="button" || aButton[i].type =="submit"){ 
                    if (aButton[i].id == obj.id && obj.id  != "btnElement" && obj.id != "btnHosafatNesiaa")
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
/*******************/