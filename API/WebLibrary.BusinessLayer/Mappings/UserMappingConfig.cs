using Mapster;
using WebLibrary.Domain.Dtos;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Requests.Identity;
using WebLibrary.Domain.Requests.User;

namespace WebLibrary.BusinessLayer.Mappings;

internal class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserDto>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<RegisterRequest, User>()
            .RequireDestinationMemberSource(true)
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.PasswordHash);

        config.NewConfig<UpdateUserRequest, User>()
            .RequireDestinationMemberSource(true)
            .Ignore(dest => dest.Login)
            .Ignore(dest => dest.PasswordHash);
    }
}