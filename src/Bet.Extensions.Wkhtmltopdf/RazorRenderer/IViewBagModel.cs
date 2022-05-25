using System.Dynamic;

namespace Bet.Extensions.Wkhtmltopdf.RazorRenderer;

public interface IViewBagModel
{
    ExpandoObject? ViewBag { get; }
}
