using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Medicare_API.Models
{
    public class Parceiro
    {
        public required int IdParceiro { get; set; } //PK
        public required string NomeParceiro { get; set; }
        public required string ApelidoParceiro { get; set; }
        public required string CNPJParceiro { get; set; }

        // Relacionamento
        [JsonIgnore]
        public List<ParceiroUtilizador> ParceiroUtilizador { get; set; } = new();
    }

}