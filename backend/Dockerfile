# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .
RUN dotnet restore ./Blogigg.Presentation/Blogigg.Presentation.csproj

RUN dotnet publish ./Blogigg.Presentation/Blogigg.Presentation.csproj -c Release -o /out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .

EXPOSE 80

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "Blogigg.Presentation.dll"]
