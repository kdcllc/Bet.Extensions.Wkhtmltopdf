#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base

RUN apt-get update -qq && apt-get -y install libgdiplus libc6-dev

WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Bet.Extensions.Wkhtmltopdf.WorkJob/Bet.Extensions.Wkhtmltopdf.WorkJob.csproj", "src/Bet.Extensions.Wkhtmltopdf.WorkJob/"]
RUN dotnet restore "src/Bet.Extensions.Wkhtmltopdf.WorkJob/Bet.Extensions.Wkhtmltopdf.WorkJob.csproj"
COPY . .
WORKDIR "/src/src/Bet.Extensions.Wkhtmltopdf.WorkJob"
RUN dotnet build "Bet.Extensions.Wkhtmltopdf.WorkJob.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bet.Extensions.Wkhtmltopdf.WorkJob.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN chmod 777 /app/

ENTRYPOINT ["dotnet", "Bet.Extensions.Wkhtmltopdf.WorkJob.dll"]
