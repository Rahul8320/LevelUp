using GraphQl_Demo.GraphQl.Query;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add GraphQL Server
builder.Services.AddGraphQLServer().AddQueryType<BookQuery>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseRouting().UseEndpoints(endpoints => endpoints.MapGraphQL());
app.MapGraphQL();

app.Run();