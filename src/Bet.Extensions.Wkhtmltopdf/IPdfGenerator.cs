using System.Threading;
using System.Threading.Tasks;

using Bet.Extensions.Wkhtmltopdf.Options;

namespace Bet.Extensions.Wkhtmltopdf
{
    public interface IPdfGenerator
    {
        Task<byte[]> GetAsync(string named, string html, CancellationToken cancellationToken);

        Task<byte[]> GetAsync(string html, CancellationToken cancellationToken);

        Task<byte[]> GetAsync(string named, string html, PdfOptions options, CancellationToken cancellationToken);

        Task<byte[]> GetAsync(string html, PdfOptions options, CancellationToken cancellationToken);
    }
}
