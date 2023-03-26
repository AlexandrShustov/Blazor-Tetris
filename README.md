# Tetris
The main purpose of this project is to touch Blazor framework.

Tetris is a popular game where you have to places (figures) in the right place, so they make a rows which disappear and give you points.
You can also play an online version of this game - you have to create a game, wait for an opponent to join, and then your goal is to place tetrominos (figures) in a way, so they make rows which would disapper and speed up the game of **your opponent**. The first who reaches the top of the field lose the game.

To try just clone the repo and launch it localy, or open the root of the repo in PowerShell and execute ``` docker build . -t tetrisapp``` and then
```docker run --rm -it -p 8000:80 -p 8001:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=8001 -e ASPNETCORE_Kestrel__Certificates__Default__Password="<put a password>" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -v $env:USERPROFILE\.aspnet\https:/https/ tetrisapp``` 
([according to the Microsoft guide about running a image over https](https://learn.microsoft.com/en-us/aspnet/core/security/docker-https?view=aspnetcore-7.0#windows-using-linux-containers)). It is not going to work if you don't configure your secrets and a dev certificate.

Stuff used: SignalR, MudBlazor, a bit of Bootsrap, ASP.NET Core 6, poor CSS knowledge
