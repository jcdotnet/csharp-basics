using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class GetPeople_StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_GetPeople = @"
            CREATE PROCEDURE [dbo].[GetPeople] AS
            BEGIN
                SELECT * FROM [dbo].[People]
            END";
            migrationBuilder.Sql(sp_GetPeople);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[GetPeople]");
        }
    }
}
