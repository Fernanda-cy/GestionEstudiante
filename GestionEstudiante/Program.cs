using GestionEstudiante.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 1. CONFIGURACIÓN DE SESIÓN
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // La sesión dura 30 minutos
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 2. ACCESOR HTTP (CRÍTICO: Necesario para el botón de Cerrar Sesión en la vista)
builder.Services.AddHttpContextAccessor();

// 3. INYECCIÓN DE DEPENDENCIAS (Tu Repositorio)
builder.Services.AddScoped<EstudianteRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 4. ACTIVAR SESIÓN (IMPORTANTE: Debe ir antes de MapControllerRoute)
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Login}/{id?}"); // Arranca en el Login

app.Run();

