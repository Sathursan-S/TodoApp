# Use the .NET SDK to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything and restore dependencies
COPY . ./
RUN dotnet restore

# Build and publish the application
RUN dotnet publish -c Release -o out

# Use the .NET runtime for the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App

# Copy the published output and the database file from the build environment
COPY --from=build-env /App/out .
COPY --from=build-env /App/*.db .

# Apply migrations
ENTRYPOINT ["sh", "-c", "dotnet TodoApi.dll && dotnet ef database update"]
