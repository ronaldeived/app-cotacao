FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CotationConsumer/CotationConsumer.csproj", "CotationConsumer/"]
RUN dotnet restore "CotationConsumer/CotationConsumer.csproj"
COPY . .
WORKDIR "/src/CotationConsumer"
RUN dotnet build "CotationConsumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CotationConsumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CotationConsumer.dll"]
