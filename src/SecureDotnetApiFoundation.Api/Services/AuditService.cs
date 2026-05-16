using SecureDotnetApiFoundation.Api.Data;
using SecureDotnetApiFoundation.Api.Models;
using System.Security.Claims;

namespace SecureDotnetApiFoundation.Api.Services;

public class AuditService
{
    private readonly AppDbContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditService(
        AppDbContext db,
        IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task LogAsync(string action, string outcome, string details)
    {
        var username = _httpContextAccessor
            .HttpContext?
            .User?
            .Identity?
            .Name ?? "Anonymous";

        var userRole = _httpContextAccessor
            .HttpContext?
            .User?
            .Claims?
            .FirstOrDefault(c => c.Type == ClaimTypes.Role)?
            .Value ?? "Unknown";

        var ipAddress = _httpContextAccessor
            .HttpContext?
            .Connection?
            .RemoteIpAddress?
            .ToString();

        var correlationId = _httpContextAccessor
            .HttpContext?
            .TraceIdentifier;

        var auditLog = new AuditLog
        {
            Username = username,
            UserRole = userRole,
            Action = action,
            Outcome = outcome,
            Details = details,
            IpAddress = ipAddress,
            CorrelationId = correlationId,
            TimestampUtc = DateTime.UtcNow
        };

        _db.AuditLogs.Add(auditLog);

        await _db.SaveChangesAsync();
    }
}
