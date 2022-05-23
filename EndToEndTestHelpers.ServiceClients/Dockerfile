FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EndToEndTestHelpers/EndToEndTestHelpers.csproj", "EndToEndTestHelpers/"]
RUN dotnet restore "EndToEndTestHelpers/EndToEndTestHelpers.csproj"
COPY . .
WORKDIR "/src/EndToEndTestHelpers"
RUN dotnet build "EndToEndTestHelpers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EndToEndTestHelpers.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EndToEndTestHelpers.dll"]
