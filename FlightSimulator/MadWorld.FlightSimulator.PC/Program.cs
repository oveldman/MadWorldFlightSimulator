using MadWorld.FlightSimulator.PC.Hubs;
using MadWorld.FlightSimulator.Connector;
using MadWorld.FlightSimulator.PC.Application;
using MadWorld.FlightSimulator.PC;
using MadWorld.FlightSimulator.Simulator;

var builder = WebApplication.CreateBuilder(args);
var useSimulator = bool.Parse(builder
    .Configuration
    .GetSection("DebugSettings:UseSimulator")
    .Value!);


builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSignalR();

builder.Services.AddPC();
builder.Services.AddApplication();

if (useSimulator)
{
    builder.Services.AddSimulator();   
}
else
{
    builder.Services.AddSimClient();   
}

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapHub<PanelHub>("/PanelHub");
app.MapHub<TestHub>("/TestHub");

app.MapFallbackToPage("/_Host");

app.Run();
