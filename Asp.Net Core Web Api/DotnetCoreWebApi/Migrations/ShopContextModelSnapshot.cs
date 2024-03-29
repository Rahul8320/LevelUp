﻿// <auto-generated />
using DotnetCoreWebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DotnetCoreWebApi.Migrations
{
    [DbContext(typeof(ShopContext))]
    partial class ShopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("DotnetCoreWebApi.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Active Wear - Men"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Active Wear - Women"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Mineral Water"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Publications"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Supplements"
                        });
                });

            modelBuilder.Entity("DotnetCoreWebApi.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<string>("Sku")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "Our boy-cut jeans are for men and women who appreciate that skate park fashions aren't just for skaters. Made from the softest and most flexible organic cotton denim.",
                            IsAvailable = true,
                            Name = "Grunge Skater Jeans",
                            Price = 68m,
                            Sku = "AWMGSJ"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            Description = "Our pre-shrunk organic cotton polo shirt is perfect for weekend activities, lounging around the house, and casual days at the office. With its triple-stitched sleeves and waistband, our polo has maximum durability",
                            IsAvailable = true,
                            Name = "Polo Shirt",
                            Price = 35m,
                            Sku = "AWMPS"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 1,
                            Description = "Hip at the skate park or around down, our pre-shrunk organic cotton graphic T-shirt has you covered.",
                            IsAvailable = true,
                            Name = "Skater Graphic T-Shirt",
                            Price = 33m,
                            Sku = "AWMSGT"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 1,
                            Description = "Wind and rain are no match for our organic bamboo slicker jacket for men and women. Triple stitched seams, zippered pockets, and a stay-tight hood are just a few features of our best-selling jacket.",
                            IsAvailable = true,
                            Name = "Slicker Jacket",
                            Price = 125m,
                            Sku = "AWMSJ"
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 1,
                            Description = "Our thermal organic fleece jacket, is brushed on both sides for ultra softness and warmth. This medium-weight jacket is versatile all year around, and can be worn with layers for the winter season.",
                            IsAvailable = true,
                            Name = "Thermal Fleece Jacket",
                            Price = 60m,
                            Sku = "AWMTFJ"
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 1,
                            Description = "Our thermal vest, made from organic bamboo with recycled plastic down filling, is a favorite of both men and women. You'll help the environment, and have a wear-easy piece for many occasions.",
                            IsAvailable = true,
                            Name = "Unisex Thermal Vest",
                            Price = 95m,
                            Sku = "AWMUTV"
                        },
                        new
                        {
                            Id = 7,
                            CategoryId = 1,
                            Description = "This organic hemp jersey pullover is perfect in a pinch. Wear for casual days at the office, a game of hoops after work, or running your weekend errands.",
                            IsAvailable = true,
                            Name = "V-Neck Pullover",
                            Price = 65m,
                            Sku = "AWMVNP"
                        },
                        new
                        {
                            Id = 8,
                            CategoryId = 1,
                            Description = "This medium-weight sweater, made from organic knitted cotton and bamboo, is the perfect solution to a chilly night at the campground or a misty walk on the beach.",
                            IsAvailable = true,
                            Name = "V-Neck Sweater",
                            Price = 65m,
                            Sku = "AWMVNS"
                        },
                        new
                        {
                            Id = 9,
                            CategoryId = 1,
                            Description = "Our pre-shrunk organic cotton V-neck T-shirt is the ultimate in comfort and durability, with triple stitching at the collar, sleeves, and waist. So versatile you'll want one in every color!",
                            IsAvailable = true,
                            Name = "V-Neck T-Shirt",
                            Price = 17m,
                            Sku = "AWMVNT"
                        },
                        new
                        {
                            Id = 10,
                            CategoryId = 2,
                            Description = "You’ll be the most environmentally conscious skier on the slopes – and the most stylish – wearing our fitted bamboo thermal ski coat, made from organic bamboo with recycled plastic down filling.",
                            IsAvailable = true,
                            Name = "Bamboo Thermal Ski Coat",
                            Price = 99m,
                            Sku = "AWWBTSC"
                        },
                        new
                        {
                            Id = 11,
                            CategoryId = 2,
                            Description = "Our cross-back training tank is made from organic cotton with 10% Lycra for form and support, and a flattering feminine cut.",
                            IsAvailable = false,
                            Name = "Cross-Back Training Tank",
                            Price = 0m,
                            Sku = "AWWCTT"
                        },
                        new
                        {
                            Id = 12,
                            CategoryId = 2,
                            Description = "Our boy-cut jeans are for men and women who appreciate that skate park fashions aren’t just for skaters. Made from the softest and most flexible organic cotton denim.",
                            IsAvailable = true,
                            Name = "Grunge Skater Jeans",
                            Price = 68m,
                            Sku = "AWWGSJ"
                        },
                        new
                        {
                            Id = 13,
                            CategoryId = 2,
                            Description = "Wind and rain are no match for our organic bamboo slicker jacket for men and women. Triple stitched seams, zippered pockets, and a stay-tight hood are just a few features of our best-selling jacket.",
                            IsAvailable = true,
                            Name = "Slicker Jacket",
                            Price = 125m,
                            Sku = "AWWSJ"
                        },
                        new
                        {
                            Id = 14,
                            CategoryId = 2,
                            Description = "Whether dancing the samba, mastering a yoga pose, or scaling the climbing wall, our stretchy dance pants, made from 80% organic cotton and 20% Lycra, are the most versatile and comfortable workout pants you’ll ever have the pleasure of wearing.",
                            IsAvailable = true,
                            Name = "Stretchy Dance Pants",
                            Price = 55m,
                            Sku = "AWWSDP"
                        },
                        new
                        {
                            Id = 15,
                            CategoryId = 2,
                            Description = "This versatile tank can be worn in the gym, under a blazer, or for a day out in the sun. Made from our patented organic bamboo and cotton mix, our ultra-soft tank never stops feeling soft, even when you forget the fabric softener.",
                            IsAvailable = true,
                            Name = "Ultra-Soft Tank Top",
                            Price = 22m,
                            Sku = "AWWUTT"
                        },
                        new
                        {
                            Id = 16,
                            CategoryId = 2,
                            Description = "Our thermal vest, made from organic bamboo with recycled plastic down filling, is a favorite of both men and women. You’ll help the environment, and have a wear-easy piece for many occasions.",
                            IsAvailable = true,
                            Name = "Unisex Thermal Vest",
                            Price = 95m,
                            Sku = "AWWUTV"
                        },
                        new
                        {
                            Id = 17,
                            CategoryId = 2,
                            Description = "Our pre-shrunk organic cotton t-shirt, with its slightly fitted waist and elegant V-neck is designed to flatter. You’ll want one in every color!",
                            IsAvailable = true,
                            Name = "V-Next T-Shirt",
                            Price = 17m,
                            Sku = "AWWVNT"
                        },
                        new
                        {
                            Id = 18,
                            CategoryId = 3,
                            Description = "An 8-ounce serving of our refreshing H+ Sport blueberry mineral water fulfills a day’s nutritional requirements for over 12 vitamins and minerals.",
                            IsAvailable = true,
                            Name = "Blueberry Mineral Water",
                            Price = 2.8m,
                            Sku = "MWB"
                        },
                        new
                        {
                            Id = 19,
                            CategoryId = 3,
                            Description = "An 8-ounce serving of our refreshing H+ Sport lemon-lime mineral water fulfills a day’s nutritional requirements for over 12 vitamins and minerals",
                            IsAvailable = true,
                            Name = "Lemon-Lime Mineral Water",
                            Price = 2.8m,
                            Sku = "MWLL"
                        },
                        new
                        {
                            Id = 20,
                            CategoryId = 3,
                            Description = "An 8-ounce serving of refreshing H+ Sport orange mineral water fulfills a day’s nutritional requirements for over 12 vitamins and minerals.",
                            IsAvailable = true,
                            Name = "Orange Mineral Water",
                            Price = 2.8m,
                            Sku = "MWO"
                        },
                        new
                        {
                            Id = 21,
                            CategoryId = 3,
                            Description = "An 8-ounce serving of our refreshing H+ Sport peach mineral water  fulfills a day’s nutritional requirements for over 12 vitamins and minerals.",
                            IsAvailable = true,
                            Name = "Peach Mineral Water",
                            Price = 2.8m,
                            Sku = "MWP"
                        },
                        new
                        {
                            Id = 22,
                            CategoryId = 3,
                            Description = "An 8-ounce serving of our refreshing H+ Sport raspberry mineral water fulfills a day’s nutritional requirements for over 12 vitamins and minerals.",
                            IsAvailable = true,
                            Name = "Raspberry Mineral Water",
                            Price = 2.8m,
                            Sku = "MWR"
                        },
                        new
                        {
                            Id = 23,
                            CategoryId = 3,
                            Description = "An 8-ounce serving of our refreshing H+ Sport strawberry mineral water fulfills a day’s nutritional requirements for over 12 vitamins and minerals.",
                            IsAvailable = true,
                            Name = "Strawberry Mineral Water",
                            Price = 2.8m,
                            Sku = "MWS"
                        },
                        new
                        {
                            Id = 24,
                            CategoryId = 4,
                            Description = "Henry Twill, founder and CEO of H+ Sport, teams up with celebrity nutritionist Jill Bayner, CNS, to bring you recipes and tips designed to maximize your athletic performance, while minimizing your time in the kitchen.",
                            IsAvailable = true,
                            Name = "In the Kitchen with H+ Sport",
                            Price = 24.99m,
                            Sku = "PITK"
                        },
                        new
                        {
                            Id = 25,
                            CategoryId = 5,
                            Description = "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.",
                            IsAvailable = true,
                            Name = "Calcium 400 IU (150 tablets)",
                            Price = 9.99m,
                            Sku = "SC400"
                        },
                        new
                        {
                            Id = 26,
                            CategoryId = 5,
                            Description = "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.",
                            IsAvailable = true,
                            Name = "Flaxseed Oil 100 mg (90 capsules)",
                            Price = 12.49m,
                            Sku = "SFO100"
                        },
                        new
                        {
                            Id = 27,
                            CategoryId = 5,
                            Description = "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.",
                            IsAvailable = true,
                            Name = "Iron 65 mg (150 caplets)",
                            Price = 13.99m,
                            Sku = "SI65"
                        },
                        new
                        {
                            Id = 28,
                            CategoryId = 5,
                            Description = "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.",
                            IsAvailable = true,
                            Name = "Magnesium 250 mg (100 tablets)",
                            Price = 12.49m,
                            Sku = "SM250"
                        },
                        new
                        {
                            Id = 29,
                            CategoryId = 5,
                            Description = "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.",
                            IsAvailable = true,
                            Name = "Multi-Vitamin (90 capsules)",
                            Price = 9.99m,
                            Sku = "SMV"
                        },
                        new
                        {
                            Id = 30,
                            CategoryId = 5,
                            Description = "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.",
                            IsAvailable = true,
                            Name = "Vitamin A 10,000 IU (125 caplets)",
                            Price = 11.99m,
                            Sku = "SVA"
                        },
                        new
                        {
                            Id = 31,
                            CategoryId = 5,
                            Description = "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.",
                            IsAvailable = true,
                            Name = "Vitamin B-Complex (100 caplets)",
                            Price = 12.99m,
                            Sku = "SVB"
                        },
                        new
                        {
                            Id = 32,
                            CategoryId = 5,
                            Description = "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.",
                            IsAvailable = true,
                            Name = "Vitamin C 1000 mg (100 tablets)",
                            Price = 9.99m,
                            Sku = "SVC"
                        },
                        new
                        {
                            Id = 33,
                            CategoryId = 5,
                            Description = "As a dietary supplement, take 1 tablet, twice daily, preferably with a meal. Store in a cool, dry place.",
                            IsAvailable = true,
                            Name = "Vitamin D3 1000 IU (100 tablets)",
                            Price = 12.49m,
                            Sku = "SVD3"
                        });
                });

            modelBuilder.Entity("DotnetCoreWebApi.Models.Product", b =>
                {
                    b.HasOne("DotnetCoreWebApi.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("DotnetCoreWebApi.Models.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
