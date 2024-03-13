using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SoftwareTest.Codes;
using SoftwareTest.Components;
using SoftwareTest.Components.Account;
using SoftwareTest.Data;
using SoftwareTest.Models;
using System.Runtime.InteropServices;
using System.Security.Authentication;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddScoped<TodolistContext>();
builder.Services.AddScoped<CprService>();
builder.Services.AddScoped<HashingHandlers>();
builder.Services.AddScoped<SymetricEncrypting>();
builder.Services.AddScoped<AsymetricEncryptHandler>();
builder.Services.AddScoped<TodoListService>();


builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
var connectionString = builder.Configuration.GetConnectionString("MockDbConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlite(connectionString));
}

else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
    
var connectionTODO = builder.Configuration.GetConnectionString("TodoConnection") ?? throw new InvalidOperationException("Connection string 'TodoConnection' not found.");
builder.Services.AddDbContext<TodolistContext>(options =>
    options.UseSqlServer(connectionTODO));

}






builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AuthenticatedUser" , policy =>
    {
        policy.RequireAuthenticatedUser();
    });
});

builder.WebHost.UseKestrel((context, serverOptions) =>
{
    serverOptions.Configure(context.Configuration.GetSection("Kestrel"))
    .Endpoint("HTTPS", listenOptions =>
    {
        listenOptions.HttpsOptions.SslProtocols = SslProtocols.Tls12;
    });
});

string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
userFolder = Path.Combine(userFolder, ".aspnet");
userFolder = Path.Combine(userFolder, "https");
userFolder = Path.Combine(userFolder, "Daniel.pfx");
builder.Configuration.GetSection("Kestrel:Endpoints:HTTPS:Certificate:Path").Value = userFolder;

string kestrelSecretPassword = builder.Configuration.GetValue<string>("mySecretKestrelPassword");
builder.Configuration.GetSection("Kestrel:Endpoints:HTTPS:Certificate:Password").Value = kestrelSecretPassword;

builder.Services.AddDataProtection();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
