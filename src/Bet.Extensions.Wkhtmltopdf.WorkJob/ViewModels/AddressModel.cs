namespace Bet.Extensions.Wkhtmltopdf.WorkJob.ViewModels
{
    public class AddressModel
    {
        public string FullName { get; set; } = string.Empty;

        public string? CompanyName { get; set; }

        public string? StreetLine1 { get; set; }

        public string? StreetLine2 { get; set; }
    }
}
