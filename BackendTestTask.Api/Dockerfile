FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BackendTestTask.Api/BackendTestTask.Api.csproj", "BackendTestTask.Api/"]
RUN dotnet restore "BackendTestTask.Api/BackendTestTask.Api.csproj"
COPY . .
WORKDIR "/src/BackendTestTask.Api"
RUN dotnet build "BackendTestTask.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BackendTestTask.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BackendTestTask.Api.dll"]
