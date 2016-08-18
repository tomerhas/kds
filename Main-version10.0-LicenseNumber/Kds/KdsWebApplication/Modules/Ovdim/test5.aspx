<%@ Page Language="C#" AutoEventWireup="true" Inherits="Modules_test5" Codebehind="test5.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%--<html>
<head>
<script type="text/javascript" src='../js/jquery.js'></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("button").click(function () {
            $("p").hide();
        });
    });
</script>
</head>

<body>
<h2>This is a heading</h2>
<p>This is a paragraph.</p>
<p>This is another paragraph.</p>
<button>Click me</button>
</body>
</html> 
--%>

<html>
<head>
<title></title>
 <script src="../../Js/jquery.js" type='text/javascript'></script>
 <script src="../../Js/jquery.fixedheader.js" type='text/javascript'></script>
 <script src="../../Js/jqueryheader.js" type='text/javascript'></script>

 <script type="text/javascript">
     $(document).ready(function () {
     $("#grdEmployee").fixedHeader({
         width: 600, height: 300
     });
     }
//     $(document).ready(function () {
//         $(".flip").click(function () {
//             $(".panel").slideToggle("fast");
//         });
//     });
//     
//    $(document).ready(function () {
//        $("button").click(function () {
//            $("div").load('test.aspx');
//        });
//    });
</script>
<style type="text/css"> 
    div.panel,p.flip
    {
        margin:0px;
        padding:5px;
        text-align:center;
        background:#e5eecc;
        border:solid 1px #c3c3c3;
    }
    div.panel
    {
        height:120px;
        display:none;
    }
</style>

</head>
<body>
    <form id="form1" runat="server">
    <div><h2>Let AJAX change this text</h2></div>
    <div id="divNetunim" runat="server" dir="rtl" style="text-align:right;width:965px;height:200px; overflow:scroll;">
     <asp:GridView ID="grdEmployee" runat="server" AllowSorting="true" 
                AllowPaging="false"  PageSize="8" AutoGenerateColumns="false" CssClass="Grid"  
                Width="950px" EmptyDataText="לא נמצאו נתונים!" ShowHeader="true" 
                OnRowDataBound="grdEmployee_RowDataBound" >                                 
            <Columns>                                                                           
                <asp:BoundField DataField="status" HeaderText="דרוש עדכון" SortExpression="status"  ItemStyle-Font-Size="Larger"  ItemStyle-CssClass="ItemRow"  HeaderStyle-Width="150px" HeaderStyle-CssClass="GridHeader"   />
                <asp:BoundField DataField="kartis_without_peilut" HeaderText="ללא דיווח"  ItemStyle-Font-Size="Larger" SortExpression="kartis_without_peilut" ItemStyle-CssClass="ItemRow" HeaderStyle-Width="145px"  HeaderStyle-CssClass="GridHeader"  />
                <asp:BoundField DataField="measher_o_mistayeg_key" HeaderText="מסתייג/מאשר/ללא התייחסות"  ItemStyle-Font-Size="Larger"  SortExpression="measher_o_mistayeg_key"  HeaderStyle-Width="250px" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />
                <asp:BoundField DataField="status_tipul_key" HeaderText="ממתין לאישור"  ItemStyle-Font-Size="Larger" SortExpression="status_tipul_key" ItemStyle-CssClass="ItemRow"   HeaderStyle-Width="150px" HeaderStyle-CssClass="GridHeader"  />
            <%--     <asp:BoundField DataField="status_tipul_key" HeaderText="ממתין לחישוב שכר" SortExpression="status_tipul_key" ItemStyle-CssClass="ItemRow" HeaderStyle-CssClass="GridHeader"  />                            
            --%> </Columns>
            <AlternatingRowStyle CssClass="GridAltRow" Height="25px" />
            <RowStyle CssClass="GridRow" Height="25px" />
            <PagerStyle CssClass="GridPagerLarge" HorizontalAlign="Center"  />                          
            <EmptyDataRowStyle CssClass="GridEmptyData" height="10px" Wrap="False"/>                                                            
        </asp:GridView>   
    
    </div>
    <button>Change Content</button>


    <div class="panel">
    <p>Because time is valuable, we deliver quick and easy learning.</p>
    <p>At W3Schools, you can study everything you need to learn, in an accessible and handy format.</p>
    </div> 
    <p class="flip">Show/Hide Panel</p>  
    </form>
    
</body>
</html>

