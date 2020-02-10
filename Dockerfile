FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-stage
WORKDIR /SpeedTestApi

COPY /SpeedTestApi/SpeedTestApi.csproj ./
RUN dotnet restore

COPY /SpeedTestApi ./
RUN dotnet publish \
    --output ./PublishedApp \
    --configuration Release \
    --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
LABEL repository="github.com/k8s-101/speedtest-api"
WORKDIR /SpeedTestApi

COPY --from=build-stage /SpeedTestApi/PublishedApp ./
ENTRYPOINT ["dotnet", "SpeedTestApi.dll"]
