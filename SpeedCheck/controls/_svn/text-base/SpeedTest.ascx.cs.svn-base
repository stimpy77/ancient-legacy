/*

This is mine. You're free to reuse, but in your source please credit me! ;)
Also, if this proved usable for your site, let me know, I'd like to hear 
about it!

Jon Davis
http://www.jondavis.net/
jon@jondavis.net

*/


using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class controls_SpeedTest : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ExecuteTest)
        {
            string genBytesUrl = ResolveClientUrl(CallBackUrl);
            string script = AstMainScript.Text
                .Replace("{$GEN_BYTES_URL$}", genBytesUrl)
                .Replace("{$BYTE_LENGTH$}", ByteLength.ToString())
                .Replace("{$COMPLETE_FUNCTION$}", ClientCompleteFunction)
                .Replace("{$USE_COOKIE$}", UseCookie.ToString().ToLower())
                .Replace("{$COOKIE_EXPIRES$}", CookieExpires.TotalDays.ToString());
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "AjaxSpeedTest",
                script);
        }
    }

    private string _CallBackUrl = "~/controls_callback/GenBytes.aspx";
    /// <summary>
    /// The URL that returns the requested number of bytes 
    /// using the query string '?len=byteCount'. 
    /// Default value is "~/controls_callback/GenBytes.aspx".
    /// </summary>
    public string CallBackUrl
    {
        get { return _CallBackUrl; }
        set { _CallBackUrl = value; }
    }

    private bool _ExecuteTest = true;

    /// <summary>
    /// Gets or sets whether to execute the client-side test. 
    /// True will add the script to the page during Page_Load, 
    /// false will not. Default value is true.
    /// </summary>
    [System.ComponentModel.Browsable(true)]
    public bool ExecuteTest
    {
        get { return _ExecuteTest; }
        set { _ExecuteTest = value; }
    }


    private int _ByteLength = 131072;
    /// <summary>
    /// Gets or sets the number of bytes to be used 
    /// to perform the PipeSpeed test. Default value
    /// is 128KB (131072 bytes).
    /// </summary>
    [System.ComponentModel.Browsable(true)]
    public int ByteLength
    {
        get { return _ByteLength; }
        set { _ByteLength = value; }
    }

    private string _ClientCompleteFunction = "astTestComplete";
    /// <summary>
    /// Gets or sets the name of the Javascript function
    /// that is to be executed when the test completes. 
    /// It should have two arguments: latency and pipeSpeed.
    /// </summary>
    [System.ComponentModel.Browsable(true)]
    public string ClientCompleteFunction
    {
        get { return _ClientCompleteFunction; }
        set { _ClientCompleteFunction = value; }
    }

    private bool _UseCookie = true;
    /// <summary>
    /// Gets or sets whether to try to use the cookie 
    /// to determine the pipeSpeed. If true, the test
    /// will not execute unless there is no cookie. 
    /// Default value is true.
    /// </summary>
    [System.ComponentModel.Browsable(true)]
    public bool UseCookie
    {
        get { return _UseCookie; }
        set { _UseCookie = value; }
    }

    /// <summary>
    /// Gets or sets the Latency cookie value 
    /// in the context of the current request. 
    /// This is just a shortcut to using 
    /// Request.Cookies["astLatency"].
    /// Default value is null.
    /// </summary>
    [System.ComponentModel.Browsable(false)]
    public int? CookieLatency
    {
        get
        {
            HttpCookie cookie = Request.Cookies["astLatency"];
            if (cookie == null) return null;
            else
            {
				try
				{
					return int.Parse(cookie.Value);
				}
				catch
				{
					return null;
				}
			}
        }
        set
        {
            HttpCookie cookie = Request.Cookies["astLatency"];
            if (cookie == null)
            {
                cookie = new HttpCookie("astLatency");
                Request.Cookies.Add(cookie);
            }
            if (value.HasValue) cookie.Value = value.Value.ToString();
            cookie.Expires = DateTime.Now.Add(CookieExpires);
            if (Response.Cookies["astLatency"] != null) Response.Cookies.Remove("astLatency");
            Response.Cookies.Add(cookie);
        }
    }

    [System.ComponentModel.Browsable(false)]
    /// <summary>
    /// Gets or sets the Latency cookie value 
    /// in the context of the current request. 
    /// This is just a shortcut to using 
    /// Request.Cookies["astPipeSpeed"]. 
    /// Default value is null.
    /// </summary>
    public int? CookiePipeSpeed
    {
        get
        {
            HttpCookie cookie = Request.Cookies["astPipeSpeed"];
            if (cookie == null) return null;
            else
            {
				try {
					return int.Parse(cookie.Value);
				} 
				catch 
				{
					return null;
				}
			}
        }
        set
        {
            HttpCookie cookie = Request.Cookies["astPipeSpeed"];
            if (cookie == null)
            {
                cookie = new HttpCookie("astPipeSpeed");
                Request.Cookies.Add(cookie);
            }
            if (value.HasValue) cookie.Value = value.Value.ToString();
            cookie.Expires = DateTime.Now.Add(CookieExpires);
            if (Response.Cookies["astPipeSpeed"] != null) Response.Cookies.Remove("astPipeSpeed");
            Response.Cookies.Add(cookie);
        }
    }

    private TimeSpan _Expires = new TimeSpan(7, 0, 0, 0);
    /// <summary>
    /// TimeSpan (in granularity of days) to retain the resulting cookie. 
    /// Default value is 7.
    /// </summary>
    [System.ComponentModel.Browsable(true)]
    public TimeSpan CookieExpires
    {
        get { return _Expires; }
        set { _Expires = value; }
    }

}
