using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.BusinessLayer.Services.UserServices;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Requests.User;

namespace WebLibrary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAllUsersAsync()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserByIdAsync([FromRoute] Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return user is not null ? Ok(user) : NoContent();
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult> UpdateUserAsync([FromBody] UpdateUserRequest request)
    {
        var isUpdate = await _userService.UpdateUserAsync(request);
        return isUpdate ? Ok() : BadRequest();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUserAsync([FromRoute] Guid id)
    {
        var isDelete = await _userService.DeleteUserAsync(id);
        return isDelete ? Ok() : BadRequest();
    }
}