FROM mcr.microsoft.com/devcontainers/dotnet:8.0

# Install the MySQL client so you can run the 'mysql' command
RUN apt-get update && apt-get install -y default-mysql-client