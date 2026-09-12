# Vault

Gestor de contraseñas multi-cliente (móvil, escritorio, web) sobre una única API.

## Stack
.NET 10 · C# 14 · Clean Architecture · EF Core · PostgreSQL · Redis · JWT

## Arquitectura

```mermaid
graph LR
    Api[Vault.Api] --> Application[Vault.Application]
    Api --> Infrastructure[Vault.Infrastructure]
    Infrastructure --> Application
    Application --> Domain[Vault.Domain]
```

## Modelo de dominio

```mermaid
classDiagram
    class Category {
        +Guid Id
        +string Name
        +Create(name) Category
        +Rename(newName)
    }
    class VaultItem {
        +Guid Id
        +Guid CategoryId
        +string SiteName
        +string Username
        +string EncryptedPassword
        +Create(...) VaultItem
    }
    Category "1" --> "many" VaultItem : categoriza
```

## Requisitos previos
1. .NET SDK 10
2. Docker Desktop
3. Visual Studio 2026 Community
4. Git
5. pgAdmin 4
6. RedisInsight
7. k6

## Flujo de Git
- `main` está protegida: todo cambio pasa por Pull Request, sin excepciones para administradores.
- Convención de ramas: `<tipo>/<descripción-corta>` (feat, fix, docs, chore, refactor, test).
- Convención de commits: Conventional Commits.

## Setup local
1. .NET 10 SDK
2. Docker Desktop (Postgres + Redis en contenedores — se añade en el Módulo 3/4)
3. `dotnet build` desde la raíz

## Decisiones de arquitectura
Ver carpeta `/docs/adr` (se añadirá cuando haya la primera decisión relevante).

## Repositorios
`IVaultItemRepository` definido en Vault.Application. Implementación actual:
InMemoryVaultItemRepository (temporal, se reemplaza por EF Core en el Módulo 3).