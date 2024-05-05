using DSM.ERPerfect.BLL;
using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.DAL;
using DSM.ERPerfect.DAL.Interfaces;
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

builder.Services.AddTransient<IClienteBusiness, ClienteBusiness>();
builder.Services.AddTransient<IClienteRepository, ClienteRepository>();

builder.Services.AddTransient<IFacturaBusiness, FacturaBusiness>();
builder.Services.AddTransient<IFacturaRepository, FacturaRepository>();

builder.Services.AddTransient<IFacturaServicioBusiness, FacturaServicioBusiness>();
builder.Services.AddTransient<IFacturaServicioRepository, FacturaServicioRepository>();

builder.Services.AddTransient<IFormaPagoBusiness, FormaPagoBusiness>();
builder.Services.AddTransient<IFormaPagoRepository, FormaPagoRepository>();

builder.Services.AddTransient<IParametroBusiness, ParametroBusiness>();
builder.Services.AddTransient<IParametroRepository, ParametroRepository>();

builder.Services.AddTransient<IRolBusiness, RolBusiness>();
builder.Services.AddTransient<IRolRepository, RolRepository>();

builder.Services.AddTransient<IServicioBusiness, ServicioBusiness>();
builder.Services.AddTransient<IServicioRepository, ServicioRepository>();

builder.Services.AddTransient<ITarifaBusiness, TarifaBusiness>();
builder.Services.AddTransient<ITarifaRepository, TarifaRepository>();

builder.Services.AddTransient<ITarifaServicioBusiness, TarifaServicioBusiness>();
builder.Services.AddTransient<ITarifaServicioRepository, TarifaServicioRepository>();

builder.Services.AddTransient<IUsuarioBusiness, UsuarioBusiness>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

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
    pattern: "{controller=Login}/{action=Index}/{id?}");

// Lenguaje por defecto del proyecto
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("es-ES"),
});

app.Run();
