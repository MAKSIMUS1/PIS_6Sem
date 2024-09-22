using lab3b_vd.Data;
using lab3b_vd.Middlewares;
using lab3b_vd.Models;
using lab3b_vd.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(connectionString));

builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddDistributedMemoryCache();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = new PathString("/Admin/AccessError");
    options.LoginPath = new PathString("/Admin/SignIn");
    options.LogoutPath = new PathString("/Admin/SignOut");
    options.Cookie.Name = "Lab3b";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
});

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "Lab3b.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = false;
    options.Cookie.IsEssential = false;
});

builder.Services.AddScoped<WsRefService>();
builder.Services.AddScoped<WsRefCommentsService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Admin/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

//app.UseCheckOnExists();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.UseDefaultRole("Administrator", "User", "Guest", "Owner");
app.UseDefaultUser("Administrator", "123123123", ["Administrator", "User"]).Wait();
app.UseDefaultUser("Owner", "123123123", ["Owner", "User"]).Wait();
app.UseDefaultUser("Guest", "123123123", ["Guest", "User"]).Wait();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=WsRef}/{action=Index}/{id?}"
    );
app.Run();
