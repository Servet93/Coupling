using LooseCouplingWebApplication;
using LooseCouplingWebApplication.Models;
using LooseCouplingWebApplication.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddSingleton<ICryptography, DesOperation>();
builder.Services.AddSingleton<IService<UserData>>(x => new UserService(x.GetRequiredService<ICryptography>(),
    AppSettings.DesUserEmailSecretKey, AppSettings.DesUserPasswordSecretKey));

builder.Services.AddSingleton<IService<VehicleData>>(x => new VehicleService(x.GetRequiredService<ICryptography>(),
    AppSettings.VehicleIdentificationNumberSecretKey));

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
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();