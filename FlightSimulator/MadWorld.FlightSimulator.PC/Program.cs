using MadWorld.FlightSimulator.PC.Hubs;
using MadWorld.FlightSimulator.Connector;
using MadWorld.FlightSimulator.PC.Application;
using MadWorld.FlightSimulator.PC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSignalR();

builder.Services.AddPC();
builder.Services.AddApplication();
builder.Services.AddSimClient();

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
