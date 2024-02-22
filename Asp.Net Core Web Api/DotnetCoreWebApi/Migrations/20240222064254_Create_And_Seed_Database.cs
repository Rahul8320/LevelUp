using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DotnetCoreWebApi.Migrations
{
    /// <inheritdoc />
    public partial class Create_And_Seed_Database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Sku = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsAvailable = table.Column<bool>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
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
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Active Wear - Men" },
                    { 2, "Active Wear - Women" },
                    { 3, "Mineral Water" },
                    { 4, "Publications" },
                    { 5, "Supplements" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "IsAvailable", "Name", "Price", "Sku" },
                values: new object[,]
                {
                    { 1, 1, "Our boy-cut jeans are for men and women who appreciate that skate park fashions aren't just for skaters. Made from the softest and most flexible organic cotton denim.", true, "Grunge Skater Jeans", 68m, "AWMGSJ" },
                    { 2, 1, "Our pre-shrunk organic cotton polo shirt is perfect for weekend activities, lounging around the house, and casual days at the office. With its triple-stitched sleeves and waistband, our polo has maximum durability", true, "Polo Shirt", 35m, "AWMPS" },
                    { 3, 1, "Hip at the skate park or around down, our pre-shrunk organic cotton graphic T-shirt has you covered.", true, "Skater Graphic T-Shirt", 33m, "AWMSGT" },
                    { 4, 1, "Wind and rain are no match for our organic bamboo slicker jacket for men and women. Triple stitched seams, zippered pockets, and a stay-tight hood are just a few features of our best-selling jacket.", true, "Slicker Jacket", 125m, "AWMSJ" },
                    { 5, 1, "Our thermal organic fleece jacket, is brushed on both sides for ultra softness and warmth. This medium-weight jacket is versatile all year around, and can be worn with layers for the winter season.", true, "Thermal Fleece Jacket", 60m, "AWMTFJ" },
                    { 6, 1, "Our thermal vest, made from organic bamboo with recycled plastic down filling, is a favorite of both men and women. You'll help the environment, and have a wear-easy piece for many occasions.", true, "Unisex Thermal Vest", 95m, "AWMUTV" },
                    { 7, 1, "This organic hemp jersey pullover is perfect in a pinch. Wear for casual days at the office, a game of hoops after work, or running your weekend errands.", true, "V-Neck Pullover", 65m, "AWMVNP" },
                    { 8, 1, "This medium-weight sweater, made from organic knitted cotton and bamboo, is the perfect solution to a chilly night at the campground or a misty walk on the beach.", true, "V-Neck Sweater", 65m, "AWMVNS" },
                    { 9, 1, "Our pre-shrunk organic cotton V-neck T-shirt is the ultimate in comfort and durability, with triple stitching at the collar, sleeves, and waist. So versatile you'll want one in every color!", true, "V-Neck T-Shirt", 17m, "AWMVNT" },
                    { 10, 2, "You’ll be the most environmentally conscious skier on the slopes – and the most stylish – wearing our fitted bamboo thermal ski coat, made from organic bamboo with recycled plastic down filling.", true, "Bamboo Thermal Ski Coat", 99m, "AWWBTSC" },
                    { 11, 2, "Our cross-back training tank is made from organic cotton with 10% Lycra for form and support, and a flattering feminine cut.", false, "Cross-Back Training Tank", 0m, "AWWCTT" },
                    { 12, 2, "Our boy-cut jeans are for men and women who appreciate that skate park fashions aren’t just for skaters. Made from the softest and most flexible organic cotton denim.", true, "Grunge Skater Jeans", 68m, "AWWGSJ" },
                    { 13, 2, "Wind and rain are no match for our organic bamboo slicker jacket for men and women. Triple stitched seams, zippered pockets, and a stay-tight hood are just a few features of our best-selling jacket.", true, "Slicker Jacket", 125m, "AWWSJ" },
                    { 14, 2, "Whether dancing the samba, mastering a yoga pose, or scaling the climbing wall, our stretchy dance pants, made from 80% organic cotton and 20% Lycra, are the most versatile and comfortable workout pants you’ll ever have the pleasure of wearing.", true, "Stretchy Dance Pants", 55m, "AWWSDP" },
                    { 15, 2, "This versatile tank can be worn in the gym, under a blazer, or for a day out in the sun. Made from our patented organic bamboo and cotton mix, our ultra-soft tank never stops feeling soft, even when you forget the fabric softener.", true, "Ultra-Soft Tank Top", 22m, "AWWUTT" },
                    { 16, 2, "Our thermal vest, made from organic bamboo with recycled plastic down filling, is a favorite of both men and women. You’ll help the environment, and have a wear-easy piece for many occasions.", true, "Unisex Thermal Vest", 95m, "AWWUTV" },
                    { 17, 2, "Our pre-shrunk organic cotton t-shirt, with its slightly fitted waist and elegant V-neck is designed to flatter. You’ll want one in every color!", true, "V-Next T-Shirt", 17m, "AWWVNT" },
                    { 18, 3, "An 8-ounce serving of our refreshing H+ Sport blueberry mineral water fulfills a day’s nutritional requirements for over 12 vitamins and minerals.", true, "Blueberry Mineral Water", 2.8m, "MWB" },
                    { 19, 3, "An 8-ounce serving of our refreshing H+ Sport lemon-lime mineral water fulfills a day’s nutritional requirements for over 12 vitamins and minerals", true, "Lemon-Lime Mineral Water", 2.8m, "MWLL" },
                    { 20, 3, "An 8-ounce serving of refreshing H+ Sport orange mineral water fulfills a day’s nutritional requirements for over 12 vitamins and minerals.", true, "Orange Mineral Water", 2.8m, "MWO" },
                    { 21, 3, "An 8-ounce serving of our refreshing H+ Sport peach mineral water  fulfills a day’s nutritional requirements for over 12 vitamins and minerals.", true, "Peach Mineral Water", 2.8m, "MWP" },
                    { 22, 3, "An 8-ounce serving of our refreshing H+ Sport raspberry mineral water fulfills a day’s nutritional requirements for over 12 vitamins and minerals.", true, "Raspberry Mineral Water", 2.8m, "MWR" },
                    { 23, 3, "An 8-ounce serving of our refreshing H+ Sport strawberry mineral water fulfills a day’s nutritional requirements for over 12 vitamins and minerals.", true, "Strawberry Mineral Water", 2.8m, "MWS" },
                    { 24, 4, "Henry Twill, founder and CEO of H+ Sport, teams up with celebrity nutritionist Jill Bayner, CNS, to bring you recipes and tips designed to maximize your athletic performance, while minimizing your time in the kitchen.", true, "In the Kitchen with H+ Sport", 24.99m, "PITK" },
                    { 25, 5, "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.", true, "Calcium 400 IU (150 tablets)", 9.99m, "SC400" },
                    { 26, 5, "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.", true, "Flaxseed Oil 100 mg (90 capsules)", 12.49m, "SFO100" },
                    { 27, 5, "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.", true, "Iron 65 mg (150 caplets)", 13.99m, "SI65" },
                    { 28, 5, "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.", true, "Magnesium 250 mg (100 tablets)", 12.49m, "SM250" },
                    { 29, 5, "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.", true, "Multi-Vitamin (90 capsules)", 9.99m, "SMV" },
                    { 30, 5, "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.", true, "Vitamin A 10,000 IU (125 caplets)", 11.99m, "SVA" },
                    { 31, 5, "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.", true, "Vitamin B-Complex (100 caplets)", 12.99m, "SVB" },
                    { 32, 5, "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.", true, "Vitamin C 1000 mg (100 tablets)", 9.99m, "SVC" },
                    { 33, 5, "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.", true, "Vitamin D3 1000 IU (100 tablets)", 12.49m, "SVD3" }
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
