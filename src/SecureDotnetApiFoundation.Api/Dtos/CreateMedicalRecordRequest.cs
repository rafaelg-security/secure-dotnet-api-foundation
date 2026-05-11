using System.ComponentModel.DataAnnotations;
namespace SecureDotnetApiFoundation.Api.Dtos;
public class CreateMedicalRecordRequest
{
    [Required]
    public Guid PatientId { get; set; }
    [Required]
    [MaxLength(200)]
    public string Diagnosis { get; set; } = string.Empty;
    [Required]
    [MaxLength(2000)]
    public string Notes { get; set; } = string.Empty;
}
