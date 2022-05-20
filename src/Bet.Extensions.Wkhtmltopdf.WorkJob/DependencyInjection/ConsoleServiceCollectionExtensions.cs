using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConsoleServiceCollectionExtensions
{
    public static void ConfigureServices(HostBuilderContext hostBuilder, IServiceCollection services)
    {
        services.AddScoped<IMain, Main>();
        services.AddPdfGenerator(genConfig: options => options.AppBaseDirectory = string.Empty);

        services.AddRazorRenderer();
    }
}
