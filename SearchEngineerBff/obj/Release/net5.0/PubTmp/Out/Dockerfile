FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
COPY . /app

ENV SearchEngineerUrl http://apigateway:80

ENTRYPOINT ["dotnet", "SearchEngineerBff.dll"]