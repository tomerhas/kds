<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test4.aspx.cs" Inherits="Modules_Test4" %>
<%@ Register Assembly="KdsLibrary" Namespace="KdsLibrary.Controls" TagPrefix="cc1"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>click() event</title>
    <script src='../js/jquery.js' type='text/javascript'></script>
     <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />  <script src="http://code.jquery.com/jquery-1.9.1.js"></script>  <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>  <link rel="stylesheet" href="/resources/demos/style.css" />

    <style type="text/css">
        .selected {      color: red;    }
        .ddlchklst
        {
            width: 170px;
            border:solid 1px silver;
        }
        .ddlchklst ul
        {
          margin:0;
          padding:0;
           border-top:solid 1px silver;
            }
        .ddlchklst li
        {
            list-style: none;
        }
    </style>


</head>
<body>
    <form id="form1" runat="server">
   

    <div>
        <input type="text" id="txt1" value="562" />
        <input type="text" id="txt2"  title="enter your age"/>

 

        <input type="button" value="Add" id="btnAdd" runat="server" />

        <input type="button" value="Sub" id="btnSub" runat="server" />

        <input type="button" value="Multi" id="btnMulti" runat="server" />

        <input type="button" value="Div" id="btnDiv" onclick="clicDiv();" runat="server" />
 
        <span id="sp"></span>


    </div>

     <div id="yellowDiv">    jQuery Tutorial  </div>
     <input id="btnHide" type="button" value="Click Hide" />
     <input id="btnShow" type="button" value="Click Show" />

      <ul id="ulist" runat="server" class="selected">    <li>HTML</li>    <li>CSS</li>    <li>jQuery</li>    <li>JavaScript</li>  </ul>
      <ul id="examplex" >    <li>aaa</li>    <li>bbb</li>    <li>ccc</li>    <li>ddd</li>  </ul>

      <input id="btnToogle" type="button" value="Toggle" />

      <div id="content"> 
        <p id="p1" class="first">פסקה ראשונה תוכן ... פסקה ראשונה תוכן...</p>  
         <p id="p2">פסקה שניה ...תוכן...פסקה שניה...תוכן...</p>
      </div>

       <label>Name:</label> <input name="name" />
       <fieldset>
          <label>Newsletter:</label> <input name="newsletter" />
       </fieldset>
  <input name="none" />

      <div id="tabs" >  
            <ul>    
                <li><a href="#tabs-1">Nunc tincidunt</a></li> 
                <li><a href="#tabs-2">Proin dolor</a></li>  
                <li><a href="#tabs-3">Aenean lacinia</a></li> 
            </ul> 
           <div id="tabs-1">  
                <p>Proin elit arcu, rutrum commodo, vehicula tempus, commodo a, risus. Curabitur nec arcu. Donec sollicitudin mi sit amet mauris. Nam elementum quam ullamcorper ante. Etiam aliquet massa et lorem. Mauris dapibus lacus auctor risus. Aenean tempor ullamcorper leo. Vivamus sed magna quis ligula eleifend adipiscing. Duis orci. Aliquam sodales tortor vitae ipsum. Aliquam nulla. Duis aliquam molestie erat. Ut et mauris vel pede varius sollicitudin. Sed ut dolor nec orci tincidunt interdum. Phasellus ipsum. Nunc tristique tempus lectus.</p> 
           </div> 
           <div id="tabs-2">    <p>Morbi tincidunt, dui sit amet facilisis feugiat, odio metus gravida ante, ut pharetra massa metus id nunc. Duis scelerisque molestie turpis. Sed fringilla, massa eget luctus malesuada, metus eros molestie lectus, ut tempus eros massa ut dolor. Aenean aliquet fringilla sem. Suspendisse sed ligula in ligula suscipit aliquam. Praesent in eros vestibulum mi adipiscing adipiscing. Morbi facilisis. Curabitur ornare consequat nunc. Aenean vel metus. Ut posuere viverra nulla. Aliquam erat volutpat. Pellentesque convallis. Maecenas feugiat, tellus pellentesque pretium posuere, felis lorem euismod felis, eu ornare leo nisi vel felis. Mauris consectetur tortor et purus.</p> 
           </div> 
            <div id="tabs-3">    <p>Mauris eleifend est et turpis. Duis id erat. Suspendisse potenti. Aliquam vulputate, pede vel vehicula accumsan, mi neque rutrum erat, eu congue orci lorem eget lorem. Vestibulum non ante. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Fusce sodales. Quisque eu urna vel enim commodo pellentesque. Praesent eu risus hendrerit ligula tempus pretium. Curabitur lorem enim, pretium nec, feugiat nec, luctus a, lacus.</p>    <p>Duis cursus. Maecenas ligula eros, blandit nec, pharetra at, semper at, magna. Nullam ac lacus. Nulla facilisi. Praesent viverra justo vitae neque. Praesent blandit adipiscing velit. Suspendisse potenti. Donec mattis, pede vel pharetra blandit, magna ligula faucibus eros, id euismod lacus dolor eget odio. Nam scelerisque. Donec non libero sed nulla mattis commodo. Ut sagittis. Donec nisi lectus, feugiat porttitor, tempor ac, tempor vitae, pede. Aenean vehicula velit eu tellus interdum rutrum. Maecenas commodo. Pellentesque nec elit. Fusce in lacus. Vivamus a libero vitae lectus hendrerit hendrerit.</p> 
           </div>
      </div>
    </form>
    <script language="javascript" type="text/javascript">
        function GetArgs() {
            var num1 = document.getElementById('txt1').value;
            var num2 = document.getElementById('txt2').value;
            var method = event.srcElement.value;

            // 2:4:Add
            return num1 + ':' + num2 + ':' + method;
        }
        
        function onSuccess(res) {
            document.getElementById('sp').innerHTML = res;
        }

        function onFailed(res) {
            alert(res);
        }

        function clicDiv() {
            $("#yellowDiv").css({ "background-color": "blue" });
           // $('#content p').append('<a href="#">new link</a>');

            $("#content").wrap("<div style='background-color:red'></div>");
           // $("#p2").wrap("<div style='background-color:yellow'></div>");

           // $("#content p.first").append("<span>תוכן חדש נוסף...</span>");
           // $('<p>תוכן חדש נוסף...</p>').insertAfter('#content p.first');
          //  $('#content p:first').after('<p>תוכן חדש נוסף...</p>');
           // $('#content p.first').after('<p>תוכן חדש נוסף...</p>')
        }


        $(document).ready(function () {
            var divElements = $("div");
            var lis = $('li');

            if (divElements.length > 0)
            { $("#yellowDiv").css({ "background-color": "yellow", "font-weight": "bold", "color": "red" }); }
            else
            { $("#yellowDiv").css({ "background-color": "red" }); }


            $("#btnHide").click(function () {
                $("#yellowDiv").hide('slow');
            });

            $("#btnShow").click(function () {
                $("#yellowDiv").fadeIn(); // show('slow', function () { $('li').css('background-color', '#0a0').css('color', 'red'); });

                var current_text = $('#tabs-1').text().split(" ").slice(0, 3).join(" ");
                //  alert(current_text);
                $('#tabs-1').text(current_text + '...');

                  $("#yellowDiv").animate({ 
                            width: "70%",
                            opacity: 0.4
                        }, 1500);
                        
                    $("#yellowDiv").animate({ 
                            marginLeft: "350px"
                          }, 2500 )
                    .animate( { fontSize:"24px" } , 1000 )
                    .animate( { borderLeftWidth:"15px" }, 1000);


            });

            $("#yellowDiv")
            // .mouseover(function () { $("#yellowDiv").css({ "background-color": "lightblue" }); })
                .mouseout(function () { $("#yellowDiv").css({ "background-color": "green" }); });

            // $("#yellowDiv").slider();
            //$("#yellowDiv").progressbar({ value: 58 });
            $("li")
                .mouseover(function () { $(this).addClass("selected"); })
                .mouseout(function () { $(this).removeClass("selected"); });

            $("#btnToogle").click(function () { $("#yellowDiv").toggle('slow'); });

            $(function () { $("#tabs").tabs(); });
            //   $(function () { $("#tabs").accordion(); });
            $(document).tooltip();
            // alert(lis.length );
            //  $('div#tabs').height('100px');

            //            $("#ulist > li").click(function () {
            //                alert("הודעה מעצבנת");
            //            });

            $("label + input").click(function () {
                alert("הודעה לא מעצבנת");
            });

            $("p:contains('Proin')").click(function () {
                alert("Proin");
            });

            $("#tabs-1:has(p:contains('Proin'))").click(function () {
                alert("tabs-1>Proin");
            });

            $("input[id]").click(function () {
                alert("a:attr('href')");
            });

            $("#ulist:first-child").click(function () {
                alert(":nth-child(2)");
            });

            $("#yellowDiv").one("dblclick", function () {
                alert("yellowDiv");
            });

            $("#yellowDiv").bind("mouseover", function () {
                $("#yellowDiv").css({ "background-color": "pink" });
            });

          
              



            $("#examplex").bind({
	            click: function () {
		            alert("הודעה");
	            },
   	            mouseover: function() {  
	                    $(this).css("color","red");
    	            },  
    	            mouseout: function() {  
	                    $(this).css("color","black");
   	            }
            });

////            $("#examplex").toggle(function () {
////                $("#examplex").css({ "background-color": "pink" });
////                } , function () {
////                    $("#examplex").css({ "background-color": "red" });
////                });

        });
       // debugger;
       // $("#txt1").keypress(function () { alert($("#txt1").("value")); }); 




     

    </script>
</body>
</html>
