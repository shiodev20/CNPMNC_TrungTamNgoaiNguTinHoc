using Microsoft.AspNetCore.Mvc;

namespace FLCManagement.Controllers
{
    public class BranchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
