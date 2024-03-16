using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebCourierHub.Data;
using WebCourierHub.Support;
using WebCourierHub.Support.ApiConfig;

var builder = WebApplication.CreateBuilder(args);
// Cookie configuration for HTTP to support cookies with SameSite=None
builder.Services.ConfigureSameSiteNoneCookies();

// Cookie configuration for HTTPS
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
});

builder.Services.AddKeyedSingleton<IApiConfig, InternalApiConfig>("Internal");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<WebCourierHubDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("Database")));
}
else if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<WebCourierHubDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();