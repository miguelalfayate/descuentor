# Descuentor

Descuentor es una aplicación que te permite hacer seguimiento de precios de productos en diferentes tiendas online, notificándote cuando hay descuentos en los artículos que te interesan.

## 🚀 Características Principales

- Seguimiento de precios de los artículos de tu "Lista de deseos"
- Extensión de Chrome para añadir productos fácilmente
- Notificaciones de descuentos
- Histórico de precios
- Soporte para múltiples tiendas online
- Interfaz web intuitiva

## 🛠️ Tecnologías

- Frontend: Blazor WebAssembly
- Backend: ASP.NET Core API
- Scraper: PuppeteerSharp
- Base de datos: PostgreSQL
- Docker para contenerización
- CI/CD con GitHub Actions

## 📋 Prerrequisitos

- .NET 9.0+
- Docker & Docker Compose
- ...

## 🔧 Instalación

```bash
# Clonar el repositorio
git clone https://github.com/miguelalfayate/descuentor.git

# Navegar al directorio
cd descuentor

# Levantar con Docker Compose
docker-compose up -d
```

## 🚀 Guía Rápida de Uso

1. Instala la extensión de Chrome
2. Navega a tu producto favorito en Amazon
3. Click en la extensión para añadir el artículo a tu lista de seguimiento

## 📖 Documentación

Documentación completa disponible en la carpeta `/memoria`.

Índice de las secciones:

1. Introducción
   1.1. Motivación
   1.2. Objetivos
   1.3. Alcance

2. Contexto tecnológico
   2.1. Análisis de la competencia
   2.2. Tecnologías existentes
   2.3. Comparativa de soluciones

3. Análisis y Diseño
   3.1. Requisitos
       3.1.1. Funcionales
       3.1.2. No Funcionales
   3.2. Arquitectura
       3.2.1. [Sistema](memoria/3.2-arquitectura/3.2.1-sistema.md)
       3.2.2. Base de Datos
       3.2.3. API Endpoints
       3.2.4. Integraciones

4. Metodología
   4.1. Proceso de desarrollo (Git Workflow)
   4.2. Herramientas
   4.3. Planificación (Timeline, Risk Assessment

5. Implementación
   5.1. Frontend
       5.1.1. Blazor WebApp
       5.1.2. Chrome Extension
   5.2. Backend
       5.2.1. API
       5.2.2. Scraper
   5.3. Seguridad
       5.3.1. Autenticación
       5.3.2. Gestión de secretos
       5.3.3. Protección de datos
   5.4. Testing
       5.4.1. Estrategia
       5.4.2. Unit Tests
       5.4.3. Integration Tests

6. Despliegue
   6.1. Entornos
       6.1.1. Desarrollo local
       6.1.2. Producción
   6.2. Docker
       6.2.1. Configuración
       6.2.2. Imágenes
   6.3. CI CD
       6.3.1. Pipeline
       6.3.2. Automatización
   6.4. Monitorización
       6.4.1. Logging
       6.4.2. Métricas
       6.4.3. Alertas

7. Resultados
   7.1. Rendimiento
       7.1.1. Pruebas de carga
       7.1.2. Benchmarks
       7.1.3. Optimizaciones
   7.2. Métricas de uso
   7.3. Feedback de usuarios

8. Aspectos Legales
   8.1. Licencias
   8.2. Privacidad
   8.3. Términos de uso
   8.4. GDPR

9. Conclusiones
   9.1. Objetivos cumplidos
   9.2. Limitaciones
   9.3. Trabajo futuro
       9.3.1. Mejoras propuestas
       9.3.2. Nuevas funcionalidades

10. Anexos
    10.1. Guías
        10.1.1. Instalación
        10.1.2. Manual de usuario
        10.1.3. Manual de administrador
    10.2. Mantenimiento
        10.2.1. Backups
        10.2.2. Actualizaciones
        10.2.3. Troubleshooting
    10.3. Referencias técnicas
        10.3.1. API Docs
        10.3.2. Configuración del sistema

Bibliografía

## 🔄 Arquitectura

```mermaid
flowchart TB
    subgraph "Frontend"
        WebUI[Blazor WebAssembly\nInterfaz Web]
        ChromeExt[Chrome Extension\nAñadir Productos]
    end

    subgraph "Backend Services"
        API[API REST\nGestión de Datos]
        Scraper[Scraper Service\nRecopilación de Precios]
        NotificationEngine[Motor de Notificaciones\nAlertas de Descuentos]
    end

    Database[(Base de Datos\nHistórico de Precios\nLista de Deseos)]
    
    WebUI <--> API
    ChromeExt <--> API
    Scraper <--> API
    API <--> Database
    NotificationEngine <--> API
    NotificationEngine <--> Database

    User[Usuario] --> WebUI
    User --> ChromeExt

    Tiendas[Tiendas Online] --> Scraper
```



Breve descripción de los componentes principales:
- WebUI (Blazor WASM)
- API Backend
- Scraper Service
- Chrome Extension

## 🧪 Tests

```bash
# Ejecutar tests
dotnet test
```

## 📝 Licencia

Este proyecto está bajo la licencia MIT License - ver el archivo [LICENSE.md](link) para más detalles.

## ✒️ Autor

* **Miguel Alfayate** - [miguelalfayate](https://github.com/miguelalfayate/)

## 🎓 Proyecto Académico

Este proyecto fue desarrollado como proyecto final del *Curso de Formación de Grado Superior en Desarrollo de Aplicaciones Multiplataforma*.

## 🔗 Enlaces Útiles

- [Demo en vivo](link)
- [Documentación técnica completa](link)
- [Reportar un bug](link)
