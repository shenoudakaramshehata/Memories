using Microsoft.AspNetCore.Mvc;

namespace Memories.Controllers
{
    public class PigController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
