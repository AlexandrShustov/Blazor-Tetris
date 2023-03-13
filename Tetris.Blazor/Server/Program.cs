using FastEndpoints;
using Tetris.Blazor.Server;
using Tetris.Blazor.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IGameStorage, GameStorage>();
builder.WebHost.UseStaticWebAssets();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseFastEndpoints();
app.MapFallbackToFile("index.html");
app.MapHub<GameHub>("/gamehub");

app.Run();
