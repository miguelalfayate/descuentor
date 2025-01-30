# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files and restore dependencies
COPY ["Descuentor.WebAssembly.csproj", "Descuentor.WebAssembly/"]
COPY ["../Descuentor.Shared/Descuentor.Shared.csproj", "Descuentor.Shared/"]
RUN dotnet restore "Descuentor.WebAssembly/Descuentor.WebAssembly.csproj"

# Copy the remaining files and build the app
COPY . .
WORKDIR "/src/Descuentor.WebAssembly"
RUN dotnet publish "Descuentor.WebAssembly.csproj" -c Release -o /app/publish

# Use the official Nginx image to serve the app
FROM nginx:alpine
WORKDIR /usr/share/nginx/html
COPY --from=build /app/publish/wwwroot .

# Copy the Nginx configuration file
COPY nginx.conf /etc/nginx/nginx.conf

# Expose port 80
EXPOSE 80

# Start Nginx
CMD ["nginx", "-g", "daemon off;"]