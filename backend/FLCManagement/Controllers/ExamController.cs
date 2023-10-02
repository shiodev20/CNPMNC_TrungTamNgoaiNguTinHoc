using Microsoft.AspNetCore.Mvc;

namespace FLCManagement.Controllers
{
    public class ExamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
