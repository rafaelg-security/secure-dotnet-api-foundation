# secure-dotnet-api-foundation

A .NET 10 secure API foundation project demonstrating enterprise secure engineering, Application Security, secure SDLC, and regulated-system awareness.

## Purpose

This project is part of a cybersecurity transition portfolio focused on:

- Application Security
- Secure Engineering
- Secure SDLC
- Technical GRC foundations
- Healthcare cybersecurity foundations
- Cloud-ready enterprise API architecture

## What This Project Demonstrates

- ASP.NET Core API architecture
- JWT authentication
- role-based authorization
- policy-based authorization
- secure middleware
- secure headers
- rate limiting
- audit logging
- DTO-based API responses
- secure error handling
- correlation IDs
- healthcare-style regulated data access
- CI pipeline with CodeQL

## Demo Credentials

```text
doctor@example.com / Doctor123!
nurse@example.com / Nurse123!
auditor@example.com / Auditor123!
admin@example.com / Admin123!
```

## Run Locally

```bash
dotnet restore
dotnet build
dotnet test
dotnet run --project src/SecureDotnetApiFoundation.Api
```

Swagger:

```text
https://localhost:5001/swagger
```

## Documentation

- `docs/SECURITY_CONTROLS.md`
- `docs/THREAT_MODEL.md`
- `docs/OWASP_MAPPING.md`
