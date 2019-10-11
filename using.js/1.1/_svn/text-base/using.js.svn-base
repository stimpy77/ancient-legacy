/**
 * @author Jon Davis <jon@jondavis.net>
 * @version 1.1
 */
var using = window.using = function( scriptName, callback, sender ) {
    var reg = using.__regscr[scriptName];
    if (reg) {
    
        // load dependencies first
        for (var r=reg.requirements.length-1; r>=0; r--) {
            if (using.__regscr[reg.requirements[r].name]) {
                using(reg.requirements[r].name, function() {
                    using(scriptName, callback, sender);
                }, sender);
                return;
            }
        }
        
        // load each script URL
        for (var u=0; u<reg.urls.length; u++) {
            if (u == reg.urls.length - 1) {
                if (callback) {
                    using.load(reg.name, reg.urls[u], reg.remote, reg.asyncWait,
                        new using.prototype.CallbackItem(callback, sender));
                } else {
                    using.load(reg.name, reg.urls[u], reg.remote, reg.asyncWait);
                }
            } else {
                using.load(reg.name, reg.urls[u], reg.remote, reg.asyncWait);
            }
        }
        
    } else {
        if (callback) {
            callback.call(sender);
        }
    }
}

using.prototype = {	

    CallbackItem : function(_callback, _sender) {
        this.callback = _callback;
        this.sender = _sender;
        this.invoke = function() {
            if (this.sender) this.callback.call(this.sender);
            else this.callback();
        };
    },

	Registration : function(_name, _version, _remote, _asyncWait, _urls) {
	    this.name = _name;
	    var a=0;
	    var arg = arguments[++a];
	    var v=true;
	    if (typeof(arg) == "string") {
	        for (var c=0; c<arg.length; c++) {
	            if ("1234567890.".indexOf(arg.substring(c)) == -1) {
	                v = false;
	                break;
	            }
	        }
	        if (v) {
	            this.version = arg; // not currently used
	            arg = arguments[++a];
	        } else {
	            this.version = "1.0.0"; // not currently used
	        }
	    }
	    if (arg && typeof(arg) == "boolean") {
	        this.remote = arg;
	        arg = arguments[++a];
	    } else {
	        this.remote = false;
	    }
	    if (arg && typeof(arg) == "number") {
	        this.asyncWait = _asyncWait;
        } else {
            this.asyncWait = 0;
        }
	    this.urls = new Array();
	    if (arg && arg.length && typeof(arg) != "string") {
	        this.urls = arg;
	    } else {
	        for (a=a; a<arguments.length; a++) {
	            if (arguments[a] && typeof(arguments[a]) == "string") {
	                this.urls.push(arguments[a]);
	            }
	        }
	    }
	    this.requirements = new Array();
	    this.requires = function(resourceName, minimumVersion) {
	        if (!minimumVersion) minimumVersion = "1.0.0"; // not currently used
	        this.requirements.push({
	            name: resourceName,
	            minVersion: minimumVersion // not currently used
	            });
	        return this;
	    }
	    return this;
	},

    register : function(name, version, remote, asyncWait, urls) {
        var reg;
        if (typeof(name) == "object") {
            reg = name;
            reg = new using.prototype.Registration(reg.name, reg.version, reg.remote, reg.asyncWait, urls);
        } else {
            reg = new using.prototype.Registration(name, version, remote, asyncWait, urls);
        }
        if (!using.__regscr) using.__regscr = { };
        if (using.__regscr[name] && window.console) {
            window.console.log("Warning: Resource named \"" + name + "\" was already registered with using.register(); overwritten.");
        }
        using.__regscr[name] = reg; //urls;
        return reg;
    },
	
	wait: 0,
	
	defaultAsyncWait: 250,
	
	load: function(scriptName, scriptUrl, remote, asyncWait, cb) {
		if (asyncWait == undefined) asyncWait = using.wait;
		if (remote && asyncWait == 0) asyncWait = using.defaultAsyncWait;

		if (!using.__callbackQueue) {
			using.__callbackQueue = {};
		}
 		var callbackQueue = this.__callbackQueue[scriptUrl];
 		if (!callbackQueue) {
 		    callbackQueue = this.__callbackQueue[scriptUrl] = new Array();
 		}
 		callbackQueue.push(new using.prototype.CallbackItem( function() {
 		    using.__regscr[scriptName] = undefined;
 		}, null));
 		if (cb) {
 		    callbackQueue.push(cb);
 		    if (callbackQueue.length > 2) return;
 		}
 		if (remote) {
 		    using.srcScript(scriptUrl, asyncWait, callbackQueue);
 		} else {
			var xhr;
			if (window.XMLHttpRequest)
				xhr = new XMLHttpRequest();
			else if (window.ActiveXObject) {
				xhr = new ActiveXObject("Microsoft.XMLHTTP"); 
			}
			xhr.onreadystatechange = function(){
				if (xhr.readyState == 4 && xhr.status == 200) {
					using.injectScript(xhr.responseText, scriptName);
					if (callbackQueue) {
					    for (var q=0; q<callbackQueue.length; q++) {
					        callbackQueue[q].invoke();
					    }
					}
					using.__callbackQueue[scriptUrl] = undefined;
				}
			};
			if (asyncWait > 0 || callbackQueue.length > 1) {
			    xhr.open("GET", scriptUrl, true);
			} else {
			    xhr.open("GET", scriptUrl, false);
			}
			xhr.send();
 		}
	}, 
	
	genScriptNode : function() {
		var scriptNode = document.createElement("script");
		scriptNode.setAttribute("type", "text/javascript");
		scriptNode.setAttribute("language", "JavaScript");
		return scriptNode;	
	},
	srcScript : function(scriptUrl, asyncWait, callbackQueue) {
		var scriptNode = using.prototype.genScriptNode();
		scriptNode.setAttribute("src", scriptUrl);
		if (callbackQueue) {
		    var execQueue = function() {
			    for (var q=0; q<callbackQueue.length; q++) {
			        callbackQueue[q].invoke();
			    }
			    callbackQueue = new Array(); // reset
		    }
			scriptNode.onload = scriptNode.onreadystatechange = function() {
				if ((!scriptNode.readyState) || scriptNode.readyState == "loaded" || scriptNode.readyState == "complete" ||
					scriptNode.readyState == 4 && scriptNode.status == 200) {
					if (asyncWait > 0) {
						setTimeout(execQueue, asyncWait);
					}
					else {
						execQueue();
					}
				}
			};
		}
		var headNode = document.getElementsByTagName("head")[0];
		headNode.appendChild(scriptNode);
	},
	injectScript : function(scriptText, scriptName) {
		var scriptNode = using.prototype.genScriptNode();
		try {
		    scriptNode.setAttribute("name", scriptName);
		} catch (err) { }
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
