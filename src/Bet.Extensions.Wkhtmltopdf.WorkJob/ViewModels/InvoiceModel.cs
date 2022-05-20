using System;
using System.Dynamic;

using Bet.Extensions.Wkhtmltopdf.RazorRenderer;

namespace Bet.Extensions.Wkhtmltopdf.WorkJob.ViewModels;

public class InvoiceModel : IViewBagModel
{
    public ExpandoObject ViewBag { get; set; }

    public int InvoiceId { get; set; }

    public DateOnly CreatedAt { get; set; }

    public DateOnly DueAt { get; set; }

    public AddressModel From { get; set; } = new AddressModel();
}
