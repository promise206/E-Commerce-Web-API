using ECommerceApp.Core.DTO;
using ECommerceApp.Domain.Model;

namespace ECommerceApp.Core.Interface
{
    public interface IAuthService
    {
        Task<string> CreateToken();
        Task<bool> LoginAsync(LoginDTO details);
        Task<ResponseDTO<string>> RegisterAsync(RegistrationDTO userDetails);
    }
}