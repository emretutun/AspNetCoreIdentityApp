using AspNetCoreIdentityApp.Web.ClaimProvider;
using AspNetCoreIdentityApp.Web.Extenisons;
using AspNetCoreIdentityApp.Repository.Models;
using AspNetCoreIdentityApp.Core.OptionsModel;
using AspNetCoreIdentityApp.Core.PermissionsRoot;
using AspNetCoreIdentityApp.Web.Requirements;
using AspNetCoreIdentityApp.Web.Seeds;
using AspNetCoreIdentityApp.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using AspNetCoreIdentityApp.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"),options =>
    {
        options.MigrationsAssembly("AspNetCoreIdentityApp.Repository");
    });

});

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));


builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddIdentityWithExt();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IClaimsTransformation, UserClaimProvider>();
builder.Services.AddScoped<IAuthorizationHandler, ExchangeExpireRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, ViolenceRequirmentHandler>();
builder.Services.AddScoped<IMemberService, MemberService>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AnkaraPolicy", policy =>
    {
        policy.RequireClaim("city", "Ankara");
    });

});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("TekirdagPolicy", policy =>
    {
        policy.RequireClaim("city", "Tekirdað");
    });

});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IstanbulPolicy", policy =>
    {
        policy.RequireClaim("city", "Ýstanbul");
    });

});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ExchangePolicy", policy =>
    {
        policy.AddRequirements(new ExchangeExpireRequirement());
    });

});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ViolencePolicy", policy =>
    {
        policy.AddRequirements(new ViolenceRequirment() { ThresholdAge=18});
    });

});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OrderPermissionReadAndDelete", policy =>
    {
        policy.RequireClaim("Permission",Permissions.Order.Read);
        policy.RequireClaim("Permission",Permissions.Order.Delete);
        policy.RequireClaim("Permission",Permissions.Stock.Delete);


    });

});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OrderPermissionReadAndDelete", policy =>
    {
        policy.RequireClaim("Permission", Permissions.Order.Read);
        policy.RequireClaim("Permission", Permissions.Order.Delete);
        policy.RequireClaim("Permission", Permissions.Stock.Delete);


    });

});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Permissions.Order.Read", policy =>
    {
        policy.RequireClaim("Permission", Permissions.Order.Read);
    });

});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Permissions.Order.Delete", policy =>
    {
        policy.RequireClaim("Permission", Permissions.Order.Delete);
    });

});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Permissions.Stock.Delete", policy =>
    {
        policy.RequireClaim("Permission", Permissions.Stock.Delete);
    });

});





builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.FromMinutes(30);
});

builder.Services.ConfigureApplicationCookie(opt =>
{
    var cookieBuilder = new CookieBuilder();

    cookieBuilder.Name = "UdemyAppCookie";
    opt.LoginPath = new PathString("/Home/Signin");
    opt.LogoutPath = new PathString("/Member/logout");
    opt.AccessDeniedPath = new PathString("/Member/AccessDenied");
    opt.Cookie = cookieBuilder;
    opt.ExpireTimeSpan = TimeSpan.FromDays(60);
    opt.SlidingExpiration = true;
});



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

    await PermissionsSeed.Seed(roleManager);
}

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
