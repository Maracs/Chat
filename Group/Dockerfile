# build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
COPY . .

RUN dotnet restore "WebApi" 
RUN dotnet publish "WebApi" -c debug -o /app --no-restore

# serve stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as serve
WORKDIR /app
COPY --from=build /app ./

COPY WebApi/.env /app/.env

EXPOSE 80
ENTRYPOINT [ "dotnet", "WebApi.dll" ]