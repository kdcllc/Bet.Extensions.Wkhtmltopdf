using System.Threading;
using System.Threading.Tasks;

using Bet.Extensions.Wkhtmltopdf.Options;

using Microsoft.Extensions.Options;

namespace Bet.Extensions.Wkhtmltopdf
{
    public class PdfGenerator : IPdfGenerator
    {
        private readonly IOptionsMonitor<PdfOptions> _pdfOptionsMonitor;
        private readonly IPdfOptionsService _pdfOptionsService;
        private readonly TaskFactory _taskFactory = new TaskFactory(TaskScheduler.Current);

        private PdfGeneratorOptions _pdgGeneratorOptions;

        public PdfGenerator(
            IPdfOptionsService pdfOptionsService,
            IOptionsMonitor<PdfOptions> pdfOptionsMonitor,
            IOptionsMonitor<PdfGeneratorOptions> pdfGeneratorMonitor)
        {
            _pdfOptionsMonitor = pdfOptionsMonitor;
            _pdgGeneratorOptions = pdfGeneratorMonitor.CurrentValue;

            pdfGeneratorMonitor.OnChange(o => _pdgGeneratorOptions = o);

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
            var currentDirectory = _pdgGeneratorOptions.UseEmbedded ? string.Empty : _pdgGeneratorOptions.AppBaseDirectory;
            return _taskFactory.StartNew(() => WkhtmlWrapper.Convert(_pdfOptionsService.GetWkhtmltopdfSwitches(options), html, currentDirectory));
        }

        public Task<byte[]> GetAsync(string html, PdfOptions options, CancellationToken cancellationToken)
        {
            var currentDirectory = _pdgGeneratorOptions.UseEmbedded ? string.Empty : _pdgGeneratorOptions.AppBaseDirectory;
            return GetAsync(string.Empty, html, options, cancellationToken);
        }
    }
}
