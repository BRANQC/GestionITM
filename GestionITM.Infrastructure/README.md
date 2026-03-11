# GestionITM.Infrastructure

Proyecto de biblioteca de clases (.NET 8) para la infraestructura de la aplicación.

## Descripción

`GestionITM.Infrastructure` implementa los detalles técnicos que soportan al dominio académico:

- Acceso a datos (ORM como Entity Framework Core, repositorios, contexto de base de datos) para estudiantes, matrículas, cursos y demás entidades.
- Integración con servicios externos (APIs de terceros, colas, almacenamiento, etc.).
- Implementaciones concretas de interfaces definidas en `GestionITM.Domain`.

## Dependencias

- Referencia directa a `GestionITM.Domain` para usar las entidades y contratos de dominio.

## Características técnicas

- Target Framework: `.NET 8`.
- `Nullable` habilitado.
- `ImplicitUsings` habilitado.

## Buenas prácticas en esta capa

- Mantener las dependencias de infraestructura aquí y no en el dominio.
- Seguir patrones como Repositorio, Unit of Work, etc., si aplica.
- Configurar la persistencia (por ejemplo, `DbContext`) y sus mapeos.

## Próximos pasos

- Agregar contexto de base de datos y configuraciones (por ejemplo, `DbContext` de EF Core).
- Implementar repositorios concretos para las entidades de dominio.
