<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="SpeedTest.aspx.cs" Inherits="_SpeedTest" %>
<%@ Register Src="~/controls/SpeedTest.ascx" TagPrefix="js" TagName="SpeedTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Bandwidth Performance Test</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<div id="SpeedTestingMsg">Measuring your pipe speed (or, if you're on a fast connection, my pipe speed) ...</div>
		<div id="SpeedResults"></div>
        <js:SpeedTest runat="server" UseCookie="false" ClientCompleteFunction="myJavascriptFunction" />
    <script language="javascript" type="text/javascript">
        function myJavascriptFunction(latency, pipeSpeed) {
			if (pipeSpeed < 0) pipeSpeed = "<i>too fast to measure</i>";
			else pipeSpeed += " kilobytes / sec";
            document.getElementById("SpeedResults").innerHTML = "HTTP Latency: " + latency + "ms<br />"
                + "Pipe speed: " + pipeSpeed;
            document.getElementById("SpeedTestingMsg").style.visibility = "hidden";
        }
    </script>
    </div>
	<div style="margin-top:50px;"><em>Bandwidth test by <a href="http://www.jondavis.net/">Jon Davis</a>. Copyright 2007.</em></div>
    </form>
</body>
</html>
