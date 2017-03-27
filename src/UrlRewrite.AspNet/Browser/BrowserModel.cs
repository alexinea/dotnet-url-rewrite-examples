using System;
using System.Web;

namespace UrlRewrite.AspNet.Browser
{
    public class BrowserModel
    {
        internal BrowserModel() { }

        public static BrowserModel CreateInstance(HttpContext httpContext) => BrowserHelper.Create(httpContext);

        public HttpContext CurrentHttpContext { get; internal set; }

        public string UserAgent { get; set; }

        public string IpAddress { get; set; }

        public BrowserBasedOn BrowserBasedOn { get; set; }

        public string BrowserAliasBasedOn => Enum.GetName(typeof(BrowserBasedOn), BrowserBasedOn);
    }
}