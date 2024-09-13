using Asp.Versioning;
using HPlusSport.API;
using HPlusSport.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddControllers()/*
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    })*/;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShopDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("ShopDb")!);
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .WithOrigins("https://localhost:7201", "http://localhost:5222")
            .WithHeaders("X-API-Version");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.MapEndpoints();

app.Run();
