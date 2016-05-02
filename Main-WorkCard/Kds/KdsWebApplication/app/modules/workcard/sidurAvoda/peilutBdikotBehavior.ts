module modules.workcard {
    export class peilutBdikotBehavior {
        constructor(
            private IApiProviderService: modules.workcard.IApiProviderService,
            private IWorkCardStateService: modules.workcard.IWorkCardStateService) {
        }

        CheckMakat = (peilut: PeilutWC): ValidationResult=> {
            var result = new ValidationResult();
            var oVal = this.GetAttributeVal(peilut.Makat.Attributes, 'OldVal');
            if (peilut.Makat.Value < 100000) {
                peilut.Makat.Value = +oVal;
                result.errorMessage = 'מספר מק"ט לא תקין, יש להקליד מספר בין 6-8 ספרות';
                result.isValid = false;
            }
            else {
                var iMktType = this.GetMakatType(peilut.Makat.Value);
                if ((iMktType == modules.workcard.GeneralEvents.MKT_VISA) || ((iMktType == modules.workcard.GeneralEvents.MKT_ELEMENT) && (this.isElementMechona(peilut.Makat.Value))) || ((iMktType == modules.workcard.GeneralEvents.MKT_VISUT))) {
                    peilut.Makat.Value = +oVal;
                    result.errorMessage = 'מספר מק"ט לא תקין, יש להקליד מספר בין 6-8 ספרות';
                    result.isValid = false;
                }
            }
            return result;
        }

        CheckValidMinutes = (peilut: PeilutWC) : ValidationResult=> {
            var result = new ValidationResult();
            var param98 = this.IWorkCardStateService.workCardResult.oParams.iMaxMinutsForKnisot;
            var maxMinutes;
            if ((peilut.MisparKnisa.Value > 0) && (peilut.MakatType.Value == 1))
                maxMinutes = param98;
            else maxMinutes = peilut.DakotTichnun.Value;

            if (+peilut.DakotBafoal.Value >= 0 && +peilut.DakotBafoal.Value > +peilut.DakotTichnun.Value) {
                result.errorMessage = "יש להקליד ערך בין 0 דקות לבין " + maxMinutes + " דקות";
                result.isValid = false;
            }
            return result;
        }

        ChkOto = (iSidur: number, iPeilut: number, sidurim: SidurWC[]): ng.IPromise<ValidationResult> => {
            var result = new ValidationResult();
            var peilut = sidurim[iSidur].PeilutList[iPeilut];
            var lOtoNo = peilut.NumOto.Value.toString();
            var oldNumBus = this.GetAttributeVal(peilut.NumOto.Attributes, 'OldVal');
            if (lOtoNo != oldNumBus) {
                if ((lOtoNo != '') && (trim(String(lOtoNo)).length >= 5)) {
                    return this.IApiProviderService.checkOtoNo(+lOtoNo)
                        .then(res=> {

                            if (res == '0') {
                                peilut.NumOto.Value = +oldNumBus;
                                result.errorMessage = "מספר רכב שגוי";
                                result.isValid = false;
                            }
                            else {
                                this.CopyOtoNum(sidurim[iSidur].PeilutList, iPeilut);
                            }
                            return result;
                        }, error=> {
                            result.errorMessage = error.data.Message;
                            result.isValid = false;
                            return result;
                    });
                }

            }
            
        }

        CopyOtoNum = (ListPeilut: PeilutWC[], iPeilut: number) => {
            var peilut = ListPeilut[iPeilut];
            var oldNumBus = this.GetAttributeVal(peilut.NumOto.Attributes, 'OldVal');
            var mustNumCar = this.GetAttributeVal(peilut.NumOto.Attributes, 'MustOtoNum');

            if (iPeilut < (ListPeilut.length - 1)) {
                var nextPeilut = ListPeilut[iPeilut + 1];
                var nextNumBus = nextPeilut.NumOto.Value.toString();
                if (nextNumBus == '') nextNumBus = '0';
                var nextMustNumCar = this.GetAttributeVal(nextPeilut.NumOto.Attributes, 'MustOtoNum');


                if (((mustNumCar == '1') && (((nextNumBus == oldNumBus) || (Number(nextNumBus) == 0))) && (nextMustNumCar == '1')) || (nextPeilut.Makat.Value.toString() == '') || (nextPeilut.Makat.Value.toString() == '0')) {

                    var answer = confirm("האם להחליף את מספר הרכב בכל הפעילויות בסידור בהן מספר הרכב הוא ריק או ".concat(String(oldNumBus)));
                    if (answer) {
                        var i = iPeilut;
                        while (i < ListPeilut.length) {
                            nextPeilut = ListPeilut[i];
                            nextNumBus = nextPeilut.NumOto.Value.toString();
                            nextMustNumCar = this.GetAttributeVal(nextPeilut.NumOto.Attributes, 'MustOtoNum');
                            if (((((nextNumBus == oldNumBus || i == iPeilut) || ((nextNumBus == '') || Number(nextNumBus) == 0)) && (((nextMustNumCar == '1') || ((nextPeilut.Makat.Value.toString() == '') || (nextPeilut.Makat.Value.toString() == '0'))))))) {
                                if ((nextPeilut.CancelPeilut.Value != '1')) {// && (_NextPeilut.cells[_COL_CAR_NUMBER].childNodes[0].disabled != true)) {
                                    nextPeilut.NumOto.Value = peilut.NumOto.Value;
                                    this.SetAttributeVal(nextPeilut.NumOto.Attributes, 'OldVal', peilut.NumOto.Value);
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

        SetKnisaActualMin = (peilut: PeilutWC): ValidationResult=> {
            var result = new ValidationResult();
            // var dDate= new Date();
            if ((peilut.MisparKnisa.Value > 0) && peilut.CancelPeilut.Value == 'ImgKnisaS') {//אם כניסה לפי צורך
                {
                    if (this.KnisaLefiZorech(peilut.Teur.Value, peilut.MisparKnisa.Value)) {
                        var lMakat = peilut.Makat.Value;

                        var objDetails = this.IWorkCardStateService.ovedDetails;
                        //   var dDate= modules.common.DateHelper.convertToDate(objDetails.DetailsDate);  
                    
                        this.IApiProviderService.getKnisaActualMin(lMakat, objDetails.DetailsDate, peilut.MisparKnisa.Value)
                            .then(res=> {
                                if (res != "0") {
                                    peilut.DakotBafoal.Value = res;
                                }
                                else {
                                    // peilut.CancelPeilut.Value = "ImgKnisaS";
                                    result.errorMessage = 'לא נמצא משך לכניסה ' + peilut.Teur.Value;
                                    result.isValid = false;
                                }
                            }, error=> {
                                //this.error = error.data.Message;
                                //this.modalErrorsShown = true;
                            });
                    }
                }
            }
            return result;
        }
        KnisaLefiZorech = (sText: string, iKnisa: number): boolean => {
            if (sText == null)
                return true; //?? 0=true or false
            else
                return (((sText.indexOf('לפי צורך') > 0) || (sText.indexOf('לפי-צורך') > 0)) && iKnisa > 0);
        }
        GetAttributeVal = (attrs: AttributeField[], sName: string): string=> {
            var val;
            attrs.forEach(function (a) {
                if (a.Name == sName)
                    val = a.Value;

            });
            return val;
        }

        SetAttributeVal = (attrs: AttributeField[], sName: string, sVal: any) => {
            var val;
            attrs.forEach(function (a) {
                if (a.Name == sName)
                    a.Value = sVal;

            });
        }

        GetMakatType = (lMakat: number): number=> {
            var iMakatType;
            var lTmpMakat = 0;

            lTmpMakat = Number(lMakat);
            if (lTmpMakat.toString().substr(0, 1) == "5" && (lTmpMakat >= 50000000))
                iMakatType = modules.workcard.GeneralEvents.MKT_VISA; //6-Visa
            else if ((lTmpMakat >= 100000) && (lTmpMakat < 50000000))
                iMakatType = modules.workcard.GeneralEvents.MKT_SHERUT; //1-kav sherut            
            else if ((lTmpMakat >= 60000000) && (lTmpMakat <= 69999999))
                iMakatType = modules.workcard.GeneralEvents.MKT_EMPTY; //2-Empty
            else if ((lTmpMakat >= 80000000) && (lTmpMakat <= 99999999))
                iMakatType = modules.workcard.GeneralEvents.MKT_NAMAK; //3-Namak
            else if ((lTmpMakat >= 70000000) && (lTmpMakat <= 70099999))
                iMakatType = modules.workcard.GeneralEvents.MKT_VISUT; //4-ויסות             
            else if ((lTmpMakat >= 70100000) && (lTmpMakat <= 79900000))
                iMakatType = modules.workcard.GeneralEvents.MKT_ELEMENT;   //5-Element          
            return iMakatType;
        }

        isElementMechona = (lMakat: number): boolean => {
            return ((Number(String(lMakat).substr(0, 3)) == 711) || (Number(String(lMakat).substr(0, 3)) == 712) || (Number(String(lMakat).substr(0, 3)) == 701));
        }


    }

    export class ValidationResult {
        constructor() {
            this.isValid = true;
        }
        isValid: boolean;
        errorMessage: string;
    }
}