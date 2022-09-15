using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Users.Infrastucture.Storeage.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 46, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 46, nullable: false),
                    StreetName = table.Column<string>(type: "TEXT", maxLength: 95, nullable: false),
                    HouseNumber = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    ApartmentNumber = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    PostalCode = table.Column<string>(type: "TEXT", maxLength: 12, nullable: false),
                    Town = table.Column<string>(type: "TEXT", maxLength: 35, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    DateOfBirth = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
