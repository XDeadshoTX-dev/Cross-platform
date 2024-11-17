using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Lab13.Server.Controllers.Managements;
using Lab13.Server.Controllers.LabsLibrary;

namespace Lab5.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
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
        [HttpPost]
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

                Response.Cookies.Append("AuthToken", userToken, new CookieOptions
                {
                    HttpOnly = false,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddHours(1)
                });

                return Redirect("/Control");
            }
            catch(Exception ex) 
            {
                ViewBag.Error = ex.Message;
                return View("Index");
            }
        }
        [HttpGet]
        public async Task<string> GetProfile()
        {
            try
            {
                var authHeader = HttpContext.Request.Headers["Authorization"].ToString();

                if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                {
                    throw new Exception("Token is missing or invalid.");
                }
                string token = authHeader.Substring("Bearer ".Length).Trim();

                string userID = await authManagements.GetUserID(token);
                string jsonResponse = await authManagements.GetUserInfo(userID);

                return jsonResponse;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        LabsLibrary labsLibrary;
        [HttpPost]
        public async Task<string> StartLab(IFormFile inputFile, string lab)
        {
            try
            {
                if (inputFile == null || inputFile.Length == 0 || inputFile.FileName != "INPUT.TXT")
                {
                    return "The file was not uploaded!";
                }
                string directory = $"../../{lab}/INPUT.TXT";
                using (var stream = new FileStream(directory, FileMode.Create))
                {
                    await inputFile.CopyToAsync(stream);
                }


                labsLibrary = new LabsLibrary(lab);
                labsLibrary.Build();
                labsLibrary.Test();
                labsLibrary.Run();
                return labsLibrary.GetOutputConsole;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
