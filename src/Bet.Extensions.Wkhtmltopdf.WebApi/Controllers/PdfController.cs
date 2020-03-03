using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bet.Extensions.Wkhtmltopdf.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PdfController : ControllerBase
    {
        private readonly ILogger<PdfController> _logger;
        private readonly IPdfGenerator _pdfGenerator;

        public PdfController(IPdfGenerator pdfGenerator, ILogger<PdfController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _pdfGenerator = pdfGenerator ?? throw new ArgumentNullException(nameof(pdfGenerator));
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(string url, CancellationToken cancellationToken)
        {
            using var client = new WebClient();
            var html = await client.DownloadStringTaskAsync(url);

            var pdf = await _pdfGenerator.GetAsync(html, cancellationToken);

            var pdfStream = new MemoryStream();
            pdfStream.Write(pdf, 0, pdf.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]string content, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(content))
            {
                await Task.CompletedTask;
            }

            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(req => req.Content(content), cancellationToken);

            var pdf = await _pdfGenerator.GetAsync(content, cancellationToken);

            var pdfStream = new MemoryStream();
            pdfStream.Write(pdf, 0, pdf.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf");
        }
    }
}
