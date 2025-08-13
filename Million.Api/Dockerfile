# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and restore dependencies
COPY ["Million.Api/Million.Api.csproj", "Million.Api/"]
RUN dotnet restore "Million.Api/Million.Api.csproj"

# Copy the rest of the code
COPY . .
WORKDIR "/src/Million.Api"

# Build and publish
RUN dotnet publish "Million.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Million.Api.dll"]
