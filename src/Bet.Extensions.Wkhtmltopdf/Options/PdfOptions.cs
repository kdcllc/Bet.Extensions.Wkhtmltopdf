
using Bet.Extensions.Wkhtmltopdf.Options.Enums;

namespace Bet.Extensions.Wkhtmltopdf.Options
{
    /// <summary>
    /// For other possible properties please refer to <see cref="!:https://wkhtmltopdf.org/usage/wkhtmltopdf.txt"/>.
    /// </summary>
    public class PdfOptions
    {
        public PdfOptions()
        {
            PageMargins = new Margins();
        }

        /// <summary>
        /// Sets the page size.
        /// </summary>
        [OptionFlag("-s")]
        public Size? PageSize { get; set; }

        /// <summary>
        /// Sets the page width in mm.
        /// </summary>
        /// <remarks>Has priority over <see cref="PageSize"/> but <see cref="PageHeight"/> has to be also specified.</remarks>
        [OptionFlag("--page-width")]
        public double? PageWidth { get; set; }

        /// <summary>
        /// Sets the page height in mm.
        /// </summary>
        /// <remarks>Has priority over <see cref="PageSize"/> but <see cref="PageWidth"/> has to be also specified.</remarks>
        [OptionFlag("--page-height")]
        public double? PageHeight { get; set; }

        /// <summary>
        /// Sets the page orientation.
        /// </summary>
        [OptionFlag("-O")]
        public Orientation? PageOrientation { get; set; }

        /// <summary>
        /// Sets the page margins.
        /// </summary>
        public Margins PageMargins { get; set; }

        /// <summary>
        /// Indicates whether the PDF should be generated in lower quality.
        /// </summary>
        [OptionFlag("-l")]
        public bool IsLowQuality { get; set; }

        /// <summary>
        /// Number of copies to print into the PDF file.
        /// </summary>
        [OptionFlag("--copies")]
        public int? Copies { get; set; }

        /// <summary>
        /// Indicates whether the PDF should be generated in grayscale.
        /// </summary>
        [OptionFlag("-g")]
        public bool IsGrayScale { get; set; }

        /// <summary>
        /// Path to header HTML file.
        /// </summary>
        [OptionFlag("--header-html")]
        public string HeaderHtml { get; set; }

        /// <summary>
        /// Sets the header spacing.
        /// </summary>
        [OptionFlag("--header-spacing")]
        public int? HeaderSpacing { get; set; }

        /// <summary>
        /// Path to footer HTML file.
        /// </summary>
        [OptionFlag("--footer-html")]
        public string FooterHtml { get; set; }

        /// <summary>
        /// Sets the footer spacing.
        /// </summary>
        [OptionFlag("--footer-spacing")]
        public int? FooterSpacing { get; set; }

        public string GetContentType()
        {
            return "application/pdf";
        }
    }
}
