namespace NutriSync.Core.Entities;

public class PatientMeasurement : Entity
{
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = null!;
    public decimal WeightKg { get; set; }
    public decimal BodyFatPct { get; set; }
    public decimal MuscleMassKg { get; set; }
    public decimal WaistCm { get; set; }
    public decimal HipCm { get; set; }
    public decimal Bmi { get; set; }
}