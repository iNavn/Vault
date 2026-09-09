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

## Setup local
1. .NET 10 SDK
2. Docker Desktop (Postgres + Redis en contenedores — se añade en el Módulo 3/4)
3. `dotnet build` desde la raíz

## Decisiones de arquitectura
Ver carpeta `/docs/adr` (se añadirá cuando haya la primera decisión relevante).