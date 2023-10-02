using Microsoft.AspNetCore.Mvc;

namespace FLCManagement.Controllers
{
    public class ClassController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
