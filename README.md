[![Build status](https://ci.appveyor.com/api/projects/status/53eor37h3tekdn4v?svg=true)](https://ci.appveyor.com/project/kdcllc/bet-extensions-wkhtmltopdf)
[![NuGet](https://img.shields.io/nuget/v/Bet.Extensions.Wkhtmltopdf.svg)](https://www.nuget.org/packages?q=Bet.Extensions.Wkhtmltopdf)
![Nuget](https://img.shields.io/nuget/dt/Bet.Extensions.Wkhtmltopdf)
[![feedz.io](https://img.shields.io/badge/endpoint.svg?url=https://f.feedz.io/kdcllc/bet-aspnetcore/shield/Bet.Extensions.Wkhtmltopdf/latest)](https://f.feedz.io/kdcllc/bet-aspnetcore/packages/Bet.Extensions.Wkhtmltopdf/latest/download)

# Bet.Extensions.Wkhtmltopdf

This is a simple nuget package and also AspNetCore Web Api project that runs in Docker Linux container and Converts Html to Pdf.

## Usage

1. In `Startup.cs` or `Program.cs` please register the service.

```csharp
    services.AddPdfGenerator();
    
```

2. Then use within components

```csharp
    var byteArray = await _pdfGenerator.GetAsync(html, stoppingToken);

    var fileName = Path.Combine(AppContext.BaseDirectory, $"{Guid.NewGuid().ToString()}.pdf");

    using var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
    fs.Write(byteArray, 0, byteArray.Length);
```

## Benchmark

For details on the benchmarking this project please refer to [Benchmarks](./src/Benchmarks/).

## References

- [DotNet Special Folder Api Linux](https://developers.redhat.com/blog/2018/11/07/dotnet-special-folder-api-linux/)
