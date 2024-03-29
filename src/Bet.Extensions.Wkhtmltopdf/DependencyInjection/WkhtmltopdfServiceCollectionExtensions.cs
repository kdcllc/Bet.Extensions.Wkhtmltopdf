﻿using System;

using Bet.Extensions.Wkhtmltopdf;
using Bet.Extensions.Wkhtmltopdf.Options;
using Bet.Extensions.Wkhtmltopdf.RazorRenderer;

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class WkhtmltopdfServiceCollectionExtensions
{
    /// <summary>
    /// Add Html To Pdf generator.
    /// </summary>
    /// <param name="services">The DI services.</param>
    /// <param name="optionName">The name of the options to be registered on the server.</param>
    /// <param name="sectionName">The section name from which to read the options configurations.</param>
    /// <param name="pdfConfig">The <see cref="PdfOptions"/> configure.</param>
    /// <param name="genConfig">The <see cref="PdfGeneratorOptions"/> configure.</param>
    /// <returns></returns>
    public static IServiceCollection AddPdfGenerator(
        this IServiceCollection services,
        string optionName = "",
        string sectionName = "PdfOptions",
        Action<PdfOptions>? pdfConfig = null,
        Action<PdfGeneratorOptions>? genConfig = null)
    {
        services.AddChangeTokenOptions(sectionName, optionName, pdfConfig);

        services.AddChangeTokenOptions("PdfGeneratorOptions", string.Empty, genConfig);

        services.TryAddTransient<IPdfOptionsService, PdfOptionsService>();

        services.TryAddSingleton<IPdfGenerator, PdfGenerator>();

        return services;
    }

    /// <summary>
    /// Adds Razor Template Renderer.
    /// </summary>
    /// <param name="services">The di services.</param>
    /// <param name="templateName">The name of the template. The default is empty.</param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IServiceCollection AddRazorRenderer(
        this IServiceCollection services,
        string templateName = "",
        Action<RazorTemplateRendererOptions, IServiceProvider>? configure = null)
    {
        services.AddChangeTokenOptions<RazorTemplateRendererOptions>(
            nameof(RazorTemplateRendererOptions),
            templateName,
            (o, c) =>
            {
                configure?.Invoke(o, c);
            });

        services.AddSingleton<IRazorTemplateRenderer, RazorTemplateRenderer>(sp =>
        {
            var options = sp.GetRequiredService<IOptionsMonitor<RazorTemplateRendererOptions>>();
            return new RazorTemplateRenderer(templateName, options);
        });

        return services;
    }
}
