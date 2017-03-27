#ASP.NET UrlWrite 样例

###问题

现有 `pc` 和 `mobile` 两文件夹（其文件夹内的结构也不同），根据用户的设备类型自动选择显示 pc 版或 mobile 版。

> **推荐**：使用栅格化的自适应设计让页面自动适应不同设备，以避免本模块造成的性能损失。

***

### ASP.NET URLWrite

项目：`src\UrlWrite.AspNet`

**仅可用于 IIS 7+**

`web.config` 的改动：

0. 在 `system.webServer:modules` 内增加配置：
```
<add name="UrlRewriter" type="UrlRewrite.AspNet.HttpMoudle.UrlRewriteModule" />
```
1. 在 `system.webServer:handlers` 内增加配置：
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