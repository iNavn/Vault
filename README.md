# Vault

Gestor de contraseñas multi-cliente (móvil, escritorio, web) sobre una única API.

## Stack
.NET 10 · C# 14 · Clean Architecture · EF Core · PostgreSQL · Redis · JWT

## Arquitectura

\`\`\`mermaid
graph LR
    Api[Vault.Api] --> Application[Vault.Application]
    Api --> Infrastructure[Vault.Infrastructure]
    Infrastructure --> Application
    Application --> Domain[Vault.Domain]
\`\`\`

## Requisitos previos
1. .NET SDK 10
2. Docker Desktop
3. Visual Studio 2026 Community
4. Git
5. pgAdmin 4
6. RedisInsight
7. k6

## Flujo de Git
- `main` está protegida: todo cambio pasa por Pull Request.
- Convención de ramas: `<tipo>/<descripción-corta>` (feat, fix, docs, chore, refactor, test).
- Convención de commits: Conventional Commits.

## Setup local
1. .NET 10 SDK
2. Docker Desktop (Postgres + Redis en contenedores — se añade en el Módulo 3/4)
3. `dotnet build` desde la raíz

## Decisiones de arquitectura
Ver carpeta `/docs/adr` (se añadirá cuando haya la primera decisión relevante).