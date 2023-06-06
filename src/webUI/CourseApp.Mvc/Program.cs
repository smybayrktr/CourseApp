using CourseApp.Infrastructure.Data;
using CourseApp.Infrastructure.Repositories;
using CourseApp.Mvc.Extensions;
using CourseApp.Services;
using CourseApp.Services.Mappings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15); //15 dakika hiçbir işlem yapmazsa session düşsün.
});

var connectionString = builder.Configuration.GetConnectionString("db");
builder.Services.AddInjections(connectionString);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt =>
                {
                    opt.LoginPath = "/Users/Login";
                    opt.AccessDeniedPath = "/Users/AccessDenied"; //Üye ama yetkisi yok
                    opt.ReturnUrlParameter = "gidilecekSayfa";
                });
builder.Services.AddMemoryCache(); //Pipline a eklemedik.HttpRequestte bir operasyon yapmıyor. 
builder.Services.AddResponseCaching(options =>
{
    options.SizeLimit=100000;//Sunucudan istemciye gönderilen yanıtın 100mblik limit



});//Doğrudan Html response unu istemcide belli süre tutmasını söyler.

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<CourseDbContext>();
context.Database.EnsureCreated();
SeedData.SeedDatabase(context);

app.UseHttpsRedirection();
app.UseResponseCaching();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

