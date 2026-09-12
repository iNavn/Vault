namespace Vault.Api.Endpoints;

using Vault.Application.Repositories;
using Vault.Domain.Entities;

public static class VaultItemEnpoints
{
    public static void MapVaultItemEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/vault-items").WithTags("VaultItems");

        group.MapGet("/vault-items/{categoryId:guid}/{siteName}/{username}/{encryptedPassword}",
            async (Guid categoryId, string siteName, string username, string encryptedPassword, IVaultItemRepository repo) =>
            {
                var item = VaultItem.Create(categoryId, siteName, username, encryptedPassword);
                await repo.AddAsync(item);

                return Results.Ok(item.Id);
            });

        group.MapGet("/vault-items/weak-passwords/", async (IVaultItemRepository repo) =>
        {
            var items = await repo.GetWeakPasswordItemsAsync();

            return items.Count != 0 ? Results.Ok(items) : Results.NotFound();
        });

        group.MapGet("/vault-items/{categoryId:guid}", async (Guid categoryId, IVaultItemRepository repo) =>
        {
            var items = await repo.GetByCategoryAsync(categoryId);

            return items.Count != 0 ? Results.Ok(items) : Results.NotFound();
        });
    }
}
