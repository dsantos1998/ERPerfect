using Microsoft.AspNetCore.Localization;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
    .AddSessionStateTempDataProvider();

builder.Services.AddSession();

#region Serilog

var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

#endregion

#region Inyección de dependencias (Dependency Injection - DI)

//builder.Services.AddTransient<IBannedIPsBusiness, BannedIPsBusiness>();
//builder.Services.AddTransient<IBannedIPsQueryBusiness, BannedIPsQueryBusiness>();
//builder.Services.AddTransient<IBannedIPsRepository, BannedIPsRepository>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();   // Habilita TempData[""] y Session[""]

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Lenguaje por defecto del proyecto
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("es-ES"),
});

app.Run();
