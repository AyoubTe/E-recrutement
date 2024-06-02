using Microsoft.AspNetCore.Mvc;

namespace E_recrutement.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
