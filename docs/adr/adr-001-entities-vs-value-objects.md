# ADR-001: Entities como clases, Value Objects como records

## Contexto
El dominio de Vault necesita distinguir objetos con identidad propia (Category, VaultItem)
de objetos que solo representan datos/resultados (PasswordStrengthResult).

## Decisión
Entities se implementan como `class` con Equals/GetHashCode sobrescritos por Id.
Value Objects se implementan como `record`, aprovechando su igualdad por valor nativa.

## Consecuencias
(+) La igualdad de cada tipo refleja su semántica real de dominio.
(-) Más código boilerplate en las Entities (constructor privado, Equals manual)
    comparado con usar records en todos lados.