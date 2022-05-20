using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using RazorLight;

namespace Bet.Extensions.Wkhtmltopdf.RazorRenderer;

public class RazorTemplateRenderer : IRazorTemplateRenderer
{
    private RazorLightEngine? _engine;

    public RazorTemplateRenderer(RazorTemplateRendererOptions options)
    {
        Name = string.Empty;
        Configure(options);
    }

    public RazorTemplateRenderer(IOptionsMonitor<RazorTemplateRendererOptions> optionsMonitor) : this(string.Empty, optionsMonitor)
    {
    }

    public RazorTemplateRenderer(string name, IOptionsMonitor<RazorTemplateRendererOptions> optionsMonitor)
    {
        Name = name;

        var options = optionsMonitor.Get(name);
        Configure(options);
    }

    public string Name { get; }

    public Task<string> RenderAsync<T>(string template, T model, bool isHtml = true, CancellationToken cancellationToken = default)
    {
        if (_engine == null)
        {
            throw new ArgumentNullException(nameof(RazorLightEngine));
        }

        dynamic? viewBag = (model as IViewBagModel)?.ViewBag;
        return _engine.CompileRenderStringAsync<T>(GetHashString(template), template, model, viewBag);
    }

    private static string GetHashString(string inputString)
    {
        var sb = new StringBuilder();
        var hashbytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(inputString));
        foreach (var b in hashbytes)
        {
            sb.Append(b.ToString("X2"));
        }

        return sb.ToString();
    }

    private void Configure(RazorTemplateRendererOptions options)
    {
        var builder = new RazorLightEngineBuilder();

        if (!string.IsNullOrEmpty(options.RootDirectory))
        {
            builder.UseFileSystemProject(options.RootDirectory);
        }
        else if (options.Project != null)
        {
            builder.UseProject(options.Project);
        }
        else if (options.EmbeddedResourceRootType != null)
        {
            builder.UseEmbeddedResourcesProject(options.EmbeddedResourceRootType);
        }

        _engine = builder
                    .UseMemoryCachingProvider()
                    .Build();
    }
}
