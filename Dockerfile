# Include the dotnet SDK as the base to build our app
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /src

# Copy all files, restore dependencies, and build
COPY . .
RUN dotnet restore
RUN dotnet build -c Release -o /app/build

# Publish to publish folder
FROM build-env AS publish
RUN dotnet publish -c Release -o /app/publish

# Get base image for nginx, set the work directory, copy published files and nginx.conf 
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot /usr/local/webapp/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf