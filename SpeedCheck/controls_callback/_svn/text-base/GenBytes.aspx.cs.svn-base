using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class controls_callback_GenBytes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sByteLen = Request["len"];
        int iByteLen = 2^10;
        if (sByteLen != null && sByteLen.Trim() != "")
        {
            iByteLen = int.Parse(sByteLen);
        }
        if (iByteLen > 10000000)
        {
            throw new ArgumentOutOfRangeException("Specified length is too high.");
        }
        byte[] ret = new byte[iByteLen];
        for (int i = 0; i < iByteLen; i++)
            ret[i] = (byte)255;
        Response.Clear();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.AddHeader("Content-Disposition", "attachment; filename=bytes.bin");
        Response.AddHeader("Content-Length", ret.Length.ToString());
        Response.ContentType = "application/octet-stream";
        //System.Threading.Thread.Sleep((int)(iByteLen / 20));
        Response.BinaryWrite(ret);
        Response.Flush();
        Response.End();
    }
}
