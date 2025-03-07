namespace NutriSync.Core.Entities;

public class Appointment : Entity
{
    public Guid PatientId { get; set; }
    public Guid NutritionistId { get; set; }
    public DateTime AppointmentDate { get; set; }

    public Nutritionist Nutritionist { get; set; } = null!;
    public Patient Patient { get; set; } = null!;
}