
var bShowMeafyen;
bShowMeafyen = false;

function getTeurMeafyen(sender, eventArgs) {
    var sElement;
    if (document.getElementById("ctl00_KdsContent_txtElement") == null) {
        sElement = document.getElementById("txtElement").value;
    }
    else { sElement = document.getElementById("ctl00_KdsContent_txtElement").value; }

    var iKodMeafyen = document.getElementById(sElement + "_txtCodeMeafyen").value;
    if (iKodMeafyen != "") {
        if (IsNumeric(iKodMeafyen)) {
            wsGeneral.GetTeurMeafyen(iKodMeafyen, GetTeurMeafyenSucceeded);
        }
        else {
            alert("קוד מאפיין לא חוקי");
            oID.select();
        }
    }

}

function GetTeurMeafyenSucceeded(result) {
    var sElement;
    if (document.getElementById("ctl00_KdsContent_txtElement") == null) {
        sElement = document.getElementById("txtElement").value;
    }
    else { sElement = document.getElementById("ctl00_KdsContent_txtElement").value; }

    if (result == '') {
        alert('קוד מאפיין לא קיים');
        document.getElementById(sElement + "_txtCodeMeafyen").select();
    }
    else {
        document.getElementById(sElement + "_txtTeurMeafyen").value = result;
    }
}

function GetCodeMeafyenSucceeded(result) {
    var sElement;
    if (document.getElementById("ctl00_KdsContent_txtElement") == null) {
        sElement = document.getElementById("txtElement").value;
    }
    else { sElement = document.getElementById("ctl00_KdsContent_txtElement").value; }

    if (result == '') {
        alert('תאור מאפיין לא קיים');
        document.getElementById(sElement + "_txtTeurMeafyen").select();
    }
    else {
        document.getElementById(sElement + "_txtCodeMeafyen").value = result;
    }
}

function getCodeMeafyen(sender, eventArgs) {
    var sElement;
    if (document.getElementById("ctl00_KdsContent_txtElement") == null) {
        sElement = document.getElementById("txtElement").value;
    }
    else { sElement = document.getElementById("ctl00_KdsContent_txtElement").value; }

    var sTeurMeafyen = document.getElementById(sElement + "_txtTeurMeafyen").value;
    if (sTeurMeafyen != "") {
        wsGeneral.GetCodeMeafyen(escape(sTeurMeafyen), GetCodeMeafyenSucceeded);
    }
}

function ShowMeafyen(Meafyen) {
    var sElement;
    if (document.getElementById("ctl00_KdsContent_txtElement") == null) {
        sElement = document.getElementById("txtElement").value;
    }
    else { sElement = document.getElementById("ctl00_KdsContent_txtElement").value; }

    var sMeafyen;
    sMeafyen = Meafyen.innerHTML;
    if (sMeafyen.indexOf("+") == -1) {
        Meafyen.innerHTML = sMeafyen.replace("-", "+");
        Meafyen.nextSibling.nextSibling.nextSibling.style.display = "none";
        bShowMeafyen = false;
    }
    else {
        Meafyen.innerHTML = sMeafyen.replace("+", "-");
        Meafyen.nextSibling.nextSibling.nextSibling.style.display = "Block";
        bShowMeafyen = true;
    }
}


function CheckChanged(iRowId) {
    var sElement;
    if (document.getElementById("ctl00_KdsContent_txtElement") == null) {
        sElement = document.getElementById("txtElement").value;
    }
    else { sElement = document.getElementById("ctl00_KdsContent_txtElement").value; }

  if (bShowMeafyen == false) {
        document.getElementById(sElement + "_txtRowSelected").value = iRowId;
        document.getElementById(sElement + "_btnBindHistory").click();

    }
}

function getTeurNatun(sender, eventArgs) {
    var sElement;
    if (document.getElementById("ctl00_KdsContent_txtElement") == null) {
        sElement = document.getElementById("txtElement").value;
    }
    else { sElement = document.getElementById("ctl00_KdsContent_txtElement").value; }

    var iKodNatun = document.getElementById(sElement + "_txtCodeNatun").value;
    if (iKodNatun != "") {
        if (IsNumeric(iKodNatun)) {
            wsGeneral.GetTeurNatun(iKodNatun, GetTeurNatunSucceeded);
        }
        else {
            alert("קוד נתון לא חוקי");
            oID.select();
        }
    }

}

function GetTeurNatunSucceeded(result) {
    var sElement;
    if (document.getElementById("ctl00_KdsContent_txtElement") == null) {
        sElement = document.getElementById("txtElement").value;
    }
    else { sElement = document.getElementById("ctl00_KdsContent_txtElement").value; }
    if (result == '') {
        alert('קוד נתון לא קיים');
        document.getElementById(sElement + "_txtCodeNatun").select();
    }
    else {
        document.getElementById(sElement + "_txtTeurNatun").value = result;
    }
}

function GetCodeNatunSucceeded(result) {
    var sElement;
    if (document.getElementById("ctl00_KdsContent_txtElement") == null) {
        sElement = document.getElementById("txtElement").value;
    }
    else { sElement = document.getElementById("ctl00_KdsContent_txtElement").value; }
    if (result == '') {
        alert('תאור נתון לא קיים');
        document.getElementById(sElement + "_txtTeurNatun").select();
    }
    else {
        document.getElementById(sElement + "_txtCodeNatun").value = result;
    }
}

function getCodeNatun(sender, eventArgs) {
    var sElement;
    if (document.getElementById("ctl00_KdsContent_txtElement") == null) {
        sElement = document.getElementById("txtElement").value;
    }
    else { sElement = document.getElementById("ctl00_KdsContent_txtElement").value; }

    var sTeurNatun = document.getElementById(sElement + "_txtTeurNatun").value;
    if (sTeurNatun != "") {
        wsGeneral.GetCodeNatun(escape(sTeurNatun), GetCodeNatunSucceeded);
    }
}

 function CheckChangedDetails(iRowId) {
   var sElement;
    if (document.getElementById("ctl00_KdsContent_txtElement") == null) {
        sElement = document.getElementById("txtElement").value;
    }
    else { sElement = document.getElementById("ctl00_KdsContent_txtElement").value; }

    document.getElementById(sElement + "_txtRowSelected").value = iRowId;
     document.getElementById(sElement + "_btnBindHistory").click();

 }

 function ShowRdoCheck() {
     document.getElementById("tdchkNoDefault").style.display = 'block';
     document.getElementById("tdchkWithDefault").style.display = 'block';
 }

 function HiddenRdoCheck() {
     document.getElementById("tdchkNoDefault").style.display = 'none';
     document.getElementById("tdchkWithDefault").style.display = 'none';
 }

 function ActiveTabChanged(sender, e) {
     document.getElementById("btnChangeTab").click();
 }
 
 function ItemSelected(sender, e) {
     var sElement;
     if (document.getElementById("ctl00_KdsContent_txtElement") == null) {
         sElement = document.getElementById("txtElement").value;
     }
     else { sElement = document.getElementById("ctl00_KdsContent_txtElement").value; }

     document.getElementById(sElement + "_grdHistoriatMeafyen").focus();
 }

 function ItemSelectedDetails(sender, e) {
     var sElement;
     if (document.getElementById("ctl00_KdsContent_txtElement") == null) {
         sElement = document.getElementById("txtElement").value;
     }
     else { sElement = document.getElementById("ctl00_KdsContent_txtElement").value; }

     document.getElementById(sElement + "_grdHistoriatNatun").focus();
 }
 if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded(); 
