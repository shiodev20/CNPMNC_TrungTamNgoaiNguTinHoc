using Microsoft.AspNetCore.Mvc;

namespace FLCManagement.Controllers
{
    public class LanguageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
