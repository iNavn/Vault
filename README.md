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
Contratos definidos en Vault.Application, con implementaciones temporales en memoria
(se reemplazan por EF Core en el Módulo 3):
- `IVaultItemRepository` → `InMemoryVaultItemRepository`
  Incluye `GetWeakPasswordItemsAsync()`, que evalúa fortaleza sobre `EncryptedPassword`
  asumiendo (temporalmente) que sigue siendo texto plano — ver comentario en el código.
- `ICategoryRepository` → `InMemoryCategoryRepository`

## Organización de endpoints
Los endpoints se agrupan por dominio en Vault.Api/Endpoints/ (extension methods sobre WebApplication),
no directamente en Program.cs. Minimal APIs elegido sobre Controllers por simplicidad para una API pura.

## Nota de diseño: lifetime de los repositorios (Scoped vs Singleton)
Los repositorios en memoria usan `Singleton` (no `Scoped`) porque necesitan persistir datos
entre peticiones HTTP distintas mientras no existe una base de datos real. Se confirmó
experimentalmente: con `Scoped`, un VaultItem creado en un POST no aparecía en un GET
posterior, porque cada petición recibe su propia instancia con su propia lista vacía —
el ámbito (scope) termina junto con la petición. Con Singleton, la misma instancia
(y su misma lista) vive durante toda la ejecución de la app.
Este comportamiento cambiará en el Módulo 3: el DbContext de EF Core se registrará como
Scoped, ya que ahí la persistencia real la da PostgreSQL, no la memoria del proceso.

## Endpoints
- `GET /vault-items/stats` — conteo total y de contraseñas débiles. Usa await secuencial
  (no Task.WhenAll) porque las implementaciones actuales en memoria no tienen espera real de I/O;
  se reevaluará cuando EF Core reemplace estos repositorios en el Módulo 3.
