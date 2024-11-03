using System;

namespace WebAppiPrueba.Models.DTOs;

public class LoginDTO
{
    public string Correo { get; set; } = string.Empty;

    public string Contra { get; set; } = string.Empty;
}
