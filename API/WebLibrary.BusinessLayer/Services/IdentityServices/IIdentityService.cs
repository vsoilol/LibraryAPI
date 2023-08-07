using WebLibrary.Domain.Dtos;
using WebLibrary.Domain.Requests.Identity;

namespace WebLibrary.BusinessLayer.Services.IdentityServices;

public interface IIdentityService
{
    Task<AuthenticationResult> LoginAsync(LoginRequest request);
    
    Task<AuthenticationResult> RegisterAsync(RegisterRequest request);
}