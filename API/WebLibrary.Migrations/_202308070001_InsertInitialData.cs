using FluentMigrator;
using WebLibrary.Domain.Entities;

namespace WebLibrary.Migrations;

[Migration(202308070001)]
public class _202308070001_InsertInitialData : Migration
{
    private readonly List<Book> _books = new()
    {
        new Book
        {
            Id = new Guid("e03a8734-4b07-4b7c-bd0c-2806055c94e3"),
            Isbn = "1933988673",
            Title = "Unlocking Android",
            Genre = "Information technology",
            Description =
                "Unlocking Android: A Developer's Guide provides concise, hands-on instruction for the Android operating system and development tools. This book teaches important architectural concepts in a straightforward writing style and builds on this with practical and useful examples throughout.",
            Author = "W. Frank Ableson",
            BorrowedTime = new DateTime(2023, 7, 1),
            ReturnDueTime = new DateTime(2023, 8, 10)
        },
        new Book
        {
            Id = new Guid("38ffff73-8396-4fd8-a55a-eeaa86aba3db"),
            Isbn = "1933988711",
            Title = "Brownfield Application Development in .NET",
            Genre = "Information technology",
            Description =
                "Brownfield Application Development in .Net shows you how to approach legacy applications with the state-of-the-art concepts, patterns, and tools you've learned to apply to new projects. Using an existing application as an example, this book guides you in applying the techniques and best practices you need to make it more maintainable and receptive to change.",
            Author = "Kyle Baley",
            BorrowedTime = null,
            ReturnDueTime = null
        },
        new Book
        {
            Id = new Guid("a165081f-5579-45e9-97d8-c60d4efd9383"),
            Isbn = "1935182463",
            Title = "ASP.NET 4.0 in Practice",
            Genre = "Information technology",
            Description =
                "ASP.NET 4.0 in Practice contains real world techniques from well-known professionals who have been using ASP.NET since the first previews.",
            Author = "Daniele Bochicchio",
            BorrowedTime = new DateTime(2023, 7, 11),
            ReturnDueTime = new DateTime(2023, 8, 20)
        }
    };

    private readonly User _user = new()
    {
        Id = new Guid("23fa02ee-ca41-4955-8fc1-dda7132a2c59"),
        FirstName = "Test",
        LastName = "Test",
        MiddleName = "Testovich",
        Login = "test_login",
        PasswordHash = "$MYHASH$V1$10000$LJ6mpiEhYgaF2RIIsE9DWOS3LjhzLNtWlSMVfEJ6120vF7i0"
    };

    public override void Up()
    {
        foreach (var book in _books)
        {
            Insert.IntoTable("Book").Row(book);
        }

        Insert.IntoTable("User").Row(_user);
    }

    public override void Down()
    {
        foreach (var book in _books)
        {
            Delete.FromTable("Book").Row(book);
        }

        Delete.FromTable("User").Row(_user);
    }
}