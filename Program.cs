using FreshFarmMarket_210705C.Model;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AuthDbContext>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();

builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options
=>
{
    options.Cookie.Name = "MyCookieAuth";
    
    // Redirect to AccessDenied Page
    options.AccessDeniedPath = "/Account/AccessDenied"; 
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Session Timeout
    options.Cookie.Name = ".AspNetCore.Identity.Application";
    options.ExpireTimeSpan = TimeSpan.FromSeconds(30);
    options.SlidingExpiration = true; 
});

builder.Services.Configure<IdentityOptions>(options =>
{
    // Lockout Settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(60);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MustBelongToHRDepartment", 
        policy => policy.RequireClaim("Department", "HR"));
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MustBelongToHRDepartment",
 policy => policy.RequireClaim("Department", "HR"));
});


builder.Services.ConfigureApplicationCookie(Config =>
{
    Config.LoginPath = "/Login";
});

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

app.UseStatusCodePagesWithRedirects("/errors/{0}");

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
