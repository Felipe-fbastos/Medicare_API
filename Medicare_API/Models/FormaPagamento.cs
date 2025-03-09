using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class FormaPagamento
    {
        private int Id { get; set; }
        private string Descricao { get; set; }
        private int QtdParcelas { get; set; }
        private int QtdMinimaParcelas { get; set; }
        private List<Promocao> Promocoes { get; set; }
    }
}