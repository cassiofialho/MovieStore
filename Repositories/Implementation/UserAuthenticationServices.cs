using Microsoft.AspNetCore.Identity;
using MovieStore.Models.Domains;
using MovieStore.Models.DTO;
using MovieStore.Repositories.Abstract;
using System.Security.Claims;

namespace MovieStore.Repositories.Implementation
{
    public class UserAuthenticationServices : IUserAuthenticationServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserAuthenticationServices(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<Status> LoginAsync(LoginModel login)
        {
            var status = new Status();
            var user = await _userManager.FindByNameAsync(login.Name);
            if(user == null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid User";
                return status;
            }
            if (!await _userManager.CheckPasswordAsync(user, login.Password))
            {
                status.StatusCode = 0;
                status.Message = "Invalid Password";
                return status;
            }
            var signInResult = await _signInManager.PasswordSignInAsync(user, login.Password, true, true);
            if (signInResult.Succeeded) 
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login.Name),
                };

                foreach(var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                status.StatusCode = 1;
                status.Message = "Logado com Sucesso!";
            }
            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "Blocked user";
                return status;
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Erro de Login";
                return status;
            }
            return status;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Status> RegisterAsync(RegistrationModel registration)
        {
            var status = new Status();
            var userExists = await _userManager.FindByNameAsync(registration.UserName);
            if(userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "User Exists!";
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = registration.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registration.UserName,
                Name = registration.UserName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registration.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "Criação do usuário falhou!";
                return status;
            }
            if(!await _roleManager.RoleExistsAsync(registration.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole(registration.Role));
            }
            if(await _roleManager.RoleExistsAsync(registration.Role))
            {
                await _userManager.AddToRoleAsync(user, registration.Role);
            }
            status.StatusCode = 1;
            status.Message = "User successfully registered";
            return status;
        }
    }
}
