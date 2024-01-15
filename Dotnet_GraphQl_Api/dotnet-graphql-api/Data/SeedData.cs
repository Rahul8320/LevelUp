using dotnet_graphql_api.Data.Models;

namespace dotnet_graphql_api.Data;

public static class SeedData
{
    public static void PopulateDb(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        AddInitialData(serviceScope.ServiceProvider.GetService<CourseDbContext>()!);
    }

    private static void AddInitialData(CourseDbContext context)
    {
        if (!context.Courses.Any())
        {
            var courses = new List<Course>
            {
                new() {
                    Id = Guid.NewGuid(),
                    Name = "Introduction to Programming",
                    Description = "A beginner's guide to programming",
                    Review = 4,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new() {
                    Id = Guid.NewGuid(),
                    Name = "Web Development Basics",
                    Description = "Fundamentals of web development",
                    Review = 5,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new() {
                    Id = Guid.NewGuid(),
                    Name = "Database Design",
                    Description = "Essentials of database design",
                    Review = 3,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new() {
                    Id = Guid.NewGuid(),
                    Name = "Mobile App Development",
                    Description = "Building mobile applications",
                    Review = 4,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new() {
                    Id = Guid.NewGuid(),
                    Name = "Machine Learning Basics",
                    Description = "Introduction to machine learning concepts",
                    Review = 5,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Courses.AddRange(courses);
            context.SaveChanges();
            Console.WriteLine("Seeded Data to the Database.");
        }
    }
}
