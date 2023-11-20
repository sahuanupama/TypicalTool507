using Microsoft.EntityFrameworkCore;
using TypicalTechTools.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Configuring database connection
var connString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<TypicalToolDBContext>(options =>
{
    options.UseSqlServer(connString);
});


builder.Services.AddSession(c =>
{
    c.IdleTimeout = TimeSpan.FromSeconds(120);
    c.Cookie.HttpOnly = true;
    c.Cookie.IsEssential = true;
    //Sets whether the cookie can be used outside the domain where it was created.
    c.Cookie.SameSite = SameSiteMode.Strict;
    //Set whether the cookie m,ust be sent via https(always) or via the 
    //same methofd it was sent(Http or Https)
    c.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSingleton<CsvParser>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
    {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    }

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
