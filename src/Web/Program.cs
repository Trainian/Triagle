using ApplicationCore.Entities.Identity;
using Infrastructure;
using Infrastructure.Identity;
using Infrastructure.Web;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Web.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) =>
{
    lc.WriteTo.Console();
    lc.WriteTo.File("loginfo/", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information);
    lc.WriteTo.Seq("http://localhost:5341/");
});

Dependencies.ConfigureServices(builder.Configuration, builder.Services);

// Disable Required not Nullable classes
builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

builder.Services.AddDefaultIdentity<WebUser>(options =>
    {
        options.SignIn.RequireConfirmedEmail = false;
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityWebContext>();

// Add services to the container.
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Administrator"));
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeAreaFolder("Administration", "/", "RequireAdministratorRole");
});
builder.Services.AddWebServices(builder.Configuration);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAuthentication().AddYandex(options =>
{
    options.ClientId = "6f683f7691c2472ca8437a4aae570db9";
    options.ClientSecret = "9348d83535e84cf79e64dfd14c2041f0";
    options.CallbackPath = "/signin-yandex";
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var scopeProvider = scope.ServiceProvider;
    try
    {
        var identityWebContext = scopeProvider.GetRequiredService<IdentityWebContext>();
        var userManager = scopeProvider.GetRequiredService<UserManager<WebUser>>();
        var roleManager = scopeProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await IdentityWebContextSeed.SeedAsync(identityWebContext, userManager, roleManager);

        var webContext = scopeProvider.GetRequiredService<WebContext>();
        await WebContextSeed.SeedAsync(webContext);
    }
    catch (Exception ex)
    {
        app.Logger.LogWarning(ex, "��������� ��������� �������� ������� � ���� ������");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseDeveloperExceptionPage();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.Run();
