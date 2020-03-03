using System.Net;
using System.Threading;

using Bet.Extensions.Wkhtmltopdf;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Benchmarks
{
    internal class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPDFGenerator(null, options => options.UseEmbedded = false);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Map("/getLarge", next =>
            {
                next.Run(async context =>
                {
                    using var client = new WebClient();
                    var html = await client.DownloadStringTaskAsync("https://www.spacex.com/missions");

                    var pdfGenerator = next.ApplicationServices.GetRequiredService<IPdfGenerator>();
                    var pdf = await pdfGenerator.GetAsync(html, CancellationToken.None);

                    context.Response.ContentType = "application/pdf";

                    var pdfStream = new System.IO.MemoryStream();
                    pdfStream.Write(pdf, 0, pdf.Length);
                    pdfStream.Position = 0;
                    await pdfStream.CopyToAsync(context.Response.Body);
                });
            });

            app.Map("/getSmall", next =>
            {
                next.Run(async context =>
                {
                    using var client = new WebClient();
                    var html = @"<!DOCTYPE html>
                        <html>
                        <head>
                        </head>
                        <body>
                            <header>
                                <h1>This is a hardcoded test</h1>
                            </header>
                            <div>
                                <h2>456789</h2>
                            </div>
                        </body>";

                    var pdfGenerator = next.ApplicationServices.GetRequiredService<IPdfGenerator>();
                    var pdf = await pdfGenerator.GetAsync(html, CancellationToken.None);

                    context.Response.ContentType = "application/pdf";

                    var pdfStream = new System.IO.MemoryStream();
                    pdfStream.Write(pdf, 0, pdf.Length);
                    pdfStream.Position = 0;
                    await pdfStream.CopyToAsync(context.Response.Body);
                });
            });
        }
    }
}
