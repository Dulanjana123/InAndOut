using Microsoft.AspNetCore.Mvc;

namespace InAndOut.Controllers
{
    public class ReturnTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
