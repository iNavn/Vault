namespace Vault.Domain.Entities;

public sealed class VaultItem
{
    public Guid Id { get; }
    public Guid CategoryId { get; private set; }
    public string SiteName { get; private set; }
    public string Username { get; private set; }
    public string EncryptedPassword { get; private set; }
    public DateTime CreatedAtUtc { get; }

    private VaultItem(Guid id, Guid categoryId, string siteName, string username, string encryptedPassword, DateTime createdAtUtc)
    {
        Id = id;
        CategoryId = categoryId;
        SiteName = siteName;
        Username = username;
        EncryptedPassword = encryptedPassword;
        CreatedAtUtc = createdAtUtc;
    }

    public static VaultItem Create(Guid categoryId, string siteName, string username, string encryptedPassword)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(siteName);
        ArgumentException.ThrowIfNullOrWhiteSpace(username);
        ArgumentException.ThrowIfNullOrWhiteSpace(encryptedPassword);

        return new VaultItem(Guid.NewGuid(), categoryId, siteName.Trim(), username.Trim(), encryptedPassword, DateTime.UtcNow);
    }

    public override bool Equals(object? obj) => obj is VaultItem other && Id == other.Id;
    public override int GetHashCode() => Id.GetHashCode();
}
