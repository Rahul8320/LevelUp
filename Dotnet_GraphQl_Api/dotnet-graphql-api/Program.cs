using dotnet_graphql_api.Data;
using dotnet_graphql_api.Data.Repositories;
using dotnet_graphql_api.GraphQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers 
builder.Services.AddControllers();

// Add Db Context
builder.Services.AddDbContext<CourseDbContext>(opt => opt.UseInMemoryDatabase("CourseList"));

// Add Repository
builder.Services.AddScoped<CoursesRepository>();
builder.Services.AddScoped<CourseQuery>();

// GraphQl
builder.Services.AddGraphQLServer().AddQueryType<CourseQuery>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// GraphQL
app.MapGraphQL();

app.MapControllers();

// seed data to db
SeedData.PopulateDb(app);

app.Run();