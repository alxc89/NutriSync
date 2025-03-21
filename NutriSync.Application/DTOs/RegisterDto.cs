﻿using NutriSync.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace NutriSync.Application.DTOs;

public class RegisterDto
{
    [Required(ErrorMessage = "Nome não foi informado, verifique!")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "Telefone não foi informado, verifique!")]
    public string Phone { get; set; } = string.Empty;
    [Required(ErrorMessage = "Email não foi informado, verifique!")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Documento não foi informado, verifique!")]
    public string Document { get; set; } = string.Empty;
    [Required(ErrorMessage = "CRN não foi informado, verifique!")]
    public string Crn { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    [Required(ErrorMessage = "Senha não foi informada, verifique!")]
    public string Password { get; set; } = string.Empty;
}
