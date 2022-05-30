FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY RelayService.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS publish
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "RelayService.dll"]