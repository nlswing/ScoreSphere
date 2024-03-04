using ScoreSphere.Models;
using Microsoft.EntityFrameworkCore;
using ScoreSphere.Hubs;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(600);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add database context
builder.Services.AddDbContext<ScoreSphereDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// Inject and use ScoreSphereService in controllers - register it with ASp.NET Core Dependency Injection
builder.Services.AddScoped<IScoreSphereService, ScoreSphereService>();

builder.Services.AddSignalR();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IScoreSphereService, ScoreSphereService>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(600);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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
        app.UseSession();

    endpoints.MapControllers();

    endpoints.MapHub<MatchCenterHub>("/matchcenterhub");
});








app.Run();
