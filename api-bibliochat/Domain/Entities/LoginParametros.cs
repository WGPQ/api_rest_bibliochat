﻿

using System.ComponentModel.DataAnnotations;

namespace api_bibliochat.Domain.Entities
{

    public class LoginParametros
    {
        [Required(ErrorMessage = "El correo es requerido")]
        public string correo { get; set; }

        //[Required(ErrorMessage = "La clave de acceso es requerida")]
        public string clave { get; set; }

    }
}
