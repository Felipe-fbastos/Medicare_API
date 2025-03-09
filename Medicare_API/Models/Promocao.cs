using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class Promocao
    {
        private int Id { get; set; }
        private int IdFormaDePagamento { get; set; }
        private FormaPagamento formaDePagamento { get; set; }
        private int IdColaborador { get; set; }
        private Utilizador colaborador { get; set; }
        private string Descricao { get; set; }
        private int IdRemedio { get; set; }
        private Remedio remedio { get; set; }
        private DateTime DtInicio { get; set; }
        private DateTime DtFim { get; set; }
        private double Valor { get; set; }
    }
}