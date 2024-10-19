using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrationAuth0(string username, string fullname, string password, string passwordConfirm, string phone, string email)
        {
            if(password != passwordConfirm)
            {
                ViewBag.Error = "Паролі не збігаються!";
                return View("Index");
            }
            return View("Index");
        }
    }
}
