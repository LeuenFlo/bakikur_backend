# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app ./

# Create directories and copy images
RUN mkdir -p wwwroot/uploads && \
    mkdir -p images
COPY --from=build /src/images ./images/

# Set correct permissions
RUN chown -R 1000:1000 wwwroot/uploads && \
    chown -R 1000:1000 images

# Install openssl for key generation
RUN apt-get update && apt-get install -y openssl && rm -rf /var/lib/apt/lists/*

# Expose port
EXPOSE 80

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

# Generate JWT key and start application
CMD JWT_KEY=$(openssl rand -base64 48) && \
    export Jwt__Key="$JWT_KEY" && \
    dotnet BakikurBackend.dll 