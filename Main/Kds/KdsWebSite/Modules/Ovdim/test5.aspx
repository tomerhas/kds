<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test5.aspx.cs" Inherits="Modules_test5" %>

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
 <script src='../js/jquery.js' type='text/javascript'></script>
 <script type="text/javascript">
     $(document).ready(function () {
         $(".flip").click(function () {
             $(".panel").slideToggle("fast");
         });
     });
     
    $(document).ready(function () {
        $("button").click(function () {
            $("div").load('test.aspx');
        });
    });
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
    <div><h2>Let AJAX change this text</h2></div>
    <button>Change Content</button>


    <div class="panel">
    <p>Because time is valuable, we deliver quick and easy learning.</p>
    <p>At W3Schools, you can study everything you need to learn, in an accessible and handy format.</p>
    </div> 
    <p class="flip">Show/Hide Panel</p>     
</body>
</html>

