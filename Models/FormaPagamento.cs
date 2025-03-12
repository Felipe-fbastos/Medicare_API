using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class FormaPagamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int QtdParcelas { get; set; }
        public int QtdMinimaParcelas { get; set; }
        public List<Promocao> Promocoes { get; set; }
    }
}