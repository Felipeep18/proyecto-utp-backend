﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAppiPrueba.Models;

public partial class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? Correo { get; set; }

    public string? Clave { get; set; }
}
