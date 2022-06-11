﻿FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build

WORKDIR /code

COPY pet-api-athena.sln .

COPY src src
COPY tests tests

RUN dotnet restore -s https://api.nuget.org/v3/index.json

RUN dotnet test -c Release --no-restore
RUN dotnet publish --no-restore src/api/pet-api-athena.csproj -o /build -c Release

FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS runtime

WORKDIR /app
COPY --from=build /build /app
EXPOSE 80
CMD ["dotnet", "pet-api-athena.dll"]
