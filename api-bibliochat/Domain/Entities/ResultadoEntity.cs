using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_bibliochat.Domain.Entities
{
    public partial class ResultadoEntity
    {
        public bool exito { get; set; }

        public string? message { get; set; }

        [NotMapped]
        public object? data { get; set; }

    }
}
