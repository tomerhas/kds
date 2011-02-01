var bScreenChanged = false;
    function OpenDiv(DivId, btnId){
        var oDiv = document.getElementById(DivId.toString());
        if (oDiv.style.display=='none'){        
            oDiv.style.display='block';  
            document.getElementById(btnId.toString()).value ='-';  
            
            switch (DivId){
            case "divEmployeeDetails":
                    document.getElementById("divNetunimLeYom").style.display='none'; 
                    document.getElementById("divParticipation").style.display='none';  
                    document.getElementById("btnPlus2").value ='+';  
                    document.getElementById("btnPlus3").value ='+';  
                break;
            case "divNetunimLeYom":
                    document.getElementById("divEmployeeDetails").style.display='none'; 
                    document.getElementById("divParticipation").style.display='none'; 
                    document.getElementById("btnPlus1").value ='+';  
                    document.getElementById("btnPlus3").value ='+';  
                break;
            case "divParticipation":
                    document.getElementById("divNetunimLeYom").style.display='none'; 
                    document.getElementById("divEmployeeDetails").style.display='none'; 
                    document.getElementById("btnPlus1").value ='+';  
                    document.getElementById("btnPlus2").value ='+';  
                break;
            }           
        }
        else{        
            oDiv.style.display='none';  
            document.getElementById(btnId.toString()).value ='+';                     
        }       
    }
    function onClientItemSelected_getID(sender, eventArgs) {
        document.getElementById("clnDate").select();
    }
    function onClientHiddenHandler_getID(sender, eventArgs){           
        GetOvedALLDetails(document.getElementById("txtId").value);
    }  
    function GetOvedNameSucceeded(result){    
       if ((result=='') || (result=='null')){
            alert('מספר אישי לא קיים');                        
            document.getElementById("txtId").select();
            document.getElementById("txtName").value='';
            document.getElementById("btnRefreshOvedDetails").disabled = true;        
        }
        else{
            document.getElementById("txtName").value=result;  
            document.getElementById("btnRefreshOvedDetails").disabled = false;         
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
            document.getElementById("txtCompany").value=root.childNodes.item(0).text;
           }
           if (root.childNodes.item(1)!=null){           
            document.getElementById("txtArea").value=root.childNodes.item(1).text;
           }
           if (root.childNodes.item(2)!=null){           
            document.getElementById("txtSnif").value=root.childNodes.item(2).text;
           }
           if (root.childNodes.item(3)!= null){    
            document.getElementById("txtMaamad").value=root.childNodes.item(3).text;
           }
           if (root.childNodes.item(4)!=null){           
            document.getElementById("txtIsuk").value=root.childNodes.item(4).text;           
           }
           if (root.childNodes.item(5)!=null){           
            document.getElementById("txtName").value=root.childNodes.item(5).text;  
           }
           if (root.childNodes.item(6)!=null){           
            document.getElementById("ddlTachograph").selectedValue = root.childNodes.item(6).text; 
           }
           if (root.childNodes.item(7)!=null){           
            document.getElementById("ddlHalbasha").selectedValue = root.childNodes.item(7).text; 
           }
           if (root.childNodes.item(8)!=null){           
             document.getElementById("ddlLina").selectedValue = root.childNodes.item(8).text;  
           }
           if (root.childNodes.item(9)){           
             document.getElementById("ddlTravleTime").selectedValue = root.childNodes.item(9).text;  
           }
            document.getElementById("btnRefreshOvedDetails").disabled = false;  
          }               
        }                
    }    
    function EnabledAllFrames(bEnable)
    {
        document.getElementById("tbEmpDetails").disabled = (!bEnable);
        document.getElementById("tbLblWorkDay").disabled = (!bEnable);
        document.getElementById("tbValWorkDay").disabled = (!bEnable);
        document.getElementById("tblPart").disabled = (!bEnable);
        document.getElementById("tbSidur").disabled = (!bEnable);
        document.getElementById("btnFindSidur").disabled = (!bEnable);
        document.getElementById("btnAddHeadrut").disabled = (!bEnable);        
        document.getElementById("btnClock").disabled = (!bEnable);
        document.getElementById("btnApprovalReport").disabled = (!bEnable);     
        document.getElementById("ddlTachograph").disabled = (!bEnable);
        document.getElementById("ddlHalbasha").disabled = (!bEnable);
        document.getElementById("ddlLina").disabled = (!bEnable);
        document.getElementById("ddlHashlamaReason").disabled = (!bEnable);
        document.getElementById("btnHashlamaForDay").disabled = (!bEnable);
        document.getElementById("btnCalcItem").disabled = (!bEnable);
        EnabledSidurimListBtn(!bEnable);   
    }
    function GetOvedALLDetails(iKodOved){
       var sCardDate;
       if (document.getElementById("clnDate").value!=''){                      
         sCardDate=document.getElementById("clnDate").value;
       }
       else{       
         sCardDate = getSysDate().toString();
       }       
       if (iKodOved!=''){
           wsGeneral.GetOvedAllDetails(iKodOved,sCardDate,GetOvedDetailsSucceeded);
           document.getElementById("txtId").value=iKodOved;    
           EnabledAllFrames(false);      
       }
    }    
    function GetOvedMisparIshiByName(){
      var sName = document.getElementById("txtName").value;
      if (sName.indexOf(")")==-1){                  
        document.getElementById("txtName").click();                          
      }                      
    }          
    function onClientHiddenHandler_getName(sender, eventArgs){    
     var iMisparIshi, iPos;
     var sOvedName=document.getElementById("txtName").value;     
     if (sOvedName!='')
     {
         iPos = sOvedName.indexOf('(');
         if (iPos!=-1)
         {
            iMisparIshi = sOvedName.substr(iPos+1, sOvedName.length-iPos-2);
            document.getElementById("txtId").value=iMisparIshi;
            GetOvedALLDetails(iMisparIshi);                   
         }
      }EnabledAllFrames(false);               
    }         
    function RefreshOvedDetails(){    
       if (document.getElementById("txtId").value!='')
       {
        GetOvedALLDetails(document.getElementById("txtId").value);
       } SetBtnChanges();       
    }
    function ShowRecivimCalculation(){
        var id = document.getElementById("txtId").value;
        var date = document.getElementById("clnDate").value;
        var sQuryString = "?id=" + id + "&date=" + date;
        document.getElementById("divHourglass").style.display = 'block'; 
        var ReturnWin = window.showModalDialog('Rechivim.aspx' + sQuryString, window, "dialogHeight: 400px; dialogWidth: 490px;dialogtop:200px;status:no;resizable:yes;");
        document.getElementById("divHourglass").style.display = 'none';
        if(ReturnWin=='' || ReturnWin=='undefined') ReturnWin=false;
        return ReturnWin;
    }
    function ShowLastUpdateUser(){
        var id = document.getElementById("txtId").value;
        var date = document.getElementById("clnDate").value;
        var sQuryString="?id=" + id + "&date=" + date;
        var ReturnWin = window.showModalDialog('MadknAcharon.aspx' + sQuryString, window, "dialogHeight: 280px; dialogWidth: 490px;dialogtop:200px;status:no;resizable:yes;");                         
        if(ReturnWin=='' || ReturnWin=='undefined') ReturnWin=false;
        return ReturnWin;   
    }
    function ShowEmployeeDetails(){    
        var id = document.getElementById("txtId").value;
        var date = String(document.getElementById("clnDate").value).substr(3);
        var sQuryString="?id=" + id + "&month=" + date;        
        var ReturnWin=window.showModalDialog('NetuneyOvedModal.aspx' + sQuryString , window , "dialogwidth:970px;dialogheight:580px;dialogtop:130px;dialogleft:25px;status:no;resizable:yes;scroll:0;");                          
        if(ReturnWin=='' || ReturnWin=='undefined') ReturnWin=false;
        return ReturnWin;   
    }  
    function RefreshCardData(){    
         bScreenChanged=false;
         document.getElementById("btnRefreshOvedDetails").click();
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
    function SetBtnChanges(){    
         bScreenChanged = true; document.getElementById("btnUpdateCard").disabled = false; 
         document.getElementById("btnRefreshOvedDetails").disabled = true;          
         document.getElementById("txtId").disabled = true;          
         document.getElementById("txtName").disabled = true;          
         document.getElementById("clnDate").disabled = true;            
    }
    function SetLvlChg(iLvl){
        var id = ("hidLvl".concat(String(iLvl))).concat("Chg");
        document.getElementById(id).value = "1";
    }    
    function CheckChanges(){    
      if (bScreenChanged) {        
        document.getElementById("btnHidClose").click();
        return false;
        }        
       else{     
        CloseWindow();return false;}      
    }
    function CheckChangesBeforePrint(){    
      if (bScreenChanged) {
        document.getElementById("btnHidPrint").click();
        return false;        
        }               
    }
    function RefreshBtn() {
        document.getElementById("btnPlus1").value = '+';
        document.getElementById("btnPlus2").value = '+';
        document.getElementById("btnPlus3").value = '+';
        document.getElementById("hidRefresh").value = "1";
        EnabledAllFrames(true);
        return true;
    }
    function CloseChgBtn(){
        bScreenChanged = false;return true;
    }
    function CheckIfCardExists(){
      var sDay = document.getElementById("clnDate").value.substr(0,2);
      var sMonth =  document.getElementById("clnDate").value.substr(3,2);
      var sFullYear = document.getElementById("clnDate").value.substr(6,4);         
      id = document.getElementById("txtId").value;
      date = document.getElementById("clnDate").value;
      if (date!=''){    
          wsGeneral.IsCardExists(id, date, callBackCheckCardExists);
          EnabledAllFrames(false);
      }      
    }
    function callBackCheckCardExists(result)
    {   var arrReturnValue = result.split("|"); 
        if (arrReturnValue[0]=="0")
        {
            var sBehaviorId='vldExBehavior';
            $find(sBehaviorId)._ensureCallout();
            $find(sBehaviorId).show(true);
            document.getElementById("btnRefreshOvedDetails").disabled = true;
            document.getElementById("txtDay").value ="";
        }
        else
        {
           document.getElementById("btnRefreshOvedDetails").disabled = false;         
           document.getElementById("txtDay").value = arrReturnValue[1];                    
        }
         document.getElementById("txtDay").title = arrReturnValue[1];
    }       
    function GetErrorMessage(id, level, pKey){   
        oId=id.id;       
    var oObj = document.getElementById(oId);
    var rc = oObj.getAttribute("ErrCnt");
    var arrKnisa; 
    if (Number(rc)>0)
    {
     var iMisparIshi = document.getElementById("txtId").value;
     var dCardDate = document.getElementById("clnDate").value;
     var sFName = oObj.getAttribute("FName");
     var iSidurNum=0; var sStartH=''; var sPShatY='';var iMKnisa = 0; 
     document.getElementById("hErrKey").value=oId;     
     if (level==1){
         wsGeneral.GetFieldErrors(level, iMisparIshi, dCardDate, iSidurNum, sStartH, sPShatY, iMKnisa, sFName, onGetdErrSucc);     
     }
     else
     {      
      if (level==2)
      {            
       var i = String(id.id).substr("lstSidurim_txtSH".length);
       iSidurNum=document.getElementById(("lstSidurim_lblSidur").concat(pKey)).innerHTML;
       sStartH = document.getElementById(("lstSidurim_txtSH").concat(pKey)).getAttribute("OrgShatHatchala");         
       wsGeneral.GetFieldErrors(level, iMisparIshi,dCardDate,iSidurNum,sStartH,sPShatY,iMKnisa,sFName, onGetdErrSucc);
      }
      else
      {
       var arrKey =  pKey.toString().split("|");
       sPShatY = document.getElementById(arrKey[0]).value;
       if (sPShatY == '__:__') {
           sPShatY = document.getElementById(arrKey[0]).getAttribute('OrgShatYetiza');           
       } else {
           var AddDay = Number(document.getElementById(arrKey[3].concat("_txtDayToAdd")).value);
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
       iMKnisa = document.getElementById(arrKey[1]).innerHTML;
       arrKnisa = iMKnisa.toString().split(",");
       iMKnisa = arrKnisa[0];
       iSidurNum = document.getElementById(("lstSidurim_lblSidur").concat(arrKey[2])).innerHTML;
       sStartH = document.getElementById(("lstSidurim_txtSH").concat(arrKey[2])).getAttribute("OrgShatHatchala");       
       wsGeneral.GetFieldErrors(level, iMisparIshi, dCardDate, iSidurNum, sStartH, sPShatY, iMKnisa, sFName, onGetdErrSucc);
      }
     }     
    }
   }    
    function onGetdErrSucc(result){    
        document.getElementById("tbErr").innerHTML = result;
        document.getElementById("btnErrors").click(); 
    }
    function ShowRemark(id)
    {
      var iMisparIshi = document.getElementById("txtId").value;
      var dCardDate = document.getElementById("clnDate").value;
      var iSidurNum =document.getElementById(("lstSidurim_lblSidur").concat(id)).innerHTML;
      var sStartH = document.getElementById(("lstSidurim_txtSH").concat(id)).getAttribute("OrgShatHatchala");      
      wsGeneral.GetLoLetashlumRemark(iMisparIshi,dCardDate,iSidurNum,sStartH,onGetdRmkSucc);      
    }
    function onGetdRmkSucc(result)
    {      
        document.getElementById("tblRmk").innerHTML = result;
        document.getElementById("btnRemark").click();          
    }
     function GetAppMsg(id){       
        if (id!=undefined){                                       
                 var olbl = document.getElementById("lblApp");
                 olbl.innerText = id.getAttribute("App");   
                 document.getElementById("btnApp").click();                 
        }     
     }
     function CancelError(id){
        $find("bMpeErr").hide();          
        iMisparIshi = document.getElementById("txtId").value;
        dCardDate = document.getElementById("clnDate").value;     
        sErrorId = id.parentNode.parentNode.children[3].innerText;  
        sMeasher =  document.getElementById("hidGoremMeasher").value;    
        wsGeneral.CancelError(iMisparIshi, dCardDate, sErrorId,sMeasher,callBackCancelError);      
     }
     function callBackCancelError(result){     
        if (result==1){SetBtnChanges();     
            var oObj = document.getElementById(document.getElementById("hErrKey").value);         
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
                document.getElementById(oObj.getAttribute("imgId")).style.display="none";}
              }
            }            
        }     
    }         
    function ApproveError(id){
        $find("bMpeErr").hide();    
        iMisparIshi = document.getElementById("txtId").value;
        dCardDate = document.getElementById("clnDate").value;
        sErrorId = id.parentNode.parentNode.children[3].innerText; 
        sMeasher =  document.getElementById("hidGoremMeasher").value;      
        wsGeneral.ApproveError(iMisparIshi, dCardDate, sErrorId,sMeasher);
        return false;
    }
    function AddSidurHeadrut() {
        if (bScreenChanged) {
            $("#hidSave")[0].value = "1";
            __doPostBack('btnConfirm', '');
        }
        
        var sQueryString;        
        sQueryString = "?dt=" + Date();
        sQueryString = sQueryString + "&MisparIshi=" + document.getElementById("txtId").value;
        sQueryString = sQueryString + "&DateCard=" + document.getElementById("clnDate").value;                
        sQueryString = sQueryString + "&MisparSidur="; 
        sQueryString = sQueryString + "&TimeStart=";
        sQueryString = sQueryString + "&TimeEnd=";
        document.getElementById("divHourglass").style.display = 'block';   
        res = window.showModalDialog('DivuachHeadrut.aspx?' + sQueryString, '', 'dialogwidth:580px;dialogheight:380px;dialogtop:280px;dialogleft:480px;status:no;resizable:no;');
        document.getElementById("divHourglass").style.display = 'none'; 
        if ((bScreenChanged) || ((res != undefined) && (res != '') && (!bScreenChanged))) {
            document.getElementById("hidExecInputChg").value = "1";
            document.getElementById("hidRefresh").value = "1";
            __doPostBack('btnAddHeadrut', '');
            bScreenChanged = false;
        }
        
        return false;
    }
    function AddSidur() {
        if (bScreenChanged) {            
            $("#hidSave")[0].value = "1";
            __doPostBack('btnConfirm', '');
        }
        
        var sQueryString;        
        sQueryString = "?dt=" + Date();
        sQueryString = sQueryString + "&EmpID=" + document.getElementById("txtId").value;
        sQueryString = sQueryString + "&CardDate=" + document.getElementById("clnDate").value;
        document.getElementById("divHourglass").style.display = 'block';
        res = window.showModalDialog('HosafatSidur.aspx?' + sQueryString, '', 'dialogwidth:1000px;dialogheight:600px;dialogtop:280px;dialogleft:180px;status:no;resizable:no;');
        document.getElementById("divHourglass").style.display = 'none'; 
        if ((bScreenChanged) || ((res != undefined) && (!bScreenChanged))) {
            document.getElementById("hidExecInputChg").value = "1";
            document.getElementById("hidRefresh").value = "1";
            __doPostBack('btnFindSidur', '');
            bScreenChanged = false;
        }
        return false;
    }
   function ShowDrvErr(){
       var iLaoved;
        if (document.getElementById("hidRashemet").value=="1")
            iLaoved="0";
           else
            iLaoved="1";
              
        var sQueryString;        
        sQueryString = "?dt=" + Date();
        sQueryString = sQueryString + "&EmpID=" + document.getElementById("txtId").value;
        sQueryString = sQueryString + "&CardDate=" + document.getElementById("clnDate").value;
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
        window.showModalDialog('ZmaniNesiot.aspx' + sQueryString, '', 'dialogwidth:650px;dialogheight:150px;dialogtop:200px;status:no;resizable:yes;');
    }
   function SetChgFlag(){
    document.getElementById("hidChanges").value=bScreenChanged;
    return true;
   }
   function ChkCardVld(){   
     var sXML,sSidurDate,dStartHour,dEndHour,SidurTime;   
     var bValid=true;var sMsg='';var sCallBack='';     
     var HashForDay = document.getElementById("HashlamaForDayValue").value;
     var HashReason = document.getElementById("ddlHashlamaReason").value;
     document.getElementById("hidExecInputChg").value="0";
     if ((Number(HashForDay)==1) && (Number(HashReason) == -1)){      
        sMsg ='סומנה השלמה ליום, יש לדווח סיבה'; 
        bValid = false;
     }
     if ((Number(HashForDay)!=1) && (Number(HashReason) > 0)){      
        sMsg =sMsg.concat('סומנה סיבת השלמה ללא סימון השלמה');
        bValid = false;
     }  

     var i=0, sSH, sSG, oDDL;
     sSH=document.getElementById("lstSidurim_txtSH".concat(i));
     sSG=document.getElementById("lstSidurim_txtSG".concat(i));
    
     while (sSH!=null)
     {
        iSidurNum = document.getElementById(("lstSidurim_lblSidur").concat(i)).innerHTML;
        oDDL = document.getElementById("lstSidurim_ddlResonIn".concat(i));        
        if (!IsValidTime(sSH.value))
        {           
           sMsg = sMsg.concat('שעת התחלה בסידור ' + iSidurNum + " אינה תקינה \n");   
           bValid = false; 
        }
        if (oDDL!=null){
            if ((sSH.value!='') && (sSH.value!="__:__")  && (oDDL.value==-1) && (oDDL.disabled == false))
            {                
               sMsg = sMsg.concat('בסידור  ' + iSidurNum + " דווחה שעת התחלה, יש לדווח סיבה \n");  
               bValid = false; 
            }
            oDDL = document.getElementById("lstSidurim_ddlResonOut".concat(i));        
            if (!IsValidTime(sSG.value))
            {            
               sMsg = sMsg.concat('שעת גמר בסידור ' + iSidurNum + " אינה תקינה \n");
               bValid = false; 
            }
            if ((sSG.value!='') && (sSG.value!="__:__")  && (oDDL.value==-1) && (oDDL.disabled == false))
            {
               sMsg = sMsg.concat('בסידור  ' + iSidurNum + " דווחה שעת גמר, יש לדווח סיבה \n");             
               bValid = false; 
            }   
            sCallBack=chkPitzulHafsaka(i,true);
            if ((sCallBack!='') && (sCallBack!=undefined)){
              sMsg = sMsg.concat(sCallBack + "\n");
              bValid = false;
            }
            if ((sSG.value!='') && (sSG.value!="__:__") && (sSH.value!='') && (sSH.value!="__:__")){
                 sSidurDate = document.getElementById("lstSidurim_lblDate".concat(i)).innerHTML;
                 sCardDate = document.getElementById("clnDate").value;          
                 dStartHour = new Date(Number(sSidurDate.substr(6,4)),Number(sSidurDate.substr(3,2))-1, Number(sSidurDate.substr(0,2)),Number(sSH.value.substr(0,2)),Number(sSH.value.substr(3,2)));
                 dEndHour = new Date(Number(sCardDate.substr(6, 4)), Number(sCardDate.substr(3, 2)) - 1, Number(sCardDate.substr(0, 2)), Number(sSG.value.substr(0, 2)), Number(sSG.value.substr(3, 2)));
                 if (document.getElementById("lstSidurim_txtDayAdd".concat(i)).value == "1")                   
                    dEndHour.setDate(dEndHour.getDate() + 1);
                 oDDL = document.getElementById("lstSidurim_ddlHashlama".concat(i));
                 if (oDDL.disabled==false){
                     SidurTime = GetSidurTime(dStartHour,dEndHour);  
                     if ((SidurTime>=Number(oDDL.value)) && Number(oDDL.value>0)) {
                        sMsg = sMsg.concat('בסידור  ' + iSidurNum + " משך הסידור שווה או גדול מזמן ההשלמה הנבחר \n");             
                        bValid = false; 
                     }
                }     
            }
        }       
        i=i+1;
        sSH=document.getElementById("lstSidurim_txtSH".concat(i));
        sSG=document.getElementById("lstSidurim_txtSG".concat(i));
     }    
     if (!bValid){     
        $find("MPClose").hide();
        $find("MPPrint").hide();
        alert(sMsg);
     }else{bScreenChanged=false;}
     return bValid;
   }
   if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();

   function GetErrorMessageSadotNosafim(id, level, pKey) {    
       oId = id.id;
       var oObj = document.getElementById(oId);
       var rc = oObj.getAttribute("ErrCnt");
         if (Number(rc) > 0) {

           var iMisparIshi = document.getElementById("txtId").value;
           var dCardDate = document.getElementById("clnDate").value;
           var sFName = oObj.getAttribute("FName");
           var iSidurNum = 0; var sStartH = ''; var sPShatY = ''; var iMKnisa = 0;
           document.getElementById("hErrKey").value = oId;     
            if (level == 2) {          
                iSidurNum = document.getElementById("MisSidur").value;
                sStartH =  document.getElementById("ShatHatchala").value;
                wsGeneral.GetFieldErrors(level, iMisparIshi, dCardDate, iSidurNum, sStartH, sPShatY, iMKnisa, sFName, onGetdErrSucc);
            }
            else {
                var arrKey = pKey.toString().split("|");
                sPShatY = dCardDate + " " + arrKey[0];
                iMKnisa = arrKey[1];
                   
                iSidurNum = document.getElementById("MisSidur").value;
                sStartH =  document.getElementById("ShatHatchala").value; 
                wsGeneral.GetFieldErrors(level, iMisparIshi, dCardDate, iSidurNum, sStartH, sPShatY, iMKnisa, sFName, onGetdErrSucc);
            }
        }
   }
function SetMeasher(iStatus)
{
    var iMisparIshi=document.getElementById("txtId").value;
    var dDate = document.getElementById("clnDate").value;
    wsGeneral.SetMeasherOMistayeg(Number(iMisparIshi), dDate, Number(iStatus), onMeasherSuccuss,null,iStatus);
}
function onMeasherSuccuss(result,iStatus)
{
    if (result=='0')
    {
        alert("אירעה שגיאה -סטטוס כרטיס לא התעדכן");
    }
    else
    {
        document.getElementById("btnPrint").disabled=false;
        if(iStatus==1)
        {
         document.getElementById("btnApprove").className = "ImgButtonApprovalChecked";        
         document.getElementById("btnNotApprove").className = "ImgButtonApprovalRegular";        
        }
        else
        {
           document.getElementById("btnNotApprove").className = "ImgButtonApprovalChecked";
           document.getElementById("btnApprove").className = "ImgButtonApprovalRegular";    
        }
    }
}
function CloseWindow()
{
  var EmpId = document.getElementById("txtId").value;
  var WCardDate = document.getElementById("clnDate").value;            
        
 switch (document.getElementById("hidSource").value)
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
     var _TravelTime = document.getElementById("ddlTravleTime");        
     var iMeafyenVal = _TravelTime.getAttribute("MeafyenVal");
     if ((((_TravelTime.value == "1") && (iMeafyenVal != "1") && (iMeafyenVal != "3")))
        || (((_TravelTime.value == "2") && (iMeafyenVal != "2") && (iMeafyenVal != "3")))
        || (((_TravelTime.value == "3") && (iMeafyenVal != "3")))) {
         _TravelTime.value = _TravelTime.getAttribute("OrgVal");
         alert('לא קיים מאפיין מתאים');
     }
     else{       
       bScreenChanged = true; document.getElementById("btnUpdateCard").disabled = false;
   }
   SetLvlChg(1);
}
function isUserIdValid(){  
    document.getElementById("btnRefreshOvedDetails").disabled = true;  
    var EmpId = document.getElementById("txtId").value;
    var iAdmin=document.getElementById("hidGoremMeasher").value;
    wsGeneral.GetAdminEmployeeById(EmpId,0,iAdmin,onUsrValidSuccess);
}
function isUserNameValid(){  
    document.getElementById("btnRefreshOvedDetails").disabled = true;  
    var EmpName = document.getElementById("txtName").value; 
    var iPos= EmpName.indexOf("(");   
    if (iPos!=-1){  
        EmpName = EmpName.substr(0,iPos-1);
    }
    var iAdmin=document.getElementById("hidGoremMeasher").value;
    wsGeneral.GetAdminEmployeeByName(EmpName,0,iAdmin, onUsrValidSuccess);
}

function onUsrValidSuccess(result){
    if (result.length==0){
        alert('מספר אישי לא קיים/אינך מורשה לצפות בעובד זה');
        EnabledAllFrames(false);
        document.getElementById("btnRefreshOvedDetails").disabled = true;  
    }
    else{
        document.getElementById("btnRefreshOvedDetails").disabled = false;  
    }
}
function onButtonFocusIn(btnID) {
  btnID.style.border = "1px solid black";
}
function onButtonFocusOut(btnID) {
    btnID.style.border = "none";
}
function setBorderBtns() {
    var aButton = document.getElementsByTagName('Input');
    for (var i = 0; i < aButton.length; i++) {
        if (aButton[i].type == "button" || aButton[i].type == "submit") {
            aButton[i].onfocus = function () { onButtonFocusIn(this); };
            aButton[i].onfocusout = function () { onButtonFocusOut(this); };
        }
    }
}