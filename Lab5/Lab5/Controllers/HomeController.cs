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
            Console.WriteLine($"{username} {fullname} {password} {passwordConfirm} {phone} {email}");
            ViewBag.Username = username;
            return RedirectToAction("Index");
        }
    }
}
