using System;
using System.IO;

namespace Bet.Extensions.Wkhtmltopdf.Options
{
    public class PdfGeneratorOptions
    {
        public PdfGeneratorOptions()
        {
            if (!UseEmbedded)
            {
                AppBaseDirectory = AppContext.BaseDirectory;
            }
        }

        public bool UseEmbedded { get; set; } = true;

        public string AppBaseDirectory { get; set; } = Path.Combine(AppContext.BaseDirectory, nameof(WkhtmlWrapper));
    }
}
