using System;
using System.IO;

using RazorLight.Razor;

namespace Bet.Extensions.Wkhtmltopdf.RazorRenderer;

public class RazorTemplateRendererOptions
{
    /// <summary>
    /// The root directory to use for the layouts.
    /// The default is 'Directory.GetCurrentDirectory()'.
    /// This allows for Directory based templates rendering.
    /// </summary>
    public string RootDirectory { get; set; } = Directory.GetCurrentDirectory();

    /// <summary>
    /// This allows for <see cref="RazorLightProject"/> to be passed in order to create templating.
    /// The default is null.
    /// </summary>
    public RazorLightProject? Project { get; set; }

    /// <summary>
    /// The embeded resource within assembly to pass.
    /// The default is null.
    /// </summary>
    public Type? EmbeddedResourceRootType { get; set; }
}
