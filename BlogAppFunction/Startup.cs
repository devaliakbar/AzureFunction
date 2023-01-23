using BlogAppFunction.Data;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(BlogAppFunction.Startup))]

namespace BlogAppFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string connectionStrig = System.Environment.GetEnvironmentVariable("PGConnectionString", System.EnvironmentVariableTarget.Process);

            builder.Services.AddDbContext<DataContext>(opt =>
            {
                opt.UseNpgsql(connectionStrig);
            });
        }
    }
}

