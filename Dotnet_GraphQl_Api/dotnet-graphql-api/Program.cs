using dotnet_graphql_api.Data;
using dotnet_graphql_api.Data.Repositories;
using dotnet_graphql_api.GraphQL;
using dotnet_graphql_api.GraphQL.Queries;
using dotnet_graphql_api.GraphQL.Types;
using GraphQL.Types;
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
builder.Services.AddSingleton<CourseType>();
builder.Services.AddSingleton<CourseQuery>();
builder.Services.AddSingleton<ISchema, AppSchema>();

builder.Services.AddGraphQL();

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
app.UseGraphQL<ISchema>();
app.UseGraphQLGraphiQL("/ui/graphql");

app.MapControllers();

app.Run();