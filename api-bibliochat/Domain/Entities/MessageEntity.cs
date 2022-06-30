using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_bibliochat.Domain.Entities
{
    public class MessageEntity
    {
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage = "El id chat es requerido")]
        public string chat { get; set; }

        [Required(ErrorMessage = "El usuario es reuqerido")]
        public string usuario { get; set; }

        [Required(ErrorMessage = "El contenido es requerido")]
        public string contenido { get; set; }

        public bool visto { get; set; }

        public DateTime createdAt { get; set; }
    }
}
