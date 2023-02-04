using Microsoft.AspNetCore.Mvc;
using MovieStore.Models.DTO;
using MovieStore.Repositories.Abstract;

namespace MovieStore.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly IUserAuthenticationServices _userAuthenticationServices;
        public UserAuthenticationController(IUserAuthenticationServices userAuthenticationServices)
        {
            _userAuthenticationServices = userAuthenticationServices;
        }
        public async Task<IActionResult> Register()
        {
            var model = new RegistrationModel()
            {
                Email = "admin@gmail.com",
                UserName = "Admin",
                Name = "Cassio",
                Password = "Admin@1234",
                PasswordConfirm = "Admin@1234",
                Role = "Admin"
            };
            var result = await _userAuthenticationServices.RegisterAsync(model);
            return Ok(result.Message);
        }

        public async Task<IActionResult> Login()
        {       
             return View();            
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            var result = await _userAuthenticationServices.LoginAsync(login);
            if(result.StatusCode == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["msg"] = "Erro ao tentar logar!";
                return RedirectToAction(nameof(Login));
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _userAuthenticationServices.LogoutAsync();

            return RedirectToAction(nameof(Login));
        }
    }
}
