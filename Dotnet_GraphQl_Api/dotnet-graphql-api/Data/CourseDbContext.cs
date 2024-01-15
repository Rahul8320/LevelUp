using dotnet_graphql_api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_graphql_api.Data;

public class CourseDbContext(DbContextOptions<CourseDbContext> options) : DbContext(options)
{
    public DbSet<Course> Courses { get; set; } = null!;
}
