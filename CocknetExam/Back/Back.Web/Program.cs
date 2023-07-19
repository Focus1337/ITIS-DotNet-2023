using Back.Web;
using Serilog;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

try
{
    Log.Information("Starting web app");
    var builder = WebApplication.CreateBuilder(args);
    var services = builder.Services;

    builder.AddCustomControllers();

    builder.AddCustomDb();
    builder.AddCustomIdentity();
    builder.ConfigureCustomIdentityOptions();
    builder.AddCustomOpenIddict();

    services.AddEndpointsApiExplorer();

    builder.AddCustomSwaggerGen();

    builder.AddCustomAuthentication();
    services.AddAuthorization();

    builder.AddCustomApplicationServices();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    await app.MigrateDbContext();

    app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "App terminated unexpectedly");
}
finally
{
    Log.Information("Closing app");
    Log.CloseAndFlush();
}