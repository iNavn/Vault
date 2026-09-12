namespace Vault.Infrastructure.Repositories;

using Vault.Application.Repositories;
using Vault.Domain.Entities;

public sealed class InMemoryCategoryRepository : ICategoryRepository
{
    private readonly List<Category> _items = [];

    public Task<Category?> GetByIdAsync(Guid id)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        return Task.FromResult(item);
    }

    public Task AddAsync(Category item)
    {
        _items.Add(item);
        return Task.CompletedTask;
    }
}
