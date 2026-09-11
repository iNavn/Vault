namespace Vault.Domain.Entities;

public sealed class Category
{
    public Guid Id { get; }
    public string Name { get; private set; }

    private Category(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Category Create(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (name.Length > 50)
            throw new ArgumentException("El nombre de la categoría no puede superar 50 caracteres.", nameof(name));

        return new Category(Guid.NewGuid(), name.Trim());
    }

    public void Rename(string newName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newName);
        Name = newName.Trim();
    }

    public override bool Equals(object? obj) => obj is Category other && Id == other.Id;
    public override int GetHashCode() => Id.GetHashCode();
}
