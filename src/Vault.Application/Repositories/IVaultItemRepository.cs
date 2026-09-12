namespace Vault.Application.Repositories;

using Vault.Domain.Entities;

public interface IVaultItemRepository
{
    Task<VaultItem?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<VaultItem>> GetByCategoryAsync(Guid categoryId);
    Task AddAsync(VaultItem item);
}
