namespace Vault.Api.Endpoints;

using Vault.Application.Repositories;
using Vault.Domain.Entities;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/categories").WithTags("Categories");

        group.MapPost("/{name}", async (string name, ICategoryRepository repo) =>
        {
            var categoria = Category.Create(name);
            await repo.AddAsync(categoria);
            return Results.Ok(categoria.Id);
        });

        group.MapGet("/{id:guid}", async (Guid id, ICategoryRepository repo) =>
        {
            var categoria = await repo.GetByIdAsync(id);
            return categoria is not null ? Results.Ok(categoria) : Results.NotFound();
        });
    }
}
