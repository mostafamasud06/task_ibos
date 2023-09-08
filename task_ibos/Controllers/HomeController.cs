using Microsoft.AspNetCore.Mvc;

namespace task_ibos.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
