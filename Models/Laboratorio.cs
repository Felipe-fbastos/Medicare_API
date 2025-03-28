using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Medicare_API.Models
{
    public class Laboratorio
    {
        public required int IdLaboratorio { get; set; } //PK
        public required string Nome { get; set; }

        // Relacionamento com Remedios
        [JsonIgnore]
        public List<Remedio> Remedios { get; set; } = new();  
    }
}

