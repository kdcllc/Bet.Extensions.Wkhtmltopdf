#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# run from the root of the project!
# docker build --pull --rm -f "src\Bet.Extensions.Wkhtmltopdf.WebApi\Dockerfile" -t betextensionswkhtmltopdf:latest .
# docker run --rm -it  -p 443:443/tcp -p 3000:80/tcp betextensionswkhtmltopdf:latest

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
