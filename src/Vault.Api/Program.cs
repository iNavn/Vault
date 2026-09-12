using Vault.Application.Repositories;
using Vault.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/ping", saludoConFecha);
static string saludoConFecha()
{
    DateTime fechaUTC = DateTime.UtcNow;
    return $"Hola la fecha y hora actual es: {fechaUTC}";
}

app.MapGet("/saludos/{n:int}", GenerarSaludos);

static string GenerarSaludos(int n)
{
    var resultado = "";
    for (int i = 0; i < n; i++)
    {
        resultado += ConstruirSaludo(i);
    }
    return resultado;
}

static string ConstruirSaludo(int indice)
{
    return $"Saludo #{indice} generado a las {DateTime.UtcNow:HH:mm:ss}\n";
}

builder.Services.AddSingleton<IVaultItemRepository, InMemoryVaultItemRepository>();

app.Run();
