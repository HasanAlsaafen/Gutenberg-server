# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0.7 AS build
WORKDIR /app

# Copy everything and restore
COPY . . 
RUN dotnet restore Gutenburg-Server.csproj
RUN dotnet publish Gutenburg-Server.csproj -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "Gutenburg-Server.dll"]
