using System.Security.Claims;
using SecureDotnetApiFoundation.Api.Data;
using SecureDotnetApiFoundation.Api.Models;
namespace SecureDotnetApiFoundation.Api.Services;
public class AuditService
{
    private readonly AppDbContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AuditService(AppDbContext db, IHttpContextAccessor httpContextAccessor) { _db = db; _httpContextAccessor = httpContextAccessor; }
    public async Task LogAsync(string action, string resource)
    {
        var context = _httpContextAccessor.HttpContext;
        var username = context?.User.FindFirstValue(ClaimTypes.Email) ?? "anonymous";
        var role = context?.User.FindFirstValue(ClaimTypes.Role) ?? "unknown";
        var correlationId = context?.Response.Headers["X-Correlation-Id"].ToString() ?? string.Empty;
        _db.AuditLogs.Add(new AuditLog { Username = username, UserRole = role, Action = action, Resource = resource, CorrelationId = correlationId });
        await _db.SaveChangesAsync();
    }
}
