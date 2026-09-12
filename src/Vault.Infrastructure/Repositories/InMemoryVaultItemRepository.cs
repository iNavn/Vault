namespace Vault.Infrastructure.Repositories;

using Vault.Application.Repositories;
using Vault.Domain.Entities;
using Vault.Domain.Services;
using Vault.Domain.ValueObjects;

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

    // LIMITACIÓN CONOCIDA: evalúa EncryptedPassword asumiendo que hoy sigue siendo texto plano.
    // Cuando Vault.Infrastructure implemente cifrado real (pendiente), este método necesitará
    // desencriptar primero, o la evaluación de fortaleza deberá moverse al momento de creación
    // del VaultItem, antes de cifrar.
    public Task<IReadOnlyList<VaultItem>> GetWeakPasswordItemsAsync()
    {
        IReadOnlyList<VaultItem> result = _items
            .Where(i => PasswordStrengthEvaluator.Evaluate(i.EncryptedPassword).Level == PasswordStrengthLevel.Weak)
            .OrderByDescending(i => i.CreatedAtUtc)
            .ToList();

        return Task.FromResult(result);
    }
}
