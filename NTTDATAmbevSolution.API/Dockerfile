# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["NTTDATAmbevSolution.API/NTTDATAmbevSolution.API.csproj", "NTTDATAmbevSolution.API/"]
COPY ["NTTDATAmbevSolution.Application/NTTDATAmbevSolution.Application.csproj", "NTTDATAmbevSolution.Application/"]
COPY ["NTTDATAmbevSolution.Domain/NTTDATAmbevSolution.Domain.csproj", "NTTDATAmbevSolution.Domain/"]
COPY ["NTTDATAmbevSolution.Infrastructure/NTTDATAmbevSolution.Infrastructure.csproj", "NTTDATAmbevSolution.Infrastructure/"]

RUN dotnet restore "NTTDATAmbevSolution.API/NTTDATAmbevSolution.API.csproj"

COPY . .
WORKDIR "/src/NTTDATAmbevSolution.API"
RUN dotnet publish "NTTDATAmbevSolution.API.csproj" -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "NTTDATAmbevSolution.API.dll"]
