using System.Reflection;
using Graph.Data;
using Graph.Interfaces;
using Graph.Models;
using Graph.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register custom services for the superheroes
builder.Services.AddScoped<ISuperheroRepository, SuperheroRepository>();
builder.Services.AddScoped<ISuperpowerRepository, AbilityRepository>();
builder.Services.AddGraphQLServer().AddQueryType<Query>().AddProjections().AddFiltering().AddSorting()
    .AddMutationType<HeroMutation>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql("Host=postgres;Port=5432;Username=testuser;Password=testpass;Database=graphdb;",
        action => action.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
});

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
    await context!.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGraphQL((PathString)"/graphql");

app.Run();