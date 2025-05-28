# ---------- ETAPA 1: BUILD ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ./WebApiPermissions.sln ./
COPY ./WebApiPermissions/WebApiPermissions.csproj ./WebApiPermissions/
COPY ./Application/Application.csproj ./Application/
COPY ./Domian/Domain.csproj ./Domian/
COPY ./Infrastructure/Infrastructure.csproj ./Infrastructure/
COPY ./Test/Test.csproj ./Test/

RUN dotnet restore

COPY . ./

RUN dotnet publish ./WebApiPermissions/WebApiPermissions.csproj \
    --configuration Release \
    --output /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish ./

EXPOSE 80

ENTRYPOINT ["dotnet", "WebApiPermissions.dll"]
