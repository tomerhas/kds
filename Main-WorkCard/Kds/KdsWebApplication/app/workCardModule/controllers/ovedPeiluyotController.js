workCardApp.controller('ovedPeiluyotController',['$rootScope','$scope','apiProvider', 'workCardStateService','$log',
    function ($rootScope, $scope, apiProvider, workCardStateService, $log) {

        var vm = this;
        vm.Sidurim = {};
        vm.NumSidurim = 0;
        vm.ChangeCollapeImg = ChangeCollapeImg;
        vm.CheckValidMinutes = CheckValidMinutes;
        vm.CheckMakat = CheckMakat;
        vm.FocusText = FocusText;
        vm.SetKnisaActualMin = SetKnisaActualMin;
        vm.SibotDivuachList = {};
        vm.HarigaList = {};
        vm.PizulList = {};
        vm.HshlamaList = {};
        vm.ChkOto = ChkOto;
        
        vm.datePickerOption = {};

        var MKT_VISUT = 4;
        var MKT_VISA = 6;
        var MKT_SHERUT = 1;
        var MKT_EMPTY = 2;
        var MKT_NAMAK = 3;
        var MKT_ELEMENT = 5;

        activate();

        function activate() {
            RegisterToOvedPeiluyotChanged();
            UpdateLists();
            $rootScope.focusTextById('this');
          //  $('.timepicker').timepicker();
        }

        function UpdateLists() {
            if (workCardStateService.lookupsContainer.container) {
                vm.SibotDivuachList = workCardStateService.lookupsContainer.container.SibotLedivuachList;
                vm.HarigaList = workCardStateService.lookupsContainer.container.HarigaList;
                vm.PizulList = workCardStateService.lookupsContainer.container.PizulList;
                vm.HshlamaList = workCardStateService.lookupsContainer.container.HashlamaList;
            }
        }

        function SetDateOptions() {
            vm.datePickerOption = {
                change: function (a,b,c)
                {
                    var curVal = this.value();
                    if (curVal == null)
                    {
                        this.value(new Date());
                    }
                }
            }
        };

        function RegisterToOvedPeiluyotChanged()
        {
            $scope.$on('ovedPeiluyot-changed', function (event, args) {
                $log.debug("ovedPeiluyotController recieved broadcast ovedPeiluyot-changed event");
                vm.Sidurim = workCardStateService.cardGlobalData.workCardResult.Sidurim.SidurimList;
                vm.Sidurim.forEach(function (sidur) {
                    sidur.MyFullDate = new Date(sidur.FullShatHatchala.Value);
                });
                InitializeCols();
                vm.NumSidurim = vm.Sidurim.length;
                UpdateLists();
            });
        }

        function InitializeCols()
        {
            var bPeilutActive;
            var bSidurActive; 
            vm.Sidurim.forEach(function (sidur) {
                bSidurActive = sidur.SidurActive.Value;
                sidur.PeilutList.forEach(function (peilut) {
                    bPeilutActive = peilut.PeilutActive.Value;
                    if ((peilut.MisparKnisa.Value > 0) && (peilut.MakatType.Value == 1))  //כניסה לפי צורך
                    {
                       if (peilut.DakotBafoal.Value == 0)
                           peilut.CancelPeilut.Value = "ImgKnisaS";
                       else
                           peilut.CancelPeilut.Value = "ImgCheckedPeilut";

                          if (bSidurActive)
                              peilut.PeilutActive.Value = true;
                     }
                    else
                    {
                        if (bPeilutActive)
                            peilut.CancelPeilut.Value = "ImgCheckedPeilut";
                        else
                            peilut.CancelPeilut.Value = "ImgCancel";
                    }
                });
            });
            
        }
        function CheckMakat(peilut) {
            var oVal = GetAttributeVal(peilut.Makat.Attributes, 'OldVal');
            if (peilut.Makat.Value < 100000) {
                peilut.Makat.Value = oVal;
                alert('מספר מק"ט לא תקין, יש להקליד מספר בין 6-8 ספרות');
            //    FocusText(obj.this)
               // oMkt.select();
            }
            else {
                var iMktType = GetMakatType(peilut.Makat.Value);
                if ((iMktType == MKT_VISA) || ((iMktType == MKT_ELEMENT) && (isElementMechona(peilut.Makat.Value))) || ((iMktType == MKT_VISUT))) {
                    peilut.Makat.Value = oVal;
                    alert('לא ניתן להקליד מק"ט ויזה או ויסות או הכנת מכונה');
                    //  oMkt.select();
                }

                //חסר טיפול
            }
         
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
            else if ((lTmpMakat >= 70100000) && (lTmpMakat <= 79900000))
                iMakatType = MKT_ELEMENT;   //5-Element          
            return iMakatType;
        }

        function isElementMechona(lMakat) {
            return ((Number(String(lMakat).substr(0, 3)) == 711) || (Number(String(lMakat).substr(0, 3)) == 712) || (Number(String(lMakat).substr(0, 3)) == 701));
        }

        function CheckValidMinutes(peilut) {
            var param98 = workCardStateService.cardGlobalData.workCardResult.oParams.iMaxMinutsForKnisot;
            var maxMinutes;
            if ((peilut.MisparKnisa.Value > 0) && (peilut.MakatType.Value == 1))
                maxMinutes = param98;
            else maxMinutes = peilut.DakotTichnun.Value;

            if (peilut.DakotBafoal.Value >= 0 && peilut.DakotBafoal.Value > peilut.DakotTichnun.Value)
                alert("יש להקליד ערך בין 0 דקות לבין " + maxMinutes + " דקות");

        }

        function ChkOto(iSidur, iPeilut) {
            var peilut = vm.Sidurim[iSidur].PeilutList[iPeilut];
            var lOtoNo = peilut.NumOto.Value;
            var oldNumBus = GetAttributeVal(peilut.NumOto.Attributes, 'OldVal');
            if (lOtoNo != oldNumBus) {
                //  var KeyID = event.keyCode;
                //  if (((KeyID >= 48) && (KeyID <= 57)) || ((KeyID >= 96) && (KeyID <= 105))) {
                //    DisabledControls(); //SetLvlChg(3);
                if ((lOtoNo != '') && (trim(String(lOtoNo)).length >= 5)) {

                    apiProvider.checkOtoNo(lOtoNo).then(function (res) {
                        if (res.data.d == '0') {
                            peilut.NumOto.Value = oldNumBus;
                            alert("מספר רכב שגוי");
                        }
                        else {
                            // $get(oId).cells[_COL_CAR_NUMBER].childNodes[0].disabled = true;
                            CopyOtoNum(vm.Sidurim[iSidur].PeilutList, iPeilut);
                        }
                    });

                }
            }
         //   }
        }
        
        function CopyOtoNum(ListPeilut, iPeilut) {
            var peilut = ListPeilut[iPeilut];
            var oldNumBus = GetAttributeVal(peilut.NumOto.Attributes, 'OldVal');
            var mustNumCar = GetAttributeVal(peilut.NumOto.Attributes, 'MustOtoNum');

            if (iPeilut < (ListPeilut.length - 1)) {
                var nextPeilut = ListPeilut[iPeilut + 1];
                var nextNumBus = nextPeilut.NumOto.Value;
                if (nextNumBus == '') nextNumBus = '0';
                var nextMustNumCar = GetAttributeVal(nextPeilut.NumOto.Attributes, 'MustOtoNum');

               
                if (((mustNumCar == '1') && (((nextNumBus == oldNumBus) || (Number(nextNumBus) == 0))) && (nextMustNumCar == '1')) || (nextPeilut.Makat.Value == '') || (nextPeilut.Makat.Value == '0')) {

                    var answer = confirm("האם להחליף את מספר הרכב בכל הפעילויות בסידור בהן מספר הרכב הוא ריק או ".concat(String(oldNumBus)));
                    if (answer)
                    {
                        var i = iPeilut;
                        while (i < ListPeilut.length)
                        {
                            nextPeilut = ListPeilut[i];
                            nextNumBus = nextPeilut.NumOto.Value;
                            nextMustNumCar = GetAttributeVal(nextPeilut.NumOto.Attributes, 'MustOtoNum');
                            if (((((nextNumBus == oldNumBus || i == iPeilut) || ((nextNumBus == '') || Number(nextNumBus) == 0)) && (((nextMustNumCar == '1') || ((nextPeilut.Makat.Value == '') || (nextPeilut.Makat.Value == '0')))))))
                            {
                                if ((nextPeilut.CancelPeilut.Value !='1')){// && (_NextPeilut.cells[_COL_CAR_NUMBER].childNodes[0].disabled != true)) {
                                    nextPeilut.NumOto.Value = peilut.NumOto.Value;
                                    SetAttributeVal(nextPeilut.NumOto.Attributes, 'OldVal', peilut.NumOto.Value);
                                 }
                            }
                            else {//אם נתקלים במספר רכב שונה עוצרים. אם נתקלים בפעילות שאינה דורשת מספר רכב או ריקה או 0 ממשיכים
                                if ((nextNumBus != '0') && (nextNumBus != '') && (nextMustNumCar != '0')) {
                                    break;
                                }
                            }
                            i++;
                        }
                    }
                }
            }
        }

        function SetKnisaActualMin(peilut) {
            if ((peilut.MisparKnisa.Value > 0) && peilut.CancelPeilut.Value == 'ImgKnisaS') {//אם כניסה לפי צורך
                {
                    if (KnisaLefiZorech(peilut.Teur.Value,peilut.MisparKnisa.Value)){ 
                        var lMakat = peilut.Makat.Value;

                        var objDetails = workCardStateService.cardGlobalData.ovedDetails;
                        var sDate = objDetails.DetailsDate.split('/');
                        var dDate = new Date(sDate[2], sDate[1] - 1, sDate[0]);

                        apiProvider.getKnisaActualMin(lMakat, dDate, peilut.MisparKnisa.Value).then(function (res) {
                            if (res.data.d != "0") {
                                peilut.DakotBafoal.Value = result;
                            }
                            else {
                               // peilut.CancelPeilut.Value = "ImgKnisaS";
                                alert('לא נמצא משך לכניסה ' + peilut.Teur.Value);
                            }
                        });       
                   }
                }
            }
        }

        function KnisaLefiZorech(sText, iKnisa) {
            if (sText == null)
                return 0;
            else
                return (((sText.indexOf('לפי צורך') > 0) || (sText.indexOf('לפי-צורך') > 0)) && iKnisa>0);       
        }


       /* function ChangeStatusPeilut(iSidur, iPeilut, FirstMkt, OrgMktType, SubMkt, PeilutAv) {
           // SetBtnChanges();
            var oColCancel;//, oColPeilutCancel;
            var misparKnisa;
         //   var iSidur = 0;
           // var _NextRow;
           // var arrKnisa, lMkt, lNxtMkt;
            var peilut=  vm.Sidurim[iSidur].PeilutList[iPeilut];
            //נבצע רק אם שורה נוכחית או שכניסות/ויסותים/ אלמנט הפסקה
            if ((FirstMkt == 0) || ((SubMkt == 1) && (FirstMkt != 0))) {
                oColCancel = peilut.CancelPeilut.Value;
              //  oColPeilutCancel = $get(Row.id).cells[_COL_CANCEL_PEILUT].childNodes[0];
                if ((oColCancel == "ImgCheckedPeilut") || (oColCancel == "ImgCheckedDisablePeilut")) {
                    if (FirstMkt != 0) //אם כניסה בעקבות שינוי רשומת אב, נעביר את רשומת האב
                        SetPeilutStatus(Row.id, true, iSidur, -1, PeilutAv);
                    else {
                        if (KnisaLefiZorech(peilut.Teur.Value, peilut.MisparKnisa.Value))
                            SetPeilutStatus(Row.id, false, iSidur, -1);
                        else
                            SetPeilutStatus(Row.id, true, iSidur, -1);
                    }
                }
                else {
                    if (PeilutAv != null) {
                        if (PeilutAv.CancelPeilut.Value == "ImgCancel")//אם רשומת האב מבוטלת לא נאפשר עדכון הפעילויות בנים
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
                lMkt = peilut.Makat.Value;
                //אם קו שירות, נבטל גם ויסותים וכניסות של אותו קו
                if (((Number(lMkt >= 100000)) && (Number(lMkt < 50000000))) &&  peilut.MisparKnisa.Value == 0) {
                    OrgMktType = 1;
                    PeilutAv = peilut;
                }
            }
            //אם במקור ביטלנו קו שירות נבטל גם כניסות 
            if (((Number(OrgMktType) == 1)) && (SubMkt != 2)) {
                if (peilut != null) {
                    _NextRow = $get(Row.id).nextSibling;
                    if (_NextRow != null) {
                       // if (_NextRow.cells[_COL_KNISA].childNodes[0].nodeValue != '') {
                        misparKnisa = _NextRow.MisparKnisa.Value;
                        lNxtMkt = NextRow.Makat.Value;
                       // } //,כניסות 
                        if (misparKnisa > 0) //|| ((lNxtMkt >= 70000000) && (lNxtMkt <= 70099999)) || (((lNxtMkt >= 74300000) && (lNxtMkt <= 74399999))))
                            ChangeStatusPeilut(_NextRow, FirstMkt, OrgMktType, 1, PeilutAv);
                        else {
                          //  lNxtMkt = Number(_NextRow.cells[_COL_MAKAT].childNodes[0].value);
                            //אם הגענו לקו שירות נעצור
                            if ((((Number(lNxtMkt >= 100000)) && (Number(lNxtMkt < 50000000))) && (_NextRow.MisparKnisa.Value == 0)))
                                ChangeStatusPeilut(_NextRow, FirstMkt, OrgMktType, 2); //קן שירות- תנאי עצירה
                            else
                                ChangeStatusPeilut(_NextRow, FirstMkt, OrgMktType, 0);
                        }
                    }
                }
            }
            return false;
        }
        function SetPeilutStatus(RowId, bFlag, iSidur, iSidurIndex, PeilutAv) {
        }*/
        function ChangeCollapeImg(index) {
            if (vm.Sidurim[index].CollapseSrc.Value.indexOf("openArrow") > -1) {
                vm.Sidurim[index].CollapseSrc.Value = "../../../../Images/closeArrow.png";
            }
            else vm.Sidurim[index].CollapseSrc.Value = "../../../../Images/openArrow.png";
        }

        function GetAttributeVal(attrs,sName) {
            var val;
            attrs.forEach(function (a) {
                if (a.Name == sName)
                    val= a.Value;

            });
            return val;
        }

        function SetAttributeVal(attrs, sName,sVal) {
            var val;
            attrs.forEach(function (a) {
                if (a.Name == sName)
                    a.Value = sVal;

            });
        }
    }]);