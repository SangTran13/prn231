using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    author_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    zip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email_address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.author_id);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    pub_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    publisher_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.pub_id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_desc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    book_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pub_id = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    advance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    royalty = table.Column<double>(type: "float", nullable: false),
                    ytd_sales = table.Column<int>(type: "int", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    published_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.book_id);
                    table.ForeignKey(
                        name: "FK_Book_Publisher_pub_id",
                        column: x => x.pub_id,
                        principalTable: "Publisher",
                        principalColumn: "pub_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    middle_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    pub_id = table.Column<int>(type: "int", nullable: false),
                    hire_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_User_Publisher_pub_id",
                        column: x => x.pub_id,
                        principalTable: "Publisher",
                        principalColumn: "pub_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Role_role_id",
                        column: x => x.role_id,
                        principalTable: "Role",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthor",
                columns: table => new
                {
                    author_id = table.Column<int>(type: "int", nullable: false),
                    book_id = table.Column<int>(type: "int", nullable: false),
                    author_order = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    royality_percentage = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthor", x => new { x.author_id, x.book_id });
                    table.ForeignKey(
                        name: "FK_BookAuthor_Author_author_id",
                        column: x => x.author_id,
                        principalTable: "Author",
                        principalColumn: "author_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthor_Book_book_id",
                        column: x => x.book_id,
                        principalTable: "Book",
                        principalColumn: "book_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "author_id", "address", "city", "email_address", "first_name", "last_name", "phone", "state", "zip" },
                values: new object[,]
                {
                    { 1, "123 Main St", "New York", "johndoe@example.com", "John", "Doe", "123-456-7890", "NY", "10001" },
                    { 2, "456 Elm St", "Los Angeles", "janesmith@example.com", "Jane", "Smith", "234-567-8901", "CA", "90001" }
                });

            migrationBuilder.InsertData(
                table: "Publisher",
                columns: new[] { "pub_id", "city", "country", "publisher_name", "state" },
                values: new object[,]
                {
                    { 1, "New York", "USA", "Tech Books Publishing", "NY" },
                    { 2, "Los Angeles", "USA", "Fiction World", "CA" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "role_id", "role_desc" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "book_id", "advance", "notes", "price", "pub_id", "published_date", "royalty", "title", "type", "ytd_sales" },
                values: new object[,]
                {
                    { 1, "5000", "Bestseller", 49.990000000000002, 1, new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 10.0, "Learn C# in 30 Days", "Programming", 1000 },
                    { 2, "3000", "Highly rated", 39.990000000000002, 2, new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 8.0, "Entity Framework Core for Professionals", "Database", 500 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "user_id", "email_address", "first_name", "hire_date", "last_name", "middle_name", "password", "pub_id", "role_id", "source" },
                values: new object[,]
                {
                    { 1, "sangtn@fpt.com", "Sang", new DateTime(2025, 2, 7, 21, 28, 35, 583, DateTimeKind.Local).AddTicks(944), "Tran", "Ngoc", "123", 1, 1, "Internal" },
                    { 2, "quynx@fpt.com", "Quy", new DateTime(2025, 2, 7, 21, 28, 35, 583, DateTimeKind.Local).AddTicks(958), "Nguyen", "Xuan", "123", 2, 2, "Internal" }
                });

            migrationBuilder.InsertData(
                table: "BookAuthor",
                columns: new[] { "author_id", "book_id", "author_order", "royality_percentage" },
                values: new object[,]
                {
                    { 1, 1, "1", 50.0 },
                    { 2, 2, "1", 50.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_pub_id",
                table: "Book",
                column: "pub_id");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_book_id",
                table: "BookAuthor",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_pub_id",
                table: "User",
                column: "pub_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_role_id",
                table: "User",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthor");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Publisher");
        }
    }
}
