using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShopApi2024.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreationTime", "DeleteTime", "Description", "ImageName", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 27, 15, 39, 47, 160, DateTimeKind.Local).AddTicks(1015), null, "ratione", "e1878e9a-a9c0-423e-bc9f-f4c43129ea78.webp", "Beauty" },
                    { 2, new DateTime(2024, 9, 27, 15, 39, 47, 161, DateTimeKind.Local).AddTicks(1090), null, "Sit tempora officia.\nAliquid et blanditiis quaerat explicabo expedita.\nUnde et culpa.\nEst quod nesciunt unde illo excepturi id ea.\nAssumenda sapiente dolor eligendi dolorem ratione maxime dolorum.\nQuo expedita soluta.", "f507920e-1ec6-4c91-a837-91becdcf0bae.webp", "Automotive" },
                    { 3, new DateTime(2024, 9, 27, 15, 39, 47, 161, DateTimeKind.Local).AddTicks(8427), null, "Voluptatibus aut quia et ut ut iste consequatur voluptate.\nIncidunt magnam saepe unde est praesentium dignissimos consequuntur.\nConsequatur debitis voluptatem.", "8dde2a6e-8c61-49b6-98c4-4805a6940aa6.webp", "Movies" },
                    { 4, new DateTime(2024, 9, 27, 15, 39, 47, 162, DateTimeKind.Local).AddTicks(7283), null, "Sint sed dolorem et magnam quidem. Ex adipisci debitis sed saepe autem accusamus ipsam. Inventore sequi quam blanditiis error officia nisi rerum. Dolorem voluptas eos qui ratione voluptas et velit facere ut. Debitis ducimus aperiam qui provident excepturi quos.", "822b5b70-dad8-4795-bcaf-06aedaba0d7d.webp", "Clothing" },
                    { 5, new DateTime(2024, 9, 27, 15, 39, 47, 163, DateTimeKind.Local).AddTicks(6463), null, "ut", "ea3848f2-d56e-4a3a-a958-210249ab9030.webp", "Movies" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreationTime", "DeleteTime", "Description", "Discount", "ImageName", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 9, 27, 15, 39, 47, 160, DateTimeKind.Local).AddTicks(1360), null, "The Football Is Good For Training And Recreational Purposes", 1.0, "bb2a0249-6fbe-4bcd-8f7a-cd003c170845", "Generic Wooden Tuna", 126.60m },
                    { 2, 1, new DateTime(2024, 9, 27, 15, 39, 47, 160, DateTimeKind.Local).AddTicks(1549), null, "The Football Is Good For Training And Recreational Purposes", 20.0, "7208ea4f-941e-4409-ac31-05b8d69a83d8", "Incredible Rubber Tuna", 33.84m },
                    { 3, 1, new DateTime(2024, 9, 27, 15, 39, 47, 160, DateTimeKind.Local).AddTicks(1580), null, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", 38.0, "a36b4678-00f1-47cc-ad88-c12861c14c48", "Handcrafted Cotton Bacon", 633.61m },
                    { 4, 1, new DateTime(2024, 9, 27, 15, 39, 47, 160, DateTimeKind.Local).AddTicks(1605), null, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", 1.0, "7ff28a1e-41e1-4f8d-a90c-7402ce46f63a", "Gorgeous Fresh Chicken", 303.56m },
                    { 5, 1, new DateTime(2024, 9, 27, 15, 39, 47, 160, DateTimeKind.Local).AddTicks(1628), null, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", 6.0, "f846e456-e1bf-4468-afd1-689da2c6762b", "Awesome Granite Table", 343.00m },
                    { 6, 2, new DateTime(2024, 9, 27, 15, 39, 47, 161, DateTimeKind.Local).AddTicks(1468), null, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", 9.0, "74d8bf37-0b93-4bb3-bf88-9a49cd0b49ef", "Practical Frozen Chair", 217.54m },
                    { 7, 2, new DateTime(2024, 9, 27, 15, 39, 47, 161, DateTimeKind.Local).AddTicks(1536), null, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", 2.0, "aa726b2a-42d4-4721-a469-6ab84cb010ea", "Licensed Fresh Computer", 12.75m },
                    { 8, 2, new DateTime(2024, 9, 27, 15, 39, 47, 161, DateTimeKind.Local).AddTicks(1600), null, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", 48.0, "48eb12a6-d55a-48a1-88ba-3c4eefd51d94", "Intelligent Metal Chips", 949.37m },
                    { 9, 2, new DateTime(2024, 9, 27, 15, 39, 47, 161, DateTimeKind.Local).AddTicks(1667), null, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", 5.0, "4045ed59-69c1-479f-ac2f-a0378bbe7611", "Small Rubber Computer", 126.97m },
                    { 10, 2, new DateTime(2024, 9, 27, 15, 39, 47, 161, DateTimeKind.Local).AddTicks(1729), null, "The Football Is Good For Training And Recreational Purposes", 10.0, "caefc71a-b58d-4a1a-abc8-10d9cd25374a", "Fantastic Metal Fish", 571.07m },
                    { 11, 3, new DateTime(2024, 9, 27, 15, 39, 47, 161, DateTimeKind.Local).AddTicks(8741), null, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", 28.0, "0b46390f-eee7-4396-b857-a2cccc1bde1f", "Unbranded Steel Ball", 317.71m },
                    { 12, 3, new DateTime(2024, 9, 27, 15, 39, 47, 161, DateTimeKind.Local).AddTicks(8872), null, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", 32.0, "b748573b-b49e-4d20-b68f-5d7462725a24", "Awesome Rubber Chicken", 828.12m },
                    { 13, 3, new DateTime(2024, 9, 27, 15, 39, 47, 161, DateTimeKind.Local).AddTicks(8954), null, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", 11.0, "0f98e287-1d60-4942-b560-35a49ce92873", "Handmade Granite Car", 528.87m },
                    { 14, 3, new DateTime(2024, 9, 27, 15, 39, 47, 161, DateTimeKind.Local).AddTicks(9072), null, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", 44.0, "beb9f01a-951e-4ef6-9dda-b3f27df75ce7", "Incredible Concrete Fish", 79.99m },
                    { 15, 3, new DateTime(2024, 9, 27, 15, 39, 47, 161, DateTimeKind.Local).AddTicks(9177), null, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", 47.0, "f44f8fd6-292d-471c-afcd-c9202fa7cf0d", "Gorgeous Frozen Computer", 629.94m },
                    { 16, 4, new DateTime(2024, 9, 27, 15, 39, 47, 162, DateTimeKind.Local).AddTicks(7604), null, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", 11.0, "07f4a829-5a68-411e-a9bf-632744923fdd", "Generic Wooden Bacon", 612.43m },
                    { 17, 4, new DateTime(2024, 9, 27, 15, 39, 47, 162, DateTimeKind.Local).AddTicks(7735), null, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", 2.0, "84ba4c12-40de-4e78-9699-c073bc5f7fcd", "Refined Rubber Computer", 679.36m },
                    { 18, 4, new DateTime(2024, 9, 27, 15, 39, 47, 162, DateTimeKind.Local).AddTicks(7851), null, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", 17.0, "6b73dfa2-6816-4805-b641-c7370b1c01a9", "Handcrafted Plastic Bacon", 716.43m },
                    { 19, 4, new DateTime(2024, 9, 27, 15, 39, 47, 162, DateTimeKind.Local).AddTicks(7969), null, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", 40.0, "e60b94b8-6361-4fad-a73f-9e6903c4fc43", "Sleek Fresh Hat", 932.34m },
                    { 20, 4, new DateTime(2024, 9, 27, 15, 39, 47, 162, DateTimeKind.Local).AddTicks(8083), null, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", 36.0, "afce37b3-9935-4187-a545-9d27126bc09d", "Fantastic Plastic Gloves", 22.56m },
                    { 21, 5, new DateTime(2024, 9, 27, 15, 39, 47, 163, DateTimeKind.Local).AddTicks(6827), null, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", 29.0, "463d73b0-08ca-4d11-afe2-3697ff41d77d", "Unbranded Granite Shirt", 846.66m },
                    { 22, 5, new DateTime(2024, 9, 27, 15, 39, 47, 163, DateTimeKind.Local).AddTicks(7038), null, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", 2.0, "1de4462c-e96e-4add-b36b-2f828ee9e0ee", "Licensed Wooden Computer", 920.43m },
                    { 23, 5, new DateTime(2024, 9, 27, 15, 39, 47, 163, DateTimeKind.Local).AddTicks(7075), null, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", 22.0, "085a7dce-75a1-4c53-93c1-afab06fc6ddc", "Small Granite Bacon", 438.97m },
                    { 24, 5, new DateTime(2024, 9, 27, 15, 39, 47, 163, DateTimeKind.Local).AddTicks(7112), null, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", 48.0, "b2107a5a-d46f-466c-8aa5-a066aa0121d0", "Awesome Rubber Hat", 889.78m },
                    { 25, 5, new DateTime(2024, 9, 27, 15, 39, 47, 163, DateTimeKind.Local).AddTicks(7150), null, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", 49.0, "dd216f78-23c2-47ef-bb7c-1b13314a3475", "Refined Wooden Salad", 401.34m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
