using MovieStore.Models.DTO;

namespace MovieStore.Repositories.Abstract
{
    public interface IUserAuthenticationServices
    {
        Task<Status> LoginAsync(LoginModel login);
        Task LogoutAsync();
        Task<Status> RegisterAsync(RegistrationModel registration);
    }
}
