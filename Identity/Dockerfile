# build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
COPY . .

RUN dotnet restore "PresentationLayer" 
RUN dotnet publish "PresentationLayer" -c debug -o /app --no-restore

# serve stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as serve
WORKDIR /app
COPY --from=build /app ./

COPY PresentationLayer/.env /app/.env

EXPOSE 80
ENTRYPOINT [ "dotnet", "PresentationLayer.dll" ]