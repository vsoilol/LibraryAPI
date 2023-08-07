using Mapster;
using WebLibrary.Domain.Dtos;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Requests.Book;

namespace WebLibrary.BusinessLayer.Mappings;

internal class BookMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Book, BookDto>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<CreateBookRequest, Book>()
            .RequireDestinationMemberSource(true)
            .Ignore(dest => dest.Id);
        
        config.NewConfig<UpdateBookRequest, Book>()
            .RequireDestinationMemberSource(true);
    }
}