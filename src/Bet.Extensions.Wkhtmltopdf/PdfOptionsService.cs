using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Bet.Extensions.Wkhtmltopdf.Options;

using Microsoft.Extensions.Logging;

namespace Bet.Extensions.Wkhtmltopdf
{
    public class PdfOptionsService : IPdfOptionsService
    {
        private readonly ILogger<PdfOptionsService> _logger;

        public PdfOptionsService(ILogger<PdfOptionsService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public string GetWkhtmltopdfSwitches(PdfOptions options)
        {
            if (options == null)
            {
                return string.Empty;
            }

            return GetSwitches(options);
        }

        private string GetSwitches(PdfOptions options)
        {
            var result = new StringBuilder();

            if (options.PageMargins != null)
            {
                result.Append(options.PageMargins.ToString());
            }

            result.Append(" ");
            result.Append(GetSwitchesBas(options));

            return result.ToString().Trim();
        }

        private string GetSwitchesBas(PdfOptions options)
        {
            var result = new StringBuilder();

            var fields = options.GetType().GetProperties();
            foreach (var fi in fields)
            {
                if (!(fi.GetCustomAttributes(typeof(OptionFlag), true).FirstOrDefault() is OptionFlag of))
                {
                    continue;
                }

                var value = fi.GetValue(options, null);
                if (value == null)
                {
                    continue;
                }

                if (fi.PropertyType == typeof(Dictionary<string, string>))
                {
                    var dictionary = (Dictionary<string, string>)value;
                    foreach (var d in dictionary)
                    {
                        result.AppendFormat(" {0} {1} {2}", of.Name, d.Key, d.Value);
                    }
                }
                else if (fi.PropertyType == typeof(bool))
                {
                    if ((bool)value)
                    {
                        result.AppendFormat(CultureInfo.InvariantCulture, " {0}", of.Name);
                    }
                }
                else
                {
                    result.AppendFormat(CultureInfo.InvariantCulture, " {0} {1}", of.Name, value);
                }
            }

            return result.ToString().Trim();
        }
    }
}
