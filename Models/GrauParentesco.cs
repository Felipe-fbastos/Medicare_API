using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Medicare_API.Models
{
    public class GrauParentesco
    {
        public required int IdGrauParentesco { get; set; } //PK
        public required string Descricao { get; set; }

        // Relacionamento 
        [JsonIgnore]
        public List<Responsavel> Responsavel { get; set; } = new();
    }
}