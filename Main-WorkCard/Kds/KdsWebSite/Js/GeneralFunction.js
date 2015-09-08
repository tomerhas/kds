// Keep user from entering more than maxLength characters
function doKeypress(control){
    maxLength = control.attributes["maxLength"].value;
    value = control.value;
     if(maxLength && value.length > maxLength-1){
          event.returnValue = false;
          maxLength = parseInt(maxLength);
     }
}
// Cancel default behavior
function doBeforePaste(control){
    maxLength = control.attributes["maxLength"].value;
     if(maxLength)
     {
          event.returnValue = false;
     }
}
// Cancel default behavior and create a new paste routine
function doPaste(control){
    maxLength = control.attributes["maxLength"].value;
    value = control.value;
     if(maxLength){
          event.returnValue = false;
          maxLength = parseInt(maxLength);
          var oTR = control.document.selection.createRange();
          var iInsertLength = maxLength - value.length + oTR.text.length;
          var sData = window.clipboardData.getData("Text").substr(0,iInsertLength);
          oTR.text = sData;
     }
  }
function IsNumber(sText) {
    var ValidChars = "-+0123456789";
    var Char = sText.charAt(0);
    if (sText.length == 1) {
        return IsNumeric(sText);
    }
    return  (ValidChars.indexOf(Char) != -1) ? IsNumeric(sText.substr(1,sText.length)) : false ;
  }
function IsNumeric(sText) {
    var ValidChars = "0123456789";
    var IsNumber=true;
    var Char;
    if (sText.length == 0) {
        return false; 
    }  	 
    for (i = 0; i < sText.length && IsNumber == true; i++){		     
	    Char = sText.charAt(i); 		    
	    if (ValidChars.indexOf(Char) == -1){ 			    
		    IsNumber = false;
		    }
	    }
    return IsNumber;			   
}
    
function IsDecimal(sText){    
    var ValidChars = "0123456789-.";
    var IsNumber=true;
    var Char;    	 
    for (i = 0; i < sText.length && IsNumber == true; i++){ 		   
	    Char = sText.charAt(i); 
	    if (ValidChars.indexOf(Char) == -1){			    
		    IsNumber = false;
		    }
	    }
    return IsNumber;			   
}

function GetOvedMisparIshi(oID)
{  
  //var sOvedname = document.getElementById("ctl00_KdsContent_txtName").value;
  var sOvedname = oID.value;
  if (sOvedname!=""){
    wsGeneral.GetOvedMisparIshi(sOvedname, GetOvedMisparSucc);   
   }
}
function GetOvedName(oID)
{
 // var iKodOved = document.getElementById("ctl00_KdsContent_txtId").value;
  var iKodOved =oID.value;
  if (iKodOved!="")
  {
      if (IsNumeric(trim(iKodOved)))
      {
          wsGeneral.GetOvedName(iKodOved,GetOvedNameSucceeded);
      }
      else {
         alert("מספר אישי לא חוקי");
        oID.select();
        //document.getElementById("ctl00_KdsContent_txtId").select();
      }  
  }    
}
function GetHebrewDay(iDay) {
    var arrDays = new Array(7); // regular array (add an optional integer
    arrDays[0] = "א";       // argument to control array's size)
    arrDays[1] = "ב";
    arrDays[2] = "ג";
    arrDays[3] = "ד";
    arrDays[4] = "ה";
    arrDays[5] = "ו";
    arrDays[6] = "שבת";

    return arrDays[iDay];
}
function getSysDate()
{
    var currentTime = new Date();
    var month = currentTime.getMonth() + 1;
    var day = currentTime.getDate();
    var year = currentTime.getFullYear();
    
    return (day + "/" + month + "/" + year);
}   

function IsIE8(){
   return (/MSIE (7|8)/.test(navigator.userAgent) && navigator.platform == "Win32");
}

function EnlargeFieldSetsForIE8(specificFieldSetsIndexes, qs, height) {
    var query = window.location.href.toString();
    query = query.indexOf("=") > -1 ? query.substr(query.indexOf("=") + 1, query.length - query.indexOf("=") + 1) : "";
    
    if(IsIE8() && (!qs || (query.length > 0 && qs.indexOf(query) > -1))) {
        $(".FilterFieldSet").each(function(i)
        {
            if(!specificFieldSetsIndexes || specificFieldSetsIndexes.indexOf(i) > -1) {
                $(this).css("height", height);
            }
        });   
    }
}

function GetQueryStringValue(KeyName) {
    hu = window.location.search.substring(1);
    gy = hu.split("&");
    for (i = 0; i < gy.length; i++) {
        ft = gy[i].split("=");
        if (ft[0] == KeyName) {
            return ft[1];
        }
    }
}

function IsValidTime(time) {
    var timeParts = time.split(":");
    if ((Number(timeParts[0]) > 23 || Number(timeParts[0]) < 0))
        return false;
    if ((Number(timeParts[1]) > 59 || Number(timeParts[1]) < 0))
        return false; 
    if (timeParts.length == 3)
        if ((Number(timeParts[2]) > 59 || Number(timeParts[2]) < 0))
             return false; 
    return true;
}

function trim(str) {
    return str.replace(/^[\s]+/, '').replace(/[\s]+$/, '').replace(/[\s]{2,}/, ' ');
}

function IsShatGmarInNextDay(shaa){
    var Time=shaa.split(':');
    var hours = Time[0];
    var minuts = Time[1];
    if ((Number(hours) > 0 && Number(hours) < 8) ||
         (Number(hours) == 0 && Number(minuts) > 0) )
        return true;
    else
        return false;
}
function IsShatHatchalaInNextDay(shaa, isSidurGrira) {
    var sParam;
    if (isSidurGrira)
        sParam= document.getElementById("SD_hidParam276").value;
    else
      sParam= document.getElementById("SD_hidParam244").value;

    var Time = shaa.split(':');
    var hours = Time[0];
    var minuts = Time[1];
    if ((Number(hours) > 0 && Number(hours) <= Number(sParam.substr(0,2))) ||
         (Number(hours) == 0 && Number(minuts) > 0))
        return true;
    else
        return false;
}
function GetDateDDMMYYYY(dDate){   
    var dd = dDate.getDate();
    var mm = dDate.getMonth()+1;//January is 0!
    var yyyy = dDate.getFullYear();
    if(dd<10){dd='0'+dd}
    if(mm<10){mm='0'+mm}

    return dd+'/'+mm+'/'+yyyy;
}


   function PrintDoc(sPrinterName,sFileName) {
       var xpdf = new ActiveXObject("Xpdf.XpdfPrint");
       xpdf.loadFile(sFileName);
        xpdf.rotate = 0;
        xpdf.forceColor = true;
        xpdf.forceGDI = true;
        xpdf.forceImage = true;
        xpdf.forceMono = true;
        xpdf.printer = sPrinterName;
        xpdf.expandSmallPages = true;
        
               
        xpdf.printPDF4();
   }

   function SelectedItemOfRadioButtonList(ObjId) {
        var f = document.forms[0];
        var e = f.elements[ObjId];
        var src = document.getElementById(ObjId);
        for (var count = 0; count < src.childNodes[0].childNodes.length; count++) {
            if (src.childNodes[0].childNodes[count].childNodes[0].childNodes[0].checked == true) {
                return src.childNodes[0].childNodes[count].childNodes[0].childNodes[0].value;
           }
       }
       return -1;
   }
   function pausecomp(millis) {
       var date = new Date();
       var curDate = null;

       do { curDate = new Date(); }
       while (curDate - date < millis);
   }
   function PadDigits(n, totalDigits) {
       n = n.toString();
       var pd = '';
       if (totalDigits > n.length) {
           for (i = 0; i < (totalDigits - n.length); i++) {
               pd += '0';
           }
       }
       return pd + n.toString();
   }
   function GetTimeInMinuts(dStartHour, dEndHour) {
       var diff = new Date();
       var timediff = new Date();
       //חישוב הפרש דקות בין שעת גמר  לשעת התחלת 
       diff.setTime(Math.abs(dStartHour.getTime() - dEndHour.getTime()));
       timediff = diff.getTime();
       return (Math.floor(timediff / (1000 * 60 )));
   }

   function GetKeyPressPosition(ctrl) {
       // return ctrl.value.length;       
       var Sel = document.selection.createRange();
       Sel.moveStart('character', -ctrl.value.length);
       return Sel.text.length;
   }

   function FreezeHeader(DivGridView) {
       var ListOfTHeader = DivGridView.getElementsByTagName("th");
       for (var i = 0; i <= ListOfTHeader.length - 1; i++) {
           ListOfTHeader[i].style.top = DivGridView.scrollTop-2;
           ListOfTHeader[i].style.left = ListOfTHeader[i].parentNode.parentNode.parentNode.parentNode.scrollLeft + 1;
           ListOfTHeader[i].style.position = "relative";
       }
   }

//   function alert(sMsg) {
//       $get("btnAlert").click();
//     //  $find("cbeAlert").ConfirmText = sMsg;
//      
//   }
   if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
   
   
   /*******************************************************************************************/
   function ChangeKeyCode(event) {

       if (event.keyCode == 13) {
           event.keyCode = 9;
           return event.keyCode
       }
      // if (event.keyCode == 13) { event.keyCode = 9;  }
   }
   


   function SimunExtendeOpen(sorce, evarg) {
       flag = true;
   }
   function SimunExtendeIdClose(sorce, evarg) {
       flag = false;
       GetOvedNameById();
   }

   function SimunExtendeNameClose(sorce, evarg) {
       flag = false;
       GetOvedIdByName();
   }
   function GetOvedNameById() {
    //   debugger;
       if (flag == false) {
           var iKodOved = document.getElementById(oTxtId).value;
           if (iKodOved != "") {
               if (IsNumeric(trim(iKodOved))) {
                   if (userId > 0)
                       wsGeneral.GetOvedToUser(iKodOved, userId, GetOvedNameByIdSucceeded);
                   else
                       wsGeneral.GetOvedName(iKodOved, GetOvedNameByIdSucceeded);
               }
               else {
                   //alert("1212");
                   alert("מספר אישי לא חוקי");
                   document.getElementById(oTxtId).value = "";
                   document.getElementById(oTxtId).focus();
               }
           }
       }
   }

   function GetOvedIdByName() {
       if (flag == false) {
           var sOvedname = document.getElementById(oTxtName).value;
           if (sOvedname != "") {
               if (sOvedname.indexOf('(') > -1) {
                   document.getElementById(oTxtId).value = trim(sOvedname.split('(')[1].replace(")", ""));
                   continue_click();
               }
               else {
                   if (userId > 0)
                       wsGeneral.GetOvedToUser(sOvedname, userId, GetOvedIdByNameSucceeded); 
                   else
                        wsGeneral.GetOvedMisparIshi(sOvedname, GetOvedIdByNameSucceeded);
               }

           }
//           else {
//               alert("שם לא חוקי");
//               document.getElementById(oTxtName).select();
//               document.getElementById(oTxtId).value = "";
//           }
       }
   }

   function unloadCard() {
       alert(' זמן ההתחברות הסתיים, יש להכנס מחדש לכרטיס העבודה');
       window.close();
   }

   function GetOvedNameByIdSucceeded(result) {
      // alert(result);
       if (result == '') {
           alert('מספר אישי לא קיים/אינך מורשה לצפות בעובד זה');
           document.getElementById(oTxtId).focus();
           document.getElementById(oTxtName).value = "";
           document.getElementById(oTxtId).value = "";
       }
       else {
           document.getElementById(oTxtName).value = result;
           continue_click();
       }
   }

   function GetOvedIdByNameSucceeded(result) {
      // obtnHdn = document.getElementById("ctl00_KdsContent_btnHidden");
       if (result == '') {
           alert('מספר אישי לא קיים/אינך מורשה לצפות בעובד זה');
           document.getElementById(oTxtName).focus();
           document.getElementById(oTxtId).value = "";
           document.getElementById(oTxtName).value = "";
       }
       else {
           document.getElementById(oTxtId).value = result;
           continue_click();
       }
   }