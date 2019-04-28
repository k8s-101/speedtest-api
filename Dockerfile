FROM microsoft/dotnet:2.1-sdk AS build-stage
WORKDIR /SpeedTestApi

COPY /SpeedTestApi/SpeedTestApi.csproj ./
RUN dotnet restore

COPY /SpeedTestApi ./
RUN dotnet publish \
    --output /PublishedApp \
    --configuration Release

FROM microsoft/dotnet:2.1-aspnetcore-runtime
LABEL repository="github.com/k8s-101/speedtest-api"
WORKDIR /SpeedTestApi

COPY --from=build-stage /PublishedApp .
ENTRYPOINT ["dotnet", "SpeedTestApi.dll"]
