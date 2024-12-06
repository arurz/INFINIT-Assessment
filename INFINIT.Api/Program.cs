using INFINIT.Configurations;
using INFINIT.ServiceLayer.DI;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configurations = builder.Configuration;

        configurations.AddConfigurations();

        builder.Services.Configure<GitHubApiSettings>(configurations.GetSection(nameof(GitHubApiSettings)));

        builder.Services.ConfigureServices(configurations);
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}