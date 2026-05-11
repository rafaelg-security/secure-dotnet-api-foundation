using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureDotnetApiFoundation.Api.Data;
using SecureDotnetApiFoundation.Api.Dtos;
using SecureDotnetApiFoundation.Api.Models;
using SecureDotnetApiFoundation.Api.Services;
namespace SecureDotnetApiFoundation.Api.Controllers;
[ApiController]
[Route("api/medical-records")]
[Authorize]
public class MedicalRecordsController : ControllerBase
{
    private readonly AppDbContext _db; private readonly AuditService _audit;
    public MedicalRecordsController(AppDbContext db, AuditService audit) { _db = db; _audit = audit; }
    [HttpGet("patient/{patientId:guid}")]
    [Authorize(Policy = "CanViewMedicalRecords")]
    public async Task<ActionResult<IEnumerable<MedicalRecordResponse>>> GetByPatient(Guid patientId)
    {
        var records = await _db.MedicalRecords.Where(r => r.PatientId == patientId).Select(r => new MedicalRecordResponse { Id = r.Id, PatientId = r.PatientId, Diagnosis = r.Diagnosis, Notes = r.Notes, CreatedAtUtc = r.CreatedAtUtc, CreatedBy = r.CreatedBy }).ToListAsync();
        await _audit.LogAsync("VIEW_MEDICAL_RECORDS", $"Patient:{patientId}");
        return Ok(records);
    }
    [HttpPost]
    [Authorize(Policy = "CanManageMedicalRecords")]
    public async Task<IActionResult> Create(CreateMedicalRecordRequest request)
    {
        var patientExists = await _db.Patients.AnyAsync(p => p.Id == request.PatientId);
        if (!patientExists) return BadRequest("Invalid patient.");
        var createdBy = User.Identity?.Name ?? "unknown";
        var record = new MedicalRecord { PatientId = request.PatientId, Diagnosis = request.Diagnosis, Notes = request.Notes, CreatedBy = createdBy };
        _db.MedicalRecords.Add(record);
        await _db.SaveChangesAsync();
        await _audit.LogAsync("CREATE_MEDICAL_RECORD", $"MedicalRecord:{record.Id}");
        return CreatedAtAction(nameof(GetByPatient), new { patientId = record.PatientId }, new { record.Id, record.PatientId, record.CreatedAtUtc });
    }
}
