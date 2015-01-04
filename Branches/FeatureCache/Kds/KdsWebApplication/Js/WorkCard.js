var bScreenChanged = false;
    function OpenDiv(DivId, btnId){
        var oDiv = $get(DivId.toString());
        if (oDiv.style.display=='none'){        
            oDiv.style.display='block';              
            switch (DivId){
                case "divEmployeeDetails":
                    $get("divNetunimLeYom").style.display = 'none';
                    $get("divParticipation").style.display = 'none';
                    $get("tbEmpDetails").style.display = 'block';
                    $get("tblPart").style.display = 'none';
                    $get("btnPlus1").className = 'ImgButtonShowMinus';
                    $get("btnPlus3").className = 'ImgButtonShowPlus';
                    $get("btnPlus2").className = 'ImgButtonShowPlus';
                    if ($get("txtIsuk").disabled != true)
                        $get("txtIsuk").select();
                    else
                        $get("txtId").focus();
                    break;
                case "divNetunimLeYom":
                    $get("divEmployeeDetails").style.display = 'none';
                    $get("divParticipation").style.display = 'none';
                    $get("tblPart").style.display = 'block';
                    $get("tbEmpDetails").style.display = 'none';
                    $get("btnPlus2").className = 'ImgButtonShowMinus';
                    $get("btnPlus3").className = 'ImgButtonShowPlus';
                    $get("btnPlus1").className = 'ImgButtonShowPlus';
                    if (($get("ddlTachograph").disabled != true) && (!($get("ddlTachograph").isDisabled))) {
                        $get("ddlTachograph").focus();                    
                    }
                    else {
                        if ($get("ddlLina").disabled != true)
                            $get("ddlLina").focus();
                    else
                        if ($get("txtId").disabled == false)
                            $get("txtId").focus();
                    }
                    break;
                case "divParticipation":
                    $get("divNetunimLeYom").style.display = 'none';
                    $get("divEmployeeDetails").style.display = 'none';
                    $get("tblPart").style.display = 'block';
                    $get("tbEmpDetails").style.display = 'none';
                    $get("btnPlus3").className = 'ImgButtonShowMinus';
                    $get("btnPlus2").className = 'ImgButtonShowPlus';
                    $get("btnPlus1").className = 'ImgButtonShowPlus';
                    if ($get("txtFirstPart").disabled != true)
                        $get("txtFirstPart").select();
                    else
                        $get("txtId").focus();
                       
                    break;
            }           
        }
        else{
            oDiv.style.display = 'none';
            $get("tbEmpDetails").style.display = 'none';
            $get("tblPart").style.display = 'none';
            switch (DivId) {
                case "divEmployeeDetails":
                     $get("btnPlus1").className = 'ImgButtonShowPlus';
                     break;
                case "divNetunimLeYom":
                     $get("btnPlus2").className = 'ImgButtonShowPlus';
                     break;
                case "divParticipation":
                     $get("btnPlus3").className = 'ImgButtonShowPlus';
                     break;
            }                      
        }       
    }
    function onClientItemSelected_getID(sender, eventArgs) {
        $get("clnDate").select();
    }
    function onClientHiddenHandler_getID(sender, eventArgs){           
        GetOvedALLDetails($get("txtId").value);
    }  
    function GetOvedNameSucceeded(result){    
       if ((result=='') || (result=='null')){
            alert('מספר אישי לא קיים');                        
            $get("txtId").select();
            $get("txtName").value='';
            SetRefreshBtn(true);      
        }
        else{
            $get("txtName").value=result;
            SetRefreshBtn(false);       
        }     
    }
    function GetOvedDetailsSucceeded(result){            
        var xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
        xmlDoc.async="false";
        xmlDoc.loadXML(result);
        root=xmlDoc.documentElement;
        if (root!=null)
        {        
          if (root.childNodes.length>0)
          {                            
           if (root.childNodes.item(0)!=null){           
            $get("txtCompany").value=root.childNodes.item(0).text;
           }
           if (root.childNodes.item(1)!=null){           
            $get("txtArea").value=root.childNodes.item(1).text;
           }
           if (root.childNodes.item(2)!=null){           
            $get("txtSnif").value=root.childNodes.item(2).text;
           }
           if (root.childNodes.item(3)!= null){    
            $get("txtMaamad").value=root.childNodes.item(3).text;
           }
           if (root.childNodes.item(4)!=null){           
            $get("txtIsuk").value=root.childNodes.item(4).text;           
           }
           if (root.childNodes.item(5)!=null){           
            $get("txtName").value=root.childNodes.item(5).text;  
           }
           if (root.childNodes.item(6)!=null){           
            $get("ddlTachograph").selectedValue = root.childNodes.item(6).text; 
           }
           if (root.childNodes.item(7)!=null){           
            $get("ddlHalbasha").selectedValue = root.childNodes.item(7).text; 
           }
           if (root.childNodes.item(8)!=null){
               $get("ddlLina").selectedValue = root.childNodes.item(8).text;  

           }
           if (root.childNodes.item(9)){           
             $get("ddlTravleTime").selectedValue = root.childNodes.item(9).text;  
           }
            SetRefreshBtn(false); 
          }               
        }                
    }    
    function EnabledAllFrames(bEnable)
    {
        $get("tbEmpDetails").disabled = (!bEnable);     
        $get("tbValWorkDay").disabled = (!bEnable);
        $get("tblPart").disabled = (!bEnable);
        $get("SD_tbSidurim").disabled = (!bEnable);
        $get("btnFindSidur").disabled = (!bEnable);
        $get("btnAddHeadrut").disabled = (!bEnable);
        $get("btnClock").disabled = (!bEnable);
        $get("btnApprovalReport").disabled = (!bEnable);      
        $get("ddlTachograph").disabled = (!bEnable);       
        $get("ddlHalbasha").disabled = true;   
        $get("ddlLina").disabled = (!bEnable);       
        $get("ddlHashlamaReason").disabled = (!bEnable);       
        $get("btnHashlamaForDay").disabled = (!bEnable);
        $get("btnCalcItem").disabled = (!bEnable);
        $get("btnNextCard").disabled = (!bEnable);
        $get("btnPrevCard").disabled = (!bEnable);
        $get("btnPrint").disabled = (!bEnable);
        $get("btnAddMyuchad").disabled = (!bEnable);
        $get("btnCloseCard").disabled = (!bEnable);
        
        if (bEnable) {            
            $get("btnPrevCard").className = "btnPrevDay";
            $get("btnNextCard").className = "btnNextDay";
            $get("btnPrint").className = "btnWorkCardPrint";
            $get("btnNextErrCard").className = "btnNextError";             
          }
        else {           
          $get("btnPrevCard").className = "btnPrevDayDis";
          $get("btnNextCard").className = "btnNextDayDis";
          $get("btnPrint").className = "btnWorkCardPrintDis";
          $get("btnNextErrCard").className = "btnNextErrorDis";            
        }
   
      $get("btnNextErrCard").disabled = (!bEnable);      
      EnabledSidurimListBtn(!bEnable,true);   
    }
    function GetOvedALLDetails(iKodOved){
       var sCardDate;
       if ($get("clnDate").value != '') {
           sCardDate = $get("clnDate").value;
       }
       else{       
         sCardDate = getSysDate().toString();
       }
     if (iKodOved != '') {
           if (String(iKodOved).length<=5){
               wsGeneral.GetOvedAllDetails(iKodOved,sCardDate,GetOvedDetailsSucceeded);
               $get("txtId").value=iKodOved;    
               EnabledAllFrames(false);      
           }
       }
    }    
    function GetOvedMisparIshiByName(){
      var sName = $get("txtName").value;
      if (sName.indexOf(")")==-1){                  
        $get("txtName").click();                          
      }                      
    }          
    function onClientHiddenHandler_getName(sender, eventArgs){    
     var iMisparIshi, iPos;
     var sOvedName=$get("txtName").value;     
     if (sOvedName!='')
     {
         iPos = sOvedName.indexOf('(');
         if (iPos!=-1)
         {
            iMisparIshi = sOvedName.substr(iPos+1, sOvedName.length-iPos-2);
            $get("txtId").value=iMisparIshi;
            GetOvedALLDetails(iMisparIshi);                   
         }
      }EnabledAllFrames(false);               
    }         
    function RefreshOvedDetails(){    
       if ($get("txtId").value!='')
       {
        GetOvedALLDetails($get("txtId").value);
       } SetBtnChanges();       
    }
    function ShowRecivimCalculation(){
        var id = $get("txtId").value;
        var date = $get("clnDate").value;
        var sQuryString = "?id=" + id + "&date=" + date;
        $get("divHourglass").style.display = 'block'; 
        var ReturnWin = window.showModalDialog('Rechivim.aspx' + sQuryString, window, "dialogHeight: 471px; dialogWidth: 715px;dialogtop:200px;status:no;resizable:yes;");
        $get("divHourglass").style.display = 'none';
        if(ReturnWin=='' || ReturnWin=='undefined') ReturnWin=false;
        return ReturnWin;
    }
    function ShowLastUpdateUser(){
        var id = $get("txtId").value;
        var date = $get("clnDate").value;
        var sQuryString="?id=" + id + "&date=" + date;
        var ReturnWin = window.showModalDialog('MadknAcharon.aspx' + sQuryString, window, "dialogHeight: 280px; dialogWidth: 490px;dialogtop:200px;status:no;resizable:no;scroll:no;");                         
        if(ReturnWin=='' || ReturnWin=='undefined') ReturnWin=false;
        return ReturnWin;   
    }
    function ShowEmployeeDetails(){    
        var id = $get("txtId").value;
        var date = String($get("clnDate").value).substr(3);
        var sQuryString="?id=" + id + "&month=" + date;        
        var ReturnWin=window.showModalDialog('NetuneyOvedModal.aspx' + sQuryString , window , "dialogwidth:970px;dialogheight:600px;dialogtop:130px;dialogleft:25px;status:no;resizable:yes;scroll:0;");                          
        if(ReturnWin=='' || ReturnWin=='undefined') ReturnWin=false;
        return ReturnWin;   
    }     
    function CheckButton(btnId,HidId){    
        if (HidId.value=="0"){        
         HidId.value="1";                         
         btnId.style.cssText = "BACKGROUND-IMAGE: url(../../Images/allscreens-checkbox.jpg)"        
        }
        else{        
            HidId.value="0";
            btnId.style.cssText = "BACKGROUND-IMAGE: url(../../Images/allscreens-checkbox-empty.jpg)";
        }SetBtnChanges();          
    }
    function SetBtnChanges() {
        var KeyID = event.keyCode;
        if ((KeyID != 45) && (KeyID != 107)) {
            bScreenChanged = true;
            disableUpdateBtn(false);
            $get("hidChanges").value = true;
            $get("hidUpdateBtn").value = "false";
            SetRefreshBtn(true);
            $get("btnPrevCard").disabled = true;
            $get("btnNextCard").disabled = true;
            $get("btnPrevCard").className = "btnPrevDayDis";
            $get("btnNextCard").className = "btnNextDayDis"
            $get("btnNextErrCard").disabled = true;
            $get("btnNextErrCard").className = "btnNextErrorDis";  
            $get("txtId").disabled = true;
            $get("txtName").disabled = true;
            $get("clnDate").disabled = true;
            $get("hidErrChg").Value = "0";
        }        
    }
    function SetLvlChg(iLvl, iSidurIndex) {
        var KeyID = event.keyCode;
        if ((KeyID != 45) && (KeyID != 107)) {
            var id = ("hidLvl".concat(String(iLvl))).concat("Chg");
            if (Number(iLvl == 1))
                $get(id).value = "1";
            else
                $get(id).value = String($get(id).value).concat(iSidurIndex + ",");
        }                    
    }    
    function CheckChanges(){    
      if (bScreenChanged) {        
        $get("btnHidClose").click();
        return false;
        }        
      else {
          ///FreeWC();
        CloseWindow();return false;}      
    }
    function CheckChangesBeforePrint(){    
      if (bScreenChanged) {
        $get("btnHidPrint").click();
        return false;        
        }               
    }
    function RefreshBtn() {      
        $get("hidRefresh").value = "1";       
        EnabledAllFrames(true);
        return true;
    }

   // function FreeWC() {
   //     var Pratim = $get("hidPratim").value.split('|');
   //     var iMisparIshi = Pratim[0];
   //     var dCardDate = Pratim[1];
   //     var imeadkenOl = $get("hidMiMeadkenOL").value;
      
   //  //  alert('dCardDate' + dCardDate);
   ////   alert('iMisparIshi' + iMisparIshi);
   // //  alert('imeadkenOl' + imeadkenOl);
   //    // alert(document.getElementById("hidMiMeadkenOL").value);
   //     wsGeneral.FreeWC(iMisparIshi, dCardDate, imeadkenOl);   
   // }

    function sleep(milliseconds) {
        var start = new Date().getTime();
        for (var i = 0; i < 1e7; i++) {
            if ((new Date().getTime() - start) > milliseconds) {
                break;
            }
        }
    }
    function CloseChgBtn(){
        bScreenChanged = false;return true;
    }
    
    function CheckIfCardExists(){
      var sDay = $get("clnDate").value.substr(0,2);
      var sMonth =  $get("clnDate").value.substr(3,2);
      var sFullYear = $get("clnDate").value.substr(6,4);         
      id = $get("txtId").value;
      date = $get("clnDate").value;
      if (date != '') {           
          wsGeneral.IsCardExists(id, date, callBackCheckCardExists);
          EnabledAllFrames(false);
      }      
    }
    function callBackCheckCardExists(result)
    {   var arrReturnValue = result.split("|"); 
        if (arrReturnValue[0]=="0")
        {
            SetRefreshBtn(true);
            var sBehaviorId='vldExBehavior';
            $find(sBehaviorId)._ensureCallout();
            $find(sBehaviorId).show(true);                        
            $get("txtDay").value ="";
        }
        else
        {
           SetRefreshBtn(false);         
           $get("txtDay").value = arrReturnValue[1];                    
        }
         $get("txtDay").title = arrReturnValue[1];
     }
     function SetRefreshBtn(bDisabled) {
         $get("btnRefreshOvedDetails").disabled = bDisabled;
         if (bDisabled)
             $get("btnRefreshOvedDetails").className = "ImgButtonShowDis";
            else
                $get("btnRefreshOvedDetails").className = "ImgButtonShow";
     }     
    function GetErrorMessage(id, level, pKey){   
        oId=id.id;       
    var oObj = $get(oId);
    var rc = oObj.getAttribute("ErrCnt");
    var arrKnisa; 
    if (Number(rc)>0)
    {
     var iMisparIshi = $get("txtId").value;
     var dCardDate = $get("clnDate").value;
     var sFName = oObj.getAttribute("FName");
     var iSidurNum=0; var sStartH=''; var sPShatY='';var iMKnisa = 0; 
     $get("hErrKey").value=oId;     
     if (level==1){
         wsGeneral.GetFieldErrors(level, iMisparIshi, dCardDate, iSidurNum, sStartH, sPShatY, iMKnisa, sFName, onGetdErrSucc);     
     }
     else
     {      
      if (level==2)
      {            
       var i = String(id.id).substr("SD_txtSH".length);
       iSidurNum=$get(("SD_lblSidur").concat(pKey)).innerHTML;
       sStartH = $get(("SD_txtSH").concat(pKey)).getAttribute("OrgShatHatchala");         
       wsGeneral.GetFieldErrors(level, iMisparIshi,dCardDate,iSidurNum,sStartH,sPShatY,iMKnisa,sFName, onGetdErrSucc);
      }
      else
      {
       var arrKey =  pKey.toString().split("|");
       sPShatY = $get(arrKey[0]).value;
       if (sPShatY == '__:__') {
           sPShatY = $get(arrKey[0]).getAttribute('OrgShatYetiza');
       } else {
           sPShatY=$get(arrKey[0]).getAttribute('OrgShatYetiza').substr(11,5);
           var AddDay = Number($get(arrKey[3].concat("_txtDayToAdd")).value);
           if (AddDay == 1) {
               //add one day to the date              
               var ShStart = new Date();
               ShStart.setFullYear(dCardDate.substr(dCardDate.length - 4, 4));
               ShStart.setMonth(Number(dCardDate.substr(3, 2)) - 1);
               ShStart.setDate(dCardDate.substr(0, 2));
               ShStart.setDate(ShStart.getDate() + 1);
               sPShatY = String(GetDateDDMMYYYY(ShStart)).concat(' ' + sPShatY);              
           }
           else {
               sPShatY = dCardDate.concat(' ' + sPShatY);              
           }
       }
       iMKnisa = $get(arrKey[1]).innerHTML;
       arrKnisa = iMKnisa.toString().split(",");
       iMKnisa = arrKnisa[0];
       iSidurNum = $get(("SD_lblSidur").concat(arrKey[2])).innerHTML;
       sStartH = $get(("SD_txtSH").concat(arrKey[2])).getAttribute("OrgShatHatchala");       
       wsGeneral.GetFieldErrors(level, iMisparIshi, dCardDate, iSidurNum, sStartH, sPShatY, iMKnisa, sFName, onGetdErrSucc);
      }}}}  
    function onGetdErrSucc(result){    
        $get("tbErr").innerHTML = result;
        $get("btnErrors").click(); 
    }
    function ShowRemark(id)
    {
      var iMisparIshi = $get("txtId").value;
      var dCardDate = $get("clnDate").value;
      var iSidurNum =$get(("SD_lblSidur").concat(id)).innerHTML;
      var sStartH = $get(("SD_txtSH").concat(id)).getAttribute("OrgShatHatchala");      
      wsGeneral.GetLoLetashlumRemark(iMisparIshi,dCardDate,iSidurNum,sStartH,onGetdRmkSucc);      
    }
    function onGetdRmkSucc(result)
    {      
        $get("tblRmk").innerHTML = result;
        $get("btnRemark").click();          
    }
     function GetAppMsg(id){       
        if (id!=undefined){                                       
                 var olbl = $get("lblApp");
                 olbl.innerText = id.getAttribute("App");   
                 $get("btnApp").click();                 
        }     
     }
     function CancelError(id){
        $find("bMpeErr").hide();          
        iMisparIshi = $get("txtId").value;
        dCardDate = $get("clnDate").value;     
        sErrorId = id.parentNode.parentNode.children[3].innerText;  
        sMeasher =  $get("hidGoremMeasher").value;    
        wsGeneral.CancelError(iMisparIshi, dCardDate, sErrorId,sMeasher,callBackCancelError);      
     }
     function callBackCancelError(result){     
        if (result==1){SetBtnChanges();     
            var oObj = $get($get("hErrKey").value);         
            var rc = oObj.getAttribute("ErrCnt");
            if (Number(rc)>0)
            {
              rc=Number(rc)-1;
              oObj.setAttribute("ErrCnt", Number(rc));             
              if (Number(rc)>0){              
                oObj.style.background = "red";
                oObj.style.color = "white";
              }
            else {
                if (oObj.style.textDecoration!='underline'){
                    oObj.style.background = "white";
                    oObj.style.color = "black";
                }
                if ((oObj.getAttribute("imgId"))!=''){
                $get(oObj.getAttribute("imgId")).style.display="none";}
              }
            }            
        }     
    }        
    function ApproveError(id){
        $find("bMpeErr").hide();    
        iMisparIshi = $get("txtId").value;
        dCardDate = $get("clnDate").value;
        sErrorId = id.parentNode.parentNode.children[3].innerText; 
        sMeasher =  $get("hidGoremMeasher").value;      
        wsGeneral.ApproveError(iMisparIshi, dCardDate, sErrorId,sMeasher);
        return false;
    }
    function AddSidurHeadrut() {
        _bScreenChanged = bScreenChanged;
        if (_bScreenChanged) {
            if (!ChkCardVld())
                return false;
            $("#hidSave")[0].value = "1";
            __doPostBack('btnConfirm', '');
        }        
        var sQueryString;        
        sQueryString = "?dt=" + Date();
        sQueryString = sQueryString + "&MisparIshi=" + $get("txtId").value;
        sQueryString = sQueryString + "&DateCard=" + $get("clnDate").value;                
        sQueryString = sQueryString + "&MisparSidur="; 
        sQueryString = sQueryString + "&TimeStart=";
        sQueryString = sQueryString + "&TimeEnd=";
        sQueryString = sQueryString + "&Status=" + $get("hidMeasherMistayeg").value;
        $get("divHourglass").style.display = 'block';   
        res = window.showModalDialog('DivuachHeadrut.aspx?' + sQueryString, '', 'dialogwidth:655px;dialogheight:405px;dialogtop:280px;dialogleft:380px;status:no;resizable:no;scroll:no');
        $get("divHourglass").style.display = 'none';
        if ((_bScreenChanged) || ((res != undefined) && (res != '') && (!_bScreenChanged))) {
            $get("hidExecInputChg").value = "1";
            $get("hidRefresh").value = "1";
            $get("hidChanges").value = "true";           
            __doPostBack('btnAddHeadrut', '');
            bScreenChanged = false;
        }
        
        return false;
    }
    function AddSidur() {
        _bScreenChanged = bScreenChanged;
        if (_bScreenChanged) {
            if (!ChkCardVld())
                return false;
            $("#hidSave")[0].value = "1";                      
            __doPostBack('btnConfirm', '');            
        }
        
        var sQueryString;        
        sQueryString = "?dt=" + Date();
        sQueryString = sQueryString + "&EmpID=" + $get("txtId").value;
        sQueryString = sQueryString + "&CardDate=" + $get("clnDate").value;
        sQueryString = sQueryString + "&Status=" + $get("hidMeasherMistayeg").value;
        $get("divHourglass").style.display = 'block';
        res = window.showModalDialog('HosafatSidur.aspx?' + sQueryString, '', 'dialogwidth:975px;dialogheight:690px;dialogtop:180px;dialogleft:50px;status:no;resizable:no;');
        $get("divHourglass").style.display = 'none';
        if ((_bScreenChanged) || ((res != undefined) && (!_bScreenChanged))) {
            $get("hidExecInputChg").value = "1";
            $get("hidRefresh").value = "1";
            $get("hidChanges").value = "true";
            __doPostBack('btnFindSidur', '');
            bScreenChanged = false;
        }
        return false;
    }
   function ShowDrvErr(){
       var iLaoved;
       if ($get("hidRashemet").value == "1" || $get("hidMenahelBank").value == "1")
            iLaoved="0";
           else
            iLaoved="1";
              
        var sQueryString;        
        sQueryString = "?dt=" + Date();
        sQueryString = sQueryString + "&EmpID=" + $get("txtId").value;
        sQueryString = sQueryString + "&CardDate=" + $get("clnDate").value;
        sQueryString = sQueryString + "&laoved=" + iLaoved;
        res=window.showModalDialog('WorkCardErrors.aspx' + sQueryString, '', 'dialogwidth:1000px;dialogheight:600px;dialogtop:280px;dialogleft:180px;status:no;resizable:no;');
        if (res==undefined){
         return false;
        }
        else{
        return true;
        }
   }
   function OpenZmaneiNessiot(id,date, type, value){ 
        var sQueryString;
        sQueryString = "?id="+ id + "&date=" + date + "&type=" + type + "&value=" + value + "&dt=" + Date(); 
        window.showModalDialog('ZmaniNesiot.aspx' + sQueryString, '', 'dialogwidth:660px;dialogheight:170px;dialogtop:200px;status:no;resizable:no;');
    }
   function SetChgFlag(){
    $get("hidChanges").value=bScreenChanged;
    return true;
   }
   function ChkCardVld(){   
     var sXML,sSidurDate,dStartHour,dEndHour,SidurTime;   
     var bValid=true;var sMsg='';var sCallBack='';
     var HashForDay = $get("HashlamaForDayValue").value;
     var HashReason = $get("ddlHashlamaReason").value;
     $get("hidExecInputChg").value = "0";

     if ((Number(HashForDay) == 1) && (Number(HashReason) == -1) && (!($get("ddlHashlamaReason").disabled))) {      
        sMsg ='סומנה השלמה ליום, יש לדווח סיבה'; 
        bValid = false;
     }
     if ((Number(HashForDay)!=1) && (Number(HashReason) > 0) && (!($get("ddlHashlamaReason").disabled))){      
        sMsg =sMsg.concat('סומנה סיבת השלמה ללא סימון השלמה');
        bValid = false;
     }  

     var i=0, sSH, sSG, oDDL;
     sSH = $get("SD_txtSH".concat(i));
     sSG = $get("SD_txtSG".concat(i));
    
     while (sSH!=null)
     {
         iSidurNum = $get(("SD_lblSidur").concat(i)).innerHTML;
         if (iSidurNum == '') iSidurNum = $get(("SD_lblSidur").concat(i)).value;
         if ((iSidurNum == '') && ($get(("SD_lblSidur").concat(i)).value == '') && ($get("SD_lblSidurCanceled".concat(i)).value != '1')) {
             sMsg = sMsg.concat('מספר סידור ' + iSidurNum + " אינו תקין \n");
             bValid = false;
         }
         else {
             if ($get("SD_lblSidurCanceled".concat(i)).value != '1') {
                 oDDL = $get("SD_ddlResonIn".concat(i));
                 if (!IsValidTime(sSH.value)) {
                     sMsg = sMsg.concat('שעת התחלה בסידור ' + iSidurNum + " אינה תקינה \n");
                     bValid = false;
                 }
                 if (oDDL != null) {
                     if ((sSH.value != '') && (sSH.value != "__:__") && (oDDL.value == -1) && (oDDL.disabled == false)) {
                         sMsg = sMsg.concat('בסידור  ' + iSidurNum + " דווחה שעת התחלה, יש לדווח סיבה \n");
                         bValid = false;
                     }
                     oDDL = $get("SD_ddlResonOut".concat(i));
                     if (!IsValidTime(sSG.value)) {
                         sMsg = sMsg.concat('שעת גמר בסידור ' + iSidurNum + " אינה תקינה \n");
                         bValid = false;
                     }
                     if ((sSG.value != '') && (sSG.value != "__:__") && (oDDL.value == -1) && (oDDL.disabled == false)){
                         sMsg = sMsg.concat('בסידור  ' + iSidurNum + " דווחה שעת גמר, יש לדווח סיבה \n");
                         bValid = false;
                     }
                     sCallBack = chkPitzulHafsaka(i, true);
                     if ((sCallBack != '') && (sCallBack != undefined)) {
                         sMsg = sMsg.concat(sCallBack + "\n");
                         bValid = false;
                     }
                     if ((sSG.value != '') && (sSG.value != "__:__") && (sSH.value != '') && (sSH.value != "__:__")){
                         sSidurDate = $get("SD_lblDate".concat(i)).innerHTML;
                         sCardDate = $get("clnDate").value;
                         dStartHour = new Date(Number(sSidurDate.substr(6, 4)), Number(sSidurDate.substr(3, 2)) - 1, Number(sSidurDate.substr(0, 2)), Number(sSH.value.substr(0, 2)), Number(sSH.value.substr(3, 2)));
                         dEndHour = new Date(Number(sCardDate.substr(6, 4)), Number(sCardDate.substr(3, 2)) - 1, Number(sCardDate.substr(0, 2)), Number(sSG.value.substr(0, 2)), Number(sSG.value.substr(3, 2)));
                         if ($get("SD_txtDayAdd".concat(i)).value == "1")
                             dEndHour.setDate(dEndHour.getDate() + 1);
                         oDDL = $get("SD_ddlHashlama".concat(i));
                         if (oDDL.disabled == false){
                             SidurTime = GetSidurTime(dStartHour, dEndHour);
                             if ((SidurTime >= Number(oDDL.value)) && Number(oDDL.value > 0)){
                                 sMsg = sMsg.concat('בסידור  ' + iSidurNum + " משך הסידור שווה או גדול מזמן ההשלמה הנבחר \n");
                                 bValid = false;
                             }
                         }
                     }
                     //נעבור על פעילויות
                     res = ChkIfPeiluyotValid(i);
                     if (res == false) {
                         sMsg = sMsg.concat(' בסידור ' + iSidurNum + " קיימת פעילות ללא שעת יציאה/שעה לא חוקית  \n ");
                         bValid = false;
                     }
                 }
             }
        }       
        i=i+1;
        sSH=$get("SD_txtSH".concat(i));
        sSG=$get("SD_txtSG".concat(i));
     }    
     if (!bValid){     
        $find("MPClose").hide();
        $find("MPPrint").hide();
        alert(sMsg);
    } else { bScreenChanged = false; }
    if (bValid) {
        $get("btnApprove").disabled = true;
        $get("btnNotApprove").disabled = true;
    }
     return bValid;
   }
   if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();

   function ChkIfPeiluyotValid(iSidurInx){
       var sActualShatYetiza;
       _Peilut = $get("SD_" + padLeft(iSidurInx, '0', 3));
       if (_Peilut != null){
           for (var j = 1; j < _Peilut.firstChild.childNodes.length; j++) {
               sActualShatYetiza = _Peilut.firstChild.childNodes[j].cells[_COL_SHAT_YETIZA].firstChild;
               if (((sActualShatYetiza.value == "") || (!IsValidTime(sActualShatYetiza.value)))
                  && (_Peilut.firstChild.childNodes[j].cells[_COL_CANCEL_PEILUT].childNodes[0].value != "1"))                  
                   return false;                
           }
       }
       return true;
   }
   function GetErrorMessageSadotNosafim(id, level, pKey){    
       oId = id.id;
       var oObj = $get(oId);
       var rc = oObj.getAttribute("ErrCnt");
         if (Number(rc) > 0) {

           var iMisparIshi = $get("txtId").value;
           var dCardDate = $get("clnDate").value;
           var sFName = oObj.getAttribute("FName");
           var iSidurNum = 0; var sStartH = ''; var sPShatY = ''; var iMKnisa = 0;
           $get("hErrKey").value = oId;     
            if (level == 2) {          
                iSidurNum = $get("MisSidur").value;
                sStartH =  $get("ShatHatchala").value;
                wsGeneral.GetFieldErrors(level, iMisparIshi, dCardDate, iSidurNum, sStartH, sPShatY, iMKnisa, sFName, onGetdErrSucc);
            }
            else {
                var arrKey = pKey.toString().split("|");
                sPShatY = dCardDate + " " + arrKey[0];
                iMKnisa = arrKey[1];
                   
                iSidurNum = $get("MisSidur").value;
                sStartH =  $get("ShatHatchala").value; 
                wsGeneral.GetFieldErrors(level, iMisparIshi, dCardDate, iSidurNum, sStartH, sPShatY, iMKnisa, sFName, onGetdErrSucc);
            }
        }
    }   
function SetMeasher(iStatus)
{
    var iMisparIshi=$get("txtId").value;
    var dDate = $get("clnDate").value;
    wsGeneral.SetMeasherOMistayeg(Number(iMisparIshi), dDate, Number(iStatus), onMeasherSuccuss,null,iStatus);
}
function onMeasherSuccuss(result,iStatus)
{
    if (result=='0')   
        alert("אירעה שגיאה -סטטוס כרטיס לא התעדכן");
    else {        
        $get("btnPrint").disabled = false;
        $get("btnPrint").className = "btnWorkCardPrint";
        
        if(iStatus==1)
        {            
            $get("btnApprove").className = "ImgButtonApprovalChecked";
            $get("btnNotApprove").className = "ImgButtonDisApprovalCheckedDisabled";                                        
        }                           
        else
        {
            $get("btnApprove").className = "ImgButtonApprovalRegularDisabled";
            $get("btnNotApprove").className = "ImgButtonDisApproveChecked"; 
        }
    }
}
function CloseWindow()
{
  var EmpId = $get("txtId").value;
  var WCardDate = $get("clnDate").value;            
        
 switch ($get("hidSource").value)
 {
    case "1":
        window.location.href("EmployeeCards.aspx?EmpID=" + EmpId + "&WCardDate=" + WCardDate);
        break;
    case "2":
        window.location.href("EmployeTotalMonthly.aspx?EmpID=" + EmpId  +  "&WCardDate=" + WCardDate);
        break;
    default:
        window.close();
        break;        
 } 
} 
function chkTravelTime(){
     var _TravelTime = $get("ddlTravleTime");        
     var iMeafyenVal = _TravelTime.getAttribute("MeafyenVal");
     if ((((_TravelTime.value == "1") && (iMeafyenVal != "1") && (iMeafyenVal != "3")))
        || (((_TravelTime.value == "2") && (iMeafyenVal != "2") && (iMeafyenVal != "3")))
        || (((_TravelTime.value == "3") && (iMeafyenVal != "3")))) {
         _TravelTime.value = _TravelTime.getAttribute("OrgVal");
         alert('לא קיים מאפיין מתאים');
     }
     else{
         bScreenChanged = true;
         disableUpdateBtn(false);
   }
   SetLvlChg(1,0);
}
function isUserIdValid(){
    SetRefreshBtn(true);
    var EmpId = $get("txtId").value;
    var iAdmin=$get("hidGoremMeasher").value;
    wsGeneral.GetAdminEmployeeById(EmpId,0,iAdmin,onUsrValidSuccess);
}
function isUserNameValid(){
    SetRefreshBtn(true); 
    var EmpName = $get("txtName").value; 
    var iPos= EmpName.indexOf("(");   
    if (iPos!=-1){  
        EmpName = EmpName.substr(0,iPos-1);
    }
    var iAdmin=$get("hidGoremMeasher").value;
    wsGeneral.GetAdminEmployeeByName(EmpName,0,iAdmin, onUsrValidSuccess);
}
function SetNewDate(_Add){
    var sCardDate = $get("clnDate").value;
    var dCardDate = new Date(Number(sCardDate.substr(6, 4)), Number(sCardDate.substr(3, 2)) - 1, Number(sCardDate.substr(0, 2)), 0, 0);

    dCardDate.setDate(dCardDate.getDate() + Number(_Add));
    $get("clnDate").value = GetDateDDMMYYYY(dCardDate);
    $get("txtDay").value = GetHebrewDay(dCardDate.getDay());
}

function onUsrValidSuccess(result){
    if (result.length==0){
        alert('מספר אישי לא קיים/אינך מורשה לצפות בעובד זה');
        EnabledAllFrames(false);
        SetRefreshBtn(true);
    }
    else {
        if (result.length == 1) {
            if (result[0] == -1)
                unloadCard();
            else
                SetRefreshBtn(false);
        }
        else
            SetRefreshBtn(false);
    }
}

function onButtonFocusIn(btnID) {
  btnID.style.border = "1px solid black";
}
function onButtonFocusOut(btnID){
    btnID.style.border = "none";
}


//function IsSessionEnd() {   
//    GetSessionStatus();
//    setTimeout(IsSessionEnd(), 100000);
//}
//function GetSessionStatus() {
//    $.ajax({
//        type: "POST",
//        url: webserviceUrl + "/IsSessionEnd",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (data) {
//            alert(data.d);
//            // prepareStaticData(data.d);
//        },
//        error: function (result) {
//           
//        }
//    });
//}
function setBorderBtns(){
    var aButton = document.getElementsByTagName('Input');
    for (var i = 0; i < aButton.length; i++) {
        if (aButton[i].type == "button" || aButton[i].type == "submit") {
            aButton[i].onfocus = function () { onButtonFocusIn(this); };
            aButton[i].onfocusout = function () { onButtonFocusOut(this); };
        }
    } 
function onTxtIdPress() {
    var key = $get("txtId").value;
}}
