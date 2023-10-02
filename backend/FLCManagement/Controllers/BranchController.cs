using FLCManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace FLCManagement.Controllers
{
    public class BranchController : Controller
    {
        public IActionResult Index()
        {
            Branch branch = new Branch() {
                branchID = 1,
                branchName = "Chi nhánh 1",
                branchAddress = "ABC",
            };

            return Ok(branch);
        }
    }
}
