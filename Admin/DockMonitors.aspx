<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DockMonitors.aspx.vb" Inherits="DiversifiedLogistics.DockMonitors" %>

<!DOCTYPE html >

<html >
<head runat="server">
    <title></title>
      <script type="application/x-javascript">
    function draw() {
      var canvas = document.getElementById("canvas");
      if (canvas.getContext) {
        var ctx = canvas.getContext("2d");

        ctx.fillStyle = "rgb(200,0,0)";
        ctx.fillRect (10, 10, 55, 50);

        ctx.fillStyle = "rgba(0, 0, 200, 0.5)";
        ctx.fillRect (30, 30, 55, 50);
      }
    }
  </script>
  <style type="text/css">
  #canvas
  {background-color:Yellow;
      }
  #header
  { vertical-align:top; display:inline;
      }
  </style>

</head>
<body onload="draw();">
    <form id="form1" runat="server">

    <div id="header">
   If you see a translucent blue square over a red square<br />
    inside a yellow square, then welcome to the HTML5 &lt;canvas&gt; tag.   >>>
    </div>
    <canvas id="canvas" width="150" height="150" >
        <p>This example requires a browser that supports the <a href="http://www.w3.org/html/wg/html5/">HTML5</a> &lt;canvas&gt; feature.</p>
    </canvas>

    <asp:Button ID="Button1" runat="server" Text="Button" />

    </form>
</body>
</html>
