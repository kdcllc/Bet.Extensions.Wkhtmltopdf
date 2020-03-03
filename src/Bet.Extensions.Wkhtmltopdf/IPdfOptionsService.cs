using Bet.Extensions.Wkhtmltopdf.Options;

namespace Bet.Extensions.Wkhtmltopdf.Internal
{
    public interface IPdfOptionsService
    {
        string GetWkhtmltopdfSwitches(PdfOptions options);
    }
}
