using System.Threading;
using System.Threading.Tasks;

using Bet.Extensions.Wkhtmltopdf.Options;

namespace Bet.Extensions.Wkhtmltopdf
{
    public interface IPdfGenerator
    {
        /// <summary>
        /// Get a PDF Document from the html. It uses named options configurations.
        /// </summary>
        /// <param name="named">The name of the options that was registered with the system.</param>
        /// <param name="html">The html to be converted.</param>
        /// <param name="cancellationToken">The CancellationToken.</param>
        /// <returns></returns>
        Task<byte[]> GetAsync(string named, string html, CancellationToken cancellationToken);

        /// <summary>
        /// Get a PDF Document from the html. It uses the default server configuration for <see cref="PdfOptions"/>.
        /// </summary>
        /// <param name="html">The html to be converted.</param>
        /// <param name="cancellationToken">The CancellationToken.</param>
        /// <returns></returns>
        Task<byte[]> GetAsync(string html, CancellationToken cancellationToken);

        /// <summary>
        /// Get a PDF Document based on custom <see cref="PdfOptions"/> configurations.
        /// </summary>
        /// <param name="named">The name of the options that was registered with the system.</param>
        /// <param name="html">The html to be converted.</param>
        /// <param name="options">The custom options for the document.</param>
        /// <param name="cancellationToken">The CancellationToken.</param>
        /// <returns></returns>
        Task<byte[]> GetAsync(string named, string html, PdfOptions options, CancellationToken cancellationToken);

        /// <summary>
        /// Get a PDF Document based on custom <see cref="PdfOptions"/> configurations.
        /// </summary>
        /// <param name="html">The html to be converted.</param>
        /// <param name="options">The custom options for the document.</param>
        /// <param name="cancellationToken">The CancellationToken.</param>
        /// <returns></returns>
        Task<byte[]> GetAsync(string html, PdfOptions options, CancellationToken cancellationToken);
    }
}
