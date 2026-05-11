using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureDotnetApiFoundation.Api.Data;
using SecureDotnetApiFoundation.Api.Services;
namespace SecureDotnetApiFoundation.Api.Controllers;
[ApiController]
[Route("api/audit-logs")]
[Authorize(Roles = "Auditor,Admin")]
public class AuditLogsController : ControllerBase
{
    private readonly AppDbContext _db; private readonly AuditService _audit;
    public AuditLogsController(AppDbContext db, AuditService audit) { _db = db; _audit = audit; }
    [HttpGet]
    public async Task<IActionResult> GetAuditLogs()
    {
        await _audit.LogAsync("VIEW_AUDIT_LOGS", "AuditLogs");
        var logs = await _db.AuditLogs.OrderByDescending(x => x.TimestampUtc).Take(100).Select(x => new { x.Username, x.UserRole, x.Action, x.Resource, x.TimestampUtc, x.CorrelationId }).ToListAsync();
        return Ok(logs);
    }
}
