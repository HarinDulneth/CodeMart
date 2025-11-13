using Microsoft.AspNetCore.Mvc;

namespace CodeMart.Server.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
