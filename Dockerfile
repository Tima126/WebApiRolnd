
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Development


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src


COPY ["webapirold/webapirold.csproj", "webapirold/"]
COPY ["BusinessLogic/BusinessLogic.csproj", "BusinessLogic/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]


RUN dotnet restore "webapirold/webapirold.csproj"


COPY . .


WORKDIR "/src/webapirold"
RUN dotnet build "webapirold.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "webapirold.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .


ENTRYPOINT ["dotnet", "webapirold.dll"]