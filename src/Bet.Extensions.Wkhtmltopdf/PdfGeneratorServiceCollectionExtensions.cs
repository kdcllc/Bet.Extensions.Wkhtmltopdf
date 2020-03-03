using System;

using Bet.Extensions.Wkhtmltopdf;
using Bet.Extensions.Wkhtmltopdf.Internal;
using Bet.Extensions.Wkhtmltopdf.Options;

using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PdfGeneratorServiceCollectionExtensions
    {
        public static IServiceCollection AddPDFGenerator(
            this IServiceCollection services,
            Action<PdfOptions>? pdfConfig = null,
            Action<PdfGeneratorOptions>? genConfig = null)
        {
            services
                .AddOptions<PdfOptions>()
                .Configure(x => pdfConfig?.Invoke(x));

            services
                .AddOptions<PdfGeneratorOptions>()
                .Configure(x => genConfig?.Invoke(x));

            services.TryAddTransient<IPdfOptionsService, PdfOptionsService>();

            services.TryAddSingleton<IPdfGenerator, PdfGenerator>();

            return services;
        }
    }
}
