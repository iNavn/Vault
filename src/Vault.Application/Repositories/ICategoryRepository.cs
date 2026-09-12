namespace Vault.Application.Repositories;

using Vault.Domain.Entities;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(Guid id);
    Task AddAsync(Category item);
}
