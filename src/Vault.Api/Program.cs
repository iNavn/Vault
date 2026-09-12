using Vault.Application.Repositories;
using Vault.Domain.Entities;
using Vault.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IVaultItemRepository, InMemoryVaultItemRepository>();
builder.Services.AddSingleton<ICategoryRepository, InMemoryCategoryRepository>();

var app = builder.Build();

app.MapGet("/ping", saludoConFecha);
static string saludoConFecha()
{
    DateTime fechaUTC = DateTime.UtcNow;
    return $"Hola la fecha y hora actual es: {fechaUTC}";
}

app.MapGet("/saludos/{n:int}", GenerarSaludos);
static string GenerarSaludos(int n)
{
    var resultado = "";
    for (int i = 0; i < n; i++)
    {
        resultado += ConstruirSaludo(i);
    }
    return resultado;
}
static string ConstruirSaludo(int indice)
{
    return $"Saludo #{indice} generado a las {DateTime.UtcNow:HH:mm:ss}\n";
}

app.MapPost("/categories/{name}", async (string name, ICategoryRepository repo) =>
{
    var categoria = Category.Create(name);
    await repo.AddAsync(categoria);

    return Results.Ok(categoria.Id);
});

app.MapGet("/categories/{id:guid}", async (Guid id, ICategoryRepository repo) =>
{
    var categoria = await repo.GetByIdAsync(id);

    return categoria != null ? Results.Ok(categoria) : Results.NotFound();
});

app.Run();
