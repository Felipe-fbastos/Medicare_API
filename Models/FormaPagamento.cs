using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Medicare_API.Models
{
    public class FormaPagamento
    {
        public required int IdFormaPagamento { get; set; }
        public required string Descricao { get; set; }
        public required int QtdParcelas { get; set; }
        public required int QtdMinimaParcelas { get; set; }

        // Relacionamento
        [JsonIgnore]
        public List<Promocao> Promocoes { get; set; } = new();  
    }
}
