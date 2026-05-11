# OWASP Mapping

## OWASP Top 10

| Risk | Control |
|---|---|
| A01 Broken Access Control | roles and policies |
| A02 Cryptographic Failures | BCrypt password hashing |
| A03 Injection | EF Core safe data access |
| A05 Security Misconfiguration | secure headers and safe errors |
| A07 Identification and Authentication Failures | JWT validation |
| A09 Security Logging and Monitoring Failures | audit logging |

## OWASP API Top 10

| Risk | Control |
|---|---|
| API1 Broken Object Level Authorization | role and policy authorization foundations |
| API2 Broken Authentication | JWT and password hashing |
| API4 Unrestricted Resource Consumption | rate limiting |
| API5 Broken Function Level Authorization | endpoint-level policies |
| API8 Security Misconfiguration | secure headers and safe middleware |
