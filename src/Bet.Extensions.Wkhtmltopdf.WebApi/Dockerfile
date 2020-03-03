#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
# add this install for html to pdf utility
RUN apt-get update -qq && apt-get -y install libgdiplus libc6-dev
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Bet.Extensions.Wkhtmltopdf.WebApi.csproj", ""]
RUN dotnet restore "./Bet.Extensions.Wkhtmltopdf.WebApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Bet.Extensions.Wkhtmltopdf.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bet.Extensions.Wkhtmltopdf.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# add this for resource being able to be copied and run.
RUN chmod 777 /app/

ENTRYPOINT ["dotnet", "Bet.Extensions.Wkhtmltopdf.WebApi.dll"]
