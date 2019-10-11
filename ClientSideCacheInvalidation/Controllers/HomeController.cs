using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace ClientSideCacheInvalidation.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult DoSomething()
        {
            // 
            // Do something here that has a side-effect
            // of making the cached data stale
            // 

            InvalidateCacheItem(Url.Action("GetData"));
            return Json("OK");
        }

        public void InvalidateCacheItem(string url)
        {
            Response.RemoveOutputCacheItem(url);
            Response.AddHeader("X-Invalidate-Cache-Item", url);
        }

        [OutputCache(Duration = 300)]
        public JsonResult GetData()
        {
            return Json(new
                {
                    LastModified = DateTime.Now.ToString()
                }, JsonRequestBehavior.AllowGet);
        }

    }
}
