
using Microsoft.EntityFrameworkCore.Design;
using PersonalBrand.API.PersonalIdentity;
using PersonalBrand.Application;
using PersonalBrand.Infrastructure;
using Serilog;                                      

namespace PersonalBrand.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // ILogggerni sozlash
            builder.Services.AddLogging(logging =>
            {
                logging.AddSerilog(dispose: true);      
            });

            Log.Logger = new LoggerConfiguration()
           .WriteTo.Console(
               theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Sixteen) 
                                                                                   
           .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day) 
           .CreateLogger();


            builder.Services.AddControllers();
            builder.Services.AddInfrastructure(builder.Configuration);
            
            // Cache qoshildi
            builder.Services.AddMemoryCache();

            builder.Services.AddApplication();
            builder.Services.AddIdentity();
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
}















