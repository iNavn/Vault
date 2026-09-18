using Vault.Application.Repositories;
using Vault.Domain.Entities;
using Vault.Infrastructure.Repositories;
using Vault.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IVaultItemRepository, InMemoryVaultItemRepository>();
builder.Services.AddSingleton<ICategoryRepository, InMemoryCategoryRepository>();

var app = builder.Build();

app.MapVaultItemEndpoints();
app.MapCategoryEndpoints();

app.Run();
