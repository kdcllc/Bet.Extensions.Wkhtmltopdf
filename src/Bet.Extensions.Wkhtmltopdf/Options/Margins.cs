﻿using System.Globalization;
using System.Linq;
using System.Text;

namespace Bet.Extensions.Wkhtmltopdf.Options;

public class Margins
{
    /// <summary>
    /// Page bottom margin in mm.
    /// </summary>
    [OptionFlag("-B")] public int? Bottom;

    /// <summary>
    /// Page left margin in mm.
    /// </summary>
    [OptionFlag("-L")] public int? Left;

    /// <summary>
    /// Page right margin in mm.
    /// </summary>
    [OptionFlag("-R")] public int? Right;

    /// <summary>
    /// Page top margin in mm.
    /// </summary>
    [OptionFlag("-T")] public int? Top;

    public Margins()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Margins"/> class.
    /// Sets the page margins.
    /// </summary>
    /// <param name="top">Page top margin in mm.</param>
    /// <param name="right">Page right margin in mm.</param>
    /// <param name="bottom">Page bottom margin in mm.</param>
    /// <param name="left">Page left margin in mm.</param>
    public Margins(int top, int right, int bottom, int left)
    {
        Top = top;
        Right = right;
        Bottom = bottom;
        Left = left;
    }

    public override string ToString()
    {
        var result = new StringBuilder();

        var fields = GetType().GetFields();
        foreach (var fi in fields)
        {
            if (!(fi.GetCustomAttributes(typeof(OptionFlag), true).FirstOrDefault() is OptionFlag of))
            {
                continue;
            }

            var value = fi.GetValue(this);
            if (value != null)
            {
                result.AppendFormat(CultureInfo.InvariantCulture, " {0} {1}", of.Name, value);
            }
        }

        return result.ToString().Trim();
    }
}
