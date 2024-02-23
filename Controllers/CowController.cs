using Microsoft.AspNetCore.Mvc;

namespace Memories.Controllers
{
    public class CowController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
