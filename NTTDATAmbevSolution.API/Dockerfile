# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["ServiceControl.WebAPI/ServiceControl.WebAPI.csproj", "ServiceControl.WebAPI/"]
COPY ["ServiceControl.Application/ServiceControl.Application.csproj", "ServiceControl.Application/"]
COPY ["ServiceControl.Domain/ServiceControl.Domain.csproj", "ServiceControl.Domain/"]
COPY ["ServiceControl.Infrastructure/ServiceControl.Infrastructure.csproj", "ServiceControl.Infrastructure/"]

RUN dotnet restore "ServiceControl.WebAPI/ServiceControl.WebAPI.csproj"

COPY . .
WORKDIR "/src/ServiceControl.WebAPI"
RUN dotnet publish "ServiceControl.WebAPI.csproj" -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ServiceControl.WebAPI.dll"]
