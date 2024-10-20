using Lab5.Controllers.Managements;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Control()
        {
            return View();
        }

        AuthManagements authManagements = new AuthManagements();
        [HttpPost]
        public async Task<IActionResult> RegistrationAuth0(string username, string fullname, string password, string passwordConfirm, string phone, string email)
        {
            try
            {
                if (password != passwordConfirm)
                {
                    ViewBag.Error = "Паролі не збігаються!";
                    return View("Index");
                }
                string clientToken = await authManagements.GetClientTokenAsync();
                await authManagements.CreateUserAsync(username, fullname, password, phone, email, clientToken);
                ViewBag.Message = "Користувача успішно створено!";
                return View("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Index");
            }
        }
        public async Task<IActionResult> LoginAuth0(string username, string password)
        {
            try
            {
                if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    ViewBag.Error = "Введіть логін та пароль!";
                    return View("Index");
                }
                string userToken = await authManagements.GetUserTokenAsync(username, password);
                return Redirect("/Control");
            }
            catch(Exception ex) 
            {
                ViewBag.Error = ex.Message;
                return View("Index");
            }
        }
    }
}
