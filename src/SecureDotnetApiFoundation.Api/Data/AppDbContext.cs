using Microsoft.EntityFrameworkCore;
using SecureDotnetApiFoundation.Api.Models;
namespace SecureDotnetApiFoundation.Api.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<User> Users => Set<User>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
}
