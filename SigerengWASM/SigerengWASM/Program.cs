using Microsoft.AspNetCore.Components.Authorization;
using SigerengWASM;
using SigerengWASM.Client.Model;
using SigerengWASM.Client.Pages;
using SigerengWASM.Components;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCascadingAuthenticationState();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddAuthentication(Constant.AuthName).AddCookie(Constant.AuthName, options => {
    options.Cookie.Name = Constant.AuthCookie;
    options.LoginPath = "/auth/login";//make page with this path
    options.AccessDeniedPath = "/auth/accessDenied";
    options.LogoutPath = "/auth/logout";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
    options.SlidingExpiration = true;
});
builder.Services.AddAuthorization();
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthStateProvider>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
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
app.UseAuthentication().UseAuthorization();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(SigerengWASM.Client._Imports).Assembly);

app.Run();
