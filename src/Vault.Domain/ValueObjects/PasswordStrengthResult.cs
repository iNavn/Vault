namespace Vault.Domain.ValueObjects;

public enum PasswordStrengthLevel
{
    Weak,
    Medium,
    Strong,
    VeryStrong
}

public sealed record PasswordStrengthResult(PasswordStrengthLevel Level, string Reason);
