using System;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using Bet.Extensions.Wkhtmltopdf;
using Bet.Extensions.Wkhtmltopdf.Options;
using Bet.Extensions.Wkhtmltopdf.RazorRenderer;
using Bet.Extensions.Wkhtmltopdf.WorkJob.ViewModels;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class Main : IMain
{
    private readonly ILogger<Main> _logger;
    private readonly IRazorTemplateRenderer _renderer;
    private readonly IPdfGenerator _pdfGenerator;
    private readonly IHostApplicationLifetime _applicationLifetime;

    public Main(
        IRazorTemplateRenderer renderer,
        IPdfGenerator pdfGenerator,
        IHostApplicationLifetime applicationLifetime,
        IConfiguration configuration,
        ILogger<Main> logger)
    {
        _renderer = renderer;
        _pdfGenerator = pdfGenerator ?? throw new ArgumentNullException(nameof(pdfGenerator));
        _applicationLifetime = applicationLifetime ?? throw new ArgumentNullException(nameof(applicationLifetime));
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IConfiguration Configuration { get; set; }

    public async Task<int> RunAsync()
    {
        _logger.LogInformation("Main executed");

        // use this token for stopping the services
        var cancellationToken = _applicationLifetime.ApplicationStopping;

        cancellationToken.ThrowIfCancellationRequested();

        var template = string.Empty;

        var razorPagePath = Path.Combine("Views", "Invoice.cshtml");
        using (var reader = new StreamReader(File.OpenRead(razorPagePath)))
        {
            template = await reader.ReadToEndAsync();
        }

        dynamic viewBag = new ExpandoObject();
        viewBag.Title = "A simple, clean, and responsive HTML invoice template Razor";

        var model = new InvoiceModel
        {
            InvoiceId = new Random().Next(1, 100),
            CreatedAt = DateOnly.FromDateTime(DateTime.Now),
            DueAt = DateOnly.FromDateTime(DateTime.Now.AddDays(30)),

            From = new AddressModel
            {
                CompanyName = "King David Consulting LLC",
                FullName = "KDCLLC"
            },

            ViewBag = viewBag,
        };

        var result = await _renderer.RenderAsync(template, model);

        var htmlPagePath = Path.Combine(AppContext.BaseDirectory, "html", "invoice.html");
        var htmlPageText = File.ReadAllText(htmlPagePath);

        var options = new PdfOptions
        {
            //DisableSmartShrinking = true,
            //LoadErrorHandling = LoadErrorHandeling.Ignore,
            //DefaultHeader = true,
            //FooterLeft = "[page]",
            //EnableLocalFileAccess = true,
            //DisableExternalLinks = true,
            //PageSize = Size.Letter,
            //PageOrientation = Orientation.Portrait
        };

        // adds page break after each page
        var pages = $"{result}<div style='page-break-after:always'></div>{htmlPageText}";

        var byteArray = await _pdfGenerator.GetAsync("http://192.168.86.141:8080/invoice.html", options, cancellationToken);

        var fileName = Path.Combine($"{Guid.NewGuid()}-invoice.pdf");

        using var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);

        fs.Write(byteArray, 0, byteArray.Length);

        _logger.LogInformation("PDF generated: {fileName}", fileName);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Process.Start("explorer.exe", fileName);
        }
        else
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            Console.WriteLine($"Output PDF file is available here: {fullPath}");
        }

        return 0;
    }
}
