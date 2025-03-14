namespace NutriSync.Core.Entities;

public class Assessment
{
    public int Id { get; set; } // Unique identifier for the assessment
    public int PatientId { get; set; } // Relates the assessment to a patient
    public DateTime AssessmentDate { get; set; } // Date when the assessment was conducted

    // Basic measurements
    public decimal Weight { get; set; } // Patient's current weight (kg)
    public decimal Height { get; set; } // Patient's height (cm)
    public decimal BMI => Height > 0 ? Weight / (Height / 100 * Height / 100) : 0; // Body Mass Index (BMI)

    // Circumferences
    public decimal Waist { get; set; } // Waist circumference (cm)
    public decimal Hip { get; set; } // Hip circumference (cm)
    public decimal Arm { get; set; } // Arm circumference (cm)
    public decimal Thigh { get; set; } // Thigh circumference (cm)

    // Additional health indicators
    public decimal? BodyFatPercentage { get; set; } // Optional: Body fat percentage
    public string? BloodPressure { get; set; } // Optional: Blood pressure (e.g., "120/80")
    public string? ActivityLevel { get; set; } // Optional: Physical activity level description
    public string? LabTests { get; set; } // Optional: Lab test results (e.g., cholesterol, glucose)

    // Additional data
    public string? Photos { get; set; } // URLs or file paths for standardized photos (front, side, back)
    public string? Comments { get; set; } // Nutritionist's observations about the assessment
    public string? Goal { get; set; } // Patient's current goal (e.g., lose 5 kg, reduce 5 cm from waist)
    public string? Progress { get; set; } // Progress description (e.g., "Lost 3 kg in 2 months")
}
