using Microsoft.AspNetCore.Mvc;

namespace KnowledgeSpace.Backend.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
