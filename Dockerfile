# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos de projeto e restaura dependências
COPY ["DesafioDevNet8.sln", "./"]
COPY ["src/DesafioDevNet8.Domain/DesafioDevNet8.Domain.csproj", "src/DesafioDevNet8.Domain/"]
COPY ["src/DesafioDevNet8.Application/DesafioDevNet8.Application.csproj", "src/DesafioDevNet8.Application/"]
COPY ["src/DesafioDevNet8.Infrastructure/DesafioDevNet8.Infrastructure.csproj", "src/DesafioDevNet8.Infrastructure/"]
COPY ["src/DesafioDevNet8.Presentation/DesafioDevNet8.Presentation.csproj", "src/DesafioDevNet8.Presentation/"]

RUN dotnet restore "DesafioDevNet8.sln"

# Copia o código-fonte e compila
COPY . .
WORKDIR "/src/src/DesafioDevNet8.Presentation"
RUN dotnet build "DesafioDevNet8.Presentation.csproj" -c Release -o /app/build

# Publish Stage
FROM build AS publish
RUN dotnet publish "DesafioDevNet8.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime Stage
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DesafioDevNet8.Presentation.dll"]
