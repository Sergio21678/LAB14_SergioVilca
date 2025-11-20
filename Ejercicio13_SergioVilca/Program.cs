// Importar los namespaces de nuestras capas
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Registrar Servicios ---

// Añadir los servicios de la capa Application (MediatR)
builder.Services.AddApplication();

// Añadir los servicios de la capa Infrastructure (DbContext)
builder.Services.AddInfrastructure(builder.Configuration);

// Añadir servicios de Controladores
builder.Services.AddControllers();

// Añadir servicios de Swagger (Documentación)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Ejercicio13_SergioVilca API", Version = "v1" });
});


var app = builder.Build();

// --- 2. Configurar el Pipeline HTTP ---

if (app.Environment.IsDevelopment())
{
    // Usar Swagger solo en desarrollo
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ejercicio13_SergioVilca v1"));
}

app.UseHttpsRedirection();

// Habilitar el ruteo para los controladores
app.MapControllers();

app.Run();