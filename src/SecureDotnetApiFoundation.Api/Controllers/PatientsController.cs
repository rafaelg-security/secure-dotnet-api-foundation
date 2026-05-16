using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureDotnetApiFoundation.Api.Data;
using SecureDotnetApiFoundation.Api.Dtos;
using SecureDotnetApiFoundation.Api.Services;

namespace SecureDotnetApiFoundation.Api.Controllers;

[ApiController]
[Route("api/patients")]
[Authorize]
public class PatientsController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly AuditService _audit;

    public PatientsController(AppDbContext db, AuditService audit)
    {
        _db = db;
        _audit = audit;
    }

    [HttpGet]
    [Authorize(Policy = "CanViewPatients")]
    public async Task<ActionResult<IEnumerable<PatientResponse>>> GetPatients()
    {
        var patients = await _db.Patients
            .Select(p => new PatientResponse
            {
                Id = p.Id,
                FullName = p.FullName,
                DateOfBirth = p.DateOfBirth,
                PatientReference = p.PatientReference
            })
            .ToListAsync();

        await _audit.LogAsync("ViewPatientList", "Success", "Patient list viewed");

        return Ok(patients);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = "CanViewPatients")]
    public async Task<ActionResult<PatientResponse>> GetPatient(Guid id)
    {
        var patient = await _db.Patients
            .Where(p => p.Id == id)
            .Select(p => new PatientResponse
            {
                Id = p.Id,
                FullName = p.FullName,
                DateOfBirth = p.DateOfBirth,
                PatientReference = p.PatientReference
            })
            .SingleOrDefaultAsync();

        if (patient is null)
        {
            await _audit.LogAsync("ViewPatient", "NotFound", $"Patient not found: {id}");
            return NotFound();
        }

        await _audit.LogAsync("ViewPatient", "Success", $"Patient viewed: {id}");

        return Ok(patient);
    }
}