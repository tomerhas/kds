﻿/**
* The sidurim js class is used in the workcard process
*
* @class Sidurim
*/

var oGridRowId;
var MKT_VISUT = 4;
var MKT_VISA = 6;
var MKT_SHERUT = 1;
var MKT_EMPTY = 2;
var MKT_NAMAK = 3;
var MKT_ELEMENT = 5;
var SIDUR_GRIRA = 99220;
/**
recieves an index of a row. Itenraction with UI (txtId, clndate). Does logic according to the makat number . Complexity - Medium
 This logic can be copied to the server using ajax

 @method chkMkt
*/
function chkMkt(oRow) {
       var iMisparIshi = $get("txtId").value;
       var dCardDate = $get("clnDate").value;    
       oRId = String(oRow.id).substr(0,oRow.id.length-6);
       var oMkt = $get(oRId).cells[_COL_MAKAT].childNodes[0];
       var lNMkt = oMkt.value; 
       var lOMkt = oMkt.getAttribute("OrgMakat");       
       var sArrPrm= new Array(7); 
       var iMktType;
       if (lNMkt < 100000) {
               oMkt.value = lOMkt;
               alert('מספר מק"ט לא תקין, יש להקליד מספר בין 6-8 ספרות');
               oMkt.select();
           }
           else {
               iMktType = GetMakatType(lNMkt);
               if ((iMktType == MKT_VISA) || ((iMktType == MKT_ELEMENT) && (isElementMechona(lNMkt))) || ((iMktType == MKT_VISUT))) {
                   oMkt.value = lOMkt;
                   oMkt.select();
                   alert('לא ניתן להקליד מק"ט ויזה או ויסות או הכנת מכונה');
               }
               else {
                   var iSidur;
                   var iInx = Number(String(oRow.id).substr(String('SD_').length, 3));
                   var oDate = $get('SD_lblDate'.concat(iInx)).firstChild.nodeValue;
                   if ($get('SD_lblSidur'.concat(iInx)).firstChild != null)
                       iSidur = $get('SD_lblSidur'.concat(iInx)).firstChild.nodeValue;
                   else
                       iSidur = $get('SD_lblSidur'.concat(iInx)).value;
                       var iSidurVisa = $get('SD_lblSidur'.concat(iInx)).getAttribute("SidurVisa");                 
                       if ((lNMkt != '') && (Number(lNMkt != 0))){
                           var oShatYetiza = $get(oRId).cells[_COL_SHAT_YETIZA].childNodes[0].value;
                           var oDayToAdd = $get(oRId).cells[_COL_DAY_TO_ADD].childNodes[0].value;
                           sArrPrm[0] = oRId;
                           sArrPrm[1] = iSidur;
                           sArrPrm[2] = iSidurVisa;
                           sArrPrm[3] = $get('SD_lblSidur'.concat(iInx)).getAttribute("Sidur93");
                           sArrPrm[4] = $get('SD_lblSidur'.concat(iInx)).getAttribute("Sidur94");
                           sArrPrm[5] = $get('SD_lblSidur'.concat(iInx)).getAttribute("Sidur95");
                           sArrPrm[6] = lOMkt;
                           sArrPrm[7] = iInx;
                           wsGeneral.CheckMakat(iMisparIshi, dCardDate, iInx, $get(oRId).rowIndex - 1, lNMkt, lOMkt, oDate, oShatYetiza, oDayToAdd, callBackMkt, null, sArrPrm);
                       }
                  }
            }         
}
/**
recieves an xml parses it and returns the makat number. 
Additional data is taken from the users input placed inside ui elements. Complexity - High
Need to replace with controller. Need to understand logic
* @method callBackMkt
*/
    function callBackMkt(result,sArrPrm)
{
   
        if (result == "-1") {
            unloadCard();
        }
        else {
            var xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
            xmlDoc.async = "false";
            xmlDoc.loadXML(result);
            root = xmlDoc.documentElement;
            var sMeafyen6 = '0';
            var sMeafyen7 = '999';
            var bMeafyen6, bMeafyen7;
            var bExist = false; var bMustCarNum = true;
            var oRId = sArrPrm[0];
            var iSidur = sArrPrm[1];
            var iSidurVisa = sArrPrm[2];
            var lOMkt = sArrPrm[6];
            var lNewMkt = $get(oRId).cells[_COL_MAKAT].childNodes[0].value;
            var iElementVal = lOMkt.substr(1, 2);
            var iSidurIndex = sArrPrm[7];
            //אם לסידור יש מאפיין 93,94,95 אחד מהן לפחות ומשנים אלמנט שהספרות 2-3 שלו שוות לערך המאפיין
            //לא ניתן לשנות את מספר האלמנט
            if (Number(lOMkt.substr(0, 1)) == 7) {
                if (((sArrPrm[3] == iElementVal) && (Number(sArrPrm[3]) != 0)) || ((sArrPrm[4] == iElementVal) && (Number(sArrPrm[4]) != 0)) || ((sArrPrm[5] == iElementVal) && (Number(sArrPrm[5]) != 0))) {
                    if (((lOMkt.substr(0, 3) == lNewMkt.substr(0, 3)))) { }
                    else {
                        $get(oRId).cells[_COL_MAKAT].childNodes[0].value = lOMkt;
                        bExist = true;
                        alert((' אלמנט '.concat(lOMkt)).concat(' חובה בסידור זה'));
                    }
                }
            }
            var oReka;
            if (root != null) {
                if (root.childNodes.length > 0) {
                    var _FirstChild = root.firstChild;
                    var iPeilutIndex;
                    iPos = String($get(oRId).id).indexOf("ctl");
                    iPeilutIndex = String($get(oRId).id).substr(iPos + 3);
                    if (GetMakatType(lNewMkt) != MKT_ELEMENT) {
                        $get(oRId).cells[_COL_ADD_NESIA_REKA].innerHTML = "<INPUT style='BORDER-RIGHT-WIDTH: 0px; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px' id=" + $get(oRId).id + "_AddReka" + $get(oRId).id + " name=SD$" + PadDigits(iSidurIndex.toString(), 3) + "$ctl" + String(iPeilutIndex) + "$AddRekaSD_" + PadDigits(iSidurIndex.toString(), 3) + "_ctl" + String(iPeilutIndex) + " src='../../images/down.png' type=image  SdrInd=" + iSidurIndex + " PeilutInd=" + String(iPeilutIndex) + " NesiaReka='1'>"
                    }
                    else {
                        if ($get(oRId).cells[_COL_ADD_NESIA_REKA].childNodes.length > 0) {
                            oReka = $get(oRId).cells[_COL_ADD_NESIA_REKA].childNodes[0].setAttribute;
                            if ((oReka != null) && (oReka != undefined))
                                $get(oRId).cells[_COL_ADD_NESIA_REKA].childNodes[0].setAttribute("NesiaReka", "0");
                        }
                        if ($get(oRId).cells[_COL_ADD_NESIA_REKA_UP].childNodes.length > 0) {
                            oReka = $get(oRId).cells[_COL_ADD_NESIA_REKA_UP].childNodes[0].setAttribute;
                            if ((oReka != null) && (oReka != undefined))
                                $get(oRId).cells[_COL_ADD_NESIA_REKA_UP].childNodes[0].setAttribute("NesiaRekaUp", "0");
                        }
                        $get(oRId).cells[_COL_ADD_NESIA_REKA].innerHTML = "";
                        $get(oRId).cells[_COL_ADD_NESIA_REKA_UP].innerHTML = "";
                    }
                    while ((_FirstChild != null) && (!bExist)) {
                        switch (_FirstChild.nodeName) {
                            case "KISUY_TOR":
                                $get(oRId).cells[_COL_KISUY_TOR].childNodes[0].value = _FirstChild.text;
                                break;
                            case "KISUY_TOR_ENABLED":
                                $get(oRId).cells[_COL_KISUY_TOR].childNodes[0].disabled = (_FirstChild.text == "0");
                                break;
                            case "KISUY_TOR_MAP":
                                $get(oRId).cells[_COL_KISUY_TOR_MAP].childNodes[0].nodeValue = _FirstChild.text;
                                break;
                            case "DESC":
                                if ($get(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].nodeValue == null)
                                    $get(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].firstChild.nodeValue = _FirstChild.text;
                                else
                                    $get(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].nodeValue = _FirstChild.text;
                                break;
                            case "SHILUT":
                                $get(oRId).cells[_COL_LINE].childNodes[0].nodeValue = _FirstChild.text;
                                break;
                            case "SHILUT_NAME":
                                $get(oRId).cells[_COL_LINE_TYPE].childNodes[0].nodeValue = _FirstChild.text;
                                break;
                            case "MAZAN_TASHLUM":
                                $get(oRId).cells[_COL_MAZAN_TASHLUM].childNodes[0].nodeValue = _FirstChild.text;
                                break;
                            case "DAKOT_DEF":
                                $get(oRId).cells[_COL_DEF_MINUTES].childNodes[0].nodeValue = _FirstChild.text;
                                break;
                            case "DAKOT_DEF_TITLE":
                                $get(oRId).cells[_COL_DEF_MINUTES].title = _FirstChild.text;
                                break;
                            case "DAKOT_BAFOAL":
                                $get(oRId).cells[_COL_ACTUAL_MINUTES].childNodes[0].value = _FirstChild.text;
                                $get(oRId).cells[_COL_ACTUAL_MINUTES].childNodes[1].errormessage = "יש להקליד ערך בין 0 ל -".concat($get(oRId).cells[_COL_MAZAN_TASHLUM].childNodes[0].nodeValue) + " דקות ";
                                break;
                            case "DAKOT_BAFOAL_ENABLED":
                                $get(oRId).cells[_COL_ACTUAL_MINUTES].childNodes[0].disabled = (_FirstChild.text == "0");
                                break;
                            case "OTO_NO":
                                $get(oRId).cells[_COL_CAR_NUMBER].childNodes[0].value = "";
                                break;
                            case "LICENSE_NUMBER":
                                $get(oRId).cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].value = "";
                                break;
                            case "OTO_NO_ENABLED":
                                $get(oRId).cells[_COL_CAR_NUMBER].childNodes[0].disabled = (_FirstChild.text == "0");
                                bMustCarNum = (_FirstChild.text == "1");
                                if (_FirstChild.text == "1")
                                    $get(oRId).cells[_COL_CAR_NUMBER].childNodes[0].setAttribute("MustOtoNum", "1");
                                break;
                            case "LICENSE_NUMBER_ENABLED":
                                $get(oRId).cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].disabled = (_FirstChild.text == "0");
                                bMustCarNum = (_FirstChild.text == "1");
                                if (_FirstChild.text == "1")
                                    $get(oRId).cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].setAttribute("MustOtoNum", "1");
                                break;
                            case "OTO_NO_TITEL":
                                $get(oRId).cells[_COL_CAR_NUMBER].childNodes[0].title = _FirstChild.text;
                                break;
                            case "LICENSE_NUMBER_TITEL":
                                $get(oRId).cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].title = _FirstChild.text;
                                break;
                            case "SHAT_YETIZA":
                                $get(oRId).cells[_COL_SHAT_YETIZA].childNodes[0].value = "";
                                break;
                            case "SHAT_YETIZA_ENABLED":
                                $get(oRId).cells[_COL_SHAT_YETIZA].childNodes[0].disabled = (_FirstChild.text == "0");
                                break;
                            case "MAKAT_NOT_EXIST":
                                $get(oRId).cells[_COL_MAKAT].childNodes[0].value = lOMkt;
                                $get(oRId).cells[_COL_MAKAT].childNodes[0].select();
                                bExist = true;
                                alert("אלמנט לא קיים");
                                break;
                            case "MEAFYEN6ERR":
                                sMeafyen6 = _FirstChild.text;
                                bMeafyen6 = true;
                                break;
                            case "MEAFYEN7ERR":
                                sMeafyen7 = _FirstChild.text;
                                bMeafyen7 = true;
                                break;
                            case "REKA_UP":
                                $get(oRId).cells[_COL_ADD_NESIA_REKA_UP].innerHTML = "<INPUT style='BORDER-RIGHT-WIDTH: 0px; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px' id='" + $get(oRId).id + "_AddRekaUp" + $get(oRId).id + "' name='SD$" + PadDigits(iSidurIndex.toString(), 3) + "$ctl" + String(iPeilutIndex) + "$AddRekaUpSD_" + PadDigits(iSidurIndex.toString(), 3) + "_ctl" + String(iPeilutIndex) + "' src='../../images/up.png' type='image'  SdrInd='" + iSidurIndex + "' PeilutInd='" + String(iPeilutIndex) + "' NesiaRekaUp='1'>"
                                break;
                            case "HYPER_LINK":
                                //debugger;
                                if (_FirstChild.text == "1") {
                                    if ($get(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].nodeValue == null)
                                        $get(oRId).cells[_COL_LINE_DESCRIPTION].innerHTML = "<a onclick='AddHosafatKnisot(" + iSidurIndex + "," + $get(oRId).id + ");' style='text-decoration:underline;cursor:pointer;'>" + $get(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].firstChild.nodeValue + "</a>"; //"<".concat(_FirstChild.text) + "</a>";
                                    else
                                        $get(oRId).cells[_COL_LINE_DESCRIPTION].innerHTML = "<a onclick='AddHosafatKnisot(" + iSidurIndex + "," + $get(oRId).id + ");' style='text-decoration:underline;cursor:pointer;'>" + $get(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].nodeValue + "</a>"; // "<".concat(_FirstChild.text) + "</a>";                            
                                }
                                else
                                    if ($get(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].outerHTML != null) {
                                        if (($get(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].outerHTML.toUpperCase().indexOf('<A')) > -1)
                                            if ($get(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].nodeValue == null)
                                                $get(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].outerHTML = $get(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].firstChild.nodeValue;
                                            else
                                                $get(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].outerHTML = $get(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].nodeValue;
                                    }
                                break;
                        }
                        _FirstChild = _FirstChild.nextSibling;
                    }
                    if ((!bExist)) {
                        $get(oRId).cells[_COL_NETZER].childNodes[0].nodeValue = 'לא';
                        $get(oRId).cells[_COL_ACTUAL_MINUTES].childNodes[0].nodeValue = '';
                        if ((bMeafyen6) || (bMeafyen7)) {
                            alert('יש להקליד ערך בתחום: ' + sMeafyen6 + " " + " עד " + sMeafyen7);
                        } else
                        //נשתול מספר רכב
                            if (bMustCarNum) {
                                SetCarNumber(iSidurIndex, oRId, iPeilutIndex);
                                SetLicenseNumber(iSidurIndex, oRId, iPeilutIndex);
                            }
                        SetBtnChanges();
                    }
                }
                else {
                    var sBehaviorId = 'vMNBeh'.concat(oRId);
                    $find(sBehaviorId)._ensureCallout();
                    $find(sBehaviorId).show(true);
                }
            } else {
                $get(oRId).cells[_COL_MAKAT].childNodes[0].value = lOMkt;
                alert('מספר מק"ט לא תקין');
            }
           $get("hidErrChg").value = "";
        }
    }
/**
works with XML. updates UI. Interacts with wsGeneral service. Complexity - Medium
@method SetNewSidurCtls
*/
    function SetNewSidurCtls(iSidurNum, result){
        //אחרי שמזינים מספר סידור חדש, נאפשר את שאר השדות לפי מאפייני הסידור
        if (result == '-1')
            unloadCard();
        else {
            var xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
            xmlDoc.async = "false";
            xmlDoc.loadXML(result);
            root = xmlDoc.documentElement;
            if (root != null) {
                if (root.childNodes.length > 0) {
                    var _FirstChild = root.firstChild;
                    while (_FirstChild != null) {
                        switch (_FirstChild.nodeName) {
                            case "SHAT_HATCHALA":
                                $get("SD_txtSH" + iSidurNum).disabled = false;
                                break;
                            case "SHAT_GMAR":
                                $get("SD_txtSG" + iSidurNum).disabled = false;
                                break;
                            case "SHAT_HATCHALA_LETASHLUM":
                                $get("SD_txtSHL" + iSidurNum).disabled = (_FirstChild.text == "0");
                                $get("SD_txtSHL" + iSidurNum).value = "";
                                break;
                            case "SHAT_GMAR_LETASHLUM":
                                $get("SD_txtSGL" + iSidurNum).disabled = (_FirstChild.text == "0");
                                $get("SD_txtSGL" + iSidurNum).value = "";
                                break;
                            case "DIVUCH_KNISA":
                                $get("SD_ddlResonIn" + iSidurNum).selected = "value";
                                $get("SD_ddlResonIn" + iSidurNum).value = "-1";
                                $get("SD_ddlResonIn" + iSidurNum).disabled = (_FirstChild.text == "0");
                                break;
                            case "DIVUCH_YETIZA":
                                $get("SD_ddlResonOut" + iSidurNum).selected = "value";
                                $get("SD_ddlResonOut" + iSidurNum).value = -1;
                                $get("SD_ddlResonOut" + iSidurNum).disabled = (_FirstChild.text == "0");
                                break;
                            case "PITZUL_HAFSAKA":
                                $get("SD_ddlPHfsaka" + iSidurNum).disabled = true;
                                break;
                            case "CHARIGA":
                                $get("SD_ddlException" + iSidurNum).disabled = (_FirstChild.text == "0");
                                if (_FirstChild.text == "0")
                                    $get("SD_ddlException" + iSidurNum).value = "-1";
                                break;
                            case "HASHLAMA":
                                if (_FirstChild.text == "0") {
                                    $get("SD_ddlHashlama" + iSidurNum).disabled = true;
                                    $get("SD_ddlHashlama" + iSidurNum).value = 0;
                                }
                                else {
                                    wsGeneral.IsHashlamaAllowed(iSidurNum, $get("clnDate").value, callBackHashlama, null, iSidurNum);

                                }
                                break;
                            case "OUT_MICHSA":
                                $get("SD_chkOutMichsa" + iSidurNum).disabled = (_FirstChild.text == "0");
                                $get("SD_chkOutMichsa" + iSidurNum).checked = false;
                                break;
                            case "ADD_PEILUT":
                                var sPrev = $get("SD_imgAddPeilut" + iSidurNum).style.display;
                                if (_FirstChild.text == "0") {
                                    $get("SD_imgAddPeilut" + iSidurNum).style.display = 'none';
                                    if (sPrev == 'block')
                                        $get("SD_imgAddPeilut" + iSidurNum).click();
                                }
                                else {
                                    $get("SD_imgAddPeilut" + iSidurNum).style.display = 'block';
                                    $get("SD_imgAddPeilut" + iSidurNum).disabled = false;
                                    if (PeilutVisaExists(iSidurNum)) {
                                        $get("SD_hidGeneralParam").value = "1";
                                        $get("SD_imgAddPeilut" + iSidurNum).click();
                                    }
                                }
                                break;
                            case "SIDUR_VISA":
                                if (_FirstChild.text == "1") {
                                    //סידור ויזה   - הוספת פעילות   
                                    $get("SD_hidGeneralParam").value = "1";
                                    $get("SD_imgAddPeilut" + iSidurNum).click();
                                }
                                break;
                        }
                        _FirstChild = _FirstChild.nextSibling;
                    }
                }
            }
            $get("SD_imgCancel".concat(iSidurNum)).disabled = false;
            $get("SD_lblSidurCanceled".concat(iSidurNum)).disabled = false;
            $get("SD_chkLoLetashlum".concat(iSidurNum)).disabled = false;
            $get("SD_chkLoLetashlum".concat(iSidurNum)).setAttribute("OrgEnabled", "1");
            $get("SD_chkLoLetashlum".concat(iSidurNum)).checked = false;
            $get("SD_imgCancel".concat(iSidurNum)).className = "ImgChecked";
            if ($get("SD_txtSH" + iSidurNum).disabled == false)
                ($get("SD_txtSH" + iSidurNum)).focus();
        }
    }
/**
updates global variable _Sidur. Complexity - Low
@method ClearSidurTitle
*/
     function ClearSidurTitle(iSidurIndex){
         _Sidur = $get("SD_lblSidur" + iSidurIndex);
         if (_Sidur != null)
             if (_Sidur.value == '') {
                 _Sidur.title = '';                
             }
     }
/**
gets value from ui and validates. Complexity - Low
@method PeilutVisaExists
*/
     function PeilutVisaExists(iSidurNum){
         var bExist=false;
         var _Peilut = $get("SD_" + padLeft(iSidurNum,'0',3));
         if (_Peilut != null) {
             for (var j = 1; j < _Peilut.firstChild.childNodes.length; j++){
                 if (_Peilut.firstChild.childNodes[j].cells[_COL_MAKAT].childNodes[0].value == '50000000'){
                     bExist = true;
                     break;
                 }
             }
         }
         return bExist;
     }
/**
gets value from ui and validates. Complexity - Low
@method CheckIfFirstPeilutWithCarNum
*/
     function CheckIfFirstPeilutWithCarNum(_Peilut, iCurrPeilutIndex) {
         var bFound = false;
         var sMustOtoNum;
         for (var j = 1; j < _Peilut.firstChild.childNodes.length; j++){
              sMustOtoNum = _Peilut.firstChild.childNodes[j].cells[_COL_CAR_NUMBER].childNodes[0].getAttribute("MustOtoNum");
              if ((sMustOtoNum == '1') && (Number(iCurrPeilutIndex)-1 != j)) 
                  bFound = true;
          }
          return bFound;
     }


     function CheckIfFirstPeilutWithCarLicense(_Peilut, iCurrPeilutIndex) {
         var bFound = false;
         var sMustOtoNum;
         for (var j = 1; j < _Peilut.firstChild.childNodes.length; j++) {
             sMustOtoNum = _Peilut.firstChild.childNodes[j].cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].getAttribute("MustOtoNum");
             if ((sMustOtoNum == '1') && (Number(iCurrPeilutIndex) - 1 != j))
                 bFound = true;
         }
         return bFound;
     }
/**
gets value from ui and validates. Complexity - Medium
@method FindCarNumInAllSidurim
*/
     function FindCarNumInAllSidurim(){
         var bMultiCarNum = false;
         var iCurrSidurNumber = 0;
         var _Sidur, _Peilut;
         var lCurrCarNumber = 0;
         var lCarNumber = 0;
         var lCarLicence = 0;
         _Sidur = $get("SD_lblSidur" + iCurrSidurNumber);
         while (_Sidur != null) {
             _Peilut = $get("SD_" + padLeft(iCurrSidurNumber, '0', 3));
             if (_Peilut != null) {
                 for (var j = 1; j < _Peilut.firstChild.childNodes.length; j++) {
                     lCurrCarNumber = _Peilut.firstChild.childNodes[j].cells[_COL_CAR_NUMBER].childNodes[0].value;
                     if ((lCurrCarNumber != '') && (lCurrCarNumber != '0'))
                         if (lCarNumber == 0){
                             lCarNumber = lCurrCarNumber;
                             lCarLicence=_Peilut.firstChild.childNodes[j].cells[_COL_CAR_NUMBER].childNodes[0].title;
                             }
                         else
                             if (lCurrCarNumber != lCarNumber)
                                 bMultiCarNum = true;
                 }
             }
             iCurrSidurNumber = iCurrSidurNumber + 1;
             _Sidur = $get("SD_lblSidur" + iCurrSidurNumber);
         }
         return bMultiCarNum + "|" + lCarNumber + "|" + lCarLicence;
     }

     function FindCarLicenseInAllSidurim() {
         var bMultiCarNum = false;
         var iCurrSidurNumber = 0;
         var _Sidur, _Peilut;
         var lCurrCarLicenseNumber = 0;
         var lCarLicence = 0;
          var lCarNumber = 0;
         _Sidur = $get("SD_lblSidur" + iCurrSidurNumber);
         while (_Sidur != null) {
             _Peilut = $get("SD_" + padLeft(iCurrSidurNumber, '0', 3));
             if (_Peilut != null) {
                 for (var j = 1; j < _Peilut.firstChild.childNodes.length; j++) {
                     lCurrCarLicenseNumber = _Peilut.firstChild.childNodes[j].cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].value;
                     if ((lCurrCarLicenseNumber != '') && (lCurrCarLicenseNumber != '0'))
                         if (lCarLicence == 0) {
                             lCarLicence = lCurrCarLicenseNumber;
                             lCarNumber = _Peilut.firstChild.childNodes[j].cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].title;
                         }
                         else
                             if (lCurrCarLicenseNumber != lCarLicence)
                                 bMultiCarNum = true;
                 }
             }
             iCurrSidurNumber = iCurrSidurNumber + 1;
             _Sidur = $get("SD_lblSidur" + iCurrSidurNumber);
         }
         return bMultiCarNum + "|" + lCarLicence + "|" + lCarNumber;
     }
/**
recieves params. gets UI values. Updates ui values and attributes. Complexity - Low
@method SetCarNumber
*/
     function SetCarNumber(iSidurIndex, oRId, iPeilutIndex) {
         var lCarNumber = 0;
         var lCurrCarNumber = 0;
         var lCurrCarLicence = 0;
         var bMultiCarNum = false;
         var sResult;      
         _Peilut = $get("SD_" + padLeft(iSidurIndex, '0', 3));
         if (_Peilut != null) {
             //אם יש פעילות אחת בסידור היא הפעילות שהוספנו ולכן נחפש את מספר הרכב בכל הסידור
             //או שיש מספר פעילויות אבל זו הראשונה שדורשת מספר רכב
             if ((_Peilut.firstChild.childNodes.length <= 2) || (!CheckIfFirstPeilutWithCarNum(_Peilut, iPeilutIndex))){
                  sResult = FindCarNumInAllSidurim();
                  sResult = sResult.split("|");
                  if (sResult[0]=='false')
                      bMultiCarNum = false;
                  else
                      bMultiCarNum = true;
                  lCarNumber = sResult[1];
                  lCurrCarLicence = sResult[2];
              }
              else{
                  for (var j = 1; j < _Peilut.firstChild.childNodes.length; j++){
                      lCurrCarNumber = _Peilut.firstChild.childNodes[j].cells[_COL_CAR_NUMBER].childNodes[0].value;
                      if ((lCurrCarNumber != '') && (lCurrCarNumber != '0'))
                          if (lCarNumber == 0) {
                              lCarNumber = lCurrCarNumber;
                              lCurrCarLicence = _Peilut.firstChild.childNodes[j].cells[_COL_CAR_NUMBER].childNodes[0].title;
                          }
                          else
                              if (lCurrCarNumber != lCarNumber)
                                  bMultiCarNum = true;
                  }
                }
              }
              if ((!bMultiCarNum) && (lCarNumber != 0)){
                  $get(oRId).cells[_COL_CAR_NUMBER].childNodes[0].value = lCarNumber;
                  $get(oRId).cells[_COL_CAR_NUMBER].childNodes[0].setAttribute("OldV", lCarNumber);
                  $get(oRId).cells[_COL_CAR_NUMBER].childNodes[0].title = lCurrCarLicence;         
              }
     }

     function SetLicenseNumber(iSidurIndex, oRId, iPeilutIndex) {
         var lCarNumber = 0;
         var lCurrCarLicenseNumber = 0; 
         var lCurrCarLicence = 0;
         var bMultiCarNum = false;
         var sResult;
         _Peilut = $get("SD_" + padLeft(iSidurIndex, '0', 3));
         if (_Peilut != null) {
             //אם יש פעילות אחת בסידור היא הפעילות שהוספנו ולכן נחפש את מספר הרכב בכל הסידור
             //או שיש מספר פעילויות אבל זו הראשונה שדורשת מספר רכב
             if ((_Peilut.firstChild.childNodes.length <= 2) || (!CheckIfFirstPeilutWithCarLicense(_Peilut, iPeilutIndex))) {
                 sResult = FindCarLicenseInAllSidurim();
                 sResult = sResult.split("|");
                 
                 if (sResult[0] == 'false')
                     bMultiCarNum = false;
                 else
                     bMultiCarNum = true;
                 lCurrCarLicence = sResult[1];
                  lCarNumber = sResult[2];
             }
             else {
                 for (var j = 1; j < _Peilut.firstChild.childNodes.length; j++) {
                     lCurrCarLicenseNumber = _Peilut.firstChild.childNodes[j].cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].value;
                     if ((lCurrCarLicenseNumber != '') && (lCurrCarLicenseNumber != '0'))
                         if (lCurrCarLicence == 0) {
                             lCurrCarLicence = lCurrCarLicenseNumber;
                             lCarNumber = _Peilut.firstChild.childNodes[j].cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].title;
                         }
                         else
                             if (lCurrCarLicenseNumber != lCurrCarLicence)
                                 bMultiCarNum = true;
                 }
             }
         }
         if ((!bMultiCarNum) && (lCurrCarLicence != 0)) {
             $get(oRId).cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].value = lCurrCarLicence;
             $get(oRId).cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].setAttribute("OldV", lCurrCarLicence);
             $get(oRId).cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].title = lCarNumber;
         }
     }
/**
recieves params. gets UI values. Updates ui values and attributes. Complexity - Low
@method chkHashlama
*/
    function chkHashlama(val,args){
        var id = val.getAttribute("index");
        var oTxt1 = $get("SD_txtSH".concat(id)).value;
        var oTxt2 = $get("SD_txtSG".concat(id)).value;
        var sSidurDate = $get("SD_lblDate".concat(id)).innerHTML;
        var sCardDate = $get("clnDate").value;      
        var dStartHour = new Date(Number(sSidurDate.substr(6,4)),Number(sSidurDate.substr(3,2))-1, Number(sSidurDate.substr(0,2)),Number(oTxt1.substr(0,2)),Number(oTxt1.substr(3,2)));
        var dEndHour = new Date(Number(sCardDate.substr(6, 4)), Number(sCardDate.substr(3, 2)) - 1, Number(sCardDate.substr(0, 2)), Number(oTxt2.substr(0, 2)), Number(oTxt2.substr(3, 2)));
        var oDDL = $get("SD_ddlHashlama".concat(id));
        var _Add = $get("SD_txtDayAdd".concat(id)).value;
        if ((Number(_Add)==1))        
            dEndHour.setDate(dEndHour.getDate() + 1);
                       
        SidurTime = GetSidurTime(dStartHour,dEndHour);  
        args.IsValid = ((Number(oDDL.value) > SidurTime) ||  (Number(oDDL.value<=0)));
    }
/**
recieves params. gets UI values. Updates ui values and attributes. Uses wsGeneral service. Complexity - Low
@method SetKnisaActualMin
*/
    function SetKnisaActualMin(oRow){
        arrKnisa = $get(oRow.id).cells[_COL_KNISA].childNodes[0].nodeValue.split(',');
        if ((Number(arrKnisa[0]) > 0) && ($get(oRow.id).cells[_COL_CANCEL].childNodes[0].className == 'ImgKnisaS')) {//אם כניסה לפי צורך
            if (KnisaLefiZorech($get(oRow.id).cells[_COL_LINE_DESCRIPTION].firstChild.nodeValue,
                                    parseInt($get(oRow.id).cells[_COL_KNISA].firstChild.nodeValue.split(',')[0]))) {
                var lMakat = $get(oRow.id).cells[_COL_MAKAT].childNodes[0].value;
                var iSidurIndex = Number(String(oRow.id).substr(String('SD_').length, 3));
                var sSidurDate = $get("SD_lblDate".concat(iSidurIndex)).innerHTML;
                wsGeneral.GetKnisaActualMin(lMakat, sSidurDate,Number(arrKnisa[0]), callBackKnisa,null, oRow);
            }
        }
    }
/**
recieves params. gets UI values. raises alert. Complexity - Low
@method callBackKnisa
*/
    function callBackKnisa(result, oRow) {
        if (result != "0") {
            $get(oRow.id).cells[_COL_ACTUAL_MINUTES].childNodes[0].value = result;
        }
        else {
            $get(oRow.id).cells[_COL_CANCEL].childNodes[0].className = "ImgKnisaS";
            alert('לא נמצא משך לכניסה ' + $get(oRow.id).cells[_COL_LINE_DESCRIPTION].innerHTML);
        }
    }

    function Test(val, args) { }
/**
recieves params. does validations. uses wsGeneral service. Complexity - Low
@method ChkOto
*/  
    function ChkOto(oRow) {
        var KeyID = event.keyCode;
        if (((KeyID >= 48) && (KeyID <= 57)) || ((KeyID >= 96) && (KeyID <= 105))) {
            oId = String(oRow.id).substr(0, oRow.id.length - 6);
            var lOtoNo = $get(oId).cells[_COL_CAR_NUMBER].childNodes[0].value;
            SetBtnChanges(); //SetLvlChg(3);
            if ((lOtoNo != '') && (trim(String(lOtoNo)).length >= 5)) {
                wsGeneral.CheckOtoNo(lOtoNo, callBackOto, null, oRow);
            }
        }           
    }
/**
recieves params. does validations. updates ui. . Complexity - Low
@method callBackOto
*/
    function callBackOto(result, oRow) {
        var oId = String(oRow.id).substr(0, oRow.id.length - 6);
        if (result == '0') {
            var sBehaviorId = 'vldCarNumBehv'.concat(oId);
            $find(sBehaviorId)._ensureCallout();
            $find(sBehaviorId).show(true);
            $get(oId).cells[_COL_CAR_NUMBER].childNodes[0].title = "מספר רכב שגוי";
            $get(oId).cells[_COL_CAR_NUMBER].childNodes[0].value = "";
        }
        else {
            $get(oId).cells[_COL_CAR_NUMBER].childNodes[0].title = result;
            var OrgDisable = $get(oId).cells[_COL_CAR_NUMBER].childNodes[0].disabled;
            $get(oId).cells[_COL_CAR_NUMBER].childNodes[0].disabled = true;
            CopyOtoNum(oRow);
        }     
    }
/**
recieves params. does validations. updates ui. calls several internal methods. . Complexity - High
@method ChangeStatusPeilut
*/
    function ChangeStatusPeilut(Row, FirstMkt, OrgMktType, SubMkt, PeilutAv)
    {   SetBtnChanges();
         var oColCancel, oColPeilutCancel;
         var iSidur=0;         
         var _NextRow;
         var arrKnisa, lMkt, lNxtMkt;
         
         //נבצע רק אם שורה נוכחית או שכניסות/ויסותים/ אלמנט הפסקה
         if ((FirstMkt == 0) || ((SubMkt == 1) && (FirstMkt!=0))) {
             oColCancel = $get(Row.id).cells[_COL_CANCEL].childNodes[0];
             oColPeilutCancel = $get(Row.id).cells[_COL_CANCEL_PEILUT].childNodes[0];
             if ((oColCancel.className == "ImgCheckedPeilut") || (oColCancel.className == "ImgCheckedDisablePeilut")) {
                 if (FirstMkt != 0) //אם כניסה בעקבות שינוי רשומת אב, נעביר את רשומת האב
                     SetPeilutStatus(Row.id, true, iSidur, -1, PeilutAv);
                 else {
                     if (KnisaLefiZorech($get(Row.id).cells[_COL_LINE_DESCRIPTION].childNodes[0].nodeValue,
                                            parseInt($get(Row.id).cells[_COL_KNISA].childNodes[0].nodeValue.split(',')[0])))
                         SetPeilutStatus(Row.id, false, iSidur, -1);
                     else
                         SetPeilutStatus(Row.id, true, iSidur, -1);
                 }               
             }
             else {               
                 if (PeilutAv != null){
                     if (PeilutAv.cells[_COL_CANCEL].firstChild.className == "ImgCancel")//אם רשומת האב מבוטלת לא נאפשר עדכון הפעילויות בנים
                         SetPeilutStatus(Row.id, true, iSidur, -1, PeilutAv);
                     else
                         SetPeilutStatus(Row.id, false, iSidur, -1, PeilutAv);
                 } else {
                     SetPeilutStatus(Row.id, false, iSidur, -1, PeilutAv);
                 }                
             }             
         }

        FirstMkt = FirstMkt + 1;
        if (Number(FirstMkt) == 1) {
            lMkt = ($get(Row.id).cells[_COL_MAKAT].childNodes[0].value);
            //אם קו שירות, נבטל גם ויסותים וכניסות של אותו קו
            if (((Number(lMkt >= 100000)) && (Number(lMkt < 50000000))) && (String($get(Row.id).cells[_COL_KNISA].childNodes[0].nodeValue).split(',')[0] == 0)) {
                OrgMktType = 1;
                PeilutAv = $get(Row.id);
            }
        }
        //אם במקור ביטלנו קו שירות נבטל גם כניסות 
        if (((Number(OrgMktType) == 1)) && (SubMkt!=2)){
            if (Row.id != ''){
                _NextRow = $get(Row.id).nextSibling;
                if (_NextRow != null) {
                    if (_NextRow.cells[_COL_KNISA].childNodes[0].nodeValue != '') {
                        arrKnisa = String(_NextRow.cells[_COL_KNISA].childNodes[0].nodeValue).split(',');
                        lNxtMkt = Number(_NextRow.cells[_COL_MAKAT].childNodes[0].value);
                    } //,כניסות 
                    if (Number(arrKnisa[0]) > 0) //|| ((lNxtMkt >= 70000000) && (lNxtMkt <= 70099999)) || (((lNxtMkt >= 74300000) && (lNxtMkt <= 74399999))))
                        ChangeStatusPeilut(_NextRow, FirstMkt, OrgMktType, 1, PeilutAv);
                    else {
                        lNxtMkt = Number(_NextRow.cells[_COL_MAKAT].childNodes[0].value);
                        //אם הגענו לקו שירות נעצור
                        if ((((Number(lNxtMkt >= 100000)) && (Number(lNxtMkt < 50000000))) && (String(_NextRow.cells[_COL_KNISA].childNodes[0].nodeValue).split(',')[0] == 0)))
                            ChangeStatusPeilut(_NextRow, FirstMkt, OrgMktType, 2 ); //קן שירות- תנאי עצירה
                          else
                            ChangeStatusPeilut(_NextRow, FirstMkt, OrgMktType, 0);
                    }
                }
            }
        }
        return false;
    }
/**
recieves params.  updates ui. calls SetSidurStatus internal method. . Complexity - Low
@method ChangeStatusSidur
*/
    function ChangeStatusSidur(id){      
     var iIndex = String(id).substr(String(id).length-1,1);
     SetBtnChanges(); SetLvlChg(2, iIndex);
     if ($get(id).className == "ImgChecked")
     {
        SetSidurStatus(iIndex,true);
        $get(id).className = "ImgCancel";
        $get("SD_lblSidurCanceled".concat(iIndex)).value="1";
     }
     else
     {
        SetSidurStatus(iIndex,false);       
        $get(id).className = "ImgChecked";
        $get("SD_lblSidurCanceled".concat(iIndex)).value="0";
    }
   
     return false;
    }
/**
recieves params.  updates ui. calls SetSidurStatus internal method. . Complexity - High
@method SetPeilutStatus
*/
    function SetPeilutStatus(RowId, bFlag, iSidur, iSidurIndex, PeilutAv)
    {   SetBtnChanges();//SetLvlChg(3);
        var oRow=$get(RowId);
        var oColPeilutCancel=oRow.cells[_COL_CANCEL_PEILUT].firstChild;
        var oColCancel=oRow.cells[_COL_CANCEL].firstChild;
        var oColTor =oRow.cells[_COL_KISUY_TOR].firstChild;
        if ((oRow.cells[_COL_ACTUAL_MINUTES].firstChild.getAttribute("OrgEnabled"))=="1"){
            oRow.cells[_COL_ACTUAL_MINUTES].firstChild.disabled = bFlag;
        }
        if ((oRow.cells[_COL_MAKAT].firstChild.getAttribute("OrgEnabled"))=="1"){
            oRow.cells[_COL_MAKAT].firstChild.disabled = bFlag; 
            }
        if ((oRow.cells[_COL_CAR_NUMBER].firstChild.getAttribute("OrgEnabled"))=="1"){    
            oRow.cells[_COL_CAR_NUMBER].firstChild.disabled = bFlag;
            }
        if ((oRow.cells[_COL_SHAT_YETIZA].firstChild.getAttribute("OrgEnabled"))=="1"){    
            oRow.cells[_COL_SHAT_YETIZA].firstChild.disabled = bFlag;
            }
        if (iSidur==1) {
           if (bFlag)             
              oColCancel.disabled = bFlag;                        
           else           
            if (oColCancel.getAttribute("OrgEnabled")=="1")            
                 oColCancel.disabled = bFlag;           
        }
        if (oColTor.className!=undefined){    
        if ((oColTor.getAttribute("OrgEnabled"))=="1"){        
          oColTor.disabled = bFlag;       
        }}
        if (oColCancel.className != undefined){
            if (bFlag){                
                if (iSidur == 1) {//הגענו מביטול סידור
                    if ($get("SD_lblSidurCanceled".concat(iSidurIndex)).value == "1") {
                        oColCancel.className = "ImgCancel"; oColPeilutCancel.value = "1";
                    }
                    else {
                        oColCancel.className = "ImgCancelDisable"; oColPeilutCancel.value = "1";
                    }
                }
                else {
                    if (oColCancel.getAttribute("OrgEnabled") == "1") {
                        if (oRow.cells[_COL_LINE_DESCRIPTION].firstChild.nodeValue != null) {
                            if (KnisaLefiZorech(oRow.cells[_COL_LINE_DESCRIPTION].firstChild.nodeValue,
                                                    parseInt($get(oRow.id).cells[_COL_KNISA].firstChild.nodeValue.split(',')[0])))
                            {
                                if (PeilutAv != null) {
                                    if (PeilutAv.cells[_COL_CANCEL].childNodes[0].className == "ImgCancel")
                                    { oColCancel.className = "ImgCancel"; oColPeilutCancel.value = "1"; }
                                }
                                else {
                                    if (oColCancel.className == "ImgKnisaS")
                                    { oColCancel.className = "ImgCheckedPeilut"; oColPeilutCancel.value = "0"; }
                                    else {
                                        oColCancel.className = "ImgKnisaS";
                                        oColPeilutCancel.value = "0";
                                        oRow.cells[_COL_ACTUAL_MINUTES].firstChild.value = '0';
                                    }
                                }
                             }
                            else {
                                oColCancel.className = "ImgCancel"; oColPeilutCancel.value = "1";
                            }
                        }
                        else {
                            oColCancel.className = "ImgCancel"; oColPeilutCancel.value = "1";
                        }
                    }
                    else {
                        oColCancel.className = "ImgCancelDisable"; oColPeilutCancel.value = "1";
                    }
                }
            }
            else {//flag=false
                if (oColCancel.getAttribute("OrgEnabled") == "1") {
                    if (PeilutAv != null) {//אם זו כניסה שיש לה נסיעת אב, נבדוק אם ביטלו את נסיעת האב, אם כן תבוטל גם הכניסה
                        if (PeilutAv.cells[_COL_CANCEL].childNodes[0].className == "ImgCancel")                        
                          {oColCancel.className = "ImgCancel"; oColPeilutCancel.value = "1"; }
                        else {
                            if (KnisaLefiZorech(oRow.cells[_COL_LINE_DESCRIPTION].firstChild.nodeValue,
                                                parseInt($get(oRow.id).cells[_COL_KNISA].firstChild.nodeValue.split(',')[0])))
                            {
                                if ((oRow.cells[_COL_ACTUAL_MINUTES].firstChild.value == 0) || (oRow.cells[_COL_ACTUAL_MINUTES].firstChild.value == '')) {
                                    oColCancel.className = "ImgKnisaS";
                                    oColPeilutCancel.value = "0";
                                }
                                else
                                { oColCancel.className = "ImgCheckedPeilut"; oColPeilutCancel.value = "0"; }                                                                
                            }
                            else{
                                   oColCancel.className = "ImgCheckedPeilut"; oColPeilutCancel.value = "0";
                                }
                         }//****
                    }
                    else {
                        if ((KnisaLefiZorech(oRow.cells[_COL_LINE_DESCRIPTION].innerHTML,
                                parseInt(oRow.cells[_COL_KNISA].innerHTML.split(',')[0]))) && (oColCancel.className != "ImgKnisaS")) {
                            {
                                oColCancel.className = "ImgKnisaS";
                                oColPeilutCancel.value = "0";
                                oRow.cells[_COL_ACTUAL_MINUTES].firstChild.value = "0";
                            }
                        }
                        else
                        { oColCancel.className = "ImgCheckedPeilut"; oColPeilutCancel.value = "0"; }
                        }
                }
                else {
                    oColCancel.className = "ImgCheckedDisablePeilut"; oColPeilutCancel.value = "0";
                }
            }                       
        }       
    }
/**
recieves params.  returns value according to input. Complexity - Low
@method KnisaLefiZorech
*/
    function KnisaLefiZorech(sText, iKnisa) {
        if (sText == null)
            return 0;
        else
            return (((sText.indexOf('לפי צורך') > 0) || (sText.indexOf('לפי-צורך') > 0)) && iKnisa>0);       
    }
    function SetLabelColor(sCtl,iIndex, sColor){        
        $get(sCtl.concat(iIndex)).style.color=sColor;  
    }
    function EnableField(sCtl,iIndex,bFlag){
        if (($get(sCtl.concat(iIndex)).getAttribute("OrgEnabled"))=="1"){     
           $get(sCtl.concat(iIndex)).disabled=bFlag;} 
    }
/**
recieves params.  sets UI color, enables fields, updates UI. Complexity - Medium
@method SetSidurStatus
*/
    function SetSidurStatus(iIndex, bFlag) {
        var _Sidur;
     MovePanel(iIndex);  
     if (bFlag){
        SetLabelColor("SD_lblSidur",iIndex,"gray");
        SetLabelColor("SD_txtSHL",iIndex,"gray");
        SetLabelColor("SD_txtSGL",iIndex,"gray");     
     }
     else{     
       SetLabelColor("SD_lblSidur",iIndex,"black");
       SetLabelColor("SD_txtSHL",iIndex,"black");
       SetLabelColor("SD_txtSGL",iIndex,"black");
   }

     $get("SD_pnlContent".concat(iIndex)).disabled = bFlag;
     _Sidur = $get("SD_lblSidur".concat(iIndex));
     _Sidur.disabled = bFlag;
     EnableField("SD_ddlHashlama",iIndex,bFlag);
     EnableField("SD_ddlPHfsaka",iIndex,bFlag);
     EnableField("SD_ddlException",iIndex,bFlag);    
     EnableField("SD_chkOutMichsa",iIndex,bFlag);   
     EnableField("SD_ddlResonOut",iIndex,bFlag);   
     EnableField("SD_ddlResonIn",iIndex,bFlag);
     EnableField("SD_chkLoLetashlum", iIndex, bFlag);
     if ($get("SD_lblSidur".concat(iIndex)).value != null) {
         if (_Sidur.value != '') {
             EnableField("SD_txtSH", iIndex, bFlag);
             EnableField("SD_txtSG", iIndex, bFlag);
         }
         else {
             EnableField("SD_txtSH", iIndex, true);
             EnableField("SD_txtSG", iIndex, true);
         }
     }
     else {
         EnableField("SD_txtSH", iIndex, bFlag);
         EnableField("SD_txtSG", iIndex, bFlag);
     }
     EnableField("SD_txtSHL", iIndex, bFlag);
     EnableField("SD_txtSGL", iIndex, bFlag);
     var _AddPeilut = $get("SD_imgAddPeilut".concat(iIndex));
     if (_AddPeilut != undefined)
         _AddPeilut.disabled = bFlag;        
     
     if ($get("SD_cImgS".concat(iIndex))!=null){  
        $get("SD_cImgS".concat(iIndex)).disabled = bFlag;}
     var sIndex = String("00".concat(String(iIndex)));
     sIndex = sIndex.substr(sIndex.length-3,3);
     var oGrid = $get("SD_".concat(sIndex));
     if (oGrid!=null){     
         for (i=1; i<oGrid.rows.length; i++){ 
          if ((oGrid.rows[i].id)!=''){
              SetPeilutStatus(oGrid.rows[i].id, bFlag, 1, iIndex);
          }
      } 
     } 
    }
/**
recieves params.  hides ui elements. Complexity - Low
@method MovePanel
*/

    function MovePanel(iIndex) {          
        if ($find("cPanel".concat(iIndex)) != null) {
            if ($find("cPanel".concat(iIndex))._collapsed == false)            
                $find("cPanel".concat(iIndex))._collapsed = true;
            else
                $find("cPanel".concat(iIndex))._collapsed = false;                
        }             
    }
/**
recieves params.  hides ui elements. Complexity - Low
@method closePanel
*/
    function closePanel(iIndex){  
        if ($find("cPanel".concat(iIndex))!=null){                           
        $find("cPanel".concat(iIndex))._collapsed = false;
        $find("cPanel".concat(iIndex))._doOpen();}              
    }
/**
recieves params.  shows ui elements.Complexity - Low
@method openPanel
*/
    function openPanel(iIndex){  
        if ($find("cPanel".concat(iIndex))){
        $find("cPanel".concat(iIndex))._doOpen();}
    }   
/**
recieves params. gets ui values. Update ui. long logic. dates monipulation. Complexity - High
@method ChkExitHour
*/
    function ChkExitHour(val,args)
    {   SetBtnChanges();//SetLvlChg(3);
        if ($get("clnDate").value!='')
        { var dShatYetiza = new Date();                                             
          var dSExitHour = new Date();     
          var dEExitHour = new Date();              
          var sGridRowID = String(val.id).substr(0,val.id.indexOf('vldPilutSY')-1);
          var sActualShatYetiza = $get(sGridRowID).cells[_COL_SHAT_YETIZA].childNodes[0].value;
          var iPDayToAdd = $get(sGridRowID).cells[_COL_DAY_TO_ADD].childNodes[0].value 
          var sParam29 = $get("SD_hidParam29").value;   
          var sEndValidHour = "23:59";
          var sCardDate = $get("clnDate").value;        
          if(IsValidTime(sActualShatYetiza)){                     
              dShatYetiza.setHours(sActualShatYetiza.substr(0,2)); 
              dShatYetiza.setMinutes(sActualShatYetiza.substr(sActualShatYetiza.length-2,2));             
              var dCardDate = new Date(Number(sCardDate.substr(6,4)), Number(sCardDate.substr(3,2))-1,Number(sCardDate.substr(0,2)),0,0);            
              var iCollpaseHeaderIndex = Number(String(val.id).substr(String('SD_').length,3)); 
              var sSidurDate = $get("SD_lblDate".concat(iCollpaseHeaderIndex)).innerHTML;          
              var dSidurDate = new Date(Number(sSidurDate.substr(6,4)),Number(sSidurDate.substr(3,2))-1, Number(sSidurDate.substr(0,2)), 0,0);           
              dSExitHour.setHours(sParam29.substr(0,2));   
              dSExitHour.setMinutes(sParam29.substr(3,2));    
              utcCardDate = Date.UTC(dCardDate.getFullYear(), dCardDate.getMonth()+1, dCardDate.getDate(),0,0,0);
              dSidurDate = Date.UTC(dSidurDate.getFullYear(), dSidurDate.getMonth()+1, dSidurDate.getDate(),0,0,0);                
              dEExitHour.setHours(sEndValidHour.substr(0,2));    
              dEExitHour.setMinutes(sEndValidHour.substr(3,2));                  
          
             val.errormessage = "  הוקלד ערך שגוי. יש להקליד שעת יציאה בטווח " + sParam29.toString() + " -  " + sEndValidHour.toString();
             var utcSExitHour = Date.UTC(dSExitHour.getFullYear(), dSExitHour.getMonth() + 1, dSExitHour.getDate(), dSExitHour.getHours(), dSExitHour.getMinutes(), 0);
             var utcEExitHour = Date.UTC(dEExitHour.getFullYear(), dEExitHour.getMonth() + 1, dEExitHour.getDate(), dEExitHour.getHours(), dEExitHour.getMinutes(), 0);
             var utcShatYetiza = Date.UTC(dShatYetiza.getFullYear(), dShatYetiza.getMonth() + 1, dShatYetiza.getDate(), dShatYetiza.getHours(), dShatYetiza.getMinutes(), 0);
             args.IsValid = ((utcSExitHour <= utcShatYetiza) && (utcShatYetiza <= utcEExitHour));
              if (args.IsValid) {//נבדוק את שעת היציאה לעומת שעת הסידור
                  iSDayToAdd = $get("SD_txtDayAdd".concat(iCollpaseHeaderIndex)).value;
                  var sSG = $get("SD_txtSG".concat(iCollpaseHeaderIndex)).value;
                  dSidurDate = new Date(Number(sSidurDate.substr(6, 4)), Number(sSidurDate.substr(3, 2)) - 1, Number(sSidurDate.substr(0, 2)), sSG.substr(0, 2), sSG.substr(3, 2));
                  var utcSidurSG = Date.UTC(dSidurDate.getFullYear(), dSidurDate.getMonth() + 1, dSidurDate.getDate(), 0, 0, 0);
                  if (utcSidurSG == utcCardDate)
                      dSidurDate.setDate(dSidurDate.getDate() + Number(iSDayToAdd));
                  else
                      if (Number(iSDayToAdd) == 0) {
                          dSidurDate.setDate(dSidurDate.getDate() - 1);
                          $get("SD_lblDate".concat(iCollpaseHeaderIndex)).innerHTML = sCardDate;
                      }
                  dShatYetiza = new Date(Number(sCardDate.substr(6, 4)), Number(sCardDate.substr(3, 2)) - 1, Number(sCardDate.substr(0, 2)), sActualShatYetiza.substr(0, 2), sActualShatYetiza.substr(sActualShatYetiza.length - 2, 2));

                  sParamNxtDay = $get("SD_hidParam242").value;                 
                  var sYear = sCardDate.substr(sCardDate.length - 4, 4);
                  var sMonth = Number(sCardDate.substr(3, 2)) - 1;
                  var sDay = sCardDate.substr(0, 2);
                  var dParamDate = new Date();var dItemDate = new Date();
                  SetDate(dParamDate, Number(sYear), Number(sMonth), Number(sDay), Number(sParamNxtDay.substr(0, 2)), Number(sParamNxtDay.substr(3, 2)));
                  SetDate(dItemDate, Number(sYear), Number(sMonth), Number(sDay), sSG.substr(0, 2), sSG.substr(3, 2));
                  dParamDate.setSeconds(0);
                  dItemDate.setSeconds(0);
                  dParamDate.setMilliseconds(0);
                  dItemDate.setMilliseconds(0);
                  var utcItemDate = Date.UTC(dItemDate.getFullYear(), dItemDate.getMonth() + 1, dItemDate.getDate(), 0, 0, 0);
                  if ((($get(sGridRowID).cells[_COL_DAY_TO_ADD].childNodes[0].value == "1")) && (utcItemDate == utcCardDate)) {                      
                      dItemDate.setDate(dItemDate.getDate() + 1);
                  }                                
                  utcShatYetiza = Date.UTC(dShatYetiza.getFullYear(), dShatYetiza.getMonth() + 1, dShatYetiza.getDate(), 0, 0, 0);
                  if (utcShatYetiza == utcCardDate) {
                      if (iPDayToAdd == 1)
                          dShatYetiza.setDate(dShatYetiza.getDate() + 1);
                  }
                  else {
                    if (iPDayToAdd == 0)
                        dShatYetiza.setDate(dShatYetiza.getDate() - 1);
                  }                  
                  val.errormessage = "שעת היציאה לא יכולה להיות גדולה משעת גמר הסידור";
                  args.IsValid = ((dShatYetiza <= dSidurDate) || (sSG==''));                                  
                  $get(sGridRowID).cells[_COL_SHAT_YETIZA].childNodes[0].title = "תאריך שעת היציאה הוא: " + GetDateDDMMYYYY(dShatYetiza);                  
                  var sRes = ChkShatYetizaKisuyT(val.getAttribute("index"));                
                  //אם פעילות מסוג שירות נשנה לכל הכניסות את שעת היציאה בהתאם
                  var lMkt = $get(sGridRowID).cells[_COL_MAKAT].childNodes[0].value;
                  var arrKnisa = $get(sGridRowID).cells[_COL_KNISA].childNodes[0].toString().split(",");                      
                  if ((GetMakatType(lMkt) == MKT_SHERUT) && (arrKnisa[0] == 0)){
                        ChangeKnisotHour($get(sGridRowID), iPDayToAdd, dShatYetiza);                   
                  }
               }         
            }
            else
            {
              val.errormessage = "שעה לא חוקית";              
              args.IsValid = false;
            }
         }
    }
    /**
    recieves many params. gets ui values. Update ui. Complexity - Medium
    @method ChangeKnisotHour
    */
  
       function ChangeKnisotHour(oCurrPeilut, iDayToAdd, dSdDate) {
           var NextRow, lMkt, sHour, lKnisaMkt, sknisaHour, arrKnisot, MktType;

           lMkt = oCurrPeilut.cells[_COL_MAKAT].childNodes[0].value;
           sHour = oCurrPeilut.cells[_COL_SHAT_YETIZA].childNodes[0].value;
           NextRow = oCurrPeilut.nextSibling;
           if (NextRow != null) {
               lKnisaMkt = NextRow.cells[_COL_MAKAT].childNodes[0].value;
               MktType = GetMakatType(lKnisaMkt);
               arrKnisot = NextRow.cells[_COL_KNISA].childNodes[0].toString().split(",");
               while ((NextRow != null) && ((MktType != MKT_NAMAK) && (MktType != MKT_EMPTY) && ((Number(arrKnisot[0]) > 0)))) {
                   if ((MktType == MKT_SHERUT) && (lKnisaMkt == lMkt) && (Number(arrKnisot[0]) > 0)) {
                       NextRow.cells[_COL_SHAT_YETIZA].childNodes[0].value = sHour;
                       NextRow.cells[_COL_DAY_TO_ADD].childNodes[0].value = Number(iDayToAdd);
                       NextRow.cells[_COL_SHAT_YETIZA].childNodes[0].title = "תאריך שעת היציאה הוא: " + GetDateDDMMYYYY(dSdDate);
                   }
                   NextRow = NextRow.nextSibling;
                   if (NextRow != null) {
                       lKnisaMkt = NextRow.cells[_COL_MAKAT].childNodes[0].value;
                       arrKnisot = NextRow.cells[_COL_KNISA].childNodes[0].toString().split(",");
                   }
               }
           }
       }
/**
recieves params. does validations. Complexity - Low
@method IsAMinValid
*/
    function IsAMinValid(val,args){
         SetBtnChanges();//SetLvlChg(3);
         var sGridRowID = val.getAttribute("index");   
         var iKnisaNum, iKnisaType, arrKnisaVal;
         var _ActMIn = $get(sGridRowID).cells[_COL_ACTUAL_MINUTES].childNodes[0];         
         var iActualMinutes = _ActMIn.value;
         var iPlainMinutes;
         arrKnisaVal = String($get(sGridRowID).cells[_COL_KNISA].childNodes[0].nodeValue).split(',');  
         iKnisaNum= Number(arrKnisaVal[0]);
         iKnisaType=Number(arrKnisaVal[1]);
         if ((iKnisaNum>0) && (iKnisaType==1)){
           iPlainMinutes =  $get("SD_hidParam98").value;
         }
         else{
             iPlainMinutes = $get(sGridRowID).cells[_COL_MAZAN_TASHLUM].childNodes[0].nodeValue;            
         }         
         args.IsValid = (Number(iActualMinutes)>=0 && Number(iActualMinutes)<=Number(iPlainMinutes));
         if (!args.IsValid) {
             _ActMIn.value = 0;
            _ActMIn.select();
         }
    }
/**
recieves params. update ui. works with dates. Complexity - Medium
@method ChkKisyT
*/
    function ChkKisyT(val,args)    
    {  SetBtnChanges();//SetLvlChg(3);
       var sGridRowID = val.getAttribute("index");
       var sValidHour = $get(sGridRowID).cells[_COL_KISUY_TOR_MAP].childNodes[0].nodeValue; //val.outerHTML.substr(val.outerHTML.indexOf('errormessage') + String('errormessage').length + 44 ,5);
       if (($get(sGridRowID).cells[_COL_KISUY_TOR].childNodes[0].value) != '')
       {
           var sCardDate = $get("clnDate").value;
           var sActualHour = args.Value;
           var sShatYetiza = $get(sGridRowID).cells[_COL_SHAT_YETIZA].childNodes[0].value;
           var OrgPTime = new Date(); var NewPTime = new Date(); var PTime = new Date();
           var AddDay =Number($get(sGridRowID).cells[_COL_DAY_TO_ADD].childNodes[0].value);

           SetDate(OrgPTime, sCardDate.substr(6, 4), Number(sCardDate.substr(3, 2))-1, sCardDate.substr(0, 2), sShatYetiza.substr(0, 2), sShatYetiza.substr(sShatYetiza.length - 2, 2)); 
           OrgPTime.setMinutes(OrgPTime.getMinutes() - Number(sValidHour));
           OrgPTime.setDate(OrgPTime.getDate() + AddDay);

           SetDate(NewPTime, sCardDate.substr(6, 4), Number(sCardDate.substr(3, 2))-1, sCardDate.substr(0, 2), sActualHour.substr(0, 2), sActualHour.substr(sActualHour.length - 2, 2));
           if ((IsShatGmarInNextDay(sActualHour)) && (AddDay=='1')){
               NewPTime.setDate(NewPTime.getDate() + AddDay);
           }
           SetDate(PTime, sCardDate.substr(6, 4), Number(sCardDate.substr(3, 2))-1, sCardDate.substr(0, 2), sShatYetiza.substr(0, 2), sShatYetiza.substr(sShatYetiza.length - 2, 2));
           PTime.setDate(PTime.getDate() + AddDay);

           var dOrgPTime = Date.UTC(OrgPTime.getFullYear(), OrgPTime.getMonth()+1, OrgPTime.getDate(),OrgPTime.getHours(),OrgPTime.getMinutes(),0);
           var dNewPTime = Date.UTC(NewPTime.getFullYear(), NewPTime.getMonth()+1, NewPTime.getDate(),NewPTime.getHours(),NewPTime.getMinutes(),0);       
           var dPTime =  Date.UTC(PTime.getFullYear(), PTime.getMonth()+1, PTime.getDate(),PTime.getHours(),PTime.getMinutes(),0); 
           args.IsValid = (dOrgPTime<=dNewPTime) && (dNewPTime<=dPTime); 
           if (!args.IsValid)                         
                val.errormessage = "כיסוי התור שהוקלד שגוי, יש להקליד שעה בטווח  " +  sShatYetiza   + " - " + OrgPTime.toLocaleTimeString().substr(0,5);           
       }
       else      
         args.IsValid = true;       
    }
/**
recieves params. update ui. works with dates. Complexity - Low
@method ChkShatYetizaKisuyT
*/
    function ChkShatYetizaKisuyT(iIndx)    
    {
        var sMapKisuyTor = $get(iIndx).cells[_COL_KISUY_TOR_MAP].childNodes[0].nodeValue; 
        if (Number(sMapKisuyTor) == 0) {
            return '';
        }
        else {
            var sShatYetiza = $get(iIndx).cells[_COL_SHAT_YETIZA].childNodes[0].value;
            if ((sShatYetiza == '') || (sShatYetiza == '__:__')) {
                $get(iIndx).cells[_COL_KISUY_TOR].childNodes[0].value = '';
            }
            else {
                var dOrgMapKisuyTor = new Date(); var dGrdKisuyTor = new Date(); var dShatYetiza = new Date();
                dOrgMapKisuyTor.setHours(sShatYetiza.substr(0, 2));
                dOrgMapKisuyTor.setMinutes(sShatYetiza.substr(sShatYetiza.length - 2, 2));
                dOrgMapKisuyTor.setMinutes(dOrgMapKisuyTor.getMinutes() - Number(sMapKisuyTor));
                $get(iIndx).cells[_COL_KISUY_TOR].childNodes[0].value = dOrgMapKisuyTor.toLocaleTimeString().substr(0, 5);
            }
        }   
    }
/**
recieves params. update ui. works with dates. Complexity - Low
@method changeStartHour
*/
  function changeStartHour(iIndex){
      $get("SD_hidCurrIndx").value = "3|" + iIndex;
      var _ShatHatchala = $get("SD_txtSH".concat(iIndex));
      var iSidur = $get("SD_lblSidur".concat(iIndex)).innerHTML;
      if (iSidur == "")
          iSidur = $get("SD_lblSidur".concat(iIndex)).value;
      var sOrgSH = _ShatHatchala.getAttribute("OrgShatHatchala");
      var sNewSH = _ShatHatchala.value;
      var sCardDate = $get("clnDate").value;

      var OrgPTime = new Date(); var NewPTime = new Date();
      SetDate(OrgPTime, sOrgSH.substr(6, 4), Number(sOrgSH.substr(3, 2)) - 1, sOrgSH.substr(0, 2), sOrgSH.substr(11, 2), sOrgSH.substr(14, 2));
      SetDate(NewPTime, sCardDate.substr(6, 4), Number(sCardDate.substr(3, 2)) - 1, sCardDate.substr(0, 2), sNewSH.substr(0, 2), sNewSH.substr(3, 2)); 
      var dOrgPTime = Date.UTC(OrgPTime.getFullYear(), OrgPTime.getMonth() + 1, OrgPTime.getDate(), OrgPTime.getHours(), OrgPTime.getMinutes(), 0);
      var dNewPTime = Date.UTC(NewPTime.getFullYear(), NewPTime.getMonth() + 1, NewPTime.getDate(), NewPTime.getHours(), NewPTime.getMinutes(), 0);
      if (dOrgPTime != dNewPTime)
          SetBtnChanges();
      if (IsValidTime(sNewSH))
        if (sNewSH.indexOf('_')==-1)
            wsGeneral.SidurStartHourChanged(sCardDate, iSidur, sNewSH, sOrgSH,iIndex, callBackStartHour, null, iIndex);
  }
/**
recieves params. update ui. works with dates. Complexity - Medium
@method changeStartHour
*/
  function callBackStartHour(result, iIndex) {
      var dSidurSHDate = new Date();
      var sCardDate = $get("clnDate").value;

      result = result.split(",");
      if  (result[0]=='-1')
          unloadCard();
        else{
              if ((result[0] == '1') && (GetKeyPressPosition($get("SD_txtSH".concat(iIndex)))==5)) {
                  $get("SD_lblSidur".concat(iIndex)).disabled = true;
                  $get("SD_btnShowMessage").click();
                  $get("SD_lblSidur".concat(iIndex)).disabled = false;
              }
              else {
                  $get("SD_lblDate".concat(iIndex)).innerHTML = sCardDate;
              }
             var dSdDate = new Date();
             var _SHNew = $get("SD_txtSH".concat(iIndex));

             SetDate(dSidurSHDate, Number(sCardDate.substr(6, 4)), Number(sCardDate.substr(3, 2)) - 1, Number(sCardDate.substr(0, 2)), "0", "0");
             dSidurSHDate.setDate(dSidurSHDate.getDate() + Number(result[1]));    
             $get("SD_lblDate".concat(iIndex)).innerHTML = GetDateDDMMYYYY(dSidurSHDate);
             var sSdDate = $get("SD_lblDate".concat(iIndex)).innerHTML; 
             var sYear = sSdDate.substr(sSdDate.length-4,4);
             var sMonth = Number(sSdDate.substr(3, 2)) - 1;
             var sDay = sSdDate.substr(0, 2);
             SetDate(dSdDate, Number(sYear), Number(sMonth), Number(sDay), 0, 0);
             _SHNew.title = "תאריך התחלת הסידור הוא: " + GetDateDDMMYYYY(dSdDate);
             //השלמה 
             $get("SD_ddlHashlama" + iIndex).disabled = (result[2] == '0');
             if (result[2]=='1')
                 HasSidurHashlama();

             //חריגה
             $get("SD_ddlException" + iIndex).disabled = (result[3] == '0');
        }
  }
/**
recieves params. update ui with message.  Complexity - Low
@method ChkStartHour
*/
  function ChkStartHour(val, args){      
        var iIndex = String(val.id).substr(String(val.id).length - 1, 1);
        $get("SD_hidCurrIndx").value = "3|" + iIndex;
        var sShatHatchala = $get("SD_txtSH".concat(iIndex));

        if (IsValidTime(sShatHatchala.value)){
            if (!IsSHBigSG(val, args)){
                if (args.IsValid == false)
                    val.errormessage = val.errormessage + "\n שעת ההתחלה אינה יכולה להיות גדולה או שווה לשעת הגמר " + "\n";
                else{
                    val.errormessage = "\n שעת ההתחלה אינה יכולה להיות גדולה או שווה לשעת הגמר \n";
                    args.IsValid = false;
                }
            }
            if (iIndex > 0){
                if (!IsSHGreaterPrvSG(val, args)){
                    if (args.IsValid == false)
                        val.errormessage = val.errormessage + "\n שעת ההתחלה שהוקלדה גורמת לחפיפת זמנים עם הסידור הקודם " + "\n";
                    else {
                        val.errormessage = "\n שעת ההתחלה שהוקלדה גורמת לחפיפת זמנים עם הסידור הקודם";
                        args.IsValid = false;
                    }
                }
            }            
        }
        else {
            args.IsValid = false;
            val.errormessage = "שעה לא חוקית";
        }
  }
/**
recieves params. update ui .  Complexity - Low
@method SetHashlama
*/

    function SetHashlama(iSidurIndex) {
        var sCardDate = $get("clnDate").value;        
        var sShatGmar = $get("SD_txtSG".concat(iSidurIndex)).value;
        var iAddDay = $get("SD_txtDayAdd".concat(iSidurIndex)).value;
        if ((sShatGmar == '') || (sShatGmar == "__:__"))
            $get("SD_ddlHashlama" + iSidurIndex).disabled = true;
        else {
            //if (sShatGmar.indexOf("_") == -1)
                wsGeneral.UpdateShatGmar(iSidurIndex, sCardDate, sShatGmar, iAddDay, callBackHashlama, null, iSidurIndex);
        }
    }
/**
recieves params. update ui .  Complexity - Low
@method callBackHashlama
*/
    function callBackHashlama(result, iSidurIndex) {
        result = result.split(",");
        if (($get("SD_ddlHashlama" + iSidurIndex))!=null){
            $get("SD_ddlHashlama" + iSidurIndex).disabled = (result[0] == '0');
            if (result[0] == '1')
                HasSidurHashlama();
        } //חריגה       
        if (($get("SD_ddlException" + iSidurIndex)) != null) {
            $get("SD_ddlException" + iSidurIndex).disabled = ((result[1] == '0') || (result[1])==undefined);           
        }
    }
/**
recieves params. validateions .  Complexity - Medium
@method ISSGValid
*/
    function ISSGValid(val, args){
         SetBtnChanges();
         //נבדוק אם שעת ההתחלה נמצאת בין פרמטרים כללים או פרמטרים של סידור         
         var iIndex = String(val.id).substr(String(val.id).length - 1, 1);
         var sCardDate = $get("clnDate").value;
         var sShatGmar = $get("SD_txtSG".concat(iIndex));
         var dShatGmar = new Date();
         
         SetDate(dShatGmar, sCardDate.substr(6, 4), Number(sCardDate.substr(3, 2)) - 1, sCardDate.substr(0, 2), 0, 0); 
         if ((!IsShatGmarInNextDay(sShatGmar.value)) && (sShatGmar.value != '00:00')) {
             $get("SD_txtDayAdd".concat(iIndex)).value = "0";
             $get("SD_txtSG".concat(iIndex)).title = "תאריך גמר הסידור הוא: " + GetDateDDMMYYYY(dShatGmar);
         }
         if (IsValidTime(sShatGmar.value)) {//             
             if (!IsSHBigSG(val, args)) {
                 if (args.IsValid == false) {
                     val.errormessage.concat("\n");
                     val.errormessage = val.errormessage.concat("\n שעת הגמר אינה יכולה להיות קטנה או שווה לשעת ההתחלה");
                 }
                 else {
                     val.errormessage = "\n שעת הגמר אינה יכולה להיות קטנה או שווה לשעת ההתחלה";
                     args.IsValid = false;
                 }
             }
             if (!IsEHourBigSHour(val, args)) {
                 if (args.IsValid == false) {
                     val.errormessage = val.errormessage + " שעת הגמר שהוקלדה גורמת לחפיפת זמנים עם הסידור הבא";
                 }
                 else {
                     val.errormessage = "שעת הגמר שהוקלדה גורמת לחפיפת זמנים עם הסידור הבא";
                     args.IsValid = false;
                 }
             }
         }
         else {
             val.errormessage = "שעה לא חוקית";
             args.IsValid = false;
         }

         if (args.IsValid)
            //נבדוק אם לאפשר השלמה   
             SetHashlama(iIndex);

    }
    /**
    recieves params. validateions.  dates. Complexity - Medium
    @method ISSHLValid
    */
    function ISSHLValid(val,args)
    {
        args.IsValid = true;
        var iIndex = String(val.id).substr(String(val.id).length - 1, 1);
        var sSidurDate = $get("SD_lblDate".concat(iIndex)).innerHTML;
        var sCardDate = $get("clnDate").value;
        var sSHatchala = $get("SD_txtSH".concat(iIndex)).value;
        var sSGmar = $get("SD_txtSG".concat(iIndex)).value;
        var sSHLetashlum = $get("SD_txtSHL".concat(iIndex)).value;
        var sSGLetashlum = $get("SD_txtSGL".concat(iIndex)).value;

        var ShatHatchala = new Date(Number(sSidurDate.substr(6, 4)), Number(sSidurDate.substr(3, 2)) - 1, Number(sSidurDate.substr(0, 2)), sSHatchala.substr(0, 2), sSHatchala.substr(sSHatchala.length - 2, 2));
        var ShatGmar = new Date(Number(sCardDate.substr(6, 4)), Number(sCardDate.substr(3, 2)) - 1, Number(sCardDate.substr(0, 2)), sSGmar.substr(0, 2), sSGmar.substr(sSGmar.length - 2, 2));
        ShatGmar.setDate(ShatGmar.getDate() + Number($get("SD_txtDayAdd".concat(iIndex)).value));
        var SHLetashlum = new Date(Number(sCardDate.substr(6, 4)), Number(sCardDate.substr(3, 2)) - 1, Number(sCardDate.substr(0, 2)), sSHLetashlum.substr(0, 2), sSHLetashlum.substr(sSHLetashlum.length - 2, 2));
        SHLetashlum.setDate(SHLetashlum.getDate() + Number($get("SD_txtDayAddSHL".concat(iIndex)).value));
        var SGLetashlum = new Date(Number(sCardDate.substr(6, 4)), Number(sCardDate.substr(3, 2)) - 1, Number(sCardDate.substr(0, 2)), sSGLetashlum.substr(0, 2), sSGLetashlum.substr(sSGLetashlum.length - 2, 2));
        SGLetashlum.setDate(SGLetashlum.getDate() + Number($get("SD_txtDayAddSGL".concat(iIndex)).value));

        var dSHatchala = Date.UTC(ShatHatchala.getFullYear(), ShatHatchala.getMonth() + 1, ShatHatchala.getDate(), ShatHatchala.getHours(), ShatHatchala.getMinutes(), 0);
        var dShatGmar = Date.UTC(ShatGmar.getFullYear(), ShatGmar.getMonth() + 1, ShatGmar.getDate(), ShatGmar.getHours(), ShatGmar.getMinutes(), 0);
        var dSHLetashlum = Date.UTC(SHLetashlum.getFullYear(), SHLetashlum.getMonth() + 1, SHLetashlum.getDate(), SHLetashlum.getHours(), SHLetashlum.getMinutes(), 0);
        var dSGLetashlum = Date.UTC(SGLetashlum.getFullYear(), SGLetashlum.getMonth() + 1, SGLetashlum.getDate(), SGLetashlum.getHours(), SGLetashlum.getMinutes(), 0);


        if (String(val.id).indexOf("vldSHL") > -1 && ((dSHLetashlum < dSHatchala) || (dSHLetashlum > dSGLetashlum))) {
            val.errormessage = "שעת התחלה לתשלום לא תקינה, יש להקליד שעה בטווח " + sSGLetashlum + " - " + sSHatchala;
            args.IsValid = false;
        }
        if (String(val.id).indexOf("vldSGL") > -1 && ( (dSGLetashlum < dSHLetashlum) || (dSGLetashlum > dShatGmar))) {
            val.errormessage = "שעת גמר לתשלום לא תקינה, יש להקליד שעה בטווח " + sSGmar + " - " + sSHLetashlum;
            args.IsValid = false;
        }
        if (dSHLetashlum > dSGLetashlum) {
            if (args.IsValid) {
                if(String(val.id).indexOf("vldSHL")>-1)
                    val.errormessage = "לא ניתן לעדכן שעת התחלה לתשלום גדולה משעת גמר לתשלום";
                else val.errormessage = "לא ניתן לעדכן שעת גמר לתשלום קטנה משעת התחלה לתשלום";
                args.IsValid = false;
            } else {
                if (String(val.id).indexOf("vldSHL") > -1)
                    val.errormessage = val.errormessage.concat(" לא ניתן לעדכן שעת התחלה לתשלום גדולה משעת גמר לתשלום \r\n");
                else val.errormessage = val.errormessage.concat("לא ניתן לעדכן שעת גמר לתשלום קטנה משעת התחלה לתשלום \r\n");
                args.IsValid = false;
            }
        }   
    }

/**
recieves params. global date mehod. Complexity - Low
@method SetDate
*/
    function SetDate(oDate, sYear, sMonth, sDay, sHour, sMinutes)
    {
        oDate.setFullYear(sYear);
        oDate.setMonth(sMonth);        
        oDate.setDate(sDay);
        oDate.setHours(sHour);
        oDate.setMinutes(sMinutes);
        oDate.setMonth(sMonth);
        return oDate;
    }
/**
recieves params. validations. Complexity - Medium
@method IsSHBigSG
*/
    function IsSHBigSG(val,args)
    {//נבדוק אם שעת ההתחלה קטנה משעת הגמר  
       var iIndex = String(val.id).substr(String(val.id).length - 1, 1);
       var isSidurGrira = ($("#SD_lblSidur".concat(iIndex)).html()==SIDUR_GRIRA);
       var sShatHatchala = $get("SD_txtSH".concat(iIndex)).value; 
       var sShatGmar = $get("SD_txtSG".concat(iIndex));
       if ((sShatGmar.value == '') || (sShatHatchala == ''))
           return true;
       else {
           var sSidurDate;
           var dCardDate = $get("clnDate").value;
           if (((IsShatHatchalaInNextDay(sShatHatchala, isSidurGrira)) || (sShatHatchala == '00:00'))) {
               sSidurDate = $get("SD_lblDate".concat(iIndex)).innerHTML;
           }
           else {
               sSidurDate = dCardDate;
           }
           var AddDay = Number($get("SD_txtDayAdd".concat(iIndex)).value);
           var ShatGmar = new Date();
           var ShatHatchala = new Date();
           var sYear = sSidurDate.substr(sSidurDate.length - 4, 4);
           var sMonth = Number(sSidurDate.substr(3, 2)) - 1;
           var sDay = sSidurDate.substr(0, 2);
           ShatHatchala.setFullYear(sYear);
           ShatHatchala.setMonth(sMonth);
           ShatHatchala.setDate(sDay);
           ShatHatchala.setHours(sShatHatchala.substr(0, 2));
           ShatHatchala.setMinutes(sShatHatchala.substr(sShatHatchala.length - 2, 2));
           sYear = dCardDate.substr(dCardDate.length - 4, 4);
           sMonth = Number(dCardDate.substr(3, 2)) - 1;
           sDay = dCardDate.substr(0, 2);
           ShatGmar.setFullYear(sYear);
           ShatGmar.setMonth(sMonth);
           ShatGmar.setDate(sDay);
           ShatGmar.setHours(sShatGmar.value.substr(0, 2));
           ShatGmar.setMinutes(sShatGmar.value.substr(sShatGmar.value.length - 2, 2));
           if (AddDay == 1) {
               ShatGmar.setDate(ShatGmar.getDate() + 1);
           }
           var dShatHatchala = Date.UTC(ShatHatchala.getFullYear(), ShatHatchala.getMonth() + 1, ShatHatchala.getDate(), ShatHatchala.getHours(), ShatHatchala.getMinutes(), 0);
           var dShatGmar = Date.UTC(ShatGmar.getFullYear(), ShatGmar.getMonth() + 1, ShatGmar.getDate(), ShatGmar.getHours(), ShatGmar.getMinutes(), 0);
           return (dShatHatchala < dShatGmar);
       }
    }
/**
recieves params. validations. Complexity - Medium
@method IsSHGreaterPrvSG
*/
    function IsSHGreaterPrvSG(val,args){
        //SetBtnChanges();
        var bNewSidur=false;
       var iIndex = String(val.id).substr(String(val.id).length - 1, 1);
       var iSidur = $get("SD_lblSidur".concat(iIndex)).innerHTML;
       if (iSidur == "")
           bNewSidur = true; 

       var iPrvIndex = Number(iIndex) - 1;
       var iLoLetashlum = $("#SD_chkLoLetashlum".concat(iIndex))[0].checked;
       var iPrevSidur = $("#SD_lblSidur".concat(iPrvIndex)).html();
       if (iPrevSidur!=null){
           var iPrvLoLetashlum = $("#SD_chkLoLetashlum".concat(iPrvIndex))[0].checked;
           var iPrvSidurCancel = $get("SD_lblSidurCanceled".concat(iPrvIndex)).value;
           
           // if ((iPrevSidur != SIDUR_CONTINUE_NAHAGUT) && (iPrevSidur != SIDUR_CONTINUE_NOT_NAHAGUT) && (iLoLetashlum == false) && (iPrevLoLetashlum==false)){
           if ((iPrevSidur != null) && (iLoLetashlum == false) && (iPrvLoLetashlum == false) && (iPrvSidurCancel != '1') && (!bNewSidur)) {
               var sShatHatchala = $get("SD_txtSH".concat(iIndex));
               var sPrevShatGmar = $get("SD_txtSG".concat(iPrvIndex));
               var sSidurDate = $get("SD_lblDate".concat(iIndex));
               var sPrevSidurDate = $get("clnDate").value;           
               if (sPrevSidurDate != null) {
                   var prvShGmar = new Date();
                   var ShStart = new Date();
                   ShStart.setFullYear(sSidurDate.innerHTML.substr(sSidurDate.innerHTML.length - 4, 4));
                   ShStart.setMonth(Number(sSidurDate.innerHTML.substr(3, 2)) - 1);
                   ShStart.setDate(sSidurDate.innerHTML.substr(0, 2));
                   ShStart.setHours(sShatHatchala.value.substr(0, 2));
                   ShStart.setMinutes(sShatHatchala.value.substr(sShatHatchala.value.length - 2, 2));
                   prvShGmar.setFullYear(sPrevSidurDate.substr(sPrevSidurDate.length - 4, 4));
                   prvShGmar.setMonth(Number(sPrevSidurDate.substr(3, 2)) - 1);
                   prvShGmar.setDate(sPrevSidurDate.substr(0, 2));
                   prvShGmar.setHours(sPrevShatGmar.value.substr(0, 2));
                   prvShGmar.setMinutes(sPrevShatGmar.value.substr(sPrevShatGmar.value.length - 2, 2));
                   if ($get("SD_txtDayAdd".concat(iPrvIndex)).value == "1")
                       prvShGmar.setDate(prvShGmar.getDate() + 1);
                   var dShatHatchala = Date.UTC(ShStart.getFullYear(), ShStart.getMonth() + 1, ShStart.getDate(), ShStart.getHours(), ShStart.getMinutes(), 0);
                   var dPrevShatGmar = Date.UTC(prvShGmar.getFullYear(), prvShGmar.getMonth() + 1, prvShGmar.getDate(), prvShGmar.getHours(), prvShGmar.getMinutes(), 0);
                   return (dShatHatchala >= dPrevShatGmar);
               }
               else { return true; }
           }
           else { return true; }       
           }
           else{ return true;}          
    }
/**
recieves params. validations. Complexity - Medium
@method IsEHourBigSHour
*/
    function IsEHourBigSHour(val,args)
    {//נבדוק ששעת הגמר קטנה משעת ההתחלה של הסידור הבא   
       SetBtnChanges();
       var bNextNewSidur=false;
       var iIndex = String(val.id).substr(String(val.id).length-1,1);
       var iNextIndex = Number(iIndex) + 1;
       var iNextSidur = $get("SD_lblSidur".concat(iNextIndex));
       if (iNextSidur == null) {
           iNextSidur = $("#SD_lblSidur".concat(iNextIndex)).html();
           bNextNewSidur = true;
       } else { iNextSidur=iNextSidur.innerHTML; }
       var iLoLetashlum = $("#SD_chkLoLetashlum".concat(iIndex))[0].checked;
       if (iNextSidur != null) {
           var iNextLoLetashlum = $("#SD_chkLoLetashlum".concat(iNextIndex))[0].checked;
           var iNextSidurCancel = $get("SD_lblSidurCanceled".concat(iNextIndex)).value;                 
           if ((iNextSidur != null) && (iLoLetashlum == false) && (iNextLoLetashlum == false) && (iNextSidurCancel != '1') && (!bNextNewSidur)){
               var sNxtStartH = $get("SD_txtSH".concat(iNextIndex));
               if ((sNxtStartH != null) && (sNxtStartH.value!='')) {
                   var sShatGmar = $get("SD_txtSG".concat(iIndex));
                   var sSidurDate = $get("SD_lblDate".concat(iIndex));
                   var AddDay = $get("SD_txtDayAdd".concat(iIndex)).value;
                   var AddDayNextSidur = $get("SD_txtDayAdd".concat(iNextIndex)).value;
                   var sNextSidurDate = $get("SD_lblDate".concat(iNextIndex));
                   var ShatGmar = new Date();
                   var dNxtStartH = new Date();
                   ShatGmar.setFullYear(sSidurDate.innerHTML.substr(sSidurDate.innerHTML.length - 4, 4));
                   ShatGmar.setMonth(Number(sSidurDate.innerHTML.substr(3, 2)) - 1);
                   ShatGmar.setDate(sSidurDate.innerHTML.substr(0, 2));
                   ShatGmar.setHours(sShatGmar.value.substr(0, 2));
                   ShatGmar.setMinutes(sShatGmar.value.substr(sShatGmar.value.length - 2, 2));
                   if (AddDay == "1")
                       ShatGmar.setDate(ShatGmar.getDate() + 1);
                   dNxtStartH.setFullYear(sNextSidurDate.innerHTML.substr(sNextSidurDate.innerHTML.length - 4, 4));
                   dNxtStartH.setMonth(Number(sNextSidurDate.innerHTML.substr(3, 2)) - 1);
                   dNxtStartH.setDate(sNextSidurDate.innerHTML.substr(0, 2));
                   dNxtStartH.setHours(sNxtStartH.value.substr(0, 2));
                   dNxtStartH.setMinutes(sNxtStartH.value.substr(sNxtStartH.value.length - 2, 2));

                   var dNextShatH = Date.UTC(dNxtStartH.getFullYear(), dNxtStartH.getMonth() + 1, dNxtStartH.getDate(), dNxtStartH.getHours(), dNxtStartH.getMinutes(), 0);
                   var dShatGmar = Date.UTC(ShatGmar.getFullYear(), ShatGmar.getMonth() + 1, ShatGmar.getDate(), ShatGmar.getHours(), ShatGmar.getMinutes(), 0);
                   return (dShatGmar <= dNextShatH);
               }
               else {
                   return true;
               }
           }
           else {
               return true;
           }   
          }
           else{
             return true;
           }        
    }
    function IsSidurMyuhad(sMisparSidur){//נבדוק אם סידור הוא רגיל או מיוחד    
        if (String(sMisparSidur).length > 1)
            return (sMisparSidur.substr(0, 2) == "99");
        else{ return false;}        
    }

/**
recieves params. open ModalDialog  . Complexity - Low
@method AddHosafatKnisot
*/
    function AddHosafatKnisot(iSidurIndx, iPeilutIndx) {
      
        _bScreenChanged = bScreenChanged;
        if ($get(iPeilutIndx.id).cells[14].childNodes[0].className == 'ImgCheckedPeilut') {
            if (_bScreenChanged) {
                if (!ChkCardVld())
                    return false;
                $("#hidSave")[0].value = "1";          
                __doPostBack('btnConfirm', '');
            }
            var ReturnWin;
            var id = $("#txtId").val();
            var SidurId = $("#SD_lblSidur".concat(iSidurIndx)).html();
            if (SidurId == '')
                SidurId = $("#SD_lblSidur".concat(iSidurIndx))[0].value;
            var CardDate = $get("clnDate").value;
            var SidurDate = $("#SD_lblDate".concat(iSidurIndx)).html();
            var iAddDay = $get(iPeilutIndx.id).cells[_COL_DAY_TO_ADD].childNodes[0].value;
            var SidurHour = $("#SD_txtSH".concat(iSidurIndx)).val();
            if (SidurHour == '')
                SidurDate = '01/01/0001';
           
            var dPeilutDate = new Date();
            dPeilutDate.setFullYear(CardDate.substr(6, 4));

            dPeilutDate.setMonth((Number(CardDate.substr(3, 2)) - 1).toString());
            dPeilutDate.setDate(CardDate.substr(0, 2));
            dPeilutDate.setDate(dPeilutDate.getDate() + Number(iAddDay));

            var ShatYetzia = $get(iPeilutIndx.id).cells[_COL_SHAT_YETIZA].childNodes[0].value;
            var MakatNesia = $get(iPeilutIndx.id).cells[_COL_MAKAT].childNodes[0].value;
            var OtoNo = $get(iPeilutIndx.id).cells[_COL_CAR_NUMBER].childNodes[0].value;
            var LicenseNumber = $get(iPeilutIndx.id).cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].value;
            var sQueryString = "?EmpID=" + id + "&SidurID=" + SidurId + "&CardDate=" + CardDate + "&SidurDate=" + SidurDate + "&SidurHour=" + SidurHour + "&ShatYetzia=" + GetDateDDMMYYYY(dPeilutDate).concat(" " + ShatYetzia) + "&MakatNesia=" + MakatNesia + "&OtoNo=" + OtoNo +"&LicenseNumber="+LicenseNumber+ "&dt=" + Date();
            $get("divHourglass").style.display = 'block';
            res = window.showModalDialog('HosafatKnisot.aspx' + sQueryString, window, 'dialogwidth:617px;dialogheight:400px;dialogtop:280px;dialogleft:340px;status:no;resizable:no;');
            $get("divHourglass").style.display = 'none';
            if ((_bScreenChanged) || ((res != undefined) && (res != '') && (!_bScreenChanged))) {
                $get("hidExecInputChg").value = "1";
                bScreenChanged = false;
                RefreshBtn();
                __doPostBack('btnRefreshOvedDetails', '');
                if (_bScreenChanged) {
                    disableUpdateBtn(false);
                }              
            }
            return res;    
        }           
    }
/**
recieves params.  validations .__doPostBack. Complexity - Low
@method AddSadotLsidur
*/
    function AddSadotLsidur(iIndex) {
        _bScreenChanged = bScreenChanged; //bScreenChanged משתנה גלובלי שמתעדכן ב- chkCardVld
        if (bScreenChanged){
            if (!ChkCardVld())
                return false;
            var SidurNumber = $get("SD_lblSidur".concat(iIndex)).innerHTML;
            var SidurDate = $get("SD_lblDate".concat(iIndex)).innerHTML;
            var SidurSHour = $get("SD_txtSH".concat(iIndex)).value;
            $("#hidSave")[0].value = "1";
            $get("hidSadotLSidur").value = ("1,".concat(SidurDate + " " + SidurSHour)).concat("," + SidurNumber);            
            __doPostBack('btnConfirm', '');
        } else {
            res = ExecSadotLsidur(iIndex, _bScreenChanged);
            return res;
        }
    }

/**
recieves params.  open ModalDialog .__doPostBack. Complexity - Medium
@method ExecSadotLsidur
*/
    function ExecSadotLsidur(iIndex, bScreenWasChg) {
        var dSidurSGDate = new Date();
        var id = $get("txtId").value;
        var CardDate = $get("clnDate").value;
        var SidurDate = $get("SD_lblDate".concat(iIndex)).innerHTML;
        var SidurSHour = $get("SD_txtSH".concat(iIndex)).value;
        var SidurEHour = $get("SD_txtSG".concat(iIndex)).value;
        var iSDayToAdd = $get("SD_txtDayAdd".concat(iIndex)).value;

        $get("hidSadotLSidur").value = "";
        SetDate(dSidurSGDate, Number(CardDate.substr(6, 4)), Number(CardDate.substr(3, 2)) - 1, Number(CardDate.substr(0, 2)), "0", "0");
        dSidurSGDate.setDate(dSidurSGDate.getDate() + Number(iSDayToAdd));
        var SidurId = $get("SD_lblSidur".concat(iIndex)).innerHTML;
        if (SidurSHour == '')
            SidurDate = '01/01/0001';

        $get("divHourglass").style.display = 'block';
        var sQuryString = "?EmpID=" + id + "&CardDate=" + CardDate + "&SidurID=" + SidurId + "&ShatHatchala=" + SidurDate + ' ' + SidurSHour + "&ShatGmar=" + SidurEHour + "&ShatGmarDate=" + GetDateDDMMYYYY(dSidurSGDate) + "&SidurDate=" + SidurDate + "&dt=" + Date();
        $get("divHourglass").style.display = 'none';        
        var res = window.showModalDialog('SadotNosafimLeSidur.aspx' + sQuryString, window, "dialogwidth:650px;dialogheight:350px;dialogtop:210px;dialogleft:220px;status:no;resizable:no;scroll:no");
        if ((bScreenWasChg) || ((res != undefined) && (res != '') && (!bScreenWasChg))) {
            $get("hidExecInputChg").value = "1";
            bScreenChanged = false;
            RefreshBtn();
            var oSh = $get("SD_txtSH".concat(iIndex));
            if (!(oSh.disabled))
                oSh.disabled = true;

            $get("divHourglass").style.display = 'block';
            __doPostBack('btnRefreshOvedDetails', '');            
        }
        return res;
    }
/**
recieves params.  open ModalDialog . Complexity - Low
@method FixSidurHeadrut
*/
    function FixSidurHeadrut(iIndex){         
        var sQueryString;
        sQueryString = "?dt=" + Date();
        sQueryString = sQueryString + "&MisparIshi=" + $get("txtId").value;
        sQueryString = sQueryString + "&DateCard=" + $get("SD_lblDate".concat(iIndex)).innerHTML;
        sQueryString = sQueryString + "&MisparSidur=" + $get("SD_lblSidur".concat(iIndex)).innerHTML;
        sQueryString = sQueryString + "&TimeStart=" + $get("SD_txtSH".concat(iIndex)).value;
        sQueryString = sQueryString + "&TimeEnd=" + $get("SD_txtSG".concat(iIndex)).value;  
        window.showModalDialog('DivuachHeadrut.aspx?' + sQueryString, '', 'dialogwidth:555px;dialogheight:390px;dialogtop:150px;dialogleft:480px;status:no;resizable:yes;');
    }
/**
recieves params.updates UI. Complexity - Low
@method ChgImg
*/
    function ChgImg(iInx){
        var img = $get("SD_cImgS".concat(iInx));
        if ((String(img.nameProp).indexOf("closeArrow.png")) > -1) {
            img.src = "../../images/openArrow.png";
        return;
       }
    if ((String(img.nameProp).indexOf("closeArrow_red.png")) > -1) {
        img.src = "../../images/openArrow_red.png";
           return;
       } 
     
       if ((String(img.nameProp).indexOf("green_down_2_big.jpg"))>-1){
            img.src= "../../images/green_up_2_big.jpg";
            return;
       }

        if ((String(img.nameProp).indexOf("openArrow.png")) > -1) {
           img.src = "../../images/closeArrow.png";
         return;
       }

     if ((String(img.nameProp).indexOf("openArrow_red.png")) > -1) {
           img.src = "../../images/closeArrow_red.png";
         return;
       }
          
       if ((String(img.nameProp).indexOf("green_up_2_big.jpg"))>-1){
         img.src= "../../images/green_down_2_big.jpg";
         return;
       }       
    }
/**
recieves params. Complexity - Low
@method SetDayToAdd
*/
    function SetDayToAdd(iInx)
    {
      var _Add = $get("SD_txtDayAdd".concat(iInx));
      var _ShatGmar = $get("SD_txtSG".concat(iInx));
      var dSdDate = $get("SD_lblDate".concat(iInx)).innerHTML;   
      var dCardDate = $get("clnDate").value; 
      if (IsShatGmarInNextDay(_ShatGmar.value) || (_ShatGmar.value=='00:00'))            
            _Add.value = "1";       
          else      
            _Add.value = "0";
    }
/**
recieves params.updates UI. Complexity - High
@method SetDay
*/
    function SetDay(iInx) {
    
      $find("pBehvDate").hide();
      var sHour;    
      var sParamNxtDay;
      var dParamDate = new Date();
      var dItemDate = new Date();
      var arrItems = iInx.split("|");
      var sCardDate = $get("clnDate").value;
      var bRaiseNextDay = false;

      switch (arrItems[0]) {
          case "1":
              if (GetKeyPressPosition($get("SD_txtSG" + arrItems[1])) == 5) { 
                  bRaiseNextDay = true;
                  sHour = $get("SD_txtSG" + arrItems[1]).value;
                  var dCardDate = new Date(Number(sCardDate.substr(6, 4)), Number(sCardDate.substr(3, 2)) - 1, Number(sCardDate.substr(0, 2)), 0, 0);
                  var dSidurTime = new Date(Number(sCardDate.substr(6, 4)), Number(sCardDate.substr(3, 2)) - 1, Number(sCardDate.substr(0, 2)), 0, 0);
                  var _Add = $get("SD_txtDayAdd".concat(arrItems[1])).value;
                  if (sHour == '00:00') {
                      _Add = '1'
                      $get("SD_txtDayAdd".concat(arrItems[1])).value = '1';
                  }
                  dSidurTime.setDate(dSidurTime.getDate() + Number(_Add));
                  $get("SD_txtSG".concat(arrItems[1])).title = "תאריך גמר הסידור הוא: " + GetDateDDMMYYYY(dSidurTime);
              }
              break;
          case "2":
              if (GetKeyPressPosition($get(arrItems[1]).cells[_COL_SHAT_YETIZA].childNodes[0]) == 5) {
                  bRaiseNextDay = true;
                  sHour = $get(arrItems[1]).cells[_COL_SHAT_YETIZA].childNodes[0].value;
                  sParamNxtDay = $get("SD_hidParam242").value;
                  var sYear = sCardDate.substr(sCardDate.length - 4, 4);
                  var sMonth = Number(sCardDate.substr(3, 2)) - 1;
                  var sDay = sCardDate.substr(0, 2);
                  var sSidurSG = $get("SD_txtSG" + arrItems[2]).value;
                  SetDate(dParamDate, Number(sYear), Number(sMonth), Number(sDay), Number(sParamNxtDay.substr(0, 2)), Number(sParamNxtDay.substr(3, 2)));
                  SetDate(dItemDate, Number(sYear), Number(sMonth), Number(sDay), Number(sSidurSG.substr(0, 2)), Number(sSidurSG.substr(3, 2)));
              }
              break;
          case "4":
         
              if (GetKeyPressPosition($get("SD_txtSHL" + arrItems[1])) == 5) {
                  $get("SD_txtDayAddSHL".concat(arrItems[1])).value = '0';
                  var isSidurGrira = ($("#SD_lblSidur".concat(arrItems[1])).html() == SIDUR_GRIRA);
                  sHour = $get("SD_txtSHL" + arrItems[1]).value;
                  if (isSidurGrira && (CheckShaaInParam(sHour, "276") || sHour == '00:00')) {
                      bRaiseNextDay = true;
                    //  var dCardDate = new Date(Number(sCardDate.substr(6, 4)), Number(sCardDate.substr(3, 2)) - 1, Number(sCardDate.substr(0, 2)), 0, 0);
                  }
                  var dSidurTime = new Date(Number(sCardDate.substr(6, 4)), Number(sCardDate.substr(3, 2)) - 1, Number(sCardDate.substr(0, 2)), 0, 0);
                //  var _Add = $get("SD_txtDayAddSHL".concat(arrItems[1])).value;
                //  dSidurTime.setDate(dSidurTime.getDate() + Number(_Add));
                  $get("SD_txtSHL".concat(arrItems[1])).title = "תאריך התחלה לתשלום הוא: " + GetDateDDMMYYYY(dSidurTime);

              }
              break;
          case "5":
              if (GetKeyPressPosition($get("SD_txtSGL" + arrItems[1])) == 5) {
              
                  $get("SD_txtDayAddSGL".concat(arrItems[1])).value = '0';
                  bRaiseNextDay = true;
                  sHour = $get("SD_txtSGL" + arrItems[1]).value;
                //  var dCardDate = new Date(Number(sCardDate.substr(6, 4)), Number(sCardDate.substr(3, 2)) - 1, Number(sCardDate.substr(0, 2)), 0, 0);
                  var dSidurTime = new Date(Number(sCardDate.substr(6, 4)), Number(sCardDate.substr(3, 2)) - 1, Number(sCardDate.substr(0, 2)), 0, 0);
                  var _Add = 0;//$get("SD_txtDayAddSGL".concat(arrItems[1])).value;
                  if (sHour == '00:00') {
                      _Add = '1'
                      $get("SD_txtDayAddSGL".concat(arrItems[1])).value = '1';
                  }
                  dSidurTime.setDate(dSidurTime.getDate() + Number(_Add));
                  $get("SD_txtSGL".concat(arrItems[1])).title = "תאריך גמר לתשלום הוא: " + GetDateDDMMYYYY(dSidurTime);
              }
              break;
      }   
      if (bRaiseNextDay) {
          if (IsShatGmarInNextDay(sHour) || arrItems[0] =="4") {
              $get("SD_hidCurrIndx").value = iInx;

              switch (arrItems[0]) {
                  case "1":
                      $get("SD_btnShowMessage").click();
                      break;
                  case "2":

                      if (($get("SD_txtDayAdd".concat(arrItems[2])).value == "1") || (dItemDate > dParamDate))
                          $get("SD_btnShowMessage").click();
                      else
                          //נבדוק ששעת גמר לא השתנתה, אם שעת גמר של הסידור היא של היום הנוכחי נשנה גם את התאריך של שעת היציאה - שינוי שעת יציאה
                      {
                          if ($get("SD_txtDayAdd".concat(arrItems[2])).value == "0") {
                              $get(arrItems[1]).cells[_COL_DAY_TO_ADD].childNodes[0].value = "0";
                          }
                      }
                      break;
                  case "4": //שעת התחלה לתשלום
                      $get("SD_btnShowMessage").click();
                      break;
                  case "5"://שעת גמר לתשלום
                      $get("SD_btnShowMessage").click();
                      break;
              }
          }
          else
              if (arrItems[0] == '2')//יציאה    
              {
                  var iAdd;
                  if (sHour == '00:00')
                      iAdd = 1;
                  else
                      iAdd = 0;

                  dItemDate.setDate(dItemDate.getDate() + iAdd);
                  $get(arrItems[1]).cells[_COL_DAY_TO_ADD].childNodes[0].value = iAdd;
                  $get(arrItems[1]).cells[_COL_SHAT_YETIZA].childNodes[0].title = "תאריך שעת היציאה הוא: " + GetDateDDMMYYYY(dItemDate);
              }
         }   
    }

/**
recieves params.validations. updates UI. Complexity - High
@method btnDay_click
*/
    function btnDay_click(iDayToAdd) {
        
        $find("pBehvDate").hide();
        var iIndx = $get("SD_hidCurrIndx").value;   
        var arrItems = iIndx.split("|"); 
        var sSdDate;
        var dSdDate = new Date();
        var sCardDate = $get("clnDate").value;
        var dCardDate = new Date(Number(sCardDate.substr(6, 4)), Number(sCardDate.substr(3, 2)) - 1, Number(sCardDate.substr(0, 2)), 0, 0);

        sSdDate = sCardDate;

        var sYear=sSdDate.substr(sSdDate.length-4,4);
        var sMonth=Number(sSdDate.substr(3,2))-1;
        var sDay = sSdDate.substr(0, 2);
        SetDate(dSdDate, sYear, sMonth, sDay, '0', '0');         
        dSdDate.setDate(dSdDate.getDate() + Number(iDayToAdd));   
         
        switch (arrItems[0]) {
            case "1":
                $get("SD_txtDayAdd" + arrItems[1]).value = Number(iDayToAdd);
                $get("SD_txtSG" + arrItems[1]).title = "תאריך גמר הסידור הוא: " + GetDateDDMMYYYY(dSdDate);
                break;
            case "2":
                $get(arrItems[1]).cells[_COL_DAY_TO_ADD].childNodes[0].value = Number(iDayToAdd);
                $get(arrItems[1]).cells[_COL_SHAT_YETIZA].childNodes[0].title = "תאריך שעת היציאה הוא: " + GetDateDDMMYYYY(dSdDate);
                //נשנה גם לכל הכניסות
                ChangeKnisotHour($get(arrItems[1]), iDayToAdd, dSdDate);
                break;
            case "3":
                var _SHNew = $get("SD_txtSH".concat(arrItems[1]));
                sCardDate = $get("clnDate").value;
                var iSidurKey = $get("SD_lblSidur".concat(arrItems[1])).innerHTML;
                if (iSidurKey == '')
                    iSidurKey = $get("SD_lblSidur".concat(arrItems[1])).value;

                _SHNew.title = "תאריך התחלת הסידור הוא: " + GetDateDDMMYYYY(dSdDate);
                $get("SD_lblDate".concat(arrItems[1])).innerHTML = GetDateDDMMYYYY(dSdDate);
                wsGeneral.UpdateSidurDate(sCardDate, iSidurKey, _SHNew.getAttribute('OrgShatHatchala'), _SHNew.value, Number(iDayToAdd), arrItems[1]);

                break;
            case "4":
                $get("SD_txtDayAddSHL" + arrItems[1]).value = Number(iDayToAdd);
                $get("SD_txtSHL" + arrItems[1]).title = "תאריך התחלה לתשלום הוא: " + GetDateDDMMYYYY(dSdDate);
                break;
            case "5":
                $get("SD_txtDayAddSGL" + arrItems[1]).value = Number(iDayToAdd);
                $get("SD_txtSGL" + arrItems[1]).title = "תאריך גמר לתשלום הוא: " + GetDateDDMMYYYY(dSdDate);
                break;
        }


        if ((arrItems[0] == '1') || (arrItems[0] == '3')) {//שעת התחלה וגמר  
            //נחשב את משך הסידור בדקות             
            iSDayToAdd = $get("SD_txtDayAdd".concat(arrItems[1])).value;
            var _SH = $get("SD_txtSH".concat(arrItems[1])).value;
            var _SG = $get("SD_txtSG" + arrItems[1]).value;
            var sSidurDate = $get("SD_lblDate".concat(arrItems[1])).innerHTML; 
            var dStartHour = new Date(Number(sSidurDate.substr(6, 4)), Number(sSidurDate.substr(3, 2)) - 1, Number(sSidurDate.substr(0, 2)), Number(_SH.substr(0, 2)), Number(_SH.substr(3, 2)));
            var dEndHour = new Date(Number(sCardDate.substr(6, 4)), Number(sCardDate.substr(3, 2)) - 1, Number(sCardDate.substr(0, 2)), Number(_SG.substr(0, 2)), Number(_SG.substr(3, 2)));
            dEndHour.setDate(dEndHour.getDate() + Number(iSDayToAdd));
            $get("SD_lblSidur".concat(arrItems[1])).title = "  משך הסידור: " + GetTimeInMinuts(dStartHour, dEndHour) + " דקות";  
            ValidatorEnable($get('SD_vldSG'.concat(arrItems[1])), true);
            ValidatorEnable($get('SD_vldSHatchala'.concat(arrItems[1])), true);
        }        
    }
/**
recieves params. updates UI. Complexity - Medium
@method SidurTimeChanged
*/
   function SidurTimeChanged(id)
   { //שעת התחלה השתנתה, נבדוק שוב את התנאים לשדה השלמה
       var sSidurDate = $get("SD_lblDate".concat(id)).innerHTML;
       var sCardDate = $get("clnDate").value;
    var ddlChariga = $get("SD_ddlException".concat(id));          
    var sYear = Number(sSidurDate.substr(6,4));
    var sMonth = Number(sSidurDate.substr(3,2))-1;
    var sDay = Number(sSidurDate.substr(0,2));
    var dSH = new Date();
    var dSG = new Date();
    var dSHL = new Date();
    var dSGL = new Date();
    var oSH = $get("SD_txtSH".concat(id)).value;
    var oSG = $get("SD_txtSG".concat(id)).value;
    var iSDayToAdd;
    if ($get("SD_txtSHL".concat(id)) != null) {
        var oSHL = $get("SD_txtSHL".concat(id)).value;
        var oSGL = $get("SD_txtSGL".concat(id)).value;
        var oZmanChariga = $get("SD_hidParam41").value;
        iSDayToAdd = $get("SD_txtDayAdd".concat(id)).value;
        var bRes = false;

        if ((oSH == '__:__') || (oSG == '__:__')) {
            $get("SD_ddlHashlama".concat(id)).disabled = true;
            ddlChariga.disabled = true;
        }
        var iCharigaType = Number(ddlChariga.getAttribute("ChrigaType"));
        if (iCharigaType != 4) {// 4- חריגה בגלל סיבת לא לתשלום
            SetDate(dSH, Number(sSidurDate.substr(6, 4)), Number(sSidurDate.substr(3, 2)) - 1, Number(sSidurDate.substr(0, 2)), Number(oSH.substr(0, 2)), Number(oSH.substr(3, 2)));
           // dSH.setDate(dSH.getDate() + Number(iSDayToAdd));
            SetDate(dSG, Number(sCardDate.substr(6, 4)), Number(sCardDate.substr(3, 2)) - 1, Number(sCardDate.substr(0, 2)), Number(oSG.substr(0, 2)), Number(oSG.substr(3, 2)));
            dSG.setDate(dSG.getDate() + Number($get("SD_txtDayAdd".concat(id)).value));
            SetDate(dSHL, Number(sCardDate.substr(6, 4)), Number(sCardDate.substr(3, 2)) - 1, Number(sCardDate.substr(0, 2)), Number(oSHL.substr(0, 2)), Number(oSHL.substr(3, 2)));
            dSHL.setDate(dSHL.getDate() + Number($get("SD_txtDayAddSHL".concat(id)).value)) ;//Number(iSDayToAdd));
            SetDate(dSGL, Number(sCardDate.substr(6, 4)), Number(sCardDate.substr(3, 2)) - 1, Number(sCardDate.substr(0, 2)), Number(oSGL.substr(0, 2)), Number(oSGL.substr(3, 2)));
            dSGL.setDate(dSGL.getDate() + Number($get("SD_txtDayAddSGL".concat(id)).value));//Number(iSDayToAdd));
            var dUTCsh = Date.UTC(dSH.getFullYear(), dSH.getMonth() + 1, dSH.getDate(), dSH.getHours(), dSH.getMinutes(), 0);
            var dUTCsg = Date.UTC(dSG.getFullYear(), dSG.getMonth() + 1, dSG.getDate(), dSG.getHours(), dSG.getMinutes(), 0);
            var dUTCshl = Date.UTC(dSHL.getFullYear(), dSHL.getMonth() + 1, dSHL.getDate(), dSHL.getHours(), dSHL.getMinutes(), 0);
            var dUTCsgl = Date.UTC(dSGL.getFullYear(), dSGL.getMonth() + 1, dSGL.getDate(), dSGL.getHours(), dSGL.getMinutes(), 0);
            var diff = new Date();
            var timediff = new Date();
            if (dUTCsh < dUTCshl) {
                diff.setTime(Math.abs(dSHL.getTime() - dSH.getTime()));
                timediff = diff.getTime();
                bRes = (((Math.floor(timediff / (1000 * 60))) >= oZmanChariga));
                if (bRes) {
                    ddlChariga.setAttribute("ChrigaType", "1"); //חריגה בשעת התחלה
                }
            }
            if (dUTCsg > dUTCsgl) {
                diff.setTime(Math.abs(dSG.getTime() - dSGL.getTime()));
                timediff = diff.getTime();
                if (bRes) {
                    if (((Math.floor(timediff / (1000 * 60))) >= oZmanChariga)) {
                        ddlChariga.setAttribute("ChrigaType", "3"); //  ובשעת גמר חריגה בשעת התחלה
                    }
                }
                else {
                    if (((Math.floor(timediff / (1000 * 60))) >= oZmanChariga)) {
                        ddlChariga.setAttribute("ChrigaType", "2"); //חריגה משעת גמר
                        bRes = true;
                    }
                }
            }
            ddlChariga.disabled = (!bRes);
        }
        if ((oSH != '') && (oSG != '')) {
            var sCardDate = $get("clnDate").value;  
            iSDayToAdd = $get("SD_txtDayAdd".concat(id)).value;
            var dStartHour = new Date(Number(sSidurDate.substr(6, 4)), Number(sSidurDate.substr(3, 2)) - 1, Number(sSidurDate.substr(0, 2)), Number(oSH.substr(0, 2)), Number(oSH.substr(3, 2)));
            var dEndHour = new Date(Number(sCardDate.substr(6, 4)), Number(sCardDate.substr(3, 2)) - 1, Number(sCardDate.substr(0, 2)), Number(oSG.substr(0, 2)), Number(oSG.substr(3, 2)));
           
            dEndHour.setDate(dEndHour.getDate() + Number(iSDayToAdd));
            $get("SD_lblSidur".concat(id)).title = "  משך הסידור: " + GetTimeInMinuts(dStartHour, dEndHour) + " דקות";
        } 
    } 
   }
/**
 Complexity - Low
@method GetSidurTime
*/
   function GetSidurTime(dStartHour,dEndHour)
   {
     var diff= new Date();     
     var timediff = new Date();
     //חישוב הפרש השעות בין שעת גמר הסידור לשעת התחלת הסידור
     diff.setTime(Math.abs(dStartHour.getTime() - dEndHour.getTime()));
     timediff = diff.getTime();     
     return (Math.floor(timediff / (1000 * 60 * 60)));     
   }
/**
 Complexity - Low
@method SortSidurim
*/
   function SortSidurim(){
     var i=0,sSH,sPrvSH;
     sSH=$get("SD_txtSH".concat(i));        
     sPrvSH=sSH;
     while (sSH!=null)
     {                        
        if ((sPrvSH.value>sSH.value))
        {
           $get("btnRefreshOvedDetails").click();
           break;
        }  
        sPrvSH=$get("SD_txtSH".concat(i));           
        i=i+1;
        sSH=$get("SD_txtSH".concat(i));       
     }    
   }
/**
 Complexity - Low
@method chkPartHour
*/
   function chkPartHour(val,args)
   {
   var sTime = args.Value;
   if (sTime=="00:00"){   
    args.IsValid=false;
   }
   else  
        if (!(IsValidTime(sTime)))
           args.IsValid=false;
  
   }
/**
 Complexity - Medium
@method chkPartHour
*/
   function HasSidurHashlama() {     
     var _HashlamaDLL, _Sidur;   
     var i=0;
     var j=0;
     var iNdex=0;
     var arrIndx = new Array();

     _Sidur = $get("SD_lblSidur" + i);
     _HashlamaDLL = $get("SD_ddlHashlama" + i);
     while ((_Sidur!=null)){
         if(_HashlamaDLL!=null){
             if (_HashlamaDLL.value > 0){
                arrIndx[iNdex] = i;//מספר הסידורים עם השלמה
                iNdex = ++iNdex;             
             } 
         }
         i=++i;
         _Sidur = $get("SD_lblSidur" + i);
         _HashlamaDLL = $get("SD_ddlHashlama" + i); 
     }
     if ((arrIndx.length > 0) && (arrIndx.length >= Number($get("SD_hidNumOfHashlama").value))) {        
         var sIndex = arrIndx.join(",");
         _Sidur = $get("SD_lblSidur" + j);
         _HashlamaDLL = $get("SD_ddlHashlama" + j);
         while (_Sidur!=null){            
             if (sIndex.toString().indexOf(j)==-1){
                if (_HashlamaDLL!=null)
                    _HashlamaDLL.disabled = true;
             }         
             j=++j;
             _Sidur = $get("SD_lblSidur" + j);
             _HashlamaDLL = $get("SD_ddlHashlama" + j); 
        }
     }
   }

/**
 updates UI.Complexity - Low
@method SetSidurimCollapseImg
*/

   function SetSidurimCollapseImg(){
     var _Sidur, _Img, _Peilut, stat;
     var i=0;
     
     _Sidur = $get("SD_lblSidur" + i);
     while (_Sidur != null) {
         if ($get("hidDriver").value == "1") //אם עמדת נהג נשים פוקוס על שעת התחלה של הסידור  הראשון            
             if (i == 0)
                 if ($get("SD_tbSidurim").disabled==false)
                    $get("SD_tbSidurim").focus();


        _Img = $get("SD_cImgS" + i);
        if (_Img!=null){
            _Peilut = $get("SD_" + padLeft(i,'0',3));
            if (_Peilut!=null){           
                for (var j=1; j<_Peilut.firstChild.childNodes.length; j++)
                { 
                    stat=_Peilut.firstChild.childNodes[j].cells[_COL_PEILUT_STATUS].firstChild.value;
                    switch (stat)
                    {
                        case "1":
                            _Img.src = "../../images/openArrow_red.png";
                            break;
                        case "2":
                            _Img.src = "../../images/green_up_2_big.jpg";
                            break;  
                    }
                }
            }
        }       
        i=++i;
        _Sidur = $get("SD_lblSidur" + i);
     }
   }

/**
recieves params. updates UI. Complexity - High
@method EnabledSidurimListBtn
*/
   function EnabledSidurimListBtn(bDisabled, bCloseAllBtn) {
     var _Sidur, _ImgPeilut,_imgCancel,_imgCancelPeilut;
     var i=0;
     var dId = $get("txtId").value;
     var GoremMEasger = $get("hidGoremMeasher").value;

     _Sidur = $get("SD_lblSidur" + i);
     while  (_Sidur!=null){
        _ImgPeilut = $get("SD_imgAddPeilut" + i);
        if (_ImgPeilut!=null){ 
            if (!_ImgPeilut.disabled){           
                _ImgPeilut.disabled = bDisabled;
            }           
            if (_ImgPeilut.disabled){
                _ImgPeilut.src = "../../images/Plus_Dis.png";
            }
            else{
                _ImgPeilut.src =  "../../images/plus.png";
            }
        }       
        _imgCancel= $get("SD_imgCancel" + i);
        if (_imgCancel!=null){ 
            if (!_imgCancel.disabled){
              _imgCancel.disabled = bDisabled;}           
            
            if (_imgCancel.disabled)
               if ((String(_imgCancel.className).indexOf("ImgChecked"))>-1) 
                     _imgCancel.className =  "ImgCheckedDisable";
               else      
                    _imgCancel.className =  "ImgCancelDisable";          
        } 
         _Peilut = $get("SD_" + padLeft(i,'0',3));
         if (_Peilut!=null){           
          for (var j=1; j<_Peilut.firstChild.childNodes.length; j++)
           {
               _imgCancelPeilut = _Peilut.firstChild.childNodes[j].cells[_COL_CANCEL];
               _imgAddNesiaReka = _Peilut.firstChild.childNodes[j].cells[_COL_ADD_NESIA_REKA];
               if (_imgAddNesiaReka.childNodes[0].disabled != undefined) {
                   if (!_imgAddNesiaReka.childNodes[0].disabled)
                       _imgAddNesiaReka.childNodes[0].disabled = bDisabled;
                   if (_imgAddNesiaReka.childNodes[0].disabled)
                       _imgAddNesiaReka.childNodes[0].src = "../../images/down_Dis.png";                                 
               }
               //reka up
               _imgAddNesiaRekaUp = _Peilut.firstChild.childNodes[j].cells[_COL_ADD_NESIA_REKA_UP];
               if ((!bCloseAllBtn) && (((((($get("hidMeasherMistayeg").value != '-1')
                    && ($get("hidDriver").value != '1'))
                    && ((GoremMEasger != dId))))) && (i == 0)))
               //אם סידור ראשון, נאפשר תמיד הוספת ריקה ממפה
               {
                   if (!_imgAddNesiaRekaUp.childNodes[0].disabled) {
                       _imgAddNesiaRekaUp.childNodes[0].disabled = false;
                       _imgAddNesiaRekaUp.childNodes[0].src = "../../images/up.png";
                   }
                   if (_imgAddNesiaRekaUp.childNodes[0].disabled)
                       _imgAddNesiaRekaUp.childNodes[0].src = "../../images/up_Dis.png";
               }
               else {
                   if (_imgAddNesiaRekaUp.childNodes[0].disabled != undefined) {
                       if (bCloseAllBtn) {
                           _imgAddNesiaRekaUp.childNodes[0].disabled = true;
                           _imgAddNesiaRekaUp.childNodes[0].src = "../../images/up_Dis.png";
                       }
                       else {
                           if (!_imgAddNesiaRekaUp.childNodes[0].disabled)
                               _imgAddNesiaRekaUp.childNodes[0].disabled = bDisabled;
                           if (_imgAddNesiaRekaUp.childNodes[0].disabled)
                               _imgAddNesiaRekaUp.childNodes[0].src = "../../images/up_Dis.png";
                       }
                   }
               }
               if (_imgCancelPeilut.firstChild.disabled!=undefined)
               {
               if (!_imgCancelPeilut.firstChild.disabled){
                   _imgCancelPeilut.firstChild.disabled = bDisabled;                  
                  }  
               arrKnisa = (_Peilut.firstChild.childNodes[j]).cells[_COL_KNISA].childNodes[0].nodeValue.split(',');
               if (_imgCancelPeilut.firstChild.disabled){
                 if(_imgCancelPeilut.firstChild.className!=undefined){
                     if ((String(_imgCancelPeilut.firstChild.className).indexOf("ImgCheckedPeilut")) > -1) {
                         if ((KnisaLefiZorech(_Peilut.firstChild.childNodes[j].cells[_COL_LINE_DESCRIPTION].innerHTML,
                                                parseInt(_Peilut.firstChild.childNodes[j].cells[_COL_KNISA].innerHTML.split(',')[0])))
                          && (_Peilut.firstChild.childNodes[j].cells[_COL_ACTUAL_MINUTES].children[0].getAttribute("idkunRashemet") != 'True')
                          &&  (Number(arrKnisa[0]) > 0)) //אם כניסה לפי צורך)         
                             _imgCancelPeilut.firstChild.disabled = false;
                            else
                             _imgCancelPeilut.firstChild.className = "ImgCheckedDisablePeilut";
                     }
                        else {
                            var dId = $get("txtId").value;
                            var GoremMEasger = $get("hidGoremMeasher").value;
                            if (($get("hidMeasherMistayeg").value != '-1') || ($get("hidDriver").value == '1') || (dId == GoremMEasger)) {//אם הכרטיס הוא עם התייחסות או שאנחנו מעמדת נהג או עובד רגיל
                             if (_imgCancelPeilut.firstChild.className == "ImgKnisaS"){
                                 if ((_Peilut.firstChild.childNodes[j].cells[_COL_ACTUAL_MINUTES].children[0].getAttribute("idkunRashemet") != 'True') && (!bCloseAllBtn))
                                     _imgCancelPeilut.firstChild.disabled = false;
                                   else
                                       _imgCancelPeilut.firstChild.className = "ImgKnisaSDisable";
                             }
                             else
                                 _imgCancelPeilut.firstChild.className = "ImgCancelDisable";
                         }
                         else {//ללא התייחסות
                             if ((_imgCancelPeilut.firstChild.className == "ImgKnisaS") && (_Peilut.firstChild.childNodes[j].cells[_COL_ACTUAL_MINUTES].children[0].getAttribute("idkunRashemet") != 'True'))
                                 _imgCancelPeilut.firstChild.className = "ImgKnisaSDisable";
                         }
                       }
                    }
                 }
              }
           }                    
         }     
        i=++i;
        _Sidur = $get("SD_lblSidur" + i);
       
     }

    //נאפשר הוספת ריקה ממפה במקרים הבאים 
    //סידור אחרון בפעילות האחרונה שיש חץ למטה
    //במידה והגענו מעמדת נהג(גם אם הכרטיס הוא ללא התייחסות), או
    //שלא הגענו מעמדת נהג, אבל רק במידה והכרטיס הוא עם התייחסות
    // גם אם הכרטיס הוא ללא התייחסות)או שמספר האישי של הכרטיס שווה לגורם שפתח את הכרטיס
    if ((!bCloseAllBtn) && ($get("hidMeasherMistayeg").value != '-1') && ($get("hidDriver").value != '1') && ((GoremMEasger != dId)))
        {  
        i = i - 1;
        _Sidur = $get("SD_lblSidur" + i);
        _Peilut = $get("SD_" + padLeft(i, '0', 3));
        if (_Peilut != null) {
            for (var j = _Peilut.firstChild.childNodes.length - 1; j >= 0; j--) {
                _imgCancelPeilut = _Peilut.firstChild.childNodes[j].cells[_COL_CANCEL];
                _imgAddNesiaReka = _Peilut.firstChild.childNodes[j].cells[_COL_ADD_NESIA_REKA];
                if ((_imgAddNesiaReka.firstChild.getAttribute("NesiaReka") == "1") && (_imgCancelPeilut.firstChild.value != "1")) {
                    if (!_imgAddNesiaReka.childNodes[0].disabled) {
                        _imgAddNesiaReka.childNodes[0].disabled = false;
                        _imgAddNesiaReka.childNodes[0].src = "../../images/down.png";
                    }
                    if (_imgAddNesiaReka.childNodes[0].disabled)
                        _imgAddNesiaReka.childNodes[0].src = "../../images/down_Dis.png";
                    break;
                }
            }
        }
    }
    if ($get('hidNextErrCard').value == '2') {
        $get('hidNextErrCard').value = '0';
        alert('לא קיים כרטיס שגוי הבא');
    }
   }


/**
recieves params. updates UI. Complexity - Low
@method SwitchHourGmarHatchala
*/
function SwitchHourGmarHatchala(iIndex,bTypeInOrOut){
    var _Sidur = $get("SD_lblSidur".concat(iIndex));
    var sSH = $get("SD_txtSH".concat(iIndex));
    var sSG = $get("SD_txtSG".concat(iIndex));
    var _ResonSelected;

    if (bTypeInOrOut == 1){//כניסה
        _ResonSelected = $get("SD_ddlResonIn".concat(iIndex));
        if ((sSG.value != '') && (sSH.value == '') && (_ResonSelected.selectedIndex == 8)){
            $get("hidSdrInd").value = iIndex.toString().concat(',' + bTypeInOrOut.toString());
            $get("btnResonOutIn").click();
        }
    }
    else //יציאה
    {
        _ResonSelected = $get("SD_ddlResonOut".concat(iIndex));
        if ((sSH.value != '') && (sSG.value == '') && (_ResonSelected.selectedIndex == 8)){
            $get("hidSdrInd").value = iIndex.toString().concat(',' + bTypeInOrOut.toString());
            $get("btnResonOutIn").click();
        }
     }
}
/**
recieves params. alert. Complexity - Low
@method chkPitzulHafsaka
*/
   function chkPitzulHafsaka(iIndex, bUpdateCard){
     var iPitzulHafsaka = $get("SD_ddlPHfsaka".concat(iIndex)).value;
     var iSidur = $get("SD_lblSidur".concat(iIndex)).innerHTML;
     var iSidurNahagut = $get("SD_lblSidurNahagut".concat(iIndex)).innerHTML; 
     var bValid=false;             
     if (iPitzulHafsaka==2){
        if (iSidurNahagut=='1'){
            iIndex=iIndex+1;
            var _SidurNext = $get("SD_lblSidurNahagut".concat(iIndex));
            if (_SidurNext!=null){
               var _SidurNextId = $get("SD_lblSidur".concat(iIndex)).innerHTML;
               while (((_SidurNextId==SIDUR_CONTINUE_NAHAGUT) || (_SidurNextId==SIDUR_CONTINUE_NOT_NAHAGUT)) && (_SidurNext!=null)) {
                    iIndex= iIndex+1;
                    _SidurNext = $get("SD_lblSidurNahagut".concat(iIndex));
                    if (_SidurNext!=null){
                        _SidurNextId = $get("SD_lblSidur".concat(iIndex)).innerHTML;
                    }                                       
               }             
               if (_SidurNext!=null){  
                   var iSidurNextNahagut = _SidurNext.innerHTML; 
                   if (iSidurNextNahagut=='1'){
                        bValid=true;
                   }
               }
            }            
        }
        if (!bValid){
            var sMsg = 'ערך לא תקין, פיצול כפול מותר רק בין שני סידורי נהגות';
            if (!bUpdateCard){
                 alert(sMsg);                 
            }
            else{
              return sMsg;
            }
        }
     }
   }
/**
recieves params. alert. Complexity - Low
@method ChkCharigaVal
*/
   function ChkCharigaVal(id)
   {
     var ddlChariga = $get("SD_ddlException".concat(id));  
     var iSelVal = Number(ddlChariga.value);
     var iCharigaType = Number(ddlChariga.getAttribute("ChrigaType"));
     if ((iSelVal > 0) && (iSelVal != iCharigaType) && (iCharigaType != 3) && (iCharigaType != 0) && (iCharigaType != 4)){
         ddlChariga.value = -1;
         alert('אין חריגה משעת התחלה או משעת גמר ');
     }
   }
/**
recieves params. play another button click. Complexity - Medium
@method CopyOtoNum
*/
   function CopyOtoNum(oRow) {
       oId = String(oRow.id).substr(0,oRow.id.length-6);
       var _CarNum = $get(oId).cells[_COL_CAR_NUMBER].childNodes[0];                
       var _CurrCarNum = _CarNum.value;
       var _OrgCarNum = _CarNum.getAttribute("OldV");
       var _MustCarNum = _CarNum.getAttribute("MustOtoNum");
       if ($get(oId).nextSibling != null) {
           var _NextPeilut = $get(oId).nextSibling.cells[_COL_CAR_NUMBER].childNodes[0];
           var _NextCarNum = _NextPeilut.value;
           var _CurrPeilutMkt = $get(oId).nextSibling.cells[_COL_MAKAT].childNodes[0];
           if (_NextCarNum != undefined) {
               var _NextMustCarNum = _NextPeilut.getAttribute("MustOtoNum");
               if (_NextCarNum == ''){ _NextCarNum = '0'; }
               if (_CurrCarNum != ''){
                   if (((_MustCarNum == '1') && (((_NextCarNum == _OrgCarNum) || (Number(_NextCarNum) == 0))) && (_NextMustCarNum == '1')) || (_CurrPeilutMkt.value == '') || (_CurrPeilutMkt.value == '0'))                         
                       {
                       $get("lblCarNumQ").innerText = "האם להחליף את מספר הרכב בכל הפעילויות בסידור בהן מספר הרכב הוא ריק או ".concat(String(_OrgCarNum));
                       $get("hidCarKey").value = _OrgCarNum + ',' + _CurrCarNum + ',' + oId;
                       $get("btnCopy").click();
                   }
               }
           }
       }      
       _CarNum.disabled = false;
   }

/*************************** license number *******************************************/

   function ChkLiscenseNumber(oRow) {

       var KeyID = event.keyCode;
       var sCardDate = $get("clnDate").value;
       if (((KeyID >= 48) && (KeyID <= 57)) || ((KeyID >= 96) && (KeyID <= 105))) {
           oId = String(oRow.id).substr(0, oRow.id.length - 6);
           var lLicenseNo = $get(oId).cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].value;
           SetBtnChanges(); //SetLvlChg(3);
           if ((lLicenseNo != '') && (trim(String(lLicenseNo)).length >= 7)) {
               wsGeneral.CheckLicenseNo(lLicenseNo,sCardDate, callBackOtoLicense, null, oRow);
           }
       }
   }
   /**
   recieves params. does validations. updates ui. . Complexity - Low
   @method callBackOto
   */
   function callBackOtoLicense(result, oRow) {

       var oId = String(oRow.id).substr(0, oRow.id.length - 6);
       if (result == '0') {
           var sBehaviorId = 'vldCarLicenseNumBehv'.concat(oId);
           $find(sBehaviorId)._ensureCallout();
           $find(sBehaviorId).show(true);
           $get(oId).cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].title = "מספר רישוי שגוי";
           $get(oId).cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].value = "";
       }
       else {
           $get(oId).cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].title = result;
           $get(oId).cells[_COL_CAR_NUMBER].childNodes[0].value = result;
           var OrgDisable = $get(oId).cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].disabled;
           $get(oId).cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].disabled = true;
           CopyOtoLicenseNum(oRow);
       }
   }

   function CopyOtoLicenseNum(oRow) {
       oId = String(oRow.id).substr(0, oRow.id.length - 6);
       var _CarNum = $get(oId).cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0];
       var _CurrCarNum = _CarNum.value;
       var _OrgCarNum = _CarNum.getAttribute("OldV");
       var _MustCarNum = _CarNum.getAttribute("MustOtoNum");
       if ($get(oId).nextSibling != null) {
           var _NextPeilut = $get(oId).nextSibling.cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0];
           var _NextCarNum = _NextPeilut.value;
           var _CurrPeilutMkt = $get(oId).nextSibling.cells[_COL_MAKAT].childNodes[0];
           if (_NextCarNum != undefined) {
               var _NextMustCarNum = _NextPeilut.getAttribute("MustOtoNum");
               if (_NextCarNum == '') { _NextCarNum = '0'; }
               if (_CurrCarNum != '') {
                   if (((_MustCarNum == '1') && (((_NextCarNum == _OrgCarNum) || (Number(_NextCarNum) == 0))) && (_NextMustCarNum == '1')) || (_CurrPeilutMkt.value == '') || (_CurrPeilutMkt.value == '0')) {
                       $get("lblCarNumQ").innerText = "האם להחליף את מספר הרישוי בכל הפעילויות בסידור בהן מספר הרכב הוא ריק או ".concat(String(_OrgCarNum));
                       $get("hidCarKey").value = _OrgCarNum + ',' + _CurrCarNum + ',' + oId;
                       $get("btnCopy").click();
                   }
               }
           }
       }
       _CarNum.disabled = false;
   }
/***************************************************************************************/
   function disableUpdateBtn(bDisabled) {
       $get("btnUpdateCard").disabled = bDisabled;
       if (bDisabled==true)
           $get("btnUpdateCard").className = "btnWorkCardUpadteDis";
         else
             $get("btnUpdateCard").className = "btnWorkCardUpadte";
    }
    function SetFocus(id, col) {
        $get(id).cells[col].childNodes[0].select();
    }
/**
recieves params.  Complexity - Low
@method GetMakatType
*/
    function GetMakatType(lMakat) {
            var iMakatType = 0;
            var lTmpMakat = 0;

            lTmpMakat = Number(lMakat);
            if (lTmpMakat.toString().substr(0, 1) == "5" && (lTmpMakat >= 50000000))
                iMakatType = MKT_VISA; //6-Visa
            else if ((lTmpMakat >= 100000) && (lTmpMakat < 50000000))
                iMakatType = MKT_SHERUT; //1-kav sherut            
            else if ((lTmpMakat >= 60000000) && (lTmpMakat <= 69999999))
                iMakatType = MKT_EMPTY; //2-Empty
            else if ((lTmpMakat >= 80000000) && (lTmpMakat <= 99999999))
                iMakatType = MKT_NAMAK; //3-Namak
            else if ((lTmpMakat >= 70000000) && (lTmpMakat <= 70099999))
                iMakatType = MKT_VISUT; //4-ויסות             
            else if ((lTmpMakat >= 70100000) && (lTmpMakat <= 79900000))
                iMakatType = MKT_ELEMENT;   //5-Element          
            return iMakatType;
        }   
    function isElementMechona(lMakat){          
      return ((Number(String(lMakat).substr(0,3))==711) || (Number(String(lMakat).substr(0,3))==712) || (Number(String(lMakat).substr(0,3))==701));
    }

/**
recieves params.  Uses wsGeneral service. Complexity - Low
@method chkNewSidur
*/
    function chkNewSidur(iSidurNum) {
        if ($find("SD_ACSidur" + iSidurNum)._flyoutHasFocus == false) {
            _Sidur = $get("SD_lblSidur" + iSidurNum);
            iMeasherMistayeg = $get("hidMeasherMistayeg").value;
            if (_Sidur != null)
                if (_Sidur.value != ""){
                    var sSidurDate = $get("SD_lblDate".concat(iSidurNum)).innerHTML; 
                    wsGeneral.IsNewSidurNumberValid(_Sidur.value, iSidurNum, sSidurDate, iMeasherMistayeg, callBackSidurVld, null, iSidurNum);
                }
        }
    }

/**
recieves params.  Uses wsGeneral service.Update UI. Complexity - Low
@method chkNewSidur
*/
    function callBackSidurVld(result, iSidurNum) {
        var Res = result.split("|");
        if (Res[0] == "1") {//error
            var sBehaviorId = 'vSBhv'.concat(iSidurNum);
            $get("SD_vldSidurNum".concat(iSidurNum)).errormessage = Res[1];
            $find(sBehaviorId)._ensureCallout();
            $find(sBehaviorId).show(true);
            $get("SD_lblSidur" + iSidurNum).value = "";
            $get("SD_lblSidur" + iSidurNum).select();
        }
        else {
            SetNewSidurCtls(iSidurNum, Res[1]);
            wsGeneral.getTeurSidurByKod(_Sidur.value, callBackNewSidur, null, iSidurNum);
        }
    }
    function callBackNewSidur(result, iSidurNum){
        if (result != -1)
            if ($get("SD_lblSidur" + iSidurNum)!=null)
                $get("SD_lblSidur" + iSidurNum).title = result;        
    }
    function SetNewSidurFocus(iSidurIndex){
        var _Sidur = $get('SD_lblSidur'.concat(iSidurIndex));
        if (!((_Sidur.style.visibility == "hidden" || _Sidur.style.display == "none" || _Sidur.disabled == true)))
            _Sidur.focus();
    }
    function SetNewPeilutFocus(iPeilutIndex){        
        if (iPeilutIndex != undefined) {
            var _Peilut = $get(iPeilutIndex);          
            if ((_Peilut != null) && (_Peilut != undefined)){
                if (!((_Peilut.style.visibility == "hidden" || _Peilut.style.display == "none" || _Peilut.disabled == true))) {                    
                    setTimeout("setFocus('" + iPeilutIndex + "')", 100); 
                }
            }
        }
    }
    function setFocus(focusControlID) {
        document.getElementById(focusControlID).focus();
    }
    function DisabledShinuyKelet()
    {
        $get("hidExecInputChg").value = '0';
        return SetChgFlag();
    }
/**
recieves params.  Update UI. Complexity - Low
@method btnCopyOtoNum
*/
    function btnCopyOtoNum(iAction)
    {
     $find("pBehvCopy").hide();
     if (iAction==1){
        var arrKey = $get("hidCarKey").value.split(",");
        var _NextPeilutCarNum, _MustCarNum, _NextPeilutMakat;
        var oId = arrKey[2];
        var _OrgCarNum = arrKey[0];
        var _CurrCarNum = arrKey[1];
        var _CarNum = $get(oId).cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0];
        var _NextPeilut = $get(oId).nextSibling;
        while (_NextPeilut!=null){
            _NextPeilutCarNum = _NextPeilut.cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].value;
            _NextPeilutMakat = _NextPeilut.cells[_COL_MAKAT].childNodes[0].value;
            if (_NextPeilutCarNum!=undefined){
                _MustCarNum = _NextPeilut.cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].getAttribute("MustOtoNum");
            if (((((_NextPeilutCarNum == _OrgCarNum) || (Number(_NextPeilutCarNum) == 0) || (_NextPeilutCarNum == '')) && (((_MustCarNum == '1') || ((_NextPeilutMakat == '') || (_NextPeilutMakat == '0'))))))){
                if ((_NextPeilut.cells[_COL_CANCEL_PEILUT].firstChild.value != '1') && (_NextPeilut.cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].disabled != true)) {
                    _CarNum.setAttribute("OldV",_CurrCarNum);
                    _NextPeilut.cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].setAttribute("OldV", _CurrCarNum);
                    _NextPeilut.cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].value = _CurrCarNum;
                    _NextPeilut.cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].title = _CarNum.title;
                }
            }
            else{//אם נתקלים במספר רכב שונה עוצרים. אם נתקלים בפעילות שאינה דורשת מספר רכב או ריקה או 0 ממשיכים
                if ((_NextPeilutCarNum!='0') && (_NextPeilutCarNum!='') && (_MustCarNum!='0')){
                    break;
                }
            }
           }
            _NextPeilut = _NextPeilut.nextSibling;
        }}}    
if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded(); 