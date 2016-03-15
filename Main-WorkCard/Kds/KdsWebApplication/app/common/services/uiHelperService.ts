module modules.common {

    export interface IUiHelperService {
        EnablePage(value: string):void;
    }

    class UiHelperService implements IUiHelperService {
        public DisablePage: boolean;
        constructor() { }

         public EnablePage=(value:string):void=> {
            if (value != '') {
                $('input, textarea, select')
                    .attr('disabled', value);//'disabled');..
                this.DisablePage = true;
            }
            else {
                $('input, textarea, select').removeAttr("disabled");
                this.DisablePage = false;
            }
        }

    }

    angular.module("modules.common").service("IUiHelperService", UiHelperService);
}