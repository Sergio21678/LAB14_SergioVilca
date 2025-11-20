# Usa la imagen base de .NET 9 para ejecutar la app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Usa la imagen SDK de .NET 9 para construir la app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar los archivos de proyecto (csproj) y restaurar dependencias
COPY ["Ejercicio13_SergioVilca/Ejercicio13_SergioVilca.csproj", "Ejercicio13_SergioVilca/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

RUN dotnet restore "Ejercicio13_SergioVilca/Ejercicio13_SergioVilca.csproj"

# Copiar todo el código fuente y construir
COPY . .
WORKDIR "/src/Ejercicio13_SergioVilca"
RUN dotnet build "Ejercicio13_SergioVilca.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicar la aplicación
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Ejercicio13_SergioVilca.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Configurar la imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ejercicio13_SergioVilca.dll"]