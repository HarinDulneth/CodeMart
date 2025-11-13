using Microsoft.AspNetCore.Mvc;

namespace CodeMart.Server.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
