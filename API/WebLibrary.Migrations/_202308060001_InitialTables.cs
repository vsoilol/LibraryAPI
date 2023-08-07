using FluentMigrator;

namespace WebLibrary.Migrations;

[Migration(202308060001)]
public class _202308060001_InitialTables : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("User")
            .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("FirstName").AsString(50).NotNullable()
            .WithColumn("LastName").AsString(50).NotNullable()
            .WithColumn("MiddleName").AsString(50).NotNullable()
            .WithColumn("Login").AsString(20).NotNullable()
            .WithColumn("PasswordHash").AsString(100).NotNullable();

        Create.Table("Book")
            .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("Isbn").AsString(40).NotNullable()
            .WithColumn("Title").AsString(50).NotNullable()
            .WithColumn("Genre").AsString(50).NotNullable()
            .WithColumn("Description").AsString(200).NotNullable()
            .WithColumn("Author").AsString(50).NotNullable()
            .WithColumn("BorrowedTime").AsDateTime2().Nullable()
            .WithColumn("ReturnDueTime").AsDateTime2().Nullable();
        
        Create.Index("IX_Book_Isbn")
            .OnTable("Book")
            .WithOptions().Unique()
            .OnColumn("Isbn").Ascending();
    }
}