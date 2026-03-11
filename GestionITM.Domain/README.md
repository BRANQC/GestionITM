# GestionITM.Domain

Proyecto de biblioteca de clases (.NET 8) que contiene el modelo de dominio de la aplicación.

## Descripción

`GestionITM.Domain` concentra los elementos centrales del negocio académico:

- Entidades de dominio principales:
  - `Estudiante` – Información básica del estudiante y su fecha de inscripción.
  - `Curso` – Información de los cursos ofrecidos (nombre, código, créditos, etc.).
  - `Matricula` – Relación entre estudiantes y cursos (periodo, estado, calificación, etc.).
  - `Product` – Productos o recursos académicos adicionales relacionados con el sistema.
- Reglas de negocio y validaciones propias del dominio.
- Contratos/abstracciones que pueden ser implementados por la capa de infraestructura.

## Características técnicas

- Target Framework: `.NET 8`.
- `Nullable` habilitado.
- `ImplicitUsings` habilitado.

## Buenas prácticas en esta capa

- Evitar dependencias hacia capas superiores (`API`, `Infrastructure`).
- Mantener el dominio lo más puro posible (POCOs, sin dependencia de frameworks).
- Centralizar las validaciones de negocio en las entidades o servicios de dominio.

## Ejemplos de contenido esperado

- Entidades como `Product`, `Estudiante`, etc.
- Value Objects si son necesarios (por ejemplo, tipos específicos para identificadores, correos, etc.).
- Excepciones de dominio específicas.
