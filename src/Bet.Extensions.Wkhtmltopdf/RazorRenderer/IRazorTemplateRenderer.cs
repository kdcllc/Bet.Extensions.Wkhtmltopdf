using System.Threading;
using System.Threading.Tasks;

namespace Bet.Extensions.Wkhtmltopdf.RazorRenderer;

/// <summary>
/// The template render is used to generate the final text.
/// </summary>
public interface IRazorTemplateRenderer
{
    /// <summary>
    /// Name of the template.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Parses the template and model.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="template"></param>
    /// <param name="model"></param>
    /// <param name="isHtml"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string> RenderAsync<T>(string template, T model, bool isHtml = true, CancellationToken cancellationToken = default);
}
