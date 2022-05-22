FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Checkout.Bank/Checkout.Bank.csproj", "Checkout.Bank/"]
RUN dotnet restore "Checkout.Bank/Checkout.Bank.csproj"
COPY . .
WORKDIR "/src/Checkout.Bank"
RUN dotnet build "Checkout.Bank.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Checkout.Bank.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Checkout.Bank.dll"]
