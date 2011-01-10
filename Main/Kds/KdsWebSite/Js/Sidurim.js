var oGridRowId;
var MKT_VISUT = 4;
var MKT_VISA = 6;
var MKT_SHERUT = 1;
var MKT_EMPTY = 2;
var MKT_NAMAK = 3;
var MKT_ELEMENT = 5;

    function chkMkt(oRow){    
       oRId = String(oRow.id).substr(0,oRow.id.length-6);
       var oMkt = document.getElementById(oRId).cells[_COL_MAKAT].childNodes[0];
       var lNMkt = oMkt.value; 
       var lOMkt = oMkt.getAttribute("OrgMakat");       
       var sArrPrm= new Array(7); 
       var iMktType;

       if (lNMkt<100000){
          alert('מספר מק"ט לא תקין, יש להקליד מספר בין 6-8 ספרות');
          oMkt.select();}
        else{
                iMktType = GetMakatType(lNMkt);
                if ((iMktType == MKT_VISA) || ((iMktType == MKT_ELEMENT) && (isElementMechona(lNMkt))) || ((iMktType == MKT_VISUT))) {
                    oMkt.value = lOMkt;
                    oMkt.select();
                    alert('לא ניתן להקליד מק"ט ויזה או ויסות או הכנת מכונה');
                }
                else {
                    var iInx = Number(String(oRow.id).substr(String('lstSidurim_').length, 3));
                    var oDate = document.getElementById('lstSidurim_lblDate'.concat(iInx)).firstChild.nodeValue;
                    var iSidur = document.getElementById('lstSidurim_lblSidur'.concat(iInx)).firstChild.nodeValue;
                    var iSidurVisa = document.getElementById('lstSidurim_lblSidur'.concat(iInx)).getAttribute("SidurVisa");
                    SetBtnChanges(); SetLvlChg(3);
                    if ((lNMkt != '') && (Number(lNMkt != 0))) {
                        var oShatYetiza = document.getElementById(oRId).cells[_COL_SHAT_YETIZA].childNodes[0].value;
                        var oDayToAdd = document.getElementById(oRId).cells[_COL_DAY_TO_ADD].childNodes[0].value;
                        sArrPrm[0] = oRId;
                        sArrPrm[1] = iSidur;
                        sArrPrm[2] = iSidurVisa;
                        sArrPrm[3] = document.getElementById('lstSidurim_lblSidur'.concat(iInx)).getAttribute("Sidur93");
                        sArrPrm[4] = document.getElementById('lstSidurim_lblSidur'.concat(iInx)).getAttribute("Sidur94");
                        sArrPrm[5] = document.getElementById('lstSidurim_lblSidur'.concat(iInx)).getAttribute("Sidur95");
                        sArrPrm[6] = lOMkt;
                        sArrPrm[7] = iInx;
                        wsGeneral.CheckMakat(lNMkt, lOMkt, oDate, oShatYetiza, oDayToAdd, callBackMkt, null, sArrPrm);
                    }
                }                         
          }                       
    }
    function callBackMkt(result,sArrPrm)
    {   
        var xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
        xmlDoc.async="false";
        xmlDoc.loadXML(result);
        root=xmlDoc.documentElement;
        var sMeafyen6='0';
        var sMeafyen7='0';
        var bMeafyen6,bMeafyen7;
        var bExist=false;
        var oRId=sArrPrm[0]; 
        var iSidur=sArrPrm[1]; 
        var iSidurVisa=sArrPrm[2]; 
        var lOMkt=sArrPrm[6]; 
        var lNewMkt=document.getElementById(oRId).cells[_COL_MAKAT].childNodes[0].value;
        var iElementVal = lOMkt.substr(1,2);
        var iSidurIndex = sArrPrm[7];
        //אם לסידור יש מאפיין 93,94,95 אחד מהן לפחות ומשנים אלמנט שהספרות 2-3 שלו שוות לערך המאפיין
        //לא ניתן לשנות את מספר האלמנט
        if (Number(lOMkt.substr(0,1))==7){
            if (((sArrPrm[3]==iElementVal) && (Number(sArrPrm[3])!=0)) || ((sArrPrm[4]==iElementVal) && (Number(sArrPrm[4])!=0)) || ((sArrPrm[5]==iElementVal) && (Number(sArrPrm[5])!=0))){                 
                if (((lOMkt.substr(0,3)==lNewMkt.substr(0,3)))){}
                else
                {                    
                    document.getElementById(oRId).cells[_COL_MAKAT].childNodes[0].value=lOMkt;
                    bExist=true;
                    alert((' אלמנט '.concat(lOMkt)).concat(' חובה בסידור זה'));
                }
            }
        }
        if (root!=null)
        {                
          if (root.childNodes.length>0)
          {
             var _FirstChild = root.firstChild;
             while ((_FirstChild!=null) && (!bExist))
             {
                switch (_FirstChild.nodeName){                
                     case "KISUY_TOR":
                          document.getElementById(oRId).cells[_COL_KISUY_TOR].childNodes[0].value = _FirstChild.text;                                              
                          break;
                     case "KISUY_TOR_ENABLED":
                          document.getElementById(oRId).cells[_COL_KISUY_TOR].childNodes[0].disabled =(_FirstChild.text=="0");
                          break;
                      case "KISUY_TOR_MAP":
                           document.getElementById(oRId).cells[_COL_KISUY_TOR].childNodes[0].setAttribute("OldTorMapV", _FirstChild.text);
                           break;
                     case "DESC":
                          if (document.getElementById(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].nodeValue==null)
                            document.getElementById(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].firstChild.nodeValue=_FirstChild.text;
                           else
                            document.getElementById(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].nodeValue=_FirstChild.text;
                          break;
                     case "SHILUT":
                           document.getElementById(oRId).cells[_COL_LINE].childNodes[0].nodeValue= _FirstChild.text;
                           break;
                     case "SHILUT_NAME":      
                           document.getElementById(oRId).cells[_COL_LINE_TYPE].childNodes[0].nodeValue= _FirstChild.text;
                           break;
                     case "DAKOT_DEF":
                           document.getElementById(oRId).cells[_COL_DEF_MINUTES].childNodes[0].nodeValue=_FirstChild.text;
                           break;
                     case "DAKOT_DEF_TITLE":
                           document.getElementById(oRId).cells[_COL_DEF_MINUTES].title=_FirstChild.text;
                           break;
                     case  "DAKOT_BAFOAL":
                           document.getElementById(oRId).cells[_COL_ACTUAL_MINUTES].childNodes[0].value=_FirstChild.text;   
                           document.getElementById(oRId).cells[_COL_ACTUAL_MINUTES].childNodes[1].errormessage="יש להקליד ערך בין 0 ל -".concat(document.getElementById(oRId).cells[_COL_DEF_MINUTES].childNodes[0].nodeValue) + " דקות ";         
                           break;
                     case "DAKOT_BAFOAL_ENABLED":
                           document.getElementById(oRId).cells[_COL_ACTUAL_MINUTES].childNodes[0].disabled=(_FirstChild.text=="0");  
                           break;
                     case "OTO_NO":
                           document.getElementById(oRId).cells[_COL_CAR_NUMBER].childNodes[0].value="";
                           break;      
                     case "OTO_NO_ENABLED":
                           document.getElementById(oRId).cells[_COL_CAR_NUMBER].childNodes[0].disabled=(_FirstChild.text=="0");
                           break;
                     case "OTO_NO_TITEL":
                           document.getElementById(oRId).cells[_COL_CAR_NUMBER].childNodes[0].title=_FirstChild.text;
                           break;
                     case "SHAT_YETIZA":
                           document.getElementById(oRId).cells[_COL_SHAT_YETIZA].childNodes[0].value="";
                           break;      
                     case "SHAT_YETIZA_ENABLED":
                           document.getElementById(oRId).cells[_COL_SHAT_YETIZA].childNodes[0].disabled=(_FirstChild.text=="0");
                           break;
                       case "MAKAT_NOT_EXIST":
                           document.getElementById(oRId).cells[_COL_MAKAT].childNodes[0].value = lOMkt;
                           document.getElementById(oRId).cells[_COL_MAKAT].childNodes[0].select();
                           bExist = true;
                           alert("אלמנט לא קיים");
                           break;
                     case "MEAFYEN6ERR":
                            sMeafyen6=_FirstChild.text;
                            bMeafyen6 = true;
                            break;
                     case "MEAFYEN7ERR":
                            sMeafyen7=_FirstChild.text;
                            bMeafyen7 = true;
                            break;
                        case "HYPER_LINK":
                            if (_FirstChild.text == "1") {
                                if (document.getElementById(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].nodeValue == null)
                                    document.getElementById(oRId).cells[_COL_LINE_DESCRIPTION].innerHTML = "<a onclick='AddHosafatKnisot(" + iSidurIndex + "," + document.getElementById(oRId).id + ");' style='text-decoration:underline;cursor:pointer;'>" + document.getElementById(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].firstChild.nodeValue + "</a>"; //"<".concat(_FirstChild.text) + "</a>";
                                else
                                    document.getElementById(oRId).cells[_COL_LINE_DESCRIPTION].innerHTML = "<a onclick='AddHosafatKnisot(" + iSidurIndex + "," + document.getElementById(oRId).id + ");' style='text-decoration:underline;cursor:pointer;'>" + document.getElementById(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].nodeValue + "</a>"; // "<".concat(_FirstChild.text) + "</a>";                            
                            }
                            else
                                if (document.getElementById(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].outerHTML != null) {
                                    if ((document.getElementById(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].outerHTML.toUpperCase().indexOf('<A')) > -1)
                                        if (document.getElementById(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].nodeValue == null)
                                            document.getElementById(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].outerHTML = document.getElementById(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].firstChild.nodeValue;
                                        else
                                            document.getElementById(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].outerHTML = document.getElementById(oRId).cells[_COL_LINE_DESCRIPTION].childNodes[0].nodeValue;
                                }
                            break;
                }
                _FirstChild = _FirstChild.nextSibling;                
             }      
             if ((!bExist)){                  
                 document.getElementById(oRId).cells[_COL_NETZER].childNodes[0].nodeValue='לא';          
                 document.getElementById(oRId).cells[_COL_ACTUAL_MINUTES].childNodes[0].nodeValue='';                           
                 if ((bMeafyen6) || (bMeafyen7)){
                    alert('יש להקליד ערך בתחום: ' + sMeafyen6 + "עד " + sMeafyen7);
                 }
             }
          }
          else{          
            var sBehaviorId='vldMakatNumBeh'.concat(oRId);
            $find(sBehaviorId)._ensureCallout();
            $find(sBehaviorId).show(true);
          }
       } 
     }                                       
 
    function chkHashlama(val,args){
        var id = val.getAttribute("index");   
        var oTxt1 = document.getElementById("lstSidurim_txtSH".concat(id)).value;
        var oTxt2 = document.getElementById("lstSidurim_txtSG".concat(id)).value;
        var sSidurDate = document.getElementById("lstSidurim_lblDate".concat(id)).innerHTML;
        var sCardDate = document.getElementById("clnDate").value;      
        var dStartHour = new Date(Number(sSidurDate.substr(6,4)),Number(sSidurDate.substr(3,2))-1, Number(sSidurDate.substr(0,2)),Number(oTxt1.substr(0,2)),Number(oTxt1.substr(3,2)));
        var dEndHour = new Date(Number(sCardDate.substr(6, 4)), Number(sCardDate.substr(3, 2)) - 1, Number(sCardDate.substr(0, 2)), Number(oTxt2.substr(0, 2)), Number(oTxt2.substr(3, 2))); 
        var oDDL= document.getElementById("lstSidurim_ddlHashlama".concat(id));
        var _Add = document.getElementById("lstSidurim_txtDayAdd".concat(id)).value;
        if ((Number(_Add)==1))        
            dEndHour.setDate(dEndHour.getDate() + 1);
                       
        SidurTime = GetSidurTime(dStartHour,dEndHour);  
        args.IsValid = ((Number(oDDL.value) > SidurTime) ||  (Number(oDDL.value<=0)));    
    }         
    function Test(val,args){}    
    function ChkOto(oRow){  
       oId = String(oRow.id).substr(0,oRow.id.length-6); 
       var lOtoNo = document.getElementById(oId).cells[_COL_CAR_NUMBER].childNodes[0].value;  
       SetBtnChanges();SetLvlChg(3);
       if (lOtoNo!=''){
        wsGeneral.CheckOtoNo(lOtoNo, callBackOto,null,oId);  
      }
    }    
    function callBackOto(result,oId){    
        if (result=='0'){        
            var sBehaviorId='vldCarNumBehv'.concat(oId);
            $find(sBehaviorId)._ensureCallout();
            $find(sBehaviorId).show(true);
            document.getElementById(oId).cells[_COL_CAR_NUMBER].childNodes[0].title= "מספר רכב שגוי";
            //document.getElementById(oId).cells[_COL_CAR_LICESNCE_NUMBER].childNodes[0].nodeValue='0';                 
        }
        else{
        document.getElementById(oId).cells[_COL_CAR_NUMBER].childNodes[0].title=result;
        }
        document.getElementById(oId).cells[_COL_CAR_NUMBER].childNodes[0].select();
    }
    function ChangeStatusPeilut(Row, FirstMkt, OrgMktType, SubMkt)
    {    SetBtnChanges();
         var oColCancel, oColPeilutCancel;
         var iSidur=0;         
         var _NextRow;
         var arrKnisa, lMkt, lNxtMkt;
         
         //נבצע רק אם שורה נוכחית או שכניסות/ויסותים/ אלמנט הפסקה
         if ((FirstMkt == 0) || ((SubMkt == 1) && (FirstMkt!=0))) {
             oColCancel = document.getElementById(Row.id).cells[_COL_CANCEL].childNodes[0];
             oColPeilutCancel = document.getElementById(Row.id).cells[_COL_CANCEL_PEILUT].childNodes[0];
             if ((oColCancel.className == "ImgChecked") || (oColCancel.className == "ImgCheckedDisable")){
                 SetPeilutStatus(Row.id, true, iSidur);
                 oColCancel.className = "ImgCancel";
                 oColPeilutCancel.value = "1";
             }
             else {
                 SetPeilutStatus(Row.id, false, iSidur);
                 oColCancel.className = "ImgChecked";
                 oColPeilutCancel.value = "0";
             }             
         }

        FirstMkt = FirstMkt + 1;
        if (Number(FirstMkt) == 1) {
            lMkt = (document.getElementById(Row.id).cells[_COL_MAKAT].childNodes[0].value);
            //אם קו שירות, נבטל גם ויסותים וכניסות של אותו קו
            if (((Number(lMkt >= 100000)) && (Number(lMkt < 50000000))) && (String(document.getElementById(Row.id).cells[_COL_KNISA].childNodes[0].nodeValue).split(',')[0] == 0))
                OrgMktType = 1;                        
        }
        //אם במקור ביטלנו קו שירות נבטל גם כניסות וויסותים
        if (((Number(OrgMktType) == 1)) && (SubMkt!=2)){
            if (Row.id != ''){
                _NextRow = document.getElementById(Row.id).nextSibling;
                if (_NextRow != null) {
                    if (_NextRow.cells[_COL_KNISA].childNodes[0].nodeValue != '') {
                        arrKnisa = String(_NextRow.cells[_COL_KNISA].childNodes[0].nodeValue).split(',');
                        lNxtMkt = Number(_NextRow.cells[_COL_MAKAT].childNodes[0].value);
                    } //ויסותים,כניסות ואלמנט הפסקה
                    if (((Number(arrKnisa[0]) > 0)) || ((lNxtMkt >= 70000000) && (lNxtMkt <= 70099999)) || (((lNxtMkt >= 74300000) && (lNxtMkt <= 74399999))))
                        ChangeStatusPeilut(_NextRow, FirstMkt, OrgMktType, 1);
                    else {
                        lNxtMkt = Number(_NextRow.cells[_COL_MAKAT].childNodes[0].value);
                        //אם הגענו לקו שירות נעצור
                        if ((((Number(lNxtMkt >= 100000)) && (Number(lNxtMkt < 50000000))) && (String(_NextRow.cells[_COL_KNISA].childNodes[0].nodeValue).split(',')[0] == 0)))
                            ChangeStatusPeilut(_NextRow, FirstMkt, OrgMktType, 2);//קן שירות- תנאי עצירה
                          else
                            ChangeStatusPeilut(_NextRow, FirstMkt, OrgMktType, 0);
                    }
                }
            }
        }
        return false;
    }    
    function ChangeStatusSidur(id){      
     var iIndex = String(id).substr(String(id).length-1,1);
     SetBtnChanges();SetLvlChg(2);
     if (document.getElementById(id).className == "ImgChecked")
     {
        SetSidurStatus(iIndex,true);       
        document.getElementById(id).className = "ImgCancel"
        document.getElementById("lstSidurim_lblSidurCanceled".concat(iIndex)).value="1";
     }
     else
     {
        SetSidurStatus(iIndex,false);       
        document.getElementById(id).className = "ImgChecked";
        document.getElementById("lstSidurim_lblSidurCanceled".concat(iIndex)).value="0";
     }
     return false;
    }    
    function SetPeilutStatus(RowId,bFlag, iSidur)
    {   SetBtnChanges();SetLvlChg(3);
        var oRow=document.getElementById(RowId);
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
        if (oColCancel.className!=undefined){               
        if (bFlag){
            oColCancel.className = "ImgCancel";oColPeilutCancel.value="1";
            
        }else{        
            oColCancel.className = "ImgChecked";oColPeilutCancel.value="0";            
         }
       }       
    }
    function SetLabelColor(sCtl,iIndex, sColor){        
        document.getElementById(sCtl.concat(iIndex)).style.color=sColor;  
    }
    function EnableField(sCtl,iIndex,bFlag){
        if ((document.getElementById(sCtl.concat(iIndex)).getAttribute("OrgEnabled"))=="1"){     
        document.getElementById(sCtl.concat(iIndex)).disabled=bFlag;} 
    }
    function SetSidurStatus(iIndex, bFlag){
     MovePanel(iIndex);  
     if (bFlag){
        SetLabelColor("lstSidurim_lblSidur",iIndex,"gray");
        SetLabelColor("lstSidurim_txtSHL",iIndex,"gray");
        SetLabelColor("lstSidurim_txtSGL",iIndex,"gray");     
     }
     else{     
       SetLabelColor("lstSidurim_lblSidur",iIndex,"black");
       SetLabelColor("lstSidurim_txtSHL",iIndex,"black");
       SetLabelColor("lstSidurim_txtSGL",iIndex,"black");    
     }
     document.getElementById("lstSidurim_pnlContent".concat(iIndex)).disabled=bFlag;  
     EnableField("lstSidurim_ddlHashlama",iIndex,bFlag);
     EnableField("lstSidurim_ddlPHfsaka",iIndex,bFlag);
     EnableField("lstSidurim_ddlException",iIndex,bFlag);    
     EnableField("lstSidurim_chkOutMichsa",iIndex,bFlag);   
     EnableField("lstSidurim_ddlResonOut",iIndex,bFlag);   
     EnableField("lstSidurim_ddlResonIn",iIndex,bFlag); 
     EnableField("lstSidurim_chkLoLetashlum",iIndex,bFlag);        
     EnableField("lstSidurim_txtSH",iIndex,bFlag);
     EnableField("lstSidurim_txtSG", iIndex, bFlag);
     EnableField("lstSidurim_txtSHL", iIndex, bFlag);
     EnableField("lstSidurim_txtSGL", iIndex, bFlag); 
     
     if (document.getElementById("lstSidurim_cImgS".concat(iIndex))!=null){  
        document.getElementById("lstSidurim_cImgS".concat(iIndex)).disabled = bFlag;}
     var sIndex = String("00".concat(String(iIndex)));
     sIndex = sIndex.substr(sIndex.length-3,3);
     var oGrid = document.getElementById("lstSidurim_".concat(sIndex));
     if (oGrid!=null){     
         for (i=1; i<oGrid.rows.length; i++){ 
          if ((oGrid.rows[i].id)!=''){        
          SetPeilutStatus(oGrid.rows[i].id,bFlag,1);}
      } 
     } 
    }

    function MovePanel(iIndex) {          
        if ($find("cPanel".concat(iIndex)) != null) {
            if ($find("cPanel".concat(iIndex))._collapsed == false)            
                $find("cPanel".concat(iIndex))._collapsed = true;
            else
                $find("cPanel".concat(iIndex))._collapsed = false;                
        }             
    }
    function closePanel(iIndex){  
        if ($find("cPanel".concat(iIndex))!=null){                           
        $find("cPanel".concat(iIndex))._collapsed = false;
        $find("cPanel".concat(iIndex))._doOpen();}              
    }
    function openPanel(iIndex){  
        if ($find("cPanel".concat(iIndex))){
        $find("cPanel".concat(iIndex))._doOpen();}
    }    
    function ChkExitHour(val,args)
    {   SetBtnChanges();SetLvlChg(3);
        if (document.getElementById("clnDate").value!='')
        { var dShatYetiza = new Date();                                             
          var dSExitHour = new Date();     
          var dEExitHour = new Date();              
          var sGridRowID = String(val.id).substr(0,val.id.indexOf('vldPeilutShatYetiza')-1);
          var sActualShatYetiza = document.getElementById(sGridRowID).cells[_COL_SHAT_YETIZA].childNodes[0].value;
          var iPDayToAdd = document.getElementById(sGridRowID).cells[_COL_DAY_TO_ADD].childNodes[0].value 
          var sParam29 = document.getElementById("lstSidurim_hidParam29").value;   
          var sEndValidHour = "23:59";
          var sCardDate = document.getElementById("clnDate").value;        
          if(IsValidTime(sActualShatYetiza)){                     
              dShatYetiza.setHours(sActualShatYetiza.substr(0,2)); 
              dShatYetiza.setMinutes(sActualShatYetiza.substr(sActualShatYetiza.length-2,2));             
              var dCardDate = new Date(Number(sCardDate.substr(6,4)), Number(sCardDate.substr(3,2))-1,Number(sCardDate.substr(0,2)),0,0);            
              var iCollpaseHeaderIndex = Number(String(val.id).substr(String('lstSidurim_').length,3)); 
              var sSidurDate = document.getElementById("lstSidurim_lblDate".concat(iCollpaseHeaderIndex)).innerHTML;          
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
                  iSDayToAdd = document.getElementById("lstSidurim_txtDayAdd".concat(iCollpaseHeaderIndex)).value;
                  var sSG = document.getElementById("lstSidurim_txtSG".concat(iCollpaseHeaderIndex)).value;
                  dSidurDate = new Date(Number(sSidurDate.substr(6, 4)), Number(sSidurDate.substr(3, 2)) - 1, Number(sSidurDate.substr(0, 2)), sSG.substr(0, 2), sSG.substr(3, 2));
                  var utcSidurSG = Date.UTC(dSidurDate.getFullYear(), dSidurDate.getMonth() + 1, dSidurDate.getDate(), 0, 0, 0);
                  if (utcSidurSG == utcCardDate)
                      dSidurDate.setDate(dSidurDate.getDate() + Number(iSDayToAdd));
                  else
                      if (Number(iSDayToAdd) == 0)
                          dSidurDate.setDate(dSidurDate.getDate() - 1);

                  dShatYetiza = new Date(Number(sSidurDate.substr(6, 4)), Number(sSidurDate.substr(3, 2)) - 1, Number(sSidurDate.substr(0, 2)), sActualShatYetiza.substr(0, 2), sActualShatYetiza.substr(sActualShatYetiza.length - 2, 2));

                  sParamNxtDay = document.getElementById("lstSidurim_hidParam242").value;                 
                  var sYear = sCardDate.substr(sCardDate.length - 4, 4);
                  var sMonth = Number(sCardDate.substr(3, 2)) - 1;
                  var sDay = sCardDate.substr(0, 2);
                  var dParamDate = new Date();var dItemDate = new Date();
                  SetDate(dParamDate, Number(sYear), Number(sMonth), Number(sDay), Number(sParamNxtDay.substr(0, 2)), Number(sParamNxtDay.substr(3, 2)));
                  SetDate(dItemDate, Number(sYear), Number(sMonth), Number(sDay), sSG.substr(0, 2), sSG.substr(3, 2));
                  var utcItemDate = Date.UTC(dItemDate.getFullYear(), dItemDate.getMonth() + 1, dItemDate.getDate(), 0, 0, 0);
                  if (((document.getElementById(sGridRowID).cells[_COL_DAY_TO_ADD].childNodes[0].value == "1")) && (utcItemDate == utcCardDate)) {                      
                      dItemDate.setDate(dItemDate.getDate() + 1);
                  }                                
                  if (((IsShatGmarInNextDay(sActualShatYetiza)) || (sActualShatYetiza == '00:00')) && (dItemDate >= dParamDate)) {
                      iPDayToAdd = "1";
                      document.getElementById(sGridRowID).cells[_COL_DAY_TO_ADD].childNodes[0].value = "1";
                  }
                  else {
                      iPDayToAdd = "0";
                      document.getElementById(sGridRowID).cells[_COL_DAY_TO_ADD].childNodes[0].value = "0";
                  }
                  var utcShatYetiza = Date.UTC(dShatYetiza.getFullYear(), dShatYetiza.getMonth() + 1, dShatYetiza.getDate(), 0, 0, 0);
                  if (utcShatYetiza == utcCardDate) {
                      if (iPDayToAdd == 1)
                          dShatYetiza.setDate(dShatYetiza.getDate() + 1);
                  }
                  else {
                    if (iPDayToAdd == 0)
                        dShatYetiza.setDate(dShatYetiza.getDate() - 1);
                  }
                  
                  val.errormessage = "שעת היציאה לא יכולה להיות גדולה משעת גמר הסידור";
                  args.IsValid = (dShatYetiza <= dSidurDate);
                                  
                  document.getElementById(sGridRowID).cells[_COL_SHAT_YETIZA].childNodes[0].title = "תאריך שעת היציאה הוא: " + GetDateDDMMYYYY(dShatYetiza);
                  var sRes = ChkShatYetizaKisuyT(val.getAttribute("index"));                
                  //אם פעילות מסוג שירות נשנה לכל הכניסות את שעת היציאה בהתאם
                  var lMkt = document.getElementById(sGridRowID).cells[_COL_MAKAT].childNodes[0].value;
                  var arrKnisa = document.getElementById(sGridRowID).cells[_COL_KNISA].childNodes[0].toString().split(",");                      
                  if ((GetMakatType(lMkt) == MKT_SHERUT) && (arrKnisa[0] == 0)){
                        ChangeKnisotHour(document.getElementById(sGridRowID), iPDayToAdd, dShatYetiza);
                   
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

       function ChangeKnisotHour(oCurrPeilut, iDayToAdd, dSdDate) {
           var NextRow, lMkt, sHour, lKnisaMkt, sknisaHour, arrKnisot, MktType;

           lMkt = oCurrPeilut.cells[_COL_MAKAT].childNodes[0].value;
           sHour = oCurrPeilut.cells[_COL_SHAT_YETIZA].childNodes[0].value;
           NextRow = oCurrPeilut.nextSibling;
           if (NextRow != null) {
               lKnisaMkt = NextRow.cells[_COL_MAKAT].childNodes[0].value;
               MktType = GetMakatType(lKnisaMkt);
               arrKnisot = NextRow.cells[_COL_KNISA].childNodes[0].toString().split(",");
               while ((NextRow != null) && ((MktType != MKT_NAMAK) && (MktType != MKT_EMPTY))) {
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
       
    function IsAMinValid(val,args){
         SetBtnChanges();SetLvlChg(3);
         var sGridRowID = val.getAttribute("index");   
         var iKnisaNum, iKnisaType, arrKnisaVal;
         var _ActMIn = document.getElementById(sGridRowID).cells[_COL_ACTUAL_MINUTES].childNodes[0];         
         var iActualMinutes = _ActMIn.value;
         var iPlainMinutes;
         arrKnisaVal = String(document.getElementById(sGridRowID).cells[_COL_KNISA].childNodes[0].nodeValue).split(',');  
         iKnisaNum= Number(arrKnisaVal[0]);
         iKnisaType=Number(arrKnisaVal[1]);
         if ((iKnisaNum>0) && (iKnisaType==1)){
           iPlainMinutes =  document.getElementById("lstSidurim_hidParam98").value;
         }
         else{
             iPlainMinutes = document.getElementById(sGridRowID).cells[_COL_MAZAN_TASHLUM].childNodes[0].nodeValue;            
         }         
         args.IsValid = (Number(iActualMinutes)>=0 && Number(iActualMinutes)<=Number(iPlainMinutes));
         if (!args.IsValid) {
             _ActMIn.value = 0;
            _ActMIn.select();
         }
    }
    function ChkKisyT(val,args)    
    {  SetBtnChanges();SetLvlChg(3);
       var sGridRowID = val.getAttribute("index");
       var sValidHour = document.getElementById(sGridRowID).cells[_COL_KISUY_TOR].childNodes[0].getAttribute("OldTorMapV"); //val.outerHTML.substr(val.outerHTML.indexOf('errormessage') + String('errormessage').length + 44 ,5);
       if ((document.getElementById(sGridRowID).cells[_COL_KISUY_TOR].childNodes[0].value) != '')
       {
           var sCardDate = document.getElementById("clnDate").value;
           var sActualHour = args.Value;
           var sShatYetiza = document.getElementById(sGridRowID).cells[_COL_SHAT_YETIZA].childNodes[0].value;
           var OrgPTime = new Date(); var NewPTime = new Date(); var PTime = new Date();
           var AddDay =Number(document.getElementById(sGridRowID).cells[_COL_DAY_TO_ADD].childNodes[0].value);

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
    function ChkShatYetizaKisuyT(iIndx)    
    {
        var sMapKisuyTor = document.getElementById(iIndx).cells[_COL_KISUY_TOR].childNodes[0].getAttribute("OldTorMapV");
        var sGrdKisuyTor = document.getElementById(iIndx).cells[_COL_KISUY_TOR].childNodes[0].value;
       if (sGrdKisuyTor!='')
       {
           var sShatYetiza = document.getElementById(iIndx).cells[_COL_SHAT_YETIZA].childNodes[0].value;   
           
           if ((sShatYetiza=='') || (sShatYetiza=='__:__')){
               document.getElementById(iIndx).cells[_COL_KISUY_TOR].childNodes[0].value = '';
           }
           else{
               var dOrgMapKisuyTor = new Date();var dGrdKisuyTor = new Date();var dShatYetiza = new Date();                             
               dOrgMapKisuyTor.setHours(sShatYetiza.substr(0,2)); 
               dOrgMapKisuyTor.setMinutes(sShatYetiza.substr(sShatYetiza.length-2,2));  
               dOrgMapKisuyTor.setMinutes(dOrgMapKisuyTor.getMinutes()- Number(sMapKisuyTor));
               document.getElementById(iIndx).cells[_COL_KISUY_TOR].childNodes[0].value = dOrgMapKisuyTor.toLocaleTimeString().substr(0, 5);               
           }     
       }      
       else
       {
       return '';
       }
  }
  function changeStartHour(iIndex) {
      document.getElementById("lstSidurim_hidCurrIndx").value = "3|" + iIndex;
      var _ShatHatchala = document.getElementById("lstSidurim_txtSH".concat(iIndex));
      var iSidur = document.getElementById("lstSidurim_lblSidur".concat(iIndex)).innerHTML;
      var sOrgSH = _ShatHatchala.getAttribute("OrgShatHatchala");
      var sNewSH = _ShatHatchala.value;
      var sCardDate = document.getElementById("clnDate").value;
      wsGeneral.SidurStartHourChanged(sCardDate, iSidur, sNewSH, sOrgSH, callBackStartHour, null, iIndex);
  }
  function callBackStartHour(result, iIndex) {
     if (result=='1')
         document.getElementById("lstSidurim_btnShowMessage").click();

     var dSdDate = new Date();     
     var _SHNew = document.getElementById("lstSidurim_txtSH".concat(iIndex));
     if (!IsShatGmarInNextDay(_SHNew.value)) {//שעת התחלה
         document.getElementById("lstSidurim_lblDate".concat(iIndex)).innerHTML = document.getElementById("clnDate").value;
     }
     var sSdDate=document.getElementById("lstSidurim_lblDate".concat(iIndex)).innerHTML; 
     var sYear = sSdDate.substr(sSdDate.length-4,4);
     var sMonth = Number(sSdDate.substr(3, 2)) - 1;
     var sDay = sSdDate.substr(0, 2);
     SetDate(dSdDate, Number(sYear), Number(sMonth), Number(sDay), 0, 0);        
     _SHNew.title = "תאריך התחלת הסידור הוא: " + GetDateDDMMYYYY(dSdDate);
  }
  function ChkStartHour(val, args){
        SetBtnChanges();
        var iIndex = String(val.id).substr(String(val.id).length - 1, 1);
        document.getElementById("lstSidurim_hidCurrIndx").value ="3|" + iIndex;
        var sShatHatchala = document.getElementById("lstSidurim_txtSH".concat(iIndex));
        if (IsValidTime(sShatHatchala.value)){                            
            if (!IsSHBigSG(val, args)) {
                if (args.IsValid == false)
                    val.errormessage = val.errormessage + "\n שעת ההתחלה אינה יכולה להיות גדולה או שווה לשעת הגמר " + "\n";
                else {
                    val.errormessage = "\n שעת ההתחלה אינה יכולה להיות גדולה או שווה לשעת הגמר \n";
                    args.IsValid = false;
                }
            }
            if (iIndex > 0) {
                if (!IsSHGreaterPrvSG(val, args)) {
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

     function ISSGValid(val, args){
         SetBtnChanges();
         //נבדוק אם שעת ההתחלה נמצאת בין פרמטרים כללים או פרמטרים של סידור         
         var iIndex = String(val.id).substr(String(val.id).length - 1, 1);
         var sCardDate = document.getElementById("clnDate").value;
         var sShatGmar = document.getElementById("lstSidurim_txtSG".concat(iIndex));
         var dShatGmar = new Date();
         
         SetDate(dShatGmar, sCardDate.substr(6, 4), Number(sCardDate.substr(3, 2)) - 1, sCardDate.substr(0, 2), 0, 0); 
         if ((!IsShatGmarInNextDay(sShatGmar.value)) && (sShatGmar.value != '00:00')) {
             document.getElementById("lstSidurim_txtDayAdd".concat(iIndex)).value = "0";
             document.getElementById("lstSidurim_txtSG".concat(iIndex)).title = "תאריך גמר הסידור הוא: " + GetDateDDMMYYYY(dShatGmar);
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
     }                     
   
    function ISSHLValid(val,args)
    {
       args.IsValid = true;
       var iIndex = String(val.id).substr(String(val.id).length-1,1);  
       var sSidurDate = document.getElementById("lstSidurim_lblDate".concat(iIndex)).innerHTML;  
       
       var sSHatchala = document.getElementById("lstSidurim_txtSH".concat(iIndex)).value; 
       var sSGmar = document.getElementById("lstSidurim_txtSG".concat(iIndex)).value;
       var sSHLetashlum = document.getElementById("lstSidurim_txtSHL".concat(iIndex)).value; 
       var sSGLetashlum = document.getElementById("lstSidurim_txtSGL".concat(iIndex)).value;
       
       var ShatHatchala = new Date(Number(sSidurDate.substr(6,4)), Number(sSidurDate.substr(3,2))-1,Number(sSidurDate.substr(0,2)),sSHatchala.substr(0,2),sSHatchala.substr(sSHatchala.length-2,2));             
       var ShatGmar = new Date(Number(sSidurDate.substr(6,4)), Number(sSidurDate.substr(3,2))-1,Number(sSidurDate.substr(0,2)),sSGmar.substr(0,2),sSGmar.substr(sSGmar.length-2,2));             
       var SHLetashlum = new Date(Number(sSidurDate.substr(6,4)), Number(sSidurDate.substr(3,2))-1,Number(sSidurDate.substr(0,2)),sSHLetashlum.substr(0,2),sSHLetashlum.substr(sSHLetashlum.length-2,2));      
       var SGLetashlum = new Date(Number(sSidurDate.substr(6,4)), Number(sSidurDate.substr(3,2))-1,Number(sSidurDate.substr(0,2)),sSGLetashlum.substr(0,2),sSGLetashlum.substr(sSGLetashlum.length-2,2));      
       
       var dSHatchala = Date.UTC(ShatHatchala.getFullYear(), ShatHatchala.getMonth()+1, ShatHatchala.getDate(),ShatHatchala.getHours(),ShatHatchala.getMinutes(),0);
       var dShatGmar = Date.UTC(ShatGmar.getFullYear(), ShatGmar.getMonth()+1, ShatGmar.getDate(),ShatGmar.getHours(),ShatGmar.getMinutes(),0);
       var dSHLetashlum = Date.UTC(SHLetashlum.getFullYear(), SHLetashlum.getMonth()+1, SHLetashlum.getDate(),SHLetashlum.getHours(),SHLetashlum.getMinutes(),0);
       var dSGLetashlum = Date.UTC(SGLetashlum.getFullYear(), SGLetashlum.getMonth()+1, SGLetashlum.getDate(),SGLetashlum.getHours(),SGLetashlum.getMinutes(),0);
       if ((dSHLetashlum<dSHatchala) ||(dSHLetashlum>dSHLetashlum))
       {
         val.errormessage = "שעת התחלה לתשלום לא תקינה, יש להקליד שעה בטווח " + sSGLetashlum + " - " + sSHatchala;
         args.IsValid = false;
       }       
       if ((dSGLetashlum<dSHLetashlum) ||(dSGLetashlum>dShatGmar))
       {
         val.errormessage = "שעת גמר לתשלום לא תקינה, יש להקליד שעה בטווח " + sSGmar + " - " + sSHLetashlum;
         args.IsValid = false;
       }       
       if (dSHLetashlum>dSGLetashlum) 
       {
        if (args.IsValid)
        {
          val.errormessage = "לא ניתן לעדכן שעת התחלה לתשלום גדולה משעת גמר לתשלום";  
          args.IsValid = false;
        }else
        {
          val.errormessage =val.errormessage.concat(" לא ניתן לעדכן שעת התחלה לתשלום גדולה משעת גמר לתשלום \r\n");  
          args.IsValid = false;
        }
       }
    }   
    function SetDate(oDate, sYear, sMonth, sDay, sHour, sMinutes)
    {
        oDate.setFullYear(sYear);
        oDate.setMonth(sMonth);
        oDate.setDate(sDay);
        oDate.setHours(sHour); 
        oDate.setMinutes(sMinutes);     
        return oDate;
    }
    function IsSHBigSG(val,args)
    {//נבדוק אם שעת ההתחלה קטנה משעת הגמר
       SetBtnChanges();
       var iIndex = String(val.id).substr(String(val.id).length-1,1);  
       var sShatHatchala = document.getElementById("lstSidurim_txtSH".concat(iIndex)); 
       var sShatGmar = document.getElementById("lstSidurim_txtSG".concat(iIndex));
       var sSidurDate = document.getElementById("lstSidurim_lblDate".concat(iIndex)); 
       var dCardDate = document.getElementById("clnDate").value;     
       var AddDay = Number(document.getElementById("lstSidurim_txtDayAdd".concat(iIndex)).value);      
       var ShatGmar = new Date();     
       var ShatHatchala = new Date();    
       var sYear=sSidurDate.innerHTML.substr(sSidurDate.innerHTML.length-4,4);
       var sMonth=Number(sSidurDate.innerHTML.substr(3,2))-1;
       var sDay=sSidurDate.innerHTML.substr(0,2);                     
       ShatHatchala.setFullYear(sYear);
       ShatHatchala.setMonth(sMonth);
       ShatHatchala.setDate(sDay);
       ShatHatchala.setHours(sShatHatchala.value.substr(0,2));
       ShatHatchala.setMinutes(sShatHatchala.value.substr(sShatHatchala.value.length - 2, 2));
       sYear = dCardDate.substr(dCardDate.length - 4, 4);
       sMonth = Number(dCardDate.substr(3, 2)) - 1;
       sDay = dCardDate.substr(0, 2);        
       ShatGmar.setFullYear(sYear);
       ShatGmar.setMonth(sMonth);
       ShatGmar.setDate(sDay);
       ShatGmar.setHours(sShatGmar.value.substr(0,2));
       ShatGmar.setMinutes(sShatGmar.value.substr(sShatGmar.value.length-2,2));         
       if (AddDay == 1){                      
            ShatGmar.setDate(ShatGmar.getDate() + 1);           
          }           
       var dShatHatchala = Date.UTC(ShatHatchala.getFullYear(), ShatHatchala.getMonth()+1, ShatHatchala.getDate(),ShatHatchala.getHours(),ShatHatchala.getMinutes(),0);
       var dShatGmar = Date.UTC(ShatGmar.getFullYear(), ShatGmar.getMonth()+1, ShatGmar.getDate(),ShatGmar.getHours(),ShatGmar.getMinutes(),0);
       return  (dShatHatchala<dShatGmar);
    }
    function IsSHGreaterPrvSG(val,args){      
       SetBtnChanges();
       var iIndex = String(val.id).substr(String(val.id).length-1,1);
       var iPrvIndex = Number(iIndex) - 1;
       var iPrevSidur = $("#lstSidurim_lblSidur".concat(iPrvIndex)).html();       
       if ((iPrevSidur != SIDUR_CONTINUE_NAHAGUT) && (iPrevSidur != SIDUR_CONTINUE_NOT_NAHAGUT)) {
           var sShatHatchala = document.getElementById("lstSidurim_txtSH".concat(iIndex));
           var sPrevShatGmar = document.getElementById("lstSidurim_txtSG".concat(iPrvIndex));
           var sSidurDate = document.getElementById("lstSidurim_lblDate".concat(iIndex));
           var sPrevSidurDate = document.getElementById("clnDate").value;           
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
               if (document.getElementById("lstSidurim_txtDayAdd".concat(iPrvIndex)).value == "1")
                   prvShGmar.setDate(prvShGmar.getDate() + 1);
               var dShatHatchala = Date.UTC(ShStart.getFullYear(), ShStart.getMonth() + 1, ShStart.getDate(), ShStart.getHours(), ShStart.getMinutes(), 0);
               var dPrevShatGmar = Date.UTC(prvShGmar.getFullYear(), prvShGmar.getMonth() + 1, prvShGmar.getDate(), prvShGmar.getHours(), prvShGmar.getMinutes(), 0);
               return (dShatHatchala >= dPrevShatGmar);
           }
           else { return true; }
       }
       else { return true; }       
    }
    function IsEHourBigSHour(val,args)
    {//נבדוק ששעת הגמר קטנה משעת ההתחלה של הסידור הבא   
       SetBtnChanges();
       var iIndex = String(val.id).substr(String(val.id).length-1,1);
       var iNextIndex = Number(iIndex) + 1;
       var iNextSidur = $("#lstSidurim_lblSidur".concat(iNextIndex)).html();
       if ((iNextSidur != SIDUR_CONTINUE_NAHAGUT) && (iNextSidur != SIDUR_CONTINUE_NOT_NAHAGUT)) {
           var sNxtStartH = document.getElementById("lstSidurim_txtSH".concat(iNextIndex));
           if (sNxtStartH != null) {
               var sShatGmar = document.getElementById("lstSidurim_txtSG".concat(iIndex));
               var sSidurDate = document.getElementById("lstSidurim_lblDate".concat(iIndex));
               var AddDay = document.getElementById("lstSidurim_txtDayAdd".concat(iIndex)).value;
               var AddDayNextSidur = document.getElementById("lstSidurim_txtDayAdd".concat(iNextIndex)).value;
               var sNextSidurDate = document.getElementById("lstSidurim_lblDate".concat(iNextIndex));
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
    function IsSidurMyuhad(sMisparSidur){//נבדוק אם סידור הוא רגיל או מיוחד    
        if (String(sMisparSidur).length > 1)
            return (sMisparSidur.substr(0, 2) == "99");
        else{ return false;}        
    }
    function AddPeilut(iIndex) {
        var iBitul = document.getElementById("lstSidurim_lblSidurCanceled".concat(iIndex)).value;        
        if ((iBitul != "1") && (iBitul != "3")) {
            if (bScreenChanged) {
                $("#hidSave")[0].value = "1";
                __doPostBack('btnConfirm', '');                    
            }
            var id = document.getElementById("txtId").value;
            var CardDate = document.getElementById("clnDate").value;
            var SidurDate =  document.getElementById("lstSidurim_lblDate".concat(iIndex)).innerHTML;
            var SidurHour = document.getElementById("lstSidurim_txtSH".concat(iIndex)).value;
            var SidurId = document.getElementById("lstSidurim_lblSidur".concat(iIndex)).innerHTML;
            if (SidurHour == '')
                SidurDate = '01/01/0001';            

            document.getElementById("divHourglass").style.display = 'block';
            var sQuryString = "?EmpID=" + id + "&CardDate=" + CardDate + "&SidurID=" + SidurId + "&SidurHour=" + SidurHour + "&SidurDate=" + SidurDate + "&dt=" + Date();
            var res = window.showModalDialog('HosafatPeiluyot.aspx' + sQuryString, window, "dialogwidth:900px;dialogheight:680px;dialogtop:130px;dialogleft:170px;status:no;resizable:yes;scroll:0;");
            document.getElementById("divHourglass").style.display = 'none';
            if ((bScreenChanged) || ((res != undefined) && (res != '') && (!bScreenChanged))) {
                document.getElementById("hidExecInputChg").value = "1";
                bScreenChanged = false;
                __doPostBack('btnRefreshOvedDetails', '');
            }
        }        
        return res;
    }
    function AddHosafatKnisot(iSidurIndx, iPeilutIndx) {
        if (document.getElementById(iPeilutIndx.id).cells[_COL_CANCEL].childNodes[0].className == 'ImgChecked') {
            if (bScreenChanged) {
                $("#hidSave")[0].value = "1";
                __doPostBack('btnConfirm', '');
            }
            var ReturnWin;
            var id = $("#txtId").val();
            var SidurId = $("#lstSidurim_lblSidur".concat(iSidurIndx)).html();
            var CardDate = document.getElementById("clnDate").value;
            var SidurDate = $("#lstSidurim_lblDate".concat(iSidurIndx)).html();
            var iAddDay = document.getElementById(iPeilutIndx.id).cells[_COL_DAY_TO_ADD].childNodes[0].value;
            var SidurHour = $("#lstSidurim_txtSH".concat(iSidurIndx)).val();
            if (SidurHour == '')
                SidurDate = '01/01/0001';
           
            var dPeilutDate = new Date();
            dPeilutDate.setFullYear(CardDate.substr(6, 4));
            dPeilutDate.setMonth((Number(CardDate.substr(3, 2)) - 1).toString());
            dPeilutDate.setDate(CardDate.substr(0, 2));
            dPeilutDate.setDate(dPeilutDate.getDate() + Number(iAddDay));

            var ShatYetzia = document.getElementById(iPeilutIndx.id).cells[_COL_SHAT_YETIZA].childNodes[0].value;
            var MakatNesia = document.getElementById(iPeilutIndx.id).cells[_COL_MAKAT].childNodes[0].value;
            var OtoNo = document.getElementById(iPeilutIndx.id).cells[_COL_CAR_NUMBER].childNodes[0].value;
            var sQueryString = "?EmpID=" + id + "&SidurID=" + SidurId + "&CardDate=" + CardDate + "&SidurDate=" + SidurDate + "&SidurHour=" + SidurHour + "&ShatYetzia=" + GetDateDDMMYYYY(dPeilutDate).concat(" " + ShatYetzia) + "&MakatNesia=" + MakatNesia + "&OtoNo=" + OtoNo + "&dt=" + Date();
            res = window.showModalDialog('HosafatKnisot.aspx' + sQueryString, window, 'dialogwidth:500px;dialogheight:380px;dialogtop:280px;dialogleft:340px;status:no;resizable:yes;');
            if ((bScreenChanged) || ((res != undefined) && (res != '') && (!bScreenChanged))){
                document.getElementById("hidExecInputChg").value = "1";
                bScreenChanged = false;
                __doPostBack('btnRefreshOvedDetails', '');
            }
            return res;    
        }           
    }
    function AddSadotLsidur(iIndex) {
    if (bScreenChanged) {
        $("#hidSave")[0].value = "1";           
        __doPostBack('btnConfirm', '');
    }
    var dPeilutDate = new Date();
    var id = document.getElementById("txtId").value;
    var CardDate = document.getElementById("clnDate").value;
    var SidurDate = document.getElementById("lstSidurim_lblDate".concat(iIndex)).innerHTML;
    var SidurSHour = document.getElementById("lstSidurim_txtSH".concat(iIndex)).value; 
    var SidurEHour = document.getElementById("lstSidurim_txtSG".concat(iIndex)).value; 
    var iSDayToAdd = document.getElementById("lstSidurim_txtDayAdd".concat(iIndex)).value;         
    var SidurDate;
    SetDate(dPeilutDate, Number(CardDate.substr(6, 4)), Number(CardDate.substr(3, 2)) - 1, Number(CardDate.substr(0, 2)), "0", "0");                        
    dPeilutDate.setDate(dPeilutDate.getDate() + Number(iSDayToAdd));                 
    var SidurId= document.getElementById("lstSidurim_lblSidur".concat(iIndex)).innerHTML;       
    if (SidurSHour=='')
        SidurDate='01/01/0001';
      
    var sQuryString = "?EmpID=" + id + "&CardDate=" + CardDate + "&SidurID=" + SidurId + "&ShatHatchala=" + SidurDate + ' ' + SidurSHour + "&ShatGmar=" + SidurEHour + "&ShatGmarDate=" + GetDateDDMMYYYY(dPeilutDate) + "&SidurDate=" + SidurDate + "&dt=" + Date();        
    var res=window.showModalDialog('SadotNosafimLeSidur.aspx' + sQuryString , window , "dialogwidth:670px;dialogheight:380px;dialogtop:10px;dialogleft:320px;status:no;resizable:yes;");
    if ((bScreenChanged) || ((res != undefined) && (res != '') && (!bScreenChanged))){
        document.getElementById("hidExecInputChg").value = "1";
        bScreenChanged = false;
        __doPostBack('btnRefreshOvedDetails', '');
    }              
    return res;
    }

    function AddNesiaReka(iPeilutIndex, iSidurIndex, iLastPeilut) {
        var iMisparIshi = document.getElementById("txtId").value;
        var dDate = document.getElementById("clnDate").value;
        var sMakatStart = document.getElementById(iPeilutIndex.id).cells[_COL_MAKAT].childNodes[0].value;
        var lCarNum = document.getElementById(iPeilutIndex.id).cells[_COL_CAR_NUMBER].childNodes[0].value;
        var sMakatDetails = GetMakatEnd(iPeilutIndex.id, lCarNum, iSidurIndex, iLastPeilut);
        var sMakatEnd = sMakatDetails.split(",")[0];
        var lCarNum = sMakatDetails.split(",")[1];
        var sShatYetiza = document.getElementById(iPeilutIndex.id).cells[_COL_SHAT_YETIZA].childNodes[0].value;
        var sPeilutDate = document.getElementById(iPeilutIndex.id).cells[_COL_SHAT_YETIZA].childNodes[0].getAttribute("OrgDate");
        var iMazanTichnun = document.getElementById(iPeilutIndex.id).cells[_COL_DEF_MINUTES].innerHTML;
        var SidurId = document.getElementById("lstSidurim_lblSidur".concat(iSidurIndex)).innerHTML;
        var SidurSH = document.getElementById("lstSidurim_txtSH".concat(iSidurIndex)).value;
        var sPeilutShatYetiza = sMakatDetails.split(",")[2];
        if (sMakatEnd == '')
            alert('לא ניתן להשלים נסיעה ריקה');
        else {          
            wsGeneral.AddNesiaReka(iMisparIshi,dDate,SidurId,SidurSH, sMakatStart, sMakatEnd, sShatYetiza, sPeilutDate, iMazanTichnun, lCarNum,sPeilutShatYetiza, callBackAddReka);
        }
     }
     function callBackAddReka(result) {
         if (result == 1)
             alert('לא נמצאה ריקה מתאימה');
         else {
             document.getElementById("hidExecInputChg").value = "1";
             __doPostBack('btnRefreshOvedDetails', '');
         }
     }
     
     function GetMakatEnd(iPeilutIndex, lCarNum, iSidurIndex, iLastPeilut) {
        var CanAddReka;
        var bExists=false;
        var arrKnisa;
        var sMakatEnd = '';
        var sMakat;
        var sPeilutShatYetiza = '';
        var _AddNesiaReka = document.getElementById(iPeilutIndex).cells[_COL_ADD_NESIA_REKA].childNodes[0];
        var NextRow, NextSidur, NextSidurimPeiluyot;
        var bFound=false;

        if (iLastPeilut == '1') {
            iSidurIndex = iSidurIndex + 1;
            NextSidur = document.getElementById("lstSidurim_tblSidurim".concat(iSidurIndex));
            while ((NextSidur != null) && (!bFound)) {
                NextSidurimPeiluyot = document.getElementById("lstSidurim_" + padLeft(iSidurIndex, '0', 3));
                if (NextSidurimPeiluyot != null) {
                    NextRow = NextSidurimPeiluyot.firstChild.childNodes[1];
                    bFound = true;
                }
                else {
                    iSidurIndex = iSidurIndex + 1;
                    NextSidur = document.getElementById("lstSidurim_tblSidurim".concat(iSidurIndex));
                }
            }
        }
        else {
            NextRow = document.getElementById(iPeilutIndex).nextSibling;
            NextSidur = document.getElementById("lstSidurim_tblSidurim".concat(iSidurIndex));
        }

        while ((NextRow != null) && (sMakatEnd == '') && (bExists == false) && (NextSidur != null)) {
            try {
                CanAddReka = NextRow.cells[_COL_ADD_NESIA_REKA].childNodes[0].getAttribute("NesiaReka");
            }
            catch (err) {
                CanAddReka == 0;
            }

            if (CanAddReka == "1") {
                sPeilutShatYetiza = sPeilutShatYetiza.concat(NextRow.cells[_COL_SHAT_YETIZA].childNodes[0].getAttribute("OrgDate") + ' ' + NextRow.cells[_COL_SHAT_YETIZA].childNodes[0].value) + '|';
                sMakatEnd = NextRow.cells[_COL_MAKAT].childNodes[0].value;
                if (lCarNum != NextRow.cells[_COL_CAR_NUMBER].childNodes[0].value)
                    lCarNum = 0;
            }
            else {
                if (NextRow.cells[_COL_MAKAT].childNodes[0].value != undefined) {
                    sMakat = NextRow.cells[_COL_MAKAT].childNodes[0].value;
                    arrKnisa = NextRow.cells[_COL_KNISA].childNodes[0].toString().split(",");
                    // 3אלמנט ללא מאפיין 9                   
                    if ((GetMakatType(sMakat) == MKT_ELEMENT) && (Number(arrKnisa[3]) == 0))
                    //if ((Number(sMakat.substr(0, 3)) >= 701) && (Number(sMakat.substr(0, 3)) <= 799) && (Number(arrKnisa[3]) == 0))
                        bExists = true;
                    else {
                        sPeilutShatYetiza = sPeilutShatYetiza.concat(NextRow.cells[_COL_SHAT_YETIZA].childNodes[0].getAttribute("OrgDate") + ' ' + NextRow.cells[_COL_SHAT_YETIZA].childNodes[0].value) + '|';
                    }
                }
            }
            NextRow = NextRow.nextSibling;
            if (NextRow == null) { //אם הגענו לסוף הסידור נעבור לסידור הבא
                iSidurIndex = iSidurIndex + 1;
                bFound = false;
                while ((NextSidur != null) && (!bFound)) {
                    NextSidurimPeiluyot = document.getElementById("lstSidurim_" + padLeft(iSidurIndex, '0', 3));
                    if (NextSidurimPeiluyot != null) {
                        NextRow = NextSidurimPeiluyot.firstChild.childNodes[1];
                        bFound = true;
                    }
                    else {
                        iSidurIndex = iSidurIndex + 1;
                        NextSidur = document.getElementById("lstSidurim_tblSidurim".concat(iSidurIndex));
                    }
                }
            }
        }
            //נמלא את שעת היציאה של כל שאר הפעילויות
            while (NextRow != null) {
                if (NextRow.cells[_COL_SHAT_YETIZA].childNodes[0].value != undefined) {
                    sPeilutShatYetiza = sPeilutShatYetiza.concat(NextRow.cells[_COL_SHAT_YETIZA].childNodes[0].getAttribute("OrgDate") + ' ' + NextRow.cells[_COL_SHAT_YETIZA].childNodes[0].value) + '|';
                }
                NextRow = NextRow.nextSibling;
            }
            return sMakatEnd.concat(",").concat(lCarNum).concat(",").concat(sPeilutShatYetiza);
        }           
    function FixSidurHeadrut(iIndex){         
        var sQueryString;
        sQueryString = "?dt=" + Date();
        sQueryString = sQueryString + "&MisparIshi=" + document.getElementById("txtId").value;
        sQueryString = sQueryString + "&DateCard=" + document.getElementById("lstSidurim_lblDate".concat(iIndex)).innerHTML;                
        sQueryString = sQueryString + "&MisparSidur=" + document.getElementById("lstSidurim_lblSidur".concat(iIndex)).innerHTML;       
        sQueryString = sQueryString + "&TimeStart=" + document.getElementById("lstSidurim_txtSH".concat(iIndex)).value; 
        sQueryString = sQueryString + "&TimeEnd=" + document.getElementById("lstSidurim_txtSG".concat(iIndex)).value;  
        window.showModalDialog('DivuachHeadrut.aspx?' + sQueryString, '', 'dialogwidth:555px;dialogheight:390px;dialogtop:150px;dialogleft:480px;status:no;resizable:yes;');
    }
    function ChgImg(iInx){
       var img = document.getElementById("lstSidurim_cImgS".concat(iInx));
       if ((String(img.nameProp).indexOf("expand_blue_big.jpg"))>-1){
        img.src= "../../images/collapse_blue_big.jpg";
        return;
       }
     
       if ((String(img.nameProp).indexOf("red_down_2_big.jpg"))>-1){
           img.src= "../../images/red_up_2_big.jpg";
           return;
       } 
     
       if ((String(img.nameProp).indexOf("green_down_2_big.jpg"))>-1){
            img.src= "../../images/green_up_2_big.jpg";
            return;
       }
      
       if ((String(img.nameProp).indexOf("collapse_blue_big.jpg"))>-1){
         img.src= "../../images/expand_blue_big.jpg";
         return;
       }
       
       if ((String(img.nameProp).indexOf("red_up_2_big.jpg"))>-1){
         img.src= "../../images/red_down_2_big.jpg";
         return;
       }
          
       if ((String(img.nameProp).indexOf("green_up_2_big.jpg"))>-1){
         img.src= "../../images/green_down_2_big.jpg";
         return;
       }       
    }
    function SetDayToAdd(iInx)
    {
      var _Add = document.getElementById("lstSidurim_txtDayAdd".concat(iInx));
      var _ShatGmar = document.getElementById("lstSidurim_txtSG".concat(iInx));
      var dSdDate = document.getElementById("lstSidurim_lblDate".concat(iInx)).innerHTML;   
      var dCardDate = document.getElementById("clnDate").value; 
      if (IsShatGmarInNextDay(_ShatGmar.value) || (_ShatGmar.value=='00:00'))            
            _Add.value = "1";       
          else      
            _Add.value = "0";            
    }
    function SetDay(iInx){
      $find("pBehvDate").hide();
      var sEndHour;
      var sSidurDate;     
      var sParamNxtDay;
      var dParamDate = new Date();
      var dItemDate = new Date();
      var arrItems = iInx.split("|");
      var sCardDate = document.getElementById("clnDate").value;
      if (arrItems[0] == '1') {//שעת גמר
          sEndHour = document.getElementById("lstSidurim_txtSG" + arrItems[1]).value;
          sSidurDate = document.getElementById("lstSidurim_lblDate".concat(arrItems[1])).innerHTML;                     
            var dCardDate = new Date(Number(sCardDate.substr(6, 4)), Number(sCardDate.substr(3, 2)) - 1, Number(sCardDate.substr(0, 2)), 0, 0);
            var dSidurTime = new Date(Number(sSidurDate.substr(6, 4)), Number(sSidurDate.substr(3, 2)) - 1, Number(sSidurDate.substr(0, 2)), 0, 0);
            var utcCardDate = Date.UTC(dCardDate.getFullYear(), dCardDate.getMonth() + 1, dCardDate.getDate(), 0, 0, 0);
            var utcSidurDate = Date.UTC(dSidurTime.getFullYear(), dSidurTime.getMonth() + 1, dSidurTime.getDate(), 0, 0, 0);
            var _Add = document.getElementById("lstSidurim_txtDayAdd".concat(arrItems[1])).value;
            if (sEndHour == '00:00') {
               _Add = '1'
               document.getElementById("lstSidurim_txtDayAdd".concat(arrItems[1])).value='1';
            }
            if (_Add == '1') {
                if (utcCardDate == utcSidurDate)
                    dSidurTime.setDate(dSidurTime.getDate() + 1);
            }
            else {//add=0                
                    dSidurTime = dCardDate;
            }
            document.getElementById("lstSidurim_txtSG".concat(arrItems[1])).title = "תאריך גמר הסידור הוא: " + GetDateDDMMYYYY(dSidurTime);                  
      }
      else {//שעת יציאה
          sEndHour = document.getElementById(arrItems[1]).cells[_COL_SHAT_YETIZA].childNodes[0].value;
          sParamNxtDay = document.getElementById("lstSidurim_hidParam242").value;
          var sYear = sCardDate.substr(sCardDate.length - 4, 4);
          var sMonth = Number(sCardDate.substr(3, 2)) - 1;
          var sDay = sCardDate.substr(0, 2);
          var sSidurSG = document.getElementById("lstSidurim_txtSG" + arrItems[2]).value;
          SetDate(dParamDate, Number(sYear), Number(sMonth), Number(sDay), Number(sParamNxtDay.substr(0, 2)), Number(sParamNxtDay.substr(3, 2)));
          SetDate(dItemDate, Number(sYear), Number(sMonth), Number(sDay), Number(sSidurSG.substr(0, 2)), Number(sSidurSG.substr(3, 2)));
         }
         if (IsShatGmarInNextDay(sEndHour)){
             document.getElementById("lstSidurim_hidCurrIndx").value = iInx;
             if (arrItems[0] == '1')//גמר
                 document.getElementById("lstSidurim_btnShowMessage").click();
             else
                 if ((document.getElementById("lstSidurim_txtDayAdd".concat(arrItems[2])).value == "1") || (dItemDate > dParamDate))
                     document.getElementById("lstSidurim_btnShowMessage").click();
         }
         else
             if (arrItems[0] == '2')//יציאה    
             {
                 var iAdd;
                 if (sEndHour == '00:00')
                     iAdd = 1;
                 else
                     iAdd = 0;

                dItemDate.setDate(dItemDate.getDate() + iAdd);
                document.getElementById(arrItems[1]).cells[_COL_DAY_TO_ADD].childNodes[0].value = iAdd;
                document.getElementById(arrItems[1]).cells[_COL_SHAT_YETIZA].childNodes[0].title = "תאריך שעת היציאה הוא: " + GetDateDDMMYYYY(dItemDate);
             }     
   }
   
    function btnDay_click(iDayToAdd){     
        $find("pBehvDate").hide();
        var iIndx = document.getElementById("lstSidurim_hidCurrIndx").value;   
        var arrItems = iIndx.split("|"); 
        var sSdDate;
        var dSdDate = new Date();
        var sCardDate = document.getElementById("clnDate").value;
        var dCardDate = new Date(Number(sCardDate.substr(6, 4)), Number(sCardDate.substr(3, 2)) - 1, Number(sCardDate.substr(0, 2)), 0, 0);
        var utcCardDate = Date.UTC(dCardDate.getFullYear(), dCardDate.getMonth() + 1, dCardDate.getDate(), 0, 0, 0);
         
        if ((arrItems[0]=='1') || (arrItems[0]=='3')){ //שעת התחלה וגמר                  
            sSdDate = document.getElementById("lstSidurim_lblDate".concat(arrItems[1])); 
        }
        else{
            sSdDate =  document.getElementById("lstSidurim_lblDate".concat(arrItems[2])); 
        }
        var sYear=sSdDate.innerHTML.substr(sSdDate.innerHTML.length-4,4);
        var sMonth=Number(sSdDate.innerHTML.substr(3,2))-1;
        var sDay=sSdDate.innerHTML.substr(0,2);             
        dSdDate.setFullYear(sYear);
        dSdDate.setMonth(sMonth);
        dSdDate.setDate(sDay);
        var utcSidurDate = Date.UTC(dSdDate.getFullYear(), dSdDate.getMonth() + 1, dSdDate.getDate(), 0, 0, 0);
        if (iDayToAdd == 1) {
            if (utcSidurDate==utcCardDate)
                dSdDate.setDate(dSdDate.getDate() + Number(iDayToAdd));             
        }
        else {//addday=0
            if (utcSidurDate > utcCardDate)
                dSdDate.setDate(dSdDate.getDate()-1);  
        }    
         
        if (arrItems[0]=='1'){
            document.getElementById("lstSidurim_txtDayAdd" + arrItems[1]).value = Number(iDayToAdd);            
            document.getElementById("lstSidurim_txtSG" + arrItems[1]).title = "תאריך גמר הסידור הוא: " + GetDateDDMMYYYY(dSdDate);
        }
        else {
            if (arrItems[0] == '3'){//שעת התחלה סידור
                var _SHNew = document.getElementById("lstSidurim_txtSH".concat(arrItems[1]));
                var sCardDate = document.getElementById("clnDate").value;
                var iSidurKey = document.getElementById("lstSidurim_lblSidur".concat(arrItems[1])).innerHTML;
                _SHNew.title = "תאריך התחלת הסידור הוא: " + GetDateDDMMYYYY(dSdDate);
                document.getElementById("lstSidurim_lblDate".concat(arrItems[1])).innerHTML = GetDateDDMMYYYY(dSdDate);            
                wsGeneral.UpdateSidurDate(sCardDate, iSidurKey, _SHNew.getAttribute('OrgShatHatchala'), _SHNew.value, Number(iDayToAdd));
            }
            else {//2
                document.getElementById(arrItems[1]).cells[_COL_DAY_TO_ADD].childNodes[0].value = Number(iDayToAdd);
                document.getElementById(arrItems[1]).cells[_COL_SHAT_YETIZA].childNodes[0].title = "תאריך שעת היציאה הוא: " + GetDateDDMMYYYY(dSdDate);
                //נשנה גם לכל הכניסות
                ChangeKnisotHour(document.getElementById(arrItems[1]), iDayToAdd, dSdDate);
            }
        }

        if ((arrItems[0] == '1') || (arrItems[0] == '3')){//שעת התחלה וגמר           
            ValidatorEnable(document.getElementById('lstSidurim_vldSG'.concat(arrItems[1]), true));
            ValidatorEnable(document.getElementById('lstSidurim_vldSHatchala'.concat(arrItems[1]), true));
        }        
    }
   function SidurTimeChanged(id)
   {//שעת התחלה השתנתה, נבדוק שוב את התנאים לשדה השלמה
    var sSidurDate = document.getElementById("lstSidurim_lblDate".concat(id)).innerHTML; 
    var ddlChariga = document.getElementById("lstSidurim_ddlException".concat(id));          
    var sYear = Number(sSidurDate.substr(6,4));
    var sMonth = Number(sSidurDate.substr(3,2))-1;
    var sDay = Number(sSidurDate.substr(0,2));
    var dSH = new Date();
    var dSG = new Date();
    var dSHL = new Date();
    var dSGL = new Date();
    var oSH = document.getElementById("lstSidurim_txtSH".concat(id)).value;
    var oSG = document.getElementById("lstSidurim_txtSG".concat(id)).value;
    if (document.getElementById("lstSidurim_txtSHL".concat(id)) != null) {
        var oSHL = document.getElementById("lstSidurim_txtSHL".concat(id)).value;
        var oSGL = document.getElementById("lstSidurim_txtSGL".concat(id)).value;
        var oZmanChariga = document.getElementById("lstSidurim_hidParam41").value;
        var iSDayToAdd = document.getElementById("lstSidurim_txtDayAdd".concat(id)).value;
        var bRes = false;

        if ((oSH == '__:__') || (oSG == '__:__')) {
            document.getElementById("lstSidurim_ddlHashlama".concat(id)).disabled = true;
            ddlChariga.disabled = true;
        }
        var iCharigaType = Number(ddlChariga.getAttribute("ChrigaType"));
        if (iCharigaType != 4) {// 4- חריגה בגלל סיבת לא לתשלום
            SetDate(dSH, Number(sSidurDate.substr(6, 4)), Number(sSidurDate.substr(3, 2)) - 1, Number(sSidurDate.substr(0, 2)), Number(oSH.substr(0, 2)), Number(oSH.substr(3, 2)));
            dSH.setDate(dSH.getDate() + Number(iSDayToAdd));
            SetDate(dSG, Number(sSidurDate.substr(6, 4)), Number(sSidurDate.substr(3, 2)) - 1, Number(sSidurDate.substr(0, 2)), Number(oSG.substr(0, 2)), Number(oSG.substr(3, 2)));
            dSG.setDate(dSG.getDate() + Number(iSDayToAdd));
            SetDate(dSHL, Number(sSidurDate.substr(6, 4)), Number(sSidurDate.substr(3, 2)) - 1, Number(sSidurDate.substr(0, 2)), Number(oSHL.substr(0, 2)), Number(oSHL.substr(3, 2)));
            dSHL.setDate(dSHL.getDate() + Number(iSDayToAdd));
            SetDate(dSGL, Number(sSidurDate.substr(6, 4)), Number(sSidurDate.substr(3, 2)) - 1, Number(sSidurDate.substr(0, 2)), Number(oSGL.substr(0, 2)), Number(oSGL.substr(3, 2)));
            dSGL.setDate(dSGL.getDate() + Number(iSDayToAdd));
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
    }
   }
   function GetSidurTime(dStartHour,dEndHour)
   {
     var diff= new Date();     
     var timediff = new Date();
     //חישוב הפרש השעות בין שעת גמר הסידור לשעת התחלת הסידור
     diff.setTime(Math.abs(dStartHour.getTime() - dEndHour.getTime()));
     timediff = diff.getTime();     
     return (Math.floor(timediff / (1000 * 60 * 60)));     
   }
   function SortSidurim(){
     var i=0,sSH,sPrvSH;
     sSH=document.getElementById("lstSidurim_txtSH".concat(i));        
     sPrvSH=sSH;
     while (sSH!=null)
     {                        
        if ((sPrvSH.value>sSH.value))
        {
           document.getElementById("btnRefreshOvedDetails").click();
           break;
        }  
        sPrvSH=document.getElementById("lstSidurim_txtSH".concat(i));           
        i=i+1;
        sSH=document.getElementById("lstSidurim_txtSH".concat(i));       
     }    
   }
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
   function HasSidurHashlama(){
     var _HashlamaDLL, _Sidur;   
     var i=0;
     var j=0;
     var iNdex=0;
     var arrIndx = new Array();
     
     _Sidur = document.getElementById("lstSidurim_lblSidur" + i);
     _HashlamaDLL = document.getElementById("lstSidurim_ddlHashlama" + i);
     while ((_Sidur!=null)){
         if(_HashlamaDLL!=null){
             if (_HashlamaDLL.value > 0){
                arrIndx[iNdex] = i;//מספר הסידורים עם השלמה
                iNdex = ++iNdex;             
             } 
         }
         i=++i;
         _Sidur = document.getElementById("lstSidurim_lblSidur" + i);
        _HashlamaDLL = document.getElementById("lstSidurim_ddlHashlama" + i); 
     }    
     if ((arrIndx.length>0) && (arrIndx.length>=Number(document.getElementById("lstSidurim_hidNumOfHashlama").value))) {        
         var sIndex = arrIndx.join(",");
         _Sidur = document.getElementById("lstSidurim_lblSidur" + j);
         _HashlamaDLL = document.getElementById("lstSidurim_ddlHashlama" + j);
         while (_Sidur!=null){            
             if (sIndex.toString().indexOf(j)==-1){
                if (_HashlamaDLL!=null)
                    _HashlamaDLL.disabled = true;
             }         
             j=++j;
            _Sidur = document.getElementById("lstSidurim_lblSidur" + j);
            _HashlamaDLL = document.getElementById("lstSidurim_ddlHashlama" + j); 
        }       
     }         
   }
   function SetSidurimCollapseImg(){
     var _Sidur, _Img, _Peilut, stat;
     var i=0;
     
     _Sidur = document.getElementById("lstSidurim_lblSidur" + i);
     while  (_Sidur!=null){
        _Img = document.getElementById("lstSidurim_cImgS" + i);
        if (_Img!=null){
            _Peilut = document.getElementById("lstSidurim_" + padLeft(i,'0',3));
            if (_Peilut!=null){           
                for (var j=1; j<_Peilut.firstChild.childNodes.length; j++)
                { 
                    stat=_Peilut.firstChild.childNodes[j].cells[_COL_PEILUT_STATUS].firstChild.value;
                    switch (stat)
                    {
                        case "1":
                             _Img.src = "../../images/red_up_2_big.jpg";
                            break;
                        case "2":
                            _Img.src = "../../images/green_up_2_big.jpg";
                            break;  
                    }
                }
            }
        }       
        i=++i;
        _Sidur = document.getElementById("lstSidurim_lblSidur" + i); 
     }      
   }
   function EnabledSidurimListBtn(bDisabled){
     var _Sidur, _ImgPeilut,_imgCancel,_imgCancelPeilut;
     var i=0;
     
     _Sidur = document.getElementById("lstSidurim_lblSidur" + i);
     while  (_Sidur!=null){
        _ImgPeilut = document.getElementById("lstSidurim_imgAddPeilut" + i);
        if (_ImgPeilut!=null){ 
            if (!_ImgPeilut.disabled){           
                _ImgPeilut.disabled = bDisabled;
            }           
            if (_ImgPeilut.disabled){
                _ImgPeilut.src =  "../../images/plus-disable.jpg";
            }
            else{
                _ImgPeilut.src =  "../../images/plus.jpg";
            }
        }       
        _imgCancel= document.getElementById("lstSidurim_imgCancel" + i);
        if (_imgCancel!=null){ 
            if (!_imgCancel.disabled){
              _imgCancel.disabled = bDisabled;}           
            
            if (_imgCancel.disabled)
               if ((String(_imgCancel.className).indexOf("ImgChecked"))>-1) 
                     _imgCancel.className =  "ImgCheckedDisable";
               else      
                    _imgCancel.className =  "ImgCancelDisable";          
        } 
         _Peilut = document.getElementById("lstSidurim_" + padLeft(i,'0',3));
         if (_Peilut!=null){           
          for (var j=1; j<_Peilut.firstChild.childNodes.length; j++)
           {
               _imgCancelPeilut = _Peilut.firstChild.childNodes[j].cells[_COL_CANCEL];
               _imgAddNesiaReka = _Peilut.firstChild.childNodes[j].cells[_COL_ADD_NESIA_REKA];
               if (_imgAddNesiaReka.childNodes[0].disabled != undefined) {
                   if (!_imgAddNesiaReka.childNodes[0].disabled)
                       _imgAddNesiaReka.childNodes[0].disabled = bDisabled;
                   if (_imgAddNesiaReka.childNodes[0].disabled)
                       _imgAddNesiaReka.childNodes[0].src = "../../images/plus-disable.jpg";                                 
               }

               if (_imgCancelPeilut.firstChild.disabled!=undefined)
               {
               if (!_imgCancelPeilut.firstChild.disabled){
                   _imgCancelPeilut.firstChild.disabled = bDisabled;
                  // _imgAddNesiaReka.disabled = bDisabled;
                  }  
               
               if (_imgCancelPeilut.firstChild.disabled){
                 if(_imgCancelPeilut.firstChild.className!=undefined){
                     if ((String(_imgCancelPeilut.firstChild.className).indexOf("ImgChecked"))>-1) 
                       _imgCancelPeilut.firstChild.className =  "ImgCheckedDisable";
                     else
                       _imgCancelPeilut.firstChild.className =  "ImgCancelDisable";              
                    }
                }
              }
           }                    
         }     
        i=++i;
        _Sidur = document.getElementById("lstSidurim_lblSidur" + i); 
     }      
   }
   function chkPitzulHafsaka(iIndex, bUpdateCard){
     var iPitzulHafsaka=document.getElementById("lstSidurim_ddlPHfsaka".concat(iIndex)).value;           
     var iSidur=document.getElementById("lstSidurim_lblSidur".concat(iIndex)).innerHTML;        
     var iSidurNahagut = document.getElementById("lstSidurim_lblSidurNahagut".concat(iIndex)).innerHTML; 
     var bValid=false;   
     
     //var sSdDate = document.getElementById("lstSidurim_lblDate".concat(iIndex)).innerHTML;            
     if (iPitzulHafsaka==2){
        if (iSidurNahagut=='1'){
            iIndex=iIndex+1;           
            var _SidurNext = document.getElementById("lstSidurim_lblSidurNahagut".concat(iIndex));
            if (_SidurNext!=null){
               var _SidurNextId = document.getElementById("lstSidurim_lblSidur".concat(iIndex)).innerHTML;
               while (((_SidurNextId==SIDUR_CONTINUE_NAHAGUT) || (_SidurNextId==SIDUR_CONTINUE_NOT_NAHAGUT)) && (_SidurNext!=null)) {
                    iIndex= iIndex+1;
                    _SidurNext = document.getElementById("lstSidurim_lblSidurNahagut".concat(iIndex));
                    if (_SidurNext!=null){
                        _SidurNextId = document.getElementById("lstSidurim_lblSidur".concat(iIndex)).innerHTML;
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
       // wsGeneral.ChkIfSidurNahagut(iIndex,sSdDate, onSuccessSidurNahagut);
     }
   }
   function ChkCharigaVal(id)
   {
     var ddlChariga = document.getElementById("lstSidurim_ddlException".concat(id));  
     var iSelVal = Number(ddlChariga.value);
     var iCharigaType = Number(ddlChariga.getAttribute("ChrigaType"));
     if ((iSelVal>0) && (iSelVal!=iCharigaType) && (iCharigaType!=3) && (iCharigaType!=0) && (iCharigaType!=4)){
        alert('אין חריגה משעת התחלה או משעת גמר ');
     }
   }
   function CopyOtoNum(oRow){
       oId = String(oRow.id).substr(0,oRow.id.length-6); 
       var _CarNum = document.getElementById(oId).cells[_COL_CAR_NUMBER].childNodes[0];  
       var _CurrCarNum = _CarNum.value;
       var _OrgCarNum = _CarNum.getAttribute("OldV");
       var _MustCarNum = _CarNum.getAttribute("MustOtoNum");
       if (document.getElementById(oId).nextSibling != null) {
           var _NextPeilut = document.getElementById(oId).nextSibling.cells[_COL_CAR_NUMBER].childNodes[0];
           var _NextCarNum = _NextPeilut.value;
           if (_NextCarNum != undefined) {
               var _NextMustCarNum = _NextPeilut.getAttribute("MustOtoNum");
               if (_NextCarNum == '') { _NextCarNum = '0'; }
               if (_CurrCarNum != '') {
                   if ((_MustCarNum == '1') && (((_NextCarNum == _OrgCarNum) || (Number(_NextCarNum) == 0))) && (_NextMustCarNum)) {
                       document.getElementById("lblCarNumQ").innerText = "האם להחליף את מספר הרכב בכל הפעילויות בסידור בהן מספר הרכב הוא ריק או ".concat(String(_OrgCarNum));
                       document.getElementById("hidCarKey").value = _OrgCarNum + ',' + _CurrCarNum + ',' + oId;
                       document.getElementById("btnCopy").click();
                   }
               }
           }
       }
   }
   function disableUpdateBtn() {
        document.getElementById("btnUpdateCard").disabled = true;
    }
    function SetFocus(id, col) {         
        document.getElementById(id).cells[col].childNodes[0].select();
    }
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
            // iMakatType = enMakatType.mElement.GetHashCode();
            else if ((lTmpMakat >= 70100000) && (lTmpMakat <= 79900000))
                iMakatType = MKT_ELEMENT;   //5-Element          
            return iMakatType;
    }
   
    function isElementMechona(lMakat)
    {      
      return ((Number(String(lMakat).substr(0,3))==711) || (Number(String(lMakat).substr(0,3))==712) || (Number(String(lMakat).substr(0,3))==701));
    }
    function btnCopyOtoNum(iAction)
    {
     $find("pBehvCopy").hide();
     if (iAction==1){
        var arrKey = document.getElementById("hidCarKey").value.split(",");
        var _NextPeilutCarNum,_MustCarNum;
        var oId = arrKey[2];
        var _OrgCarNum = arrKey[0];
        var _CurrCarNum = arrKey[1];
        var _CarNum = document.getElementById(oId).cells[_COL_CAR_NUMBER].childNodes[0]; 
        var _NextPeilut = document.getElementById(oId).nextSibling;
        while (_NextPeilut!=null){
            _NextPeilutCarNum=_NextPeilut.cells[_COL_CAR_NUMBER].childNodes[0].value;
            if (_NextPeilutCarNum!=undefined){
            _MustCarNum = _NextPeilut.cells[_COL_CAR_NUMBER].childNodes[0].getAttribute("MustOtoNum");
            
            if (((_NextPeilutCarNum==_OrgCarNum) || (Number(_NextPeilutCarNum)==0) || (_NextPeilutCarNum==''))  && (_MustCarNum=='1')){
                if ((_NextPeilut.cells[_COL_CANCEL_PEILUT].firstChild.value!='1') && (_NextPeilut.cells[_COL_CAR_NUMBER].childNodes[0].disabled!=true)) {
                    _CarNum.setAttribute("OldV",_CurrCarNum);
                    _NextPeilut.cells[_COL_CAR_NUMBER].childNodes[0].setAttribute("OldV",_CurrCarNum);
                    _NextPeilut.cells[_COL_CAR_NUMBER].childNodes[0].value=_CurrCarNum;
                }
            }
            else{//אם נתקלים במספר רכב שונה עוצרים. אם נתקלים בפעילות שאינה דורשת מספר רכב או ריקה או 0 ממשיכים
                if ((_NextPeilutCarNum!='0') && (_NextPeilutCarNum!='') && (_MustCarNum!='0')){
                    break;
                }
            }
           }
            _NextPeilut = _NextPeilut.nextSibling;
        }
     }                
   }   
if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded(); 
