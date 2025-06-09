using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Threading.Tasks;

namespace GateWay
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Ocelot.json konfiqurasiya
            builder.Configuration
                   .SetBasePath(builder.Environment.ContentRootPath)
                   .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
                   .AddEnvironmentVariables();

            // Ocelot servis əlavə et
            builder.Services.AddOcelot(builder.Configuration);

            // Əgər Gateway-ə öz controller əlavə etmisənsə
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Swagger yalnız development üçün
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // HTTPS redirect varsa aç (opsional)
            // app.UseHttpsRedirection();

            // 🚨 MÜTLƏQ: Routing → Authorization → MapControllers
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();

            // Ocelot ən sonda gəlir
            await app.UseOcelot();

            // App run
            app.Run();
        }
    }
}
