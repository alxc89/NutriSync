using NutriSync.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace NutriSync.Application.DTOs.Nutritionist;

public class CreateNutritionistDto
{
    public CreateNutritionistDto(string name, string phone, string email,
                string document, string crn, DateTime birthDate,
                Gender gender, string password)
    {
        Name = name;
        Phone = phone;
        Email = email;
        Document = document;
        Crn = crn;
        BirthDate = birthDate;
        Gender = gender;
        Password = password;
    }

    /// <summary>
    /// Nome
    /// </summary>
    /// <example>João da Silva</example>
    [Required(ErrorMessage = "Nome não foi informado, verifique!")]
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Telefone
    /// </summary>
    /// <example>(11) 99999-9999</example>
    [Required(ErrorMessage = "Telefone não foi informado, verifique!")]
    public string Phone { get; set; } = string.Empty;
    /// <summary>
    /// Email
    /// </summary>
    /// <example>joao.silva@nutrisync.com</example>
    [Required(ErrorMessage = "Email não foi informado, verifique!")]
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// Documento
    /// </summary>
    /// <example>123.456.789-00</example>
    [Required(ErrorMessage = "Documento não foi informado, verifique!")]
    public string Document { get; set; } = string.Empty;
    /// <summary>
    /// CRN
    /// </summary>
    /// <example>12345</example>
    [Required(ErrorMessage = "CRN não foi informado, verifique!")]
    public string Crn { get; set; } = string.Empty;
    /// <summary>
    /// Data de nascimento
    /// </summary>
    public DateTime BirthDate { get; set; }
    /// <summary>
    /// Gênero
    /// </summary>
    /// <example>0</example>
    public Gender Gender { get; set; }
    /// <summary>
    /// Senha
    /// </summary>
    /// <example>123456</example>
    [Required(ErrorMessage = "Senha não foi informada, verifique!")]
    public string Password { get; set; } = string.Empty;
}
