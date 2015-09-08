<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="DataEntryView" Title="Untitled Page" Codebehind="DataEntryView.aspx.cs" %>
<%@ Register TagPrefix="kds" TagName="GridViewPager" Src="~/UserControls/GridViewPager.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <script src="../Js/SystemManager.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">
     <asp:Button ID="btnRefresh" runat="server" CssClass="Hidden" />
    <asp:UpdatePanel ID="upMessage" runat="server" RenderMode="Inline">
     <ContentTemplate>
    <asp:HiddenField ID="hdQueryString" runat="server" />
    <table>
        <tr>
            <td>
                <asp:Button Text="הוסף" ID="btnNew" runat="server"  
                    CssClass="ImgButtonSearch"   /> 
                    
            </td>
            <td>
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label> 
            </td>
        </tr>
        <tr>
            <td colspan="2">
         <%--   <div id="divGrid" runat="server" dir="rtl" style="vertical-align:middle">--%>
             <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" 
                    AllowPaging="true" AllowSorting="true"   HorizontalAlign="Right"
                    EmptyDataText="לא נמצאו נתונים!"  
                    CssClass="Grid" 
                    HeaderStyle-CssClass="GridHeader"
                    HeaderStyle-ForeColor="White" 
                    PageSize="12">
                    <Columns>
                        <asp:TemplateField ItemStyle-CssClass="ItemRow">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="lbDelete" ImageUrl="~/Images/delete.png" CommandName="Select"  OnClick="PromptDelete" />
                                <ajaxToolkit:ConfirmButtonExtender ID="cbDelete" runat="server"
                                    TargetControlID="lbDelete" DisplayModalPopupID="mpConfirm" />
                                <ajaxToolkit:ModalPopupExtender ID="mpConfirm" runat="server"
                                    TargetControlID="lbDelete"
                                    PopupControlID="pnlConfirm"
                                    OkControlID="OkConfButton" 
                                    CancelControlID="CancelConfButton"  />
                                <asp:Panel runat="server" ID="pnlConfirm" CssClass="PanelMessage" Width="200px" style="display:none;position:static;" >
                                    <asp:Label ID="lblHeaderMessage" runat="server" Width="97%" BackColor="#696969" ForeColor="White">מחיקה</asp:Label>
                                    <br /> <br /> 
                                        <asp:Label runat="server" ID="lblPrompt"></asp:Label>
                                     <br /><br /> 
                                    <asp:Button ID="OkConfButton" runat="server" Text="OK" CssClass="ImgButtonMake" CausesValidation="false" OnClick="PromptDelete" />
                                    <asp:Button ID="CancelConfButton" runat="server" CssClass="ImgButtonMake" CausesValidation="false" Text="Cancel" />
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="ItemRow">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbSelect" runat="server" CommandName="Select"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <AlternatingRowStyle CssClass="GridAltRow"  />
                    <RowStyle CssClass="GridRow"   />
                    <SelectedRowStyle CssClass="SelectedGridRowNoIndent" />
                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center"  />                          
                    <PagerTemplate>
                         <kds:GridViewPager runat="server" ID="ucGridPager" />
                    </PagerTemplate>
                    <EmptyDataRowStyle CssClass="GridEmptyData" height="20px" Wrap="False"/> 
             </asp:GridView>
            <%--</div>--%>
            </td>
        </tr>
    </table>
     <input type="button" ID="btnRefreshPrint"  runat="server"  onserverclick="btnRefreshPrint_click"  style="display:none" CausesValidation="false" />
    </ContentTemplate></asp:UpdatePanel>
    <script type="text/javascript">
        function refresh(){
            $get("<%=btnRefresh.ClientID %>").click();
        }
        
        function success(){
            return true;
        }

        function PrintGridData() {
////            var prtGrid = document.getElementById('<%=GridView1.ClientID %>');
////            // prtGrid.border = 0;
////            //   prtGrid.attributes["dir"] = "rtl";
////            var prtwin = window.open('', 'PrintGridViewData', 'left=100,top=100,width=100,height=100,tollbar=0,scrollbars=1,status=0,resizable=1');
////            prtwin.document.write(prtGrid.outerHTML);
////            prtwin.document.close();
////            prtwin.focus();
////            prtwin.print();
////            prtwin.close();
////         //   debugger;
////            document.getElementById("ctl00_KdsContent_btnRefreshPrint").click();
////        }

////function TableToExcel()
            ////{
            $("#<%=GridView1.ClientID %> A").removeAttr("href");
            $("#<%=GridView1.ClientID %> A").css("COLOR", "black");

            var strCopy = document.getElementById('<%=GridView1.ClientID %>').outerHTML;
            window.clipboardData.setData("Text", strCopy);
            var objExcel = new ActiveXObject ("Excel.Application");
            objExcel.visible = true;

            var objWorkbook = objExcel.Workbooks.Add;
            var objWorksheet = objWorkbook.Worksheets(1);
            objWorksheet.Paste;
            document.getElementById("ctl00_KdsContent_btnRefreshPrint").click();
}


    </script>
</asp:Content>

