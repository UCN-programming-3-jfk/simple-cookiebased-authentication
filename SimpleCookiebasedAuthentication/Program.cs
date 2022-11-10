using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleCookiebasedAuthentication.DataAccess;

var builder = WebApplication.CreateBuilder(args);

//This sets up the cookie authentication scheme,
//and sets the default paths for
// - where unathenticated users are sent: LoginPath
// - where authenticated users trying to access a place which they don't have the role for: AccessDeniedPath
// - where the log out action sends them: LogoutPath
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
       options =>
       {
           options.LoginPath = "/Accounts/Login";
           options.AccessDeniedPath = "/Accounts/AccessDenied";
           options.LogoutPath = "/Accounts/LogOut";
       });
builder.Services.AddControllersWithViews();

//register the dependency on a Data Access Layer
//where the ASP.NET framework can see which classes to instantiate
//when controllers have to be instantiated
builder.Services.AddSingleton<IUserProvider, UserProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//added to enable authentication
app.UseAuthentication();    
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.Run();
