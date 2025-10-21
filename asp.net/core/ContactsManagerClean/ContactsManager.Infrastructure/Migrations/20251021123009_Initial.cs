using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContactsManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReceiveNewsletters = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("28dc009e-fc31-4598-a5a5-692f3e84e0cd"), "Germany" },
                    { new Guid("59c3a91a-e333-409d-8d95-c6e42ccd1680"), "Spain" },
                    { new Guid("6b820c12-ff44-4412-9e14-7941a869fb03"), "USA" },
                    { new Guid("d736fb55-4204-41da-b3da-144b4c70dd51"), "UK" },
                    { new Guid("ffd79e28-72c6-4181-a5eb-f09b02252cd6"), "Canada" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Address", "BirthDate", "CountryId", "Email", "Gender", "Name", "ReceiveNewsletters" },
                values: new object[,]
                {
                    { new Guid("14c0fb52-71fd-4a56-a4bd-42ba072611ac"), "5352 Meadow Ridge Junction", new DateTime(1975, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("59c3a91a-e333-409d-8d95-c6e42ccd1680"), "rscrymgeour5@tripadvisor.com", "Female", "Rubina", true },
                    { new Guid("19110652-d1be-45a1-8a2e-e9703d09496d"), "06224 Straubel Place", new DateTime(2007, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ffd79e28-72c6-4181-a5eb-f09b02252cd6"), "mstebbings8@godaddy.com", "Male", "Merill", true },
                    { new Guid("5eebda1a-3bba-47e8-a030-5878484a1d01"), "87 Clemons Road", new DateTime(1989, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6b820c12-ff44-4412-9e14-7941a869fb03"), "bphilipeau1@cnet.com", "Male", "Blinnie", true },
                    { new Guid("65ba3b4f-0f20-4251-ad75-b38cc01d0769"), "726 Eastwood Center", new DateTime(2009, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ffd79e28-72c6-4181-a5eb-f09b02252cd6"), "jfunnell9@e-recht24.de", "Other", "Jasen", true },
                    { new Guid("7f711f5d-de40-478d-8dc1-0357d69fe70c"), "0205 Manley Court", new DateTime(2007, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6b820c12-ff44-4412-9e14-7941a869fb03"), "fjewess0@mozilla.org", "Male", "Franklyn", false },
                    { new Guid("a78c2bbe-4037-4d9b-8db0-6ea251ece14d"), "99 Arrowood Crossing", new DateTime(2000, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("28dc009e-fc31-4598-a5a5-692f3e84e0cd"), "vshowering7@themeforest.net", "Female", "Veronike", true },
                    { new Guid("b2bb228d-e31f-47b4-8fe1-2349a09c005a"), "11213 Pepper Wood Parkway", new DateTime(1987, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d736fb55-4204-41da-b3da-144b4c70dd51"), "sdranfield2@weather.com", "Male", "Stanfield", false },
                    { new Guid("ea6273dc-0c9a-4e52-b65e-9244dfc7c60b"), "35223 Roxbury Center", new DateTime(1997, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d736fb55-4204-41da-b3da-144b4c70dd51"), "klilleycrop3@delicious.com", "Male", "Krisha", true },
                    { new Guid("fa1dd467-f430-4be3-9dc4-ed23a6b4b16e"), "22 Bowman Pass", new DateTime(2007, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("59c3a91a-e333-409d-8d95-c6e42ccd1680"), "sseamer4@prnewswire.com", "Male", "Sebastien", true },
                    { new Guid("ff6db138-c462-48ad-86c4-789aecae8903"), "13480 Browning Way", new DateTime(1999, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("28dc009e-fc31-4598-a5a5-692f3e84e0cd"), "gtempleton6@about.me", "Female", "Gerianna", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_CountryId",
                table: "People",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
