namespace Vault.Infrastructure.Repositories;

using Vault.Application.Repositories;
using Vault.Domain.Entities;

public sealed class InMemoryVaultItemRepository : IVaultItemRepository
{
    private readonly List<VaultItem> _items = [];

    public Task<VaultItem?> GetByIdAsync(Guid id)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        return Task.FromResult(item);
    }

    public Task<IReadOnlyList<VaultItem>> GetByCategoryAsync(Guid categoryId)
    {
        IReadOnlyList<VaultItem> result = _items
            .Where(i => i.CategoryId == categoryId)
            .OrderByDescending(i => i.CreatedAtUtc)
            .ToList();

        return Task.FromResult(result);
    }

    public Task AddAsync(VaultItem item)
    {
        _items.Add(item);
        return Task.CompletedTask;
    }
}
