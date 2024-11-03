using System;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebAppiPrueba.Models;

namespace WebAppiPrueba.Custom;

public class Utilidades
{

    private readonly IConfiguration _configuration;
    public Utilidades(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string encriptarSHA256(string texto)
    {
        using (var sha256Hash = SHA256.Create())
        {
            // Computar el hash
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));

            // Convertir el array de bytes a string
            var builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("X2"));
            }

            return builder.ToString();
        }
    }

    public string generarJWT(Usuario usuario)
    {
        // Crear la informacion del usuario para el token
        var userClaims = new[]{
            new Claim(ClaimTypes.NameIdentifier,usuario.IdUsuario.ToString()),
            new Claim(ClaimTypes.Email,usuario.Correo!)
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        // Crear detalle del token
        var JwtConfig = new JwtSecurityToken(
            claims: userClaims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(JwtConfig);
    }
}
