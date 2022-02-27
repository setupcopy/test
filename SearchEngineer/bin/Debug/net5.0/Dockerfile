FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

WORKDIR /SearchEngineer

EXPOSE 80
EXPOSE 443


COPY . /SearchEngineer

ENTRYPOINT ["dotnet", "SearchEngineer.dll"]