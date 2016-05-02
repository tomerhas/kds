module modules.common {

    export interface IUiHelperService {
        EnablePage(value: boolean): void;
        FocusTextById(id: string): void;
        DisabledControls(): void;
    }

    class UiHelperService implements IUiHelperService {
        
        constructor(private IWorkCardStateService: modules.workcard.IWorkCardStateService) {
        }

         public EnablePage=(value:boolean):void=> {
            if (value != true) {
                $('input, textarea, select')
                    .attr('disabled', 'disabled');//'disabled');..
                //this.DisablePage = true;
                this.IWorkCardStateService.DisablePageX = true;
            }
            else {
                $('input, textarea, select').removeAttr("disabled");
                this.IWorkCardStateService.DisablePageX = false;
               // this.DisablePage = false;
            }
        }

        public FocusTextById=(id:string):void=> {
            $("#" + id).select();
         }

        public DisabledControls=()=> {
            $('.cntlDis').attr('disabled', 'disabled');
        }

        //public FocusText=(obj:any)=> {
        //obj.select();
        //}
        //public trim=(str:string)=> {
        //    return str.replace(/^[\s]+/, '').replace(/[\s]+$/, '').replace(/[\s]{2,}/, ' ');
        //}
    }

    angular.module("modules.common").service("IUiHelperService", UiHelperService);
}