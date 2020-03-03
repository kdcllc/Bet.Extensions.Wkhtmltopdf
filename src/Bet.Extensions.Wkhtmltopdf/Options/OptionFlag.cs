using System;

namespace Bet.Extensions.Wkhtmltopdf.Options
{
    internal sealed class OptionFlag : Attribute
    {
        public OptionFlag(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
