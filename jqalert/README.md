# jqalert - The jQuery-empowered alert() replacement

_ jqalert_ is a Javascript library that uses jQuery to create a compelling alternative to window.alert().

Author: Jon Davis [ [http://www.jondavis.net/](http://www.jondavis.net/) ]

Current version: 0.9  
[jqalert.js](jqalert.js) (unpacked - 25kb)

Requires: jQuery v1.2.3 @ [http://www.jquery.com/](http://www.jquery.com/)  
Suggested: jqDnR by Brice Burgess @ [http://dev.iceburg.net/jquery/jqDnR/](http://dev.iceburg.net/jquery/jqDnR/)

Note that this current build still has a few bugs and missing features. Among the bugs is the use of getEmptyOptions() and then setting {modal: true} can have unpredictable results, as demonstrated below.

### But that Windows 98 look is so ugly!

Skin it. Or just wait; coming in a future build will be more named styles and named style extensibility. Also plan to add more optional buttons like OK / Cancel, or Yes / No, or Yes / No / Cancel, along with dialog result.

* * *

### To use:

Just add the following tags to your <head>:

> <pre><script type="text/javascript" language="javascript" src="http://cachefile.net/scripts/jquery/1.2.3/jquery-1.2.3.js"></script>
> <script type="text/javascript" language="javascript" src="http://cachefile.net/scripts/jquery/plugins/jqDnR/2007.08.19r2/jqDnR.js"></script>
> **<script type="text/javascript" language="javascript" src="http://cachefile.net/scripts/jquery/plugins/jqalert/0.9/jqalert.js"></script>**</pre>

.. then when you need to do an alert(message), instead call jqalert(message, title).

**To make _jqalert_ override window.alert, add this to your <head> tag:**

> <pre><script type="text/javascript" language="javascript">
> try {
> 	window.alert = window.jqalert;
> } catch (err) {
> 	window.alert('Your browser does not support overloading window.alert. ' + err);
> }
> </script></pre>

* * *

### Demos

<button onclick="jqalert('This is the default behavior. You can get this by calling jqalert(message, title).<br />Notice that it is modal, and by including the <a href=&quot;http://dev.iceburg.net/jquery/jqDnR/&quot;>jqDnR extension script</a> you can<br />drag this window around with the title bar.', 'Demo: Default Behavior')">Default Behavior</button> <button onclick="jqalert('Calling jqalert(message), without a title<br />parameter, removes the title bar.')">No Title</button> <button onclick="jqalert('You can set a third parameter of {modal: false} to turn of modality. Notice<br />that you can still interact with the page even while this message is shown.', 'Demo: Non-Modal', {modal: false})">Non-Modal</button> <button onclick="jqalert('<h1>HTML <i>Markup</i>HTML Markup</h1><p>The <em>message</em> parameter accepts HTML markup.</p>', 'Demo: HTML Markup')"></button> <button onclick="jqalert($(&quot;#htmlElementDemo&quot;).html(), 'Demo: HTML Element Message')">HTML Element Message</button> <button onclick="jqalert('This demonstrates the use of an &quot;info&quot; icon by setting<br />the third parameter to {icon: &quot;alert_icon_info.png&quot;}.', 'Demo: Info Icon', {icon: &quot;alert_icon_info.png&quot;})">Info Icon</button> <button onclick="jqalert('This demonstrates the use of an &quot;warning&quot; icon by setting<br />the third parameter to {icon: &quot;alert_warning_info.png&quot;}.', 'Demo: Warning Icon', {icon: &quot;alert_icon_warning.png&quot;})">Warning Icon</button> <button onclick="jqalert('This demonstrates the use of an &quot;error&quot; icon by setting<br />the third parameter to {icon: &quot;alert_error_info.png&quot;}.', 'Demo: Error Icon', {icon: &quot;alert_icon_error.png&quot;})">Error Icon</button> <button onclick="jqalert('If you want to build up your own custom parameters,<br />you might want to start with a clean slate by<br />setting for the third parameter <tt>jqalerter.getEmptyOptions()</tt>.<br />You can add options inline using  <tt>.setOption(name, value)</tt>.<br />In this case, modal: true and backgroundColor: white were used.', 'Demo: Empty Options', jqalerter.getEmptyOptions().setOption('modal', true).setOption('backgroundColor', 'white'))">Empty Options (unpredictable results)</button> <button onclick="jqalert('The entire alert window can reuse a custom HTML element.<br />Here one is in a modal alert with a &quot;clean slate&quot; option set.', 'Demo: Custom Window Element', 
        jqalerter.getEmptyOptions()
        .setOption('modal', true)
        .setOption('dynamicCss', true) // needed for z-indexing
        .setOption('windowCss', null), // needed for z-indexing
        $(&quot;#windowElementDemo&quot;)[0])">Custom Window Element (modal; near-empty options) (unpredictable results)</button> <button onclick="jqalert('The entire alert window can reuse a custom HTML element.<br />Here one is in a non-modal alert.<br />&nbsp;<br />Note: There is a bug right now, you cannot now use the modal one with clean slate options.', 'Demo: Custom Window Element', jqalerter.getDefaultOptions()
        .setOption('dynamicCss', false)
        .setOption('modal', false),
        $(&quot;#windowElementDemo&quot;)[0])">Custom Window Element (non-modal; default options)</button>

<div id="htmlElementDemo" style="display:none;">

# <u>HTML</u> _message_ from a <div>!

Using HTML for the message can be handy if you want to "pre-fab" your  
messages in markup without declaring the markup in script.

</div>

<div id="windowElementDemo" class="myOwnStuff" style="display: none;">

<div class="jqalert-container">

<div class="jqalert">

<div class="jqalert-buttons"><button onclick="window.jqalerter.closeVirtualWindow(this.parentNode.parentNode.parentNode);">OK</button></div>

</div>

</div>

</div>