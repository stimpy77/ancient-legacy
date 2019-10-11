# Client-side cache invalidation
## Proof of concept

This was just a proof of concept for an ongoing app whereby client-side files
that were cached on the client side were marked as "stale" when the server
decided to tell the client that they should be marked as "stale". The push
messaging would happen when a POST to an API would mutate a model, thus
invalidating the client-side cache of the model, so as such the POST action
would return a response with a Response header of: 
`X-Invalidate-Cache-Item: {..cache item key..}`.

Client-side script would spot this and do a new GET with a cachebusting
querystring to get a fresh copy of the model.

### Server-side sample (Controllers\HomeController.cs)

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

### Client-side sample

    <script>
        var $APPROOT = "@Url.Content("~/")";

        var httpGET = function(url, data, callback) {
            return httpAction(url, data, callback, "GET");
        };

        var httpPOST = function(url, data, callback) {
            return httpAction(url, data, callback, "POST");
        };

        var httpAction = function(url, data, callback, method) {
            url = cachebust(url);
            if (typeof(data) === "function") {
                callback = data;
                data = null;
            }
            return $.ajax(url, {
                data: data,
                type: method,
                success: function(responsedata, status, xhr) {
                    handleResponse(responsedata, status, xhr, callback);
                }
            });
        };

        var handleResponse = function(data, status, xhr, callback) {
            handleInvalidationFlags(xhr);
            callback.call(this, data, status, xhr);
        };

        function handleInvalidationFlags(xhr) {
            // capture HTTP header
            var invalidatedItemsHeader = xhr.getResponseHeader("X-Invalidate-Cache-Item");
            if (!invalidatedItemsHeader) return;
            invalidatedItemsHeader = invalidatedItemsHeader.split(';');
            
            // get invalidation flags from session storage
            var invalidatedItems = sessionStorage.getItem("invalidated-cache-items");
            invalidatedItems = invalidatedItems ? JSON.parse(invalidatedItems) : {};
            
            // update invalidation flags data set
            for (var i in invalidatedItemsHeader) {
                invalidatedItems[prepurl(invalidatedItemsHeader[i])] = Date.now();
            }
            
            // store revised invalidation flags data set back into session storage
            sessionStorage.setItem("invalidated-cache-items", JSON.stringify(invalidatedItems));
        }

        // since we're using IIS/ASP.NET which ignores case on the path, we need a function to force lower-case on the path

        function prepurl(u) {
            return u.split('?')[0].toLowerCase() + (u.indexOf("?") > -1 ? "?" + u.split('?')[1] : "");
        }

        function cachebust(url) {
            // get invalidation flags from session storage
            var invalidatedItems = sessionStorage.getItem("invalidated-cache-items");
            invalidatedItems = invalidatedItems ? JSON.parse(invalidatedItems) : {};

            // if item match, return concatonated URL
            var invalidated = invalidatedItems[prepurl(url)];
            if (invalidated) {
                return url + (url.indexOf("?") > -1 ? "&" : "?") + "_nocache=" + invalidated;
            }
            // no match; return unmodified
            return url;
        }

        // application logic
        httpGET($APPROOT + "Home/GetData", function(o) {
            $('#results').text("Last modified: " + o.LastModified);
        });

        $('#reload').on('click', function() {
            window.location.reload();
        });

        $('#invalidate').on('click', function() {
            httpPOST($APPROOT + "Home/DoSomething", function(o) {
                window.location.reload();
            });
        });
    </script>
