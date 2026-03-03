# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY HelloWorldMvc.sln ./
COPY WebApplication/WebApplication.csproj WebApplication/
RUN dotnet restore HelloWorldMvc.sln

COPY . .
RUN dotnet publish WebApplication/WebApplication.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "WebApplication.dll"]
