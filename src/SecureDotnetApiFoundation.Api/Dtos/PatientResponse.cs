namespace SecureDotnetApiFoundation.Api.Dtos;
public class PatientResponse
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string PatientReference { get; set; } = string.Empty;
}
