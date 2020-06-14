using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MockDataGenerator.ServiceHelper;

[assembly: FunctionsStartup(typeof(MockDataGenerator.FunctionStartup.Startup))]
namespace MockDataGenerator.FunctionStartup
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IGenerator,Generator>();
        }
    }
}
