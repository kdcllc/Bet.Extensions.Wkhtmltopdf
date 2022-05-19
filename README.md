﻿# Bet.Extensions.Wkhtmltopdf

[![Build status](https://ci.appveyor.com/api/projects/status/53eor37h3tekdn4v?svg=true)](https://ci.appveyor.com/project/kdcllc/bet-extensions-wkhtmltopdf)
[![NuGet](https://img.shields.io/nuget/v/Bet.Extensions.Wkhtmltopdf.svg)](https://www.nuget.org/packages?q=Bet.Extensions.Wkhtmltopdf)
![Nuget](https://img.shields.io/nuget/dt/Bet.Extensions.Wkhtmltopdf)
[![feedz.io](https://img.shields.io/badge/endpoint.svg?url=https://f.feedz.io/kdcllc/bet-aspnetcore/shield/Bet.Extensions.Wkhtmltopdf/latest)](https://f.feedz.io/kdcllc/bet-aspnetcore/packages/Bet.Extensions.Wkhtmltopdf/latest/download)

> The second letter in the Hebrew alphabet is the ב bet/beit. Its meaning is "house". In the ancient pictographic Hebrew it was a symbol resembling a tent on a landscape.

_Note: Pre-release packages are distributed via [feedz.io](https://f.feedz.io/kdcllc/bet-aspnetcore/nuget/index.json)._

## Summary

The purpose of this project is to provide a wrapper around [wkhtmltopdf](https://wkhtmltopdf.org/) html to pdf generator.

- [`Bet.Extensions.Wkhtmltopdf`](./src/Bet.Extensions.Wkhtmltopdf/) the library to be used in DotNetCore projects.
- [`Bet.Extensions.Wkhtmltopdf.WebApi`](./src/Bet.Extensions.Wkhtmltopdf.WebApi/) the AspNetCore sample project that runs in Docker Container.
- [`Bet.Extensions.Wkhtmltopdf.WorkJob`](./src/Bet.Extensions.Wkhtmltopdf.WorkJob/) the DotNetCore Console App sample.

## Hire me

Please send [email](mailto:kingdavidconsulting@gmail.com) if you consider to **hire me**.

[![buymeacoffee](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/vyve0og)

## Give a Star! :star:

If you like or are using this project to learn or start your solution, please give it a star. Thanks!

## Install

```bash
    dotnet add package Bet.Extensions.Wkhtmltopdf
```

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

## WSL Linux

```bash
    sudo apt-get install libjpeg62
```

## Benchmark

For details on the benchmarking this project please refer to [Benchmarks](./src/Benchmarks/).

## References

- [DotNet Special Folder Api Linux](https://developers.redhat.com/blog/2018/11/07/dotnet-special-folder-api-linux/)
