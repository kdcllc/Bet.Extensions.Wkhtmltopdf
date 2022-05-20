using Bet.Extensions.Wkhtmltopdf.Options;

namespace Bet.Extensions.Wkhtmltopdf;

public interface IPdfOptionsService
{
    /// <summary>
    /// Generates switches based on the configurations.
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    string GetWkhtmltopdfSwitches(PdfOptions options);
}
