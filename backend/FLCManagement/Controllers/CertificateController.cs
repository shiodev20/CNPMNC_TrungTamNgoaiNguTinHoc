using Microsoft.AspNetCore.Mvc;

namespace FLCManagement.Controllers
{
    public class CertificateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
