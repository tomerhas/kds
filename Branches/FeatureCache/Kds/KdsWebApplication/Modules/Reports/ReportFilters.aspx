<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Modules_Reports_ReportFilters" Codebehind="ReportFilters.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src='../../js/jquery.js' type='text/javascript'></script>
    <script src="../../Js/String.js" type="text/javascript"></script>
    <script src="../../Js/GeneralFunction.js" type="text/javascript"></script>
    <script src="../../Js/ListBoxExtended.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            EnlargeFieldSetsForIE8(null, "DriverWithoutSignature,DriverWithoutTacograph", 100);
             

            var KamutIdkuneyRashemet = {
                type: "KamutIdkuneyRashemet",
                snif: $('#ctl00_KdsContent_P_SNIF'),
                ezor: $('#ctl00_KdsContent_P_EZOR'),
                rashemet: $('#ctl00_KdsContent_PMISRASHEMETTxtbxAutoComplete'),
                Init: function () {
                    this.rashemet.attr('disabled', 'disabled');
                    this.ezor.attr('disabled', 'disabled');
                    this.snif.attr('disabled', 'disabled');
                    var k = this;

                    if ($("input[name='ctl00$KdsContent$P_TEZUGA']:checked").val() != undefined) {
                        DisabledRashemetOrSnif($("input[name='ctl00$KdsContent$P_TEZUGA']:checked").val(), k);
                    }
                    else {
                        $("#ctl00_KdsContent_P_TEZUGA_0").attr('checked', true);
                        DisabledRashemetOrSnif($("input[name='ctl00$KdsContent$P_TEZUGA']:checked").val(), k);
                    }


                    $("input[name='ctl00$KdsContent$P_TEZUGA'").click(function () {
                        // if (queries['RdlName'] == 'KamutIdkuneyRashemet') {
                        DisabledRashemetOrSnif($(this).val(), k);
                        // }
                    });
                }
            }


            var queries = {};
            $.each(document.location.search.substr(1).split('&'), function (c, q) {
                var i = q.split('=');
                queries[i[0].toString()] = i[1].toString();
            });

            switch (queries['RdlName']) {
                case 'KamutIdkuneyRashemet':
                    {
                        KamutIdkuneyRashemet.Init();
                    }
                    break;
            }
        });

        function DisabledRashemetOrSnif(val, k) {
            if (val == "1") {
                k.rashemet.removeAttr('disabled', 'disabled');
                k.ezor.attr('disabled', 'disabled');
                k.snif.attr('disabled', 'disabled');
                $("#ctl00_KdsContent_P_SNIF option:selected").val('');

            }
            else {
                k.rashemet.attr('disabled', 'disabled');
                k.ezor.removeAttr('disabled', 'disabled');
                k.snif.removeAttr('disabled', 'disabled');
                k.rashemet.val('');
                $('#ctl00_KdsContent_PMISRASHEMETListBoxExtended').empty();
            }
        }
      
    
    </script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="KdsContent">
    <asp:UpdatePanel ID="PnlFilter" runat="server" RenderMode="Inline" UpdateMode="Always">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="UserId" runat="server" Style="display: none" />
                        <div id="DivDynamicFilter" runat="server">
                            <fieldset id="fsFilter" runat="server"  class="FilterFieldSet">
                                <legend id="LegendFilter" style="background-color: White">סינון </legend>
                                <table width="100%" class="FilterTable">
                                    <tr>
                                        <td width="100%" id="TdFilter" runat="server">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtControlChanged" runat="server" OnClick="BtControlChanged_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button Text="הצג" ID="btnDisplay" runat="server" CssClass="ImgButtonSearch"
                            OnClick="btnDisplay_Click" OnClientClick="CancelAllSelectedItems();" />
                    </td>
                </tr>
            </table>
            <input type="hidden" id="Param100" name="Param100" runat="server" />
            <input type="hidden" id="Param280" name="Param280" runat="server" />
            <input type="hidden" id="MisRashamot" name="MisRashamot" runat="server" />
        </ContentTemplate>
   <%--  <Triggers><asp:PostBackTrigger ControlID="btnDisplay"  /></Triggers>--%>
    </asp:UpdatePanel>
    <script language="javascript" type="text/javascript">
       
        function CancelAllSelectedItems() {
            var src = document.getElementsByTagName("Select");
            for (var objNumber = 0; objNumber < src.length; objNumber++) {
                if (String(src[objNumber].name).indexOf("ListBoxExtended", 0) > 0) {
                    for (var count = 0; count < src[objNumber].options.length; count++) {
                        if (src[objNumber].options[count].selected == true) {
                            src[objNumber].options[count].selected = false;
                        }
                    }
                }
            }
        }
        function  FireControlChanged() {
            CancelAllSelectedItems();
            var BtControlChanged = document.getElementById("ctl00_KdsContent_BtControlChanged");
            BtControlChanged.click();
            //            SetAutoCompleteExtender();
        }

        function SetAutoCompleteExtender() {
            var CurrentRdlname = GetQueryStringValue("RdlName");
            var AutoCompleteExtender = "";

            if (CurrentRdlname == "Presence") {
                var txtStartDate = document.getElementById("ctl00_KdsContent_P_STARTDATE").value;
                var txtEndDate = document.getElementById("ctl00_KdsContent_P_ENDDATE").value;
                var txtUserID = document.getElementById("ctl00_KdsContent_UserId").value;
                var txtWorkerLevel = SelectedItemOfRadioButtonList("ctl00_KdsContent_P_WORKERVIEWLEVEL");
                AutoCompleteExtender = txtWorkerLevel + ',' + txtUserID + ',' + txtStartDate + ',' + txtEndDate;
                var AutoComplete = $find('ctl00_KdsContent_AutoComplete_PMISPARISHITxtbxAutoComplete');
                AutoComplete.set_contextKey(AutoCompleteExtender);
            }
        }

        function UpdateHiddenField(ControlName) {
            var OriginControl = document.getElementById("ctl00_KdsContent_" + ControlName);
            var HiddenControl = document.getElementById("ctl00_KdsContent_Hidden" + ControlName);
            HiddenControl.value = value;
        }
        function RitzaValidation(val, args) {
            var txtMisRitza = document.getElementById("ctl00_KdsContent_P_MIS_RITZA").value;
            if (txtMisRitza == -1) {
                args.IsValid = false;
            }
        }
        function IsEmpty(sender, args) {
            var CurrentObj = sender.controltovalidate;
            if (document.getElementById(CurrentObj).value == "") {
                args.IsValid = false;
            }
        }


        function IsEmptyPresence(sender, args) {
           // debugger;
            var CurrentObj = sender.controltovalidate;
            var objCheckBoxList = document.getElementById("ctl00_KdsContent_P_SNIF");
            var OptionsChecked = false;
            if (objCheckBoxList != null) {
                for (var i = 0; i <= objCheckBoxList.length - 1; i++)
                    if (objCheckBoxList.options[i].selected==true){
                   // if (objCheckBoxList.children[0].checked == true) {
                        OptionsChecked = true;
                        break;
                    }
            }
            if (!OptionsChecked && document.getElementById(CurrentObj).value == "") {
                args.IsValid = false;
            }
        }
        function IsAlowedPeriod(sender, args) {
            //   debugger
            var CurrentObj = sender.controltovalidate;
            var chodesh_me = document.getElementById(CurrentObj).value.split('/'); //ValidatorName.substr(0, ValidatorName.length - 2) + "ME").value;
            var FromDate = new Date(Number(chodesh_me[2]), Number(chodesh_me[1] - 1), Number(chodesh_me[0]), '00', '00', '00');
            var today = new Date();
            var mis = document.getElementById("ctl00_KdsContent_P_MIS_RASHEMET").value;
            var Param100 = document.getElementById("ctl00_KdsContent_Param100").value;
            //debugger;
            today.setDate(1);
            today.setMonth(today.getMonth() - Param100);
            today.setHours(0);
            today.setMinutes(0);
            today.setSeconds(0);
            today.setMilliseconds(0);

            if (FromDate.getTime() < today.getTime()) {
                alert("לא ניתן להוציא דו''ח לתאריך שנבחר");
                args.IsValid = false;
            }
            else if (FromDate.getTime() > (new Date().getTime())) {
                alert("לא ניתן להוציא דו''ח מעבר להיום");
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
                if (mis != "")
                    checkRashemet(mis, args);
            }

        }
        function CheckRashamSucceded(result, args) {
            args.IsValid = (result > 0);
        }
        function IsValidRashemet1(sender, args) {
            //  debugger 
            var CurrentObj = sender.controltovalidate;
            var mis = document.getElementById(CurrentObj).value;
            if (mis == "") {
                args.IsValid = false;
            }
            else checkRashemet(mis, args);

        }
        function IsValidRashemetByTezuga(sender, args) {
            if (($('#ctl00_KdsContent_P_TEZUGA_0').is(':checked')) || (!($('#ctl00_KdsContent_P_TEZUGA_0').is(':checked')) && !($('#ctl00_KdsContent_P_TEZUGA_1').is(':checked')))) {
                var CurrentObj = sender.controltovalidate;
                var mis = document.getElementById(CurrentObj).value;
                if (mis == "") {
                    args.IsValid = false;
                }
                else {
                    var str = mis.split(',');
                    for (i = 0; i < str.length; i++) {
                        if(str[i].length>0)
                            checkRashemet(str[i], args);
                    }
                }
            }
        }
        function checkRashemet(mis, args) {
            if (document.getElementById("ctl00_KdsContent_MisRashamot").value.indexOf("," + mis + ",") == -1) {
                alert("מספר אישי של רשמת " + mis + " לא קיים או לא פעיל לתאריך הנבחר");
                document.getElementById("ctl00_KdsContent_P_MIS_RASHEMET").value = "";
                args.IsValid = false;
            }
        }

        function CountRechiv(sender, args) {
            args.IsValid = ($("#" + sender.controltovalidate).find("option:selected").length < 15)
        }
        function CblMaamadValidation(val, args) {
            args.IsValid = true;
        }
        function ValidateCheckBoxList(sender, args) {
            var ValidatorName = Mid(sender.id, 0, sender.id.length - 13);
            var objCheckBoxList = document.getElementById(ValidatorName);
            var OptionsChecked = false;
            if (objCheckBoxList != null) {
                for (var i = 0; i <= objCheckBoxList.cells.length - 1; i++) {
                    if (objCheckBoxList.cells[i].children[0].checked == true) {
                        OptionsChecked = true;
                        break;
                    }
                }
                args.IsValid = OptionsChecked;
            }
        }
        function ValidateTvachChodashim(sender, args) {
            var chodesh_me = document.getElementById('ctl00_KdsContent_P_STARTDATE').value.split('/'); //ValidatorName.substr(0, ValidatorName.length - 2) + "ME").value;
            var chodesh_ad = document.getElementById('ctl00_KdsContent_P_ENDDATE').value.split('/'); //ValidatorName.substr(0, ValidatorName.length - 2) + "AD").value;
            var MeDate, AdDate;
            if (chodesh_me.length == 3) {
                MeDate = new Date(Number(chodesh_me[2]), Number(chodesh_me[1]) - 1, Number(chodesh_me[0]), '00', '00', '00');
                AdDate = new Date(Number(chodesh_ad[2]), Number(chodesh_ad[1]) - 1, Number(chodesh_ad[0]), '00', '00', '00');
            }
            else if (chodesh_me.length == 2) {
                MeDate = new Date(Number(chodesh_me[1]), Number(chodesh_me[0]) - 1, '01', '00', '00', '00');
                AdDate = new Date(Number(chodesh_ad[1]), Number(chodesh_ad[0]) - 1, '01', '00', '00', '00');
                AdDate.setMonth(AdDate.getMonth() + 1);
                AdDate.setDate(AdDate.getDate() - 1);
            }
            args.IsValid = (AdDate >= MeDate);
        }

        function IsAlowedDate(sender, args) {
             //debugger
            var CurrentObj = sender.controltovalidate;
            var chodesh_me = document.getElementById(CurrentObj).value.split('/'); //ValidatorName.substr(0, ValidatorName.length - 2) + "ME").value;
            var FromDate = new Date(Number(chodesh_me[2]), Number(chodesh_me[1] - 1), Number(chodesh_me[0]), '00', '00', '00');
            var today = new Date();
            // var mis = document.getElementById("ctl00_KdsContent_P_MIS_RASHEMET").value;
            var Param100 = document.getElementById("ctl00_KdsContent_Param100").value;
            today.setMonth(today.getMonth() - Param100);
            today.setHours(0);
            today.setMinutes(0);
            today.setSeconds(0);
            today.setMilliseconds(0);

            if (FromDate.getTime() < today.getTime())
                args.IsValid = false;
            else {
                args.IsValid = true;
                //                if (mis != "")
                //                    checkRashemet(mis, args);
            }
        }


        function IsAlowedDateIturim(sender, args) {
          //  debugger
            var chodesh_me = document.getElementById('ctl00_KdsContent_P_STARTDATE').value.split('/'); //ValidatorName.substr(0, ValidatorName.length - 2) + "ME").value;
            var chodesh_ad = document.getElementById('ctl00_KdsContent_P_ENDDATE').value.split('/'); //ValidatorName.substr(0, ValidatorName.length - 2) + "AD").value;

            var MeDate, AdDate;
            MeDate = new Date(Number(chodesh_me[2]), Number(chodesh_me[1]) - 1, Number(chodesh_me[0]), '00', '00', '00');
            AdDate = new Date(Number(chodesh_ad[2]), Number(chodesh_ad[1]) - 1, Number(chodesh_ad[0]), '00', '00', '00');
            var Param280 = document.getElementById("ctl00_KdsContent_Param280").value;

            if (DateDiff.inDays(MeDate, AdDate) > parseInt(Param280)) {
                args.IsValid = false;
                document.getElementById("ctl00_KdsContent_P_STARTDATE_Validator_01").errormessage = "טווח התאריכים הנבחר גדול מ-" + Param280 + " יום, יש להקליד טווח תאריכים נמוך יותר";
            }
            else
                args.IsValid = true;
        }

        var  DateDiff = {

            inDays: function(d1, d2) {
                var t2 = d2.getTime();
                var t1 = d1.getTime();

                return parseInt((t2-t1)/(24*3600*1000));
            }
        }

        function IsAlowedEndDate(sender, args) {
            //  debugger
            var CurrentObj = sender.controltovalidate;
            var chodesh_ad = document.getElementById(CurrentObj).value.split('/'); //ValidatorName.substr(0, ValidatorName.length - 2) + "ME").value;
            var AdDate = new Date(Number(chodesh_ad[2]), Number(chodesh_ad[1] - 1), Number(chodesh_ad[0]), '00', '00', '00');
            var today = new Date();

            if (AdDate > today)
                args.IsValid = false;
            else {
                args.IsValid = true;
            }
        }
     
     
    </script>
</asp:Content>
