using SecureDotnetApiFoundation.Api.Models;
namespace SecureDotnetApiFoundation.Api.Data;
public static class SeedData
{
    public static void Initialize(AppDbContext db)
    {
        if (db.Users.Any()) return;
        db.Users.AddRange(
            new User { Username = "doctor@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Doctor123!"), Role = "Doctor" },
            new User { Username = "nurse@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Nurse123!"), Role = "Nurse" },
            new User { Username = "auditor@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Auditor123!"), Role = "Auditor" },
            new User { Username = "admin@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"), Role = "Admin" }
        );
        var patient = new Patient { FullName = "Maria Silva", DateOfBirth = new DateOnly(1985, 4, 12), PatientReference = "PAT-10001" };
        db.Patients.Add(patient);
        db.MedicalRecords.Add(new MedicalRecord { PatientId = patient.Id, Diagnosis = "Hypertension", Notes = "Initial diagnosis and treatment plan.", CreatedBy = "doctor@example.com" });
        db.SaveChanges();
    }
}
