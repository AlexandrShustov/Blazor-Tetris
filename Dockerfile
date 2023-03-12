FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app
COPY . ./files
WORKDIR ./files/Tetris.Blazor/Server
RUN dotnet workload restore
RUN dotnet build Tetris.Blazor.Server.csproj -c Debug -o /app/build
RUN dotnet publish Tetris.Blazor.Server.csproj -c Release -o /app/publish

FROM base as runtime
WORKDIR /app
COPY --from=build /app/publish/ /app
ENTRYPOINT [ "dotnet", "Tetris.Blazor.Server.dll" ]

#docker run --rm -it -p 8000:80 -p 8001:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=8001 -e ASPNETCORE_Kestrel__Certificates__Default__Password="<put a password>" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -v $env:USERPROFILE\.aspnet\https:/https/ myapp