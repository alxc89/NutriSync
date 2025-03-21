using NutriSync.Core.Enums;
using NutriSync.Core.Interfaces;
using NutriSync.Core.ValueObject;

namespace NutriSync.Core.Entities;

public class Nutritionist : Entity
{
    private Nutritionist()
    {

    }

    public Nutritionist(string name, string phone, string email, string document, string crn, DateTime birthDate, Gender gender)
    {
        Name = name;
        Phone = phone;
        Email = email;
        Document = document;
        Crn = crn;
        BirthDate = birthDate;
        Gender = gender;
        TenantId = email;
    }

    public void Update(string name, string phone, string email, string crn, Address address)
    {
        Name = name;
        Phone = phone;
        Email = email;
        Crn = crn;
        Address = address;
    }

    private void SetStatus()
    {
        Status = Status.Active;
    }

    public void SetUserId(Guid userId)
        => UserId = userId;

    public Status Status { get; set; } = Status.PendingPayment;
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string Crn { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }

    public Address Address { get; set; } = null!;

    public Guid? UserId { get; set; } = null; // Relaciona com a tabela de usuários
    public UserBase User { get; set; } = null!;

    public ICollection<MealPlan> MealPlans { get; set; } = [];
    public ICollection<Appointment> Appointments { get; set; } = [];
    public ICollection<Order> Orders { get; set; } = [];
}
