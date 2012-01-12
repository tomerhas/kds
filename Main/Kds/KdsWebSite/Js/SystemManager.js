   function openEdit(){
        var url=$get(queryStringTargetControlID).value;
        var retVal=window.showModalDialog(url,'editor','dialogHeight:450px;dialogWidth:480px;status:no;scroll:no');
        if(retVal=='OK'){
           refresh();
        }
    }
    
    function EndRequestHandler(sender, args) {
       if (args.get_error() == undefined){
           if($get(queryStringTargetControlID).value!=''){
            openEdit();
            $get(queryStringTargetControlID).value='';
           }
       }
       else
           alert("There was an error" + args.get_error().message);
    }
    
    function load() {
       Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    }
    if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded(); 
