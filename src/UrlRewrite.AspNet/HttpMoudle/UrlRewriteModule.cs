using System;
using System.Web;
using System.Web.SessionState;
using UrlRewrite.AspNet.Browser;

namespace UrlRewrite.AspNet.HttpMoudle
{
    public class UrlRewriteModule : IHttpModule, IRequiresSessionState
    {
        protected BrowserModel Browser { get; set; }

        /// <summary>
        /// 加载事件管道 
        /// </summary>
        /// <param name="app"></param>
        public void Init(HttpApplication app)
        {
            app.AuthenticateRequest += UrlRewriter_AuthorizeRequest;
        }

        /// <summary>
        /// 注销 
        /// </summary>
        public void Dispose()
        {
        }

        protected void UrlRewriter_AuthorizeRequest(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;

            // 判断是否已重写 
            if (app.Request.Url.AbsoluteUri.IndexOf("UrlRewriter=true", StringComparison.Ordinal) > 0) { return; }

            // 写入追踪日志消息 
            app.Context.Trace.Write("Url重写", "开始执行。");

            //加载浏览器识别模块
            Browser = BrowserModel.CreateInstance(app.Context);

            //获取重写规则地址
            var newTargetUrl = ResolveUrl(
                app.Context.Request.ApplicationPath, 
                Browser.BrowserAliasBasedOn.ToLower() == "pc" ? "pc" : "mobile", 
                app.Request.Path);

            //写入追踪日志消息
            app.Context.Trace.Write("Url重写", "重写到：" + newTargetUrl);

            //重写地址
            RewriteUrl(app.Context, newTargetUrl);
        }

        protected string ResolveUrl(string appPath, string targetWebSiteType, string url)
        {
            return $"{appPath}{targetWebSiteType}/"
                + (url.StartsWith("~")
                    ? url.SubString(1)
                    : url.StartsWith("/")
                        ? url.SubString(1)
                        : url.StartsWith(appPath) ? url.Replace(appPath, "") : url);
        }

        /// <summary>
        /// 重写地址 
        /// </summary>
        /// <param name="ctx">  </param>
        /// <param name="newTargetUrl"> 重写的目标地址 </param>
        protected void RewriteUrl(HttpContext ctx, string newTargetUrl)
            => RewriteUrl(ctx, newTargetUrl, out string x, out string y);


        /// <summary>
        /// 重写地址 
        /// </summary>
        /// <param name="context">                 请求的上下文 </param>
        /// <param name="newTargetUrl">            重写的目的URL </param>
        /// <param name="filePath">                重写目的URL的实际物理路径 </param>
        /// <param name="newTargetUrlLessQString"> 不能参数的地址 </param>
        protected void RewriteUrl(HttpContext context, string newTargetUrl, out string newTargetUrlLessQString, out string filePath)
        {
            newTargetUrlLessQString = newTargetUrl.SubString(0, newTargetUrl.IndexOf('?'));

            filePath = string.Empty;

            var queryString = string.Empty;
            if (newTargetUrl.IndexOf('?') > -1)
            {
                queryString = newTargetUrl.SubString(newTargetUrl.IndexOf('?') + 1);
            }
            if (queryString.Length > 1 && !queryString.EndsWith("&"))
            {
                queryString += "&";
            }
            queryString += "UrlRewriter=true";

            context.RewritePath(newTargetUrlLessQString, string.Empty, queryString);
        }
    }
}