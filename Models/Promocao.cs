using System;
using System.ComponentModel.DataAnnotations;


namespace Medicare_API.Models
{
    public class Promocao
    {
        [Key]
        public int IdPromocao { get; set; }
        public int IdFormaDePagamento { get; set; }  // Chave estrangeira para FormaPagamento
        public FormaPagamento formaDePagamento { get; set; }  // Relacionamento com FormaPagamento
        public int IdColaborador { get; set; }  // Chave estrangeira para Utilizador (Colaborador)
        public Utilizador Colaborador { get; set; }  // Relacionamento com Utilizador (Colaborador)
        public string Descricao { get; set; }
        public int IdRemedio { get; set; }  // Chave estrangeira para Remedio
        public Remedio remedio { get; set; }  // Relacionamento com Remedio
        public DateTime DtInicio { get; set; }
        public DateTime DtFim { get; set; }
        public double Valor { get; set; }
    }
}
