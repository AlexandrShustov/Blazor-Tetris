# Tetris
The main purpose of this project is to touch a bit Blazor framework.

Tetris is a popular game where you have to places (figures) in the right place, so they make a rows which disappear and give you points.
There is a feature that allows to play an online version of this game - you create a game, wait for an opponent to join, and then you have to make rows so they disapper and speed up the game of **your opponent**. The first who reach the top of the field lose the game.

To run the project you have to open the root of the repo in PowerShell and execute ` docker build . -t tetrisapp` and then
```docker run --rm -it -p 8000:80 -p 8001:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=8001 -e ASPNETCORE_Kestrel__Certificates__Default__Password="<put a password>" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -v $env:USERPROFILE\.aspnet\https:/https/ tetrisapp``` 
([according to the Microsoft guide about running a image over https](https://learn.microsoft.com/en-us/aspnet/core/security/docker-https?view=aspnetcore-7.0#windows-using-linux-containers))

Stuff used: SignalR, MudBlazor, a bit of Bootsrap, ASP.NET Core 6, poor CSS knowledge
