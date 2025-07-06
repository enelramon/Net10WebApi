# 🚀 .NET 10 Web API with JWT & API Key Authentication

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?style=flat-square&logo=csharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg?style=flat-square)](LICENSE)
[![Build Status](https://img.shields.io/badge/Build-Passing-brightgreen?style=flat-square)](https://github.com)

## 📋 Descripción

Una API Web moderna construida con .NET 10 que implementa autenticación dual utilizando **JWT Bearer tokens** y **API Keys**. Esta API proporciona endpoints seguros con diferentes niveles de autorización, perfecta para aplicaciones que requieren tanto autenticación de usuarios como autenticación de servicios.

## ✨ Características Principales

- 🔐 **Autenticación Dual**: JWT Bearer tokens y API Keys
- 🛡️ **Autorización Flexible**: Endpoints con diferentes esquemas de autenticación
- 📖 **Documentación OpenAPI**: Swagger UI integrado
- 🏗️ **Arquitectura Minimal API**: Usando el patrón más moderno de .NET
- 🔧 **Configuración In-Memory**: Fácil configuración para desarrollo
- 🚀 **File-scoped Program**: Aprovechando .NET 10 features

## 🛠️ Tecnologías Utilizadas

| Tecnología | Versión | Propósito |
|-----------|---------|-----------|
| ![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet) | 10.0 | Framework principal |
| ![JWT](https://img.shields.io/badge/JWT-8.0-000000?style=flat-square&logo=jsonwebtokens) | 8.* | Autenticación JWT |
| ![OpenAPI](https://img.shields.io/badge/OpenAPI-10.0-6BA539?style=flat-square&logo=openapiinitiative) | 10.* | Documentación API |
| ![Swagger](https://img.shields.io/badge/Swagger-UI-85EA2D?style=flat-square&logo=swagger) | - | Interfaz de documentación |

## 🚀 Inicio Rápido

### Prerrequisitos

- ✅ .NET 10 SDK instalado
- ✅ Visual Studio 2024 o VS Code
- ✅ Git (opcional)

### 🔧 Instalación

1. **Clona el repositorio** (o descarga el código)
   ```bash
   git clone <repository-url>
   cd Net10WebApi
   ```

2. **Ejecuta la aplicación**
   ```bash
   dotnet run
   ```

3. **Accede a la documentación**
   - 🌐 Swagger UI: `http://localhost:5000/swagger`
   - 📄 OpenAPI Spec: `http://localhost:5000/swagger/v1/swagger.json`

## 🔐 Autenticación

### JWT Bearer Token

Para endpoints que requieren autenticación JWT, incluye el header:

```http
Authorization: Bearer <tu-jwt-token>
```

**Ejemplo de JWT válido para testing:**
```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c
```

### API Key

Para endpoints que requieren API Key, incluye el header:

```http
X-API-KEY: ESTA_ES_MI_API_KEY_ULTRA_SECRETA_PARA_SERVICIOS
```

## 📊 Endpoints Disponibles

### 🌍 Público

| Método | Endpoint | Descripción | Autenticación |
|--------|----------|-------------|---------------|
| `GET` | `/api/data/public` | Datos públicos | ❌ No requerida |

### 🔑 Solo JWT

| Método | Endpoint | Descripción | Autenticación |
|--------|----------|-------------|---------------|
| `GET` | `/api/data/user-data` | Datos del usuario | 🔐 JWT Bearer |

### 🗝️ Solo API Key

| Método | Endpoint | Descripción | Autenticación |
|--------|----------|-------------|---------------|
| `GET` | `/api/data/service-data` | Datos de servicio | 🔑 API Key |

### 🔄 JWT o API Key

| Método | Endpoint | Descripción | Autenticación |
|--------|----------|-------------|---------------|
| `GET` | `/api/data/flexible-data` | Datos flexibles | 🔐 JWT Bearer OR 🔑 API Key |

## 🧪 Ejemplos de Uso

### 📝 Usando cURL

```bash
# Endpoint público
curl -X GET "http://localhost:5000/api/data/public"

# Con JWT Bearer
curl -X GET "http://localhost:5000/api/data/user-data" \
  -H "Authorization: Bearer <tu-jwt-token>"

# Con API Key
curl -X GET "http://localhost:5000/api/data/service-data" \
  -H "X-API-KEY: ESTA_ES_MI_API_KEY_ULTRA_SECRETA_PARA_SERVICIOS"

# Con cualquiera de los dos
curl -X GET "http://localhost:5000/api/data/flexible-data" \
  -H "X-API-KEY: ESTA_ES_MI_API_KEY_ULTRA_SECRETA_PARA_SERVICIOS"
```

### 📱 Usando JavaScript/Fetch

```javascript
// Endpoint público
const publicData = await fetch('/api/data/public');
const result = await publicData.json();

// Con JWT Bearer
const userData = await fetch('/api/data/user-data', {
  headers: {
    'Authorization': 'Bearer <tu-jwt-token>'
  }
});

// Con API Key
const serviceData = await fetch('/api/data/service-data', {
  headers: {
    'X-API-KEY': 'ESTA_ES_MI_API_KEY_ULTRA_SECRETA_PARA_SERVICIOS'
  }
});
```

## ⚙️ Configuración

### 🔧 Configuración JWT

```csharp
["Jwt:Issuer"] = "https://tu-dominio.com"
["Jwt:Audience"] = "https://tu-dominio.com"
["Jwt:Key"] = "ESTA_ES_UNA_CLAVE_SECRETA_MUY_LARGA_Y_SEGURA_CAMBIALA"
```

### 🔑 Configuración API Key

```csharp
["ApiKey"] = "ESTA_ES_MI_API_KEY_ULTRA_SECRETA_PARA_SERVICIOS"
```

> ⚠️ **Importante**: En producción, estas claves deben almacenarse de forma segura usando Azure Key Vault, variables de entorno o User Secrets.

## 🏗️ Arquitectura

```
┌─────────────────────────────────────────────────────────────┐
│                        .NET 10 Web API                      │
├─────────────────────────────────────────────────────────────┤
│  🔐 Authentication Layer                                    │
│  ├── JWT Bearer Handler                                     │
│  └── API Key Handler (Custom)                              │
├─────────────────────────────────────────────────────────────┤
│  📋 Endpoints                                               │
│  ├── Public Endpoints                                       │
│  ├── JWT-only Endpoints                                     │
│  ├── API Key-only Endpoints                                 │
│  └── Flexible Auth Endpoints                                │
├─────────────────────────────────────────────────────────────┤
│  📖 Documentation                                           │
│  ├── OpenAPI Specification                                  │
│  └── Swagger UI                                             │
└─────────────────────────────────────────────────────────────┘
```

## 🔍 Características Técnicas

### 🎯 Middleware Pipeline

1. **Authentication** - Identifica al usuario/servicio
2. **Authorization** - Verifica permisos
3. **OpenAPI** - Genera documentación
4. **Routing** - Enruta a endpoints

### 🛡️ Seguridad

- ✅ **Validación de Issuer**: Verifica el emisor del JWT
- ✅ **Validación de Audience**: Verifica la audiencia del JWT
- ✅ **Validación de Lifetime**: Verifica la vigencia del token
- ✅ **Validación de Signature**: Verifica la firma del JWT
- ✅ **API Key Validation**: Validación segura de API Keys

## 🚀 Despliegue

### 📦 Docker

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["program.cs", "."]
RUN dotnet restore
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Net10WebApi.dll"]
```

### ☁️ Azure App Service

```bash
# Publicar a Azure
az webapp up --name your-app-name --resource-group your-rg --plan your-plan
```

## 📚 Documentación Adicional

- 📖 [Documentación de .NET 10](https://docs.microsoft.com/en-us/dotnet/)
- 🔐 [JWT.io - JWT Debugger](https://jwt.io/)
- 📄 [OpenAPI Specification](https://swagger.io/specification/)
- 🏗️ [Minimal APIs en .NET](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis)

## 🤝 Contribuir

1. 🍴 Fork el proyecto
2. 🌟 Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. 💾 Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. 📤 Push a la rama (`git push origin feature/AmazingFeature`)
5. 🔄 Abre un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo [LICENSE](LICENSE) para más detalles.

## 👥 Autores

- **Tu Nombre** - *Trabajo inicial* - [TuGitHub](https://github.com/tu-usuario)

## 🙏 Agradecimientos

- 🏢 Microsoft por .NET 10
- 🌟 Comunidad de .NET
- 📖 OpenAPI Initiative
- 🔐 JWT.io community

## 🚀 Mejoras Futuras

### 🔐 **Autenticación y Seguridad**
- [ ] **Rate Limiting**: Implementar limitación de requests por IP/usuario
- [ ] **OAuth2/OpenID Connect**: Integrar con proveedores externos (Google, Microsoft, GitHub)
- [ ] **Refresh Tokens**: Sistema de renovación automática de tokens JWT
- [ ] **Two-Factor Authentication (2FA)**: Autenticación de dos factores
- [ ] **API Versioning**: Versionado de API (v1, v2, etc.)
- [ ] **CORS Configuration**: Configuración avanzada de CORS para múltiples dominios
- [ ] **IP Whitelisting**: Lista blanca de IPs permitidas para API Keys

### 💾 **Base de Datos y Persistencia**
- [ ] **Entity Framework Core**: Integración con SQL Server/PostgreSQL/MySQL
- [ ] **Redis Cache**: Implementar caché distribuido para mejor performance
- [ ] **MongoDB**: Soporte para datos NoSQL
- [ ] **Azure Cosmos DB**: Base de datos global distribuida
- [ ] **Database Migrations**: Migraciones automáticas de base de datos
- [ ] **Connection Pooling**: Optimización de conexiones a BD

### 📊 **Monitoreo y Logging**
- [ ] **Serilog**: Logging estructurado avanzado con diferentes outputs
- [ ] **Application Insights**: Monitoreo y telemetría de Azure
- [ ] **Health Checks**: Endpoints de salud de la aplicación y dependencias
- [ ] **Metrics**: Métricas personalizadas con Prometheus
- [ ] **OpenTelemetry**: Distributed tracing para microservicios
- [ ] **Error Tracking**: Integración con Sentry o similar

### 🚀 **Performance y Escalabilidad**
- [ ] **Memory Caching**: Caché en memoria para datos frecuentemente accedidos
- [ ] **Background Services**: Servicios en segundo plano para tareas pesadas
- [ ] **Message Queues**: RabbitMQ/Azure Service Bus para comunicación asíncrona
- [ ] **SignalR**: Comunicación en tiempo real con WebSockets
- [ ] **GraphQL**: Alternativa a REST API para consultas flexibles
- [ ] **Response Compression**: Compresión automática de respuestas

### 🔄 **Integración y APIs**
- [ ] **HTTP Clients**: Integrar con APIs externas de terceros
- [ ] **gRPC**: Comunicación high-performance entre servicios
- [ ] **WebHooks**: Sistema de notificaciones automáticas
- [ ] **File Upload/Download**: Manejo seguro de archivos con validación
- [ ] **Email Services**: Envío de emails (SendGrid, SMTP, etc.)
- [ ] **SMS Services**: Notificaciones por SMS (Twilio, etc.)

### 🧪 **Testing y Calidad**
- [ ] **Unit Tests**: Pruebas unitarias con xUnit/NUnit
- [ ] **Integration Tests**: Tests de integración completos
- [ ] **API Tests**: Tests automatizados de endpoints con diferentes auth
- [ ] **Code Coverage**: Reportes de cobertura de código
- [ ] **Static Code Analysis**: SonarQube/Analyzers para calidad de código
- [ ] **Performance Tests**: Load testing con NBomber o similar

### 🔧 **DevOps y Deployment**
- [ ] **Docker**: Containerización completa con multi-stage builds
- [ ] **GitHub Actions**: CI/CD pipeline automatizado
- [ ] **Azure DevOps**: Pipeline de deployment a múltiples ambientes
- [ ] **Kubernetes**: Orquestación de contenedores con Helm charts
- [ ] **Environment Configuration**: Configuración por ambiente (dev/staging/prod)
- [ ] **Infrastructure as Code**: Terraform o ARM templates

### 📱 **Frontend y UI**
- [ ] **Blazor Server/WASM**: Frontend .NET nativo
- [ ] **Admin Dashboard**: Panel administrativo para gestión de usuarios/APIs
- [ ] **API Documentation Site**: Documentación personalizada interactiva
- [ ] **Real-time Dashboard**: Dashboard en tiempo real con métricas
- [ ] **Mobile App**: Aplicación móvil con .NET MAUI

### 🎯 **Funcionalidades de Negocio**
- [ ] **User Management**: Sistema completo de gestión de usuarios
- [ ] **Role-based Access Control**: Control de acceso basado en roles
- [ ] **Audit Logging**: Registro de auditoría de todas las acciones
- [ ] **Multi-tenancy**: Soporte para múltiples inquilinos
- [ ] **Notification System**: Sistema de notificaciones push
- [ ] **Reporting**: Generación de reportes en PDF/Excel

### 🌐 **Internacionalización**
- [ ] **Multi-language Support**: Soporte para múltiples idiomas
- [ ] **Localization**: Localización de mensajes y formatos
- [ ] **Time Zone Handling**: Manejo correcto de zonas horarias
- [ ] **Currency Support**: Soporte para múltiples monedas

### 🔍 **Características Avanzadas**
- [ ] **Machine Learning**: Integración con ML.NET para análisis predictivo
- [ ] **Blockchain Integration**: Integración con blockchain para trazabilidad
- [ ] **AI Integration**: Integración con Azure OpenAI/ChatGPT
- [ ] **IoT Support**: Endpoints especializados para dispositivos IoT
- [ ] **Event Sourcing**: Patrón de Event Sourcing para auditoría completa

---

💡 **¿Tienes alguna idea adicional?** ¡Abre un issue o envía un PR para discutir nuevas funcionalidades!

---

⭐ **¡Si te gusta este proyecto, no olvides darle una estrella!** ⭐
