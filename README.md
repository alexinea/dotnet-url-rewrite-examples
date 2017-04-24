# ASP.NET UrlWrite 样例

### 问题

现有 `pc` 和 `mobile` 两文件夹（其文件夹内的结构也不同），根据用户的设备类型自动选择显示 pc 版或 mobile 版。

> **推荐**：使用栅格化的自适应设计让页面自动适应不同设备，以避免本模块造成的性能损失。

***

### ASP.NET URLWrite

项目：`src\UrlWrite.AspNet`

**仅可用于 IIS 7+**

`web.config` 的改动：

在 `system.webServer:modules` 内增加配置：

```
<add name="UrlRewriter" type="UrlRewrite.AspNet.HttpMoudle.UrlRewriteModule" />
```

在 `system.webServer:handlers` 内增加配置：

```
<remove name="ExtensionlessUrlHandler-ISAPI-4.0_32bit" />
<remove name="ExtensionlessUrlHandler-ISAPI-4.0_64bit" />
<remove name="ExtensionlessUrlHandler-Integrated-4.0" />
<remove name="Rewriter-32" />
<remove name="Rewriter-64" />
<add name="ExtensionlessUrlHandler-ISAPI-4.0_32bit" path="*." verb="GET,HEAD,POST,DEBUG,PUT,DELETE,PATCH,OPTIONS" modules="IsapiModule" scriptProcessor="%windir%\Microsoft.NET\Framework\v4.0.30319\aspnet_isapi.dll" preCondition="classicMode,runtimeVersionv4.0,bitness32" responseBufferLimit="0" />
<add name="ExtensionlessUrlHandler-ISAPI-4.0_64bit" path="*." verb="GET,HEAD,POST,DEBUG,PUT,DELETE,PATCH,OPTIONS" modules="IsapiModule" scriptProcessor="%windir%\Microsoft.NET\Framework64\v4.0.30319\aspnet_isapi.dll" preCondition="classicMode,runtimeVersionv4.0,bitness64" responseBufferLimit="0" />
<add name="ExtensionlessUrlHandler-Integrated-4.0" path="*." verb="GET,HEAD,POST,DEBUG,PUT,DELETE,PATCH,OPTIONS" type="System.Web.Handlers.TransferRequestHandler" preCondition="integratedMode,runtimeVersionv4.0" />
<add name="Rewriter-32" path="*" verb="GET,HEAD,POST,DEBUG,PUT,DELETE,PATCH,OPTIONS" modules="IsapiModule" scriptProcessor="%windir%\Microsoft.NET\Framework\v4.0.30319\aspnet_isapi.dll" resourceType="Unspecified" requireAccess="None" preCondition="classicMode,runtimeVersionv4.0,bitness32" />
<add name="Rewriter-64" path="*" verb="GET,HEAD,POST,DEBUG,PUT,DELETE,PATCH,OPTIONS" modules="IsapiModule" scriptProcessor="%windir%\Microsoft.NET\Framework\v4.0.30319\aspnet_isapi.dll" resourceType="Unspecified" requireAccess="None" preCondition="classicMode,runtimeVersionv4.0,bitness64" />
```

推荐阅读：

+ [Bikeman868/UrlRewrite.Net](https://github.com/Bikeman868/UrlRewrite.Net)

***

### ASP.NET Core URLWrite

项目：`src\UrlWrite.AspNetCore`

NuGet 安装：

```
PM> install-package Wangkanai.Responsive -pre
```

在 `ConfigureServices` 方法中配置：
```
services.AddResponsive()
        .AddViewSuffix()
        .AddViewSubfolder();
```

在 `Configure` 方法中使用中间件：
```
app.UseResponsive();
```

`ASP.NET Core Responsive` 会根据你设备的种类调用不同的视图：

+ 如果使用的是 `AddViewSuffix()`，则路径形如 `views/[controller]/[action]/index.mobile.cshtml` 
+ 如果使用的是 `AddViewSubfolder（）`，则路径形如 `views/[controller]/[action]/mobile/index.cshtml`

推荐阅读：

+ [URL Rewriting Middleware in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/url-rewriting)
+ [ASP.NET Core Responsive](https://github.com/wangkanai/Responsive)