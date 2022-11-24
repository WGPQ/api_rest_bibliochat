using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_bibliochat.Domain.Entities
{
    public class SessionEntity
    {
        [Key]
        public string? Id { get; set; }

        public string? id_usuario { get; set; }

        public DateTime? inicio { get; set; }
        public DateTime? fin { get; set; }
    }
}
