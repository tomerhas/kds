<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PremiaCalculation.aspx.cs" Inherits="Modules_Premia_PremiaCalculation" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">
    
    <asp:UpdatePanel runat="server" ID="upMain">
        <ContentTemplate>
            <asp:HiddenField ID="hdCurrentStage" runat="server" />
            <asp:HiddenField ID="hdUserId" runat="server" />
            <asp:HiddenField ID="hdErrorMessage" runat="server" />
            <asp:Button runat="server" ID="btnAdvance" style="display:none;" />
            <div>
                <br />
                <table cellspacing="20">
                    <tr>
                        <td>חודש: 
                            <asp:DropDownList runat="server" ID="ddPeriods" AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td>בקשת חישוב:
                            <asp:DropDownList runat="server" ID="ddBatchRequests" AutoPostBack="true"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
                
                
                <table cellspacing="20">
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnStage1" Text="בנה קובץ נתוני נוכחות" 
                                CssClass ="ImgButtonSearch"  Width="190px" OnClientClick="javascript:getNewBatchRequest(8, this.value); return false;" />
                        </td>
                        <td>
                            <asp:Image runat="server" ID="imgOK1" ImageUrl="~/Images/allscreens-checkbox.jpg" />
                            <asp:Image runat="server" ID="imgError1" ImageUrl="~/Images/allscreens-cancle-btn.jpg" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblMessage1" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnStage2" Text="הפעל חישוב"  
                                CssClass ="ImgButtonSearch"  Width="190px" OnClientClick="javascript:getNewBatchRequest(9, this.value); return false;"/>
                        </td>
                        <td>
                            <asp:Image runat="server" ID="imgOK2" ImageUrl="~/Images/allscreens-checkbox.jpg" />
                            <asp:Image runat="server" ID="imgError2" ImageUrl="~/Images/allscreens-cancle-btn.jpg" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblMessage2" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnStage3" Text="שמור נתוני פרמיות" 
                                CssClass ="ImgButtonSearch" Width="190px" OnClientClick="javascript:getNewBatchRequest(10, this.value); return false;"/>
                        </td>
                        <td>
                            <asp:Image runat="server" ID="imgOK3" ImageUrl="~/Images/allscreens-checkbox.jpg" />
                            <asp:Image runat="server" ID="imgError3" ImageUrl="~/Images/allscreens-cancle-btn.jpg" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblMessage3" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
                
                 <div id="dvLoader" class="Progress"  style="text-align:right;position:absolute;left:52%;top:48%; z-index:1000; visibility:hidden;"   >
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/progress.gif" style="width: 100px; height: 100px" />
        </div>     
            </div>
            <script type="text/javascript">
                var processBatchNumber;
                var batchType;
                var period;
                var requestNumber;
                var currentStage;
                var isInProcess=false;
                function getNewBatchRequest(btchType, desc){
                    if(isInProcess==false){
                        isInProcess=true;
                        batchType = btchType;
                        $get("<%=hdCurrentStage.ClientID %>").value = batchType - 7;
                        wsBatch.OpenBatchRequest(batchType,
                            document.getElementById("<%=hdUserId.ClientID %>").value, desc, 
                            RunStage, ProcessFailed);
                    }
                }
                
                function RunStage(btchNum){
                    showLoader(true);
                    var stageIndex=batchType-7;
                    document.getElementById("ctl00_KdsContent_lblMessage"+stageIndex).innerText=
                        "בקשתך בביצוע אנא המתן ,מס' הבקשה הינו " + btchNum;
                    processBatchNumber=btchNum;
                    var ddPeriods=document.getElementById("<%=ddPeriods.ClientID %>");
                    period=ddPeriods.options[ddPeriods.selectedIndex].value;
                    var ddRequests=document.getElementById("<%=ddBatchRequests.ClientID %>");
                    requestNumber=ddRequests.options[ddRequests.selectedIndex].value;
                    wsBatch.RunPremiaRoutine(batchType,requestNumber,period,
                        document.getElementById("<%=hdUserId.ClientID %>").value,processBatchNumber,
                        ProcessSucceeded, ProcessFailed);
                    
                }
                
                function ProcessSucceeded(result){
                    isInProcess=false;
                    showLoader(false);
                    document.getElementById("<%=hdErrorMessage.ClientID %>").value=result;
                    AdvanceStage();
                }
                
                function ProcessFailed(message){
                    isInProcess=false;
                    showLoader(false);
                    document.getElementById("<%=hdErrorMessage.ClientID %>").value=message;
                    AdvanceStage();
                }
                
                function AdvanceStage(){
                    document.getElementById("<%=btnAdvance.ClientID %>").click();
                }
                
                function showLoader(show){
                    if (show) {
                        $get('dvLoader').style.visibility = 'visible';
                        disableButtons();
                    }
                    else {
                        $get('dvLoader').style.visibility = 'hidden';
                        enableButtons();
                    }
                }

                function disableButtons() {
                    $get("<%=btnStage1.ClientID %>").disabled = true;
                    $get("<%=btnStage2.ClientID %>").disabled = true;
                    $get("<%=btnStage3.ClientID %>").disabled = true;

                }

                function enableButtons() {
                    currentStage = parseInt($get("<%=hdCurrentStage.ClientID %>").value);
                    if (currentStage >= 1) 
                        $get("<%=btnStage1.ClientID %>").disabled = false;
                    if (currentStage >= 2)
                        $get("<%=btnStage2.ClientID %>").disabled = false;
                    if (currentStage >= 3)
                        $get("<%=btnStage3.ClientID %>").disabled = false;
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

