using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_bibliochat.Domain.Entities
{
    public partial class UsuarioEntity
    {
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage ="Los nombres son requeridos")]
        public string nombres { get; set; }

        [Required(ErrorMessage = "Los apellidos son requeridos")]
        public string apellidos { get; set; }

        //[Required(ErrorMessage = "El telefono es requerido")]
        public string telefono { get; set; }

        [Required(ErrorMessage = "El correo es requerido")]
        public string correo { get; set; }

        [NotMapped]
        public string clave { get; set; }

        [Required(ErrorMessage = "El rol es requerido")]
        public string rol { get; set; }
        public bool activo { get; set; }
        public bool verificado { get; set; }
        public bool conectado { get; set; }
        public DateTime conectedAt { get; set; }

    }
}
