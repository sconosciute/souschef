FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

ENV DOTNET_USE_POLLING_FILE_WATCHER 1

USER $APP_UID
WORKDIR /code/app
EXPOSE 80
EXPOSE 443
EXPOSE 5293

COPY souschef-be.csproj .
COPY Properties/launchSettings.json .
COPY appsettings.json .
COPY appsettings.Development.json .

RUN dotnet restore

ENTRYPOINT dotnet watch

