# Secure .NET API Foundation

## Overview

Secure .NET API Foundation is a healthcare-oriented ASP.NET Core API project focused on Application Security, Secure SDLC and Technical GRC concepts.

The project demonstrates secure API engineering practices including:

- JWT authentication
- Role-Based Access Control (RBAC)
- Audit logging
- API hardening
- Rate limiting
- Secure middleware
- OWASP-aligned controls

---

## Architecture

Main domains:

- Auth
- Patients
- MedicalRecords
- AuditLogs

---

## Security Controls

| Control | Purpose |
|---|---|
| JWT Authentication | Secure user authentication |
| RBAC Policies | Least privilege authorization |
| Rate Limiting | Abuse and brute force mitigation |
| Audit Logging | Accountability and traceability |
| Security Headers | HTTP hardening |
| Exception Middleware | Prevent information leakage |
| Correlation IDs | Request traceability |

---

## Authentication

The API uses JWT Bearer authentication with:

- issuer validation
- audience validation
- signing key validation
- expiration validation

Swagger integration allows authenticated testing directly from the UI.

---

## Authorization

Policy-based authorization is used to protect sensitive endpoints.

Example policies:

- CanViewPatients
- CanViewMedicalRecords
- CanManageMedicalRecords
- CanViewAuditLogs

---

## OWASP API Security Mapping

| OWASP Risk | Mitigation |
|---|---|
| Broken Access Control | RBAC policies |
| Authentication Failures | JWT validation |
| Security Misconfiguration | Security headers |
| Injection | DTO validation |
| Abuse / DoS | Rate limiting |
| Logging Failures | Audit logging |

---

## Security Testing Scenarios

The API was tested against common AppSec scenarios:

- Access without JWT
- Invalid role access
- Invalid JWT tokens
- Brute force login attempts
- IDOR-style resource access testing

---

## Technologies

- ASP.NET Core
- Entity Framework Core
- JWT Bearer Authentication
- Swagger / OpenAPI
- BCrypt
- GitHub Actions

---

## Future Improvements

- FluentValidation integration
- Refresh token support
- Integration security testing
- CodeQL security scanning
- Dependency vulnerability scanning
- Secrets scanning
- API versioning

---

## Security Disclaimer

This project is intended for educational and portfolio purposes only.