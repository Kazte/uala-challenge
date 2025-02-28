using ErrorOr;

namespace ualax.application.Features.Users
{
    public interface IUserService
    {
        Task RegisterUser(string username);
        Task<string> LoginUser(string username);
    }
}
