# Threat Model

## System Context

A healthcare-style API exposes patients and medical records to authenticated users with different roles.

## Assets

- user accounts
- JWT tokens
- patient records
- medical records
- audit logs
- role assignments

## Trust Boundaries

- external client to API
- authentication layer
- authorization layer
- API to database
- user role boundaries

## Threats and Mitigations

| Threat | Example | Mitigation |
|---|---|---|
| Broken access control | unauthorized role accesses records | RBAC and policies |
| Token misuse | expired or forged token | JWT validation |
| Brute force | repeated login attempts | rate limiting |
| Data leakage | internal data returned | DTOs |
| Error disclosure | stack trace exposed | safe exception middleware |
| Missing auditability | no trace of sensitive access | audit logging |
