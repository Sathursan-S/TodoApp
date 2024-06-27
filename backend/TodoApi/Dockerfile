# Use the official .NET SDK image to build and publish the application
FROM mcr.microsoft.com/dotnet/nightly/sdk:8.0 AS build

# Set the working directory
WORKDIR /src

COPY bin .

# Copy the project files
COPY *.csproj ./
RUN dotnet restore "TodoApi.csproj"

# Copy the remaining source code and build the application
COPY . .
RUN dotnet publish "TodoApi.csproj" -c Release -o /publish

# Use the official ASP.NET Core runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Set the working directory
WORKDIR /app

# Copy the published application from the build stage
COPY --from=build /publish .

# Expose the port that the application runs on
EXPOSE 5053

# Define the entry point for the container
ENTRYPOINT ["dotnet", "TodoApi.dll"]