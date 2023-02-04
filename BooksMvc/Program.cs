using BooksMvc.Models;
using BooksMvc.Repository;
using DbBooks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("SqlServer");

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DbBook>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddScoped<BookRepository>();

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<DbBook>()
    .AddDefaultTokenProviders();

//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.Configure<IdentityOptions>(opt =>
{
#if DEBUG
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 3;
    opt.Password.RequiredUniqueChars = 3;
#endif

    opt.User.RequireUniqueEmail = false;
    opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIGKLMNOPQRSTUVWXYZ1234567890";

    opt.Lockout.AllowedForNewUsers = false;
    opt.Lockout.MaxFailedAccessAttempts = 10;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
});

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.Name = "WebStore";
    opt.Cookie.HttpOnly = true;

    opt.ExpireTimeSpan = TimeSpan.FromDays(10);

    opt.LoginPath = "/Account/Login";
    opt.LogoutPath = "/Account/Logout";
    opt.AccessDeniedPath = "/Account/AccessDenied";

    opt.SlidingExpiration = true;
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var Db = scope.ServiceProvider.GetRequiredService<DbBook>();
    //Db.Database.EnsureDeleted();
    // Db.Database.EnsureCreated();
    Db.Database.Migrate();
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();
    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();

