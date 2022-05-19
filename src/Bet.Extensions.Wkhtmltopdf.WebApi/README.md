# Bet.Extensions.Wkhtmltopdf

[![Build status](https://ci.appveyor.com/api/projects/status/53eor37h3tekdn4v?svg=true)](https://ci.appveyor.com/project/kdcllc/bet-extensions-wkhtmltopdf)
[![NuGet](https://img.shields.io/nuget/v/Bet.Extensions.Wkhtmltopdf.svg)](https://www.nuget.org/packages?q=Bet.Extensions.Wkhtmltopdf)
![Nuget](https://img.shields.io/nuget/dt/Bet.Extensions.Wkhtmltopdf)
[![feedz.io](https://img.shields.io/badge/endpoint.svg?url=https://f.feedz.io/kdcllc/bet-aspnetcore/shield/Bet.Extensions.Wkhtmltopdf/latest)](https://f.feedz.io/kdcllc/bet-aspnetcore/packages/Bet.Extensions.Wkhtmltopdf/latest/download)

> The second letter in the Hebrew alphabet is the ב bet/beit. Its meaning is "house". In the ancient pictographic Hebrew it was a symbol resembling a tent on a landscape.

_Note: Pre-release packages are distributed via [feedz.io](https://f.feedz.io/kdcllc/bet-aspnetcore/nuget/index.json)._

## Summary

The purpose of this project is to provide a wrapper around [wkhtmltopdf](https://wkhtmltopdf.org/) html to pdf generator.

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

```csharp
    // register with di
    services.AddPdfGenerator(genConfig: options =>
    {
        // allows for including wkhtmltopdf inside of the project
        options.UseEmbedded = false;
    });


    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get(string url, CancellationToken cancellationToken)
    {
        using var client = new WebClient();
        var html = await client.DownloadStringTaskAsync(url);

        var pdf = await _pdfGenerator.GetAsync(html, cancellationToken);

        var pdfStream = new MemoryStream();
        pdfStream.Write(pdf, 0, pdf.Length);
        pdfStream.Position = 0;
        return new FileStreamResult(pdfStream, "application/pdf");
    }
```

### Sample Docker file

```bash
    FROM mcr.microsoft.com/dotnet/aspnet:6.0  AS base
    # add this install for html to pdf utility
    RUN apt-get update -qq && apt-get -y install libgdiplus libc6-dev
    WORKDIR /app
    EXPOSE 80
    EXPOSE 443

    FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
    WORKDIR /src
    COPY ["build", "build/"]
    COPY ["Directory.Build.props", "."]
    COPY ["Directory.Build.targets", "."]

    COPY ["src/Bet.Extensions.Wkhtmltopdf/Bet.Extensions.Wkhtmltopdf.csproj", "src/Bet.Extensions.Wkhtmltopdf/"]
    COPY ["src/Bet.Extensions.Wkhtmltopdf.WebApi/Bet.Extensions.Wkhtmltopdf.WebApi.csproj", "src/Bet.Extensions.Wkhtmltopdf.WebApi/"]
    RUN dotnet restore "src/Bet.Extensions.Wkhtmltopdf.WebApi/Bet.Extensions.Wkhtmltopdf.WebApi.csproj"
    COPY . .
    WORKDIR "/src/src/Bet.Extensions.Wkhtmltopdf.WebApi"
    RUN dotnet build "Bet.Extensions.Wkhtmltopdf.WebApi.csproj" -c Release -o /app/build

    FROM build AS publish
    RUN dotnet publish "Bet.Extensions.Wkhtmltopdf.WebApi.csproj" -c Release -o /app/publish

    FROM base AS final
    WORKDIR /app
    COPY --from=publish /app/publish .

    # add this for resource being able to be copied and run.
    RUN chmod 777 /app/

    ENTRYPOINT ["dotnet", "Bet.Extensions.Wkhtmltopdf.WebApi.dll"]
```
