/**
 * @author Jon Davis <jon@jondavis.net>
 * @version 1.0.1
 */
var using = window.using = function( scriptName, callback, sender ) {
	var cb = function() {
		using.__regscr[scriptName] = undefined;
		if (callback) {
			callback.call(this);
		}
	}
	var url = using.__regscr[scriptName];
	if (url) {
		if (typeof(url) == "string") {
			using.load(url, cb, sender);
		} else { // array
			var uwait = 0;
			for (var i=0; i < url.length; i++) {
				var idx = url[i];
				if (typeof(idx) == "boolean" || idx.toString() == "async") {
					uwait = using.defaultAsyncWait;
				}
				else if (typeof(idx) == "number") {
					uwait = idx;
				}
				else {
					if (i == url.length - 1) {
						using.load(url[i], cb, sender, uwait);
					}
					else {
						using.load(url[i], null, null, uwait);
					}
				}
			}
		}
	} else {
		cb.call(sender);
	}
}

using.prototype = {	
	register: function(scriptName, scriptUrls){
		if (!using.__regscr) {
			using.__regscr = {};
		}
		if (!using.__regscr[scriptName]) {
			if (arguments.length == 2)
				using.__regscr[scriptName] = scriptUrls;
			else {
				var urls = new Array();
				for (var a=1; a<arguments.length; a++) {
					urls.push(arguments[a]);
				}
				using.__regscr[scriptName] = urls;
			}
		}
	},
	
	wait: 0,
	
	defaultAsyncWait: 250,
	
	load: function(scriptUrl, callback, sender, uwait) {
		if (!uwait) uwait = using.wait;
		if (callback && uwait == 0) uwait = using.defaultAsyncWait;
		if (!using.__loading) {
			using.__loading = {};
		}
		var loadScript = this.__loading[scriptUrl];
		if (using.__loading[scriptUrl]) {
			if (callback) {
				using.__loading[scriptUrl] = {
					callback: function(){
						loadScript.callback();
						callback.call(sender); // this sender passing is WRONG
					}
				};
			}
			return;
		}
		else {
			if (callback) {
				using.__loading[scriptUrl] = {
					callback: function(){
						callback.call(sender);
					}
				};
			}
			if (uwait > 0) {
				using.srcScript(scriptUrl, callback, sender, uwait);
			} else {
				var xhr;
				if (window.XMLHttpRequest)
					xhr = new XMLHttpRequest()
				else if (window.ActiveXObject) {
					xhr = new ActiveXObject("Microsoft.XMLHTTP"); 
				}
				xhr.onreadystatechange = function(){
					if (xhr.readyState == 4 && xhr.status == 200) {
						using.injectScript(xhr.responseText);
						if (using.__loading[scriptUrl] && using.__loading[scriptUrl].callback) {
							using.__loading[scriptUrl].callback.call(sender);
						}
						using.__loading[scriptUrl] = undefined;
					}
				};
				xhr.open("GET", scriptUrl, false);
				xhr.send();
			}
		}
	},
	genScriptNode : function() {
		var scriptNode = document.createElement("script");
		scriptNode.setAttribute("type", "text/javascript");
		scriptNode.setAttribute("language", "JavaScript");
		return scriptNode;	
	},
	srcScript : function(scriptUrl, callback, sender, uwait) {
		var scriptNode = using.prototype.genScriptNode();
		scriptNode.setAttribute("src", scriptUrl);
		if (callback) {
			scriptNode.onload = scriptNode.onreadystatechange = function() {
				if (!scriptNode.readyState || scriptNode.readyState == "loaded" ||
					scriptNode.readyState == 4 && scriptNode.status == 200) {
					if (uwait > 0) {
						setTimeout(function(){
							callback.call(sender);
						}, uwait);
					}
					else {
						callback.call(sender); // use of callback / sender here is WRONG!!
					}
				}
			};
		}
		var headNode = document.getElementsByTagName("head")[0];
		headNode.appendChild(scriptNode);
	},
	injectScript : function(scriptText) {
		var scriptNode = using.prototype.genScriptNode();
		//var scrTextNode = document.createTextNode(scriptText);
		//scriptNode.appendChild(scrTextNode);
		scriptNode.text = scriptText;
		var headNode = document.getElementsByTagName("head")[0];
		headNode.appendChild(scriptNode);
	}
};
using.register = using.prototype.register;
using.load = using.prototype.load;
using.wait = using.prototype.wait;
using.defaultAsyncWait = using.prototype.defaultAsyncWait;
using.srcScript = using.prototype.srcScript;
using.injectScript = using.prototype.injectScript;
