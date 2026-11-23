FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY Portfolio/Portfolio.Web/Portfolio.Web.csproj Portfolio/Portfolio.Web/
RUN dotnet restore Portfolio/Portfolio.Web/Portfolio.Web.csproj
COPY Portfolio/ Portfolio/
RUN dotnet publish Portfolio/Portfolio.Web/Portfolio.Web.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080
USER app
ENTRYPOINT ["dotnet", "Portfolio.Web.dll"]
