using Microsoft.AspNetCore.Mvc;

namespace CodeMart.Server.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
