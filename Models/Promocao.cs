using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class Promocao
    {
        public int Id { get; set; }
        public int IdFormaDePagamento { get; set; }
        private FormaPagamento formaDePagamento { get; set; }
        public int IdColaborador { get; set; }
        private Utilizador colaborador { get; set; }
        public string Descricao { get; set; }
        public int IdRemedio { get; set; }
        private Remedio remedio { get; set; }
        public DateTime DtInicio { get; set; }
        public DateTime DtFim { get; set; }
        public double Valor { get; set; }
    }
}