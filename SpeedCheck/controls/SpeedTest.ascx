<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SpeedTest.ascx.cs" Inherits="controls_SpeedTest" %>
<asp:Literal runat="server" ID="AstMainScript" Visible="false">
<script language="javascript" type="text/javascript">

/*

This is mine. You're free to reuse, but in your source please credit me! ;)
Also, if this proved usable for your site, let me know, I'd like to hear 
about it!

Jon Davis
http://www.jondavis.net/
jon@jondavis.net

*/

var astGenBytesUrl = "{$GEN_BYTES_URL$}";
var astByteLength = {$BYTE_LENGTH$};
var astLatency = astReadCookie("astLatency");
var astPipeSpeed = astReadCookie("astPipeSpeed");
var astLatencyStart;
var astLatencyComplete;
var astPipeSpeedStart;
var astPipeSpeedComplete;
var astUseCookie = {$USE_COOKIE$};

function astBeginMeasureLatency()
{
    astLatencyStart = new Date();
    astLatencyComplete = null;
    astLoadXMLDoc(astGenBytesUrl + "?len=1",
        astCompleteMeasureLatency);
}

function astCompleteMeasureLatency()
{
    if (astReq.readyState == 4) 
    {
        astLatencyComplete = new Date();
        astLatency = astDiff(astLatencyStart, astLatencyComplete);
        
        astBeginMeasurePipeSpeed();
    }
}

function astBeginMeasurePipeSpeed()
{
    astPipeSpeedStart = new Date();
    astPipeSpeedComplete = null;
    astLoadXMLDoc(astGenBytesUrl + "?len=" + astByteLength,
        astCompleteMeasurePipeSpeed);
}

function astCompleteMeasurePipeSpeed()
{
    if (astReq.readyState == 4) 
    {
        astPipeSpeedComplete = new Date();
        var tmpSpeed = astDiff(astPipeSpeedStart, astPipeSpeedComplete) - astLatency;
        astPipeSpeed = Math.round(astByteLength / tmpSpeed);
        
        if ({$USE_COOKIE$}) {
            astCreateCookie("astLatency", astLatency, {$COOKIE_EXPIRES$});
            astCreateCookie("astPipeSpeed", astPipeSpeed, {$COOKIE_EXPIRES$});
        }
        
        if ("$COMPLETE_FUNCTION$" != "") {
            {$COMPLETE_FUNCTION$}(astLatency, astPipeSpeed);
        }
    }
}

function astCreateCookie(name,value,days) {
	if (days) {
		var date = new Date();
		date.setTime(date.getTime()+(days*24*60*60*1000));
		var expires = "; expires="+date.toGMTString();
	}
	else var expires = "";
	document.cookie = name+"="+value+expires+"; path=/";
}

function astReadCookie(name) {
	var nameEQ = name + "=";
	var ca = document.cookie.split(';');
	for(var i=0;i < ca.length;i++) {
		var c = ca[i];
		while (c.charAt(0)==' ') c = c.substring(1,c.length);
		if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
	}
	return null;
}

function astEraseCookie(name) {
	astCreateCookie(name,"",-1);
}

function astDiff(start, end) {
    return end.getTime() - start.getTime();
}

function astTestComplete(latency, pipeSpeed) {
//    alert("Latency:\t\t" + latency + "ms\n"
//        + "Pipe speed:\t" + pipeSpeed + "kb/s" );
}

var astReq;

// parts of the following function from
// http://developer.apple.com/internet/webcontent/xmlhttpreq.html
// and/or
// http://www.w3schools.com/xml/xml_http.asp
function astLoadXMLDoc(url, func) {
	astReq = false;
    // branch for native XMLHttpRequest object
    if(window.XMLHttpRequest && !(window.ActiveXObject)) {
    	try {
			astReq = new XMLHttpRequest();
        } catch(e) {
			astReq = false;
            alert(e);
        }
    // branch for IE/Windows ActiveX version
    } 
    else if(window.ActiveXObject) {
       	try {
        	astReq = new ActiveXObject("Msxml2.XMLHTTP");
      	} catch(e) {
        	try {
          		astReq = new ActiveXObject("Microsoft.XMLHTTP");
        	} catch(e) {
          		astReq = false;
        	}
		}
    }
	if (astReq) {
		astReq.onreadystatechange = func;
		astReq.open("GET", url, true);
		astReq.send("");
	}
}


if (astLatency == null || !astUseCookie) {
    astBeginMeasureLatency();
} else {
    {$COMPLETE_FUNCTION$}(astLatency, astPipeSpeed);
}


</script>
</asp:Literal>