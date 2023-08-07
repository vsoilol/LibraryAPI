using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Mapster;
using Microsoft.IdentityModel.Tokens;
using WebLibrary.BusinessLayer.Constants;
using WebLibrary.BusinessLayer.Extensions;
using WebLibrary.DataAccessLayer.Repositories.UserRepositories;
using WebLibrary.Domain.Dtos;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Requests.Identity;
using WebLibrary.Domain.Settings;

namespace WebLibrary.BusinessLayer.Services.IdentityServices;

internal class IdentityService : IIdentityService
{
    private readonly IUserRepository _userRepository;
    private readonly AuthSettings _authSettings;

    public IdentityService(IUserRepository userRepository, AuthSettings authSettings)
    {
        _userRepository = userRepository;
        _authSettings = authSettings;
    }

    public async Task<AuthenticationResult> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetUserByLoginAsync(request.Login);

        if (user is null)
        {
            return new AuthenticationResult(new[] { LoginErrorMessages.UserDoesNotExist });
        }

        var userHasValidPassword = SecurePasswordHasher.Verify(request.Password, user.PasswordHash);

        if (!userHasValidPassword)
        {
            return new AuthenticationResult(new[] { LoginErrorMessages.InvalidPasswordCombination });
        }

        return GenerateAuthenticationResultForUser(user);
    }

    public async Task<AuthenticationResult> RegisterAsync(RegisterRequest request)
    {
        var existingUser = await _userRepository.GetUserByLoginAsync(request.Login);

        if (existingUser is not null)
        {
            return new AuthenticationResult(new[] { RegisterErrorMessages.UserAlreadyExists });
        }

        var passwordHash = SecurePasswordHasher.Hash(request.Password);

        var newUser = request.Adapt<User>();
        newUser.PasswordHash = passwordHash;

        var createdUser = await _userRepository.InsertAsync(newUser);

        return GenerateAuthenticationResultForUser(createdUser);
    }

    private AuthenticationResult GenerateAuthenticationResultForUser(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_authSettings.Secret);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Login),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Email, user.Login),
            new("Id", user.Id.ToString()),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.Add(_authSettings.TokenLifetime),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            ),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);

        return new AuthenticationResult(jwtToken);
    }
}