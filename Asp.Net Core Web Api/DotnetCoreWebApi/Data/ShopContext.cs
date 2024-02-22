using DotnetCoreWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetCoreWebApi.Data;

public class ShopContext(DbContextOptions<ShopContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasMany(c => c.Products).WithOne(a => a.Category).HasForeignKey(a => a.CategoryId);

        SeedCategories(modelBuilder);
        SeedProducts(modelBuilder);
    }

    public static void SeedCategories(ModelBuilder builder)
    {
        builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Active Wear - Men" },
            new Category { Id = 2, Name = "Active Wear - Women" },
            new Category { Id = 3, Name = "Mineral Water" },
            new Category { Id = 4, Name = "Publications" },
            new Category { Id = 5, Name = "Supplements" }
        );
    }

    public static void SeedProducts(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, CategoryId = 1, Name = "Grunge Skater Jeans", Description = "Our boy-cut jeans are for men and women who appreciate that skate park fashions aren't just for skaters. Made from the softest and most flexible organic cotton denim.", Sku = "AWMGSJ", Price = 68, IsAvailable = true },
            new Product { Id = 2, CategoryId = 1, Name = "Polo Shirt", Description = "Our pre-shrunk organic cotton polo shirt is perfect for weekend activities, lounging around the house, and casual days at the office. With its triple-stitched sleeves and waistband, our polo has maximum durability", Sku = "AWMPS", Price = 35, IsAvailable = true },
            new Product { Id = 3, CategoryId = 1, Name = "Skater Graphic T-Shirt", Description = "Hip at the skate park or around down, our pre-shrunk organic cotton graphic T-shirt has you covered.", Sku = "AWMSGT", Price = 33, IsAvailable = true },
            new Product { Id = 4, CategoryId = 1, Name = "Slicker Jacket", Description = "Wind and rain are no match for our organic bamboo slicker jacket for men and women. Triple stitched seams, zippered pockets, and a stay-tight hood are just a few features of our best-selling jacket.", Sku = "AWMSJ", Price = 125, IsAvailable = true },
            new Product { Id = 5, CategoryId = 1, Name = "Thermal Fleece Jacket", Description = "Our thermal organic fleece jacket, is brushed on both sides for ultra softness and warmth. This medium-weight jacket is versatile all year around, and can be worn with layers for the winter season.", Sku = "AWMTFJ", Price = 60, IsAvailable = true },
            new Product { Id = 6, CategoryId = 1, Name = "Unisex Thermal Vest", Description = "Our thermal vest, made from organic bamboo with recycled plastic down filling, is a favorite of both men and women. You'll help the environment, and have a wear-easy piece for many occasions.", Sku = "AWMUTV", Price = 95, IsAvailable = true },
            new Product { Id = 7, CategoryId = 1, Name = "V-Neck Pullover", Description = "This organic hemp jersey pullover is perfect in a pinch. Wear for casual days at the office, a game of hoops after work, or running your weekend errands.", Sku = "AWMVNP", Price = 65, IsAvailable = true },
            new Product { Id = 8, CategoryId = 1, Name = "V-Neck Sweater", Description = "This medium-weight sweater, made from organic knitted cotton and bamboo, is the perfect solution to a chilly night at the campground or a misty walk on the beach.", Sku = "AWMVNS", Price = 65, IsAvailable = true },
            new Product { Id = 9, CategoryId = 1, Name = "V-Neck T-Shirt", Description = "Our pre-shrunk organic cotton V-neck T-shirt is the ultimate in comfort and durability, with triple stitching at the collar, sleeves, and waist. So versatile you'll want one in every color!", Sku = "AWMVNT", Price = 17, IsAvailable = true },
            new Product { Id = 10, CategoryId = 2, Name = "Bamboo Thermal Ski Coat", Description = "You’ll be the most environmentally conscious skier on the slopes – and the most stylish – wearing our fitted bamboo thermal ski coat, made from organic bamboo with recycled plastic down filling.", Sku = "AWWBTSC", Price = 99, IsAvailable = true },
            new Product { Id = 11, CategoryId = 2, Name = "Cross-Back Training Tank", Description = "Our cross-back training tank is made from organic cotton with 10% Lycra for form and support, and a flattering feminine cut.", Sku = "AWWCTT", Price = 0, IsAvailable = false },
            new Product { Id = 12, CategoryId = 2, Name = "Grunge Skater Jeans", Description = "Our boy-cut jeans are for men and women who appreciate that skate park fashions aren’t just for skaters. Made from the softest and most flexible organic cotton denim.", Sku = "AWWGSJ", Price = 68, IsAvailable = true },
            new Product { Id = 13, CategoryId = 2, Name = "Slicker Jacket", Description = "Wind and rain are no match for our organic bamboo slicker jacket for men and women. Triple stitched seams, zippered pockets, and a stay-tight hood are just a few features of our best-selling jacket.", Sku = "AWWSJ", Price = 125, IsAvailable = true },
            new Product { Id = 14, CategoryId = 2, Name = "Stretchy Dance Pants", Description = "Whether dancing the samba, mastering a yoga pose, or scaling the climbing wall, our stretchy dance pants, made from 80% organic cotton and 20% Lycra, are the most versatile and comfortable workout pants you’ll ever have the pleasure of wearing.", Sku = "AWWSDP", Price = 55, IsAvailable = true },
            new Product { Id = 15, CategoryId = 2, Name = "Ultra-Soft Tank Top", Description = "This versatile tank can be worn in the gym, under a blazer, or for a day out in the sun. Made from our patented organic bamboo and cotton mix, our ultra-soft tank never stops feeling soft, even when you forget the fabric softener.", Sku = "AWWUTT", Price = 22, IsAvailable = true },
            new Product { Id = 16, CategoryId = 2, Name = "Unisex Thermal Vest", Description = "Our thermal vest, made from organic bamboo with recycled plastic down filling, is a favorite of both men and women. You’ll help the environment, and have a wear-easy piece for many occasions.", Sku = "AWWUTV", Price = 95, IsAvailable = true },
            new Product { Id = 17, CategoryId = 2, Name = "V-Next T-Shirt", Description = "Our pre-shrunk organic cotton t-shirt, with its slightly fitted waist and elegant V-neck is designed to flatter. You’ll want one in every color!", Sku = "AWWVNT", Price = 17, IsAvailable = true },
            new Product { Id = 18, CategoryId = 3, Name = "Blueberry Mineral Water", Description = "An 8-ounce serving of our refreshing H+ Sport blueberry mineral water fulfills a day’s nutritional requirements for over 12 vitamins and minerals.", Sku = "MWB", Price = 2.8M, IsAvailable = true },
            new Product { Id = 19, CategoryId = 3, Name = "Lemon-Lime Mineral Water", Description = "An 8-ounce serving of our refreshing H+ Sport lemon-lime mineral water fulfills a day’s nutritional requirements for over 12 vitamins and minerals", Sku = "MWLL", Price = 2.8M, IsAvailable = true },
            new Product { Id = 20, CategoryId = 3, Name = "Orange Mineral Water", Description = "An 8-ounce serving of refreshing H+ Sport orange mineral water fulfills a day’s nutritional requirements for over 12 vitamins and minerals.", Sku = "MWO", Price = 2.8M, IsAvailable = true },
            new Product { Id = 21, CategoryId = 3, Name = "Peach Mineral Water", Description = "An 8-ounce serving of our refreshing H+ Sport peach mineral water  fulfills a day’s nutritional requirements for over 12 vitamins and minerals.", Sku = "MWP", Price = 2.8M, IsAvailable = true },
            new Product { Id = 22, CategoryId = 3, Name = "Raspberry Mineral Water", Description = "An 8-ounce serving of our refreshing H+ Sport raspberry mineral water fulfills a day’s nutritional requirements for over 12 vitamins and minerals.", Sku = "MWR", Price = 2.8M, IsAvailable = true },
            new Product { Id = 23, CategoryId = 3, Name = "Strawberry Mineral Water", Description = "An 8-ounce serving of our refreshing H+ Sport strawberry mineral water fulfills a day’s nutritional requirements for over 12 vitamins and minerals.", Sku = "MWS", Price = 2.8M, IsAvailable = true },
            new Product { Id = 24, CategoryId = 4, Name = "In the Kitchen with H+ Sport", Description = "Henry Twill, founder and CEO of H+ Sport, teams up with celebrity nutritionist Jill Bayner, CNS, to bring you recipes and tips designed to maximize your athletic performance, while minimizing your time in the kitchen.", Sku = "PITK", Price = 24.99M, IsAvailable = true },
            new Product { Id = 25, CategoryId = 5, Name = "Calcium 400 IU (150 tablets)", Description = "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.", Sku = "SC400", Price = 9.99M, IsAvailable = true },
            new Product { Id = 26, CategoryId = 5, Name = "Flaxseed Oil 100 mg (90 capsules)", Description = "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.", Sku = "SFO100", Price = 12.49M, IsAvailable = true },
            new Product { Id = 27, CategoryId = 5, Name = "Iron 65 mg (150 caplets)", Description = "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.", Sku = "SI65", Price = 13.99M, IsAvailable = true },
            new Product { Id = 28, CategoryId = 5, Name = "Magnesium 250 mg (100 tablets)", Description = "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.", Sku = "SM250", Price = 12.49M, IsAvailable = true },
            new Product { Id = 29, CategoryId = 5, Name = "Multi-Vitamin (90 capsules)", Description = "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.", Sku = "SMV", Price = 9.99M, IsAvailable = true },
            new Product { Id = 30, CategoryId = 5, Name = "Vitamin A 10,000 IU (125 caplets)", Description = "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.", Sku = "SVA", Price = 11.99M, IsAvailable = true },
            new Product { Id = 31, CategoryId = 5, Name = "Vitamin B-Complex (100 caplets)", Description = "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.", Sku = "SVB", Price = 12.99M, IsAvailable = true },
            new Product { Id = 32, CategoryId = 5, Name = "Vitamin C 1000 mg (100 tablets)", Description = "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.", Sku = "SVC", Price = 9.99M, IsAvailable = true },
            new Product { Id = 33, CategoryId = 5, Name = "Vitamin D3 1000 IU (100 tablets)", Description = "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.", Sku = "SVD3", Price = 12.49M, IsAvailable = true }
        );
    }
}
