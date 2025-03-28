using System;
using System.ComponentModel.DataAnnotations;


namespace Medicare_API.Models
{
    public class PromocaoDTO
    {
        public required int IdFormaPagamento { get; set; }
        public required int IdColaborador { get; set; } 
        public required int IdRemedio { get; set; }
        public required string Descricao { get; set; }
        public required DateTime DtInicio { get; set; }
        public required DateTime DtFim { get; set; }
        public required double Valor { get; set; }
    }
}
