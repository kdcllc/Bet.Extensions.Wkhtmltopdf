using System.Threading;
using System.Threading.Tasks;

using Bet.Extensions.Wkhtmltopdf.Options;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bet.Extensions.Wkhtmltopdf;

public class PdfGenerator : IPdfGenerator
{
    private readonly IOptionsMonitor<PdfOptions> _pdfOptionsMonitor;
    private readonly ILoggerFactory _loggerFactory;
    private readonly IPdfOptionsService _pdfOptionsService;
    private readonly TaskFactory _taskFactory = new TaskFactory(TaskScheduler.Current);

    private PdfGeneratorOptions _pdgGeneratorOptions;

    public PdfGenerator(
        IPdfOptionsService pdfOptionsService,
        IOptionsMonitor<PdfOptions> pdfOptionsMonitor,
        IOptionsMonitor<PdfGeneratorOptions> pdfGeneratorMonitor,
        ILoggerFactory loggerFactory)
    {
        _pdfOptionsMonitor = pdfOptionsMonitor;
        _pdgGeneratorOptions = pdfGeneratorMonitor.CurrentValue;

        pdfGeneratorMonitor.OnChange(o => _pdgGeneratorOptions = o);

        _loggerFactory = loggerFactory ?? throw new System.ArgumentNullException(nameof(loggerFactory));
        _pdfOptionsService = pdfOptionsService ?? throw new System.ArgumentNullException(nameof(pdfOptionsService));
    }

    public Task<byte[]> GetAsync(string named, string html, CancellationToken cancellationToken)
    {
        var options = _pdfOptionsService.GetWkhtmltopdfSwitches(_pdfOptionsMonitor.Get(named));
        var currentDirectory = _pdgGeneratorOptions.UseEmbedded ? string.Empty : _pdgGeneratorOptions.AppBaseDirectory;

        return _taskFactory.StartNew(() => WkhtmlWrapper.Convert(options, html, currentDirectory));
    }

    public Task<byte[]> GetAsync(string html, CancellationToken cancellationToken)
    {
        return GetAsync(string.Empty, html, cancellationToken);
    }

    public Task<byte[]> GetAsync(string named, string html, PdfOptions options, CancellationToken cancellationToken)
    {
        if (!html.StartsWith("http", System.StringComparison.InvariantCultureIgnoreCase)
            && !html.StartsWith("file", System.StringComparison.InvariantCultureIgnoreCase))
        {
            html = $"file:///{html}";
        }

        var currentDirectory = _pdgGeneratorOptions.UseEmbedded ? string.Empty : _pdgGeneratorOptions.AppBaseDirectory;
        return _taskFactory.StartNew(() => WkhtmlWrapper.Convert(_pdfOptionsService.GetWkhtmltopdfSwitches(options), html, currentDirectory));
    }

    public Task<byte[]> GetAsync(string html, PdfOptions options, CancellationToken cancellationToken)
    {
        return GetAsync(string.Empty, html, options, cancellationToken);
    }
}
