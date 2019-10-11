#### *Rough HTML-to-Markdown conversion*

This README.md file is a direct automated conversion of
index.html to Markdown format. As such, it will have several
inconsistencies, formatting issues, and missing images.

This project was an effort that shortly preceded jQuery UI,
back when jQuery itself was relatively new, and plugins were
the cool thing to produce and contribute to the Javascript
community. jqDialogForms was essentially a modal dialog
plugin with forms input support, so the idea / user story was, 
in VB6-like UI style, a user might click a button, get a 
pop-over "window" (really a floating div), fill out a form, 
hit "OK", and it might use jQuery AJAX to submit the form in 
that "window" and the "window" would close. All inline on the 
page.

----


<div id="header">

# jqDialogForms

<div id="header-bottom">

<div>Submit bug reports and suggestions at jQuery.com: [http://plugins.jquery.com/project/jqDialogForms](http://plugins.jquery.com/project/jqDialogForms)</div>

</div>

</div>

<div id="contentBody">

*   [<span>About jqDialogForms</span>](#About)
*   [<span>How To Use</span>](#HowToUse)
*   [<span>Demo</span>](#Demo)
*   [<span>Options</span>](#Options)
*   [<span>Download</span>](#Download)

<div id="About">![](images/doc/sshot_jqDF_Alert.gif)

jqDialogForms is a Javascript library based on jQuery that was built for producing windowed forms using strictly dynamic HTML. The inspiration comes from the development workflow of Visual Basic "Forms" and .NET Windows Forms, combined with the utility of HTML <form>'s and jQuery's serialize().

Features:

*   Modeless+parent disabling+serializeable dialog form windows  

*   Reference a DOM element or a string of HTML as the message or form fields to be displayed
*   Automatically switch from fixed positioning to absolute positioning when in quirks mode IE7, and position to the current vertical scroll position
*   jqDnR enabled: Resizeable, Windows 9x style
*   jqDnR enabled: Drag around the screen via the title bar
*   Activateable; show multiple windows, and activate/focus each window as it is clicked on (Windows 9x application windows activation behavior)
*   Lacking CSS conformance of a dialog window container, a prefab one is used that includes a title bar, a close box, and OK/Cancel buttons.
*   OK button invokes Apply event handler and then, if not invalidated, closes the window.
*   Dirty state detection; on editable forms, OK/Apply buttons are disabled unless a field is changed
*   Exposes form serialize functions that outputs name/value HTML/querystring encoding (in the style of jQuery serialize()), or JSON serialization.
*   "Smart" top positioning for oversized dialogs
*   CSS-driven layout

![](images/doc/sshot_jqDF_childwin.gif)

Among the several known issues:

*   Multiple windows do not retain their activation order.
*   No modal dialog or truly modeless (non-disabling) support
*   <strike>No demo</strike> No extensive demo
*   No documentation  

A few months back I released a Javascript library called [jqAlert](http://www.jondavis.net/codeprojects/jqalert/). I will probably rewrite jqAlert to use jqDialogForms.

It's NOT perfect, there are a few missing parts and pieces, but as far as I know it's a stable beta and can probably be used in production environments (I think).

</div>

<div id="HowToUse">

Unlike normal jQuery plug-ins, jqDialogForms is only _dependent upon_ jQuery, it does not _attach to_ jQuery.

To use jqDialogForms, you must instantiate a new DialogWindow object with your options, pass a function callback to its `apply()` event, and then invoke its `show()` function.

Here is the constructor signature for the DialogWindow object:

> <pre>var DialogWindow = function(optional_message, optional_options, optional_parentWindow) { ... }</pre>

For example,

    var msg = 'Greetings.';
    var myWindow = new DialogWindow(msg, {title: 'Enter your name'});
    myWindow.show();
    
* * *

> <pre>var msg = '<p>Please enter your name.</p><p><input name="your_name" style="width: 250px" /></p>';
> var myWindow = new DialogWindow(msg, {title: 'Enter your name'});
> myWindow.el.style.width = '300px';
> myWindow.beforeShow(function() { alert('beforeShow'); });
> myWindow.show(function() { alert('showing'); });
> myWindow.apply(function() { alert('applying: ' + myWindow.serialize(true)); });
> myWindow.show();
>             </pre>

### Alert

This sample demonstrates the simplest form of a dialog.

    <button type="button" id="demo_alert" onclick="(new DialogWindow('Hi.')).show();">Alert</button>  

* * *

<div id="demo_multiact_container">

### Multiple Forms & Activation

This sample displays multiple dialog windows. Click on the titlebar, message text, or gray space of each one to "activate" it.

    <script>
    function demo_multiact() {
        var txt = 'Click to activate';
		var win1 = new DialogWindow(txt), win2 = new DialogWindow(txt), win3 = new DialogWindow(txt);
		win1.show();
		win2.show();
		win3.show();
		win1.el.style.left = (parseInt(win1.el.style.left.replace(/px/, '')) - 80).toString() + 'px'; win1.el.style.top = (parseInt(win1.el.style.top.replace(/px/, '')) - 20).toString() + 'px';
		win3.el.style.left = (parseInt(win3.el.style.left.replace(/px/, '')) + 80).toString() + 'px'; win3.el.style.top = (parseInt(win3.el.style.top.replace(/px/, '')) + 20).toString() + 'px';
	}</script>
	<button type="button" id="demo_multiact" onclick="demo_multiact()">Multiple windows and activation/selection</button>
	
* * *

<div id="demo_apply_handler_container">

### 'Apply' Handler

Demonstrates the automatic detection of changes to form fields, followed by the manual handling of the 'Apply' event, which is raised when the OK button or the Apply button is clicked. (OK=Apply+Close.)

    <script type="text/javascript" language="javascript">
	    function demo_apply_handler() { 
		    var applwin = new DialogWindow('change me<br><input type="textbox" name="Field1" value="Field1_Value" />', { 
			    hideApplyButton: false, hideCancelButton: false }); // assign event handler for apply event
			applwin.apply(function() {
				alert('applying...');
				alert(this.serialize(true)); });
				applwin.el.style.width = '400px';
				applwin.el.style.height = '200px';
				applwin.show();
			});
		}
	</script>
	<button type="button" id="demo_apply_handler" onclick="demo_apply_handler()">'Apply' Handler</button>
	
* * *

<div id="demo_parent_child_container">

### Parent/Child Window Ownership

Demonstrates a parent's "ownership" of a child window, forcing the child window to stay on top of the parent window.

    <script language="javascript" type="text/javascript">
	    function demo_parent_child() {
            var parent = new DialogWindow('Parent: Try to select me');
			parent.show();
			var child = new DialogWindow('Child: I\'m pretty much parent-owned modeless.', null, parent);
			child.show();
			parent.el.style.left = (parseInt(parent.el.style.left.replace(/px/, '')) - 225).toString() + 'px';
			parent.el.style.top = (parseInt(parent.el.style.top.replace(/px/, '')) - 20).toString() + 'px';
		}</script>
		<button type="button" onclick="demo_parent_child()">Parent/Child</button>  

* * *

<div id="demo_parent_child_field_blocking_container">

### Parent/Child Window Ownership With Parent Fields Blocking

Demonstrates a parent's "ownership" of a child window, forcing the child window to stay on top of the parent window, and blocking the parent's input fields until the child window is closed.

    <script language="javascript" type="text/javascript">function demo_parent_child_field_blocking() { var parent = new DialogWindow('Parent: Try to close me'); parent.show(); var child = new DialogWindow('Child: Close me to access parent', { modal: 'parent' }, parent); child.show(); parent.el.style.left = (parseInt(parent.el.style.left.replace(/px/, '')) - 170).toString() + 'px'; parent.el.style.top = (parseInt(parent.el.style.top.replace(/px/, '')) - 50).toString() + 'px'; }</script> <button type="button" onclick="demo_parent_child_field_blocking()">Parent/Child (blocking)</button>  

* * *

<div id="demo_domhtml_container">

### DOM Element => HTML

This sample shows how the HTML of a DOM element can be used for the content body.

    <script type="text/javascript">function demo_domhtml() { var frmwin = new DialogWindow( $('#demo_dom_form_container').html(), { title: 'DOM Referenced Dialog' }); frmwin.show(); }</script> <button type="button" onclick="demo_domhtml()">DOM Referenced Dialog</button>  

* * *

### DOM Element (direct)

This sample shows how a DOM element can be directly used for the content body. _Note: Once consumed, it is destroyed with the window._

    <script type="text/javascript">
        function demo_dom() {
		    if ($('#demo_dom_form2').length == 0) {
			    alert('No DOM object available to consume.');
			}
			var frmwin = new DialogWindow( $('#demo_dom_form2')[0], {
			    title: 'DOM Referenced Dialog'
			});
			frmwin.show();
		}
	</script>
	<button type="button" onclick="demo_dom()">DOM Referenced Dialog</button>  

* * *

### Form Serialization

This sample demonstrates the two types of form serialization: default encoding and JSON.

    <script type="text/javascript">
        function demo_domserialize() {
		    var frmwin = new DialogWindow( $('#demo_dom_form_container').html() + $('#demo_serialize_buttons').html(), { title: 'DOM Referenced Dialog' }); frmwin.show();
		}
	</script>
	<button type="button" onclick="demo_domserialize()">DOM Referenced Dialog</button>  

The default options ...

    DialogWindow.DefaultOptions = {

        containerWindowElement: null,

        reuseElement: false,        // set to true to use the element object rather 
                                    // than its HTML

        title: 'Dialog',            // the text for dialogTitle element

        top: 'middle',              // set to pixels or to 'middle'

        left: 'center',             // set to pixels or to 'center'

        css: {                      // injected to the dialogWindow element
            position: 'fixed',
            minWidth: '120px'
        },

        iconUrl: '',                // if set, adds an image to dialogIcon element

        modal: false,               // can be false (normal), true (page blocking -- not yet implemented), 
                                    //      'modeless' (takes Z-Index of parent), or 
                                    //      'parent' (like 'modeless' but also block 
                                    //      parent's message and buttons)
        showAnimate: null,          // function; callback function that uses jQuery

                                    //      animate options structure, w/ added 
                                    //      props: speed, easing, callback

        hideAnimate: null,          // function; callback function for performing an 
                                    //      animation after closing a form

        hideOkCancelButtons: false, // hide the element that contains the OK / Apply 
                                    //      / Cancel buttons

        hideOkButton: false,        // hide the OK button

        hideApplyButton: true,      // hide the Apply button

        hideCancelButton: true,     // hide the Cancel button

        allowResize: true,          // enable jqDnR for resizing

        okText: 'OK',               // the text to be displayed on the OK button

        applyText: 'Apply',         // the text to be displayed on the Apply button

        cancelText: 'Cancel'        // the text to be displayed on the Cancel button
    };

