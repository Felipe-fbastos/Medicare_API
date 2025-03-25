using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Medicare_API.Models
{
    public class FormaPagamento
    {
        [Key]
        public int IdFormaPagamento { get; set; }
        public string Descricao { get; set; }
        public int QtdParcelas { get; set; }
        public int QtdMinimaParcelas { get; set; }
        public List<Promocao> Promocoes { get; set; } = new();  // Relacionamento com Promocoes
    }
}
