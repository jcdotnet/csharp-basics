using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class InsertPerson_StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_InsertPerson = @"CREATE PROCEDURE [dbo].[InsertPerson] (
@Id uniqueidentifier, @Name nvarchar(40), 
@Email nvarchar(40), @Address nvarchar(200), 
@BirthDate datetime2(7), @Gender nvarchar(10), 
@CountryId uniqueidentifier, @ReceiveNewsletters bit) AS
BEGIN
    INSERT INTO [dbo].[People](Id, Name, Email, Address, BirthDate, Gender, CountryId, ReceiveNewsLetters) 
    VALUES (@Id, @Name, @Email, @Address, @BirthDate, @Gender, @CountryId, @ReceiveNewsLetters)
END";
            migrationBuilder.Sql(sp_InsertPerson);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[InsertPerson]");
        }
    }
}
