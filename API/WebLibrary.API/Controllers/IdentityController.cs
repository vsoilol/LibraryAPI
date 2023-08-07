using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.BusinessLayer.Services.IdentityServices;
using WebLibrary.Domain.Dtos;
using WebLibrary.Domain.Requests.Identity;

namespace WebLibrary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdentityController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public IdentityController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<AuthenticationResult>> RegisterAsync([FromBody] RegisterRequest request)
    {
        var response = await _identityService.RegisterAsync(request);
        return response.Success ? Ok(response) : BadRequest(response);
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResult>> LoginAsync([FromBody] LoginRequest request)
    {
        var response = await _identityService.LoginAsync(request);

        return response.Success ? Ok(response) : BadRequest(response);
    }
}