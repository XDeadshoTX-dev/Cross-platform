using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Lab13.Server.Controllers.Managements;
using Lab13.Server.Controllers.LabsLibrary;

namespace Lab13.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        AuthManagements authManagements = new AuthManagements();
        [HttpPost("RegistrationAuth0")]
        public async Task<IActionResult> RegistrationAuth0(string username, string fullname, string password, string passwordConfirm, string phone, string email)
        {
            try
            {
                if (password != passwordConfirm)
                {
                    return BadRequest("Паролі не збігаються!");
                }
                string clientToken = await authManagements.GetClientTokenAsync();
                await authManagements.CreateUserAsync(username, fullname, password, phone, email, clientToken);
                return Ok("Користувача успішно створено!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("LoginAuth0")]
        public async Task<IActionResult> LoginAuth0([FromBody] LoginModel loginData)
        {
            try
            {
                if (loginData == null || string.IsNullOrEmpty(loginData.username) || string.IsNullOrEmpty(loginData.password))
                {
                    return BadRequest("Введіть логін та пароль!");
                }
                string userToken = await authManagements.GetUserTokenAsync(loginData.username, loginData.password);

                return Ok(new { message = "Успішний вхід", token = userToken });
            }
            catch(Exception ex) 
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        public class LoginModel
        {
            public string username { get; set; }
            public string password { get; set; }
        }
        [HttpGet("GetProfile")]
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
        Labs labsLibrary;
        [HttpPost("StartLab")]
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


                labsLibrary = new Labs(lab);
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
