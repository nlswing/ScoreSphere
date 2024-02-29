using ScoreSphere.Models;
using Microsoft.EntityFrameworkCore;
using ScoreSphere.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add database context
builder.Services.AddDbContext<ScoreSphereDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inject and use ScoreSphereService in controllers - register it with ASP.NET Core Dependency Injection
builder.Services.AddScoped<IScoreSphereService, ScoreSphereService>();

// Configure server-side SignalR
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting(); // Make sure UseRouting is called before UseAuthorization

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllers();

    endpoints.MapHub<ScoreSphere.Hubs.MatchCenterHub>("/matchcenterhub");
});

app.Run();
