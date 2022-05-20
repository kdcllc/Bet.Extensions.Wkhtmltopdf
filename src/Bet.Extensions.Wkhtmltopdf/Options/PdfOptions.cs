using Bet.Extensions.Wkhtmltopdf.Options.Enums;

namespace Bet.Extensions.Wkhtmltopdf.Options;

/// <summary>
/// For other possible properties please refer to <see href="!:https://wkhtmltopdf.org/usage/wkhtmltopdf.txt"/>.
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
    /// Path to a header file or url.
    /// </summary>
    [OptionFlag("--header-html")]
    public string HeaderHtml { get; set; }

    /// <summary>
    ///  Add a default header, with the name of the
    /// page to the left, and the page number to
    /// the right, this is short for:
    ///   --header-left='[webpage]'
    ///   --header-right='[page]/[toPage]' --top 2cm
    ///   --header-line.
    /// </summary>
    [OptionFlag("--default-header ")]
    public bool DefaultHeader { get; set; }

    /// <summary>
    /// Display line below the header.
    /// </summary>
    [OptionFlag("--header-line")]
    public string HeaderLine { get; set; }

    /// <summary>
    /// Right aligned header text.
    /// <text></text>
    /// </summary>
    [OptionFlag("--header-right")]
    public string HeaderRight { get; set; }

    /// <summary>
    /// Left aligned header text.
    /// </summary>
    [OptionFlag("--header-left")]
    public string HeaderLeft { get; set; }

    /// <summary>
    /// Sets the header spacing.
    /// </summary>
    [OptionFlag("--header-spacing")]
    public int? HeaderSpacing { get; set; }

    /// <summary>
    /// Path to the footer HTML file or url.
    /// </summary>
    [OptionFlag("--footer-html")]
    public string FooterHtml { get; set; }

    /// <summary>
    /// Left aligned footer text.
    /// </summary>
    [OptionFlag("--footer-right")]
    public string FooterRight { get; set; }

    /// <summary>
    /// The text for the footer.
    /// </summary>
    [OptionFlag("--footer-left")]
    public string FooterLeft { get; set; }

    /// <summary>
    /// Sets the footer spacing.
    /// </summary>
    [OptionFlag("--footer-spacing")]
    public int? FooterSpacing { get; set; }

    /// <summary>
    /// Use this zoom factor (default 1).
    /// </summary>
    [OptionFlag("--footer-spacing")]
    public double? ZoomFactor { get; set; }

    /// <summary>
    /// Disable the intelligent shrinking strategy
    /// used by WebKit that makes the pixel/dpi ratio non-constant.
    /// By default it is enabled.
    /// </summary>
    [OptionFlag("--disable-smart-shrinking")]
    public bool DisableSmartShrinking { get; set; }

    /// <summary>
    /// Do not make links to remote web pages.
    /// </summary>
    [OptionFlag("--disable-external-links")]
    public bool DisableExternalLinks { get; set; }

    /// <summary>
    /// Allowed conversion of a local file to readin other local files.
    /// </summary>
    [OptionFlag("--enable-local-file-access")]
    public bool EnableLocalFileAccess { get; set; }

    /// <summary>
    /// Do not make links to remote web pages.
    /// </summary>
    [OptionFlag("--load-error-handling")]
    public LoadErrorHandeling LoadErrorHandling { get; set; } = LoadErrorHandeling.Abort;

    public string GetContentType()
    {
        return "application/pdf";
    }
}
