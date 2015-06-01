<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TestScan.aspx.cs" Inherits="TestScan" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="KdsContent" Runat="Server">
   <div class="Progress" id="divHourglass"  style="display:none;text-align:center;position:absolute;left:52%;top:48%; z-index:1000;width:150px" >
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/progress.gif" style="width: 100px; height: 100px" /><br /> מבצע סריקה אנא המתן...
</div> 
<div id="gg" runat="server">
 <asp:UpdatePanel ID="upscan" runat="server" RenderMode="Inline">
 <ContentTemplate> 
    <div>מספר אישי
        <asp:TextBox ID="misIshi" runat="server" Text="76133" ></asp:TextBox>
        תאריך
        <asp:TextBox ID="Taarich" runat="server" Text="10/11/2010"></asp:TextBox>
       
        <input id="srok" runat="server"  type="button"   value="סרוק טכוגרף"  
                    class="ImgButtonApprovalRegular"   Width="120px" 
                    onclick="srok();"     />
        <asp:Button ID="ShowTachograf" runat="server"  Text="צפה בטכוגרף"   
                        CssClass="ImgButtonApprovalRegular"  Width="120px"  
                         OnClick="ShowTachograf_Click"  />   



        <asp:Button  id="hideBtn" runat="server"    OnClick="hideBtn_onClick" style="display:none" />           
          <div id="prtMsg" style="display:none;width:400;height:60;" class="PanelMessage" >
              <br /><br /><br />
                                  אנא המתן לסיום תהליך הסורק
          </div>
         <div runat="server" id="divgrdBarcodes" style="height: 100px; width: 100px;display:none; overflow-y:scroll;direction: ltr;"  >
            <asp:GridView ID="grdBarcodes" runat="server" GridLines="None" ShowHeader="true" CssClass="Grid"
                AllowPaging="false" AutoGenerateColumns="false" AllowSorting="true" Width="120px"
                EmptyDataText="לא נמצאו נתונים!" EmptyDataRowStyle-CssClass="GridHeader" OnRowDataBound="grdBarcodes_RowDataBound">
                <Columns>
                    <%--<asp:BoundField DataField="BARCODE" />--%>
                    <asp:HyperLinkField DataTextField="BARKOD" NavigateUrl="#" HeaderStyle-HorizontalAlign="Center" 
                        HeaderText="ברקוד" ItemStyle-Width="300px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader" />
                    
                    
                   <%-- <asp:ButtonField DataTextField="BARKOD" HeaderText="ברקוד"  />--%>
                </Columns>
                <AlternatingRowStyle CssClass="GridAltRow" />
                <RowStyle CssClass="GridRow" />
                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                <EmptyDataRowStyle CssClass="GridEmptyData" Height="20px" Wrap="False" />
                
            </asp:GridView>
        </div>
       
          <input type="hidden" id="hiddenBarcodes" name="hiddenBarcodes"   runat="server" />
          <input type="hidden" id="hiddenPathBarcodes" name="hiddenPathBarcodes"   runat="server" />
          <input type="hidden" id="hiddenFileExportPath" name="hiddenFileExportPath"   runat="server" />
    </div>
 
</ContentTemplate>
</asp:UpdatePanel>
     <script type="text/javascript">
         function srok() {
             document.getElementById("divHourglass").style.display = 'block';
             var a = setTimeout("srok_Click()", 1000);
             document.getElementById("ctl00_KdsContent_hiddenBarcodes").value = "";
         }

         function srok_Click() {
             //  debugger;
             var file;
             var url = document.getElementById("ctl00_KdsContent_hiddenFileExportPath").value;
             var myObject = new ActiveXObject('Scripting.FileSystemObject');
             //"C:\\PrintFiles\\kds\\myExport.txt")
             if (myObject.FileExists(url)) {//"C:\\PrintFiles\\kds\\myExport.txt"))
                 file = myObject.GetFile(url);
                 file.Delete();
             }

            // deleteFile();
             var url = document.getElementById("ctl00_KdsContent_hiddenFileExportPath").value;
             var obj = new ActiveXObject("WScript.Shell");
             var path = '"C:\\Program Files\\EMC Captiva\\QuickScan\\eggedscan.cmd"';
             var result = obj.run(path, 0, false);
 
             
             var startTime = new Date(); var endTime = null;
             do {
                         endTime = new Date();
                }
                     while ((!myObject.FileExists(url)) && (((endTime - startTime) / (1000)) < 30));
             document.getElementById("divHourglass").style.display = 'none';

            if (myObject.FileExists(url)) {//"C:\\PrintFiles\\kds\\myExport.txt")) {
                obj = null;
                myObject = null;
                var x= window.setTimeout("ShlofBarcodesFromFile()",1000); 
             }
             else {
                 alert("הסריקה נכשלה. וודא שדפי הטכוגרף ממוקמים במקום הנכון ונסה לסרוק שנית.");
             }

         }
         function deleteFile() {
             var myObject = new ActiveXObject('Scripting.FileSystemObject');
             var url = document.getElementById("ctl00_KdsContent_hiddenFileExportPath").value;
             var file = myObject.GetFile(url);//"C:\\PrintFiles\\kds\\myExport.txt")
             if (myObject.FileExists(url))//"C:\\PrintFiles\\kds\\myExport.txt"))
                 file.Delete();  
         }
         function ShlofBarcodesFromFile() {
             var myObject, afile, line, pratim;
             var url = document.getElementById("ctl00_KdsContent_hiddenFileExportPath").value;
            // debugger;
             myObject = new ActiveXObject('Scripting.FileSystemObject');
             afile = myObject.OpenTextFile(url);//"C:\\PrintFiles\\kds\\myExport.txt")
          //   var file = myObject.GetFile(url);//"C:\\PrintFiles\\kds\\myExport.txt")
             while (!afile.AtEndOfStream) {
                line = afile.ReadLine();
                pratim =line.split(',');
                document.getElementById("ctl00_KdsContent_hiddenBarcodes").value += pratim[0] + ";";
            }
            
            document.getElementById("ctl00_KdsContent_hiddenBarcodes").value =
                    document.getElementById("ctl00_KdsContent_hiddenBarcodes").value.substr(0, document.getElementById("ctl00_KdsContent_hiddenBarcodes").value.length - 1);

            afile.close();
         //   file.Delete();
            document.getElementById("ctl00_KdsContent_hideBtn").click(); 
        }
        function OpenTachograf(barcode) {

            var url = document.getElementById("ctl00_KdsContent_hiddenPathBarcodes").value + barcode + ".jpg";
            display(url, barcode);
           // window.showModalDialog(url, window, "dialogHeight: 938px; dialogWidth:900px;");
         
        }
        function display(myimage, barcode) {
            html = "<HTML><HEAD><TITLE>" + barcode + "</TITLE>" +
                      "</HEAD><BODY LEFTMARGIN=0 "
                      + "MARGINWIDTH=0 TOPMARGIN=0 MARGINHEIGHT=0><CENTER>"
                      + "<IMG SRC='" + myimage + "' width='100%' BORDER=0 NAME=image "
                       + "onload='window.resizeTo(document.image.width/1.5,document.image.height/1.5)'>"
                      + "</CENTER>"
                      + "</BODY></HTML>";
            popup = window.open('', 'image', 'toolbar=0,location=0,directories=0,menuBar=0,scrollbars=0,resizable=1');
            popup.document.open();
            popup.document.write(html);
            popup.document.focus();
            popup.document.close()
        };
        
     </script>
</div>
</asp:Content>
