#FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
#WORKDIR /app
#COPY . .
#RUN dotnet workload restore
#RUN dotnet restore
#RUN dotnet publish -o /app/published-app

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app
COPY . ./files
WORKDIR ./files/Tetris.Blazor/Server
RUN dotnet workload restore
RUN dotnet build Tetris.Blazor.Server.csproj -c Debug -o /app/build

FROM base as runtime
WORKDIR /app
COPY --from=build /app/build/ /app
ENTRYPOINT [ "dotnet", "Tetris.Blazor.Server.dll" ]