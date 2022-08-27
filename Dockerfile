FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8000
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY / /src
RUN dotnet build Listic.sln -c Release

RUN dotnet publish /src/API/API.csproj -c Release -o out

FROM base
WORKDIR /app
COPY --from=build /src/out .

RUN apk add icu-libs

ENTRYPOINT dotnet API.dll
