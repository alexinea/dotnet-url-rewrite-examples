using Microsoft.AspNetCore.Mvc;

namespace UrlRewrite.AspNetCore.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}