using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Medicare_API.Models
{
    public class TipoOrdemGrandeza
    {
        public  int IdTipoOrdemGrandeza { get; set; } // PK
        public required string Descricao { get; set; }
        public required string Simbolos { get; set; }

        // Relacionamento
        [JsonIgnore]
        public List<Remedio> Remedios { get; set; } = new();  
    }
}