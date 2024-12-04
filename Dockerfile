# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the CSPROJ file and restore dependencies
COPY ["dietologist-backend.csproj", "./"]
RUN dotnet restore "./dietologist-backend.csproj"

# Copy the rest of the application code
COPY . ./
RUN dotnet publish -c Release -o out

# Stage 2: Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./

# Expose port 80
EXPOSE 80

# Set the entry point for the container
ENTRYPOINT ["dotnet", "dietologist-backend.dll"]