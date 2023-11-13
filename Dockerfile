# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app

# Copy the project file and restore dependencies
COPY *.csproj .
RUN dotnet restore

# Copy the remaining files
COPY . .

# Build the application
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

WORKDIR /app

# Copy the built application
COPY --from=build /app/out .

# Start the application
CMD ["dotnet", "GCCWebAPI.dll"]