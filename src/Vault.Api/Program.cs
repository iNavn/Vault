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

app.MapPost("/vault-items/{categoryId:guid}/{siteName}/{username}/{encryptedPassword}",
    async (Guid categoryId, string siteName, string username, string encryptedPassword, IVaultItemRepository repo) =>
    {
        var item = VaultItem.Create(categoryId, siteName, username, encryptedPassword);
        await repo.AddAsync(item);

        return Results.Ok(item.Id);
    });

app.MapGet("/vault-items/weak-passwords/", async (IVaultItemRepository repo) =>
{
    var items = await repo.GetWeakPasswordItemsAsync();

    return items.Count != 0 ? Results.Ok(items) : Results.NotFound();
});

app.MapGet("/vault-items/{categoryId:guid}", async (Guid categoryId, IVaultItemRepository repo) =>
{
    var items = await repo.GetByCategoryAsync(categoryId);

    return items.Count != 0 ? Results.Ok(items) : Results.NotFound();
});

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
