namespace Vault.Domain.Services;

using Vault.Domain.ValueObjects;

public static class PasswordStrengthEvaluator
{
	public static PasswordStrengthResult Evaluate(string password)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(password);

		var hasUpper = false;
		var hasLower = false;
		var hasDigit = false;
		var hasSymbol = false;

		foreach (var c in password)
		{
			if (char.IsUpper(c)) hasUpper = true;
			else if (char.IsLower(c)) hasLower = true;
			else if (char.IsDigit(c)) hasDigit = true;
			else hasSymbol = true;
		}

		var varietyCount = 0;
		if (hasUpper) varietyCount++;
		if (hasLower) varietyCount++;
		if (hasDigit) varietyCount++;
		if (hasSymbol) varietyCount++;

		return (password.Length, varietyCount) switch
		{
            ( < 4, _) => new PasswordStrengthResult(PasswordStrengthLevel.Weak, "Extremadamente corta"),
            ( < 8, _) => new PasswordStrengthResult(PasswordStrengthLevel.Weak, "Menos de 8 caracteres."),
			( >= 8, 1) => new PasswordStrengthResult(PasswordStrengthLevel.Weak, "Solo usa un tipo de carácter."),
			( >= 8, 2) => new PasswordStrengthResult(PasswordStrengthLevel.Medium, "Combina dos tipos de carácter."),
			( >= 12, >= 3) => new PasswordStrengthResult(PasswordStrengthLevel.VeryStrong, "Longitud robusta y variedad alta de caracteres."),
			( >= 8, >= 3) => new PasswordStrengthResult(PasswordStrengthLevel.Strong, "Buena variedad de caracteres."),
			_ => new PasswordStrengthResult(PasswordStrengthLevel.Weak, "No cumple los criterios mínimos.")
		};
	}
}
