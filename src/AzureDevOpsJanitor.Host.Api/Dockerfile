#Setup build image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

#Set container filesystem to /build (and create folder if it doesnt exist)
WORKDIR /build

#Copy files to container file system.
COPY ./ ./src/

#Set workdir to current project folder
WORKDIR /build/src

#Restore csproj packages.
RUN dotnet restore

#RUN dotnet test Tests/AcceptanceTests/AcceptanceTests.csproj 

#Compile source code using standard Release profile
RUN dotnet publish -c Release -o /build/out src/ResourceProvisioning.Broker.Host.Api/ResourceProvisioning.Broker.Host.Api.csproj

#Setup final container images.
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app

EXPOSE 50900

ENV ASPNETCORE_URLS="http://*:50900"

#Copy binaries from publish container to final container
COPY --from=build-env /build/out .

#Run dotnet executable
ENTRYPOINT ["dotnet", "ResourceProvisioning.Broker.Host.Api.dll"]