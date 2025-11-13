using Microsoft.AspNetCore.Mvc;

namespace CodeMart.Server.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
