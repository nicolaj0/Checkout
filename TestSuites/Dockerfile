FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestSuites/TestSuites.csproj", "TestSuites/"]
RUN dotnet restore "TestSuites/TestSuites.csproj"
COPY . .
WORKDIR "/src/TestSuites"
RUN dotnet build "TestSuites.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestSuites.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestSuites.dll"]
