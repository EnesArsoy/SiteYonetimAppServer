using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SiteYonetimApp.Services;
using SiteYonetimAppServer.Data;
using SiteYonetimAppServer.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<AuthenticationService>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
//QuestPDF.Settings.License = LicenseType.Community;

// HttpClient yapýlandýrmasýný ekliyoruz
builder.Services.AddHttpClient<IGeneralService, GeneralService>();  // Burada HttpClient yapýlandýrmasý

// Genel servisi Scoped olarak kaydediyoruz
builder.Services.AddScoped<IGeneralService, GeneralService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
