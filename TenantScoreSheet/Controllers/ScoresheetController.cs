using Microsoft.AspNetCore.Mvc;

namespace TenantScoreSheet.Controllers
{
    public class ScoresheetController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
