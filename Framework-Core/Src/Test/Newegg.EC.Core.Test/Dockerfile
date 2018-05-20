FROM docker.neg/base/aspnetcore:2.0.0-jessie

# Set the Working Directory
WORKDIR /app

# Configure the listening port to 5000
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT Development

EXPOSE 5000

# Copy the app
COPY . /app

# Start the app
ENTRYPOINT dotnet Newegg.EC.Core.Test.dll
